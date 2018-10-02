// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.Utils
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Security.Cryptography
{
  internal static class Utils
  {
    private static object s_InternalSyncObject = new object();
    internal const int DefaultRsaProviderType = 24;
    [SecurityCritical]
    private static volatile SafeProvHandle _safeProvHandle;
    [SecurityCritical]
    private static volatile SafeProvHandle _safeDssProvHandle;
    private static volatile RNGCryptoServiceProvider _rng;

    [SecuritySafeCritical]
    static Utils()
    {
    }

    private static object InternalSyncObject
    {
      get
      {
        return Utils.s_InternalSyncObject;
      }
    }

    internal static SafeProvHandle StaticProvHandle
    {
      [SecurityCritical] get
      {
        if (Utils._safeProvHandle == null)
        {
          lock (Utils.InternalSyncObject)
          {
            if (Utils._safeProvHandle == null)
              Utils._safeProvHandle = Utils.AcquireProvHandle(new CspParameters(24));
          }
        }
        return Utils._safeProvHandle;
      }
    }

    internal static SafeProvHandle StaticDssProvHandle
    {
      [SecurityCritical] get
      {
        if (Utils._safeDssProvHandle == null)
        {
          lock (Utils.InternalSyncObject)
          {
            if (Utils._safeDssProvHandle == null)
              Utils._safeDssProvHandle = Utils.CreateProvHandle(new CspParameters(13), true);
          }
        }
        return Utils._safeDssProvHandle;
      }
    }

    [SecurityCritical]
    internal static SafeProvHandle AcquireProvHandle(CspParameters parameters)
    {
      if (parameters == null)
        parameters = new CspParameters(24);
      SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
      Utils._AcquireCSP(parameters, ref invalidHandle);
      return invalidHandle;
    }

    [SecurityCritical]
    internal static SafeProvHandle CreateProvHandle(CspParameters parameters, bool randomKeyContainer)
    {
      SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
      int hr = Utils._OpenCSP(parameters, 0U, ref invalidHandle);
      KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
      if (hr != 0)
      {
        if ((parameters.Flags & CspProviderFlags.UseExistingKey) != CspProviderFlags.NoFlags || hr != -2146893799 && hr != -2146893802 && hr != -2147024894)
          throw new CryptographicException(hr);
        if (!randomKeyContainer && !CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
        {
          KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(parameters, KeyContainerPermissionFlags.Create);
          containerPermission.AccessEntries.Add(accessEntry);
          containerPermission.Demand();
        }
        Utils._CreateCSP(parameters, randomKeyContainer, ref invalidHandle);
      }
      else if (!randomKeyContainer && !CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
      {
        KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(parameters, KeyContainerPermissionFlags.Open);
        containerPermission.AccessEntries.Add(accessEntry);
        containerPermission.Demand();
      }
      return invalidHandle;
    }

    [SecurityCritical]
    internal static CryptoKeySecurity GetKeySetSecurityInfo(SafeProvHandle hProv, AccessControlSections accessControlSections)
    {
      SecurityInfos securityInfo = (SecurityInfos) 0;
      Privilege privilege = (Privilege) null;
      if ((accessControlSections & AccessControlSections.Owner) != AccessControlSections.None)
        securityInfo |= SecurityInfos.Owner;
      if ((accessControlSections & AccessControlSections.Group) != AccessControlSections.None)
        securityInfo |= SecurityInfos.Group;
      if ((accessControlSections & AccessControlSections.Access) != AccessControlSections.None)
        securityInfo |= SecurityInfos.DiscretionaryAcl;
      byte[] binaryForm = (byte[]) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      int error;
      try
      {
        if ((accessControlSections & AccessControlSections.Audit) != AccessControlSections.None)
        {
          securityInfo |= SecurityInfos.SystemAcl;
          privilege = new Privilege("SeSecurityPrivilege");
          privilege.Enable();
        }
        binaryForm = Utils._GetKeySetSecurityInfo(hProv, securityInfo, out error);
      }
      finally
      {
        privilege?.Revert();
      }
      if (error == 0 && (binaryForm == null || binaryForm.Length == 0))
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NoSecurityDescriptor"));
      if (error == 8)
        throw new OutOfMemoryException();
      if (error == 5)
        throw new UnauthorizedAccessException();
      if (error == 1314)
        throw new PrivilegeNotHeldException("SeSecurityPrivilege");
      if (error != 0)
        throw new CryptographicException(error);
      return new CryptoKeySecurity(new CommonSecurityDescriptor(false, false, new RawSecurityDescriptor(binaryForm, 0), true));
    }

    [SecurityCritical]
    internal static void SetKeySetSecurityInfo(SafeProvHandle hProv, CryptoKeySecurity cryptoKeySecurity, AccessControlSections accessControlSections)
    {
      SecurityInfos securityInfo = (SecurityInfos) 0;
      Privilege privilege = (Privilege) null;
      if ((accessControlSections & AccessControlSections.Owner) != AccessControlSections.None && cryptoKeySecurity._securityDescriptor.Owner != (SecurityIdentifier) null)
        securityInfo |= SecurityInfos.Owner;
      if ((accessControlSections & AccessControlSections.Group) != AccessControlSections.None && cryptoKeySecurity._securityDescriptor.Group != (SecurityIdentifier) null)
        securityInfo |= SecurityInfos.Group;
      if ((accessControlSections & AccessControlSections.Audit) != AccessControlSections.None)
        securityInfo |= SecurityInfos.SystemAcl;
      if ((accessControlSections & AccessControlSections.Access) != AccessControlSections.None && cryptoKeySecurity._securityDescriptor.IsDiscretionaryAclPresent)
        securityInfo |= SecurityInfos.DiscretionaryAcl;
      if (securityInfo == (SecurityInfos) 0)
        return;
      int hr = 0;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        if ((securityInfo & SecurityInfos.SystemAcl) != (SecurityInfos) 0)
        {
          privilege = new Privilege("SeSecurityPrivilege");
          privilege.Enable();
        }
        byte[] descriptorBinaryForm = cryptoKeySecurity.GetSecurityDescriptorBinaryForm();
        if (descriptorBinaryForm != null)
        {
          if (descriptorBinaryForm.Length != 0)
            hr = Utils.SetKeySetSecurityInfo(hProv, securityInfo, descriptorBinaryForm);
        }
      }
      finally
      {
        privilege?.Revert();
      }
      if (hr == 5 || hr == 1307 || hr == 1308)
        throw new UnauthorizedAccessException();
      if (hr == 1314)
        throw new PrivilegeNotHeldException("SeSecurityPrivilege");
      if (hr == 6)
        throw new NotSupportedException(Environment.GetResourceString("AccessControl_InvalidHandle"));
      if (hr != 0)
        throw new CryptographicException(hr);
    }

    [SecurityCritical]
    internal static byte[] ExportCspBlobHelper(bool includePrivateParameters, CspParameters parameters, SafeKeyHandle safeKeyHandle)
    {
      if (includePrivateParameters && !CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
      {
        KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
        KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(parameters, KeyContainerPermissionFlags.Export);
        containerPermission.AccessEntries.Add(accessEntry);
        containerPermission.Demand();
      }
      byte[] o = (byte[]) null;
      Utils.ExportCspBlob(safeKeyHandle, includePrivateParameters ? 7 : 6, JitHelpers.GetObjectHandleOnStack<byte[]>(ref o));
      return o;
    }

    [SecuritySafeCritical]
    internal static unsafe void GetKeyPairHelper(CspAlgorithmType keyType, CspParameters parameters, bool randomKeyContainer, int dwKeySize, ref SafeProvHandle safeProvHandle, ref SafeKeyHandle safeKeyHandle)
    {
      SafeProvHandle provHandle = Utils.CreateProvHandle(parameters, randomKeyContainer);
      if (parameters.CryptoKeySecurity != null)
      {
        KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
        KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(parameters, KeyContainerPermissionFlags.ChangeAcl);
        containerPermission.AccessEntries.Add(accessEntry);
        containerPermission.Demand();
        Utils.SetKeySetSecurityInfo(provHandle, parameters.CryptoKeySecurity, parameters.CryptoKeySecurity.ChangedAccessControlSections);
      }
      if (parameters.ParentWindowHandle != IntPtr.Zero)
      {
        IntPtr parentWindowHandle = parameters.ParentWindowHandle;
        IntPtr pbData = parentWindowHandle;
        if (!AppContextSwitches.DoNotAddrOfCspParentWindowHandle)
          pbData = new IntPtr((void*) &parentWindowHandle);
        Utils.SetProviderParameter(provHandle, parameters.KeyNumber, 10U, pbData);
      }
      else if (parameters.KeyPassword != null)
      {
        IntPtr coTaskMemAnsi = Marshal.SecureStringToCoTaskMemAnsi(parameters.KeyPassword);
        try
        {
          Utils.SetProviderParameter(provHandle, parameters.KeyNumber, 11U, coTaskMemAnsi);
        }
        finally
        {
          if (coTaskMemAnsi != IntPtr.Zero)
            Marshal.ZeroFreeCoTaskMemAnsi(coTaskMemAnsi);
        }
      }
      safeProvHandle = provHandle;
      SafeKeyHandle invalidHandle = SafeKeyHandle.InvalidHandle;
      int userKey = Utils._GetUserKey(safeProvHandle, parameters.KeyNumber, ref invalidHandle);
      if (userKey != 0)
      {
        if ((parameters.Flags & CspProviderFlags.UseExistingKey) != CspProviderFlags.NoFlags || userKey != -2146893811)
          throw new CryptographicException(userKey);
        Utils._GenerateKey(safeProvHandle, parameters.KeyNumber, parameters.Flags, dwKeySize, ref invalidHandle);
      }
      byte[] keyParameter = Utils._GetKeyParameter(invalidHandle, 9U);
      int num = (int) keyParameter[0] | (int) keyParameter[1] << 8 | (int) keyParameter[2] << 16 | (int) keyParameter[3] << 24;
      if (keyType == CspAlgorithmType.Rsa && num != 41984 && num != 9216 || keyType == CspAlgorithmType.Dss && num != 8704)
      {
        invalidHandle.Dispose();
        throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_WrongKeySpec"));
      }
      safeKeyHandle = invalidHandle;
    }

    [SecurityCritical]
    internal static void ImportCspBlobHelper(CspAlgorithmType keyType, byte[] keyBlob, bool publicOnly, ref CspParameters parameters, bool randomKeyContainer, ref SafeProvHandle safeProvHandle, ref SafeKeyHandle safeKeyHandle)
    {
      if (safeKeyHandle != null && !safeKeyHandle.IsClosed)
        safeKeyHandle.Dispose();
      safeKeyHandle = SafeKeyHandle.InvalidHandle;
      if (publicOnly)
      {
        parameters.KeyNumber = Utils._ImportCspBlob(keyBlob, keyType == CspAlgorithmType.Dss ? Utils.StaticDssProvHandle : Utils.StaticProvHandle, CspProviderFlags.NoFlags, ref safeKeyHandle);
      }
      else
      {
        if (!CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
        {
          KeyContainerPermission containerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
          KeyContainerPermissionAccessEntry accessEntry = new KeyContainerPermissionAccessEntry(parameters, KeyContainerPermissionFlags.Import);
          containerPermission.AccessEntries.Add(accessEntry);
          containerPermission.Demand();
        }
        if (safeProvHandle == null)
          safeProvHandle = Utils.CreateProvHandle(parameters, randomKeyContainer);
        parameters.KeyNumber = Utils._ImportCspBlob(keyBlob, safeProvHandle, parameters.Flags, ref safeKeyHandle);
      }
    }

    [SecurityCritical]
    internal static CspParameters SaveCspParameters(CspAlgorithmType keyType, CspParameters userParameters, CspProviderFlags defaultFlags, ref bool randomKeyContainer)
    {
      CspParameters cspParameters;
      if (userParameters == null)
      {
        cspParameters = new CspParameters(keyType == CspAlgorithmType.Dss ? 13 : 24, (string) null, (string) null, defaultFlags);
      }
      else
      {
        Utils.ValidateCspFlags(userParameters.Flags);
        cspParameters = new CspParameters(userParameters);
      }
      if (cspParameters.KeyNumber == -1)
        cspParameters.KeyNumber = keyType == CspAlgorithmType.Dss ? 2 : 1;
      else if (cspParameters.KeyNumber == 8704 || cspParameters.KeyNumber == 9216)
        cspParameters.KeyNumber = 2;
      else if (cspParameters.KeyNumber == 41984)
        cspParameters.KeyNumber = 1;
      randomKeyContainer = (cspParameters.Flags & CspProviderFlags.CreateEphemeralKey) == CspProviderFlags.CreateEphemeralKey;
      if (cspParameters.KeyContainerName == null && (cspParameters.Flags & CspProviderFlags.UseDefaultKeyContainer) == CspProviderFlags.NoFlags)
      {
        cspParameters.Flags |= CspProviderFlags.CreateEphemeralKey;
        randomKeyContainer = true;
      }
      return cspParameters;
    }

    [SecurityCritical]
    private static void ValidateCspFlags(CspProviderFlags flags)
    {
      if ((flags & CspProviderFlags.UseExistingKey) != CspProviderFlags.NoFlags)
      {
        CspProviderFlags cspProviderFlags = CspProviderFlags.UseNonExportableKey | CspProviderFlags.UseArchivableKey | CspProviderFlags.UseUserProtectedKey;
        if ((flags & cspProviderFlags) != CspProviderFlags.NoFlags)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"));
      }
      if ((flags & CspProviderFlags.UseUserProtectedKey) == CspProviderFlags.NoFlags)
        return;
      if (!Environment.UserInteractive)
        throw new InvalidOperationException(Environment.GetResourceString("Cryptography_NotInteractive"));
      new UIPermission(UIPermissionWindow.SafeTopLevelWindows).Demand();
    }

    internal static RNGCryptoServiceProvider StaticRandomNumberGenerator
    {
      get
      {
        if (Utils._rng == null)
          Utils._rng = new RNGCryptoServiceProvider();
        return Utils._rng;
      }
    }

    internal static byte[] GenerateRandom(int keySize)
    {
      byte[] data = new byte[keySize];
      Utils.StaticRandomNumberGenerator.GetBytes(data);
      return data;
    }

    [SecurityCritical]
    [RegistryPermission(SecurityAction.Assert, Read = "HKEY_LOCAL_MACHINE\\System\\CurrentControlSet\\Control\\Lsa")]
    internal static bool ReadLegacyFipsPolicy()
    {
      try
      {
        using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Lsa", false))
        {
          if (registryKey == null)
            return false;
          object obj = registryKey.GetValue("FIPSAlgorithmPolicy");
          if (obj == null)
            return false;
          if (registryKey.GetValueKind("FIPSAlgorithmPolicy") != RegistryValueKind.DWord)
            return true;
          return (uint) (int) obj > 0U;
        }
      }
      catch (SecurityException ex)
      {
        return true;
      }
    }

    [SecurityCritical]
    internal static bool HasAlgorithm(int dwCalg, int dwKeySize)
    {
      lock (Utils.InternalSyncObject)
        return Utils.SearchForAlgorithm(Utils.StaticProvHandle, dwCalg, dwKeySize);
    }

    internal static int ObjToAlgId(object hashAlg, OidGroup group)
    {
      if (hashAlg == null)
        throw new ArgumentNullException(nameof (hashAlg));
      string oid = (string) null;
      string name = hashAlg as string;
      if (name != null)
        oid = CryptoConfig.MapNameToOID(name, group) ?? name;
      else if (hashAlg is HashAlgorithm)
        oid = CryptoConfig.MapNameToOID(hashAlg.GetType().ToString(), group);
      else if ((object) (hashAlg as Type) != null)
        oid = CryptoConfig.MapNameToOID(hashAlg.ToString(), group);
      if (oid == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
      return X509Utils.GetAlgIdFromOid(oid, group);
    }

    internal static HashAlgorithm ObjToHashAlgorithm(object hashAlg)
    {
      if (hashAlg == null)
        throw new ArgumentNullException(nameof (hashAlg));
      HashAlgorithm hashAlgorithm = (HashAlgorithm) null;
      if (hashAlg is string)
      {
        hashAlgorithm = (HashAlgorithm) CryptoConfig.CreateFromName((string) hashAlg);
        if (hashAlgorithm == null)
        {
          string friendlyNameFromOid = X509Utils.GetFriendlyNameFromOid((string) hashAlg, OidGroup.HashAlgorithm);
          if (friendlyNameFromOid != null)
            hashAlgorithm = (HashAlgorithm) CryptoConfig.CreateFromName(friendlyNameFromOid);
        }
      }
      else if (hashAlg is HashAlgorithm)
        hashAlgorithm = (HashAlgorithm) hashAlg;
      else if ((object) (hashAlg as Type) != null)
        hashAlgorithm = (HashAlgorithm) CryptoConfig.CreateFromName(hashAlg.ToString());
      if (hashAlgorithm == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidValue"));
      return hashAlgorithm;
    }

    internal static string DiscardWhiteSpaces(string inputBuffer)
    {
      return Utils.DiscardWhiteSpaces(inputBuffer, 0, inputBuffer.Length);
    }

    internal static string DiscardWhiteSpaces(string inputBuffer, int inputOffset, int inputCount)
    {
      int num1 = 0;
      for (int index = 0; index < inputCount; ++index)
      {
        if (char.IsWhiteSpace(inputBuffer[inputOffset + index]))
          ++num1;
      }
      char[] chArray = new char[inputCount - num1];
      int num2 = 0;
      for (int index = 0; index < inputCount; ++index)
      {
        if (!char.IsWhiteSpace(inputBuffer[inputOffset + index]))
          chArray[num2++] = inputBuffer[inputOffset + index];
      }
      return new string(chArray);
    }

    internal static int ConvertByteArrayToInt(byte[] input)
    {
      int num = 0;
      for (int index = 0; index < input.Length; ++index)
        num = num * 256 + (int) input[index];
      return num;
    }

    internal static byte[] ConvertIntToByteArray(int dwInput)
    {
      byte[] numArray1 = new byte[8];
      int length = 0;
      if (dwInput == 0)
        return new byte[1];
      int num1 = dwInput;
      while (num1 > 0)
      {
        int num2 = num1 % 256;
        numArray1[length] = (byte) num2;
        num1 = (num1 - num2) / 256;
        ++length;
      }
      byte[] numArray2 = new byte[length];
      for (int index = 0; index < length; ++index)
        numArray2[index] = numArray1[length - index - 1];
      return numArray2;
    }

    internal static void ConvertIntToByteArray(uint dwInput, ref byte[] counter)
    {
      uint num1 = dwInput;
      int num2 = 0;
      Array.Clear((Array) counter, 0, counter.Length);
      if (dwInput == 0U)
        return;
      while (num1 > 0U)
      {
        uint num3 = num1 % 256U;
        counter[3 - num2] = (byte) num3;
        num1 = (num1 - num3) / 256U;
        ++num2;
      }
    }

    internal static byte[] FixupKeyParity(byte[] key)
    {
      byte[] numArray = new byte[key.Length];
      for (int index = 0; index < key.Length; ++index)
      {
        numArray[index] = (byte) ((uint) key[index] & 254U);
        byte num1 = (byte) ((int) numArray[index] & 15 ^ (int) numArray[index] >> 4);
        byte num2 = (byte) ((int) num1 & 3 ^ (int) num1 >> 2);
        if ((byte) ((int) num2 & 1 ^ (int) num2 >> 1) == (byte) 0)
          numArray[index] |= (byte) 1;
      }
      return numArray;
    }

    [SecurityCritical]
    internal static unsafe void DWORDFromLittleEndian(uint* x, int digits, byte* block)
    {
      int index1 = 0;
      int index2 = 0;
      while (index1 < digits)
      {
        x[index1] = (uint) ((int) block[index2] | (int) block[index2 + 1] << 8 | (int) block[index2 + 2] << 16 | (int) block[index2 + 3] << 24);
        ++index1;
        index2 += 4;
      }
    }

    internal static void DWORDToLittleEndian(byte[] block, uint[] x, int digits)
    {
      int index1 = 0;
      int index2 = 0;
      while (index1 < digits)
      {
        block[index2] = (byte) (x[index1] & (uint) byte.MaxValue);
        block[index2 + 1] = (byte) (x[index1] >> 8 & (uint) byte.MaxValue);
        block[index2 + 2] = (byte) (x[index1] >> 16 & (uint) byte.MaxValue);
        block[index2 + 3] = (byte) (x[index1] >> 24 & (uint) byte.MaxValue);
        ++index1;
        index2 += 4;
      }
    }

    [SecurityCritical]
    internal static unsafe void DWORDFromBigEndian(uint* x, int digits, byte* block)
    {
      int index1 = 0;
      int index2 = 0;
      while (index1 < digits)
      {
        x[index1] = (uint) ((int) block[index2] << 24 | (int) block[index2 + 1] << 16 | (int) block[index2 + 2] << 8) | (uint) block[index2 + 3];
        ++index1;
        index2 += 4;
      }
    }

    internal static void DWORDToBigEndian(byte[] block, uint[] x, int digits)
    {
      int index1 = 0;
      int index2 = 0;
      while (index1 < digits)
      {
        block[index2] = (byte) (x[index1] >> 24 & (uint) byte.MaxValue);
        block[index2 + 1] = (byte) (x[index1] >> 16 & (uint) byte.MaxValue);
        block[index2 + 2] = (byte) (x[index1] >> 8 & (uint) byte.MaxValue);
        block[index2 + 3] = (byte) (x[index1] & (uint) byte.MaxValue);
        ++index1;
        index2 += 4;
      }
    }

    [SecurityCritical]
    internal static unsafe void QuadWordFromBigEndian(ulong* x, int digits, byte* block)
    {
      int index1 = 0;
      int index2 = 0;
      while (index1 < digits)
      {
        x[index1] = (ulong) ((long) block[index2] << 56 | (long) block[index2 + 1] << 48 | (long) block[index2 + 2] << 40 | (long) block[index2 + 3] << 32 | (long) block[index2 + 4] << 24 | (long) block[index2 + 5] << 16 | (long) block[index2 + 6] << 8) | (ulong) block[index2 + 7];
        ++index1;
        index2 += 8;
      }
    }

    internal static void QuadWordToBigEndian(byte[] block, ulong[] x, int digits)
    {
      int index1 = 0;
      int index2 = 0;
      while (index1 < digits)
      {
        block[index2] = (byte) (x[index1] >> 56 & (ulong) byte.MaxValue);
        block[index2 + 1] = (byte) (x[index1] >> 48 & (ulong) byte.MaxValue);
        block[index2 + 2] = (byte) (x[index1] >> 40 & (ulong) byte.MaxValue);
        block[index2 + 3] = (byte) (x[index1] >> 32 & (ulong) byte.MaxValue);
        block[index2 + 4] = (byte) (x[index1] >> 24 & (ulong) byte.MaxValue);
        block[index2 + 5] = (byte) (x[index1] >> 16 & (ulong) byte.MaxValue);
        block[index2 + 6] = (byte) (x[index1] >> 8 & (ulong) byte.MaxValue);
        block[index2 + 7] = (byte) (x[index1] & (ulong) byte.MaxValue);
        ++index1;
        index2 += 8;
      }
    }

    internal static byte[] Int(uint i)
    {
      return new byte[4]
      {
        (byte) (i >> 24),
        (byte) (i >> 16),
        (byte) (i >> 8),
        (byte) i
      };
    }

    [SecurityCritical]
    internal static byte[] RsaOaepEncrypt(RSA rsa, HashAlgorithm hash, PKCS1MaskGenerationMethod mgf, RandomNumberGenerator rng, byte[] data)
    {
      int length1 = rsa.KeySize / 8;
      int length2 = hash.HashSize / 8;
      if (data.Length + 2 + 2 * length2 > length1)
        throw new CryptographicException(string.Format((IFormatProvider) null, Environment.GetResourceString("Cryptography_Padding_EncDataTooBig"), (object) (length1 - 2 - 2 * length2)));
      hash.ComputeHash(EmptyArray<byte>.Value);
      byte[] rgbSeed = new byte[length1 - length2];
      Buffer.InternalBlockCopy((Array) hash.Hash, 0, (Array) rgbSeed, 0, length2);
      rgbSeed[rgbSeed.Length - data.Length - 1] = (byte) 1;
      Buffer.InternalBlockCopy((Array) data, 0, (Array) rgbSeed, rgbSeed.Length - data.Length, data.Length);
      byte[] numArray = new byte[length2];
      rng.GetBytes(numArray);
      byte[] mask1 = mgf.GenerateMask(numArray, rgbSeed.Length);
      for (int index = 0; index < rgbSeed.Length; ++index)
        rgbSeed[index] = (byte) ((uint) rgbSeed[index] ^ (uint) mask1[index]);
      byte[] mask2 = mgf.GenerateMask(rgbSeed, length2);
      for (int index = 0; index < numArray.Length; ++index)
        numArray[index] ^= mask2[index];
      byte[] rgb = new byte[length1];
      Buffer.InternalBlockCopy((Array) numArray, 0, (Array) rgb, 0, numArray.Length);
      Buffer.InternalBlockCopy((Array) rgbSeed, 0, (Array) rgb, numArray.Length, rgbSeed.Length);
      return rsa.EncryptValue(rgb);
    }

    [SecurityCritical]
    internal static byte[] RsaOaepDecrypt(RSA rsa, HashAlgorithm hash, PKCS1MaskGenerationMethod mgf, byte[] encryptedData)
    {
      int num = rsa.KeySize / 8;
      byte[] numArray1;
      try
      {
        numArray1 = rsa.DecryptValue(encryptedData);
      }
      catch (CryptographicException ex)
      {
        throw new CryptographicException(Environment.GetResourceString("Cryptography_OAEPDecoding"));
      }
      int length = hash.HashSize / 8;
      int dstOffsetBytes = num - numArray1.Length;
      if (dstOffsetBytes < 0 || dstOffsetBytes >= length)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_OAEPDecoding"));
      byte[] rgbSeed1 = new byte[length];
      Buffer.InternalBlockCopy((Array) numArray1, 0, (Array) rgbSeed1, dstOffsetBytes, rgbSeed1.Length - dstOffsetBytes);
      byte[] rgbSeed2 = new byte[numArray1.Length - rgbSeed1.Length + dstOffsetBytes];
      Buffer.InternalBlockCopy((Array) numArray1, rgbSeed1.Length - dstOffsetBytes, (Array) rgbSeed2, 0, rgbSeed2.Length);
      byte[] mask1 = mgf.GenerateMask(rgbSeed2, rgbSeed1.Length);
      for (int index = 0; index < rgbSeed1.Length; ++index)
        rgbSeed1[index] ^= mask1[index];
      byte[] mask2 = mgf.GenerateMask(rgbSeed1, rgbSeed2.Length);
      for (int index = 0; index < rgbSeed2.Length; ++index)
        rgbSeed2[index] = (byte) ((uint) rgbSeed2[index] ^ (uint) mask2[index]);
      hash.ComputeHash(EmptyArray<byte>.Value);
      byte[] hash1 = hash.Hash;
      int index1;
      for (index1 = 0; index1 < length; ++index1)
      {
        if ((int) rgbSeed2[index1] != (int) hash1[index1])
          throw new CryptographicException(Environment.GetResourceString("Cryptography_OAEPDecoding"));
      }
      for (; index1 < rgbSeed2.Length && rgbSeed2[index1] != (byte) 1; ++index1)
      {
        if (rgbSeed2[index1] != (byte) 0)
          throw new CryptographicException(Environment.GetResourceString("Cryptography_OAEPDecoding"));
      }
      if (index1 == rgbSeed2.Length)
        throw new CryptographicException(Environment.GetResourceString("Cryptography_OAEPDecoding"));
      int srcOffsetBytes = index1 + 1;
      byte[] numArray2 = new byte[rgbSeed2.Length - srcOffsetBytes];
      Buffer.InternalBlockCopy((Array) rgbSeed2, srcOffsetBytes, (Array) numArray2, 0, numArray2.Length);
      return numArray2;
    }

    [SecurityCritical]
    internal static byte[] RsaPkcs1Padding(RSA rsa, byte[] oid, byte[] hash)
    {
      int length = rsa.KeySize / 8;
      byte[] numArray1 = new byte[length];
      byte[] numArray2 = new byte[oid.Length + 8 + hash.Length];
      numArray2[0] = (byte) 48;
      int num1 = numArray2.Length - 2;
      numArray2[1] = (byte) num1;
      numArray2[2] = (byte) 48;
      int num2 = oid.Length + 2;
      numArray2[3] = (byte) num2;
      Buffer.InternalBlockCopy((Array) oid, 0, (Array) numArray2, 4, oid.Length);
      numArray2[4 + oid.Length] = (byte) 5;
      numArray2[4 + oid.Length + 1] = (byte) 0;
      numArray2[4 + oid.Length + 2] = (byte) 4;
      numArray2[4 + oid.Length + 3] = (byte) hash.Length;
      Buffer.InternalBlockCopy((Array) hash, 0, (Array) numArray2, oid.Length + 8, hash.Length);
      int dstOffsetBytes = length - numArray2.Length;
      if (dstOffsetBytes <= 2)
        throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Cryptography_InvalidOID"));
      numArray1[0] = (byte) 0;
      numArray1[1] = (byte) 1;
      for (int index = 2; index < dstOffsetBytes - 1; ++index)
        numArray1[index] = byte.MaxValue;
      numArray1[dstOffsetBytes - 1] = (byte) 0;
      Buffer.InternalBlockCopy((Array) numArray2, 0, (Array) numArray1, dstOffsetBytes, numArray2.Length);
      return numArray1;
    }

    internal static bool CompareBigIntArrays(byte[] lhs, byte[] rhs)
    {
      if (lhs == null)
        return rhs == null;
      int index1 = 0;
      int index2 = 0;
      while (index1 < lhs.Length && lhs[index1] == (byte) 0)
        ++index1;
      while (index2 < rhs.Length && rhs[index2] == (byte) 0)
        ++index2;
      int num = lhs.Length - index1;
      if (rhs.Length - index2 != num)
        return false;
      for (int index3 = 0; index3 < num; ++index3)
      {
        if ((int) lhs[index1 + index3] != (int) rhs[index2 + index3])
          return false;
      }
      return true;
    }

    internal static HashAlgorithmName OidToHashAlgorithmName(string oid)
    {
      if (oid == "1.3.14.3.2.26")
        return HashAlgorithmName.SHA1;
      if (oid == "2.16.840.1.101.3.4.2.1")
        return HashAlgorithmName.SHA256;
      if (oid == "2.16.840.1.101.3.4.2.2")
        return HashAlgorithmName.SHA384;
      if (oid == "2.16.840.1.101.3.4.2.3")
        return HashAlgorithmName.SHA512;
      throw new NotSupportedException();
    }

    internal static bool DoesRsaKeyOverride(RSA rsaKey, string methodName, Type[] parameterTypes)
    {
      Type type = rsaKey.GetType();
      if (rsaKey is RSACryptoServiceProvider || type.FullName == "System.Security.Cryptography.RSACng")
        return true;
      return Utils.DoesRsaKeyOverrideSlowPath(type, methodName, parameterTypes);
    }

    private static bool DoesRsaKeyOverrideSlowPath(Type t, string methodName, Type[] parameterTypes)
    {
      return !(t.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public, (Binder) null, parameterTypes, (ParameterModifier[]) null).DeclaringType == typeof (RSA));
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern SafeHashHandle CreateHash(SafeProvHandle hProv, int algid);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void EndHash(SafeHashHandle hHash, ObjectHandleOnStack retHash);

    [SecurityCritical]
    internal static byte[] EndHash(SafeHashHandle hHash)
    {
      byte[] o = (byte[]) null;
      Utils.EndHash(hHash, JitHelpers.GetObjectHandleOnStack<byte[]>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void ExportCspBlob(SafeKeyHandle hKey, int blobType, ObjectHandleOnStack retBlob);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern bool GetPersistKeyInCsp(SafeProvHandle hProv);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void HashData(SafeHashHandle hHash, byte[] data, int cbData, int ibStart, int cbSize);

    [SecurityCritical]
    internal static void HashData(SafeHashHandle hHash, byte[] data, int ibStart, int cbSize)
    {
      Utils.HashData(hHash, data, data.Length, ibStart, cbSize);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern bool SearchForAlgorithm(SafeProvHandle hProv, int algID, int keyLength);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void SetKeyParamDw(SafeKeyHandle hKey, int param, int dwValue);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void SetKeyParamRgb(SafeKeyHandle hKey, int param, byte[] value, int cbValue);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int SetKeySetSecurityInfo(SafeProvHandle hProv, SecurityInfos securityInfo, byte[] sd);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void SetPersistKeyInCsp(SafeProvHandle hProv, bool fPersistKeyInCsp);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void SetProviderParameter(SafeProvHandle hProv, int keyNumber, uint paramID, IntPtr pbData);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SignValue(SafeKeyHandle hKey, int keyNumber, int calgKey, int calgHash, byte[] hash, int cbHash, ObjectHandleOnStack retSignature);

    [SecurityCritical]
    internal static byte[] SignValue(SafeKeyHandle hKey, int keyNumber, int calgKey, int calgHash, byte[] hash)
    {
      byte[] o = (byte[]) null;
      Utils.SignValue(hKey, keyNumber, calgKey, calgHash, hash, hash.Length, JitHelpers.GetObjectHandleOnStack<byte[]>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern bool VerifySign(SafeKeyHandle hKey, int calgKey, int calgHash, byte[] hash, int cbHash, byte[] signature, int cbSignature);

    [SecurityCritical]
    internal static bool VerifySign(SafeKeyHandle hKey, int calgKey, int calgHash, byte[] hash, byte[] signature)
    {
      return Utils.VerifySign(hKey, calgKey, calgHash, hash, hash.Length, signature, signature.Length);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void _CreateCSP(CspParameters param, bool randomKeyContainer, ref SafeProvHandle hProv);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int _DecryptData(SafeKeyHandle hKey, byte[] data, int ib, int cb, ref byte[] outputBuffer, int outputOffset, PaddingMode PaddingMode, bool fDone);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int _EncryptData(SafeKeyHandle hKey, byte[] data, int ib, int cb, ref byte[] outputBuffer, int outputOffset, PaddingMode PaddingMode, bool fDone);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void _ExportKey(SafeKeyHandle hKey, int blobType, object cspObject);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void _GenerateKey(SafeProvHandle hProv, int algid, CspProviderFlags flags, int keySize, ref SafeKeyHandle hKey);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool _GetEnforceFipsPolicySetting();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern byte[] _GetKeyParameter(SafeKeyHandle hKey, uint paramID);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern byte[] _GetKeySetSecurityInfo(SafeProvHandle hProv, SecurityInfos securityInfo, out int error);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object _GetProviderParameter(SafeProvHandle hProv, int keyNumber, uint paramID);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int _GetUserKey(SafeProvHandle hProv, int keyNumber, ref SafeKeyHandle hKey);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void _ImportBulkKey(SafeProvHandle hProv, int algid, bool useSalt, byte[] key, ref SafeKeyHandle hKey);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int _ImportCspBlob(byte[] keyBlob, SafeProvHandle hProv, CspProviderFlags flags, ref SafeKeyHandle hKey);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void _ImportKey(SafeProvHandle hCSP, int keyNumber, CspProviderFlags flags, object cspObject, ref SafeKeyHandle hKey);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool _ProduceLegacyHmacValues();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int _OpenCSP(CspParameters param, uint flags, ref SafeProvHandle hProv);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void _AcquireCSP(CspParameters param, ref SafeProvHandle hProv);
  }
}

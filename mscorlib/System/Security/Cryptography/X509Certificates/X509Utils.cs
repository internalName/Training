// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.X509Certificates.X509Utils
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
  internal static class X509Utils
  {
    private static bool OidGroupWillNotUseActiveDirectory(OidGroup group)
    {
      if (group != OidGroup.HashAlgorithm && group != OidGroup.EncryptionAlgorithm && (group != OidGroup.PublicKeyAlgorithm && group != OidGroup.SignatureAlgorithm) && (group != OidGroup.Attribute && group != OidGroup.ExtensionOrAttribute))
        return group == OidGroup.KeyDerivationFunction;
      return true;
    }

    [SecurityCritical]
    private static CRYPT_OID_INFO FindOidInfo(OidKeyType keyType, string key, OidGroup group)
    {
      IntPtr num = IntPtr.Zero;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        num = keyType != OidKeyType.Oid ? Marshal.StringToCoTaskMemUni(key) : Marshal.StringToCoTaskMemAnsi(key);
        if (!X509Utils.OidGroupWillNotUseActiveDirectory(group))
        {
          OidGroup dwGroupId = group | OidGroup.DisableSearchDS;
          IntPtr oidInfo = X509Utils.CryptFindOIDInfo(keyType, num, dwGroupId);
          if (oidInfo != IntPtr.Zero)
            return (CRYPT_OID_INFO) Marshal.PtrToStructure(oidInfo, typeof (CRYPT_OID_INFO));
        }
        IntPtr oidInfo1 = X509Utils.CryptFindOIDInfo(keyType, num, group);
        if (oidInfo1 != IntPtr.Zero)
          return (CRYPT_OID_INFO) Marshal.PtrToStructure(oidInfo1, typeof (CRYPT_OID_INFO));
        if (group != OidGroup.AllGroups)
        {
          IntPtr oidInfo2 = X509Utils.CryptFindOIDInfo(keyType, num, OidGroup.AllGroups);
          if (oidInfo2 != IntPtr.Zero)
            return (CRYPT_OID_INFO) Marshal.PtrToStructure(oidInfo2, typeof (CRYPT_OID_INFO));
        }
        return new CRYPT_OID_INFO();
      }
      finally
      {
        if (num != IntPtr.Zero)
          Marshal.FreeCoTaskMem(num);
      }
    }

    [SecuritySafeCritical]
    internal static int GetAlgIdFromOid(string oid, OidGroup oidGroup)
    {
      if (string.Equals(oid, "2.16.840.1.101.3.4.2.1", StringComparison.Ordinal))
        return 32780;
      if (string.Equals(oid, "2.16.840.1.101.3.4.2.2", StringComparison.Ordinal))
        return 32781;
      if (string.Equals(oid, "2.16.840.1.101.3.4.2.3", StringComparison.Ordinal))
        return 32782;
      return X509Utils.FindOidInfo(OidKeyType.Oid, oid, oidGroup).AlgId;
    }

    [SecuritySafeCritical]
    internal static string GetFriendlyNameFromOid(string oid, OidGroup oidGroup)
    {
      return X509Utils.FindOidInfo(OidKeyType.Oid, oid, oidGroup).pwszName;
    }

    [SecuritySafeCritical]
    internal static string GetOidFromFriendlyName(string friendlyName, OidGroup oidGroup)
    {
      return X509Utils.FindOidInfo(OidKeyType.Name, friendlyName, oidGroup).pszOID;
    }

    internal static int NameOrOidToAlgId(string oid, OidGroup oidGroup)
    {
      if (oid == null)
        return 32772;
      int algIdFromOid = X509Utils.GetAlgIdFromOid(CryptoConfig.MapNameToOID(oid, oidGroup) ?? oid, oidGroup);
      switch (algIdFromOid)
      {
        case -1:
        case 0:
          throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidOID"));
        default:
          return algIdFromOid;
      }
    }

    internal static X509ContentType MapContentType(uint contentType)
    {
      switch (contentType)
      {
        case 1:
          return X509ContentType.Cert;
        case 4:
          return X509ContentType.SerializedStore;
        case 5:
          return X509ContentType.SerializedCert;
        case 8:
        case 9:
          return X509ContentType.Pkcs7;
        case 10:
          return X509ContentType.Authenticode;
        case 12:
          return X509ContentType.Pfx;
        default:
          return X509ContentType.Unknown;
      }
    }

    internal static uint MapKeyStorageFlags(X509KeyStorageFlags keyStorageFlags)
    {
      if ((keyStorageFlags & (X509KeyStorageFlags.UserKeySet | X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable | X509KeyStorageFlags.UserProtected | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.EphemeralKeySet)) != keyStorageFlags)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"), nameof (keyStorageFlags));
      X509KeyStorageFlags x509KeyStorageFlags = keyStorageFlags & (X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.EphemeralKeySet);
      if (x509KeyStorageFlags == (X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.EphemeralKeySet))
        throw new ArgumentException(Environment.GetResourceString("Cryptography_X509_InvalidFlagCombination", (object) x509KeyStorageFlags), nameof (keyStorageFlags));
      uint num = 0;
      if ((keyStorageFlags & X509KeyStorageFlags.UserKeySet) == X509KeyStorageFlags.UserKeySet)
        num |= 4096U;
      else if ((keyStorageFlags & X509KeyStorageFlags.MachineKeySet) == X509KeyStorageFlags.MachineKeySet)
        num |= 32U;
      if ((keyStorageFlags & X509KeyStorageFlags.Exportable) == X509KeyStorageFlags.Exportable)
        num |= 1U;
      if ((keyStorageFlags & X509KeyStorageFlags.UserProtected) == X509KeyStorageFlags.UserProtected)
        num |= 2U;
      if ((keyStorageFlags & X509KeyStorageFlags.EphemeralKeySet) == X509KeyStorageFlags.EphemeralKeySet)
        num |= 33280U;
      return num;
    }

    [SecurityCritical]
    internal static SafeCertStoreHandle ExportCertToMemoryStore(X509Certificate certificate)
    {
      SafeCertStoreHandle invalidHandle = SafeCertStoreHandle.InvalidHandle;
      X509Utils._OpenX509Store(2U, 8704U, (string) null, ref invalidHandle);
      X509Utils._AddCertificateToStore(invalidHandle, certificate.CertContext);
      return invalidHandle;
    }

    [SecurityCritical]
    internal static IntPtr PasswordToHGlobalUni(object password)
    {
      if (password != null)
      {
        string s1 = password as string;
        if (s1 != null)
          return Marshal.StringToHGlobalUni(s1);
        SecureString s2 = password as SecureString;
        if (s2 != null)
          return Marshal.SecureStringToGlobalAllocUnicode(s2);
      }
      return IntPtr.Zero;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("crypt32")]
    private static extern IntPtr CryptFindOIDInfo(OidKeyType dwKeyType, IntPtr pvKey, OidGroup dwGroupId);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void _AddCertificateToStore(SafeCertStoreHandle safeCertStoreHandle, SafeCertContextHandle safeCertContext);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void _DuplicateCertContext(IntPtr handle, ref SafeCertContextHandle safeCertContext);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern byte[] _ExportCertificatesToBlob(SafeCertStoreHandle safeCertStoreHandle, X509ContentType contentType, IntPtr password);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern byte[] _GetCertRawData(SafeCertContextHandle safeCertContext);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void _GetDateNotAfter(SafeCertContextHandle safeCertContext, ref Win32Native.FILE_TIME fileTime);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void _GetDateNotBefore(SafeCertContextHandle safeCertContext, ref Win32Native.FILE_TIME fileTime);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string _GetIssuerName(SafeCertContextHandle safeCertContext, bool legacyV1Mode);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string _GetPublicKeyOid(SafeCertContextHandle safeCertContext);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern byte[] _GetPublicKeyParameters(SafeCertContextHandle safeCertContext);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern byte[] _GetPublicKeyValue(SafeCertContextHandle safeCertContext);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string _GetSubjectInfo(SafeCertContextHandle safeCertContext, uint displayType, bool legacyV1Mode);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern byte[] _GetSerialNumber(SafeCertContextHandle safeCertContext);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern byte[] _GetThumbprint(SafeCertContextHandle safeCertContext);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void _LoadCertFromBlob(byte[] rawData, IntPtr password, uint dwFlags, bool persistKeySet, ref SafeCertContextHandle pCertCtx);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void _LoadCertFromFile(string fileName, IntPtr password, uint dwFlags, bool persistKeySet, ref SafeCertContextHandle pCertCtx);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void _OpenX509Store(uint storeType, uint flags, string storeName, ref SafeCertStoreHandle safeCertStoreHandle);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern uint _QueryCertBlobType(byte[] rawData);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern uint _QueryCertFileType(string fileName);
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.Win32
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
  internal static class Win32
  {
    internal const int FALSE = 0;
    internal const int TRUE = 1;
    private static bool _LsaLookupNames2Supported;
    private static bool _WellKnownSidApisSupported;

    [SecuritySafeCritical]
    static Win32()
    {
      Win32Native.OSVERSIONINFO osVer1 = new Win32Native.OSVERSIONINFO();
      if (!Environment.GetVersion(osVer1))
        throw new SystemException(Environment.GetResourceString("InvalidOperation_GetVersion"));
      if (osVer1.MajorVersion > 5 || osVer1.MinorVersion > 0)
      {
        System.Security.Principal.Win32._LsaLookupNames2Supported = true;
        System.Security.Principal.Win32._WellKnownSidApisSupported = true;
      }
      else
      {
        System.Security.Principal.Win32._LsaLookupNames2Supported = false;
        Win32Native.OSVERSIONINFOEX osVer2 = new Win32Native.OSVERSIONINFOEX();
        if (!Environment.GetVersionEx(osVer2))
          throw new SystemException(Environment.GetResourceString("InvalidOperation_GetVersion"));
        if (osVer2.ServicePackMajor < (ushort) 3)
          System.Security.Principal.Win32._WellKnownSidApisSupported = false;
        else
          System.Security.Principal.Win32._WellKnownSidApisSupported = true;
      }
    }

    internal static bool LsaLookupNames2Supported
    {
      get
      {
        return System.Security.Principal.Win32._LsaLookupNames2Supported;
      }
    }

    internal static bool WellKnownSidApisSupported
    {
      get
      {
        return System.Security.Principal.Win32._WellKnownSidApisSupported;
      }
    }

    [SecurityCritical]
    internal static SafeLsaPolicyHandle LsaOpenPolicy(string systemName, PolicyRights rights)
    {
      Win32Native.LSA_OBJECT_ATTRIBUTES attributes;
      attributes.Length = Marshal.SizeOf(typeof (Win32Native.LSA_OBJECT_ATTRIBUTES));
      attributes.RootDirectory = IntPtr.Zero;
      attributes.ObjectName = IntPtr.Zero;
      attributes.Attributes = 0;
      attributes.SecurityDescriptor = IntPtr.Zero;
      attributes.SecurityQualityOfService = IntPtr.Zero;
      SafeLsaPolicyHandle handle;
      uint num;
      if ((num = Win32Native.LsaOpenPolicy(systemName, ref attributes, (int) rights, out handle)) == 0U)
        return handle;
      if (num == 3221225506U)
        throw new UnauthorizedAccessException();
      if (num == 3221225626U || num == 3221225495U)
        throw new OutOfMemoryException();
      throw new SystemException(Win32Native.GetMessage(Win32Native.LsaNtStatusToWinError((int) num)));
    }

    [SecurityCritical]
    internal static byte[] ConvertIntPtrSidToByteArraySid(IntPtr binaryForm)
    {
      if ((int) Marshal.ReadByte(binaryForm, 0) != (int) SecurityIdentifier.Revision)
        throw new ArgumentException(Environment.GetResourceString("IdentityReference_InvalidSidRevision"), nameof (binaryForm));
      byte num = Marshal.ReadByte(binaryForm, 1);
      if (num < (byte) 0 || (int) num > (int) SecurityIdentifier.MaxSubAuthorities)
        throw new ArgumentException(Environment.GetResourceString("IdentityReference_InvalidNumberOfSubauthorities", (object) SecurityIdentifier.MaxSubAuthorities), nameof (binaryForm));
      int length = 8 + (int) num * 4;
      byte[] destination = new byte[length];
      Marshal.Copy(binaryForm, destination, 0, length);
      return destination;
    }

    [SecurityCritical]
    internal static int CreateSidFromString(string stringSid, out byte[] resultSid)
    {
      IntPtr ByteArray = IntPtr.Zero;
      int lastWin32Error;
      try
      {
        if (1 != Win32Native.ConvertStringSidToSid(stringSid, out ByteArray))
        {
          lastWin32Error = Marshal.GetLastWin32Error();
          goto label_6;
        }
        else
          resultSid = System.Security.Principal.Win32.ConvertIntPtrSidToByteArraySid(ByteArray);
      }
      finally
      {
        Win32Native.LocalFree(ByteArray);
      }
      return 0;
label_6:
      resultSid = (byte[]) null;
      return lastWin32Error;
    }

    [SecurityCritical]
    internal static int CreateWellKnownSid(WellKnownSidType sidType, SecurityIdentifier domainSid, out byte[] resultSid)
    {
      if (!System.Security.Principal.Win32.WellKnownSidApisSupported)
        throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_RequiresW2kSP3"));
      uint maxBinaryLength = (uint) SecurityIdentifier.MaxBinaryLength;
      resultSid = new byte[(int) maxBinaryLength];
      if (Win32Native.CreateWellKnownSid((int) sidType, domainSid == (SecurityIdentifier) null ? (byte[]) null : domainSid.BinaryForm, resultSid, ref maxBinaryLength) != 0)
        return 0;
      resultSid = (byte[]) null;
      return Marshal.GetLastWin32Error();
    }

    [SecurityCritical]
    internal static bool IsEqualDomainSid(SecurityIdentifier sid1, SecurityIdentifier sid2)
    {
      if (!System.Security.Principal.Win32.WellKnownSidApisSupported)
        throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_RequiresW2kSP3"));
      if (sid1 == (SecurityIdentifier) null || sid2 == (SecurityIdentifier) null)
        return false;
      byte[] numArray1 = new byte[sid1.BinaryLength];
      sid1.GetBinaryForm(numArray1, 0);
      byte[] numArray2 = new byte[sid2.BinaryLength];
      sid2.GetBinaryForm(numArray2, 0);
      bool result;
      if (Win32Native.IsEqualDomainSid(numArray1, numArray2, out result) != 0)
        return result;
      return false;
    }

    [SecurityCritical]
    internal static unsafe void InitializeReferencedDomainsPointer(SafeLsaMemoryHandle referencedDomains)
    {
      referencedDomains.Initialize((ulong) (uint) Marshal.SizeOf(typeof (Win32Native.LSA_REFERENCED_DOMAIN_LIST)));
      Win32Native.LSA_REFERENCED_DOMAIN_LIST referencedDomainList = referencedDomains.Read<Win32Native.LSA_REFERENCED_DOMAIN_LIST>(0UL);
      byte* pointer = (byte*) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        referencedDomains.AcquirePointer(ref pointer);
        if (referencedDomainList.Domains.IsNull())
          return;
        long num = (byte*) ((Win32Native.LSA_TRUST_INFORMATION*) (void*) referencedDomainList.Domains + referencedDomainList.Entries) - pointer;
        referencedDomains.Initialize((ulong) num);
      }
      finally
      {
        if ((IntPtr) pointer != IntPtr.Zero)
          referencedDomains.ReleasePointer();
      }
    }

    [SecurityCritical]
    internal static int GetWindowsAccountDomainSid(SecurityIdentifier sid, out SecurityIdentifier resultSid)
    {
      if (!System.Security.Principal.Win32.WellKnownSidApisSupported)
        throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_RequiresW2kSP3"));
      byte[] numArray1 = new byte[sid.BinaryLength];
      sid.GetBinaryForm(numArray1, 0);
      uint maxBinaryLength = (uint) SecurityIdentifier.MaxBinaryLength;
      byte[] numArray2 = new byte[(int) maxBinaryLength];
      if (Win32Native.GetWindowsAccountDomainSid(numArray1, numArray2, ref maxBinaryLength) != 0)
      {
        resultSid = new SecurityIdentifier(numArray2, 0);
        return 0;
      }
      resultSid = (SecurityIdentifier) null;
      return Marshal.GetLastWin32Error();
    }

    [SecurityCritical]
    internal static bool IsWellKnownSid(SecurityIdentifier sid, WellKnownSidType type)
    {
      if (!System.Security.Principal.Win32.WellKnownSidApisSupported)
        throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_RequiresW2kSP3"));
      byte[] numArray = new byte[sid.BinaryLength];
      sid.GetBinaryForm(numArray, 0);
      return Win32Native.IsWellKnownSid(numArray, (int) type) != 0;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int ImpersonateLoggedOnUser(SafeAccessTokenHandle hToken);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int OpenThreadToken(TokenAccessLevels dwDesiredAccess, WinSecurityContext OpenAs, out SafeAccessTokenHandle phThreadToken);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int RevertToSelf();

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern int SetThreadToken(SafeAccessTokenHandle hToken);
  }
}

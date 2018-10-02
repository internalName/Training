// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.X509Certificates.CRYPT_OID_INFO
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
  internal struct CRYPT_OID_INFO
  {
    internal int cbSize;
    [MarshalAs(UnmanagedType.LPStr)]
    internal string pszOID;
    [MarshalAs(UnmanagedType.LPWStr)]
    internal string pwszName;
    internal OidGroup dwGroupId;
    internal int AlgId;
    internal int cbData;
    internal IntPtr pbData;
  }
}

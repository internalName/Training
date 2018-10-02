// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.X509Certificates.X509ContentType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography.X509Certificates
{
  /// <summary>Задает формат сертификата X.509.</summary>
  [ComVisible(true)]
  public enum X509ContentType
  {
    Unknown = 0,
    Cert = 1,
    SerializedCert = 2,
    Pfx = 3,
    Pkcs12 = 3,
    SerializedStore = 4,
    Pkcs7 = 5,
    Authenticode = 6,
  }
}

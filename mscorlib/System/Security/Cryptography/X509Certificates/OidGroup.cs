// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.X509Certificates.OidGroup
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Cryptography.X509Certificates
{
  internal enum OidGroup
  {
    DisableSearchDS = -2147483648, // -0x80000000
    AllGroups = 0,
    HashAlgorithm = 1,
    EncryptionAlgorithm = 2,
    PublicKeyAlgorithm = 3,
    SignatureAlgorithm = 4,
    Attribute = 5,
    ExtensionOrAttribute = 6,
    EnhancedKeyUsage = 7,
    Policy = 8,
    Template = 9,
    KeyDerivationFunction = 10, // 0x0000000A
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSAPKCS1SHA1SignatureDescription
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Cryptography
{
  internal class RSAPKCS1SHA1SignatureDescription : RSAPKCS1SignatureDescription
  {
    public RSAPKCS1SHA1SignatureDescription()
      : base("SHA1", "System.Security.Cryptography.SHA1Cng")
    {
    }
  }
}

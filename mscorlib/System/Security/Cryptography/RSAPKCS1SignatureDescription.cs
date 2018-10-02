// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.RSAPKCS1SignatureDescription
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Cryptography
{
  internal abstract class RSAPKCS1SignatureDescription : SignatureDescription
  {
    private string _hashAlgorithm;

    protected RSAPKCS1SignatureDescription(string hashAlgorithm, string digestAlgorithm)
    {
      this.KeyAlgorithm = "System.Security.Cryptography.RSA";
      this.DigestAlgorithm = digestAlgorithm;
      this.FormatterAlgorithm = "System.Security.Cryptography.RSAPKCS1SignatureFormatter";
      this.DeformatterAlgorithm = "System.Security.Cryptography.RSAPKCS1SignatureDeformatter";
      this._hashAlgorithm = hashAlgorithm;
    }

    public override sealed AsymmetricSignatureDeformatter CreateDeformatter(AsymmetricAlgorithm key)
    {
      AsymmetricSignatureDeformatter deformatter = base.CreateDeformatter(key);
      deformatter.SetHashAlgorithm(this._hashAlgorithm);
      return deformatter;
    }

    public override sealed AsymmetricSignatureFormatter CreateFormatter(AsymmetricAlgorithm key)
    {
      AsymmetricSignatureFormatter formatter = base.CreateFormatter(key);
      formatter.SetHashAlgorithm(this._hashAlgorithm);
      return formatter;
    }
  }
}

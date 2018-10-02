// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityDocumentElement
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security
{
  [Serializable]
  internal sealed class SecurityDocumentElement : ISecurityElementFactory
  {
    private int m_position;
    private SecurityDocument m_document;

    internal SecurityDocumentElement(SecurityDocument document, int position)
    {
      this.m_document = document;
      this.m_position = position;
    }

    SecurityElement ISecurityElementFactory.CreateSecurityElement()
    {
      return this.m_document.GetElement(this.m_position, true);
    }

    object ISecurityElementFactory.Copy()
    {
      return (object) new SecurityDocumentElement(this.m_document, this.m_position);
    }

    string ISecurityElementFactory.GetTag()
    {
      return this.m_document.GetTagForElement(this.m_position);
    }

    string ISecurityElementFactory.Attribute(string attributeName)
    {
      return this.m_document.GetAttributeForElement(this.m_position, attributeName);
    }
  }
}

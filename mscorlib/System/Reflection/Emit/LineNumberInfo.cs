// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.LineNumberInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
  internal sealed class LineNumberInfo
  {
    private int m_DocumentCount;
    private REDocument[] m_Documents;
    private const int InitialSize = 16;
    private int m_iLastFound;

    internal LineNumberInfo()
    {
      this.m_DocumentCount = 0;
      this.m_iLastFound = 0;
    }

    internal void AddLineNumberInfo(ISymbolDocumentWriter document, int iOffset, int iStartLine, int iStartColumn, int iEndLine, int iEndColumn)
    {
      this.m_Documents[this.FindDocument(document)].AddLineNumberInfo(document, iOffset, iStartLine, iStartColumn, iEndLine, iEndColumn);
    }

    private int FindDocument(ISymbolDocumentWriter document)
    {
      if (this.m_iLastFound < this.m_DocumentCount && this.m_Documents[this.m_iLastFound].m_document == document)
        return this.m_iLastFound;
      for (int index = 0; index < this.m_DocumentCount; ++index)
      {
        if (this.m_Documents[index].m_document == document)
        {
          this.m_iLastFound = index;
          return this.m_iLastFound;
        }
      }
      this.EnsureCapacity();
      this.m_iLastFound = this.m_DocumentCount;
      this.m_Documents[this.m_iLastFound] = new REDocument(document);
      checked { ++this.m_DocumentCount; }
      return this.m_iLastFound;
    }

    private void EnsureCapacity()
    {
      if (this.m_DocumentCount == 0)
      {
        this.m_Documents = new REDocument[16];
      }
      else
      {
        if (this.m_DocumentCount != this.m_Documents.Length)
          return;
        REDocument[] reDocumentArray = new REDocument[this.m_DocumentCount * 2];
        Array.Copy((Array) this.m_Documents, (Array) reDocumentArray, this.m_DocumentCount);
        this.m_Documents = reDocumentArray;
      }
    }

    internal void EmitLineNumberInfo(ISymbolWriter symWriter)
    {
      for (int index = 0; index < this.m_DocumentCount; ++index)
        this.m_Documents[index].EmitLineNumberInfo(symWriter);
    }
  }
}

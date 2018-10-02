// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.REDocument
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
  internal sealed class REDocument
  {
    private int[] m_iOffsets;
    private int[] m_iLines;
    private int[] m_iColumns;
    private int[] m_iEndLines;
    private int[] m_iEndColumns;
    internal ISymbolDocumentWriter m_document;
    private int m_iLineNumberCount;
    private const int InitialSize = 16;

    internal REDocument(ISymbolDocumentWriter document)
    {
      this.m_iLineNumberCount = 0;
      this.m_document = document;
    }

    internal void AddLineNumberInfo(ISymbolDocumentWriter document, int iOffset, int iStartLine, int iStartColumn, int iEndLine, int iEndColumn)
    {
      this.EnsureCapacity();
      this.m_iOffsets[this.m_iLineNumberCount] = iOffset;
      this.m_iLines[this.m_iLineNumberCount] = iStartLine;
      this.m_iColumns[this.m_iLineNumberCount] = iStartColumn;
      this.m_iEndLines[this.m_iLineNumberCount] = iEndLine;
      this.m_iEndColumns[this.m_iLineNumberCount] = iEndColumn;
      checked { ++this.m_iLineNumberCount; }
    }

    private void EnsureCapacity()
    {
      if (this.m_iLineNumberCount == 0)
      {
        this.m_iOffsets = new int[16];
        this.m_iLines = new int[16];
        this.m_iColumns = new int[16];
        this.m_iEndLines = new int[16];
        this.m_iEndColumns = new int[16];
      }
      else
      {
        if (this.m_iLineNumberCount != this.m_iOffsets.Length)
          return;
        int length = checked (this.m_iLineNumberCount * 2);
        int[] numArray1 = new int[length];
        Array.Copy((Array) this.m_iOffsets, (Array) numArray1, this.m_iLineNumberCount);
        this.m_iOffsets = numArray1;
        int[] numArray2 = new int[length];
        Array.Copy((Array) this.m_iLines, (Array) numArray2, this.m_iLineNumberCount);
        this.m_iLines = numArray2;
        int[] numArray3 = new int[length];
        Array.Copy((Array) this.m_iColumns, (Array) numArray3, this.m_iLineNumberCount);
        this.m_iColumns = numArray3;
        int[] numArray4 = new int[length];
        Array.Copy((Array) this.m_iEndLines, (Array) numArray4, this.m_iLineNumberCount);
        this.m_iEndLines = numArray4;
        int[] numArray5 = new int[length];
        Array.Copy((Array) this.m_iEndColumns, (Array) numArray5, this.m_iLineNumberCount);
        this.m_iEndColumns = numArray5;
      }
    }

    internal void EmitLineNumberInfo(ISymbolWriter symWriter)
    {
      if (this.m_iLineNumberCount == 0)
        return;
      int[] offsets = new int[this.m_iLineNumberCount];
      Array.Copy((Array) this.m_iOffsets, (Array) offsets, this.m_iLineNumberCount);
      int[] lines = new int[this.m_iLineNumberCount];
      Array.Copy((Array) this.m_iLines, (Array) lines, this.m_iLineNumberCount);
      int[] columns = new int[this.m_iLineNumberCount];
      Array.Copy((Array) this.m_iColumns, (Array) columns, this.m_iLineNumberCount);
      int[] endLines = new int[this.m_iLineNumberCount];
      Array.Copy((Array) this.m_iEndLines, (Array) endLines, this.m_iLineNumberCount);
      int[] endColumns = new int[this.m_iLineNumberCount];
      Array.Copy((Array) this.m_iEndColumns, (Array) endColumns, this.m_iLineNumberCount);
      symWriter.DefineSequencePoints(this.m_document, offsets, lines, columns, endLines, endColumns);
    }
  }
}

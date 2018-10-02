// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.ScopeTree
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
  internal sealed class ScopeTree
  {
    internal int[] m_iOffsets;
    internal ScopeAction[] m_ScopeActions;
    internal int m_iCount;
    internal int m_iOpenScopeCount;
    internal const int InitialSize = 16;
    internal LocalSymInfo[] m_localSymInfos;

    internal ScopeTree()
    {
      this.m_iOpenScopeCount = 0;
      this.m_iCount = 0;
    }

    internal int GetCurrentActiveScopeIndex()
    {
      int num = 0;
      int index = this.m_iCount - 1;
      if (this.m_iCount == 0)
        return -1;
      for (; num > 0 || this.m_ScopeActions[index] == ScopeAction.Close; --index)
      {
        if (this.m_ScopeActions[index] == ScopeAction.Open)
          --num;
        else
          ++num;
      }
      return index;
    }

    internal void AddLocalSymInfoToCurrentScope(string strName, byte[] signature, int slot, int startOffset, int endOffset)
    {
      int activeScopeIndex = this.GetCurrentActiveScopeIndex();
      if (this.m_localSymInfos[activeScopeIndex] == null)
        this.m_localSymInfos[activeScopeIndex] = new LocalSymInfo();
      this.m_localSymInfos[activeScopeIndex].AddLocalSymInfo(strName, signature, slot, startOffset, endOffset);
    }

    internal void AddUsingNamespaceToCurrentScope(string strNamespace)
    {
      int activeScopeIndex = this.GetCurrentActiveScopeIndex();
      if (this.m_localSymInfos[activeScopeIndex] == null)
        this.m_localSymInfos[activeScopeIndex] = new LocalSymInfo();
      this.m_localSymInfos[activeScopeIndex].AddUsingNamespace(strNamespace);
    }

    internal void AddScopeInfo(ScopeAction sa, int iOffset)
    {
      if (sa == ScopeAction.Close && this.m_iOpenScopeCount <= 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_UnmatchingSymScope"));
      this.EnsureCapacity();
      this.m_ScopeActions[this.m_iCount] = sa;
      this.m_iOffsets[this.m_iCount] = iOffset;
      this.m_localSymInfos[this.m_iCount] = (LocalSymInfo) null;
      checked { ++this.m_iCount; }
      if (sa == ScopeAction.Open)
        ++this.m_iOpenScopeCount;
      else
        --this.m_iOpenScopeCount;
    }

    internal void EnsureCapacity()
    {
      if (this.m_iCount == 0)
      {
        this.m_iOffsets = new int[16];
        this.m_ScopeActions = new ScopeAction[16];
        this.m_localSymInfos = new LocalSymInfo[16];
      }
      else
      {
        if (this.m_iCount != this.m_iOffsets.Length)
          return;
        int length = checked (this.m_iCount * 2);
        int[] numArray = new int[length];
        Array.Copy((Array) this.m_iOffsets, (Array) numArray, this.m_iCount);
        this.m_iOffsets = numArray;
        ScopeAction[] scopeActionArray = new ScopeAction[length];
        Array.Copy((Array) this.m_ScopeActions, (Array) scopeActionArray, this.m_iCount);
        this.m_ScopeActions = scopeActionArray;
        LocalSymInfo[] localSymInfoArray = new LocalSymInfo[length];
        Array.Copy((Array) this.m_localSymInfos, (Array) localSymInfoArray, this.m_iCount);
        this.m_localSymInfos = localSymInfoArray;
      }
    }

    internal void EmitScopeTree(ISymbolWriter symWriter)
    {
      for (int index = 0; index < this.m_iCount; ++index)
      {
        if (this.m_ScopeActions[index] == ScopeAction.Open)
          symWriter.OpenScope(this.m_iOffsets[index]);
        else
          symWriter.CloseScope(this.m_iOffsets[index]);
        if (this.m_localSymInfos[index] != null)
          this.m_localSymInfos[index].EmitLocalSymInfo(symWriter);
      }
    }
  }
}

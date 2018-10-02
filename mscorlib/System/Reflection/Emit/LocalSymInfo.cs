// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.LocalSymInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
  internal class LocalSymInfo
  {
    internal string[] m_strName;
    internal byte[][] m_ubSignature;
    internal int[] m_iLocalSlot;
    internal int[] m_iStartOffset;
    internal int[] m_iEndOffset;
    internal int m_iLocalSymCount;
    internal string[] m_namespace;
    internal int m_iNameSpaceCount;
    internal const int InitialSize = 16;

    internal LocalSymInfo()
    {
      this.m_iLocalSymCount = 0;
      this.m_iNameSpaceCount = 0;
    }

    private void EnsureCapacityNamespace()
    {
      if (this.m_iNameSpaceCount == 0)
      {
        this.m_namespace = new string[16];
      }
      else
      {
        if (this.m_iNameSpaceCount != this.m_namespace.Length)
          return;
        string[] strArray = new string[checked (this.m_iNameSpaceCount * 2)];
        Array.Copy((Array) this.m_namespace, (Array) strArray, this.m_iNameSpaceCount);
        this.m_namespace = strArray;
      }
    }

    private void EnsureCapacity()
    {
      if (this.m_iLocalSymCount == 0)
      {
        this.m_strName = new string[16];
        this.m_ubSignature = new byte[16][];
        this.m_iLocalSlot = new int[16];
        this.m_iStartOffset = new int[16];
        this.m_iEndOffset = new int[16];
      }
      else
      {
        if (this.m_iLocalSymCount != this.m_strName.Length)
          return;
        int length = checked (this.m_iLocalSymCount * 2);
        int[] numArray1 = new int[length];
        Array.Copy((Array) this.m_iLocalSlot, (Array) numArray1, this.m_iLocalSymCount);
        this.m_iLocalSlot = numArray1;
        int[] numArray2 = new int[length];
        Array.Copy((Array) this.m_iStartOffset, (Array) numArray2, this.m_iLocalSymCount);
        this.m_iStartOffset = numArray2;
        int[] numArray3 = new int[length];
        Array.Copy((Array) this.m_iEndOffset, (Array) numArray3, this.m_iLocalSymCount);
        this.m_iEndOffset = numArray3;
        string[] strArray = new string[length];
        Array.Copy((Array) this.m_strName, (Array) strArray, this.m_iLocalSymCount);
        this.m_strName = strArray;
        byte[][] numArray4 = new byte[length][];
        Array.Copy((Array) this.m_ubSignature, (Array) numArray4, this.m_iLocalSymCount);
        this.m_ubSignature = numArray4;
      }
    }

    internal void AddLocalSymInfo(string strName, byte[] signature, int slot, int startOffset, int endOffset)
    {
      this.EnsureCapacity();
      this.m_iStartOffset[this.m_iLocalSymCount] = startOffset;
      this.m_iEndOffset[this.m_iLocalSymCount] = endOffset;
      this.m_iLocalSlot[this.m_iLocalSymCount] = slot;
      this.m_strName[this.m_iLocalSymCount] = strName;
      this.m_ubSignature[this.m_iLocalSymCount] = signature;
      checked { ++this.m_iLocalSymCount; }
    }

    internal void AddUsingNamespace(string strNamespace)
    {
      this.EnsureCapacityNamespace();
      this.m_namespace[this.m_iNameSpaceCount] = strNamespace;
      checked { ++this.m_iNameSpaceCount; }
    }

    internal virtual void EmitLocalSymInfo(ISymbolWriter symWriter)
    {
      for (int index = 0; index < this.m_iLocalSymCount; ++index)
        symWriter.DefineLocalVariable(this.m_strName[index], FieldAttributes.PrivateScope, this.m_ubSignature[index], SymAddressKind.ILOffset, this.m_iLocalSlot[index], 0, 0, this.m_iStartOffset[index], this.m_iEndOffset[index]);
      for (int index = 0; index < this.m_iNameSpaceCount; ++index)
        symWriter.UsingNamespace(this.m_namespace[index]);
    }
  }
}

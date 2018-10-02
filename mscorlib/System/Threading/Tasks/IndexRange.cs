// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.IndexRange
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Threading.Tasks
{
  [StructLayout(LayoutKind.Auto)]
  internal struct IndexRange
  {
    internal long m_nFromInclusive;
    internal long m_nToExclusive;
    internal volatile Shared<long> m_nSharedCurrentIndexOffset;
    internal int m_bRangeFinished;
  }
}

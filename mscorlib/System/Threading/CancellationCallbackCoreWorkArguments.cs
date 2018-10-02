// Decompiled with JetBrains decompiler
// Type: System.Threading.CancellationCallbackCoreWorkArguments
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Threading
{
  internal struct CancellationCallbackCoreWorkArguments
  {
    internal SparselyPopulatedArrayFragment<CancellationCallbackInfo> m_currArrayFragment;
    internal int m_currArrayIndex;

    public CancellationCallbackCoreWorkArguments(SparselyPopulatedArrayFragment<CancellationCallbackInfo> currArrayFragment, int currArrayIndex)
    {
      this.m_currArrayFragment = currArrayFragment;
      this.m_currArrayIndex = currArrayIndex;
    }
  }
}

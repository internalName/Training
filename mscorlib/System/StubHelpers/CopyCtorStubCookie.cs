// Decompiled with JetBrains decompiler
// Type: System.StubHelpers.CopyCtorStubCookie
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.StubHelpers
{
  internal struct CopyCtorStubCookie
  {
    public IntPtr m_srcInstancePtr;
    public uint m_dstStackOffset;
    public IntPtr m_ctorPtr;
    public IntPtr m_dtorPtr;
    public IntPtr m_pNext;

    public void SetData(IntPtr srcInstancePtr, uint dstStackOffset, IntPtr ctorPtr, IntPtr dtorPtr)
    {
      this.m_srcInstancePtr = srcInstancePtr;
      this.m_dstStackOffset = dstStackOffset;
      this.m_ctorPtr = ctorPtr;
      this.m_dtorPtr = dtorPtr;
    }

    public void SetNext(IntPtr pNext)
    {
      this.m_pNext = pNext;
    }
  }
}

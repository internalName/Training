// Decompiled with JetBrains decompiler
// Type: System.Reflection.ConstArray
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Reflection
{
  [Serializable]
  internal struct ConstArray
  {
    internal int m_length;
    internal IntPtr m_constArray;

    public IntPtr Signature
    {
      get
      {
        return this.m_constArray;
      }
    }

    public int Length
    {
      get
      {
        return this.m_length;
      }
    }

    public unsafe byte this[int index]
    {
      [SecuritySafeCritical] get
      {
        if (index < 0 || index >= this.m_length)
          throw new IndexOutOfRangeException();
        return *(byte*) ((IntPtr) this.m_constArray.ToPointer() + index);
      }
    }
  }
}

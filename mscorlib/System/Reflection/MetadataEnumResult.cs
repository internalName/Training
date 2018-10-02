// Decompiled with JetBrains decompiler
// Type: System.Reflection.MetadataEnumResult
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Reflection
{
  internal struct MetadataEnumResult
  {
    private int[] largeResult;
    private int length;
    private unsafe fixed int smallResult[16];

    public int Length
    {
      get
      {
        return this.length;
      }
    }

    public unsafe int this[int index]
    {
      [SecurityCritical] get
      {
        if (this.largeResult != null)
          return this.largeResult[index];
        fixed (int* numPtr = &this.smallResult.FixedElementField)
          return numPtr[index];
      }
    }
  }
}

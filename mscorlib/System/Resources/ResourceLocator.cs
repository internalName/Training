// Decompiled with JetBrains decompiler
// Type: System.Resources.ResourceLocator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Resources
{
  internal struct ResourceLocator
  {
    internal object _value;
    internal int _dataPos;

    internal ResourceLocator(int dataPos, object value)
    {
      this._dataPos = dataPos;
      this._value = value;
    }

    internal int DataPosition
    {
      get
      {
        return this._dataPos;
      }
    }

    internal object Value
    {
      get
      {
        return this._value;
      }
      set
      {
        this._value = value;
      }
    }

    internal static bool CanCache(ResourceTypeCode value)
    {
      return value <= ResourceTypeCode.TimeSpan;
    }
  }
}

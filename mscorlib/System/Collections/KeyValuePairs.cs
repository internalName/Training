// Decompiled with JetBrains decompiler
// Type: System.Collections.KeyValuePairs
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;

namespace System.Collections
{
  [DebuggerDisplay("{value}", Name = "[{key}]", Type = "")]
  internal class KeyValuePairs
  {
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private object key;
    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private object value;

    public KeyValuePairs(object key, object value)
    {
      this.value = value;
      this.key = key;
    }

    public object Key
    {
      get
      {
        return this.key;
      }
    }

    public object Value
    {
      get
      {
        return this.value;
      }
    }
  }
}

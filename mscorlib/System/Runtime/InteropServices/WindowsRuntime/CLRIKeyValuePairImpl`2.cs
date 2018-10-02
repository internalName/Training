// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.CLRIKeyValuePairImpl`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class CLRIKeyValuePairImpl<K, V> : IKeyValuePair<K, V>
  {
    private readonly KeyValuePair<K, V> _pair;

    public CLRIKeyValuePairImpl([In] ref KeyValuePair<K, V> pair)
    {
      this._pair = pair;
    }

    public K Key
    {
      get
      {
        return this._pair.Key;
      }
    }

    public V Value
    {
      get
      {
        return this._pair.Value;
      }
    }

    internal static object BoxHelper(object pair)
    {
      KeyValuePair<K, V> pair1 = (KeyValuePair<K, V>) pair;
      return (object) new CLRIKeyValuePairImpl<K, V>(ref pair1);
    }

    internal static object UnboxHelper(object wrapper)
    {
      return (object) ((CLRIKeyValuePairImpl<K, V>) wrapper)._pair;
    }

    public override string ToString()
    {
      return this._pair.ToString();
    }
  }
}

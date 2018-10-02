// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.NameCache
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Concurrent;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class NameCache
  {
    private static ConcurrentDictionary<string, object> ht = new ConcurrentDictionary<string, object>();
    private string name;

    internal object GetCachedValue(string name)
    {
      this.name = name;
      object obj;
      if (!NameCache.ht.TryGetValue(name, out obj))
        return (object) null;
      return obj;
    }

    internal void SetCachedValue(object value)
    {
      NameCache.ht[this.name] = value;
    }
  }
}

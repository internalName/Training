// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.MapToDictionaryAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class MapToDictionaryAdapter
  {
    private MapToDictionaryAdapter()
    {
    }

    [SecurityCritical]
    internal V Indexer_Get<K, V>(K key)
    {
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      return MapToDictionaryAdapter.Lookup<K, V>(JitHelpers.UnsafeCast<IMap<K, V>>((object) this), key);
    }

    [SecurityCritical]
    internal void Indexer_Set<K, V>(K key, V value)
    {
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      MapToDictionaryAdapter.Insert<K, V>(JitHelpers.UnsafeCast<IMap<K, V>>((object) this), key, value);
    }

    [SecurityCritical]
    internal ICollection<K> Keys<K, V>()
    {
      return (ICollection<K>) new DictionaryKeyCollection<K, V>((IDictionary<K, V>) JitHelpers.UnsafeCast<IMap<K, V>>((object) this));
    }

    [SecurityCritical]
    internal ICollection<V> Values<K, V>()
    {
      return (ICollection<V>) new DictionaryValueCollection<K, V>((IDictionary<K, V>) JitHelpers.UnsafeCast<IMap<K, V>>((object) this));
    }

    [SecurityCritical]
    internal bool ContainsKey<K, V>(K key)
    {
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      return JitHelpers.UnsafeCast<IMap<K, V>>((object) this).HasKey(key);
    }

    [SecurityCritical]
    internal void Add<K, V>(K key, V value)
    {
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      if (this.ContainsKey<K, V>(key))
        throw new ArgumentException(Environment.GetResourceString("Argument_AddingDuplicate"));
      MapToDictionaryAdapter.Insert<K, V>(JitHelpers.UnsafeCast<IMap<K, V>>((object) this), key, value);
    }

    [SecurityCritical]
    internal bool Remove<K, V>(K key)
    {
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      IMap<K, V> map = JitHelpers.UnsafeCast<IMap<K, V>>((object) this);
      if (!map.HasKey(key))
        return false;
      try
      {
        map.Remove(key);
        return true;
      }
      catch (Exception ex)
      {
        if (-2147483637 == ex._HResult)
          return false;
        throw;
      }
    }

    [SecurityCritical]
    internal bool TryGetValue<K, V>(K key, out V value)
    {
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      IMap<K, V> _this = JitHelpers.UnsafeCast<IMap<K, V>>((object) this);
      if (!_this.HasKey(key))
      {
        value = default (V);
        return false;
      }
      try
      {
        value = MapToDictionaryAdapter.Lookup<K, V>(_this, key);
        return true;
      }
      catch (KeyNotFoundException ex)
      {
        value = default (V);
        return false;
      }
    }

    private static V Lookup<K, V>(IMap<K, V> _this, K key)
    {
      try
      {
        return _this.Lookup(key);
      }
      catch (Exception ex)
      {
        if (-2147483637 == ex._HResult)
          throw new KeyNotFoundException(Environment.GetResourceString("Arg_KeyNotFound"));
        throw;
      }
    }

    private static bool Insert<K, V>(IMap<K, V> _this, K key, V value)
    {
      return _this.Insert(key, value);
    }
  }
}

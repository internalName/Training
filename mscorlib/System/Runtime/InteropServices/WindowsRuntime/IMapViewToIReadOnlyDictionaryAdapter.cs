// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.IMapViewToIReadOnlyDictionaryAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  [DebuggerDisplay("Count = {Count}")]
  internal sealed class IMapViewToIReadOnlyDictionaryAdapter
  {
    private IMapViewToIReadOnlyDictionaryAdapter()
    {
    }

    [SecurityCritical]
    internal V Indexer_Get<K, V>(K key)
    {
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      return IMapViewToIReadOnlyDictionaryAdapter.Lookup<K, V>(JitHelpers.UnsafeCast<IMapView<K, V>>((object) this), key);
    }

    [SecurityCritical]
    internal IEnumerable<K> Keys<K, V>()
    {
      return (IEnumerable<K>) new ReadOnlyDictionaryKeyCollection<K, V>((IReadOnlyDictionary<K, V>) JitHelpers.UnsafeCast<IMapView<K, V>>((object) this));
    }

    [SecurityCritical]
    internal IEnumerable<V> Values<K, V>()
    {
      return (IEnumerable<V>) new ReadOnlyDictionaryValueCollection<K, V>((IReadOnlyDictionary<K, V>) JitHelpers.UnsafeCast<IMapView<K, V>>((object) this));
    }

    [SecurityCritical]
    internal bool ContainsKey<K, V>(K key)
    {
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      return JitHelpers.UnsafeCast<IMapView<K, V>>((object) this).HasKey(key);
    }

    [SecurityCritical]
    internal bool TryGetValue<K, V>(K key, out V value)
    {
      if ((object) key == null)
        throw new ArgumentNullException(nameof (key));
      IMapView<K, V> mapView = JitHelpers.UnsafeCast<IMapView<K, V>>((object) this);
      if (!mapView.HasKey(key))
      {
        value = default (V);
        return false;
      }
      try
      {
        value = mapView.Lookup(key);
        return true;
      }
      catch (Exception ex)
      {
        if (-2147483637 == ex._HResult)
        {
          value = default (V);
          return false;
        }
        throw;
      }
    }

    private static V Lookup<K, V>(IMapView<K, V> _this, K key)
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
  }
}

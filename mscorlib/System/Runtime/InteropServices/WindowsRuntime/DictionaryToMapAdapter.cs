// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.DictionaryToMapAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class DictionaryToMapAdapter
  {
    private DictionaryToMapAdapter()
    {
    }

    [SecurityCritical]
    internal V Lookup<K, V>(K key)
    {
      V v;
      if (!JitHelpers.UnsafeCast<IDictionary<K, V>>((object) this).TryGetValue(key, out v))
      {
        Exception exception = (Exception) new KeyNotFoundException(Environment.GetResourceString("Arg_KeyNotFound"));
        exception.SetErrorCode(-2147483637);
        throw exception;
      }
      return v;
    }

    [SecurityCritical]
    internal uint Size<K, V>()
    {
      return (uint) JitHelpers.UnsafeCast<IDictionary<K, V>>((object) this).Count;
    }

    [SecurityCritical]
    internal bool HasKey<K, V>(K key)
    {
      return JitHelpers.UnsafeCast<IDictionary<K, V>>((object) this).ContainsKey(key);
    }

    [SecurityCritical]
    internal IReadOnlyDictionary<K, V> GetView<K, V>()
    {
      IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>((object) this);
      return dictionary as IReadOnlyDictionary<K, V> ?? (IReadOnlyDictionary<K, V>) new ReadOnlyDictionary<K, V>(dictionary);
    }

    [SecurityCritical]
    internal bool Insert<K, V>(K key, V value)
    {
      IDictionary<K, V> dictionary = JitHelpers.UnsafeCast<IDictionary<K, V>>((object) this);
      bool flag = dictionary.ContainsKey(key);
      dictionary[key] = value;
      return flag;
    }

    [SecurityCritical]
    internal void Remove<K, V>(K key)
    {
      if (!JitHelpers.UnsafeCast<IDictionary<K, V>>((object) this).Remove(key))
      {
        Exception exception = (Exception) new KeyNotFoundException(Environment.GetResourceString("Arg_KeyNotFound"));
        exception.SetErrorCode(-2147483637);
        throw exception;
      }
    }

    [SecurityCritical]
    internal void Clear<K, V>()
    {
      JitHelpers.UnsafeCast<IDictionary<K, V>>((object) this).Clear();
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.MapViewToReadOnlyCollectionAdapter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  internal sealed class MapViewToReadOnlyCollectionAdapter
  {
    private MapViewToReadOnlyCollectionAdapter()
    {
    }

    [SecurityCritical]
    internal int Count<K, V>()
    {
      IMapView<K, V> mapView = JitHelpers.UnsafeCast<object>((object) this) as IMapView<K, V>;
      if (mapView != null)
      {
        uint size = mapView.Size;
        if ((uint) int.MaxValue < size)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingDictionaryTooLarge"));
        return (int) size;
      }
      uint size1 = JitHelpers.UnsafeCast<IVectorView<KeyValuePair<K, V>>>((object) this).Size;
      if ((uint) int.MaxValue < size1)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_CollectionBackingListTooLarge"));
      return (int) size1;
    }
  }
}

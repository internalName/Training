// Decompiled with JetBrains decompiler
// Type: System.Internal
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;
using System.StubHelpers;

namespace System
{
  internal static class Internal
  {
    private static void CommonlyUsedGenericInstantiations()
    {
      Array.Sort<double>((double[]) null);
      Array.Sort<int>((int[]) null);
      Array.Sort<IntPtr>((IntPtr[]) null);
      ArraySegment<byte> arraySegment = new ArraySegment<byte>(new byte[1], 0, 0);
      Dictionary<char, object> dictionary1 = new Dictionary<char, object>();
      Dictionary<Guid, byte> dictionary2 = new Dictionary<Guid, byte>();
      Dictionary<Guid, object> dictionary3 = new Dictionary<Guid, object>();
      Dictionary<Guid, Guid> dictionary4 = new Dictionary<Guid, Guid>();
      Dictionary<short, IntPtr> dictionary5 = new Dictionary<short, IntPtr>();
      Dictionary<int, byte> dictionary6 = new Dictionary<int, byte>();
      Dictionary<int, int> dictionary7 = new Dictionary<int, int>();
      Dictionary<int, object> dictionary8 = new Dictionary<int, object>();
      Dictionary<IntPtr, bool> dictionary9 = new Dictionary<IntPtr, bool>();
      Dictionary<IntPtr, short> dictionary10 = new Dictionary<IntPtr, short>();
      Dictionary<object, bool> dictionary11 = new Dictionary<object, bool>();
      Dictionary<object, char> dictionary12 = new Dictionary<object, char>();
      Dictionary<object, Guid> dictionary13 = new Dictionary<object, Guid>();
      Dictionary<object, int> dictionary14 = new Dictionary<object, int>();
      Dictionary<object, long> dictionary15 = new Dictionary<object, long>();
      Dictionary<uint, WeakReference> dictionary16 = new Dictionary<uint, WeakReference>();
      Dictionary<object, uint> dictionary17 = new Dictionary<object, uint>();
      Dictionary<uint, object> dictionary18 = new Dictionary<uint, object>();
      Dictionary<long, object> dictionary19 = new Dictionary<long, object>();
      Dictionary<MemberTypes, object> dictionary20 = new Dictionary<MemberTypes, object>();
      EnumEqualityComparer<MemberTypes> equalityComparer = new EnumEqualityComparer<MemberTypes>();
      Dictionary<object, KeyValuePair<object, object>> dictionary21 = new Dictionary<object, KeyValuePair<object, object>>();
      Dictionary<KeyValuePair<object, object>, object> dictionary22 = new Dictionary<KeyValuePair<object, object>, object>();
      Internal.NullableHelper<bool>();
      int num1 = (int) Internal.NullableHelper<byte>();
      int num2 = (int) Internal.NullableHelper<char>();
      Internal.NullableHelper<DateTime>();
      Internal.NullableHelper<Decimal>();
      Internal.NullableHelper<double>();
      Internal.NullableHelper<Guid>();
      int num3 = (int) Internal.NullableHelper<short>();
      Internal.NullableHelper<int>();
      Internal.NullableHelper<long>();
      double num4 = (double) Internal.NullableHelper<float>();
      Internal.NullableHelper<TimeSpan>();
      Internal.NullableHelper<DateTimeOffset>();
      List<bool> boolList = new List<bool>();
      List<byte> byteList = new List<byte>();
      List<char> charList = new List<char>();
      List<DateTime> dateTimeList = new List<DateTime>();
      List<Decimal> numList1 = new List<Decimal>();
      List<double> doubleList = new List<double>();
      List<Guid> guidList = new List<Guid>();
      List<short> shortList = new List<short>();
      List<int> intList = new List<int>();
      List<long> longList = new List<long>();
      List<TimeSpan> timeSpanList = new List<TimeSpan>();
      List<sbyte> sbyteList = new List<sbyte>();
      List<float> floatList = new List<float>();
      List<ushort> ushortList = new List<ushort>();
      List<uint> uintList = new List<uint>();
      List<ulong> ulongList = new List<ulong>();
      List<IntPtr> numList2 = new List<IntPtr>();
      List<KeyValuePair<object, object>> keyValuePairList = new List<KeyValuePair<object, object>>();
      List<GCHandle> gcHandleList = new List<GCHandle>();
      List<DateTimeOffset> dateTimeOffsetList = new List<DateTimeOffset>();
      KeyValuePair<char, ushort> keyValuePair1 = new KeyValuePair<char, ushort>(char.MinValue, (ushort) 0);
      KeyValuePair<ushort, double> keyValuePair2 = new KeyValuePair<ushort, double>((ushort) 0, double.MinValue);
      KeyValuePair<object, int> keyValuePair3 = new KeyValuePair<object, int>((object) string.Empty, int.MinValue);
      KeyValuePair<int, int> keyValuePair4 = new KeyValuePair<int, int>(int.MinValue, int.MinValue);
      Internal.SZArrayHelper<bool>((SZArrayHelper) null);
      Internal.SZArrayHelper<byte>((SZArrayHelper) null);
      Internal.SZArrayHelper<DateTime>((SZArrayHelper) null);
      Internal.SZArrayHelper<Decimal>((SZArrayHelper) null);
      Internal.SZArrayHelper<double>((SZArrayHelper) null);
      Internal.SZArrayHelper<Guid>((SZArrayHelper) null);
      Internal.SZArrayHelper<short>((SZArrayHelper) null);
      Internal.SZArrayHelper<int>((SZArrayHelper) null);
      Internal.SZArrayHelper<long>((SZArrayHelper) null);
      Internal.SZArrayHelper<TimeSpan>((SZArrayHelper) null);
      Internal.SZArrayHelper<sbyte>((SZArrayHelper) null);
      Internal.SZArrayHelper<float>((SZArrayHelper) null);
      Internal.SZArrayHelper<ushort>((SZArrayHelper) null);
      Internal.SZArrayHelper<uint>((SZArrayHelper) null);
      Internal.SZArrayHelper<ulong>((SZArrayHelper) null);
      Internal.SZArrayHelper<DateTimeOffset>((SZArrayHelper) null);
      Internal.SZArrayHelper<CustomAttributeTypedArgument>((SZArrayHelper) null);
      Internal.SZArrayHelper<CustomAttributeNamedArgument>((SZArrayHelper) null);
    }

    private static T NullableHelper<T>() where T : struct
    {
      Nullable.Compare<T>(new T?(), new T?());
      Nullable.Equals<T>(new T?(), new T?());
      return new T?().GetValueOrDefault();
    }

    private static void SZArrayHelper<T>(SZArrayHelper oSZArrayHelper)
    {
      oSZArrayHelper.get_Count<T>();
      oSZArrayHelper.get_Item<T>(0);
      oSZArrayHelper.GetEnumerator<T>();
    }

    [SecurityCritical]
    private static void CommonlyUsedWinRTRedirectedInterfaceStubs()
    {
      Internal.WinRT_IEnumerable<byte>((IterableToEnumerableAdapter) null, (EnumerableToIterableAdapter) null, (IIterable<byte>) null);
      Internal.WinRT_IEnumerable<char>((IterableToEnumerableAdapter) null, (EnumerableToIterableAdapter) null, (IIterable<char>) null);
      Internal.WinRT_IEnumerable<short>((IterableToEnumerableAdapter) null, (EnumerableToIterableAdapter) null, (IIterable<short>) null);
      Internal.WinRT_IEnumerable<ushort>((IterableToEnumerableAdapter) null, (EnumerableToIterableAdapter) null, (IIterable<ushort>) null);
      Internal.WinRT_IEnumerable<int>((IterableToEnumerableAdapter) null, (EnumerableToIterableAdapter) null, (IIterable<int>) null);
      Internal.WinRT_IEnumerable<uint>((IterableToEnumerableAdapter) null, (EnumerableToIterableAdapter) null, (IIterable<uint>) null);
      Internal.WinRT_IEnumerable<long>((IterableToEnumerableAdapter) null, (EnumerableToIterableAdapter) null, (IIterable<long>) null);
      Internal.WinRT_IEnumerable<ulong>((IterableToEnumerableAdapter) null, (EnumerableToIterableAdapter) null, (IIterable<ulong>) null);
      Internal.WinRT_IEnumerable<float>((IterableToEnumerableAdapter) null, (EnumerableToIterableAdapter) null, (IIterable<float>) null);
      Internal.WinRT_IEnumerable<double>((IterableToEnumerableAdapter) null, (EnumerableToIterableAdapter) null, (IIterable<double>) null);
      Internal.WinRT_IEnumerable<string>((IterableToEnumerableAdapter) null, (EnumerableToIterableAdapter) null, (IIterable<string>) null);
      typeof (IIterable<string>).ToString();
      typeof (IIterator<string>).ToString();
      Internal.WinRT_IEnumerable<object>((IterableToEnumerableAdapter) null, (EnumerableToIterableAdapter) null, (IIterable<object>) null);
      typeof (IIterable<object>).ToString();
      typeof (IIterator<object>).ToString();
      Internal.WinRT_IList<int>((VectorToListAdapter) null, (VectorToCollectionAdapter) null, (ListToVectorAdapter) null, (IVector<int>) null);
      Internal.WinRT_IList<string>((VectorToListAdapter) null, (VectorToCollectionAdapter) null, (ListToVectorAdapter) null, (IVector<string>) null);
      typeof (IVector<string>).ToString();
      Internal.WinRT_IList<object>((VectorToListAdapter) null, (VectorToCollectionAdapter) null, (ListToVectorAdapter) null, (IVector<object>) null);
      typeof (IVector<object>).ToString();
      Internal.WinRT_IReadOnlyList<int>((IVectorViewToIReadOnlyListAdapter) null, (IReadOnlyListToIVectorViewAdapter) null, (IVectorView<int>) null);
      Internal.WinRT_IReadOnlyList<string>((IVectorViewToIReadOnlyListAdapter) null, (IReadOnlyListToIVectorViewAdapter) null, (IVectorView<string>) null);
      typeof (IVectorView<string>).ToString();
      Internal.WinRT_IReadOnlyList<object>((IVectorViewToIReadOnlyListAdapter) null, (IReadOnlyListToIVectorViewAdapter) null, (IVectorView<object>) null);
      typeof (IVectorView<object>).ToString();
      Internal.WinRT_IDictionary<string, int>((MapToDictionaryAdapter) null, (MapToCollectionAdapter) null, (DictionaryToMapAdapter) null, (IMap<string, int>) null);
      typeof (IMap<string, int>).ToString();
      Internal.WinRT_IDictionary<string, string>((MapToDictionaryAdapter) null, (MapToCollectionAdapter) null, (DictionaryToMapAdapter) null, (IMap<string, string>) null);
      typeof (IMap<string, string>).ToString();
      Internal.WinRT_IDictionary<string, object>((MapToDictionaryAdapter) null, (MapToCollectionAdapter) null, (DictionaryToMapAdapter) null, (IMap<string, object>) null);
      typeof (IMap<string, object>).ToString();
      Internal.WinRT_IDictionary<object, object>((MapToDictionaryAdapter) null, (MapToCollectionAdapter) null, (DictionaryToMapAdapter) null, (IMap<object, object>) null);
      typeof (IMap<object, object>).ToString();
      Internal.WinRT_IReadOnlyDictionary<string, int>((IMapViewToIReadOnlyDictionaryAdapter) null, (IReadOnlyDictionaryToIMapViewAdapter) null, (IMapView<string, int>) null, (MapViewToReadOnlyCollectionAdapter) null);
      typeof (IMapView<string, int>).ToString();
      Internal.WinRT_IReadOnlyDictionary<string, string>((IMapViewToIReadOnlyDictionaryAdapter) null, (IReadOnlyDictionaryToIMapViewAdapter) null, (IMapView<string, string>) null, (MapViewToReadOnlyCollectionAdapter) null);
      typeof (IMapView<string, string>).ToString();
      Internal.WinRT_IReadOnlyDictionary<string, object>((IMapViewToIReadOnlyDictionaryAdapter) null, (IReadOnlyDictionaryToIMapViewAdapter) null, (IMapView<string, object>) null, (MapViewToReadOnlyCollectionAdapter) null);
      typeof (IMapView<string, object>).ToString();
      Internal.WinRT_IReadOnlyDictionary<object, object>((IMapViewToIReadOnlyDictionaryAdapter) null, (IReadOnlyDictionaryToIMapViewAdapter) null, (IMapView<object, object>) null, (MapViewToReadOnlyCollectionAdapter) null);
      typeof (IMapView<object, object>).ToString();
      Internal.WinRT_Nullable<bool>();
      Internal.WinRT_Nullable<byte>();
      Internal.WinRT_Nullable<int>();
      Internal.WinRT_Nullable<uint>();
      Internal.WinRT_Nullable<long>();
      Internal.WinRT_Nullable<ulong>();
      Internal.WinRT_Nullable<float>();
      Internal.WinRT_Nullable<double>();
    }

    [SecurityCritical]
    private static void WinRT_IEnumerable<T>(IterableToEnumerableAdapter iterableToEnumerableAdapter, EnumerableToIterableAdapter enumerableToIterableAdapter, IIterable<T> iterable)
    {
      iterableToEnumerableAdapter.GetEnumerator_Stub<T>();
      enumerableToIterableAdapter.First_Stub<T>();
    }

    [SecurityCritical]
    private static void WinRT_IList<T>(VectorToListAdapter vectorToListAdapter, VectorToCollectionAdapter vectorToCollectionAdapter, ListToVectorAdapter listToVectorAdapter, IVector<T> vector)
    {
      Internal.WinRT_IEnumerable<T>((IterableToEnumerableAdapter) null, (EnumerableToIterableAdapter) null, (IIterable<T>) null);
      vectorToListAdapter.Indexer_Get<T>(0);
      vectorToListAdapter.Indexer_Set<T>(0, default (T));
      vectorToListAdapter.Insert<T>(0, default (T));
      vectorToListAdapter.RemoveAt<T>(0);
      vectorToCollectionAdapter.Count<T>();
      vectorToCollectionAdapter.Add<T>(default (T));
      vectorToCollectionAdapter.Clear<T>();
      listToVectorAdapter.GetAt<T>(0U);
      int num = (int) listToVectorAdapter.Size<T>();
      listToVectorAdapter.SetAt<T>(0U, default (T));
      listToVectorAdapter.InsertAt<T>(0U, default (T));
      listToVectorAdapter.RemoveAt<T>(0U);
      listToVectorAdapter.Append<T>(default (T));
      listToVectorAdapter.RemoveAtEnd<T>();
      listToVectorAdapter.Clear<T>();
    }

    [SecurityCritical]
    private static void WinRT_IReadOnlyCollection<T>(VectorViewToReadOnlyCollectionAdapter vectorViewToReadOnlyCollectionAdapter)
    {
      Internal.WinRT_IEnumerable<T>((IterableToEnumerableAdapter) null, (EnumerableToIterableAdapter) null, (IIterable<T>) null);
      vectorViewToReadOnlyCollectionAdapter.Count<T>();
    }

    [SecurityCritical]
    private static void WinRT_IReadOnlyList<T>(IVectorViewToIReadOnlyListAdapter vectorToListAdapter, IReadOnlyListToIVectorViewAdapter listToVectorAdapter, IVectorView<T> vectorView)
    {
      Internal.WinRT_IEnumerable<T>((IterableToEnumerableAdapter) null, (EnumerableToIterableAdapter) null, (IIterable<T>) null);
      Internal.WinRT_IReadOnlyCollection<T>((VectorViewToReadOnlyCollectionAdapter) null);
      vectorToListAdapter.Indexer_Get<T>(0);
      listToVectorAdapter.GetAt<T>(0U);
      int num = (int) listToVectorAdapter.Size<T>();
    }

    [SecurityCritical]
    private static void WinRT_IDictionary<K, V>(MapToDictionaryAdapter mapToDictionaryAdapter, MapToCollectionAdapter mapToCollectionAdapter, DictionaryToMapAdapter dictionaryToMapAdapter, IMap<K, V> map)
    {
      Internal.WinRT_IEnumerable<KeyValuePair<K, V>>((IterableToEnumerableAdapter) null, (EnumerableToIterableAdapter) null, (IIterable<KeyValuePair<K, V>>) null);
      mapToDictionaryAdapter.Indexer_Get<K, V>(default (K));
      mapToDictionaryAdapter.Indexer_Set<K, V>(default (K), default (V));
      mapToDictionaryAdapter.ContainsKey<K, V>(default (K));
      mapToDictionaryAdapter.Add<K, V>(default (K), default (V));
      mapToDictionaryAdapter.Remove<K, V>(default (K));
      V v;
      mapToDictionaryAdapter.TryGetValue<K, V>(default (K), out v);
      mapToCollectionAdapter.Count<K, V>();
      mapToCollectionAdapter.Add<K, V>(new KeyValuePair<K, V>(default (K), default (V)));
      mapToCollectionAdapter.Clear<K, V>();
      dictionaryToMapAdapter.Lookup<K, V>(default (K));
      int num = (int) dictionaryToMapAdapter.Size<K, V>();
      dictionaryToMapAdapter.HasKey<K, V>(default (K));
      dictionaryToMapAdapter.Insert<K, V>(default (K), default (V));
      dictionaryToMapAdapter.Remove<K, V>(default (K));
      dictionaryToMapAdapter.Clear<K, V>();
    }

    [SecurityCritical]
    private static void WinRT_IReadOnlyDictionary<K, V>(IMapViewToIReadOnlyDictionaryAdapter mapToDictionaryAdapter, IReadOnlyDictionaryToIMapViewAdapter dictionaryToMapAdapter, IMapView<K, V> mapView, MapViewToReadOnlyCollectionAdapter mapViewToReadOnlyCollectionAdapter)
    {
      Internal.WinRT_IEnumerable<KeyValuePair<K, V>>((IterableToEnumerableAdapter) null, (EnumerableToIterableAdapter) null, (IIterable<KeyValuePair<K, V>>) null);
      Internal.WinRT_IReadOnlyCollection<KeyValuePair<K, V>>((VectorViewToReadOnlyCollectionAdapter) null);
      mapToDictionaryAdapter.Indexer_Get<K, V>(default (K));
      mapToDictionaryAdapter.ContainsKey<K, V>(default (K));
      V v;
      mapToDictionaryAdapter.TryGetValue<K, V>(default (K), out v);
      mapViewToReadOnlyCollectionAdapter.Count<K, V>();
      dictionaryToMapAdapter.Lookup<K, V>(default (K));
      int num = (int) dictionaryToMapAdapter.Size<K, V>();
      dictionaryToMapAdapter.HasKey<K, V>(default (K));
    }

    [SecurityCritical]
    private static void WinRT_Nullable<T>() where T : struct
    {
      T? nullable = new T?();
      NullableMarshaler.ConvertToNative<T>(ref nullable);
      NullableMarshaler.ConvertToManagedRetVoid<T>(IntPtr.Zero, ref nullable);
    }
  }
}

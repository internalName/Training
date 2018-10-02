// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.IMPLTYPEFLAGS
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Определяет атрибуты реализованного или унаследованного интерфейса типа.
  /// </summary>
  [Flags]
  [__DynamicallyInvokable]
  [Serializable]
  public enum IMPLTYPEFLAGS
  {
    [__DynamicallyInvokable] IMPLTYPEFLAG_FDEFAULT = 1,
    [__DynamicallyInvokable] IMPLTYPEFLAG_FSOURCE = 2,
    [__DynamicallyInvokable] IMPLTYPEFLAG_FRESTRICTED = 4,
    [__DynamicallyInvokable] IMPLTYPEFLAG_FDEFAULTVTABLE = 8,
  }
}

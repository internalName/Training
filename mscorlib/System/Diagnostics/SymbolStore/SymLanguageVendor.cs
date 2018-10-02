// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.SymLanguageVendor
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>
  ///   Хранит открытые идентификаторы GUID для поставщиков языков, используемые в хранилище символов.
  /// </summary>
  [ComVisible(true)]
  public class SymLanguageVendor
  {
    /// <summary>
    ///   Задает идентификатор GUID поставщика языка Microsoft.
    /// </summary>
    public static readonly Guid Microsoft = new Guid(-1723120188, (short) -6423, (short) 4562, (byte) 144, (byte) 63, (byte) 0, (byte) 192, (byte) 79, (byte) 163, (byte) 2, (byte) 161);
  }
}

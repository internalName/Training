// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.ISymbolVariable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>Представляет переменную в хранилище символов.</summary>
  [ComVisible(true)]
  public interface ISymbolVariable
  {
    /// <summary>Возвращает имя переменной.</summary>
    /// <returns>Имя переменной.</returns>
    string Name { get; }

    /// <summary>Возвращает атрибуты переменной.</summary>
    /// <returns>Атрибуты переменной.</returns>
    object Attributes { get; }

    /// <summary>Возвращает подпись переменной.</summary>
    /// <returns>
    ///   Подпись переменной в виде непрозрачного большого двоичного объекта.
    /// </returns>
    byte[] GetSignature();

    /// <summary>
    ///   Возвращает значение <see cref="T:System.Diagnostics.SymbolStore.SymAddressKind" />, описывающее тип адреса.
    /// </summary>
    /// <returns>
    ///   Тип адреса.
    ///    Одно из значений <see cref="T:System.Diagnostics.SymbolStore.SymAddressKind" />.
    /// </returns>
    SymAddressKind AddressKind { get; }

    /// <summary>Возвращает первый адрес переменной.</summary>
    /// <returns>Первый адрес переменной.</returns>
    int AddressField1 { get; }

    /// <summary>Возвращает второй адрес переменной.</summary>
    /// <returns>Второй адрес переменной.</returns>
    int AddressField2 { get; }

    /// <summary>Возвращает третий адрес переменной.</summary>
    /// <returns>Третий адрес переменной.</returns>
    int AddressField3 { get; }

    /// <summary>
    ///   Возвращает начальное смещение переменной в области этой переменной.
    /// </summary>
    /// <returns>Начальное смещение переменной.</returns>
    int StartOffset { get; }

    /// <summary>
    ///   Возвращает конечное смещение переменной в области этой переменной.
    /// </summary>
    /// <returns>Конечное смещение переменной.</returns>
    int EndOffset { get; }
  }
}

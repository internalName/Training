// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.ISymbolScope
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>
  ///   Представляет лексическую область видимости в объекте <see cref="T:System.Diagnostics.SymbolStore.ISymbolMethod" />, обеспечивая доступ к начальному и конечному смещениям этой области, а также к ее дочерним и родительской областям.
  /// </summary>
  [ComVisible(true)]
  public interface ISymbolScope
  {
    /// <summary>
    ///   Возвращает метод, содержащий текущую лексическую область видимости.
    /// </summary>
    /// <returns>
    ///   Метод, содержащий текущую лексическую область видимости.
    /// </returns>
    ISymbolMethod Method { get; }

    /// <summary>
    ///   Возвращает родительскую лексическую область видимости текущей области видимости.
    /// </summary>
    /// <returns>Родительский лексическая область текущей области.</returns>
    ISymbolScope Parent { get; }

    /// <summary>
    ///   Возвращает дочерние лексические области видимости текущей лексической области видимости.
    /// </summary>
    /// <returns>
    ///   Дочерние лексические области, текущей лексической области.
    /// </returns>
    ISymbolScope[] GetChildren();

    /// <summary>
    ///   Возвращает начальное смещение текущей лексической области видимости.
    /// </summary>
    /// <returns>Начальное смещение текущей лексической области.</returns>
    int StartOffset { get; }

    /// <summary>
    ///   Возвращает конечное смещение текущей лексической области видимости.
    /// </summary>
    /// <returns>Конечное смещение текущей лексической области.</returns>
    int EndOffset { get; }

    /// <summary>
    ///   Возвращает локальные переменные в текущей лексической области видимости.
    /// </summary>
    /// <returns>
    ///   Локальные переменные в текущей лексической области видимости.
    /// </returns>
    ISymbolVariable[] GetLocals();

    /// <summary>
    ///   Возвращает пространства имен, используемые в текущей области.
    /// </summary>
    /// <returns>Пространства имен, используемые в текущей области.</returns>
    ISymbolNamespace[] GetNamespaces();
  }
}

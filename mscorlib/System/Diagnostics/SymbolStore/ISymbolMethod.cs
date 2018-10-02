// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.ISymbolMethod
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>Представляет метод в хранилище символов.</summary>
  [ComVisible(true)]
  public interface ISymbolMethod
  {
    /// <summary>
    ///   Возвращает объект <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" />, содержащий метаданные для текущего метода.
    /// </summary>
    /// <returns>Токен метаданных для текущего метода.</returns>
    SymbolToken Token { get; }

    /// <summary>Возвращает число точек следования в методе.</summary>
    /// <returns>Число точек следования в методе.</returns>
    int SequencePointCount { get; }

    /// <summary>Возвращает точки следования для текущего метода.</summary>
    /// <param name="offsets">
    ///   Массив смещений в байтах от начала метода для точек следования.
    /// </param>
    /// <param name="documents">
    ///   Массив документов, в которых находятся точки следования.
    /// </param>
    /// <param name="lines">
    ///   Массив строк в документах, в которых находятся точки следования.
    /// </param>
    /// <param name="columns">
    ///   Массив столбцов в документах, в которых находятся точки следования.
    /// </param>
    /// <param name="endLines">
    ///   Массив строк в документах, в которых заканчиваются точки следования.
    /// </param>
    /// <param name="endColumns">
    ///   Массив столбцов в документах, в которых заканчиваются точки следования.
    /// </param>
    void GetSequencePoints(int[] offsets, ISymbolDocument[] documents, int[] lines, int[] columns, int[] endLines, int[] endColumns);

    /// <summary>
    ///   Возвращает корневую лексическую область для текущего метода.
    ///    Эта область включает весь метод.
    /// </summary>
    /// <returns>
    ///   Корневая лексическая область, которая включает весь метод.
    /// </returns>
    ISymbolScope RootScope { get; }

    /// <summary>
    ///   Возвращает наиболее узкую внешнюю лексическую область, если задано смещение в методе.
    /// </summary>
    /// <param name="offset">
    ///   Смещение в байтах лексической области в методе.
    /// </param>
    /// <returns>
    ///   Наиболее узкая внешняя лексическая область для заданного смещения в байтах в методе.
    /// </returns>
    ISymbolScope GetScope(int offset);

    /// <summary>
    ///   Возвращает смещение на языке MSIL в методе, соответствующее заданной позиции.
    /// </summary>
    /// <param name="document">
    ///   Документ, для которого запрашивается смещение.
    /// </param>
    /// <param name="line">
    ///   Строка документа, соответствующая смещению.
    /// </param>
    /// <param name="column">
    ///   Столбец документа, соответствующий смещению.
    /// </param>
    /// <returns>Смещение в заданном документе.</returns>
    int GetOffset(ISymbolDocument document, int line, int column);

    /// <summary>
    ///   Возвращает массив пар начального и конечного смещения, соответствующих диапазонам на языке MSIL, занимаемым данной позицией в этом методе.
    /// </summary>
    /// <param name="document">
    ///   Документ, для которого запрашивается смещение.
    /// </param>
    /// <param name="line">
    ///   Строка документа, соответствующая этим диапазонам.
    /// </param>
    /// <param name="column">
    ///   Столбец документа, соответствующий этим диапазонам.
    /// </param>
    /// <returns>Массив пар начального и конечного смещения.</returns>
    int[] GetRanges(ISymbolDocument document, int line, int column);

    /// <summary>Возвращает параметры текущего метода.</summary>
    /// <returns>Массив параметров текущего метода.</returns>
    ISymbolVariable[] GetParameters();

    /// <summary>
    ///   Возвращает пространство имен, в котором определен текущий метод.
    /// </summary>
    /// <returns>
    ///   Пространство имен, в котором определен текущий метод.
    /// </returns>
    ISymbolNamespace GetNamespace();

    /// <summary>
    ///   Возвращает начальную и конечную позицию для исходных документов текущего метода.
    /// </summary>
    /// <param name="docs">
    ///   Начальный и конечный исходные документы.
    /// </param>
    /// <param name="lines">
    ///   Начальная и конечная строки соответствующих исходных документов.
    /// </param>
    /// <param name="columns">
    ///   Начальный и конечный столбцы соответствующих исходных документов.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если эти позиции определены; в противном случае — <see langword="false" />.
    /// </returns>
    bool GetSourceStartEnd(ISymbolDocument[] docs, int[] lines, int[] columns);
  }
}

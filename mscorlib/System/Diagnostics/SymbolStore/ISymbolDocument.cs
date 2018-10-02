// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.ISymbolDocument
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>
  ///   Представляет документ, на который ссылается хранилище символов.
  /// </summary>
  [ComVisible(true)]
  public interface ISymbolDocument
  {
    /// <summary>Возвращает URL-адрес текущего документа.</summary>
    /// <returns>URL-адрес текущего документа.</returns>
    string URL { get; }

    /// <summary>Возвращает тип текущего документа.</summary>
    /// <returns>Тип текущего документа.</returns>
    Guid DocumentType { get; }

    /// <summary>Возвращает язык текущего документа.</summary>
    /// <returns>Язык текущего документа.</returns>
    Guid Language { get; }

    /// <summary>Возвращает поставщика языка текущего документа.</summary>
    /// <returns>Поставщик языка текущего документа.</returns>
    Guid LanguageVendor { get; }

    /// <summary>
    ///   Возвращает идентификатор алгоритма подсчета контрольной суммы.
    /// </summary>
    /// <returns>
    ///   Идентификатор GUID алгоритма подсчета контрольной суммы.
    ///    Все значения равны нулю, если контрольная сумма отсутствует.
    /// </returns>
    Guid CheckSumAlgorithmId { get; }

    /// <summary>Возвращает контрольную сумму.</summary>
    /// <returns>Контрольная сумма.</returns>
    byte[] GetCheckSum();

    /// <summary>
    ///   Возвращает ближайшую строку, являющуюся точкой следования, для заданной строки документа, которая может являться или не являться точкой следования.
    /// </summary>
    /// <param name="line">Заданная строка в документе.</param>
    /// <returns>Ближайшая строка, являющаяся точкой следования.</returns>
    int FindClosestLine(int line);

    /// <summary>
    ///   Проверяет, хранится ли текущий документ в хранилище символов.
    /// </summary>
    /// <returns>
    ///   Возвращает значение <see langword="true" />, если текущий документ хранится в хранилище символов; в противном случае — <see langword="false" />.
    /// </returns>
    bool HasEmbeddedSource { get; }

    /// <summary>Возвращает длину внедренного источника в байтах.</summary>
    /// <returns>Длина источника текущего документа.</returns>
    int SourceLength { get; }

    /// <summary>
    ///   Возвращает внедренный источник документа для указанного диапазона.
    /// </summary>
    /// <param name="startLine">
    ///   Начальная строка текущего документа.
    /// </param>
    /// <param name="startColumn">
    ///   Начальный столбец текущего документа.
    /// </param>
    /// <param name="endLine">Конечная строка текущего документа.</param>
    /// <param name="endColumn">
    ///   Конечный столбец текущего документа.
    /// </param>
    /// <returns>Источник документа для указанного диапазона.</returns>
    byte[] GetSourceRange(int startLine, int startColumn, int endLine, int endColumn);
  }
}

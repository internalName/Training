// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.ISymbolReader
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>
  ///   Представляет средство чтения символов для управляемого кода.
  /// </summary>
  [ComVisible(true)]
  public interface ISymbolReader
  {
    /// <summary>
    ///   Получает документ на основании заданного языка, поставщика и типа.
    /// </summary>
    /// <param name="url">
    ///   URL-адрес, предназначенный для идентификации документа.
    /// </param>
    /// <param name="language">
    ///   Язык документа.
    ///    Этому параметру можно присвоить значение <see cref="F:System.Guid.Empty" />.
    /// </param>
    /// <param name="languageVendor">
    ///   Удостоверение поставщика языка документа.
    ///    Этому параметру можно присвоить значение <see cref="F:System.Guid.Empty" />.
    /// </param>
    /// <param name="documentType">
    ///   Тип документа.
    ///    Этому параметру можно присвоить значение <see cref="F:System.Guid.Empty" />.
    /// </param>
    /// <returns>Указанный документ.</returns>
    ISymbolDocument GetDocument(string url, Guid language, Guid languageVendor, Guid documentType);

    /// <summary>
    ///   Получает массив всех документов, определенных в хранилище символов.
    /// </summary>
    /// <returns>
    ///   Массив всех документов, определенных в хранилище символов.
    /// </returns>
    ISymbolDocument[] GetDocuments();

    /// <summary>
    ///   Получает токен метаданных для метода, заданного в качестве точки входа пользователя для модуля (если существует).
    /// </summary>
    /// <returns>
    ///   Токен метаданных для метода, который является точкой входа пользователя для модуля.
    /// </returns>
    SymbolToken UserEntryPoint { get; }

    /// <summary>
    ///   Получает объект метода средства чтения символов для заданного идентификатора метода.
    /// </summary>
    /// <param name="method">Токен метаданных метода.</param>
    /// <returns>
    ///   Объект метода средства чтения символов для заданного идентификатора метода.
    /// </returns>
    ISymbolMethod GetMethod(SymbolToken method);

    /// <summary>
    ///   Получает объект метода средства чтения символов для заданных идентификатора метода и его версии в режиме "Изменить и продолжить".
    /// </summary>
    /// <param name="method">Токен метаданных метода.</param>
    /// <param name="version">
    ///   Версия в режиме "Изменить и продолжить" этого метода.
    /// </param>
    /// <returns>
    ///   Объект метода средства чтения символов для заданного идентификатора метода.
    /// </returns>
    ISymbolMethod GetMethod(SymbolToken method, int version);

    /// <summary>
    ///   Возвращает переменные, которые не являются локальными, для заданного родительского объекта.
    /// </summary>
    /// <param name="parent">
    ///   Токен метаданных для типа, для которого запрашиваются эти переменные.
    /// </param>
    /// <returns>Массив переменных для родительского элемента.</returns>
    ISymbolVariable[] GetVariables(SymbolToken parent);

    /// <summary>Возвращает все глобальные переменные в модуле.</summary>
    /// <returns>Массив всех переменных в модуле.</returns>
    ISymbolVariable[] GetGlobalVariables();

    /// <summary>
    ///   Получает объект метода средства чтения символов, содержащий указанную позицию в документе.
    /// </summary>
    /// <param name="document">
    ///   Документ, в котором находится метод.
    /// </param>
    /// <param name="line">
    ///   Позиция строки в документе.
    ///    Нумерация строк начинается с 1.
    /// </param>
    /// <param name="column">
    ///   Позиция столбца в документе.
    ///    Нумерация столбцов начинается с 1.
    /// </param>
    /// <returns>
    ///   Объект метода средства чтения для заданной позиции в документе.
    /// </returns>
    ISymbolMethod GetMethodFromDocumentPosition(ISymbolDocument document, int line, int column);

    /// <summary>
    ///   Возвращает значение атрибута для заданного имени атрибута.
    /// </summary>
    /// <param name="parent">
    ///   Токен метаданных для объекта, для которого запрашивается атрибут.
    /// </param>
    /// <param name="name">Имя атрибута.</param>
    /// <returns>Значение атрибута.</returns>
    byte[] GetSymAttribute(SymbolToken parent, string name);

    /// <summary>
    ///   Возвращает пространства имен, определенные в глобальной области в текущем хранилище символов.
    /// </summary>
    /// <returns>
    ///   Пространства имен, определенные в глобальной области в текущем хранилище символов.
    /// </returns>
    ISymbolNamespace[] GetNamespaces();
  }
}

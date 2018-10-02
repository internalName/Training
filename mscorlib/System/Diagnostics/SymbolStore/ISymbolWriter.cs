// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.ISymbolWriter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>
  ///   Представляет модуль записи символов для управляемого кода.
  /// </summary>
  [ComVisible(true)]
  public interface ISymbolWriter
  {
    /// <summary>
    ///   Задает интерфейс включения метаданных, чтобы связать со средством записи.
    /// </summary>
    /// <param name="emitter">Интерфейс порождения метаданных.</param>
    /// <param name="filename">
    ///   Имя файла, для которого записываются символы отладки.
    ///    Имя файла требуется всех модулей записи, а другие — нет.
    ///    Если имя файла задано для модуля записи, который не использует имена файлов, этот параметр пропускается.
    /// </param>
    /// <param name="fFullBuild">
    ///   <see langword="true" /> Указывает, что это полное перестроение; <see langword="false" /> Указывает, что это добавочная компиляция.
    /// </param>
    void Initialize(IntPtr emitter, string filename, bool fFullBuild);

    /// <summary>Определяет исходный документ.</summary>
    /// <param name="url">
    ///   URL-адрес, предназначенный для идентификации документа.
    /// </param>
    /// <param name="language">
    ///   Язык документа.
    ///    Этот параметр может иметь значение <see cref="F:System.Guid.Empty" />.
    /// </param>
    /// <param name="languageVendor">
    ///   Удостоверение поставщика языка документа.
    ///    Этот параметр может иметь значение <see cref="F:System.Guid.Empty" />.
    /// </param>
    /// <param name="documentType">
    ///   Тип документа.
    ///    Этот параметр может иметь значение <see cref="F:System.Guid.Empty" />.
    /// </param>
    /// <returns>Объект, представляющий документ.</returns>
    ISymbolDocumentWriter DefineDocument(string url, Guid language, Guid languageVendor, Guid documentType);

    /// <summary>
    ///   Идентифицирует пользовательский метод в качестве точки входа для текущего модуля.
    /// </summary>
    /// <param name="entryMethod">
    ///   Токен метаданных для метода, который является пользовательской точкой входа.
    /// </param>
    void SetUserEntryPoint(SymbolToken entryMethod);

    /// <summary>
    ///   Открывает метод для размещения символьной информации.
    /// </summary>
    /// <param name="method">
    ///   Токен метаданных для открываемого метода.
    /// </param>
    void OpenMethod(SymbolToken method);

    /// <summary>Закрывает текущий метод.</summary>
    void CloseMethod();

    /// <summary>
    ///   Определяет группу точек следования в текущем методе.
    /// </summary>
    /// <param name="document">
    ///   Объект документа, для которого определяются точки следования.
    /// </param>
    /// <param name="offsets">
    ///   Смещения точек следования определяются от начала методов.
    /// </param>
    /// <param name="lines">Строки документа для точек следования.</param>
    /// <param name="columns">
    ///   Позиции документа для точек следования.
    /// </param>
    /// <param name="endLines">
    ///   Конечные строки документа для точек следования.
    /// </param>
    /// <param name="endColumns">
    ///   Конечные позиции документа для точек следования.
    /// </param>
    void DefineSequencePoints(ISymbolDocumentWriter document, int[] offsets, int[] lines, int[] columns, int[] endLines, int[] endColumns);

    /// <summary>
    ///   Открывает новую лексическую область видимости в текущем методе.
    /// </summary>
    /// <param name="startOffset">
    ///   Смещение в байтах от начала метода до первой инструкции в лексической области.
    /// </param>
    /// <returns>
    ///   Непрозрачный идентификатор области видимости, который можно использовать с методом <see cref="M:System.Diagnostics.SymbolStore.ISymbolWriter.SetScopeRange(System.Int32,System.Int32,System.Int32)" /> для определения начального и конечного смещений области видимости в дальнейшем.
    ///    В этом случае смещения, переданные методам <see cref="M:System.Diagnostics.SymbolStore.ISymbolWriter.OpenScope(System.Int32)" /> и <see cref="M:System.Diagnostics.SymbolStore.ISymbolWriter.CloseScope(System.Int32)" />, пропускаются.
    ///    Идентификатор области видимости действителен только в текущем методе.
    /// </returns>
    int OpenScope(int startOffset);

    /// <summary>Закрывает текущую лексическую область видимости.</summary>
    /// <param name="endOffset">
    ///   Точки после последней инструкции в области.
    /// </param>
    void CloseScope(int endOffset);

    /// <summary>
    ///   Определяет диапазон смещений для заданной лексической области видимости.
    /// </summary>
    /// <param name="scopeID">Идентификатор лексической области.</param>
    /// <param name="startOffset">
    ///   Смещение начала лексической области в байтах.
    /// </param>
    /// <param name="endOffset">
    ///   Смещение байтов конца лексической области.
    /// </param>
    void SetScopeRange(int scopeID, int startOffset, int endOffset);

    /// <summary>
    ///   Определяет одну переменную в текущей лексической области видимости.
    /// </summary>
    /// <param name="name">Имя локальной переменной.</param>
    /// <param name="attributes">
    ///   Побитовое сочетание атрибутов локальной переменной.
    /// </param>
    /// <param name="signature">Подпись локальной переменной.</param>
    /// <param name="addrKind">
    ///   Типы адресов <paramref name="addr1" />, <paramref name="addr2" />, и <paramref name="addr3" />.
    /// </param>
    /// <param name="addr1">
    ///   Первый адрес для спецификации локальной переменной.
    /// </param>
    /// <param name="addr2">
    ///   Второй адрес для спецификации локальной переменной.
    /// </param>
    /// <param name="addr3">
    ///   Третий адрес для спецификации локальной переменной.
    /// </param>
    /// <param name="startOffset">
    ///   Начальное смещение для переменной.
    ///    Если этот параметр равен нулю, он обрабатывается и переменная определяется для всей области.
    ///    Если параметр имеет ненулевое значение, переменная находится в границах смещений текущей области.
    /// </param>
    /// <param name="endOffset">
    ///   Конечное смещение для переменной.
    ///    Если этот параметр равен нулю, он обрабатывается и переменная определяется для всей области.
    ///    Если параметр имеет ненулевое значение, переменная находится в границах смещений текущей области.
    /// </param>
    void DefineLocalVariable(string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3, int startOffset, int endOffset);

    /// <summary>
    ///   Определяет отдельный параметр в текущем методе.
    ///    Тип каждого параметра извлекается из его позиции в подписи метода.
    /// </summary>
    /// <param name="name">Имя параметра.</param>
    /// <param name="attributes">
    ///   Побитовое сочетание атрибутов параметра.
    /// </param>
    /// <param name="sequence">Сигнатура параметра.</param>
    /// <param name="addrKind">
    ///   Типы адресов <paramref name="addr1" />, <paramref name="addr2" />, и <paramref name="addr3" />.
    /// </param>
    /// <param name="addr1">
    ///   Первый адрес для спецификации параметра.
    /// </param>
    /// <param name="addr2">
    ///   Второй адрес для спецификации параметра.
    /// </param>
    /// <param name="addr3">
    ///   Третий адрес для спецификации параметра.
    /// </param>
    void DefineParameter(string name, ParameterAttributes attributes, int sequence, SymAddressKind addrKind, int addr1, int addr2, int addr3);

    /// <summary>Определяет поле в типе или глобальное поле.</summary>
    /// <param name="parent">Метаданные типа или метода маркера.</param>
    /// <param name="name">Имя поля.</param>
    /// <param name="attributes">
    ///   Побитовое сочетание атрибутов полей.
    /// </param>
    /// <param name="signature">Подпись поля.</param>
    /// <param name="addrKind">
    ///   Типы адресов <paramref name="addr1" /> и <paramref name="addr2" />.
    /// </param>
    /// <param name="addr1">Первый адрес для спецификации поля.</param>
    /// <param name="addr2">Второй адрес для спецификации поля.</param>
    /// <param name="addr3">Третий адрес для спецификации поля.</param>
    void DefineField(SymbolToken parent, string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3);

    /// <summary>Определяет одну глобальную переменную.</summary>
    /// <param name="name">Имя глобальной переменной.</param>
    /// <param name="attributes">
    ///   Побитовое сочетание атрибутов глобальной переменной.
    /// </param>
    /// <param name="signature">Подпись глобальной переменной.</param>
    /// <param name="addrKind">
    ///   Типы адресов <paramref name="addr1" />, <paramref name="addr2" />, и <paramref name="addr3" />.
    /// </param>
    /// <param name="addr1">
    ///   Первый адрес для спецификации глобальной переменной.
    /// </param>
    /// <param name="addr2">
    ///   Второй адрес для спецификации глобальной переменной.
    /// </param>
    /// <param name="addr3">
    ///   Третий адрес для спецификации глобальной переменной.
    /// </param>
    void DefineGlobalVariable(string name, FieldAttributes attributes, byte[] signature, SymAddressKind addrKind, int addr1, int addr2, int addr3);

    /// <summary>
    ///   Закрывает <see cref="T:System.Diagnostics.SymbolStore.ISymbolWriter" /> и фиксирует символы в хранилище символов.
    /// </summary>
    void Close();

    /// <summary>
    ///   Определяет атрибут для заданного имени и значения атрибута.
    /// </summary>
    /// <param name="parent">
    ///   Токен метаданных, для которого определяется атрибут.
    /// </param>
    /// <param name="name">Имя атрибута.</param>
    /// <param name="data">Значение атрибута.</param>
    void SetSymAttribute(SymbolToken parent, string name, byte[] data);

    /// <summary>Открывает новое пространство имен.</summary>
    /// <param name="name">Имя нового пространства имен.</param>
    void OpenNamespace(string name);

    /// <summary>Закрывает последнее пространство имен.</summary>
    void CloseNamespace();

    /// <summary>
    ///   Указывает, что в открытой лексической области видимости используется заданное полное имя пространства имен.
    /// </summary>
    /// <param name="fullName">Полное имя пространства имен.</param>
    void UsingNamespace(string fullName);

    /// <summary>
    ///   Указывает истинные начало и конец метода в исходном файле.
    ///    Используйте <see cref="M:System.Diagnostics.SymbolStore.ISymbolWriter.SetMethodSourceRange(System.Diagnostics.SymbolStore.ISymbolDocumentWriter,System.Int32,System.Int32,System.Diagnostics.SymbolStore.ISymbolDocumentWriter,System.Int32,System.Int32)" /> можно задать размер метод независимо от точки следования, которые существуют в методе.
    /// </summary>
    /// <param name="startDoc">
    ///   Документ, содержащий начальную позицию.
    /// </param>
    /// <param name="startLine">Номер начальной строки.</param>
    /// <param name="startColumn">Начальный столбец.</param>
    /// <param name="endDoc">
    ///   Документ, содержащий конечную позицию.
    /// </param>
    /// <param name="endLine">Конечный номер строки.</param>
    /// <param name="endColumn">Номер последнего столбца.</param>
    void SetMethodSourceRange(ISymbolDocumentWriter startDoc, int startLine, int startColumn, ISymbolDocumentWriter endDoc, int endLine, int endColumn);

    /// <summary>
    ///   Задает базовый <see langword="ISymUnmanagedWriter" /> (соответствующий неуправляемый интерфейс), управляемый <see cref="T:System.Diagnostics.SymbolStore.ISymbolWriter" /> для порождения символов.
    /// </summary>
    /// <param name="underlyingWriter">
    ///   Указатель на код, представляющий основное средство записи.
    /// </param>
    void SetUnderlyingWriter(IntPtr underlyingWriter);
  }
}

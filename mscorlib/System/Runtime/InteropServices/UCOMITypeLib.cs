// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMITypeLib
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.ITypeLib" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.ITypeLib instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("00020402-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface UCOMITypeLib
  {
    /// <summary>Возвращает число описаний типов в библиотеке типов.</summary>
    /// <returns>Число описаний типов в библиотеке типов.</returns>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetTypeInfoCount();

    /// <summary>Извлекает описание заданного типа в библиотеке.</summary>
    /// <param name="index">
    ///   Индекс <see langword="UCOMITypeInfo" /> интерфейс для возврата.
    /// </param>
    /// <param name="ppTI">
    ///   При удачном возвращении <see langword="UCOMITypeInfo" /> описания типа, на который указывает <paramref name="index" />.
    /// </param>
    void GetTypeInfo(int index, out UCOMITypeInfo ppTI);

    /// <summary>Возвращает тип описания типа.</summary>
    /// <param name="index">
    ///   Индекс описания типа в библиотеке типов.
    /// </param>
    /// <param name="pTKind">
    ///   Ссылка на <see langword="TYPEKIND" /> перечисление для описания типа.
    /// </param>
    void GetTypeInfoType(int index, out TYPEKIND pTKind);

    /// <summary>
    ///   Извлекает описание типа, соответствующее заданному идентификатору GUID.
    /// </summary>
    /// <param name="guid">
    ///   Идентификатор IID интерфейса CLSID класса, информация о типе которого запрашивается.
    /// </param>
    /// <param name="ppTInfo">
    ///   При удачном возвращении — запрошенный <see langword="ITypeInfo" /> интерфейса.
    /// </param>
    void GetTypeInfoOfGuid(ref Guid guid, out UCOMITypeInfo ppTInfo);

    /// <summary>
    ///   Возвращает структуру, содержащую атрибуты библиотеки.
    /// </summary>
    /// <param name="ppTLibAttr">
    ///   При удачном возвращении структуру, содержащую атрибуты библиотеки.
    /// </param>
    void GetLibAttr(out IntPtr ppTLibAttr);

    /// <summary>
    ///   Позволяет компилятору клиента выполнить привязку библиотеки типов, переменные, константы и глобальные функции.
    /// </summary>
    /// <param name="ppTComp">
    ///   При удачном возвращении экземпляр <see langword="UCOMITypeComp" /> экземпляра для данного <see langword="ITypeLib" />.
    /// </param>
    void GetTypeComp(out UCOMITypeComp ppTComp);

    /// <summary>
    ///   Извлекает строку документации библиотеки, полное имя файла справки и путь и идентификатор контекста для раздела справки библиотеки в файле справки.
    /// </summary>
    /// <param name="index">
    ///   Индекс описания типа, документация которого возвращается.
    /// </param>
    /// <param name="strName">
    ///   Возвращает строку, содержащую имя указанного элемента.
    /// </param>
    /// <param name="strDocString">
    ///   Возвращает строку, содержащую строку документации для заданного элемента.
    /// </param>
    /// <param name="dwHelpContext">
    ///   Возвращает идентификатор контекста справки, связанный с указанным элементом.
    /// </param>
    /// <param name="strHelpFile">
    ///   Возвращает строку, содержащую полное имя файла справки.
    /// </param>
    void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

    /// <summary>
    ///   Указывает, переданной строки содержит имя типа или члена, описанного в библиотеке.
    /// </summary>
    /// <param name="szNameBuf">Строка для проверки.</param>
    /// <param name="lHashVal">
    ///   Хэш-значение <paramref name="szNameBuf" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="szNameBuf" /> был найден в библиотеке типов; в противном случае <see langword="false" />.
    /// </returns>
    [return: MarshalAs(UnmanagedType.Bool)]
    bool IsName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal);

    /// <summary>
    ///   Обнаруживает экземпляры описания типа в библиотеке типов.
    /// </summary>
    /// <param name="szNameBuf">Имя для поиска.</param>
    /// <param name="lHashVal">
    ///   Вычисляемый хэш-значение для ускорения поиска по <see langword="LHashValOfNameSys" /> функции.
    ///    Если <paramref name="lHashVal" /> равно 0, то значение рассчитывается.
    /// </param>
    /// <param name="ppTInfo">
    ///   При удачном возвращении — массив указателей на описания типов, содержащих имя, указанное в <paramref name="szNameBuf" />.
    /// </param>
    /// <param name="rgMemId">
    ///   Массив <see langword="MEMBERID" /> в найденных элементов; <paramref name="rgMemId" /> [i] является <see langword="MEMBERID" /> выполняющим индексацию в описании типа, заданном параметром <paramref name="ppTInfo" /> [i].
    ///    Не может быть <see langword="null" />.
    /// </param>
    /// <param name="pcFound">
    ///   Указывает, сколько экземпляров следует искать при входе.
    ///    Например <paramref name="pcFound" /> = 1 может быть вызван для поиска первого экземпляра.
    ///    Поиск прекращается после обнаружения первого экземпляра.
    /// 
    ///   При выходе показывает число обнаруженных экземпляров.
    ///    Если <see langword="in" /> и <see langword="out" /> значения <paramref name="pcFound" /> совпадают, могут существовать другие описания типа, содержащие нужное имя.
    /// </param>
    void FindName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal, [MarshalAs(UnmanagedType.LPArray), Out] UCOMITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray), Out] int[] rgMemId, ref short pcFound);

    /// <summary>
    ///   Выпуски <see cref="T:System.Runtime.InteropServices.TYPELIBATTR" /> полученный из <see cref="M:System.Runtime.InteropServices.UCOMITypeLib.GetLibAttr(System.IntPtr@)" />.
    /// </summary>
    /// <param name="pTLibAttr">
    ///   <see langword="TLIBATTR" /> Выпуска.
    /// </param>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    void ReleaseTLibAttr(IntPtr pTLibAttr);
  }
}

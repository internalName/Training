// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.ITypeLib
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Предоставляет управляемое определение <see langword="ITypeLib" /> интерфейса.
  /// </summary>
  [Guid("00020402-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [__DynamicallyInvokable]
  [ComImport]
  public interface ITypeLib
  {
    /// <summary>Возвращает число описаний типов в библиотеке типов.</summary>
    /// <returns>Число описаний типов в библиотеке типов.</returns>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetTypeInfoCount();

    /// <summary>Извлекает описание заданного типа в библиотеке.</summary>
    /// <param name="index">
    ///   Индекс <see langword="ITypeInfo" /> интерфейс для возврата.
    /// </param>
    /// <param name="ppTI">
    ///   При возвращении данного метода содержит <see langword="ITypeInfo" /> описания типа, на который указывает <paramref name="index" />.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetTypeInfo(int index, out ITypeInfo ppTI);

    /// <summary>Возвращает тип описания типа.</summary>
    /// <param name="index">
    ///   Индекс описания типа в библиотеке типов.
    /// </param>
    /// <param name="pTKind">
    ///   При возвращении данного метода содержит ссылку на <see langword="TYPEKIND" /> перечисление для описания типа.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetTypeInfoType(int index, out TYPEKIND pTKind);

    /// <summary>
    ///   Извлекает описание типа, соответствующее заданному идентификатору GUID.
    /// </summary>
    /// <param name="guid">
    ///   Идентификатор IID интерфейса или CLSID класса, информация о типе которого запрашивается.
    /// </param>
    /// <param name="ppTInfo">
    ///   При возвращении данного метода содержит запрошенный <see langword="ITypeInfo" /> интерфейса.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetTypeInfoOfGuid(ref Guid guid, out ITypeInfo ppTInfo);

    /// <summary>
    ///   Возвращает структуру, содержащую атрибуты библиотеки.
    /// </summary>
    /// <param name="ppTLibAttr">
    ///   При возвращении данного метода содержит структуру, содержащую атрибуты библиотеки.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    void GetLibAttr(out IntPtr ppTLibAttr);

    /// <summary>
    ///   Позволяет компилятору клиента выполнить привязку библиотеки типов, переменные, константы и глобальные функции.
    /// </summary>
    /// <param name="ppTComp">
    ///   При возвращении данного метода содержит экземпляр <see langword="ITypeComp" /> экземпляра для данного <see langword="ITypeLib" />.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetTypeComp(out ITypeComp ppTComp);

    /// <summary>
    ///   Извлекает строку документации библиотеки, полное имя файла справки и путь и идентификатор контекста для раздела справки библиотеки в файле справки.
    /// </summary>
    /// <param name="index">
    ///   Индекс описания типа, документация которого возвращается.
    /// </param>
    /// <param name="strName">
    ///   При возвращении данного метода содержит строку, представляющую имя указанного элемента.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="strDocString">
    ///   При возвращении данного метода содержит строку, представляющую строку документации для заданного элемента.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="dwHelpContext">
    ///   При возвращении данного метода содержит идентификатор контекста справки, связанный с указанным элементом.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="strHelpFile">
    ///   При возвращении данного метода содержит строку, представляющую полное имя файла справки.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

    /// <summary>
    ///   Указывает, переданной строки содержит имя типа или члена, описанного в библиотеке.
    /// </summary>
    /// <param name="szNameBuf">
    ///   Строка для проверки.
    ///    Это параметр/out.
    /// </param>
    /// <param name="lHashVal">
    ///   Хэш-значение <paramref name="szNameBuf" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="szNameBuf" /> был найден в библиотеке типов; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    [return: MarshalAs(UnmanagedType.Bool)]
    bool IsName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal);

    /// <summary>
    ///   Обнаруживает экземпляры описания типа в библиотеке типов.
    /// </summary>
    /// <param name="szNameBuf">
    ///   Имя для поиска.
    ///    Это параметр/out.
    /// </param>
    /// <param name="lHashVal">
    ///   Вычисляемый хэш-значение для ускорения поиска по <see langword="LHashValOfNameSys" /> функции.
    ///    Если <paramref name="lHashVal" /> равно 0, то значение рассчитывается.
    /// </param>
    /// <param name="ppTInfo">
    ///   При возвращении данного метода содержит массив указателей на описания типов, содержащих имя, указанное в <paramref name="szNameBuf" />.
    ///    Этот параметр передается неинициализированным.
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
    [__DynamicallyInvokable]
    void FindName([MarshalAs(UnmanagedType.LPWStr)] string szNameBuf, int lHashVal, [MarshalAs(UnmanagedType.LPArray), Out] ITypeInfo[] ppTInfo, [MarshalAs(UnmanagedType.LPArray), Out] int[] rgMemId, ref short pcFound);

    /// <summary>
    ///   Выпуски <see cref="T:System.Runtime.InteropServices.TYPELIBATTR" /> структуры полученный из <see cref="M:System.Runtime.InteropServices.ComTypes.ITypeLib.GetLibAttr(System.IntPtr@)" /> метод.
    /// </summary>
    /// <param name="pTLibAttr">
    ///   <see langword="TLIBATTR" /> Структуру для выпуска.
    /// </param>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    void ReleaseTLibAttr(IntPtr pTLibAttr);
  }
}

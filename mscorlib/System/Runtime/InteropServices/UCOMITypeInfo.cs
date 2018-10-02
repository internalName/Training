// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMITypeInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.ITypeInfo" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.ITypeInfo instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("00020401-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface UCOMITypeInfo
  {
    /// <summary>
    ///   Извлекает <see cref="T:System.Runtime.InteropServices.TYPEATTR" /> структуру, содержащую атрибуты описания типа.
    /// </summary>
    /// <param name="ppTypeAttr">
    ///   При удачном возвращении ссылку на структуру, содержащую атрибуты этого описания типа.
    /// </param>
    void GetTypeAttr(out IntPtr ppTypeAttr);

    /// <summary>
    ///   Извлекает <see langword="ITypeComp" /> интерфейса для описания типа, позволяющий компилятору клиента выполнить привязку к элементам описания типа.
    /// </summary>
    /// <param name="ppTComp">
    ///   При удачном возвращении ссылку на <see langword="UCOMITypeComp" /> содержащей библиотеки типов.
    /// </param>
    void GetTypeComp(out UCOMITypeComp ppTComp);

    /// <summary>
    ///   Извлекает <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> структуру, содержащую сведения о заданной функции.
    /// </summary>
    /// <param name="index">Индекс возвращаемого описания функции.</param>
    /// <param name="ppFuncDesc">
    ///   Ссылка на <see langword="FUNCDESC" /> описывающий указанной функции.
    /// </param>
    void GetFuncDesc(int index, out IntPtr ppFuncDesc);

    /// <summary>
    ///   Извлекает <see langword="VARDESC" /> структуру, описывающую указанную переменную.
    /// </summary>
    /// <param name="index">
    ///   Индекс возвращаемого описания переменной.
    /// </param>
    /// <param name="ppVarDesc">
    ///   При удачном возвращении ссылку на <see langword="VARDESC" /> описывающий указанную переменную.
    /// </param>
    void GetVarDesc(int index, out IntPtr ppVarDesc);

    /// <summary>
    ///   Извлекает переменную с указанным Идентификатором (либо имя свойства или метода и его параметры), соответствующие указанным идентификатором функции.
    /// </summary>
    /// <param name="memid">
    ///   Идентификатор элемента, для которого имя (или имена) должны быть возвращены.
    /// </param>
    /// <param name="rgBstrNames">
    ///   При удачном возвращении содержит имя (или имена), связанное с элементом.
    /// </param>
    /// <param name="cMaxNames">
    ///   Длина <paramref name="rgBstrNames" /> массива.
    /// </param>
    /// <param name="pcNames">
    ///   При удачном возвращении — число имен в <paramref name="rgBstrNames" /> массива.
    /// </param>
    void GetNames(int memid, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2), Out] string[] rgBstrNames, int cMaxNames, out int pcNames);

    /// <summary>
    ///   Если в описании типа описывается класс COM, он извлекает описание типа для реализованных типов интерфейсов.
    /// </summary>
    /// <param name="index">
    ///   Индекс реализованного типа, дескриптор которого возвращается.
    /// </param>
    /// <param name="href">
    ///   Ссылка на дескриптор реализованного интерфейса.
    /// </param>
    void GetRefTypeOfImplType(int index, out int href);

    /// <summary>
    ///   Извлекает <see cref="T:System.Runtime.InteropServices.IMPLTYPEFLAGS" /> значение для одного реализованного интерфейса или базового интерфейса в описание типа.
    /// </summary>
    /// <param name="index">
    ///   Индекс реализованного интерфейса или базового интерфейса.
    /// </param>
    /// <param name="pImplTypeFlags">
    ///   При удачном возвращении ссылку на <see langword="IMPLTYPEFLAGS" /> перечисления.
    /// </param>
    void GetImplTypeFlags(int index, out int pImplTypeFlags);

    /// <summary>
    ///   Сопоставления между имена и идентификаторы элементов и параметров.
    /// </summary>
    /// <param name="rgszNames">
    ///   При удачном возвращении — массив имен для сопоставления.
    /// </param>
    /// <param name="cNames">Число имен для сопоставления.</param>
    /// <param name="pMemId">
    ///   Ссылка на массив, в имени которых расположены сопоставления.
    /// </param>
    void GetIDsOfNames([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1, ArraySubType = UnmanagedType.LPWStr), In] string[] rgszNames, int cNames, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1), Out] int[] pMemId);

    /// <summary>
    ///   Вызывает метод или обращается к свойству объекта, который реализует интерфейс, описанный в описании типа.
    /// </summary>
    /// <param name="pvInstance">
    ///   Ссылка на интерфейс, описанный в данном описании типа.
    /// </param>
    /// <param name="memid">Определяет члена интерфейса.</param>
    /// <param name="wFlags">Флаги, описывающие контекст вызова.</param>
    /// <param name="pDispParams">
    ///   Ссылка на структуру, содержащую массив аргументов, массив DISPID для именованных аргументов, а также счетчики количества элементов в каждом массиве.
    /// </param>
    /// <param name="pVarResult">
    ///   Ссылка на место хранения результата.
    ///    Если <paramref name="wFlags" /> указывает <see langword="DISPATCH_PROPERTYPUT" /> или <see langword="DISPATCH_PROPERTYPUTREF" />, <paramref name="pVarResult" /> игнорируется.
    ///    Значение <see langword="null" /> если результат не требуется.
    /// </param>
    /// <param name="pExcepInfo">
    ///   Указывает на структуру сведений об исключении, заполняемую только если <see langword="DISP_E_EXCEPTION" /> возвращается.
    /// </param>
    /// <param name="puArgErr">
    ///   Если <see langword="Invoke" /> возвращает <see langword="DISP_E_TYPEMISMATCH" />, <paramref name="puArgErr" /> указывает индекс в <paramref name="rgvarg" /> аргумента с неверным типом.
    ///    Если более одного аргумента возвращает сообщение об ошибке <paramref name="puArgErr" /> указывает только с первым аргументом с ошибкой.
    /// </param>
    void Invoke([MarshalAs(UnmanagedType.IUnknown)] object pvInstance, int memid, short wFlags, ref DISPPARAMS pDispParams, out object pVarResult, out EXCEPINFO pExcepInfo, out int puArgErr);

    /// <summary>
    ///   Извлекает строку документации, полное имя файла справки и путь и идентификатор контекста для раздела справки для заданного описания типа.
    /// </summary>
    /// <param name="index">
    ///   Идентификатор элемента, документация которого возвращается.
    /// </param>
    /// <param name="strName">
    ///   При удачном возвращении имя метода элемента.
    /// </param>
    /// <param name="strDocString">
    ///   При удачном возвращении строку документации для заданного элемента.
    /// </param>
    /// <param name="dwHelpContext">
    ///   При удачном возвращении ссылку на контекст справки, связанный с указанным элементом.
    /// </param>
    /// <param name="strHelpFile">
    ///   При удачном возвращении полное имя файла справки.
    /// </param>
    void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

    /// <summary>
    ///   Извлекает описание или спецификацию точки входа для функции в библиотеке DLL.
    /// </summary>
    /// <param name="memid">
    ///   Идентификатор функции-члена, описание входа DLL — должны быть возвращены.
    /// </param>
    /// <param name="invKind">
    ///   Задает тип члена, определенного <paramref name="memid" />.
    /// </param>
    /// <param name="pBstrDllName">
    ///   Если это не <see langword="null" />, функция задает <paramref name="pBstrDllName" /> для <see langword="BSTR" /> содержащий имя библиотеки DLL.
    /// </param>
    /// <param name="pBstrName">
    ///   Если это не <see langword="null" />, функция задает <paramref name="lpbstrName" /> для <see langword="BSTR" /> содержащее имя точки входа.
    /// </param>
    /// <param name="pwOrdinal">
    ///   Если это не <see langword="null" />, и функция определяется по порядковому номеру, затем <paramref name="lpwOrdinal" /> задается для указания на порядковый номер.
    /// </param>
    void GetDllEntry(int memid, INVOKEKIND invKind, out string pBstrDllName, out string pBstrName, out short pwOrdinal);

    /// <summary>
    ///   Если описание типа ссылается на другие описания типов, он извлекает описания ссылочных типов.
    /// </summary>
    /// <param name="hRef">
    ///   Дескриптор для описания ссылочного типа для возврата.
    /// </param>
    /// <param name="ppTI">
    ///   При удачном возвращении — Описание ссылочного типа.
    /// </param>
    void GetRefTypeInfo(int hRef, out UCOMITypeInfo ppTI);

    /// <summary>
    ///   Извлекает адреса статических функций и переменных, определенных в библиотеке DLL.
    /// </summary>
    /// <param name="memid">
    ///   Идентификатор элемента <see langword="static" /> адреса элемента для извлечения.
    /// </param>
    /// <param name="invKind">
    ///   Указывает, является ли элемент свойством и если да, какой тип.
    /// </param>
    /// <param name="ppv">
    ///   При удачном возвращении ссылку на <see langword="static" /> член.
    /// </param>
    void AddressOfMember(int memid, INVOKEKIND invKind, out IntPtr ppv);

    /// <summary>
    ///   Создает новый экземпляр типа, который описывает класс компонента (совместный класс).
    /// </summary>
    /// <param name="pUnkOuter">
    ///   Объект, действующий как проверяющий <see langword="IUnknown" />.
    /// </param>
    /// <param name="riid">
    ///   Идентификатор IID интерфейса, который вызывающая сторона использует для связи с итоговым объектом.
    /// </param>
    /// <param name="ppvObj">
    ///   При удачном возвращении ссылку на созданный объект.
    /// </param>
    void CreateInstance([MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

    /// <summary>Возвращает сведения о маршалинге.</summary>
    /// <param name="memid">
    ///   Необходим идентификатор элемента, который указывает, какие сведения о маршалинге.
    /// </param>
    /// <param name="pBstrMops">
    ///   Ссылка на строку кода операции используется при маршалинге полей структуры, описанной в описании ссылочного типа, либо возвращает <see langword="null" /> Если сведения отсутствуют.
    /// </param>
    void GetMops(int memid, out string pBstrMops);

    /// <summary>
    ///   Извлекает библиотеку типов, содержащую описание этого типа и его индекс внутри библиотеки типов.
    /// </summary>
    /// <param name="ppTLB">
    ///   При удачном возвращении — ссылка на содержащую библиотеки типов.
    /// </param>
    /// <param name="pIndex">
    ///   При удачном возвращении ссылку на индекс описания типа внутри содержащей библиотеки типов.
    /// </param>
    void GetContainingTypeLib(out UCOMITypeLib ppTLB, out int pIndex);

    /// <summary>
    ///   Выпуски <see cref="T:System.Runtime.InteropServices.TYPEATTR" /> ранее возвращенных <see cref="M:System.Runtime.InteropServices.UCOMITypeInfo.GetTypeAttr(System.IntPtr@)" />.
    /// </summary>
    /// <param name="pTypeAttr">
    ///   Ссылка на <see langword="TYPEATTR" /> выпуска.
    /// </param>
    void ReleaseTypeAttr(IntPtr pTypeAttr);

    /// <summary>
    ///   Выпуски <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> ранее возвращенных <see cref="M:System.Runtime.InteropServices.UCOMITypeInfo.GetFuncDesc(System.Int32,System.IntPtr@)" />.
    /// </summary>
    /// <param name="pFuncDesc">
    ///   Ссылка на <see langword="FUNCDESC" /> выпуска.
    /// </param>
    void ReleaseFuncDesc(IntPtr pFuncDesc);

    /// <summary>
    ///   Выпуски <see langword="VARDESC" /> ранее возвращенных <see cref="M:System.Runtime.InteropServices.UCOMITypeInfo.GetVarDesc(System.Int32,System.IntPtr@)" />.
    /// </summary>
    /// <param name="pVarDesc">
    ///   Ссылка на <see langword="VARDESC" /> выпуска.
    /// </param>
    void ReleaseVarDesc(IntPtr pVarDesc);
  }
}

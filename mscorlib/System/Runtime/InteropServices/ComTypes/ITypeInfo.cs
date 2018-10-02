// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.ITypeInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Предоставляет управляемое определение компонента автоматизации ITypeInfo интерфейса.
  /// </summary>
  [Guid("00020401-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [__DynamicallyInvokable]
  [ComImport]
  public interface ITypeInfo
  {
    /// <summary>
    ///   Извлекает <see cref="T:System.Runtime.InteropServices.TYPEATTR" /> структуру, содержащую атрибуты описания типа.
    /// </summary>
    /// <param name="ppTypeAttr">
    ///   При возвращении данного метода содержит ссылку на структуру, содержащую атрибуты этого описания типа.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    void GetTypeAttr(out IntPtr ppTypeAttr);

    /// <summary>
    ///   Извлекает <see langword="ITypeComp" /> интерфейса для описания типа, позволяющий компилятору клиента выполнить привязку к элементам описания типа.
    /// </summary>
    /// <param name="ppTComp">
    ///   При возвращении данного метода содержит ссылку на <see langword="ITypeComp" /> интерфейса, содержащего библиотеку типов.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetTypeComp(out ITypeComp ppTComp);

    /// <summary>
    ///   Извлекает <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> структуру, содержащую сведения о заданной функции.
    /// </summary>
    /// <param name="index">Индекс возвращаемого описания функции.</param>
    /// <param name="ppFuncDesc">
    ///   При возвращении данного метода содержит ссылку на <see langword="FUNCDESC" /> Структура, описывающая указанной функции.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    void GetFuncDesc(int index, out IntPtr ppFuncDesc);

    /// <summary>
    ///   Извлекает <see langword="VARDESC" /> структуру, описывающую указанную переменную.
    /// </summary>
    /// <param name="index">
    ///   Индекс возвращаемого описания переменной.
    /// </param>
    /// <param name="ppVarDesc">
    ///   При возвращении данного метода содержит ссылку на <see langword="VARDESC" /> структуру, описывающую указанную переменную.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    void GetVarDesc(int index, out IntPtr ppVarDesc);

    /// <summary>
    ///   Извлекает переменную с указанным Идентификатором (либо имя свойства или метода и его параметры), соответствующий заданному идентификатору функции.
    /// </summary>
    /// <param name="memid">
    ///   Идентификатор элемента, для которого имя (или имена) должны быть возвращены.
    /// </param>
    /// <param name="rgBstrNames">
    ///   При возвращении данного метода содержит имя (или имена), связанное с элементом.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="cMaxNames">
    ///   Длина <paramref name="rgBstrNames" /> массива.
    /// </param>
    /// <param name="pcNames">
    ///   При возвращении этого метода содержит число имен в <paramref name="rgBstrNames" /> массива.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetNames(int memid, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2), Out] string[] rgBstrNames, int cMaxNames, out int pcNames);

    /// <summary>
    ///   Извлекает описание типа для реализованных типов интерфейсов, если в описании типа описывается класс COM.
    /// </summary>
    /// <param name="index">
    ///   Индекс реализованного типа, дескриптор которого возвращается.
    /// </param>
    /// <param name="href">
    ///   При возвращении данного метода содержит ссылку на дескриптор для реализованного интерфейса.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetRefTypeOfImplType(int index, out int href);

    /// <summary>
    ///   Извлекает <see cref="T:System.Runtime.InteropServices.IMPLTYPEFLAGS" /> значение для одного реализованного интерфейса или базового интерфейса в описание типа.
    /// </summary>
    /// <param name="index">
    ///   Индекс реализованного интерфейса или базового интерфейса.
    /// </param>
    /// <param name="pImplTypeFlags">
    ///   При возвращении данного метода содержит ссылку на <see langword="IMPLTYPEFLAGS" /> перечисления.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetImplTypeFlags(int index, out IMPLTYPEFLAGS pImplTypeFlags);

    /// <summary>
    ///   Сопоставления между имена и идентификаторы элементов и параметров.
    /// </summary>
    /// <param name="rgszNames">Массив имен для сопоставления.</param>
    /// <param name="cNames">Количество сопоставляемых имен.</param>
    /// <param name="pMemId">
    ///   При возвращении данного метода содержит ссылку на массив, в который помещены сопоставления имен.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetIDsOfNames([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1, ArraySubType = UnmanagedType.LPWStr), In] string[] rgszNames, int cNames, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1), Out] int[] pMemId);

    /// <summary>
    ///   Вызывает метод или обращается к свойству объекта, который реализует интерфейс, описанный в описании типа.
    /// </summary>
    /// <param name="pvInstance">
    ///   Ссылка на интерфейс, описанный в данном описании типа.
    /// </param>
    /// <param name="memid">
    ///   Значение, определяющее члена интерфейса.
    /// </param>
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
    ///   Указатель на структуру сведений об исключении, заполняемую только если <see langword="DISP_E_EXCEPTION" /> возвращается.
    /// </param>
    /// <param name="puArgErr">
    ///   Если <see langword="Invoke" /> возвращает <see langword="DISP_E_TYPEMISMATCH" />, <paramref name="puArgErr" /> указывает индекс в <paramref name="rgvarg" /> аргумента с неверным типом.
    ///    Если более одного аргумента возвращает сообщение об ошибке <paramref name="puArgErr" /> указывает только с первым аргументом с ошибкой.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    void Invoke([MarshalAs(UnmanagedType.IUnknown)] object pvInstance, int memid, short wFlags, ref DISPPARAMS pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, out int puArgErr);

    /// <summary>
    ///   Извлекает строку документации, полное имя файла справки и путь и идентификатор контекста для раздела справки для заданного описания типа.
    /// </summary>
    /// <param name="index">
    ///   Идентификатор элемента, документация которого возвращается.
    /// </param>
    /// <param name="strName">
    ///   При возвращении данного метода содержит имя метода элемента.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="strDocString">
    ///   При возвращении данного метода содержит строку документации для заданного элемента.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="dwHelpContext">
    ///   При возвращении данного метода содержит ссылку на контекст справки, связанный с указанным элементом.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="strHelpFile">
    ///   При возвращении данного метода содержит полное имя файла справки.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetDocumentation(int index, out string strName, out string strDocString, out int dwHelpContext, out string strHelpFile);

    /// <summary>
    ///   Извлекает описание или спецификацию точки входа для функции в библиотеке DLL.
    /// </summary>
    /// <param name="memid">
    ///   Идентификатор функции-члена, описание входа DLL — должны быть возвращены.
    /// </param>
    /// <param name="invKind">
    ///   Один из <see cref="T:System.Runtime.InteropServices.ComTypes.INVOKEKIND" /> значений, определяющее тип члена, определенного <paramref name="memid" />.
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
    void GetDllEntry(int memid, INVOKEKIND invKind, IntPtr pBstrDllName, IntPtr pBstrName, IntPtr pwOrdinal);

    /// <summary>
    ///   Извлекает описания ссылочных типов, если описание типа ссылается на другие описания типов.
    /// </summary>
    /// <param name="hRef">
    ///   Дескриптор для описания ссылочного типа для возврата.
    /// </param>
    /// <param name="ppTI">
    ///   При возвращении данного метода содержит описание ссылочного типа.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetRefTypeInfo(int hRef, out ITypeInfo ppTI);

    /// <summary>
    ///   Извлекает адреса статических функций и переменных, определенных в библиотеке DLL.
    /// </summary>
    /// <param name="memid">
    ///   Идентификатор члена <see langword="static" /> адреса элемента для извлечения.
    /// </param>
    /// <param name="invKind">
    ///   Один из <see cref="T:System.Runtime.InteropServices.ComTypes.INVOKEKIND" />  значений, указывающее, является ли элемент свойством и если да, какой тип.
    /// </param>
    /// <param name="ppv">
    ///   При возвращении данного метода содержит ссылку на <see langword="static" /> член.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    void AddressOfMember(int memid, INVOKEKIND invKind, out IntPtr ppv);

    /// <summary>
    ///   Создает новый экземпляр типа, который описывает класс компонента (совместный класс).
    /// </summary>
    /// <param name="pUnkOuter">
    ///   Объект, действующий как проверяющий <see langword="IUnknown" />.
    /// </param>
    /// <param name="riid">
    ///   Идентификатор IID интерфейса, используемый вызывающим объектом для связи с итоговым объектом.
    /// </param>
    /// <param name="ppvObj">
    ///   При возвращении данного метода содержит ссылку на созданный объект.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void CreateInstance([MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, [In] ref Guid riid, [MarshalAs(UnmanagedType.IUnknown)] out object ppvObj);

    /// <summary>Возвращает сведения о маршалинге.</summary>
    /// <param name="memid">
    ///   Необходим идентификатор элемента, который указывает, какие сведения о маршалинге.
    /// </param>
    /// <param name="pBstrMops">
    ///   При возвращении данного метода содержит ссылку на <see langword="opcode" /> строку, используемую при маршалинге полей структуры, описанной в описании ссылочного типа, либо возвращает <see langword="null" /> Если сведения отсутствуют.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetMops(int memid, out string pBstrMops);

    /// <summary>
    ///   Извлекает библиотеку типов, содержащую описание этого типа и его индекс внутри библиотеки типов.
    /// </summary>
    /// <param name="ppTLB">
    ///   При возвращении данного метода содержит ссылку на содержащей библиотеки типов.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="pIndex">
    ///   При возвращении данного метода содержит ссылку на индекс описания типа внутри содержащей библиотеки типов.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetContainingTypeLib(out ITypeLib ppTLB, out int pIndex);

    /// <summary>
    ///   Выпуски <see cref="T:System.Runtime.InteropServices.TYPEATTR" /> структуры, возвращенный ранее методом <see cref="M:System.Runtime.InteropServices.ComTypes.ITypeInfo.GetTypeAttr(System.IntPtr@)" /> метод.
    /// </summary>
    /// <param name="pTypeAttr">
    ///   Ссылку на <see langword="TYPEATTR" /> структуру для выпуска.
    /// </param>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    void ReleaseTypeAttr(IntPtr pTypeAttr);

    /// <summary>
    ///   Выпуски <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> структуры, возвращенный ранее методом <see cref="M:System.Runtime.InteropServices.ComTypes.ITypeInfo.GetFuncDesc(System.Int32,System.IntPtr@)" /> метод.
    /// </summary>
    /// <param name="pFuncDesc">
    ///   Ссылку на <see langword="FUNCDESC" /> структуру для выпуска.
    /// </param>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    void ReleaseFuncDesc(IntPtr pFuncDesc);

    /// <summary>
    ///   Выпуски <see langword="VARDESC" /> структуры, возвращенный ранее методом <see cref="M:System.Runtime.InteropServices.ComTypes.ITypeInfo.GetVarDesc(System.Int32,System.IntPtr@)" /> метод.
    /// </summary>
    /// <param name="pVarDesc">
    ///   Ссылку на <see langword="VARDESC" /> структуру для выпуска.
    /// </param>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    void ReleaseVarDesc(IntPtr pVarDesc);
  }
}

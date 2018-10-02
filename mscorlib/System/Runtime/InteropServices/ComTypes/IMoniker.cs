// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.IMoniker
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Предоставляет управляемое определение <see langword="IMoniker" /> интерфейс с функциональными возможностями COM <see langword="IPersist" /> и <see langword="IPersistStream" />.
  /// </summary>
  [Guid("0000000f-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [__DynamicallyInvokable]
  [ComImport]
  public interface IMoniker
  {
    /// <summary>Извлекает идентификатор класса (CLSID) объекта.</summary>
    /// <param name="pClassID">
    ///   При возвращении этого метода содержит CLSID.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetClassID(out Guid pClassID);

    /// <summary>
    ///   Проверяет наличие изменений в объекте с момента последнего сохранения.
    /// </summary>
    /// <returns>
    ///   <see langword="S_OK" /><see langword="HRESULT" /> Значение, если объект был изменен; в противном случае — <see langword="S_FALSE" /><see langword="HRESULT" /> значение.
    /// </returns>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsDirty();

    /// <summary>
    ///   Инициализирует объект из потока, в котором он был ранее сохранен.
    /// </summary>
    /// <param name="pStm">Поток, из которого загружается объект.</param>
    [__DynamicallyInvokable]
    void Load(IStream pStm);

    /// <summary>Сохраняет объект в указанном потоке.</summary>
    /// <param name="pStm">
    ///   Поток, в котором требуется сохранить объект.
    /// </param>
    /// <param name="fClearDirty">
    ///   <see langword="true" /> очистка измененного флага после сохранения; в противном случае <see langword="false" />
    /// </param>
    [__DynamicallyInvokable]
    void Save(IStream pStm, [MarshalAs(UnmanagedType.Bool)] bool fClearDirty);

    /// <summary>
    ///   Возвращает размер в байтах потока, необходимого для сохранения объекта.
    /// </summary>
    /// <param name="pcbSize">
    ///   При возвращении данного метода содержит <see langword="long" /> значение, указывающее размер в байтах потока, необходимого для сохранения этого объекта.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetSizeMax(out long pcbSize);

    /// <summary>
    ///   Использует моникер для привязки к определяемому объекту.
    /// </summary>
    /// <param name="pbc">
    ///   Ссылку на <see langword="IBindCtx" /> интерфейс объекта контекста привязки, используемый в данной операции привязки.
    /// </param>
    /// <param name="pmkToLeft">
    ///   Ссылка на моникер слева от данного моникера, если моникер является частью составного моникера.
    /// </param>
    /// <param name="riidResult">
    ///   Идентификатор интерфейса (IID) интерфейса, который клиент планирует использовать для взаимодействия с объектом, который определен моникером.
    /// </param>
    /// <param name="ppvResult">
    ///   При возвращении данного метода содержит ссылку на интерфейс, запрошенный <paramref name="riidResult" />.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void BindToObject(IBindCtx pbc, IMoniker pmkToLeft, [In] ref Guid riidResult, [MarshalAs(UnmanagedType.Interface)] out object ppvResult);

    /// <summary>
    ///   Получает указатель интерфейса на хранилище, содержащего объект, определенный моникером.
    /// </summary>
    /// <param name="pbc">
    ///   Ссылку на <see langword="IBindCtx" /> интерфейса объекта контекстной привязки, используемые во время данной операции привязки.
    /// </param>
    /// <param name="pmkToLeft">
    ///   Ссылка на моникер слева от данного моникера, если моникер является частью составного моникера.
    /// </param>
    /// <param name="riid">
    ///   Идентификатор интерфейса (IID) запрошенного интерфейса хранилища.
    /// </param>
    /// <param name="ppvObj">
    ///   При возвращении данного метода содержит ссылку на интерфейс, запрошенный <paramref name="riid" />.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void BindToStorage(IBindCtx pbc, IMoniker pmkToLeft, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppvObj);

    /// <summary>
    ///   Возвращает уменьшенный моникер, представляющий собой другой моникер, который ссылается на один и тот же объект как текущий моникер, но может быть привязано с аналогичной или большей эффективностью.
    /// </summary>
    /// <param name="pbc">
    ///   Ссылку на <see langword="IBindCtx" /> интерфейс контекста привязки, используемый в данной операции привязки.
    /// </param>
    /// <param name="dwReduceHowFar">
    ///   Значение, указывающее способ уменьшения текущий моникер.
    /// </param>
    /// <param name="ppmkToLeft">
    ///   Ссылка на моникер слева от текущего моникера.
    /// </param>
    /// <param name="ppmkReduced">
    ///   При возвращении данного метода содержит ссылку на уменьшенную форму текущего моникера, который может быть <see langword="null" /> при возникновении ошибки или если текущий моникер уменьшен до нуля.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void Reduce(IBindCtx pbc, int dwReduceHowFar, ref IMoniker ppmkToLeft, out IMoniker ppmkReduced);

    /// <summary>
    ///   Объединяет текущий моникер с другим, создавая составной моникер.
    /// </summary>
    /// <param name="pmkRight">
    ///   Ссылку на <see langword="IMoniker" /> интерфейс моникера для добавления в конец данного моникера.
    /// </param>
    /// <param name="fOnlyIfNotGeneric">
    ///   <see langword="true" /> Чтобы указать, что вызывающему объекту требуется нестандартное объединение.
    ///    Операция выполняется только в том случае, если <paramref name="pmkRight" /> является классом моникера, текущий моникер можно объединить с некоторым способом, кроме универсального объединения.
    ///   <see langword="false" /> Чтобы указать, что при необходимости метод может создать универсальное объединение.
    /// </param>
    /// <param name="ppmkComposite">
    ///   При возвращении данного метода содержит ссылку на итоговый составной моникер.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void ComposeWith(IMoniker pmkRight, [MarshalAs(UnmanagedType.Bool)] bool fOnlyIfNotGeneric, out IMoniker ppmkComposite);

    /// <summary>
    ///   Предоставляет указатель на перечислитель, способный перечислить компоненты составного моникера.
    /// </summary>
    /// <param name="fForward">
    ///   <see langword="true" /> для перечисления моникеров слева направо.
    ///   <see langword="false" /> для перечисления справа налево.
    /// </param>
    /// <param name="ppenumMoniker">
    ///   При возвращении данного метода содержит ссылку на объект перечислителя для моникера.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void Enum([MarshalAs(UnmanagedType.Bool)] bool fForward, out IEnumMoniker ppenumMoniker);

    /// <summary>
    ///   Сравнивает текущий моникер с заданным моникером и показывает, совпадают ли они.
    /// </summary>
    /// <param name="pmkOtherMoniker">
    ///   Ссылка на моникер, используемый для сравнения.
    /// </param>
    /// <returns>
    ///   <see langword="S_OK" /><see langword="HRESULT" /> Значение, если моникеры совпадают; в противном случае — <see langword="S_FALSE" /><see langword="HRESULT" /> значение.
    /// </returns>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsEqual(IMoniker pmkOtherMoniker);

    /// <summary>
    ///   Вычисляет 32-разрядное целое число, используя внутреннее состояние моникера.
    /// </summary>
    /// <param name="pdwHash">
    ///   При возвращении данного метода содержит хэш-значение моникера.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void Hash(out int pdwHash);

    /// <summary>
    ///   Определяет, является ли объект, который идентифицируется текущим моникером в данный момент загружен и запущен.
    /// </summary>
    /// <param name="pbc">
    ///   Ссылка на контекст привязки, используемый в данной операции привязки.
    /// </param>
    /// <param name="pmkToLeft">
    ///   Ссылка на моникер слева от данного моникера, если текущий моникер является частью составного.
    /// </param>
    /// <param name="pmkNewlyRunning">
    ///   Ссылка на моникер, недавно добавленных в таблицу текущих объектов (ROT).
    /// </param>
    /// <returns>
    ///   <see langword="S_OK" /><see langword="HRESULT" /> Значение, если выполняется моникер; <see langword="S_FALSE" /><see langword="HRESULT" /> значение моникер не работает, или <see langword="E_UNEXPECTED" /><see langword="HRESULT" /> значение.
    /// </returns>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsRunning(IBindCtx pbc, IMoniker pmkToLeft, IMoniker pmkNewlyRunning);

    /// <summary>
    ///   Предоставляет число, указывающее время последнего изменения объекта, определенного текущим моникером.
    /// </summary>
    /// <param name="pbc">
    ///   Ссылка на контекст привязки, используемый в данной операции привязки.
    /// </param>
    /// <param name="pmkToLeft">
    ///   Ссылка на моникер слева от данного моникера, если моникер является частью составного моникера.
    /// </param>
    /// <param name="pFileTime">
    ///   При возвращении данного метода содержит время последнего изменения.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetTimeOfLastChange(IBindCtx pbc, IMoniker pmkToLeft, out FILETIME pFileTime);

    /// <summary>
    ///   Предоставляет специальное имя, в правой части текущий моникер, либо подобной структурой приводит к нулевому.
    /// </summary>
    /// <param name="ppmk">
    ///   При возвращении данного метода содержит моникер, который является инверсией текущего моникера.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void Inverse(out IMoniker ppmk);

    /// <summary>
    ///   Создает новый моникер на основании общего префикса, который используется совместно с другим.
    /// </summary>
    /// <param name="pmkOther">
    ///   Ссылку на <see langword="IMoniker" /> интерфейс другого моникера для сравнения с текущий моникер Общий префикс.
    /// </param>
    /// <param name="ppmkPrefix">
    ///   При возвращении данного метода содержит моникер, который является общим префиксом текущего моникера и <paramref name="pmkOther" />.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void CommonPrefixWith(IMoniker pmkOther, out IMoniker ppmkPrefix);

    /// <summary>
    ///   Предоставляет моникер, добавление которого к текущему моникеру (или к одной из подобных структур) приводит к получению указанного моникера.
    /// </summary>
    /// <param name="pmkOther">
    ///   Ссылка на специальное имя, к которому следует предпринять относительный путь.
    /// </param>
    /// <param name="ppmkRelPath">
    ///   При возвращении данного метода содержит ссылку на относительный моникер.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void RelativePathTo(IMoniker pmkOther, out IMoniker ppmkRelPath);

    /// <summary>
    ///   Возвращает отображаемое имя, которое представляет собой текущий моникер доступное для чтения пользователю.
    /// </summary>
    /// <param name="pbc">
    ///   Ссылка на контекст привязки, используемый в данной операции.
    /// </param>
    /// <param name="pmkToLeft">
    ///   Ссылка на моникер слева от данного моникера, если моникер является частью составного моникера.
    /// </param>
    /// <param name="ppszDisplayName">
    ///   При возвращении данного метода содержит строку отображаемого имени.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetDisplayName(IBindCtx pbc, IMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] out string ppszDisplayName);

    /// <summary>
    ///   Считывает столько знаков указанного отображаемого имени <see cref="M:System.Runtime.InteropServices.ComTypes.IMoniker.ParseDisplayName(System.Runtime.InteropServices.ComTypes.IBindCtx,System.Runtime.InteropServices.ComTypes.IMoniker,System.String,System.Int32@,System.Runtime.InteropServices.ComTypes.IMoniker@)" /> понимает и создает моникер, соответствующий считанной части.
    /// </summary>
    /// <param name="pbc">
    ///   Ссылка на контекст привязки, используемый в данной операции привязки.
    /// </param>
    /// <param name="pmkToLeft">
    ///   Ссылка на моникер, созданный из отображаемого имени до этой точки.
    /// </param>
    /// <param name="pszDisplayName">
    ///   Ссылка на строку, содержащую оставшуюся часть анализируемого отображаемого имени.
    /// </param>
    /// <param name="pchEaten">
    ///   При возвращении данного метода содержит число символов, использованных при анализе <paramref name="pszDisplayName" />.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="ppmkOut">
    ///   При возвращении данного метода содержит ссылку на моникер, построенный из <paramref name="pszDisplayName" />.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void ParseDisplayName(IBindCtx pbc, IMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] string pszDisplayName, out int pchEaten, out IMoniker ppmkOut);

    /// <summary>
    ///   Указывает, является ли данное специальное имя одним из классов моникеров, предоставляемых системой.
    /// </summary>
    /// <param name="pdwMksys">
    ///   При возвращении данного метода содержит указатель на целое число, которое является одним из значений из <see langword="MKSYS" /> перечисления и ссылается на один из классов COM моникера.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   <see langword="S_OK" /><see langword="HRESULT" /> Значение, если моникер является моникером системы; в противном случае — <see langword="S_FALSE" /><see langword="HRESULT" /> значение.
    /// </returns>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsSystemMoniker(out int pdwMksys);
  }
}

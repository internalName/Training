// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMIMoniker
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.IMoniker" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IMoniker instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("0000000f-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface UCOMIMoniker
  {
    /// <summary>Извлекает идентификатор класса (CLSID) объекта.</summary>
    /// <param name="pClassID">
    ///   При удачном возвращении содержит CLSID.
    /// </param>
    void GetClassID(out Guid pClassID);

    /// <summary>
    ///   Проверяет наличие изменений в объекте с момента последнего сохранения.
    /// </summary>
    /// <returns>
    ///   <see langword="S_OK" /><see langword="HRESULT" /> Значение, если объект был изменен; в противном случае — <see langword="S_FALSE" /><see langword="HRESULT" /> значение.
    /// </returns>
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsDirty();

    /// <summary>
    ///   Инициализирует объект из потока, в котором он был ранее сохранен.
    /// </summary>
    /// <param name="pStm">Поток, из которого загружается объект.</param>
    void Load(UCOMIStream pStm);

    /// <summary>Сохраняет объект в указанном потоке.</summary>
    /// <param name="pStm">Поток, в котором будет сохранен объект.</param>
    /// <param name="fClearDirty">
    ///   Указывает, следует ли очистка измененного флага после сохранения.
    /// </param>
    void Save(UCOMIStream pStm, [MarshalAs(UnmanagedType.Bool)] bool fClearDirty);

    /// <summary>
    ///   Возвращает размер в байтах потока, необходимого для сохранения объекта.
    /// </summary>
    /// <param name="pcbSize">
    ///   При удачном возвращении содержит <see langword="long" /> значение, указывающее размер в байтах потока, необходимого для сохранения этого объекта.
    /// </param>
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
    ///   Идентификатор интерфейса (IID) интерфейса, клиент пытается использовать для взаимодействия с объектом, который определен моникером.
    /// </param>
    /// <param name="ppvResult">
    ///   При удачном возвращении — ссылка на интерфейс, запрошенный <paramref name="riidResult" />.
    /// </param>
    void BindToObject(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, [In] ref Guid riidResult, [MarshalAs(UnmanagedType.Interface)] out object ppvResult);

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
    ///   При удачном возвращении — ссылка на интерфейс запрошено <paramref name="riid" />.
    /// </param>
    void BindToStorage(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppvObj);

    /// <summary>
    ///   Возвращает уменьшенный моникер, представляющий собой другой моникер, который ссылается на тот же объект, такой, но может быть привязан с аналогичной или большей эффективности.
    /// </summary>
    /// <param name="pbc">
    ///   Ссылку на <see langword="IBindCtx" /> интерфейс контекста привязки, используемый в данной операции привязки.
    /// </param>
    /// <param name="dwReduceHowFar">
    ///   Задает способ уменьшения моникера.
    /// </param>
    /// <param name="ppmkToLeft">
    ///   Ссылка на моникер слева от данного моникера.
    /// </param>
    /// <param name="ppmkReduced">
    ///   При удачном возвращении ссылку на сокращенной формы этот моникер, который может быть <see langword="null" /> при возникновении ошибки или если этот моникер уменьшен до нуля.
    /// </param>
    void Reduce(UCOMIBindCtx pbc, int dwReduceHowFar, ref UCOMIMoniker ppmkToLeft, out UCOMIMoniker ppmkReduced);

    /// <summary>
    ///   Объединяет текущий моникер с другим, создавая составной моникер.
    /// </summary>
    /// <param name="pmkRight">
    ///   Ссылку на <see langword="IMoniker" /> интерфейс моникера, для добавления в конец данного моникера.
    /// </param>
    /// <param name="fOnlyIfNotGeneric">
    ///   Если <see langword="true" />, вызывающему объекту требуется нестандартное объединение, поэтому операция выполняется только в том случае, если <paramref name="pmkRight" /> является классом моникера, этот моникер может быть объединено любым способом, кроме универсального объединения.
    ///    Если <see langword="false" />, при необходимости метод может создать универсальное объединение.
    /// </param>
    /// <param name="ppmkComposite">
    ///   При удачном возвращении ссылку на итоговый составной моникер.
    /// </param>
    void ComposeWith(UCOMIMoniker pmkRight, [MarshalAs(UnmanagedType.Bool)] bool fOnlyIfNotGeneric, out UCOMIMoniker ppmkComposite);

    /// <summary>
    ///   Предоставляет указатель на перечислитель, способный перечислить компоненты составного моникера.
    /// </summary>
    /// <param name="fForward">
    ///   Если <see langword="true" />, перечисляет моникеров слева направо.
    ///    Если <see langword="false" />, перечисляет справа налево.
    /// </param>
    /// <param name="ppenumMoniker">
    ///   При удачном возвращении ссылается на объект перечислителя для моникера.
    /// </param>
    void Enum([MarshalAs(UnmanagedType.Bool)] bool fForward, out UCOMIEnumMoniker ppenumMoniker);

    /// <summary>
    ///   Сравнивает этот моникер с заданным моникером и показывает, совпадают ли они.
    /// </summary>
    /// <param name="pmkOtherMoniker">
    ///   Ссылка на моникер, используемый для сравнения.
    /// </param>
    void IsEqual(UCOMIMoniker pmkOtherMoniker);

    /// <summary>
    ///   Вычисляет 32-разрядное целое число, используя внутреннее состояние моникера.
    /// </summary>
    /// <param name="pdwHash">
    ///   При удачном возвращении содержит хэш-значение моникера.
    /// </param>
    void Hash(out int pdwHash);

    /// <summary>
    ///   Определяет, загружен и запущен ли в данный момент объект, определенный этим моникером.
    /// </summary>
    /// <param name="pbc">
    ///   Ссылка на контекст привязки, используемый в данной операции привязки.
    /// </param>
    /// <param name="pmkToLeft">
    ///   Ссылка на моникер слева от данного моникера, если данный моникер является частью составного.
    /// </param>
    /// <param name="pmkNewlyRunning">
    ///   Ссылка на моникер, недавно добавленных таблицы запущенных объектов.
    /// </param>
    void IsRunning(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, UCOMIMoniker pmkNewlyRunning);

    /// <summary>
    ///   Предоставляет число, указывающее время последнего изменения объекта, определенного этим моникером.
    /// </summary>
    /// <param name="pbc">
    ///   Ссылка на контекст привязки, используемый в данной операции привязки.
    /// </param>
    /// <param name="pmkToLeft">
    ///   Ссылка на моникер слева от данного моникера, если моникер является частью составного моникера.
    /// </param>
    /// <param name="pFileTime">
    ///   При удачном возвращении содержит время последнего изменения.
    /// </param>
    void GetTimeOfLastChange(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, out FILETIME pFileTime);

    /// <summary>
    ///   Предоставляет специальное имя, справа от данного моникера или подобной структурой приводит к нулевому.
    /// </summary>
    /// <param name="ppmk">
    ///   При удачном возвращении содержит моникер, который является обратным по отношению моникера.
    /// </param>
    void Inverse(out UCOMIMoniker ppmk);

    /// <summary>
    ///   Создает новый моникер на основании общего префикса, который используется совместно с другим.
    /// </summary>
    /// <param name="pmkOther">
    ///   Ссылку на <see langword="IMoniker" /> интерфейс другого моникера, сравниваемый с данным Общий префикс.
    /// </param>
    /// <param name="ppmkPrefix">
    ///   При удачном возвращении содержит моникер, который является общим префиксом данного специального имени и <paramref name="pmkOther" />.
    /// </param>
    void CommonPrefixWith(UCOMIMoniker pmkOther, out UCOMIMoniker ppmkPrefix);

    /// <summary>
    ///   Предоставляет моникер, добавление которого к данному моникеру (или к одной из подобных структур) приводит к получению указанного моникера.
    /// </summary>
    /// <param name="pmkOther">
    ///   Ссылка на специальное имя, к которому следует предпринять относительный путь.
    /// </param>
    /// <param name="ppmkRelPath">
    ///   При удачном возвращении — ссылка на относительный моникер.
    /// </param>
    void RelativePathTo(UCOMIMoniker pmkOther, out UCOMIMoniker ppmkRelPath);

    /// <summary>
    ///   Возвращает отображаемое имя, которое представляет собой этот моникер доступное для чтения пользователю.
    /// </summary>
    /// <param name="pbc">
    ///   Ссылка на контекст привязки, используемый в данной операции.
    /// </param>
    /// <param name="pmkToLeft">
    ///   Ссылка на моникер слева от данного моникера, если моникер является частью составного моникера.
    /// </param>
    /// <param name="ppszDisplayName">
    ///   При удачном возвращении содержит строку отображаемого имени.
    /// </param>
    void GetDisplayName(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] out string ppszDisplayName);

    /// <summary>
    ///   Считывает столько знаков указанного отображаемого имени, как он понимает и создает моникер, соответствующий считанной части.
    /// </summary>
    /// <param name="pbc">
    ///   Ссылка на контекст привязки, используемый в данной операции привязки.
    /// </param>
    /// <param name="pmkToLeft">
    ///   Ссылка на специальное имя, созданное из отображаемого имени до этой точки.
    /// </param>
    /// <param name="pszDisplayName">
    ///   Ссылка на строку, содержащую оставшуюся часть анализируемого отображаемого имени.
    /// </param>
    /// <param name="pchEaten">
    ///   При удачном возвращении содержит число знаков в <paramref name="pszDisplayName" /> переданных на этом шаге.
    /// </param>
    /// <param name="ppmkOut">
    ///   Ссылка на моникер, построенный из <paramref name="pszDisplayName" />.
    /// </param>
    void ParseDisplayName(UCOMIBindCtx pbc, UCOMIMoniker pmkToLeft, [MarshalAs(UnmanagedType.LPWStr)] string pszDisplayName, out int pchEaten, out UCOMIMoniker ppmkOut);

    /// <summary>
    ///   Указывает, является ли данное специальное имя одним из классов моникеров, предоставляемых системой.
    /// </summary>
    /// <param name="pdwMksys">
    ///   Указатель на целое число, которое является одним из значений из <see langword="MKSYS" /> перечисления и ссылается на один из классов COM моникера.
    /// </param>
    void IsSystemMoniker(out int pdwMksys);
  }
}

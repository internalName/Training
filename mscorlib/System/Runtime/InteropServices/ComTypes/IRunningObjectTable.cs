// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.IRunningObjectTable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Предоставляет управляемое определение <see langword="IRunningObjectTable" /> интерфейса.
  /// </summary>
  [Guid("00000010-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [__DynamicallyInvokable]
  [ComImport]
  public interface IRunningObjectTable
  {
    /// <summary>Регистрирует, что предоставленный объект запущена.</summary>
    /// <param name="grfFlags">
    ///   Указывает ли ссылка таблицы запущенных объектов элемента (ROT) <paramref name="punkObject" /> слабой или строгой, а также управляет доступом к объекту, используя соответствующую запись в таблице ROT.
    /// </param>
    /// <param name="punkObject">
    ///   Ссылка на объект, зарегистрированный как выполняемый.
    /// </param>
    /// <param name="pmkObjectName">
    ///   Ссылка на специальное имя, определяющее <paramref name="punkObject" />.
    /// </param>
    /// <returns>
    ///   Значение, которое может использоваться для идентификации записи в таблице ROT при последовательных вызовах <see cref="M:System.Runtime.InteropServices.ComTypes.IRunningObjectTable.Revoke(System.Int32)" /> или <see cref="M:System.Runtime.InteropServices.ComTypes.IRunningObjectTable.NoteChangeTime(System.Int32,System.Runtime.InteropServices.ComTypes.FILETIME@)" />.
    /// </returns>
    [__DynamicallyInvokable]
    int Register(int grfFlags, [MarshalAs(UnmanagedType.Interface)] object punkObject, IMoniker pmkObjectName);

    /// <summary>
    ///   Отменяет регистрацию указанного объекта из таблицы текущих объектов (ROT).
    /// </summary>
    /// <param name="dwRegister">
    ///   Удаляемая запись в таблице текущих объектов (ROT).
    /// </param>
    [__DynamicallyInvokable]
    void Revoke(int dwRegister);

    /// <summary>
    ///   Определяет, зарегистрирован ли указанный моникер в таблице текущих объектов (ROT).
    /// </summary>
    /// <param name="pmkObjectName">
    ///   Ссылка на специальное имя для поиска в таблице текущих объектов (ROT).
    /// </param>
    /// <returns>
    ///   <see langword="HRESULT" /> Значение, указывающее на успешное или неуспешное выполнение операции.
    /// </returns>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsRunning(IMoniker pmkObjectName);

    /// <summary>
    ///   Возвращает зарегистрированный объект, если предоставленное имя объекта зарегистрировано как выполняемое.
    /// </summary>
    /// <param name="pmkObjectName">
    ///   Ссылка на специальное имя для поиска в таблице текущих объектов (ROT).
    /// </param>
    /// <param name="ppunkObject">
    ///   При возвращении данного метода содержит запрошенный выполняемый объект.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   <see langword="HRESULT" /> Значение, указывающее на успешное или неуспешное выполнение операции.
    /// </returns>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetObject(IMoniker pmkObjectName, [MarshalAs(UnmanagedType.Interface)] out object ppunkObject);

    /// <summary>
    ///   Заметки о времени, что определенный объект изменен так <see langword="IMoniker::GetTimeOfLastChange" /> можно сообщить о времени внесения изменений.
    /// </summary>
    /// <param name="dwRegister">
    ///   Таблицы текущих объектов (ROT) запись измененного объекта.
    /// </param>
    /// <param name="pfiletime">
    ///   Ссылка на время последнего изменения объекта.
    /// </param>
    [__DynamicallyInvokable]
    void NoteChangeTime(int dwRegister, ref FILETIME pfiletime);

    /// <summary>
    ///   Выполняет поиск этого моникера в таблице текущих объектов (ROT) и сообщает записанное время изменения, если он имеется.
    /// </summary>
    /// <param name="pmkObjectName">
    ///   Ссылка на специальное имя для поиска в таблице текущих объектов (ROT).
    /// </param>
    /// <param name="pfiletime">
    ///   При возвращении данного объекта содержит время последнего изменения объекта.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   <see langword="HRESULT" /> Значение, указывающее на успешное или неуспешное выполнение операции.
    /// </returns>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int GetTimeOfLastChange(IMoniker pmkObjectName, out FILETIME pfiletime);

    /// <summary>
    ///   Перечисляет объекты, зарегистрированные в данный момент выполняется.
    /// </summary>
    /// <param name="ppenumMoniker">
    ///   При возвращении данного метода содержит новый перечислитель для таблицы текущих объектов (ROT).
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void EnumRunning(out IEnumMoniker ppenumMoniker);
  }
}

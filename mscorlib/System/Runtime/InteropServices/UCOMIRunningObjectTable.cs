// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMIRunningObjectTable
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.IRunningObjectTable" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IRunningObjectTable instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("00000010-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface UCOMIRunningObjectTable
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
    /// <param name="pdwRegister">
    ///   Ссылка на 32-разрядное значение, которое может использоваться для идентификации записи в таблице ROT при последовательных вызовах <see cref="M:System.Runtime.InteropServices.UCOMIRunningObjectTable.Revoke(System.Int32)" /> или <see cref="M:System.Runtime.InteropServices.UCOMIRunningObjectTable.NoteChangeTime(System.Int32,System.Runtime.InteropServices.FILETIME@)" />.
    /// </param>
    void Register(int grfFlags, [MarshalAs(UnmanagedType.Interface)] object punkObject, UCOMIMoniker pmkObjectName, out int pdwRegister);

    /// <summary>
    ///   Отменяет регистрацию указанного объекта в таблице ROT.
    /// </summary>
    /// <param name="dwRegister">Запись ROT отмены.</param>
    void Revoke(int dwRegister);

    /// <summary>
    ///   Определяет, зарегистрирован ли в данный момент указанного моникера в таблице ROT.
    /// </summary>
    /// <param name="pmkObjectName">
    ///   Ссылка на специальное имя для поиска в таблице ROT.
    /// </param>
    void IsRunning(UCOMIMoniker pmkObjectName);

    /// <summary>
    ///   Возвращает зарегистрированный объект, если предоставленное имя объекта зарегистрировано как выполняемое.
    /// </summary>
    /// <param name="pmkObjectName">
    ///   Ссылка на специальное имя для поиска в таблице ROT.
    /// </param>
    /// <param name="ppunkObject">
    ///   При удачном возвращении содержит запрошенный выполняемый объект.
    /// </param>
    void GetObject(UCOMIMoniker pmkObjectName, [MarshalAs(UnmanagedType.Interface)] out object ppunkObject);

    /// <summary>
    ///   Выполняет запись времени, поэтому изменения определенного объекта <see langword="IMoniker::GetTimeOfLastChange" /> может сообщить о времени внесения изменений.
    /// </summary>
    /// <param name="dwRegister">
    ///   Запись ROT для измененного объекта.
    /// </param>
    /// <param name="pfiletime">
    ///   Ссылка на время последнего изменения объекта.
    /// </param>
    void NoteChangeTime(int dwRegister, ref FILETIME pfiletime);

    /// <summary>
    ///   Выполняет поиск этого моникера в таблице ROT и сообщает записанное время изменения, если он имеется.
    /// </summary>
    /// <param name="pmkObjectName">
    ///   Ссылка на специальное имя для поиска в таблице ROT.
    /// </param>
    /// <param name="pfiletime">
    ///   При удачном возвращении содержит время последнего изменения объекта.
    /// </param>
    void GetTimeOfLastChange(UCOMIMoniker pmkObjectName, out FILETIME pfiletime);

    /// <summary>
    ///   Перечисляет объекты, зарегистрированные в данный момент выполняется.
    /// </summary>
    /// <param name="ppenumMoniker">
    ///   При удачном возвращении — новый перечислитель для ROT.
    /// </param>
    void EnumRunning(out UCOMIEnumMoniker ppenumMoniker);
  }
}

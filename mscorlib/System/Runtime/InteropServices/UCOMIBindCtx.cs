// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMIBindCtx
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.BIND_OPTS" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IBindCtx instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("0000000e-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface UCOMIBindCtx
  {
    /// <summary>
    ///   Регистрирует переданный объект как один из объектов, был привязан во время операции моникера и которые необходимо освободить после ее завершения.
    /// </summary>
    /// <param name="punk">
    ///   Объект для регистрации с целью освобождения.
    /// </param>
    void RegisterObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

    /// <summary>
    ///   Удаляет объект из набора зарегистрированных объектов, нуждающихся в освобождении.
    /// </summary>
    /// <param name="punk">
    ///   Объект для отмены регистрации для выпуска.
    /// </param>
    void RevokeObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

    /// <summary>
    ///   Освобождает все объекты, зарегистрированные с контекстом привязки при помощи <see cref="M:System.Runtime.InteropServices.UCOMIBindCtx.RegisterObjectBound(System.Object)" />.
    /// </summary>
    void ReleaseBoundObjects();

    /// <summary>
    ///   Хранить в контексте привязки блок параметров, применяющийся к более поздним <see langword="UCOMIMoniker" /> операций, использующих этот контекст привязки.
    /// </summary>
    /// <param name="pbindopts">
    ///   Структура, содержащая задаваемые параметры привязки.
    /// </param>
    void SetBindOptions([In] ref BIND_OPTS pbindopts);

    /// <summary>
    ///   Возвращает параметры текущей привязки, хранящиеся в контексте этой привязки.
    /// </summary>
    /// <param name="pbindopts">
    ///   Указатель на структуру для получения параметров привязки.
    /// </param>
    void GetBindOptions(ref BIND_OPTS pbindopts);

    /// <summary>
    ///   Возвращает доступ к таблице текущих объектов (ROT), относящейся к этому процессу привязки.
    /// </summary>
    /// <param name="pprot">При удачном возвращении ссылку ROT.</param>
    void GetRunningObjectTable(out UCOMIRunningObjectTable pprot);

    /// <summary>
    ///   Регистрация заданный указатель для объекта под указанным именем в таблице внутреннего представления указателей объектов.
    /// </summary>
    /// <param name="pszKey">
    ///   Имя регистрируемого <paramref name="punk" /> с.
    /// </param>
    /// <param name="punk">Регистрируемый объект.</param>
    void RegisterObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [MarshalAs(UnmanagedType.Interface)] object punk);

    /// <summary>
    ///   Выполняет поиск заданного ключа таблицы внутреннего представления контекстных параметров объекта и возвращает соответствующий объект, если он существует.
    /// </summary>
    /// <param name="pszKey">Имя объекта для поиска.</param>
    /// <param name="ppunk">
    ///   При удачном возвращении — указатель на интерфейс объекта.
    /// </param>
    void GetObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [MarshalAs(UnmanagedType.Interface)] out object ppunk);

    /// <summary>
    ///   Перечисляет строки, являющиеся ключами таблицы внутреннего представления контекстных параметров объекта.
    /// </summary>
    /// <param name="ppenum">
    ///   При удачном возвращении ссылку на перечислитель параметров объекта.
    /// </param>
    void EnumObjectParam(out UCOMIEnumString ppenum);

    /// <summary>
    ///   Отменяет регистрацию объектов, найденных в этом разделе в таблице внутреннего представления контекстных параметров объекта, если любой такой ключ зарегистрирован.
    /// </summary>
    /// <param name="pszKey">Ключ для отмены регистрации.</param>
    void RevokeObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey);
  }
}

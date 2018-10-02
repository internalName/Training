// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.IBindCtx
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Предоставляет управляемое определение <see langword="IBindCtx" /> интерфейса.
  /// </summary>
  [Guid("0000000e-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [__DynamicallyInvokable]
  [ComImport]
  public interface IBindCtx
  {
    /// <summary>
    ///   Регистрирует переданный объект как один из объектов, был привязан во время операции моникера и которые необходимо освободить после завершения операции.
    /// </summary>
    /// <param name="punk">
    ///   Объект для регистрации с целью освобождения.
    /// </param>
    [__DynamicallyInvokable]
    void RegisterObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

    /// <summary>
    ///   Удаляет объект из набора зарегистрированных объектов, нуждающихся в освобождении.
    /// </summary>
    /// <param name="punk">
    ///   Объект для отмены регистрации для выпуска.
    /// </param>
    [__DynamicallyInvokable]
    void RevokeObjectBound([MarshalAs(UnmanagedType.Interface)] object punk);

    /// <summary>
    ///   Освобождает все объекты, зарегистрированные в контексте привязки с помощью <see cref="M:System.Runtime.InteropServices.ComTypes.IBindCtx.RegisterObjectBound(System.Object)" /> метод.
    /// </summary>
    [__DynamicallyInvokable]
    void ReleaseBoundObjects();

    /// <summary>
    ///   Хранит блок параметров в контексте привязки.
    ///    Эти параметры будут применены к более поздним <see langword="UCOMIMoniker" /> операций, которые используют этот контекст привязки.
    /// </summary>
    /// <param name="pbindopts">
    ///   Структура, содержащая задаваемые параметры привязки.
    /// </param>
    [__DynamicallyInvokable]
    void SetBindOptions([In] ref BIND_OPTS pbindopts);

    /// <summary>
    ///   Возвращает параметры текущей привязки, хранящиеся в контексте этой привязки.
    /// </summary>
    /// <param name="pbindopts">
    ///   Указатель на структуру для получения параметров привязки.
    /// </param>
    [__DynamicallyInvokable]
    void GetBindOptions(ref BIND_OPTS pbindopts);

    /// <summary>
    ///   Возвращает доступ к таблице текущих объектов (ROT) применимо к этому процессу привязки.
    /// </summary>
    /// <param name="pprot">
    ///   При возвращении данного метода содержит ссылку на таблицу текущих объектов (ROT).
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetRunningObjectTable(out IRunningObjectTable pprot);

    /// <summary>
    ///   Регистрирует заданный указатель для объекта под указанным именем в таблице внутреннего представления указателей объектов.
    /// </summary>
    /// <param name="pszKey">
    ///   Имя регистрируемого <paramref name="punk" /> с.
    /// </param>
    /// <param name="punk">Регистрируемый объект.</param>
    [__DynamicallyInvokable]
    void RegisterObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [MarshalAs(UnmanagedType.Interface)] object punk);

    /// <summary>
    ///   Выполняет поиск заданного ключа таблицы внутреннего представления контекстных параметров объекта и возвращает соответствующий объект, если он существует.
    /// </summary>
    /// <param name="pszKey">Имя объекта для поиска.</param>
    /// <param name="ppunk">
    ///   При возвращении данного метода содержит указатель на интерфейс объекта.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey, [MarshalAs(UnmanagedType.Interface)] out object ppunk);

    /// <summary>
    ///   Перечисляет строки, являющиеся ключами таблицы внутреннего представления контекстных параметров объекта.
    /// </summary>
    /// <param name="ppenum">
    ///   При возвращении данного метода содержит ссылку на перечислитель параметров объекта.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void EnumObjectParam(out IEnumString ppenum);

    /// <summary>
    ///   Отменяет регистрацию объектов, найденных в указанном разделе в таблице внутреннего представления контекстных параметров объекта, если этот ключ зарегистрирован.
    /// </summary>
    /// <param name="pszKey">Ключ для отмены регистрации.</param>
    /// <returns>
    ///   <see langword="S_OK" /><see langword="HRESULT" /> Значение, если указанный ключ был успешно удален из таблицы; в противном случае — <see langword="S_FALSE" /><see langword="HRESULT" /> значение.
    /// </returns>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int RevokeObjectParam([MarshalAs(UnmanagedType.LPWStr)] string pszKey);
  }
}

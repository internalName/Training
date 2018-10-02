// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.IPersistFile
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Предоставляет управляемое определение <see langword="IPersistFile" /> интерфейс с функциональными возможностями <see langword="IPersist" />.
  /// </summary>
  [Guid("0000010b-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [__DynamicallyInvokable]
  [ComImport]
  public interface IPersistFile
  {
    /// <summary>Извлекает идентификатор класса (CLSID) объекта.</summary>
    /// <param name="pClassID">
    ///   При возвращении данного метода содержит ссылку на идентификатор CLSID.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetClassID(out Guid pClassID);

    /// <summary>
    ///   Проверяет наличие изменений в объекте с момента последнего сохранения в текущем файле.
    /// </summary>
    /// <returns>
    ///   <see langword="S_OK" /> Если файл был изменен с момента последнего сохранения; <see langword="S_FALSE" /> Если файл не был изменен с момента последнего сохранения.
    /// </returns>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.PreserveSig)]
    int IsDirty();

    /// <summary>
    ///   Открывает указанный файл и инициализирует объект из содержимого файла.
    /// </summary>
    /// <param name="pszFileName">
    ///   Нулем строка, содержащая абсолютный путь для открытия файла.
    /// </param>
    /// <param name="dwMode">
    ///   Сочетание значений из <see langword="STGM" /> перечисление, чтобы указать режим доступа, в котором следует открыть <paramref name="pszFileName" />.
    /// </param>
    [__DynamicallyInvokable]
    void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);

    /// <summary>Сохраняет копию объекта в указанный файл.</summary>
    /// <param name="pszFileName">
    ///   Нулем строка, содержащая абсолютный путь файла, в котором требуется сохранить объект.
    /// </param>
    /// <param name="fRemember">
    ///   <see langword="true" /> используется для <paramref name="pszFileName" /> параметр в виде файла; в противном случае <see langword="false" />.
    /// </param>
    [__DynamicallyInvokable]
    void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);

    /// <summary>
    ///   Сообщает объекту, что он может выполнять запись в файл.
    /// </summary>
    /// <param name="pszFileName">
    ///   Абсолютный путь файла, где ранее был сохранен объект.
    /// </param>
    [__DynamicallyInvokable]
    void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

    /// <summary>
    ///   Возвращает абсолютный путь к текущему выполняемому файлу объекта или, если выполняемый файл отсутствует, по умолчанию запрос имени файла объекта.
    /// </summary>
    /// <param name="ppszFileName">
    ///   При возвращении данного метода содержит адрес указателя нулем строку, содержащую путь для текущего файла или по умолчанию запрос имени файла (например, *.txt).
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
  }
}

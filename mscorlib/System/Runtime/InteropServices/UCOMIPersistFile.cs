// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMIPersistFile
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.IPersistFile" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.IPersistFile instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("0000010b-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface UCOMIPersistFile
  {
    /// <summary>Извлекает идентификатор класса (CLSID) объекта.</summary>
    /// <param name="pClassID">
    ///   При удачном возвращении ссылку на идентификатор CLSID.
    /// </param>
    void GetClassID(out Guid pClassID);

    /// <summary>
    ///   Проверяет наличие изменений в объекте с момента последнего сохранения в текущем файле.
    /// </summary>
    /// <returns>
    ///   <see langword="S_OK" /> Если файл был изменен с момента последнего сохранения; <see langword="S_FALSE" /> Если файл не был изменен с момента последнего сохранения.
    /// </returns>
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
    void Load([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, int dwMode);

    /// <summary>Сохраняет копию объекта в указанный файл.</summary>
    /// <param name="pszFileName">
    ///   Нулем строка, содержащая абсолютный путь файла, в котором требуется сохранить объект.
    /// </param>
    /// <param name="fRemember">
    ///   Указывает, является ли <paramref name="pszFileName" /> — для использования в качестве текущего рабочего файла.
    /// </param>
    void Save([MarshalAs(UnmanagedType.LPWStr)] string pszFileName, [MarshalAs(UnmanagedType.Bool)] bool fRemember);

    /// <summary>
    ///   Сообщает объекту, что он может выполнять запись в файл.
    /// </summary>
    /// <param name="pszFileName">
    ///   Абсолютный путь файла, где ранее был сохранен объект.
    /// </param>
    void SaveCompleted([MarshalAs(UnmanagedType.LPWStr)] string pszFileName);

    /// <summary>
    ///   Возвращает абсолютный путь к выполняемому файлу объекта, или, если нет текущая рабочая файл по умолчанию запрос имени файла объекта.
    /// </summary>
    /// <param name="ppszFileName">
    ///   Адрес указателя нулем строку, содержащую путь для текущего файла или запрос имени файла по умолчанию (например, *.txt).
    /// </param>
    void GetCurFile([MarshalAs(UnmanagedType.LPWStr)] out string ppszFileName);
  }
}

// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.Registry
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32
{
  /// <summary>
  ///   Предоставляет <see cref="T:Microsoft.Win32.RegistryKey" /> объекты, представляющие корневые разделы в реестре Windows и <see langword="static" /> методы для доступа к пары "ключ значение".
  /// </summary>
  [ComVisible(true)]
  public static class Registry
  {
    /// <summary>
    ///   Содержит сведения о текущих настройках пользователя.
    ///    Это поле считывает базовый ключ реестра HKEY_CURRENT_USER
    /// </summary>
    public static readonly RegistryKey CurrentUser = RegistryKey.GetBaseKey(RegistryKey.HKEY_CURRENT_USER);
    /// <summary>
    ///   Содержит данные о конфигурации для локального компьютера.
    ///    Это поле считывает базовый ключ реестра HKEY_LOCAL_MACHINE.
    /// </summary>
    public static readonly RegistryKey LocalMachine = RegistryKey.GetBaseKey(RegistryKey.HKEY_LOCAL_MACHINE);
    /// <summary>
    ///   Определяет типы (или классы) документов и свойства, связанные с этими типами.
    ///    Это поле считывает базовый ключ реестра HKEY_CLASSES_ROOT.
    /// </summary>
    public static readonly RegistryKey ClassesRoot = RegistryKey.GetBaseKey(RegistryKey.HKEY_CLASSES_ROOT);
    /// <summary>
    ///   Содержит сведения о пользовательских настроек по умолчанию.
    ///    Это поле считывает базовый ключ реестра HKEY_USERS.
    /// </summary>
    public static readonly RegistryKey Users = RegistryKey.GetBaseKey(RegistryKey.HKEY_USERS);
    /// <summary>
    ///   Содержит сведения о производительности для компонентов программного обеспечения.
    ///    Это поле считывает базовый ключ реестра раздел HKEY_PERFORMANCE_DATA.
    /// </summary>
    public static readonly RegistryKey PerformanceData = RegistryKey.GetBaseKey(RegistryKey.HKEY_PERFORMANCE_DATA);
    /// <summary>
    ///   Содержит сведения о конфигурации, относящиеся к оборудованию, не относящиеся к пользователю.
    ///    Это поле считывает базовый ключ реестра HKEY_CURRENT_CONFIG.
    /// </summary>
    public static readonly RegistryKey CurrentConfig = RegistryKey.GetBaseKey(RegistryKey.HKEY_CURRENT_CONFIG);
    /// <summary>
    ///   Содержит динамические данные реестра.
    ///    Это поле считывает базовый ключ реестра HKEY_DYN_DATA Windows.
    /// </summary>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Операционная система не поддерживает динамические данные; то есть он не является Windows 98, Windows 98 второго издания и Windows Millennium Edition (Windows Me).
    /// </exception>
    [Obsolete("The DynData registry key only works on Win9x, which is no longer supported by the CLR.  On NT-based operating systems, use the PerformanceData registry key instead.")]
    public static readonly RegistryKey DynData = RegistryKey.GetBaseKey(RegistryKey.HKEY_DYN_DATA);

    [SecuritySafeCritical]
    static Registry()
    {
    }

    [SecurityCritical]
    private static RegistryKey GetBaseKeyFromKeyName(string keyName, out string subKeyName)
    {
      if (keyName == null)
        throw new ArgumentNullException(nameof (keyName));
      int length = keyName.IndexOf('\\');
      RegistryKey registryKey;
      switch (length == -1 ? keyName.ToUpper(CultureInfo.InvariantCulture) : keyName.Substring(0, length).ToUpper(CultureInfo.InvariantCulture))
      {
        case "HKEY_CLASSES_ROOT":
          registryKey = Registry.ClassesRoot;
          break;
        case "HKEY_CURRENT_CONFIG":
          registryKey = Registry.CurrentConfig;
          break;
        case "HKEY_CURRENT_USER":
          registryKey = Registry.CurrentUser;
          break;
        case "HKEY_DYN_DATA":
          registryKey = RegistryKey.GetBaseKey(RegistryKey.HKEY_DYN_DATA);
          break;
        case "HKEY_LOCAL_MACHINE":
          registryKey = Registry.LocalMachine;
          break;
        case "HKEY_PERFORMANCE_DATA":
          registryKey = Registry.PerformanceData;
          break;
        case "HKEY_USERS":
          registryKey = Registry.Users;
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_RegInvalidKeyName", (object) nameof (keyName)));
      }
      subKeyName = length == -1 || length == keyName.Length ? string.Empty : keyName.Substring(length + 1, keyName.Length - length - 1);
      return registryKey;
    }

    /// <summary>
    ///   Возвращает значение, связанное с указанным именем, в указанный раздел реестра.
    ///    Если имя не найдено в указанном разделе, возвращает значение по умолчанию, указать, или <see langword="null" /> Если указанный ключ не существует.
    /// </summary>
    /// <param name="keyName">
    ///   Полный путь ключа, начиная с корневой допустимый раздел реестра, например «HKEY_CURRENT_USER».
    /// </param>
    /// <param name="valueName">Имя пары имя/значение.</param>
    /// <param name="defaultValue">
    ///   Значение, возвращаемое, если <paramref name="valueName" /> не существует.
    /// </param>
    /// <returns>
    ///   <see langword="null" />Если заданные подраздела <paramref name="keyName" /> не существует; в противном случае — значение, связанное с <paramref name="valueName" />, или <paramref name="defaultValue" /> Если <paramref name="valueName" /> не найден.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для чтения из раздела реестра.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> , Содержит указанное значение, помечен для удаления.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="keyName" />не начинается с правильного корневого раздела реестра.
    /// </exception>
    [SecuritySafeCritical]
    public static object GetValue(string keyName, string valueName, object defaultValue)
    {
      string subKeyName;
      RegistryKey registryKey = Registry.GetBaseKeyFromKeyName(keyName, out subKeyName).OpenSubKey(subKeyName);
      if (registryKey == null)
        return (object) null;
      try
      {
        return registryKey.GetValue(valueName, defaultValue);
      }
      finally
      {
        registryKey.Close();
      }
    }

    /// <summary>
    ///   Задает пары указанного имени и значения в указанный раздел реестра.
    ///    Если указанный ключ не существует, он создается.
    /// </summary>
    /// <param name="keyName">
    ///   Полный путь ключа, начиная с корневой допустимый раздел реестра, например «HKEY_CURRENT_USER».
    /// </param>
    /// <param name="valueName">Имя пары имя/значение.</param>
    /// <param name="value">Значение для сохранения.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="keyName" />не начинается с правильного корневого раздела реестра.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="keyName" />больше, чем максимальная длина допускается (255 символов).
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Доступно только для чтения и поэтому не может быть записан; например, это корневой узел.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для создания или изменения реестра.
    /// </exception>
    public static void SetValue(string keyName, string valueName, object value)
    {
      Registry.SetValue(keyName, valueName, value, RegistryValueKind.Unknown);
    }

    /// <summary>
    ///   Задает пары имя/значение в указанный раздел реестра, используя заданный тип данных реестра.
    ///    Если указанный ключ не существует, он создается.
    /// </summary>
    /// <param name="keyName">
    ///   Полный путь ключа, начиная с корневой допустимый раздел реестра, например «HKEY_CURRENT_USER».
    /// </param>
    /// <param name="valueName">Имя пары имя/значение.</param>
    /// <param name="value">Значение для сохранения.</param>
    /// <param name="valueKind">
    ///   Тип данных реестра, используемый для хранения данных.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="keyName" />не начинается с правильного корневого раздела реестра.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="keyName" />больше, чем максимальная длина допускается (255 символов).
    /// 
    ///   -или-
    /// 
    ///   Тип <paramref name="value" /> не соответствует типу данных реестра, определяемое <paramref name="valueKind" />, поэтому данные не удалось правильно преобразовать.
    /// </exception>
    /// <exception cref="T:System.UnauthorizedAccessException">
    ///   <see cref="T:Microsoft.Win32.RegistryKey" /> Доступно только для чтения и поэтому не может быть записан; например, это корневой узел или ключ не был открыт с доступом на запись.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Пользователь не имеет разрешения, необходимые для создания или изменения реестра.
    /// </exception>
    [SecuritySafeCritical]
    public static void SetValue(string keyName, string valueName, object value, RegistryValueKind valueKind)
    {
      string subKeyName;
      RegistryKey subKey = Registry.GetBaseKeyFromKeyName(keyName, out subKeyName).CreateSubKey(subKeyName);
      try
      {
        subKey.SetValue(valueName, value, valueKind);
      }
      finally
      {
        subKey.Close();
      }
    }
  }
}

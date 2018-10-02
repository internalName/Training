// Decompiled with JetBrains decompiler
// Type: System.IAppDomainSetup
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Предоставляет сведения о привязке сборок, которые могут быть добавлены в экземпляр класса <see cref="T:System.AppDomain" />.
  /// </summary>
  [Guid("27FFF232-A7A8-40dd-8D4A-734AD59FCD41")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComVisible(true)]
  public interface IAppDomainSetup
  {
    /// <summary>
    ///   Возвращает или задает имя каталога, содержащего приложение.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> содержащий имя базового каталога приложения.
    /// </returns>
    string ApplicationBase { get; set; }

    /// <summary>Возвращает или задает имя приложения.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> , содержащий имя приложения.
    /// </returns>
    string ApplicationName { get; set; }

    /// <summary>
    ///   Возвращает и задает имя области, определенной в приложение где создаются теневые копии файлов.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> , полное имя каталога имя файла и путь, где создаются теневые копии файлов.
    /// </returns>
    string CachePath { get; set; }

    /// <summary>
    ///   Возвращает или задает имя файла конфигурации для домена приложения.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> , задающий имя файла конфигурации.
    /// </returns>
    string ConfigurationFile { get; set; }

    /// <summary>
    ///   Возвращает или задает каталог, в которых хранятся и становятся доступными динамически созданные файлы.
    /// </summary>
    /// <returns>
    ///   Значение <see cref="T:System.String" /> указывает на каталог, содержащий динамические сборки.
    /// </returns>
    string DynamicBase { get; set; }

    /// <summary>
    ///   Возвращает или задает расположение файла лицензии, связанного с этим доменом.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> задающий имя файла лицензии.
    /// </returns>
    string LicenseFile { get; set; }

    /// <summary>
    ///   Возвращает или задает список каталогов, в которых используется в сочетании с <see cref="P:System.AppDomainSetup.ApplicationBase" /> directory, чтобы производить поиск закрытых сборок.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.String" /> содержащий список имен каталогов, где каждое имя разделяются точкой с запятой.
    /// </returns>
    string PrivateBinPath { get; set; }

    /// <summary>
    ///   Возвращает или задает путь к закрытому каталогу двоичных файлов, используемый для размещения приложения.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.String" /> содержащий список имен каталогов, где каждое имя разделяются точкой с запятой.
    /// </returns>
    string PrivateBinPathProbe { get; set; }

    /// <summary>
    ///   Возвращает или задает имена каталогов, содержащих сборки, для которых будут созданы теневые копии.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.String" /> содержащий список имен каталогов, где каждое имя разделяются точкой с запятой.
    /// </returns>
    string ShadowCopyDirectories { get; set; }

    /// <summary>
    ///   Возвращает или задает строку, позволяющую определить, включено ли теневое копирование.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> содержит значение «true», чтобы указать, что теневое копирование включено, или «false», чтобы указать, если копирование отключено.
    /// </returns>
    string ShadowCopyFiles { get; set; }
  }
}

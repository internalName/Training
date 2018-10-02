// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.FileIOPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.AccessControl;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Разрешает применять действия безопасности для <see cref="T:System.Security.Permissions.FileIOPermission" /> к коду с помощью декларативной безопасности.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class FileIOPermissionAttribute : CodeAccessSecurityAttribute
  {
    private string m_read;
    private string m_write;
    private string m_append;
    private string m_pathDiscovery;
    private string m_viewAccess;
    private string m_changeAccess;
    [OptionalField(VersionAdded = 2)]
    private FileIOPermissionAccess m_allLocalFiles;
    [OptionalField(VersionAdded = 2)]
    private FileIOPermissionAccess m_allFiles;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.FileIOPermissionAttribute" /> указанным значением <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </summary>
    /// <param name="action">
    ///   Одно из значений <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="action" /> Параметр не является допустимым <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </exception>
    public FileIOPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>
    ///   Возвращает или задает доступ на чтение для файла или каталога, заданного строковым значением.
    /// </summary>
    /// <returns>
    ///   Абсолютный путь к файлу или каталогу для доступа на чтение.
    /// </returns>
    public string Read
    {
      get
      {
        return this.m_read;
      }
      set
      {
        this.m_read = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает доступ на запись для файла или каталога, заданного строковым значением.
    /// </summary>
    /// <returns>
    ///   Абсолютный путь к файлу или каталогу для доступа на запись.
    /// </returns>
    public string Write
    {
      get
      {
        return this.m_write;
      }
      set
      {
        this.m_write = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает добавить доступ к файлу или каталогу, заданному строковым значением.
    /// </summary>
    /// <returns>
    ///   Абсолютный путь к файлу или каталогу для доступа к дозаписи.
    /// </returns>
    public string Append
    {
      get
      {
        return this.m_append;
      }
      set
      {
        this.m_append = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает файл или каталог, к которому необходимо предоставить обнаружение пути.
    /// </summary>
    /// <returns>Абсолютный путь к файлу или каталогу.</returns>
    public string PathDiscovery
    {
      get
      {
        return this.m_pathDiscovery;
      }
      set
      {
        this.m_pathDiscovery = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает файл или каталог, в котором можно просмотреть данные управления доступом.
    /// </summary>
    /// <returns>
    ///   Абсолютный путь к файлу или каталогу, в котором можно просмотреть сведения об элементе управления.
    /// </returns>
    public string ViewAccessControl
    {
      get
      {
        return this.m_viewAccess;
      }
      set
      {
        this.m_viewAccess = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает файл или каталог, в котором можно изменить данные управления доступом.
    /// </summary>
    /// <returns>
    ///   Абсолютный путь к файлу или каталогу, в котором можно изменить сведения об элементе управления.
    /// </returns>
    public string ChangeAccessControl
    {
      get
      {
        return this.m_changeAccess;
      }
      set
      {
        this.m_changeAccess = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает полный доступ к файлу или каталогу, заданному строковым значением.
    /// </summary>
    /// <returns>
    ///   Абсолютный путь файла или каталога для полного доступа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод get не поддерживается для этого свойства.
    /// </exception>
    [Obsolete("Please use the ViewAndModify property instead.")]
    public string All
    {
      set
      {
        this.m_read = value;
        this.m_write = value;
        this.m_append = value;
        this.m_pathDiscovery = value;
      }
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_GetMethod"));
      }
    }

    /// <summary>
    ///   Возвращает или задает файл или каталог, в какой файл данных можно просмотреть и изменить.
    /// </summary>
    /// <returns>
    ///   Абсолютный путь к файлу или каталогу в файл, который можно просмотреть и изменить данные.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   <see langword="get" /> Вызывается метод доступа.
    ///    Метод доступа предоставляется только для совместимости с компилятором C#.
    /// </exception>
    public string ViewAndModify
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_GetMethod"));
      }
      set
      {
        this.m_read = value;
        this.m_write = value;
        this.m_append = value;
        this.m_pathDiscovery = value;
      }
    }

    /// <summary>
    ///   Получает или задает разрешенный доступ ко всем файлам.
    /// </summary>
    /// <returns>
    ///   Побитовое сочетание <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> значений, представляющих разрешения для всех файлов.
    ///    Значение по умолчанию — <see cref="F:System.Security.Permissions.FileIOPermissionAccess.NoAccess" />.
    /// </returns>
    public FileIOPermissionAccess AllFiles
    {
      get
      {
        return this.m_allFiles;
      }
      set
      {
        this.m_allFiles = value;
      }
    }

    /// <summary>
    ///   Получает или задает разрешенный доступ ко всем локальным файлам.
    /// </summary>
    /// <returns>
    ///   Побитовое сочетание <see cref="T:System.Security.Permissions.FileIOPermissionAccess" /> значения, которые представляет разрешения для всех локальных файлов.
    ///    Значение по умолчанию — <see cref="F:System.Security.Permissions.FileIOPermissionAccess.NoAccess" />.
    /// </returns>
    public FileIOPermissionAccess AllLocalFiles
    {
      get
      {
        return this.m_allLocalFiles;
      }
      set
      {
        this.m_allLocalFiles = value;
      }
    }

    /// <summary>
    ///   Создает и возвращает новый объект <see cref="T:System.Security.Permissions.FileIOPermission" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Permissions.FileIOPermission" /> соответствует этому атрибуту.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Сведения о пути для файла или каталога, для которого должна быть обеспечена безопасность доступа содержит недопустимые символы или спецификаторы подстановочных знаков.
    /// </exception>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new FileIOPermission(PermissionState.Unrestricted);
      FileIOPermission fileIoPermission = new FileIOPermission(PermissionState.None);
      if (this.m_read != null)
        fileIoPermission.SetPathList(FileIOPermissionAccess.Read, this.m_read);
      if (this.m_write != null)
        fileIoPermission.SetPathList(FileIOPermissionAccess.Write, this.m_write);
      if (this.m_append != null)
        fileIoPermission.SetPathList(FileIOPermissionAccess.Append, this.m_append);
      if (this.m_pathDiscovery != null)
        fileIoPermission.SetPathList(FileIOPermissionAccess.PathDiscovery, this.m_pathDiscovery);
      if (this.m_viewAccess != null)
        fileIoPermission.SetPathList(FileIOPermissionAccess.NoAccess, AccessControlActions.View, new string[1]
        {
          this.m_viewAccess
        }, false);
      if (this.m_changeAccess != null)
        fileIoPermission.SetPathList(FileIOPermissionAccess.NoAccess, AccessControlActions.Change, new string[1]
        {
          this.m_changeAccess
        }, false);
      fileIoPermission.AllFiles = this.m_allFiles;
      fileIoPermission.AllLocalFiles = this.m_allLocalFiles;
      return (IPermission) fileIoPermission;
    }
  }
}

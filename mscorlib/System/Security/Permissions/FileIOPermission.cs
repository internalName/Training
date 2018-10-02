// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.FileIOPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Управляет возможностью доступа к файлам и папкам.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class FileIOPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
  {
    private FileIOAccess m_read;
    private FileIOAccess m_write;
    private FileIOAccess m_append;
    private FileIOAccess m_pathDiscovery;
    [OptionalField(VersionAdded = 2)]
    private FileIOAccess m_viewAcl;
    [OptionalField(VersionAdded = 2)]
    private FileIOAccess m_changeAcl;
    private bool m_unrestricted;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.FileIOPermission" /> указанным состоянием разрешения: полностью ограниченное или неограниченное.
    /// </summary>
    /// <param name="state">
    ///   Одно из значений перечисления <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="state" /> не является допустимым значением для <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </exception>
    public FileIOPermission(PermissionState state)
    {
      if (state == PermissionState.Unrestricted)
      {
        this.m_unrestricted = true;
      }
      else
      {
        if (state != PermissionState.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
        this.m_unrestricted = false;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.FileIOPermission" /> с заданным доступом к указанному файлу или каталогу.
    /// </summary>
    /// <param name="access">
    ///   Битовая комбинация значений перечисления <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.
    /// </param>
    /// <param name="path">Абсолютный путь к файлу или каталогу.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="access" /> не является допустимым значением для <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="path" /> не является допустимой строкой.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="path" /> не указывает абсолютный путь к файлу или каталогу.
    /// </exception>
    [SecuritySafeCritical]
    public FileIOPermission(FileIOPermissionAccess access, string path)
    {
      FileIOPermission.VerifyAccess(access);
      string[] pathListOrig = new string[1]{ path };
      this.AddPathList(access, pathListOrig, false, true, false);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.FileIOPermission" /> заданным уровнем доступа к указанным файлам или каталогам.
    /// </summary>
    /// <param name="access">
    ///   Битовая комбинация значений перечисления <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.
    /// </param>
    /// <param name="pathList">
    ///   Массив, содержащий абсолютные пути к файлам и каталогам.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="access" /> не является допустимым значением для <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.
    /// 
    ///   -или-
    /// 
    ///   Запись в массиве <paramref name="pathList" /> не является допустимой строкой.
    /// </exception>
    [SecuritySafeCritical]
    public FileIOPermission(FileIOPermissionAccess access, string[] pathList)
    {
      FileIOPermission.VerifyAccess(access);
      this.AddPathList(access, pathList, false, true, false);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.FileIOPermission" /> указанным доступом к назначенному файлу или каталогу, а также заданными правами доступа к сведениям об управлении файлами.
    /// </summary>
    /// <param name="access">
    ///   Битовая комбинация значений перечисления <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.
    /// </param>
    /// <param name="control">
    ///   Битовая комбинация значений перечисления <see cref="T:System.Security.AccessControl.AccessControlActions" />.
    /// </param>
    /// <param name="path">Абсолютный путь к файлу или каталогу.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="access" /> не является допустимым значением для <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="path" /> не является допустимой строкой.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="path" /> не указывает абсолютный путь к файлу или каталогу.
    /// </exception>
    [SecuritySafeCritical]
    public FileIOPermission(FileIOPermissionAccess access, AccessControlActions control, string path)
    {
      FileIOPermission.VerifyAccess(access);
      string[] pathListOrig = new string[1]{ path };
      this.AddPathList(access, control, pathListOrig, false, true, false);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.FileIOPermission" /> с указанным доступом к назначенным файлам и каталогам, а также с заданными правами доступа к сведениям об управлении файлами.
    /// </summary>
    /// <param name="access">
    ///   Битовая комбинация значений перечисления <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.
    /// </param>
    /// <param name="control">
    ///   Битовая комбинация значений перечисления <see cref="T:System.Security.AccessControl.AccessControlActions" />.
    /// </param>
    /// <param name="pathList">
    ///   Массив, содержащий абсолютные пути к файлам и каталогам.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="access" /> не является допустимым значением для <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.
    /// 
    ///   -или-
    /// 
    ///   Запись в массиве <paramref name="pathList" /> не является допустимой строкой.
    /// </exception>
    [SecuritySafeCritical]
    public FileIOPermission(FileIOPermissionAccess access, AccessControlActions control, string[] pathList)
      : this(access, control, pathList, true, true)
    {
    }

    [SecurityCritical]
    internal FileIOPermission(FileIOPermissionAccess access, string[] pathList, bool checkForDuplicates, bool needFullPath)
    {
      FileIOPermission.VerifyAccess(access);
      this.AddPathList(access, pathList, checkForDuplicates, needFullPath, true);
    }

    [SecurityCritical]
    internal FileIOPermission(FileIOPermissionAccess access, AccessControlActions control, string[] pathList, bool checkForDuplicates, bool needFullPath)
    {
      FileIOPermission.VerifyAccess(access);
      this.AddPathList(access, control, pathList, checkForDuplicates, needFullPath, true);
    }

    /// <summary>
    ///   Задает указанный доступ к указанному файлу или каталогу, заменяя существующее состояние разрешения.
    /// </summary>
    /// <param name="access">
    ///   Поразрядное сочетание значений <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.
    /// </param>
    /// <param name="path">Абсолютный путь к файлу или каталогу.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="access" /> не является допустимым значением для <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="path" /> не является допустимой строкой.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="path" /> не указывает абсолютный путь к файлу или каталогу.
    /// </exception>
    public void SetPathList(FileIOPermissionAccess access, string path)
    {
      string[] pathList;
      if (path == null)
        pathList = new string[0];
      else
        pathList = new string[1]{ path };
      this.SetPathList(access, pathList, false);
    }

    /// <summary>
    ///   Устанавливает заданный доступ для указанных файлов и каталогов, заменяя текущее состояние заданного доступа на новый набор путей.
    /// </summary>
    /// <param name="access">
    ///   Поразрядное сочетание значений <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.
    /// </param>
    /// <param name="pathList">
    ///   Массив, содержащий абсолютные пути к файлам и каталогам.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="access" /> не является допустимым значением для <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.
    /// 
    ///   -или-
    /// 
    ///   Запись в параметре <paramref name="pathList" /> не является допустимой строкой.
    /// </exception>
    public void SetPathList(FileIOPermissionAccess access, string[] pathList)
    {
      this.SetPathList(access, pathList, true);
    }

    internal void SetPathList(FileIOPermissionAccess access, string[] pathList, bool checkForDuplicates)
    {
      this.SetPathList(access, AccessControlActions.None, pathList, checkForDuplicates);
    }

    [SecuritySafeCritical]
    internal void SetPathList(FileIOPermissionAccess access, AccessControlActions control, string[] pathList, bool checkForDuplicates)
    {
      FileIOPermission.VerifyAccess(access);
      if ((access & FileIOPermissionAccess.Read) != FileIOPermissionAccess.NoAccess)
        this.m_read = (FileIOAccess) null;
      if ((access & FileIOPermissionAccess.Write) != FileIOPermissionAccess.NoAccess)
        this.m_write = (FileIOAccess) null;
      if ((access & FileIOPermissionAccess.Append) != FileIOPermissionAccess.NoAccess)
        this.m_append = (FileIOAccess) null;
      if ((access & FileIOPermissionAccess.PathDiscovery) != FileIOPermissionAccess.NoAccess)
        this.m_pathDiscovery = (FileIOAccess) null;
      if ((control & AccessControlActions.View) != AccessControlActions.None)
        this.m_viewAcl = (FileIOAccess) null;
      if ((control & AccessControlActions.Change) != AccessControlActions.None)
        this.m_changeAcl = (FileIOAccess) null;
      this.m_unrestricted = false;
      this.AddPathList(access, control, pathList, checkForDuplicates, true, true);
    }

    /// <summary>
    ///   Добавляет доступ для заданного файла или каталога к существующему состоянию разрешения.
    /// </summary>
    /// <param name="access">
    ///   Поразрядное сочетание значений <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.
    /// </param>
    /// <param name="path">Абсолютный путь к файлу или каталогу.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="access" /> не является допустимым значением для <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="path" /> не является допустимой строкой.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="path" /> не указывает абсолютный путь к файлу или каталогу.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="path" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="path" /> имеет недопустимый формат.
    /// </exception>
    [SecuritySafeCritical]
    public void AddPathList(FileIOPermissionAccess access, string path)
    {
      string[] pathListOrig;
      if (path == null)
        pathListOrig = new string[0];
      else
        pathListOrig = new string[1]{ path };
      this.AddPathList(access, pathListOrig, false, true, false);
    }

    /// <summary>
    ///   Добавляет доступ для заданных файлов или каталогов к существующему состоянию разрешения.
    /// </summary>
    /// <param name="access">
    ///   Поразрядное сочетание значений <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.
    /// </param>
    /// <param name="pathList">
    ///   Массив, содержащий абсолютные пути к файлам и каталогам.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="access" /> не является допустимым значением для <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.
    /// 
    ///   -или-
    /// 
    ///   Запись в массиве <paramref name="pathList" /> недопустима.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Запись в массиве <paramref name="pathList" /> имеет недопустимый формат.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="pathList" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public void AddPathList(FileIOPermissionAccess access, string[] pathList)
    {
      this.AddPathList(access, pathList, true, true, true);
    }

    [SecurityCritical]
    internal void AddPathList(FileIOPermissionAccess access, string[] pathListOrig, bool checkForDuplicates, bool needFullPath, bool copyPathList)
    {
      this.AddPathList(access, AccessControlActions.None, pathListOrig, checkForDuplicates, needFullPath, copyPathList);
    }

    [SecurityCritical]
    internal void AddPathList(FileIOPermissionAccess access, AccessControlActions control, string[] pathListOrig, bool checkForDuplicates, bool needFullPath, bool copyPathList)
    {
      if (pathListOrig == null)
        throw new ArgumentNullException("pathList");
      if (pathListOrig.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyPath"));
      FileIOPermission.VerifyAccess(access);
      if (this.m_unrestricted)
        return;
      string[] str = pathListOrig;
      if (copyPathList)
      {
        str = new string[pathListOrig.Length];
        Array.Copy((Array) pathListOrig, (Array) str, pathListOrig.Length);
      }
      FileIOPermission.CheckIllegalCharacters(str, needFullPath);
      ArrayList listFromExpressions = StringExpressionSet.CreateListFromExpressions(str, needFullPath);
      if ((access & FileIOPermissionAccess.Read) != FileIOPermissionAccess.NoAccess)
      {
        if (this.m_read == null)
          this.m_read = new FileIOAccess();
        this.m_read.AddExpressions(listFromExpressions, checkForDuplicates);
      }
      if ((access & FileIOPermissionAccess.Write) != FileIOPermissionAccess.NoAccess)
      {
        if (this.m_write == null)
          this.m_write = new FileIOAccess();
        this.m_write.AddExpressions(listFromExpressions, checkForDuplicates);
      }
      if ((access & FileIOPermissionAccess.Append) != FileIOPermissionAccess.NoAccess)
      {
        if (this.m_append == null)
          this.m_append = new FileIOAccess();
        this.m_append.AddExpressions(listFromExpressions, checkForDuplicates);
      }
      if ((access & FileIOPermissionAccess.PathDiscovery) != FileIOPermissionAccess.NoAccess)
      {
        if (this.m_pathDiscovery == null)
          this.m_pathDiscovery = new FileIOAccess(true);
        this.m_pathDiscovery.AddExpressions(listFromExpressions, checkForDuplicates);
      }
      if ((control & AccessControlActions.View) != AccessControlActions.None)
      {
        if (this.m_viewAcl == null)
          this.m_viewAcl = new FileIOAccess();
        this.m_viewAcl.AddExpressions(listFromExpressions, checkForDuplicates);
      }
      if ((control & AccessControlActions.Change) == AccessControlActions.None)
        return;
      if (this.m_changeAcl == null)
        this.m_changeAcl = new FileIOAccess();
      this.m_changeAcl.AddExpressions(listFromExpressions, checkForDuplicates);
    }

    /// <summary>
    ///   Возвращает все файлы и каталоги с заданным <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.
    /// </summary>
    /// <param name="access">
    ///   Одно из значений <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />, представляющее один тип доступа к файлам.
    /// </param>
    /// <returns>
    ///   Массив, содержащий пути к файлам и каталогам, к которым предоставлен доступ, заданный параметром <paramref name="access" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="access" /> не является допустимым значением <see cref="T:System.Security.Permissions.FileIOPermissionAccess" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="access" /> имеет значение <see cref="F:System.Security.Permissions.FileIOPermissionAccess.AllAccess" />, которое представляет несколько типов доступа к файлам, или <see cref="F:System.Security.Permissions.FileIOPermissionAccess.NoAccess" />, которое не представляет ни один тип доступа к файлам.
    /// </exception>
    [SecuritySafeCritical]
    public string[] GetPathList(FileIOPermissionAccess access)
    {
      FileIOPermission.VerifyAccess(access);
      FileIOPermission.ExclusiveAccess(access);
      if (FileIOPermission.AccessIsSet(access, FileIOPermissionAccess.Read))
      {
        if (this.m_read == null)
          return (string[]) null;
        return this.m_read.ToStringArray();
      }
      if (FileIOPermission.AccessIsSet(access, FileIOPermissionAccess.Write))
      {
        if (this.m_write == null)
          return (string[]) null;
        return this.m_write.ToStringArray();
      }
      if (FileIOPermission.AccessIsSet(access, FileIOPermissionAccess.Append))
      {
        if (this.m_append == null)
          return (string[]) null;
        return this.m_append.ToStringArray();
      }
      if (!FileIOPermission.AccessIsSet(access, FileIOPermissionAccess.PathDiscovery))
        return (string[]) null;
      if (this.m_pathDiscovery == null)
        return (string[]) null;
      return this.m_pathDiscovery.ToStringArray();
    }

    /// <summary>
    ///   Получает или задает разрешенный доступ ко всем локальным файлам.
    /// </summary>
    /// <returns>
    ///   Набор флагов файлового ввода-вывода для всех локальных файлов.
    /// </returns>
    public FileIOPermissionAccess AllLocalFiles
    {
      get
      {
        if (this.m_unrestricted)
          return FileIOPermissionAccess.AllAccess;
        FileIOPermissionAccess permissionAccess = FileIOPermissionAccess.NoAccess;
        if (this.m_read != null && this.m_read.AllLocalFiles)
          permissionAccess |= FileIOPermissionAccess.Read;
        if (this.m_write != null && this.m_write.AllLocalFiles)
          permissionAccess |= FileIOPermissionAccess.Write;
        if (this.m_append != null && this.m_append.AllLocalFiles)
          permissionAccess |= FileIOPermissionAccess.Append;
        if (this.m_pathDiscovery != null && this.m_pathDiscovery.AllLocalFiles)
          permissionAccess |= FileIOPermissionAccess.PathDiscovery;
        return permissionAccess;
      }
      set
      {
        if ((value & FileIOPermissionAccess.Read) != FileIOPermissionAccess.NoAccess)
        {
          if (this.m_read == null)
            this.m_read = new FileIOAccess();
          this.m_read.AllLocalFiles = true;
        }
        else if (this.m_read != null)
          this.m_read.AllLocalFiles = false;
        if ((value & FileIOPermissionAccess.Write) != FileIOPermissionAccess.NoAccess)
        {
          if (this.m_write == null)
            this.m_write = new FileIOAccess();
          this.m_write.AllLocalFiles = true;
        }
        else if (this.m_write != null)
          this.m_write.AllLocalFiles = false;
        if ((value & FileIOPermissionAccess.Append) != FileIOPermissionAccess.NoAccess)
        {
          if (this.m_append == null)
            this.m_append = new FileIOAccess();
          this.m_append.AllLocalFiles = true;
        }
        else if (this.m_append != null)
          this.m_append.AllLocalFiles = false;
        if ((value & FileIOPermissionAccess.PathDiscovery) != FileIOPermissionAccess.NoAccess)
        {
          if (this.m_pathDiscovery == null)
            this.m_pathDiscovery = new FileIOAccess(true);
          this.m_pathDiscovery.AllLocalFiles = true;
        }
        else
        {
          if (this.m_pathDiscovery == null)
            return;
          this.m_pathDiscovery.AllLocalFiles = false;
        }
      }
    }

    /// <summary>
    ///   Получает или задает разрешенный доступ ко всем файлам.
    /// </summary>
    /// <returns>
    ///   Набор флагов файлового ввода-вывода для всех файлов.
    /// </returns>
    public FileIOPermissionAccess AllFiles
    {
      get
      {
        if (this.m_unrestricted)
          return FileIOPermissionAccess.AllAccess;
        FileIOPermissionAccess permissionAccess = FileIOPermissionAccess.NoAccess;
        if (this.m_read != null && this.m_read.AllFiles)
          permissionAccess |= FileIOPermissionAccess.Read;
        if (this.m_write != null && this.m_write.AllFiles)
          permissionAccess |= FileIOPermissionAccess.Write;
        if (this.m_append != null && this.m_append.AllFiles)
          permissionAccess |= FileIOPermissionAccess.Append;
        if (this.m_pathDiscovery != null && this.m_pathDiscovery.AllFiles)
          permissionAccess |= FileIOPermissionAccess.PathDiscovery;
        return permissionAccess;
      }
      set
      {
        if (value == FileIOPermissionAccess.AllAccess)
        {
          this.m_unrestricted = true;
        }
        else
        {
          if ((value & FileIOPermissionAccess.Read) != FileIOPermissionAccess.NoAccess)
          {
            if (this.m_read == null)
              this.m_read = new FileIOAccess();
            this.m_read.AllFiles = true;
          }
          else if (this.m_read != null)
            this.m_read.AllFiles = false;
          if ((value & FileIOPermissionAccess.Write) != FileIOPermissionAccess.NoAccess)
          {
            if (this.m_write == null)
              this.m_write = new FileIOAccess();
            this.m_write.AllFiles = true;
          }
          else if (this.m_write != null)
            this.m_write.AllFiles = false;
          if ((value & FileIOPermissionAccess.Append) != FileIOPermissionAccess.NoAccess)
          {
            if (this.m_append == null)
              this.m_append = new FileIOAccess();
            this.m_append.AllFiles = true;
          }
          else if (this.m_append != null)
            this.m_append.AllFiles = false;
          if ((value & FileIOPermissionAccess.PathDiscovery) != FileIOPermissionAccess.NoAccess)
          {
            if (this.m_pathDiscovery == null)
              this.m_pathDiscovery = new FileIOAccess(true);
            this.m_pathDiscovery.AllFiles = true;
          }
          else
          {
            if (this.m_pathDiscovery == null)
              return;
            this.m_pathDiscovery.AllFiles = false;
          }
        }
      }
    }

    private static void VerifyAccess(FileIOPermissionAccess access)
    {
      if ((access & ~FileIOPermissionAccess.AllAccess) != FileIOPermissionAccess.NoAccess)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) access));
    }

    private static void ExclusiveAccess(FileIOPermissionAccess access)
    {
      if (access == FileIOPermissionAccess.NoAccess)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
      if ((access & access - 1) != FileIOPermissionAccess.NoAccess)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
    }

    private static void CheckIllegalCharacters(string[] str, bool onlyCheckExtras)
    {
      for (int index = 0; index < str.Length; ++index)
      {
        if (str[index] == null)
          throw new ArgumentNullException("path");
        if (FileIOPermission.CheckExtraPathCharacters(str[index]))
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPathChars"));
        if (!onlyCheckExtras)
          Path.CheckInvalidPathChars(str[index], false);
      }
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool CheckExtraPathCharacters(string path)
    {
      for (int index = !CodeAccessSecurityEngine.QuickCheckForAllDemands() || AppContextSwitches.UseLegacyPathHandling ? 0 : (PathInternal.IsDevice(path) ? 4 : 0); index < path.Length; ++index)
      {
        switch (path[index])
        {
          case char.MinValue:
          case '*':
          case '?':
            return true;
          default:
            continue;
        }
      }
      return false;
    }

    private static bool AccessIsSet(FileIOPermissionAccess access, FileIOPermissionAccess question)
    {
      return (uint) (access & question) > 0U;
    }

    private bool IsEmpty()
    {
      if (this.m_unrestricted || this.m_read != null && !this.m_read.IsEmpty() || (this.m_write != null && !this.m_write.IsEmpty() || this.m_append != null && !this.m_append.IsEmpty()) || (this.m_pathDiscovery != null && !this.m_pathDiscovery.IsEmpty() || this.m_viewAcl != null && !this.m_viewAcl.IsEmpty()))
        return false;
      if (this.m_changeAcl != null)
        return this.m_changeAcl.IsEmpty();
      return true;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущее разрешение неограниченным.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущее разрешение является неограниченным. В противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsUnrestricted()
    {
      return this.m_unrestricted;
    }

    /// <summary>
    ///   Определяет, является ли текущее разрешение подмножеством указанного разрешения.
    /// </summary>
    /// <param name="target">
    ///   Разрешение, для которого требуется проверить отношение подмножества.
    ///    Его тип должен совпадать с типом текущего разрешения.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если текущее разрешение является подмножеством указанного разрешения. В противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="target" /> не равен <see langword="null" /> и имеет тип, не совпадающий с типом текущего разрешения.
    /// </exception>
    public override bool IsSubsetOf(IPermission target)
    {
      if (target == null)
        return this.IsEmpty();
      FileIOPermission fileIoPermission = target as FileIOPermission;
      if (fileIoPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (fileIoPermission.IsUnrestricted())
        return true;
      if (this.IsUnrestricted() || this.m_read != null && !this.m_read.IsSubsetOf(fileIoPermission.m_read) || (this.m_write != null && !this.m_write.IsSubsetOf(fileIoPermission.m_write) || this.m_append != null && !this.m_append.IsSubsetOf(fileIoPermission.m_append)) || (this.m_pathDiscovery != null && !this.m_pathDiscovery.IsSubsetOf(fileIoPermission.m_pathDiscovery) || this.m_viewAcl != null && !this.m_viewAcl.IsSubsetOf(fileIoPermission.m_viewAcl)))
        return false;
      if (this.m_changeAcl != null)
        return this.m_changeAcl.IsSubsetOf(fileIoPermission.m_changeAcl);
      return true;
    }

    /// <summary>
    ///   Создает и возвращает разрешение, представляющее собой пересечение текущего и указанного разрешений.
    /// </summary>
    /// <param name="target">
    ///   Разрешение, пересекающееся с текущим разрешением.
    ///    Его тип должен совпадать с типом текущего разрешения.
    /// </param>
    /// <returns>
    ///   Новое разрешение, представляющее собой пересечение текущего и указанного разрешений.
    ///    Это новое разрешение равно <see langword="null" />, если пересечение является пустым.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="target" /> не равен <see langword="null" /> и имеет тип, не совпадающий с типом текущего разрешения.
    /// </exception>
    public override IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      FileIOPermission fileIoPermission = target as FileIOPermission;
      if (fileIoPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.IsUnrestricted())
        return target.Copy();
      if (fileIoPermission.IsUnrestricted())
        return this.Copy();
      FileIOAccess fileIoAccess1 = this.m_read == null ? (FileIOAccess) null : this.m_read.Intersect(fileIoPermission.m_read);
      FileIOAccess fileIoAccess2 = this.m_write == null ? (FileIOAccess) null : this.m_write.Intersect(fileIoPermission.m_write);
      FileIOAccess fileIoAccess3 = this.m_append == null ? (FileIOAccess) null : this.m_append.Intersect(fileIoPermission.m_append);
      FileIOAccess fileIoAccess4 = this.m_pathDiscovery == null ? (FileIOAccess) null : this.m_pathDiscovery.Intersect(fileIoPermission.m_pathDiscovery);
      FileIOAccess fileIoAccess5 = this.m_viewAcl == null ? (FileIOAccess) null : this.m_viewAcl.Intersect(fileIoPermission.m_viewAcl);
      FileIOAccess fileIoAccess6 = this.m_changeAcl == null ? (FileIOAccess) null : this.m_changeAcl.Intersect(fileIoPermission.m_changeAcl);
      if ((fileIoAccess1 == null || fileIoAccess1.IsEmpty()) && (fileIoAccess2 == null || fileIoAccess2.IsEmpty()) && ((fileIoAccess3 == null || fileIoAccess3.IsEmpty()) && (fileIoAccess4 == null || fileIoAccess4.IsEmpty())) && ((fileIoAccess5 == null || fileIoAccess5.IsEmpty()) && (fileIoAccess6 == null || fileIoAccess6.IsEmpty())))
        return (IPermission) null;
      return (IPermission) new FileIOPermission(PermissionState.None)
      {
        m_unrestricted = false,
        m_read = fileIoAccess1,
        m_write = fileIoAccess2,
        m_append = fileIoAccess3,
        m_pathDiscovery = fileIoAccess4,
        m_viewAcl = fileIoAccess5,
        m_changeAcl = fileIoAccess6
      };
    }

    /// <summary>
    ///   Создает разрешение, представляющее собой объединение текущего и указанного разрешений.
    /// </summary>
    /// <param name="other">
    ///   Разрешение, которое требуется объединить с текущим разрешением.
    ///    Его тип должен совпадать с типом текущего разрешения.
    /// </param>
    /// <returns>
    ///   Новое разрешение, представляющее собой объединение текущего и указанного разрешений.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="other" /> не равен <see langword="null" /> и имеет тип, не совпадающий с типом текущего разрешения.
    /// </exception>
    public override IPermission Union(IPermission other)
    {
      if (other == null)
        return this.Copy();
      FileIOPermission fileIoPermission = other as FileIOPermission;
      if (fileIoPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.IsUnrestricted() || fileIoPermission.IsUnrestricted())
        return (IPermission) new FileIOPermission(PermissionState.Unrestricted);
      FileIOAccess fileIoAccess1 = this.m_read == null ? fileIoPermission.m_read : this.m_read.Union(fileIoPermission.m_read);
      FileIOAccess fileIoAccess2 = this.m_write == null ? fileIoPermission.m_write : this.m_write.Union(fileIoPermission.m_write);
      FileIOAccess fileIoAccess3 = this.m_append == null ? fileIoPermission.m_append : this.m_append.Union(fileIoPermission.m_append);
      FileIOAccess fileIoAccess4 = this.m_pathDiscovery == null ? fileIoPermission.m_pathDiscovery : this.m_pathDiscovery.Union(fileIoPermission.m_pathDiscovery);
      FileIOAccess fileIoAccess5 = this.m_viewAcl == null ? fileIoPermission.m_viewAcl : this.m_viewAcl.Union(fileIoPermission.m_viewAcl);
      FileIOAccess fileIoAccess6 = this.m_changeAcl == null ? fileIoPermission.m_changeAcl : this.m_changeAcl.Union(fileIoPermission.m_changeAcl);
      if ((fileIoAccess1 == null || fileIoAccess1.IsEmpty()) && (fileIoAccess2 == null || fileIoAccess2.IsEmpty()) && ((fileIoAccess3 == null || fileIoAccess3.IsEmpty()) && (fileIoAccess4 == null || fileIoAccess4.IsEmpty())) && ((fileIoAccess5 == null || fileIoAccess5.IsEmpty()) && (fileIoAccess6 == null || fileIoAccess6.IsEmpty())))
        return (IPermission) null;
      return (IPermission) new FileIOPermission(PermissionState.None)
      {
        m_unrestricted = false,
        m_read = fileIoAccess1,
        m_write = fileIoAccess2,
        m_append = fileIoAccess3,
        m_pathDiscovery = fileIoAccess4,
        m_viewAcl = fileIoAccess5,
        m_changeAcl = fileIoAccess6
      };
    }

    /// <summary>
    ///   Создает и возвращает идентичную копию текущего разрешения.
    /// </summary>
    /// <returns>Копия текущего разрешения.</returns>
    public override IPermission Copy()
    {
      FileIOPermission fileIoPermission = new FileIOPermission(PermissionState.None);
      if (this.m_unrestricted)
      {
        fileIoPermission.m_unrestricted = true;
      }
      else
      {
        fileIoPermission.m_unrestricted = false;
        if (this.m_read != null)
          fileIoPermission.m_read = this.m_read.Copy();
        if (this.m_write != null)
          fileIoPermission.m_write = this.m_write.Copy();
        if (this.m_append != null)
          fileIoPermission.m_append = this.m_append.Copy();
        if (this.m_pathDiscovery != null)
          fileIoPermission.m_pathDiscovery = this.m_pathDiscovery.Copy();
        if (this.m_viewAcl != null)
          fileIoPermission.m_viewAcl = this.m_viewAcl.Copy();
        if (this.m_changeAcl != null)
          fileIoPermission.m_changeAcl = this.m_changeAcl.Copy();
      }
      return (IPermission) fileIoPermission;
    }

    /// <summary>
    ///   Создает кодировку XML для разрешения и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Кодировка XML разрешения, включающая любые сведения о состоянии.
    /// </returns>
    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.FileIOPermission");
      if (!this.IsUnrestricted())
      {
        if (this.m_read != null && !this.m_read.IsEmpty())
          permissionElement.AddAttribute("Read", SecurityElement.Escape(this.m_read.ToString()));
        if (this.m_write != null && !this.m_write.IsEmpty())
          permissionElement.AddAttribute("Write", SecurityElement.Escape(this.m_write.ToString()));
        if (this.m_append != null && !this.m_append.IsEmpty())
          permissionElement.AddAttribute("Append", SecurityElement.Escape(this.m_append.ToString()));
        if (this.m_pathDiscovery != null && !this.m_pathDiscovery.IsEmpty())
          permissionElement.AddAttribute("PathDiscovery", SecurityElement.Escape(this.m_pathDiscovery.ToString()));
        if (this.m_viewAcl != null && !this.m_viewAcl.IsEmpty())
          permissionElement.AddAttribute("ViewAcl", SecurityElement.Escape(this.m_viewAcl.ToString()));
        if (this.m_changeAcl != null && !this.m_changeAcl.IsEmpty())
          permissionElement.AddAttribute("ChangeAcl", SecurityElement.Escape(this.m_changeAcl.ToString()));
      }
      else
        permissionElement.AddAttribute("Unrestricted", "true");
      return permissionElement;
    }

    /// <summary>
    ///   Восстанавливает разрешение с указанным состоянием из кодировки XML.
    /// </summary>
    /// <param name="esd">
    ///   Кодировка XML, используемая для восстановления разрешения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="esd" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="esd" /> не является допустимым элементом разрешения.
    /// 
    ///   -или-
    /// 
    ///   Несовместимый номер версии параметра <paramref name="esd" />.
    /// </exception>
    [SecuritySafeCritical]
    public override void FromXml(SecurityElement esd)
    {
      CodeAccessPermission.ValidateElement(esd, (IPermission) this);
      if (XMLUtil.IsUnrestricted(esd))
      {
        this.m_unrestricted = true;
      }
      else
      {
        this.m_unrestricted = false;
        string str1 = esd.Attribute("Read");
        this.m_read = str1 == null ? (FileIOAccess) null : new FileIOAccess(str1);
        string str2 = esd.Attribute("Write");
        this.m_write = str2 == null ? (FileIOAccess) null : new FileIOAccess(str2);
        string str3 = esd.Attribute("Append");
        this.m_append = str3 == null ? (FileIOAccess) null : new FileIOAccess(str3);
        string str4 = esd.Attribute("PathDiscovery");
        if (str4 != null)
        {
          this.m_pathDiscovery = new FileIOAccess(str4);
          this.m_pathDiscovery.PathDiscovery = true;
        }
        else
          this.m_pathDiscovery = (FileIOAccess) null;
        string str5 = esd.Attribute("ViewAcl");
        this.m_viewAcl = str5 == null ? (FileIOAccess) null : new FileIOAccess(str5);
        string str6 = esd.Attribute("ChangeAcl");
        if (str6 != null)
          this.m_changeAcl = new FileIOAccess(str6);
        else
          this.m_changeAcl = (FileIOAccess) null;
      }
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return FileIOPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 2;
    }

    /// <summary>
    ///   Определяет, равен ли заданный объект <see cref="T:System.Security.Permissions.FileIOPermission" /> текущему объекту <see cref="T:System.Security.Permissions.FileIOPermission" />.
    /// </summary>
    /// <param name="obj">
    ///   Объект <see cref="T:System.Security.Permissions.FileIOPermission" />, который требуется сравнить с текущим объектом <see cref="T:System.Security.Permissions.FileIOPermission" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный объект <see cref="T:System.Security.Permissions.FileIOPermission" /> равен текущему объекту <see cref="T:System.Security.Permissions.FileIOPermission" />; в противном случае — <see langword="false" />.
    /// </returns>
    [ComVisible(false)]
    public override bool Equals(object obj)
    {
      FileIOPermission fileIoPermission = obj as FileIOPermission;
      if (fileIoPermission == null)
        return false;
      if (this.m_unrestricted && fileIoPermission.m_unrestricted)
        return true;
      if (this.m_unrestricted != fileIoPermission.m_unrestricted)
        return false;
      if (this.m_read == null)
      {
        if (fileIoPermission.m_read != null && !fileIoPermission.m_read.IsEmpty())
          return false;
      }
      else if (!this.m_read.Equals((object) fileIoPermission.m_read))
        return false;
      if (this.m_write == null)
      {
        if (fileIoPermission.m_write != null && !fileIoPermission.m_write.IsEmpty())
          return false;
      }
      else if (!this.m_write.Equals((object) fileIoPermission.m_write))
        return false;
      if (this.m_append == null)
      {
        if (fileIoPermission.m_append != null && !fileIoPermission.m_append.IsEmpty())
          return false;
      }
      else if (!this.m_append.Equals((object) fileIoPermission.m_append))
        return false;
      if (this.m_pathDiscovery == null)
      {
        if (fileIoPermission.m_pathDiscovery != null && !fileIoPermission.m_pathDiscovery.IsEmpty())
          return false;
      }
      else if (!this.m_pathDiscovery.Equals((object) fileIoPermission.m_pathDiscovery))
        return false;
      if (this.m_viewAcl == null)
      {
        if (fileIoPermission.m_viewAcl != null && !fileIoPermission.m_viewAcl.IsEmpty())
          return false;
      }
      else if (!this.m_viewAcl.Equals((object) fileIoPermission.m_viewAcl))
        return false;
      if (this.m_changeAcl == null)
      {
        if (fileIoPermission.m_changeAcl != null && !fileIoPermission.m_changeAcl.IsEmpty())
          return false;
      }
      else if (!this.m_changeAcl.Equals((object) fileIoPermission.m_changeAcl))
        return false;
      return true;
    }

    /// <summary>
    ///   Возвращает хэш-код для объекта <see cref="T:System.Security.Permissions.FileIOPermission" />, который можно использовать в алгоритмах хэширования и структурах данных, например в хэш-таблице.
    /// </summary>
    /// <returns>
    ///   Хэш-код для текущего объекта <see cref="T:System.Security.Permissions.FileIOPermission" />.
    /// </returns>
    [ComVisible(false)]
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    [SecuritySafeCritical]
    internal static void QuickDemand(FileIOPermissionAccess access, string fullPath, bool checkForDuplicates = false, bool needFullPath = true)
    {
      if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
        new FileIOPermission(access, new string[1]
        {
          fullPath
        }, (checkForDuplicates ? 1 : 0) != 0, (needFullPath ? 1 : 0) != 0).Demand();
      else
        FileIOPermission.EmulateFileIOPermissionChecks(fullPath);
    }

    [SecuritySafeCritical]
    internal static void QuickDemand(FileIOPermissionAccess access, string[] fullPathList, bool checkForDuplicates = false, bool needFullPath = true)
    {
      if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
      {
        new FileIOPermission(access, fullPathList, checkForDuplicates, needFullPath).Demand();
      }
      else
      {
        foreach (string fullPath in fullPathList)
          FileIOPermission.EmulateFileIOPermissionChecks(fullPath);
      }
    }

    [SecuritySafeCritical]
    internal static void QuickDemand(PermissionState state)
    {
      if (CodeAccessSecurityEngine.QuickCheckForAllDemands())
        return;
      new FileIOPermission(state).Demand();
    }

    [SecuritySafeCritical]
    internal static void QuickDemand(FileIOPermissionAccess access, AccessControlActions control, string fullPath, bool checkForDuplicates = false, bool needFullPath = true)
    {
      if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
        new FileIOPermission(access, control, new string[1]
        {
          fullPath
        }, (checkForDuplicates ? 1 : 0) != 0, (needFullPath ? 1 : 0) != 0).Demand();
      else
        FileIOPermission.EmulateFileIOPermissionChecks(fullPath);
    }

    [SecuritySafeCritical]
    internal static void QuickDemand(FileIOPermissionAccess access, AccessControlActions control, string[] fullPathList, bool checkForDuplicates = true, bool needFullPath = true)
    {
      if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
      {
        new FileIOPermission(access, control, fullPathList, checkForDuplicates, needFullPath).Demand();
      }
      else
      {
        foreach (string fullPath in fullPathList)
          FileIOPermission.EmulateFileIOPermissionChecks(fullPath);
      }
    }

    internal static void EmulateFileIOPermissionChecks(string fullPath)
    {
      if (!AppContextSwitches.UseLegacyPathHandling && PathInternal.IsDevice(fullPath))
        return;
      if (PathInternal.HasWildCardCharacters(fullPath))
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPathChars"));
      if (PathInternal.HasInvalidVolumeSeparator(fullPath))
        throw new NotSupportedException(Environment.GetResourceString("Argument_PathFormatNotSupported"));
    }
  }
}

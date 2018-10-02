// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.RegistryPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Разрешает применять действия безопасности для <see cref="T:System.Security.Permissions.RegistryPermission" /> к коду с помощью декларативной безопасности.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class RegistryPermissionAttribute : CodeAccessSecurityAttribute
  {
    private string m_read;
    private string m_write;
    private string m_create;
    private string m_viewAcl;
    private string m_changeAcl;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.RegistryPermissionAttribute" /> указанным значением <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </summary>
    /// <param name="action">
    ///   Одно из значений <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="action" /> Параметр не является допустимым <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </exception>
    public RegistryPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>
    ///   Возвращает или задает доступ на чтение для указанных разделов реестра.
    /// </summary>
    /// <returns>
    ///   Список путей к разделам реестра, доступ для чтения, разделенных точкой с запятой.
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
    ///   Возвращает или задает доступ на запись для указанных разделов реестра.
    /// </summary>
    /// <returns>
    ///   Разделенный точками с запятыми список путей к разделам реестра, для доступа к записи.
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
    ///   Возвращает или задает созданный уровень доступа для указанных разделов реестра.
    /// </summary>
    /// <returns>
    ///   Список путей к разделам реестра, для создания уровня доступа, разделенных точкой с запятой.
    /// </returns>
    public string Create
    {
      get
      {
        return this.m_create;
      }
      set
      {
        this.m_create = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает представление управления доступом для указанных разделов реестра.
    /// </summary>
    /// <returns>
    ///   Разделенный точками с запятыми список путей к разделам реестра, для управления доступом к просмотру.
    /// </returns>
    public string ViewAccessControl
    {
      get
      {
        return this.m_viewAcl;
      }
      set
      {
        this.m_viewAcl = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает изменение управления доступом для указанных разделов реестра.
    /// </summary>
    /// <returns>
    ///   Список путей к разделам реестра, для управления доступом к изменениям, разделенных точкой с запятой.
    ///    .
    /// </returns>
    public string ChangeAccessControl
    {
      get
      {
        return this.m_changeAcl;
      }
      set
      {
        this.m_changeAcl = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает указанный набор разделов реестра, которые можно просматривать и изменять.
    /// </summary>
    /// <returns>
    ///   Список путей к разделам реестра, для создания, чтения и записи, разделенных точкой с запятой.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызывается метод доступа get; он предоставляется только для совместимости с компилятором C#.
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
        this.m_create = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает полный доступ для указанных разделов реестра.
    /// </summary>
    /// <returns>
    ///   Разделенный точками с запятыми список путей к разделам реестра, для полного доступа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызывается метод доступа get; он предоставляется только для совместимости с компилятором C#.
    /// </exception>
    [Obsolete("Please use the ViewAndModify property instead.")]
    public string All
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_GetMethod"));
      }
      set
      {
        this.m_read = value;
        this.m_write = value;
        this.m_create = value;
      }
    }

    /// <summary>
    ///   Создает и возвращает новый объект <see cref="T:System.Security.Permissions.RegistryPermission" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Permissions.RegistryPermission" />, соответствующий этому атрибуту.
    /// </returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new RegistryPermission(PermissionState.Unrestricted);
      RegistryPermission registryPermission = new RegistryPermission(PermissionState.None);
      if (this.m_read != null)
        registryPermission.SetPathList(RegistryPermissionAccess.Read, this.m_read);
      if (this.m_write != null)
        registryPermission.SetPathList(RegistryPermissionAccess.Write, this.m_write);
      if (this.m_create != null)
        registryPermission.SetPathList(RegistryPermissionAccess.Create, this.m_create);
      if (this.m_viewAcl != null)
        registryPermission.SetPathList(AccessControlActions.View, this.m_viewAcl);
      if (this.m_changeAcl != null)
        registryPermission.SetPathList(AccessControlActions.Change, this.m_changeAcl);
      return (IPermission) registryPermission;
    }
  }
}

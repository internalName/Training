// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.UrlIdentityPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Разрешает применять действия безопасности для <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> к коду с помощью декларативной безопасности.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class UrlIdentityPermissionAttribute : CodeAccessSecurityAttribute
  {
    private string m_url;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.UrlIdentityPermissionAttribute" /> указанным значением <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </summary>
    /// <param name="action">
    ///   Одно из значений <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </param>
    public UrlIdentityPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>
    ///   Возвращает или задает полный URL-адрес вызывающего кода.
    /// </summary>
    /// <returns>
    ///   URL-адрес в соответствии с URL-адресом, указанным хостом.
    /// </returns>
    public string Url
    {
      get
      {
        return this.m_url;
      }
      set
      {
        this.m_url = value;
      }
    }

    /// <summary>
    ///   Создает и возвращает новый объект <see cref="T:System.Security.Permissions.UrlIdentityPermission" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> соответствует этому атрибуту.
    /// </returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new UrlIdentityPermission(PermissionState.Unrestricted);
      if (this.m_url == null)
        return (IPermission) new UrlIdentityPermission(PermissionState.None);
      return (IPermission) new UrlIdentityPermission(this.m_url);
    }
  }
}

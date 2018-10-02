// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.SiteIdentityPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Разрешает выполнять действия по безопасности для <see cref="T:System.Security.Permissions.SiteIdentityPermission" /> для применения в коде с помощью декларативной безопасности.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class SiteIdentityPermissionAttribute : CodeAccessSecurityAttribute
  {
    private string m_site;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.SiteIdentityPermissionAttribute" /> указанным значением <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </summary>
    /// <param name="action">
    ///   Одно из значений <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </param>
    public SiteIdentityPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>Возвращает или задает имя узла вызывающего кода.</summary>
    /// <returns>
    ///   Имя узла для сравнения имени узла, указанным поставщиком безопасности.
    /// </returns>
    public string Site
    {
      get
      {
        return this.m_site;
      }
      set
      {
        this.m_site = value;
      }
    }

    /// <summary>
    ///   Создает и возвращает новый экземпляр <see cref="T:System.Security.Permissions.SiteIdentityPermission" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Permissions.SiteIdentityPermission" /> соответствует этому атрибуту.
    /// </returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new SiteIdentityPermission(PermissionState.Unrestricted);
      if (this.m_site == null)
        return (IPermission) new SiteIdentityPermission(PermissionState.None);
      return (IPermission) new SiteIdentityPermission(this.m_site);
    }
  }
}

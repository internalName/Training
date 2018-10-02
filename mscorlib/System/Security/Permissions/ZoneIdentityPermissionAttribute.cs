// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.ZoneIdentityPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Разрешает применять действия безопасности для <see cref="T:System.Security.Permissions.ZoneIdentityPermission" /> к коду с помощью декларативной безопасности.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class ZoneIdentityPermissionAttribute : CodeAccessSecurityAttribute
  {
    private SecurityZone m_flag = SecurityZone.NoZone;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.ZoneIdentityPermissionAttribute" /> указанным значением <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </summary>
    /// <param name="action">
    ///   Одно из значений <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </param>
    public ZoneIdentityPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>
    ///   Возвращает или задает членство в зоне содержимого, указанной значением свойства.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Security.SecurityZone" />.
    /// </returns>
    public SecurityZone Zone
    {
      get
      {
        return this.m_flag;
      }
      set
      {
        this.m_flag = value;
      }
    }

    /// <summary>
    ///   Создает и возвращает новый объект <see cref="T:System.Security.Permissions.ZoneIdentityPermission" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Permissions.ZoneIdentityPermission" />, соответствующий этому атрибуту.
    /// </returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new ZoneIdentityPermission(PermissionState.Unrestricted);
      return (IPermission) new ZoneIdentityPermission(this.m_flag);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.PrincipalPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Разрешает применять действия безопасности для <see cref="T:System.Security.Permissions.PrincipalPermission" /> к коду с помощью декларативной безопасности.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class PrincipalPermissionAttribute : CodeAccessSecurityAttribute
  {
    private bool m_authenticated = true;
    private string m_name;
    private string m_role;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.PrincipalPermissionAttribute" /> указанным значением <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </summary>
    /// <param name="action">
    ///   Одно из значений <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </param>
    public PrincipalPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>
    ///   Получает или задает имя удостоверения, связанного с текущим субъектом.
    /// </summary>
    /// <returns>
    ///   Имя для сопоставления с именем, предоставляемым базовым поставщиком безопасности на основе ролей.
    /// </returns>
    public string Name
    {
      get
      {
        return this.m_name;
      }
      set
      {
        this.m_name = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает членство в указанной роли безопасности.
    /// </summary>
    /// <returns>
    ///   Имя роли, предоставленное основным поставщиком безопасности на основе ролей.
    /// </returns>
    public string Role
    {
      get
      {
        return this.m_role;
      }
      set
      {
        this.m_role = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, прошел ли текущий субъект проверку подлинности от основного поставщика безопасности на основе ролей.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий субъект прошел проверку подлинности; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool Authenticated
    {
      get
      {
        return this.m_authenticated;
      }
      set
      {
        this.m_authenticated = value;
      }
    }

    /// <summary>
    ///   Создает и возвращает новый объект <see cref="T:System.Security.Permissions.PrincipalPermission" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Permissions.PrincipalPermission" />, соответствующий этому атрибуту.
    /// </returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new PrincipalPermission(PermissionState.Unrestricted);
      return (IPermission) new PrincipalPermission(this.m_name, this.m_role, this.m_authenticated);
    }
  }
}

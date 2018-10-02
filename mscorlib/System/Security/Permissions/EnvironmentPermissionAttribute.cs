// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.EnvironmentPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Разрешает применять действия безопасности для <see cref="T:System.Security.Permissions.EnvironmentPermission" /> к коду с помощью декларативной безопасности.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class EnvironmentPermissionAttribute : CodeAccessSecurityAttribute
  {
    private string m_read;
    private string m_write;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.EnvironmentPermissionAttribute" /> указанным значением <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </summary>
    /// <param name="action">
    ///   Одно из значений <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="action" /> не является допустимым значением для <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </exception>
    public EnvironmentPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>
    ///   Возвращает или задает доступ на чтение для переменных среды, указанных строковым значением.
    /// </summary>
    /// <returns>Список переменных среды для доступа на чтение.</returns>
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
    ///   Возвращает или задает доступ на запись для переменных среды, указанных строковым значением.
    /// </summary>
    /// <returns>Список переменных среды для доступа на запись.</returns>
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
    ///   Устанавливает полный доступ для переменных среды, указанных строковым значением.
    /// </summary>
    /// <returns>Список переменных среды для полного доступа.</returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод get не поддерживается для этого свойства.
    /// </exception>
    public string All
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_GetMethod"));
      }
      set
      {
        this.m_write = value;
        this.m_read = value;
      }
    }

    /// <summary>
    ///   Создает и возвращает новый объект <see cref="T:System.Security.Permissions.EnvironmentPermission" />.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Security.Permissions.EnvironmentPermission" /> Соответствует этому атрибуту.
    /// </returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new EnvironmentPermission(PermissionState.Unrestricted);
      EnvironmentPermission environmentPermission = new EnvironmentPermission(PermissionState.None);
      if (this.m_read != null)
        environmentPermission.SetPathList(EnvironmentPermissionAccess.Read, this.m_read);
      if (this.m_write != null)
        environmentPermission.SetPathList(EnvironmentPermissionAccess.Write, this.m_write);
      return (IPermission) environmentPermission;
    }
  }
}

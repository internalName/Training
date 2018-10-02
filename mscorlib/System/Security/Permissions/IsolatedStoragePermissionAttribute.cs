// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.IsolatedStoragePermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Разрешает выполнять действия по безопасности для <see cref="T:System.Security.Permissions.IsolatedStoragePermission" /> для применения в коде с помощью декларативной безопасности.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public abstract class IsolatedStoragePermissionAttribute : CodeAccessSecurityAttribute
  {
    internal long m_userQuota;
    internal IsolatedStorageContainment m_allowed;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.IsolatedStoragePermissionAttribute" /> указанным значением <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </summary>
    /// <param name="action">
    ///   Одно из значений <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </param>
    protected IsolatedStoragePermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>
    ///   Возвращает или задает максимальный размер хранилища пользователя квоты.
    /// </summary>
    /// <returns>
    ///   Максимальный размер квоты хранилища пользователя в байтах.
    /// </returns>
    public long UserQuota
    {
      set
      {
        this.m_userQuota = value;
      }
      get
      {
        return this.m_userQuota;
      }
    }

    /// <summary>
    ///   Возвращает или задает уровень изолированного хранилища, которое должно быть объявлено.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Security.Permissions.IsolatedStorageContainment" />.
    /// </returns>
    public IsolatedStorageContainment UsageAllowed
    {
      set
      {
        this.m_allowed = value;
      }
      get
      {
        return this.m_allowed;
      }
    }
  }
}

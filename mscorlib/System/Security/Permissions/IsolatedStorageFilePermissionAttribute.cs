// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.IsolatedStorageFilePermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Разрешает применять действия безопасности для <see cref="T:System.Security.Permissions.IsolatedStorageFilePermission" /> к коду с помощью декларативной безопасности.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class IsolatedStorageFilePermissionAttribute : IsolatedStoragePermissionAttribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.IsolatedStorageFilePermissionAttribute" /> указанным значением <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </summary>
    /// <param name="action">
    ///   Одно из значений <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </param>
    public IsolatedStorageFilePermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>
    ///   Создает и возвращает новый объект <see cref="T:System.Security.Permissions.IsolatedStorageFilePermission" />.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Security.Permissions.IsolatedStorageFilePermission" /> Соответствует этому атрибуту.
    /// </returns>
    public override IPermission CreatePermission()
    {
      IsolatedStorageFilePermission storageFilePermission;
      if (this.m_unrestricted)
      {
        storageFilePermission = new IsolatedStorageFilePermission(PermissionState.Unrestricted);
      }
      else
      {
        storageFilePermission = new IsolatedStorageFilePermission(PermissionState.None);
        storageFilePermission.UserQuota = this.m_userQuota;
        storageFilePermission.UsageAllowed = this.m_allowed;
      }
      return (IPermission) storageFilePermission;
    }
  }
}

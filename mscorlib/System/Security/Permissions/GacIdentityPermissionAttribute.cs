// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.GacIdentityPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Разрешает выполнять действия по безопасности для <see cref="T:System.Security.Permissions.GacIdentityPermission" /> для применения в коде с помощью декларативной безопасности.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class GacIdentityPermissionAttribute : CodeAccessSecurityAttribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.GacIdentityPermissionAttribute" /> заданным значением <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </summary>
    /// <param name="action">
    ///   Одно из значений <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="action" /> Параметр не является допустимым <see cref="T:System.Security.Permissions.SecurityAction" /> значение.
    /// </exception>
    public GacIdentityPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>
    ///   Создает новый объект <see cref="T:System.Security.Permissions.GacIdentityPermission" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Permissions.GacIdentityPermission" /> соответствует этому атрибуту.
    /// </returns>
    public override IPermission CreatePermission()
    {
      return (IPermission) new GacIdentityPermission();
    }
  }
}

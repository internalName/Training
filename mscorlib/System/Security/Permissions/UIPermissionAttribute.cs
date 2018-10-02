// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.UIPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Разрешает применять действия безопасности для <see cref="T:System.Security.Permissions.UIPermission" /> к коду с помощью декларативной безопасности.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class UIPermissionAttribute : CodeAccessSecurityAttribute
  {
    private UIPermissionWindow m_windowFlag;
    private UIPermissionClipboard m_clipboardFlag;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.UIPermissionAttribute" /> указанным значением <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </summary>
    /// <param name="action">
    ///   Одно из значений <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </param>
    public UIPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>
    ///   Возвращает или задает тип доступа к ресурсам окна, которые разрешено.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Security.Permissions.UIPermissionWindow" />.
    /// </returns>
    public UIPermissionWindow Window
    {
      get
      {
        return this.m_windowFlag;
      }
      set
      {
        this.m_windowFlag = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает тип доступа в буфер обмена, которое разрешено.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Security.Permissions.UIPermissionClipboard" />.
    /// </returns>
    public UIPermissionClipboard Clipboard
    {
      get
      {
        return this.m_clipboardFlag;
      }
      set
      {
        this.m_clipboardFlag = value;
      }
    }

    /// <summary>
    ///   Создает и возвращает новый объект <see cref="T:System.Security.Permissions.UIPermission" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Permissions.UIPermission" /> соответствует этому атрибуту.
    /// </returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new UIPermission(PermissionState.Unrestricted);
      return (IPermission) new UIPermission(this.m_windowFlag, this.m_clipboardFlag);
    }
  }
}

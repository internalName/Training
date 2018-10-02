// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.UIPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Управляет разрешениями, относящимися к пользовательским интерфейсам и буферу обмена.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class UIPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
  {
    private UIPermissionWindow m_windowFlag;
    private UIPermissionClipboard m_clipboardFlag;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.UIPermission" /> указанным состоянием доступа: полностью ограниченный или неограниченный.
    /// </summary>
    /// <param name="state">Одно из значений перечисления.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="state" /> Параметр не является допустимым <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </exception>
    public UIPermission(PermissionState state)
    {
      if (state == PermissionState.Unrestricted)
      {
        this.SetUnrestricted(true);
      }
      else
      {
        if (state != PermissionState.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
        this.SetUnrestricted(false);
        this.Reset();
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.UIPermission" /> указанными разрешениями для доступа к окнам и буферу обмена.
    /// </summary>
    /// <param name="windowFlag">Одно из значений перечисления.</param>
    /// <param name="clipboardFlag">Одно из значений перечисления.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="windowFlag" /> не является допустимым значением <see cref="T:System.Security.Permissions.UIPermissionWindow" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="clipboardFlag" /> не является допустимым значением <see cref="T:System.Security.Permissions.UIPermissionClipboard" />.
    /// </exception>
    public UIPermission(UIPermissionWindow windowFlag, UIPermissionClipboard clipboardFlag)
    {
      UIPermission.VerifyWindowFlag(windowFlag);
      UIPermission.VerifyClipboardFlag(clipboardFlag);
      this.m_windowFlag = windowFlag;
      this.m_clipboardFlag = clipboardFlag;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.UIPermission" /> с доступом к окнам и без доступа к буферу обмена.
    /// </summary>
    /// <param name="windowFlag">Одно из значений перечисления.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="windowFlag" /> не является допустимым значением <see cref="T:System.Security.Permissions.UIPermissionWindow" />.
    /// </exception>
    public UIPermission(UIPermissionWindow windowFlag)
    {
      UIPermission.VerifyWindowFlag(windowFlag);
      this.m_windowFlag = windowFlag;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.UIPermission" /> с доступом к буферу обмена и без доступа к окнам.
    /// </summary>
    /// <param name="clipboardFlag">Одно из значений перечисления.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="clipboardFlag" /> не является допустимым значением <see cref="T:System.Security.Permissions.UIPermissionClipboard" />.
    /// </exception>
    public UIPermission(UIPermissionClipboard clipboardFlag)
    {
      UIPermission.VerifyClipboardFlag(clipboardFlag);
      this.m_clipboardFlag = clipboardFlag;
    }

    /// <summary>
    ///   Возвращает или задает уровень доступа к окну, представленный разрешением.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Security.Permissions.UIPermissionWindow" />.
    /// </returns>
    public UIPermissionWindow Window
    {
      set
      {
        UIPermission.VerifyWindowFlag(value);
        this.m_windowFlag = value;
      }
      get
      {
        return this.m_windowFlag;
      }
    }

    /// <summary>
    ///   Получает или задает уровень доступа к буферу обмена, представленный разрешением.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Security.Permissions.UIPermissionClipboard" />.
    /// </returns>
    public UIPermissionClipboard Clipboard
    {
      set
      {
        UIPermission.VerifyClipboardFlag(value);
        this.m_clipboardFlag = value;
      }
      get
      {
        return this.m_clipboardFlag;
      }
    }

    private static void VerifyWindowFlag(UIPermissionWindow flag)
    {
      switch (flag)
      {
        case UIPermissionWindow.NoWindows:
        case UIPermissionWindow.SafeSubWindows:
        case UIPermissionWindow.SafeTopLevelWindows:
        case UIPermissionWindow.AllWindows:
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) flag));
      }
    }

    private static void VerifyClipboardFlag(UIPermissionClipboard flag)
    {
      switch (flag)
      {
        case UIPermissionClipboard.NoClipboard:
        case UIPermissionClipboard.OwnClipboard:
        case UIPermissionClipboard.AllClipboard:
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) flag));
      }
    }

    private void Reset()
    {
      this.m_windowFlag = UIPermissionWindow.NoWindows;
      this.m_clipboardFlag = UIPermissionClipboard.NoClipboard;
    }

    private void SetUnrestricted(bool unrestricted)
    {
      if (!unrestricted)
        return;
      this.m_windowFlag = UIPermissionWindow.AllWindows;
      this.m_clipboardFlag = UIPermissionClipboard.AllClipboard;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущее разрешение неограниченным.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущее разрешение является неограниченным. В противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsUnrestricted()
    {
      if (this.m_windowFlag == UIPermissionWindow.AllWindows)
        return this.m_clipboardFlag == UIPermissionClipboard.AllClipboard;
      return false;
    }

    /// <summary>
    ///   Определяет, является ли текущее разрешение подмножеством указанного разрешения.
    /// </summary>
    /// <param name="target">
    ///   Разрешение на проверку наличия связи подмножеств.
    ///    Его тип должен совпадать с типом текущего разрешения.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если текущее разрешение является подмножеством указанного разрешения. В противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="target" /> не равен <see langword="null" /> и имеет тип, не совпадающий с типом текущего разрешения.
    /// </exception>
    public override bool IsSubsetOf(IPermission target)
    {
      if (target == null)
      {
        if (this.m_windowFlag == UIPermissionWindow.NoWindows)
          return this.m_clipboardFlag == UIPermissionClipboard.NoClipboard;
        return false;
      }
      try
      {
        UIPermission uiPermission = (UIPermission) target;
        if (uiPermission.IsUnrestricted())
          return true;
        if (this.IsUnrestricted())
          return false;
        return this.m_windowFlag <= uiPermission.m_windowFlag && this.m_clipboardFlag <= uiPermission.m_clipboardFlag;
      }
      catch (InvalidCastException ex)
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      }
    }

    /// <summary>
    ///   Создает и возвращает разрешение, представляющее собой пересечение текущего и указанного разрешений.
    /// </summary>
    /// <param name="target">
    ///   Разрешение, пересекающееся с текущим разрешением.
    ///    Его тип должен совпадать с типом текущего разрешения.
    /// </param>
    /// <returns>
    ///   Новое разрешение, представляющее собой пересечение текущего и указанного разрешений.
    ///    Это новое разрешение равно <see langword="null" />, если пересечение является пустым.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="target" /> не равен <see langword="null" /> и имеет тип, не совпадающий с типом текущего разрешения.
    /// </exception>
    public override IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      if (!this.VerifyType(target))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      UIPermission uiPermission = (UIPermission) target;
      UIPermissionWindow windowFlag = this.m_windowFlag < uiPermission.m_windowFlag ? this.m_windowFlag : uiPermission.m_windowFlag;
      UIPermissionClipboard clipboardFlag = this.m_clipboardFlag < uiPermission.m_clipboardFlag ? this.m_clipboardFlag : uiPermission.m_clipboardFlag;
      if (windowFlag == UIPermissionWindow.NoWindows && clipboardFlag == UIPermissionClipboard.NoClipboard)
        return (IPermission) null;
      return (IPermission) new UIPermission(windowFlag, clipboardFlag);
    }

    /// <summary>
    ///   Создает разрешение, представляющее собой объединение текущего и указанного разрешений.
    /// </summary>
    /// <param name="target">
    ///   Разрешение, которое требуется объединить с текущим разрешением.
    ///    Его тип должен совпадать с типом текущего разрешения.
    /// </param>
    /// <returns>
    ///   Новое разрешение, представляющее собой объединение текущего и указанного разрешений.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="target" /> не равен <see langword="null" /> и имеет тип, не совпадающий с типом текущего разрешения.
    /// </exception>
    public override IPermission Union(IPermission target)
    {
      if (target == null)
        return this.Copy();
      if (!this.VerifyType(target))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      UIPermission uiPermission = (UIPermission) target;
      UIPermissionWindow windowFlag = this.m_windowFlag > uiPermission.m_windowFlag ? this.m_windowFlag : uiPermission.m_windowFlag;
      UIPermissionClipboard clipboardFlag = this.m_clipboardFlag > uiPermission.m_clipboardFlag ? this.m_clipboardFlag : uiPermission.m_clipboardFlag;
      if (windowFlag == UIPermissionWindow.NoWindows && clipboardFlag == UIPermissionClipboard.NoClipboard)
        return (IPermission) null;
      return (IPermission) new UIPermission(windowFlag, clipboardFlag);
    }

    /// <summary>
    ///   Создает и возвращает идентичную копию текущего разрешения.
    /// </summary>
    /// <returns>Копия текущего разрешения.</returns>
    public override IPermission Copy()
    {
      return (IPermission) new UIPermission(this.m_windowFlag, this.m_clipboardFlag);
    }

    /// <summary>
    ///   Создает кодировку XML для разрешения и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Кодировка XML разрешения, включающая любые сведения о состоянии.
    /// </returns>
    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.UIPermission");
      if (!this.IsUnrestricted())
      {
        if (this.m_windowFlag != UIPermissionWindow.NoWindows)
          permissionElement.AddAttribute("Window", Enum.GetName(typeof (UIPermissionWindow), (object) this.m_windowFlag));
        if (this.m_clipboardFlag != UIPermissionClipboard.NoClipboard)
          permissionElement.AddAttribute("Clipboard", Enum.GetName(typeof (UIPermissionClipboard), (object) this.m_clipboardFlag));
      }
      else
        permissionElement.AddAttribute("Unrestricted", "true");
      return permissionElement;
    }

    /// <summary>
    ///   Восстанавливает разрешение с указанным состоянием из кодировки XML.
    /// </summary>
    /// <param name="esd">
    ///   Кодировка XML, используемая для восстановления разрешения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="esd" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="esd" /> не является допустимым элементом разрешения.
    /// 
    ///   -или-
    /// 
    ///   Недопустимый номер версии параметра <paramref name="esd" />.
    /// </exception>
    public override void FromXml(SecurityElement esd)
    {
      CodeAccessPermission.ValidateElement(esd, (IPermission) this);
      if (XMLUtil.IsUnrestricted(esd))
      {
        this.SetUnrestricted(true);
      }
      else
      {
        this.m_windowFlag = UIPermissionWindow.NoWindows;
        this.m_clipboardFlag = UIPermissionClipboard.NoClipboard;
        string str1 = esd.Attribute("Window");
        if (str1 != null)
          this.m_windowFlag = (UIPermissionWindow) Enum.Parse(typeof (UIPermissionWindow), str1);
        string str2 = esd.Attribute("Clipboard");
        if (str2 == null)
          return;
        this.m_clipboardFlag = (UIPermissionClipboard) Enum.Parse(typeof (UIPermissionClipboard), str2);
      }
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return UIPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 7;
    }
  }
}

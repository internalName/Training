// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.FileDialogPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Управляет возможностью доступа к файлам или папкам с помощью диалогового окна Файл.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class FileDialogPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
  {
    private FileDialogPermissionAccess access;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.FileDialogPermission" /> с указанным состоянием разрешения: ограниченным или неограниченным.
    /// </summary>
    /// <param name="state">
    ///   Одно из значений <see cref="T:System.Security.Permissions.PermissionState" /> (<see langword="Unrestricted" /> или <see langword="None" />).
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="state" /> не является допустимым значением для <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </exception>
    public FileDialogPermission(PermissionState state)
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
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.FileDialogPermission" /> с заданным доступом.
    /// </summary>
    /// <param name="access">
    ///   Поразрядное сочетание значений <see cref="T:System.Security.Permissions.FileDialogPermissionAccess" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="access" /> не является допустимым сочетанием значений <see cref="T:System.Security.Permissions.FileDialogPermissionAccess" />.
    /// </exception>
    public FileDialogPermission(FileDialogPermissionAccess access)
    {
      FileDialogPermission.VerifyAccess(access);
      this.access = access;
    }

    /// <summary>Получает или задает разрешенный доступ к файлам.</summary>
    /// <returns>Разрешенный доступ к файлам.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Попытка установить для параметра <paramref name="access" /> значение, которое не является допустимым сочетанием значений <see cref="T:System.Security.Permissions.FileDialogPermissionAccess" />.
    /// </exception>
    public FileDialogPermissionAccess Access
    {
      get
      {
        return this.access;
      }
      set
      {
        FileDialogPermission.VerifyAccess(value);
        this.access = value;
      }
    }

    /// <summary>
    ///   Создает и возвращает идентичную копию текущего разрешения.
    /// </summary>
    /// <returns>Копия текущего разрешения.</returns>
    public override IPermission Copy()
    {
      return (IPermission) new FileDialogPermission(this.access);
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
    ///   Номер версии <paramref name="esd" /> параметр не поддерживается.
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
        this.access = FileDialogPermissionAccess.None;
        string str = esd.Attribute("Access");
        if (str == null)
          return;
        this.access = (FileDialogPermissionAccess) Enum.Parse(typeof (FileDialogPermissionAccess), str);
      }
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return FileDialogPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 1;
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
      FileDialogPermissionAccess access = this.access & ((FileDialogPermission) target).Access;
      if (access == FileDialogPermissionAccess.None)
        return (IPermission) null;
      return (IPermission) new FileDialogPermission(access);
    }

    /// <summary>
    ///   Определяет, является ли текущее разрешение подмножеством указанного разрешения.
    /// </summary>
    /// <param name="target">
    ///   Разрешение, для которого требуется проверить отношение подмножества.
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
        return this.access == FileDialogPermissionAccess.None;
      try
      {
        FileDialogPermission dialogPermission = (FileDialogPermission) target;
        if (dialogPermission.IsUnrestricted())
          return true;
        if (this.IsUnrestricted())
          return false;
        int num1 = (int) (this.access & FileDialogPermissionAccess.Open);
        int num2 = (int) (this.access & FileDialogPermissionAccess.Save);
        int num3 = (int) (dialogPermission.Access & FileDialogPermissionAccess.Open);
        int num4 = (int) (dialogPermission.Access & FileDialogPermissionAccess.Save);
        return num1 <= num3 && num2 <= num4;
      }
      catch (InvalidCastException ex)
      {
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущее разрешение неограниченным.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущее разрешение является неограниченным. В противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsUnrestricted()
    {
      return this.access == FileDialogPermissionAccess.OpenSave;
    }

    private void Reset()
    {
      this.access = FileDialogPermissionAccess.None;
    }

    private void SetUnrestricted(bool unrestricted)
    {
      if (!unrestricted)
        return;
      this.access = FileDialogPermissionAccess.OpenSave;
    }

    /// <summary>
    ///   Создает кодировку XML для разрешения и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Кодировка XML разрешения, включающая сведения о состоянии.
    /// </returns>
    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.FileDialogPermission");
      if (!this.IsUnrestricted())
      {
        if (this.access != FileDialogPermissionAccess.None)
          permissionElement.AddAttribute("Access", Enum.GetName(typeof (FileDialogPermissionAccess), (object) this.access));
      }
      else
        permissionElement.AddAttribute("Unrestricted", "true");
      return permissionElement;
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
      return (IPermission) new FileDialogPermission(this.access | ((FileDialogPermission) target).Access);
    }

    private static void VerifyAccess(FileDialogPermissionAccess access)
    {
      if ((access & ~FileDialogPermissionAccess.OpenSave) != FileDialogPermissionAccess.None)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) access));
    }
  }
}

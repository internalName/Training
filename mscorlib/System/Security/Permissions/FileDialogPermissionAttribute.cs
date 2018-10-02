// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.FileDialogPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Разрешает применять действия безопасности для <see cref="T:System.Security.Permissions.FileDialogPermission" /> к коду с помощью декларативной безопасности.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class FileDialogPermissionAttribute : CodeAccessSecurityAttribute
  {
    private FileDialogPermissionAccess m_access;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.FileDialogPermissionAttribute" /> указанным значением <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </summary>
    /// <param name="action">
    ///   Одно из значений <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </param>
    public FileDialogPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, объявлено ли разрешение на открытие файлов с помощью диалогового окна файла.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если разрешение на открытие файлов с помощью диалога файлов объявлено; в противном случае — <see langword="false" />.
    /// </returns>
    public bool Open
    {
      get
      {
        return (uint) (this.m_access & FileDialogPermissionAccess.Open) > 0U;
      }
      set
      {
        this.m_access = value ? this.m_access | FileDialogPermissionAccess.Open : this.m_access & ~FileDialogPermissionAccess.Open;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, объявлено ли разрешение на сохранение файлов с помощью диалогового окна файла.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если разрешение на сохранение файлов с помощью диалога файлов объявлено; в противном случае — <see langword="false" />.
    /// </returns>
    public bool Save
    {
      get
      {
        return (uint) (this.m_access & FileDialogPermissionAccess.Save) > 0U;
      }
      set
      {
        this.m_access = value ? this.m_access | FileDialogPermissionAccess.Save : this.m_access & ~FileDialogPermissionAccess.Save;
      }
    }

    /// <summary>
    ///   Создает и возвращает новый объект <see cref="T:System.Security.Permissions.FileDialogPermission" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Permissions.FileDialogPermission" />, соответствующий этому атрибуту.
    /// </returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new FileDialogPermission(PermissionState.Unrestricted);
      return (IPermission) new FileDialogPermission(this.m_access);
    }
  }
}

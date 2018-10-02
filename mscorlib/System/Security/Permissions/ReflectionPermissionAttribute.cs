// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.ReflectionPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Разрешает применять действия безопасности для <see cref="T:System.Security.Permissions.ReflectionPermission" /> к коду с помощью декларативной безопасности.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class ReflectionPermissionAttribute : CodeAccessSecurityAttribute
  {
    private ReflectionPermissionFlag m_flag;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.ReflectionPermissionAttribute" /> указанным значением <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </summary>
    /// <param name="action">
    ///   Одно из значений <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </param>
    public ReflectionPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>
    ///   Получает или задает текущие допустимые использования отражения.
    /// </summary>
    /// <returns>
    ///   Один или несколько <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" /> значения, объединенные с помощью битовой операции или.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Предпринята попытка задать этому свойству недопустимое значение.
    ///    Допустимые значения см. в разделе <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" />.
    /// </exception>
    public ReflectionPermissionFlag Flags
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
    ///   Возвращает или задает значение, указывающее, допустимо ли отражение на члены, которые не видны.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если отражение на члены, которые не видны разрешено; в противном случае — <see langword="false" />.
    /// </returns>
    [Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
    public bool TypeInformation
    {
      get
      {
        return (uint) (this.m_flag & ReflectionPermissionFlag.TypeInformation) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | ReflectionPermissionFlag.TypeInformation : this.m_flag & ~ReflectionPermissionFlag.TypeInformation;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, разрешено ли вызов действий на члены, не являющиеся открытыми.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если вызов действий на члены, не являющиеся открытыми, разрешен; в противном случае — <see langword="false" />.
    /// </returns>
    public bool MemberAccess
    {
      get
      {
        return (uint) (this.m_flag & ReflectionPermissionFlag.MemberAccess) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | ReflectionPermissionFlag.MemberAccess : this.m_flag & ~ReflectionPermissionFlag.MemberAccess;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, следует ли использовать некоторые функции в <see cref="N:System.Reflection.Emit" />, такие как разрешено генерирование символов отладки.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если использование соответствующих функций разрешено; в противном случае — <see langword="false" />.
    /// </returns>
    [Obsolete("This permission is no longer used by the CLR.")]
    public bool ReflectionEmit
    {
      get
      {
        return (uint) (this.m_flag & ReflectionPermissionFlag.ReflectionEmit) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | ReflectionPermissionFlag.ReflectionEmit : this.m_flag & ~ReflectionPermissionFlag.ReflectionEmit;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, разрешено ли вызов с ограничениями элементов, не являющиеся открытыми.
    ///    Вызов с ограничениями означает, что набор прав сборки, содержащей вызываемый член закрытым должен быть равен или подмножество, набор прав, вызывающей сборки.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если вызов с ограничениями элементов, не являющиеся открытыми, разрешен; в противном случае — <see langword="false" />.
    /// </returns>
    public bool RestrictedMemberAccess
    {
      get
      {
        return (uint) (this.m_flag & ReflectionPermissionFlag.RestrictedMemberAccess) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | ReflectionPermissionFlag.RestrictedMemberAccess : this.m_flag & ~ReflectionPermissionFlag.RestrictedMemberAccess;
      }
    }

    /// <summary>
    ///   Создает и возвращает новый объект <see cref="T:System.Security.Permissions.ReflectionPermission" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Permissions.ReflectionPermission" />, соответствующий этому атрибуту.
    /// </returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new ReflectionPermission(PermissionState.Unrestricted);
      return (IPermission) new ReflectionPermission(this.m_flag);
    }
  }
}

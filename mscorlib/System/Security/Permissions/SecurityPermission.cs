// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.SecurityPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Описывает набор разрешений безопасности, примененных к коду.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SecurityPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
  {
    private SecurityPermissionFlag m_flags;
    private const string _strHeaderAssertion = "Assertion";
    private const string _strHeaderUnmanagedCode = "UnmanagedCode";
    private const string _strHeaderExecution = "Execution";
    private const string _strHeaderSkipVerification = "SkipVerification";
    private const string _strHeaderControlThread = "ControlThread";
    private const string _strHeaderControlEvidence = "ControlEvidence";
    private const string _strHeaderControlPolicy = "ControlPolicy";
    private const string _strHeaderSerializationFormatter = "SerializationFormatter";
    private const string _strHeaderControlDomainPolicy = "ControlDomainPolicy";
    private const string _strHeaderControlPrincipal = "ControlPrincipal";
    private const string _strHeaderControlAppDomain = "ControlAppDomain";

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.SecurityPermission" /> с указанным состоянием разрешения: ограниченным или неограниченным.
    /// </summary>
    /// <param name="state">
    ///   Одно из значений <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="state" /> не является допустимым значением для <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </exception>
    public SecurityPermission(PermissionState state)
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
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.SecurityPermission" /> с заданным исходным состоянием флагов.
    /// </summary>
    /// <param name="flag">
    ///   Начальное состояние разрешения, представленное побитовым сочетанием всех битов разрешений, определенных <see cref="T:System.Security.Permissions.SecurityPermissionFlag" />, с помощью логической операции ИЛИ.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="flag" /> не является допустимым значением для <see cref="T:System.Security.Permissions.SecurityPermissionFlag" />.
    /// </exception>
    public SecurityPermission(SecurityPermissionFlag flag)
    {
      this.VerifyAccess(flag);
      this.SetUnrestricted(false);
      this.m_flags = flag;
    }

    private void SetUnrestricted(bool unrestricted)
    {
      if (!unrestricted)
        return;
      this.m_flags = SecurityPermissionFlag.AllFlags;
    }

    private void Reset()
    {
      this.m_flags = SecurityPermissionFlag.NoFlags;
    }

    /// <summary>Получает или задает флаги разрешений безопасности.</summary>
    /// <returns>
    ///   Состояние текущего разрешения, представленное побитовым сочетанием всех битов разрешений, определенных <see cref="T:System.Security.Permissions.SecurityPermissionFlag" />, с помощью логической операции ИЛИ.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Предпринята попытка задать этому свойству недопустимое значение.
    ///    Допустимые значения см. в разделе <see cref="T:System.Security.Permissions.SecurityPermissionFlag" />.
    /// </exception>
    public SecurityPermissionFlag Flags
    {
      set
      {
        this.VerifyAccess(value);
        this.m_flags = value;
      }
      get
      {
        return this.m_flags;
      }
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
        return this.m_flags == SecurityPermissionFlag.NoFlags;
      SecurityPermission securityPermission = target as SecurityPermission;
      if (securityPermission != null)
        return (this.m_flags & ~securityPermission.m_flags) == SecurityPermissionFlag.NoFlags;
      throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
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
      SecurityPermission securityPermission = (SecurityPermission) target;
      if (securityPermission.IsUnrestricted() || this.IsUnrestricted())
        return (IPermission) new SecurityPermission(PermissionState.Unrestricted);
      return (IPermission) new SecurityPermission(this.m_flags | securityPermission.m_flags);
    }

    /// <summary>
    ///   Создает и возвращает разрешение, представляющее собой пересечение текущего и указанного разрешений.
    /// </summary>
    /// <param name="target">
    ///   Разрешение, пересекающееся с текущим разрешением.
    ///    Его тип должен совпадать с типом текущего разрешения.
    /// </param>
    /// <returns>
    ///   Новый объект разрешения, представляющий собой пересечение текущего и указанного разрешений.
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
      SecurityPermission securityPermission = (SecurityPermission) target;
      SecurityPermissionFlag flag;
      if (securityPermission.IsUnrestricted())
      {
        if (this.IsUnrestricted())
          return (IPermission) new SecurityPermission(PermissionState.Unrestricted);
        flag = this.m_flags;
      }
      else
        flag = !this.IsUnrestricted() ? this.m_flags & securityPermission.m_flags : securityPermission.m_flags;
      if (flag == SecurityPermissionFlag.NoFlags)
        return (IPermission) null;
      return (IPermission) new SecurityPermission(flag);
    }

    /// <summary>
    ///   Создает и возвращает идентичную копию текущего разрешения.
    /// </summary>
    /// <returns>Копия текущего разрешения.</returns>
    public override IPermission Copy()
    {
      if (this.IsUnrestricted())
        return (IPermission) new SecurityPermission(PermissionState.Unrestricted);
      return (IPermission) new SecurityPermission(this.m_flags);
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущее разрешение неограниченным.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущее разрешение является неограниченным. В противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsUnrestricted()
    {
      return this.m_flags == SecurityPermissionFlag.AllFlags;
    }

    private void VerifyAccess(SecurityPermissionFlag type)
    {
      if ((type & ~SecurityPermissionFlag.AllFlags) != SecurityPermissionFlag.NoFlags)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) type));
    }

    /// <summary>
    ///   Создает кодировку XML для разрешения и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Кодировка XML разрешения, включающая любые сведения о состоянии.
    /// </returns>
    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.SecurityPermission");
      if (!this.IsUnrestricted())
        permissionElement.AddAttribute("Flags", XMLUtil.BitFieldEnumToString(typeof (SecurityPermissionFlag), (object) this.m_flags));
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
    ///   Номер версии параметра <paramref name="esd" /> не поддерживается.
    /// </exception>
    public override void FromXml(SecurityElement esd)
    {
      CodeAccessPermission.ValidateElement(esd, (IPermission) this);
      if (XMLUtil.IsUnrestricted(esd))
      {
        this.m_flags = SecurityPermissionFlag.AllFlags;
      }
      else
      {
        this.Reset();
        this.SetUnrestricted(false);
        string str = esd.Attribute("Flags");
        if (str == null)
          return;
        this.m_flags = (SecurityPermissionFlag) Enum.Parse(typeof (SecurityPermissionFlag), str);
      }
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return SecurityPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 6;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.ReflectionPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Управляет доступом к закрытым типам и членам через API <see cref="N:System.Reflection" />.
  ///    Управляет некоторыми функциями API <see cref="N:System.Reflection.Emit" />.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class ReflectionPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
  {
    internal const ReflectionPermissionFlag AllFlagsAndMore = ReflectionPermissionFlag.AllFlags | ReflectionPermissionFlag.RestrictedMemberAccess;
    private ReflectionPermissionFlag m_flags;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.ReflectionPermission" /> с указанным состоянием разрешения: полностью ограниченное или неограниченное.
    /// </summary>
    /// <param name="state">
    ///   Одно из значений <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="state" /> не является допустимым значением для <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </exception>
    public ReflectionPermission(PermissionState state)
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
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.ReflectionPermission" /> с заданным доступом.
    /// </summary>
    /// <param name="flag">
    ///   Одно из значений <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="flag" /> не является допустимым значением для <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" />.
    /// </exception>
    public ReflectionPermission(ReflectionPermissionFlag flag)
    {
      this.VerifyAccess(flag);
      this.SetUnrestricted(false);
      this.m_flags = flag;
    }

    private void SetUnrestricted(bool unrestricted)
    {
      if (unrestricted)
        this.m_flags = ReflectionPermissionFlag.AllFlags | ReflectionPermissionFlag.RestrictedMemberAccess;
      else
        this.Reset();
    }

    private void Reset()
    {
      this.m_flags = ReflectionPermissionFlag.NoFlags;
    }

    /// <summary>
    ///   Получает или задает тип отражения, допустимого для текущего разрешения.
    /// </summary>
    /// <returns>Флаги, заданные для текущего разрешения.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Предпринята попытка задать этому свойству недопустимое значение.
    ///    Допустимые значения см. в разделе <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" />.
    /// </exception>
    public ReflectionPermissionFlag Flags
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
    ///   Возвращает значение, указывающее, является ли текущее разрешение неограниченным.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущее разрешение является неограниченным. В противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsUnrestricted()
    {
      return this.m_flags == (ReflectionPermissionFlag.AllFlags | ReflectionPermissionFlag.RestrictedMemberAccess);
    }

    /// <summary>
    ///   Создает разрешение, представляющее собой объединение текущего и указанного разрешений.
    /// </summary>
    /// <param name="other">
    ///   Разрешение, которое требуется объединить с текущим разрешением.
    ///    Его тип должен совпадать с типом текущего разрешения.
    /// </param>
    /// <returns>
    ///   Новое разрешение, представляющее собой объединение текущего и указанного разрешений.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="other" /> не равен <see langword="null" /> и имеет тип, не совпадающий с типом текущего разрешения.
    /// </exception>
    public override IPermission Union(IPermission other)
    {
      if (other == null)
        return this.Copy();
      if (!this.VerifyType(other))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      ReflectionPermission reflectionPermission = (ReflectionPermission) other;
      if (this.IsUnrestricted() || reflectionPermission.IsUnrestricted())
        return (IPermission) new ReflectionPermission(PermissionState.Unrestricted);
      return (IPermission) new ReflectionPermission(this.m_flags | reflectionPermission.m_flags);
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
        return this.m_flags == ReflectionPermissionFlag.NoFlags;
      try
      {
        ReflectionPermission reflectionPermission = (ReflectionPermission) target;
        if (reflectionPermission.IsUnrestricted())
          return true;
        if (this.IsUnrestricted())
          return false;
        return (this.m_flags & ~reflectionPermission.m_flags) == ReflectionPermissionFlag.NoFlags;
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
      ReflectionPermissionFlag flag = ((ReflectionPermission) target).m_flags & this.m_flags;
      if (flag == ReflectionPermissionFlag.NoFlags)
        return (IPermission) null;
      return (IPermission) new ReflectionPermission(flag);
    }

    /// <summary>
    ///   Создает и возвращает идентичную копию текущего разрешения.
    /// </summary>
    /// <returns>Копия текущего разрешения.</returns>
    public override IPermission Copy()
    {
      if (this.IsUnrestricted())
        return (IPermission) new ReflectionPermission(PermissionState.Unrestricted);
      return (IPermission) new ReflectionPermission(this.m_flags);
    }

    private void VerifyAccess(ReflectionPermissionFlag type)
    {
      if ((type & ~(ReflectionPermissionFlag.AllFlags | ReflectionPermissionFlag.RestrictedMemberAccess)) != ReflectionPermissionFlag.NoFlags)
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
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.ReflectionPermission");
      if (!this.IsUnrestricted())
        permissionElement.AddAttribute("Flags", XMLUtil.BitFieldEnumToString(typeof (ReflectionPermissionFlag), (object) this.m_flags));
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
        this.m_flags = ReflectionPermissionFlag.AllFlags | ReflectionPermissionFlag.RestrictedMemberAccess;
      }
      else
      {
        this.Reset();
        this.SetUnrestricted(false);
        string str = esd.Attribute("Flags");
        if (str == null)
          return;
        this.m_flags = (ReflectionPermissionFlag) Enum.Parse(typeof (ReflectionPermissionFlag), str);
      }
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return ReflectionPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 4;
    }
  }
}

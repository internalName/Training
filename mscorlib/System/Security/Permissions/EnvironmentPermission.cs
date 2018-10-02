// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.EnvironmentPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Управляет доступом к системным и пользовательским переменным среды.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class EnvironmentPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
  {
    private StringExpressionSet m_read;
    private StringExpressionSet m_write;
    private bool m_unrestricted;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.EnvironmentPermission" /> с указанным состоянием разрешения: ограниченным или неограниченным.
    /// </summary>
    /// <param name="state">
    ///   Одно из значений <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="state" /> не является допустимым значением для <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </exception>
    public EnvironmentPermission(PermissionState state)
    {
      if (state == PermissionState.Unrestricted)
      {
        this.m_unrestricted = true;
      }
      else
      {
        if (state != PermissionState.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
        this.m_unrestricted = false;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.EnvironmentPermission" /> заданным доступом к указанным переменным среды.
    /// </summary>
    /// <param name="flag">
    ///   Одно из значений <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />.
    /// </param>
    /// <param name="pathList">
    ///   Список переменных среды (разделенных точкой с запятой), к которым предоставляется доступ.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="pathList" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="flag" /> не является допустимым значением для <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />.
    /// </exception>
    public EnvironmentPermission(EnvironmentPermissionAccess flag, string pathList)
    {
      this.SetPathList(flag, pathList);
    }

    /// <summary>
    ///   Задает указанный доступ для указанных переменных среды в существующее состояние разрешения.
    /// </summary>
    /// <param name="flag">
    ///   Одно из значений <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />.
    /// </param>
    /// <param name="pathList">
    ///   Список переменных среды (разделенных точкой с запятой).
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="pathList" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="flag" /> не является допустимым значением для <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />.
    /// </exception>
    public void SetPathList(EnvironmentPermissionAccess flag, string pathList)
    {
      this.VerifyFlag(flag);
      this.m_unrestricted = false;
      if ((flag & EnvironmentPermissionAccess.Read) != EnvironmentPermissionAccess.NoAccess)
        this.m_read = (StringExpressionSet) null;
      if ((flag & EnvironmentPermissionAccess.Write) != EnvironmentPermissionAccess.NoAccess)
        this.m_write = (StringExpressionSet) null;
      this.AddPathList(flag, pathList);
    }

    /// <summary>
    ///   Добавляет доступ для заданных переменных среды в существующее состояние разрешения.
    /// </summary>
    /// <param name="flag">
    ///   Одно из значений <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />.
    /// </param>
    /// <param name="pathList">
    ///   Список переменных среды (разделенных точкой с запятой).
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="pathList" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="flag" /> не является допустимым значением для <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />.
    /// </exception>
    [SecuritySafeCritical]
    public void AddPathList(EnvironmentPermissionAccess flag, string pathList)
    {
      this.VerifyFlag(flag);
      if (this.FlagIsSet(flag, EnvironmentPermissionAccess.Read))
      {
        if (this.m_read == null)
          this.m_read = (StringExpressionSet) new EnvironmentStringExpressionSet();
        this.m_read.AddExpressions(pathList);
      }
      if (!this.FlagIsSet(flag, EnvironmentPermissionAccess.Write))
        return;
      if (this.m_write == null)
        this.m_write = (StringExpressionSet) new EnvironmentStringExpressionSet();
      this.m_write.AddExpressions(pathList);
    }

    /// <summary>
    ///   Получает все переменные среды с указанным <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />.
    /// </summary>
    /// <param name="flag">
    ///   Одно из значений <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />, представляющее один тип доступа к переменным среды.
    /// </param>
    /// <returns>
    ///   Список переменных среды (разделенных точкой с запятой) для выбранного флага.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="flag" /> не является допустимым значением <see cref="T:System.Security.Permissions.EnvironmentPermissionAccess" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="flag" /> имеет значение <see cref="F:System.Security.Permissions.EnvironmentPermissionAccess.AllAccess" />, которое представляет несколько типов доступа к переменным среды, или <see cref="F:System.Security.Permissions.EnvironmentPermissionAccess.NoAccess" />, которое не представляет ни одного типа доступа к переменным среды.
    /// </exception>
    public string GetPathList(EnvironmentPermissionAccess flag)
    {
      this.VerifyFlag(flag);
      this.ExclusiveFlag(flag);
      if (this.FlagIsSet(flag, EnvironmentPermissionAccess.Read))
      {
        if (this.m_read == null)
          return "";
        return this.m_read.ToString();
      }
      if (this.FlagIsSet(flag, EnvironmentPermissionAccess.Write) && this.m_write != null)
        return this.m_write.ToString();
      return "";
    }

    private void VerifyFlag(EnvironmentPermissionAccess flag)
    {
      if ((flag & ~EnvironmentPermissionAccess.AllAccess) != EnvironmentPermissionAccess.NoAccess)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) flag));
    }

    private void ExclusiveFlag(EnvironmentPermissionAccess flag)
    {
      if (flag == EnvironmentPermissionAccess.NoAccess)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
      if ((flag & flag - 1) != EnvironmentPermissionAccess.NoAccess)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
    }

    private bool FlagIsSet(EnvironmentPermissionAccess flag, EnvironmentPermissionAccess question)
    {
      return (uint) (flag & question) > 0U;
    }

    private bool IsEmpty()
    {
      if (this.m_unrestricted || this.m_read != null && !this.m_read.IsEmpty())
        return false;
      if (this.m_write != null)
        return this.m_write.IsEmpty();
      return true;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущее разрешение неограниченным.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущее разрешение является неограниченным. В противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsUnrestricted()
    {
      return this.m_unrestricted;
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
    [SecuritySafeCritical]
    public override bool IsSubsetOf(IPermission target)
    {
      if (target == null)
        return this.IsEmpty();
      try
      {
        EnvironmentPermission environmentPermission = (EnvironmentPermission) target;
        if (environmentPermission.IsUnrestricted())
          return true;
        if (this.IsUnrestricted())
          return false;
        return (this.m_read == null || this.m_read.IsSubsetOf(environmentPermission.m_read)) && (this.m_write == null || this.m_write.IsSubsetOf(environmentPermission.m_write));
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
    [SecuritySafeCritical]
    public override IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      if (!this.VerifyType(target))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.IsUnrestricted())
        return target.Copy();
      EnvironmentPermission environmentPermission = (EnvironmentPermission) target;
      if (environmentPermission.IsUnrestricted())
        return this.Copy();
      StringExpressionSet stringExpressionSet1 = this.m_read == null ? (StringExpressionSet) null : this.m_read.Intersect(environmentPermission.m_read);
      StringExpressionSet stringExpressionSet2 = this.m_write == null ? (StringExpressionSet) null : this.m_write.Intersect(environmentPermission.m_write);
      if ((stringExpressionSet1 == null || stringExpressionSet1.IsEmpty()) && (stringExpressionSet2 == null || stringExpressionSet2.IsEmpty()))
        return (IPermission) null;
      return (IPermission) new EnvironmentPermission(PermissionState.None)
      {
        m_unrestricted = false,
        m_read = stringExpressionSet1,
        m_write = stringExpressionSet2
      };
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
    [SecuritySafeCritical]
    public override IPermission Union(IPermission other)
    {
      if (other == null)
        return this.Copy();
      if (!this.VerifyType(other))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      EnvironmentPermission environmentPermission = (EnvironmentPermission) other;
      if (this.IsUnrestricted() || environmentPermission.IsUnrestricted())
        return (IPermission) new EnvironmentPermission(PermissionState.Unrestricted);
      StringExpressionSet stringExpressionSet1 = this.m_read == null ? environmentPermission.m_read : this.m_read.Union(environmentPermission.m_read);
      StringExpressionSet stringExpressionSet2 = this.m_write == null ? environmentPermission.m_write : this.m_write.Union(environmentPermission.m_write);
      if ((stringExpressionSet1 == null || stringExpressionSet1.IsEmpty()) && (stringExpressionSet2 == null || stringExpressionSet2.IsEmpty()))
        return (IPermission) null;
      return (IPermission) new EnvironmentPermission(PermissionState.None)
      {
        m_unrestricted = false,
        m_read = stringExpressionSet1,
        m_write = stringExpressionSet2
      };
    }

    /// <summary>
    ///   Создает и возвращает идентичную копию текущего разрешения.
    /// </summary>
    /// <returns>Копия текущего разрешения.</returns>
    public override IPermission Copy()
    {
      EnvironmentPermission environmentPermission = new EnvironmentPermission(PermissionState.None);
      if (this.m_unrestricted)
      {
        environmentPermission.m_unrestricted = true;
      }
      else
      {
        environmentPermission.m_unrestricted = false;
        if (this.m_read != null)
          environmentPermission.m_read = this.m_read.Copy();
        if (this.m_write != null)
          environmentPermission.m_write = this.m_write.Copy();
      }
      return (IPermission) environmentPermission;
    }

    /// <summary>
    ///   Создает кодировку XML для разрешения и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Кодировка XML разрешения, включающая любые сведения о состоянии.
    /// </returns>
    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.EnvironmentPermission");
      if (!this.IsUnrestricted())
      {
        if (this.m_read != null && !this.m_read.IsEmpty())
          permissionElement.AddAttribute("Read", SecurityElement.Escape(this.m_read.ToString()));
        if (this.m_write != null && !this.m_write.IsEmpty())
          permissionElement.AddAttribute("Write", SecurityElement.Escape(this.m_write.ToString()));
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
        this.m_unrestricted = true;
      }
      else
      {
        this.m_unrestricted = false;
        this.m_read = (StringExpressionSet) null;
        this.m_write = (StringExpressionSet) null;
        string str1 = esd.Attribute("Read");
        if (str1 != null)
          this.m_read = (StringExpressionSet) new EnvironmentStringExpressionSet(str1);
        string str2 = esd.Attribute("Write");
        if (str2 == null)
          return;
        this.m_write = (StringExpressionSet) new EnvironmentStringExpressionSet(str2);
      }
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return EnvironmentPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 0;
    }
  }
}

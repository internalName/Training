// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.RegistryPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.AccessControl;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Управляет возможностью доступа к переменным реестра.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class RegistryPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
  {
    private StringExpressionSet m_read;
    private StringExpressionSet m_write;
    private StringExpressionSet m_create;
    [OptionalField(VersionAdded = 2)]
    private StringExpressionSet m_viewAcl;
    [OptionalField(VersionAdded = 2)]
    private StringExpressionSet m_changeAcl;
    private bool m_unrestricted;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.RegistryPermission" /> с указанным состоянием разрешения: полностью ограниченное или неограниченное.
    /// </summary>
    /// <param name="state">
    ///   Одно из значений <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="state" /> не является допустимым значением для <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </exception>
    public RegistryPermission(PermissionState state)
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
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.RegistryPermission" /> с заданным доступом к указанным переменным реестра.
    /// </summary>
    /// <param name="access">
    ///   Одно из значений <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.
    /// </param>
    /// <param name="pathList">
    ///   Список переменных реестра (разделенных точкой с запятой), к которым предоставляется доступ.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="access" /> не является допустимым значением для <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="pathList" /> не является допустимой строкой.
    /// </exception>
    public RegistryPermission(RegistryPermissionAccess access, string pathList)
    {
      this.SetPathList(access, pathList);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.RegistryPermission" /> с указанным доступом к указанным переменным реестра и указанными правами доступа к сведениям об элементе управления реестра.
    /// </summary>
    /// <param name="access">
    ///   Одно из значений <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.
    /// </param>
    /// <param name="control">
    ///   Побитовое сочетание значений <see cref="T:System.Security.AccessControl.AccessControlActions" />.
    /// </param>
    /// <param name="pathList">
    ///   Список переменных реестра (разделенных точкой с запятой), к которым предоставляется доступ.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="access" /> не является допустимым значением для <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="pathList" /> не является допустимой строкой.
    /// </exception>
    public RegistryPermission(RegistryPermissionAccess access, AccessControlActions control, string pathList)
    {
      this.m_unrestricted = false;
      this.AddPathList(access, control, pathList);
    }

    /// <summary>
    ///   Задает новый доступ для указанных имен переменных реестра в существующее состояние разрешения.
    /// </summary>
    /// <param name="access">
    ///   Одно из значений <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.
    /// </param>
    /// <param name="pathList">
    ///   Список переменных реестра (разделенных точкой с запятой).
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="access" /> не является допустимым значением для <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="pathList" /> не является допустимой строкой.
    /// </exception>
    public void SetPathList(RegistryPermissionAccess access, string pathList)
    {
      this.VerifyAccess(access);
      this.m_unrestricted = false;
      if ((access & RegistryPermissionAccess.Read) != RegistryPermissionAccess.NoAccess)
        this.m_read = (StringExpressionSet) null;
      if ((access & RegistryPermissionAccess.Write) != RegistryPermissionAccess.NoAccess)
        this.m_write = (StringExpressionSet) null;
      if ((access & RegistryPermissionAccess.Create) != RegistryPermissionAccess.NoAccess)
        this.m_create = (StringExpressionSet) null;
      this.AddPathList(access, pathList);
    }

    internal void SetPathList(AccessControlActions control, string pathList)
    {
      this.m_unrestricted = false;
      if ((control & AccessControlActions.View) != AccessControlActions.None)
        this.m_viewAcl = (StringExpressionSet) null;
      if ((control & AccessControlActions.Change) != AccessControlActions.None)
        this.m_changeAcl = (StringExpressionSet) null;
      this.AddPathList(RegistryPermissionAccess.NoAccess, control, pathList);
    }

    /// <summary>
    ///   Добавляет доступ для заданных переменных реестра в существующее состояние разрешения.
    /// </summary>
    /// <param name="access">
    ///   Одно из значений <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.
    /// </param>
    /// <param name="pathList">
    ///   Список переменных реестра (разделенных точкой с запятой).
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="access" /> не является допустимым значением для <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="pathList" /> не является допустимой строкой.
    /// </exception>
    public void AddPathList(RegistryPermissionAccess access, string pathList)
    {
      this.AddPathList(access, AccessControlActions.None, pathList);
    }

    /// <summary>
    ///   Добавляет доступ для указанных переменных реестра в существующее состояние разрешения, задавая уровень доступа разрешения реестра и действия по управлению доступом.
    /// </summary>
    /// <param name="access">
    ///   Одно из значений <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.
    /// </param>
    /// <param name="control">
    ///   Одно из значений <see cref="T:System.Security.AccessControl.AccessControlActions" />.
    /// </param>
    /// <param name="pathList">
    ///   Список переменных реестра (разделенных точкой с запятой).
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="access" /> не является допустимым значением для <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="pathList" /> не является допустимой строкой.
    /// </exception>
    [SecuritySafeCritical]
    public void AddPathList(RegistryPermissionAccess access, AccessControlActions control, string pathList)
    {
      this.VerifyAccess(access);
      if ((access & RegistryPermissionAccess.Read) != RegistryPermissionAccess.NoAccess)
      {
        if (this.m_read == null)
          this.m_read = new StringExpressionSet();
        this.m_read.AddExpressions(pathList);
      }
      if ((access & RegistryPermissionAccess.Write) != RegistryPermissionAccess.NoAccess)
      {
        if (this.m_write == null)
          this.m_write = new StringExpressionSet();
        this.m_write.AddExpressions(pathList);
      }
      if ((access & RegistryPermissionAccess.Create) != RegistryPermissionAccess.NoAccess)
      {
        if (this.m_create == null)
          this.m_create = new StringExpressionSet();
        this.m_create.AddExpressions(pathList);
      }
      if ((control & AccessControlActions.View) != AccessControlActions.None)
      {
        if (this.m_viewAcl == null)
          this.m_viewAcl = new StringExpressionSet();
        this.m_viewAcl.AddExpressions(pathList);
      }
      if ((control & AccessControlActions.Change) == AccessControlActions.None)
        return;
      if (this.m_changeAcl == null)
        this.m_changeAcl = new StringExpressionSet();
      this.m_changeAcl.AddExpressions(pathList);
    }

    /// <summary>
    ///   Получает пути для всех переменных реестра с заданным <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.
    /// </summary>
    /// <param name="access">
    ///   Одно из значений <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />, представляющее один тип доступа к переменным среды.
    /// </param>
    /// <returns>
    ///   Список переменных реестра (разделенных точкой с запятой) с указанным <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="access" /> не является допустимым значением <see cref="T:System.Security.Permissions.RegistryPermissionAccess" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="access" /> имеет значение <see cref="F:System.Security.Permissions.RegistryPermissionAccess.AllAccess" />, которое представляет несколько типов доступа к переменным реестра, или <see cref="F:System.Security.Permissions.RegistryPermissionAccess.NoAccess" />, которое не представляет ни один тип доступа к переменным реестра.
    /// </exception>
    [SecuritySafeCritical]
    public string GetPathList(RegistryPermissionAccess access)
    {
      this.VerifyAccess(access);
      this.ExclusiveAccess(access);
      if ((access & RegistryPermissionAccess.Read) != RegistryPermissionAccess.NoAccess)
      {
        if (this.m_read == null)
          return "";
        return this.m_read.UnsafeToString();
      }
      if ((access & RegistryPermissionAccess.Write) != RegistryPermissionAccess.NoAccess)
      {
        if (this.m_write == null)
          return "";
        return this.m_write.UnsafeToString();
      }
      if ((access & RegistryPermissionAccess.Create) != RegistryPermissionAccess.NoAccess && this.m_create != null)
        return this.m_create.UnsafeToString();
      return "";
    }

    private void VerifyAccess(RegistryPermissionAccess access)
    {
      if ((access & ~RegistryPermissionAccess.AllAccess) != RegistryPermissionAccess.NoAccess)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", (object) access));
    }

    private void ExclusiveAccess(RegistryPermissionAccess access)
    {
      if (access == RegistryPermissionAccess.NoAccess)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
      if ((access & access - 1) != RegistryPermissionAccess.NoAccess)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
    }

    private bool IsEmpty()
    {
      if (this.m_unrestricted || this.m_read != null && !this.m_read.IsEmpty() || (this.m_write != null && !this.m_write.IsEmpty() || this.m_create != null && !this.m_create.IsEmpty()) || this.m_viewAcl != null && !this.m_viewAcl.IsEmpty())
        return false;
      if (this.m_changeAcl != null)
        return this.m_changeAcl.IsEmpty();
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
      RegistryPermission registryPermission = target as RegistryPermission;
      if (registryPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (registryPermission.IsUnrestricted())
        return true;
      if (this.IsUnrestricted() || this.m_read != null && !this.m_read.IsSubsetOf(registryPermission.m_read) || (this.m_write != null && !this.m_write.IsSubsetOf(registryPermission.m_write) || this.m_create != null && !this.m_create.IsSubsetOf(registryPermission.m_create)) || this.m_viewAcl != null && !this.m_viewAcl.IsSubsetOf(registryPermission.m_viewAcl))
        return false;
      if (this.m_changeAcl != null)
        return this.m_changeAcl.IsSubsetOf(registryPermission.m_changeAcl);
      return true;
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
      RegistryPermission registryPermission = (RegistryPermission) target;
      if (registryPermission.IsUnrestricted())
        return this.Copy();
      StringExpressionSet stringExpressionSet1 = this.m_read == null ? (StringExpressionSet) null : this.m_read.Intersect(registryPermission.m_read);
      StringExpressionSet stringExpressionSet2 = this.m_write == null ? (StringExpressionSet) null : this.m_write.Intersect(registryPermission.m_write);
      StringExpressionSet stringExpressionSet3 = this.m_create == null ? (StringExpressionSet) null : this.m_create.Intersect(registryPermission.m_create);
      StringExpressionSet stringExpressionSet4 = this.m_viewAcl == null ? (StringExpressionSet) null : this.m_viewAcl.Intersect(registryPermission.m_viewAcl);
      StringExpressionSet stringExpressionSet5 = this.m_changeAcl == null ? (StringExpressionSet) null : this.m_changeAcl.Intersect(registryPermission.m_changeAcl);
      if ((stringExpressionSet1 == null || stringExpressionSet1.IsEmpty()) && (stringExpressionSet2 == null || stringExpressionSet2.IsEmpty()) && ((stringExpressionSet3 == null || stringExpressionSet3.IsEmpty()) && (stringExpressionSet4 == null || stringExpressionSet4.IsEmpty())) && (stringExpressionSet5 == null || stringExpressionSet5.IsEmpty()))
        return (IPermission) null;
      return (IPermission) new RegistryPermission(PermissionState.None)
      {
        m_unrestricted = false,
        m_read = stringExpressionSet1,
        m_write = stringExpressionSet2,
        m_create = stringExpressionSet3,
        m_viewAcl = stringExpressionSet4,
        m_changeAcl = stringExpressionSet5
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
      RegistryPermission registryPermission = (RegistryPermission) other;
      if (this.IsUnrestricted() || registryPermission.IsUnrestricted())
        return (IPermission) new RegistryPermission(PermissionState.Unrestricted);
      StringExpressionSet stringExpressionSet1 = this.m_read == null ? registryPermission.m_read : this.m_read.Union(registryPermission.m_read);
      StringExpressionSet stringExpressionSet2 = this.m_write == null ? registryPermission.m_write : this.m_write.Union(registryPermission.m_write);
      StringExpressionSet stringExpressionSet3 = this.m_create == null ? registryPermission.m_create : this.m_create.Union(registryPermission.m_create);
      StringExpressionSet stringExpressionSet4 = this.m_viewAcl == null ? registryPermission.m_viewAcl : this.m_viewAcl.Union(registryPermission.m_viewAcl);
      StringExpressionSet stringExpressionSet5 = this.m_changeAcl == null ? registryPermission.m_changeAcl : this.m_changeAcl.Union(registryPermission.m_changeAcl);
      if ((stringExpressionSet1 == null || stringExpressionSet1.IsEmpty()) && (stringExpressionSet2 == null || stringExpressionSet2.IsEmpty()) && ((stringExpressionSet3 == null || stringExpressionSet3.IsEmpty()) && (stringExpressionSet4 == null || stringExpressionSet4.IsEmpty())) && (stringExpressionSet5 == null || stringExpressionSet5.IsEmpty()))
        return (IPermission) null;
      return (IPermission) new RegistryPermission(PermissionState.None)
      {
        m_unrestricted = false,
        m_read = stringExpressionSet1,
        m_write = stringExpressionSet2,
        m_create = stringExpressionSet3,
        m_viewAcl = stringExpressionSet4,
        m_changeAcl = stringExpressionSet5
      };
    }

    /// <summary>
    ///   Создает и возвращает идентичную копию текущего разрешения.
    /// </summary>
    /// <returns>Копия текущего разрешения.</returns>
    public override IPermission Copy()
    {
      RegistryPermission registryPermission = new RegistryPermission(PermissionState.None);
      if (this.m_unrestricted)
      {
        registryPermission.m_unrestricted = true;
      }
      else
      {
        registryPermission.m_unrestricted = false;
        if (this.m_read != null)
          registryPermission.m_read = this.m_read.Copy();
        if (this.m_write != null)
          registryPermission.m_write = this.m_write.Copy();
        if (this.m_create != null)
          registryPermission.m_create = this.m_create.Copy();
        if (this.m_viewAcl != null)
          registryPermission.m_viewAcl = this.m_viewAcl.Copy();
        if (this.m_changeAcl != null)
          registryPermission.m_changeAcl = this.m_changeAcl.Copy();
      }
      return (IPermission) registryPermission;
    }

    /// <summary>
    ///   Создает кодировку XML для разрешения и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Кодировка XML разрешения, включающая любые сведения о состоянии.
    /// </returns>
    [SecuritySafeCritical]
    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.RegistryPermission");
      if (!this.IsUnrestricted())
      {
        if (this.m_read != null && !this.m_read.IsEmpty())
          permissionElement.AddAttribute("Read", SecurityElement.Escape(this.m_read.UnsafeToString()));
        if (this.m_write != null && !this.m_write.IsEmpty())
          permissionElement.AddAttribute("Write", SecurityElement.Escape(this.m_write.UnsafeToString()));
        if (this.m_create != null && !this.m_create.IsEmpty())
          permissionElement.AddAttribute("Create", SecurityElement.Escape(this.m_create.UnsafeToString()));
        if (this.m_viewAcl != null && !this.m_viewAcl.IsEmpty())
          permissionElement.AddAttribute("ViewAccessControl", SecurityElement.Escape(this.m_viewAcl.UnsafeToString()));
        if (this.m_changeAcl != null && !this.m_changeAcl.IsEmpty())
          permissionElement.AddAttribute("ChangeAccessControl", SecurityElement.Escape(this.m_changeAcl.UnsafeToString()));
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
        this.m_create = (StringExpressionSet) null;
        this.m_viewAcl = (StringExpressionSet) null;
        this.m_changeAcl = (StringExpressionSet) null;
        string str1 = esd.Attribute("Read");
        if (str1 != null)
          this.m_read = new StringExpressionSet(str1);
        string str2 = esd.Attribute("Write");
        if (str2 != null)
          this.m_write = new StringExpressionSet(str2);
        string str3 = esd.Attribute("Create");
        if (str3 != null)
          this.m_create = new StringExpressionSet(str3);
        string str4 = esd.Attribute("ViewAccessControl");
        if (str4 != null)
          this.m_viewAcl = new StringExpressionSet(str4);
        string str5 = esd.Attribute("ChangeAccessControl");
        if (str5 == null)
          return;
        this.m_changeAcl = new StringExpressionSet(str5);
      }
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return RegistryPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 5;
    }
  }
}

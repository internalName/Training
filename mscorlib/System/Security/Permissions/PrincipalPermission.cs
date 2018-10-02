// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.PrincipalPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Security.Principal;
using System.Security.Util;
using System.Threading;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Позволяет выполнять проверки по активному субъекту (см. <see cref="T:System.Security.Principal.IPrincipal" />) с помощью конструкций языка, определенных для декларативных и императивных действий по обеспечению безопасности.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class PrincipalPermission : IPermission, ISecurityEncodable, IUnrestrictedPermission, IBuiltInPermission
  {
    private IDRole[] m_array;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.PrincipalPermission" /> указанным значением <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </summary>
    /// <param name="state">
    ///   Одно из значений <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="state" /> Параметр не является допустимым <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </exception>
    public PrincipalPermission(PermissionState state)
    {
      if (state == PermissionState.Unrestricted)
      {
        this.m_array = new IDRole[1];
        this.m_array[0] = new IDRole();
        this.m_array[0].m_authenticated = true;
        this.m_array[0].m_id = (string) null;
        this.m_array[0].m_role = (string) null;
      }
      else
      {
        if (state != PermissionState.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
        this.m_array = new IDRole[1];
        this.m_array[0] = new IDRole();
        this.m_array[0].m_authenticated = false;
        this.m_array[0].m_id = "";
        this.m_array[0].m_role = "";
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.PrincipalPermission" /> для указанных объектов <paramref name="name" /> и <paramref name="role" />.
    /// </summary>
    /// <param name="name">
    ///   Имя пользователя объекта <see cref="T:System.Security.Principal.IPrincipal" />.
    /// </param>
    /// <param name="role">
    ///   Роль пользователя объекта <see cref="T:System.Security.Principal.IPrincipal" /> (например, администратор).
    /// </param>
    public PrincipalPermission(string name, string role)
    {
      this.m_array = new IDRole[1];
      this.m_array[0] = new IDRole();
      this.m_array[0].m_authenticated = true;
      this.m_array[0].m_id = name;
      this.m_array[0].m_role = role;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.PrincipalPermission" /> для указанных объектов <paramref name="name" />, <paramref name="role" /> и состояния проверки подлинности.
    /// </summary>
    /// <param name="name">
    ///   Имя пользователя объекта <see cref="T:System.Security.Principal.IPrincipal" />.
    /// </param>
    /// <param name="role">
    ///   Роль пользователя объекта <see cref="T:System.Security.Principal.IPrincipal" /> (например, администратор).
    /// </param>
    /// <param name="isAuthenticated">
    ///   Значение <see langword="true" /> — пользователь прошел проверку подлинности; в противном случае — <see langword="false" />.
    /// </param>
    public PrincipalPermission(string name, string role, bool isAuthenticated)
    {
      this.m_array = new IDRole[1];
      this.m_array[0] = new IDRole();
      this.m_array[0].m_authenticated = isAuthenticated;
      this.m_array[0].m_id = name;
      this.m_array[0].m_role = role;
    }

    private PrincipalPermission(IDRole[] array)
    {
      this.m_array = array;
    }

    private bool IsEmpty()
    {
      for (int index = 0; index < this.m_array.Length; ++index)
      {
        if (this.m_array[index].m_id == null || !this.m_array[index].m_id.Equals("") || (this.m_array[index].m_role == null || !this.m_array[index].m_role.Equals("")) || this.m_array[index].m_authenticated)
          return false;
      }
      return true;
    }

    private bool VerifyType(IPermission perm)
    {
      return perm != null && !(perm.GetType() != this.GetType());
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущее разрешение неограниченным.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущее разрешение является неограниченным. В противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsUnrestricted()
    {
      for (int index = 0; index < this.m_array.Length; ++index)
      {
        if (this.m_array[index].m_id != null || this.m_array[index].m_role != null || !this.m_array[index].m_authenticated)
          return false;
      }
      return true;
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
    ///   Параметр <paramref name="target" /> является объектом, тип которого не совпадает с типом текущего разрешения.
    /// </exception>
    public bool IsSubsetOf(IPermission target)
    {
      if (target == null)
        return this.IsEmpty();
      try
      {
        PrincipalPermission principalPermission = (PrincipalPermission) target;
        if (principalPermission.IsUnrestricted())
          return true;
        if (this.IsUnrestricted())
          return false;
        for (int index1 = 0; index1 < this.m_array.Length; ++index1)
        {
          bool flag = false;
          for (int index2 = 0; index2 < principalPermission.m_array.Length; ++index2)
          {
            if (principalPermission.m_array[index2].m_authenticated == this.m_array[index1].m_authenticated && (principalPermission.m_array[index2].m_id == null || this.m_array[index1].m_id != null && this.m_array[index1].m_id.Equals(principalPermission.m_array[index2].m_id)) && (principalPermission.m_array[index2].m_role == null || this.m_array[index1].m_role != null && this.m_array[index1].m_role.Equals(principalPermission.m_array[index2].m_role)))
            {
              flag = true;
              break;
            }
          }
          if (!flag)
            return false;
        }
        return true;
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
    ///    Это новое разрешение будет иметь значение <see langword="null" />, если пересечение является пустым.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="target" /> не равен <see langword="null" /> и не является экземпляром того же класса, что и текущее разрешение.
    /// </exception>
    public IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      if (!this.VerifyType(target))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.IsUnrestricted())
        return target.Copy();
      PrincipalPermission principalPermission = (PrincipalPermission) target;
      if (principalPermission.IsUnrestricted())
        return this.Copy();
      List<IDRole> idRoleList = (List<IDRole>) null;
      for (int index1 = 0; index1 < this.m_array.Length; ++index1)
      {
        for (int index2 = 0; index2 < principalPermission.m_array.Length; ++index2)
        {
          if (principalPermission.m_array[index2].m_authenticated == this.m_array[index1].m_authenticated)
          {
            if (principalPermission.m_array[index2].m_id == null || this.m_array[index1].m_id == null || this.m_array[index1].m_id.Equals(principalPermission.m_array[index2].m_id))
            {
              if (idRoleList == null)
                idRoleList = new List<IDRole>();
              idRoleList.Add(new IDRole()
              {
                m_id = principalPermission.m_array[index2].m_id == null ? this.m_array[index1].m_id : principalPermission.m_array[index2].m_id,
                m_role = principalPermission.m_array[index2].m_role == null || this.m_array[index1].m_role == null || this.m_array[index1].m_role.Equals(principalPermission.m_array[index2].m_role) ? (principalPermission.m_array[index2].m_role == null ? this.m_array[index1].m_role : principalPermission.m_array[index2].m_role) : "",
                m_authenticated = principalPermission.m_array[index2].m_authenticated
              });
            }
            else if (principalPermission.m_array[index2].m_role == null || this.m_array[index1].m_role == null || this.m_array[index1].m_role.Equals(principalPermission.m_array[index2].m_role))
            {
              if (idRoleList == null)
                idRoleList = new List<IDRole>();
              idRoleList.Add(new IDRole()
              {
                m_id = "",
                m_role = principalPermission.m_array[index2].m_role == null ? this.m_array[index1].m_role : principalPermission.m_array[index2].m_role,
                m_authenticated = principalPermission.m_array[index2].m_authenticated
              });
            }
          }
        }
      }
      if (idRoleList == null)
        return (IPermission) null;
      IDRole[] array = new IDRole[idRoleList.Count];
      IEnumerator enumerator = (IEnumerator) idRoleList.GetEnumerator();
      int num = 0;
      while (enumerator.MoveNext())
        array[num++] = (IDRole) enumerator.Current;
      return (IPermission) new PrincipalPermission(array);
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
    ///   Параметр <paramref name="other" /> является объектом, тип которого не совпадает с типом текущего разрешения.
    /// </exception>
    public IPermission Union(IPermission other)
    {
      if (other == null)
        return this.Copy();
      if (!this.VerifyType(other))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      PrincipalPermission principalPermission = (PrincipalPermission) other;
      if (this.IsUnrestricted() || principalPermission.IsUnrestricted())
        return (IPermission) new PrincipalPermission(PermissionState.Unrestricted);
      IDRole[] array = new IDRole[this.m_array.Length + principalPermission.m_array.Length];
      int index1;
      for (index1 = 0; index1 < this.m_array.Length; ++index1)
        array[index1] = this.m_array[index1];
      for (int index2 = 0; index2 < principalPermission.m_array.Length; ++index2)
        array[index1 + index2] = principalPermission.m_array[index2];
      return (IPermission) new PrincipalPermission(array);
    }

    /// <summary>
    ///   Определяет, равен ли заданный объект <see cref="T:System.Security.Permissions.PrincipalPermission" /> текущему объекту <see cref="T:System.Security.Permissions.PrincipalPermission" />.
    /// </summary>
    /// <param name="obj">
    ///   Объект <see cref="T:System.Security.Permissions.PrincipalPermission" />, который требуется сравнить с текущим объектом <see cref="T:System.Security.Permissions.PrincipalPermission" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный объект <see cref="T:System.Security.Permissions.PrincipalPermission" /> равен текущему объекту <see cref="T:System.Security.Permissions.PrincipalPermission" />; в противном случае — <see langword="false" />.
    /// </returns>
    [ComVisible(false)]
    public override bool Equals(object obj)
    {
      IPermission target = obj as IPermission;
      return (obj == null || target != null) && this.IsSubsetOf(target) && (target == null || target.IsSubsetOf((IPermission) this));
    }

    /// <summary>
    ///   Возвращает хэш-код для объекта <see cref="T:System.Security.Permissions.PrincipalPermission" />, который можно использовать в алгоритмах хэширования и структурах данных, например в хэш-таблице.
    /// </summary>
    /// <returns>
    ///   Хэш-код для текущего объекта <see cref="T:System.Security.Permissions.PrincipalPermission" />.
    /// </returns>
    [ComVisible(false)]
    public override int GetHashCode()
    {
      int num = 0;
      for (int index = 0; index < this.m_array.Length; ++index)
        num += this.m_array[index].GetHashCode();
      return num;
    }

    /// <summary>
    ///   Создает и возвращает идентичную копию текущего разрешения.
    /// </summary>
    /// <returns>Копия текущего разрешения.</returns>
    public IPermission Copy()
    {
      return (IPermission) new PrincipalPermission(this.m_array);
    }

    [SecurityCritical]
    private void ThrowSecurityException()
    {
      AssemblyName assemblyName = (AssemblyName) null;
      Evidence evidence = (Evidence) null;
      PermissionSet.s_fullTrust.Assert();
      try
      {
        Assembly callingAssembly = Assembly.GetCallingAssembly();
        assemblyName = callingAssembly.GetName();
        if (callingAssembly != Assembly.GetExecutingAssembly())
          evidence = callingAssembly.Evidence;
      }
      catch
      {
      }
      PermissionSet.RevertAssert();
      throw new SecurityException(Environment.GetResourceString("Security_PrincipalPermission"), assemblyName, (PermissionSet) null, (PermissionSet) null, (MethodInfo) null, SecurityAction.Demand, (object) this, (IPermission) this, evidence);
    }

    /// <summary>
    ///   Определяет во время выполнения, соответствует ли текущий субъект субъекту, указанному текущим разрешением.
    /// </summary>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Текущий субъект не прошел проверку безопасности для участника, указанного текущим разрешением.
    /// 
    ///   -или-
    /// 
    ///   Текущий <see cref="T:System.Security.Principal.IPrincipal" /> — <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public void Demand()
    {
      new SecurityPermission(SecurityPermissionFlag.ControlPrincipal).Assert();
      IPrincipal currentPrincipal = Thread.CurrentPrincipal;
      if (currentPrincipal == null)
        this.ThrowSecurityException();
      if (this.m_array == null)
        return;
      int length = this.m_array.Length;
      bool flag = false;
      for (int index = 0; index < length; ++index)
      {
        if (this.m_array[index].m_authenticated)
        {
          IIdentity identity = currentPrincipal.Identity;
          if (identity.IsAuthenticated && (this.m_array[index].m_id == null || string.Compare(identity.Name, this.m_array[index].m_id, StringComparison.OrdinalIgnoreCase) == 0))
          {
            if (this.m_array[index].m_role == null)
            {
              flag = true;
            }
            else
            {
              WindowsPrincipal windowsPrincipal = currentPrincipal as WindowsPrincipal;
              flag = windowsPrincipal == null || !(this.m_array[index].Sid != (SecurityIdentifier) null) ? currentPrincipal.IsInRole(this.m_array[index].m_role) : windowsPrincipal.IsInRole(this.m_array[index].Sid);
            }
            if (flag)
              break;
          }
        }
        else
        {
          flag = true;
          break;
        }
      }
      if (flag)
        return;
      this.ThrowSecurityException();
    }

    /// <summary>
    ///   Создает кодировку XML для разрешения и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Кодировка XML разрешения, включающая любые сведения о состоянии.
    /// </returns>
    public SecurityElement ToXml()
    {
      SecurityElement element = new SecurityElement("IPermission");
      XMLUtil.AddClassAttribute(element, this.GetType(), "System.Security.Permissions.PrincipalPermission");
      element.AddAttribute("version", "1");
      int length = this.m_array.Length;
      for (int index = 0; index < length; ++index)
        element.AddChild(this.m_array[index].ToXml());
      return element;
    }

    /// <summary>
    ///   Восстанавливает разрешение с указанным состоянием из кодировки XML.
    /// </summary>
    /// <param name="elem">
    ///   Кодировка XML, используемая для восстановления разрешения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="elem" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="elem" /> не является допустимым элементом разрешения.
    /// 
    ///   -или-
    /// 
    ///   Недопустимый номер версии параметра <paramref name="elem" />.
    /// </exception>
    public void FromXml(SecurityElement elem)
    {
      CodeAccessPermission.ValidateElement(elem, (IPermission) this);
      if (elem.InternalChildren != null && elem.InternalChildren.Count != 0)
      {
        int count = elem.InternalChildren.Count;
        int num = 0;
        this.m_array = new IDRole[count];
        foreach (SecurityElement child in elem.Children)
        {
          IDRole idRole = new IDRole();
          idRole.FromXml(child);
          this.m_array[num++] = idRole;
        }
      }
      else
        this.m_array = new IDRole[0];
    }

    /// <summary>
    ///   Создает и возвращает строку, представляющую текущее разрешение.
    /// </summary>
    /// <returns>Представление текущего разрешения.</returns>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return PrincipalPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 8;
    }
  }
}

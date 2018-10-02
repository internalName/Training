// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.StrongNameIdentityPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Определяет разрешение удостоверения для строгих имен.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class StrongNameIdentityPermission : CodeAccessPermission, IBuiltInPermission
  {
    private bool m_unrestricted;
    private StrongName2[] m_strongNames;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> указанным значением <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </summary>
    /// <param name="state">
    ///   Одно из значений <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="state" /> не является допустимым значением для <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </exception>
    public StrongNameIdentityPermission(PermissionState state)
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
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> для указанного удостоверения строгого имени.
    /// </summary>
    /// <param name="blob">
    ///   Открытый ключ, определяющий пространство имен удостоверений строгих имен.
    /// </param>
    /// <param name="name">
    ///   Часть простого имени удостоверения строгого имени.
    ///    Это соответствует имени сборки.
    /// </param>
    /// <param name="version">Номер версии удостоверения.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="blob" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="name" /> представляет собой пустую строку ("").
    /// </exception>
    public StrongNameIdentityPermission(StrongNamePublicKeyBlob blob, string name, Version version)
    {
      if (blob == null)
        throw new ArgumentNullException(nameof (blob));
      if (name != null && name.Equals(""))
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyStrongName"));
      this.m_unrestricted = false;
      this.m_strongNames = new StrongName2[1];
      this.m_strongNames[0] = new StrongName2(blob, name, version);
    }

    /// <summary>
    ///   Возвращает или задает большой двоичный объект открытого ключа, который определяет пространство имен удостоверений строгих имен.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Permissions.StrongNamePublicKeyBlob" />, содержащий открытый ключ удостоверения, или <see langword="null" />, если ключ отсутствует.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойству задано значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Не удалось получить значение свойства, поскольку содержащееся в нем удостоверение неоднозначно.
    /// </exception>
    public StrongNamePublicKeyBlob PublicKey
    {
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (PublicKey));
        this.m_unrestricted = false;
        if (this.m_strongNames != null && this.m_strongNames.Length == 1)
        {
          this.m_strongNames[0].m_publicKeyBlob = value;
        }
        else
        {
          this.m_strongNames = new StrongName2[1];
          this.m_strongNames[0] = new StrongName2(value, "", new Version());
        }
      }
      get
      {
        if (this.m_strongNames == null || this.m_strongNames.Length == 0)
          return (StrongNamePublicKeyBlob) null;
        if (this.m_strongNames.Length > 1)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_AmbiguousIdentity"));
        return this.m_strongNames[0].m_publicKeyBlob;
      }
    }

    /// <summary>
    ///   Возвращает или задает часть простого имени идентификатора строгого имени.
    /// </summary>
    /// <returns>Простое имя идентификатора.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Значением является пустая строка ("").
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Не удалось получить значение свойства, поскольку содержащееся в нем удостоверение неоднозначно.
    /// </exception>
    public string Name
    {
      set
      {
        if (value != null && value.Length == 0)
          throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"));
        this.m_unrestricted = false;
        if (this.m_strongNames != null && this.m_strongNames.Length == 1)
        {
          this.m_strongNames[0].m_name = value;
        }
        else
        {
          this.m_strongNames = new StrongName2[1];
          this.m_strongNames[0] = new StrongName2((StrongNamePublicKeyBlob) null, value, new Version());
        }
      }
      get
      {
        if (this.m_strongNames == null || this.m_strongNames.Length == 0)
          return "";
        if (this.m_strongNames.Length > 1)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_AmbiguousIdentity"));
        return this.m_strongNames[0].m_name;
      }
    }

    /// <summary>Возвращает или задает номер версии удостоверения.</summary>
    /// <returns>Версия удостоверения.</returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Не удалось получить значение свойства, поскольку удостоверение неоднозначно.
    /// </exception>
    public Version Version
    {
      set
      {
        this.m_unrestricted = false;
        if (this.m_strongNames != null && this.m_strongNames.Length == 1)
        {
          this.m_strongNames[0].m_version = value;
        }
        else
        {
          this.m_strongNames = new StrongName2[1];
          this.m_strongNames[0] = new StrongName2((StrongNamePublicKeyBlob) null, "", value);
        }
      }
      get
      {
        if (this.m_strongNames == null || this.m_strongNames.Length == 0)
          return new Version();
        if (this.m_strongNames.Length > 1)
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_AmbiguousIdentity"));
        return this.m_strongNames[0].m_version;
      }
    }

    /// <summary>
    ///   Создает и возвращает идентичную копию текущего разрешения.
    /// </summary>
    /// <returns>Копия текущего разрешения.</returns>
    public override IPermission Copy()
    {
      StrongNameIdentityPermission identityPermission = new StrongNameIdentityPermission(PermissionState.None);
      identityPermission.m_unrestricted = this.m_unrestricted;
      if (this.m_strongNames != null)
      {
        identityPermission.m_strongNames = new StrongName2[this.m_strongNames.Length];
        for (int index = 0; index < this.m_strongNames.Length; ++index)
          identityPermission.m_strongNames[index] = this.m_strongNames[index].Copy();
      }
      return (IPermission) identityPermission;
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
        return !this.m_unrestricted && (this.m_strongNames == null || this.m_strongNames.Length == 0);
      StrongNameIdentityPermission identityPermission = target as StrongNameIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (identityPermission.m_unrestricted)
        return true;
      if (this.m_unrestricted)
        return false;
      if (this.m_strongNames != null)
      {
        foreach (StrongName2 strongName1 in this.m_strongNames)
        {
          bool flag = false;
          if (identityPermission.m_strongNames != null)
          {
            foreach (StrongName2 strongName2 in identityPermission.m_strongNames)
            {
              if (strongName1.IsSubsetOf(strongName2))
              {
                flag = true;
                break;
              }
            }
          }
          if (!flag)
            return false;
        }
      }
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
    ///   Новое разрешение, представляющее собой пересечение текущего разрешения и указанного разрешения, или <see langword="null" />, если пересечение является пустым.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="target" /> не равен <see langword="null" /> и имеет тип, не совпадающий с типом текущего разрешения.
    /// </exception>
    public override IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      StrongNameIdentityPermission identityPermission = target as StrongNameIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.m_unrestricted && identityPermission.m_unrestricted)
        return (IPermission) new StrongNameIdentityPermission(PermissionState.None)
        {
          m_unrestricted = true
        };
      if (this.m_unrestricted)
        return identityPermission.Copy();
      if (identityPermission.m_unrestricted)
        return this.Copy();
      if (this.m_strongNames == null || identityPermission.m_strongNames == null || (this.m_strongNames.Length == 0 || identityPermission.m_strongNames.Length == 0))
        return (IPermission) null;
      List<StrongName2> strongName2List = new List<StrongName2>();
      foreach (StrongName2 strongName1 in this.m_strongNames)
      {
        foreach (StrongName2 strongName2 in identityPermission.m_strongNames)
        {
          StrongName2 strongName2_1 = strongName1.Intersect(strongName2);
          if (strongName2_1 != null)
            strongName2List.Add(strongName2_1);
        }
      }
      if (strongName2List.Count == 0)
        return (IPermission) null;
      return (IPermission) new StrongNameIdentityPermission(PermissionState.None)
      {
        m_strongNames = strongName2List.ToArray()
      };
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
    /// 
    ///   -или-
    /// 
    ///   Разрешения не равны, и одно является подмножеством другого.
    /// </exception>
    public override IPermission Union(IPermission target)
    {
      if (target == null)
      {
        if ((this.m_strongNames == null || this.m_strongNames.Length == 0) && !this.m_unrestricted)
          return (IPermission) null;
        return this.Copy();
      }
      StrongNameIdentityPermission identityPermission = target as StrongNameIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.m_unrestricted || identityPermission.m_unrestricted)
        return (IPermission) new StrongNameIdentityPermission(PermissionState.None)
        {
          m_unrestricted = true
        };
      if (this.m_strongNames == null || this.m_strongNames.Length == 0)
      {
        if (identityPermission.m_strongNames == null || identityPermission.m_strongNames.Length == 0)
          return (IPermission) null;
        return identityPermission.Copy();
      }
      if (identityPermission.m_strongNames == null || identityPermission.m_strongNames.Length == 0)
        return this.Copy();
      List<StrongName2> strongName2List = new List<StrongName2>();
      foreach (StrongName2 strongName in this.m_strongNames)
        strongName2List.Add(strongName);
      foreach (StrongName2 strongName in identityPermission.m_strongNames)
      {
        bool flag = false;
        foreach (StrongName2 target1 in strongName2List)
        {
          if (strongName.Equals(target1))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          strongName2List.Add(strongName);
      }
      return (IPermission) new StrongNameIdentityPermission(PermissionState.None)
      {
        m_strongNames = strongName2List.ToArray()
      };
    }

    /// <summary>
    ///   Восстанавливает разрешение с указанным состоянием из кодировки XML.
    /// </summary>
    /// <param name="e">
    ///   Кодировка XML, используемая для восстановления разрешения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="e" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="e" /> не является допустимым элементом разрешения.
    /// 
    ///   -или-
    /// 
    ///   Недопустимый номер версии параметра <paramref name="e" />.
    /// </exception>
    public override void FromXml(SecurityElement e)
    {
      this.m_unrestricted = false;
      this.m_strongNames = (StrongName2[]) null;
      CodeAccessPermission.ValidateElement(e, (IPermission) this);
      string strA = e.Attribute("Unrestricted");
      if (strA != null && string.Compare(strA, "true", StringComparison.OrdinalIgnoreCase) == 0)
      {
        this.m_unrestricted = true;
      }
      else
      {
        string publicKey1 = e.Attribute("PublicKeyBlob");
        string name1 = e.Attribute("Name");
        string version1 = e.Attribute("AssemblyVersion");
        List<StrongName2> strongName2List = new List<StrongName2>();
        if (publicKey1 != null || name1 != null || version1 != null)
        {
          StrongName2 strongName2 = new StrongName2(publicKey1 == null ? (StrongNamePublicKeyBlob) null : new StrongNamePublicKeyBlob(publicKey1), name1, version1 == null ? (Version) null : new Version(version1));
          strongName2List.Add(strongName2);
        }
        ArrayList children = e.Children;
        if (children != null)
        {
          foreach (SecurityElement securityElement in children)
          {
            string publicKey2 = securityElement.Attribute("PublicKeyBlob");
            string name2 = securityElement.Attribute("Name");
            string version2 = securityElement.Attribute("AssemblyVersion");
            if (publicKey2 != null || name2 != null || version2 != null)
            {
              StrongName2 strongName2 = new StrongName2(publicKey2 == null ? (StrongNamePublicKeyBlob) null : new StrongNamePublicKeyBlob(publicKey2), name2, version2 == null ? (Version) null : new Version(version2));
              strongName2List.Add(strongName2);
            }
          }
        }
        if (strongName2List.Count == 0)
          return;
        this.m_strongNames = strongName2List.ToArray();
      }
    }

    /// <summary>
    ///   Создает кодировку XML для разрешения и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Кодировка XML разрешения, включающая любые сведения о состоянии.
    /// </returns>
    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.StrongNameIdentityPermission");
      if (this.m_unrestricted)
        permissionElement.AddAttribute("Unrestricted", "true");
      else if (this.m_strongNames != null)
      {
        if (this.m_strongNames.Length == 1)
        {
          if (this.m_strongNames[0].m_publicKeyBlob != null)
            permissionElement.AddAttribute("PublicKeyBlob", Hex.EncodeHexString(this.m_strongNames[0].m_publicKeyBlob.PublicKey));
          if (this.m_strongNames[0].m_name != null)
            permissionElement.AddAttribute("Name", this.m_strongNames[0].m_name);
          if ((object) this.m_strongNames[0].m_version != null)
            permissionElement.AddAttribute("AssemblyVersion", this.m_strongNames[0].m_version.ToString());
        }
        else
        {
          for (int index = 0; index < this.m_strongNames.Length; ++index)
          {
            SecurityElement child = new SecurityElement("StrongName");
            if (this.m_strongNames[index].m_publicKeyBlob != null)
              child.AddAttribute("PublicKeyBlob", Hex.EncodeHexString(this.m_strongNames[index].m_publicKeyBlob.PublicKey));
            if (this.m_strongNames[index].m_name != null)
              child.AddAttribute("Name", this.m_strongNames[index].m_name);
            if ((object) this.m_strongNames[index].m_version != null)
              child.AddAttribute("AssemblyVersion", this.m_strongNames[index].m_version.ToString());
            permissionElement.AddChild(child);
          }
        }
      }
      return permissionElement;
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return StrongNameIdentityPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 12;
    }
  }
}

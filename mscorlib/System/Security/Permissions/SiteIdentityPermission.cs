// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.SiteIdentityPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Определяет разрешение удостоверения для веб-сайта, являющегося источником кода.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class SiteIdentityPermission : CodeAccessPermission, IBuiltInPermission
  {
    [OptionalField(VersionAdded = 2)]
    private bool m_unrestricted;
    [OptionalField(VersionAdded = 2)]
    private SiteString[] m_sites;
    [OptionalField(VersionAdded = 2)]
    private string m_serializedPermission;
    private SiteString m_site;

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      if (this.m_serializedPermission != null)
      {
        this.FromXml(SecurityElement.FromString(this.m_serializedPermission));
        this.m_serializedPermission = (string) null;
      }
      else
      {
        if (this.m_site == null)
          return;
        this.m_unrestricted = false;
        this.m_sites = new SiteString[1];
        this.m_sites[0] = this.m_site;
        this.m_site = (SiteString) null;
      }
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) == (StreamingContextStates) 0)
        return;
      this.m_serializedPermission = this.ToXml().ToString();
      if (this.m_sites == null || this.m_sites.Length != 1)
        return;
      this.m_site = this.m_sites[0];
    }

    [OnSerialized]
    private void OnSerialized(StreamingContext ctx)
    {
      if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) == (StreamingContextStates) 0)
        return;
      this.m_serializedPermission = (string) null;
      this.m_site = (SiteString) null;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.SiteIdentityPermission" /> указанным значением <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </summary>
    /// <param name="state">
    ///   Одно из значений <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="state" /> не является допустимым значением для <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </exception>
    public SiteIdentityPermission(PermissionState state)
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
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.SiteIdentityPermission" /> для представления удостоверения указанного сайта.
    /// </summary>
    /// <param name="site">Имя сайта или подстановочное выражение.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="site" /> не является допустимой строкой или не соответствует допустимому подстановочному имени сайта.
    /// </exception>
    public SiteIdentityPermission(string site)
    {
      this.Site = site;
    }

    /// <summary>Возвращает или задает текущий узел.</summary>
    /// <returns>Текущий узел.</returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Не удается получить удостоверение узла, поскольку удостоверение неоднозначно.
    /// </exception>
    public string Site
    {
      set
      {
        this.m_unrestricted = false;
        this.m_sites = new SiteString[1];
        this.m_sites[0] = new SiteString(value);
      }
      get
      {
        if (this.m_sites == null)
          return "";
        if (this.m_sites.Length == 1)
          return this.m_sites[0].ToString();
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_AmbiguousIdentity"));
      }
    }

    /// <summary>
    ///   Создает и возвращает идентичную копию текущего разрешения.
    /// </summary>
    /// <returns>Копия текущего разрешения.</returns>
    public override IPermission Copy()
    {
      SiteIdentityPermission identityPermission = new SiteIdentityPermission(PermissionState.None);
      identityPermission.m_unrestricted = this.m_unrestricted;
      if (this.m_sites != null)
      {
        identityPermission.m_sites = new SiteString[this.m_sites.Length];
        for (int index = 0; index < this.m_sites.Length; ++index)
          identityPermission.m_sites[index] = this.m_sites[index].Copy();
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
        return !this.m_unrestricted && (this.m_sites == null || this.m_sites.Length == 0);
      SiteIdentityPermission identityPermission = target as SiteIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (identityPermission.m_unrestricted)
        return true;
      if (this.m_unrestricted)
        return false;
      if (this.m_sites != null)
      {
        foreach (SiteString site1 in this.m_sites)
        {
          bool flag = false;
          if (identityPermission.m_sites != null)
          {
            foreach (SiteString site2 in identityPermission.m_sites)
            {
              if (site1.IsSubsetOf(site2))
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
      SiteIdentityPermission identityPermission = target as SiteIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.m_unrestricted && identityPermission.m_unrestricted)
        return (IPermission) new SiteIdentityPermission(PermissionState.None)
        {
          m_unrestricted = true
        };
      if (this.m_unrestricted)
        return identityPermission.Copy();
      if (identityPermission.m_unrestricted)
        return this.Copy();
      if (this.m_sites == null || identityPermission.m_sites == null || (this.m_sites.Length == 0 || identityPermission.m_sites.Length == 0))
        return (IPermission) null;
      List<SiteString> siteStringList = new List<SiteString>();
      foreach (SiteString site1 in this.m_sites)
      {
        foreach (SiteString site2 in identityPermission.m_sites)
        {
          SiteString siteString = site1.Intersect(site2);
          if (siteString != null)
            siteStringList.Add(siteString);
        }
      }
      if (siteStringList.Count == 0)
        return (IPermission) null;
      return (IPermission) new SiteIdentityPermission(PermissionState.None)
      {
        m_sites = siteStringList.ToArray()
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
    ///   Разрешения не равны, и одно не является подмножеством другого.
    /// </exception>
    public override IPermission Union(IPermission target)
    {
      if (target == null)
      {
        if ((this.m_sites == null || this.m_sites.Length == 0) && !this.m_unrestricted)
          return (IPermission) null;
        return this.Copy();
      }
      SiteIdentityPermission identityPermission = target as SiteIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.m_unrestricted || identityPermission.m_unrestricted)
        return (IPermission) new SiteIdentityPermission(PermissionState.None)
        {
          m_unrestricted = true
        };
      if (this.m_sites == null || this.m_sites.Length == 0)
      {
        if (identityPermission.m_sites == null || identityPermission.m_sites.Length == 0)
          return (IPermission) null;
        return identityPermission.Copy();
      }
      if (identityPermission.m_sites == null || identityPermission.m_sites.Length == 0)
        return this.Copy();
      List<SiteString> siteStringList = new List<SiteString>();
      foreach (SiteString site in this.m_sites)
        siteStringList.Add(site);
      foreach (SiteString site in identityPermission.m_sites)
      {
        bool flag = false;
        foreach (SiteString siteString in siteStringList)
        {
          if (site.Equals((object) siteString))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          siteStringList.Add(site);
      }
      return (IPermission) new SiteIdentityPermission(PermissionState.None)
      {
        m_sites = siteStringList.ToArray()
      };
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
      this.m_unrestricted = false;
      this.m_sites = (SiteString[]) null;
      CodeAccessPermission.ValidateElement(esd, (IPermission) this);
      string strA = esd.Attribute("Unrestricted");
      if (strA != null && string.Compare(strA, "true", StringComparison.OrdinalIgnoreCase) == 0)
      {
        this.m_unrestricted = true;
      }
      else
      {
        string site1 = esd.Attribute("Site");
        List<SiteString> siteStringList = new List<SiteString>();
        if (site1 != null)
          siteStringList.Add(new SiteString(site1));
        ArrayList children = esd.Children;
        if (children != null)
        {
          foreach (SecurityElement securityElement in children)
          {
            string site2 = securityElement.Attribute("Site");
            if (site2 != null)
              siteStringList.Add(new SiteString(site2));
          }
        }
        if (siteStringList.Count == 0)
          return;
        this.m_sites = siteStringList.ToArray();
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
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.SiteIdentityPermission");
      if (this.m_unrestricted)
        permissionElement.AddAttribute("Unrestricted", "true");
      else if (this.m_sites != null)
      {
        if (this.m_sites.Length == 1)
        {
          permissionElement.AddAttribute("Site", this.m_sites[0].ToString());
        }
        else
        {
          for (int index = 0; index < this.m_sites.Length; ++index)
          {
            SecurityElement child = new SecurityElement("Site");
            child.AddAttribute("Site", this.m_sites[index].ToString());
            permissionElement.AddChild(child);
          }
        }
      }
      return permissionElement;
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return SiteIdentityPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 11;
    }
  }
}

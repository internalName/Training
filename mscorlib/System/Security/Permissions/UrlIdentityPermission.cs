// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.UrlIdentityPermission
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
  ///   Определяет разрешение удостоверения для URL-адреса, являющегося источником кода.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class UrlIdentityPermission : CodeAccessPermission, IBuiltInPermission
  {
    [OptionalField(VersionAdded = 2)]
    private bool m_unrestricted;
    [OptionalField(VersionAdded = 2)]
    private URLString[] m_urls;
    [OptionalField(VersionAdded = 2)]
    private string m_serializedPermission;
    private URLString m_url;

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
        if (this.m_url == null)
          return;
        this.m_unrestricted = false;
        this.m_urls = new URLString[1];
        this.m_urls[0] = this.m_url;
        this.m_url = (URLString) null;
      }
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) == (StreamingContextStates) 0)
        return;
      this.m_serializedPermission = this.ToXml().ToString();
      if (this.m_urls == null || this.m_urls.Length != 1)
        return;
      this.m_url = this.m_urls[0];
    }

    [OnSerialized]
    private void OnSerialized(StreamingContext ctx)
    {
      if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) == (StreamingContextStates) 0)
        return;
      this.m_serializedPermission = (string) null;
      this.m_url = (URLString) null;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> указанным значением <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </summary>
    /// <param name="state">
    ///   Одно из значений <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="state" /> не является допустимым значением для <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </exception>
    public UrlIdentityPermission(PermissionState state)
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
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> для представления идентификатора URL-адреса, который описывается параметром <paramref name="site" />.
    /// </summary>
    /// <param name="site">
    ///   URL-адрес или выражение с подстановочными знаками.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="site" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Длина параметра <paramref name="site" /> равна нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Одна из составляющих параметра <paramref name="site" /> (URL-адрес, каталог или веб-узел) имеет недопустимое значение.
    /// </exception>
    public UrlIdentityPermission(string site)
    {
      if (site == null)
        throw new ArgumentNullException(nameof (site));
      this.Url = site;
    }

    internal UrlIdentityPermission(URLString site)
    {
      this.m_unrestricted = false;
      this.m_urls = new URLString[1];
      this.m_urls[0] = site;
    }

    internal void AppendOrigin(ArrayList originList)
    {
      if (this.m_urls == null)
      {
        originList.Add((object) "");
      }
      else
      {
        for (int index = 0; index < this.m_urls.Length; ++index)
          originList.Add((object) this.m_urls[index].ToString());
      }
    }

    /// <summary>
    ///   Возвращает или задает URL-адрес, представляющий удостоверение интернет-кода.
    /// </summary>
    /// <returns>
    ///   URL-адрес, представляющий удостоверение интернет-кода.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   URL-адрес получить не удалось, поскольку удостоверение неоднозначно.
    /// </exception>
    public string Url
    {
      set
      {
        this.m_unrestricted = false;
        if (value == null || value.Length == 0)
        {
          this.m_urls = (URLString[]) null;
        }
        else
        {
          this.m_urls = new URLString[1];
          this.m_urls[0] = new URLString(value);
        }
      }
      get
      {
        if (this.m_urls == null)
          return "";
        if (this.m_urls.Length == 1)
          return this.m_urls[0].ToString();
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_AmbiguousIdentity"));
      }
    }

    /// <summary>
    ///   Создает и возвращает идентичную копию текущего разрешения.
    /// </summary>
    /// <returns>Копия текущего разрешения.</returns>
    public override IPermission Copy()
    {
      UrlIdentityPermission identityPermission = new UrlIdentityPermission(PermissionState.None);
      identityPermission.m_unrestricted = this.m_unrestricted;
      if (this.m_urls != null)
      {
        identityPermission.m_urls = new URLString[this.m_urls.Length];
        for (int index = 0; index < this.m_urls.Length; ++index)
          identityPermission.m_urls[index] = (URLString) this.m_urls[index].Copy();
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
    ///   Параметр <paramref name="target" /> не равен <see langword="null" /> и имеет тип, отличный от типа текущего разрешения.
    /// 
    ///   -или-
    /// 
    ///   Свойство URL-адреса не является допустимым URL-адресом.
    /// </exception>
    public override bool IsSubsetOf(IPermission target)
    {
      if (target == null)
        return !this.m_unrestricted && (this.m_urls == null || this.m_urls.Length == 0);
      UrlIdentityPermission identityPermission = target as UrlIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (identityPermission.m_unrestricted)
        return true;
      if (this.m_unrestricted)
        return false;
      if (this.m_urls != null)
      {
        foreach (URLString url1 in this.m_urls)
        {
          bool flag = false;
          if (identityPermission.m_urls != null)
          {
            foreach (URLString url2 in identityPermission.m_urls)
            {
              if (url1.IsSubsetOf((SiteString) url2))
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
    /// 
    ///   -или-
    /// 
    ///   Свойство URL-адреса не является допустимым URL-адресом.
    /// </exception>
    public override IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      UrlIdentityPermission identityPermission = target as UrlIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.m_unrestricted && identityPermission.m_unrestricted)
        return (IPermission) new UrlIdentityPermission(PermissionState.None)
        {
          m_unrestricted = true
        };
      if (this.m_unrestricted)
        return identityPermission.Copy();
      if (identityPermission.m_unrestricted)
        return this.Copy();
      if (this.m_urls == null || identityPermission.m_urls == null || (this.m_urls.Length == 0 || identityPermission.m_urls.Length == 0))
        return (IPermission) null;
      List<URLString> urlStringList = new List<URLString>();
      foreach (URLString url1 in this.m_urls)
      {
        foreach (URLString url2 in identityPermission.m_urls)
        {
          URLString urlString = (URLString) url1.Intersect((SiteString) url2);
          if (urlString != null)
            urlStringList.Add(urlString);
        }
      }
      if (urlStringList.Count == 0)
        return (IPermission) null;
      return (IPermission) new UrlIdentityPermission(PermissionState.None)
      {
        m_urls = urlStringList.ToArray()
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
    ///   Свойство <see cref="P:System.Security.Permissions.UrlIdentityPermission.Url" /> не является допустимым URL-адресом.
    /// 
    ///   -или-
    /// 
    ///   Два разрешения не равны, и одно не является подмножеством другого.
    /// </exception>
    public override IPermission Union(IPermission target)
    {
      if (target == null)
      {
        if ((this.m_urls == null || this.m_urls.Length == 0) && !this.m_unrestricted)
          return (IPermission) null;
        return this.Copy();
      }
      UrlIdentityPermission identityPermission = target as UrlIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      if (this.m_unrestricted || identityPermission.m_unrestricted)
        return (IPermission) new UrlIdentityPermission(PermissionState.None)
        {
          m_unrestricted = true
        };
      if (this.m_urls == null || this.m_urls.Length == 0)
      {
        if (identityPermission.m_urls == null || identityPermission.m_urls.Length == 0)
          return (IPermission) null;
        return identityPermission.Copy();
      }
      if (identityPermission.m_urls == null || identityPermission.m_urls.Length == 0)
        return this.Copy();
      List<URLString> urlStringList = new List<URLString>();
      foreach (URLString url in this.m_urls)
        urlStringList.Add(url);
      foreach (URLString url1 in identityPermission.m_urls)
      {
        bool flag = false;
        foreach (URLString url2 in urlStringList)
        {
          if (url1.Equals(url2))
          {
            flag = true;
            break;
          }
        }
        if (!flag)
          urlStringList.Add(url1);
      }
      return (IPermission) new UrlIdentityPermission(PermissionState.None)
      {
        m_urls = urlStringList.ToArray()
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
      this.m_urls = (URLString[]) null;
      CodeAccessPermission.ValidateElement(esd, (IPermission) this);
      string strA = esd.Attribute("Unrestricted");
      if (strA != null && string.Compare(strA, "true", StringComparison.OrdinalIgnoreCase) == 0)
      {
        this.m_unrestricted = true;
      }
      else
      {
        string url1 = esd.Attribute("Url");
        List<URLString> urlStringList = new List<URLString>();
        if (url1 != null)
          urlStringList.Add(new URLString(url1, true));
        ArrayList children = esd.Children;
        if (children != null)
        {
          foreach (SecurityElement securityElement in children)
          {
            string url2 = securityElement.Attribute("Url");
            if (url2 != null)
              urlStringList.Add(new URLString(url2, true));
          }
        }
        if (urlStringList.Count == 0)
          return;
        this.m_urls = urlStringList.ToArray();
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
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.UrlIdentityPermission");
      if (this.m_unrestricted)
        permissionElement.AddAttribute("Unrestricted", "true");
      else if (this.m_urls != null)
      {
        if (this.m_urls.Length == 1)
        {
          permissionElement.AddAttribute("Url", this.m_urls[0].ToString());
        }
        else
        {
          for (int index = 0; index < this.m_urls.Length; ++index)
          {
            SecurityElement child = new SecurityElement("Url");
            child.AddAttribute("Url", this.m_urls[index].ToString());
            permissionElement.AddChild(child);
          }
        }
      }
      return permissionElement;
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return UrlIdentityPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 13;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.Site
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>
  ///   Предоставляет веб-сайт, являющийся источником сборки кода в качестве свидетельства для оценки политики.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class Site : EvidenceBase, IIdentityPermissionFactory
  {
    private SiteString m_name;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Policy.Site" /> класса веб-сайт, являющийся источником сборки кода.
    /// </summary>
    /// <param name="name">
    ///   Веб-сайт, являющийся источником для связанной сборки кода.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    public Site(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      this.m_name = new SiteString(name);
    }

    private Site(SiteString name)
    {
      this.m_name = name;
    }

    /// <summary>
    ///   Создает новый <see cref="T:System.Security.Policy.Site" /> объекта из указанного URL-адреса.
    /// </summary>
    /// <param name="url">
    ///   URL-адрес, из которого будет создан новый <see cref="T:System.Security.Policy.Site" /> объекта.
    /// </param>
    /// <returns>Новый объект сайта.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="url" /> Параметр не является допустимым URL-адресом.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="url" /> Параметр является именем файла.
    /// </exception>
    public static Site CreateFromUrl(string url)
    {
      return new Site(Site.ParseSiteFromUrl(url));
    }

    private static SiteString ParseSiteFromUrl(string name)
    {
      if (string.Compare(new URLString(name).Scheme, "file", StringComparison.OrdinalIgnoreCase) == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
      return new SiteString(new URLString(name).Host);
    }

    /// <summary>
    ///   Возвращает веб-сайт, являющийся источником сборки кода.
    /// </summary>
    /// <returns>Веб-сайт, являющийся источником сборки кода.</returns>
    public string Name
    {
      get
      {
        return this.m_name.ToString();
      }
    }

    internal SiteString GetSiteString()
    {
      return this.m_name;
    }

    /// <summary>
    ///   Создает разрешение подлинности, соответствующее текущему <see cref="T:System.Security.Policy.Site" /> объекта.
    /// </summary>
    /// <param name="evidence">
    ///   Свидетельство, используемое при создании разрешения идентификаторов.
    /// </param>
    /// <returns>
    ///   Разрешение идентификаторов для текущего сайта <see cref="T:System.Security.Policy.Site" /> объекта.
    /// </returns>
    public IPermission CreateIdentityPermission(Evidence evidence)
    {
      return (IPermission) new SiteIdentityPermission(this.Name);
    }

    /// <summary>
    ///   Сравнивает текущий <see cref="T:System.Security.Policy.Site" /> для указанного объекта для эквивалентности.
    /// </summary>
    /// <param name="o">
    ///   Объект, который требуется сравнить с текущим объектом.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если два экземпляра <see cref="T:System.Security.Policy.Site" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object o)
    {
      Site site = o as Site;
      if (site == null)
        return false;
      return string.Equals(this.Name, site.Name, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>Возвращает хэш-код текущего имени веб-сайта.</summary>
    /// <returns>Хэш-код текущего имени веб-сайта.</returns>
    public override int GetHashCode()
    {
      return this.Name.GetHashCode();
    }

    /// <summary>
    ///   Создает новый объект, являющийся копией текущего экземпляра.
    /// </summary>
    /// <returns>Новый объект, являющийся копией этого экземпляра.</returns>
    public override EvidenceBase Clone()
    {
      return (EvidenceBase) new Site(this.m_name);
    }

    /// <summary>
    ///   Создает эквивалентную копию <see cref="T:System.Security.Policy.Site" /> объекта.
    /// </summary>
    /// <returns>
    ///   Новый объект, идентичный текущему <see cref="T:System.Security.Policy.Site" /> объекта.
    /// </returns>
    public object Copy()
    {
      return (object) this.Clone();
    }

    internal SecurityElement ToXml()
    {
      SecurityElement securityElement = new SecurityElement("System.Security.Policy.Site");
      securityElement.AddAttribute("version", "1");
      if (this.m_name != null)
        securityElement.AddChild(new SecurityElement("Name", this.m_name.ToString()));
      return securityElement;
    }

    /// <summary>
    ///   Возвращает строковое представление текущего объекта <see cref="T:System.Security.Policy.Site" /> объекта.
    /// </summary>
    /// <returns>Представление текущего сайта.</returns>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }

    internal object Normalize()
    {
      return (object) this.m_name.ToString().ToUpper(CultureInfo.InvariantCulture);
    }
  }
}

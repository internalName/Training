// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.Url
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>
  ///   Предоставляет URL-адрес, являющийся источником сборки кода в качестве свидетельства для оценки политики.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class Url : EvidenceBase, IIdentityPermissionFactory
  {
    private URLString m_url;

    internal Url(string name, bool parsed)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      this.m_url = new URLString(name, parsed);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Policy.Url" /> класса на URL-адрес, являющийся источником сборки кода.
    /// </summary>
    /// <param name="name">
    ///   URL-адрес, являющийся источником для связанной сборки кода.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    public Url(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      this.m_url = new URLString(name);
    }

    private Url(Url url)
    {
      this.m_url = url.m_url;
    }

    /// <summary>
    ///   Возвращает URL-адрес, являющийся источником сборки кода.
    /// </summary>
    /// <returns>URL-адрес, являющийся источником сборки кода.</returns>
    public string Value
    {
      get
      {
        return this.m_url.ToString();
      }
    }

    internal URLString GetURLString()
    {
      return this.m_url;
    }

    /// <summary>
    ///   Создает разрешение подлинности, соответствующее текущему экземпляру <see cref="T:System.Security.Policy.Url" /> свидетельства.
    /// </summary>
    /// <param name="evidence">
    ///   Свидетельство, используемое при создании разрешения подлинности.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> для указанного <see cref="T:System.Security.Policy.Url" /> свидетельства.
    /// </returns>
    public IPermission CreateIdentityPermission(Evidence evidence)
    {
      return (IPermission) new UrlIdentityPermission(this.m_url);
    }

    /// <summary>
    ///   Сравнивает текущий <see cref="T:System.Security.Policy.Url" /> объект свидетельства для указанного объекта для эквивалентности.
    /// </summary>
    /// <param name="o">
    ///   <see cref="T:System.Security.Policy.Url" /> Объект свидетельства, который требуется сравнить с текущим объектом.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если два объекта <see cref="T:System.Security.Policy.Url" /> равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool Equals(object o)
    {
      Url url = o as Url;
      if (url == null)
        return false;
      return url.m_url.Equals(this.m_url);
    }

    /// <summary>Возвращает хэш-код текущего URL-адреса.</summary>
    /// <returns>Хэш-код текущего URL-адреса.</returns>
    public override int GetHashCode()
    {
      return this.m_url.GetHashCode();
    }

    /// <summary>
    ///   Создает новый объект, являющийся копией текущего экземпляра.
    /// </summary>
    /// <returns>Новый объект, являющийся копией этого экземпляра.</returns>
    public override EvidenceBase Clone()
    {
      return (EvidenceBase) new Url(this);
    }

    /// <summary>Создает новую копию объекта свидетельства.</summary>
    /// <returns>Новая, идентичная копия объекта свидетельства.</returns>
    public object Copy()
    {
      return (object) this.Clone();
    }

    internal SecurityElement ToXml()
    {
      SecurityElement securityElement = new SecurityElement("System.Security.Policy.Url");
      securityElement.AddAttribute("version", "1");
      if (this.m_url != null)
        securityElement.AddChild(new SecurityElement(nameof (Url), this.m_url.ToString()));
      return securityElement;
    }

    /// <summary>
    ///   Возвращает строковое представление текущего объекта <see cref="T:System.Security.Policy.Url" />.
    /// </summary>
    /// <returns>
    ///   Представление текущего <see cref="T:System.Security.Policy.Url" />.
    /// </returns>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }

    internal object Normalize()
    {
      return (object) this.m_url.NormalizeUrl();
    }
  }
}

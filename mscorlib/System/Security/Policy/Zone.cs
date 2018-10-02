// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.Zone
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Security.Policy
{
  /// <summary>
  ///   Предоставляет зону безопасности сборки кода в качестве свидетельства для оценки политики.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class Zone : EvidenceBase, IIdentityPermissionFactory
  {
    private static readonly string[] s_names = new string[6]
    {
      "MyComputer",
      "Intranet",
      "Trusted",
      "Internet",
      "Untrusted",
      "NoZone"
    };
    [OptionalField(VersionAdded = 2)]
    private string m_url;
    private SecurityZone m_zone;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Policy.Zone" /> класса с зоной, являющийся источником сборки кода.
    /// </summary>
    /// <param name="zone">
    ///   Зона, являющаяся источником для связанной сборки кода.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="zone" /> Параметр не является допустимым <see cref="T:System.Security.SecurityZone" />.
    /// </exception>
    public Zone(SecurityZone zone)
    {
      switch (zone)
      {
        case SecurityZone.MyComputer:
        case SecurityZone.Intranet:
        case SecurityZone.Trusted:
        case SecurityZone.Internet:
        case SecurityZone.Untrusted:
          this.m_zone = zone;
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_IllegalZone"));
      }
    }

    private Zone(Zone zone)
    {
      this.m_url = zone.m_url;
      this.m_zone = zone.m_zone;
    }

    private Zone(string url)
    {
      this.m_url = url;
      this.m_zone = SecurityZone.NoZone;
    }

    /// <summary>Создает новую зону с указанным URL-адресом.</summary>
    /// <param name="url">URL-адрес для создания зоны.</param>
    /// <returns>Новая зона с указанным URL-адрес.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="url" /> имеет значение <see langword="null" />.
    /// </exception>
    public static Zone CreateFromUrl(string url)
    {
      if (url == null)
        throw new ArgumentNullException(nameof (url));
      return new Zone(url);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern SecurityZone _CreateFromUrl(string url);

    /// <summary>
    ///   Создает разрешение подлинности, соответствующее текущему экземпляру <see cref="T:System.Security.Policy.Zone" /> свидетельства.
    /// </summary>
    /// <param name="evidence">
    ///   Свидетельство, используемое при создании разрешения подлинности.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Permissions.ZoneIdentityPermission" /> для указанного <see cref="T:System.Security.Policy.Zone" /> свидетельства.
    /// </returns>
    public IPermission CreateIdentityPermission(Evidence evidence)
    {
      return (IPermission) new ZoneIdentityPermission(this.SecurityZone);
    }

    /// <summary>Получает зону, являющийся источником сборки кода.</summary>
    /// <returns>Зона, являющийся источником сборки кода.</returns>
    public SecurityZone SecurityZone
    {
      [SecuritySafeCritical] get
      {
        if (this.m_url != null)
          this.m_zone = Zone._CreateFromUrl(this.m_url);
        return this.m_zone;
      }
    }

    /// <summary>
    ///   Сравнивает текущий <see cref="T:System.Security.Policy.Zone" /> объект свидетельства для указанного объекта для эквивалентности.
    /// </summary>
    /// <param name="o">
    ///   <see cref="T:System.Security.Policy.Zone" /> Объект свидетельства, который требуется сравнить с текущим объектом.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если два объекта <see cref="T:System.Security.Policy.Zone" /> равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="o" /> Параметр не <see cref="T:System.Security.Policy.Zone" /> объекта.
    /// </exception>
    public override bool Equals(object o)
    {
      Zone zone = o as Zone;
      if (zone == null)
        return false;
      return this.SecurityZone == zone.SecurityZone;
    }

    /// <summary>Возвращает хэш-код текущей зоны.</summary>
    /// <returns>Хэш-код текущей зоны.</returns>
    public override int GetHashCode()
    {
      return (int) this.SecurityZone;
    }

    /// <summary>
    ///   Создает новый объект, являющийся копией текущего экземпляра.
    /// </summary>
    /// <returns>Новый объект, являющийся копией этого экземпляра.</returns>
    public override EvidenceBase Clone()
    {
      return (EvidenceBase) new Zone(this);
    }

    /// <summary>Создает эквивалентную копию объекта свидетельства.</summary>
    /// <returns>Новая, идентичная копия объекта свидетельства.</returns>
    public object Copy()
    {
      return (object) this.Clone();
    }

    internal SecurityElement ToXml()
    {
      SecurityElement securityElement = new SecurityElement("System.Security.Policy.Zone");
      securityElement.AddAttribute("version", "1");
      if (this.SecurityZone != SecurityZone.NoZone)
        securityElement.AddChild(new SecurityElement(nameof (Zone), Zone.s_names[(int) this.SecurityZone]));
      else
        securityElement.AddChild(new SecurityElement(nameof (Zone), Zone.s_names[Zone.s_names.Length - 1]));
      return securityElement;
    }

    /// <summary>
    ///   Возвращает строковое представление текущего объекта <see cref="T:System.Security.Policy.Zone" />.
    /// </summary>
    /// <returns>
    ///   Представление текущего <see cref="T:System.Security.Policy.Zone" />.
    /// </returns>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }

    internal object Normalize()
    {
      return (object) Zone.s_names[(int) this.SecurityZone];
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.ZoneIdentityPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Определяет разрешение удостоверения для зоны, являющейся источником кода.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class ZoneIdentityPermission : CodeAccessPermission, IBuiltInPermission
  {
    private SecurityZone m_zone = SecurityZone.NoZone;
    private const uint AllZones = 31;
    [OptionalField(VersionAdded = 2)]
    private uint m_zones;
    [OptionalField(VersionAdded = 2)]
    private string m_serializedPermission;

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) == (StreamingContextStates) 0)
        return;
      if (this.m_serializedPermission != null)
      {
        this.FromXml(SecurityElement.FromString(this.m_serializedPermission));
        this.m_serializedPermission = (string) null;
      }
      else
      {
        this.SecurityZone = this.m_zone;
        this.m_zone = SecurityZone.NoZone;
      }
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) == (StreamingContextStates) 0)
        return;
      this.m_serializedPermission = this.ToXml().ToString();
      this.m_zone = this.SecurityZone;
    }

    [OnSerialized]
    private void OnSerialized(StreamingContext ctx)
    {
      if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) == (StreamingContextStates) 0)
        return;
      this.m_serializedPermission = (string) null;
      this.m_zone = SecurityZone.NoZone;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.ZoneIdentityPermission" /> указанным значением <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </summary>
    /// <param name="state">
    ///   Одно из значений <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="state" /> не является допустимым значением для <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </exception>
    public ZoneIdentityPermission(PermissionState state)
    {
      if (state == PermissionState.Unrestricted)
      {
        this.m_zones = 31U;
      }
      else
      {
        if (state != PermissionState.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
        this.m_zones = 0U;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.ZoneIdentityPermission" /> для представления идентификатора указанной зоны.
    /// </summary>
    /// <param name="zone">Идентификатор зоны.</param>
    public ZoneIdentityPermission(SecurityZone zone)
    {
      this.SecurityZone = zone;
    }

    internal ZoneIdentityPermission(uint zones)
    {
      this.m_zones = zones & 31U;
    }

    internal void AppendZones(ArrayList zoneList)
    {
      int num1 = 0;
      uint num2 = 1;
      while (num2 < 31U)
      {
        if (((int) this.m_zones & (int) num2) != 0)
          zoneList.Add((object) (SecurityZone) num1);
        ++num1;
        num2 <<= 1;
      }
    }

    /// <summary>
    ///   Возвращает или задает зону, представленную текущим <see cref="T:System.Security.Permissions.ZoneIdentityPermission" />.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Security.SecurityZone" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Значение параметра не является допустимым значением для <see cref="T:System.Security.SecurityZone" />.
    /// </exception>
    public SecurityZone SecurityZone
    {
      set
      {
        ZoneIdentityPermission.VerifyZone(value);
        if (value == SecurityZone.NoZone)
          this.m_zones = 0U;
        else
          this.m_zones = 1U << (int) (value & (SecurityZone) 31);
      }
      get
      {
        SecurityZone securityZone = SecurityZone.NoZone;
        int num1 = 0;
        uint num2 = 1;
        while (num2 < 31U)
        {
          if (((int) this.m_zones & (int) num2) != 0)
          {
            if (securityZone != SecurityZone.NoZone)
              return SecurityZone.NoZone;
            securityZone = (SecurityZone) num1;
          }
          ++num1;
          num2 <<= 1;
        }
        return securityZone;
      }
    }

    private static void VerifyZone(SecurityZone zone)
    {
      switch (zone)
      {
        case SecurityZone.MyComputer:
        case SecurityZone.Intranet:
        case SecurityZone.Trusted:
        case SecurityZone.Internet:
        case SecurityZone.Untrusted:
          break;
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_IllegalZone"));
      }
    }

    /// <summary>
    ///   Создает и возвращает идентичную копию текущего разрешения.
    /// </summary>
    /// <returns>Копия текущего разрешения.</returns>
    public override IPermission Copy()
    {
      return (IPermission) new ZoneIdentityPermission(this.m_zones);
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
    ///   Параметр <paramref name="target" /> не является <see langword="null" />, это разрешение не представляет зону безопасности <see cref="F:System.Security.SecurityZone.NoZone" />, а указанное разрешение не совпадает с текущим разрешением.
    /// </exception>
    public override bool IsSubsetOf(IPermission target)
    {
      if (target == null)
        return this.m_zones == 0U;
      ZoneIdentityPermission identityPermission = target as ZoneIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      return ((int) this.m_zones & (int) identityPermission.m_zones) == (int) this.m_zones;
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
      ZoneIdentityPermission identityPermission = target as ZoneIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      uint zones = this.m_zones & identityPermission.m_zones;
      if (zones == 0U)
        return (IPermission) null;
      return (IPermission) new ZoneIdentityPermission(zones);
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
    ///   Два разрешения не являются одинаковыми, и текущее разрешение не представляет зону безопасности <see cref="F:System.Security.SecurityZone.NoZone" />.
    /// </exception>
    public override IPermission Union(IPermission target)
    {
      if (target == null)
      {
        if (this.m_zones == 0U)
          return (IPermission) null;
        return this.Copy();
      }
      ZoneIdentityPermission identityPermission = target as ZoneIdentityPermission;
      if (identityPermission == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      return (IPermission) new ZoneIdentityPermission(this.m_zones | identityPermission.m_zones);
    }

    /// <summary>
    ///   Создает кодировку XML для разрешения и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Кодировка XML разрешения, включающая любые сведения о состоянии.
    /// </returns>
    public override SecurityElement ToXml()
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.ZoneIdentityPermission");
      if (this.SecurityZone != SecurityZone.NoZone)
      {
        permissionElement.AddAttribute("Zone", Enum.GetName(typeof (SecurityZone), (object) this.SecurityZone));
      }
      else
      {
        int num1 = 0;
        uint num2 = 1;
        while (num2 < 31U)
        {
          if (((int) this.m_zones & (int) num2) != 0)
          {
            SecurityElement child = new SecurityElement("Zone");
            child.AddAttribute("Zone", Enum.GetName(typeof (SecurityZone), (object) (SecurityZone) num1));
            permissionElement.AddChild(child);
          }
          ++num1;
          num2 <<= 1;
        }
      }
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
      this.m_zones = 0U;
      CodeAccessPermission.ValidateElement(esd, (IPermission) this);
      string str = esd.Attribute("Zone");
      if (str != null)
        this.SecurityZone = (SecurityZone) Enum.Parse(typeof (SecurityZone), str);
      if (esd.Children == null)
        return;
      foreach (SecurityElement child in esd.Children)
      {
        int num = (int) Enum.Parse(typeof (SecurityZone), child.Attribute("Zone"));
        if (num != -1)
          this.m_zones |= (uint) (1 << num);
      }
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return ZoneIdentityPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 14;
    }
  }
}

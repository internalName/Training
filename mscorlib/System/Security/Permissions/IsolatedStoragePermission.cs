// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.IsolatedStoragePermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Представляет доступ к общим возможностям изолированного хранилища.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, ControlEvidence = true, ControlPolicy = true)]
  public abstract class IsolatedStoragePermission : CodeAccessPermission, IUnrestrictedPermission
  {
    internal long m_userQuota;
    internal long m_machineQuota;
    internal long m_expirationDays;
    internal bool m_permanentData;
    internal IsolatedStorageContainment m_allowed;
    private const string _strUserQuota = "UserQuota";
    private const string _strMachineQuota = "MachineQuota";
    private const string _strExpiry = "Expiry";
    private const string _strPermDat = "Permanent";

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.IsolatedStoragePermission" /> с указанным состоянием разрешения: ограниченным или неограниченным.
    /// </summary>
    /// <param name="state">
    ///   Одно из значений <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="state" /> не является допустимым значением для <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </exception>
    protected IsolatedStoragePermission(PermissionState state)
    {
      if (state == PermissionState.Unrestricted)
      {
        this.m_userQuota = long.MaxValue;
        this.m_machineQuota = long.MaxValue;
        this.m_expirationDays = long.MaxValue;
        this.m_permanentData = true;
        this.m_allowed = IsolatedStorageContainment.UnrestrictedIsolatedStorage;
      }
      else
      {
        if (state != PermissionState.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
        this.m_userQuota = 0L;
        this.m_machineQuota = 0L;
        this.m_expirationDays = 0L;
        this.m_permanentData = false;
        this.m_allowed = IsolatedStorageContainment.None;
      }
    }

    internal IsolatedStoragePermission(IsolatedStorageContainment UsageAllowed, long ExpirationDays, bool PermanentData)
    {
      this.m_userQuota = 0L;
      this.m_machineQuota = 0L;
      this.m_expirationDays = ExpirationDays;
      this.m_permanentData = PermanentData;
      this.m_allowed = UsageAllowed;
    }

    internal IsolatedStoragePermission(IsolatedStorageContainment UsageAllowed, long ExpirationDays, bool PermanentData, long UserQuota)
    {
      this.m_machineQuota = 0L;
      this.m_userQuota = UserQuota;
      this.m_expirationDays = ExpirationDays;
      this.m_permanentData = PermanentData;
      this.m_allowed = UsageAllowed;
    }

    /// <summary>
    ///   Получает или задает квоту для общего размера общего объема хранилища каждого пользователя.
    /// </summary>
    /// <returns>
    ///   Размер ресурсов, выделенных для пользователя, в байтах.
    /// </returns>
    public long UserQuota
    {
      set
      {
        this.m_userQuota = value;
      }
      get
      {
        return this.m_userQuota;
      }
    }

    /// <summary>
    ///   Возвращает или задает тип допустимого вложения изолированного хранилища.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Security.Permissions.IsolatedStorageContainment" />.
    /// </returns>
    public IsolatedStorageContainment UsageAllowed
    {
      set
      {
        this.m_allowed = value;
      }
      get
      {
        return this.m_allowed;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущее разрешение неограниченным.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущее разрешение является неограниченным. В противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsUnrestricted()
    {
      return this.m_allowed == IsolatedStorageContainment.UnrestrictedIsolatedStorage;
    }

    internal static long min(long x, long y)
    {
      if (x <= y)
        return x;
      return y;
    }

    internal static long max(long x, long y)
    {
      if (x >= y)
        return x;
      return y;
    }

    /// <summary>
    ///   Создает кодировку XML для разрешения и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Кодировка XML разрешения, включающая любые сведения о состоянии.
    /// </returns>
    public override SecurityElement ToXml()
    {
      return this.ToXml(this.GetType().FullName);
    }

    internal SecurityElement ToXml(string permName)
    {
      SecurityElement permissionElement = CodeAccessPermission.CreatePermissionElement((IPermission) this, permName);
      if (!this.IsUnrestricted())
      {
        permissionElement.AddAttribute("Allowed", Enum.GetName(typeof (IsolatedStorageContainment), (object) this.m_allowed));
        if (this.m_userQuota > 0L)
          permissionElement.AddAttribute("UserQuota", this.m_userQuota.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        if (this.m_machineQuota > 0L)
          permissionElement.AddAttribute("MachineQuota", this.m_machineQuota.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        if (this.m_expirationDays > 0L)
          permissionElement.AddAttribute("Expiry", this.m_expirationDays.ToString((IFormatProvider) CultureInfo.InvariantCulture));
        if (this.m_permanentData)
          permissionElement.AddAttribute("Permanent", this.m_permanentData.ToString());
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
      this.m_allowed = IsolatedStorageContainment.None;
      if (XMLUtil.IsUnrestricted(esd))
      {
        this.m_allowed = IsolatedStorageContainment.UnrestrictedIsolatedStorage;
      }
      else
      {
        string str = esd.Attribute("Allowed");
        if (str != null)
          this.m_allowed = (IsolatedStorageContainment) Enum.Parse(typeof (IsolatedStorageContainment), str);
      }
      if (this.m_allowed == IsolatedStorageContainment.UnrestrictedIsolatedStorage)
      {
        this.m_userQuota = long.MaxValue;
        this.m_machineQuota = long.MaxValue;
        this.m_expirationDays = long.MaxValue;
        this.m_permanentData = true;
      }
      else
      {
        string s1 = esd.Attribute("UserQuota");
        this.m_userQuota = s1 != null ? long.Parse(s1, (IFormatProvider) CultureInfo.InvariantCulture) : 0L;
        string s2 = esd.Attribute("MachineQuota");
        this.m_machineQuota = s2 != null ? long.Parse(s2, (IFormatProvider) CultureInfo.InvariantCulture) : 0L;
        string s3 = esd.Attribute("Expiry");
        this.m_expirationDays = s3 != null ? long.Parse(s3, (IFormatProvider) CultureInfo.InvariantCulture) : 0L;
        string str = esd.Attribute("Permanent");
        this.m_permanentData = str != null && bool.Parse(str);
      }
    }
  }
}

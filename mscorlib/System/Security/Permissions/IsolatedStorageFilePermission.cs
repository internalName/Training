// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.IsolatedStorageFilePermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Указывает разрешенное использование закрытой виртуальной файловой системы.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class IsolatedStorageFilePermission : IsolatedStoragePermission, IBuiltInPermission
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.IsolatedStorageFilePermission" /> с указанным состоянием разрешения: полностью ограниченное или неограниченное.
    /// </summary>
    /// <param name="state">
    ///   Одно из значений <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="state" /> не является допустимым значением для <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </exception>
    public IsolatedStorageFilePermission(PermissionState state)
      : base(state)
    {
    }

    internal IsolatedStorageFilePermission(IsolatedStorageContainment UsageAllowed, long ExpirationDays, bool PermanentData)
      : base(UsageAllowed, ExpirationDays, PermanentData)
    {
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
    /// </exception>
    public override IPermission Union(IPermission target)
    {
      if (target == null)
        return this.Copy();
      if (!this.VerifyType(target))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      IsolatedStorageFilePermission storageFilePermission1 = (IsolatedStorageFilePermission) target;
      if (this.IsUnrestricted() || storageFilePermission1.IsUnrestricted())
        return (IPermission) new IsolatedStorageFilePermission(PermissionState.Unrestricted);
      IsolatedStorageFilePermission storageFilePermission2 = new IsolatedStorageFilePermission(PermissionState.None);
      storageFilePermission2.m_userQuota = IsolatedStoragePermission.max(this.m_userQuota, storageFilePermission1.m_userQuota);
      storageFilePermission2.m_machineQuota = IsolatedStoragePermission.max(this.m_machineQuota, storageFilePermission1.m_machineQuota);
      storageFilePermission2.m_expirationDays = IsolatedStoragePermission.max(this.m_expirationDays, storageFilePermission1.m_expirationDays);
      storageFilePermission2.m_permanentData = this.m_permanentData || storageFilePermission1.m_permanentData;
      storageFilePermission2.m_allowed = (IsolatedStorageContainment) IsolatedStoragePermission.max((long) this.m_allowed, (long) storageFilePermission1.m_allowed);
      return (IPermission) storageFilePermission2;
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
      {
        if (this.m_userQuota == 0L && this.m_machineQuota == 0L && (this.m_expirationDays == 0L && !this.m_permanentData))
          return this.m_allowed == IsolatedStorageContainment.None;
        return false;
      }
      try
      {
        IsolatedStorageFilePermission storageFilePermission = (IsolatedStorageFilePermission) target;
        if (storageFilePermission.IsUnrestricted())
          return true;
        return storageFilePermission.m_userQuota >= this.m_userQuota && storageFilePermission.m_machineQuota >= this.m_machineQuota && storageFilePermission.m_expirationDays >= this.m_expirationDays && (storageFilePermission.m_permanentData || !this.m_permanentData) && storageFilePermission.m_allowed >= this.m_allowed;
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
    ///    Это новое разрешение равно <see langword="null" />, если пересечение является пустым.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="target" /> не равен <see langword="null" /> и имеет тип, не совпадающий с типом текущего разрешения.
    /// </exception>
    public override IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      if (!this.VerifyType(target))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      IsolatedStorageFilePermission storageFilePermission1 = (IsolatedStorageFilePermission) target;
      if (storageFilePermission1.IsUnrestricted())
        return this.Copy();
      if (this.IsUnrestricted())
        return target.Copy();
      IsolatedStorageFilePermission storageFilePermission2 = new IsolatedStorageFilePermission(PermissionState.None);
      storageFilePermission2.m_userQuota = IsolatedStoragePermission.min(this.m_userQuota, storageFilePermission1.m_userQuota);
      storageFilePermission2.m_machineQuota = IsolatedStoragePermission.min(this.m_machineQuota, storageFilePermission1.m_machineQuota);
      storageFilePermission2.m_expirationDays = IsolatedStoragePermission.min(this.m_expirationDays, storageFilePermission1.m_expirationDays);
      storageFilePermission2.m_permanentData = this.m_permanentData && storageFilePermission1.m_permanentData;
      storageFilePermission2.m_allowed = (IsolatedStorageContainment) IsolatedStoragePermission.min((long) this.m_allowed, (long) storageFilePermission1.m_allowed);
      if (storageFilePermission2.m_userQuota == 0L && storageFilePermission2.m_machineQuota == 0L && (storageFilePermission2.m_expirationDays == 0L && !storageFilePermission2.m_permanentData) && storageFilePermission2.m_allowed == IsolatedStorageContainment.None)
        return (IPermission) null;
      return (IPermission) storageFilePermission2;
    }

    /// <summary>
    ///   Создает и возвращает идентичную копию текущего разрешения.
    /// </summary>
    /// <returns>Копия текущего разрешения.</returns>
    public override IPermission Copy()
    {
      IsolatedStorageFilePermission storageFilePermission = new IsolatedStorageFilePermission(PermissionState.Unrestricted);
      if (!this.IsUnrestricted())
      {
        storageFilePermission.m_userQuota = this.m_userQuota;
        storageFilePermission.m_machineQuota = this.m_machineQuota;
        storageFilePermission.m_expirationDays = this.m_expirationDays;
        storageFilePermission.m_permanentData = this.m_permanentData;
        storageFilePermission.m_allowed = this.m_allowed;
      }
      return (IPermission) storageFilePermission;
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return IsolatedStorageFilePermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 3;
    }

    /// <summary>
    ///   Создает кодировку XML для разрешения и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Кодировка XML разрешения, включающая любые сведения о состоянии.
    /// </returns>
    [ComVisible(false)]
    public override SecurityElement ToXml()
    {
      return this.ToXml("System.Security.Permissions.IsolatedStorageFilePermission");
    }
  }
}

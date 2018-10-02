// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.HostProtectionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Позволяет применять декларативные действия по безопасности для определения требований защиты узла.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class HostProtectionAttribute : CodeAccessSecurityAttribute
  {
    private HostProtectionResource m_resources;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.HostProtectionAttribute" /> со значениями по умолчанию.
    /// </summary>
    public HostProtectionAttribute()
      : base(SecurityAction.LinkDemand)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.HostProtectionAttribute" /> заданным значением <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </summary>
    /// <param name="action">
    ///   Одно из значений <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="action" /> не является <see cref="F:System.Security.Permissions.SecurityAction.LinkDemand" />.
    /// </exception>
    public HostProtectionAttribute(SecurityAction action)
      : base(action)
    {
      if (action != SecurityAction.LinkDemand)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"));
    }

    /// <summary>
    ///   Возвращает или задает флаги, определяющие категории функциональных возможностей, которые могут быть потенциально опасны для узла.
    /// </summary>
    /// <returns>
    ///   Поразрядное сочетание значений <see cref="T:System.Security.Permissions.HostProtectionResource" />.
    ///    Значение по умолчанию — <see cref="F:System.Security.Permissions.HostProtectionResource.None" />.
    /// </returns>
    public HostProtectionResource Resources
    {
      get
      {
        return this.m_resources;
      }
      set
      {
        this.m_resources = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, предоставляется ли возможность синхронизации.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если возможность синхронизации предоставляется; в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    public bool Synchronization
    {
      get
      {
        return (uint) (this.m_resources & HostProtectionResource.Synchronization) > 0U;
      }
      set
      {
        this.m_resources = value ? this.m_resources | HostProtectionResource.Synchronization : this.m_resources & ~HostProtectionResource.Synchronization;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, предоставляется ли потокам возможность использовать общее состояние.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если потокам предоставляется возможность использовать общее состояние, в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    public bool SharedState
    {
      get
      {
        return (uint) (this.m_resources & HostProtectionResource.SharedState) > 0U;
      }
      set
      {
        this.m_resources = value ? this.m_resources | HostProtectionResource.SharedState : this.m_resources & ~HostProtectionResource.SharedState;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, предоставляется ли возможность управлять внешними процессами.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если возможность управлять внешними процессам предоставляется, в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    public bool ExternalProcessMgmt
    {
      get
      {
        return (uint) (this.m_resources & HostProtectionResource.ExternalProcessMgmt) > 0U;
      }
      set
      {
        this.m_resources = value ? this.m_resources | HostProtectionResource.ExternalProcessMgmt : this.m_resources & ~HostProtectionResource.ExternalProcessMgmt;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, предоставляется ли процессам возможность влиять на свое выполнение.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если процессам предоставляется возможность влиять на свое выполнение, в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    public bool SelfAffectingProcessMgmt
    {
      get
      {
        return (uint) (this.m_resources & HostProtectionResource.SelfAffectingProcessMgmt) > 0U;
      }
      set
      {
        this.m_resources = value ? this.m_resources | HostProtectionResource.SelfAffectingProcessMgmt : this.m_resources & ~HostProtectionResource.SelfAffectingProcessMgmt;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, предоставляется ли возможность управлять внешними потоками.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если возможность управлять внешними потоками предоставляется, в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    public bool ExternalThreading
    {
      get
      {
        return (uint) (this.m_resources & HostProtectionResource.ExternalThreading) > 0U;
      }
      set
      {
        this.m_resources = value ? this.m_resources | HostProtectionResource.ExternalThreading : this.m_resources & ~HostProtectionResource.ExternalThreading;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, предоставляется ли потокам возможность влиять на свое выполнение.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если потокам предоставляется возможность влиять на свое выполнение, в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    public bool SelfAffectingThreading
    {
      get
      {
        return (uint) (this.m_resources & HostProtectionResource.SelfAffectingThreading) > 0U;
      }
      set
      {
        this.m_resources = value ? this.m_resources | HostProtectionResource.SelfAffectingThreading : this.m_resources & ~HostProtectionResource.SelfAffectingThreading;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, предоставляется ли инфраструктура безопасности.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если инфраструктура безопасности предоставляется; в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    [ComVisible(true)]
    public bool SecurityInfrastructure
    {
      get
      {
        return (uint) (this.m_resources & HostProtectionResource.SecurityInfrastructure) > 0U;
      }
      set
      {
        this.m_resources = value ? this.m_resources | HostProtectionResource.SecurityInfrastructure : this.m_resources & ~HostProtectionResource.SecurityInfrastructure;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, предоставляется ли пользовательский интерфейс.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если пользовательский интерфейс предоставляется, в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="false" />.
    /// </returns>
    public bool UI
    {
      get
      {
        return (uint) (this.m_resources & HostProtectionResource.UI) > 0U;
      }
      set
      {
        this.m_resources = value ? this.m_resources | HostProtectionResource.UI : this.m_resources & ~HostProtectionResource.UI;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, определяющее, могут ли ресурсы допускать утечку памяти при прерывании операции.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если ресурсы могут допускать утечку памяти при прерывании операции, в противном случае — <see langword="false" />.
    /// </returns>
    public bool MayLeakOnAbort
    {
      get
      {
        return (uint) (this.m_resources & HostProtectionResource.MayLeakOnAbort) > 0U;
      }
      set
      {
        this.m_resources = value ? this.m_resources | HostProtectionResource.MayLeakOnAbort : this.m_resources & ~HostProtectionResource.MayLeakOnAbort;
      }
    }

    /// <summary>Создает и возвращает новое разрешение защиты узла.</summary>
    /// <returns>
    ///   Значение <see cref="T:System.Security.IPermission" />, соответствующее текущему атрибуту.
    /// </returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new HostProtectionPermission(PermissionState.Unrestricted);
      return (IPermission) new HostProtectionPermission(this.m_resources);
    }
  }
}

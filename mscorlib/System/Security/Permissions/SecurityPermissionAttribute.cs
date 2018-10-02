// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.SecurityPermissionAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Разрешает применять действия безопасности для <see cref="T:System.Security.Permissions.SecurityPermission" /> к коду с помощью декларативной безопасности.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  [Serializable]
  public sealed class SecurityPermissionAttribute : CodeAccessSecurityAttribute
  {
    private SecurityPermissionFlag m_flag;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.SecurityPermissionAttribute" /> указанным значением <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </summary>
    /// <param name="action">
    ///   Одно из значений <see cref="T:System.Security.Permissions.SecurityAction" />.
    /// </param>
    public SecurityPermissionAttribute(SecurityAction action)
      : base(action)
    {
    }

    /// <summary>
    ///   Возвращает или задает все флаги разрешения, составляющие разрешения <see cref="T:System.Security.Permissions.SecurityPermission" />.
    /// </summary>
    /// <returns>
    ///   Одно или несколько значений <see cref="T:System.Security.Permissions.SecurityPermissionFlag" />, объединенных с помощью битового оператора ИЛИ.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Предпринята попытка задать этому свойству недопустимое значение.
    ///    Допустимые значения см. в разделе <see cref="T:System.Security.Permissions.SecurityPermissionFlag" />.
    /// </exception>
    public SecurityPermissionFlag Flags
    {
      get
      {
        return this.m_flag;
      }
      set
      {
        this.m_flag = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, объявлено ли разрешение на подтверждение того, что все вызывающие объекты этого кода имеют необходимое разрешение на выполнение операции.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если разрешение на подтверждение объявлено; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool Assertion
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.Assertion) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.Assertion : this.m_flag & ~SecurityPermissionFlag.Assertion;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, объявлено ли разрешение на вызов неуправляемого кода.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если разрешение на вызов неуправляемого кода объявлено. В противном случае — значение <see langword="false" />.
    /// </returns>
    public bool UnmanagedCode
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.UnmanagedCode) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.UnmanagedCode : this.m_flag & ~SecurityPermissionFlag.UnmanagedCode;
      }
    }

    /// <summary>
    ///   Получает или задает значение, указывающее, объявлено ли разрешение на обход проверки кода.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если разрешение на обход проверки кода объявлено; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool SkipVerification
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.SkipVerification) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.SkipVerification : this.m_flag & ~SecurityPermissionFlag.SkipVerification;
      }
    }

    /// <summary>
    ///   Получает или задает значение, указывающее, объявлено ли разрешение на выполнение кода.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если разрешение на выполнение кода объявлено. В противном случае — значение <see langword="false" />.
    /// </returns>
    public bool Execution
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.Execution) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.Execution : this.m_flag & ~SecurityPermissionFlag.Execution;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, объявлено ли разрешение на обход управления потоками.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если разрешение на управление потоками объявлено; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool ControlThread
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.ControlThread) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.ControlThread : this.m_flag & ~SecurityPermissionFlag.ControlThread;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, объявлено ли разрешение на изменение или управление свидетельством.
    /// </summary>
    /// <returns>
    ///   <see langword="true" />, если возможность изменения или управления свидетельством объявлена. В противном случае — <see langword="false" />.
    /// </returns>
    public bool ControlEvidence
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.ControlEvidence) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.ControlEvidence : this.m_flag & ~SecurityPermissionFlag.ControlEvidence;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, объявлено ли разрешение на просмотр и управление политикой безопасности.
    /// </summary>
    /// <returns>
    ///   <see langword="true" />, если разрешение на управление политикой безопасности объявлено; в противном случае — <see langword="false" />.
    /// </returns>
    public bool ControlPolicy
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.ControlPolicy) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.ControlPolicy : this.m_flag & ~SecurityPermissionFlag.ControlPolicy;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, может ли код использовать модуль форматирования сериализации для сериализации или десериализации объекта.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если код может использовать модуль форматирования сериализации для сериализации или десериализации объекта; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool SerializationFormatter
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.SerializationFormatter) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.SerializationFormatter : this.m_flag & ~SecurityPermissionFlag.SerializationFormatter;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, объявлено ли разрешение на изменение или управление политикой безопасности домена.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если разрешение на изменение или управление политикой безопасности в домене приложения объявлено. В противном случае — значение <see langword="false" />.
    /// </returns>
    public bool ControlDomainPolicy
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.ControlDomainPolicy) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.ControlDomainPolicy : this.m_flag & ~SecurityPermissionFlag.ControlDomainPolicy;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, объявлено ли разрешение на управление текущим участником.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если разрешение на управление текущим участником объявлено; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool ControlPrincipal
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.ControlPrincipal) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.ControlPrincipal : this.m_flag & ~SecurityPermissionFlag.ControlPrincipal;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, объявлено ли разрешение на управление <see cref="T:System.AppDomain" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если разрешение на управление <see cref="T:System.AppDomain" /> объявлено; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool ControlAppDomain
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.ControlAppDomain) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.ControlAppDomain : this.m_flag & ~SecurityPermissionFlag.ControlAppDomain;
      }
    }

    /// <summary>
    ///   Получает или задает значение, указывающее, может ли код настраивать каналы и типы удаленного взаимодействия.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если код может настраивать каналы и типы удаленного взаимодействия; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool RemotingConfiguration
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.RemotingConfiguration) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.RemotingConfiguration : this.m_flag & ~SecurityPermissionFlag.RemotingConfiguration;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, может ли код подключиться к инфраструктуре среды CLR (например, добавление Remoting Context Sinks, Envoy Sinks и Dynamic Sinks).
    /// </summary>
    /// <returns>
    ///   <see langword="true" />, если код может подключиться к инфраструктуре среды CLR; в противном случае — <see langword="false" />.
    /// </returns>
    [ComVisible(true)]
    public bool Infrastructure
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.Infrastructure) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.Infrastructure : this.m_flag & ~SecurityPermissionFlag.Infrastructure;
      }
    }

    /// <summary>
    ///   Возвращает или задает значение, указывающее, имеет ли код разрешение на выполнение переадресации привязки в файле конфигурации приложения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если код может выполнять переадресации привязки; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool BindingRedirects
    {
      get
      {
        return (uint) (this.m_flag & SecurityPermissionFlag.BindingRedirects) > 0U;
      }
      set
      {
        this.m_flag = value ? this.m_flag | SecurityPermissionFlag.BindingRedirects : this.m_flag & ~SecurityPermissionFlag.BindingRedirects;
      }
    }

    /// <summary>
    ///   Создает и возвращает новый объект <see cref="T:System.Security.Permissions.SecurityPermission" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.Permissions.SecurityPermission" />, соответствующий этому атрибуту.
    /// </returns>
    public override IPermission CreatePermission()
    {
      if (this.m_unrestricted)
        return (IPermission) new SecurityPermission(PermissionState.Unrestricted);
      return (IPermission) new SecurityPermission(this.m_flag);
    }
  }
}

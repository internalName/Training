// Decompiled with JetBrains decompiler
// Type: System.Runtime.Hosting.ActivationArguments
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Runtime.Hosting
{
  /// <summary>
  ///   Предоставляет данные для активации на основе манифеста приложения.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class ActivationArguments : EvidenceBase
  {
    private bool m_useFusionActivationContext;
    private bool m_activateInstance;
    private string m_appFullName;
    private string[] m_appManifestPaths;
    private string[] m_activationData;

    private ActivationArguments()
    {
    }

    internal bool UseFusionActivationContext
    {
      get
      {
        return this.m_useFusionActivationContext;
      }
    }

    internal bool ActivateInstance
    {
      get
      {
        return this.m_activateInstance;
      }
      set
      {
        this.m_activateInstance = value;
      }
    }

    internal string ApplicationFullName
    {
      get
      {
        return this.m_appFullName;
      }
    }

    internal string[] ApplicationManifestPaths
    {
      get
      {
        return this.m_appManifestPaths;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Hosting.ActivationArguments" /> класса с заданной идентификации приложения.
    /// </summary>
    /// <param name="applicationIdentity">
    ///   Объект, определяющий приложение для активации на основе манифеста.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="applicationIdentity" /> имеет значение <see langword="null" />.
    /// </exception>
    public ActivationArguments(ApplicationIdentity applicationIdentity)
      : this(applicationIdentity, (string[]) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Hosting.ActivationArguments" /> класса с данными идентификации и активации указанного приложения.
    /// </summary>
    /// <param name="applicationIdentity">
    ///   Объект, определяющий приложение для активации на основе манифеста.
    /// </param>
    /// <param name="activationData">
    ///   Массив строк, содержащих данные активации хостом.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="applicationIdentity" /> имеет значение <see langword="null" />.
    /// </exception>
    public ActivationArguments(ApplicationIdentity applicationIdentity, string[] activationData)
    {
      if (applicationIdentity == null)
        throw new ArgumentNullException(nameof (applicationIdentity));
      this.m_appFullName = applicationIdentity.FullName;
      this.m_activationData = activationData;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Hosting.ActivationArguments" /> класса с заданным контекстом активации.
    /// </summary>
    /// <param name="activationData">
    ///   Объект, определяющий приложение для активации на основе манифеста.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="activationData" /> имеет значение <see langword="null" />.
    /// </exception>
    public ActivationArguments(ActivationContext activationData)
      : this(activationData, (string[]) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Hosting.ActivationArguments" /> класса с заданным контекстом активации и данные активации.
    /// </summary>
    /// <param name="activationContext">
    ///   Объект, определяющий приложение для активации на основе манифеста.
    /// </param>
    /// <param name="activationData">
    ///   Массив строк, содержащих данные активации хостом.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="activationContext" /> имеет значение <see langword="null" />.
    /// </exception>
    public ActivationArguments(ActivationContext activationContext, string[] activationData)
    {
      if (activationContext == null)
        throw new ArgumentNullException(nameof (activationContext));
      this.m_appFullName = activationContext.Identity.FullName;
      this.m_appManifestPaths = activationContext.ManifestPaths;
      this.m_activationData = activationData;
      this.m_useFusionActivationContext = true;
    }

    internal ActivationArguments(string appFullName, string[] appManifestPaths, string[] activationData)
    {
      if (appFullName == null)
        throw new ArgumentNullException(nameof (appFullName));
      this.m_appFullName = appFullName;
      this.m_appManifestPaths = appManifestPaths;
      this.m_activationData = activationData;
      this.m_useFusionActivationContext = true;
    }

    /// <summary>
    ///   Возвращает удостоверение приложения для манифеста приложения.
    /// </summary>
    /// <returns>
    ///   Объект, определяющий приложение для активации на основе манифеста.
    /// </returns>
    public ApplicationIdentity ApplicationIdentity
    {
      get
      {
        return new ApplicationIdentity(this.m_appFullName);
      }
    }

    /// <summary>
    ///   Возвращает контекст активации для активации на основе манифеста приложения.
    /// </summary>
    /// <returns>
    ///   Объект, определяющий приложение активации на основе манифеста.
    /// </returns>
    public ActivationContext ActivationContext
    {
      get
      {
        if (!this.UseFusionActivationContext)
          return (ActivationContext) null;
        if (this.m_appManifestPaths == null)
          return new ActivationContext(new ApplicationIdentity(this.m_appFullName));
        return new ActivationContext(new ApplicationIdentity(this.m_appFullName), this.m_appManifestPaths);
      }
    }

    /// <summary>Возвращает данные активации из узла.</summary>
    /// <returns>Массив строк, содержащих данные активации хостом.</returns>
    public string[] ActivationData
    {
      get
      {
        return this.m_activationData;
      }
    }

    /// <summary>
    ///   Создает копию текущего объекта <see cref="T:System.Runtime.Hosting.ActivationArguments" /> объекта.
    /// </summary>
    /// <returns>Копия текущего объекта.</returns>
    public override EvidenceBase Clone()
    {
      ActivationArguments activationArguments = new ActivationArguments();
      activationArguments.m_useFusionActivationContext = this.m_useFusionActivationContext;
      activationArguments.m_activateInstance = this.m_activateInstance;
      activationArguments.m_appFullName = this.m_appFullName;
      if (this.m_appManifestPaths != null)
      {
        activationArguments.m_appManifestPaths = new string[this.m_appManifestPaths.Length];
        Array.Copy((Array) this.m_appManifestPaths, (Array) activationArguments.m_appManifestPaths, activationArguments.m_appManifestPaths.Length);
      }
      if (this.m_activationData != null)
      {
        activationArguments.m_activationData = new string[this.m_activationData.Length];
        Array.Copy((Array) this.m_activationData, (Array) activationArguments.m_activationData, activationArguments.m_activationData.Length);
      }
      activationArguments.m_activateInstance = this.m_activateInstance;
      activationArguments.m_appFullName = this.m_appFullName;
      activationArguments.m_useFusionActivationContext = this.m_useFusionActivationContext;
      return (EvidenceBase) activationArguments;
    }
  }
}

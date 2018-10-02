// Decompiled with JetBrains decompiler
// Type: System.ActivationContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Deployment.Internal.Isolation;
using System.Deployment.Internal.Isolation.Manifest;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>
  ///   Идентифицирует контекст активации для текущего приложения.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(false)]
  [Serializable]
  public sealed class ActivationContext : IDisposable, ISerializable
  {
    private ApplicationIdentity _applicationIdentity;
    private ArrayList _definitionIdentities;
    private ArrayList _manifests;
    private string[] _manifestPaths;
    private ActivationContext.ContextForm _form;
    private ActivationContext.ApplicationStateDisposition _appRunState;
    private IActContext _actContext;
    private const int DefaultComponentCount = 2;

    private ActivationContext()
    {
    }

    [SecurityCritical]
    private ActivationContext(SerializationInfo info, StreamingContext context)
    {
      string applicationIdentityFullName = (string) info.GetValue("FullName", typeof (string));
      string[] manifestPaths = (string[]) info.GetValue(nameof (ManifestPaths), typeof (string[]));
      if (manifestPaths == null)
        this.CreateFromName(new ApplicationIdentity(applicationIdentityFullName));
      else
        this.CreateFromNameAndManifests(new ApplicationIdentity(applicationIdentityFullName), manifestPaths);
    }

    internal ActivationContext(ApplicationIdentity applicationIdentity)
    {
      this.CreateFromName(applicationIdentity);
    }

    internal ActivationContext(ApplicationIdentity applicationIdentity, string[] manifestPaths)
    {
      this.CreateFromNameAndManifests(applicationIdentity, manifestPaths);
    }

    [SecuritySafeCritical]
    private void CreateFromName(ApplicationIdentity applicationIdentity)
    {
      if (applicationIdentity == null)
        throw new ArgumentNullException(nameof (applicationIdentity));
      this._applicationIdentity = applicationIdentity;
      IEnumDefinitionIdentity definitionIdentity = this._applicationIdentity.Identity.EnumAppPath();
      this._definitionIdentities = new ArrayList(2);
      IDefinitionIdentity[] DefinitionIdentity = new IDefinitionIdentity[1];
      while (definitionIdentity.Next(1U, DefinitionIdentity) == 1U)
        this._definitionIdentities.Add((object) DefinitionIdentity[0]);
      this._definitionIdentities.TrimToSize();
      if (this._definitionIdentities.Count <= 1)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidAppId"));
      this._manifestPaths = (string[]) null;
      this._manifests = (ArrayList) null;
      this._actContext = IsolationInterop.CreateActContext(this._applicationIdentity.Identity);
      this._form = ActivationContext.ContextForm.StoreBounded;
      this._appRunState = ActivationContext.ApplicationStateDisposition.Undefined;
    }

    [SecuritySafeCritical]
    private void CreateFromNameAndManifests(ApplicationIdentity applicationIdentity, string[] manifestPaths)
    {
      if (applicationIdentity == null)
        throw new ArgumentNullException(nameof (applicationIdentity));
      if (manifestPaths == null)
        throw new ArgumentNullException(nameof (manifestPaths));
      this._applicationIdentity = applicationIdentity;
      IEnumDefinitionIdentity definitionIdentity = this._applicationIdentity.Identity.EnumAppPath();
      this._manifests = new ArrayList(2);
      this._manifestPaths = new string[manifestPaths.Length];
      IDefinitionIdentity[] DefinitionIdentity = new IDefinitionIdentity[1];
      int index = 0;
      while (definitionIdentity.Next(1U, DefinitionIdentity) == 1U)
      {
        ICMS manifest = (ICMS) IsolationInterop.ParseManifest(manifestPaths[index], (IManifestParseErrorCallback) null, ref IsolationInterop.IID_ICMS);
        if (!IsolationInterop.IdentityAuthority.AreDefinitionsEqual(0U, manifest.Identity, DefinitionIdentity[0]))
          throw new ArgumentException(Environment.GetResourceString("Argument_IllegalAppIdMismatch"));
        this._manifests.Add((object) manifest);
        this._manifestPaths[index] = manifestPaths[index];
        ++index;
      }
      if (index != manifestPaths.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_IllegalAppId"));
      this._manifests.TrimToSize();
      if (this._manifests.Count <= 1)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidAppId"));
      this._definitionIdentities = (ArrayList) null;
      this._actContext = (IActContext) null;
      this._form = ActivationContext.ContextForm.Loose;
      this._appRunState = ActivationContext.ApplicationStateDisposition.Undefined;
    }

    /// <summary>
    ///   Позволяет <see cref="T:System.ActivationContext" /> попытаться освободить ресурсы и выполнить другие операции очистки, перед тем как объект <see cref="T:System.ActivationContext" /> сборщиком мусора.
    /// </summary>
    ~ActivationContext()
    {
      this.Dispose(false);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.ActivationContext" /> класса с использованием удостоверения указанного приложения.
    /// </summary>
    /// <param name="identity">Объект, определяющий приложение.</param>
    /// <returns>Объект с удостоверением указанного приложения.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="identity" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Удостоверение развертывания или приложения задано в <paramref name="identity" />.
    /// </exception>
    public static ActivationContext CreatePartialActivationContext(ApplicationIdentity identity)
    {
      return new ActivationContext(identity);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.ActivationContext" /> класса с использованием удостоверения указанного приложения и массивом путей манифестов.
    /// </summary>
    /// <param name="identity">Объект, определяющий приложение.</param>
    /// <param name="manifestPaths">
    ///   Массив строк с путями манифеста приложения.
    /// </param>
    /// <returns>
    ///   Объект с удостоверением указанного приложения и массивом путей манифестов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="identity" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="manifestPaths" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Удостоверение развертывания или приложения задано в <paramref name="identity" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="identity" />не соответствует удостоверению в манифестах.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="identity" />не имеет одинаковое количество компонентов путями манифеста.
    /// </exception>
    public static ActivationContext CreatePartialActivationContext(ApplicationIdentity identity, string[] manifestPaths)
    {
      return new ActivationContext(identity, manifestPaths);
    }

    /// <summary>
    ///   Возвращает удостоверение приложения для текущего приложения.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.ApplicationIdentity" /> Идентифицирующий текущего приложения.
    /// </returns>
    public ApplicationIdentity Identity
    {
      get
      {
        return this._applicationIdentity;
      }
    }

    /// <summary>
    ///   Возвращает форму, или контекст хранения, для текущего приложения.
    /// </summary>
    /// <returns>Одно из значений перечисления.</returns>
    public ActivationContext.ContextForm Form
    {
      get
      {
        return this._form;
      }
    }

    /// <summary>
    ///   Возвращает манифест приложения ClickOnce для текущего приложения.
    /// </summary>
    /// <returns>
    ///   Массив байтов, который содержит манифест приложения ClickOnce для приложения, связанного с этим <see cref="T:System.ActivationContext" />.
    /// </returns>
    public byte[] ApplicationManifestBytes
    {
      get
      {
        return this.GetApplicationManifestBytes();
      }
    }

    /// <summary>
    ///   Получает манифест развертывания ClickOnce для текущего приложения.
    /// </summary>
    /// <returns>
    ///   Массив байтов, который содержит манифест развертывания ClickOnce для приложения, связанного с этим <see cref="T:System.ActivationContext" />.
    /// </returns>
    public byte[] DeploymentManifestBytes
    {
      get
      {
        return this.GetDeploymentManifestBytes();
      }
    }

    internal string[] ManifestPaths
    {
      get
      {
        return this._manifestPaths;
      }
    }

    /// <summary>
    ///   Освобождает все ресурсы, занятые модулем <see cref="T:System.ActivationContext" />.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    internal string ApplicationDirectory
    {
      [SecurityCritical] get
      {
        if (this._form == ActivationContext.ContextForm.Loose)
          return Path.GetDirectoryName(this._manifestPaths[this._manifestPaths.Length - 1]);
        string ApplicationPath;
        this._actContext.ApplicationBasePath(0U, out ApplicationPath);
        return ApplicationPath;
      }
    }

    internal string DataDirectory
    {
      [SecurityCritical] get
      {
        if (this._form == ActivationContext.ContextForm.Loose)
          return (string) null;
        string ppszPath;
        this._actContext.GetApplicationStateFilesystemLocation(1U, UIntPtr.Zero, IntPtr.Zero, out ppszPath);
        return ppszPath;
      }
    }

    internal ICMS ActivationContextData
    {
      [SecurityCritical] get
      {
        return this.ApplicationComponentManifest;
      }
    }

    internal ICMS DeploymentComponentManifest
    {
      [SecurityCritical] get
      {
        if (this._form == ActivationContext.ContextForm.Loose)
          return (ICMS) this._manifests[0];
        return this.GetComponentManifest((IDefinitionIdentity) this._definitionIdentities[0]);
      }
    }

    internal ICMS ApplicationComponentManifest
    {
      [SecurityCritical] get
      {
        if (this._form == ActivationContext.ContextForm.Loose)
          return (ICMS) this._manifests[this._manifests.Count - 1];
        return this.GetComponentManifest((IDefinitionIdentity) this._definitionIdentities[this._definitionIdentities.Count - 1]);
      }
    }

    internal ActivationContext.ApplicationStateDisposition LastApplicationStateResult
    {
      get
      {
        return this._appRunState;
      }
    }

    [SecurityCritical]
    internal ICMS GetComponentManifest(IDefinitionIdentity component)
    {
      object ManifestInteface;
      this._actContext.GetComponentManifest(0U, component, ref IsolationInterop.IID_ICMS, out ManifestInteface);
      return ManifestInteface as ICMS;
    }

    [SecuritySafeCritical]
    internal byte[] GetDeploymentManifestBytes()
    {
      string FullPath;
      if (this._form == ActivationContext.ContextForm.Loose)
      {
        FullPath = this._manifestPaths[0];
      }
      else
      {
        object ManifestInteface;
        this._actContext.GetComponentManifest(0U, (IDefinitionIdentity) this._definitionIdentities[0], ref IsolationInterop.IID_IManifestInformation, out ManifestInteface);
        ((IManifestInformation) ManifestInteface).get_FullPath(out FullPath);
        Marshal.ReleaseComObject(ManifestInteface);
      }
      return ActivationContext.ReadBytesFromFile(FullPath);
    }

    [SecuritySafeCritical]
    internal byte[] GetApplicationManifestBytes()
    {
      string FullPath;
      if (this._form == ActivationContext.ContextForm.Loose)
      {
        FullPath = this._manifestPaths[this._manifests.Count - 1];
      }
      else
      {
        object ManifestInteface;
        this._actContext.GetComponentManifest(0U, (IDefinitionIdentity) this._definitionIdentities[1], ref IsolationInterop.IID_IManifestInformation, out ManifestInteface);
        ((IManifestInformation) ManifestInteface).get_FullPath(out FullPath);
        Marshal.ReleaseComObject(ManifestInteface);
      }
      return ActivationContext.ReadBytesFromFile(FullPath);
    }

    [SecuritySafeCritical]
    internal void PrepareForExecution()
    {
      if (this._form == ActivationContext.ContextForm.Loose)
        return;
      this._actContext.PrepareForExecution(IntPtr.Zero, IntPtr.Zero);
    }

    [SecuritySafeCritical]
    internal ActivationContext.ApplicationStateDisposition SetApplicationState(ActivationContext.ApplicationState s)
    {
      if (this._form == ActivationContext.ContextForm.Loose)
        return ActivationContext.ApplicationStateDisposition.Undefined;
      uint ulDisposition;
      this._actContext.SetApplicationRunningState(0U, (uint) s, out ulDisposition);
      this._appRunState = (ActivationContext.ApplicationStateDisposition) ulDisposition;
      return this._appRunState;
    }

    [SecuritySafeCritical]
    private void Dispose(bool fDisposing)
    {
      this._applicationIdentity = (ApplicationIdentity) null;
      this._definitionIdentities = (ArrayList) null;
      this._manifests = (ArrayList) null;
      this._manifestPaths = (string[]) null;
      if (this._actContext == null)
        return;
      Marshal.ReleaseComObject((object) this._actContext);
    }

    private static byte[] ReadBytesFromFile(string manifestPath)
    {
      byte[] buffer = (byte[]) null;
      using (FileStream fileStream = new FileStream(manifestPath, FileMode.Open, FileAccess.Read))
      {
        int length = (int) fileStream.Length;
        buffer = new byte[length];
        if (fileStream.CanSeek)
          fileStream.Seek(0L, SeekOrigin.Begin);
        fileStream.Read(buffer, 0, length);
      }
      return buffer;
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (this._applicationIdentity != null)
        info.AddValue("FullName", (object) this._applicationIdentity.FullName, typeof (string));
      if (this._manifestPaths == null)
        return;
      info.AddValue("ManifestPaths", (object) this._manifestPaths, typeof (string[]));
    }

    /// <summary>Указывает контекст для манифеста приложения.</summary>
    public enum ContextForm
    {
      Loose,
      StoreBounded,
    }

    internal enum ApplicationState
    {
      Undefined,
      Starting,
      Running,
    }

    internal enum ApplicationStateDisposition
    {
      Undefined = 0,
      Starting = 1,
      Running = 2,
      StartingMigrated = 65537, // 0x00010001
      RunningFirstTime = 131074, // 0x00020002
    }
  }
}

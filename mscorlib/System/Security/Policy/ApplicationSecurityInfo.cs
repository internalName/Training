// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.ApplicationSecurityInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Deployment.Internal.Isolation;
using System.Deployment.Internal.Isolation.Manifest;
using System.Runtime.Hosting;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;
using System.Threading;

namespace System.Security.Policy
{
  /// <summary>
  ///   Содержит свидетельство безопасности для приложения.
  ///    Этот класс не наследуется.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
  public sealed class ApplicationSecurityInfo
  {
    private ActivationContext m_context;
    private object m_appId;
    private object m_deployId;
    private object m_defaultRequest;
    private object m_appEvidence;

    internal ApplicationSecurityInfo()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Policy.ApplicationSecurityInfo" /> класса с помощью предоставленного активации контекста.
    /// </summary>
    /// <param name="activationContext">
    ///   <see cref="T:System.ActivationContext" /> Объект, который однозначно определяет целевое приложение.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="activationContext" /> имеет значение <see langword="null" />.
    /// </exception>
    public ApplicationSecurityInfo(ActivationContext activationContext)
    {
      if (activationContext == null)
        throw new ArgumentNullException(nameof (activationContext));
      this.m_context = activationContext;
    }

    /// <summary>
    ///   Возвращает или задает информацию идентификации приложения.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.ApplicationId" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметру <see cref="P:System.Security.Policy.ApplicationSecurityInfo.ApplicationId" /> задается значение <see langword="null" />.
    /// </exception>
    public ApplicationId ApplicationId
    {
      get
      {
        if (this.m_appId == null && this.m_context != null)
          Interlocked.CompareExchange(ref this.m_appId, (object) ApplicationSecurityInfo.ParseApplicationId(this.m_context.ApplicationComponentManifest), (object) null);
        return this.m_appId as ApplicationId;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        this.m_appId = (object) value;
      }
    }

    /// <summary>
    ///   Возвращает или задает верхний элемент в приложении, который описан в идентификации развертывания.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.ApplicationId" /> Объект, описывающий элемент более высокого уровня приложения.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметру <see cref="P:System.Security.Policy.ApplicationSecurityInfo.DeploymentId" /> задается значение <see langword="null" />.
    /// </exception>
    public ApplicationId DeploymentId
    {
      get
      {
        if (this.m_deployId == null && this.m_context != null)
          Interlocked.CompareExchange(ref this.m_deployId, (object) ApplicationSecurityInfo.ParseApplicationId(this.m_context.DeploymentComponentManifest), (object) null);
        return this.m_deployId as ApplicationId;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        this.m_deployId = (object) value;
      }
    }

    /// <summary>
    ///   Возвращает или задает набор разрешений по умолчанию.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.PermissionSet" /> объект, представляющий разрешения по умолчанию для приложения.
    ///    Значение по умолчанию — <see cref="T:System.Security.PermissionSet" /> с состоянием разрешения <see cref="F:System.Security.Permissions.PermissionState.None" />
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметру <see cref="P:System.Security.Policy.ApplicationSecurityInfo.DefaultRequestSet" /> задается значение <see langword="null" />.
    /// </exception>
    public PermissionSet DefaultRequestSet
    {
      get
      {
        if (this.m_defaultRequest == null)
        {
          PermissionSet permissionSet1 = new PermissionSet(PermissionState.None);
          if (this.m_context != null)
          {
            ICMS componentManifest = this.m_context.ApplicationComponentManifest;
            string defaultPermissionSetId = ((IMetadataSectionEntry) componentManifest.MetadataSectionEntry).defaultPermissionSetID;
            object ppUnknown = (object) null;
            if (defaultPermissionSetId != null && defaultPermissionSetId.Length > 0)
            {
              ((ISectionWithStringKey) componentManifest.PermissionSetSection).Lookup(defaultPermissionSetId, out ppUnknown);
              IPermissionSetEntry permissionSetEntry = ppUnknown as IPermissionSetEntry;
              if (permissionSetEntry != null)
              {
                SecurityElement permissionSetXml = SecurityElement.FromString(permissionSetEntry.AllData.XmlSegment);
                string str = permissionSetXml.Attribute("temp:Unrestricted");
                if (str != null)
                  permissionSetXml.AddAttribute("Unrestricted", str);
                if (string.Compare(permissionSetXml.Attribute("SameSite"), "Site", StringComparison.OrdinalIgnoreCase) == 0)
                {
                  Url url = new Url(this.m_context.Identity.CodeBase);
                  URLString urlString = url.GetURLString();
                  SecurityElement webPermission = new NetCodeGroup((IMembershipCondition) new AllMembershipCondition()).CreateWebPermission(urlString.Host, urlString.Scheme, urlString.Port, "System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
                  if (webPermission != null)
                    permissionSetXml.AddChild(webPermission);
                  if (string.Compare("file:", 0, this.m_context.Identity.CodeBase, 0, 5, StringComparison.OrdinalIgnoreCase) == 0)
                  {
                    PolicyStatement policy = new FileCodeGroup((IMembershipCondition) new AllMembershipCondition(), FileIOPermissionAccess.Read | FileIOPermissionAccess.PathDiscovery).CalculatePolicy(url);
                    if (policy != null)
                    {
                      PermissionSet permissionSet2 = policy.PermissionSet;
                      if (permissionSet2 != null)
                        permissionSetXml.AddChild(permissionSet2.GetPermission(typeof (FileIOPermission)).ToXml());
                    }
                  }
                }
                permissionSet1 = (PermissionSet) new ReadOnlyPermissionSet(permissionSetXml);
              }
            }
          }
          Interlocked.CompareExchange(ref this.m_defaultRequest, (object) permissionSet1, (object) null);
        }
        return this.m_defaultRequest as PermissionSet;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        this.m_defaultRequest = (object) value;
      }
    }

    /// <summary>Возвращает или задает свидетельство для приложения.</summary>
    /// <returns>
    ///   <see cref="T:System.Security.Policy.Evidence" /> Объект для приложения.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметру <see cref="P:System.Security.Policy.ApplicationSecurityInfo.ApplicationEvidence" /> задается значение <see langword="null" />.
    /// </exception>
    public Evidence ApplicationEvidence
    {
      get
      {
        if (this.m_appEvidence == null)
        {
          Evidence evidence1 = new Evidence();
          if (this.m_context != null)
          {
            evidence1 = new Evidence();
            Url evidence2 = new Url(this.m_context.Identity.CodeBase);
            evidence1.AddHostEvidence<Url>(evidence2);
            evidence1.AddHostEvidence<Zone>(Zone.CreateFromUrl(this.m_context.Identity.CodeBase));
            if (string.Compare("file:", 0, this.m_context.Identity.CodeBase, 0, 5, StringComparison.OrdinalIgnoreCase) != 0)
              evidence1.AddHostEvidence<Site>(Site.CreateFromUrl(this.m_context.Identity.CodeBase));
            evidence1.AddHostEvidence<StrongName>(new StrongName(new StrongNamePublicKeyBlob(this.DeploymentId.m_publicKeyToken), this.DeploymentId.Name, this.DeploymentId.Version));
            evidence1.AddHostEvidence<ActivationArguments>(new ActivationArguments(this.m_context));
          }
          Interlocked.CompareExchange(ref this.m_appEvidence, (object) evidence1, (object) null);
        }
        return this.m_appEvidence as Evidence;
      }
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        this.m_appEvidence = (object) value;
      }
    }

    private static ApplicationId ParseApplicationId(ICMS manifest)
    {
      if (manifest.Identity == null)
        return (ApplicationId) null;
      return new ApplicationId(Hex.DecodeHexString(manifest.Identity.GetAttribute("", "publicKeyToken")), manifest.Identity.GetAttribute("", "name"), new Version(manifest.Identity.GetAttribute("", "version")), manifest.Identity.GetAttribute("", "processorArchitecture"), manifest.Identity.GetAttribute("", "culture"));
    }
  }
}

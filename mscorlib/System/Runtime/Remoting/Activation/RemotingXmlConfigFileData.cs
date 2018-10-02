// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Activation.RemotingXmlConfigFileData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Reflection;

namespace System.Runtime.Remoting.Activation
{
  internal class RemotingXmlConfigFileData
  {
    internal bool UrlObjRefMode = RemotingConfigHandler.UrlObjRefMode;
    internal ArrayList ChannelEntries = new ArrayList();
    internal ArrayList InteropXmlElementEntries = new ArrayList();
    internal ArrayList InteropXmlTypeEntries = new ArrayList();
    internal ArrayList PreLoadEntries = new ArrayList();
    internal ArrayList RemoteAppEntries = new ArrayList();
    internal ArrayList ServerActivatedEntries = new ArrayList();
    internal ArrayList ServerWellKnownEntries = new ArrayList();
    internal static volatile bool LoadTypes;
    internal string ApplicationName;
    internal RemotingXmlConfigFileData.LifetimeEntry Lifetime;
    internal RemotingXmlConfigFileData.CustomErrorsEntry CustomErrors;

    internal void AddInteropXmlElementEntry(string xmlElementName, string xmlElementNamespace, string urtTypeName, string urtAssemblyName)
    {
      this.TryToLoadTypeIfApplicable(urtTypeName, urtAssemblyName);
      this.InteropXmlElementEntries.Add((object) new RemotingXmlConfigFileData.InteropXmlElementEntry(xmlElementName, xmlElementNamespace, urtTypeName, urtAssemblyName));
    }

    internal void AddInteropXmlTypeEntry(string xmlTypeName, string xmlTypeNamespace, string urtTypeName, string urtAssemblyName)
    {
      this.TryToLoadTypeIfApplicable(urtTypeName, urtAssemblyName);
      this.InteropXmlTypeEntries.Add((object) new RemotingXmlConfigFileData.InteropXmlTypeEntry(xmlTypeName, xmlTypeNamespace, urtTypeName, urtAssemblyName));
    }

    internal void AddPreLoadEntry(string typeName, string assemblyName)
    {
      this.TryToLoadTypeIfApplicable(typeName, assemblyName);
      this.PreLoadEntries.Add((object) new RemotingXmlConfigFileData.PreLoadEntry(typeName, assemblyName));
    }

    internal RemotingXmlConfigFileData.RemoteAppEntry AddRemoteAppEntry(string appUri)
    {
      RemotingXmlConfigFileData.RemoteAppEntry remoteAppEntry = new RemotingXmlConfigFileData.RemoteAppEntry(appUri);
      this.RemoteAppEntries.Add((object) remoteAppEntry);
      return remoteAppEntry;
    }

    internal void AddServerActivatedEntry(string typeName, string assemName, ArrayList contextAttributes)
    {
      this.TryToLoadTypeIfApplicable(typeName, assemName);
      this.ServerActivatedEntries.Add((object) new RemotingXmlConfigFileData.TypeEntry(typeName, assemName, contextAttributes));
    }

    internal RemotingXmlConfigFileData.ServerWellKnownEntry AddServerWellKnownEntry(string typeName, string assemName, ArrayList contextAttributes, string objURI, WellKnownObjectMode objMode)
    {
      this.TryToLoadTypeIfApplicable(typeName, assemName);
      RemotingXmlConfigFileData.ServerWellKnownEntry serverWellKnownEntry = new RemotingXmlConfigFileData.ServerWellKnownEntry(typeName, assemName, contextAttributes, objURI, objMode);
      this.ServerWellKnownEntries.Add((object) serverWellKnownEntry);
      return serverWellKnownEntry;
    }

    private void TryToLoadTypeIfApplicable(string typeName, string assemblyName)
    {
      if (!RemotingXmlConfigFileData.LoadTypes)
        return;
      Assembly assembly = Assembly.Load(assemblyName);
      if (assembly == (Assembly) null)
        throw new RemotingException(Environment.GetResourceString("Remoting_AssemblyLoadFailed", (object) assemblyName));
      if (assembly.GetType(typeName, false, false) == (Type) null)
        throw new RemotingException(Environment.GetResourceString("Remoting_BadType", (object) typeName));
    }

    internal class ChannelEntry
    {
      internal ArrayList ClientSinkProviders = new ArrayList();
      internal ArrayList ServerSinkProviders = new ArrayList();
      internal string TypeName;
      internal string AssemblyName;
      internal Hashtable Properties;
      internal bool DelayLoad;

      internal ChannelEntry(string typeName, string assemblyName, Hashtable properties)
      {
        this.TypeName = typeName;
        this.AssemblyName = assemblyName;
        this.Properties = properties;
      }
    }

    internal class ClientWellKnownEntry
    {
      internal string TypeName;
      internal string AssemblyName;
      internal string Url;

      internal ClientWellKnownEntry(string typeName, string assemName, string url)
      {
        this.TypeName = typeName;
        this.AssemblyName = assemName;
        this.Url = url;
      }
    }

    internal class ContextAttributeEntry
    {
      internal string TypeName;
      internal string AssemblyName;
      internal Hashtable Properties;

      internal ContextAttributeEntry(string typeName, string assemName, Hashtable properties)
      {
        this.TypeName = typeName;
        this.AssemblyName = assemName;
        this.Properties = properties;
      }
    }

    internal class InteropXmlElementEntry
    {
      internal string XmlElementName;
      internal string XmlElementNamespace;
      internal string UrtTypeName;
      internal string UrtAssemblyName;

      internal InteropXmlElementEntry(string xmlElementName, string xmlElementNamespace, string urtTypeName, string urtAssemblyName)
      {
        this.XmlElementName = xmlElementName;
        this.XmlElementNamespace = xmlElementNamespace;
        this.UrtTypeName = urtTypeName;
        this.UrtAssemblyName = urtAssemblyName;
      }
    }

    internal class CustomErrorsEntry
    {
      internal CustomErrorsModes Mode;

      internal CustomErrorsEntry(CustomErrorsModes mode)
      {
        this.Mode = mode;
      }
    }

    internal class InteropXmlTypeEntry
    {
      internal string XmlTypeName;
      internal string XmlTypeNamespace;
      internal string UrtTypeName;
      internal string UrtAssemblyName;

      internal InteropXmlTypeEntry(string xmlTypeName, string xmlTypeNamespace, string urtTypeName, string urtAssemblyName)
      {
        this.XmlTypeName = xmlTypeName;
        this.XmlTypeNamespace = xmlTypeNamespace;
        this.UrtTypeName = urtTypeName;
        this.UrtAssemblyName = urtAssemblyName;
      }
    }

    internal class LifetimeEntry
    {
      internal bool IsLeaseTimeSet;
      internal bool IsRenewOnCallTimeSet;
      internal bool IsSponsorshipTimeoutSet;
      internal bool IsLeaseManagerPollTimeSet;
      private TimeSpan _leaseTime;
      private TimeSpan _renewOnCallTime;
      private TimeSpan _sponsorshipTimeout;
      private TimeSpan _leaseManagerPollTime;

      internal TimeSpan LeaseTime
      {
        get
        {
          return this._leaseTime;
        }
        set
        {
          this._leaseTime = value;
          this.IsLeaseTimeSet = true;
        }
      }

      internal TimeSpan RenewOnCallTime
      {
        get
        {
          return this._renewOnCallTime;
        }
        set
        {
          this._renewOnCallTime = value;
          this.IsRenewOnCallTimeSet = true;
        }
      }

      internal TimeSpan SponsorshipTimeout
      {
        get
        {
          return this._sponsorshipTimeout;
        }
        set
        {
          this._sponsorshipTimeout = value;
          this.IsSponsorshipTimeoutSet = true;
        }
      }

      internal TimeSpan LeaseManagerPollTime
      {
        get
        {
          return this._leaseManagerPollTime;
        }
        set
        {
          this._leaseManagerPollTime = value;
          this.IsLeaseManagerPollTimeSet = true;
        }
      }
    }

    internal class PreLoadEntry
    {
      internal string TypeName;
      internal string AssemblyName;

      public PreLoadEntry(string typeName, string assemblyName)
      {
        this.TypeName = typeName;
        this.AssemblyName = assemblyName;
      }
    }

    internal class RemoteAppEntry
    {
      internal ArrayList WellKnownObjects = new ArrayList();
      internal ArrayList ActivatedObjects = new ArrayList();
      internal string AppUri;

      internal RemoteAppEntry(string appUri)
      {
        this.AppUri = appUri;
      }

      internal void AddWellKnownEntry(string typeName, string assemName, string url)
      {
        this.WellKnownObjects.Add((object) new RemotingXmlConfigFileData.ClientWellKnownEntry(typeName, assemName, url));
      }

      internal void AddActivatedEntry(string typeName, string assemName, ArrayList contextAttributes)
      {
        this.ActivatedObjects.Add((object) new RemotingXmlConfigFileData.TypeEntry(typeName, assemName, contextAttributes));
      }
    }

    internal class ServerWellKnownEntry : RemotingXmlConfigFileData.TypeEntry
    {
      internal string ObjectURI;
      internal WellKnownObjectMode ObjectMode;

      internal ServerWellKnownEntry(string typeName, string assemName, ArrayList contextAttributes, string objURI, WellKnownObjectMode objMode)
        : base(typeName, assemName, contextAttributes)
      {
        this.ObjectURI = objURI;
        this.ObjectMode = objMode;
      }
    }

    internal class SinkProviderEntry
    {
      internal ArrayList ProviderData = new ArrayList();
      internal string TypeName;
      internal string AssemblyName;
      internal Hashtable Properties;
      internal bool IsFormatter;

      internal SinkProviderEntry(string typeName, string assemName, Hashtable properties, bool isFormatter)
      {
        this.TypeName = typeName;
        this.AssemblyName = assemName;
        this.Properties = properties;
        this.IsFormatter = isFormatter;
      }
    }

    internal class TypeEntry
    {
      internal string TypeName;
      internal string AssemblyName;
      internal ArrayList ContextAttributes;

      internal TypeEntry(string typeName, string assemName, ArrayList contextAttributes)
      {
        this.TypeName = typeName;
        this.AssemblyName = assemName;
        this.ContextAttributes = contextAttributes;
      }
    }
  }
}

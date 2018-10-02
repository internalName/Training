// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.RemotingConfigHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Security;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Runtime.Remoting
{
  internal static class RemotingConfigHandler
  {
    private static volatile CustomErrorsModes _errorMode = CustomErrorsModes.RemoteOnly;
    private static volatile bool _errorsModeSet = false;
    private static volatile bool _bMachineConfigLoaded = false;
    private static volatile bool _bUrlObjRefMode = false;
    private static Queue _delayLoadChannelConfigQueue = new Queue();
    public static RemotingConfigHandler.RemotingConfigInfo Info = new RemotingConfigHandler.RemotingConfigInfo();
    private static volatile string _applicationName;
    private const string _machineConfigFilename = "machine.config";

    internal static string ApplicationName
    {
      get
      {
        if (RemotingConfigHandler._applicationName == null)
          throw new RemotingException(Environment.GetResourceString("Remoting_Config_NoAppName"));
        return RemotingConfigHandler._applicationName;
      }
      set
      {
        if (RemotingConfigHandler._applicationName != null)
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_AppNameSet"), (object) RemotingConfigHandler._applicationName));
        RemotingConfigHandler._applicationName = value;
        char[] chArray = new char[1]{ '/' };
        if (RemotingConfigHandler._applicationName.StartsWith("/", StringComparison.Ordinal))
          RemotingConfigHandler._applicationName = RemotingConfigHandler._applicationName.TrimStart(chArray);
        if (!RemotingConfigHandler._applicationName.EndsWith("/", StringComparison.Ordinal))
          return;
        RemotingConfigHandler._applicationName = RemotingConfigHandler._applicationName.TrimEnd(chArray);
      }
    }

    internal static bool HasApplicationNameBeenSet()
    {
      return RemotingConfigHandler._applicationName != null;
    }

    internal static bool UrlObjRefMode
    {
      get
      {
        return RemotingConfigHandler._bUrlObjRefMode;
      }
    }

    internal static CustomErrorsModes CustomErrorsMode
    {
      get
      {
        return RemotingConfigHandler._errorMode;
      }
      set
      {
        if (RemotingConfigHandler._errorsModeSet)
          throw new RemotingException(Environment.GetResourceString("Remoting_Config_ErrorsModeSet"));
        RemotingConfigHandler._errorMode = value;
        RemotingConfigHandler._errorsModeSet = true;
      }
    }

    [SecurityCritical]
    internal static IMessageSink FindDelayLoadChannelForCreateMessageSink(string url, object data, out string objectURI)
    {
      RemotingConfigHandler.LoadMachineConfigIfNecessary();
      objectURI = (string) null;
      foreach (DelayLoadClientChannelEntry loadChannelConfig in RemotingConfigHandler._delayLoadChannelConfigQueue)
      {
        IChannelSender channel = loadChannelConfig.Channel;
        if (channel != null)
        {
          IMessageSink messageSink = channel.CreateMessageSink(url, data, out objectURI);
          if (messageSink != null)
          {
            loadChannelConfig.RegisterChannel();
            return messageSink;
          }
        }
      }
      return (IMessageSink) null;
    }

    [SecurityCritical]
    private static void LoadMachineConfigIfNecessary()
    {
      if (RemotingConfigHandler._bMachineConfigLoaded)
        return;
      lock (RemotingConfigHandler.Info)
      {
        if (RemotingConfigHandler._bMachineConfigLoaded)
          return;
        RemotingXmlConfigFileData defaultConfiguration = RemotingXmlConfigFileParser.ParseDefaultConfiguration();
        if (defaultConfiguration != null)
          RemotingConfigHandler.ConfigureRemoting(defaultConfiguration, false);
        string str = Config.MachineDirectory + "machine.config";
        new FileIOPermission(FileIOPermissionAccess.Read, str).Assert();
        RemotingXmlConfigFileData configData = RemotingConfigHandler.LoadConfigurationFromXmlFile(str);
        if (configData != null)
          RemotingConfigHandler.ConfigureRemoting(configData, false);
        RemotingConfigHandler._bMachineConfigLoaded = true;
      }
    }

    [SecurityCritical]
    internal static void DoConfiguration(string filename, bool ensureSecurity)
    {
      RemotingConfigHandler.LoadMachineConfigIfNecessary();
      RemotingXmlConfigFileData configData = RemotingConfigHandler.LoadConfigurationFromXmlFile(filename);
      if (configData == null)
        return;
      RemotingConfigHandler.ConfigureRemoting(configData, ensureSecurity);
    }

    private static RemotingXmlConfigFileData LoadConfigurationFromXmlFile(string filename)
    {
      try
      {
        if (filename != null)
          return RemotingXmlConfigFileParser.ParseConfigFile(filename);
        return (RemotingXmlConfigFileData) null;
      }
      catch (Exception ex)
      {
        Exception exception = ex;
        Exception innerException = (Exception) (exception.InnerException as FileNotFoundException);
        if (innerException != null)
          exception = innerException;
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_ReadFailure"), (object) filename, (object) exception));
      }
    }

    [SecurityCritical]
    private static void ConfigureRemoting(RemotingXmlConfigFileData configData, bool ensureSecurity)
    {
      try
      {
        string applicationName = configData.ApplicationName;
        if (applicationName != null)
          RemotingConfigHandler.ApplicationName = applicationName;
        if (configData.CustomErrors != null)
          RemotingConfigHandler._errorMode = configData.CustomErrors.Mode;
        RemotingConfigHandler.ConfigureChannels(configData, ensureSecurity);
        if (configData.Lifetime != null)
        {
          if (configData.Lifetime.IsLeaseTimeSet)
            LifetimeServices.LeaseTime = configData.Lifetime.LeaseTime;
          if (configData.Lifetime.IsRenewOnCallTimeSet)
            LifetimeServices.RenewOnCallTime = configData.Lifetime.RenewOnCallTime;
          if (configData.Lifetime.IsSponsorshipTimeoutSet)
            LifetimeServices.SponsorshipTimeout = configData.Lifetime.SponsorshipTimeout;
          if (configData.Lifetime.IsLeaseManagerPollTimeSet)
            LifetimeServices.LeaseManagerPollTime = configData.Lifetime.LeaseManagerPollTime;
        }
        RemotingConfigHandler._bUrlObjRefMode = configData.UrlObjRefMode;
        RemotingConfigHandler.Info.StoreRemoteAppEntries(configData);
        RemotingConfigHandler.Info.StoreActivatedExports(configData);
        RemotingConfigHandler.Info.StoreInteropEntries(configData);
        RemotingConfigHandler.Info.StoreWellKnownExports(configData);
        if (configData.ServerActivatedEntries.Count <= 0)
          return;
        ActivationServices.StartListeningForRemoteRequests();
      }
      catch (Exception ex)
      {
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_ConfigurationFailure"), (object) ex));
      }
    }

    [SecurityCritical]
    private static void ConfigureChannels(RemotingXmlConfigFileData configData, bool ensureSecurity)
    {
      RemotingServices.RegisterWellKnownChannels();
      foreach (RemotingXmlConfigFileData.ChannelEntry channelEntry in configData.ChannelEntries)
      {
        if (!channelEntry.DelayLoad)
          ChannelServices.RegisterChannel(RemotingConfigHandler.CreateChannelFromConfigEntry(channelEntry), ensureSecurity);
        else
          RemotingConfigHandler._delayLoadChannelConfigQueue.Enqueue((object) new DelayLoadClientChannelEntry(channelEntry, ensureSecurity));
      }
    }

    [SecurityCritical]
    internal static IChannel CreateChannelFromConfigEntry(RemotingXmlConfigFileData.ChannelEntry entry)
    {
      Type type = RemotingConfigHandler.RemotingConfigInfo.LoadType(entry.TypeName, entry.AssemblyName);
      bool flag1 = typeof (IChannelReceiver).IsAssignableFrom(type);
      bool flag2 = typeof (IChannelSender).IsAssignableFrom(type);
      IClientChannelSinkProvider channelSinkProvider1 = (IClientChannelSinkProvider) null;
      IServerChannelSinkProvider channelSinkProvider2 = (IServerChannelSinkProvider) null;
      if (entry.ClientSinkProviders.Count > 0)
        channelSinkProvider1 = RemotingConfigHandler.CreateClientChannelSinkProviderChain(entry.ClientSinkProviders);
      if (entry.ServerSinkProviders.Count > 0)
        channelSinkProvider2 = RemotingConfigHandler.CreateServerChannelSinkProviderChain(entry.ServerSinkProviders);
      object[] args;
      if (flag1 & flag2)
        args = new object[3]
        {
          (object) entry.Properties,
          (object) channelSinkProvider1,
          (object) channelSinkProvider2
        };
      else if (flag1)
      {
        args = new object[2]
        {
          (object) entry.Properties,
          (object) channelSinkProvider2
        };
      }
      else
      {
        if (!flag2)
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_InvalidChannelType"), (object) type.FullName));
        args = new object[2]
        {
          (object) entry.Properties,
          (object) channelSinkProvider1
        };
      }
      try
      {
        return (IChannel) Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, (Binder) null, args, (CultureInfo) null, (object[]) null);
      }
      catch (MissingMethodException ex)
      {
        string str = (string) null;
        if (flag1 & flag2)
          str = "MyChannel(IDictionary properties, IClientChannelSinkProvider clientSinkProvider, IServerChannelSinkProvider serverSinkProvider)";
        else if (flag1)
          str = "MyChannel(IDictionary properties, IServerChannelSinkProvider serverSinkProvider)";
        else if (flag2)
          str = "MyChannel(IDictionary properties, IClientChannelSinkProvider clientSinkProvider)";
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_ChannelMissingCtor"), (object) type.FullName, (object) str));
      }
    }

    [SecurityCritical]
    private static IClientChannelSinkProvider CreateClientChannelSinkProviderChain(ArrayList entries)
    {
      IClientChannelSinkProvider channelSinkProvider1 = (IClientChannelSinkProvider) null;
      IClientChannelSinkProvider channelSinkProvider2 = (IClientChannelSinkProvider) null;
      foreach (RemotingXmlConfigFileData.SinkProviderEntry entry in entries)
      {
        if (channelSinkProvider1 == null)
        {
          channelSinkProvider1 = (IClientChannelSinkProvider) RemotingConfigHandler.CreateChannelSinkProvider(entry, false);
          channelSinkProvider2 = channelSinkProvider1;
        }
        else
        {
          channelSinkProvider2.Next = (IClientChannelSinkProvider) RemotingConfigHandler.CreateChannelSinkProvider(entry, false);
          channelSinkProvider2 = channelSinkProvider2.Next;
        }
      }
      return channelSinkProvider1;
    }

    [SecurityCritical]
    private static IServerChannelSinkProvider CreateServerChannelSinkProviderChain(ArrayList entries)
    {
      IServerChannelSinkProvider channelSinkProvider1 = (IServerChannelSinkProvider) null;
      IServerChannelSinkProvider channelSinkProvider2 = (IServerChannelSinkProvider) null;
      foreach (RemotingXmlConfigFileData.SinkProviderEntry entry in entries)
      {
        if (channelSinkProvider1 == null)
        {
          channelSinkProvider1 = (IServerChannelSinkProvider) RemotingConfigHandler.CreateChannelSinkProvider(entry, true);
          channelSinkProvider2 = channelSinkProvider1;
        }
        else
        {
          channelSinkProvider2.Next = (IServerChannelSinkProvider) RemotingConfigHandler.CreateChannelSinkProvider(entry, true);
          channelSinkProvider2 = channelSinkProvider2.Next;
        }
      }
      return channelSinkProvider1;
    }

    [SecurityCritical]
    private static object CreateChannelSinkProvider(RemotingXmlConfigFileData.SinkProviderEntry entry, bool bServer)
    {
      Type type = RemotingConfigHandler.RemotingConfigInfo.LoadType(entry.TypeName, entry.AssemblyName);
      if (bServer)
      {
        if (!typeof (IServerChannelSinkProvider).IsAssignableFrom(type))
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_InvalidSinkProviderType"), (object) type.FullName, (object) "IServerChannelSinkProvider"));
      }
      else if (!typeof (IClientChannelSinkProvider).IsAssignableFrom(type))
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_InvalidSinkProviderType"), (object) type.FullName, (object) "IClientChannelSinkProvider"));
      if (entry.IsFormatter && (bServer && !typeof (IServerFormatterSinkProvider).IsAssignableFrom(type) || !bServer && !typeof (IClientFormatterSinkProvider).IsAssignableFrom(type)))
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_SinkProviderNotFormatter"), (object) type.FullName));
      object[] args = new object[2]
      {
        (object) entry.Properties,
        (object) entry.ProviderData
      };
      try
      {
        return Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance, (Binder) null, args, (CultureInfo) null, (object[]) null);
      }
      catch (MissingMethodException ex)
      {
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_SinkProviderMissingCtor"), (object) type.FullName, (object) "MySinkProvider(IDictionary properties, ICollection providerData)"));
      }
    }

    [SecurityCritical]
    internal static ActivatedClientTypeEntry IsRemotelyActivatedClientType(RuntimeType svrType)
    {
      RemotingTypeCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(svrType);
      string simpleAssemblyName = reflectionCachedData.SimpleAssemblyName;
      ActivatedClientTypeEntry activatedClientTypeEntry = RemotingConfigHandler.Info.QueryRemoteActivate(svrType.FullName, simpleAssemblyName);
      if (activatedClientTypeEntry == null)
      {
        string assemblyName = reflectionCachedData.AssemblyName;
        activatedClientTypeEntry = RemotingConfigHandler.Info.QueryRemoteActivate(svrType.FullName, assemblyName) ?? RemotingConfigHandler.Info.QueryRemoteActivate(svrType.Name, simpleAssemblyName);
      }
      return activatedClientTypeEntry;
    }

    internal static ActivatedClientTypeEntry IsRemotelyActivatedClientType(string typeName, string assemblyName)
    {
      return RemotingConfigHandler.Info.QueryRemoteActivate(typeName, assemblyName);
    }

    [SecurityCritical]
    internal static WellKnownClientTypeEntry IsWellKnownClientType(RuntimeType svrType)
    {
      string simpleAssemblyName = InternalRemotingServices.GetReflectionCachedData(svrType).SimpleAssemblyName;
      return RemotingConfigHandler.Info.QueryConnect(svrType.FullName, simpleAssemblyName) ?? RemotingConfigHandler.Info.QueryConnect(svrType.Name, simpleAssemblyName);
    }

    internal static WellKnownClientTypeEntry IsWellKnownClientType(string typeName, string assemblyName)
    {
      return RemotingConfigHandler.Info.QueryConnect(typeName, assemblyName);
    }

    private static void ParseGenericType(string typeAssem, int indexStart, out string typeName, out string assemName)
    {
      int length1 = typeAssem.Length;
      int num = 1;
      int startIndex = indexStart;
      while (num > 0 && ++startIndex < length1 - 1)
      {
        if (typeAssem[startIndex] == '[')
          ++num;
        else if (typeAssem[startIndex] == ']')
          --num;
      }
      if (num > 0 || startIndex >= length1)
      {
        typeName = (string) null;
        assemName = (string) null;
      }
      else
      {
        int length2 = typeAssem.IndexOf(',', startIndex);
        if (length2 >= 0 && length2 < length1 - 1)
        {
          typeName = typeAssem.Substring(0, length2).Trim();
          assemName = typeAssem.Substring(length2 + 1).Trim();
        }
        else
        {
          typeName = (string) null;
          assemName = (string) null;
        }
      }
    }

    internal static void ParseType(string typeAssem, out string typeName, out string assemName)
    {
      string typeAssem1 = typeAssem;
      int indexStart = typeAssem1.IndexOf("[");
      if (indexStart >= 0 && indexStart < typeAssem1.Length - 1)
      {
        RemotingConfigHandler.ParseGenericType(typeAssem1, indexStart, out typeName, out assemName);
      }
      else
      {
        int length = typeAssem1.IndexOf(",");
        if (length >= 0 && length < typeAssem1.Length - 1)
        {
          typeName = typeAssem1.Substring(0, length).Trim();
          assemName = typeAssem1.Substring(length + 1).Trim();
        }
        else
        {
          typeName = (string) null;
          assemName = (string) null;
        }
      }
    }

    [SecurityCritical]
    internal static bool IsActivationAllowed(RuntimeType svrType)
    {
      if (svrType == (RuntimeType) null)
        return false;
      string simpleAssemblyName = InternalRemotingServices.GetReflectionCachedData(svrType).SimpleAssemblyName;
      return RemotingConfigHandler.Info.ActivationAllowed(svrType.FullName, simpleAssemblyName);
    }

    [SecurityCritical]
    internal static bool IsActivationAllowed(string TypeName)
    {
      string qualifiedTypeName = RemotingServices.InternalGetTypeNameFromQualifiedTypeName(TypeName);
      if (qualifiedTypeName == null)
        return false;
      string typeName;
      string assemName;
      RemotingConfigHandler.ParseType(qualifiedTypeName, out typeName, out assemName);
      if (assemName == null)
        return false;
      int length = assemName.IndexOf(',');
      if (length != -1)
        assemName = assemName.Substring(0, length);
      return RemotingConfigHandler.Info.ActivationAllowed(typeName, assemName);
    }

    internal static void RegisterActivatedServiceType(ActivatedServiceTypeEntry entry)
    {
      RemotingConfigHandler.Info.AddActivatedType(entry.TypeName, entry.AssemblyName, entry.ContextAttributes);
    }

    [SecurityCritical]
    internal static void RegisterWellKnownServiceType(WellKnownServiceTypeEntry entry)
    {
      string typeName = entry.TypeName;
      string assemblyName = entry.AssemblyName;
      string objectUri = entry.ObjectUri;
      WellKnownObjectMode mode = entry.Mode;
      lock (RemotingConfigHandler.Info)
        RemotingConfigHandler.Info.AddWellKnownEntry(entry);
    }

    internal static void RegisterActivatedClientType(ActivatedClientTypeEntry entry)
    {
      RemotingConfigHandler.Info.AddActivatedClientType(entry);
    }

    internal static void RegisterWellKnownClientType(WellKnownClientTypeEntry entry)
    {
      RemotingConfigHandler.Info.AddWellKnownClientType(entry);
    }

    [SecurityCritical]
    internal static Type GetServerTypeForUri(string URI)
    {
      URI = Identity.RemoveAppNameOrAppGuidIfNecessary(URI);
      return RemotingConfigHandler.Info.GetServerTypeForUri(URI);
    }

    internal static ActivatedServiceTypeEntry[] GetRegisteredActivatedServiceTypes()
    {
      return RemotingConfigHandler.Info.GetRegisteredActivatedServiceTypes();
    }

    internal static WellKnownServiceTypeEntry[] GetRegisteredWellKnownServiceTypes()
    {
      return RemotingConfigHandler.Info.GetRegisteredWellKnownServiceTypes();
    }

    internal static ActivatedClientTypeEntry[] GetRegisteredActivatedClientTypes()
    {
      return RemotingConfigHandler.Info.GetRegisteredActivatedClientTypes();
    }

    internal static WellKnownClientTypeEntry[] GetRegisteredWellKnownClientTypes()
    {
      return RemotingConfigHandler.Info.GetRegisteredWellKnownClientTypes();
    }

    [SecurityCritical]
    internal static ServerIdentity CreateWellKnownObject(string uri)
    {
      uri = Identity.RemoveAppNameOrAppGuidIfNecessary(uri);
      return RemotingConfigHandler.Info.StartupWellKnownObject(uri);
    }

    internal class RemotingConfigInfo
    {
      private static char[] SepSpace = new char[1]{ ' ' };
      private static char[] SepPound = new char[1]{ '#' };
      private static char[] SepSemiColon = new char[1]
      {
        ';'
      };
      private static char[] SepEquals = new char[1]{ '=' };
      private static object s_wkoStartLock = new object();
      private static PermissionSet s_fullTrust = new PermissionSet(PermissionState.Unrestricted);
      private Hashtable _exportableClasses;
      private Hashtable _remoteTypeInfo;
      private Hashtable _remoteAppInfo;
      private Hashtable _wellKnownExportInfo;

      internal RemotingConfigInfo()
      {
        this._remoteTypeInfo = Hashtable.Synchronized(new Hashtable());
        this._exportableClasses = Hashtable.Synchronized(new Hashtable());
        this._remoteAppInfo = Hashtable.Synchronized(new Hashtable());
        this._wellKnownExportInfo = Hashtable.Synchronized(new Hashtable());
      }

      private string EncodeTypeAndAssemblyNames(string typeName, string assemblyName)
      {
        return typeName + ", " + assemblyName.ToLower(CultureInfo.InvariantCulture);
      }

      internal void StoreActivatedExports(RemotingXmlConfigFileData configData)
      {
        foreach (RemotingXmlConfigFileData.TypeEntry serverActivatedEntry in configData.ServerActivatedEntries)
          RemotingConfiguration.RegisterActivatedServiceType(new ActivatedServiceTypeEntry(serverActivatedEntry.TypeName, serverActivatedEntry.AssemblyName)
          {
            ContextAttributes = RemotingConfigHandler.RemotingConfigInfo.CreateContextAttributesFromConfigEntries(serverActivatedEntry.ContextAttributes)
          });
      }

      [SecurityCritical]
      internal void StoreInteropEntries(RemotingXmlConfigFileData configData)
      {
        foreach (RemotingXmlConfigFileData.InteropXmlElementEntry interopXmlElementEntry in configData.InteropXmlElementEntries)
        {
          Type type = Assembly.Load(interopXmlElementEntry.UrtAssemblyName).GetType(interopXmlElementEntry.UrtTypeName);
          SoapServices.RegisterInteropXmlElement(interopXmlElementEntry.XmlElementName, interopXmlElementEntry.XmlElementNamespace, type);
        }
        foreach (RemotingXmlConfigFileData.InteropXmlTypeEntry interopXmlTypeEntry in configData.InteropXmlTypeEntries)
        {
          Type type = Assembly.Load(interopXmlTypeEntry.UrtAssemblyName).GetType(interopXmlTypeEntry.UrtTypeName);
          SoapServices.RegisterInteropXmlType(interopXmlTypeEntry.XmlTypeName, interopXmlTypeEntry.XmlTypeNamespace, type);
        }
        foreach (RemotingXmlConfigFileData.PreLoadEntry preLoadEntry in configData.PreLoadEntries)
        {
          Assembly assembly = Assembly.Load(preLoadEntry.AssemblyName);
          if (preLoadEntry.TypeName != null)
            SoapServices.PreLoad(assembly.GetType(preLoadEntry.TypeName));
          else
            SoapServices.PreLoad(assembly);
        }
      }

      internal void StoreRemoteAppEntries(RemotingXmlConfigFileData configData)
      {
        char[] chArray = new char[1]{ '/' };
        foreach (RemotingXmlConfigFileData.RemoteAppEntry remoteAppEntry in configData.RemoteAppEntries)
        {
          string appUrl = remoteAppEntry.AppUri;
          if (appUrl != null && !appUrl.EndsWith("/", StringComparison.Ordinal))
            appUrl = appUrl.TrimEnd(chArray);
          foreach (RemotingXmlConfigFileData.TypeEntry activatedObject in remoteAppEntry.ActivatedObjects)
            RemotingConfiguration.RegisterActivatedClientType(new ActivatedClientTypeEntry(activatedObject.TypeName, activatedObject.AssemblyName, appUrl)
            {
              ContextAttributes = RemotingConfigHandler.RemotingConfigInfo.CreateContextAttributesFromConfigEntries(activatedObject.ContextAttributes)
            });
          foreach (RemotingXmlConfigFileData.ClientWellKnownEntry wellKnownObject in remoteAppEntry.WellKnownObjects)
            RemotingConfiguration.RegisterWellKnownClientType(new WellKnownClientTypeEntry(wellKnownObject.TypeName, wellKnownObject.AssemblyName, wellKnownObject.Url)
            {
              ApplicationUrl = appUrl
            });
        }
      }

      [SecurityCritical]
      internal void StoreWellKnownExports(RemotingXmlConfigFileData configData)
      {
        foreach (RemotingXmlConfigFileData.ServerWellKnownEntry serverWellKnownEntry in configData.ServerWellKnownEntries)
          RemotingConfigHandler.RegisterWellKnownServiceType(new WellKnownServiceTypeEntry(serverWellKnownEntry.TypeName, serverWellKnownEntry.AssemblyName, serverWellKnownEntry.ObjectURI, serverWellKnownEntry.ObjectMode)
          {
            ContextAttributes = (IContextAttribute[]) null
          });
      }

      private static IContextAttribute[] CreateContextAttributesFromConfigEntries(ArrayList contextAttributes)
      {
        int count = contextAttributes.Count;
        if (count == 0)
          return (IContextAttribute[]) null;
        IContextAttribute[] contextAttributeArray = new IContextAttribute[count];
        int num = 0;
        foreach (RemotingXmlConfigFileData.ContextAttributeEntry contextAttribute in contextAttributes)
        {
          Assembly assembly = Assembly.Load(contextAttribute.AssemblyName);
          Hashtable properties = contextAttribute.Properties;
          IContextAttribute instance;
          if (properties != null && properties.Count > 0)
          {
            object[] args = new object[1]
            {
              (object) properties
            };
            instance = (IContextAttribute) Activator.CreateInstance(assembly.GetType(contextAttribute.TypeName, false, false), BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, (Binder) null, args, (CultureInfo) null, (object[]) null);
          }
          else
            instance = (IContextAttribute) Activator.CreateInstance(assembly.GetType(contextAttribute.TypeName, false, false), true);
          contextAttributeArray[num++] = instance;
        }
        return contextAttributeArray;
      }

      internal bool ActivationAllowed(string typeName, string assemblyName)
      {
        return this._exportableClasses.ContainsKey((object) this.EncodeTypeAndAssemblyNames(typeName, assemblyName));
      }

      internal ActivatedClientTypeEntry QueryRemoteActivate(string typeName, string assemblyName)
      {
        ActivatedClientTypeEntry activatedClientTypeEntry = this._remoteTypeInfo[(object) this.EncodeTypeAndAssemblyNames(typeName, assemblyName)] as ActivatedClientTypeEntry;
        if (activatedClientTypeEntry == null)
          return (ActivatedClientTypeEntry) null;
        if (activatedClientTypeEntry.GetRemoteAppEntry() == null)
        {
          RemoteAppEntry entry = (RemoteAppEntry) this._remoteAppInfo[(object) activatedClientTypeEntry.ApplicationUrl];
          if (entry == null)
            throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Activation_MissingRemoteAppEntry"), (object) activatedClientTypeEntry.ApplicationUrl));
          activatedClientTypeEntry.CacheRemoteAppEntry(entry);
        }
        return activatedClientTypeEntry;
      }

      internal WellKnownClientTypeEntry QueryConnect(string typeName, string assemblyName)
      {
        return this._remoteTypeInfo[(object) this.EncodeTypeAndAssemblyNames(typeName, assemblyName)] as WellKnownClientTypeEntry ?? (WellKnownClientTypeEntry) null;
      }

      internal ActivatedServiceTypeEntry[] GetRegisteredActivatedServiceTypes()
      {
        ActivatedServiceTypeEntry[] serviceTypeEntryArray = new ActivatedServiceTypeEntry[this._exportableClasses.Count];
        int num = 0;
        foreach (DictionaryEntry exportableClass in this._exportableClasses)
          serviceTypeEntryArray[num++] = (ActivatedServiceTypeEntry) exportableClass.Value;
        return serviceTypeEntryArray;
      }

      internal WellKnownServiceTypeEntry[] GetRegisteredWellKnownServiceTypes()
      {
        WellKnownServiceTypeEntry[] serviceTypeEntryArray = new WellKnownServiceTypeEntry[this._wellKnownExportInfo.Count];
        int num = 0;
        foreach (DictionaryEntry dictionaryEntry in this._wellKnownExportInfo)
        {
          WellKnownServiceTypeEntry serviceTypeEntry = (WellKnownServiceTypeEntry) dictionaryEntry.Value;
          serviceTypeEntryArray[num++] = new WellKnownServiceTypeEntry(serviceTypeEntry.TypeName, serviceTypeEntry.AssemblyName, serviceTypeEntry.ObjectUri, serviceTypeEntry.Mode)
          {
            ContextAttributes = serviceTypeEntry.ContextAttributes
          };
        }
        return serviceTypeEntryArray;
      }

      internal ActivatedClientTypeEntry[] GetRegisteredActivatedClientTypes()
      {
        int length = 0;
        foreach (DictionaryEntry dictionaryEntry in this._remoteTypeInfo)
        {
          if (dictionaryEntry.Value is ActivatedClientTypeEntry)
            ++length;
        }
        ActivatedClientTypeEntry[] activatedClientTypeEntryArray = new ActivatedClientTypeEntry[length];
        int num = 0;
        foreach (DictionaryEntry dictionaryEntry in this._remoteTypeInfo)
        {
          ActivatedClientTypeEntry activatedClientTypeEntry = dictionaryEntry.Value as ActivatedClientTypeEntry;
          if (activatedClientTypeEntry != null)
          {
            string appUrl = (string) null;
            RemoteAppEntry remoteAppEntry = activatedClientTypeEntry.GetRemoteAppEntry();
            if (remoteAppEntry != null)
              appUrl = remoteAppEntry.GetAppURI();
            activatedClientTypeEntryArray[num++] = new ActivatedClientTypeEntry(activatedClientTypeEntry.TypeName, activatedClientTypeEntry.AssemblyName, appUrl)
            {
              ContextAttributes = activatedClientTypeEntry.ContextAttributes
            };
          }
        }
        return activatedClientTypeEntryArray;
      }

      internal WellKnownClientTypeEntry[] GetRegisteredWellKnownClientTypes()
      {
        int length = 0;
        foreach (DictionaryEntry dictionaryEntry in this._remoteTypeInfo)
        {
          if (dictionaryEntry.Value is WellKnownClientTypeEntry)
            ++length;
        }
        WellKnownClientTypeEntry[] knownClientTypeEntryArray = new WellKnownClientTypeEntry[length];
        int num = 0;
        foreach (DictionaryEntry dictionaryEntry in this._remoteTypeInfo)
        {
          WellKnownClientTypeEntry knownClientTypeEntry1 = dictionaryEntry.Value as WellKnownClientTypeEntry;
          if (knownClientTypeEntry1 != null)
          {
            WellKnownClientTypeEntry knownClientTypeEntry2 = new WellKnownClientTypeEntry(knownClientTypeEntry1.TypeName, knownClientTypeEntry1.AssemblyName, knownClientTypeEntry1.ObjectUrl);
            RemoteAppEntry remoteAppEntry = knownClientTypeEntry1.GetRemoteAppEntry();
            if (remoteAppEntry != null)
              knownClientTypeEntry2.ApplicationUrl = remoteAppEntry.GetAppURI();
            knownClientTypeEntryArray[num++] = knownClientTypeEntry2;
          }
        }
        return knownClientTypeEntryArray;
      }

      internal void AddActivatedType(string typeName, string assemblyName, IContextAttribute[] contextAttributes)
      {
        if (typeName == null)
          throw new ArgumentNullException(nameof (typeName));
        if (assemblyName == null)
          throw new ArgumentNullException(nameof (assemblyName));
        if (this.CheckForRedirectedClientType(typeName, assemblyName))
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_CantUseRedirectedTypeForWellKnownService"), (object) typeName, (object) assemblyName));
        this._exportableClasses.Add((object) this.EncodeTypeAndAssemblyNames(typeName, assemblyName), (object) new ActivatedServiceTypeEntry(typeName, assemblyName)
        {
          ContextAttributes = contextAttributes
        });
      }

      private bool CheckForServiceEntryWithType(string typeName, string asmName)
      {
        if (!this.CheckForWellKnownServiceEntryWithType(typeName, asmName))
          return this.ActivationAllowed(typeName, asmName);
        return true;
      }

      private bool CheckForWellKnownServiceEntryWithType(string typeName, string asmName)
      {
        foreach (DictionaryEntry dictionaryEntry in this._wellKnownExportInfo)
        {
          WellKnownServiceTypeEntry serviceTypeEntry = (WellKnownServiceTypeEntry) dictionaryEntry.Value;
          if (typeName == serviceTypeEntry.TypeName)
          {
            bool flag = false;
            if (asmName == serviceTypeEntry.AssemblyName)
              flag = true;
            else if (string.Compare(serviceTypeEntry.AssemblyName, 0, asmName, 0, asmName.Length, StringComparison.OrdinalIgnoreCase) == 0 && serviceTypeEntry.AssemblyName[asmName.Length] == ',')
              flag = true;
            if (flag)
              return true;
          }
        }
        return false;
      }

      private bool CheckForRedirectedClientType(string typeName, string asmName)
      {
        int length = asmName.IndexOf(",");
        if (length != -1)
          asmName = asmName.Substring(0, length);
        if (this.QueryRemoteActivate(typeName, asmName) == null)
          return this.QueryConnect(typeName, asmName) != null;
        return true;
      }

      internal void AddActivatedClientType(ActivatedClientTypeEntry entry)
      {
        if (this.CheckForRedirectedClientType(entry.TypeName, entry.AssemblyName))
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_TypeAlreadyRedirected"), (object) entry.TypeName, (object) entry.AssemblyName));
        if (this.CheckForServiceEntryWithType(entry.TypeName, entry.AssemblyName))
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_CantRedirectActivationOfWellKnownService"), (object) entry.TypeName, (object) entry.AssemblyName));
        string applicationUrl = entry.ApplicationUrl;
        RemoteAppEntry entry1 = (RemoteAppEntry) this._remoteAppInfo[(object) applicationUrl];
        if (entry1 == null)
        {
          entry1 = new RemoteAppEntry(applicationUrl, applicationUrl);
          this._remoteAppInfo.Add((object) applicationUrl, (object) entry1);
        }
        if (entry1 != null)
          entry.CacheRemoteAppEntry(entry1);
        this._remoteTypeInfo.Add((object) this.EncodeTypeAndAssemblyNames(entry.TypeName, entry.AssemblyName), (object) entry);
      }

      internal void AddWellKnownClientType(WellKnownClientTypeEntry entry)
      {
        if (this.CheckForRedirectedClientType(entry.TypeName, entry.AssemblyName))
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_TypeAlreadyRedirected"), (object) entry.TypeName, (object) entry.AssemblyName));
        if (this.CheckForServiceEntryWithType(entry.TypeName, entry.AssemblyName))
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_CantRedirectActivationOfWellKnownService"), (object) entry.TypeName, (object) entry.AssemblyName));
        string applicationUrl = entry.ApplicationUrl;
        RemoteAppEntry entry1 = (RemoteAppEntry) null;
        if (applicationUrl != null)
        {
          entry1 = (RemoteAppEntry) this._remoteAppInfo[(object) applicationUrl];
          if (entry1 == null)
          {
            entry1 = new RemoteAppEntry(applicationUrl, applicationUrl);
            this._remoteAppInfo.Add((object) applicationUrl, (object) entry1);
          }
        }
        if (entry1 != null)
          entry.CacheRemoteAppEntry(entry1);
        this._remoteTypeInfo.Add((object) this.EncodeTypeAndAssemblyNames(entry.TypeName, entry.AssemblyName), (object) entry);
      }

      [SecurityCritical]
      internal void AddWellKnownEntry(WellKnownServiceTypeEntry entry)
      {
        this.AddWellKnownEntry(entry, true);
      }

      [SecurityCritical]
      internal void AddWellKnownEntry(WellKnownServiceTypeEntry entry, bool fReplace)
      {
        if (this.CheckForRedirectedClientType(entry.TypeName, entry.AssemblyName))
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_CantUseRedirectedTypeForWellKnownService"), (object) entry.TypeName, (object) entry.AssemblyName));
        string lower = entry.ObjectUri.ToLower(CultureInfo.InvariantCulture);
        if (fReplace)
        {
          this._wellKnownExportInfo[(object) lower] = (object) entry;
          IdentityHolder.RemoveIdentity(entry.ObjectUri);
        }
        else
          this._wellKnownExportInfo.Add((object) lower, (object) entry);
      }

      [SecurityCritical]
      internal Type GetServerTypeForUri(string URI)
      {
        Type type = (Type) null;
        WellKnownServiceTypeEntry serviceTypeEntry = (WellKnownServiceTypeEntry) this._wellKnownExportInfo[(object) URI.ToLower(CultureInfo.InvariantCulture)];
        if (serviceTypeEntry != null)
          type = RemotingConfigHandler.RemotingConfigInfo.LoadType(serviceTypeEntry.TypeName, serviceTypeEntry.AssemblyName);
        return type;
      }

      [SecurityCritical]
      internal ServerIdentity StartupWellKnownObject(string URI)
      {
        string lower = URI.ToLower(CultureInfo.InvariantCulture);
        ServerIdentity serverIdentity = (ServerIdentity) null;
        WellKnownServiceTypeEntry serviceTypeEntry = (WellKnownServiceTypeEntry) this._wellKnownExportInfo[(object) lower];
        if (serviceTypeEntry != null)
          serverIdentity = this.StartupWellKnownObject(serviceTypeEntry.AssemblyName, serviceTypeEntry.TypeName, serviceTypeEntry.ObjectUri, serviceTypeEntry.Mode);
        return serverIdentity;
      }

      [SecurityCritical]
      internal ServerIdentity StartupWellKnownObject(string asmName, string svrTypeName, string URI, WellKnownObjectMode mode)
      {
        return this.StartupWellKnownObject(asmName, svrTypeName, URI, mode, false);
      }

      [SecurityCritical]
      internal ServerIdentity StartupWellKnownObject(string asmName, string svrTypeName, string URI, WellKnownObjectMode mode, bool fReplace)
      {
        lock (RemotingConfigHandler.RemotingConfigInfo.s_wkoStartLock)
        {
          Type type = RemotingConfigHandler.RemotingConfigInfo.LoadType(svrTypeName, asmName);
          if (!type.IsMarshalByRef)
            throw new RemotingException(Environment.GetResourceString("Remoting_WellKnown_MustBeMBR", (object) svrTypeName));
          ServerIdentity serverIdentity = (ServerIdentity) IdentityHolder.ResolveIdentity(URI);
          if (serverIdentity != null && serverIdentity.IsRemoteDisconnected())
          {
            IdentityHolder.RemoveIdentity(URI);
            serverIdentity = (ServerIdentity) null;
          }
          if (serverIdentity == null)
          {
            RemotingConfigHandler.RemotingConfigInfo.s_fullTrust.Assert();
            try
            {
              MarshalByRefObject instance = (MarshalByRefObject) Activator.CreateInstance(type, true);
              if (RemotingServices.IsClientProxy((object) instance))
              {
                RemotingServices.MarshalInternal((MarshalByRefObject) new RedirectionProxy(instance, type)
                {
                  ObjectMode = mode
                }, URI, type, true, true);
                serverIdentity = (ServerIdentity) IdentityHolder.ResolveIdentity(URI);
                serverIdentity.SetSingletonObjectMode();
              }
              else if (type.IsCOMObject && mode == WellKnownObjectMode.Singleton)
              {
                RemotingServices.MarshalInternal((MarshalByRefObject) new ComRedirectionProxy(instance, type), URI, type, true, true);
                serverIdentity = (ServerIdentity) IdentityHolder.ResolveIdentity(URI);
                serverIdentity.SetSingletonObjectMode();
              }
              else
              {
                if (RemotingServices.GetObjectUri(instance) != null)
                  throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_WellKnown_CtorCantMarshal"), (object) URI));
                RemotingServices.MarshalInternal(instance, URI, type, true, true);
                serverIdentity = (ServerIdentity) IdentityHolder.ResolveIdentity(URI);
                if (mode == WellKnownObjectMode.SingleCall)
                  serverIdentity.SetSingleCallObjectMode();
                else
                  serverIdentity.SetSingletonObjectMode();
              }
            }
            catch
            {
              throw;
            }
            finally
            {
              if (serverIdentity != null)
                serverIdentity.IsInitializing = false;
              CodeAccessPermission.RevertAssert();
            }
          }
          return serverIdentity;
        }
      }

      [SecurityCritical]
      internal static Type LoadType(string typeName, string assemblyName)
      {
        Assembly assembly = (Assembly) null;
        new FileIOPermission(PermissionState.Unrestricted).Assert();
        try
        {
          assembly = Assembly.Load(assemblyName);
        }
        finally
        {
          CodeAccessPermission.RevertAssert();
        }
        if (assembly == (Assembly) null)
          throw new RemotingException(Environment.GetResourceString("Remoting_AssemblyLoadFailed", (object) assemblyName));
        Type type = assembly.GetType(typeName, false, false);
        if (type == (Type) null)
          throw new RemotingException(Environment.GetResourceString("Remoting_BadType", (object) (typeName + ", " + assemblyName)));
        return type;
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Activation.RemotingXmlConfigFileParser
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting.Activation
{
  internal static class RemotingXmlConfigFileParser
  {
    private static Hashtable _channelTemplates = RemotingXmlConfigFileParser.CreateSyncCaseInsensitiveHashtable();
    private static Hashtable _clientChannelSinkTemplates = RemotingXmlConfigFileParser.CreateSyncCaseInsensitiveHashtable();
    private static Hashtable _serverChannelSinkTemplates = RemotingXmlConfigFileParser.CreateSyncCaseInsensitiveHashtable();

    private static Hashtable CreateSyncCaseInsensitiveHashtable()
    {
      return Hashtable.Synchronized(RemotingXmlConfigFileParser.CreateCaseInsensitiveHashtable());
    }

    private static Hashtable CreateCaseInsensitiveHashtable()
    {
      return new Hashtable((IEqualityComparer) StringComparer.InvariantCultureIgnoreCase);
    }

    public static RemotingXmlConfigFileData ParseDefaultConfiguration()
    {
      ConfigNode configNode1 = new ConfigNode("system.runtime.remoting", (ConfigNode) null);
      ConfigNode parent1 = new ConfigNode("application", configNode1);
      configNode1.Children.Add(parent1);
      ConfigNode configNode2 = new ConfigNode("channels", parent1);
      parent1.Children.Add(configNode2);
      configNode2.Children.Add(new ConfigNode("channel", parent1)
      {
        Attributes = {
          new DictionaryEntry((object) "ref", (object) "http client"),
          new DictionaryEntry((object) "displayName", (object) "http client (delay loaded)"),
          new DictionaryEntry((object) "delayLoadAsClientChannel", (object) "true")
        }
      });
      configNode2.Children.Add(new ConfigNode("channel", parent1)
      {
        Attributes = {
          new DictionaryEntry((object) "ref", (object) "tcp client"),
          new DictionaryEntry((object) "displayName", (object) "tcp client (delay loaded)"),
          new DictionaryEntry((object) "delayLoadAsClientChannel", (object) "true")
        }
      });
      configNode2.Children.Add(new ConfigNode("channel", parent1)
      {
        Attributes = {
          new DictionaryEntry((object) "ref", (object) "ipc client"),
          new DictionaryEntry((object) "displayName", (object) "ipc client (delay loaded)"),
          new DictionaryEntry((object) "delayLoadAsClientChannel", (object) "true")
        }
      });
      ConfigNode parent2 = new ConfigNode("channels", configNode1);
      configNode1.Children.Add(parent2);
      parent2.Children.Add(new ConfigNode("channel", parent2)
      {
        Attributes = {
          new DictionaryEntry((object) "id", (object) "http"),
          new DictionaryEntry((object) "type", (object) "System.Runtime.Remoting.Channels.Http.HttpChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")
        }
      });
      parent2.Children.Add(new ConfigNode("channel", parent2)
      {
        Attributes = {
          new DictionaryEntry((object) "id", (object) "http client"),
          new DictionaryEntry((object) "type", (object) "System.Runtime.Remoting.Channels.Http.HttpClientChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")
        }
      });
      parent2.Children.Add(new ConfigNode("channel", parent2)
      {
        Attributes = {
          new DictionaryEntry((object) "id", (object) "http server"),
          new DictionaryEntry((object) "type", (object) "System.Runtime.Remoting.Channels.Http.HttpServerChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")
        }
      });
      parent2.Children.Add(new ConfigNode("channel", parent2)
      {
        Attributes = {
          new DictionaryEntry((object) "id", (object) "tcp"),
          new DictionaryEntry((object) "type", (object) "System.Runtime.Remoting.Channels.Tcp.TcpChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")
        }
      });
      parent2.Children.Add(new ConfigNode("channel", parent2)
      {
        Attributes = {
          new DictionaryEntry((object) "id", (object) "tcp client"),
          new DictionaryEntry((object) "type", (object) "System.Runtime.Remoting.Channels.Tcp.TcpClientChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")
        }
      });
      parent2.Children.Add(new ConfigNode("channel", parent2)
      {
        Attributes = {
          new DictionaryEntry((object) "id", (object) "tcp server"),
          new DictionaryEntry((object) "type", (object) "System.Runtime.Remoting.Channels.Tcp.TcpServerChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")
        }
      });
      parent2.Children.Add(new ConfigNode("channel", parent2)
      {
        Attributes = {
          new DictionaryEntry((object) "id", (object) "ipc"),
          new DictionaryEntry((object) "type", (object) "System.Runtime.Remoting.Channels.Ipc.IpcChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")
        }
      });
      parent2.Children.Add(new ConfigNode("channel", parent2)
      {
        Attributes = {
          new DictionaryEntry((object) "id", (object) "ipc client"),
          new DictionaryEntry((object) "type", (object) "System.Runtime.Remoting.Channels.Ipc.IpcClientChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")
        }
      });
      parent2.Children.Add(new ConfigNode("channel", parent2)
      {
        Attributes = {
          new DictionaryEntry((object) "id", (object) "ipc server"),
          new DictionaryEntry((object) "type", (object) "System.Runtime.Remoting.Channels.Ipc.IpcServerChannel, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")
        }
      });
      ConfigNode parent3 = new ConfigNode("channelSinkProviders", configNode1);
      configNode1.Children.Add(parent3);
      ConfigNode parent4 = new ConfigNode("clientProviders", parent3);
      parent3.Children.Add(parent4);
      parent4.Children.Add(new ConfigNode("formatter", parent4)
      {
        Attributes = {
          new DictionaryEntry((object) "id", (object) "soap"),
          new DictionaryEntry((object) "type", (object) "System.Runtime.Remoting.Channels.SoapClientFormatterSinkProvider, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")
        }
      });
      parent4.Children.Add(new ConfigNode("formatter", parent4)
      {
        Attributes = {
          new DictionaryEntry((object) "id", (object) "binary"),
          new DictionaryEntry((object) "type", (object) "System.Runtime.Remoting.Channels.BinaryClientFormatterSinkProvider, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")
        }
      });
      ConfigNode parent5 = new ConfigNode("serverProviders", parent3);
      parent3.Children.Add(parent5);
      parent5.Children.Add(new ConfigNode("formatter", parent5)
      {
        Attributes = {
          new DictionaryEntry((object) "id", (object) "soap"),
          new DictionaryEntry((object) "type", (object) "System.Runtime.Remoting.Channels.SoapServerFormatterSinkProvider, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")
        }
      });
      parent5.Children.Add(new ConfigNode("formatter", parent5)
      {
        Attributes = {
          new DictionaryEntry((object) "id", (object) "binary"),
          new DictionaryEntry((object) "type", (object) "System.Runtime.Remoting.Channels.BinaryServerFormatterSinkProvider, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")
        }
      });
      parent5.Children.Add(new ConfigNode("provider", parent5)
      {
        Attributes = {
          new DictionaryEntry((object) "id", (object) "wsdl"),
          new DictionaryEntry((object) "type", (object) "System.Runtime.Remoting.MetadataServices.SdlChannelSinkProvider, System.Runtime.Remoting, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")
        }
      });
      return RemotingXmlConfigFileParser.ParseConfigNode(configNode1);
    }

    public static RemotingXmlConfigFileData ParseConfigFile(string filename)
    {
      return RemotingXmlConfigFileParser.ParseConfigNode(new ConfigTreeParser().Parse(filename, "/configuration/system.runtime.remoting"));
    }

    private static RemotingXmlConfigFileData ParseConfigNode(ConfigNode rootNode)
    {
      RemotingXmlConfigFileData configData = new RemotingXmlConfigFileData();
      if (rootNode == null)
        return (RemotingXmlConfigFileData) null;
      foreach (DictionaryEntry attribute in rootNode.Attributes)
      {
        int num = attribute.Key.ToString() == "version" ? 1 : 0;
      }
      ConfigNode configNode1 = (ConfigNode) null;
      ConfigNode configNode2 = (ConfigNode) null;
      ConfigNode configNode3 = (ConfigNode) null;
      ConfigNode configNode4 = (ConfigNode) null;
      ConfigNode configNode5 = (ConfigNode) null;
      foreach (ConfigNode child in rootNode.Children)
      {
        string name = child.Name;
        if (!(name == "application"))
        {
          if (!(name == "channels"))
          {
            if (!(name == "channelSinkProviders"))
            {
              if (!(name == "debug"))
              {
                if (name == "customErrors")
                {
                  if (configNode5 != null)
                    RemotingXmlConfigFileParser.ReportUniqueSectionError(rootNode, configNode5, configData);
                  configNode5 = child;
                }
              }
              else
              {
                if (configNode4 != null)
                  RemotingXmlConfigFileParser.ReportUniqueSectionError(rootNode, configNode4, configData);
                configNode4 = child;
              }
            }
            else
            {
              if (configNode3 != null)
                RemotingXmlConfigFileParser.ReportUniqueSectionError(rootNode, configNode3, configData);
              configNode3 = child;
            }
          }
          else
          {
            if (configNode2 != null)
              RemotingXmlConfigFileParser.ReportUniqueSectionError(rootNode, configNode2, configData);
            configNode2 = child;
          }
        }
        else
        {
          if (configNode1 != null)
            RemotingXmlConfigFileParser.ReportUniqueSectionError(rootNode, configNode1, configData);
          configNode1 = child;
        }
      }
      if (configNode4 != null)
        RemotingXmlConfigFileParser.ProcessDebugNode(configNode4, configData);
      if (configNode3 != null)
        RemotingXmlConfigFileParser.ProcessChannelSinkProviderTemplates(configNode3, configData);
      if (configNode2 != null)
        RemotingXmlConfigFileParser.ProcessChannelTemplates(configNode2, configData);
      if (configNode1 != null)
        RemotingXmlConfigFileParser.ProcessApplicationNode(configNode1, configData);
      if (configNode5 != null)
        RemotingXmlConfigFileParser.ProcessCustomErrorsNode(configNode5, configData);
      return configData;
    }

    private static void ReportError(string errorStr, RemotingXmlConfigFileData configData)
    {
      throw new RemotingException(errorStr);
    }

    private static void ReportUniqueSectionError(ConfigNode parent, ConfigNode child, RemotingXmlConfigFileData configData)
    {
      RemotingXmlConfigFileParser.ReportError(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_NodeMustBeUnique"), (object) child.Name, (object) parent.Name), configData);
    }

    private static void ReportUnknownValueError(ConfigNode node, string value, RemotingXmlConfigFileData configData)
    {
      RemotingXmlConfigFileParser.ReportError(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_UnknownValue"), (object) node.Name, (object) value), configData);
    }

    private static void ReportMissingAttributeError(ConfigNode node, string attributeName, RemotingXmlConfigFileData configData)
    {
      RemotingXmlConfigFileParser.ReportMissingAttributeError(node.Name, attributeName, configData);
    }

    private static void ReportMissingAttributeError(string nodeDescription, string attributeName, RemotingXmlConfigFileData configData)
    {
      RemotingXmlConfigFileParser.ReportError(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_RequiredXmlAttribute"), (object) nodeDescription, (object) attributeName), configData);
    }

    private static void ReportMissingTypeAttributeError(ConfigNode node, string attributeName, RemotingXmlConfigFileData configData)
    {
      RemotingXmlConfigFileParser.ReportError(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_MissingTypeAttribute"), (object) node.Name, (object) attributeName), configData);
    }

    private static void ReportMissingXmlTypeAttributeError(ConfigNode node, string attributeName, RemotingXmlConfigFileData configData)
    {
      RemotingXmlConfigFileParser.ReportError(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_MissingXmlTypeAttribute"), (object) node.Name, (object) attributeName), configData);
    }

    private static void ReportInvalidTimeFormatError(string time, RemotingXmlConfigFileData configData)
    {
      RemotingXmlConfigFileParser.ReportError(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_InvalidTimeFormat"), (object) time), configData);
    }

    private static void ReportNonTemplateIdAttributeError(ConfigNode node, RemotingXmlConfigFileData configData)
    {
      RemotingXmlConfigFileParser.ReportError(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_NonTemplateIdAttribute"), (object) node.Name), configData);
    }

    private static void ReportTemplateCannotReferenceTemplateError(ConfigNode node, RemotingXmlConfigFileData configData)
    {
      RemotingXmlConfigFileParser.ReportError(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_TemplateCannotReferenceTemplate"), (object) node.Name), configData);
    }

    private static void ReportUnableToResolveTemplateReferenceError(ConfigNode node, string referenceName, RemotingXmlConfigFileData configData)
    {
      RemotingXmlConfigFileParser.ReportError(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_UnableToResolveTemplate"), (object) node.Name, (object) referenceName), configData);
    }

    private static void ReportAssemblyVersionInfoPresent(string assemName, string entryDescription, RemotingXmlConfigFileData configData)
    {
      RemotingXmlConfigFileParser.ReportError(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Config_VersionPresent"), (object) assemName, (object) entryDescription), configData);
    }

    private static void ProcessDebugNode(ConfigNode node, RemotingXmlConfigFileData configData)
    {
      foreach (DictionaryEntry attribute in node.Attributes)
      {
        if (attribute.Key.ToString() == "loadTypes")
          RemotingXmlConfigFileData.LoadTypes = Convert.ToBoolean((string) attribute.Value, (IFormatProvider) CultureInfo.InvariantCulture);
      }
    }

    private static void ProcessApplicationNode(ConfigNode node, RemotingXmlConfigFileData configData)
    {
      foreach (DictionaryEntry attribute in node.Attributes)
      {
        if (attribute.Key.ToString().Equals("name"))
          configData.ApplicationName = (string) attribute.Value;
      }
      foreach (ConfigNode child in node.Children)
      {
        string name = child.Name;
        if (!(name == "channels"))
        {
          if (!(name == "client"))
          {
            if (!(name == "lifetime"))
            {
              if (!(name == "service"))
              {
                if (name == "soapInterop")
                  RemotingXmlConfigFileParser.ProcessSoapInteropNode(child, configData);
              }
              else
                RemotingXmlConfigFileParser.ProcessServiceNode(child, configData);
            }
            else
              RemotingXmlConfigFileParser.ProcessLifetimeNode(node, child, configData);
          }
          else
            RemotingXmlConfigFileParser.ProcessClientNode(child, configData);
        }
        else
          RemotingXmlConfigFileParser.ProcessChannelsNode(child, configData);
      }
    }

    private static void ProcessCustomErrorsNode(ConfigNode node, RemotingXmlConfigFileData configData)
    {
      foreach (DictionaryEntry attribute in node.Attributes)
      {
        if (attribute.Key.ToString().Equals("mode"))
        {
          string strA = (string) attribute.Value;
          CustomErrorsModes mode = CustomErrorsModes.On;
          if (string.Compare(strA, "on", StringComparison.OrdinalIgnoreCase) == 0)
            mode = CustomErrorsModes.On;
          else if (string.Compare(strA, "off", StringComparison.OrdinalIgnoreCase) == 0)
            mode = CustomErrorsModes.Off;
          else if (string.Compare(strA, "remoteonly", StringComparison.OrdinalIgnoreCase) == 0)
            mode = CustomErrorsModes.RemoteOnly;
          else
            RemotingXmlConfigFileParser.ReportUnknownValueError(node, strA, configData);
          configData.CustomErrors = new RemotingXmlConfigFileData.CustomErrorsEntry(mode);
        }
      }
    }

    private static void ProcessLifetimeNode(ConfigNode parentNode, ConfigNode node, RemotingXmlConfigFileData configData)
    {
      if (configData.Lifetime != null)
        RemotingXmlConfigFileParser.ReportUniqueSectionError(node, parentNode, configData);
      configData.Lifetime = new RemotingXmlConfigFileData.LifetimeEntry();
      foreach (DictionaryEntry attribute in node.Attributes)
      {
        string str = attribute.Key.ToString();
        if (!(str == "leaseTime"))
        {
          if (!(str == "sponsorshipTimeout"))
          {
            if (!(str == "renewOnCallTime"))
            {
              if (str == "leaseManagerPollTime")
                configData.Lifetime.LeaseManagerPollTime = RemotingXmlConfigFileParser.ParseTime((string) attribute.Value, configData);
            }
            else
              configData.Lifetime.RenewOnCallTime = RemotingXmlConfigFileParser.ParseTime((string) attribute.Value, configData);
          }
          else
            configData.Lifetime.SponsorshipTimeout = RemotingXmlConfigFileParser.ParseTime((string) attribute.Value, configData);
        }
        else
          configData.Lifetime.LeaseTime = RemotingXmlConfigFileParser.ParseTime((string) attribute.Value, configData);
      }
    }

    private static void ProcessServiceNode(ConfigNode node, RemotingXmlConfigFileData configData)
    {
      foreach (ConfigNode child in node.Children)
      {
        string name = child.Name;
        if (!(name == "wellknown"))
        {
          if (name == "activated")
            RemotingXmlConfigFileParser.ProcessServiceActivatedNode(child, configData);
        }
        else
          RemotingXmlConfigFileParser.ProcessServiceWellKnownNode(child, configData);
      }
    }

    private static void ProcessClientNode(ConfigNode node, RemotingXmlConfigFileData configData)
    {
      string appUri = (string) null;
      foreach (DictionaryEntry attribute in node.Attributes)
      {
        string str = attribute.Key.ToString();
        if (!(str == "url"))
        {
          if (str == "displayName")
            ;
        }
        else
          appUri = (string) attribute.Value;
      }
      RemotingXmlConfigFileData.RemoteAppEntry remoteApp = configData.AddRemoteAppEntry(appUri);
      foreach (ConfigNode child in node.Children)
      {
        string name = child.Name;
        if (!(name == "wellknown"))
        {
          if (name == "activated")
            RemotingXmlConfigFileParser.ProcessClientActivatedNode(child, configData, remoteApp);
        }
        else
          RemotingXmlConfigFileParser.ProcessClientWellKnownNode(child, configData, remoteApp);
      }
      if (remoteApp.ActivatedObjects.Count <= 0 || appUri != null)
        return;
      RemotingXmlConfigFileParser.ReportMissingAttributeError(node, "url", configData);
    }

    private static void ProcessSoapInteropNode(ConfigNode node, RemotingXmlConfigFileData configData)
    {
      foreach (DictionaryEntry attribute in node.Attributes)
      {
        if (attribute.Key.ToString() == "urlObjRef")
          configData.UrlObjRefMode = Convert.ToBoolean(attribute.Value, (IFormatProvider) CultureInfo.InvariantCulture);
      }
      foreach (ConfigNode child in node.Children)
      {
        string name = child.Name;
        if (!(name == "preLoad"))
        {
          if (!(name == "interopXmlElement"))
          {
            if (name == "interopXmlType")
              RemotingXmlConfigFileParser.ProcessInteropXmlTypeNode(child, configData);
          }
          else
            RemotingXmlConfigFileParser.ProcessInteropXmlElementNode(child, configData);
        }
        else
          RemotingXmlConfigFileParser.ProcessPreLoadNode(child, configData);
      }
    }

    private static void ProcessChannelsNode(ConfigNode node, RemotingXmlConfigFileData configData)
    {
      foreach (ConfigNode child in node.Children)
      {
        if (child.Name.Equals("channel"))
        {
          RemotingXmlConfigFileData.ChannelEntry channelEntry = RemotingXmlConfigFileParser.ProcessChannelsChannelNode(child, configData, false);
          configData.ChannelEntries.Add((object) channelEntry);
        }
      }
    }

    private static void ProcessServiceWellKnownNode(ConfigNode node, RemotingXmlConfigFileData configData)
    {
      string typeName = (string) null;
      string assemName = (string) null;
      ArrayList contextAttributes = new ArrayList();
      string objURI = (string) null;
      WellKnownObjectMode objMode = WellKnownObjectMode.Singleton;
      bool flag = false;
      foreach (DictionaryEntry attribute in node.Attributes)
      {
        string str = attribute.Key.ToString();
        if (!(str == "displayName"))
        {
          if (!(str == "mode"))
          {
            if (!(str == "objectUri"))
            {
              if (str == "type")
                RemotingConfigHandler.ParseType((string) attribute.Value, out typeName, out assemName);
            }
            else
              objURI = (string) attribute.Value;
          }
          else
          {
            string strA = (string) attribute.Value;
            flag = true;
            if (string.CompareOrdinal(strA, "Singleton") == 0)
              objMode = WellKnownObjectMode.Singleton;
            else if (string.CompareOrdinal(strA, "SingleCall") == 0)
              objMode = WellKnownObjectMode.SingleCall;
            else
              flag = false;
          }
        }
      }
      foreach (ConfigNode child in node.Children)
      {
        string name = child.Name;
        if (!(name == "contextAttribute"))
        {
          if (name == "lifetime")
            ;
        }
        else
          contextAttributes.Add((object) RemotingXmlConfigFileParser.ProcessContextAttributeNode(child, configData));
      }
      if (!flag)
        RemotingXmlConfigFileParser.ReportError(Environment.GetResourceString("Remoting_Config_MissingWellKnownModeAttribute"), configData);
      if (typeName == null || assemName == null)
        RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "type", configData);
      if (objURI == null)
        objURI = typeName + ".soap";
      configData.AddServerWellKnownEntry(typeName, assemName, contextAttributes, objURI, objMode);
    }

    private static void ProcessServiceActivatedNode(ConfigNode node, RemotingXmlConfigFileData configData)
    {
      string typeName = (string) null;
      string assemName = (string) null;
      ArrayList contextAttributes = new ArrayList();
      foreach (DictionaryEntry attribute in node.Attributes)
      {
        if (attribute.Key.ToString() == "type")
          RemotingConfigHandler.ParseType((string) attribute.Value, out typeName, out assemName);
      }
      foreach (ConfigNode child in node.Children)
      {
        string name = child.Name;
        if (!(name == "contextAttribute"))
        {
          if (name == "lifetime")
            ;
        }
        else
          contextAttributes.Add((object) RemotingXmlConfigFileParser.ProcessContextAttributeNode(child, configData));
      }
      if (typeName == null || assemName == null)
        RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "type", configData);
      if (RemotingXmlConfigFileParser.CheckAssemblyNameForVersionInfo(assemName))
        RemotingXmlConfigFileParser.ReportAssemblyVersionInfoPresent(assemName, "service activated", configData);
      configData.AddServerActivatedEntry(typeName, assemName, contextAttributes);
    }

    private static void ProcessClientWellKnownNode(ConfigNode node, RemotingXmlConfigFileData configData, RemotingXmlConfigFileData.RemoteAppEntry remoteApp)
    {
      string typeName = (string) null;
      string assemName = (string) null;
      string url = (string) null;
      foreach (DictionaryEntry attribute in node.Attributes)
      {
        string str = attribute.Key.ToString();
        if (!(str == "displayName"))
        {
          if (!(str == "type"))
          {
            if (str == "url")
              url = (string) attribute.Value;
          }
          else
            RemotingConfigHandler.ParseType((string) attribute.Value, out typeName, out assemName);
        }
      }
      if (url == null)
        RemotingXmlConfigFileParser.ReportMissingAttributeError("WellKnown client", "url", configData);
      if (typeName == null || assemName == null)
        RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "type", configData);
      if (RemotingXmlConfigFileParser.CheckAssemblyNameForVersionInfo(assemName))
        RemotingXmlConfigFileParser.ReportAssemblyVersionInfoPresent(assemName, "client wellknown", configData);
      remoteApp.AddWellKnownEntry(typeName, assemName, url);
    }

    private static void ProcessClientActivatedNode(ConfigNode node, RemotingXmlConfigFileData configData, RemotingXmlConfigFileData.RemoteAppEntry remoteApp)
    {
      string typeName = (string) null;
      string assemName = (string) null;
      ArrayList contextAttributes = new ArrayList();
      foreach (DictionaryEntry attribute in node.Attributes)
      {
        if (attribute.Key.ToString() == "type")
          RemotingConfigHandler.ParseType((string) attribute.Value, out typeName, out assemName);
      }
      foreach (ConfigNode child in node.Children)
      {
        if (child.Name == "contextAttribute")
          contextAttributes.Add((object) RemotingXmlConfigFileParser.ProcessContextAttributeNode(child, configData));
      }
      if (typeName == null || assemName == null)
        RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "type", configData);
      remoteApp.AddActivatedEntry(typeName, assemName, contextAttributes);
    }

    private static void ProcessInteropXmlElementNode(ConfigNode node, RemotingXmlConfigFileData configData)
    {
      string typeName1 = (string) null;
      string assemName1 = (string) null;
      string typeName2 = (string) null;
      string assemName2 = (string) null;
      foreach (DictionaryEntry attribute in node.Attributes)
      {
        string str = attribute.Key.ToString();
        if (!(str == "xml"))
        {
          if (str == "clr")
            RemotingConfigHandler.ParseType((string) attribute.Value, out typeName2, out assemName2);
        }
        else
          RemotingConfigHandler.ParseType((string) attribute.Value, out typeName1, out assemName1);
      }
      if (typeName1 == null || assemName1 == null)
        RemotingXmlConfigFileParser.ReportMissingXmlTypeAttributeError(node, "xml", configData);
      if (typeName2 == null || assemName2 == null)
        RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "clr", configData);
      configData.AddInteropXmlElementEntry(typeName1, assemName1, typeName2, assemName2);
    }

    private static void ProcessInteropXmlTypeNode(ConfigNode node, RemotingXmlConfigFileData configData)
    {
      string typeName1 = (string) null;
      string assemName1 = (string) null;
      string typeName2 = (string) null;
      string assemName2 = (string) null;
      foreach (DictionaryEntry attribute in node.Attributes)
      {
        string str = attribute.Key.ToString();
        if (!(str == "xml"))
        {
          if (str == "clr")
            RemotingConfigHandler.ParseType((string) attribute.Value, out typeName2, out assemName2);
        }
        else
          RemotingConfigHandler.ParseType((string) attribute.Value, out typeName1, out assemName1);
      }
      if (typeName1 == null || assemName1 == null)
        RemotingXmlConfigFileParser.ReportMissingXmlTypeAttributeError(node, "xml", configData);
      if (typeName2 == null || assemName2 == null)
        RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "clr", configData);
      configData.AddInteropXmlTypeEntry(typeName1, assemName1, typeName2, assemName2);
    }

    private static void ProcessPreLoadNode(ConfigNode node, RemotingXmlConfigFileData configData)
    {
      string typeName = (string) null;
      string assemName = (string) null;
      foreach (DictionaryEntry attribute in node.Attributes)
      {
        string str = attribute.Key.ToString();
        if (!(str == "type"))
        {
          if (str == "assembly")
            assemName = (string) attribute.Value;
        }
        else
          RemotingConfigHandler.ParseType((string) attribute.Value, out typeName, out assemName);
      }
      if (assemName == null)
        RemotingXmlConfigFileParser.ReportError(Environment.GetResourceString("Remoting_Config_PreloadRequiresTypeOrAssembly"), configData);
      configData.AddPreLoadEntry(typeName, assemName);
    }

    private static RemotingXmlConfigFileData.ContextAttributeEntry ProcessContextAttributeNode(ConfigNode node, RemotingXmlConfigFileData configData)
    {
      string typeName = (string) null;
      string assemName = (string) null;
      Hashtable insensitiveHashtable = RemotingXmlConfigFileParser.CreateCaseInsensitiveHashtable();
      foreach (DictionaryEntry attribute in node.Attributes)
      {
        string lower = ((string) attribute.Key).ToLower(CultureInfo.InvariantCulture);
        if (lower == "type")
          RemotingConfigHandler.ParseType((string) attribute.Value, out typeName, out assemName);
        else
          insensitiveHashtable[(object) lower] = attribute.Value;
      }
      if (typeName == null || assemName == null)
        RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "type", configData);
      return new RemotingXmlConfigFileData.ContextAttributeEntry(typeName, assemName, insensitiveHashtable);
    }

    private static RemotingXmlConfigFileData.ChannelEntry ProcessChannelsChannelNode(ConfigNode node, RemotingXmlConfigFileData configData, bool isTemplate)
    {
      string str = (string) null;
      string typeName = (string) null;
      string assemName = (string) null;
      Hashtable insensitiveHashtable = RemotingXmlConfigFileParser.CreateCaseInsensitiveHashtable();
      bool flag = false;
      RemotingXmlConfigFileData.ChannelEntry channelEntry1 = (RemotingXmlConfigFileData.ChannelEntry) null;
      foreach (DictionaryEntry attribute in node.Attributes)
      {
        string key = (string) attribute.Key;
        if (!(key == "displayName"))
        {
          if (!(key == "id"))
          {
            if (!(key == "ref"))
            {
              if (!(key == "type"))
              {
                if (key == "delayLoadAsClientChannel")
                  flag = Convert.ToBoolean((string) attribute.Value, (IFormatProvider) CultureInfo.InvariantCulture);
                else
                  insensitiveHashtable[(object) key] = attribute.Value;
              }
              else
                RemotingConfigHandler.ParseType((string) attribute.Value, out typeName, out assemName);
            }
            else if (isTemplate)
            {
              RemotingXmlConfigFileParser.ReportTemplateCannotReferenceTemplateError(node, configData);
            }
            else
            {
              channelEntry1 = (RemotingXmlConfigFileData.ChannelEntry) RemotingXmlConfigFileParser._channelTemplates[attribute.Value];
              if (channelEntry1 == null)
              {
                RemotingXmlConfigFileParser.ReportUnableToResolveTemplateReferenceError(node, attribute.Value.ToString(), configData);
              }
              else
              {
                typeName = channelEntry1.TypeName;
                assemName = channelEntry1.AssemblyName;
                foreach (DictionaryEntry property in channelEntry1.Properties)
                  insensitiveHashtable[property.Key] = property.Value;
              }
            }
          }
          else if (!isTemplate)
            RemotingXmlConfigFileParser.ReportNonTemplateIdAttributeError(node, configData);
          else
            str = ((string) attribute.Value).ToLower(CultureInfo.InvariantCulture);
        }
      }
      if (typeName == null || assemName == null)
        RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "type", configData);
      RemotingXmlConfigFileData.ChannelEntry channelEntry2 = new RemotingXmlConfigFileData.ChannelEntry(typeName, assemName, insensitiveHashtable);
      channelEntry2.DelayLoad = flag;
      foreach (ConfigNode child in node.Children)
      {
        string name = child.Name;
        if (!(name == "clientProviders"))
        {
          if (name == "serverProviders")
            RemotingXmlConfigFileParser.ProcessSinkProviderNodes(child, channelEntry2, configData, true);
        }
        else
          RemotingXmlConfigFileParser.ProcessSinkProviderNodes(child, channelEntry2, configData, false);
      }
      if (channelEntry1 != null)
      {
        if (channelEntry2.ClientSinkProviders.Count == 0)
          channelEntry2.ClientSinkProviders = channelEntry1.ClientSinkProviders;
        if (channelEntry2.ServerSinkProviders.Count == 0)
          channelEntry2.ServerSinkProviders = channelEntry1.ServerSinkProviders;
      }
      if (!isTemplate)
        return channelEntry2;
      RemotingXmlConfigFileParser._channelTemplates[(object) str] = (object) channelEntry2;
      return (RemotingXmlConfigFileData.ChannelEntry) null;
    }

    private static void ProcessSinkProviderNodes(ConfigNode node, RemotingXmlConfigFileData.ChannelEntry channelEntry, RemotingXmlConfigFileData configData, bool isServer)
    {
      foreach (ConfigNode child in node.Children)
      {
        RemotingXmlConfigFileData.SinkProviderEntry sinkProviderEntry = RemotingXmlConfigFileParser.ProcessSinkProviderNode(child, configData, false, isServer);
        if (isServer)
          channelEntry.ServerSinkProviders.Add((object) sinkProviderEntry);
        else
          channelEntry.ClientSinkProviders.Add((object) sinkProviderEntry);
      }
    }

    private static RemotingXmlConfigFileData.SinkProviderEntry ProcessSinkProviderNode(ConfigNode node, RemotingXmlConfigFileData configData, bool isTemplate, bool isServer)
    {
      bool isFormatter = false;
      string name = node.Name;
      if (name.Equals("formatter"))
        isFormatter = true;
      else if (name.Equals("provider"))
        isFormatter = false;
      else
        RemotingXmlConfigFileParser.ReportError(Environment.GetResourceString("Remoting_Config_ProviderNeedsElementName"), configData);
      string str = (string) null;
      string typeName = (string) null;
      string assemName = (string) null;
      Hashtable insensitiveHashtable = RemotingXmlConfigFileParser.CreateCaseInsensitiveHashtable();
      RemotingXmlConfigFileData.SinkProviderEntry sinkProviderEntry1 = (RemotingXmlConfigFileData.SinkProviderEntry) null;
      foreach (DictionaryEntry attribute in node.Attributes)
      {
        string key = (string) attribute.Key;
        if (!(key == "id"))
        {
          if (!(key == "ref"))
          {
            if (key == "type")
              RemotingConfigHandler.ParseType((string) attribute.Value, out typeName, out assemName);
            else
              insensitiveHashtable[(object) key] = attribute.Value;
          }
          else if (isTemplate)
          {
            RemotingXmlConfigFileParser.ReportTemplateCannotReferenceTemplateError(node, configData);
          }
          else
          {
            sinkProviderEntry1 = !isServer ? (RemotingXmlConfigFileData.SinkProviderEntry) RemotingXmlConfigFileParser._clientChannelSinkTemplates[attribute.Value] : (RemotingXmlConfigFileData.SinkProviderEntry) RemotingXmlConfigFileParser._serverChannelSinkTemplates[attribute.Value];
            if (sinkProviderEntry1 == null)
            {
              RemotingXmlConfigFileParser.ReportUnableToResolveTemplateReferenceError(node, attribute.Value.ToString(), configData);
            }
            else
            {
              typeName = sinkProviderEntry1.TypeName;
              assemName = sinkProviderEntry1.AssemblyName;
              foreach (DictionaryEntry property in sinkProviderEntry1.Properties)
                insensitiveHashtable[property.Key] = property.Value;
            }
          }
        }
        else if (!isTemplate)
          RemotingXmlConfigFileParser.ReportNonTemplateIdAttributeError(node, configData);
        else
          str = (string) attribute.Value;
      }
      if (typeName == null || assemName == null)
        RemotingXmlConfigFileParser.ReportMissingTypeAttributeError(node, "type", configData);
      RemotingXmlConfigFileData.SinkProviderEntry sinkProviderEntry2 = new RemotingXmlConfigFileData.SinkProviderEntry(typeName, assemName, insensitiveHashtable, isFormatter);
      foreach (ConfigNode child in node.Children)
      {
        SinkProviderData sinkProviderData = RemotingXmlConfigFileParser.ProcessSinkProviderData(child, configData);
        sinkProviderEntry2.ProviderData.Add((object) sinkProviderData);
      }
      if (sinkProviderEntry1 != null && sinkProviderEntry2.ProviderData.Count == 0)
        sinkProviderEntry2.ProviderData = sinkProviderEntry1.ProviderData;
      if (!isTemplate)
        return sinkProviderEntry2;
      if (isServer)
        RemotingXmlConfigFileParser._serverChannelSinkTemplates[(object) str] = (object) sinkProviderEntry2;
      else
        RemotingXmlConfigFileParser._clientChannelSinkTemplates[(object) str] = (object) sinkProviderEntry2;
      return (RemotingXmlConfigFileData.SinkProviderEntry) null;
    }

    private static SinkProviderData ProcessSinkProviderData(ConfigNode node, RemotingXmlConfigFileData configData)
    {
      SinkProviderData sinkProviderData1 = new SinkProviderData(node.Name);
      foreach (ConfigNode child in node.Children)
      {
        SinkProviderData sinkProviderData2 = RemotingXmlConfigFileParser.ProcessSinkProviderData(child, configData);
        sinkProviderData1.Children.Add((object) sinkProviderData2);
      }
      foreach (DictionaryEntry attribute in node.Attributes)
        sinkProviderData1.Properties[attribute.Key] = attribute.Value;
      return sinkProviderData1;
    }

    private static void ProcessChannelTemplates(ConfigNode node, RemotingXmlConfigFileData configData)
    {
      foreach (ConfigNode child in node.Children)
      {
        if (child.Name == "channel")
          RemotingXmlConfigFileParser.ProcessChannelsChannelNode(child, configData, true);
      }
    }

    private static void ProcessChannelSinkProviderTemplates(ConfigNode node, RemotingXmlConfigFileData configData)
    {
      foreach (ConfigNode child in node.Children)
      {
        string name = child.Name;
        if (!(name == "clientProviders"))
        {
          if (name == "serverProviders")
            RemotingXmlConfigFileParser.ProcessChannelProviderTemplates(child, configData, true);
        }
        else
          RemotingXmlConfigFileParser.ProcessChannelProviderTemplates(child, configData, false);
      }
    }

    private static void ProcessChannelProviderTemplates(ConfigNode node, RemotingXmlConfigFileData configData, bool isServer)
    {
      foreach (ConfigNode child in node.Children)
        RemotingXmlConfigFileParser.ProcessSinkProviderNode(child, configData, true, isServer);
    }

    private static bool CheckAssemblyNameForVersionInfo(string assemName)
    {
      if (assemName == null)
        return false;
      return assemName.IndexOf(',') != -1;
    }

    private static TimeSpan ParseTime(string time, RemotingXmlConfigFileData configData)
    {
      string time1 = time;
      string str = "s";
      int length = 0;
      char c = ' ';
      if (time.Length > 0)
        c = time[time.Length - 1];
      TimeSpan timeSpan = TimeSpan.FromSeconds(0.0);
      try
      {
        if (!char.IsDigit(c))
        {
          if (time.Length == 0)
            RemotingXmlConfigFileParser.ReportInvalidTimeFormatError(time1, configData);
          time = time.ToLower(CultureInfo.InvariantCulture);
          length = 1;
          if (time.EndsWith("ms", StringComparison.Ordinal))
            length = 2;
          str = time.Substring(time.Length - length, length);
        }
        int num = int.Parse(time.Substring(0, time.Length - length), (IFormatProvider) CultureInfo.InvariantCulture);
        if (!(str == "d"))
        {
          if (!(str == "h"))
          {
            if (!(str == "m"))
            {
              if (!(str == "s"))
              {
                if (str == "ms")
                  timeSpan = TimeSpan.FromMilliseconds((double) num);
                else
                  RemotingXmlConfigFileParser.ReportInvalidTimeFormatError(time1, configData);
              }
              else
                timeSpan = TimeSpan.FromSeconds((double) num);
            }
            else
              timeSpan = TimeSpan.FromMinutes((double) num);
          }
          else
            timeSpan = TimeSpan.FromHours((double) num);
        }
        else
          timeSpan = TimeSpan.FromDays((double) num);
      }
      catch (Exception ex)
      {
        RemotingXmlConfigFileParser.ReportInvalidTimeFormatError(time1, configData);
      }
      return timeSpan;
    }
  }
}

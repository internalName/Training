// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Activation.LocalActivator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
  [SecurityCritical]
  internal class LocalActivator : ContextAttribute, IActivator
  {
    internal LocalActivator()
      : base("RemoteActivationService.rem")
    {
    }

    [SecurityCritical]
    public override bool IsContextOK(Context ctx, IConstructionCallMessage ctorMsg)
    {
      if (RemotingConfigHandler.Info == null)
        return true;
      RuntimeType activationType = ctorMsg.ActivationType as RuntimeType;
      if (activationType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      WellKnownClientTypeEntry knownClientTypeEntry = RemotingConfigHandler.IsWellKnownClientType(activationType);
      string str1 = knownClientTypeEntry == null ? (string) null : knownClientTypeEntry.ObjectUrl;
      if (str1 != null)
      {
        ctorMsg.Properties[(object) "Connect"] = (object) str1;
        return false;
      }
      ActivatedClientTypeEntry activatedClientTypeEntry = RemotingConfigHandler.IsRemotelyActivatedClientType(activationType);
      string str2 = (string) null;
      if (activatedClientTypeEntry == null)
      {
        object[] activationAttributes = ctorMsg.CallSiteActivationAttributes;
        if (activationAttributes != null)
        {
          for (int index = 0; index < activationAttributes.Length; ++index)
          {
            UrlAttribute urlAttribute = activationAttributes[index] as UrlAttribute;
            if (urlAttribute != null)
              str2 = urlAttribute.UrlValue;
          }
        }
        if (str2 == null)
          return true;
      }
      else
        str2 = activatedClientTypeEntry.ApplicationUrl;
      string str3 = str2.EndsWith("/", StringComparison.Ordinal) ? str2 + "RemoteActivationService.rem" : str2 + "/RemoteActivationService.rem";
      ctorMsg.Properties[(object) "Remote"] = (object) str3;
      return false;
    }

    [SecurityCritical]
    public override void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
    {
      if (!ctorMsg.Properties.Contains((object) "Remote"))
        return;
      AppDomainLevelActivator domainLevelActivator = new AppDomainLevelActivator((string) ctorMsg.Properties[(object) "Remote"]);
      IActivator activator = ctorMsg.Activator;
      if (activator.Level < ActivatorLevel.AppDomain)
      {
        domainLevelActivator.NextActivator = activator;
        ctorMsg.Activator = (IActivator) domainLevelActivator;
      }
      else if (activator.NextActivator == null)
      {
        activator.NextActivator = (IActivator) domainLevelActivator;
      }
      else
      {
        while (activator.NextActivator.Level >= ActivatorLevel.AppDomain)
          activator = activator.NextActivator;
        domainLevelActivator.NextActivator = activator.NextActivator;
        activator.NextActivator = (IActivator) domainLevelActivator;
      }
    }

    public virtual IActivator NextActivator
    {
      [SecurityCritical] get
      {
        return (IActivator) null;
      }
      [SecurityCritical] set
      {
        throw new InvalidOperationException();
      }
    }

    public virtual ActivatorLevel Level
    {
      [SecurityCritical] get
      {
        return ActivatorLevel.AppDomain;
      }
    }

    private static MethodBase GetMethodBase(IConstructionCallMessage msg)
    {
      MethodBase methodBase = msg.MethodBase;
      if ((MethodBase) null == methodBase)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MethodMissing"), (object) msg.MethodName, (object) msg.TypeName));
      return methodBase;
    }

    [SecurityCritical]
    [ComVisible(true)]
    public virtual IConstructionReturnMessage Activate(IConstructionCallMessage ctorMsg)
    {
      if (ctorMsg == null)
        throw new ArgumentNullException(nameof (ctorMsg));
      if (ctorMsg.Properties.Contains((object) "Remote"))
        return LocalActivator.DoRemoteActivation(ctorMsg);
      if (!ctorMsg.Properties.Contains((object) "Permission"))
        return ctorMsg.Activator.Activate(ctorMsg);
      Type activationType = ctorMsg.ActivationType;
      object[] activationAttributes = (object[]) null;
      if (activationType.IsContextful)
      {
        IList contextProperties = ctorMsg.ContextProperties;
        if (contextProperties != null && contextProperties.Count > 0)
          activationAttributes = new object[1]
          {
            (object) new RemotePropertyHolderAttribute(contextProperties)
          };
      }
      RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(LocalActivator.GetMethodBase(ctorMsg));
      object[] args = Message.CoerceArgs((IMethodMessage) ctorMsg, reflectionCachedData.Parameters);
      object serverObj = Activator.CreateInstance(activationType, args, activationAttributes);
      if (RemotingServices.IsClientProxy(serverObj))
      {
        RedirectionProxy redirectionProxy = new RedirectionProxy((MarshalByRefObject) serverObj, activationType);
        RemotingServices.MarshalInternal((MarshalByRefObject) redirectionProxy, (string) null, activationType);
        serverObj = (object) redirectionProxy;
      }
      return ActivationServices.SetupConstructionReply(serverObj, ctorMsg, (Exception) null);
    }

    internal static IConstructionReturnMessage DoRemoteActivation(IConstructionCallMessage ctorMsg)
    {
      string property = (string) ctorMsg.Properties[(object) "Remote"];
      IActivator activator;
      try
      {
        activator = (IActivator) RemotingServices.Connect(typeof (IActivator), property);
      }
      catch (Exception ex)
      {
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Activation_ConnectFailed"), (object) ex));
      }
      ctorMsg.Properties.Remove((object) "Remote");
      return activator.Activate(ctorMsg);
    }
  }
}

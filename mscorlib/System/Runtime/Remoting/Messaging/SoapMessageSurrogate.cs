// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.SoapMessageSurrogate
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Security;
using System.Text;

namespace System.Runtime.Remoting.Messaging
{
  internal class SoapMessageSurrogate : ISerializationSurrogate
  {
    private static Type _voidType = typeof (void);
    private static Type _soapFaultType = typeof (SoapFault);
    private string DefaultFakeRecordAssemblyName = "http://schemas.microsoft.com/urt/SystemRemotingSoapTopRecord";
    private object _rootObj;
    [SecurityCritical]
    private RemotingSurrogateSelector _ss;

    [SecurityCritical]
    internal SoapMessageSurrogate(RemotingSurrogateSelector ss)
    {
      this._ss = ss;
    }

    internal void SetRootObject(object obj)
    {
      this._rootObj = obj;
    }

    [SecurityCritical]
    internal virtual string[] GetInArgNames(IMethodCallMessage m, int c)
    {
      string[] strArray = new string[c];
      for (int index = 0; index < c; ++index)
      {
        string str = m.GetInArgName(index) ?? "__param" + (object) index;
        strArray[index] = str;
      }
      return strArray;
    }

    [SecurityCritical]
    internal virtual string[] GetNames(IMethodCallMessage m, int c)
    {
      string[] strArray = new string[c];
      for (int index = 0; index < c; ++index)
      {
        string str = m.GetArgName(index) ?? "__param" + (object) index;
        strArray[index] = str;
      }
      return strArray;
    }

    [SecurityCritical]
    public virtual void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      if (obj != null && obj != this._rootObj)
      {
        new MessageSurrogate(this._ss).GetObjectData(obj, info, context);
      }
      else
      {
        IMethodReturnMessage methodReturnMessage = obj as IMethodReturnMessage;
        if (methodReturnMessage != null)
        {
          if (methodReturnMessage.Exception == null)
          {
            MethodBase methodBase = methodReturnMessage.MethodBase;
            SoapMethodAttribute cachedSoapAttribute = (SoapMethodAttribute) InternalRemotingServices.GetCachedSoapAttribute((object) methodBase);
            string responseXmlElementName = cachedSoapAttribute.ResponseXmlElementName;
            string responseXmlNamespace = cachedSoapAttribute.ResponseXmlNamespace;
            string returnXmlElementName = cachedSoapAttribute.ReturnXmlElementName;
            ArgMapper argMapper = new ArgMapper((IMethodMessage) methodReturnMessage, true);
            object[] args = argMapper.Args;
            info.FullTypeName = responseXmlElementName;
            info.AssemblyName = responseXmlNamespace;
            Type returnType = ((MethodInfo) methodBase).ReturnType;
            if (!(returnType == (Type) null) && !(returnType == SoapMessageSurrogate._voidType))
              info.AddValue(returnXmlElementName, methodReturnMessage.ReturnValue, returnType);
            if (args == null)
              return;
            Type[] argTypes = argMapper.ArgTypes;
            for (int argNum = 0; argNum < args.Length; ++argNum)
            {
              string name = argMapper.GetArgName(argNum);
              if (name == null || name.Length == 0)
                name = "__param" + (object) argNum;
              info.AddValue(name, args[argNum], argTypes[argNum].IsByRef ? argTypes[argNum].GetElementType() : argTypes[argNum]);
            }
          }
          else
          {
            object data = CallContext.GetData("__ClientIsClr");
            bool flag1 = data == null || (bool) data;
            info.FullTypeName = "FormatterWrapper";
            info.AssemblyName = this.DefaultFakeRecordAssemblyName;
            Exception exception = methodReturnMessage.Exception;
            StringBuilder stringBuilder = new StringBuilder();
            bool flag2 = false;
            for (; exception != null; exception = exception.InnerException)
            {
              if (exception.Message.StartsWith("MustUnderstand", StringComparison.Ordinal))
                flag2 = true;
              stringBuilder.Append(" **** ");
              stringBuilder.Append(exception.GetType().FullName);
              stringBuilder.Append(" - ");
              stringBuilder.Append(exception.Message);
            }
            ServerFault serverFault = !flag1 ? new ServerFault(methodReturnMessage.Exception.GetType().AssemblyQualifiedName, stringBuilder.ToString(), methodReturnMessage.Exception.StackTrace) : new ServerFault(methodReturnMessage.Exception);
            string faultCode = "Server";
            if (flag2)
              faultCode = "MustUnderstand";
            SoapFault soapFault = new SoapFault(faultCode, stringBuilder.ToString(), (string) null, serverFault);
            info.AddValue("__WrappedObject", (object) soapFault, SoapMessageSurrogate._soapFaultType);
          }
        }
        else
        {
          IMethodCallMessage m = (IMethodCallMessage) obj;
          MethodBase methodBase = m.MethodBase;
          string namespaceForMethodCall = SoapServices.GetXmlNamespaceForMethodCall(methodBase);
          object[] inArgs = m.InArgs;
          string[] inArgNames = this.GetInArgNames(m, inArgs.Length);
          Type[] methodSignature = (Type[]) m.MethodSignature;
          info.FullTypeName = m.MethodName;
          info.AssemblyName = namespaceForMethodCall;
          int[] marshalRequestArgMap = InternalRemotingServices.GetReflectionCachedData(methodBase).MarshalRequestArgMap;
          for (int index1 = 0; index1 < inArgs.Length; ++index1)
          {
            string name = inArgNames[index1] == null || inArgNames[index1].Length == 0 ? "__param" + (object) index1 : inArgNames[index1];
            int index2 = marshalRequestArgMap[index1];
            Type type = !methodSignature[index2].IsByRef ? methodSignature[index2] : methodSignature[index2].GetElementType();
            info.AddValue(name, inArgs[index1], type);
          }
        }
      }
    }

    [SecurityCritical]
    public virtual object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_PopulateData"));
    }
  }
}

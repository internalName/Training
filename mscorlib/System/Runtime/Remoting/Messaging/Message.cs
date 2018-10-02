// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.Message
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
  [Serializable]
  internal class Message : IMethodCallMessage, IMethodMessage, IMessage, IInternalMessage, ISerializable
  {
    internal static string CallContextKey = "__CallContext";
    internal static string UriKey = "__Uri";
    internal const int Sync = 0;
    internal const int BeginAsync = 1;
    internal const int EndAsync = 2;
    internal const int Ctor = 4;
    internal const int OneWay = 8;
    internal const int CallMask = 15;
    internal const int FixedArgs = 16;
    internal const int VarArgs = 32;
    private string _MethodName;
    private Type[] _MethodSignature;
    private MethodBase _MethodBase;
    private object _properties;
    private string _URI;
    private string _typeName;
    private Exception _Fault;
    private Identity _ID;
    private ServerIdentity _srvID;
    private ArgMapper _argMapper;
    [SecurityCritical]
    private LogicalCallContext _callContext;
    private IntPtr _frame;
    private IntPtr _methodDesc;
    private IntPtr _metaSigHolder;
    private IntPtr _delegateMD;
    private IntPtr _governingType;
    private int _flags;
    private bool _initDone;

    public virtual Exception GetFault()
    {
      return this._Fault;
    }

    public virtual void SetFault(Exception e)
    {
      this._Fault = e;
    }

    internal virtual void SetOneWay()
    {
      this._flags |= 8;
    }

    public virtual int GetCallType()
    {
      this.InitIfNecessary();
      return this._flags;
    }

    internal IntPtr GetFramePtr()
    {
      return this._frame;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern void GetAsyncBeginInfo(out AsyncCallback acbd, out object state);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern object GetThisPtr();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern IAsyncResult GetAsyncResult();

    public void Init()
    {
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern object GetReturnValue();

    internal Message()
    {
    }

    [SecurityCritical]
    internal void InitFields(MessageData msgData)
    {
      this._frame = msgData.pFrame;
      this._delegateMD = msgData.pDelegateMD;
      this._methodDesc = msgData.pMethodDesc;
      this._flags = msgData.iFlags;
      this._initDone = true;
      this._metaSigHolder = msgData.pSig;
      this._governingType = msgData.thGoverningType;
      this._MethodName = (string) null;
      this._MethodSignature = (Type[]) null;
      this._MethodBase = (MethodBase) null;
      this._URI = (string) null;
      this._Fault = (Exception) null;
      this._ID = (Identity) null;
      this._srvID = (ServerIdentity) null;
      this._callContext = (LogicalCallContext) null;
      if (this._properties == null)
        return;
      ((IDictionary) this._properties).Clear();
    }

    private void InitIfNecessary()
    {
      if (this._initDone)
        return;
      this.Init();
      this._initDone = true;
    }

    ServerIdentity IInternalMessage.ServerIdentityObject
    {
      [SecurityCritical] get
      {
        return this._srvID;
      }
      [SecurityCritical] set
      {
        this._srvID = value;
      }
    }

    Identity IInternalMessage.IdentityObject
    {
      [SecurityCritical] get
      {
        return this._ID;
      }
      [SecurityCritical] set
      {
        this._ID = value;
      }
    }

    [SecurityCritical]
    void IInternalMessage.SetURI(string URI)
    {
      this._URI = URI;
    }

    [SecurityCritical]
    void IInternalMessage.SetCallContext(LogicalCallContext callContext)
    {
      this._callContext = callContext;
    }

    [SecurityCritical]
    bool IInternalMessage.HasProperties()
    {
      return this._properties != null;
    }

    public IDictionary Properties
    {
      [SecurityCritical] get
      {
        if (this._properties == null)
          Interlocked.CompareExchange(ref this._properties, (object) new MCMDictionary((IMethodCallMessage) this, (IDictionary) null), (object) null);
        return (IDictionary) this._properties;
      }
    }

    public string Uri
    {
      [SecurityCritical] get
      {
        return this._URI;
      }
      set
      {
        this._URI = value;
      }
    }

    public bool HasVarArgs
    {
      [SecurityCritical] get
      {
        if ((this._flags & 16) == 0 && (this._flags & 32) == 0)
        {
          if (!this.InternalHasVarArgs())
            this._flags |= 16;
          else
            this._flags |= 32;
        }
        return 1 == (this._flags & 32);
      }
    }

    public int ArgCount
    {
      [SecurityCritical] get
      {
        return this.InternalGetArgCount();
      }
    }

    [SecurityCritical]
    public object GetArg(int argNum)
    {
      return this.InternalGetArg(argNum);
    }

    [SecurityCritical]
    public string GetArgName(int index)
    {
      if (index >= this.ArgCount)
        throw new ArgumentOutOfRangeException(nameof (index));
      ParameterInfo[] parameters = InternalRemotingServices.GetReflectionCachedData(this.GetMethodBase()).Parameters;
      if (index < parameters.Length)
        return parameters[index].Name;
      return "VarArg" + (object) (index - parameters.Length);
    }

    public object[] Args
    {
      [SecurityCritical] get
      {
        return this.InternalGetArgs();
      }
    }

    public int InArgCount
    {
      [SecurityCritical] get
      {
        if (this._argMapper == null)
          this._argMapper = new ArgMapper((IMethodMessage) this, false);
        return this._argMapper.ArgCount;
      }
    }

    [SecurityCritical]
    public object GetInArg(int argNum)
    {
      if (this._argMapper == null)
        this._argMapper = new ArgMapper((IMethodMessage) this, false);
      return this._argMapper.GetArg(argNum);
    }

    [SecurityCritical]
    public string GetInArgName(int index)
    {
      if (this._argMapper == null)
        this._argMapper = new ArgMapper((IMethodMessage) this, false);
      return this._argMapper.GetArgName(index);
    }

    public object[] InArgs
    {
      [SecurityCritical] get
      {
        if (this._argMapper == null)
          this._argMapper = new ArgMapper((IMethodMessage) this, false);
        return this._argMapper.Args;
      }
    }

    [SecurityCritical]
    private void UpdateNames()
    {
      RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(this.GetMethodBase());
      this._typeName = reflectionCachedData.TypeAndAssemblyName;
      this._MethodName = reflectionCachedData.MethodName;
    }

    public string MethodName
    {
      [SecurityCritical] get
      {
        if (this._MethodName == null)
          this.UpdateNames();
        return this._MethodName;
      }
    }

    public string TypeName
    {
      [SecurityCritical] get
      {
        if (this._typeName == null)
          this.UpdateNames();
        return this._typeName;
      }
    }

    public object MethodSignature
    {
      [SecurityCritical] get
      {
        if (this._MethodSignature == null)
          this._MethodSignature = Message.GenerateMethodSignature(this.GetMethodBase());
        return (object) this._MethodSignature;
      }
    }

    public LogicalCallContext LogicalCallContext
    {
      [SecurityCritical] get
      {
        return this.GetLogicalCallContext();
      }
    }

    public MethodBase MethodBase
    {
      [SecurityCritical] get
      {
        return this.GetMethodBase();
      }
    }

    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
    }

    [SecurityCritical]
    internal MethodBase GetMethodBase()
    {
      if ((MethodBase) null == this._MethodBase)
        this._MethodBase = RuntimeType.GetMethodBase(Type.GetTypeFromHandleUnsafe(this._governingType), (IRuntimeMethodInfo) new RuntimeMethodInfoStub(this._methodDesc, (object) null));
      return this._MethodBase;
    }

    [SecurityCritical]
    internal LogicalCallContext SetLogicalCallContext(LogicalCallContext callCtx)
    {
      LogicalCallContext callContext = this._callContext;
      this._callContext = callCtx;
      return callContext;
    }

    [SecurityCritical]
    internal LogicalCallContext GetLogicalCallContext()
    {
      if (this._callContext == null)
        this._callContext = new LogicalCallContext();
      return this._callContext;
    }

    [SecurityCritical]
    internal static Type[] GenerateMethodSignature(MethodBase mb)
    {
      ParameterInfo[] parameters = InternalRemotingServices.GetReflectionCachedData(mb).Parameters;
      Type[] typeArray = new Type[parameters.Length];
      for (int index = 0; index < parameters.Length; ++index)
        typeArray[index] = parameters[index].ParameterType;
      return typeArray;
    }

    [SecurityCritical]
    internal static object[] CoerceArgs(IMethodMessage m)
    {
      RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(m.MethodBase);
      return Message.CoerceArgs(m, reflectionCachedData.Parameters);
    }

    [SecurityCritical]
    internal static object[] CoerceArgs(IMethodMessage m, ParameterInfo[] pi)
    {
      return Message.CoerceArgs(m.MethodBase, m.Args, pi);
    }

    [SecurityCritical]
    internal static object[] CoerceArgs(MethodBase mb, object[] args, ParameterInfo[] pi)
    {
      if (pi == null)
        throw new ArgumentNullException(nameof (pi));
      if (pi.Length != args.Length)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_ArgMismatch"), (object) mb.DeclaringType.FullName, (object) mb.Name, (object) args.Length, (object) pi.Length));
      for (int index = 0; index < pi.Length; ++index)
      {
        ParameterInfo parameterInfo = pi[index];
        Type parameterType = parameterInfo.ParameterType;
        object obj = args[index];
        if (obj != null)
          args[index] = Message.CoerceArg(obj, parameterType);
        else if (parameterType.IsByRef)
        {
          Type elementType = parameterType.GetElementType();
          if (elementType.IsValueType)
          {
            if (parameterInfo.IsOut)
              args[index] = Activator.CreateInstance(elementType, true);
            else if (!elementType.IsGenericType || !(elementType.GetGenericTypeDefinition() == typeof (Nullable<>)))
              throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MissingArgValue"), (object) elementType.FullName, (object) index));
          }
        }
        else if (parameterType.IsValueType && (!parameterType.IsGenericType || !(parameterType.GetGenericTypeDefinition() == typeof (Nullable<>))))
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MissingArgValue"), (object) parameterType.FullName, (object) index));
      }
      return args;
    }

    [SecurityCritical]
    internal static object CoerceArg(object value, Type pt)
    {
      object obj = (object) null;
      if (value != null)
      {
        Exception InnerException = (Exception) null;
        try
        {
          if (pt.IsByRef)
            pt = pt.GetElementType();
          obj = !pt.IsInstanceOfType(value) ? Convert.ChangeType(value, pt, (IFormatProvider) CultureInfo.InvariantCulture) : value;
        }
        catch (Exception ex)
        {
          InnerException = ex;
        }
        if (obj == null)
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_CoercionFailed"), !RemotingServices.IsTransparentProxy(value) ? (object) value.ToString() : (object) typeof (MarshalByRefObject).ToString(), (object) pt), InnerException);
      }
      return obj;
    }

    [SecurityCritical]
    internal static object SoapCoerceArg(object value, Type pt, Hashtable keyToNamespaceTable)
    {
      object obj = (object) null;
      if (value != null)
      {
        try
        {
          if (pt.IsByRef)
            pt = pt.GetElementType();
          if (pt.IsInstanceOfType(value))
          {
            obj = value;
          }
          else
          {
            string s = value as string;
            if (s != null)
            {
              if (pt == typeof (double))
                obj = !(s == "INF") ? (!(s == "-INF") ? (object) double.Parse(s, (IFormatProvider) CultureInfo.InvariantCulture) : (object) double.NegativeInfinity) : (object) double.PositiveInfinity;
              else if (pt == typeof (float))
                obj = !(s == "INF") ? (!(s == "-INF") ? (object) float.Parse(s, (IFormatProvider) CultureInfo.InvariantCulture) : (object) float.NegativeInfinity) : (object) float.PositiveInfinity;
              else if (SoapType.typeofISoapXsd.IsAssignableFrom(pt))
              {
                if (pt == SoapType.typeofSoapTime)
                  obj = (object) SoapTime.Parse(s);
                else if (pt == SoapType.typeofSoapDate)
                  obj = (object) SoapDate.Parse(s);
                else if (pt == SoapType.typeofSoapYearMonth)
                  obj = (object) SoapYearMonth.Parse(s);
                else if (pt == SoapType.typeofSoapYear)
                  obj = (object) SoapYear.Parse(s);
                else if (pt == SoapType.typeofSoapMonthDay)
                  obj = (object) SoapMonthDay.Parse(s);
                else if (pt == SoapType.typeofSoapDay)
                  obj = (object) SoapDay.Parse(s);
                else if (pt == SoapType.typeofSoapMonth)
                  obj = (object) SoapMonth.Parse(s);
                else if (pt == SoapType.typeofSoapHexBinary)
                  obj = (object) SoapHexBinary.Parse(s);
                else if (pt == SoapType.typeofSoapBase64Binary)
                  obj = (object) SoapBase64Binary.Parse(s);
                else if (pt == SoapType.typeofSoapInteger)
                  obj = (object) SoapInteger.Parse(s);
                else if (pt == SoapType.typeofSoapPositiveInteger)
                  obj = (object) SoapPositiveInteger.Parse(s);
                else if (pt == SoapType.typeofSoapNonPositiveInteger)
                  obj = (object) SoapNonPositiveInteger.Parse(s);
                else if (pt == SoapType.typeofSoapNonNegativeInteger)
                  obj = (object) SoapNonNegativeInteger.Parse(s);
                else if (pt == SoapType.typeofSoapNegativeInteger)
                  obj = (object) SoapNegativeInteger.Parse(s);
                else if (pt == SoapType.typeofSoapAnyUri)
                  obj = (object) SoapAnyUri.Parse(s);
                else if (pt == SoapType.typeofSoapQName)
                {
                  obj = (object) SoapQName.Parse(s);
                  SoapQName soapQname = (SoapQName) obj;
                  soapQname.Namespace = soapQname.Key.Length != 0 ? (string) keyToNamespaceTable[(object) ("xmlns:" + soapQname.Key)] : (string) keyToNamespaceTable[(object) "xmlns"];
                }
                else if (pt == SoapType.typeofSoapNotation)
                  obj = (object) SoapNotation.Parse(s);
                else if (pt == SoapType.typeofSoapNormalizedString)
                  obj = (object) SoapNormalizedString.Parse(s);
                else if (pt == SoapType.typeofSoapToken)
                  obj = (object) SoapToken.Parse(s);
                else if (pt == SoapType.typeofSoapLanguage)
                  obj = (object) SoapLanguage.Parse(s);
                else if (pt == SoapType.typeofSoapName)
                  obj = (object) SoapName.Parse(s);
                else if (pt == SoapType.typeofSoapIdrefs)
                  obj = (object) SoapIdrefs.Parse(s);
                else if (pt == SoapType.typeofSoapEntities)
                  obj = (object) SoapEntities.Parse(s);
                else if (pt == SoapType.typeofSoapNmtoken)
                  obj = (object) SoapNmtoken.Parse(s);
                else if (pt == SoapType.typeofSoapNmtokens)
                  obj = (object) SoapNmtokens.Parse(s);
                else if (pt == SoapType.typeofSoapNcName)
                  obj = (object) SoapNcName.Parse(s);
                else if (pt == SoapType.typeofSoapId)
                  obj = (object) SoapId.Parse(s);
                else if (pt == SoapType.typeofSoapIdref)
                  obj = (object) SoapIdref.Parse(s);
                else if (pt == SoapType.typeofSoapEntity)
                  obj = (object) SoapEntity.Parse(s);
              }
              else if (pt == typeof (bool))
              {
                if (s == "1" || s == "true")
                {
                  obj = (object) true;
                }
                else
                {
                  if (!(s == "0") && !(s == "false"))
                    throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_CoercionFailed"), (object) s, (object) pt));
                  obj = (object) false;
                }
              }
              else
                obj = !(pt == typeof (DateTime)) ? (!pt.IsPrimitive ? (!(pt == typeof (TimeSpan)) ? (!(pt == typeof (char)) ? Convert.ChangeType(value, pt, (IFormatProvider) CultureInfo.InvariantCulture) : (object) s[0]) : (object) SoapDuration.Parse(s)) : Convert.ChangeType(value, pt, (IFormatProvider) CultureInfo.InvariantCulture)) : (object) SoapDateTime.Parse(s);
            }
            else
              obj = Convert.ChangeType(value, pt, (IFormatProvider) CultureInfo.InvariantCulture);
          }
        }
        catch (Exception ex)
        {
        }
        if (obj == null)
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_CoercionFailed"), !RemotingServices.IsTransparentProxy(value) ? (object) value.ToString() : (object) typeof (MarshalByRefObject).ToString(), (object) pt));
      }
      return obj;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern bool InternalHasVarArgs();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern int InternalGetArgCount();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern object InternalGetArg(int argNum);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern object[] InternalGetArgs();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern void PropagateOutParameters(object[] OutArgs, object retVal);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern bool Dispatch(object target);

    [SecurityCritical]
    [Conditional("_REMOTING_DEBUG")]
    public static void DebugOut(string s)
    {
      Message.OutToUnmanagedDebugger("\nRMTING: Thrd " + (object) Thread.CurrentThread.GetHashCode() + " : " + s);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void OutToUnmanagedDebugger(string s);

    [SecurityCritical]
    internal static LogicalCallContext PropagateCallContextFromMessageToThread(IMessage msg)
    {
      return CallContext.SetLogicalCallContext((LogicalCallContext) msg.Properties[(object) Message.CallContextKey]);
    }

    [SecurityCritical]
    internal static void PropagateCallContextFromThreadToMessage(IMessage msg)
    {
      LogicalCallContext logicalCallContext = Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext;
      msg.Properties[(object) Message.CallContextKey] = (object) logicalCallContext;
    }

    [SecurityCritical]
    internal static void PropagateCallContextFromThreadToMessage(IMessage msg, LogicalCallContext oldcctx)
    {
      Message.PropagateCallContextFromThreadToMessage(msg);
      CallContext.SetLogicalCallContext(oldcctx);
    }
  }
}

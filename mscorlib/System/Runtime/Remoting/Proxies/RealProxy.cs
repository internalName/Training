// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Proxies.RealProxy
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.Runtime.Remoting.Proxies
{
  /// <summary>Предоставляет базовую функциональность для прокси.</summary>
  [SecurityCritical]
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public abstract class RealProxy
  {
    private static IntPtr _defaultStub = RealProxy.GetDefaultStub();
    private static IntPtr _defaultStubValue = new IntPtr(-1);
    private static object _defaultStubData = (object) RealProxy._defaultStubValue;
    private object _tp;
    private object _identity;
    private MarshalByRefObject _serverObject;
    private RealProxyFlags _flags;
    internal GCHandle _srvIdentity;
    internal int _optFlags;
    internal int _domainID;

    [SecuritySafeCritical]
    static RealProxy()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" /> класс, представляющий удаленный объект указанного <see cref="T:System.Type" />.
    /// </summary>
    /// <param name="classToProxy">
    ///   <see cref="T:System.Type" /> Удаленного объекта, для которого необходимо создать учетную запись-посредник.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="classToProxy" /> не является интерфейсом и не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// </exception>
    [SecurityCritical]
    protected RealProxy(Type classToProxy)
      : this(classToProxy, (IntPtr) 0, (object) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" />.
    /// </summary>
    /// <param name="classToProxy">
    ///   <see cref="T:System.Type" /> Удаленного объекта, для которого необходимо создать учетную запись-посредник.
    /// </param>
    /// <param name="stub">
    ///   Заглушка для связи с новым экземпляром прокси.
    /// </param>
    /// <param name="stubData">
    ///   Данные заглушки для установки заданной заглушки и нового экземпляра прокси.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="classToProxy" /> не является интерфейсом и не является производным от <see cref="T:System.MarshalByRefObject" />.
    /// </exception>
    [SecurityCritical]
    protected RealProxy(Type classToProxy, IntPtr stub, object stubData)
    {
      if (!classToProxy.IsMarshalByRef && !classToProxy.IsInterface)
        throw new ArgumentException(Environment.GetResourceString("Remoting_Proxy_ProxyTypeIsNotMBR"));
      if ((IntPtr) 0 == stub)
      {
        stub = RealProxy._defaultStub;
        stubData = RealProxy._defaultStubData;
      }
      this._tp = (object) null;
      if (stubData == null)
        throw new ArgumentNullException("stubdata");
      this._tp = RemotingServices.CreateTransparentProxy(this, classToProxy, stub, stubData);
      if (!(this is RemotingProxy))
        return;
      this._flags |= RealProxyFlags.RemotingProxy;
    }

    internal bool IsRemotingProxy()
    {
      return (this._flags & RealProxyFlags.RemotingProxy) == RealProxyFlags.RemotingProxy;
    }

    internal bool Initialized
    {
      get
      {
        return (this._flags & RealProxyFlags.Initialized) == RealProxyFlags.Initialized;
      }
      set
      {
        if (value)
          this._flags |= RealProxyFlags.Initialized;
        else
          this._flags &= ~RealProxyFlags.Initialized;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр объекта <see cref="T:System.Type" /> удаленного объекта, текущий экземпляр <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" /> с заданным <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />.
    /// </summary>
    /// <param name="ctorMsg">
    ///   Сообщение о вызове конструирования, содержащее параметры конструктора для нового экземпляра удаленного объекта, представленного текущим <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" />.
    ///    Может иметь значение <see langword="null" />.
    /// </param>
    /// <returns>Результат запроса конструирования.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения UnmanagedCode.
    /// </exception>
    [SecurityCritical]
    [ComVisible(true)]
    public IConstructionReturnMessage InitializeServerObject(IConstructionCallMessage ctorMsg)
    {
      IConstructionReturnMessage constructionReturnMessage = (IConstructionReturnMessage) null;
      if (this._serverObject == null)
      {
        Type proxiedType = this.GetProxiedType();
        if (ctorMsg != null && ctorMsg.ActivationType != proxiedType)
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Proxy_BadTypeForActivation"), (object) proxiedType.FullName, (object) ctorMsg.ActivationType));
        this._serverObject = RemotingServices.AllocateUninitializedObject(proxiedType);
        this.SetContextForDefaultStub();
        MarshalByRefObject transparentProxy = (MarshalByRefObject) this.GetTransparentProxy();
        IMethodReturnMessage methodReturnMessage = (IMethodReturnMessage) null;
        Exception e = (Exception) null;
        if (ctorMsg != null)
        {
          methodReturnMessage = RemotingServices.ExecuteMessage(transparentProxy, (IMethodCallMessage) ctorMsg);
          e = methodReturnMessage.Exception;
        }
        else
        {
          try
          {
            RemotingServices.CallDefaultCtor((object) transparentProxy);
          }
          catch (Exception ex)
          {
            e = ex;
          }
        }
        if (e == null)
        {
          object[] outArgs = methodReturnMessage == null ? (object[]) null : methodReturnMessage.OutArgs;
          int outArgsCount = outArgs == null ? 0 : outArgs.Length;
          LogicalCallContext callCtx = methodReturnMessage == null ? (LogicalCallContext) null : methodReturnMessage.LogicalCallContext;
          constructionReturnMessage = (IConstructionReturnMessage) new ConstructorReturnMessage(transparentProxy, outArgs, outArgsCount, callCtx, ctorMsg);
          this.SetupIdentity();
          if (this.IsRemotingProxy())
            this.Initialized = true;
        }
        else
          constructionReturnMessage = (IConstructionReturnMessage) new ConstructorReturnMessage(e, ctorMsg);
      }
      return constructionReturnMessage;
    }

    /// <summary>
    ///   Возвращает серверный объект, представленный текущим экземпляром прокси.
    /// </summary>
    /// <returns>
    ///   Серверный объект, представленный текущим экземпляром прокси.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения UnmanagedCode.
    /// </exception>
    [SecurityCritical]
    protected MarshalByRefObject GetUnwrappedServer()
    {
      return this.UnwrappedServerObject;
    }

    /// <summary>
    ///   Отсоединяет текущий экземпляр прокси от удаленного серверного объекта, который он представляет.
    /// </summary>
    /// <returns>Обособленный серверный объект.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения UnmanagedCode.
    /// </exception>
    [SecurityCritical]
    protected MarshalByRefObject DetachServer()
    {
      object transparentProxy = this.GetTransparentProxy();
      if (transparentProxy != null)
        RemotingServices.ResetInterfaceCache(transparentProxy);
      MarshalByRefObject serverObject = this._serverObject;
      this._serverObject = (MarshalByRefObject) null;
      serverObject.__ResetServerIdentity();
      return serverObject;
    }

    /// <summary>
    ///   Присоединяет текущий экземпляр прокси к заданному удаленному <see cref="T:System.MarshalByRefObject" />.
    /// </summary>
    /// <param name="s">
    ///   <see cref="T:System.MarshalByRefObject" /> Представляющий текущий экземпляр прокси-сервера.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения UnmanagedCode.
    /// </exception>
    [SecurityCritical]
    protected void AttachServer(MarshalByRefObject s)
    {
      object transparentProxy = this.GetTransparentProxy();
      if (transparentProxy != null)
        RemotingServices.ResetInterfaceCache(transparentProxy);
      this.AttachServerHelper(s);
    }

    [SecurityCritical]
    private void SetupIdentity()
    {
      if (this._identity != null)
        return;
      this._identity = (object) IdentityHolder.FindOrCreateServerIdentity(this._serverObject, (string) null, 0);
      ((Identity) this._identity).RaceSetTransparentProxy(this.GetTransparentProxy());
    }

    [SecurityCritical]
    private void SetContextForDefaultStub()
    {
      if (!(this.GetStub() == RealProxy._defaultStub))
        return;
      object stubData = RealProxy.GetStubData(this);
      if (!(stubData is IntPtr) || !((IntPtr) stubData).Equals((object) RealProxy._defaultStubValue))
        return;
      RealProxy.SetStubData(this, (object) Thread.CurrentContext.InternalContextID);
    }

    [SecurityCritical]
    internal bool DoContextsMatch()
    {
      bool flag = false;
      if (this.GetStub() == RealProxy._defaultStub)
      {
        object stubData = RealProxy.GetStubData(this);
        if (stubData is IntPtr && ((IntPtr) stubData).Equals((object) Thread.CurrentContext.InternalContextID))
          flag = true;
      }
      return flag;
    }

    [SecurityCritical]
    internal void AttachServerHelper(MarshalByRefObject s)
    {
      if (s == null || this._serverObject != null)
        throw new ArgumentException(Environment.GetResourceString("ArgumentNull_Generic"), nameof (s));
      this._serverObject = s;
      this.SetupIdentity();
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern IntPtr GetStub();

    /// <summary>Устанавливает данные заглушки для заданного прокси.</summary>
    /// <param name="rp">
    ///   Прокси-сервер, для которого требуется задать данные заглушки.
    /// </param>
    /// <param name="stubData">Новые данные заглушки.</param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения UnmanagedCode.
    /// </exception>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void SetStubData(RealProxy rp, object stubData);

    internal void SetSrvInfo(GCHandle srvIdentity, int domainID)
    {
      this._srvIdentity = srvIdentity;
      this._domainID = domainID;
    }

    /// <summary>
    ///   Извлекает данные заглушки, сохраненные для заданного прокси.
    /// </summary>
    /// <param name="rp">
    ///   Прокси-сервер, для которых заглушки запрашиваются данные.
    /// </param>
    /// <returns>Данные заглушки для заданного прокси.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения UnmanagedCode.
    /// </exception>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern object GetStubData(RealProxy rp);

    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern IntPtr GetDefaultStub();

    /// <summary>
    ///   Возвращает <see cref="T:System.Type" /> объекта, текущий экземпляр <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" /> представляет.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Type" /> Объекта, текущий экземпляр <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" /> представляет.
    /// </returns>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Type GetProxiedType();

    /// <summary>
    ///   При переопределении в производном классе, вызывает метод, который задан в предоставленном <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> на удаленный объект, представленный текущим экземпляром.
    /// </summary>
    /// <param name="msg">
    ///   Объект <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> содержащий <see cref="T:System.Collections.IDictionary" /> со сведениями о вызове метода.
    /// </param>
    /// <returns>
    ///   Сообщение, возвращенное вызванным методом, содержащее возвращаемое значение и <see langword="out" /> или <see langword="ref" /> Параметры.
    /// </returns>
    public abstract IMessage Invoke(IMessage msg);

    /// <summary>
    ///   Создает <see cref="T:System.Runtime.Remoting.ObjRef" /> для заданного типа объекта и регистрирует его с инфраструктурой удаленного взаимодействия в качестве объекта, активируемого клиентом.
    /// </summary>
    /// <param name="requestedType">
    ///   Тип объекта, который <see cref="T:System.Runtime.Remoting.ObjRef" /> создается.
    /// </param>
    /// <returns>
    ///   Новый экземпляр <see cref="T:System.Runtime.Remoting.ObjRef" /> созданный для указанного типа.
    /// </returns>
    [SecurityCritical]
    public virtual ObjRef CreateObjRef(Type requestedType)
    {
      if (this._identity == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_NoIdentityEntry"));
      return new ObjRef((MarshalByRefObject) this.GetTransparentProxy(), requestedType);
    }

    /// <summary>
    ///   Добавляет прозрачный прокси объекта, представленного текущим экземпляром <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" /> в указанный <see cref="T:System.Runtime.Serialization.SerializationInfo" />.
    /// </summary>
    /// <param name="info">
    ///   <see cref="T:System.Runtime.Serialization.SerializationInfo" /> В который сериализуется прозрачный прокси.
    /// </param>
    /// <param name="context">Источник и назначение сериализации.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="info" /> или параметра <paramref name="context" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения SerializationFormatter.
    /// </exception>
    [SecurityCritical]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      RemotingServices.GetObjectData(this.GetTransparentProxy(), info, context);
    }

    [SecurityCritical]
    private static void HandleReturnMessage(IMessage reqMsg, IMessage retMsg)
    {
      IMethodReturnMessage methodReturnMessage = retMsg as IMethodReturnMessage;
      if (retMsg == null || methodReturnMessage == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadType"));
      Exception exception = methodReturnMessage.Exception;
      if (exception != null)
        throw exception.PrepForRemoting();
      if (retMsg is StackBasedReturnMessage)
        return;
      if (reqMsg is Message)
      {
        RealProxy.PropagateOutParameters(reqMsg, methodReturnMessage.Args, methodReturnMessage.ReturnValue);
      }
      else
      {
        if (!(reqMsg is ConstructorCallMessage))
          return;
        RealProxy.PropagateOutParameters(reqMsg, methodReturnMessage.Args, (object) null);
      }
    }

    [SecurityCritical]
    internal static void PropagateOutParameters(IMessage msg, object[] outArgs, object returnValue)
    {
      Message message = msg as Message;
      if (message == null)
      {
        ConstructorCallMessage constructorCallMessage = msg as ConstructorCallMessage;
        if (constructorCallMessage != null)
          message = constructorCallMessage.GetMessage();
      }
      if (message == null)
        throw new ArgumentException(Environment.GetResourceString("Remoting_Proxy_ExpectedOriginalMessage"));
      RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(message.GetMethodBase());
      if (outArgs != null && outArgs.Length != 0)
      {
        object[] args = message.Args;
        ParameterInfo[] parameters = reflectionCachedData.Parameters;
        foreach (int marshalRequestArg in reflectionCachedData.MarshalRequestArgMap)
        {
          ParameterInfo parameterInfo = parameters[marshalRequestArg];
          if (parameterInfo.IsIn && parameterInfo.ParameterType.IsByRef && !parameterInfo.IsOut)
            outArgs[marshalRequestArg] = args[marshalRequestArg];
        }
        if (reflectionCachedData.NonRefOutArgMap.Length != 0)
        {
          foreach (int nonRefOutArg in reflectionCachedData.NonRefOutArgMap)
          {
            Array destinationArray = args[nonRefOutArg] as Array;
            if (destinationArray != null)
              Array.Copy((Array) outArgs[nonRefOutArg], destinationArray, destinationArray.Length);
          }
        }
        int[] outRefArgMap = reflectionCachedData.OutRefArgMap;
        if (outRefArgMap.Length != 0)
        {
          foreach (int index in outRefArgMap)
            RealProxy.ValidateReturnArg(outArgs[index], parameters[index].ParameterType);
        }
      }
      if ((message.GetCallType() & 15) != 1)
      {
        Type returnType = reflectionCachedData.ReturnType;
        if (returnType != (Type) null)
          RealProxy.ValidateReturnArg(returnValue, returnType);
      }
      message.PropagateOutParameters(outArgs, returnValue);
    }

    private static void ValidateReturnArg(object arg, Type paramType)
    {
      if (paramType.IsByRef)
        paramType = paramType.GetElementType();
      if (paramType.IsValueType)
      {
        if (arg == null)
        {
          if (!paramType.IsGenericType || !(paramType.GetGenericTypeDefinition() == typeof (Nullable<>)))
            throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_ReturnValueTypeCannotBeNull"));
        }
        else if (!paramType.IsInstanceOfType(arg))
          throw new InvalidCastException(Environment.GetResourceString("Remoting_Proxy_BadReturnType"));
      }
      else if (arg != null && !paramType.IsInstanceOfType(arg))
        throw new InvalidCastException(Environment.GetResourceString("Remoting_Proxy_BadReturnType"));
    }

    [SecurityCritical]
    internal static IMessage EndInvokeHelper(Message reqMsg, bool bProxyCase)
    {
      AsyncResult asyncResult = reqMsg.GetAsyncResult() as AsyncResult;
      IMessage message = (IMessage) null;
      if (asyncResult == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadAsyncResult"));
      if (asyncResult.AsyncDelegate != reqMsg.GetThisPtr())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MismatchedAsyncResult"));
      if (!asyncResult.IsCompleted)
        asyncResult.AsyncWaitHandle.WaitOne(-1, Thread.CurrentContext.IsThreadPoolAware);
      lock (asyncResult)
      {
        if (asyncResult.EndInvokeCalled)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EndInvokeCalledMultiple"));
        asyncResult.EndInvokeCalled = true;
        IMethodReturnMessage replyMessage = (IMethodReturnMessage) asyncResult.GetReplyMessage();
        if (!bProxyCase)
        {
          Exception exception = replyMessage.Exception;
          if (exception != null)
            throw exception.PrepForRemoting();
          reqMsg.PropagateOutParameters(replyMessage.Args, replyMessage.ReturnValue);
        }
        else
          message = (IMessage) replyMessage;
        Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.Merge(replyMessage.LogicalCallContext);
      }
      return message;
    }

    /// <summary>
    ///   Запрашивает неуправляемую ссылку на объект, представленный текущим экземпляром прокси.
    /// </summary>
    /// <param name="fIsMarshalled">
    ///   Значение <see langword="true" />, если ссылка на объект запрашивается для маршалинга в удаленное расположение. Значение <see langword="false" />, если ссылка на объект запрашивается для связи с неуправляемыми объектами в текущем процессе через COM.
    /// </param>
    /// <returns>
    ///   Указатель на COM Callable Wrapper, если ссылка на объект запрашивается для связи с неуправляемыми объектами в текущем процессе через COM, или указатель на кэшированный или вновь созданный COM-интерфейс <see langword="IUnknown" />, если ссылка на объект запрашивается для маршалинга в удаленное расположение.
    /// </returns>
    [SecurityCritical]
    public virtual IntPtr GetCOMIUnknown(bool fIsMarshalled)
    {
      return MarshalByRefObject.GetComIUnknown((MarshalByRefObject) this.GetTransparentProxy());
    }

    /// <summary>
    ///   Сохраняет неуправляемый прокси объекта, представленный текущим экземпляром.
    /// </summary>
    /// <param name="i">
    ///   Указатель на <see langword="IUnknown" /> интерфейс для объекта, представленного текущим экземпляром прокси.
    /// </param>
    public virtual void SetCOMIUnknown(IntPtr i)
    {
    }

    /// <summary>
    ///   Запрашивает COM-интерфейс с указанным идентификатором.
    /// </summary>
    /// <param name="iid">Ссылка на запрашиваемый интерфейс.</param>
    /// <returns>Указатель на запрошенный интерфейс.</returns>
    public virtual IntPtr SupportsInterface(ref Guid iid)
    {
      return IntPtr.Zero;
    }

    /// <summary>
    ///   Возвращает прозрачный прокси для текущего экземпляра <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" />.
    /// </summary>
    /// <returns>
    ///   Прозрачный прокси для текущего экземпляра прокси-сервера.
    /// </returns>
    public virtual object GetTransparentProxy()
    {
      return this._tp;
    }

    internal MarshalByRefObject UnwrappedServerObject
    {
      get
      {
        return this._serverObject;
      }
    }

    internal virtual Identity IdentityObject
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
      {
        return (Identity) this._identity;
      }
      set
      {
        this._identity = (object) value;
      }
    }

    [SecurityCritical]
    private void PrivateInvoke(ref MessageData msgData, int type)
    {
      IMessage message1 = (IMessage) null;
      CallType callType = (CallType) type;
      IMessage message2 = (IMessage) null;
      int msgFlags = -1;
      RemotingProxy remotingProxy = (RemotingProxy) null;
      if (CallType.MethodCall == callType)
      {
        Message message3 = new Message();
        message3.InitFields(msgData);
        message1 = (IMessage) message3;
        msgFlags = message3.GetCallType();
      }
      else if (CallType.ConstructorCall == callType)
      {
        msgFlags = 0;
        remotingProxy = this as RemotingProxy;
        bool flag = false;
        ConstructorCallMessage constructorCallMessage1;
        if (!this.IsRemotingProxy())
        {
          constructorCallMessage1 = new ConstructorCallMessage((object[]) null, (object[]) null, (object[]) null, (RuntimeType) this.GetProxiedType());
        }
        else
        {
          constructorCallMessage1 = remotingProxy.ConstructorMessage;
          Identity identityObject = remotingProxy.IdentityObject;
          if (identityObject != null)
            flag = identityObject.IsWellKnown();
        }
        if (constructorCallMessage1 == null | flag)
        {
          ConstructorCallMessage constructorCallMessage2 = new ConstructorCallMessage((object[]) null, (object[]) null, (object[]) null, (RuntimeType) this.GetProxiedType());
          constructorCallMessage2.SetFrame(msgData);
          message1 = (IMessage) constructorCallMessage2;
          if (flag)
          {
            remotingProxy.ConstructorMessage = (ConstructorCallMessage) null;
            if (constructorCallMessage2.ArgCount != 0)
              throw new RemotingException(Environment.GetResourceString("Remoting_Activation_WellKnownCTOR"));
          }
          message2 = (IMessage) new ConstructorReturnMessage((MarshalByRefObject) this.GetTransparentProxy(), (object[]) null, 0, (LogicalCallContext) null, (IConstructionCallMessage) constructorCallMessage2);
        }
        else
        {
          constructorCallMessage1.SetFrame(msgData);
          message1 = (IMessage) constructorCallMessage1;
        }
      }
      ChannelServices.IncrementRemoteCalls();
      if (!this.IsRemotingProxy() && (msgFlags & 2) == 2)
        message2 = RealProxy.EndInvokeHelper(message1 as Message, true);
      if (message2 == null)
      {
        Thread currentThread = Thread.CurrentThread;
        LogicalCallContext logicalCallContext = currentThread.GetMutableExecutionContext().LogicalCallContext;
        this.SetCallContextInMessage(message1, msgFlags, logicalCallContext);
        logicalCallContext.PropagateOutgoingHeadersToMessage(message1);
        message2 = this.Invoke(message1);
        this.ReturnCallContextToThread(currentThread, message2, msgFlags, logicalCallContext);
        Thread.CurrentThread.GetMutableExecutionContext().LogicalCallContext.PropagateIncomingHeadersToCallContext(message2);
      }
      if (!this.IsRemotingProxy() && (msgFlags & 1) == 1)
      {
        Message m = message1 as Message;
        AsyncResult asyncResult = new AsyncResult(m);
        asyncResult.SyncProcessMessage(message2);
        message2 = (IMessage) new ReturnMessage((object) asyncResult, (object[]) null, 0, (LogicalCallContext) null, (IMethodCallMessage) m);
      }
      RealProxy.HandleReturnMessage(message1, message2);
      if (CallType.ConstructorCall != callType)
        return;
      IConstructionReturnMessage constructionReturnMessage = message2 as IConstructionReturnMessage;
      if (constructionReturnMessage == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_BadReturnTypeForActivation"));
      ConstructorReturnMessage constructorReturnMessage = constructionReturnMessage as ConstructorReturnMessage;
      MarshalByRefObject marshalByRefObject;
      if (constructorReturnMessage != null)
      {
        marshalByRefObject = (MarshalByRefObject) constructorReturnMessage.GetObject();
        if (marshalByRefObject == null)
          throw new RemotingException(Environment.GetResourceString("Remoting_Activation_NullReturnValue"));
      }
      else
      {
        marshalByRefObject = (MarshalByRefObject) RemotingServices.InternalUnmarshal((ObjRef) constructionReturnMessage.ReturnValue, this.GetTransparentProxy(), true);
        if (marshalByRefObject == null)
          throw new RemotingException(Environment.GetResourceString("Remoting_Activation_NullFromInternalUnmarshal"));
      }
      if (marshalByRefObject != (MarshalByRefObject) this.GetTransparentProxy())
        throw new RemotingException(Environment.GetResourceString("Remoting_Activation_InconsistentState"));
      if (!this.IsRemotingProxy())
        return;
      remotingProxy.ConstructorMessage = (ConstructorCallMessage) null;
    }

    private void SetCallContextInMessage(IMessage reqMsg, int msgFlags, LogicalCallContext cctx)
    {
      Message message = reqMsg as Message;
      if (msgFlags != 0)
        return;
      if (message != null)
        message.SetLogicalCallContext(cctx);
      else
        ((ConstructorCallMessage) reqMsg).SetLogicalCallContext(cctx);
    }

    [SecurityCritical]
    private void ReturnCallContextToThread(Thread currentThread, IMessage retMsg, int msgFlags, LogicalCallContext currCtx)
    {
      if (msgFlags != 0 || retMsg == null)
        return;
      IMethodReturnMessage methodReturnMessage = retMsg as IMethodReturnMessage;
      if (methodReturnMessage == null)
        return;
      LogicalCallContext logicalCallContext1 = methodReturnMessage.LogicalCallContext;
      if (logicalCallContext1 == null)
      {
        currentThread.GetMutableExecutionContext().LogicalCallContext = currCtx;
      }
      else
      {
        if (methodReturnMessage is StackBasedReturnMessage)
          return;
        ExecutionContext executionContext = currentThread.GetMutableExecutionContext();
        LogicalCallContext logicalCallContext2 = executionContext.LogicalCallContext;
        executionContext.LogicalCallContext = logicalCallContext1;
        if (logicalCallContext2 == logicalCallContext1)
          return;
        IPrincipal principal = logicalCallContext2.Principal;
        if (principal == null)
          return;
        logicalCallContext1.Principal = principal;
      }
    }

    [SecurityCritical]
    internal virtual void Wrap()
    {
      ServerIdentity identity = this._identity as ServerIdentity;
      if (identity == null || !(this is RemotingProxy))
        return;
      RealProxy.SetStubData(this, (object) identity.ServerContext.InternalContextID);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Proxies.RealProxy" /> со значениями по умолчанию.
    /// </summary>
    protected RealProxy()
    {
    }
  }
}

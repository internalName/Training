// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.Context
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>
  ///   Определяет среду для объектов, которые находятся внутри нее и для которого может быть реализована политика.
  /// </summary>
  [ComVisible(true)]
  public class Context
  {
    private static DynamicPropertyHolder _dphGlobal = new DynamicPropertyHolder();
    private static LocalDataStoreMgr _localDataStoreMgr = new LocalDataStoreMgr();
    private static int _ctxIDCounter = 0;
    internal const int CTX_DEFAULT_CONTEXT = 1;
    internal const int CTX_FROZEN = 2;
    internal const int CTX_THREADPOOL_AWARE = 4;
    private const int GROW_BY = 8;
    private const int STATICS_BUCKET_SIZE = 8;
    private IContextProperty[] _ctxProps;
    private DynamicPropertyHolder _dphCtx;
    private volatile LocalDataStoreHolder _localDataStore;
    private IMessageSink _serverContextChain;
    private IMessageSink _clientContextChain;
    private AppDomain _appDomain;
    private object[] _ctxStatics;
    private IntPtr _internalContext;
    private int _ctxID;
    private int _ctxFlags;
    private int _numCtxProps;
    private int _ctxStaticsCurrentBucket;
    private int _ctxStaticsFreeIndex;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.Contexts.Context" />.
    /// </summary>
    [SecurityCritical]
    public Context()
      : this(0)
    {
    }

    [SecurityCritical]
    private Context(int flags)
    {
      this._ctxFlags = flags;
      this._ctxID = (this._ctxFlags & 1) == 0 ? Interlocked.Increment(ref Context._ctxIDCounter) : 0;
      DomainSpecificRemotingData remotingData = Thread.GetDomain().RemotingData;
      if (remotingData != null)
      {
        IContextProperty[] contextProperties = remotingData.AppDomainContextProperties;
        if (contextProperties != null)
        {
          for (int index = 0; index < contextProperties.Length; ++index)
            this.SetProperty(contextProperties[index]);
        }
      }
      if ((this._ctxFlags & 1) != 0)
        this.Freeze();
      this.SetupInternalContext((this._ctxFlags & 1) == 1);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void SetupInternalContext(bool bDefault);

    /// <summary>
    ///   Очищает вспомогательные объекты для контекстов не по умолчанию.
    /// </summary>
    [SecuritySafeCritical]
    ~Context()
    {
      if (!(this._internalContext != IntPtr.Zero) || (this._ctxFlags & 1) != 0)
        return;
      this.CleanupInternalContext();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void CleanupInternalContext();

    /// <summary>
    ///   Возвращает идентификатор контекста для текущего контекста.
    /// </summary>
    /// <returns>Идентификатор контекста для текущего контекста.</returns>
    public virtual int ContextID
    {
      [SecurityCritical] get
      {
        return this._ctxID;
      }
    }

    internal virtual IntPtr InternalContextID
    {
      get
      {
        return this._internalContext;
      }
    }

    internal virtual AppDomain AppDomain
    {
      get
      {
        return this._appDomain;
      }
    }

    internal bool IsDefaultContext
    {
      get
      {
        return this._ctxID == 0;
      }
    }

    /// <summary>
    ///   Возвращает контекст по умолчанию для текущего домена приложения.
    /// </summary>
    /// <returns>
    ///   Контекст по умолчанию для <see cref="T:System.AppDomain" /> пространства имен.
    /// </returns>
    public static Context DefaultContext
    {
      [SecurityCritical] get
      {
        return Thread.GetDomain().GetDefaultContext();
      }
    }

    [SecurityCritical]
    internal static Context CreateDefaultContext()
    {
      return new Context(1);
    }

    /// <summary>
    ///   Возвращает контекстное свойство, указанные по имени.
    /// </summary>
    /// <param name="name">Имя свойства.</param>
    /// <returns>Заданное свойство контекста.</returns>
    [SecurityCritical]
    public virtual IContextProperty GetProperty(string name)
    {
      if (this._ctxProps == null || name == null)
        return (IContextProperty) null;
      IContextProperty contextProperty = (IContextProperty) null;
      for (int index = 0; index < this._numCtxProps; ++index)
      {
        if (this._ctxProps[index].Name.Equals(name))
        {
          contextProperty = this._ctxProps[index];
          break;
        }
      }
      return contextProperty;
    }

    /// <summary>Задает контекстное свойство по имени.</summary>
    /// <param name="prop">Действительное контекстное свойство.</param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Существует попытка добавить свойства в контекст по умолчанию.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Контекст является замороженным.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойства или имени свойства — <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public virtual void SetProperty(IContextProperty prop)
    {
      if (prop == null || prop.Name == null)
        throw new ArgumentNullException(prop == null ? nameof (prop) : "property name");
      if ((this._ctxFlags & 2) != 0)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_AddContextFrozen"));
      lock (this)
      {
        Context.CheckPropertyNameClash(prop.Name, this._ctxProps, this._numCtxProps);
        if (this._ctxProps == null || this._numCtxProps == this._ctxProps.Length)
          this._ctxProps = Context.GrowPropertiesArray(this._ctxProps);
        this._ctxProps[this._numCtxProps++] = prop;
      }
    }

    [SecurityCritical]
    internal virtual void InternalFreeze()
    {
      this._ctxFlags |= 2;
      for (int index = 0; index < this._numCtxProps; ++index)
        this._ctxProps[index].Freeze(this);
    }

    /// <summary>
    ///   Замораживает контекст, делая невозможным Добавление и удаление контекстных свойств из текущего контекста.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Контекст уже закреплен.
    /// </exception>
    [SecurityCritical]
    public virtual void Freeze()
    {
      lock (this)
      {
        if ((this._ctxFlags & 2) != 0)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ContextAlreadyFrozen"));
        this.InternalFreeze();
      }
    }

    internal virtual void SetThreadPoolAware()
    {
      this._ctxFlags |= 4;
    }

    internal virtual bool IsThreadPoolAware
    {
      get
      {
        return (this._ctxFlags & 4) == 4;
      }
    }

    /// <summary>Возвращает массив текущих контекстных свойств.</summary>
    /// <returns>
    ///   Массив текущих контекстных свойств; в противном случае — <see langword="null" /> Если контекст не имеют назначенного его свойств.
    /// </returns>
    public virtual IContextProperty[] ContextProperties
    {
      [SecurityCritical] get
      {
        if (this._ctxProps == null)
          return (IContextProperty[]) null;
        lock (this)
        {
          IContextProperty[] contextPropertyArray = new IContextProperty[this._numCtxProps];
          Array.Copy((Array) this._ctxProps, (Array) contextPropertyArray, this._numCtxProps);
          return contextPropertyArray;
        }
      }
    }

    [SecurityCritical]
    internal static void CheckPropertyNameClash(string name, IContextProperty[] props, int count)
    {
      for (int index = 0; index < count; ++index)
      {
        if (props[index].Name.Equals(name))
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_DuplicatePropertyName"));
      }
    }

    internal static IContextProperty[] GrowPropertiesArray(IContextProperty[] props)
    {
      IContextProperty[] contextPropertyArray = new IContextProperty[(props != null ? props.Length : 0) + 8];
      if (props != null)
        Array.Copy((Array) props, (Array) contextPropertyArray, props.Length);
      return contextPropertyArray;
    }

    [SecurityCritical]
    internal virtual IMessageSink GetServerContextChain()
    {
      if (this._serverContextChain == null)
      {
        IMessageSink nextSink = ServerContextTerminatorSink.MessageSink;
        int numCtxProps = this._numCtxProps;
        while (numCtxProps-- > 0)
        {
          IContributeServerContextSink ctxProp = (object) this._ctxProps[numCtxProps] as IContributeServerContextSink;
          if (ctxProp != null)
          {
            nextSink = ctxProp.GetServerContextSink(nextSink);
            if (nextSink == null)
              throw new RemotingException(Environment.GetResourceString("Remoting_Contexts_BadProperty"));
          }
        }
        lock (this)
        {
          if (this._serverContextChain == null)
            this._serverContextChain = nextSink;
        }
      }
      return this._serverContextChain;
    }

    [SecurityCritical]
    internal virtual IMessageSink GetClientContextChain()
    {
      if (this._clientContextChain == null)
      {
        IMessageSink nextSink = ClientContextTerminatorSink.MessageSink;
        for (int index = 0; index < this._numCtxProps; ++index)
        {
          IContributeClientContextSink ctxProp = (object) this._ctxProps[index] as IContributeClientContextSink;
          if (ctxProp != null)
          {
            nextSink = ctxProp.GetClientContextSink(nextSink);
            if (nextSink == null)
              throw new RemotingException(Environment.GetResourceString("Remoting_Contexts_BadProperty"));
          }
        }
        lock (this)
        {
          if (this._clientContextChain == null)
            this._clientContextChain = nextSink;
        }
      }
      return this._clientContextChain;
    }

    [SecurityCritical]
    internal virtual IMessageSink CreateServerObjectChain(MarshalByRefObject serverObj)
    {
      IMessageSink nextSink = (IMessageSink) new ServerObjectTerminatorSink(serverObj);
      int numCtxProps = this._numCtxProps;
      while (numCtxProps-- > 0)
      {
        IContributeObjectSink ctxProp = (object) this._ctxProps[numCtxProps] as IContributeObjectSink;
        if (ctxProp != null)
        {
          nextSink = ctxProp.GetObjectSink(serverObj, nextSink);
          if (nextSink == null)
            throw new RemotingException(Environment.GetResourceString("Remoting_Contexts_BadProperty"));
        }
      }
      return nextSink;
    }

    [SecurityCritical]
    internal virtual IMessageSink CreateEnvoyChain(MarshalByRefObject objectOrProxy)
    {
      IMessageSink nextSink = EnvoyTerminatorSink.MessageSink;
      int index = 0;
      MarshalByRefObject marshalByRefObject = objectOrProxy;
      for (; index < this._numCtxProps; ++index)
      {
        IContributeEnvoySink ctxProp = (object) this._ctxProps[index] as IContributeEnvoySink;
        if (ctxProp != null)
        {
          nextSink = ctxProp.GetEnvoySink(marshalByRefObject, nextSink);
          if (nextSink == null)
            throw new RemotingException(Environment.GetResourceString("Remoting_Contexts_BadProperty"));
        }
      }
      return nextSink;
    }

    [SecurityCritical]
    internal IMessage NotifyActivatorProperties(IMessage msg, bool bServerSide)
    {
      IMessage message = (IMessage) null;
      try
      {
        int numCtxProps = this._numCtxProps;
        while (numCtxProps-- != 0)
        {
          IContextPropertyActivator ctxProp = (object) this._ctxProps[numCtxProps] as IContextPropertyActivator;
          if (ctxProp != null)
          {
            IConstructionCallMessage msg1 = msg as IConstructionCallMessage;
            if (msg1 != null)
            {
              if (!bServerSide)
                ctxProp.CollectFromClientContext(msg1);
              else
                ctxProp.DeliverClientContextToServerContext(msg1);
            }
            else if (bServerSide)
              ctxProp.CollectFromServerContext((IConstructionReturnMessage) msg);
            else
              ctxProp.DeliverServerContextToClientContext((IConstructionReturnMessage) msg);
          }
        }
      }
      catch (Exception ex)
      {
        message = (IMessage) new ReturnMessage(ex, !(msg is IConstructionCallMessage) ? (IMethodCallMessage) new ErrorMessage() : (IMethodCallMessage) msg);
        if (msg != null)
          ((ReturnMessage) message).SetLogicalCallContext((LogicalCallContext) msg.Properties[(object) Message.CallContextKey]);
      }
      return message;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.String" /> класса представление текущего контекста.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.String" /> класса представление текущего контекста.
    /// </returns>
    public override string ToString()
    {
      return "ContextID: " + (object) this._ctxID;
    }

    /// <summary>Выполняет код в другом контексте.</summary>
    /// <param name="deleg">
    ///   Делегат, используемый для запроса обратного вызова.
    /// </param>
    [SecurityCritical]
    public void DoCallBack(CrossContextDelegate deleg)
    {
      if (deleg == null)
        throw new ArgumentNullException(nameof (deleg));
      if ((this._ctxFlags & 2) == 0)
        throw new RemotingException(Environment.GetResourceString("Remoting_Contexts_ContextNotFrozenForCallBack"));
      Context currentContext = Thread.CurrentContext;
      if (currentContext == this)
      {
        deleg();
      }
      else
      {
        currentContext.DoCallBackGeneric(this.InternalContextID, deleg);
        GC.KeepAlive((object) this);
      }
    }

    [SecurityCritical]
    internal static void DoCallBackFromEE(IntPtr targetCtxID, IntPtr privateData, int targetDomainID)
    {
      if (targetDomainID == 0)
      {
        CrossContextDelegate deleg = new CrossContextDelegate(new CallBackHelper(privateData, true, targetDomainID).Func);
        Thread.CurrentContext.DoCallBackGeneric(targetCtxID, deleg);
      }
      else
      {
        TransitionCall transitionCall = new TransitionCall(targetCtxID, privateData, targetDomainID);
        Message.PropagateCallContextFromThreadToMessage((IMessage) transitionCall);
        IMessage msg = Thread.CurrentContext.GetClientContextChain().SyncProcessMessage((IMessage) transitionCall);
        Message.PropagateCallContextFromMessageToThread(msg);
        IMethodReturnMessage methodReturnMessage = msg as IMethodReturnMessage;
        if (methodReturnMessage != null && methodReturnMessage.Exception != null)
          throw methodReturnMessage.Exception;
      }
    }

    [SecurityCritical]
    internal void DoCallBackGeneric(IntPtr targetCtxID, CrossContextDelegate deleg)
    {
      TransitionCall transitionCall = new TransitionCall(targetCtxID, deleg);
      Message.PropagateCallContextFromThreadToMessage((IMessage) transitionCall);
      IMessage msg = this.GetClientContextChain().SyncProcessMessage((IMessage) transitionCall);
      if (msg != null)
        Message.PropagateCallContextFromMessageToThread(msg);
      IMethodReturnMessage methodReturnMessage = msg as IMethodReturnMessage;
      if (methodReturnMessage != null && methodReturnMessage.Exception != null)
        throw methodReturnMessage.Exception;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ExecuteCallBackInEE(IntPtr privateData);

    private LocalDataStore MyLocalStore
    {
      get
      {
        if (this._localDataStore == null)
        {
          lock (Context._localDataStoreMgr)
          {
            if (this._localDataStore == null)
              this._localDataStore = Context._localDataStoreMgr.CreateLocalDataStore();
          }
        }
        return this._localDataStore.Store;
      }
    }

    /// <summary>Выделяет неименованную область данных.</summary>
    /// <returns>Локальная область данных.</returns>
    [SecurityCritical]
    public static LocalDataStoreSlot AllocateDataSlot()
    {
      return Context._localDataStoreMgr.AllocateDataSlot();
    }

    /// <summary>Выделяет именованную область данных.</summary>
    /// <param name="name">Требуемое имя области данных.</param>
    /// <returns>Объект области локальных данных.</returns>
    [SecurityCritical]
    public static LocalDataStoreSlot AllocateNamedDataSlot(string name)
    {
      return Context._localDataStoreMgr.AllocateNamedDataSlot(name);
    }

    /// <summary>Ищет именованную область данных.</summary>
    /// <param name="name">Имя области данных.</param>
    /// <returns>Возвращает локальную область данных.</returns>
    [SecurityCritical]
    public static LocalDataStoreSlot GetNamedDataSlot(string name)
    {
      return Context._localDataStoreMgr.GetNamedDataSlot(name);
    }

    /// <summary>
    ///   Освобождает именованную область данных во всех контекстах.
    /// </summary>
    /// <param name="name">Имя освобождаемой области данных.</param>
    [SecurityCritical]
    public static void FreeNamedDataSlot(string name)
    {
      Context._localDataStoreMgr.FreeNamedDataSlot(name);
    }

    /// <summary>
    ///   Задает данные в указанной области в текущем контексте.
    /// </summary>
    /// <param name="slot">
    ///   Область данных, где данные должны быть добавлены.
    /// </param>
    /// <param name="data">Данные, которые требуется добавить.</param>
    [SecurityCritical]
    public static void SetData(LocalDataStoreSlot slot, object data)
    {
      Thread.CurrentContext.MyLocalStore.SetData(slot, data);
    }

    /// <summary>
    ///   Извлекает значение из заданной области текущего контекста.
    /// </summary>
    /// <param name="slot">Область данных, содержащий данные.</param>
    /// <returns>
    ///   Возвращает данные, связанные с <paramref name="slot" />.
    /// </returns>
    [SecurityCritical]
    public static object GetData(LocalDataStoreSlot slot)
    {
      return Thread.CurrentContext.MyLocalStore.GetData(slot);
    }

    private int ReserveSlot()
    {
      if (this._ctxStatics == null)
      {
        this._ctxStatics = new object[8];
        this._ctxStatics[0] = (object) null;
        this._ctxStaticsFreeIndex = 1;
        this._ctxStaticsCurrentBucket = 0;
      }
      if (this._ctxStaticsFreeIndex == 8)
      {
        object[] objArray = new object[8];
        object[] ctxStatics = this._ctxStatics;
        while (ctxStatics[0] != null)
          ctxStatics = (object[]) ctxStatics[0];
        ctxStatics[0] = (object) objArray;
        this._ctxStaticsFreeIndex = 1;
        ++this._ctxStaticsCurrentBucket;
      }
      return this._ctxStaticsFreeIndex++ | this._ctxStaticsCurrentBucket << 16;
    }

    /// <summary>
    ///   Регистрирует динамическое свойство, реализующее интерфейс <see cref="T:System.Runtime.Remoting.Contexts.IDynamicProperty" /> интерфейс со службой удаленного доступа.
    /// </summary>
    /// <param name="prop">Регистрируемое динамическое свойство.</param>
    /// <param name="obj">
    ///   Объект или прокси, для которого <paramref name="property" /> зарегистрирован.
    /// </param>
    /// <param name="ctx">
    ///   Контекст, для которого <paramref name="property" /> зарегистрирован.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если свойство было успешно зарегистрировано; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Либо <paramref name="prop" /> или ее имя — <see langword="null" />, или он не является динамическим (он не реализует <see cref="T:System.Runtime.Remoting.Contexts.IDynamicProperty" />).
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Указаны как объект так и контекст (оба <paramref name="obj" /> и <paramref name="ctx" /> не <see langword="null" />).
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.Infrastructure)]
    public static bool RegisterDynamicProperty(IDynamicProperty prop, ContextBoundObject obj, Context ctx)
    {
      if (prop == null || prop.Name == null || !(prop is IContributeDynamicSink))
        throw new ArgumentNullException(nameof (prop));
      if (obj != null && ctx != null)
        throw new ArgumentException(Environment.GetResourceString("Argument_NonNullObjAndCtx"));
      return obj == null ? Context.AddDynamicProperty(ctx, prop) : IdentityHolder.AddDynamicProperty((MarshalByRefObject) obj, prop);
    }

    /// <summary>
    ///   Отменяет регистрацию динамическое свойство, реализующее интерфейс <see cref="T:System.Runtime.Remoting.Contexts.IDynamicProperty" /> интерфейс.
    /// </summary>
    /// <param name="name">
    ///   Имя динамического свойства для отмены регистрации.
    /// </param>
    /// <param name="obj">
    ///   Объект или прокси, для которого <paramref name="property" /> зарегистрирован.
    /// </param>
    /// <param name="ctx">
    ///   Контекст, для которого <paramref name="property" /> зарегистрирован.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если объект был успешно удалена; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Указаны как объект так и контекст (оба <paramref name="obj" /> и <paramref name="ctx" /> не <see langword="null" />).
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.Infrastructure)]
    public static bool UnregisterDynamicProperty(string name, ContextBoundObject obj, Context ctx)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (obj != null && ctx != null)
        throw new ArgumentException(Environment.GetResourceString("Argument_NonNullObjAndCtx"));
      return obj == null ? Context.RemoveDynamicProperty(ctx, name) : IdentityHolder.RemoveDynamicProperty((MarshalByRefObject) obj, name);
    }

    [SecurityCritical]
    internal static bool AddDynamicProperty(Context ctx, IDynamicProperty prop)
    {
      if (ctx != null)
        return ctx.AddPerContextDynamicProperty(prop);
      return Context.AddGlobalDynamicProperty(prop);
    }

    [SecurityCritical]
    private bool AddPerContextDynamicProperty(IDynamicProperty prop)
    {
      if (this._dphCtx == null)
      {
        DynamicPropertyHolder dynamicPropertyHolder = new DynamicPropertyHolder();
        lock (this)
        {
          if (this._dphCtx == null)
            this._dphCtx = dynamicPropertyHolder;
        }
      }
      return this._dphCtx.AddDynamicProperty(prop);
    }

    [SecurityCritical]
    private static bool AddGlobalDynamicProperty(IDynamicProperty prop)
    {
      return Context._dphGlobal.AddDynamicProperty(prop);
    }

    [SecurityCritical]
    internal static bool RemoveDynamicProperty(Context ctx, string name)
    {
      if (ctx != null)
        return ctx.RemovePerContextDynamicProperty(name);
      return Context.RemoveGlobalDynamicProperty(name);
    }

    [SecurityCritical]
    private bool RemovePerContextDynamicProperty(string name)
    {
      if (this._dphCtx == null)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Contexts_NoProperty"), (object) name));
      return this._dphCtx.RemoveDynamicProperty(name);
    }

    [SecurityCritical]
    private static bool RemoveGlobalDynamicProperty(string name)
    {
      return Context._dphGlobal.RemoveDynamicProperty(name);
    }

    internal virtual IDynamicProperty[] PerContextDynamicProperties
    {
      get
      {
        if (this._dphCtx == null)
          return (IDynamicProperty[]) null;
        return this._dphCtx.DynamicProperties;
      }
    }

    internal static ArrayWithSize GlobalDynamicSinks
    {
      [SecurityCritical] get
      {
        return Context._dphGlobal.DynamicSinks;
      }
    }

    internal virtual ArrayWithSize DynamicSinks
    {
      [SecurityCritical] get
      {
        if (this._dphCtx == null)
          return (ArrayWithSize) null;
        return this._dphCtx.DynamicSinks;
      }
    }

    [SecurityCritical]
    internal virtual bool NotifyDynamicSinks(IMessage msg, bool bCliSide, bool bStart, bool bAsync, bool bNotifyGlobals)
    {
      bool flag = false;
      if (bNotifyGlobals && Context._dphGlobal.DynamicProperties != null)
      {
        ArrayWithSize globalDynamicSinks = Context.GlobalDynamicSinks;
        if (globalDynamicSinks != null)
        {
          DynamicPropertyHolder.NotifyDynamicSinks(msg, globalDynamicSinks, bCliSide, bStart, bAsync);
          flag = true;
        }
      }
      ArrayWithSize dynamicSinks = this.DynamicSinks;
      if (dynamicSinks != null)
      {
        DynamicPropertyHolder.NotifyDynamicSinks(msg, dynamicSinks, bCliSide, bStart, bAsync);
        flag = true;
      }
      return flag;
    }
  }
}

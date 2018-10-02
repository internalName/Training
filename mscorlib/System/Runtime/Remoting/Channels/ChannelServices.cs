// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.ChannelServices
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>
  ///   Предоставляет статические методы для регистрации канала удаленного взаимодействия, разрешения и поиска URL-адреса.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  public sealed class ChannelServices
  {
    private static volatile object[] s_currentChannelData = (object[]) null;
    private static object s_channelLock = new object();
    private static volatile RegisteredChannelList s_registeredChannels = new RegisteredChannelList();
    [SecurityCritical]
    private static volatile unsafe Perf_Contexts* perf_Contexts = ChannelServices.GetPrivateContextsPerfCounters();
    private static bool unloadHandlerRegistered = false;
    private static volatile IMessageSink xCtxChannel;

    [SecuritySafeCritical]
    static unsafe ChannelServices()
    {
    }

    internal static object[] CurrentChannelData
    {
      [SecurityCritical] get
      {
        if (ChannelServices.s_currentChannelData == null)
          ChannelServices.RefreshChannelData();
        return ChannelServices.s_currentChannelData;
      }
    }

    private ChannelServices()
    {
    }

    private static long remoteCalls
    {
      get
      {
        return Thread.GetDomain().RemotingData.ChannelServicesData.remoteCalls;
      }
      set
      {
        Thread.GetDomain().RemotingData.ChannelServicesData.remoteCalls = value;
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe Perf_Contexts* GetPrivateContextsPerfCounters();

    /// <summary>Регистрирует канал со службами канала.</summary>
    /// <param name="chnl">Канал для регистрации.</param>
    /// <param name="ensureSecurity">
    ///   <see langword="true" /> гарантирует, что защита включена; в противном случае <see langword="false" />.
    ///    Значение <see langword="false" /> не влияет на параметр безопасности для канала TCP или IPC.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="chnl" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   Канал уже зарегистрирован.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Не поддерживается в Windows 98 для <see cref="T:System.Runtime.Remoting.Channels.Tcp.TcpServerChannel" /> и на всех платформах для <see cref="T:System.Runtime.Remoting.Channels.Http.HttpServerChannel" />.
    ///    Разместите службу с помощью Internet Information Services (IIS), если требуется безопасный канал HTTP.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void RegisterChannel(IChannel chnl, bool ensureSecurity)
    {
      ChannelServices.RegisterChannelInternal(chnl, ensureSecurity);
    }

    /// <summary>
    ///   Регистрирует канал со службами канала.
    ///   <see cref="M:System.Runtime.Remoting.Channels.ChannelServices.RegisterChannel(System.Runtime.Remoting.Channels.IChannel)" /> устарел.
    ///    Взамен рекомендуется использовать <see cref="M:System.Runtime.Remoting.Channels.ChannelServices.RegisterChannel(System.Runtime.Remoting.Channels.IChannel,System.Boolean)" />.
    /// </summary>
    /// <param name="chnl">Канал для регистрации.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="chnl" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   Канал уже зарегистрирован.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("Use System.Runtime.Remoting.ChannelServices.RegisterChannel(IChannel chnl, bool ensureSecurity) instead.", false)]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void RegisterChannel(IChannel chnl)
    {
      ChannelServices.RegisterChannelInternal(chnl, false);
    }

    [SecurityCritical]
    internal static unsafe void RegisterChannelInternal(IChannel chnl, bool ensureSecurity)
    {
      if (chnl == null)
        throw new ArgumentNullException(nameof (chnl));
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        Monitor.Enter(ChannelServices.s_channelLock, ref lockTaken);
        string channelName = chnl.ChannelName;
        RegisteredChannelList registeredChannels1 = ChannelServices.s_registeredChannels;
        if (channelName == null || channelName.Length == 0 || -1 == registeredChannels1.FindChannelIndex(chnl.ChannelName))
        {
          if (ensureSecurity)
          {
            ISecurableChannel securableChannel = chnl as ISecurableChannel;
            if (securableChannel != null)
              securableChannel.IsSecured = ensureSecurity;
            else
              throw new RemotingException(Environment.GetResourceString("Remoting_Channel_CannotBeSecured", (object) (chnl.ChannelName ?? chnl.ToString())));
          }
          RegisteredChannel[] registeredChannels2 = registeredChannels1.RegisteredChannels;
          RegisteredChannel[] channels = registeredChannels2 != null ? new RegisteredChannel[registeredChannels2.Length + 1] : new RegisteredChannel[1];
          if (!ChannelServices.unloadHandlerRegistered && !(chnl is CrossAppDomainChannel))
          {
            AppDomain.CurrentDomain.DomainUnload += new EventHandler(ChannelServices.UnloadHandler);
            ChannelServices.unloadHandlerRegistered = true;
          }
          int channelPriority = chnl.ChannelPriority;
          int index;
          for (index = 0; index < registeredChannels2.Length; ++index)
          {
            RegisteredChannel registeredChannel = registeredChannels2[index];
            if (channelPriority > registeredChannel.Channel.ChannelPriority)
            {
              channels[index] = new RegisteredChannel(chnl);
              break;
            }
            channels[index] = registeredChannel;
          }
          if (index == registeredChannels2.Length)
          {
            channels[registeredChannels2.Length] = new RegisteredChannel(chnl);
          }
          else
          {
            for (; index < registeredChannels2.Length; ++index)
              channels[index + 1] = registeredChannels2[index];
          }
          if ((IntPtr) ChannelServices.perf_Contexts != IntPtr.Zero)
            ++ChannelServices.perf_Contexts->cChannels;
          ChannelServices.s_registeredChannels = new RegisteredChannelList(channels);
          ChannelServices.RefreshChannelData();
        }
        else
          throw new RemotingException(Environment.GetResourceString("Remoting_ChannelNameAlreadyRegistered", (object) chnl.ChannelName));
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit(ChannelServices.s_channelLock);
      }
    }

    /// <summary>
    ///   Отменяет регистрацию указанного канала в списке зарегистрированных каналов.
    /// </summary>
    /// <param name="chnl">Канал, для отмены регистрации.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="chnl" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Канал не зарегистрирован.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static unsafe void UnregisterChannel(IChannel chnl)
    {
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        Monitor.Enter(ChannelServices.s_channelLock, ref lockTaken);
        if (chnl != null)
        {
          RegisteredChannelList registeredChannels1 = ChannelServices.s_registeredChannels;
          int channelIndex = registeredChannels1.FindChannelIndex(chnl);
          if (-1 == channelIndex)
            throw new RemotingException(Environment.GetResourceString("Remoting_ChannelNotRegistered", (object) chnl.ChannelName));
          RegisteredChannel[] registeredChannels2 = registeredChannels1.RegisteredChannels;
          RegisteredChannel[] channels = new RegisteredChannel[registeredChannels2.Length - 1];
          (chnl as IChannelReceiver)?.StopListening((object) null);
          int index1 = 0;
          int index2 = 0;
          while (index2 < registeredChannels2.Length)
          {
            if (index2 == channelIndex)
            {
              ++index2;
            }
            else
            {
              channels[index1] = registeredChannels2[index2];
              ++index1;
              ++index2;
            }
          }
          if ((IntPtr) ChannelServices.perf_Contexts != IntPtr.Zero)
            --ChannelServices.perf_Contexts->cChannels;
          ChannelServices.s_registeredChannels = new RegisteredChannelList(channels);
        }
        ChannelServices.RefreshChannelData();
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit(ChannelServices.s_channelLock);
      }
    }

    /// <summary>
    ///   Возвращает список зарегистрированных в настоящее время каналов.
    /// </summary>
    /// <returns>
    ///   Массив всех зарегистрированных в настоящее время каналов.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    public static IChannel[] RegisteredChannels
    {
      [SecurityCritical] get
      {
        RegisteredChannelList registeredChannels = ChannelServices.s_registeredChannels;
        int count = registeredChannels.Count;
        if (count == 0)
          return new IChannel[0];
        int length = count - 1;
        int num = 0;
        IChannel[] channelArray = new IChannel[length];
        for (int index = 0; index < count; ++index)
        {
          IChannel channel = registeredChannels.GetChannel(index);
          if (!(channel is CrossAppDomainChannel))
            channelArray[num++] = channel;
        }
        return channelArray;
      }
    }

    [SecurityCritical]
    internal static IMessageSink CreateMessageSink(string url, object data, out string objectURI)
    {
      IMessageSink messageSink = (IMessageSink) null;
      objectURI = (string) null;
      RegisteredChannelList registeredChannels = ChannelServices.s_registeredChannels;
      int count = registeredChannels.Count;
      for (int index = 0; index < count; ++index)
      {
        if (registeredChannels.IsSender(index))
        {
          messageSink = ((IChannelSender) registeredChannels.GetChannel(index)).CreateMessageSink(url, data, out objectURI);
          if (messageSink != null)
            break;
        }
      }
      if (objectURI == null)
        objectURI = url;
      return messageSink;
    }

    [SecurityCritical]
    internal static IMessageSink CreateMessageSink(object data)
    {
      string objectURI;
      return ChannelServices.CreateMessageSink((string) null, data, out objectURI);
    }

    /// <summary>
    ///   Возвращает зарегистрированный канал с указанным именем.
    /// </summary>
    /// <param name="name">Имя канала.</param>
    /// <returns>
    ///   Интерфейс к зарегистрированному каналу или <see langword="null" /> Если канал не зарегистрирован.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static IChannel GetChannel(string name)
    {
      RegisteredChannelList registeredChannels = ChannelServices.s_registeredChannels;
      int channelIndex = registeredChannels.FindChannelIndex(name);
      if (0 > channelIndex)
        return (IChannel) null;
      IChannel channel = registeredChannels.GetChannel(channelIndex);
      if (channel is CrossAppDomainChannel || channel is CrossContextChannel)
        return (IChannel) null;
      return channel;
    }

    /// <summary>
    ///   Возвращает массив всех URL-адресов, которые могут использоваться для доступа к указанному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Для получения массива URL-адрес для объекта.
    /// </param>
    /// <returns>
    ///   Массив строк, содержащий URL-адреса, которые могут использоваться для удаленного определения объекта, или <see langword="null" /> Если ни один объект не найден.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static string[] GetUrlsForObject(MarshalByRefObject obj)
    {
      if (obj == null)
        return (string[]) null;
      RegisteredChannelList registeredChannels = ChannelServices.s_registeredChannels;
      int count = registeredChannels.Count;
      Hashtable hashtable = new Hashtable();
      bool fServer;
      Identity identity = MarshalByRefObject.GetIdentity(obj, out fServer);
      if (identity != null)
      {
        string objUri = identity.ObjURI;
        if (objUri != null)
        {
          for (int index1 = 0; index1 < count; ++index1)
          {
            if (registeredChannels.IsReceiver(index1))
            {
              try
              {
                string[] urlsForUri = ((IChannelReceiver) registeredChannels.GetChannel(index1)).GetUrlsForUri(objUri);
                for (int index2 = 0; index2 < urlsForUri.Length; ++index2)
                  hashtable.Add((object) urlsForUri[index2], (object) urlsForUri[index2]);
              }
              catch (NotSupportedException ex)
              {
              }
            }
          }
        }
      }
      ICollection keys = hashtable.Keys;
      string[] strArray = new string[keys.Count];
      int num = 0;
      foreach (string str in (IEnumerable) keys)
        strArray[num++] = str;
      return strArray;
    }

    [SecurityCritical]
    internal static IMessageSink GetChannelSinkForProxy(object obj)
    {
      IMessageSink messageSink = (IMessageSink) null;
      if (RemotingServices.IsTransparentProxy(obj))
      {
        RemotingProxy realProxy = RemotingServices.GetRealProxy(obj) as RemotingProxy;
        if (realProxy != null)
          messageSink = realProxy.IdentityObject.ChannelSink;
      }
      return messageSink;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Collections.IDictionary" /> свойств для заданного прокси.
    /// </summary>
    /// <param name="obj">
    ///   Прокси-сервер, свойства которого необходимо извлечь.
    /// </param>
    /// <returns>
    ///   Интерфейс к словарю свойств или <see langword="null" /> Если свойства не найдены.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере у одного из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static IDictionary GetChannelSinkProperties(object obj)
    {
      IMessageSink channelSinkForProxy = ChannelServices.GetChannelSinkForProxy(obj);
      IClientChannelSink clientChannelSink = channelSinkForProxy as IClientChannelSink;
      if (clientChannelSink == null)
        return channelSinkForProxy as IDictionary ?? (IDictionary) null;
      ArrayList arrayList = new ArrayList();
      do
      {
        IDictionary properties = clientChannelSink.Properties;
        if (properties != null)
          arrayList.Add((object) properties);
        clientChannelSink = clientChannelSink.NextChannelSink;
      }
      while (clientChannelSink != null);
      return (IDictionary) new AggregateDictionary((ICollection) arrayList);
    }

    internal static IMessageSink GetCrossContextChannelSink()
    {
      if (ChannelServices.xCtxChannel == null)
        ChannelServices.xCtxChannel = CrossContextChannel.MessageSink;
      return ChannelServices.xCtxChannel;
    }

    [SecurityCritical]
    internal static unsafe void IncrementRemoteCalls(long cCalls)
    {
      ChannelServices.remoteCalls += cCalls;
      if ((IntPtr) ChannelServices.perf_Contexts == IntPtr.Zero)
        return;
      ChannelServices.perf_Contexts->cRemoteCalls += (int) cCalls;
    }

    [SecurityCritical]
    internal static void IncrementRemoteCalls()
    {
      ChannelServices.IncrementRemoteCalls(1L);
    }

    [SecurityCritical]
    internal static void RefreshChannelData()
    {
      bool lockTaken = false;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        Monitor.Enter(ChannelServices.s_channelLock, ref lockTaken);
        ChannelServices.s_currentChannelData = ChannelServices.CollectChannelDataFromChannels();
      }
      finally
      {
        if (lockTaken)
          Monitor.Exit(ChannelServices.s_channelLock);
      }
    }

    [SecurityCritical]
    private static object[] CollectChannelDataFromChannels()
    {
      RemotingServices.RegisterWellKnownChannels();
      RegisteredChannelList registeredChannels = ChannelServices.s_registeredChannels;
      int count = registeredChannels.Count;
      int receiverCount = registeredChannels.ReceiverCount;
      object[] objArray1 = new object[receiverCount];
      int length = 0;
      int index1 = 0;
      int index2 = 0;
      for (; index1 < count; ++index1)
      {
        IChannel channel = registeredChannels.GetChannel(index1);
        if (channel == null)
          throw new RemotingException(Environment.GetResourceString("Remoting_ChannelNotRegistered", (object) ""));
        if (registeredChannels.IsReceiver(index1))
        {
          object channelData = ((IChannelReceiver) channel).ChannelData;
          objArray1[index2] = channelData;
          if (channelData != null)
            ++length;
          ++index2;
        }
      }
      if (length != receiverCount)
      {
        object[] objArray2 = new object[length];
        int num = 0;
        for (int index3 = 0; index3 < receiverCount; ++index3)
        {
          object obj = objArray1[index3];
          if (obj != null)
            objArray2[num++] = obj;
        }
        objArray1 = objArray2;
      }
      return objArray1;
    }

    private static bool IsMethodReallyPublic(MethodInfo mi)
    {
      if (!mi.IsPublic || mi.IsStatic)
        return false;
      if (!mi.IsGenericMethod)
        return true;
      foreach (Type genericArgument in mi.GetGenericArguments())
      {
        if (!genericArgument.IsVisible)
          return false;
      }
      return true;
    }

    /// <summary>Отправляет входящие удаленные вызовы.</summary>
    /// <param name="sinkStack">
    ///   Стек каналов сервера приемников, что сообщение уже прошло.
    /// </param>
    /// <param name="msg">Оправляемое сообщение.</param>
    /// <param name="replyMsg">
    ///   При возвращении данного метода содержит <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> содержащий ответ от сервера сообщение, которое содержится в <paramref name="msg" /> параметр.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Runtime.Remoting.Channels.ServerProcessing" /> который предоставляет состояние обработки сообщений сервером.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="msg" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static ServerProcessing DispatchMessage(IServerChannelSinkStack sinkStack, IMessage msg, out IMessage replyMsg)
    {
      ServerProcessing serverProcessing = ServerProcessing.Complete;
      replyMsg = (IMessage) null;
      try
      {
        if (msg == null)
          throw new ArgumentNullException(nameof (msg));
        ChannelServices.IncrementRemoteCalls();
        ServerIdentity wellKnownObject = ChannelServices.CheckDisconnectedOrCreateWellKnownObject(msg);
        if (wellKnownObject.ServerType == typeof (AppDomain))
          throw new RemotingException(Environment.GetResourceString("Remoting_AppDomainsCantBeCalledRemotely"));
        IMethodCallMessage methodCallMessage = msg as IMethodCallMessage;
        if (methodCallMessage == null)
        {
          if (!typeof (IMessageSink).IsAssignableFrom(wellKnownObject.ServerType))
            throw new RemotingException(Environment.GetResourceString("Remoting_AppDomainsCantBeCalledRemotely"));
          serverProcessing = ServerProcessing.Complete;
          replyMsg = ChannelServices.GetCrossContextChannelSink().SyncProcessMessage(msg);
        }
        else
        {
          MethodInfo methodBase = (MethodInfo) methodCallMessage.MethodBase;
          if (!ChannelServices.IsMethodReallyPublic(methodBase) && !RemotingServices.IsMethodAllowedRemotely((MethodBase) methodBase))
            throw new RemotingException(Environment.GetResourceString("Remoting_NonPublicOrStaticCantBeCalledRemotely"));
          InternalRemotingServices.GetReflectionCachedData((MethodBase) methodBase);
          if (RemotingServices.IsOneWay((MethodBase) methodBase))
          {
            serverProcessing = ServerProcessing.OneWay;
            ChannelServices.GetCrossContextChannelSink().AsyncProcessMessage(msg, (IMessageSink) null);
          }
          else
          {
            serverProcessing = ServerProcessing.Complete;
            if (!wellKnownObject.ServerType.IsContextful)
            {
              object[] args = new object[2]
              {
                (object) msg,
                (object) wellKnownObject.ServerContext
              };
              replyMsg = (IMessage) CrossContextChannel.SyncProcessMessageCallback(args);
            }
            else
              replyMsg = ChannelServices.GetCrossContextChannelSink().SyncProcessMessage(msg);
          }
        }
      }
      catch (Exception ex1)
      {
        if (serverProcessing != ServerProcessing.OneWay)
        {
          try
          {
            IMethodCallMessage mcm = msg != null ? (IMethodCallMessage) msg : (IMethodCallMessage) new ErrorMessage();
            replyMsg = (IMessage) new ReturnMessage(ex1, mcm);
            if (msg != null)
              ((ReturnMessage) replyMsg).SetLogicalCallContext((LogicalCallContext) msg.Properties[(object) Message.CallContextKey]);
          }
          catch (Exception ex2)
          {
          }
        }
      }
      return serverProcessing;
    }

    /// <summary>
    ///   Синхронно отправляет входящего сообщения в цепи стороне сервера, на основе URI на внедренном в сообщение.
    /// </summary>
    /// <param name="msg">Оправляемое сообщение.</param>
    /// <returns>
    ///   Ответное сообщение возвращается вызовом в серверную цепочку.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="msg" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static IMessage SyncDispatchMessage(IMessage msg)
    {
      IMessage message = (IMessage) null;
      bool flag = false;
      try
      {
        if (msg == null)
          throw new ArgumentNullException(nameof (msg));
        ChannelServices.IncrementRemoteCalls();
        if (!(msg is TransitionCall))
        {
          ChannelServices.CheckDisconnectedOrCreateWellKnownObject(msg);
          flag = RemotingServices.IsOneWay(((IMethodMessage) msg).MethodBase);
        }
        IMessageSink contextChannelSink = ChannelServices.GetCrossContextChannelSink();
        if (!flag)
          message = contextChannelSink.SyncProcessMessage(msg);
        else
          contextChannelSink.AsyncProcessMessage(msg, (IMessageSink) null);
      }
      catch (Exception ex1)
      {
        if (!flag)
        {
          try
          {
            IMethodCallMessage mcm = msg != null ? (IMethodCallMessage) msg : (IMethodCallMessage) new ErrorMessage();
            message = (IMessage) new ReturnMessage(ex1, mcm);
            if (msg != null)
              ((ReturnMessage) message).SetLogicalCallContext(mcm.LogicalCallContext);
          }
          catch (Exception ex2)
          {
          }
        }
      }
      return message;
    }

    /// <summary>
    ///   Асинхронно отправляет указанное сообщение в цепи стороне сервера, на основе URI на внедренном в сообщение.
    /// </summary>
    /// <param name="msg">Оправляемое сообщение.</param>
    /// <param name="replySink">
    ///   Приемник, который обрабатывает возвращаемое сообщение, если он не <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Runtime.Remoting.Messaging.IMessageCtrl" /> объект, используемый для контроля сообщения, отправляемого асинхронно.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="msg" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static IMessageCtrl AsyncDispatchMessage(IMessage msg, IMessageSink replySink)
    {
      IMessageCtrl messageCtrl = (IMessageCtrl) null;
      try
      {
        if (msg == null)
          throw new ArgumentNullException(nameof (msg));
        ChannelServices.IncrementRemoteCalls();
        if (!(msg is TransitionCall))
          ChannelServices.CheckDisconnectedOrCreateWellKnownObject(msg);
        messageCtrl = ChannelServices.GetCrossContextChannelSink().AsyncProcessMessage(msg, replySink);
      }
      catch (Exception ex1)
      {
        if (replySink != null)
        {
          try
          {
            IMethodCallMessage methodCallMessage = (IMethodCallMessage) msg;
            ReturnMessage returnMessage = new ReturnMessage(ex1, (IMethodCallMessage) msg);
            if (msg != null)
              returnMessage.SetLogicalCallContext(methodCallMessage.LogicalCallContext);
            replySink.SyncProcessMessage((IMessage) returnMessage);
          }
          catch (Exception ex2)
          {
          }
        }
      }
      return messageCtrl;
    }

    /// <summary>
    ///   Создает цепочку приемников канала для указанного канала.
    /// </summary>
    /// <param name="provider">
    ///   Первый поставщик в цепи поставщиков приемников, которые создают цепь приемников канала.
    /// </param>
    /// <param name="channel">
    ///   <see cref="T:System.Runtime.Remoting.Channels.IChannelReceiver" /> Для которого необходимо создать цепочку приемников канала.
    /// </param>
    /// <returns>
    ///   Новая цепочка приемников канала для указанного канала.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static IServerChannelSink CreateServerChannelSinkChain(IServerChannelSinkProvider provider, IChannelReceiver channel)
    {
      if (provider == null)
        return (IServerChannelSink) new DispatchChannelSink();
      IServerChannelSinkProvider channelSinkProvider = provider;
      while (channelSinkProvider.Next != null)
        channelSinkProvider = channelSinkProvider.Next;
      channelSinkProvider.Next = (IServerChannelSinkProvider) new DispatchChannelSinkProvider();
      IServerChannelSink sink = provider.CreateSink(channel);
      channelSinkProvider.Next = (IServerChannelSinkProvider) null;
      return sink;
    }

    [SecurityCritical]
    internal static ServerIdentity CheckDisconnectedOrCreateWellKnownObject(IMessage msg)
    {
      ServerIdentity serverIdentity = InternalSink.GetServerIdentity(msg);
      if (serverIdentity == null || serverIdentity.IsRemoteDisconnected())
      {
        string uri = InternalSink.GetURI(msg);
        if (uri != null)
        {
          ServerIdentity wellKnownObject = RemotingConfigHandler.CreateWellKnownObject(uri);
          if (wellKnownObject != null)
            serverIdentity = wellKnownObject;
        }
      }
      if (serverIdentity == null || serverIdentity.IsRemoteDisconnected())
        throw new RemotingException(Environment.GetResourceString("Remoting_Disconnected", (object) InternalSink.GetURI(msg)));
      return serverIdentity;
    }

    [SecurityCritical]
    internal static void UnloadHandler(object sender, EventArgs e)
    {
      ChannelServices.StopListeningOnAllChannels();
    }

    [SecurityCritical]
    private static void StopListeningOnAllChannels()
    {
      try
      {
        RegisteredChannelList registeredChannels = ChannelServices.s_registeredChannels;
        int count = registeredChannels.Count;
        for (int index = 0; index < count; ++index)
        {
          if (registeredChannels.IsReceiver(index))
            ((IChannelReceiver) registeredChannels.GetChannel(index)).StopListening((object) null);
        }
      }
      catch (Exception ex)
      {
      }
    }

    [SecurityCritical]
    internal static void NotifyProfiler(IMessage msg, RemotingProfilerEvent profilerEvent)
    {
      switch (profilerEvent)
      {
        case RemotingProfilerEvent.ClientSend:
          if (!RemotingServices.CORProfilerTrackRemoting())
            break;
          Guid id1;
          RemotingServices.CORProfilerRemotingClientSendingMessage(out id1, false);
          if (!RemotingServices.CORProfilerTrackRemotingCookie())
            break;
          msg.Properties[(object) "CORProfilerCookie"] = (object) id1;
          break;
        case RemotingProfilerEvent.ClientReceive:
          if (!RemotingServices.CORProfilerTrackRemoting())
            break;
          Guid id2 = Guid.Empty;
          if (RemotingServices.CORProfilerTrackRemotingCookie())
          {
            object property = msg.Properties[(object) "CORProfilerCookie"];
            if (property != null)
              id2 = (Guid) property;
          }
          RemotingServices.CORProfilerRemotingClientReceivingReply(id2, false);
          break;
      }
    }

    [SecurityCritical]
    internal static string FindFirstHttpUrlForObject(string objectUri)
    {
      if (objectUri == null)
        return (string) null;
      RegisteredChannelList registeredChannels = ChannelServices.s_registeredChannels;
      int count = registeredChannels.Count;
      for (int index = 0; index < count; ++index)
      {
        if (registeredChannels.IsReceiver(index))
        {
          IChannelReceiver channel = (IChannelReceiver) registeredChannels.GetChannel(index);
          string fullName = channel.GetType().FullName;
          if (string.CompareOrdinal(fullName, "System.Runtime.Remoting.Channels.Http.HttpChannel") == 0 || string.CompareOrdinal(fullName, "System.Runtime.Remoting.Channels.Http.HttpServerChannel") == 0)
          {
            string[] urlsForUri = channel.GetUrlsForUri(objectUri);
            if (urlsForUri != null && urlsForUri.Length != 0)
              return urlsForUri[0];
          }
        }
      }
      return (string) null;
    }
  }
}

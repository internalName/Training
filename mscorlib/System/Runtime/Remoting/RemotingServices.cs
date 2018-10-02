// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.RemotingServices
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Remoting.Services;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Runtime.Remoting
{
  /// <summary>
  ///   Обеспечивает несколько методов для использования и публикации удаленных объектов и прокси.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public static class RemotingServices
  {
    private static readonly object s_delayLoadChannelLock = new object();
    private const BindingFlags LookupAll = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
    private const string FieldGetterName = "FieldGetter";
    private const string FieldSetterName = "FieldSetter";
    private const string IsInstanceOfTypeName = "IsInstanceOfType";
    private const string CanCastToXmlTypeName = "CanCastToXmlType";
    private const string InvokeMemberName = "InvokeMember";
    private static volatile MethodBase s_FieldGetterMB;
    private static volatile MethodBase s_FieldSetterMB;
    private static volatile MethodBase s_IsInstanceOfTypeMB;
    private static volatile MethodBase s_CanCastToXmlTypeMB;
    private static volatile MethodBase s_InvokeMemberMB;
    private static volatile bool s_bRemoteActivationConfigured;
    private static volatile bool s_bRegisteredWellKnownChannels;
    private static bool s_bInProcessOfRegisteringWellKnownChannels;

    /// <summary>
    ///   Возвращает логическое значение, указывающее, является ли данный объект прозрачным прокси или настоящим объектом.
    /// </summary>
    /// <param name="proxy">Ссылка на объект для проверки.</param>
    /// <returns>
    ///   Логическое значение, указывающее, является ли объект, указанный в <paramref name="proxy" /> параметр является прозрачным прокси или настоящим объектом.
    /// </returns>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern bool IsTransparentProxy(object proxy);

    /// <summary>
    ///   Возвращает логическое значение, указывающее, содержится ли объект, представленный данным прокси в контексте, отличном от объекта, который вызвал текущий метод.
    /// </summary>
    /// <param name="tp">Объект для проверки.</param>
    /// <returns>
    ///   <see langword="true" /> Если объект выходит за пределы текущего контекста; в противном случае — <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    public static bool IsObjectOutOfContext(object tp)
    {
      if (!RemotingServices.IsTransparentProxy(tp))
        return false;
      RealProxy realProxy = RemotingServices.GetRealProxy(tp);
      ServerIdentity identityObject = realProxy.IdentityObject as ServerIdentity;
      if (identityObject == null || !(realProxy is RemotingProxy))
        return true;
      return Thread.CurrentContext != identityObject.ServerContext;
    }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, содержится ли объект, указанный данным прозрачным прокси в другом домене приложения от объекта, который вызвал текущий метод.
    /// </summary>
    /// <param name="tp">Объект для проверки.</param>
    /// <returns>
    ///   <see langword="true" /> Если объект не принадлежит текущему домену приложения; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool IsObjectOutOfAppDomain(object tp)
    {
      return RemotingServices.IsClientProxy(tp);
    }

    internal static bool IsClientProxy(object obj)
    {
      MarshalByRefObject marshalByRefObject = obj as MarshalByRefObject;
      if (marshalByRefObject == null)
        return false;
      bool flag = false;
      bool fServer;
      Identity identity = MarshalByRefObject.GetIdentity(marshalByRefObject, out fServer);
      if (identity != null && !(identity is ServerIdentity))
        flag = true;
      return flag;
    }

    [SecurityCritical]
    internal static bool IsObjectOutOfProcess(object tp)
    {
      if (!RemotingServices.IsTransparentProxy(tp))
        return false;
      Identity identityObject = RemotingServices.GetRealProxy(tp).IdentityObject;
      if (identityObject is ServerIdentity)
        return false;
      if (identityObject == null)
        return true;
      ObjRef objectRef = identityObject.ObjectRef;
      return objectRef == null || !objectRef.IsFromThisProcess();
    }

    /// <summary>
    ///   Возвращает настоящий прокси, поддерживающий указанный прозрачный прокси.
    /// </summary>
    /// <param name="proxy">Прозрачный прокси.</param>
    /// <returns>Реальные прокси, поддерживающий прозрачный прокси.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern RealProxy GetRealProxy(object proxy);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object CreateTransparentProxy(RealProxy rp, RuntimeType typeToProxy, IntPtr stub, object stubData);

    [SecurityCritical]
    internal static object CreateTransparentProxy(RealProxy rp, Type typeToProxy, IntPtr stub, object stubData)
    {
      RuntimeType typeToProxy1 = typeToProxy as RuntimeType;
      if (typeToProxy1 == (RuntimeType) null)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), (object) nameof (typeToProxy)));
      return RemotingServices.CreateTransparentProxy(rp, typeToProxy1, stub, stubData);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern MarshalByRefObject AllocateUninitializedObject(RuntimeType objectType);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void CallDefaultCtor(object o);

    [SecurityCritical]
    internal static MarshalByRefObject AllocateUninitializedObject(Type objectType)
    {
      RuntimeType objectType1 = objectType as RuntimeType;
      if (objectType1 == (RuntimeType) null)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), (object) nameof (objectType)));
      return RemotingServices.AllocateUninitializedObject(objectType1);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern MarshalByRefObject AllocateInitializedObject(RuntimeType objectType);

    [SecurityCritical]
    internal static MarshalByRefObject AllocateInitializedObject(Type objectType)
    {
      RuntimeType objectType1 = objectType as RuntimeType;
      if (objectType1 == (RuntimeType) null)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_WrongType"), (object) nameof (objectType)));
      return RemotingServices.AllocateInitializedObject(objectType1);
    }

    [SecurityCritical]
    internal static bool RegisterWellKnownChannels()
    {
      if (!RemotingServices.s_bRegisteredWellKnownChannels)
      {
        bool lockTaken = false;
        object configLock = Thread.GetDomain().RemotingData.ConfigLock;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
          Monitor.Enter(configLock, ref lockTaken);
          if (!RemotingServices.s_bRegisteredWellKnownChannels)
          {
            if (!RemotingServices.s_bInProcessOfRegisteringWellKnownChannels)
            {
              RemotingServices.s_bInProcessOfRegisteringWellKnownChannels = true;
              CrossAppDomainChannel.RegisterChannel();
              RemotingServices.s_bRegisteredWellKnownChannels = true;
            }
          }
        }
        finally
        {
          if (lockTaken)
            Monitor.Exit(configLock);
        }
      }
      return true;
    }

    [SecurityCritical]
    internal static void InternalSetRemoteActivationConfigured()
    {
      if (RemotingServices.s_bRemoteActivationConfigured)
        return;
      RemotingServices.nSetRemoteActivationConfigured();
      RemotingServices.s_bRemoteActivationConfigured = true;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void nSetRemoteActivationConfigured();

    /// <summary>Получает идентификатор сеанса для сообщения.</summary>
    /// <param name="msg">
    ///   <see cref="T:System.Runtime.Remoting.Messaging.IMethodMessage" /> Для которого запрашивается идентификатор сеанса.
    /// </param>
    /// <returns>
    ///   Строка идентификатора сеанса, однозначно определяющее в текущем сеансе.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static string GetSessionIdForMethodMessage(IMethodMessage msg)
    {
      return msg.Uri;
    }

    /// <summary>
    ///   Возвращает объект службы времени существования, который управляет политикой времени существования указанного объекта.
    /// </summary>
    /// <param name="obj">
    ///   Объекта, чтобы получить время жизни службы.
    /// </param>
    /// <returns>
    ///   Объект, который управляет временем существования <paramref name="obj" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecuritySafeCritical]
    public static object GetLifetimeService(MarshalByRefObject obj)
    {
      return obj?.GetLifetimeService();
    }

    /// <summary>Извлекает URI для указанного объекта.</summary>
    /// <param name="obj">
    ///   <see cref="T:System.MarshalByRefObject" /> Для которого запрашивается URI.
    /// </param>
    /// <returns>
    ///   URI указанного объекта, если оно имеется, или <see langword="null" /> если объект не был упакован.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static string GetObjectUri(MarshalByRefObject obj)
    {
      bool fServer;
      return MarshalByRefObject.GetIdentity(obj, out fServer)?.URI;
    }

    /// <summary>
    ///   Задает URI для дальнейшего вызова в <see cref="M:System.Runtime.Remoting.RemotingServices.Marshal(System.MarshalByRefObject)" /> метод.
    /// </summary>
    /// <param name="obj">Задаваемый объект URI.</param>
    /// <param name="uri">URI, чтобы назначить указанный объект.</param>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   <paramref name="obj" /> — локальный объект, уже был маршалирован или для уже был вызван текущий метод.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static void SetObjectUriForMarshal(MarshalByRefObject obj, string uri)
    {
      bool fServer;
      Identity identity1 = MarshalByRefObject.GetIdentity(obj, out fServer);
      Identity identity2 = (Identity) (identity1 as ServerIdentity);
      if (identity1 != null && identity2 == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_SetObjectUriForMarshal__ObjectNeedsToBeLocal"));
      if (identity1 != null && identity1.URI != null)
        throw new RemotingException(Environment.GetResourceString("Remoting_SetObjectUriForMarshal__UriExists"));
      if (identity1 == null)
      {
        Context defaultContext = Thread.GetDomain().GetDefaultContext();
        ServerIdentity id = new ServerIdentity(obj, defaultContext, uri);
        if ((Identity) obj.__RaceSetServerIdentity(id) != id)
          throw new RemotingException(Environment.GetResourceString("Remoting_SetObjectUriForMarshal__UriExists"));
      }
      else
        identity1.SetOrCreateURI(uri, true);
    }

    /// <summary>
    ///   Принимает <see cref="T:System.MarshalByRefObject" />, регистрирует его с инфраструктурой удаленного взаимодействия и преобразует его в экземпляр <see cref="T:System.Runtime.Remoting.ObjRef" /> класса.
    /// </summary>
    /// <param name="Obj">Преобразуемый объект.</param>
    /// <returns>
    ///   Экземпляр <see cref="T:System.Runtime.Remoting.ObjRef" /> класс, представляющий объект, указанный в <paramref name="Obj" /> параметр.
    /// </returns>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   <paramref name="Obj" /> Параметр является прокси объектом.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static ObjRef Marshal(MarshalByRefObject Obj)
    {
      return RemotingServices.MarshalInternal(Obj, (string) null, (Type) null);
    }

    /// <summary>
    ///   Преобразует данный <see cref="T:System.MarshalByRefObject" /> в экземпляр <see cref="T:System.Runtime.Remoting.ObjRef" /> класса с заданным URI.
    /// </summary>
    /// <param name="Obj">Преобразуемый объект.</param>
    /// <param name="URI">
    ///   Указанный URI, с которого требуется инициализировать новый <see cref="T:System.Runtime.Remoting.ObjRef" />.
    ///    Может иметь значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Экземпляр <see cref="T:System.Runtime.Remoting.ObjRef" /> класс, представляющий объект, указанный в <paramref name="Obj" /> параметра.
    /// </returns>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   <paramref name="Obj" />прокси-сервер объекта и <paramref name="URI" /> не <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающим объектам выше в стеке вызовов не имеет разрешения для настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static ObjRef Marshal(MarshalByRefObject Obj, string URI)
    {
      return RemotingServices.MarshalInternal(Obj, URI, (Type) null);
    }

    /// <summary>
    ///   Принимает <see cref="T:System.MarshalByRefObject" /> и преобразует его в экземпляр <see cref="T:System.Runtime.Remoting.ObjRef" /> класса с указанным URI и предоставленным <see cref="T:System.Type" />.
    /// </summary>
    /// <param name="Obj">
    ///   Объект, преобразуемый в <see cref="T:System.Runtime.Remoting.ObjRef" />.
    /// </param>
    /// <param name="ObjURI">
    ///   URI объекта, указанного в <paramref name="Obj" /> с маршалируется параметра.
    ///    Может иметь значение <see langword="null" />.
    /// </param>
    /// <param name="RequestedType">
    ///   <see cref="T:System.Type" /><paramref name="Obj" /> Маршалируется как.
    ///    Может иметь значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Экземпляр <see cref="T:System.Runtime.Remoting.ObjRef" /> класс, представляющий объект, указанный в <paramref name="Obj" /> параметр.
    /// </returns>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   <paramref name="Obj" /> является прокси удаленного объекта и <paramref name="ObjUri" /> параметр не <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.RemotingConfiguration)]
    public static ObjRef Marshal(MarshalByRefObject Obj, string ObjURI, Type RequestedType)
    {
      return RemotingServices.MarshalInternal(Obj, ObjURI, RequestedType);
    }

    [SecurityCritical]
    internal static ObjRef MarshalInternal(MarshalByRefObject Obj, string ObjURI, Type RequestedType)
    {
      return RemotingServices.MarshalInternal(Obj, ObjURI, RequestedType, true);
    }

    [SecurityCritical]
    internal static ObjRef MarshalInternal(MarshalByRefObject Obj, string ObjURI, Type RequestedType, bool updateChannelData)
    {
      return RemotingServices.MarshalInternal(Obj, ObjURI, RequestedType, updateChannelData, false);
    }

    [SecurityCritical]
    internal static ObjRef MarshalInternal(MarshalByRefObject Obj, string ObjURI, Type RequestedType, bool updateChannelData, bool isInitializing)
    {
      if (Obj == null)
        return (ObjRef) null;
      Identity identity = RemotingServices.GetOrCreateIdentity(Obj, ObjURI, isInitializing);
      if (RequestedType != (Type) null)
      {
        ServerIdentity serverIdentity = identity as ServerIdentity;
        if (serverIdentity != null)
        {
          serverIdentity.ServerType = RequestedType;
          serverIdentity.MarshaledAsSpecificType = true;
        }
      }
      ObjRef or = identity.ObjectRef;
      if (or == null)
      {
        ObjRef objRefGiven = !RemotingServices.IsTransparentProxy((object) Obj) ? Obj.CreateObjRef(RequestedType) : RemotingServices.GetRealProxy((object) Obj).CreateObjRef(RequestedType);
        if (identity == null || objRefGiven == null)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidMarshalByRefObject"), nameof (Obj));
        or = identity.RaceSetObjRef(objRefGiven);
      }
      ServerIdentity serverIdentity1 = identity as ServerIdentity;
      if (serverIdentity1 != null)
      {
        MarshalByRefObject marshalByRefObject = (MarshalByRefObject) null;
        serverIdentity1.GetServerObjectChain(out marshalByRefObject);
        Lease lease = identity.Lease;
        if (lease != null)
        {
          lock (lease)
          {
            if (lease.CurrentState == LeaseState.Expired)
              lease.ActivateLease();
            else
              lease.RenewInternal(identity.Lease.InitialLeaseTime);
          }
        }
        if (updateChannelData && or.ChannelInfo != null)
        {
          object[] currentChannelData = ChannelServices.CurrentChannelData;
          if (!(Obj is AppDomain))
          {
            or.ChannelInfo.ChannelData = currentChannelData;
          }
          else
          {
            int length = currentChannelData.Length;
            object[] objArray = new object[length];
            Array.Copy((Array) currentChannelData, (Array) objArray, length);
            for (int index = 0; index < length; ++index)
            {
              if (!(objArray[index] is CrossAppDomainData))
                objArray[index] = (object) null;
            }
            or.ChannelInfo.ChannelData = objArray;
          }
        }
      }
      TrackingServices.MarshaledObject((object) Obj, or);
      return or;
    }

    [SecurityCritical]
    private static Identity GetOrCreateIdentity(MarshalByRefObject Obj, string ObjURI, bool isInitializing)
    {
      int flags = 2;
      if (isInitializing)
        flags |= 4;
      Identity identity;
      if (RemotingServices.IsTransparentProxy((object) Obj))
      {
        identity = RemotingServices.GetRealProxy((object) Obj).IdentityObject;
        if (identity == null)
        {
          identity = (Identity) IdentityHolder.FindOrCreateServerIdentity(Obj, ObjURI, flags);
          identity.RaceSetTransparentProxy((object) Obj);
        }
        ServerIdentity serverIdentity = identity as ServerIdentity;
        if (serverIdentity != null)
        {
          identity = (Identity) IdentityHolder.FindOrCreateServerIdentity(serverIdentity.TPOrObject, ObjURI, flags);
          if (ObjURI != null && ObjURI != Identity.RemoveAppNameOrAppGuidIfNecessary(identity.ObjURI))
            throw new RemotingException(Environment.GetResourceString("Remoting_URIExists"));
        }
        else if (ObjURI != null && ObjURI != identity.ObjURI)
          throw new RemotingException(Environment.GetResourceString("Remoting_URIToProxy"));
      }
      else
        identity = (Identity) IdentityHolder.FindOrCreateServerIdentity(Obj, ObjURI, flags);
      return identity;
    }

    /// <summary>
    ///   Упорядочивает указанный маршалер, давая ссылку на объект в предоставленный <see cref="T:System.Runtime.Serialization.SerializationInfo" />.
    /// </summary>
    /// <param name="obj">Объект для сериализации.</param>
    /// <param name="info">
    ///   <see cref="T:System.Runtime.Serialization.SerializationInfo" /> В сериализации объекта.
    /// </param>
    /// <param name="context">Источник и назначение сериализации.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="obj" /> или параметра <paramref name="info" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      RemotingServices.MarshalInternal((MarshalByRefObject) obj, (string) null, (Type) null).GetObjectData(info, context);
    }

    /// <summary>
    ///   Принимает <see cref="T:System.Runtime.Remoting.ObjRef" /> и создает прокси-объект от него.
    /// </summary>
    /// <param name="objectRef">
    ///   <see cref="T:System.Runtime.Remoting.ObjRef" /> Представляющий удаленный объект, для которого создается прокси-сервера.
    /// </param>
    /// <returns>
    ///   Прокси для объекта, заданного <see cref="T:System.Runtime.Remoting.ObjRef" /> представляет.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="T:System.Runtime.Remoting.ObjRef" /> Экземпляра, заданного в <paramref name="objectRef" /> параметр не является правильным.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecurityCritical]
    public static object Unmarshal(ObjRef objectRef)
    {
      return RemotingServices.InternalUnmarshal(objectRef, (object) null, false);
    }

    /// <summary>
    ///   Принимает <see cref="T:System.Runtime.Remoting.ObjRef" /> и создает прокси-объект от него, подгоняя его тип на сервере.
    /// </summary>
    /// <param name="objectRef">
    ///   <see cref="T:System.Runtime.Remoting.ObjRef" /> Представляющий удаленный объект, для которого создается прокси-сервера.
    /// </param>
    /// <param name="fRefine">
    ///   <see langword="true" /> Чтобы уточнить прокси для типа сервера. в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Прокси для объекта, заданного <see cref="T:System.Runtime.Remoting.ObjRef" /> представляет.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <see cref="T:System.Runtime.Remoting.ObjRef" /> Экземпляра, заданного в <paramref name="objectRef" /> параметр не является правильным.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   По крайней мере один из вызывающих, находящихся в стеке вызовов не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecurityCritical]
    public static object Unmarshal(ObjRef objectRef, bool fRefine)
    {
      return RemotingServices.InternalUnmarshal(objectRef, (object) null, fRefine);
    }

    /// <summary>
    ///   Создает прокси для хорошо известного объекта, если заданы <see cref="T:System.Type" /> и URL-адрес.
    /// </summary>
    /// <param name="classToProxy">
    ///   <see cref="T:System.Type" /> Хорошо известного объекта со стороны сервера, к которому необходимо подключиться.
    /// </param>
    /// <param name="url">URL-адрес серверного класса.</param>
    /// <returns>
    ///   Прокси удаленного объекта, который указывает на конечную точку, обслуживаемую указанным хорошо известным объектом.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecurityCritical]
    [ComVisible(true)]
    public static object Connect(Type classToProxy, string url)
    {
      return RemotingServices.Unmarshal(classToProxy, url, (object) null);
    }

    /// <summary>
    ///   Создает прокси для хорошо известного объекта, если заданы <see cref="T:System.Type" />, URL-адрес и данные указанного канала.
    /// </summary>
    /// <param name="classToProxy">
    ///   <see cref="T:System.Type" /> Хорошо известного объекта, к которому необходимо подключиться.
    /// </param>
    /// <param name="url">URL-адрес хорошо известного объекта.</param>
    /// <param name="data">
    ///   Данные для указанного канала.
    ///    Может иметь значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Прокси, который указывает на конечную точку, обслуживаемую указанным хорошо известным объектом.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecurityCritical]
    [ComVisible(true)]
    public static object Connect(Type classToProxy, string url, object data)
    {
      return RemotingServices.Unmarshal(classToProxy, url, data);
    }

    /// <summary>
    ///   Прекращает получение дальнейших сообщений по зарегистрированным каналам удаленного доступа.
    /// </summary>
    /// <param name="obj">Объекты, отключаемые от своих каналов.</param>
    /// <returns>
    ///   <see langword="true" /> Если объект был отключен от зарегистрированного канала удаленного доступа успешно; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="obj" /> Параметр является прокси.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет право настраивать каналы и типы удаленного взаимодействия.
    /// </exception>
    [SecurityCritical]
    public static bool Disconnect(MarshalByRefObject obj)
    {
      return RemotingServices.Disconnect(obj, true);
    }

    [SecurityCritical]
    internal static bool Disconnect(MarshalByRefObject obj, bool bResetURI)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      bool fServer;
      Identity identity = MarshalByRefObject.GetIdentity(obj, out fServer);
      bool flag = false;
      if (identity != null)
      {
        if (!(identity is ServerIdentity))
          throw new RemotingException(Environment.GetResourceString("Remoting_CantDisconnectClientProxy"));
        if (identity.IsInIDTable())
        {
          IdentityHolder.RemoveIdentity(identity.URI, bResetURI);
          flag = true;
        }
        TrackingServices.DisconnectedObject((object) obj);
      }
      return flag;
    }

    /// <summary>
    ///   Возвращает цепочку приемников делегата, которые должны использоваться при отправке сообщений на удаленный объект, представленный указанным прокси.
    /// </summary>
    /// <param name="obj">
    ///   Прокси удаленного объекта, запросившего приемников делегата связаны.
    /// </param>
    /// <returns>
    ///   Цепочка приемников делегата, связанных с указанным прокси.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static IMessageSink GetEnvoyChainForProxy(MarshalByRefObject obj)
    {
      IMessageSink messageSink = (IMessageSink) null;
      if (RemotingServices.IsObjectOutOfContext((object) obj))
      {
        Identity identityObject = RemotingServices.GetRealProxy((object) obj).IdentityObject;
        if (identityObject != null)
          messageSink = identityObject.EnvoyChain;
      }
      return messageSink;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Runtime.Remoting.ObjRef" /> представляющий удаленный объект из указанного прокси.
    /// </summary>
    /// <param name="obj">
    ///   Прокси, подключенный к объекту, который вы хотите создать <see cref="T:System.Runtime.Remoting.ObjRef" /> для.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Runtime.Remoting.ObjRef" /> представляющий удаленный объект, указанный прокси-сервер подключен, или <see langword="null" /> если объект или прокси не были маршалированы.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static ObjRef GetObjRefForProxy(MarshalByRefObject obj)
    {
      ObjRef objRef = (ObjRef) null;
      if (!RemotingServices.IsTransparentProxy((object) obj))
        throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_BadType"));
      Identity identityObject = RemotingServices.GetRealProxy((object) obj).IdentityObject;
      if (identityObject != null)
        objRef = identityObject.ObjectRef;
      return objRef;
    }

    [SecurityCritical]
    internal static object Unmarshal(Type classToProxy, string url)
    {
      return RemotingServices.Unmarshal(classToProxy, url, (object) null);
    }

    [SecurityCritical]
    internal static object Unmarshal(Type classToProxy, string url, object data)
    {
      if ((Type) null == classToProxy)
        throw new ArgumentNullException(nameof (classToProxy));
      if (url == null)
        throw new ArgumentNullException(nameof (url));
      if (!classToProxy.IsMarshalByRef && !classToProxy.IsInterface)
        throw new RemotingException(Environment.GetResourceString("Remoting_NotRemotableByReference"));
      Identity idObj = IdentityHolder.ResolveIdentity(url);
      if (idObj == null || idObj.ChannelSink == null || idObj.EnvoyChain == null)
      {
        IMessageSink chnlSink = (IMessageSink) null;
        IMessageSink envoySink = (IMessageSink) null;
        string envoyAndChannelSinks = RemotingServices.CreateEnvoyAndChannelSinks(url, data, out chnlSink, out envoySink);
        if (chnlSink == null)
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Connect_CantCreateChannelSink"), (object) url));
        if (envoyAndChannelSinks == null)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidUrl"));
        idObj = IdentityHolder.FindOrCreateIdentity(envoyAndChannelSinks, url, (ObjRef) null);
        RemotingServices.SetEnvoyAndChannelSinks(idObj, chnlSink, envoySink);
      }
      return RemotingServices.GetOrCreateProxy(classToProxy, idObj);
    }

    [SecurityCritical]
    internal static object Wrap(ContextBoundObject obj)
    {
      return RemotingServices.Wrap(obj, (object) null, true);
    }

    [SecurityCritical]
    internal static object Wrap(ContextBoundObject obj, object proxy, bool fCreateSinks)
    {
      if (obj == null || RemotingServices.IsTransparentProxy((object) obj))
        return (object) obj;
      Identity idObj;
      if (proxy != null)
      {
        RealProxy realProxy = RemotingServices.GetRealProxy(proxy);
        if (realProxy.UnwrappedServerObject == null)
          realProxy.AttachServerHelper((MarshalByRefObject) obj);
        idObj = MarshalByRefObject.GetIdentity((MarshalByRefObject) obj);
      }
      else
        idObj = (Identity) IdentityHolder.FindOrCreateServerIdentity((MarshalByRefObject) obj, (string) null, 0);
      proxy = RemotingServices.GetOrCreateProxy(idObj, proxy, true);
      RemotingServices.GetRealProxy(proxy).Wrap();
      if (fCreateSinks)
      {
        IMessageSink chnlSink = (IMessageSink) null;
        IMessageSink envoySink = (IMessageSink) null;
        RemotingServices.CreateEnvoyAndChannelSinks((MarshalByRefObject) proxy, (ObjRef) null, out chnlSink, out envoySink);
        RemotingServices.SetEnvoyAndChannelSinks(idObj, chnlSink, envoySink);
      }
      RealProxy realProxy1 = RemotingServices.GetRealProxy(proxy);
      if (realProxy1.UnwrappedServerObject == null)
        realProxy1.AttachServerHelper((MarshalByRefObject) obj);
      return proxy;
    }

    internal static string GetObjectUriFromFullUri(string fullUri)
    {
      if (fullUri == null)
        return (string) null;
      int num = fullUri.LastIndexOf('/');
      if (num == -1)
        return fullUri;
      return fullUri.Substring(num + 1);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object Unwrap(ContextBoundObject obj);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object AlwaysUnwrap(ContextBoundObject obj);

    [SecurityCritical]
    internal static object InternalUnmarshal(ObjRef objectRef, object proxy, bool fRefine)
    {
      Context currentContext = Thread.CurrentContext;
      if (!ObjRef.IsWellFormed(objectRef))
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_BadObjRef"), (object) "Unmarshal"));
      if (objectRef.IsWellKnown())
      {
        object obj = RemotingServices.Unmarshal(typeof (MarshalByRefObject), objectRef.URI);
        Identity identity = IdentityHolder.ResolveIdentity(objectRef.URI);
        if (identity.ObjectRef == null)
          identity.RaceSetObjRef(objectRef);
        return obj;
      }
      Identity orCreateIdentity = IdentityHolder.FindOrCreateIdentity(objectRef.URI, (string) null, objectRef);
      currentContext = Thread.CurrentContext;
      ServerIdentity serverIdentity = orCreateIdentity as ServerIdentity;
      object obj1;
      if (serverIdentity != null)
      {
        currentContext = Thread.CurrentContext;
        if (!serverIdentity.IsContextBound)
        {
          if (proxy != null)
            throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadInternalState_ProxySameAppDomain"), Array.Empty<object>()));
          obj1 = (object) serverIdentity.TPOrObject;
        }
        else
        {
          IMessageSink chnlSink = (IMessageSink) null;
          IMessageSink envoySink = (IMessageSink) null;
          RemotingServices.CreateEnvoyAndChannelSinks(serverIdentity.TPOrObject, (ObjRef) null, out chnlSink, out envoySink);
          RemotingServices.SetEnvoyAndChannelSinks(orCreateIdentity, chnlSink, envoySink);
          obj1 = RemotingServices.GetOrCreateProxy(orCreateIdentity, proxy, true);
        }
      }
      else
      {
        IMessageSink chnlSink = (IMessageSink) null;
        IMessageSink envoySink = (IMessageSink) null;
        if (!objectRef.IsObjRefLite())
          RemotingServices.CreateEnvoyAndChannelSinks((MarshalByRefObject) null, objectRef, out chnlSink, out envoySink);
        else
          RemotingServices.CreateEnvoyAndChannelSinks(objectRef.URI, (object) null, out chnlSink, out envoySink);
        RemotingServices.SetEnvoyAndChannelSinks(orCreateIdentity, chnlSink, envoySink);
        if (objectRef.HasProxyAttribute())
          fRefine = true;
        obj1 = RemotingServices.GetOrCreateProxy(orCreateIdentity, proxy, fRefine);
      }
      TrackingServices.UnmarshaledObject(obj1, objectRef);
      return obj1;
    }

    [SecurityCritical]
    private static object GetOrCreateProxy(Identity idObj, object proxy, bool fRefine)
    {
      if (proxy == null)
      {
        ServerIdentity serverIdentity = idObj as ServerIdentity;
        Type classToProxy;
        if (serverIdentity != null)
        {
          classToProxy = serverIdentity.ServerType;
        }
        else
        {
          IRemotingTypeInfo typeInfo = idObj.ObjectRef.TypeInfo;
          classToProxy = (Type) null;
          if (typeInfo is TypeInfo && !fRefine || typeInfo == null)
          {
            classToProxy = typeof (MarshalByRefObject);
          }
          else
          {
            string typeName1 = typeInfo.TypeName;
            if (typeName1 != null)
            {
              string typeName2 = (string) null;
              string assemName = (string) null;
              TypeInfo.ParseTypeAndAssembly(typeName1, out typeName2, out assemName);
              Assembly assembly = FormatterServices.LoadAssemblyFromStringNoThrow(assemName);
              if (assembly != (Assembly) null)
                classToProxy = assembly.GetType(typeName2, false, false);
            }
          }
          if ((Type) null == classToProxy)
            throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), (object) typeInfo.TypeName));
        }
        proxy = (object) RemotingServices.SetOrCreateProxy(idObj, classToProxy, (object) null);
      }
      else
        proxy = (object) RemotingServices.SetOrCreateProxy(idObj, (Type) null, proxy);
      if (proxy == null)
        throw new RemotingException(Environment.GetResourceString("Remoting_UnexpectedNullTP"));
      return proxy;
    }

    [SecurityCritical]
    private static object GetOrCreateProxy(Type classToProxy, Identity idObj)
    {
      object obj = (object) idObj.TPOrObject ?? (object) RemotingServices.SetOrCreateProxy(idObj, classToProxy, (object) null);
      ServerIdentity serverIdentity = idObj as ServerIdentity;
      if (serverIdentity != null)
      {
        Type serverType = serverIdentity.ServerType;
        if (!classToProxy.IsAssignableFrom(serverType))
          throw new InvalidCastException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("InvalidCast_FromTo"), (object) serverType.FullName, (object) classToProxy.FullName));
      }
      return obj;
    }

    [SecurityCritical]
    private static MarshalByRefObject SetOrCreateProxy(Identity idObj, Type classToProxy, object proxy)
    {
      RealProxy realProxy = (RealProxy) null;
      if (proxy == null)
      {
        ServerIdentity serverIdentity = idObj as ServerIdentity;
        if (idObj.ObjectRef != null)
          realProxy = ActivationServices.GetProxyAttribute(classToProxy).CreateProxy(idObj.ObjectRef, classToProxy, (object) null, (Context) null);
        if (realProxy == null)
          realProxy = ActivationServices.DefaultProxyAttribute.CreateProxy(idObj.ObjectRef, classToProxy, (object) null, serverIdentity == null ? (Context) null : serverIdentity.ServerContext);
      }
      else
        realProxy = RemotingServices.GetRealProxy(proxy);
      realProxy.IdentityObject = idObj;
      proxy = realProxy.GetTransparentProxy();
      proxy = idObj.RaceSetTransparentProxy(proxy);
      return (MarshalByRefObject) proxy;
    }

    private static bool AreChannelDataElementsNull(object[] channelData)
    {
      foreach (object obj in channelData)
      {
        if (obj != null)
          return false;
      }
      return true;
    }

    [SecurityCritical]
    internal static void CreateEnvoyAndChannelSinks(MarshalByRefObject tpOrObject, ObjRef objectRef, out IMessageSink chnlSink, out IMessageSink envoySink)
    {
      chnlSink = (IMessageSink) null;
      envoySink = (IMessageSink) null;
      if (objectRef == null)
      {
        chnlSink = ChannelServices.GetCrossContextChannelSink();
        envoySink = Thread.CurrentContext.CreateEnvoyChain(tpOrObject);
      }
      else
      {
        object[] channelData = objectRef.ChannelInfo.ChannelData;
        if (channelData != null && !RemotingServices.AreChannelDataElementsNull(channelData))
        {
          for (int index = 0; index < channelData.Length; ++index)
          {
            chnlSink = ChannelServices.CreateMessageSink(channelData[index]);
            if (chnlSink != null)
              break;
          }
          if (chnlSink == null)
          {
            lock (RemotingServices.s_delayLoadChannelLock)
            {
              for (int index = 0; index < channelData.Length; ++index)
              {
                chnlSink = ChannelServices.CreateMessageSink(channelData[index]);
                if (chnlSink != null)
                  break;
              }
              if (chnlSink == null)
              {
                foreach (object data in channelData)
                {
                  string objectURI;
                  chnlSink = RemotingConfigHandler.FindDelayLoadChannelForCreateMessageSink((string) null, data, out objectURI);
                  if (chnlSink != null)
                    break;
                }
              }
            }
          }
        }
        if (objectRef.EnvoyInfo != null && objectRef.EnvoyInfo.EnvoySinks != null)
          envoySink = objectRef.EnvoyInfo.EnvoySinks;
        else
          envoySink = EnvoyTerminatorSink.MessageSink;
      }
    }

    [SecurityCritical]
    internal static string CreateEnvoyAndChannelSinks(string url, object data, out IMessageSink chnlSink, out IMessageSink envoySink)
    {
      string channelSink = RemotingServices.CreateChannelSink(url, data, out chnlSink);
      envoySink = EnvoyTerminatorSink.MessageSink;
      return channelSink;
    }

    [SecurityCritical]
    private static string CreateChannelSink(string url, object data, out IMessageSink chnlSink)
    {
      string objectURI = (string) null;
      chnlSink = ChannelServices.CreateMessageSink(url, data, out objectURI);
      if (chnlSink == null)
      {
        lock (RemotingServices.s_delayLoadChannelLock)
        {
          chnlSink = ChannelServices.CreateMessageSink(url, data, out objectURI);
          if (chnlSink == null)
            chnlSink = RemotingConfigHandler.FindDelayLoadChannelForCreateMessageSink(url, data, out objectURI);
        }
      }
      return objectURI;
    }

    internal static void SetEnvoyAndChannelSinks(Identity idObj, IMessageSink chnlSink, IMessageSink envoySink)
    {
      if (idObj.ChannelSink == null && chnlSink != null)
        idObj.RaceSetChannelSink(chnlSink);
      if (idObj.EnvoyChain != null)
        return;
      if (envoySink == null)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadInternalState_FailEnvoySink"), Array.Empty<object>()));
      idObj.RaceSetEnvoyChain(envoySink);
    }

    [SecurityCritical]
    private static bool CheckCast(RealProxy rp, RuntimeType castType)
    {
      bool flag = false;
      if ((Type) castType == typeof (object))
        return true;
      if (!castType.IsInterface && !castType.IsMarshalByRef)
        return false;
      if ((Type) castType != typeof (IObjectReference))
      {
        IRemotingTypeInfo remotingTypeInfo = rp as IRemotingTypeInfo;
        if (remotingTypeInfo != null)
        {
          flag = remotingTypeInfo.CanCastTo((Type) castType, rp.GetTransparentProxy());
        }
        else
        {
          Identity identityObject = rp.IdentityObject;
          if (identityObject != null)
          {
            ObjRef objectRef = identityObject.ObjectRef;
            if (objectRef != null)
            {
              IRemotingTypeInfo typeInfo = objectRef.TypeInfo;
              if (typeInfo != null)
                flag = typeInfo.CanCastTo((Type) castType, rp.GetTransparentProxy());
            }
          }
        }
      }
      return flag;
    }

    [SecurityCritical]
    internal static bool ProxyCheckCast(RealProxy rp, RuntimeType castType)
    {
      return RemotingServices.CheckCast(rp, castType);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object CheckCast(object objToExpand, RuntimeType type);

    [SecurityCritical]
    internal static GCHandle CreateDelegateInvocation(WaitCallback waitDelegate, object state)
    {
      return GCHandle.Alloc((object) new object[2]
      {
        (object) waitDelegate,
        state
      });
    }

    [SecurityCritical]
    internal static void DisposeDelegateInvocation(GCHandle delegateCallToken)
    {
      delegateCallToken.Free();
    }

    [SecurityCritical]
    internal static object CreateProxyForDomain(int appDomainId, IntPtr defCtxID)
    {
      return (object) (AppDomain) RemotingServices.Unmarshal(RemotingServices.CreateDataForDomain(appDomainId, defCtxID));
    }

    [SecurityCritical]
    internal static object CreateDataForDomainCallback(object[] args)
    {
      RemotingServices.RegisterWellKnownChannels();
      ObjRef objRef = RemotingServices.MarshalInternal((MarshalByRefObject) Thread.CurrentContext.AppDomain, (string) null, (Type) null, false);
      ServerIdentity identity = (ServerIdentity) MarshalByRefObject.GetIdentity((MarshalByRefObject) Thread.CurrentContext.AppDomain);
      identity.SetHandle();
      objRef.SetServerIdentity(identity.GetHandle());
      objRef.SetDomainID(AppDomain.CurrentDomain.GetId());
      return (object) objRef;
    }

    [SecurityCritical]
    internal static ObjRef CreateDataForDomain(int appDomainId, IntPtr defCtxID)
    {
      RemotingServices.RegisterWellKnownChannels();
      InternalCrossContextDelegate ftnToCall = new InternalCrossContextDelegate(RemotingServices.CreateDataForDomainCallback);
      return (ObjRef) Thread.CurrentThread.InternalCrossContextCallback((Context) null, defCtxID, appDomainId, ftnToCall, (object[]) null);
    }

    /// <summary>
    ///   Возвращает метод базового из данного <see cref="T:System.Runtime.Remoting.Messaging.IMethodMessage" />.
    /// </summary>
    /// <param name="msg">
    ///   Сообщение метода для извлечения из базового метода.
    /// </param>
    /// <returns>
    ///   База метода, извлеченная из <paramref name="msg" /> параметр.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий не имеет разрешения инфраструктуры, или по крайней мере один из вызывающих, находящихся в стеке вызовов не имеет разрешения на получение сведений о закрытых членов типе.
    /// </exception>
    [SecurityCritical]
    public static MethodBase GetMethodBaseFromMethodMessage(IMethodMessage msg)
    {
      return RemotingServices.InternalGetMethodBaseFromMethodMessage(msg);
    }

    [SecurityCritical]
    internal static MethodBase InternalGetMethodBaseFromMethodMessage(IMethodMessage msg)
    {
      if (msg == null)
        return (MethodBase) null;
      Type qualifiedTypeName = RemotingServices.InternalGetTypeFromQualifiedTypeName(msg.TypeName);
      if (qualifiedTypeName == (Type) null)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), (object) msg.TypeName));
      Type[] methodSignature = (Type[]) msg.MethodSignature;
      return RemotingServices.GetMethodBase(msg, qualifiedTypeName, methodSignature);
    }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, перезагружен ли метод в данном сообщении.
    /// </summary>
    /// <param name="msg">
    ///   Сообщение, содержащее вызов метода в вопросе.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если метод, вызываемый в <paramref name="msg" /> перегруженных; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static bool IsMethodOverloaded(IMethodMessage msg)
    {
      return InternalRemotingServices.GetReflectionCachedData(msg.MethodBase).IsOverloaded();
    }

    [SecurityCritical]
    private static MethodBase GetMethodBase(IMethodMessage msg, Type t, Type[] signature)
    {
      MethodBase methodBase = (MethodBase) null;
      if (msg is IConstructionCallMessage || msg is IConstructionReturnMessage)
      {
        if (signature == null)
        {
          RuntimeType runtimeType = t as RuntimeType;
          ConstructorInfo[] constructorInfoArray = !(runtimeType == (RuntimeType) null) ? runtimeType.GetConstructors() : t.GetConstructors();
          if (1 != constructorInfoArray.Length)
            throw new AmbiguousMatchException(Environment.GetResourceString("Remoting_AmbiguousCTOR"));
          methodBase = (MethodBase) constructorInfoArray[0];
        }
        else
        {
          RuntimeType runtimeType = t as RuntimeType;
          methodBase = !(runtimeType == (RuntimeType) null) ? (MethodBase) runtimeType.GetConstructor(signature) : (MethodBase) t.GetConstructor(signature);
        }
      }
      else if (msg is IMethodCallMessage || msg is IMethodReturnMessage)
      {
        if (signature == null)
        {
          RuntimeType runtimeType = t as RuntimeType;
          methodBase = !(runtimeType == (RuntimeType) null) ? (MethodBase) runtimeType.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic) : (MethodBase) t.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        }
        else
        {
          RuntimeType runtimeType = t as RuntimeType;
          methodBase = !(runtimeType == (RuntimeType) null) ? (MethodBase) runtimeType.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, CallingConventions.Any, signature, (ParameterModifier[]) null) : (MethodBase) t.GetMethod(msg.MethodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, signature, (ParameterModifier[]) null);
        }
      }
      return methodBase;
    }

    [SecurityCritical]
    internal static bool IsMethodAllowedRemotely(MethodBase method)
    {
      if (RemotingServices.s_FieldGetterMB == (MethodBase) null || RemotingServices.s_FieldSetterMB == (MethodBase) null || (RemotingServices.s_IsInstanceOfTypeMB == (MethodBase) null || RemotingServices.s_InvokeMemberMB == (MethodBase) null) || RemotingServices.s_CanCastToXmlTypeMB == (MethodBase) null)
      {
        CodeAccessPermission.Assert(true);
        if (RemotingServices.s_FieldGetterMB == (MethodBase) null)
          RemotingServices.s_FieldGetterMB = (MethodBase) typeof (object).GetMethod("FieldGetter", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        if (RemotingServices.s_FieldSetterMB == (MethodBase) null)
          RemotingServices.s_FieldSetterMB = (MethodBase) typeof (object).GetMethod("FieldSetter", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        if (RemotingServices.s_IsInstanceOfTypeMB == (MethodBase) null)
          RemotingServices.s_IsInstanceOfTypeMB = (MethodBase) typeof (MarshalByRefObject).GetMethod("IsInstanceOfType", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        if (RemotingServices.s_CanCastToXmlTypeMB == (MethodBase) null)
          RemotingServices.s_CanCastToXmlTypeMB = (MethodBase) typeof (MarshalByRefObject).GetMethod("CanCastToXmlType", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        if (RemotingServices.s_InvokeMemberMB == (MethodBase) null)
          RemotingServices.s_InvokeMemberMB = (MethodBase) typeof (MarshalByRefObject).GetMethod("InvokeMember", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      }
      if (!(method == RemotingServices.s_FieldGetterMB) && !(method == RemotingServices.s_FieldSetterMB) && (!(method == RemotingServices.s_IsInstanceOfTypeMB) && !(method == RemotingServices.s_InvokeMemberMB)))
        return method == RemotingServices.s_CanCastToXmlTypeMB;
      return true;
    }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, ожидает ли клиент, вызывающий метод указанного в данного сообщения сервер, чтобы завершить обработку перед продолжением выполнения метода.
    /// </summary>
    /// <param name="method">Метод в вопросе.</param>
    /// <returns>
    ///   <see langword="true" /> Если метод односторонний; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    public static bool IsOneWay(MethodBase method)
    {
      if (method == (MethodBase) null)
        return false;
      return InternalRemotingServices.GetReflectionCachedData(method).IsOneWayMethod();
    }

    internal static bool FindAsyncMethodVersion(MethodInfo method, out MethodInfo beginMethod, out MethodInfo endMethod)
    {
      beginMethod = (MethodInfo) null;
      endMethod = (MethodInfo) null;
      string str1 = "Begin" + method.Name;
      string str2 = "End" + method.Name;
      ArrayList params1_1 = new ArrayList();
      ArrayList params1_2 = new ArrayList();
      Type type = typeof (IAsyncResult);
      Type returnType = method.ReturnType;
      foreach (ParameterInfo parameter in method.GetParameters())
      {
        if (parameter.IsOut)
          params1_2.Add((object) parameter);
        else if (parameter.ParameterType.IsByRef)
        {
          params1_1.Add((object) parameter);
          params1_2.Add((object) parameter);
        }
        else
          params1_1.Add((object) parameter);
      }
      params1_1.Add((object) typeof (AsyncCallback));
      params1_1.Add((object) typeof (object));
      params1_2.Add((object) typeof (IAsyncResult));
      foreach (MethodInfo method1 in method.DeclaringType.GetMethods())
      {
        ParameterInfo[] parameters = method1.GetParameters();
        if (method1.Name.Equals(str1) && method1.ReturnType == type && RemotingServices.CompareParameterList(params1_1, parameters))
          beginMethod = method1;
        else if (method1.Name.Equals(str2) && method1.ReturnType == returnType && RemotingServices.CompareParameterList(params1_2, parameters))
          endMethod = method1;
      }
      return beginMethod != (MethodInfo) null && endMethod != (MethodInfo) null;
    }

    private static bool CompareParameterList(ArrayList params1, ParameterInfo[] params2)
    {
      if (params1.Count != params2.Length)
        return false;
      int index = 0;
      foreach (object obj in params1)
      {
        ParameterInfo parameterInfo1 = params2[index];
        ParameterInfo parameterInfo2 = obj as ParameterInfo;
        if (parameterInfo2 != null)
        {
          if (parameterInfo2.ParameterType != parameterInfo1.ParameterType || parameterInfo2.IsIn != parameterInfo1.IsIn || parameterInfo2.IsOut != parameterInfo1.IsOut)
            return false;
        }
        else if ((Type) obj != parameterInfo1.ParameterType && parameterInfo1.IsIn)
          return false;
        ++index;
      }
      return true;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Type" /> объекта с указанным URI.
    /// </summary>
    /// <param name="URI">
    ///   URI объекта, <see cref="T:System.Type" /> запрашивается.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Type" /> Объекта с указанным URI.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий не имеет разрешения инфраструктуры, или по крайней мере один из вызывающих, находящихся в стеке вызовов не имеет разрешения на получение сведений о закрытых членов типе.
    /// </exception>
    [SecurityCritical]
    public static Type GetServerTypeForUri(string URI)
    {
      Type type = (Type) null;
      if (URI != null)
      {
        ServerIdentity serverIdentity = (ServerIdentity) IdentityHolder.ResolveIdentity(URI);
        type = serverIdentity != null ? serverIdentity.ServerType : RemotingConfigHandler.GetServerTypeForUri(URI);
      }
      return type;
    }

    [SecurityCritical]
    internal static void DomainUnloaded(int domainID)
    {
      IdentityHolder.FlushIdentityTable();
      CrossAppDomainSink.DomainUnloaded(domainID);
    }

    [SecurityCritical]
    internal static IntPtr GetServerContextForProxy(object tp)
    {
      ObjRef objRef = (ObjRef) null;
      bool bSameDomain;
      int domainId;
      return RemotingServices.GetServerContextForProxy(tp, out objRef, out bSameDomain, out domainId);
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal static int GetServerDomainIdForProxy(object tp)
    {
      return RemotingServices.GetRealProxy(tp).IdentityObject.ObjectRef.GetServerDomainId();
    }

    [SecurityCritical]
    internal static void GetServerContextAndDomainIdForProxy(object tp, out IntPtr contextId, out int domainId)
    {
      ObjRef objRef;
      bool bSameDomain;
      contextId = RemotingServices.GetServerContextForProxy(tp, out objRef, out bSameDomain, out domainId);
    }

    [SecurityCritical]
    private static IntPtr GetServerContextForProxy(object tp, out ObjRef objRef, out bool bSameDomain, out int domainId)
    {
      IntPtr num = IntPtr.Zero;
      objRef = (ObjRef) null;
      bSameDomain = false;
      domainId = 0;
      if (RemotingServices.IsTransparentProxy(tp))
      {
        Identity identityObject = RemotingServices.GetRealProxy(tp).IdentityObject;
        if (identityObject != null)
        {
          ServerIdentity serverIdentity = identityObject as ServerIdentity;
          if (serverIdentity != null)
          {
            bSameDomain = true;
            num = serverIdentity.ServerContext.InternalContextID;
            domainId = Thread.GetDomain().GetId();
          }
          else
          {
            objRef = identityObject.ObjectRef;
            num = objRef == null ? IntPtr.Zero : objRef.GetServerContext(out domainId);
          }
        }
        else
          num = Context.DefaultContext.InternalContextID;
      }
      return num;
    }

    [SecurityCritical]
    internal static Context GetServerContext(MarshalByRefObject obj)
    {
      Context context = (Context) null;
      if (!RemotingServices.IsTransparentProxy((object) obj) && obj is ContextBoundObject)
      {
        context = Thread.CurrentContext;
      }
      else
      {
        ServerIdentity identityObject = RemotingServices.GetRealProxy((object) obj).IdentityObject as ServerIdentity;
        if (identityObject != null)
          context = identityObject.ServerContext;
      }
      return context;
    }

    [SecurityCritical]
    private static object GetType(object tp)
    {
      Type type = (Type) null;
      Identity identityObject = RemotingServices.GetRealProxy(tp).IdentityObject;
      if (identityObject != null && identityObject.ObjectRef != null && identityObject.ObjectRef.TypeInfo != null)
      {
        string typeName = identityObject.ObjectRef.TypeInfo.TypeName;
        if (typeName != null)
          type = RemotingServices.InternalGetTypeFromQualifiedTypeName(typeName);
      }
      return (object) type;
    }

    [SecurityCritical]
    internal static byte[] MarshalToBuffer(object o, bool crossRuntime)
    {
      if (crossRuntime)
      {
        if (RemotingServices.IsTransparentProxy(o))
        {
          if (RemotingServices.GetRealProxy(o) is RemotingProxy && ChannelServices.RegisteredChannels.Length == 0)
            return (byte[]) null;
        }
        else
        {
          MarshalByRefObject marshalByRefObject = o as MarshalByRefObject;
          if (marshalByRefObject != null && ActivationServices.GetProxyAttribute(marshalByRefObject.GetType()) == ActivationServices.DefaultProxyAttribute && ChannelServices.RegisteredChannels.Length == 0)
            return (byte[]) null;
        }
      }
      MemoryStream memoryStream = new MemoryStream();
      RemotingSurrogateSelector surrogateSelector = new RemotingSurrogateSelector();
      new BinaryFormatter()
      {
        SurrogateSelector = ((ISurrogateSelector) surrogateSelector),
        Context = new StreamingContext(StreamingContextStates.Other)
      }.Serialize((Stream) memoryStream, o, (Header[]) null, false);
      return memoryStream.GetBuffer();
    }

    [SecurityCritical]
    internal static object UnmarshalFromBuffer(byte[] b, bool crossRuntime)
    {
      object proxy = new BinaryFormatter()
      {
        AssemblyFormat = FormatterAssemblyStyle.Simple,
        SurrogateSelector = ((ISurrogateSelector) null),
        Context = new StreamingContext(StreamingContextStates.Other)
      }.Deserialize((Stream) new MemoryStream(b), (HeaderHandler) null, false);
      if (crossRuntime && RemotingServices.IsTransparentProxy(proxy) && RemotingServices.GetRealProxy(proxy) is RemotingProxy)
      {
        if (ChannelServices.RegisteredChannels.Length == 0)
          return (object) null;
        proxy.GetHashCode();
      }
      return proxy;
    }

    internal static object UnmarshalReturnMessageFromBuffer(byte[] b, IMethodCallMessage msg)
    {
      return new BinaryFormatter()
      {
        SurrogateSelector = ((ISurrogateSelector) null),
        Context = new StreamingContext(StreamingContextStates.Other)
      }.DeserializeMethodResponse((Stream) new MemoryStream(b), (HeaderHandler) null, msg);
    }

    /// <summary>
    ///   Подключается к указанному удаленному объекту и выполняет предоставленный <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> на нем.
    /// </summary>
    /// <param name="target">
    ///   Удаленный объект, метод которого необходимо вызвать.
    /// </param>
    /// <param name="reqMsg">
    ///   Сообщение вызова метода метода указанного удаленного объекта.
    /// </param>
    /// <returns>Ответ удаленного метода.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   Метод был вызван из контекста, отличного от собственного контекста объекта.
    /// </exception>
    [SecurityCritical]
    public static IMethodReturnMessage ExecuteMessage(MarshalByRefObject target, IMethodCallMessage reqMsg)
    {
      if (target == null)
        throw new ArgumentNullException(nameof (target));
      RealProxy realProxy = RemotingServices.GetRealProxy((object) target);
      if (realProxy is RemotingProxy && !realProxy.DoContextsMatch())
        throw new RemotingException(Environment.GetResourceString("Remoting_Proxy_WrongContext"));
      return (IMethodReturnMessage) new StackBuilderSink(target).SyncProcessMessage((IMessage) reqMsg);
    }

    [SecurityCritical]
    internal static string DetermineDefaultQualifiedTypeName(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      string xmlType = (string) null;
      string xmlTypeNamespace = (string) null;
      if (SoapServices.GetXmlTypeForInteropType(type, out xmlType, out xmlTypeNamespace))
        return "soap:" + xmlType + ", " + xmlTypeNamespace;
      return type.AssemblyQualifiedName;
    }

    [SecurityCritical]
    internal static string GetDefaultQualifiedTypeName(RuntimeType type)
    {
      return InternalRemotingServices.GetReflectionCachedData(type).QualifiedTypeName;
    }

    internal static string InternalGetClrTypeNameFromQualifiedTypeName(string qualifiedTypeName)
    {
      if (qualifiedTypeName.Length > 4 && string.CompareOrdinal(qualifiedTypeName, 0, "clr:", 0, 4) == 0)
        return qualifiedTypeName.Substring(4);
      return (string) null;
    }

    private static int IsSoapType(string qualifiedTypeName)
    {
      if (qualifiedTypeName.Length > 5 && string.CompareOrdinal(qualifiedTypeName, 0, "soap:", 0, 5) == 0)
        return qualifiedTypeName.IndexOf(',', 5);
      return -1;
    }

    [SecurityCritical]
    internal static string InternalGetSoapTypeNameFromQualifiedTypeName(string xmlTypeName, string xmlTypeNamespace)
    {
      string typeNamespace;
      string assemblyName;
      if (!SoapServices.DecodeXmlNamespaceForClrTypeNamespace(xmlTypeNamespace, out typeNamespace, out assemblyName))
        return (string) null;
      string str = typeNamespace == null || typeNamespace.Length <= 0 ? xmlTypeName : typeNamespace + "." + xmlTypeName;
      try
      {
        return str + ", " + assemblyName;
      }
      catch
      {
      }
      return (string) null;
    }

    [SecurityCritical]
    internal static string InternalGetTypeNameFromQualifiedTypeName(string qualifiedTypeName)
    {
      if (qualifiedTypeName == null)
        throw new ArgumentNullException(nameof (qualifiedTypeName));
      string qualifiedTypeName1 = RemotingServices.InternalGetClrTypeNameFromQualifiedTypeName(qualifiedTypeName);
      if (qualifiedTypeName1 != null)
        return qualifiedTypeName1;
      int num = RemotingServices.IsSoapType(qualifiedTypeName);
      if (num != -1)
      {
        string qualifiedTypeName2 = RemotingServices.InternalGetSoapTypeNameFromQualifiedTypeName(qualifiedTypeName.Substring(5, num - 5), qualifiedTypeName.Substring(num + 2, qualifiedTypeName.Length - (num + 2)));
        if (qualifiedTypeName2 != null)
          return qualifiedTypeName2;
      }
      return qualifiedTypeName;
    }

    [SecurityCritical]
    internal static RuntimeType InternalGetTypeFromQualifiedTypeName(string qualifiedTypeName, bool partialFallback)
    {
      if (qualifiedTypeName == null)
        throw new ArgumentNullException(nameof (qualifiedTypeName));
      string qualifiedTypeName1 = RemotingServices.InternalGetClrTypeNameFromQualifiedTypeName(qualifiedTypeName);
      if (qualifiedTypeName1 != null)
        return RemotingServices.LoadClrTypeWithPartialBindFallback(qualifiedTypeName1, partialFallback);
      int num = RemotingServices.IsSoapType(qualifiedTypeName);
      if (num != -1)
      {
        string str = qualifiedTypeName.Substring(5, num - 5);
        string xmlTypeNamespace = qualifiedTypeName.Substring(num + 2, qualifiedTypeName.Length - (num + 2));
        RuntimeType interopTypeFromXmlType = (RuntimeType) SoapServices.GetInteropTypeFromXmlType(str, xmlTypeNamespace);
        if (interopTypeFromXmlType != (RuntimeType) null)
          return interopTypeFromXmlType;
        string qualifiedTypeName2 = RemotingServices.InternalGetSoapTypeNameFromQualifiedTypeName(str, xmlTypeNamespace);
        if (qualifiedTypeName2 != null)
          return RemotingServices.LoadClrTypeWithPartialBindFallback(qualifiedTypeName2, true);
      }
      return RemotingServices.LoadClrTypeWithPartialBindFallback(qualifiedTypeName, partialFallback);
    }

    [SecurityCritical]
    internal static Type InternalGetTypeFromQualifiedTypeName(string qualifiedTypeName)
    {
      return (Type) RemotingServices.InternalGetTypeFromQualifiedTypeName(qualifiedTypeName, true);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static RuntimeType LoadClrTypeWithPartialBindFallback(string typeName, bool partialFallback)
    {
      if (!partialFallback)
        return (RuntimeType) Type.GetType(typeName, false);
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return RuntimeTypeHandle.GetTypeByName(typeName, false, false, false, ref stackMark, true);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool CORProfilerTrackRemoting();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool CORProfilerTrackRemotingCookie();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool CORProfilerTrackRemotingAsync();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void CORProfilerRemotingClientSendingMessage(out Guid id, bool fIsAsync);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void CORProfilerRemotingClientReceivingReply(Guid id, bool fIsAsync);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void CORProfilerRemotingServerReceivingMessage(Guid id, bool fIsAsync);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void CORProfilerRemotingServerSendingReply(out Guid id, bool fIsAsync);

    /// <summary>
    ///   Регистрирует этап удаленного обмена во внешнем отладчике.
    /// </summary>
    /// <param name="stage">
    ///   Определенная внутри константа, определяющая этап удаленного обмена.
    /// </param>
    [SecurityCritical]
    [Conditional("REMOTING_PERF")]
    [Obsolete("Use of this method is not recommended. The LogRemotingStage existed for internal diagnostic purposes only.")]
    public static void LogRemotingStage(int stage)
    {
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void ResetInterfaceCache(object proxy);
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.ObjRef
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting
{
  /// <summary>
  ///   Хранит все сведения, необходимые для создания прокси для связи с удаленным объектом.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class ObjRef : IObjectReference, ISerializable
  {
    private static Type orType = typeof (ObjRef);
    internal const int FLG_MARSHALED_OBJECT = 1;
    internal const int FLG_WELLKNOWN_OBJREF = 2;
    internal const int FLG_LITE_OBJREF = 4;
    internal const int FLG_PROXY_ATTRIBUTE = 8;
    internal string uri;
    internal IRemotingTypeInfo typeInfo;
    internal IEnvoyInfo envoyInfo;
    internal IChannelInfo channelInfo;
    internal int objrefFlags;
    internal GCHandle srvIdentity;
    internal int domainID;

    internal void SetServerIdentity(GCHandle hndSrvIdentity)
    {
      this.srvIdentity = hndSrvIdentity;
    }

    internal GCHandle GetServerIdentity()
    {
      return this.srvIdentity;
    }

    internal void SetDomainID(int id)
    {
      this.domainID = id;
    }

    internal int GetDomainID()
    {
      return this.domainID;
    }

    [SecurityCritical]
    private ObjRef(ObjRef o)
    {
      this.uri = o.uri;
      this.typeInfo = o.typeInfo;
      this.envoyInfo = o.envoyInfo;
      this.channelInfo = o.channelInfo;
      this.objrefFlags = o.objrefFlags;
      this.SetServerIdentity(o.GetServerIdentity());
      this.SetDomainID(o.GetDomainID());
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.ObjRef" /> класса для ссылки на указанный <see cref="T:System.MarshalByRefObject" /> указанного <see cref="T:System.Type" />.
    /// </summary>
    /// <param name="o">
    ///   Объект, созданный <see cref="T:System.Runtime.Remoting.ObjRef" /> будет ссылаться на экземпляр.
    /// </param>
    /// <param name="requestedType">
    ///   <see cref="T:System.Type" /> Объекта, новый <see cref="T:System.Runtime.Remoting.ObjRef" /> будет ссылаться на экземпляр.
    /// </param>
    [SecurityCritical]
    public ObjRef(MarshalByRefObject o, Type requestedType)
    {
      if (o == null)
        throw new ArgumentNullException(nameof (o));
      RuntimeType requestedType1 = requestedType as RuntimeType;
      if (requestedType != (Type) null && requestedType1 == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      bool fServer;
      Identity identity = MarshalByRefObject.GetIdentity(o, out fServer);
      this.Init((object) o, identity, requestedType1);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.ObjRef" /> из сериализованных данных.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении исключения.
    /// </param>
    [SecurityCritical]
    protected ObjRef(SerializationInfo info, StreamingContext context)
    {
      string str = (string) null;
      bool flag = false;
      SerializationInfoEnumerator enumerator = info.GetEnumerator();
      while (enumerator.MoveNext())
      {
        if (enumerator.Name.Equals(nameof (uri)))
          this.uri = (string) enumerator.Value;
        else if (enumerator.Name.Equals(nameof (typeInfo)))
          this.typeInfo = (IRemotingTypeInfo) enumerator.Value;
        else if (enumerator.Name.Equals(nameof (envoyInfo)))
          this.envoyInfo = (IEnvoyInfo) enumerator.Value;
        else if (enumerator.Name.Equals(nameof (channelInfo)))
          this.channelInfo = (IChannelInfo) enumerator.Value;
        else if (enumerator.Name.Equals(nameof (objrefFlags)))
        {
          object obj = enumerator.Value;
          this.objrefFlags = !(obj.GetType() == typeof (string)) ? (int) obj : ((IConvertible) obj).ToInt32((IFormatProvider) null);
        }
        else if (enumerator.Name.Equals("fIsMarshalled"))
        {
          object obj = enumerator.Value;
          if ((!(obj.GetType() == typeof (string)) ? (int) obj : ((IConvertible) obj).ToInt32((IFormatProvider) null)) == 0)
            flag = true;
        }
        else if (enumerator.Name.Equals("url"))
          str = (string) enumerator.Value;
        else if (enumerator.Name.Equals("SrvIdentity"))
          this.SetServerIdentity((GCHandle) enumerator.Value);
        else if (enumerator.Name.Equals("DomainId"))
          this.SetDomainID((int) enumerator.Value);
      }
      if (!flag)
        this.objrefFlags |= 1;
      else
        this.objrefFlags &= -2;
      if (str == null)
        return;
      this.uri = str;
      this.objrefFlags |= 4;
    }

    [SecurityCritical]
    internal bool CanSmuggle()
    {
      if (this.GetType() != typeof (ObjRef) || this.IsObjRefLite())
        return false;
      Type type1 = (Type) null;
      if (this.typeInfo != null)
        type1 = this.typeInfo.GetType();
      Type type2 = (Type) null;
      if (this.channelInfo != null)
        type2 = this.channelInfo.GetType();
      if (!(type1 == (Type) null) && !(type1 == typeof (System.Runtime.Remoting.TypeInfo)) && !(type1 == typeof (DynamicTypeInfo)) || (this.envoyInfo != null || !(type2 == (Type) null) && !(type2 == typeof (System.Runtime.Remoting.ChannelInfo))))
        return false;
      if (this.channelInfo != null)
      {
        foreach (object obj in this.channelInfo.ChannelData)
        {
          if (!(obj is CrossAppDomainData))
            return false;
        }
      }
      return true;
    }

    [SecurityCritical]
    internal ObjRef CreateSmuggleableCopy()
    {
      return new ObjRef(this);
    }

    /// <summary>
    ///   Заполняет указанный <see cref="T:System.Runtime.Serialization.SerializationInfo" /> данными, необходимыми для сериализации текущего <see cref="T:System.Runtime.Remoting.ObjRef" /> экземпляра.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" /> для заполнения данными.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении сериализации.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения модуля форматирования сериализации.
    /// </exception>
    [SecurityCritical]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      info.SetType(ObjRef.orType);
      if (!this.IsObjRefLite())
      {
        info.AddValue("uri", (object) this.uri, typeof (string));
        info.AddValue("objrefFlags", this.objrefFlags);
        info.AddValue("typeInfo", (object) this.typeInfo, typeof (IRemotingTypeInfo));
        info.AddValue("envoyInfo", (object) this.envoyInfo, typeof (IEnvoyInfo));
        info.AddValue("channelInfo", (object) this.GetChannelInfoHelper(), typeof (IChannelInfo));
      }
      else
        info.AddValue("url", (object) this.uri, typeof (string));
    }

    [SecurityCritical]
    private IChannelInfo GetChannelInfoHelper()
    {
      System.Runtime.Remoting.ChannelInfo channelInfo1 = this.channelInfo as System.Runtime.Remoting.ChannelInfo;
      if (channelInfo1 == null)
        return this.channelInfo;
      object[] channelData = channelInfo1.ChannelData;
      if (channelData == null)
        return (IChannelInfo) channelInfo1;
      string[] data = (string[]) CallContext.GetData("__bashChannelUrl");
      if (data == null)
        return (IChannelInfo) channelInfo1;
      string str1 = data[0];
      string str2 = data[1];
      System.Runtime.Remoting.ChannelInfo channelInfo2 = new System.Runtime.Remoting.ChannelInfo();
      channelInfo2.ChannelData = new object[channelData.Length];
      for (int index = 0; index < channelData.Length; ++index)
      {
        channelInfo2.ChannelData[index] = channelData[index];
        ChannelDataStore channelDataStore1 = channelInfo2.ChannelData[index] as ChannelDataStore;
        if (channelDataStore1 != null)
        {
          string[] channelUris = channelDataStore1.ChannelUris;
          if (channelUris != null && channelUris.Length == 1 && channelUris[0].Equals(str1))
          {
            ChannelDataStore channelDataStore2 = channelDataStore1.InternalShallowCopy();
            channelDataStore2.ChannelUris = new string[1];
            channelDataStore2.ChannelUris[0] = str2;
            channelInfo2.ChannelData[index] = (object) channelDataStore2;
          }
        }
      }
      return (IChannelInfo) channelInfo2;
    }

    /// <summary>
    ///   Возвращает или задает URI указанного экземпляра объекта.
    /// </summary>
    /// <returns>URI указанного экземпляра объекта.</returns>
    public virtual string URI
    {
      get
      {
        return this.uri;
      }
      set
      {
        this.uri = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Runtime.Remoting.IRemotingTypeInfo" /> для объекта, <see cref="T:System.Runtime.Remoting.ObjRef" /> Описание.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.Remoting.IRemotingTypeInfo" /> Для объекта, <see cref="T:System.Runtime.Remoting.ObjRef" /> Описание.
    /// </returns>
    public virtual IRemotingTypeInfo TypeInfo
    {
      get
      {
        return this.typeInfo;
      }
      set
      {
        this.typeInfo = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Runtime.Remoting.IEnvoyInfo" /> для объекта <see cref="T:System.Runtime.Remoting.ObjRef" />.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.Remoting.IEnvoyInfo" /> Интерфейс для <see cref="T:System.Runtime.Remoting.ObjRef" />.
    /// </returns>
    public virtual IEnvoyInfo EnvoyInfo
    {
      get
      {
        return this.envoyInfo;
      }
      set
      {
        this.envoyInfo = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Runtime.Remoting.IChannelInfo" /> для объекта <see cref="T:System.Runtime.Remoting.ObjRef" />.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.Remoting.IChannelInfo" /> Интерфейс для <see cref="T:System.Runtime.Remoting.ObjRef" />.
    /// </returns>
    public virtual IChannelInfo ChannelInfo
    {
      [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)] get
      {
        return this.channelInfo;
      }
      set
      {
        this.channelInfo = value;
      }
    }

    /// <summary>
    ///   Возвращает ссылку на удаленный объект, который <see cref="T:System.Runtime.Remoting.ObjRef" /> Описание.
    /// </summary>
    /// <param name="context">
    ///   Контекст, где находится текущий объект.
    /// </param>
    /// <returns>
    ///   Ссылка на удаленный объект, <see cref="T:System.Runtime.Remoting.ObjRef" /> Описание.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения модуля форматирования сериализации.
    /// </exception>
    [SecurityCritical]
    public virtual object GetRealObject(StreamingContext context)
    {
      return this.GetRealObjectHelper();
    }

    [SecurityCritical]
    internal object GetRealObjectHelper()
    {
      if (!this.IsMarshaledObject())
        return (object) this;
      if (this.IsObjRefLite())
      {
        int num = this.uri.IndexOf(RemotingConfiguration.ApplicationId);
        if (num > 0)
          this.uri = this.uri.Substring(num - 1);
      }
      return this.GetCustomMarshaledCOMObject(RemotingServices.Unmarshal(this, !(this.GetType() == typeof (ObjRef))));
    }

    [SecurityCritical]
    private object GetCustomMarshaledCOMObject(object ret)
    {
      if (this.TypeInfo is DynamicTypeInfo)
      {
        IntPtr pUnk = IntPtr.Zero;
        if (this.IsFromThisProcess())
        {
          if (!this.IsFromThisAppDomain())
          {
            try
            {
              bool fIsURTAggregated;
              pUnk = ((__ComObject) ret).GetIUnknown(out fIsURTAggregated);
              if (pUnk != IntPtr.Zero)
              {
                if (!fIsURTAggregated)
                {
                  string typeName1 = this.TypeInfo.TypeName;
                  string typeName2 = (string) null;
                  string assemName = (string) null;
                  System.Runtime.Remoting.TypeInfo.ParseTypeAndAssembly(typeName1, out typeName2, out assemName);
                  Assembly assembly = FormatterServices.LoadAssemblyFromStringNoThrow(assemName);
                  if (assembly == (Assembly) null)
                    throw new RemotingException(Environment.GetResourceString("Serialization_AssemblyNotFound", (object) assemName));
                  Type t = assembly.GetType(typeName2, false, false);
                  if (t != (Type) null && !t.IsVisible)
                    t = (Type) null;
                  object objectForIunknown = Marshal.GetTypedObjectForIUnknown(pUnk, t);
                  if (objectForIunknown != null)
                    ret = objectForIunknown;
                }
              }
            }
            finally
            {
              if (pUnk != IntPtr.Zero)
                Marshal.Release(pUnk);
            }
          }
        }
      }
      return ret;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.ObjRef" /> со значениями по умолчанию.
    /// </summary>
    public ObjRef()
    {
      this.objrefFlags = 0;
    }

    internal bool IsMarshaledObject()
    {
      return (this.objrefFlags & 1) == 1;
    }

    internal void SetMarshaledObject()
    {
      this.objrefFlags |= 1;
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal bool IsWellKnown()
    {
      return (this.objrefFlags & 2) == 2;
    }

    internal void SetWellKnown()
    {
      this.objrefFlags |= 2;
    }

    internal bool HasProxyAttribute()
    {
      return (this.objrefFlags & 8) == 8;
    }

    internal void SetHasProxyAttribute()
    {
      this.objrefFlags |= 8;
    }

    internal bool IsObjRefLite()
    {
      return (this.objrefFlags & 4) == 4;
    }

    internal void SetObjRefLite()
    {
      this.objrefFlags |= 4;
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    private CrossAppDomainData GetAppDomainChannelData()
    {
      int index = 0;
      for (; index < this.ChannelInfo.ChannelData.Length; ++index)
      {
        CrossAppDomainData crossAppDomainData = this.ChannelInfo.ChannelData[index] as CrossAppDomainData;
        if (crossAppDomainData != null)
          return crossAppDomainData;
      }
      return (CrossAppDomainData) null;
    }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, является ли текущий <see cref="T:System.Runtime.Remoting.ObjRef" /> экземпляр ссылается на объект, расположенный в текущем процессе.
    /// </summary>
    /// <returns>
    ///   Логическое значение, указывающее, является ли текущий <see cref="T:System.Runtime.Remoting.ObjRef" /> экземпляр ссылается на объект, расположенный в текущем процессе.
    /// </returns>
    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public bool IsFromThisProcess()
    {
      if (this.IsWellKnown())
        return false;
      CrossAppDomainData domainChannelData = this.GetAppDomainChannelData();
      if (domainChannelData != null)
        return domainChannelData.IsFromThisProcess();
      return false;
    }

    /// <summary>
    ///   Возвращает логическое значение, указывающее, является ли текущий <see cref="T:System.Runtime.Remoting.ObjRef" /> экземпляр ссылается на объект, расположенный в текущем <see cref="T:System.AppDomain" />.
    /// </summary>
    /// <returns>
    ///   Логическое значение, указывающее, является ли текущий <see cref="T:System.Runtime.Remoting.ObjRef" /> экземпляр ссылается на объект, расположенный в текущем <see cref="T:System.AppDomain" />.
    /// </returns>
    [SecurityCritical]
    public bool IsFromThisAppDomain()
    {
      CrossAppDomainData domainChannelData = this.GetAppDomainChannelData();
      if (domainChannelData != null)
        return domainChannelData.IsFromThisAppDomain();
      return false;
    }

    [SecurityCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    internal int GetServerDomainId()
    {
      if (!this.IsFromThisProcess())
        return 0;
      return this.GetAppDomainChannelData().DomainID;
    }

    [SecurityCritical]
    internal IntPtr GetServerContext(out int domainId)
    {
      IntPtr num = IntPtr.Zero;
      domainId = 0;
      if (this.IsFromThisProcess())
      {
        CrossAppDomainData domainChannelData = this.GetAppDomainChannelData();
        domainId = domainChannelData.DomainID;
        if (AppDomain.IsDomainIdValid(domainChannelData.DomainID))
          num = domainChannelData.ContextID;
      }
      return num;
    }

    [SecurityCritical]
    internal void Init(object o, Identity idObj, RuntimeType requestedType)
    {
      this.uri = idObj.URI;
      MarshalByRefObject tpOrObject = idObj.TPOrObject;
      RuntimeType runtimeType1 = RemotingServices.IsTransparentProxy((object) tpOrObject) ? (RuntimeType) RemotingServices.GetRealProxy((object) tpOrObject).GetProxiedType() : (RuntimeType) tpOrObject.GetType();
      RuntimeType runtimeType2 = (RuntimeType) null == requestedType ? runtimeType1 : requestedType;
      if ((RuntimeType) null != requestedType && !requestedType.IsAssignableFrom((System.Reflection.TypeInfo) runtimeType1) && !typeof (IMessageSink).IsAssignableFrom((Type) runtimeType1))
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_InvalidRequestedType"), (object) requestedType.ToString()));
      this.TypeInfo = !runtimeType1.IsCOMObject ? (IRemotingTypeInfo) InternalRemotingServices.GetReflectionCachedData(runtimeType2).TypeInfo : (IRemotingTypeInfo) new DynamicTypeInfo(runtimeType2);
      if (!idObj.IsWellKnown())
      {
        this.EnvoyInfo = System.Runtime.Remoting.EnvoyInfo.CreateEnvoyInfo(idObj as ServerIdentity);
        IChannelInfo channelInfo = (IChannelInfo) new System.Runtime.Remoting.ChannelInfo();
        if (o is AppDomain)
        {
          object[] channelData = channelInfo.ChannelData;
          int length = channelData.Length;
          object[] objArray = new object[length];
          Array.Copy((Array) channelData, (Array) objArray, length);
          for (int index = 0; index < length; ++index)
          {
            if (!(objArray[index] is CrossAppDomainData))
              objArray[index] = (object) null;
          }
          channelInfo.ChannelData = objArray;
        }
        this.ChannelInfo = channelInfo;
        if (runtimeType1.HasProxyAttribute)
          this.SetHasProxyAttribute();
      }
      else
        this.SetWellKnown();
      if (!ObjRef.ShouldUseUrlObjRef())
        return;
      if (this.IsWellKnown())
      {
        this.SetObjRefLite();
      }
      else
      {
        string httpUrlForObject = ChannelServices.FindFirstHttpUrlForObject(this.URI);
        if (httpUrlForObject == null)
          return;
        this.URI = httpUrlForObject;
        this.SetObjRefLite();
      }
    }

    internal static bool ShouldUseUrlObjRef()
    {
      return RemotingConfigHandler.UrlObjRefMode;
    }

    [SecurityCritical]
    internal static bool IsWellFormed(ObjRef objectRef)
    {
      bool flag = true;
      if (objectRef == null || objectRef.URI == null || !objectRef.IsWellKnown() && !objectRef.IsObjRefLite() && (!(objectRef.GetType() != ObjRef.orType) && objectRef.ChannelInfo == null))
        flag = false;
      return flag;
    }
  }
}

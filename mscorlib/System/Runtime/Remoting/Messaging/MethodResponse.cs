// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.MethodResponse
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>
  ///   Реализует <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> интерфейс, чтобы создать сообщение, которое выступает в качестве ответа метода удаленного объекта.
  /// </summary>
  [SecurityCritical]
  [CLSCompliant(false)]
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class MethodResponse : IMethodReturnMessage, IMethodMessage, IMessage, ISerializable, ISerializationRootObject, IInternalMessage
  {
    private MethodBase MI;
    private string methodName;
    private Type[] methodSignature;
    private string uri;
    private string typeName;
    private object retVal;
    private Exception fault;
    private object[] outArgs;
    private LogicalCallContext callContext;
    /// <summary>
    ///   Указывает <see cref="T:System.Collections.IDictionary" /> интерфейс, который представляет коллекцию свойств сообщения удаленного взаимодействия.
    /// </summary>
    protected IDictionary InternalProperties;
    /// <summary>
    ///   Указывает <see cref="T:System.Collections.IDictionary" /> интерфейс, который представляет коллекцию свойств сообщения удаленного взаимодействия.
    /// </summary>
    protected IDictionary ExternalProperties;
    private int argCount;
    private bool fSoap;
    private ArgMapper argMapper;
    private RemotingMethodCachedData _methodCache;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Messaging.MethodResponse" /> класс из массива заголовков удаленного взаимодействия и сообщение запроса.
    /// </summary>
    /// <param name="h1">
    ///   Массив заголовков удаленного взаимодействия, содержащий пары ключ значение.
    ///    Этот массив используется для инициализации <see cref="T:System.Runtime.Remoting.Messaging.MethodResponse" /> поля для заголовков, которые принадлежат к пространству имен «http://schemas.microsoft.com/clr/soap/messageProperties».
    /// </param>
    /// <param name="mcm">
    ///   Сообщение запроса, который действует как вызов метода удаленного объекта.
    /// </param>
    [SecurityCritical]
    public MethodResponse(Header[] h1, IMethodCallMessage mcm)
    {
      if (mcm == null)
        throw new ArgumentNullException(nameof (mcm));
      Message message = mcm as Message;
      this.MI = message == null ? mcm.MethodBase : message.GetMethodBase();
      if (this.MI == (MethodBase) null)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MethodMissing"), (object) mcm.MethodName, (object) mcm.TypeName));
      this._methodCache = InternalRemotingServices.GetReflectionCachedData(this.MI);
      this.argCount = this._methodCache.Parameters.Length;
      this.fSoap = true;
      this.FillHeaders(h1);
    }

    [SecurityCritical]
    internal MethodResponse(IMethodCallMessage msg, SmuggledMethodReturnMessage smuggledMrm, ArrayList deserializedArgs)
    {
      this.MI = msg.MethodBase;
      this._methodCache = InternalRemotingServices.GetReflectionCachedData(this.MI);
      this.methodName = msg.MethodName;
      this.uri = msg.Uri;
      this.typeName = msg.TypeName;
      if (this._methodCache.IsOverloaded())
        this.methodSignature = (Type[]) msg.MethodSignature;
      this.retVal = smuggledMrm.GetReturnValue(deserializedArgs);
      this.outArgs = smuggledMrm.GetArgs(deserializedArgs);
      this.fault = smuggledMrm.GetException(deserializedArgs);
      this.callContext = smuggledMrm.GetCallContext(deserializedArgs);
      if (smuggledMrm.MessagePropertyCount > 0)
        smuggledMrm.PopulateMessageProperties(this.Properties, deserializedArgs);
      this.argCount = this._methodCache.Parameters.Length;
      this.fSoap = false;
    }

    [SecurityCritical]
    internal MethodResponse(IMethodCallMessage msg, object handlerObject, BinaryMethodReturnMessage smuggledMrm)
    {
      if (msg != null)
      {
        this.MI = msg.MethodBase;
        this._methodCache = InternalRemotingServices.GetReflectionCachedData(this.MI);
        this.methodName = msg.MethodName;
        this.uri = msg.Uri;
        this.typeName = msg.TypeName;
        if (this._methodCache.IsOverloaded())
          this.methodSignature = (Type[]) msg.MethodSignature;
        this.argCount = this._methodCache.Parameters.Length;
      }
      this.retVal = smuggledMrm.ReturnValue;
      this.outArgs = smuggledMrm.Args;
      this.fault = smuggledMrm.Exception;
      this.callContext = smuggledMrm.LogicalCallContext;
      if (smuggledMrm.HasProperties)
        smuggledMrm.PopulateMessageProperties(this.Properties);
      this.fSoap = false;
    }

    [SecurityCritical]
    internal MethodResponse(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      this.SetObjectData(info, context);
    }

    /// <summary>
    ///   Инициализирует внутренний обработчик сериализации из массива заголовков удаленного взаимодействия, которые применяются к методу.
    /// </summary>
    /// <param name="h">
    ///   Массив заголовков удаленного взаимодействия, содержащих пары "ключ значение".
    ///    Этот массив используется для инициализации <see cref="T:System.Runtime.Remoting.Messaging.MethodResponse" /> поля для заголовков, которые принадлежат к пространству имен «http://schemas.microsoft.com/clr/soap/messageProperties».
    /// </param>
    /// <returns>Внутренний обработчик сериализации.</returns>
    [SecurityCritical]
    public virtual object HeaderHandler(Header[] h)
    {
      SerializationMonkey uninitializedObject = (SerializationMonkey) FormatterServices.GetUninitializedObject(typeof (SerializationMonkey));
      Header[] h1;
      if (h != null && h.Length != 0 && h[0].Name == "__methodName")
      {
        if (h.Length > 1)
        {
          h1 = new Header[h.Length - 1];
          Array.Copy((Array) h, 1, (Array) h1, 0, h.Length - 1);
        }
        else
          h1 = (Header[]) null;
      }
      else
        h1 = h;
      Type type = (Type) null;
      MethodInfo mi = this.MI as MethodInfo;
      if (mi != (MethodInfo) null)
        type = mi.ReturnType;
      ParameterInfo[] parameters = this._methodCache.Parameters;
      int length = this._methodCache.MarshalResponseArgMap.Length;
      if (!(type == (Type) null) && !(type == typeof (void)))
        ++length;
      Type[] typeArray = new Type[length];
      string[] strArray = new string[length];
      int index = 0;
      if (!(type == (Type) null) && !(type == typeof (void)))
        typeArray[index++] = type;
      foreach (int marshalResponseArg in this._methodCache.MarshalResponseArgMap)
      {
        strArray[index] = parameters[marshalResponseArg].Name;
        typeArray[index++] = !parameters[marshalResponseArg].ParameterType.IsByRef ? parameters[marshalResponseArg].ParameterType : parameters[marshalResponseArg].ParameterType.GetElementType();
      }
      uninitializedObject.FieldTypes = typeArray;
      uninitializedObject.FieldNames = strArray;
      this.FillHeaders(h1, true);
      uninitializedObject._obj = (ISerializationRootObject) this;
      return (object) uninitializedObject;
    }

    /// <summary>
    ///   Задает информацию для метода из настроек сериализации.
    /// </summary>
    /// <param name="info">
    ///   Данные для сериализации или десериализации удаленного объекта.
    /// </param>
    /// <param name="ctx">
    ///   Контекст определенного сериализуемого потока.
    /// </param>
    [SecurityCritical]
    public void RootSetObjectData(SerializationInfo info, StreamingContext ctx)
    {
      this.SetObjectData(info, ctx);
    }

    [SecurityCritical]
    internal void SetObjectData(SerializationInfo info, StreamingContext ctx)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      if (this.fSoap)
      {
        this.SetObjectFromSoapData(info);
      }
      else
      {
        SerializationInfoEnumerator enumerator = info.GetEnumerator();
        bool flag1 = false;
        bool flag2 = false;
        while (enumerator.MoveNext())
        {
          if (enumerator.Name.Equals("__return"))
          {
            flag1 = true;
            break;
          }
          if (enumerator.Name.Equals("__fault"))
          {
            flag2 = true;
            this.fault = (Exception) enumerator.Value;
            break;
          }
          this.FillHeader(enumerator.Name, enumerator.Value);
        }
        if (flag2 & flag1)
          throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadSerialization"));
      }
    }

    /// <summary>
    ///   <see cref="M:System.Runtime.Remoting.Messaging.MethodResponse.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> Метод не реализован.
    /// </summary>
    /// <param name="info">
    ///   Данные для сериализации или десериализации удаленного объекта.
    /// </param>
    /// <param name="context">
    ///   Контекст определенного сериализуемого потока.
    /// </param>
    [SecurityCritical]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
    }

    internal void SetObjectFromSoapData(SerializationInfo info)
    {
      Hashtable keyToNamespaceTable = (Hashtable) info.GetValue("__keyToNamespaceTable", typeof (Hashtable));
      ArrayList arrayList = (ArrayList) info.GetValue("__paramNameList", typeof (ArrayList));
      SoapFault soapFault = (SoapFault) info.GetValue("__fault", typeof (SoapFault));
      if (soapFault != null)
      {
        ServerFault detail = soapFault.Detail as ServerFault;
        if (detail != null)
        {
          if (detail.Exception != null)
          {
            this.fault = detail.Exception;
          }
          else
          {
            Type type = Type.GetType(detail.ExceptionType, false, false);
            if (type == (Type) null)
            {
              StringBuilder stringBuilder = new StringBuilder();
              stringBuilder.Append("\nException Type: ");
              stringBuilder.Append(detail.ExceptionType);
              stringBuilder.Append("\n");
              stringBuilder.Append("Exception Message: ");
              stringBuilder.Append(detail.ExceptionMessage);
              stringBuilder.Append("\n");
              stringBuilder.Append(detail.StackTrace);
              this.fault = (Exception) new ServerException(stringBuilder.ToString());
            }
            else
            {
              object[] args = new object[1]
              {
                (object) detail.ExceptionMessage
              };
              this.fault = (Exception) Activator.CreateInstance(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, (Binder) null, args, (CultureInfo) null, (object[]) null);
            }
          }
        }
        else if (soapFault.Detail != null && soapFault.Detail.GetType() == typeof (string) && ((string) soapFault.Detail).Length != 0)
          this.fault = (Exception) new ServerException((string) soapFault.Detail);
        else
          this.fault = (Exception) new ServerException(soapFault.FaultString);
      }
      else
      {
        MethodInfo mi = this.MI as MethodInfo;
        int num = 0;
        if (mi != (MethodInfo) null)
        {
          Type returnType = mi.ReturnType;
          if (returnType != typeof (void))
          {
            ++num;
            object obj = info.GetValue((string) arrayList[0], typeof (object));
            this.retVal = !(obj is string) ? obj : Message.SoapCoerceArg(obj, returnType, keyToNamespaceTable);
          }
        }
        ParameterInfo[] parameters = this._methodCache.Parameters;
        object obj1 = this.InternalProperties == null ? (object) null : this.InternalProperties[(object) "__UnorderedParams"];
        if (obj1 != null && obj1 is bool && (bool) obj1)
        {
          for (int index1 = num; index1 < arrayList.Count; ++index1)
          {
            string name = (string) arrayList[index1];
            int index2 = -1;
            for (int index3 = 0; index3 < parameters.Length; ++index3)
            {
              if (name.Equals(parameters[index3].Name))
                index2 = parameters[index3].Position;
            }
            if (index2 == -1)
            {
              if (!name.StartsWith("__param", StringComparison.Ordinal))
                throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadSerialization"));
              index2 = int.Parse(name.Substring(7), (IFormatProvider) CultureInfo.InvariantCulture);
            }
            if (index2 >= this.argCount)
              throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadSerialization"));
            if (this.outArgs == null)
              this.outArgs = new object[this.argCount];
            this.outArgs[index2] = Message.SoapCoerceArg(info.GetValue(name, typeof (object)), parameters[index2].ParameterType, keyToNamespaceTable);
          }
        }
        else
        {
          if (this.argMapper == null)
            this.argMapper = new ArgMapper((IMethodMessage) this, true);
          for (int index1 = num; index1 < arrayList.Count; ++index1)
          {
            string name = (string) arrayList[index1];
            if (this.outArgs == null)
              this.outArgs = new object[this.argCount];
            int index2 = this.argMapper.Map[index1 - num];
            this.outArgs[index2] = Message.SoapCoerceArg(info.GetValue(name, typeof (object)), parameters[index2].ParameterType, keyToNamespaceTable);
          }
        }
      }
    }

    [SecurityCritical]
    internal LogicalCallContext GetLogicalCallContext()
    {
      if (this.callContext == null)
        this.callContext = new LogicalCallContext();
      return this.callContext;
    }

    internal LogicalCallContext SetLogicalCallContext(LogicalCallContext ctx)
    {
      LogicalCallContext callContext = this.callContext;
      this.callContext = ctx;
      return callContext;
    }

    /// <summary>
    ///   Возвращает универсальный код ресурса (URI) удаленного объекта, для которого осуществляется вызов метода.
    /// </summary>
    /// <returns>URI удаленного объекта.</returns>
    public string Uri
    {
      [SecurityCritical] get
      {
        return this.uri;
      }
      set
      {
        this.uri = value;
      }
    }

    /// <summary>Возвращает имя вызванного метода.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> содержащая имя вызываемого метода.
    /// </returns>
    public string MethodName
    {
      [SecurityCritical] get
      {
        return this.methodName;
      }
    }

    /// <summary>
    ///   Возвращает полное имя типа удаленного объекта, для которого осуществляется вызов метода.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> содержащий полное имя типа удаленного объекта, для которого осуществляется вызов метода.
    /// </returns>
    public string TypeName
    {
      [SecurityCritical] get
      {
        return this.typeName;
      }
    }

    /// <summary>Возвращает объект, содержащий подпись метода.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.Object" /> содержащий сигнатуру метода.
    /// </returns>
    public object MethodSignature
    {
      [SecurityCritical] get
      {
        return (object) this.methodSignature;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Reflection.MethodBase" /> вызванного метода.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Reflection.MethodBase" /> Вызванного метода.
    /// </returns>
    public MethodBase MethodBase
    {
      [SecurityCritical] get
      {
        return this.MI;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, принимает ли метод переменное число аргументов.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если метод может принимать переменное число аргументов; в противном случае — <see langword="false" />.
    /// </returns>
    public bool HasVarArgs
    {
      [SecurityCritical] get
      {
        return false;
      }
    }

    /// <summary>Возвращает число аргументов, передаваемых в метод.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.Int32" /> представляющий количество аргументов, передаваемых в метод.
    /// </returns>
    public int ArgCount
    {
      [SecurityCritical] get
      {
        if (this.outArgs == null)
          return 0;
        return this.outArgs.Length;
      }
    }

    /// <summary>
    ///   Возвращает аргумент метода в виде объекта по указанному индексу.
    /// </summary>
    /// <param name="argNum">Индекс запрошенного аргумента.</param>
    /// <returns>Аргумент метода в виде объекта.</returns>
    [SecurityCritical]
    public object GetArg(int argNum)
    {
      return this.outArgs[argNum];
    }

    /// <summary>
    ///   Возвращает имя аргумента метода по указанному индексу.
    /// </summary>
    /// <param name="index">Индекс запрошенного аргумента.</param>
    /// <returns>Имя аргумента метода.</returns>
    [SecurityCritical]
    public string GetArgName(int index)
    {
      if (!(this.MI != (MethodBase) null))
        return "__param" + (object) index;
      RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(this.MI);
      ParameterInfo[] parameters = reflectionCachedData.Parameters;
      if (index < 0 || index >= parameters.Length)
        throw new ArgumentOutOfRangeException(nameof (index));
      return reflectionCachedData.Parameters[index].Name;
    }

    /// <summary>Возвращает массив аргументов, передаваемых в метод.</summary>
    /// <returns>
    ///   Массив объектов типа <see cref="T:System.Object" /> представляющий аргументы, переданные в метод.
    /// </returns>
    public object[] Args
    {
      [SecurityCritical] get
      {
        return this.outArgs;
      }
    }

    /// <summary>
    ///   Возвращает число аргументов в вызове метода помечен как <see langword="ref" /> или <see langword="out" /> Параметры.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Int32" /> что представляет число аргументов в вызове метода, отмеченные как <see langword="ref" /> или <see langword="out" /> Параметры.
    /// </returns>
    public int OutArgCount
    {
      [SecurityCritical] get
      {
        if (this.argMapper == null)
          this.argMapper = new ArgMapper((IMethodMessage) this, true);
        return this.argMapper.ArgCount;
      }
    }

    /// <summary>
    ///   Возвращает указанный аргумент, отмеченный как <see langword="ref" /> параметр или <see langword="out" /> параметра.
    /// </summary>
    /// <param name="argNum">Индекс запрошенного аргумента.</param>
    /// <returns>
    ///   Указанный аргумент, помеченный как <see langword="ref" /> параметр или <see langword="out" /> параметра.
    /// </returns>
    [SecurityCritical]
    public object GetOutArg(int argNum)
    {
      if (this.argMapper == null)
        this.argMapper = new ArgMapper((IMethodMessage) this, true);
      return this.argMapper.GetArg(argNum);
    }

    /// <summary>
    ///   Возвращает имя указанного аргумента, помеченного как <see langword="ref" /> параметр или <see langword="out" /> параметра.
    /// </summary>
    /// <param name="index">Индекс запрошенного аргумента.</param>
    /// <returns>
    ///   Имя аргумента или <see langword="null" /> если текущий метод не реализован.
    /// </returns>
    [SecurityCritical]
    public string GetOutArgName(int index)
    {
      if (this.argMapper == null)
        this.argMapper = new ArgMapper((IMethodMessage) this, true);
      return this.argMapper.GetArgName(index);
    }

    /// <summary>
    ///   Возвращает массив аргументов в вызове метода, которые помечены как <see langword="ref" /> или <see langword="out" /> Параметры.
    /// </summary>
    /// <returns>
    ///   Массив объектов типа <see cref="T:System.Object" /> представляющий аргументы в вызове метода, которые помечены как <see langword="ref" /> или <see langword="out" /> Параметры.
    /// </returns>
    public object[] OutArgs
    {
      [SecurityCritical] get
      {
        if (this.argMapper == null)
          this.argMapper = new ArgMapper((IMethodMessage) this, true);
        return this.argMapper.Args;
      }
    }

    /// <summary>
    ///   Возвращает исключение, инициированное во время вызова метода, или <see langword="null" /> если метод не выдал исключение.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Exception" /> Исключение при вызове метода или <see langword="null" /> Если метод не выдал исключение.
    /// </returns>
    public Exception Exception
    {
      [SecurityCritical] get
      {
        return this.fault;
      }
    }

    /// <summary>Получает возвращаемое значение вызова метода.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.Object" /> представляющий возвращаемое значение вызова метода.
    /// </returns>
    public object ReturnValue
    {
      [SecurityCritical] get
      {
        return this.retVal;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Collections.IDictionary" /> интерфейс, который представляет коллекцию свойств сообщения удаленного взаимодействия.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Collections.IDictionary" /> Интерфейс, который представляет коллекцию свойств сообщения удаленного взаимодействия.
    /// </returns>
    public virtual IDictionary Properties
    {
      [SecurityCritical] get
      {
        lock (this)
        {
          if (this.InternalProperties == null)
            this.InternalProperties = (IDictionary) new Hashtable();
          if (this.ExternalProperties == null)
            this.ExternalProperties = (IDictionary) new MRMDictionary((IMethodReturnMessage) this, this.InternalProperties);
          return this.ExternalProperties;
        }
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> для текущего вызова метода.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.Remoting.Messaging.LogicalCallContext" /> Для текущего вызова метода.
    /// </returns>
    public LogicalCallContext LogicalCallContext
    {
      [SecurityCritical] get
      {
        return this.GetLogicalCallContext();
      }
    }

    [SecurityCritical]
    internal void FillHeaders(Header[] h)
    {
      this.FillHeaders(h, false);
    }

    [SecurityCritical]
    private void FillHeaders(Header[] h, bool bFromHeaderHandler)
    {
      if (h == null)
        return;
      if (bFromHeaderHandler && this.fSoap)
      {
        for (int index = 0; index < h.Length; ++index)
        {
          Header header = h[index];
          if (header.HeaderNamespace == "http://schemas.microsoft.com/clr/soap/messageProperties")
            this.FillHeader(header.Name, header.Value);
          else
            this.FillHeader(LogicalCallContext.GetPropertyKeyForHeader(header), (object) header);
        }
      }
      else
      {
        for (int index = 0; index < h.Length; ++index)
          this.FillHeader(h[index].Name, h[index].Value);
      }
    }

    [SecurityCritical]
    internal void FillHeader(string name, object value)
    {
      if (name.Equals("__MethodName"))
        this.methodName = (string) value;
      else if (name.Equals("__Uri"))
        this.uri = (string) value;
      else if (name.Equals("__MethodSignature"))
        this.methodSignature = (Type[]) value;
      else if (name.Equals("__TypeName"))
        this.typeName = (string) value;
      else if (name.Equals("__OutArgs"))
        this.outArgs = (object[]) value;
      else if (name.Equals("__CallContext"))
      {
        if (value is string)
        {
          this.callContext = new LogicalCallContext();
          this.callContext.RemotingData.LogicalCallID = (string) value;
        }
        else
          this.callContext = (LogicalCallContext) value;
      }
      else if (name.Equals("__Return"))
      {
        this.retVal = value;
      }
      else
      {
        if (this.InternalProperties == null)
          this.InternalProperties = (IDictionary) new Hashtable();
        this.InternalProperties[(object) name] = value;
      }
    }

    ServerIdentity IInternalMessage.ServerIdentityObject
    {
      [SecurityCritical] get
      {
        return (ServerIdentity) null;
      }
      [SecurityCritical] set
      {
      }
    }

    Identity IInternalMessage.IdentityObject
    {
      [SecurityCritical] get
      {
        return (Identity) null;
      }
      [SecurityCritical] set
      {
      }
    }

    [SecurityCritical]
    void IInternalMessage.SetURI(string val)
    {
      this.uri = val;
    }

    [SecurityCritical]
    void IInternalMessage.SetCallContext(LogicalCallContext newCallContext)
    {
      this.callContext = newCallContext;
    }

    [SecurityCritical]
    bool IInternalMessage.HasProperties()
    {
      if (this.ExternalProperties == null)
        return this.InternalProperties != null;
      return true;
    }
  }
}

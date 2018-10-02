// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.MethodCall
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>
  ///   Реализует <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> интерфейс для создания сообщения запроса, который действует как вызов метода удаленного объекта.
  /// </summary>
  [SecurityCritical]
  [CLSCompliant(false)]
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class MethodCall : IMethodCallMessage, IMethodMessage, IMessage, ISerializable, IInternalMessage, ISerializationRootObject
  {
    private const BindingFlags LookupAll = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
    private const BindingFlags LookupPublic = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
    private string uri;
    private string methodName;
    private MethodBase MI;
    private string typeName;
    private object[] args;
    private Type[] instArgs;
    private LogicalCallContext callContext;
    private Type[] methodSignature;
    /// <summary>
    ///   <see cref="T:System.Collections.IDictionary" /> Интерфейс, который представляет коллекцию свойств сообщения удаленного взаимодействия.
    /// </summary>
    protected IDictionary ExternalProperties;
    /// <summary>
    ///   <see cref="T:System.Collections.IDictionary" /> Интерфейс, который представляет коллекцию свойств сообщения удаленного взаимодействия.
    /// </summary>
    protected IDictionary InternalProperties;
    private ServerIdentity srvID;
    private Identity identity;
    private bool fSoap;
    private bool fVarArgs;
    private ArgMapper argMapper;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" /> класс из массива заголовков удаленного взаимодействия.
    /// </summary>
    /// <param name="h1">
    ///   Массив заголовков удаленного взаимодействия, содержащий пары ключ значение.
    ///    Этот массив используется для инициализации <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" /> поля для заголовков, которые принадлежат к пространству имен «http://schemas.microsoft.com/clr/soap/messageProperties».
    /// </param>
    [SecurityCritical]
    public MethodCall(Header[] h1)
    {
      this.Init();
      this.fSoap = true;
      this.FillHeaders(h1);
      this.ResolveMethod();
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" /> класса путем копирования существующего сообщения.
    /// </summary>
    /// <param name="msg">Сообщение удаленного взаимодействия.</param>
    [SecurityCritical]
    public MethodCall(IMessage msg)
    {
      if (msg == null)
        throw new ArgumentNullException(nameof (msg));
      this.Init();
      IDictionaryEnumerator enumerator = msg.Properties.GetEnumerator();
      while (enumerator.MoveNext())
        this.FillHeader(enumerator.Key.ToString(), enumerator.Value);
      IMethodCallMessage methodCallMessage = msg as IMethodCallMessage;
      if (methodCallMessage != null)
        this.MI = methodCallMessage.MethodBase;
      this.ResolveMethod();
    }

    [SecurityCritical]
    internal MethodCall(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      this.Init();
      this.SetObjectData(info, context);
    }

    [SecurityCritical]
    internal MethodCall(SmuggledMethodCallMessage smuggledMsg, ArrayList deserializedArgs)
    {
      this.uri = smuggledMsg.Uri;
      this.typeName = smuggledMsg.TypeName;
      this.methodName = smuggledMsg.MethodName;
      this.methodSignature = (Type[]) smuggledMsg.GetMethodSignature(deserializedArgs);
      this.args = smuggledMsg.GetArgs(deserializedArgs);
      this.instArgs = smuggledMsg.GetInstantiation(deserializedArgs);
      this.callContext = smuggledMsg.GetCallContext(deserializedArgs);
      this.ResolveMethod();
      if (smuggledMsg.MessagePropertyCount <= 0)
        return;
      smuggledMsg.PopulateMessageProperties(this.Properties, deserializedArgs);
    }

    [SecurityCritical]
    internal MethodCall(object handlerObject, BinaryMethodCallMessage smuggledMsg)
    {
      if (handlerObject != null)
      {
        this.uri = handlerObject as string;
        if (this.uri == null)
        {
          MarshalByRefObject marshalByRefObject = handlerObject as MarshalByRefObject;
          if (marshalByRefObject != null)
          {
            bool fServer;
            this.srvID = MarshalByRefObject.GetIdentity(marshalByRefObject, out fServer) as ServerIdentity;
            this.uri = this.srvID.URI;
          }
        }
      }
      this.typeName = smuggledMsg.TypeName;
      this.methodName = smuggledMsg.MethodName;
      this.methodSignature = (Type[]) smuggledMsg.MethodSignature;
      this.args = smuggledMsg.Args;
      this.instArgs = smuggledMsg.InstantiationArgs;
      this.callContext = smuggledMsg.LogicalCallContext;
      this.ResolveMethod();
      if (!smuggledMsg.HasProperties)
        return;
      smuggledMsg.PopulateMessageProperties(this.Properties);
    }

    /// <summary>
    ///   Задает информацию для метода из настроек сериализации.
    /// </summary>
    /// <param name="info">
    ///   Данные для сериализации или десериализации удаленного объекта.
    /// </param>
    /// <param name="ctx">Контекст заданного потока сериализации.</param>
    [SecurityCritical]
    public void RootSetObjectData(SerializationInfo info, StreamingContext ctx)
    {
      this.SetObjectData(info, ctx);
    }

    [SecurityCritical]
    internal void SetObjectData(SerializationInfo info, StreamingContext context)
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
        while (enumerator.MoveNext())
          this.FillHeader(enumerator.Name, enumerator.Value);
        if (context.State != StreamingContextStates.Remoting || context.Context == null)
          return;
        Header[] context1 = context.Context as Header[];
        if (context1 == null)
          return;
        for (int index = 0; index < context1.Length; ++index)
          this.FillHeader(context1[index].Name, context1[index].Value);
      }
    }

    private static Type ResolveTypeRelativeTo(string typeName, int offset, int count, Type serverType)
    {
      Type baseTypes = MethodCall.ResolveTypeRelativeToBaseTypes(typeName, offset, count, serverType);
      if (baseTypes == (Type) null)
      {
        foreach (Type type in serverType.GetInterfaces())
        {
          string fullName = type.FullName;
          if (fullName.Length == count && string.CompareOrdinal(typeName, offset, fullName, 0, count) == 0)
            return type;
        }
      }
      return baseTypes;
    }

    private static Type ResolveTypeRelativeToBaseTypes(string typeName, int offset, int count, Type serverType)
    {
      if (typeName == null || serverType == (Type) null)
        return (Type) null;
      string fullName = serverType.FullName;
      if (fullName.Length == count && string.CompareOrdinal(typeName, offset, fullName, 0, count) == 0)
        return serverType;
      return MethodCall.ResolveTypeRelativeToBaseTypes(typeName, offset, count, serverType.BaseType);
    }

    internal Type ResolveType()
    {
      Type newType = (Type) null;
      if (this.srvID == null)
        this.srvID = IdentityHolder.CasualResolveIdentity(this.uri) as ServerIdentity;
      if (this.srvID != null)
      {
        Type lastCalledType = this.srvID.GetLastCalledType(this.typeName);
        if (lastCalledType != (Type) null)
          return lastCalledType;
        int num1 = 0;
        if (string.CompareOrdinal(this.typeName, 0, "clr:", 0, 4) == 0)
          num1 = 4;
        int num2 = this.typeName.IndexOf(',', num1);
        if (num2 == -1)
          num2 = this.typeName.Length;
        Type serverType = this.srvID.ServerType;
        newType = MethodCall.ResolveTypeRelativeTo(this.typeName, num1, num2 - num1, serverType);
      }
      if (newType == (Type) null)
        newType = RemotingServices.InternalGetTypeFromQualifiedTypeName(this.typeName);
      if (this.srvID != null)
        this.srvID.SetLastCalledType(this.typeName, newType);
      return newType;
    }

    /// <summary>
    ///   Задает информацию для метода из предварительно инициализированных удаленное взаимодействие свойств сообщения.
    /// </summary>
    [SecurityCritical]
    public void ResolveMethod()
    {
      this.ResolveMethod(true);
    }

    [SecurityCritical]
    internal void ResolveMethod(bool bThrowIfNotResolved)
    {
      if (!(this.MI == (MethodBase) null) || this.methodName == null)
        return;
      RuntimeType runtimeType = this.ResolveType() as RuntimeType;
      if (this.methodName.Equals(".ctor"))
        return;
      if (runtimeType == (RuntimeType) null)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), (object) this.typeName));
      if (this.methodSignature != null)
      {
        bool flag = false;
        int num = this.instArgs == null ? 0 : this.instArgs.Length;
        if (num == 0)
        {
          try
          {
            this.MI = (MethodBase) runtimeType.GetMethod(this.methodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, CallingConventions.Any, this.methodSignature, (ParameterModifier[]) null);
            flag = true;
          }
          catch (AmbiguousMatchException ex)
          {
          }
        }
        if (!flag)
        {
          MemberInfo[] members = runtimeType.FindMembers(MemberTypes.Method, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, Type.FilterName, (object) this.methodName);
          int length = 0;
          for (int index = 0; index < members.Length; ++index)
          {
            try
            {
              MethodInfo methodInfo = (MethodInfo) members[index];
              if ((methodInfo.IsGenericMethod ? methodInfo.GetGenericArguments().Length : 0) == num)
              {
                if (num > 0)
                  methodInfo = methodInfo.MakeGenericMethod(this.instArgs);
                members[length] = (MemberInfo) methodInfo;
                ++length;
              }
            }
            catch (ArgumentException ex)
            {
            }
            catch (VerificationException ex)
            {
            }
          }
          MethodInfo[] methodInfoArray = new MethodInfo[length];
          for (int index = 0; index < length; ++index)
            methodInfoArray[index] = (MethodInfo) members[index];
          this.MI = Type.DefaultBinder.SelectMethod(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, (MethodBase[]) methodInfoArray, this.methodSignature, (ParameterModifier[]) null);
        }
      }
      else
      {
        RemotingTypeCachedData remotingTypeCachedData = (RemotingTypeCachedData) null;
        if (this.instArgs == null)
        {
          remotingTypeCachedData = InternalRemotingServices.GetReflectionCachedData(runtimeType);
          this.MI = remotingTypeCachedData.GetLastCalledMethod(this.methodName);
          if (this.MI != (MethodBase) null)
            return;
        }
        bool flag = false;
        try
        {
          this.MI = (MethodBase) runtimeType.GetMethod(this.methodName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
          if (this.instArgs != null)
          {
            if (this.instArgs.Length != 0)
              this.MI = (MethodBase) ((MethodInfo) this.MI).MakeGenericMethod(this.instArgs);
          }
        }
        catch (AmbiguousMatchException ex)
        {
          flag = true;
          this.ResolveOverloadedMethod(runtimeType);
        }
        if (this.MI != (MethodBase) null && !flag && remotingTypeCachedData != null)
          remotingTypeCachedData.SetLastCalledMethod(this.methodName, this.MI);
      }
      if (this.MI == (MethodBase) null & bThrowIfNotResolved)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MethodMissing"), (object) this.methodName, (object) this.typeName));
    }

    private void ResolveOverloadedMethod(RuntimeType t)
    {
      if (this.args == null)
        return;
      MemberInfo[] member = t.GetMember(this.methodName, MemberTypes.Method, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
      int length1 = member.Length;
      switch (length1)
      {
        case 0:
          break;
        case 1:
          this.MI = member[0] as MethodBase;
          break;
        default:
          int length2 = this.args.Length;
          MethodBase methodBase1 = (MethodBase) null;
          for (int index = 0; index < length1; ++index)
          {
            MethodBase methodBase2 = member[index] as MethodBase;
            if (methodBase2.GetParameters().Length == length2)
            {
              if (methodBase1 != (MethodBase) null)
                throw new RemotingException(Environment.GetResourceString("Remoting_AmbiguousMethod"));
              methodBase1 = methodBase2;
            }
          }
          if (!(methodBase1 != (MethodBase) null))
            break;
          this.MI = methodBase1;
          break;
      }
    }

    private void ResolveOverloadedMethod(RuntimeType t, string methodName, ArrayList argNames, ArrayList argValues)
    {
      MemberInfo[] member = t.GetMember(methodName, MemberTypes.Method, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
      int length = member.Length;
      switch (length)
      {
        case 0:
          break;
        case 1:
          this.MI = member[0] as MethodBase;
          break;
        default:
          MethodBase methodBase1 = (MethodBase) null;
          for (int index1 = 0; index1 < length; ++index1)
          {
            MethodBase methodBase2 = member[index1] as MethodBase;
            ParameterInfo[] parameters = methodBase2.GetParameters();
            if (parameters.Length == argValues.Count)
            {
              bool flag = true;
              for (int index2 = 0; index2 < parameters.Length; ++index2)
              {
                Type type = parameters[index2].ParameterType;
                if (type.IsByRef)
                  type = type.GetElementType();
                if (type != argValues[index2].GetType())
                {
                  flag = false;
                  break;
                }
              }
              if (flag)
              {
                methodBase1 = methodBase2;
                break;
              }
            }
          }
          if (methodBase1 == (MethodBase) null)
            throw new RemotingException(Environment.GetResourceString("Remoting_AmbiguousMethod"));
          this.MI = methodBase1;
          break;
      }
    }

    /// <summary>
    ///   <see cref="M:System.Runtime.Remoting.Messaging.MethodCall.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> Метод не реализован.
    /// </summary>
    /// <param name="info">
    ///   Данные для сериализации или десериализации удаленного объекта.
    /// </param>
    /// <param name="context">
    ///   Контекст определенного сериализуемого потока.
    /// </param>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
    }

    [SecurityCritical]
    internal void SetObjectFromSoapData(SerializationInfo info)
    {
      this.methodName = info.GetString("__methodName");
      ArrayList arrayList = (ArrayList) info.GetValue("__paramNameList", typeof (ArrayList));
      Hashtable keyToNamespaceTable = (Hashtable) info.GetValue("__keyToNamespaceTable", typeof (Hashtable));
      if (this.MI == (MethodBase) null)
      {
        ArrayList argValues = new ArrayList();
        ArrayList argNames = arrayList;
        for (int index = 0; index < argNames.Count; ++index)
          argValues.Add(info.GetValue((string) argNames[index], typeof (object)));
        RuntimeType t = this.ResolveType() as RuntimeType;
        if (t == (RuntimeType) null)
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_BadType"), (object) this.typeName));
        this.ResolveOverloadedMethod(t, this.methodName, argNames, argValues);
        if (this.MI == (MethodBase) null)
          throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_Message_MethodMissing"), (object) this.methodName, (object) this.typeName));
      }
      RemotingMethodCachedData reflectionCachedData = InternalRemotingServices.GetReflectionCachedData(this.MI);
      ParameterInfo[] parameters = reflectionCachedData.Parameters;
      int[] marshalRequestArgMap = reflectionCachedData.MarshalRequestArgMap;
      object obj = this.InternalProperties == null ? (object) null : this.InternalProperties[(object) "__UnorderedParams"];
      this.args = new object[parameters.Length];
      if (obj != null && obj is bool && (bool) obj)
      {
        for (int index1 = 0; index1 < arrayList.Count; ++index1)
        {
          string name = (string) arrayList[index1];
          int index2 = -1;
          for (int index3 = 0; index3 < parameters.Length; ++index3)
          {
            if (name.Equals(parameters[index3].Name))
            {
              index2 = parameters[index3].Position;
              break;
            }
          }
          if (index2 == -1)
          {
            if (!name.StartsWith("__param", StringComparison.Ordinal))
              throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadSerialization"));
            index2 = int.Parse(name.Substring(7), (IFormatProvider) CultureInfo.InvariantCulture);
          }
          if (index2 >= this.args.Length)
            throw new RemotingException(Environment.GetResourceString("Remoting_Message_BadSerialization"));
          this.args[index2] = Message.SoapCoerceArg(info.GetValue(name, typeof (object)), parameters[index2].ParameterType, keyToNamespaceTable);
        }
      }
      else
      {
        for (int index = 0; index < arrayList.Count; ++index)
        {
          string name = (string) arrayList[index];
          this.args[marshalRequestArgMap[index]] = Message.SoapCoerceArg(info.GetValue(name, typeof (object)), parameters[marshalRequestArgMap[index]].ParameterType, keyToNamespaceTable);
        }
        this.PopulateOutArguments(reflectionCachedData);
      }
    }

    [SecurityCritical]
    [PermissionSet(SecurityAction.Assert, Unrestricted = true)]
    private void PopulateOutArguments(RemotingMethodCachedData methodCache)
    {
      ParameterInfo[] parameters = methodCache.Parameters;
      foreach (int outOnlyArg in methodCache.OutOnlyArgMap)
      {
        Type elementType = parameters[outOnlyArg].ParameterType.GetElementType();
        if (elementType.IsValueType)
          this.args[outOnlyArg] = Activator.CreateInstance(elementType, true);
      }
    }

    /// <summary>
    ///   Инициализирует <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" />.
    /// </summary>
    public virtual void Init()
    {
    }

    /// <summary>Возвращает число аргументов, переданных методу.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.Int32" /> представляющий количество аргументов, передаваемых в метод.
    /// </returns>
    public int ArgCount
    {
      [SecurityCritical] get
      {
        if (this.args != null)
          return this.args.Length;
        return 0;
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
      return this.args[argNum];
    }

    /// <summary>
    ///   Возвращает имя аргумента метода по указанному индексу.
    /// </summary>
    /// <param name="index">Индекс запрошенного аргумента.</param>
    /// <returns>Имя аргумента метода.</returns>
    [SecurityCritical]
    public string GetArgName(int index)
    {
      this.ResolveMethod();
      return InternalRemotingServices.GetReflectionCachedData(this.MI).Parameters[index].Name;
    }

    /// <summary>Возвращает массив аргументов, передаваемых в метод.</summary>
    /// <returns>
    ///   Массив объектов типа <see cref="T:System.Object" /> представляющий аргументы, переданные в метод.
    /// </returns>
    public object[] Args
    {
      [SecurityCritical] get
      {
        return this.args;
      }
    }

    /// <summary>
    ///   Возвращает число аргументов в вызове метода, которые не помечены как <see langword="out" /> параметров.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Int32" /> представляющий число аргументов в вызове метода, которые не помечены как <see langword="out" /> параметров.
    /// </returns>
    public int InArgCount
    {
      [SecurityCritical] get
      {
        if (this.argMapper == null)
          this.argMapper = new ArgMapper((IMethodMessage) this, false);
        return this.argMapper.ArgCount;
      }
    }

    /// <summary>
    ///   Возвращает аргумент метода по указанному индексу, который не помечен как <see langword="out" /> параметр.
    /// </summary>
    /// <param name="argNum">Индекс запрошенного аргумента.</param>
    /// <returns>
    ///   Аргумент метода, который не помечен как <see langword="out" /> параметр.
    /// </returns>
    [SecurityCritical]
    public object GetInArg(int argNum)
    {
      if (this.argMapper == null)
        this.argMapper = new ArgMapper((IMethodMessage) this, false);
      return this.argMapper.GetArg(argNum);
    }

    /// <summary>
    ///   Возвращает имя аргумента метода по указанному индексу, который не помечен как <see langword="out" /> параметр.
    /// </summary>
    /// <param name="index">Индекс запрошенного аргумента.</param>
    /// <returns>
    ///   Имя аргумента метода, который не помечен как <see langword="out" /> параметр.
    /// </returns>
    [SecurityCritical]
    public string GetInArgName(int index)
    {
      if (this.argMapper == null)
        this.argMapper = new ArgMapper((IMethodMessage) this, false);
      return this.argMapper.GetArgName(index);
    }

    /// <summary>
    ///   Возвращает массив аргументов в вызове метода, которые не помечены как <see langword="out" /> параметров.
    /// </summary>
    /// <returns>
    ///   Массив объектов типа <see cref="T:System.Object" /> представляющий аргументы в вызове метода, которые не помечены как <see langword="out" /> параметров.
    /// </returns>
    public object[] InArgs
    {
      [SecurityCritical] get
      {
        if (this.argMapper == null)
          this.argMapper = new ArgMapper((IMethodMessage) this, false);
        return this.argMapper.Args;
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
        if (this.methodSignature != null)
          return (object) this.methodSignature;
        if (this.MI != (MethodBase) null)
          this.methodSignature = Message.GenerateMethodSignature(this.MethodBase);
        return (object) null;
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
        if (this.MI == (MethodBase) null)
          this.MI = RemotingServices.InternalGetMethodBaseFromMethodMessage((IMethodMessage) this);
        return this.MI;
      }
    }

    /// <summary>
    ///   Возвращает или задает универсальный код ресурса (URI) удаленного объекта, для которого осуществляется вызов метода.
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
        return this.fVarArgs;
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
            this.ExternalProperties = (IDictionary) new MCMDictionary((IMethodCallMessage) this, this.InternalProperties);
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

    ServerIdentity IInternalMessage.ServerIdentityObject
    {
      [SecurityCritical] get
      {
        return this.srvID;
      }
      [SecurityCritical] set
      {
        this.srvID = value;
      }
    }

    Identity IInternalMessage.IdentityObject
    {
      [SecurityCritical] get
      {
        return this.identity;
      }
      [SecurityCritical] set
      {
        this.identity = value;
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
    internal virtual bool FillSpecialHeader(string key, object value)
    {
      if (key != null)
      {
        if (key.Equals("__Uri"))
          this.uri = (string) value;
        else if (key.Equals("__MethodName"))
          this.methodName = (string) value;
        else if (key.Equals("__MethodSignature"))
          this.methodSignature = (Type[]) value;
        else if (key.Equals("__TypeName"))
          this.typeName = (string) value;
        else if (key.Equals("__Args"))
        {
          this.args = (object[]) value;
        }
        else
        {
          if (!key.Equals("__CallContext"))
            return false;
          if (value is string)
          {
            this.callContext = new LogicalCallContext();
            this.callContext.RemotingData.LogicalCallID = (string) value;
          }
          else
            this.callContext = (LogicalCallContext) value;
        }
      }
      return true;
    }

    [SecurityCritical]
    internal void FillHeader(string key, object value)
    {
      if (this.FillSpecialHeader(key, value))
        return;
      if (this.InternalProperties == null)
        this.InternalProperties = (IDictionary) new Hashtable();
      this.InternalProperties[(object) key] = value;
    }

    /// <summary>
    ///   Инициализирует внутренний обработчик сериализации из массива заголовков удаленного взаимодействия, которые применяются к методу.
    /// </summary>
    /// <param name="h">
    ///   Массив заголовков удаленного взаимодействия, содержащих пары "ключ значение".
    ///    Этот массив используется для инициализации <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" /> поля для заголовков, которые принадлежат к пространству имен «http://schemas.microsoft.com/clr/soap/messageProperties».
    /// </param>
    /// <returns>Внутренний обработчик сериализации.</returns>
    [SecurityCritical]
    public virtual object HeaderHandler(Header[] h)
    {
      SerializationMonkey uninitializedObject = (SerializationMonkey) FormatterServices.GetUninitializedObject(typeof (SerializationMonkey));
      Header[] h1;
      if (h != null && h.Length != 0 && h[0].Name == "__methodName")
      {
        this.methodName = (string) h[0].Value;
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
      this.FillHeaders(h1, true);
      this.ResolveMethod(false);
      uninitializedObject._obj = (ISerializationRootObject) this;
      if (this.MI != (MethodBase) null)
      {
        ArgMapper argMapper = new ArgMapper(this.MI, false);
        uninitializedObject.fieldNames = argMapper.ArgNames;
        uninitializedObject.fieldTypes = argMapper.ArgTypes;
      }
      return (object) uninitializedObject;
    }
  }
}

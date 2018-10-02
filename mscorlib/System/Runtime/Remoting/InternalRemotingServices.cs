// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.InternalRemotingServices
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.Remoting
{
  /// <summary>
  ///   Определяет служебные методы для использования инфраструктурой удаленного взаимодействия платформы .NET Framework.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  public class InternalRemotingServices
  {
    /// <summary>
    ///   Отправляет сообщение об удаленном канале на неуправляемый отладчик.
    /// </summary>
    /// <param name="s">Строка для размещения в сообщении.</param>
    [SecurityCritical]
    [Conditional("_LOGGING")]
    public static void DebugOutChnl(string s)
    {
      Message.OutToUnmanagedDebugger("CHNL:" + s + "\n");
    }

    /// <summary>
    ///   Отправляет любое количество сообщений об удаленных каналах на внутренний отладчик.
    /// </summary>
    /// <param name="messages">
    ///   Массив объектов типа <see cref="T:System.Object" /> содержащий любое количество сообщений.
    /// </param>
    [Conditional("_LOGGING")]
    public static void RemotingTrace(params object[] messages)
    {
    }

    /// <summary>
    ///   Указывает внутреннему отладчику на необходимость проверки условия и отображения сообщения, если условие равно <see langword="false" />.
    /// </summary>
    /// <param name="condition">
    ///   <see langword="true" /> Чтобы сообщение не отображалось; в противном случае — <see langword="false" />.
    /// </param>
    /// <param name="message">
    ///   Сообщение, отображаемое, если <paramref name="condition" /> является <see langword="false" />.
    /// </param>
    [Conditional("_DEBUG")]
    public static void RemotingAssert(bool condition, string message)
    {
    }

    /// <summary>
    ///   Задает внутренний идентифицирующие сведения для объекта удаленного сервера для каждого вызова метода от клиента к серверу.
    /// </summary>
    /// <param name="m">
    ///   Объект <see cref="T:System.Runtime.Remoting.Messaging.MethodCall" /> представляющий вызов метода удаленного объекта.
    /// </param>
    /// <param name="srvID">
    ///   Внутренняя идентифицирующие сведения для объекта удаленного сервера.
    /// </param>
    [SecurityCritical]
    [CLSCompliant(false)]
    public static void SetServerIdentity(MethodCall m, object srvID)
    {
      ((IInternalMessage) m).ServerIdentityObject = (ServerIdentity) srvID;
    }

    internal static RemotingMethodCachedData GetReflectionCachedData(MethodBase mi)
    {
      RuntimeMethodInfo runtimeMethodInfo;
      if ((MethodInfo) (runtimeMethodInfo = mi as RuntimeMethodInfo) != (MethodInfo) null)
        return runtimeMethodInfo.RemotingCache;
      RuntimeConstructorInfo runtimeConstructorInfo;
      if ((ConstructorInfo) (runtimeConstructorInfo = mi as RuntimeConstructorInfo) != (ConstructorInfo) null)
        return runtimeConstructorInfo.RemotingCache;
      throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeReflectionObject"));
    }

    internal static RemotingTypeCachedData GetReflectionCachedData(RuntimeType type)
    {
      return type.RemotingCache;
    }

    internal static RemotingCachedData GetReflectionCachedData(MemberInfo mi)
    {
      MethodBase mi1;
      if ((mi1 = mi as MethodBase) != (MethodBase) null)
        return (RemotingCachedData) InternalRemotingServices.GetReflectionCachedData(mi1);
      RuntimeType type;
      if ((type = mi as RuntimeType) != (RuntimeType) null)
        return (RemotingCachedData) InternalRemotingServices.GetReflectionCachedData(type);
      RuntimeFieldInfo runtimeFieldInfo;
      if ((FieldInfo) (runtimeFieldInfo = mi as RuntimeFieldInfo) != (FieldInfo) null)
        return (RemotingCachedData) runtimeFieldInfo.RemotingCache;
      SerializationFieldInfo serializationFieldInfo;
      if ((FieldInfo) (serializationFieldInfo = mi as SerializationFieldInfo) != (FieldInfo) null)
        return (RemotingCachedData) serializationFieldInfo.RemotingCache;
      throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeReflectionObject"));
    }

    internal static RemotingCachedData GetReflectionCachedData(RuntimeParameterInfo reflectionObject)
    {
      return (RemotingCachedData) reflectionObject.RemotingCache;
    }

    /// <summary>
    ///   Возвращает соответствующий атрибут, связанный с SOAP для параметра, члена или метод указанного класса.
    /// </summary>
    /// <param name="reflectionObject">
    ///   Класс члена или параметра метода.
    /// </param>
    /// <returns>
    ///   Относящиеся к SOAP атрибут для заданного класса члена или параметра метода.
    /// </returns>
    [SecurityCritical]
    public static SoapAttribute GetCachedSoapAttribute(object reflectionObject)
    {
      MemberInfo mi = reflectionObject as MemberInfo;
      RuntimeParameterInfo reflectionObject1 = reflectionObject as RuntimeParameterInfo;
      if (mi != (MemberInfo) null)
        return InternalRemotingServices.GetReflectionCachedData(mi).GetSoapAttribute();
      if (reflectionObject1 != null)
        return InternalRemotingServices.GetReflectionCachedData(reflectionObject1).GetSoapAttribute();
      return (SoapAttribute) null;
    }
  }
}

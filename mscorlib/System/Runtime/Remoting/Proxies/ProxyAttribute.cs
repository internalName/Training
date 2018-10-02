// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Proxies.ProxyAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Contexts;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Proxies
{
  /// <summary>
  ///   Указывает, что тип объекта требуется настраиваемый прокси-сервер.
  /// </summary>
  [SecurityCritical]
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public class ProxyAttribute : Attribute, IContextAttribute
  {
    /// <summary>
    ///   Создает или неинициализированный объект <see cref="T:System.MarshalByRefObject" /> или прозрачный прокси, в зависимости от того, наличие указанного типа в текущем контексте.
    /// </summary>
    /// <param name="serverType">
    ///   Тип объекта создаваемого экземпляра.
    /// </param>
    /// <returns>
    ///   Инициализированная <see cref="T:System.MarshalByRefObject" /> или прозрачный прокси.
    /// </returns>
    [SecurityCritical]
    public virtual MarshalByRefObject CreateInstance(Type serverType)
    {
      if (serverType == (Type) null)
        throw new ArgumentNullException(nameof (serverType));
      RuntimeType serverType1 = serverType as RuntimeType;
      if (serverType1 == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      if (!serverType.IsContextful)
        throw new RemotingException(Environment.GetResourceString("Remoting_Activation_MBR_ProxyAttribute"));
      if (serverType.IsAbstract)
        throw new RemotingException(Environment.GetResourceString("Acc_CreateAbst"));
      return this.CreateInstanceInternal(serverType1);
    }

    internal MarshalByRefObject CreateInstanceInternal(RuntimeType serverType)
    {
      return ActivationServices.CreateInstance(serverType);
    }

    /// <summary>
    ///   Создает экземпляр удаленного прокси для удаленного объекта, описанного заданным <see cref="T:System.Runtime.Remoting.ObjRef" />, и на сервере.
    /// </summary>
    /// <param name="objRef">
    ///   Ссылка на объект, удаленный объект, для которого необходимо создать учетную запись-посредник.
    /// </param>
    /// <param name="serverType">
    ///   Тип сервера, на котором удаленный объект.
    /// </param>
    /// <param name="serverObject">Объект сервера.</param>
    /// <param name="serverContext">
    ///   Контекст, в котором находится объект сервера.
    /// </param>
    /// <returns>
    ///   Новый экземпляр удаленного прокси для удаленного объекта, описанного в указанном <see cref="T:System.Runtime.Remoting.ObjRef" />.
    /// </returns>
    [SecurityCritical]
    public virtual RealProxy CreateProxy(ObjRef objRef, Type serverType, object serverObject, Context serverContext)
    {
      RemotingProxy remotingProxy = new RemotingProxy(serverType);
      if (serverContext != null)
        RealProxy.SetStubData((RealProxy) remotingProxy, (object) serverContext.InternalContextID);
      if (objRef != null && objRef.GetServerIdentity().IsAllocated)
        remotingProxy.SetSrvInfo(objRef.GetServerIdentity(), objRef.GetDomainID());
      remotingProxy.Initialized = true;
      Type type = serverType;
      if (!type.IsContextful && !type.IsMarshalByRef && serverContext != null)
        throw new RemotingException(Environment.GetResourceString("Remoting_Activation_MBR_ProxyAttribute"));
      return (RealProxy) remotingProxy;
    }

    /// <summary>Проверяет заданный контекст.</summary>
    /// <param name="ctx">Контекст, который требуется проверить.</param>
    /// <param name="msg">Сообщение для удаленного вызова.</param>
    /// <returns>Указанный контекст.</returns>
    [SecurityCritical]
    [ComVisible(true)]
    public bool IsContextOK(Context ctx, IConstructionCallMessage msg)
    {
      return true;
    }

    /// <summary>Возвращает свойства для нового контекста.</summary>
    /// <param name="msg">
    ///   Сообщение, для которого требуется получить контекст.
    /// </param>
    [SecurityCritical]
    [ComVisible(true)]
    public void GetPropertiesForNewContext(IConstructionCallMessage msg)
    {
    }
  }
}

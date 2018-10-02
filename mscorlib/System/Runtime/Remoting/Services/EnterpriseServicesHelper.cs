// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Services.EnterpriseServicesHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Security;

namespace System.Runtime.Remoting.Services
{
  /// <summary>
  ///   Предоставляет API, необходимые для связи и работы с неуправляемыми классами вне <see cref="T:System.AppDomain" />.
  ///    Этот класс не наследуется.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  public sealed class EnterpriseServicesHelper
  {
    /// <summary>
    ///   Заключает в оболочку заданный <see langword="IUnknown" /> COM-интерфейс с Runtime Callable Wrapper (времени выполнения RCW).
    /// </summary>
    /// <param name="punk">
    ///   Указатель на <see langword="IUnknown" /> в оболочку COM-интерфейс.
    /// </param>
    /// <returns>
    ///   Вызываемая оболочка времени Выполнения которых указанный <see langword="IUnknown" /> заключается в оболочку.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения UnmanagedCode.
    /// </exception>
    [SecurityCritical]
    public static object WrapIUnknownWithComObject(IntPtr punk)
    {
      return Marshal.InternalWrapIUnknownWithComObject(punk);
    }

    /// <summary>
    ///   Создает <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> из указанного <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />.
    /// </summary>
    /// <param name="ctorMsg">
    ///   Вызов создания объекта из которой новый <see cref="T:System.Runtime.Remoting.Messaging.ReturnMessage" /> возвращения экземпляра.
    /// </param>
    /// <param name="retObj">
    ///   Объект <see cref="T:System.Runtime.Remoting.ObjRef" /> представляющий объект, который создается с помощью вызова конструирования в <paramref name="ctorMsg" />.
    /// </param>
    /// <returns>
    ///   Значение <see cref="T:System.Runtime.Remoting.Activation.IConstructionReturnMessage" /> возвращается из вызова конструирования, который указан в <paramref name="ctorMsg" /> параметр.
    /// </returns>
    [ComVisible(true)]
    public static IConstructionReturnMessage CreateConstructionReturnMessage(IConstructionCallMessage ctorMsg, MarshalByRefObject retObj)
    {
      return (IConstructionReturnMessage) new ConstructorReturnMessage(retObj, (object[]) null, 0, (LogicalCallContext) null, ctorMsg);
    }

    /// <summary>
    ///   Переключает COM Callable Wrapper (CCW) между двумя экземплярами одного класса.
    /// </summary>
    /// <param name="oldcp">
    ///   Прокси, который представляет старый экземпляр класса, на который ссылается COM.
    /// </param>
    /// <param name="newcp">
    ///   Прокси, который представляет новый экземпляр класса, на который ссылается COM.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешение UnmanagedCode.
    /// </exception>
    [SecurityCritical]
    public static void SwitchWrappers(RealProxy oldcp, RealProxy newcp)
    {
      object transparentProxy1 = oldcp.GetTransparentProxy();
      object transparentProxy2 = newcp.GetTransparentProxy();
      RemotingServices.GetServerContextForProxy(transparentProxy1);
      RemotingServices.GetServerContextForProxy(transparentProxy2);
      Marshal.InternalSwitchCCW(transparentProxy1, transparentProxy2);
    }
  }
}

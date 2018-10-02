// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Activation.IConstructionCallMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
  /// <summary>Представляет запрос вызова конструкции объекта.</summary>
  [ComVisible(true)]
  public interface IConstructionCallMessage : IMethodCallMessage, IMethodMessage, IMessage
  {
    /// <summary>
    ///   Возвращает или задает активатор, который активирует удаленные объекты.
    /// </summary>
    /// <returns>Активатор, который активирует удаленные объекты.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    IActivator Activator { [SecurityCritical] get; [SecurityCritical] set; }

    /// <summary>Возвращает атрибуты активации узла вызова.</summary>
    /// <returns>Атрибуты активации узла вызова.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    object[] CallSiteActivationAttributes { [SecurityCritical] get; }

    /// <summary>
    ///   Возвращает полное имя типа удаленного типа, подлежащего активации.
    /// </summary>
    /// <returns>
    ///   Полное имя типа удаленного типа, подлежащего активации.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    string ActivationTypeName { [SecurityCritical] get; }

    /// <summary>
    ///   Возвращает тип удаленного объекта, подлежащего активации.
    /// </summary>
    /// <returns>Тип удаленного объекта, подлежащего активации.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    Type ActivationType { [SecurityCritical] get; }

    /// <summary>
    ///   Возвращает список свойств контекста, которые определяют контекст, в котором должен быть создан объект.
    /// </summary>
    /// <returns>
    ///   Список свойств контекста, в котором конструируется объект.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    IList ContextProperties { [SecurityCritical] get; }
  }
}

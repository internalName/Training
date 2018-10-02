// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Activation.IActivator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Activation
{
  /// <summary>
  ///   Предоставляет базовую функциональность для класса активатора удаленного взаимодействия.
  /// </summary>
  [ComVisible(true)]
  public interface IActivator
  {
    /// <summary>Возвращает или задает следующий активатор в цепи.</summary>
    /// <returns>Следующий активатор в цепи.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    IActivator NextActivator { [SecurityCritical] get; [SecurityCritical] set; }

    /// <summary>
    ///   Создает экземпляр объекта, который указан в предоставленном <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />.
    /// </summary>
    /// <param name="msg">
    ///   Сведения об объекте, который необходим для активации, хранящиеся в <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />.
    /// </param>
    /// <returns>
    ///   Состояние активации объекта, содержащегося в <see cref="T:System.Runtime.Remoting.Activation.IConstructionReturnMessage" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    [SecurityCritical]
    IConstructionReturnMessage Activate(IConstructionCallMessage msg);

    /// <summary>
    ///   Возвращает <see cref="T:System.Runtime.Remoting.Activation.ActivatorLevel" /> где действует этот активатор.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.Remoting.Activation.ActivatorLevel" /> Где действует этот активатор.
    /// </returns>
    ActivatorLevel Level { [SecurityCritical] get; }
  }
}

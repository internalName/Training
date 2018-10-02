// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.ObjectHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Lifetime;
using System.Security;

namespace System.Runtime.Remoting
{
  /// <summary>
  ///   Создает оболочку для ссылки на объект маршалинг по значению, разрешая их возвращение через косвенное обращение.
  /// </summary>
  [ClassInterface(ClassInterfaceType.AutoDual)]
  [ComVisible(true)]
  public class ObjectHandle : MarshalByRefObject, IObjectHandle
  {
    private object WrappedObject;

    private ObjectHandle()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Remoting.ObjectHandle" /> упаковки данного объекта класса <paramref name="o" />.
    /// </summary>
    /// <param name="o">
    ///   Объект, который является оболочкой для нового <see cref="T:System.Runtime.Remoting.ObjectHandle" />.
    /// </param>
    public ObjectHandle(object o)
    {
      this.WrappedObject = o;
    }

    /// <summary>Возвращает объект оболочки.</summary>
    /// <returns>Объект оболочки.</returns>
    public object Unwrap()
    {
      return this.WrappedObject;
    }

    /// <summary>
    ///   Инициализирует аренду времени существования перенесенного объекта.
    /// </summary>
    /// <returns>
    ///   Инициализированный объект <see cref="T:System.Runtime.Remoting.Lifetime.ILease" /> позволяет управлять временем существования упакованного объекта.
    /// </returns>
    [SecurityCritical]
    public override object InitializeLifetimeService()
    {
      MarshalByRefObject wrappedObject = this.WrappedObject as MarshalByRefObject;
      if (wrappedObject != null && wrappedObject.InitializeLifetimeService() == null)
        return (object) null;
      return (object) (ILease) base.InitializeLifetimeService();
    }
  }
}

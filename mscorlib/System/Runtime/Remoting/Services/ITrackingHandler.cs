// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Services.ITrackingHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Services
{
  /// <summary>
  ///   Указывает, что реализующий объект должен быть уведомлен о маршалинге, распаковка и отключении объектов и прокси инфраструктурой удаленного взаимодействия.
  /// </summary>
  [ComVisible(true)]
  public interface ITrackingHandler
  {
    /// <summary>Уведомляет текущий экземпляр упакованный объект.</summary>
    /// <param name="obj">Объект, который был передан.</param>
    /// <param name="or">
    ///   <see cref="T:System.Runtime.Remoting.ObjRef" /> Полученный в результате маршалинг и представляет указанный объект.
    /// </param>
    [SecurityCritical]
    void MarshaledObject(object obj, ObjRef or);

    /// <summary>Уведомляет текущий экземпляр о распаковать объект.</summary>
    /// <param name="obj">Распакованный объект.</param>
    /// <param name="or">
    ///   <see cref="T:System.Runtime.Remoting.ObjRef" /> Представляющий указанный объект.
    /// </param>
    [SecurityCritical]
    void UnmarshaledObject(object obj, ObjRef or);

    /// <summary>
    ///   Уведомляет текущий экземпляр, что объект был отключен от его прокси.
    /// </summary>
    /// <param name="obj">Отсоединенный объект.</param>
    [SecurityCritical]
    void DisconnectedObject(object obj);
  }
}

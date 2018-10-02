// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.MessageSurrogateFilter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>
  ///   Определяет ли <see cref="T:System.Runtime.Remoting.Messaging.RemotingSurrogateSelector" /> класс должен игнорировать определенный <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> свойства при создании <see cref="T:System.Runtime.Remoting.ObjRef" /> для <see cref="T:System.MarshalByRefObject" /> класса.
  /// </summary>
  /// <param name="key">
  ///   Ключ свойства сообщения определенного удаленного взаимодействия.
  /// </param>
  /// <param name="value">
  ///   Значение свойства сообщения определенного удаленного взаимодействия.
  /// </param>
  /// <returns>
  ///   Значение true, если <see cref="T:System.Runtime.Remoting.Messaging.RemotingSurrogateSelector" /> класс должен игнорировать определенный <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> свойства при создании <see cref="T:System.Runtime.Remoting.ObjRef" /> для <see cref="T:System.MarshalByRefObject" /> класса.
  /// </returns>
  [ComVisible(true)]
  public delegate bool MessageSurrogateFilter(string key, object value);
}

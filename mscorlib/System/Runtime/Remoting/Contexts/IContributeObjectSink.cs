// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.IContributeObjectSink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>
  ///   Предоставляет приемник перехвата для объекта на серверной стороне вызова удаленного взаимодействия.
  /// </summary>
  [ComVisible(true)]
  public interface IContributeObjectSink
  {
    /// <summary>
    ///   Добавляет приемник сообщения предоставленного объекта сервера перед заданной цепочки приемников.
    /// </summary>
    /// <param name="obj">
    ///   Серверный объект, предоставляющий приемник сообщений, находится в цепочку перед заданной цепочки.
    /// </param>
    /// <param name="nextSink">
    ///   Сформированная на данный момент цепочка приемников.
    /// </param>
    /// <returns>Составная цепочка приемников.</returns>
    [SecurityCritical]
    IMessageSink GetObjectSink(MarshalByRefObject obj, IMessageSink nextSink);
  }
}

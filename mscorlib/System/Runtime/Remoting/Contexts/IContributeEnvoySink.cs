﻿// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Contexts.IContributeEnvoySink
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
  /// <summary>
  ///   Предоставляет приемник сообщений делегатов со стороны клиента.
  /// </summary>
  [ComVisible(true)]
  public interface IContributeEnvoySink
  {
    /// <summary>
    ///   Принимает первый приемник в цепочке приемников до сих и помещает его приемник сообщений перед уже сформированной цепочки.
    /// </summary>
    /// <param name="obj">
    ///   Серверный объект, для которого создается цепочка.
    /// </param>
    /// <param name="nextSink">
    ///   Сформированная на данный момент цепочка приемников.
    /// </param>
    /// <returns>Составная цепочка приемников.</returns>
    [SecurityCritical]
    IMessageSink GetEnvoySink(MarshalByRefObject obj, IMessageSink nextSink);
  }
}

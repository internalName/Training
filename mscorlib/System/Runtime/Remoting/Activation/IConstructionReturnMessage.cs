// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Activation.IConstructionReturnMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Activation
{
  /// <summary>
  ///   Идентифицирует <see cref="T:System.Runtime.Remoting.Messaging.IMethodReturnMessage" /> возвращаемое после попытки активации удаленного объекта.
  /// </summary>
  [ComVisible(true)]
  public interface IConstructionReturnMessage : IMethodReturnMessage, IMethodMessage, IMessage
  {
  }
}

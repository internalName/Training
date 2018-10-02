// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.ISecurableChannel
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>
  ///   <see cref="T:System.Runtime.Remoting.Channels.ISecurableChannel" /> Содержит одно свойство <see cref="P:System.Runtime.Remoting.Channels.ISecurableChannel.IsSecured" />, который возвращает или задает логическое значение, которое указывает, является ли текущий канал безопасности.
  /// </summary>
  public interface ISecurableChannel
  {
    /// <summary>
    ///   Возвращает или задает логическое значение, указывающее, является ли текущий канал безопасным.
    /// </summary>
    /// <returns>
    ///   Логическое значение, указывающее, является ли текущий канал безопасным.
    /// </returns>
    bool IsSecured { [SecurityCritical] get; [SecurityCritical] set; }
  }
}

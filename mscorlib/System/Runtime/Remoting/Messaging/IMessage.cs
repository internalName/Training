// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.IMessage
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  /// <summary>
  ///   Содержит связи, данные, передаваемые между сотрудничающих сообщение приемниками.
  /// </summary>
  [ComVisible(true)]
  public interface IMessage
  {
    /// <summary>
    ///   Возвращает <see cref="T:System.Collections.IDictionary" /> представляющий коллекцию свойств сообщений.
    /// </summary>
    /// <returns>
    ///   Словарь, представляющий коллекцию свойств сообщений.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий оператор делает вызов через ссылку на интерфейс и не имеет разрешения инфраструктуры.
    /// </exception>
    IDictionary Properties { [SecurityCritical] get; }
  }
}

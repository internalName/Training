// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.IChannelSinkBase
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>
  ///   Предоставляет основной интерфейс для приемников каналов.
  /// </summary>
  [ComVisible(true)]
  public interface IChannelSinkBase
  {
    /// <summary>
    ///   Возвращает словарь, через который доступ к свойствам приемника.
    /// </summary>
    /// <returns>
    ///   Словарь, через который может осуществляться свойствам приемника, или <see langword="null" /> Если приемник канала не поддерживает свойств.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Непосредственный вызывающий объект не имеет разрешения инфраструктуры.
    /// </exception>
    IDictionary Properties { [SecurityCritical] get; }
  }
}

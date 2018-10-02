// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.BaseChannelSinkWithProperties
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
  /// <summary>
  ///   Предоставляет базовую реализацию для приемников каналов, которые нужно предоставить интерфейс словаря к его свойствам.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  [SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
  public abstract class BaseChannelSinkWithProperties : BaseChannelObjectWithProperties
  {
  }
}

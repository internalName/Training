// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.LogMessageEventHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security.Permissions;

namespace System.Diagnostics
{
  [Serializable]
  [HostProtection(SecurityAction.LinkDemand, ExternalThreading = true, Synchronization = true)]
  internal delegate void LogMessageEventHandler(LoggingLevels level, LogSwitch category, string message, StackTrace location);
}

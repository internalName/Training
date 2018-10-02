// Decompiled with JetBrains decompiler
// Type: System.Threading.Tasks.AsyncCausalityStatus
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
  [FriendAccessAllowed]
  internal enum AsyncCausalityStatus
  {
    Started,
    Completed,
    Canceled,
    Error,
  }
}

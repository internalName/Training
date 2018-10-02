// Decompiled with JetBrains decompiler
// Type: System.Security.SuppressUnmanagedCodeSecurityAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security
{
  /// <summary>
  ///   Позволяет управляемому коду вызывать неуправляемый код без обхода стека.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
  [ComVisible(true)]
  public sealed class SuppressUnmanagedCodeSecurityAttribute : Attribute
  {
  }
}

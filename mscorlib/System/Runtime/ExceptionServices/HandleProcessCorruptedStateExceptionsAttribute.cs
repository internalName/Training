// Decompiled with JetBrains decompiler
// Type: System.Runtime.ExceptionServices.HandleProcessCorruptedStateExceptionsAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.ExceptionServices
{
  /// <summary>
  ///   Позволяет управляемому коду обрабатывать исключения, указывающие на повреждение состояния процесса.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
  public sealed class HandleProcessCorruptedStateExceptionsAttribute : Attribute
  {
  }
}

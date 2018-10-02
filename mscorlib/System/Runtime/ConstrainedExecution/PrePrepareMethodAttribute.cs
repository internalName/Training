// Decompiled with JetBrains decompiler
// Type: System.Runtime.ConstrainedExecution.PrePrepareMethodAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.ConstrainedExecution
{
  /// <summary>
  ///   Указывает, что служба генерирования машинных образов подготовить метод для включения в области ограниченного выполнения (CER).
  /// </summary>
  [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
  public sealed class PrePrepareMethodAttribute : Attribute
  {
  }
}

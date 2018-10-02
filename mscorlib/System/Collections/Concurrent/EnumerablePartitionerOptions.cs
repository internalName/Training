// Decompiled with JetBrains decompiler
// Type: System.Collections.Concurrent.EnumerablePartitionerOptions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Collections.Concurrent
{
  /// <summary>
  ///   Задает параметры, которые управляют поведением буферизации разделитель
  /// </summary>
  [Flags]
  [__DynamicallyInvokable]
  [Serializable]
  public enum EnumerablePartitionerOptions
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] NoBuffering = 1,
  }
}

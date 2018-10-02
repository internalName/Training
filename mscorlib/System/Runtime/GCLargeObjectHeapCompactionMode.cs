// Decompiled with JetBrains decompiler
// Type: System.Runtime.GCLargeObjectHeapCompactionMode
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime
{
  /// <summary>
  ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
  /// 
  ///   Указывает, является ли следующей блокирующей сборки мусора сжимает кучу больших объектов (LOH).
  /// </summary>
  [__DynamicallyInvokable]
  [Serializable]
  public enum GCLargeObjectHeapCompactionMode
  {
    [__DynamicallyInvokable] Default = 1,
    [__DynamicallyInvokable] CompactOnce = 2,
  }
}

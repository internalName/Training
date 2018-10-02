// Decompiled with JetBrains decompiler
// Type: System.Reflection.ProcessorArchitecture
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Определяет процессор и количество бит на слово в платформе, для которой предназначен исполняемый файл.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum ProcessorArchitecture
  {
    [__DynamicallyInvokable] None,
    [__DynamicallyInvokable] MSIL,
    [__DynamicallyInvokable] X86,
    [__DynamicallyInvokable] IA64,
    [__DynamicallyInvokable] Amd64,
    [__DynamicallyInvokable] Arm,
  }
}

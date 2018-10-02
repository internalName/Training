// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.CompilationRelaxations
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Указывает параметры, контролирующие точность кода, создаваемого компилятором just-in-time (JIT) среды CLR.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [Serializable]
  public enum CompilationRelaxations
  {
    NoStringInterning = 8,
  }
}

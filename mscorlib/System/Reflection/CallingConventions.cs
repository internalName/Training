// Decompiled with JetBrains decompiler
// Type: System.Reflection.CallingConventions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Определяет допустимые соглашения о вызовах для метода.
  /// </summary>
  [Flags]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum CallingConventions
  {
    [__DynamicallyInvokable] Standard = 1,
    [__DynamicallyInvokable] VarArgs = 2,
    [__DynamicallyInvokable] Any = VarArgs | Standard, // 0x00000003
    [__DynamicallyInvokable] HasThis = 32, // 0x00000020
    [__DynamicallyInvokable] ExplicitThis = 64, // 0x00000040
  }
}

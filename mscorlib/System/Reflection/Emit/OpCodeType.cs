// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.OpCodeType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>Описывает типы инструкций MSIL.</summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public enum OpCodeType
  {
    [Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")] Annotation,
    [__DynamicallyInvokable] Macro,
    [__DynamicallyInvokable] Nternal,
    [__DynamicallyInvokable] Objmodel,
    [__DynamicallyInvokable] Prefix,
    [__DynamicallyInvokable] Primitive,
  }
}

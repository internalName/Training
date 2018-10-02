// Decompiled with JetBrains decompiler
// Type: System.Reflection.GenericParameterAttributes
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  /// <summary>
  ///   Описывает ограничения параметра универсального типа для универсального типа или метода.
  /// </summary>
  [Flags]
  [__DynamicallyInvokable]
  public enum GenericParameterAttributes
  {
    [__DynamicallyInvokable] None = 0,
    [__DynamicallyInvokable] VarianceMask = 3,
    [__DynamicallyInvokable] Covariant = 1,
    [__DynamicallyInvokable] Contravariant = 2,
    [__DynamicallyInvokable] SpecialConstraintMask = 28, // 0x0000001C
    [__DynamicallyInvokable] ReferenceTypeConstraint = 4,
    [__DynamicallyInvokable] NotNullableValueTypeConstraint = 8,
    [__DynamicallyInvokable] DefaultConstructorConstraint = 16, // 0x00000010
  }
}

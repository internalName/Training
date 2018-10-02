// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.UnsafeValueTypeAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Указывает, что тип содержит неуправляемый массив, который может переполниться.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Struct)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class UnsafeValueTypeAttribute : Attribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.CompilerServices.UnsafeValueTypeAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public UnsafeValueTypeAttribute()
    {
    }
  }
}

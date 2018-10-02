// Decompiled with JetBrains decompiler
// Type: System.FlagsAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Указывает, что перечисление может обрабатываться как битовое поле (т. е. набор флагов).
  /// </summary>
  [AttributeUsage(AttributeTargets.Enum, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class FlagsAttribute : Attribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.FlagsAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public FlagsAttribute()
    {
    }
  }
}

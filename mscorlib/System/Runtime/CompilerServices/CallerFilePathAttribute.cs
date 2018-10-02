// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.CallerFilePathAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Позволяет получить полный путь исходного файла, содержащего вызывающий объект.
  ///    Это путь к файлу во время компиляции.
  /// </summary>
  [AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class CallerFilePathAttribute : Attribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.CompilerServices.CallerFilePathAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public CallerFilePathAttribute()
    {
    }
  }
}

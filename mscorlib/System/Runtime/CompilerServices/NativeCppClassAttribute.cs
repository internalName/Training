// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.NativeCppClassAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Применяет метаданные в сборке, которая указывает, что тип является неуправляемым.
  ///     Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Struct, Inherited = true)]
  [ComVisible(true)]
  [Serializable]
  public sealed class NativeCppClassAttribute : Attribute
  {
  }
}

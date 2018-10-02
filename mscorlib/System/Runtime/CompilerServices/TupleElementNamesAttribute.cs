// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.TupleElementNamesAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Указывает, что использование кортежа значений для элемента должно обрабатываться как кортеж с именами элементов.
  /// </summary>
  [CLSCompliant(false)]
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
  public sealed class TupleElementNamesAttribute : Attribute
  {
    private readonly string[] _transformNames;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.CompilerServices.TupleElementNamesAttribute" />.
    /// </summary>
    /// <param name="transformNames">
    ///   Строковый массив, который указывает (в обходе в глубину в прямом порядке конструкции типа), каким вхождениям кортежа значений будут назначаться имена элементов.
    /// </param>
    public TupleElementNamesAttribute(string[] transformNames)
    {
      if (transformNames == null)
        throw new ArgumentNullException(nameof (transformNames));
      this._transformNames = transformNames;
    }

    /// <summary>
    ///   Указывает, в обходе в глубину в прямом порядке конструкции типа, каким элементам кортежа значений будут назначаться имена элементов.
    /// </summary>
    /// <returns>
    ///   Массив, указывающий, каким элементам кортежа значений будут назначаться имена элементов.
    /// </returns>
    public IList<string> TransformNames
    {
      get
      {
        return (IList<string>) this._transformNames;
      }
    }
  }
}

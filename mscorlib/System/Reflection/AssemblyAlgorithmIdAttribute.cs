// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyAlgorithmIdAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Configuration.Assemblies;
using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Задает алгоритм хэширования всех файлов в сборке.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  public sealed class AssemblyAlgorithmIdAttribute : Attribute
  {
    private uint m_algId;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.AssemblyAlgorithmIdAttribute" /> класса с помощью указанного хэш-алгоритма, с помощью одного из членов <see cref="T:System.Configuration.Assemblies.AssemblyHashAlgorithm" /> для представления алгоритма хеширования.
    /// </summary>
    /// <param name="algorithmId">
    ///   Член <see langword="AssemblyHashAlgorithm" /> представляющий хэш-алгоритм.
    /// </param>
    public AssemblyAlgorithmIdAttribute(AssemblyHashAlgorithm algorithmId)
    {
      this.m_algId = (uint) algorithmId;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.AssemblyAlgorithmIdAttribute" /> класса с заданным алгоритмом хеширования, используя для представления алгоритма хеширования целое число без знака.
    /// </summary>
    /// <param name="algorithmId">
    ///   Целое число без знака, представляющее алгоритм хеширования.
    /// </param>
    [CLSCompliant(false)]
    public AssemblyAlgorithmIdAttribute(uint algorithmId)
    {
      this.m_algId = algorithmId;
    }

    /// <summary>
    ///   Возвращает алгоритм хеширования содержимого манифеста сборки.
    /// </summary>
    /// <returns>
    ///   Целое число без знака, представляющее алгоритм хеширования сборки.
    /// </returns>
    [CLSCompliant(false)]
    public uint AlgorithmId
    {
      get
      {
        return this.m_algId;
      }
    }
  }
}

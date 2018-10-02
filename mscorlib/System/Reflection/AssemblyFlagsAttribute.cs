// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyFlagsAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Задает побитовое сочетание <see cref="T:System.Reflection.AssemblyNameFlags" /> флагов для сборки, описывающих параметры компилятора just-in-time (JIT), ли сборки — и имеет ли полный или измененный с использованием маркера открытого ключа.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AssemblyFlagsAttribute : Attribute
  {
    private AssemblyNameFlags m_flags;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.AssemblyFlagsAttribute" /> класса указанное сочетание <see cref="T:System.Reflection.AssemblyNameFlags" /> флаги, приводится как целое число без знака.
    /// </summary>
    /// <param name="flags">
    ///   Побитовое сочетание <see cref="T:System.Reflection.AssemblyNameFlags" /> флаги, приводится как целое число без знака, представляющее параметры компилятора just-in-time (JIT), продолжительности, ли сборка перенаправления и имеет ли полный или измененный с использованием маркера открытого ключа.
    /// </param>
    [Obsolete("This constructor has been deprecated. Please use AssemblyFlagsAttribute(AssemblyNameFlags) instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    [CLSCompliant(false)]
    public AssemblyFlagsAttribute(uint flags)
    {
      this.m_flags = (AssemblyNameFlags) flags;
    }

    /// <summary>
    ///   Возвращает целое число без знака, представляющее сочетание <see cref="T:System.Reflection.AssemblyNameFlags" /> флаги указывается при создании экземпляра атрибута.
    /// </summary>
    /// <returns>
    ///   Целое число без знака, представляющее побитовое сочетание <see cref="T:System.Reflection.AssemblyNameFlags" /> флаги.
    /// </returns>
    [Obsolete("This property has been deprecated. Please use AssemblyFlags instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    [CLSCompliant(false)]
    public uint Flags
    {
      get
      {
        return (uint) this.m_flags;
      }
    }

    /// <summary>
    ///   Возвращает целое число, представляющее сочетание <see cref="T:System.Reflection.AssemblyNameFlags" /> флаги указывается при создании экземпляра атрибута.
    /// </summary>
    /// <returns>
    ///   Целое число, представляющее побитовое сочетание <see cref="T:System.Reflection.AssemblyNameFlags" /> флаги.
    /// </returns>
    [__DynamicallyInvokable]
    public int AssemblyFlags
    {
      [__DynamicallyInvokable] get
      {
        return (int) this.m_flags;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.AssemblyFlagsAttribute" /> класса указанное сочетание <see cref="T:System.Reflection.AssemblyNameFlags" /> флаги, приведен в виде целочисленного значения.
    /// </summary>
    /// <param name="assemblyFlags">
    ///   Побитовое сочетание <see cref="T:System.Reflection.AssemblyNameFlags" /> флаги, приведение как целое значение, представляющее параметры компилятора just-in-time (JIT), продолжительности, ли сборка перенаправления и имеет ли полный или измененный с использованием маркера открытого ключа.
    /// </param>
    [Obsolete("This constructor has been deprecated. Please use AssemblyFlagsAttribute(AssemblyNameFlags) instead. http://go.microsoft.com/fwlink/?linkid=14202")]
    public AssemblyFlagsAttribute(int assemblyFlags)
    {
      this.m_flags = (AssemblyNameFlags) assemblyFlags;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.AssemblyFlagsAttribute" /> класса указанное сочетание <see cref="T:System.Reflection.AssemblyNameFlags" /> флаги.
    /// </summary>
    /// <param name="assemblyFlags">
    ///   Побитовое сочетание <see cref="T:System.Reflection.AssemblyNameFlags" /> флаги, представляющие параметры компилятора just-in-time (JIT), продолжительности, ли сборка перенаправления и имеет ли полный или измененный с использованием маркера открытого ключа.
    /// </param>
    [__DynamicallyInvokable]
    public AssemblyFlagsAttribute(AssemblyNameFlags assemblyFlags)
    {
      this.m_flags = assemblyFlags;
    }
  }
}

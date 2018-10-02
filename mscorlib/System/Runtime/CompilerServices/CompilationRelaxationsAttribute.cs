// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.CompilationRelaxationsAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Контролирует точность кода, создаваемого JIT-компилятором среды CLR.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Method)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class CompilationRelaxationsAttribute : Attribute
  {
    private int m_relaxations;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.CompilerServices.CompilationRelaxationsAttribute" /> класса заданные релаксации компиляции.
    /// </summary>
    /// <param name="relaxations">Релаксации компиляции.</param>
    [__DynamicallyInvokable]
    public CompilationRelaxationsAttribute(int relaxations)
    {
      this.m_relaxations = relaxations;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.CompilerServices.CompilationRelaxationsAttribute" /> заданным значением <see cref="T:System.Runtime.CompilerServices.CompilationRelaxations" />.
    /// </summary>
    /// <param name="relaxations">
    ///   Одно из значений <see cref="T:System.Runtime.CompilerServices.CompilationRelaxations" />.
    /// </param>
    public CompilationRelaxationsAttribute(System.Runtime.CompilerServices.CompilationRelaxations relaxations)
    {
      this.m_relaxations = (int) relaxations;
    }

    /// <summary>
    ///   Возвращает релаксации компиляции, заданные при создании текущего объекта.
    /// </summary>
    /// <returns>
    ///   Релаксации компиляции, заданные при создании текущего объекта.
    /// 
    ///   Используйте <see cref="T:System.Runtime.CompilerServices.CompilationRelaxations" /> перечисления с <see cref="P:System.Runtime.CompilerServices.CompilationRelaxationsAttribute.CompilationRelaxations" /> свойство.
    /// </returns>
    [__DynamicallyInvokable]
    public int CompilationRelaxations
    {
      [__DynamicallyInvokable] get
      {
        return this.m_relaxations;
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.LoaderOptimizationAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Используется для задания политики оптимизации загрузчика для основного метода исполняемого приложения.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  [ComVisible(true)]
  public sealed class LoaderOptimizationAttribute : Attribute
  {
    internal byte _val;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.LoaderOptimizationAttribute" /> класса с указанным значением.
    /// </summary>
    /// <param name="value">
    ///   Значение, эквивалентное значению <see cref="T:System.LoaderOptimization" /> константой.
    /// </param>
    public LoaderOptimizationAttribute(byte value)
    {
      this._val = value;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.LoaderOptimizationAttribute" /> класса с указанным значением.
    /// </summary>
    /// <param name="value">
    ///   Константа <see cref="T:System.LoaderOptimization" />.
    /// </param>
    public LoaderOptimizationAttribute(LoaderOptimization value)
    {
      this._val = (byte) value;
    }

    /// <summary>
    ///   Возвращает текущую <see cref="T:System.LoaderOptimization" /> значение для данного экземпляра.
    /// </summary>
    /// <returns>
    ///   Константа <see cref="T:System.LoaderOptimization" />.
    /// </returns>
    public LoaderOptimization Value
    {
      get
      {
        return (LoaderOptimization) this._val;
      }
    }
  }
}

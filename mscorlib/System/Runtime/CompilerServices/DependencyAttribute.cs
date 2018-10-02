// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.DependencyAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Указывает, когда должна быть загружена ссылающейся сборкой зависимость.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
  [Serializable]
  public sealed class DependencyAttribute : Attribute
  {
    private string dependentAssembly;
    private LoadHint loadHint;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.CompilerServices.DependencyAttribute" /> заданным значением <see cref="T:System.Runtime.CompilerServices.LoadHint" />.
    /// </summary>
    /// <param name="dependentAssemblyArgument">
    ///   Зависимая сборка для привязки.
    /// </param>
    /// <param name="loadHintArgument">
    ///   Одно из значений <see cref="T:System.Runtime.CompilerServices.LoadHint" />.
    /// </param>
    public DependencyAttribute(string dependentAssemblyArgument, LoadHint loadHintArgument)
    {
      this.dependentAssembly = dependentAssemblyArgument;
      this.loadHint = loadHintArgument;
    }

    /// <summary>Возвращает значение зависимой сборки.</summary>
    /// <returns>Имя зависимой сборки.</returns>
    public string DependentAssembly
    {
      get
      {
        return this.dependentAssembly;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Runtime.CompilerServices.LoadHint" /> значение, указывающее, когда загрузить зависимость сборки.
    /// </summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Runtime.CompilerServices.LoadHint" />.
    /// </returns>
    public LoadHint LoadHint
    {
      get
      {
        return this.loadHint;
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.DefaultDependencyAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Предоставляет подсказку для общеязыковой среды выполнения (CLR), указывающее, вероятно, как зависимость для загрузки.
  ///    Этот класс используется зависимой сборки, чтобы указать, какие следует применять, когда родительский не указывает <see cref="T:System.Runtime.CompilerServices.DependencyAttribute" /> атрибута.
  ///     Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly)]
  [Serializable]
  public sealed class DefaultDependencyAttribute : Attribute
  {
    private LoadHint loadHint;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.CompilerServices.DefaultDependencyAttribute" /> с заданным <see cref="T:System.Runtime.CompilerServices.LoadHint" /> привязки.
    /// </summary>
    /// <param name="loadHintArgument">
    ///   Один из <see cref="T:System.Runtime.CompilerServices.LoadHint" /> значений, указывающее приоритет привязки по умолчанию.
    /// </param>
    public DefaultDependencyAttribute(LoadHint loadHintArgument)
    {
      this.loadHint = loadHintArgument;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Runtime.CompilerServices.LoadHint" /> значение, указывающее, когда сборка загружается зависимость.
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

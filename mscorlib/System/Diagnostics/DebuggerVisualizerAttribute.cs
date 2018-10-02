// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.DebuggerVisualizerAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics
{
  /// <summary>
  ///   Указывает, что у типа есть визуализатор.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
  [ComVisible(true)]
  public sealed class DebuggerVisualizerAttribute : Attribute
  {
    private string visualizerObjectSourceName;
    private string visualizerName;
    private string description;
    private string targetName;
    private Type target;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> класса, указывая имя типа визуализатора.
    /// </summary>
    /// <param name="visualizerTypeName">
    ///   Полное имя типа визуализатора.
    /// </param>
    public DebuggerVisualizerAttribute(string visualizerTypeName)
    {
      this.visualizerName = visualizerTypeName;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> класс, указав имя типа визуализатора и имя типа источника объекта визуализатора.
    /// </summary>
    /// <param name="visualizerTypeName">
    ///   Полное имя типа визуализатора.
    /// </param>
    /// <param name="visualizerObjectSourceTypeName">
    ///   Полное имя источника объекта визуализатора.
    /// </param>
    public DebuggerVisualizerAttribute(string visualizerTypeName, string visualizerObjectSourceTypeName)
    {
      this.visualizerName = visualizerTypeName;
      this.visualizerObjectSourceName = visualizerObjectSourceTypeName;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> класса, указывая имя типа визуализатор и тип источника объекта визуализатора.
    /// </summary>
    /// <param name="visualizerTypeName">
    ///   Полное имя типа визуализатора.
    /// </param>
    /// <param name="visualizerObjectSource">
    ///   Тип источника объекта визуализатора.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="visualizerObjectSource" /> имеет значение <see langword="null" />.
    /// </exception>
    public DebuggerVisualizerAttribute(string visualizerTypeName, Type visualizerObjectSource)
    {
      if (visualizerObjectSource == (Type) null)
        throw new ArgumentNullException(nameof (visualizerObjectSource));
      this.visualizerName = visualizerTypeName;
      this.visualizerObjectSourceName = visualizerObjectSource.AssemblyQualifiedName;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" /> с указанием типа визуализатора.
    /// </summary>
    /// <param name="visualizer">Тип визуализатора.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="isualizer" /> имеет значение <see langword="null" />.
    /// </exception>
    public DebuggerVisualizerAttribute(Type visualizer)
    {
      if (visualizer == (Type) null)
        throw new ArgumentNullException(nameof (visualizer));
      this.visualizerName = visualizer.AssemblyQualifiedName;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" />, задающий тип визуализатора и тип источника объекта визуализатора.
    /// </summary>
    /// <param name="visualizer">Тип визуализатора.</param>
    /// <param name="visualizerObjectSource">
    ///   Тип источника объекта визуализатора.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="isualizer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="visualizerObjectSource" /> имеет значение <see langword="null" />.
    /// </exception>
    public DebuggerVisualizerAttribute(Type visualizer, Type visualizerObjectSource)
    {
      if (visualizer == (Type) null)
        throw new ArgumentNullException(nameof (visualizer));
      if (visualizerObjectSource == (Type) null)
        throw new ArgumentNullException(nameof (visualizerObjectSource));
      this.visualizerName = visualizer.AssemblyQualifiedName;
      this.visualizerObjectSourceName = visualizerObjectSource.AssemblyQualifiedName;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.DebuggerVisualizerAttribute" />, задающий тип визуализатора и имя типа источника объекта визуализатора.
    /// </summary>
    /// <param name="visualizer">Тип визуализатора.</param>
    /// <param name="visualizerObjectSourceTypeName">
    ///   Полное имя источника объекта визуализатора.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="isualizer" /> имеет значение <see langword="null" />.
    /// </exception>
    public DebuggerVisualizerAttribute(Type visualizer, string visualizerObjectSourceTypeName)
    {
      if (visualizer == (Type) null)
        throw new ArgumentNullException(nameof (visualizer));
      this.visualizerName = visualizer.AssemblyQualifiedName;
      this.visualizerObjectSourceName = visualizerObjectSourceTypeName;
    }

    /// <summary>
    ///   Возвращает полное имя типа визуализатора источник объекта.
    /// </summary>
    /// <returns>Полное имя источника объекта визуализатора.</returns>
    public string VisualizerObjectSourceTypeName
    {
      get
      {
        return this.visualizerObjectSourceName;
      }
    }

    /// <summary>Возвращает полное имя типа визуализатора.</summary>
    /// <returns>Визуализатор полное имя типа.</returns>
    public string VisualizerTypeName
    {
      get
      {
        return this.visualizerName;
      }
    }

    /// <summary>Возвращает или задает описание визуализатора.</summary>
    /// <returns>Описание визуализатора.</returns>
    public string Description
    {
      get
      {
        return this.description;
      }
      set
      {
        this.description = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает тип целевого объекта, когда атрибут применяется на уровне сборки.
    /// </summary>
    /// <returns>
    ///   Тип, который является целевым объектом визуализатора.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Невозможно задать значение, поскольку это <see langword="null" />.
    /// </exception>
    public Type Target
    {
      set
      {
        if (value == (Type) null)
          throw new ArgumentNullException(nameof (value));
        this.targetName = value.AssemblyQualifiedName;
        this.target = value;
      }
      get
      {
        return this.target;
      }
    }

    /// <summary>
    ///   Возвращает или задает полное имя типа, когда атрибут применяется на уровне сборки.
    /// </summary>
    /// <returns>Полное имя типа целевого объекта.</returns>
    public string TargetTypeName
    {
      set
      {
        this.targetName = value;
      }
      get
      {
        return this.targetName;
      }
    }
  }
}

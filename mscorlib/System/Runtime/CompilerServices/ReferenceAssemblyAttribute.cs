// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.ReferenceAssemblyAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Идентифицирует сборку как ссылочную сборку, которая содержит метаданные, но не исполняемый код.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class ReferenceAssemblyAttribute : Attribute
  {
    private string _description;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.CompilerServices.ReferenceAssemblyAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public ReferenceAssemblyAttribute()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.CompilerServices.ReferenceAssemblyAttribute" /> используя заданное описание.
    /// </summary>
    /// <param name="description">Описание ссылочной сборки.</param>
    [__DynamicallyInvokable]
    public ReferenceAssemblyAttribute(string description)
    {
      this._description = description;
    }

    /// <summary>Возвращает описание ссылочной сборки.</summary>
    /// <returns>Описание ссылочной сборки.</returns>
    [__DynamicallyInvokable]
    public string Description
    {
      [__DynamicallyInvokable] get
      {
        return this._description;
      }
    }
  }
}

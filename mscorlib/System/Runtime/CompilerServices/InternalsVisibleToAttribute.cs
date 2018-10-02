// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.InternalsVisibleToAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Задает, что типы, видимые обычно только в пределах текущей сборки, являются видимыми для заданной сборки.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class InternalsVisibleToAttribute : Attribute
  {
    private bool _allInternalsVisible = true;
    private string _assemblyName;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.CompilerServices.InternalsVisibleToAttribute" /> с именем заданной дружественной сборки.
    /// </summary>
    /// <param name="assemblyName">Имя дружественной сборки.</param>
    [__DynamicallyInvokable]
    public InternalsVisibleToAttribute(string assemblyName)
    {
      this._assemblyName = assemblyName;
    }

    /// <summary>
    ///   Получает имя дружественной сборки, для которой будут сделаны доступными все типы и члены типов, помеченные ключевым словом <see langword="internal" />.
    /// </summary>
    /// <returns>Строка, представляющая имя дружественной сборки.</returns>
    [__DynamicallyInvokable]
    public string AssemblyName
    {
      [__DynamicallyInvokable] get
      {
        return this._assemblyName;
      }
    }

    /// <summary>Это свойство не реализовано.</summary>
    /// <returns>Это свойство не возвращает значение.</returns>
    public bool AllInternalsVisible
    {
      get
      {
        return this._allInternalsVisible;
      }
      set
      {
        this._allInternalsVisible = value;
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblyMetadataAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  /// <summary>
  ///   Определяет пару метаданных «ключ-значение» для помеченной сборки.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class AssemblyMetadataAttribute : Attribute
  {
    private string m_key;
    private string m_value;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.AssemblyMetadataAttribute" /> используя указанные метаданные ключ и значение.
    /// </summary>
    /// <param name="key">Ключ метаданных.</param>
    /// <param name="value">Значение метаданных.</param>
    [__DynamicallyInvokable]
    public AssemblyMetadataAttribute(string key, string value)
    {
      this.m_key = key;
      this.m_value = value;
    }

    /// <summary>Возвращает ключ метаданных.</summary>
    /// <returns>Ключ метаданных.</returns>
    [__DynamicallyInvokable]
    public string Key
    {
      [__DynamicallyInvokable] get
      {
        return this.m_key;
      }
    }

    /// <summary>Возвращает значение метаданных.</summary>
    /// <returns>Значение метаданных.</returns>
    [__DynamicallyInvokable]
    public string Value
    {
      [__DynamicallyInvokable] get
      {
        return this.m_value;
      }
    }
  }
}

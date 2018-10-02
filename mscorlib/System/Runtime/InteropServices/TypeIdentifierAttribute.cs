// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TypeIdentifierAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>Обеспечивает поддержку эквивалентности типов.</summary>
  [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
  [ComVisible(false)]
  [__DynamicallyInvokable]
  public sealed class TypeIdentifierAttribute : Attribute
  {
    internal string Scope_;
    internal string Identifier_;

    /// <summary>
    ///   Создает новый экземпляр класса <see cref="T:System.Runtime.InteropServices.TypeIdentifierAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public TypeIdentifierAttribute()
    {
    }

    /// <summary>
    ///   Создает новый экземпляр <see cref="T:System.Runtime.InteropServices.TypeIdentifierAttribute" /> класса с заданной областью и идентификатор.
    /// </summary>
    /// <param name="scope">Первая строка эквивалентности типов.</param>
    /// <param name="identifier">
    ///   Вторая строка эквивалентности типов.
    /// </param>
    [__DynamicallyInvokable]
    public TypeIdentifierAttribute(string scope, string identifier)
    {
      this.Scope_ = scope;
      this.Identifier_ = identifier;
    }

    /// <summary>
    ///   Возвращает значение <paramref name="scope" /> переданный в параметр <see cref="M:System.Runtime.InteropServices.TypeIdentifierAttribute.#ctor(System.String,System.String)" /> конструктора.
    /// </summary>
    /// <returns>
    ///   Значение конструктор <paramref name="scope" /> параметр.
    /// </returns>
    [__DynamicallyInvokable]
    public string Scope
    {
      [__DynamicallyInvokable] get
      {
        return this.Scope_;
      }
    }

    /// <summary>
    ///   Возвращает значение <paramref name="identifier" /> переданный в параметр <see cref="M:System.Runtime.InteropServices.TypeIdentifierAttribute.#ctor(System.String,System.String)" /> конструктора.
    /// </summary>
    /// <returns>
    ///   Значение конструктор <paramref name="identifier" /> параметр.
    /// </returns>
    [__DynamicallyInvokable]
    public string Identifier
    {
      [__DynamicallyInvokable] get
      {
        return this.Identifier_;
      }
    }
  }
}

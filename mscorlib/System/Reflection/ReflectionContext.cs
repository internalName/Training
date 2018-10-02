// Decompiled with JetBrains decompiler
// Type: System.Reflection.ReflectionContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  /// <summary>
  ///   Представляет контекст, который может предоставлять объекты отражения.
  /// </summary>
  [__DynamicallyInvokable]
  public abstract class ReflectionContext
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.ReflectionContext" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected ReflectionContext()
    {
    }

    /// <summary>
    ///   Возвращает представление, в данном контексте отражения сборки, представленного объектом из другого контекста отражения.
    /// </summary>
    /// <param name="assembly">
    ///   Внешнее представление сборки для представления в этом контексте.
    /// </param>
    /// <returns>Представление сборки в данном контексте отражения.</returns>
    [__DynamicallyInvokable]
    public abstract Assembly MapAssembly(Assembly assembly);

    /// <summary>
    ///   Возвращает представление, в данном контексте отражения типа, представленного объектом из другого контекста отражения.
    /// </summary>
    /// <param name="type">
    ///   Внешнее представление типа для представления в этом контексте.
    /// </param>
    /// <returns>Представление типа в данном контексте отражения...</returns>
    [__DynamicallyInvokable]
    public abstract TypeInfo MapType(TypeInfo type);

    /// <summary>
    ///   Возвращает представление типа указанного объекта в данном контексте отражения.
    /// </summary>
    /// <param name="value">Объект для представления.</param>
    /// <returns>Объект, представляющий тип указанного объекта.</returns>
    [__DynamicallyInvokable]
    public virtual TypeInfo GetTypeForObject(object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      return this.MapType(value.GetType().GetTypeInfo());
    }
  }
}

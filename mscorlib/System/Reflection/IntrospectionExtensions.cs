// Decompiled with JetBrains decompiler
// Type: System.Reflection.IntrospectionExtensions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  /// <summary>
  ///   Содержит методы для преобразования <see cref="T:System.Type" /> объектов.
  /// </summary>
  [__DynamicallyInvokable]
  public static class IntrospectionExtensions
  {
    /// <summary>
    ///   Возвращает <see cref="T:System.Reflection.TypeInfo" /> представление указанного типа.
    /// </summary>
    /// <param name="type">
    ///   Тип, преобразование которого выполняется.
    /// </param>
    /// <returns>Преобразованный объект.</returns>
    [__DynamicallyInvokable]
    public static TypeInfo GetTypeInfo(this Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      return ((IReflectableType) type)?.GetTypeInfo();
    }
  }
}

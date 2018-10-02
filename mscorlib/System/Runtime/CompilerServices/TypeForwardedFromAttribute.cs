// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.TypeForwardedFromAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Указывает источник <see cref="T:System.Type" /> в другой сборке.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class TypeForwardedFromAttribute : Attribute
  {
    private string assemblyFullName;

    private TypeForwardedFromAttribute()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.CompilerServices.TypeForwardedFromAttribute" />.
    /// </summary>
    /// <param name="assemblyFullName">
    ///   Источник <see cref="T:System.Type" /> в другой сборке.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="assemblyFullName" /> имеет значение <see langword="null" /> или пуст.
    /// </exception>
    [__DynamicallyInvokable]
    public TypeForwardedFromAttribute(string assemblyFullName)
    {
      if (string.IsNullOrEmpty(assemblyFullName))
        throw new ArgumentNullException(nameof (assemblyFullName));
      this.assemblyFullName = assemblyFullName;
    }

    /// <summary>Получает квалифицированное имя типа источника.</summary>
    /// <returns>Квалифицированное имя типа источника.</returns>
    [__DynamicallyInvokable]
    public string AssemblyFullName
    {
      [__DynamicallyInvokable] get
      {
        return this.assemblyFullName;
      }
    }
  }
}

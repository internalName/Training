// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.DebuggerTypeProxyAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics
{
  /// <summary>Указывает прокси-тип отображения для типа.</summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class DebuggerTypeProxyAttribute : Attribute
  {
    private string typeName;
    private string targetName;
    private Type target;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Diagnostics.DebuggerTypeProxyAttribute" /> класса с помощью типа прокси-сервера.
    /// </summary>
    /// <param name="type">Тип прокси-сервера.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public DebuggerTypeProxyAttribute(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      this.typeName = type.AssemblyQualifiedName;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Diagnostics.DebuggerTypeProxyAttribute" /> класса, используя имя типа прокси-сервера.
    /// </summary>
    /// <param name="typeName">Введите имя прокси-типа.</param>
    [__DynamicallyInvokable]
    public DebuggerTypeProxyAttribute(string typeName)
    {
      this.typeName = typeName;
    }

    /// <summary>Возвращает имя типа прокси-типа.</summary>
    /// <returns>Введите имя прокси-типа.</returns>
    [__DynamicallyInvokable]
    public string ProxyTypeName
    {
      [__DynamicallyInvokable] get
      {
        return this.typeName;
      }
    }

    /// <summary>
    ///   Возвращает или задает тип целевого объекта для атрибута.
    /// </summary>
    /// <returns>Тип целевого объекта для атрибута.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметру <see cref="P:System.Diagnostics.DebuggerTypeProxyAttribute.Target" /> задается значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public Type Target
    {
      [__DynamicallyInvokable] set
      {
        if (value == (Type) null)
          throw new ArgumentNullException(nameof (value));
        this.targetName = value.AssemblyQualifiedName;
        this.target = value;
      }
      [__DynamicallyInvokable] get
      {
        return this.target;
      }
    }

    /// <summary>Возвращает или задает имя типа целевого объекта.</summary>
    /// <returns>Имя типа целевого объекта.</returns>
    [__DynamicallyInvokable]
    public string TargetTypeName
    {
      [__DynamicallyInvokable] get
      {
        return this.targetName;
      }
      [__DynamicallyInvokable] set
      {
        this.targetName = value;
      }
    }
  }
}

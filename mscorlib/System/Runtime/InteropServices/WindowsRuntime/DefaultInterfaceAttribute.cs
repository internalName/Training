// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.DefaultInterfaceAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.WindowsRuntime
{
  /// <summary>
  ///   Определяет интерфейс по умолчанию управляемого Среда выполнения Windows класса.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class DefaultInterfaceAttribute : Attribute
  {
    private Type m_defaultInterface;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.WindowsRuntime.DefaultInterfaceAttribute" />.
    /// </summary>
    /// <param name="defaultInterface">
    ///   Тип интерфейса, который указан как интерфейс по умолчанию для класса атрибут применяется к.
    /// </param>
    [__DynamicallyInvokable]
    public DefaultInterfaceAttribute(Type defaultInterface)
    {
      this.m_defaultInterface = defaultInterface;
    }

    /// <summary>Возвращает тип интерфейса по умолчанию.</summary>
    /// <returns>Тип интерфейса по умолчанию.</returns>
    [__DynamicallyInvokable]
    public Type DefaultInterface
    {
      [__DynamicallyInvokable] get
      {
        return this.m_defaultInterface;
      }
    }
  }
}

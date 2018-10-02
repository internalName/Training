// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.StateMachineAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Позволяет определить, является ли метод методом конечного компьютера.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  [Serializable]
  public class StateMachineAttribute : Attribute
  {
    /// <summary>
    ///   Возвращает объект типа для базового типа состояния компьютера, созданного компилятором для реализации методом конечного компьютера.
    /// </summary>
    /// <returns>
    ///   Возвращает объект типа для базового типа состояния компьютера, созданного компилятором для реализации методом конечного компьютера.
    /// </returns>
    [__DynamicallyInvokable]
    public Type StateMachineType { [__DynamicallyInvokable] get; private set; }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.CompilerServices.StateMachineAttribute" />.
    /// </summary>
    /// <param name="stateMachineType">
    ///   Тип объекта для базового типа состояния компьютера, созданного компилятором для реализации методом конечного компьютера.
    /// </param>
    [__DynamicallyInvokable]
    public StateMachineAttribute(Type stateMachineType)
    {
      this.StateMachineType = stateMachineType;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.AsyncStateMachineAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Указывает, помечен ли метод с помощью Async (Visual Basic) или async (справочник по C#) модификатор.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class AsyncStateMachineAttribute : StateMachineAttribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.CompilerServices.AsyncStateMachineAttribute" />.
    /// </summary>
    /// <param name="stateMachineType">
    ///   Тип объекта для базового типа состояния компьютера, который используется для реализации методом конечного компьютера.
    /// </param>
    [__DynamicallyInvokable]
    public AsyncStateMachineAttribute(Type stateMachineType)
      : base(stateMachineType)
    {
    }
  }
}

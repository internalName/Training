// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.IteratorStateMachineAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Указывает, помечен ли метод в Visual Basic с <see langword="Iterator" /> модификатор.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class IteratorStateMachineAttribute : StateMachineAttribute
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.CompilerServices.IteratorStateMachineAttribute" />.
    /// </summary>
    /// <param name="stateMachineType">
    ///   Тип объекта для базового типа состояния компьютера, который используется для реализации методом конечного компьютера.
    /// </param>
    [__DynamicallyInvokable]
    public IteratorStateMachineAttribute(Type stateMachineType)
      : base(stateMachineType)
    {
    }
  }
}

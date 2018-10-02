// Decompiled with JetBrains decompiler
// Type: System.Reflection.AmbiguousMatchException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
  /// <summary>
  ///   Исключение, создаваемое, когда привязка члена приводит к тому, что критерию связывания соответствуют несколько членов.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class AmbiguousMatchException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.AmbiguousMatchException" /> с пустой строкой сообщения и корневой причиной исключения <see langword="null" />.
    /// </summary>
    [__DynamicallyInvokable]
    public AmbiguousMatchException()
      : base(Environment.GetResourceString("RFLCT.Ambiguous"))
    {
      this.SetErrorCode(-2147475171);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.AmbiguousMatchException" /> класс со строкой сообщения, установленной на данное сообщение и исключение корневой причины, значение <see langword="null" />.
    /// </summary>
    /// <param name="message">
    ///   Строка с указанием причины возникновения этого исключения.
    /// </param>
    [__DynamicallyInvokable]
    public AmbiguousMatchException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147475171);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.AmbiguousMatchException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public AmbiguousMatchException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2147475171);
    }

    internal AmbiguousMatchException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}

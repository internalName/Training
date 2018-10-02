// Decompiled with JetBrains decompiler
// Type: System.MulticastNotSupportedException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, которое выдается при попытке объединить два делегата на основе <see cref="T:System.Delegate" /> введите вместо <see cref="T:System.MulticastDelegate" /> типа.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class MulticastNotSupportedException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MulticastNotSupportedException" />.
    /// </summary>
    public MulticastNotSupportedException()
      : base(Environment.GetResourceString("Arg_MulticastNotSupportedException"))
    {
      this.SetErrorCode(-2146233068);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MulticastNotSupportedException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    public MulticastNotSupportedException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233068);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MulticastNotSupportedException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="inner" /> не является указателем null (<see langword="Nothing" /> в Visual Basic), то текущее исключение создается в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public MulticastNotSupportedException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233068);
    }

    internal MulticastNotSupportedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}

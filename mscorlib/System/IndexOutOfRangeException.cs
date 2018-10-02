// Decompiled with JetBrains decompiler
// Type: System.IndexOutOfRangeException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, возникающее при попытке обращения к элементу массива или коллекции с индексом, который находится вне границ.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class IndexOutOfRangeException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IndexOutOfRangeException" />.
    /// </summary>
    [__DynamicallyInvokable]
    public IndexOutOfRangeException()
      : base(Environment.GetResourceString("Arg_IndexOutOfRangeException"))
    {
      this.SetErrorCode(-2146233080);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IndexOutOfRangeException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    [__DynamicallyInvokable]
    public IndexOutOfRangeException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233080);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IndexOutOfRangeException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="innerException" /> не является указателем null (<see langword="Nothing" /> в Visual Basic), то текущее исключение создается в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public IndexOutOfRangeException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233080);
    }

    internal IndexOutOfRangeException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Reflection.TargetException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
  /// <summary>
  ///   Представляет исключение, которое возникает при попытке вызвать недопустимый адресат.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class TargetException : ApplicationException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.TargetException" /> с помощью пустого сообщения и исключения, представляющего первопричину.
    /// </summary>
    public TargetException()
    {
      this.SetErrorCode(-2146232829);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.TargetException" /> с помощью заданного сообщения и исключения, представляющего первопричину.
    /// </summary>
    /// <param name="message">
    ///   Значение типа <see langword="String" />, описывающее причину возникновения исключения.
    /// </param>
    public TargetException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146232829);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.TargetException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public TargetException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146232829);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.TargetException" /> класс, содержащий указанную информацию сериализации и контекст.
    /// </summary>
    /// <param name="info">
    ///   Данные для сериализации или десериализации объекта.
    /// </param>
    /// <param name="context">Источник и назначение для объекта.</param>
    protected TargetException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}

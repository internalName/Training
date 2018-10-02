// Decompiled with JetBrains decompiler
// Type: System.ArgumentNullException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>
  ///   Исключение, которое создается при передаче пустой ссылки (<see langword="Nothing" /> в Visual Basic) методу, который не принимает ее как допустимый аргумент.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class ArgumentNullException : ArgumentException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ArgumentNullException" />.
    /// </summary>
    [__DynamicallyInvokable]
    public ArgumentNullException()
      : base(Environment.GetResourceString("ArgumentNull_Generic"))
    {
      this.SetErrorCode(-2147467261);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.ArgumentNullException" /> класс с именем параметра, ставшего причиной этого исключения.
    /// </summary>
    /// <param name="paramName">
    ///   Имя параметра, вызвавшего данное исключение.
    /// </param>
    [__DynamicallyInvokable]
    public ArgumentNullException(string paramName)
      : base(Environment.GetResourceString("ArgumentNull_Generic"), paramName)
    {
      this.SetErrorCode(-2147467261);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.ArgumentNullException" /> класс с указанным сообщением об ошибке и исключение, которое стало причиной данного исключения.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке с объяснением причины исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, вызвавшее текущее исключение, или пустая ссылка (<see langword="Nothing" /> в Visual Basic), если внутреннее исключение не задано.
    /// </param>
    [__DynamicallyInvokable]
    public ArgumentNullException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147467261);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ArgumentNullException" /> класса с указанным сообщением об ошибке и именем параметра, ставшего причиной этого исключения.
    /// </summary>
    /// <param name="paramName">
    ///   Имя параметра, вызвавшего данное исключение.
    /// </param>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    [__DynamicallyInvokable]
    public ArgumentNullException(string paramName, string message)
      : base(message, paramName)
    {
      this.SetErrorCode(-2147467261);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ArgumentNullException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Объект, описывающий источник или цель сериализованных данных.
    /// </param>
    [SecurityCritical]
    protected ArgumentNullException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}

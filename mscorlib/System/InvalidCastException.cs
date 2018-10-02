﻿// Decompiled with JetBrains decompiler
// Type: System.InvalidCastException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, которое выдается в случае недопустимого приведения или явного преобразования.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class InvalidCastException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.InvalidCastException" />.
    /// </summary>
    [__DynamicallyInvokable]
    public InvalidCastException()
      : base(Environment.GetResourceString("Arg_InvalidCastException"))
    {
      this.SetErrorCode(-2147467262);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.InvalidCastException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    [__DynamicallyInvokable]
    public InvalidCastException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147467262);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.InvalidCastException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public InvalidCastException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147467262);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.InvalidCastException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected InvalidCastException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.InvalidCastException" /> класса с указанным сообщением и кодом ошибки.
    /// </summary>
    /// <param name="message">
    ///   Произошла сообщение, указывающее причину исключения.
    /// </param>
    /// <param name="errorCode">
    ///   Код ошибки (HRESULT) значение связанного с исключением.
    /// </param>
    [__DynamicallyInvokable]
    public InvalidCastException(string message, int errorCode)
      : base(message)
    {
      this.SetErrorCode(errorCode);
    }
  }
}

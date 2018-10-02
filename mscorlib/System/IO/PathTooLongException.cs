﻿// Decompiled with JetBrains decompiler
// Type: System.IO.PathTooLongException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
  /// <summary>
  ///   Исключение, которое возникает, когда путь или полное имя файла длиннее, чем максимальная длина, определенная системой.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class PathTooLongException : IOException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.PathTooLongException" /> класса HRESULT, установленной в значение COR_E_PATHTOOLONG.
    /// </summary>
    [__DynamicallyInvokable]
    public PathTooLongException()
      : base(Environment.GetResourceString("IO.PathTooLong"))
    {
      this.SetErrorCode(-2147024690);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.PathTooLongException" /> класса со строкой сообщения, значение <paramref name="message" /> и HRESULT присвоено значение COR_E_PATHTOOLONG.
    /// </summary>
    /// <param name="message">
    ///   Строка <see cref="T:System.String" />, описывающая ошибку.
    ///    Содержимое <paramref name="message" /> должно быть понятно пользователю.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    [__DynamicallyInvokable]
    public PathTooLongException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147024690);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.PathTooLongException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Строка <see cref="T:System.String" />, описывающая ошибку.
    ///    Содержимое <paramref name="message" /> должно быть понятно пользователю.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public PathTooLongException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147024690);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.PathTooLongException" /> класс, содержащий указанную информацию сериализации и контекст.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта о созданном исключении.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    protected PathTooLongException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}

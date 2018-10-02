// Decompiled with JetBrains decompiler
// Type: System.NotImplementedException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Это исключение выбрасывается, когда запрошенный метод или операция не реализованы.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class NotImplementedException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.NotImplementedException" /> стандартными свойствами.
    /// </summary>
    [__DynamicallyInvokable]
    public NotImplementedException()
      : base(Environment.GetResourceString("Arg_NotImplementedException"))
    {
      this.SetErrorCode(-2147467263);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.NotImplementedException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    [__DynamicallyInvokable]
    public NotImplementedException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147467263);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.NotImplementedException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public NotImplementedException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2147467263);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.NotImplementedException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта о созданном исключении.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    protected NotImplementedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}

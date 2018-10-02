// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.KeyNotFoundException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Collections.Generic
{
  /// <summary>
  ///   Исключение, которое выдается, когда ключ, указанный для доступа к элементу в коллекции, не совпадает ни с одним ключом в коллекции.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class KeyNotFoundException : SystemException, ISerializable
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.Generic.KeyNotFoundException" /> с использованием значения по умолчанию.
    /// </summary>
    [__DynamicallyInvokable]
    public KeyNotFoundException()
      : base(Environment.GetResourceString("Arg_KeyNotFound"))
    {
      this.SetErrorCode(-2146232969);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.Generic.KeyNotFoundException" /> указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    [__DynamicallyInvokable]
    public KeyNotFoundException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146232969);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Collections.Generic.KeyNotFoundException" /> класса с указанным сообщением об ошибке и ссылкой на внутреннее исключение, которое стало причиной данного исключения.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public KeyNotFoundException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146232969);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Collections.Generic.KeyNotFoundException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта о созданном исключении.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или месте назначения.
    /// </param>
    protected KeyNotFoundException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}

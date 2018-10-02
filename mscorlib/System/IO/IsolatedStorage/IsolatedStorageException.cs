// Decompiled with JetBrains decompiler
// Type: System.IO.IsolatedStorage.IsolatedStorageException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO.IsolatedStorage
{
  /// <summary>
  ///   Исключение вызывается при сбое операции в изолированном хранилище.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class IsolatedStorageException : Exception
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> стандартными свойствами.
    /// </summary>
    public IsolatedStorageException()
      : base(Environment.GetResourceString("IsolatedStorage_Exception"))
    {
      this.SetErrorCode(-2146233264);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    public IsolatedStorageException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233264);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public IsolatedStorageException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233264);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.IsolatedStorage.IsolatedStorageException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected IsolatedStorageException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}

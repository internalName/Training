// Decompiled with JetBrains decompiler
// Type: System.AppDomainUnloadedException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System
{
  /// <summary>
  ///   Исключение, выбрасываемое при попытке доступа к выгруженному домену приложения.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class AppDomainUnloadedException : SystemException
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.AppDomainUnloadedException" />.
    /// </summary>
    public AppDomainUnloadedException()
      : base(Environment.GetResourceString("Arg_AppDomainUnloadedException"))
    {
      this.SetErrorCode(-2146234348);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.AppDomainUnloadedException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    public AppDomainUnloadedException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146234348);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.AppDomainUnloadedException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="innerException" /> не является указателем NULL, текущее исключение возникло в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public AppDomainUnloadedException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146234348);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.AppDomainUnloadedException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected AppDomainUnloadedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}

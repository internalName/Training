// Decompiled with JetBrains decompiler
// Type: System.NotFiniteNumberException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>
  ///   Исключение, которое возникает, когда значение с плавающей запятой является плюс бесконечностью, отрицательной бесконечностью или не является числовым (NaN).
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class NotFiniteNumberException : ArithmeticException
  {
    private double _offendingNumber;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.NotFiniteNumberException" />.
    /// </summary>
    public NotFiniteNumberException()
      : base(Environment.GetResourceString("Arg_NotFiniteNumberException"))
    {
      this._offendingNumber = 0.0;
      this.SetErrorCode(-2146233048);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.NotFiniteNumberException" /> класса с недопустимым числом.
    /// </summary>
    /// <param name="offendingNumber">
    ///   Значение аргумента, вызвавшего исключение.
    /// </param>
    public NotFiniteNumberException(double offendingNumber)
    {
      this._offendingNumber = offendingNumber;
      this.SetErrorCode(-2146233048);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.NotFiniteNumberException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    public NotFiniteNumberException(string message)
      : base(message)
    {
      this._offendingNumber = 0.0;
      this.SetErrorCode(-2146233048);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.NotFiniteNumberException" /> с указанным сообщением об ошибке и недопустимое число.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    /// <param name="offendingNumber">
    ///   Значение аргумента, вызвавшего исключение.
    /// </param>
    public NotFiniteNumberException(string message, double offendingNumber)
      : base(message)
    {
      this._offendingNumber = offendingNumber;
      this.SetErrorCode(-2146233048);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.NotFiniteNumberException" /> класса с указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="innerException" /> не является указателем null (<see langword="Nothing" /> в Visual Basic), то текущее исключение создается в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public NotFiniteNumberException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146233048);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.NotFiniteNumberException" /> класса с указанным сообщением об ошибке, недопустимым числом и ссылку на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="offendingNumber">
    ///   Значение аргумента, вызвавшего исключение.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="innerException" /> не является указателем null (<see langword="Nothing" /> в Visual Basic), то текущее исключение создается в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public NotFiniteNumberException(string message, double offendingNumber, Exception innerException)
      : base(message, innerException)
    {
      this._offendingNumber = offendingNumber;
      this.SetErrorCode(-2146233048);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.NotFiniteNumberException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected NotFiniteNumberException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this._offendingNumber = (double) info.GetInt32(nameof (OffendingNumber));
    }

    /// <summary>
    ///   Возвращает недопустимое число, которое является плюс бесконечностью, минус бесконечностью или не является числовым (NaN).
    /// </summary>
    /// <returns>Недопустимое число.</returns>
    public double OffendingNumber
    {
      get
      {
        return this._offendingNumber;
      }
    }

    /// <summary>
    ///   Наборы <see cref="T:System.Runtime.Serialization.SerializationInfo" /> объект с недопустимым числом и дополнительными сведениями об исключении.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="info" /> Объект <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      base.GetObjectData(info, context);
      info.AddValue("OffendingNumber", (object) this._offendingNumber, typeof (int));
    }
  }
}

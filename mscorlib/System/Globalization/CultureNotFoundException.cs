// Decompiled with JetBrains decompiler
// Type: System.Globalization.CultureNotFoundException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Globalization
{
  /// <summary>
  ///   Исключение, возникающее при вызове метода, который осуществляет попытку создать недоступные язык и региональные параметры.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class CultureNotFoundException : ArgumentException, ISerializable
  {
    private string m_invalidCultureName;
    private int? m_invalidCultureId;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Globalization.CultureNotFoundException" /> строкой сообщений, настроенной на отображение предоставляемого системой сообщения.
    /// </summary>
    [__DynamicallyInvokable]
    public CultureNotFoundException()
      : base(CultureNotFoundException.DefaultMessage)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Globalization.CultureNotFoundException" /> указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, отображаемое с этим исключением.
    /// </param>
    [__DynamicallyInvokable]
    public CultureNotFoundException(string message)
      : base(message)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Globalization.CultureNotFoundException" /> заданным сообщением об ошибке и именем параметра, ставшего причиной этого исключения.
    /// </summary>
    /// <param name="paramName">
    ///   Имя параметра, вызвавшего текущее исключение.
    /// </param>
    /// <param name="message">
    ///   Сообщение об ошибке, отображаемое с этим исключением.
    /// </param>
    [__DynamicallyInvokable]
    public CultureNotFoundException(string paramName, string message)
      : base(message, paramName)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Globalization.CultureNotFoundException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, отображаемое с этим исключением.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="innerException" /> не является указателем NULL, текущее исключение возникло в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public CultureNotFoundException(string message, Exception innerException)
      : base(message, innerException)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Globalization.CultureNotFoundException" /> заданным сообщением об ошибке, недействительным идентификатором языка и региональных параметров и именем параметра, ставшего причиной этого исключения.
    /// </summary>
    /// <param name="paramName">
    ///   Имя параметра, вызвавшего текущее исключение.
    /// </param>
    /// <param name="invalidCultureId">
    ///   Идентификатор языка и региональных параметров, который не удается найти.
    /// </param>
    /// <param name="message">
    ///   Сообщение об ошибке, отображаемое с этим исключением.
    /// </param>
    public CultureNotFoundException(string paramName, int invalidCultureId, string message)
      : base(message, paramName)
    {
      this.m_invalidCultureId = new int?(invalidCultureId);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Globalization.CultureNotFoundException" /> заданным сообщением об ошибке, недопустимым идентификатором языка и региональных параметров и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, отображаемое с этим исключением.
    /// </param>
    /// <param name="invalidCultureId">
    ///   Идентификатор языка и региональных параметров, который не удается найти.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="innerException" /> не является указателем NULL, текущее исключение возникло в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public CultureNotFoundException(string message, int invalidCultureId, Exception innerException)
      : base(message, innerException)
    {
      this.m_invalidCultureId = new int?(invalidCultureId);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Globalization.CultureNotFoundException" /> заданным сообщением об ошибке, недействительным именем языка и региональных параметров и именем параметра, ставшего причиной этого исключения.
    /// </summary>
    /// <param name="paramName">
    ///   Имя параметра, вызвавшего текущее исключение.
    /// </param>
    /// <param name="invalidCultureName">
    ///   Имя языка и региональных параметров, которое не удается найти.
    /// </param>
    /// <param name="message">
    ///   Сообщение об ошибке, отображаемое с этим исключением.
    /// </param>
    [__DynamicallyInvokable]
    public CultureNotFoundException(string paramName, string invalidCultureName, string message)
      : base(message, paramName)
    {
      this.m_invalidCultureName = invalidCultureName;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Globalization.CultureNotFoundException" /> заданным сообщением об ошибке, недопустимым именем языка и региональных параметров и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, отображаемое с этим исключением.
    /// </param>
    /// <param name="invalidCultureName">
    ///   Имя языка и региональных параметров, которое не удается найти.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="innerException" /> не является указателем NULL, текущее исключение возникло в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public CultureNotFoundException(string message, string invalidCultureName, Exception innerException)
      : base(message, innerException)
    {
      this.m_invalidCultureName = invalidCultureName;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Globalization.CultureNotFoundException" />, используя указанные данные сериализации и контекст.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected CultureNotFoundException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.m_invalidCultureId = (int?) info.GetValue(nameof (InvalidCultureId), typeof (int?));
      this.m_invalidCultureName = (string) info.GetValue(nameof (InvalidCultureName), typeof (string));
    }

    /// <summary>
    ///   Задает объекту <see cref="T:System.Runtime.Serialization.SerializationInfo" /> имя параметра и дополнительную информацию об исключении.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      base.GetObjectData(info, context);
      int? nullable = new int?();
      int? invalidCultureId = this.m_invalidCultureId;
      info.AddValue("InvalidCultureId", (object) invalidCultureId, typeof (int?));
      info.AddValue("InvalidCultureName", (object) this.m_invalidCultureName, typeof (string));
    }

    /// <summary>
    ///   Возвращает идентификатор языка и региональных параметров, который не удается найти.
    /// </summary>
    /// <returns>
    ///   Недействительный идентификатор языка и региональных параметров.
    /// </returns>
    public virtual int? InvalidCultureId
    {
      get
      {
        return this.m_invalidCultureId;
      }
    }

    /// <summary>
    ///   Возвращает имя языка и региональных параметров, которое не удается найти.
    /// </summary>
    /// <returns>
    ///   Недействительное имя языка и региональных параметров.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual string InvalidCultureName
    {
      [__DynamicallyInvokable] get
      {
        return this.m_invalidCultureName;
      }
    }

    private static string DefaultMessage
    {
      get
      {
        return Environment.GetResourceString("Argument_CultureNotSupported");
      }
    }

    private string FormatedInvalidCultureId
    {
      get
      {
        if (this.InvalidCultureId.HasValue)
          return string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0} (0x{0:x4})", (object) this.InvalidCultureId.Value);
        return this.InvalidCultureName;
      }
    }

    /// <summary>
    ///   Возвращает сообщение об ошибке с объяснением причин исключения.
    /// </summary>
    /// <returns>Текстовая строка с подробным описанием исключения.</returns>
    [__DynamicallyInvokable]
    public override string Message
    {
      [__DynamicallyInvokable] get
      {
        string message = base.Message;
        if (!this.m_invalidCultureId.HasValue && this.m_invalidCultureName == null)
          return message;
        string resourceString = Environment.GetResourceString("Argument_CultureInvalidIdentifier", (object) this.FormatedInvalidCultureId);
        if (message == null)
          return resourceString;
        return message + Environment.NewLine + resourceString;
      }
    }
  }
}

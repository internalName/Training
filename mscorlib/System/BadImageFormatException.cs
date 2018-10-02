// Decompiled with JetBrains decompiler
// Type: System.BadImageFormatException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System
{
  /// <summary>
  ///   Исключение, которое выдается при недопустимом образе файла библиотеки динамической компоновки (DLL) или выполняемой программы.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class BadImageFormatException : SystemException
  {
    private string _fileName;
    private string _fusionLog;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.BadImageFormatException" />.
    /// </summary>
    [__DynamicallyInvokable]
    public BadImageFormatException()
      : base(Environment.GetResourceString("Arg_BadImageFormatException"))
    {
      this.SetErrorCode(-2147024885);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.BadImageFormatException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    [__DynamicallyInvokable]
    public BadImageFormatException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147024885);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.BadImageFormatException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="inner" /> не является указателем NULL, текущее исключение возникло в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public BadImageFormatException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2147024885);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.BadImageFormatException" /> указанной ошибки сообщения и имя класса.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    /// <param name="fileName">
    ///   Полное имя файла с недопустимым изображением.
    /// </param>
    [__DynamicallyInvokable]
    public BadImageFormatException(string message, string fileName)
      : base(message)
    {
      this.SetErrorCode(-2147024885);
      this._fileName = fileName;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.BadImageFormatException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="fileName">
    ///   Полное имя файла с недопустимым изображением.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public BadImageFormatException(string message, string fileName, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2147024885);
      this._fileName = fileName;
    }

    /// <summary>
    ///   Получает сообщение об ошибке и имя файла, вызвавшего данное исключение.
    /// </summary>
    /// <returns>
    ///   Строка, содержащая сообщение об ошибке и имя файла, вызвавшего данное исключение.
    /// </returns>
    [__DynamicallyInvokable]
    public override string Message
    {
      [__DynamicallyInvokable] get
      {
        this.SetMessageField();
        return this._message;
      }
    }

    private void SetMessageField()
    {
      if (this._message != null)
        return;
      if (this._fileName == null && this.HResult == -2146233088)
        this._message = Environment.GetResourceString("Arg_BadImageFormatException");
      else
        this._message = FileLoadException.FormatFileLoadExceptionMessage(this._fileName, this.HResult);
    }

    /// <summary>Возвращает имя файла, вызвавшего данное исключение.</summary>
    /// <returns>
    ///   Имя файла с недопустимым образом или пустая ссылка, если имя файла не было передано конструктору для текущего экземпляра.
    /// </returns>
    [__DynamicallyInvokable]
    public string FileName
    {
      [__DynamicallyInvokable] get
      {
        return this._fileName;
      }
    }

    /// <summary>
    ///   Возвращает полное имя данного исключения и, возможно, сообщение об ошибке, имя внутреннего исключения и трассировку стека.
    /// </summary>
    /// <returns>
    ///   Строка, содержащая полное имя данного исключения и, возможно, сообщение об ошибке, имя внутреннего исключения и трассировку стека.
    /// </returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      string str = this.GetType().FullName + ": " + this.Message;
      if (this._fileName != null && this._fileName.Length != 0)
        str = str + Environment.NewLine + Environment.GetResourceString("IO.FileName_Name", (object) this._fileName);
      if (this.InnerException != null)
        str = str + " ---> " + this.InnerException.ToString();
      if (this.StackTrace != null)
        str = str + Environment.NewLine + this.StackTrace;
      try
      {
        if (this.FusionLog != null)
        {
          if (str == null)
            str = " ";
          str += Environment.NewLine;
          str += Environment.NewLine;
          str += this.FusionLog;
        }
      }
      catch (SecurityException ex)
      {
      }
      return str;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.BadImageFormatException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, хранящий сериализованные данные объекта, относящиеся к выдаваемому исключению.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    protected BadImageFormatException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this._fileName = info.GetString("BadImageFormat_FileName");
      try
      {
        this._fusionLog = info.GetString("BadImageFormat_FusionLog");
      }
      catch
      {
        this._fusionLog = (string) null;
      }
    }

    private BadImageFormatException(string fileName, string fusionLog, int hResult)
      : base((string) null)
    {
      this.SetErrorCode(hResult);
      this._fileName = fileName;
      this._fusionLog = fusionLog;
      this.SetMessageField();
    }

    /// <summary>
    ///   Возвращает имя журнала, в котором описано, почему загрузка сборки не выполнена.
    /// </summary>
    /// <returns>
    ///   A <see langword="String" /> содержащая ошибки, сообщенные кэшем сборок.
    /// </returns>
    public string FusionLog
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)] get
      {
        return this._fusionLog;
      }
    }

    /// <summary>
    ///   Наборы <see cref="T:System.Runtime.Serialization.SerializationInfo" /> объект с именем файла, журнал кэша сборок и дополнительными сведениями об исключении.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта о созданном исключении.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
      info.AddValue("BadImageFormat_FileName", (object) this._fileName, typeof (string));
      try
      {
        info.AddValue("BadImageFormat_FusionLog", (object) this.FusionLog, typeof (string));
      }
      catch (SecurityException ex)
      {
      }
    }
  }
}

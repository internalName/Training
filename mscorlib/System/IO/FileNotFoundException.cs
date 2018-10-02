// Decompiled with JetBrains decompiler
// Type: System.IO.FileNotFoundException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
  /// <summary>
  ///   Исключение, которое выдается при попытке получить доступ к файлу или каталогу, которых нет на диске.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class FileNotFoundException : IOException
  {
    private string _fileName;
    private string _fusionLog;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileNotFoundException" />, причем для его строки сообщения задано значение системное сообщение, а HRESULT имеет значение COR_E_FILENOTFOUND.
    /// </summary>
    [__DynamicallyInvokable]
    public FileNotFoundException()
      : base(Environment.GetResourceString("IO.FileNotFound"))
    {
      this.SetErrorCode(-2147024894);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileNotFoundException" />, причем для его строки сообщения задано значение <paramref name="message" />, а свойство HRESULT имеет значение COR_E_FILENOTFOUND.
    /// </summary>
    /// <param name="message">
    ///   Описание ошибки.
    ///    Содержимое <paramref name="message" /> должно быть понятно пользователю.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    [__DynamicallyInvokable]
    public FileNotFoundException(string message)
      : base(message)
    {
      this.SetErrorCode(-2147024894);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileNotFoundException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Описание ошибки.
    ///    Содержимое <paramref name="message" /> должно быть понятно пользователю.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public FileNotFoundException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147024894);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileNotFoundException" /> так, что для его строки сообщения задано значение <paramref name="message" />, указывающее имя файла, который не удалось найти, а HRESULT имеет значение COR_E_FILENOTFOUND.
    /// </summary>
    /// <param name="message">
    ///   Описание ошибки.
    ///    Содержимое <paramref name="message" /> должно быть понятно пользователю.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    /// <param name="fileName">
    ///   Полное имя файла с недопустимым изображением.
    /// </param>
    [__DynamicallyInvokable]
    public FileNotFoundException(string message, string fileName)
      : base(message)
    {
      this.SetErrorCode(-2147024894);
      this._fileName = fileName;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileNotFoundException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="fileName">
    ///   Полное имя файла с недопустимым изображением.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public FileNotFoundException(string message, string fileName, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2147024894);
      this._fileName = fileName;
    }

    /// <summary>
    ///   Возвращает сообщение об ошибке с объяснением причин исключения.
    /// </summary>
    /// <returns>Сообщение об ошибке.</returns>
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
      {
        this._message = Environment.GetResourceString("IO.FileNotFound");
      }
      else
      {
        if (this._fileName == null)
          return;
        this._message = FileLoadException.FormatFileLoadExceptionMessage(this._fileName, this.HResult);
      }
    }

    /// <summary>Получает имя файла, который не удается найти.</summary>
    /// <returns>
    ///   Имя файла или <see langword="null" />, если имя файла не было передано в конструктор для данного экземпляра.
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
    ///   Полное имя данного исключения и, возможно, сообщение об ошибке, имя внутреннего исключения и трассировка стека.
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
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.FileNotFoundException" /> класс, содержащий указанную информацию сериализации и контекст.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта о возникающем исключении.
    /// </param>
    /// <param name="context">
    ///   Объект, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    protected FileNotFoundException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this._fileName = info.GetString("FileNotFound_FileName");
      try
      {
        this._fusionLog = info.GetString("FileNotFound_FusionLog");
      }
      catch
      {
        this._fusionLog = (string) null;
      }
    }

    private FileNotFoundException(string fileName, string fusionLog, int hResult)
      : base((string) null)
    {
      this.SetErrorCode(hResult);
      this._fileName = fileName;
      this._fusionLog = fusionLog;
      this.SetMessageField();
    }

    /// <summary>
    ///   Возвращает имя журнала, описывающий, почему загрузка сборки не удалось.
    /// </summary>
    /// <returns>Ошибки, сообщенные кэшем сборок.</returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public string FusionLog
    {
      [SecuritySafeCritical, SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy)] get
      {
        return this._fusionLog;
      }
    }

    /// <summary>
    ///   Устанавливает объект <see cref="T:System.Runtime.Serialization.SerializationInfo" /> с именем файла и дополнительными сведениями об исключении.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта о возникающем исключении.
    /// </param>
    /// <param name="context">
    ///   Объект, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
      info.AddValue("FileNotFound_FileName", (object) this._fileName, typeof (string));
      try
      {
        info.AddValue("FileNotFound_FusionLog", (object) this.FusionLog, typeof (string));
      }
      catch (SecurityException ex)
      {
      }
    }
  }
}

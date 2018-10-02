// Decompiled with JetBrains decompiler
// Type: System.IO.FileLoadException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.IO
{
  /// <summary>
  ///   Исключение, которое создается в случае, когда управляемая сборка найдена, но не может быть загружена.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class FileLoadException : IOException
  {
    private string _fileName;
    private string _fusionLog;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.FileLoadException" /> классе и задайте <see cref="P:System.Exception.Message" /> Свойства нового экземпляра системное сообщение с описанием ошибки, например «не удалось загрузить указанный файл.»
    ///    Это сообщение учитывает текущую культуру системы.
    /// </summary>
    [__DynamicallyInvokable]
    public FileLoadException()
      : base(Environment.GetResourceString("IO.FileLoad"))
    {
      this.SetErrorCode(-2146232799);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileLoadException" /> указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Строка <see cref="T:System.String" />, описывающая ошибку.
    ///    Содержимое <paramref name="message" /> должно быть понятно пользователю.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    [__DynamicallyInvokable]
    public FileLoadException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146232799);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileLoadException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Строка <see cref="T:System.String" />, описывающая ошибку.
    ///    Содержимое <paramref name="message" /> должно быть понятно пользователю.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public FileLoadException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146232799);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.FileLoadException" /> класса с указанным сообщением об ошибке и имя файла, который не может быть загружен.
    /// </summary>
    /// <param name="message">
    ///   Строка <see cref="T:System.String" />, описывающая ошибку.
    ///    Содержимое <paramref name="message" /> должно быть понятно пользователю.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    /// <param name="fileName">
    ///   A <see cref="T:System.String" /> содержащий имя файла, который не был загружен.
    /// </param>
    [__DynamicallyInvokable]
    public FileLoadException(string message, string fileName)
      : base(message)
    {
      this.SetErrorCode(-2146232799);
      this._fileName = fileName;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.IO.FileLoadException" /> класса с указанным сообщением об ошибке, имя файла, который не может быть загружен и ссылку на внутреннее исключение, которое стало причиной данного исключения.
    /// </summary>
    /// <param name="message">
    ///   Строка <see cref="T:System.String" />, описывающая ошибку.
    ///    Содержимое <paramref name="message" /> должно быть понятно пользователю.
    ///    Код, вызывающий этот конструктор, должен обеспечить локализацию данной строки в соответствии с текущим языком и региональными параметрами системы.
    /// </param>
    /// <param name="fileName">
    ///   A <see cref="T:System.String" /> содержащий имя файла, который не был загружен.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public FileLoadException(string message, string fileName, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146232799);
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
      this._message = FileLoadException.FormatFileLoadExceptionMessage(this._fileName, this.HResult);
    }

    /// <summary>Возвращает имя файла, вызвавшего данное исключение.</summary>
    /// <returns>
    ///   A <see cref="T:System.String" /> содержащий имя файла с недопустимым образом или пустая ссылка, если имя файла не было передано конструктору для текущего экземпляра.
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
    ///   Возвращает полное имя текущего исключения и, возможно, сообщение об ошибке, имя внутреннего исключения и трассировку стека.
    /// </summary>
    /// <returns>
    ///   Строка, содержащая полное имя данного исключения и, возможно, сообщение об ошибке, имя внутреннего исключения и стек трассировки, в зависимости от <see cref="T:System.IO.FileLoadException" /> используется конструктор.
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
    ///   Инициализирует новый экземпляр класса <see cref="T:System.IO.FileLoadException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта о созданном исключении.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    protected FileLoadException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this._fileName = info.GetString("FileLoad_FileName");
      try
      {
        this._fusionLog = info.GetString("FileLoad_FusionLog");
      }
      catch
      {
        this._fusionLog = (string) null;
      }
    }

    private FileLoadException(string fileName, string fusionLog, int hResult)
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
    /// <returns>Строка, содержащая ошибки, сообщенные кэшем сборок.</returns>
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
    ///   Наборы <see cref="T:System.Runtime.Serialization.SerializationInfo" /> с именем файла и дополнительными сведениями об исключении.
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
      info.AddValue("FileLoad_FileName", (object) this._fileName, typeof (string));
      try
      {
        info.AddValue("FileLoad_FusionLog", (object) this.FusionLog, typeof (string));
      }
      catch (SecurityException ex)
      {
      }
    }

    [SecuritySafeCritical]
    internal static string FormatFileLoadExceptionMessage(string fileName, int hResult)
    {
      string s1 = (string) null;
      FileLoadException.GetFileLoadExceptionMessage(hResult, JitHelpers.GetStringHandleOnStack(ref s1));
      string s2 = (string) null;
      FileLoadException.GetMessageForHR(hResult, JitHelpers.GetStringHandleOnStack(ref s2));
      return string.Format((IFormatProvider) CultureInfo.CurrentCulture, s1, (object) fileName, (object) s2);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetFileLoadExceptionMessage(int hResult, StringHandleOnStack retString);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetMessageForHR(int hresult, StringHandleOnStack retString);
  }
}

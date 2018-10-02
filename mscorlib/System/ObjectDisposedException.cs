// Decompiled with JetBrains decompiler
// Type: System.ObjectDisposedException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>
  ///   Исключение, которое выбрасывается при выполнении операции над удаленным объектом.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class ObjectDisposedException : InvalidOperationException
  {
    private string objectName;

    private ObjectDisposedException()
      : this((string) null, Environment.GetResourceString("ObjectDisposed_Generic"))
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.ObjectDisposedException" /> класса строка, содержащая имя удаленного объекта.
    /// </summary>
    /// <param name="objectName">
    ///   Строка, содержащая имя удаленного объекта.
    /// </param>
    [__DynamicallyInvokable]
    public ObjectDisposedException(string objectName)
      : this(objectName, Environment.GetResourceString("ObjectDisposed_Generic"))
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.ObjectDisposedException" /> класса с заданным именем объекта и сообщением.
    /// </summary>
    /// <param name="objectName">Имя удаленного объекта.</param>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    [__DynamicallyInvokable]
    public ObjectDisposedException(string objectName, string message)
      : base(message)
    {
      this.SetErrorCode(-2146232798);
      this.objectName = objectName;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ObjectDisposedException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если <paramref name="innerException" /> не <see langword="null" />, текущее исключение сгенерировано в <see langword="catch" /> блок, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public ObjectDisposedException(string message, Exception innerException)
      : base(message, innerException)
    {
      this.SetErrorCode(-2146232798);
    }

    /// <summary>Возвращает сообщение с описанием ошибки.</summary>
    /// <returns>Строка с описанием ошибки.</returns>
    [__DynamicallyInvokable]
    public override string Message
    {
      [__DynamicallyInvokable] get
      {
        string objectName = this.ObjectName;
        if (objectName == null || objectName.Length == 0)
          return base.Message;
        return base.Message + Environment.NewLine + Environment.GetResourceString("ObjectDisposed_ObjectName_Name", (object) objectName);
      }
    }

    /// <summary>Возвращает имя удаленного объекта.</summary>
    /// <returns>Строка, содержащая имя удаленного объекта.</returns>
    [__DynamicallyInvokable]
    public string ObjectName
    {
      [__DynamicallyInvokable] get
      {
        if (this.objectName == null && !CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
          return string.Empty;
        return this.objectName;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.ObjectDisposedException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта о созданном исключении.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    protected ObjectDisposedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.objectName = info.GetString(nameof (ObjectName));
    }

    /// <summary>
    ///   Извлекает <see cref="T:System.Runtime.Serialization.SerializationInfo" /> объект с именем параметра и дополнительными сведениями об исключении.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта о созданном исключении.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" />, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      base.GetObjectData(info, context);
      info.AddValue("ObjectName", (object) this.ObjectName, typeof (string));
    }
  }
}

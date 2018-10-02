// Decompiled with JetBrains decompiler
// Type: System.TypeLoadException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>
  ///   Исключение, которое выдается в случае сбоя при загрузке типа.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class TypeLoadException : SystemException, ISerializable
  {
    private string ClassName;
    private string AssemblyName;
    private string MessageArg;
    internal int ResourceId;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.TypeLoadException" />.
    /// </summary>
    [__DynamicallyInvokable]
    public TypeLoadException()
      : base(Environment.GetResourceString("Arg_TypeLoadException"))
    {
      this.SetErrorCode(-2146233054);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.TypeLoadException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    [__DynamicallyInvokable]
    public TypeLoadException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233054);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.TypeLoadException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public TypeLoadException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233054);
    }

    /// <summary>
    ///   Получает сообщение об ошибке для данного исключения.
    /// </summary>
    /// <returns>Строка сообщения об ошибке.</returns>
    [__DynamicallyInvokable]
    public override string Message
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        this.SetMessageField();
        return this._message;
      }
    }

    [SecurityCritical]
    private void SetMessageField()
    {
      if (this._message != null)
        return;
      if (this.ClassName == null && this.ResourceId == 0)
      {
        this._message = Environment.GetResourceString("Arg_TypeLoadException");
      }
      else
      {
        if (this.AssemblyName == null)
          this.AssemblyName = Environment.GetResourceString("IO_UnknownFileName");
        if (this.ClassName == null)
          this.ClassName = Environment.GetResourceString("IO_UnknownFileName");
        string s = (string) null;
        TypeLoadException.GetTypeLoadExceptionMessage(this.ResourceId, JitHelpers.GetStringHandleOnStack(ref s));
        this._message = string.Format((IFormatProvider) CultureInfo.CurrentCulture, s, (object) this.ClassName, (object) this.AssemblyName, (object) this.MessageArg);
      }
    }

    /// <summary>Получает полное имя типа, вызвавшего исключение.</summary>
    /// <returns>Полное имя типа.</returns>
    [__DynamicallyInvokable]
    public string TypeName
    {
      [__DynamicallyInvokable] get
      {
        if (this.ClassName == null)
          return string.Empty;
        return this.ClassName;
      }
    }

    [SecurityCritical]
    private TypeLoadException(string className, string assemblyName, string messageArg, int resourceId)
      : base((string) null)
    {
      this.SetErrorCode(-2146233054);
      this.ClassName = className;
      this.AssemblyName = assemblyName;
      this.MessageArg = messageArg;
      this.ResourceId = resourceId;
      this.SetMessageField();
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.TypeLoadException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Объект <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    protected TypeLoadException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      this.ClassName = info.GetString("TypeLoadClassName");
      this.AssemblyName = info.GetString("TypeLoadAssemblyName");
      this.MessageArg = info.GetString("TypeLoadMessageArg");
      this.ResourceId = info.GetInt32("TypeLoadResourceID");
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetTypeLoadExceptionMessage(int resourceId, StringHandleOnStack retString);

    /// <summary>
    ///   Устанавливает объект <see cref="T:System.Runtime.Serialization.SerializationInfo" /> с именем класса, именем метода, идентификатором ресурса и дополнительными сведениями об исключении.
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
      info.AddValue("TypeLoadClassName", (object) this.ClassName, typeof (string));
      info.AddValue("TypeLoadAssemblyName", (object) this.AssemblyName, typeof (string));
      info.AddValue("TypeLoadMessageArg", (object) this.MessageArg, typeof (string));
      info.AddValue("TypeLoadResourceID", this.ResourceId);
    }
  }
}

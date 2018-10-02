// Decompiled with JetBrains decompiler
// Type: System.MissingMemberException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>
  ///   Исключение, возникающее при попытке динамического доступа к члену класса, который не существует или не объявлен как открытый.
  ///    При удалении члена из библиотеки класса необходимо перекомпилировать все сборки, ссылающиеся на эту библиотеку.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class MissingMemberException : MemberAccessException, ISerializable
  {
    /// <summary>Содержит имя класса отсутствующего элемента.</summary>
    protected string ClassName;
    /// <summary>Содержит имя отсутствующего члена.</summary>
    protected string MemberName;
    /// <summary>Содержит подпись отсутствующего члена.</summary>
    protected byte[] Signature;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MissingMemberException" />.
    /// </summary>
    [__DynamicallyInvokable]
    public MissingMemberException()
      : base(Environment.GetResourceString("Arg_MissingMemberException"))
    {
      this.SetErrorCode(-2146233070);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MissingMemberException" /> с указанным сообщением об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    [__DynamicallyInvokable]
    public MissingMemberException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233070);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MissingMemberException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, которое стало основной причиной данного исключения.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Экземпляр <see cref="T:System.Exception" />, являющийся причиной текущего исключения <see langword="Exception" />.
    ///    Если <paramref name="inner" /> не является пустой ссылкой (<see langword="Nothing" /> в Visual Basic), текущее исключение <see langword="Exception" /> вызывается в блоке catch, обрабатывающем исключение <paramref name="inner" />.
    /// </param>
    [__DynamicallyInvokable]
    public MissingMemberException(string message, Exception inner)
      : base(message, inner)
    {
      this.SetErrorCode(-2146233070);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.MissingMemberException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected MissingMemberException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this.ClassName = info.GetString("MMClassName");
      this.MemberName = info.GetString("MMMemberName");
      this.Signature = (byte[]) info.GetValue("MMSignature", typeof (byte[]));
    }

    /// <summary>
    ///   Получает текстовую строку с именем класса, именем элемента и подписью отсутствующего элемента.
    /// </summary>
    /// <returns>Строка сообщения об ошибке.</returns>
    [__DynamicallyInvokable]
    public override string Message
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        if (this.ClassName == null)
          return base.Message;
        return Environment.GetResourceString("MissingMember_Name", (object) (this.ClassName + "." + this.MemberName + (this.Signature != null ? " " + MissingMemberException.FormatSignature(this.Signature) : "")));
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string FormatSignature(byte[] signature);

    private MissingMemberException(string className, string memberName, byte[] signature)
    {
      this.ClassName = className;
      this.MemberName = memberName;
      this.Signature = signature;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.MissingMemberException" /> класса с заданным именем класса и имя члена.
    /// </summary>
    /// <param name="className">
    ///   Имя класса, в котором была произведена попытка доступа к несуществующему члену.
    /// </param>
    /// <param name="memberName">Имя члена, который не доступен.</param>
    public MissingMemberException(string className, string memberName)
    {
      this.ClassName = className;
      this.MemberName = memberName;
    }

    /// <summary>
    ///   Наборы <see cref="T:System.Runtime.Serialization.SerializationInfo" /> объект с именем класса, имя члена, подпись отсутствующего члена, а также дополнительные сведения об исключении.
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
      info.AddValue("MMClassName", (object) this.ClassName, typeof (string));
      info.AddValue("MMMemberName", (object) this.MemberName, typeof (string));
      info.AddValue("MMSignature", (object) this.Signature, typeof (byte[]));
    }
  }
}

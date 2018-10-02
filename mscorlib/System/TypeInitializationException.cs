// Decompiled with JetBrains decompiler
// Type: System.TypeInitializationException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>
  ///   Исключение, которое выбрасывается как оболочка для исключения, выброшенного инициализатором класса.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class TypeInitializationException : SystemException
  {
    private string _typeName;

    private TypeInitializationException()
      : base(Environment.GetResourceString("TypeInitialization_Default"))
    {
      this.SetErrorCode(-2146233036);
    }

    private TypeInitializationException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146233036);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.TypeInitializationException" /> с сообщение по умолчанию, указанным именем типа и ссылку на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="fullTypeName">
    ///   Полное имя типа, который не удалось инициализировать.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="innerException" /> не является указателем null (<see langword="Nothing" /> в Visual Basic), то текущее исключение создается в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public TypeInitializationException(string fullTypeName, Exception innerException)
      : base(Environment.GetResourceString("TypeInitialization_Type", (object) fullTypeName), innerException)
    {
      this._typeName = fullTypeName;
      this.SetErrorCode(-2146233036);
    }

    internal TypeInitializationException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this._typeName = info.GetString(nameof (TypeName));
    }

    /// <summary>
    ///   Возвращает полное имя типа, который не удалось инициализировать.
    /// </summary>
    /// <returns>
    ///   Полное имя типа, который не удалось инициализировать.
    /// </returns>
    [__DynamicallyInvokable]
    public string TypeName
    {
      [__DynamicallyInvokable] get
      {
        if (this._typeName == null)
          return string.Empty;
        return this._typeName;
      }
    }

    /// <summary>
    ///   Наборы <see cref="T:System.Runtime.Serialization.SerializationInfo" /> объект с именем типа и дополнительными сведениями об исключении.
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
      info.AddValue("TypeName", (object) this.TypeName, typeof (string));
    }
  }
}

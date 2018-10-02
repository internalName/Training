// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.PrivilegeNotHeldException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.Serialization;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Исключение вызывается, когда метод в пространстве имен <see cref="N:System.Security.AccessControl" /> пытается использовать отсутствующую у него привилегию.
  /// </summary>
  [Serializable]
  public sealed class PrivilegeNotHeldException : UnauthorizedAccessException, ISerializable
  {
    private readonly string _privilegeName;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AccessControl.PrivilegeNotHeldException" />.
    /// </summary>
    public PrivilegeNotHeldException()
      : base(Environment.GetResourceString("PrivilegeNotHeld_Default"))
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.PrivilegeNotHeldException" /> используя указанный прав доступа.
    /// </summary>
    /// <param name="privilege">Привилегия, не включена.</param>
    public PrivilegeNotHeldException(string privilege)
      : base(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("PrivilegeNotHeld_Named"), (object) privilege))
    {
      this._privilegeName = privilege;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.AccessControl.PrivilegeNotHeldException" /> используя указанное исключение.
    /// </summary>
    /// <param name="privilege">Привилегия, не включена.</param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если параметр <paramref name="innerException" /> не является указателем null (<see langword="Nothing" /> в Visual Basic), то текущее исключение создается в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public PrivilegeNotHeldException(string privilege, Exception inner)
      : base(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("PrivilegeNotHeld_Named"), (object) privilege), inner)
    {
      this._privilegeName = privilege;
    }

    internal PrivilegeNotHeldException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this._privilegeName = info.GetString(nameof (PrivilegeName));
    }

    /// <summary>Возвращает имя права, которым не включена.</summary>
    /// <returns>Имя права, которым не удалось включить метод.</returns>
    public string PrivilegeName
    {
      get
      {
        return this._privilegeName;
      }
    }

    /// <summary>
    ///   Наборы <paramref name="info" /> параметр со сведениями об исключении.
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
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      base.GetObjectData(info, context);
      info.AddValue("PrivilegeName", (object) this._privilegeName, typeof (string));
    }
  }
}

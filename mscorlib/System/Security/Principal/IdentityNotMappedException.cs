// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.IdentityNotMappedException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Principal
{
  /// <summary>
  ///   Представляет исключение для участника, удостоверение которого не может быть сопоставлено с известным удостоверением.
  /// </summary>
  [ComVisible(false)]
  [Serializable]
  public sealed class IdentityNotMappedException : SystemException
  {
    private IdentityReferenceCollection unmappedIdentities;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Principal.IdentityNotMappedException" />.
    /// </summary>
    public IdentityNotMappedException()
      : base(Environment.GetResourceString("IdentityReference_IdentityNotMapped"))
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Principal.IdentityNotMappedException" /> с помощью заданного сообщения об ошибке.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    public IdentityNotMappedException(string message)
      : base(message)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Principal.IdentityNotMappedException" />, используя указанное сообщение об ошибке и внутреннее исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение об ошибке, указывающее причину создания исключения.
    /// </param>
    /// <param name="inner">
    ///   Исключение, которое является причиной текущего исключения.
    ///    Если значение параметра <paramref name="inner" /> не равно NULL, текущее исключение вызывается в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    public IdentityNotMappedException(string message, Exception inner)
      : base(message, inner)
    {
    }

    internal IdentityNotMappedException(string message, IdentityReferenceCollection unmappedIdentities)
      : this(message)
    {
      this.unmappedIdentities = unmappedIdentities;
    }

    internal IdentityNotMappedException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>
    ///   Возвращает сведения сериализации с данными, необходимыми для создания экземпляра этого объекта <see cref="T:System.Security.Principal.IdentityNotMappedException" />.
    /// </summary>
    /// <param name="serializationInfo">
    ///   Объект, содержащий сериализованные данные объекта о возникающем исключении.
    /// </param>
    /// <param name="streamingContext">
    ///   Объект, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
    {
      base.GetObjectData(serializationInfo, streamingContext);
    }

    /// <summary>
    ///   Представляет коллекцию несопоставленных удостоверений для исключения <see cref="T:System.Security.Principal.IdentityNotMappedException" />.
    /// </summary>
    /// <returns>Коллекция несопоставленных удостоверений.</returns>
    public IdentityReferenceCollection UnmappedIdentities
    {
      get
      {
        if (this.unmappedIdentities == null)
          this.unmappedIdentities = new IdentityReferenceCollection();
        return this.unmappedIdentities;
      }
    }
  }
}

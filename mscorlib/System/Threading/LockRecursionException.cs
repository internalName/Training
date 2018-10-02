// Decompiled with JetBrains decompiler
// Type: System.Threading.LockRecursionException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Threading
{
  /// <summary>
  ///   Исключение, которое создается, когда рекурсивная запись блокировки не совместима с рекурсивной политикой блокировки.
  /// </summary>
  [TypeForwardedFrom("System.Core, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=b77a5c561934e089")]
  [__DynamicallyInvokable]
  [Serializable]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public class LockRecursionException : Exception
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.LockRecursionException" /> системным сообщением, содержащим описание ошибки.
    /// </summary>
    [__DynamicallyInvokable]
    public LockRecursionException()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.LockRecursionException" /> с использованием заданного сообщения, содержащего описание ошибки.
    /// </summary>
    /// <param name="message">
    ///   Сообщение с описанием исключения.
    ///    Вызывающий объект этого конструктора должен убедиться, что эта строка локализована для текущего языка и региональных параметров системы.
    /// </param>
    [__DynamicallyInvokable]
    public LockRecursionException(string message)
      : base(message)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.LockRecursionException" /> с сериализованными данными.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    protected LockRecursionException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Threading.LockRecursionException" /> указанным сообщением об ошибке и ссылкой на внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">
    ///   Сообщение с описанием исключения.
    ///    Вызывающий объект этого конструктора должен убедиться, что эта строка локализована для текущего языка и региональных параметров системы.
    /// </param>
    /// <param name="innerException">
    ///   Исключение, которое вызвало текущее исключение.
    ///    Если значение параметра <paramref name="innerException" /> не равно <see langword="null" />, текущее исключение сгенерировано в блоке <see langword="catch" />, обрабатывающем внутреннее исключение.
    /// </param>
    [__DynamicallyInvokable]
    public LockRecursionException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}

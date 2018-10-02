// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices._Exception
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Предоставляет открытые члены <see cref="T:System.Exception" /> класс в неуправляемый код.
  /// </summary>
  [Guid("b36b5c63-42ef-38bc-a07e-0b34c98f164a")]
  [InterfaceType(ComInterfaceType.InterfaceIsDual)]
  [CLSCompliant(false)]
  [ComVisible(true)]
  public interface _Exception
  {
    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Exception.ToString" /> метод.
    /// </summary>
    /// <returns>
    ///   Строка, представляющая текущий объект <see cref="T:System.Exception" />.
    /// </returns>
    string ToString();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Object.Equals(System.Object)" /> метод.
    /// </summary>
    /// <param name="obj">
    ///   Объект <see cref="T:System.Object" />, с которым сравнивается текущий объект <see cref="T:System.Object" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный объект <see cref="T:System.Object" /> равен текущему объекту <see cref="T:System.Object" />; в противном случае — <see langword="false" />.
    /// </returns>
    bool Equals(object obj);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Object.GetHashCode" /> метод.
    /// </summary>
    /// <returns>Хэш-код для текущего экземпляра.</returns>
    int GetHashCode();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Exception.GetType" /> метод.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />, представляющий точный тип среды выполнения текущего экземпляра.
    /// </returns>
    Type GetType();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Exception.Message" /> свойство.
    /// </summary>
    /// <returns>
    ///   Сообщение об ошибке с объяснением причин исключения или пустая строка ("").
    /// </returns>
    string Message { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Exception.GetBaseException" /> метод.
    /// </summary>
    /// <returns>
    ///   В цепочке исключений создается первое исключение.
    ///    Если значением свойства <see cref="P:System.Exception.InnerException" /> текущего исключения является пустая ссылка (<see langword="Nothing" /> в Visual Basic), это свойство возвращает текущее исключение.
    /// </returns>
    Exception GetBaseException();

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Exception.StackTrace" /> свойство.
    /// </summary>
    /// <returns>
    ///   Строка, описывающая содержимое стека вызовов, где самый последний вызов метода, перечислены в первую очередь.
    /// </returns>
    string StackTrace { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Exception.HelpLink" /> свойство.
    /// </summary>
    /// <returns>
    ///   Универсального имени ресурса (URN) или унифицированный указатель ресурса (URL) к файлу справки.
    /// </returns>
    string HelpLink { get; set; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Exception.Source" /> свойство.
    /// </summary>
    /// <returns>Имя приложения или объекта, вызвавшего ошибку.</returns>
    string Source { get; set; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="M:System.Exception.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> метод
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" />, содержащий сериализованные данные объекта по возникающему исключению.
    /// </param>
    /// <param name="context">
    ///   <see cref="T:System.Runtime.Serialization.StreamingContext" /> Структуры, содержащий контекстные сведения об источнике или назначении.
    /// </param>
    [SecurityCritical]
    void GetObjectData(SerializationInfo info, StreamingContext context);

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Exception.InnerException" /> свойство.
    /// </summary>
    /// <returns>
    ///   Экземпляр <see cref="T:System.Exception" /> описывающий ошибку, вызвавшее текущее исключение.
    ///   <see cref="P:System.Exception.InnerException" /> Свойство возвращает то же значение, которое было передано конструктору, или пустую ссылку (<see langword="Nothing" /> в Visual Basic), если конструктору не было передано значение внутреннего исключения.
    ///    Это свойство доступно только для чтения.
    /// </returns>
    Exception InnerException { get; }

    /// <summary>
    ///   Предоставляет COM-объектов с доступом зависят от версии <see cref="P:System.Exception.TargetSite" /> свойство.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Reflection.MethodBase" /> Объект, который вызвал текущее исключение.
    /// </returns>
    MethodBase TargetSite { get; }
  }
}

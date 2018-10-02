// Decompiled with JetBrains decompiler
// Type: System.Reflection.ReflectionTypeLoadException
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
  /// <summary>
  ///   Исключение вызывается методом <see cref="M:System.Reflection.Module.GetTypes" />, если какой-либо из классов модуля не может быть загружен.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class ReflectionTypeLoadException : SystemException, ISerializable
  {
    private Type[] _classes;
    private Exception[] _exceptions;

    private ReflectionTypeLoadException()
      : base(Environment.GetResourceString("ReflectionTypeLoad_LoadFailed"))
    {
      this.SetErrorCode(-2146232830);
    }

    private ReflectionTypeLoadException(string message)
      : base(message)
    {
      this.SetErrorCode(-2146232830);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.ReflectionTypeLoadException" /> с данными классами и связанными исключениями.
    /// </summary>
    /// <param name="classes">
    ///   Массив объектов типа <see langword="Type" /> содержащий классы, которые были определены в модуле и загружены.
    ///    Этот массив может содержать пустые ссылки (<see langword="Nothing" /> в Visual Basic) значения.
    /// </param>
    /// <param name="exceptions">
    ///   Массив объектов типа <see langword="Exception" /> содержащий исключения, которые были созданы загрузчиком класса.
    ///    Пустая ссылка (<see langword="Nothing" /> в Visual Basic) значения в <paramref name="classes" /> массиве исключений в этом <paramref name="exceptions" /> массива.
    /// </param>
    [__DynamicallyInvokable]
    public ReflectionTypeLoadException(Type[] classes, Exception[] exceptions)
      : base((string) null)
    {
      this._classes = classes;
      this._exceptions = exceptions;
      this.SetErrorCode(-2146232830);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.ReflectionTypeLoadException" /> класса с данными классами, связанными исключениями и описаниями исключений.
    /// </summary>
    /// <param name="classes">
    ///   Массив объектов типа <see langword="Type" /> содержащий классы, которые были определены в модуле и загружены.
    ///    Этот массив может содержать пустые ссылки (<see langword="Nothing" /> в Visual Basic) значения.
    /// </param>
    /// <param name="exceptions">
    ///   Массив объектов типа <see langword="Exception" /> содержащий исключения, которые были созданы загрузчиком класса.
    ///    Пустая ссылка (<see langword="Nothing" /> в Visual Basic) значения в <paramref name="classes" /> массиве исключений в этом <paramref name="exceptions" /> массива.
    /// </param>
    /// <param name="message">
    ///   A <see langword="String" /> описания причины возникновения этого исключения.
    /// </param>
    [__DynamicallyInvokable]
    public ReflectionTypeLoadException(Type[] classes, Exception[] exceptions, string message)
      : base(message)
    {
      this._classes = classes;
      this._exceptions = exceptions;
      this.SetErrorCode(-2146232830);
    }

    internal ReflectionTypeLoadException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
      this._classes = (Type[]) info.GetValue(nameof (Types), typeof (Type[]));
      this._exceptions = (Exception[]) info.GetValue("Exceptions", typeof (Exception[]));
    }

    /// <summary>
    ///   Возвращает массив классов, которые были определены в модуле и загружены.
    /// </summary>
    /// <returns>
    ///   Массив объектов типа <see langword="Type" /> содержащий классы, которые были определены в модуле и загружены.
    ///    Этот массив может содержать некоторые <see langword="null" /> значения.
    /// </returns>
    [__DynamicallyInvokable]
    public Type[] Types
    {
      [__DynamicallyInvokable] get
      {
        return this._classes;
      }
    }

    /// <summary>
    ///   Получает массив исключений, создаваемых загрузчиком классов.
    /// </summary>
    /// <returns>
    ///   Массив типа <see langword="Exception" />, который содержит исключения, создаваемые загрузчиком классов.
    ///    Значения Null в массиве <see cref="P:System.Reflection.ReflectionTypeLoadException.Types" /> этого экземпляра выравниваются в соответствии с исключениями в этом массиве.
    /// </returns>
    [__DynamicallyInvokable]
    public Exception[] LoaderExceptions
    {
      [__DynamicallyInvokable] get
      {
        return this._exceptions;
      }
    }

    /// <summary>
    ///   Предоставляет <see cref="T:System.Runtime.Serialization.ISerializable" /> реализацию для сериализованных объектов.
    /// </summary>
    /// <param name="info">
    ///   Сведения и данные, необходимые для сериализации или десериализации объекта.
    /// </param>
    /// <param name="context">Контекст для сериализации.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <see langword="info" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      base.GetObjectData(info, context);
      info.AddValue("Types", (object) this._classes, typeof (Type[]));
      info.AddValue("Exceptions", (object) this._exceptions, typeof (Exception[]));
    }
  }
}

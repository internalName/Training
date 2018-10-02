// Decompiled with JetBrains decompiler
// Type: System.Nullable`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Versioning;

namespace System
{
  /// <summary>
  ///   Представляет тип значения, которому можно присвоить значение <see langword="null" />.
  /// </summary>
  /// <typeparam name="T">
  ///   Базовый тип значения универсального типа <see cref="T:System.Nullable`1" />.
  /// </typeparam>
  [NonVersionable]
  [__DynamicallyInvokable]
  [Serializable]
  public struct Nullable<T> where T : struct
  {
    private bool hasValue;
    internal T value;

    /// <summary>
    ///   Инициализирует новый экземпляр структуры <see cref="T:System.Nullable`1" /> заданным значением.
    /// </summary>
    /// <param name="value">Тип значения.</param>
    [NonVersionable]
    [__DynamicallyInvokable]
    public Nullable(T value)
    {
      this.value = value;
      this.hasValue = true;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, имеет ли текущий объект <see cref="T:System.Nullable`1" /> допустимое значение своего базового типа.
    /// </summary>
    /// <returns>
    ///   <see langword="true" />, если текущий объект <see cref="T:System.Nullable`1" /> имеет значение; <see langword="false" />, если текущий объект <see cref="T:System.Nullable`1" /> не имеет значения.
    /// </returns>
    [__DynamicallyInvokable]
    public bool HasValue
    {
      [NonVersionable, __DynamicallyInvokable] get
      {
        return this.hasValue;
      }
    }

    /// <summary>
    ///   Возвращает значение текущего объекта <see cref="T:System.Nullable`1" />, если ему присвоено допустимое базовое значение.
    /// </summary>
    /// <returns>
    ///   Значение текущего объекта <see cref="T:System.Nullable`1" />, если свойство <see cref="P:System.Nullable`1.HasValue" /> имеет значение <see langword="true" />.
    ///    Если свойство <see cref="P:System.Nullable`1.HasValue" /> имеет значение <see langword="false" />, то вызывается исключение.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Значение свойства <see cref="P:System.Nullable`1.HasValue" /> — <see langword="false" />.
    /// </exception>
    [__DynamicallyInvokable]
    public T Value
    {
      [__DynamicallyInvokable] get
      {
        if (!this.hasValue)
          ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_NoValue);
        return this.value;
      }
    }

    /// <summary>
    ///   Извлекает значение текущего объекта <see cref="T:System.Nullable`1" /> или значение этого объекта по умолчанию.
    /// </summary>
    /// <returns>
    ///   Значение свойства <see cref="P:System.Nullable`1.Value" />, если свойство <see cref="P:System.Nullable`1.HasValue" /> имеет значение <see langword="true" />; в противном случае — значение по умолчанию текущего объекта <see cref="T:System.Nullable`1" />.
    ///    Тип значения по умолчанию является аргументом типа текущего объекта <see cref="T:System.Nullable`1" />, а значение значения по умолчанию состоит только из двоичных нулей.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public T GetValueOrDefault()
    {
      return this.value;
    }

    /// <summary>
    ///   Извлекает значение текущего объекта <see cref="T:System.Nullable`1" /> или значение этого объекта по умолчанию.
    /// </summary>
    /// <param name="defaultValue">
    ///   Значение, которое следует вернуть, если свойство <see cref="P:System.Nullable`1.HasValue" /> имеет значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Значение свойства <see cref="P:System.Nullable`1.Value" />, если свойство <see cref="P:System.Nullable`1.HasValue" /> имеет значение <see langword="true" />; в противном случае значение параметра <paramref name="defaultValue" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public T GetValueOrDefault(T defaultValue)
    {
      if (!this.hasValue)
        return defaultValue;
      return this.value;
    }

    /// <summary>
    ///   Указывает, равен ли текущий объект <see cref="T:System.Nullable`1" /> указанному объекту.
    /// </summary>
    /// <param name="other">Объект.</param>
    /// <returns>
    /// <see langword="true" />, если значение параметра <paramref name="other" /> равно текущему объекту <see cref="T:System.Nullable`1" />, в противном случае — <see langword="false" />.
    /// 
    /// В следующей таблице описывается, как определяется равенство сравниваемых значений.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Описание
    /// 
    ///         <see langword="true" />
    /// 
    ///         <see cref="P:System.Nullable`1.HasValue" /> Свойство <see langword="false" />, и <paramref name="other" /> параметр <see langword="null" />.
    ///          То есть два значения null равны по определению.
    /// 
    ///         -или-
    /// 
    ///         <see cref="P:System.Nullable`1.HasValue" /> Свойство <see langword="true" />, и значение, возвращаемое <see cref="P:System.Nullable`1.Value" /> равно <paramref name="other" /> параметр.
    /// 
    ///         <see langword="false" />
    /// 
    ///         <see cref="P:System.Nullable`1.HasValue" /> Свойство для текущего <see cref="T:System.Nullable`1" /> Структура <see langword="true" />, и <paramref name="other" /> параметр <see langword="null" />.
    /// 
    ///         -или-
    /// 
    ///         <see cref="P:System.Nullable`1.HasValue" /> Свойство для текущего <see cref="T:System.Nullable`1" /> Структура <see langword="false" />, и <paramref name="other" /> параметр не <see langword="null" />.
    /// 
    ///         -или-
    /// 
    ///         <see cref="P:System.Nullable`1.HasValue" /> Свойство для текущего <see cref="T:System.Nullable`1" /> Структура <see langword="true" />, и значение, возвращаемое <see cref="P:System.Nullable`1.Value" /> свойства не равно <paramref name="other" /> параметр.
    ///       </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object other)
    {
      if (!this.hasValue)
        return other == null;
      if (other == null)
        return false;
      return this.value.Equals(other);
    }

    /// <summary>
    ///   Извлекает хэш-код объекта, возвращенного свойством <see cref="P:System.Nullable`1.Value" />.
    /// </summary>
    /// <returns>
    ///   Хэш-код объекта, возвращенного свойством <see cref="P:System.Nullable`1.Value" />, если свойство <see cref="P:System.Nullable`1.HasValue" /> имеет значение <see langword="true" />, или нуль, если свойство <see cref="P:System.Nullable`1.HasValue" /> имеет значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      if (!this.hasValue)
        return 0;
      return this.value.GetHashCode();
    }

    /// <summary>
    ///   Возвращает текстовое представление значения текущего объекта <see cref="T:System.Nullable`1" />.
    /// </summary>
    /// <returns>
    ///   Текстовое представление значения текущего объекта <see cref="T:System.Nullable`1" />, если свойство <see cref="P:System.Nullable`1.HasValue" /> имеет значение <see langword="true" />, или пустая строка (""), если свойство <see cref="P:System.Nullable`1.HasValue" /> имеет значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      if (!this.hasValue)
        return "";
      return this.value.ToString();
    }

    /// <summary>
    ///   Создает новый объект <see cref="T:System.Nullable`1" />, инициализированный заданным значением.
    /// </summary>
    /// <param name="value">Тип значения.</param>
    /// <returns>
    ///   Объект <see cref="T:System.Nullable`1" />, свойство <see cref="P:System.Nullable`1.Value" /> которого инициализируется значением параметра <paramref name="value" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static implicit operator T?(T value)
    {
      return new T?(value);
    }

    /// <summary>
    ///   Определяет явное преобразование экземпляра <see cref="T:System.Nullable`1" /> в его базовое значение.
    /// </summary>
    /// <param name="value">Значение, допускающее значения NULL.</param>
    /// <returns>
    ///   Значение свойства <see cref="P:System.Nullable`1.Value" /> для параметра <paramref name="value" />.
    /// </returns>
    [NonVersionable]
    [__DynamicallyInvokable]
    public static explicit operator T(T? value)
    {
      return value.Value;
    }
  }
}

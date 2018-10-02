// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>
  ///   Предоставляет базовую функциональность для общего языка модули форматирования при сериализации среды выполнения.
  /// </summary>
  [CLSCompliant(false)]
  [ComVisible(true)]
  [Serializable]
  public abstract class Formatter : IFormatter
  {
    /// <summary>
    ///   Содержит <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" /> использовать с текущим модулем форматирования.
    /// </summary>
    protected ObjectIDGenerator m_idGenerator;
    /// <summary>
    ///   Содержит <see cref="T:System.Collections.Queue" /> объектов, предназначенных для сериализации.
    /// </summary>
    protected Queue m_objectQueue;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Serialization.Formatter" />.
    /// </summary>
    protected Formatter()
    {
      this.m_objectQueue = new Queue();
      this.m_idGenerator = new ObjectIDGenerator();
    }

    /// <summary>
    ///   При переопределении в производном классе десериализует поток, подсоединенный форматирования данных, при его создании, создание граф объектов, идентичный графу, первоначально сериализованному в этот поток.
    /// </summary>
    /// <param name="serializationStream">
    ///   Поток для десериализации.
    /// </param>
    /// <returns>Верхний объект десериализованного графа объектов.</returns>
    public abstract object Deserialize(Stream serializationStream);

    /// <summary>
    ///   Возвращает следующий объект для сериализации из внутренней рабочей очереди модуль форматирования.
    /// </summary>
    /// <param name="objID">
    ///   Идентификатор, назначенный текущему объекту при сериализации.
    /// </param>
    /// <returns>Следующий объект для сериализации.</returns>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Следующего объекта, извлеченного из рабочей очереди не было назначенного идентификатора.
    /// </exception>
    protected virtual object GetNext(out long objID)
    {
      if (this.m_objectQueue.Count == 0)
      {
        objID = 0L;
        return (object) null;
      }
      object obj = this.m_objectQueue.Dequeue();
      bool firstTime;
      objID = this.m_idGenerator.HasId(obj, out firstTime);
      if (firstTime)
        throw new SerializationException(Environment.GetResourceString("Serialization_NoID"));
      return obj;
    }

    /// <summary>Намечает объект для сериализации.</summary>
    /// <param name="obj">Объект, назначенный для сериализации.</param>
    /// <returns>Идентификатор объекта, назначенный объекту.</returns>
    protected virtual long Schedule(object obj)
    {
      if (obj == null)
        return 0;
      bool firstTime;
      long id = this.m_idGenerator.GetId(obj, out firstTime);
      if (firstTime)
        this.m_objectQueue.Enqueue(obj);
      return id;
    }

    /// <summary>
    ///   При переопределении в производном классе сериализует граф объектов с заданным корнем в поток, который уже присоединен к форматирования.
    /// </summary>
    /// <param name="serializationStream">
    ///   Поток, в который сериализуются объекты.
    /// </param>
    /// <param name="graph">Объект в корне графа для сериализации.</param>
    public abstract void Serialize(Stream serializationStream, object graph);

    /// <summary>
    ///   При переопределении в производном классе записывает массив в поток, уже присоединен к форматирования.
    /// </summary>
    /// <param name="obj">Массив для записи.</param>
    /// <param name="name">Имя массива.</param>
    /// <param name="memberType">Тип элементов массива.</param>
    protected abstract void WriteArray(object obj, string name, Type memberType);

    /// <summary>
    ///   При переопределении в производном классе записывает логическое значение в поток, уже присоединен к форматирования.
    /// </summary>
    /// <param name="val">Значение для записи.</param>
    /// <param name="name">Имя элемента.</param>
    protected abstract void WriteBoolean(bool val, string name);

    /// <summary>
    ///   При переопределении в производном классе записывает в поток, уже присоединен к форматирования 8-разрядное целое число без знака.
    /// </summary>
    /// <param name="val">Значение для записи.</param>
    /// <param name="name">Имя элемента.</param>
    protected abstract void WriteByte(byte val, string name);

    /// <summary>
    ///   При переопределении в производном классе записывает символ Юникода в поток, уже присоединен к модуль форматирования.
    /// </summary>
    /// <param name="val">Значение для записи.</param>
    /// <param name="name">Имя элемента.</param>
    protected abstract void WriteChar(char val, string name);

    /// <summary>
    ///   При переопределении в производном классе записывает <see cref="T:System.DateTime" /> в поток, который уже присоединен к форматирования.
    /// </summary>
    /// <param name="val">Значение для записи.</param>
    /// <param name="name">Имя элемента.</param>
    protected abstract void WriteDateTime(DateTime val, string name);

    /// <summary>
    ///   При переопределении в производном классе записывает <see cref="T:System.Decimal" /> в поток, который уже присоединен к форматирования.
    /// </summary>
    /// <param name="val">Значение для записи.</param>
    /// <param name="name">Имя элемента.</param>
    protected abstract void WriteDecimal(Decimal val, string name);

    /// <summary>
    ///   При переопределении в производном классе записывает в поток, уже присоединен к форматирования числом двойной точности с плавающей запятой.
    /// </summary>
    /// <param name="val">Значение для записи.</param>
    /// <param name="name">Имя элемента.</param>
    protected abstract void WriteDouble(double val, string name);

    /// <summary>
    ///   При переопределении в производном классе записывает 16-разрядное целое число со знаком в поток, уже присоединен к форматирования.
    /// </summary>
    /// <param name="val">Значение для записи.</param>
    /// <param name="name">Имя элемента.</param>
    protected abstract void WriteInt16(short val, string name);

    /// <summary>
    ///   При переопределении в производном классе записывает 32-разрядное целое число со знаком в поток.
    /// </summary>
    /// <param name="val">Значение для записи.</param>
    /// <param name="name">Имя элемента.</param>
    protected abstract void WriteInt32(int val, string name);

    /// <summary>
    ///   При переопределении в производном классе записывает 64-разрядного знакового целого числа в поток.
    /// </summary>
    /// <param name="val">Значение для записи.</param>
    /// <param name="name">Имя элемента.</param>
    protected abstract void WriteInt64(long val, string name);

    /// <summary>
    ///   При переопределении в производном классе записывает ссылку на объект в поток, который уже присоединен к форматирования.
    /// </summary>
    /// <param name="obj">Ссылка на объект для записи.</param>
    /// <param name="name">Имя элемента.</param>
    /// <param name="memberType">Указывает тип объекта ссылки.</param>
    protected abstract void WriteObjectRef(object obj, string name, Type memberType);

    /// <summary>
    ///   Проверяет тип полученных данных и вызывает соответствующие <see langword="Write" /> метод для записи в поток, который уже присоединен к форматирования.
    /// </summary>
    /// <param name="memberName">
    ///   Имя члена, который требуется сериализовать.
    /// </param>
    /// <param name="data">
    ///   Объект для записи в поток, подсоединенный к форматирования.
    /// </param>
    protected virtual void WriteMember(string memberName, object data)
    {
      if (data == null)
      {
        this.WriteObjectRef(data, memberName, typeof (object));
      }
      else
      {
        Type type = data.GetType();
        if (type == typeof (bool))
          this.WriteBoolean(Convert.ToBoolean(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (char))
          this.WriteChar(Convert.ToChar(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (sbyte))
          this.WriteSByte(Convert.ToSByte(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (byte))
          this.WriteByte(Convert.ToByte(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (short))
          this.WriteInt16(Convert.ToInt16(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (int))
          this.WriteInt32(Convert.ToInt32(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (long))
          this.WriteInt64(Convert.ToInt64(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (float))
          this.WriteSingle(Convert.ToSingle(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (double))
          this.WriteDouble(Convert.ToDouble(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (DateTime))
          this.WriteDateTime(Convert.ToDateTime(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (Decimal))
          this.WriteDecimal(Convert.ToDecimal(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (ushort))
          this.WriteUInt16(Convert.ToUInt16(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (uint))
          this.WriteUInt32(Convert.ToUInt32(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type == typeof (ulong))
          this.WriteUInt64(Convert.ToUInt64(data, (IFormatProvider) CultureInfo.InvariantCulture), memberName);
        else if (type.IsArray)
          this.WriteArray(data, memberName, type);
        else if (type.IsValueType)
          this.WriteValueType(data, memberName, type);
        else
          this.WriteObjectRef(data, memberName, type);
      }
    }

    /// <summary>
    ///   При переопределении в производном классе записывает 8-битовое целое число со знаком в поток, уже присоединен к форматирования.
    /// </summary>
    /// <param name="val">Значение для записи.</param>
    /// <param name="name">Имя элемента.</param>
    [CLSCompliant(false)]
    protected abstract void WriteSByte(sbyte val, string name);

    /// <summary>
    ///   При переопределении в производном классе записывает в поток, уже присоединен к форматирования числом с плавающей запятой одиночной точности.
    /// </summary>
    /// <param name="val">Значение для записи.</param>
    /// <param name="name">Имя элемента.</param>
    protected abstract void WriteSingle(float val, string name);

    /// <summary>
    ///   При переопределении в производном классе записывает <see cref="T:System.TimeSpan" /> в поток, который уже присоединен к форматирования.
    /// </summary>
    /// <param name="val">Значение для записи.</param>
    /// <param name="name">Имя элемента.</param>
    protected abstract void WriteTimeSpan(TimeSpan val, string name);

    /// <summary>
    ///   При переопределении в производном классе записывает 16-разрядное целое число без знака в поток, уже присоединен к форматирования.
    /// </summary>
    /// <param name="val">Значение для записи.</param>
    /// <param name="name">Имя элемента.</param>
    [CLSCompliant(false)]
    protected abstract void WriteUInt16(ushort val, string name);

    /// <summary>
    ///   При переопределении в производном классе записывает 32-разрядное целое число без знака в поток, уже присоединен к форматирования.
    /// </summary>
    /// <param name="val">Значение для записи.</param>
    /// <param name="name">Имя элемента.</param>
    [CLSCompliant(false)]
    protected abstract void WriteUInt32(uint val, string name);

    /// <summary>
    ///   При переопределении в производном классе записывает в поток, уже присоединен к форматирования 64-разрядное целое число без знака.
    /// </summary>
    /// <param name="val">Значение для записи.</param>
    /// <param name="name">Имя элемента.</param>
    [CLSCompliant(false)]
    protected abstract void WriteUInt64(ulong val, string name);

    /// <summary>
    ///   При переопределении в производном классе записывает значение заданного типа в поток, который уже присоединен к форматирования.
    /// </summary>
    /// <param name="obj">Объект, представляющий тип значения.</param>
    /// <param name="name">Имя элемента.</param>
    /// <param name="memberType">
    ///   <see cref="T:System.Type" /> Типа значения.
    /// </param>
    protected abstract void WriteValueType(object obj, string name, Type memberType);

    /// <summary>
    ///   При переопределении в производном классе Возвращает или задает <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> использовать с текущим модулем форматирования.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.Serialization.ISurrogateSelector" />, используемый с текущим модулем форматирования.
    /// </returns>
    public abstract ISurrogateSelector SurrogateSelector { get; set; }

    /// <summary>
    ///   При переопределении в производном классе Возвращает или задает <see cref="T:System.Runtime.Serialization.SerializationBinder" /> использовать с текущим модулем форматирования.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.Serialization.SerializationBinder" />, используемый с текущим модулем форматирования.
    /// </returns>
    public abstract SerializationBinder Binder { get; set; }

    /// <summary>
    ///   При переопределении в производном классе Возвращает или задает <see cref="T:System.Runtime.Serialization.StreamingContext" /> для текущей сериализации.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.Serialization.StreamingContext" /> Для текущей сериализации.
    /// </returns>
    public abstract StreamingContext Context { get; set; }
  }
}

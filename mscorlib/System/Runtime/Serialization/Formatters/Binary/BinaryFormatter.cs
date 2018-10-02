// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  /// <summary>
  ///   Сериализует и десериализует объект или полный граф связанных объектов в двоичном формате.
  /// </summary>
  [ComVisible(true)]
  public sealed class BinaryFormatter : IRemotingFormatter, IFormatter
  {
    private static Dictionary<Type, TypeInformation> typeNameCache = new Dictionary<Type, TypeInformation>();
    internal FormatterTypeStyle m_typeFormat = FormatterTypeStyle.TypesAlways;
    internal TypeFilterLevel m_securityLevel = TypeFilterLevel.Full;
    internal ISurrogateSelector m_surrogates;
    internal StreamingContext m_context;
    internal SerializationBinder m_binder;
    internal FormatterAssemblyStyle m_assemblyFormat;
    internal object[] m_crossAppDomainArray;

    /// <summary>
    ///   Возвращает или задает формат, в котором описания типов располагаются в сериализованном потоке.
    /// </summary>
    /// <returns>Стиль разбивки типов для использования.</returns>
    public FormatterTypeStyle TypeFormat
    {
      get
      {
        return this.m_typeFormat;
      }
      set
      {
        this.m_typeFormat = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает поведение десериализатора в части, касающейся поиска и загрузки сборок.
    /// </summary>
    /// <returns>
    ///   Один из <see cref="T:System.Runtime.Serialization.Formatters.FormatterAssemblyStyle" /> значений, определяющих поведение десериализатор.
    /// </returns>
    public FormatterAssemblyStyle AssemblyFormat
    {
      get
      {
        return this.m_assemblyFormat;
      }
      set
      {
        this.m_assemblyFormat = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> автоматической десериализации <see cref="T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter" /> выполняет.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.Serialization.Formatters.TypeFilterLevel" /> Представляющий текущий уровень автоматической десериализации.
    /// </returns>
    public TypeFilterLevel FilterLevel
    {
      get
      {
        return this.m_securityLevel;
      }
      set
      {
        this.m_securityLevel = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Runtime.Serialization.ISurrogateSelector" /> управляющий подстановкой типа при сериализации и десериализации.
    /// </summary>
    /// <returns>
    ///   Суррогатный селектор, используемый с этим форматером.
    /// </returns>
    public ISurrogateSelector SurrogateSelector
    {
      get
      {
        return this.m_surrogates;
      }
      set
      {
        this.m_surrogates = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает объект типа <see cref="T:System.Runtime.Serialization.SerializationBinder" /> управляет привязкой сериализованного объекта к типу.
    /// </summary>
    /// <returns>
    ///   Связыватель сериализации, используемый с этим форматером.
    /// </returns>
    public SerializationBinder Binder
    {
      get
      {
        return this.m_binder;
      }
      set
      {
        this.m_binder = value;
      }
    }

    /// <summary>
    ///   Возвращает или задает <see cref="T:System.Runtime.Serialization.StreamingContext" /> для данного модуля форматирования.
    /// </summary>
    /// <returns>
    ///   Контекст потоковой передачи для использования с этим форматером.
    /// </returns>
    public StreamingContext Context
    {
      get
      {
        return this.m_context;
      }
      set
      {
        this.m_context = value;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter" /> со значениями по умолчанию.
    /// </summary>
    public BinaryFormatter()
    {
      this.m_surrogates = (ISurrogateSelector) null;
      this.m_context = new StreamingContext(StreamingContextStates.All);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter" /> класса с заданным суррогатным селектором и потоковым контекстом.
    /// </summary>
    /// <param name="selector">
    ///   Используемый <see cref="T:System.Runtime.Serialization.ISurrogateSelector" />.
    ///    Может иметь значение <see langword="null" />.
    /// </param>
    /// <param name="context">
    ///   Источник и назначение сериализованных данных.
    /// </param>
    public BinaryFormatter(ISurrogateSelector selector, StreamingContext context)
    {
      this.m_surrogates = selector;
      this.m_context = context;
    }

    /// <summary>Десериализует заданный поток в граф объектов.</summary>
    /// <param name="serializationStream">
    ///   Поток, из которого десериализуется граф объекта.
    /// </param>
    /// <returns>Верхний (корневой) графа объектов.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="serializationStream" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   <paramref name="serializationStream" /> Поддерживает поиск, но его длина равна 0.
    /// 
    ///   -или-
    /// 
    ///   Целевой тип данных — <see cref="T:System.Decimal" />, но значение находится за пределами диапазона <see cref="T:System.Decimal" /> типа.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public object Deserialize(Stream serializationStream)
    {
      return this.Deserialize(serializationStream, (HeaderHandler) null);
    }

    [SecurityCritical]
    internal object Deserialize(Stream serializationStream, HeaderHandler handler, bool fCheck)
    {
      return this.Deserialize(serializationStream, handler, fCheck, (IMethodCallMessage) null);
    }

    /// <summary>
    ///   Десериализует заданный поток в граф объектов.
    ///    Предоставленный <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" /> обрабатывает любые заголовки в этом потоке.
    /// </summary>
    /// <param name="serializationStream">
    ///   Поток, из которого десериализуется граф объекта.
    /// </param>
    /// <param name="handler">
    ///   <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" /> Которая обрабатывает любые заголовки в <paramref name="serializationStream" />.
    ///    Может иметь значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Десериализованный объект или верхний объект (корень) графа объектов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="serializationStream" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   <paramref name="serializationStream" /> Поддерживает поиск, но его длина равна 0.
    /// 
    ///   -или-
    /// 
    ///   Целевой тип данных — <see cref="T:System.Decimal" />, но значение находится за пределами диапазона <see cref="T:System.Decimal" /> типа.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    public object Deserialize(Stream serializationStream, HeaderHandler handler)
    {
      return this.Deserialize(serializationStream, handler, true);
    }

    /// <summary>
    ///   Десериализует ответ удаленный вызов метода из предоставленного <see cref="T:System.IO.Stream" />.
    /// </summary>
    /// <param name="serializationStream">
    ///   Поток, из которого десериализуется граф объекта.
    /// </param>
    /// <param name="handler">
    ///   <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" /> Которая обрабатывает любые заголовки в <paramref name="serializationStream" />.
    ///    Может иметь значение <see langword="null" />.
    /// </param>
    /// <param name="methodCallMessage">
    ///   <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> Содержащий сведения об источнике вызова.
    /// </param>
    /// <returns>Десериализованный ответ удаленного вызова метода.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="serializationStream" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   <paramref name="serializationStream" /> Поддерживает поиск, но его длина равна 0.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    public object DeserializeMethodResponse(Stream serializationStream, HeaderHandler handler, IMethodCallMessage methodCallMessage)
    {
      return this.Deserialize(serializationStream, handler, true, methodCallMessage);
    }

    /// <summary>
    ///   Десериализует заданный поток в граф объектов.
    ///    Предоставленный <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" /> обрабатывает любые заголовки в этом потоке.
    /// </summary>
    /// <param name="serializationStream">
    ///   Поток, из которого десериализуется граф объекта.
    /// </param>
    /// <param name="handler">
    ///   <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" /> Которая обрабатывает любые заголовки в <paramref name="serializationStream" />.
    ///    Может иметь значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Десериализованный объект или верхний объект (корень) графа объектов.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="serializationStream" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   <paramref name="serializationStream" /> Поддерживает поиск, но его длина равна 0.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecurityCritical]
    [ComVisible(false)]
    public object UnsafeDeserialize(Stream serializationStream, HeaderHandler handler)
    {
      return this.Deserialize(serializationStream, handler, false);
    }

    /// <summary>
    ///   Десериализует ответ удаленный вызов метода из предоставленного <see cref="T:System.IO.Stream" />.
    /// </summary>
    /// <param name="serializationStream">
    ///   Поток, из которого десериализуется граф объекта.
    /// </param>
    /// <param name="handler">
    ///   <see cref="T:System.Runtime.Remoting.Messaging.HeaderHandler" /> Которая обрабатывает любые заголовки в <paramref name="serializationStream" />.
    ///    Может иметь значение <see langword="null" />.
    /// </param>
    /// <param name="methodCallMessage">
    ///   <see cref="T:System.Runtime.Remoting.Messaging.IMethodCallMessage" /> Содержащий сведения об источнике вызова.
    /// </param>
    /// <returns>Десериализованный ответ удаленного вызова метода.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="serializationStream" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   <paramref name="serializationStream" /> Поддерживает поиск, но его длина равна 0.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecurityCritical]
    [ComVisible(false)]
    public object UnsafeDeserializeMethodResponse(Stream serializationStream, HeaderHandler handler, IMethodCallMessage methodCallMessage)
    {
      return this.Deserialize(serializationStream, handler, false, methodCallMessage);
    }

    [SecurityCritical]
    internal object Deserialize(Stream serializationStream, HeaderHandler handler, bool fCheck, IMethodCallMessage methodCallMessage)
    {
      return this.Deserialize(serializationStream, handler, fCheck, false, methodCallMessage);
    }

    [SecurityCritical]
    internal object Deserialize(Stream serializationStream, HeaderHandler handler, bool fCheck, bool isCrossAppDomain, IMethodCallMessage methodCallMessage)
    {
      if (serializationStream == null)
        throw new ArgumentNullException(nameof (serializationStream), Environment.GetResourceString("ArgumentNull_WithParamName", (object) serializationStream));
      if (serializationStream.CanSeek && serializationStream.Length == 0L)
        throw new SerializationException(Environment.GetResourceString("Serialization_Stream"));
      ObjectReader objectReader = new ObjectReader(serializationStream, this.m_surrogates, this.m_context, new InternalFE()
      {
        FEtypeFormat = this.m_typeFormat,
        FEserializerTypeEnum = InternalSerializerTypeE.Binary,
        FEassemblyFormat = this.m_assemblyFormat,
        FEsecurityLevel = this.m_securityLevel
      }, this.m_binder);
      objectReader.crossAppDomainArray = this.m_crossAppDomainArray;
      return objectReader.Deserialize(handler, new __BinaryParser(serializationStream, objectReader), fCheck, isCrossAppDomain, methodCallMessage);
    }

    /// <summary>
    ///   Сериализует объект или граф объектов с заданной вершиной (корнем) в заданном потоке.
    /// </summary>
    /// <param name="serializationStream">
    ///   Поток, в который выполняется сериализация графа.
    /// </param>
    /// <param name="graph">Объект в корне графа для сериализации.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="serializationStream" /> — <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="graph" /> Имеет значение null.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Произошла ошибка во время сериализации, например, если объект в <paramref name="graph" /> параметр не отмечен как сериализуемый.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    public void Serialize(Stream serializationStream, object graph)
    {
      this.Serialize(serializationStream, graph, (Header[]) null);
    }

    /// <summary>
    ///   Сериализует объект или граф объектов с заданной вершиной (корнем) в заданном потоке присоединение предоставленные заголовки.
    /// </summary>
    /// <param name="serializationStream">
    ///   Поток, в который выполняется сериализация объекта.
    /// </param>
    /// <param name="graph">Объект в корне графа для сериализации.</param>
    /// <param name="headers">
    ///   Удаленные заголовки, включаемые в сериализацию.
    ///    Может иметь значение <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Значение параметра <paramref name="serializationStream" /> — <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Произошла ошибка во время сериализации, например, если объект в <paramref name="graph" /> параметр не отмечен как сериализуемый.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    public void Serialize(Stream serializationStream, object graph, Header[] headers)
    {
      this.Serialize(serializationStream, graph, headers, true);
    }

    [SecurityCritical]
    internal void Serialize(Stream serializationStream, object graph, Header[] headers, bool fCheck)
    {
      if (serializationStream == null)
        throw new ArgumentNullException(nameof (serializationStream), Environment.GetResourceString("ArgumentNull_WithParamName", (object) serializationStream));
      ObjectWriter objectWriter = new ObjectWriter(this.m_surrogates, this.m_context, new InternalFE()
      {
        FEtypeFormat = this.m_typeFormat,
        FEserializerTypeEnum = InternalSerializerTypeE.Binary,
        FEassemblyFormat = this.m_assemblyFormat
      }, this.m_binder);
      __BinaryWriter serWriter = new __BinaryWriter(serializationStream, objectWriter, this.m_typeFormat);
      objectWriter.Serialize(graph, headers, serWriter, fCheck);
      this.m_crossAppDomainArray = objectWriter.crossAppDomainArray;
    }

    internal static TypeInformation GetTypeInformation(Type type)
    {
      lock (BinaryFormatter.typeNameCache)
      {
        TypeInformation typeInformation = (TypeInformation) null;
        if (!BinaryFormatter.typeNameCache.TryGetValue(type, out typeInformation))
        {
          bool hasTypeForwardedFrom;
          string clrAssemblyName = FormatterServices.GetClrAssemblyName(type, out hasTypeForwardedFrom);
          typeInformation = new TypeInformation(FormatterServices.GetClrTypeFullName(type), clrAssemblyName, hasTypeForwardedFrom);
          BinaryFormatter.typeNameCache.Add(type, typeInformation);
        }
        return typeInformation;
      }
    }
  }
}

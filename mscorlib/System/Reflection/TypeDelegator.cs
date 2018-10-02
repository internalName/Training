// Decompiled with JetBrains decompiler
// Type: System.Reflection.TypeDelegator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Создает оболочку для <see cref="T:System.Type" /> методы объекта и делегаты, <see langword="Type" />.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class TypeDelegator : TypeInfo
  {
    /// <summary>Значение, указывающее тип сведений.</summary>
    protected Type typeImpl;

    /// <summary>
    ///   Возвращает значение, указывающее, может ли указанный тип назначен этот тип.
    /// </summary>
    /// <param name="typeInfo">Проверяемый тип.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный тип может быть присвоен этому типу; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool IsAssignableFrom(TypeInfo typeInfo)
    {
      if ((Type) typeInfo == (Type) null)
        return false;
      return this.IsAssignableFrom(typeInfo.AsType());
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Reflection.TypeDelegator" /> стандартными свойствами.
    /// </summary>
    protected TypeDelegator()
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.TypeDelegator" /> класс, задавая инкапсулирующий экземпляр.
    /// </summary>
    /// <param name="delegatingType">
    ///   Экземпляр класса <see cref="T:System.Type" /> инкапсулирует вызов метода объекта.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="delegatingType" /> имеет значение <see langword="null" />.
    /// </exception>
    public TypeDelegator(Type delegatingType)
    {
      if (delegatingType == (Type) null)
        throw new ArgumentNullException(nameof (delegatingType));
      this.typeImpl = delegatingType;
    }

    /// <summary>
    ///   Возвращает идентификатор GUID (глобальный идентификатор) реализуемого типа.
    /// </summary>
    /// <returns>Идентификатор GUID.</returns>
    public override Guid GUID
    {
      get
      {
        return this.typeImpl.GUID;
      }
    }

    /// <summary>
    ///   Возвращает значение, которое идентифицирует этот объект в метаданных.
    /// </summary>
    /// <returns>
    ///   Значение, которое в сочетании с модулем, однозначно идентифицирует этот объект в метаданных.
    /// </returns>
    public override int MetadataToken
    {
      get
      {
        return this.typeImpl.MetadataToken;
      }
    }

    /// <summary>
    ///   Вызывает указанный член.
    ///    Вызываемый метод должен быть доступен и обеспечивать наиболее точное соответствие заданному списку аргументов с учетом ограничений заданного модуля привязки и атрибутов вызова.
    /// </summary>
    /// <param name="name">
    ///   Имя вызываемого члена.
    ///    Это может быть конструктор, метод, свойство или поле.
    ///    Если задана пустая строка ("») передается значение по умолчанию-член вызывается.
    /// </param>
    /// <param name="invokeAttr">
    ///   Атрибут вызова.
    ///    Это должен быть один из следующих <see cref="T:System.Reflection.BindingFlags" /> : <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="Static" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" />, или <see langword="SetProperty" />.
    ///    Необходимо указать подходящий атрибут вызова.
    ///    Должен быть вызван, если статический член <see langword="Static" /> должен быть установлен флаг.
    /// </param>
    /// <param name="binder">
    ///   Объект, позволяющий осуществлять привязку, приведение типов аргументов, вызов элементов, а также поиск объектов <see langword="MemberInfo" /> с помощью отражения.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    ///    См. раздел <see cref="T:System.Reflection.Binder" />.
    /// </param>
    /// <param name="target">
    ///   Объект, для которого следует вызвать указанный член.
    /// </param>
    /// <param name="args">
    ///   Массив объектов типа <see langword="Object" /> содержащий число, порядок и тип параметров члена.
    ///    Если <paramref name="args" /> содержит неинициализированный объект <see langword="Object" />, он обрабатывается как пустой и может быть преобразован по умолчанию связывателем в 0, 0,0 или строку.
    /// </param>
    /// <param name="modifiers">
    ///   Массив объектов типа <see langword="ParameterModifer" /> то же длины, как <paramref name="args" />, элементы которого представляют атрибуты, связанные с аргументами члена, вызываемого.
    ///    Параметр имеет атрибуты, связанные с ним в сигнатуре члена.
    ///    ByRef, используйте <see langword="ParameterModifer.ByRef" />, и при отсутствии параметра — <see langword="ParameterModifer.None" />.
    ///    Связыватель по умолчанию точное совпадение.
    ///    Атрибуты, такие как <see langword="In" /> и <see langword="InOut" /> не используется в привязке и могут быть просмотрены с помощью <see langword="ParameterInfo" />.
    /// </param>
    /// <param name="culture">
    ///   Экземпляр объекта <see langword="CultureInfo" />, используемого для управления приведением типов.
    ///    Это необходимо, например, чтобы преобразовать строку, представляющую число 1000, в <see langword="Double" /> значение, поскольку в разных культурах 1000 представляется по-разному.
    ///    Если <paramref name="culture" /> является <see langword="null" />,  <see langword="CultureInfo" /> для текущего потока <see langword="CultureInfo" /> используется.
    /// </param>
    /// <param name="namedParameters">
    ///   Массив объектов типа <see langword="String" /> содержащий имена параметров, совпадающие, начиная с нулевого элемента, с <paramref name="args" /> массива.
    ///    Существует не должно быть пустых в массиве.
    ///    Если <paramref name="args" />.
    ///    Значение <see langword="Length" /> больше значения <paramref name="namedParameters" />.
    ///   <see langword="Length" />, остальные параметры заполняются по порядку.
    /// </param>
    /// <returns>
    ///   <see langword="Object" /> Представляет возвращаемое значение вызываемого элемента.
    /// </returns>
    public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
    {
      return this.typeImpl.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
    }

    /// <summary>Возвращает модуль, содержащий реализованный тип.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Module" /> объект, представляющий модуль реализуемого типа.
    /// </returns>
    public override Module Module
    {
      get
      {
        return this.typeImpl.Module;
      }
    }

    /// <summary>Получает сборку реализуемого типа.</summary>
    /// <returns>
    ///   <see cref="T:System.Reflection.Assembly" /> Объект, представляющий сборку реализуемого типа.
    /// </returns>
    public override Assembly Assembly
    {
      get
      {
        return this.typeImpl.Assembly;
      }
    }

    /// <summary>
    ///   Возвращает дескриптор представления внутренних метаданных реализуемого типа.
    /// </summary>
    /// <returns>
    ///   Объект <see langword="RuntimeTypeHandle" />.
    /// </returns>
    public override RuntimeTypeHandle TypeHandle
    {
      get
      {
        return this.typeImpl.TypeHandle;
      }
    }

    /// <summary>Возвращает имя реализуемого типа с удалением пути.</summary>
    /// <returns>
    ///   A <see langword="String" /> содержащая неполное имя типа.
    /// </returns>
    public override string Name
    {
      get
      {
        return this.typeImpl.Name;
      }
    }

    /// <summary>Возвращает полное имя реализуемого типа.</summary>
    /// <returns>
    ///   A <see langword="String" /> содержащий полное имя типа.
    /// </returns>
    public override string FullName
    {
      get
      {
        return this.typeImpl.FullName;
      }
    }

    /// <summary>Возвращает пространство имен реализуемого типа.</summary>
    /// <returns>
    ///   A <see langword="String" /> содержащее пространство имен типа.
    /// </returns>
    public override string Namespace
    {
      get
      {
        return this.typeImpl.Namespace;
      }
    }

    /// <summary>Возвращает полное имя сборки.</summary>
    /// <returns>
    ///   A <see langword="String" /> содержащая полное имя сборки.
    /// </returns>
    public override string AssemblyQualifiedName
    {
      get
      {
        return this.typeImpl.AssemblyQualifiedName;
      }
    }

    /// <summary>Возвращает базовый тип текущего типа.</summary>
    /// <returns>Базовый тип для типа.</returns>
    public override Type BaseType
    {
      get
      {
        return this.typeImpl.BaseType;
      }
    }

    /// <summary>
    ///   Возвращает конструктор, который реализован <see langword="TypeDelegator" />.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, влияющая на способ выполнения поиска.
    ///    Значение представляет собой сочетание нуля или более битовых флагов из <see cref="T:System.Reflection.BindingFlags" />.
    /// </param>
    /// <param name="binder">
    ///   Объект, позволяющий осуществлять привязку, приведение типов аргументов, вызов элементов, а также поиск объектов <see langword="MemberInfo" /> с помощью отражения.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    /// </param>
    /// <param name="callConvention">Соглашения о вызовах.</param>
    /// <param name="types">
    ///   Массив объектов типа <see langword="Type" /> списком номер параметра, порядок и типы.
    ///    Типы не могут быть <see langword="null" />; использовать соответствующий <see langword="GetMethod" /> метода или пустой массив для поиска метода без параметров.
    /// </param>
    /// <param name="modifiers">
    ///   Массив объектов типа <see langword="ParameterModifier" /> того же длины, что <paramref name="types" /> массив, элементы которого представляют атрибуты, связанные с параметрами метода.
    /// </param>
    /// <returns>
    ///   Объект <see langword="ConstructorInfo" /> объект для метода, который соответствует заданным критериям, или <see langword="null" /> если совпадение не найдено.
    /// </returns>
    protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      return this.typeImpl.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
    }

    /// <summary>
    ///   Возвращает массив <see cref="T:System.Reflection.ConstructorInfo" /> объекты, представляющие конструкторы, определенные для типа, инкапсулированного в текущем <see cref="T:System.Reflection.TypeDelegator" />.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, влияющая на способ выполнения поиска.
    ///    Значение представляет собой сочетание нуля или более битовых флагов из <see cref="T:System.Reflection.BindingFlags" />.
    /// </param>
    /// <returns>
    ///   Массив объектов типа <see langword="ConstructorInfo" /> содержащий заданные конструкторы, определенные для этого класса.
    ///    Если конструкторы не определены, возвращается пустой массив.
    ///    В зависимости от значения заданного параметра возвращаются только открытые конструкторы или открытые и закрытые конструкторы.
    /// </returns>
    [ComVisible(true)]
    public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
    {
      return this.typeImpl.GetConstructors(bindingAttr);
    }

    /// <summary>
    ///   Ищет метод с параметрами, соответствующими указанным модификаторам и типам аргументов, с учетом заданных ограничений привязки и соглашений о вызовах.
    /// </summary>
    /// <param name="name">Имя метода.</param>
    /// <param name="bindingAttr">
    ///   Битовая маска, влияющая на способ выполнения поиска.
    ///    Значение представляет собой сочетание нуля или более битовых флагов из <see cref="T:System.Reflection.BindingFlags" />.
    /// </param>
    /// <param name="binder">
    ///   Объект, позволяющий осуществлять привязку, приведение типов аргументов, вызов элементов, а также поиск объектов <see langword="MemberInfo" /> с помощью отражения.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    /// </param>
    /// <param name="callConvention">Соглашения о вызовах.</param>
    /// <param name="types">
    ///   Массив объектов типа <see langword="Type" /> списком номер параметра, порядок и типы.
    ///    Типы не могут быть <see langword="null" />; использовать соответствующий <see langword="GetMethod" /> метода или пустой массив для поиска метода без параметров.
    /// </param>
    /// <param name="modifiers">
    ///   Массив объектов типа <see langword="ParameterModifier" /> того же длины, что <paramref name="types" /> массив, элементы которого представляют атрибуты, связанные с параметрами метода.
    /// </param>
    /// <returns>
    ///   A <see langword="MethodInfoInfo" /> объекта для реализации метода, который соответствует заданным критериям, или <see langword="null" /> если совпадение не найдено.
    /// </returns>
    protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      if (types == null)
        return this.typeImpl.GetMethod(name, bindingAttr);
      return this.typeImpl.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
    }

    /// <summary>
    ///   Возвращает массив <see cref="T:System.Reflection.MethodInfo" /> объекты, представляющие методы указанного типа, инкапсулированного в текущем <see cref="T:System.Reflection.TypeDelegator" />.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, влияющая на способ выполнения поиска.
    ///    Значение представляет собой сочетание нуля или более битовых флагов из <see cref="T:System.Reflection.BindingFlags" />.
    /// </param>
    /// <returns>
    ///   Массив <see langword="MethodInfo" /> объекты, представляющие методы, определенные для данного <see langword="TypeDelegator" />.
    /// </returns>
    public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
    {
      return this.typeImpl.GetMethods(bindingAttr);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Reflection.FieldInfo" /> объект, представляющий поле с указанным именем.
    /// </summary>
    /// <param name="name">Имя поля для поиска.</param>
    /// <param name="bindingAttr">
    ///   Битовая маска, влияющая на способ выполнения поиска.
    ///    Значение представляет собой сочетание нуля или более битовых флагов из <see cref="T:System.Reflection.BindingFlags" />.
    /// </param>
    /// <returns>
    ///   Объект <see langword="FieldInfo" /> объект, представляющий поле объявлено или унаследовано это <see langword="TypeDelegator" /> с указанным именем.
    ///    Возвращает <see langword="null" /> Если поле не найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    public override FieldInfo GetField(string name, BindingFlags bindingAttr)
    {
      return this.typeImpl.GetField(name, bindingAttr);
    }

    /// <summary>
    ///   Возвращает массив <see cref="T:System.Reflection.FieldInfo" /> объектов, представляющих поля данных определен для типа, оболочкой для которого текущий <see cref="T:System.Reflection.TypeDelegator" />.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, влияющая на способ выполнения поиска.
    ///    Значение представляет собой сочетание нуля или более битовых флагов из <see cref="T:System.Reflection.BindingFlags" />.
    /// </param>
    /// <returns>
    ///   Массив объектов типа <see langword="FieldInfo" /> с полями, которое объявлено или унаследовано текущим <see langword="TypeDelegator" />.
    ///    Если нет соответствующих полей, возвращается пустой массив.
    /// </returns>
    public override FieldInfo[] GetFields(BindingFlags bindingAttr)
    {
      return this.typeImpl.GetFields(bindingAttr);
    }

    /// <summary>
    ///   Возвращает заданный интерфейс, реализованный в оболочку текущим типом <see cref="T:System.Reflection.TypeDelegator" />.
    /// </summary>
    /// <param name="name">
    ///   Полное имя интерфейса, реализуемого текущего класса.
    /// </param>
    /// <param name="ignoreCase">
    ///   <see langword="true" /> Если регистр не должен учитываться; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Объект <see langword="Type" /> объект, представляющий интерфейс, реализованный (прямо или косвенно) в текущем классе, с полным именем, соответствующим заданному имени.
    ///    Если интерфейс с совпадающим именем не найден, значение null возвращается.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    public override Type GetInterface(string name, bool ignoreCase)
    {
      return this.typeImpl.GetInterface(name, ignoreCase);
    }

    /// <summary>
    ///   Возвращает все интерфейсы, реализованные в текущем классе и его базовых классов.
    /// </summary>
    /// <returns>
    ///   Массив объектов типа <see langword="Type" /> содержащий все интерфейсы, реализованные в текущем классе и его базовых классов.
    ///    Если они не определены, возвращается пустой массив.
    /// </returns>
    public override Type[] GetInterfaces()
    {
      return this.typeImpl.GetInterfaces();
    }

    /// <summary>Возвращает указанное событие.</summary>
    /// <param name="name">Имя возвращаемого события.</param>
    /// <param name="bindingAttr">
    ///   Битовая маска, влияющая на способ выполнения поиска.
    ///    Значение представляет собой сочетание нуля или более битовых флагов из <see cref="T:System.Reflection.BindingFlags" />.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Reflection.EventInfo" /> Объект, представляющий событие объявлено или унаследовано этот тип с указанным именем.
    ///    Этот метод возвращает <see langword="null" /> если такое событие найдено.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
    {
      return this.typeImpl.GetEvent(name, bindingAttr);
    }

    /// <summary>
    ///   Возвращает массив <see cref="T:System.Reflection.EventInfo" /> объектов, представляющий все открытые события, объявленные или наследуемые текущим <see langword="TypeDelegator" />.
    /// </summary>
    /// <returns>
    ///   Возвращает массив объектов типа <see langword="EventInfo" /> содержащий все события, которое объявлено или унаследовано текущим типом.
    ///    Если нет событий, возвращается пустой массив.
    /// </returns>
    public override EventInfo[] GetEvents()
    {
      return this.typeImpl.GetEvents();
    }

    /// <summary>
    ///   При переопределении в производном классе выполняет поиск заданного свойства, параметры которого соответствуют типам и модификаторам заданных аргументов, с использованием заданных ограничений привязки.
    /// </summary>
    /// <param name="name">Возвращаемое свойство.</param>
    /// <param name="bindingAttr">
    ///   Битовая маска, влияющая на способ выполнения поиска.
    ///    Значение представляет собой сочетание нуля или более битовых флагов из <see cref="T:System.Reflection.BindingFlags" />.
    /// </param>
    /// <param name="binder">
    ///   Объект, позволяющий осуществлять привязку, приведение типов аргументов, вызов элементов, а также поиск объектов <see langword="MemberInfo" /> с помощью отражения.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    ///    См. раздел <see cref="T:System.Reflection.Binder" />.
    /// </param>
    /// <param name="returnType">
    ///   Тип возвращаемого значения свойства.
    /// </param>
    /// <param name="types">
    ///   Список типов параметров.
    ///    Список представляет число, порядок и типы параметров.
    ///    Типы не могут иметь значение null; использовать соответствующий <see langword="GetMethod" /> метода или пустой массив для поиска метода без параметров.
    /// </param>
    /// <param name="modifiers">
    ///   Массив такую же длину, что и типы элементов, представляющих атрибуты, связанные с параметрами метода.
    /// </param>
    /// <returns>
    ///   A <see cref="T:System.Reflection.PropertyInfo" /> объекта для свойства, которое соответствует заданным критериям, или значение null, если совпадение не найдено.
    /// </returns>
    protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
    {
      if (returnType == (Type) null && types == null)
        return this.typeImpl.GetProperty(name, bindingAttr);
      return this.typeImpl.GetProperty(name, bindingAttr, binder, returnType, types, modifiers);
    }

    /// <summary>
    ///   Возвращает массив <see cref="T:System.Reflection.PropertyInfo" /> объекты, представляющие свойства типа, оболочкой для которого текущий <see cref="T:System.Reflection.TypeDelegator" />.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, влияющая на способ выполнения поиска.
    ///    Значение представляет собой сочетание нуля или более битовых флагов из <see cref="T:System.Reflection.BindingFlags" />.
    /// </param>
    /// <returns>
    ///   Массив <see langword="PropertyInfo" /> объектов, представляющих свойства, определенные для данного <see langword="TypeDelegator" />.
    /// </returns>
    public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
    {
      return this.typeImpl.GetProperties(bindingAttr);
    }

    /// <summary>
    ///   Возвращает события, определенные в <paramref name="bindingAttr" /> объявлены или унаследованы текущим объектом <see langword="TypeDelegator" />.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, влияющая на способ выполнения поиска.
    ///    Значение представляет собой сочетание нуля или более битовых флагов из <see cref="T:System.Reflection.BindingFlags" />.
    /// </param>
    /// <returns>
    ///   Массив объектов типа <see langword="EventInfo" /> содержащий события, заданные в <paramref name="bindingAttr" />.
    ///    Если нет событий, возвращается пустой массив.
    /// </returns>
    public override EventInfo[] GetEvents(BindingFlags bindingAttr)
    {
      return this.typeImpl.GetEvents(bindingAttr);
    }

    /// <summary>
    ///   Возвращает вложенные типы, заданные в <paramref name="bindingAttr" /> объявленные или унаследованные типом, оболочкой для которого текущий <see cref="T:System.Reflection.TypeDelegator" />.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, влияющая на способ выполнения поиска.
    ///    Значение представляет собой сочетание нуля или более битовых флагов из <see cref="T:System.Reflection.BindingFlags" />.
    /// </param>
    /// <returns>
    ///   Массив объектов типа <see langword="Type" /> содержащий вложенные типы.
    /// </returns>
    public override Type[] GetNestedTypes(BindingFlags bindingAttr)
    {
      return this.typeImpl.GetNestedTypes(bindingAttr);
    }

    /// <summary>
    ///   Возвращает вложенный тип, заданный в <paramref name="name" /> и <paramref name="bindingAttr" /> объявленные или унаследованные типом, представленный текущим <see cref="T:System.Reflection.TypeDelegator" />.
    /// </summary>
    /// <param name="name">Имя вложенного типа.</param>
    /// <param name="bindingAttr">
    ///   Битовая маска, влияющая на способ выполнения поиска.
    ///    Значение представляет собой сочетание нуля или более битовых флагов из <see cref="T:System.Reflection.BindingFlags" />.
    /// </param>
    /// <returns>
    ///   Объект <see langword="Type" /> объект, предоставляющий вложенный тип.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    public override Type GetNestedType(string name, BindingFlags bindingAttr)
    {
      return this.typeImpl.GetNestedType(name, bindingAttr);
    }

    /// <summary>
    ///   Возвращает члены (свойства, методы, конструкторы, поля, события и вложенные типы), задаваемые по данной <paramref name="name" />, <paramref name="type" />, и <paramref name="bindingAttr" />.
    /// </summary>
    /// <param name="name">Имя члена, который требуется получить.</param>
    /// <param name="type">
    ///   Битовая маска, влияющая на способ выполнения поиска.
    ///    Значение представляет собой сочетание нуля или более битовых флагов из <see cref="T:System.Reflection.BindingFlags" />.
    /// </param>
    /// <param name="bindingAttr">Тип возвращаемых элементов.</param>
    /// <returns>
    ///   Массив объектов типа <see langword="MemberInfo" /> содержащего все элементы текущего класса и его базовых классов, удовлетворяющие заданным критериям.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
    {
      return this.typeImpl.GetMember(name, type, bindingAttr);
    }

    /// <summary>
    ///   Возвращает члены, задаваемые параметром <paramref name="bindingAttr" />.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, влияющая на способ выполнения поиска.
    ///    Значение представляет собой сочетание нуля или более битовых флагов из <see cref="T:System.Reflection.BindingFlags" />.
    /// </param>
    /// <returns>
    ///   Массив объектов типа <see langword="MemberInfo" /> содержащего все элементы текущего класса и его базовых классов, удовлетворяющие <paramref name="bindingAttr" /> фильтра.
    /// </returns>
    public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
    {
      return this.typeImpl.GetMembers(bindingAttr);
    }

    /// <summary>
    ///   Возвращает атрибуты, назначенные <see langword="TypeDelegator" />.
    /// </summary>
    /// <returns>
    ///   Объект <see langword="TypeAttributes" /> объект, представляющий флаги атрибутов реализации.
    /// </returns>
    protected override TypeAttributes GetAttributeFlagsImpl()
    {
      return this.typeImpl.Attributes;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли <see cref="T:System.Type" /> является массивом.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если <see cref="T:System.Type" /> является массивом; в противном случае — значение <see langword="false" />.
    /// </returns>
    protected override bool IsArrayImpl()
    {
      return this.typeImpl.IsArray;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли <see cref="T:System.Type" /> является одним из типов-примитивов.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> является одним из типов-примитивов; в противном случае — значение <see langword="false" />.
    /// </returns>
    protected override bool IsPrimitiveImpl()
    {
      return this.typeImpl.IsPrimitive;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли <see cref="T:System.Type" /> передается по ссылке.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> передан по ссылке; в противном случае — значение <see langword="false" />.
    /// </returns>
    protected override bool IsByRefImpl()
    {
      return this.typeImpl.IsByRef;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли <see cref="T:System.Type" /> является указателем.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если <see cref="T:System.Type" /> является указателем; в противном случае — значение <see langword="false" />.
    /// </returns>
    protected override bool IsPointerImpl()
    {
      return this.typeImpl.IsPointer;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли тип типом значения; то есть, не является классом или интерфейсом.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если тип является типом значения; в противном случае — <see langword="false" />.
    /// </returns>
    protected override bool IsValueTypeImpl()
    {
      return this.typeImpl.IsValueType;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли <see cref="T:System.Type" /> является COM-объектом.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если <see cref="T:System.Type" /> является COM-объектом, в противном случае — значение <see langword="false" />.
    /// </returns>
    protected override bool IsCOMObjectImpl()
    {
      return this.typeImpl.IsCOMObject;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, представляет ли этот данный объект сконструированный универсальный тип.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если этот объект представляет сконструированный универсальный тип; в противном случае — значение <see langword="false" />.
    /// </returns>
    public override bool IsConstructedGenericType
    {
      get
      {
        return this.typeImpl.IsConstructedGenericType;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Type" /> входящей объекта, на который ссылается данный массив, указатель или ByRef.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Type" /> Объекта, используемого или упоминаемого в текущем массиве, указатель или <see langword="ByRef" />, или <see langword="null" /> Если текущий <see cref="T:System.Type" /> не является массивом, указателем или параметром <see langword="ByRef" />.
    /// </returns>
    public override Type GetElementType()
    {
      return this.typeImpl.GetElementType();
    }

    /// <summary>
    ///   Возвращает значение, указывающее ли текущий <see cref="T:System.Type" /> использует или обращается к другому типу; это, является ли текущий <see cref="T:System.Type" /> является массивом, указателем или параметром ByRef.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="T:System.Type" /> является массивом, указателем или параметром ByRef; в противном случае — <see langword="false" />.
    /// </returns>
    protected override bool HasElementTypeImpl()
    {
      return this.typeImpl.HasElementType;
    }

    /// <summary>
    ///   Возвращает базовый <see cref="T:System.Type" /> представляющий реализуемого типа.
    /// </summary>
    /// <returns>Базовый тип.</returns>
    public override Type UnderlyingSystemType
    {
      get
      {
        return this.typeImpl.UnderlyingSystemType;
      }
    }

    /// <summary>
    ///   Возвращает пользовательские атрибуты, определенные для этого типа, указывая, следует ли для поиска цепочки наследования этого типа.
    /// </summary>
    /// <param name="inherit">
    ///   Определяет необходимость поиска цепочки наследования этого типа для поиска атрибутов.
    /// </param>
    /// <returns>
    ///   Массив объектов, содержащий все пользовательские атрибуты, определенные для этого типа.
    /// </returns>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    public override object[] GetCustomAttributes(bool inherit)
    {
      return this.typeImpl.GetCustomAttributes(inherit);
    }

    /// <summary>
    ///   Возвращает массив настраиваемых атрибутов, принадлежащих указанному типу.
    /// </summary>
    /// <param name="attributeType">
    ///   Массив настраиваемых атрибутов, принадлежащих указанному типу.
    /// </param>
    /// <param name="inherit">
    ///   Определяет необходимость поиска цепочки наследования этого типа для поиска атрибутов.
    /// </param>
    /// <returns>
    ///   Массив объектов, содержащий пользовательские атрибуты, определенные в этом типе, которые соответствуют <paramref name="attributeType" /> параметр, указывающий, следует ли выполнять поиск цепочки наследования типа или <see langword="null" /> Если пользовательские атрибуты не определены для этого типа.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    public override object[] GetCustomAttributes(Type attributeType, bool inherit)
    {
      return this.typeImpl.GetCustomAttributes(attributeType, inherit);
    }

    /// <summary>
    ///   Указывает, является ли пользовательский атрибут, заданный <paramref name="attributeType" /> определен.
    /// </summary>
    /// <param name="attributeType">
    ///   Определяет необходимость поиска цепочки наследования этого типа для поиска атрибутов.
    /// </param>
    /// <param name="inherit">
    ///   Массив настраиваемых атрибутов, принадлежащих указанному типу.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если пользовательский атрибут, заданный параметром <paramref name="attributeType" /> определен; в противном случае — <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.ReflectionTypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    public override bool IsDefined(Type attributeType, bool inherit)
    {
      return this.typeImpl.IsDefined(attributeType, inherit);
    }

    /// <summary>
    ///   Возвращает сопоставление для интерфейса заданного типа.
    /// </summary>
    /// <param name="interfaceType">
    ///   <see cref="T:System.Type" /> Интерфейса для получения сопоставление.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Reflection.InterfaceMapping" /> Объект, представляющий сопоставление интерфейса для <paramref name="interfaceType" />.
    /// </returns>
    [ComVisible(true)]
    public override InterfaceMapping GetInterfaceMap(Type interfaceType)
    {
      return this.typeImpl.GetInterfaceMap(interfaceType);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Type
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System
{
  /// <summary>
  ///   Представляет объявления типов для классов, интерфейсов, массивов, значений, перечислений параметров, определений универсальных типов и открытых или закрытых сконструированных универсальных типов.
  /// 
  ///   Исходный код .NET Framework для этого типа см. в указанном источнике.
  /// </summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_Type))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class Type : MemberInfo, _Type, IReflect
  {
    /// <summary>
    ///   Предоставляет фильтр членов, используемый для атрибутов.
    ///    Это поле доступно только для чтения.
    /// </summary>
    public static readonly MemberFilter FilterAttribute = new MemberFilter(__Filters.Instance.FilterAttribute);
    /// <summary>
    ///   Представляет фильтр членов с учетом регистра, применяемый к именам.
    ///    Это поле доступно только для чтения.
    /// </summary>
    public static readonly MemberFilter FilterName = new MemberFilter(__Filters.Instance.FilterName);
    /// <summary>
    ///   Представляет фильтр членов без учета регистра, применяемый к именам.
    ///    Это поле доступно только для чтения.
    /// </summary>
    public static readonly MemberFilter FilterNameIgnoreCase = new MemberFilter(__Filters.Instance.FilterIgnoreCase);
    /// <summary>
    ///   Представляет отсутствующее значение в данных объекта <see cref="T:System.Type" />.
    ///    Это поле доступно только для чтения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly object Missing = (object) System.Reflection.Missing.Value;
    /// <summary>
    ///   Разделяет имена в пространстве имен класса <see cref="T:System.Type" />.
    ///    Это поле доступно только для чтения.
    /// </summary>
    public static readonly char Delimiter = '.';
    /// <summary>
    ///   Представляет пустой массив типа <see cref="T:System.Type" />.
    ///    Это поле доступно только для чтения.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly Type[] EmptyTypes = EmptyArray<Type>.Value;
    private static Binder defaultBinder;
    private const BindingFlags DefaultLookup = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
    internal const BindingFlags DeclaredOnlyLookup = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

    /// <summary>
    ///   Возвращает значение <see cref="T:System.Reflection.MemberTypes" />, позволяющее определить, каким типом является этот член: обычным или вложенным.
    /// </summary>
    /// <returns>
    ///   Значение <see cref="T:System.Reflection.MemberTypes" />, позволяющее определить, каким типом является этот член: обычным или вложенным.
    /// </returns>
    public override MemberTypes MemberType
    {
      get
      {
        return MemberTypes.TypeInfo;
      }
    }

    /// <summary>
    ///   Возвращает тип, объявивший текущий вложенный тип или параметр универсального типа.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />, представляющий включающий тип, если текущий тип является вложенным, или определение универсального типа, если текущий тип является параметром универсального типа, или тип, объявивший этот универсальный метод, если текущий тип является параметром типа универсального метода; в противном случае — значение <see langword="null" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override Type DeclaringType
    {
      [__DynamicallyInvokable] get
      {
        return (Type) null;
      }
    }

    /// <summary>
    ///   Возвращает метод <see cref="T:System.Reflection.MethodBase" />, который представляет объявляемый метод, если текущий <see cref="T:System.Type" /> представляет параметр типа универсального метода.
    /// </summary>
    /// <returns>
    ///   Если текущий объект <see cref="T:System.Type" /> представляет параметр типа универсального метода, класс <see cref="T:System.Reflection.MethodBase" />, представляющий объявляемый метод; в противном случае — значение <see langword="null" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual MethodBase DeclaringMethod
    {
      [__DynamicallyInvokable] get
      {
        return (MethodBase) null;
      }
    }

    /// <summary>
    ///   Возвращает объект класса, который использовался для получения этого члена.
    /// </summary>
    /// <returns>
    ///   Объект <see langword="Type" />, с помощью которого был получен данный объект <see cref="T:System.Type" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override Type ReflectedType
    {
      [__DynamicallyInvokable] get
      {
        return (Type) null;
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Type" /> с указанным именем, позволяющий определить, будет ли создаваться исключение в случае невозможности найти тип и будет ли учитываться регистр при поиске.
    /// </summary>
    /// <param name="typeName">
    ///   Имя искомого типа с указанием сборки.
    ///    См. раздел <see cref="P:System.Type.AssemblyQualifiedName" />.
    ///    Если тип находится в выполняемой в данный момент сборке или библиотеке Mscorlib.dll, достаточно предоставить имя типа с указанием пространства имен.
    /// </param>
    /// <param name="throwOnError">
    ///   Значение <see langword="true" />, чтобы при невозможности найти тип создавалось исключение; значение <see langword="false" />, чтобы возвращалось значение <see langword="null" />. При задании значения <see langword="false" /> также подавляются некоторые другие условия исключения, однако не все.
    ///    См. раздел "Исключения".
    /// </param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" />, чтобы не учитывать регистр при поиске <paramref name="typeName" />, значение <see langword="false" />, чтобы учитывать регистр при поиске <paramref name="typeName" />.
    /// </param>
    /// <returns>
    ///   Тип с указанным именем.
    ///    Если тип не найден, параметр <paramref name="throwOnError" /> определяет дальнейшее действие — возврат значения <see langword="null" /> или создание исключения.
    ///    В некоторых случаях исключение создается независимо от значения параметра <paramref name="throwOnError" />.
    ///    См. раздел "Исключения".
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Инициализатор класса вызывается и создает исключение.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и тип не найден.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и <paramref name="typeName" /> содержит недопустимые знаки, например внедренные табуляции.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и <paramref name="typeName" /> является пустой строкой.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и <paramref name="typeName" /> представляет тип массива с недопустимым размером.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет массив <see cref="T:System.TypedReference" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и <paramref name="typeName" /> содержит недопустимый синтаксис.
    ///    Например, "MyType[,*,]".
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет универсальный тип, имеющий тип указателя, тип <see langword="ByRef" /> или <see cref="T:System.Void" /> в качестве одного из его аргументов типа.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет универсальный тип, который содержит неправильное количество аргументов типа.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет универсальный тип, и один из его аргументов типа не удовлетворяет ограничениям для соответствующего параметра типа.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и не удалось найти сборку либо одну из ее зависимостей.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или одна из ее зависимостей найдена, но не может быть загружена.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Сборка или одна из ее зависимостей является недопустимой.
    /// 
    ///   -или-
    /// 
    ///   В текущий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка была скомпилирована в более поздней версии.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Type GetType(string typeName, bool throwOnError, bool ignoreCase)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Type) RuntimeType.GetType(typeName, throwOnError, ignoreCase, false, ref stackMark);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Type" /> с заданным именем, выполняя поиск с учетом регистра и указывая, будет ли создаваться исключение в случае невозможности найти тип.
    /// </summary>
    /// <param name="typeName">
    ///   Имя искомого типа с указанием сборки.
    ///    См. раздел <see cref="P:System.Type.AssemblyQualifiedName" />.
    ///    Если тип находится в выполняемой в данный момент сборке или библиотеке Mscorlib.dll, достаточно предоставить имя типа с указанием пространства имен.
    /// </param>
    /// <param name="throwOnError">
    ///   Значение <see langword="true" />, чтобы создать исключение, если тип не удается найти; значение <see langword="false" />, чтобы вернуть значение <see langword="null" />.
    ///    Кроме того, при указании значения <see langword="false" /> подавляются некоторые другие условия возникновения исключений, но не все из них.
    ///    См. раздел "Исключения".
    /// </param>
    /// <returns>
    ///   Тип с указанным именем.
    ///    Если тип не найден, параметр <paramref name="throwOnError" /> определяет дальнейшее действие — возврат значения <see langword="null" /> или создание исключения.
    ///    В некоторых случаях исключение создается независимо от значения параметра <paramref name="throwOnError" />.
    ///    См. раздел "Исключения".
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Инициализатор класса вызывается и создает исключение.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и тип не найден.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и <paramref name="typeName" /> содержит недопустимые знаки, например внедренные табуляции.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и <paramref name="typeName" /> является пустой строкой.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и <paramref name="typeName" /> представляет тип массива с недопустимым размером.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет массив <see cref="T:System.TypedReference" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и <paramref name="typeName" /> содержит недопустимый синтаксис.
    ///    Например, "MyType[,*,]".
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет универсальный тип, имеющий тип указателя, тип <see langword="ByRef" /> или <see cref="T:System.Void" /> в качестве одного из его аргументов типа.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет универсальный тип, который содержит неправильное количество аргументов типа.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет универсальный тип, и один из его аргументов типа не удовлетворяет ограничениям для соответствующего параметра типа.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и не удалось найти сборку либо одну из ее зависимостей.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение базового класса <see cref="T:System.IO.IOException" />.
    /// 
    ///   Сборка или одна из ее зависимостей найдена, но не может быть загружена.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Сборка или одна из ее зависимостей является недопустимой.
    /// 
    ///   -или-
    /// 
    ///   В текущий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка была скомпилирована в более поздней версии.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Type GetType(string typeName, bool throwOnError)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Type) RuntimeType.GetType(typeName, throwOnError, false, false, ref stackMark);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Type" /> с указанным именем, учитывая при поиске регистр.
    /// </summary>
    /// <param name="typeName">
    ///   Имя искомого типа с указанием сборки.
    ///    См. раздел <see cref="P:System.Type.AssemblyQualifiedName" />.
    ///    Если тип находится в выполняемой в данный момент сборке или библиотеке Mscorlib.dll, достаточно предоставить имя типа с указанием пространства имен.
    /// </param>
    /// <returns>
    ///   Тип с указанным именем, если он существует; в противном случае — значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Инициализатор класса вызывается и создает исключение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="typeName" /> представляет универсальный тип, имеющий тип указателя, тип <see langword="ByRef" /> или <see cref="T:System.Void" /> в качестве одного из его аргументов типа.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет универсальный тип, который содержит неправильное количество аргументов типа.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет универсальный тип, и один из его аргументов типа не удовлетворяет ограничениям для соответствующего параметра типа.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="typeName" /> представляет массив <see cref="T:System.TypedReference" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///     В .NET for Windows Store apps или переносимой библиотеки классов, перехват исключения базового класса, <see cref="T:System.IO.IOException" />, вместо этого.
    /// 
    ///   Сборка или одна из ее зависимостей найдена, но не может быть загружена.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Сборка или одна из ее зависимостей является недопустимой.
    /// 
    ///   -или-
    /// 
    ///   В текущий момент загружена среда CLR версии 2.0 или более поздней версии. Сборка была скомпилирована в более поздней версии.
    /// </exception>
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Type GetType(string typeName)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Type) RuntimeType.GetType(typeName, false, false, false, ref stackMark);
    }

    /// <summary>
    ///   Возвращает тип с указанным именем; дополнительно может предоставлять настраиваемые методы для разрешения сборки и типа.
    /// </summary>
    /// <param name="typeName">
    ///   Имя получаемого типа.
    ///    Если задан параметр <paramref name="typeResolver" />, имя типа может быть любой строкой, которую может разрешить объект <paramref name="typeResolver" />.
    ///    Если задан параметр <paramref name="assemblyResolver" /> или если используется стандартное разрешение типов, параметр <paramref name="typeName" /> должен быть именем с указанием сборки (см. описание свойства <see cref="P:System.Type.AssemblyQualifiedName" />), кроме случаев, когда этот тип находится в текущей выполняемой сборке или в библиотеке Mscorlib.dll — тогда достаточно задать имя типа с указанием пространства имен.
    /// </param>
    /// <param name="assemblyResolver">
    ///   Метод, находящий и возвращающий сборку, заданную в параметре <paramref name="typeName" />.
    ///    Имя сборки передается методу <paramref name="assemblyResolver" /> в виде объекта <see cref="T:System.Reflection.AssemblyName" />.
    ///    Если объект <paramref name="typeName" /> не содержит имя сборки, метод <paramref name="assemblyResolver" /> не вызывается.
    ///    Если метод <paramref name="assemblyResolver" /> не указан, выполняется стандартное разрешение сборки.
    /// 
    ///   Внимание. Не передавайте методы от неизвестных или не имеющих доверия вызывающих модулей.
    ///    В противном случае возможно повышение привилегий для вредоносного кода.
    ///    Рекомендуется использовать только методы, предоставленные пользователями или знакомые им.
    /// </param>
    /// <param name="typeResolver">
    ///   Метод, находящий и возвращающий тип, заданный в параметре <paramref name="typeName" />, из сборки, возвращенной методом <paramref name="assemblyResolver" /> или стандартным методом разрешения сборки.
    ///    Если сборка не предоставлена, ее может предоставить метод <paramref name="typeResolver" />.
    ///    Метод также принимает параметр, указывающий, следует ли выполнять поиск без учета регистра; этому параметру передается значение <see langword="false" />.
    /// 
    ///   Внимание. Не передавайте методы от неизвестных или не имеющих доверия вызывающих модулей.
    /// </param>
    /// <returns>
    ///   Тип с указанным именем или значение <see langword="null" />, если тип не найден.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Инициализатор класса вызывается и создает исключение.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Произошла ошибка при интерпретации <paramref name="typeName" /> в имя типа и имя сборки (например, если имя простого типа содержит неэкранированный специальный знак).
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет универсальный тип, имеющий тип указателя, тип <see langword="ByRef" /> или <see cref="T:System.Void" /> в качестве одного из его аргументов типа.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет универсальный тип, который содержит неправильное количество аргументов типа.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет универсальный тип, и один из его аргументов типа не удовлетворяет ограничениям для соответствующего параметра типа.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="typeName" /> представляет массив <see cref="T:System.TypedReference" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или одна из ее зависимостей найдена, но не может быть загружена.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> содержит недопустимое имя сборки.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> является допустимым именем сборки без имени типа.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Сборка или одна из ее зависимостей является недопустимой.
    /// 
    ///   -или-
    /// 
    ///   Сборка была скомпилирована в более поздней версии среды CLR, чем версия, загруженная в текущий момент.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Type GetType(string typeName, Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TypeNameParser.GetType(typeName, assemblyResolver, typeResolver, false, false, ref stackMark);
    }

    /// <summary>
    ///   Возвращает тип с заданным именем и указывает, следует ли создавать исключение в случае невозможности найти тип, а также может предоставлять настраиваемые методы для разрешения сборки и типа.
    /// </summary>
    /// <param name="typeName">
    ///   Имя получаемого типа.
    ///    Если задан параметр <paramref name="typeResolver" />, имя типа может быть любой строкой, которую может разрешить объект <paramref name="typeResolver" />.
    ///    Если задан параметр <paramref name="assemblyResolver" /> или если используется стандартное разрешение типов, параметр <paramref name="typeName" /> должен быть именем с указанием сборки (см. описание свойства <see cref="P:System.Type.AssemblyQualifiedName" />), кроме случаев, когда этот тип находится в текущей выполняемой сборке или в библиотеке Mscorlib.dll — тогда достаточно задать имя типа с указанием пространства имен.
    /// </param>
    /// <param name="assemblyResolver">
    ///   Метод, находящий и возвращающий сборку, заданную в параметре <paramref name="typeName" />.
    ///    Имя сборки передается методу <paramref name="assemblyResolver" /> в виде объекта <see cref="T:System.Reflection.AssemblyName" />.
    ///    Если объект <paramref name="typeName" /> не содержит имя сборки, метод <paramref name="assemblyResolver" /> не вызывается.
    ///    Если метод <paramref name="assemblyResolver" /> не указан, выполняется стандартное разрешение сборки.
    /// 
    ///   Внимание. Не передавайте методы от неизвестных или не имеющих доверия вызывающих модулей.
    ///    В противном случае возможно повышение привилегий для вредоносного кода.
    ///    Рекомендуется использовать только методы, предоставленные пользователями или знакомые им.
    /// </param>
    /// <param name="typeResolver">
    ///   Метод, находящий и возвращающий тип, заданный в параметре <paramref name="typeName" />, из сборки, возвращенной методом <paramref name="assemblyResolver" /> или стандартным методом разрешения сборки.
    ///    Если сборка не предоставлена, этот метод может предоставить ее.
    ///    Метод также принимает параметр, указывающий, следует ли выполнять поиск без учета регистра; этому параметру передается значение <see langword="false" />.
    /// 
    ///   Внимание. Не передавайте методы от неизвестных или не имеющих доверия вызывающих модулей.
    /// </param>
    /// <param name="throwOnError">
    ///   Значение <see langword="true" />, чтобы создать исключение, если тип не удается найти; значение <see langword="false" />, чтобы вернуть значение <see langword="null" />.
    ///    Кроме того, при указании значения <see langword="false" /> подавляются некоторые другие условия возникновения исключений, но не все из них.
    ///    См. раздел "Исключения".
    /// </param>
    /// <returns>
    ///   Тип с указанным именем.
    ///    Если тип не найден, параметр <paramref name="throwOnError" /> определяет дальнейшее действие — возврат значения <see langword="null" /> или создание исключения.
    ///    В некоторых случаях исключение создается независимо от значения параметра <paramref name="throwOnError" />.
    ///    См. раздел "Исключения".
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Инициализатор класса вызывается и создает исключение.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и тип не найден.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и <paramref name="typeName" /> содержит недопустимые знаки, например внедренные табуляции.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и <paramref name="typeName" /> является пустой строкой.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и <paramref name="typeName" /> представляет тип массива с недопустимым размером.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет массив <see cref="T:System.TypedReference" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Произошла ошибка при интерпретации <paramref name="typeName" /> в имя типа и имя сборки (например, если имя простого типа содержит неэкранированный специальный знак).
    /// 
    ///   -или-
    /// 
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и <paramref name="typeName" /> содержит недопустимый синтаксис (например "MyType[,*,]").
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет универсальный тип, имеющий тип указателя, тип <see langword="ByRef" /> или <see cref="T:System.Void" /> в качестве одного из его аргументов типа.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет универсальный тип, который содержит неправильное количество аргументов типа.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет универсальный тип, и один из его аргументов типа не удовлетворяет ограничениям для соответствующего параметра типа.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и не удалось найти сборку либо одну из ее зависимостей.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> содержит недопустимое имя сборки.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> является допустимым именем сборки без имени типа.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или одна из ее зависимостей найдена, но не может быть загружена.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Сборка или одна из ее зависимостей является недопустимой.
    /// 
    ///   -или-
    /// 
    ///   Сборка была скомпилирована в более поздней версии среды CLR, чем версия, загруженная в текущий момент.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Type GetType(string typeName, Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TypeNameParser.GetType(typeName, assemblyResolver, typeResolver, throwOnError, false, ref stackMark);
    }

    /// <summary>
    ///   Возвращает тип с заданным именем и указывает, следует ли выполнять поиск без учета регистра и следует ли создавать исключение в случае невозможности найти тип, а также может предоставлять настраиваемые методы для разрешения сборки и типа.
    /// </summary>
    /// <param name="typeName">
    ///   Имя получаемого типа.
    ///    Если задан параметр <paramref name="typeResolver" />, имя типа может быть любой строкой, которую может разрешить объект <paramref name="typeResolver" />.
    ///    Если задан параметр <paramref name="assemblyResolver" /> или если используется стандартное разрешение типов, параметр <paramref name="typeName" /> должен быть именем с указанием сборки (см. описание свойства <see cref="P:System.Type.AssemblyQualifiedName" />), кроме случаев, когда этот тип находится в текущей выполняемой сборке или в библиотеке Mscorlib.dll — тогда достаточно задать имя типа с указанием пространства имен.
    /// </param>
    /// <param name="assemblyResolver">
    ///   Метод, находящий и возвращающий сборку, заданную в параметре <paramref name="typeName" />.
    ///    Имя сборки передается методу <paramref name="assemblyResolver" /> в виде объекта <see cref="T:System.Reflection.AssemblyName" />.
    ///    Если объект <paramref name="typeName" /> не содержит имя сборки, метод <paramref name="assemblyResolver" /> не вызывается.
    ///    Если метод <paramref name="assemblyResolver" /> не указан, выполняется стандартное разрешение сборки.
    /// 
    ///   Внимание. Не передавайте методы от неизвестных или не имеющих доверия вызывающих модулей.
    ///    В противном случае возможно повышение привилегий для вредоносного кода.
    ///    Рекомендуется использовать только методы, предоставленные пользователями или знакомые им.
    /// </param>
    /// <param name="typeResolver">
    ///   Метод, находящий и возвращающий тип, заданный в параметре <paramref name="typeName" />, из сборки, возвращенной методом <paramref name="assemblyResolver" /> или стандартным методом разрешения сборки.
    ///    Если сборка не предоставлена, этот метод может предоставить ее.
    ///    Метод также принимает параметр, указывающий, следует ли выполнять поиск без учета регистра; этому параметру передается значение <paramref name="ignoreCase" />.
    /// 
    ///   Внимание. Не передавайте методы от неизвестных или не имеющих доверия вызывающих модулей.
    /// </param>
    /// <param name="throwOnError">
    ///   Значение <see langword="true" />, чтобы создать исключение, если тип не удается найти; значение <see langword="false" />, чтобы вернуть значение <see langword="null" />.
    ///    Кроме того, при указании значения <see langword="false" /> подавляются некоторые другие условия возникновения исключений, но не все из них.
    ///    См. раздел "Исключения".
    /// </param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" />, чтобы не учитывать регистр при поиске <paramref name="typeName" />, значение <see langword="false" />, чтобы учитывать регистр при поиске <paramref name="typeName" />.
    /// </param>
    /// <returns>
    ///   Тип с указанным именем.
    ///    Если тип не найден, параметр <paramref name="throwOnError" /> определяет дальнейшее действие — возврат значения <see langword="null" /> или создание исключения.
    ///    В некоторых случаях исключение создается независимо от значения параметра <paramref name="throwOnError" />.
    ///    См. раздел "Исключения".
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Инициализатор класса вызывается и создает исключение.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и тип не найден.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и <paramref name="typeName" /> содержит недопустимые знаки, например внедренные табуляции.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и <paramref name="typeName" /> является пустой строкой.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и <paramref name="typeName" /> представляет тип массива с недопустимым размером.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет массив <see cref="T:System.TypedReference" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Произошла ошибка при интерпретации <paramref name="typeName" /> в имя типа и имя сборки (например, если имя простого типа содержит неэкранированный специальный знак).
    /// 
    ///   -или-
    /// 
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и <paramref name="typeName" /> содержит недопустимый синтаксис (например "MyType[,*,]").
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет универсальный тип, имеющий тип указателя, тип <see langword="ByRef" /> или <see cref="T:System.Void" /> в качестве одного из его аргументов типа.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет универсальный тип, который содержит неправильное количество аргументов типа.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет универсальный тип, и один из его аргументов типа не удовлетворяет ограничениям для соответствующего параметра типа.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="throwOnError" /> имеет значение <see langword="true" />, и не удалось найти сборку либо одну из ее зависимостей.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или одна из ее зависимостей найдена, но не может быть загружена.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> содержит недопустимое имя сборки.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> является допустимым именем сборки без имени типа.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Сборка или одна из ее зависимостей является недопустимой.
    /// 
    ///   -или-
    /// 
    ///   Сборка была скомпилирована в более поздней версии среды CLR, чем версия, загруженная в текущий момент.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Type GetType(string typeName, Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError, bool ignoreCase)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return TypeNameParser.GetType(typeName, assemblyResolver, typeResolver, throwOnError, ignoreCase, ref stackMark);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Type" /> с заданным именем, позволяющий определить, будет ли учитываться регистр при поиске, и будет ли создаваться исключение в случае невозможности найти тип.
    ///    Тип загружается не для выполнения, а только для отражения.
    /// </summary>
    /// <param name="typeName">
    ///   Имя искомого типа <see cref="T:System.Type" /> с указанием сборки.
    /// </param>
    /// <param name="throwIfNotFound">
    ///   Значение <see langword="true" />, чтобы в случае невозможности найти тип создавалось исключение <see cref="T:System.TypeLoadException" />; значение <see langword="false" />, чтобы при невозможности найти тип возвращалось значение <see langword="null" />.
    ///    Кроме того, при указании значения <see langword="false" /> подавляются некоторые другие условия возникновения исключений, но не все из них.
    ///    См. раздел "Исключения".
    /// </param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" />, чтобы не учитывать регистр при поиске <paramref name="typeName" />, значение <see langword="false" />, чтобы учитывать регистр при поиске <paramref name="typeName" />.
    /// </param>
    /// <returns>
    ///   Тип с указанным именем, если он существует; в противном случае — значение <see langword="null" />.
    ///    Если тип не найден, параметр <paramref name="throwIfNotFound" /> определяет дальнейшее действие — возврат значения <see langword="null" /> или создание исключения.
    ///    В некоторых случаях исключение создается независимо от значения параметра <paramref name="throwIfNotFound" />.
    ///    См. раздел "Исключения".
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="typeName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Инициализатор класса вызывается и создает исключение.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   <paramref name="throwIfNotFound" /> имеет значение <see langword="true" />, и тип не найден.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="throwIfNotFound" /> имеет значение <see langword="true" />, и <paramref name="typeName" /> содержит недопустимые знаки, например внедренные табуляции.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="throwIfNotFound" /> имеет значение <see langword="true" />, и <paramref name="typeName" /> является пустой строкой.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="throwIfNotFound" /> имеет значение <see langword="true" />, и <paramref name="typeName" /> представляет тип массива с недопустимым размером.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет массив объектов <see cref="T:System.TypedReference" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="typeName" /> не включает имя сборки.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="throwIfNotFound" /> имеет значение <see langword="true" />, и <paramref name="typeName" /> содержит недопустимый синтаксис (например, "MyType[,*,]").
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет универсальный тип, имеющий тип указателя, тип <see langword="ByRef" /> или <see cref="T:System.Void" /> в качестве одного из его аргументов типа.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет универсальный тип, который содержит неправильное количество аргументов типа.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeName" /> представляет универсальный тип, и один из его аргументов типа не удовлетворяет ограничениям для соответствующего параметра типа.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   <paramref name="throwIfNotFound" /> имеет значение <see langword="true" />, и не удалось найти сборку либо одну из ее зависимостей.
    /// </exception>
    /// <exception cref="T:System.IO.FileLoadException">
    ///   Сборка или одна из ее зависимостей найдена, но не может быть загружена.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Сборка или одна из ее зависимостей является недопустимой.
    /// 
    ///   -или-
    /// 
    ///   Сборка была скомпилирована в более поздней версии среды CLR, чем версия, загруженная в текущий момент.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Type ReflectionOnlyGetType(string typeName, bool throwIfNotFound, bool ignoreCase)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Type) RuntimeType.GetType(typeName, throwIfNotFound, ignoreCase, true, ref stackMark);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Type" />, который представляет указатель на текущий тип.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />, который представляет указатель на текущий тип.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызванный метод не поддерживается в базовом классе.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Текущий тип — <see cref="T:System.TypedReference" />.
    /// 
    ///   -или-
    /// 
    ///   Текущий тип — <see langword="ByRef" />.
    ///    То есть <see cref="P:System.Type.IsByRef" /> возвращает <see langword="true" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual Type MakePointerType()
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Возвращает атрибут <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" />, описывающий структуру текущего типа.
    /// </summary>
    /// <returns>
    ///   Возвращает атрибут <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" />, описывающий общие особенности структуры текущего типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызванный метод не поддерживается в базовом классе.
    /// </exception>
    public virtual StructLayoutAttribute StructLayoutAttribute
    {
      get
      {
        throw new NotSupportedException();
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Type" />, который представляет текущий тип при передаче в качестве параметра <see langword="ref" /> (параметра <see langword="ByRef" /> в Visual Basic).
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />, который представляет текущий тип при передаче в качестве параметра <see langword="ref" /> (параметра <see langword="ByRef" /> в Visual Basic).
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызванный метод не поддерживается в базовом классе.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Текущий тип — <see cref="T:System.TypedReference" />.
    /// 
    ///   -или-
    /// 
    ///   Текущий тип — <see langword="ByRef" />.
    ///    То есть <see cref="P:System.Type.IsByRef" /> возвращает <see langword="true" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual Type MakeByRefType()
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Type" />, представляющий одномерный массив текущего типа с нижней границей, равной нулю.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />, представляющий одномерный массив текущего типа с нижней границей, равной нулю.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызванный метод не поддерживается в базовом классе.
    ///    Реализацию должны обеспечивать производные классы.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Текущий тип — <see cref="T:System.TypedReference" />.
    /// 
    ///   -или-
    /// 
    ///   Текущий тип — <see langword="ByRef" />.
    ///    То есть <see cref="P:System.Type.IsByRef" /> возвращает <see langword="true" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual Type MakeArrayType()
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Type" />, представляющий массив текущего типа указанной размерности.
    /// </summary>
    /// <param name="rank">
    ///   Размерность массива.
    ///    Это число должно быть меньше либо равно 32.
    /// </param>
    /// <returns>
    ///   Объект, представляющий массив текущего типа указанной размерности.
    /// </returns>
    /// <exception cref="T:System.IndexOutOfRangeException">
    ///   <paramref name="rank" /> недопустим.
    ///    Например, 0 или отрицательное число.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызванный метод не поддерживается в базовом классе.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Текущий тип — <see cref="T:System.TypedReference" />.
    /// 
    ///   -или-
    /// 
    ///   Текущий тип — <see langword="ByRef" />.
    ///    То есть <see cref="P:System.Type.IsByRef" /> возвращает <see langword="true" />.
    /// 
    ///   -или-
    /// 
    ///   Значение <paramref name="rank" /> больше 32.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual Type MakeArrayType(int rank)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    ///   Возвращает тип, связанный с указанным идентификатором ProgID, и возвращает значение NULL, если при загрузке объекта <see cref="T:System.Type" /> возникла ошибка.
    /// </summary>
    /// <param name="progID">
    ///   Идентификатор ProgID извлекаемого типа.
    /// </param>
    /// <returns>
    ///   Тип, связанный с указанным идентификатором ProgID, если идентификатор <paramref name="progID" /> является допустимой записью в реестре и с ним связан определенный тип; в противном случае — значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <paramref name="progID" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static Type GetTypeFromProgID(string progID)
    {
      return RuntimeType.GetTypeFromProgIDImpl(progID, (string) null, false);
    }

    /// <summary>
    ///   Возвращает тип, связанный с заданным идентификатором ProgID, позволяющим определить, будет ли выбрасываться исключение при происхождении ошибки во время загрузки типа.
    /// </summary>
    /// <param name="progID">
    ///   Идентификатор ProgID извлекаемого типа.
    /// </param>
    /// <param name="throwOnError">
    ///   Значение <see langword="true" /> для вызова любого возникшего исключения.
    /// 
    ///   -или-
    /// 
    ///   Значение <see langword="false" /> для игнорирования всех происходящих исключений.
    /// </param>
    /// <returns>
    ///   Тип, связанный с указанным идентификатором ProgID, если идентификатор <paramref name="progID" /> является допустимой записью в реестре и с ним связан определенный тип; в противном случае — значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <paramref name="progID" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">
    ///   Указанный идентификатор ProgID не зарегистрирован.
    /// </exception>
    [SecurityCritical]
    public static Type GetTypeFromProgID(string progID, bool throwOnError)
    {
      return RuntimeType.GetTypeFromProgIDImpl(progID, (string) null, throwOnError);
    }

    /// <summary>
    ///   Возвращает с указанного сервера тип, связанный с заданным идентификатором ProgID, и возвращает значение NULL, если при загрузке типа произошла ошибка.
    /// </summary>
    /// <param name="progID">
    ///   Идентификатор ProgID извлекаемого типа.
    /// </param>
    /// <param name="server">
    ///   Сервер, с которого должен быть загружен тип.
    ///    Если в качестве имени сервера задано значение <see langword="null" />, этот метод автоматически перейдет к поиску на локальном компьютере.
    /// </param>
    /// <returns>
    ///   Тип, связанный с заданным идентификатором ProgID, если идентификатор <paramref name="progID" /> является допустимой записью в реестре и с ним связан определенный тип; в противном случае — значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <paramref name="prodID" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static Type GetTypeFromProgID(string progID, string server)
    {
      return RuntimeType.GetTypeFromProgIDImpl(progID, server, false);
    }

    /// <summary>
    ///   Возвращает с указанного сервера тип, связанный с заданным идентификатором progID, который позволяет определить, будет ли выбрасываться исключение при происхождении ошибки во время загрузки типа.
    /// </summary>
    /// <param name="progID">
    ///   Идентификатор ProgID извлекаемого типа <see cref="T:System.Type" />.
    /// </param>
    /// <param name="server">
    ///   Сервер, с которого должен быть загружен тип.
    ///    Если в качестве имени сервера задано значение <see langword="null" />, этот метод автоматически перейдет к поиску на локальном компьютере.
    /// </param>
    /// <param name="throwOnError">
    ///   Значение <see langword="true" /> для вызова любого возникшего исключения.
    /// 
    ///   -или-
    /// 
    ///   Значение <see langword="false" /> для игнорирования всех происходящих исключений.
    /// </param>
    /// <returns>
    ///   Тип, связанный с заданным идентификатором ProgID, если идентификатор <paramref name="progID" /> является допустимой записью в реестре и с ним связан определенный тип; в противном случае — значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Свойство <paramref name="progID" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">
    ///   Указанный идентификатор progID не зарегистрирован.
    /// </exception>
    [SecurityCritical]
    public static Type GetTypeFromProgID(string progID, string server, bool throwOnError)
    {
      return RuntimeType.GetTypeFromProgIDImpl(progID, server, throwOnError);
    }

    /// <summary>Возвращает тип, связанный с заданным кодом CLSID.</summary>
    /// <param name="clsid">Код CLSID извлекаемого типа.</param>
    /// <returns>
    ///   <see langword="System.__ComObject" /> вне зависимости от того, допустим ли код CLSID.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Type GetTypeFromCLSID(Guid clsid)
    {
      return RuntimeType.GetTypeFromCLSIDImpl(clsid, (string) null, false);
    }

    /// <summary>
    ///   Возвращает тип, связанный с заданным кодом CLSID, позволяющий определить, будет ли выбрасываться исключение в случае происхождения ошибки при загрузке типа.
    /// </summary>
    /// <param name="clsid">Код CLSID извлекаемого типа.</param>
    /// <param name="throwOnError">
    ///   Значение <see langword="true" /> для вызова любого возникшего исключения.
    /// 
    ///   -или-
    /// 
    ///   Значение <see langword="false" /> для игнорирования всех происходящих исключений.
    /// </param>
    /// <returns>
    ///   <see langword="System.__ComObject" /> вне зависимости от того, допустим ли код CLSID.
    /// </returns>
    [SecuritySafeCritical]
    public static Type GetTypeFromCLSID(Guid clsid, bool throwOnError)
    {
      return RuntimeType.GetTypeFromCLSIDImpl(clsid, (string) null, throwOnError);
    }

    /// <summary>
    ///   Возвращает с указанного сервера тип, связанный с заданным кодом CLSID.
    /// </summary>
    /// <param name="clsid">Код CLSID извлекаемого типа.</param>
    /// <param name="server">
    ///   Сервер, с которого должен быть загружен тип.
    ///    Если в качестве имени сервера задано значение <see langword="null" />, этот метод автоматически перейдет к поиску на локальном компьютере.
    /// </param>
    /// <returns>
    ///   <see langword="System.__ComObject" /> вне зависимости от того, допустим ли код CLSID.
    /// </returns>
    [SecuritySafeCritical]
    public static Type GetTypeFromCLSID(Guid clsid, string server)
    {
      return RuntimeType.GetTypeFromCLSIDImpl(clsid, server, false);
    }

    /// <summary>
    ///   Возвращает с указанного сервера тип, связанный с заданным кодом CLSID, позволяющий определить, будет ли выбрасываться исключение при происхождении ошибки во время загрузки типа.
    /// </summary>
    /// <param name="clsid">Код CLSID извлекаемого типа.</param>
    /// <param name="server">
    ///   Сервер, с которого должен быть загружен тип.
    ///    Если в качестве имени сервера задано значение <see langword="null" />, этот метод автоматически перейдет к поиску на локальном компьютере.
    /// </param>
    /// <param name="throwOnError">
    ///   Значение <see langword="true" /> для вызова любого возникшего исключения.
    /// 
    ///   -или-
    /// 
    ///   Значение <see langword="false" /> для игнорирования всех происходящих исключений.
    /// </param>
    /// <returns>
    ///   <see langword="System.__ComObject" /> вне зависимости от того, допустим ли код CLSID.
    /// </returns>
    [SecuritySafeCritical]
    public static Type GetTypeFromCLSID(Guid clsid, string server, bool throwOnError)
    {
      return RuntimeType.GetTypeFromCLSIDImpl(clsid, server, throwOnError);
    }

    /// <summary>
    ///   Возвращает код базового типа указанного объекта <see cref="T:System.Type" />.
    /// </summary>
    /// <param name="type">
    ///   Тип, код базового типа которого требуется получить.
    /// </param>
    /// <returns>
    ///   Код базового типа или <see cref="F:System.TypeCode.Empty" />, если <paramref name="type" /> — <see langword="null" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static TypeCode GetTypeCode(Type type)
    {
      if (type == (Type) null)
        return TypeCode.Empty;
      return type.GetTypeCodeImpl();
    }

    /// <summary>
    ///   Возвращает код базового типа этого экземпляра <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>Код типа базового типа.</returns>
    protected virtual TypeCode GetTypeCodeImpl()
    {
      if (this != this.UnderlyingSystemType && this.UnderlyingSystemType != (Type) null)
        return Type.GetTypeCode(this.UnderlyingSystemType);
      return TypeCode.Object;
    }

    /// <summary>
    ///   Возвращает идентификатор GUID, связанный с объектом <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Идентификатор GUID, связанный с объектом <see cref="T:System.Type" />.
    /// </returns>
    public abstract Guid GUID { get; }

    /// <summary>
    ///   Возвращает ссылку на связыватель по умолчанию, который реализует внутренние правила выбора соответствующих членов, вызываемых методом <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" />.
    /// </summary>
    /// <returns>
    ///   Ссылка на связыватель, используемый в системе по умолчанию.
    /// </returns>
    public static Binder DefaultBinder
    {
      get
      {
        if (Type.defaultBinder == null)
          Type.CreateBinder();
        return Type.defaultBinder;
      }
    }

    private static void CreateBinder()
    {
      if (Type.defaultBinder != null)
        return;
      System.DefaultBinder defaultBinder = new System.DefaultBinder();
      Interlocked.CompareExchange<Binder>(ref Type.defaultBinder, (Binder) defaultBinder, (Binder) null);
    }

    /// <summary>
    ///   При переопределении в производном классе вызывает указанный член, соответствующий заданным ограничениям привязки, списку аргументов, модификаторов, а также языку и региональным параметрам.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащая имя вызываемого элемента — конструктора, метода, свойства или поля.
    /// 
    ///   -или-
    /// 
    ///   Пустая строка ("") — в этом случае будет вызван член по умолчанию.
    /// 
    ///   -или-
    /// 
    ///   Для членов <see langword="IDispatch" /> — строка, представляющая идентификатор DispID, например "[DispID=3]".
    /// </param>
    /// <param name="invokeAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    ///    Тип доступа может быть обозначен одним из флагов <see langword="BindingFlags" />, например <see langword="Public" />, <see langword="NonPublic" />, <see langword="Private" />, <see langword="InvokeMethod" />, <see langword="GetField" /> и т.д.
    ///    Тип поиска указывать необязательно.
    ///    Если тип поиска не указан, используются флаги <see langword="BindingFlags.Public" /> | <see langword="BindingFlags.Instance" /> | <see langword="BindingFlags.Static" />.
    /// </param>
    /// <param name="binder">
    ///   Объект, определяющий набор свойств и разрешающий привязку, что может включать выбор перегруженных методов, приведение типов аргументов и вызов члена с помощью отражения.
    /// 
    ///   -или-
    /// 
    ///   Пустая ссылка (Nothing в Visual Basic) для использования свойства <see cref="P:System.Type.DefaultBinder" />.
    ///    Обратите внимание, что для успешного вызова перегруженных версий метода с переменными аргументами может потребоваться явное объявление объекта <see cref="T:System.Reflection.Binder" />.
    /// </param>
    /// <param name="target">
    ///   Объект, для которого следует вызвать указанный член.
    /// </param>
    /// <param name="args">
    ///   Массив с аргументами, передаваемыми вызываемому члену.
    /// </param>
    /// <param name="modifiers">
    ///   Массив объектов <see cref="T:System.Reflection.ParameterModifier" />, представляющих атрибуты, связанные с соответствующим элементом в массиве <paramref name="args" />.
    ///    Атрибуты, связанные с параметром, хранятся в сигнатуре члена.
    /// 
    ///   Связыватель по умолчанию обрабатывает этот параметр только при вызове COM-компонента.
    /// </param>
    /// <param name="culture">
    ///   Объект <see cref="T:System.Globalization.CultureInfo" />, представляющий используемый языковой стандарт глобализации. Он может понадобиться для выполнения преобразований, зависящих от языкового стандарта, например приведения числа в строковом формате к типу Double.
    /// 
    ///   -или-
    /// 
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic) для использования объекта <see cref="T:System.Globalization.CultureInfo" /> текущего потока.
    /// </param>
    /// <param name="namedParameters">
    ///   Массив, содержащий имена параметров, в которые передаются значения элементов массива <paramref name="args" />.
    /// </param>
    /// <returns>
    ///   Объект, представляющий возвращаемое значение вызываемого элемента.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="invokeAttr" /> не содержит <see langword="CreateInstance" />, а <paramref name="name" /> равно <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="args" /> и <paramref name="modifiers" /> имеют разную длину.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="invokeAttr" /> не является допустимым атрибутом <see cref="T:System.Reflection.BindingFlags" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="invokeAttr" /> не содержит одного из следующих флагов привязки: <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" /> или <see langword="SetProperty" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="invokeAttr" /> содержит <see langword="CreateInstance" /> в сочетании с <see langword="InvokeMethod" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" /> или <see langword="SetProperty" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="invokeAttr" /> содержит как <see langword="GetField" />, так и <see langword="SetField" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="invokeAttr" /> содержит как <see langword="GetProperty" />, так и <see langword="SetProperty" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="invokeAttr" /> содержит <see langword="InvokeMethod" /> в сочетании с <see langword="SetField" /> или <see langword="SetProperty" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="invokeAttr" /> содержит <see langword="SetField" />, а <paramref name="args" /> содержит более одного элемента.
    /// 
    ///   -или-
    /// 
    ///   Массив именованных параметров больше, чем массив аргументов.
    /// 
    ///   -или-
    /// 
    ///   Этот метод вызывается для объекта COM, и один из следующих флагов привязки не был передан: <see langword="BindingFlags.InvokeMethod" />, <see langword="BindingFlags.GetProperty" />, <see langword="BindingFlags.SetProperty" />, <see langword="BindingFlags.PutDispProperty" /> или <see langword="BindingFlags.PutRefDispProperty" />.
    /// 
    ///   -или-
    /// 
    ///   Один из массивов именованных параметров содержит строку, имеющую значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Указанный член является инициализатором класса.
    /// </exception>
    /// <exception cref="T:System.MissingFieldException">
    ///   Невозможно найти поле или свойство.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Невозможно найти метод, который соответствует аргументам в <paramref name="args" />.
    /// 
    ///   -или-
    /// 
    ///   Невозможно найти члены с именами аргументов, указанными в <paramref name="namedParameters" />.
    /// 
    ///   -или-
    /// 
    ///   Текущий объект <see cref="T:System.Type" /> представляет тип, содержащий параметры открытого типа, то есть <see cref="P:System.Type.ContainsGenericParameters" /> возвращает <see langword="true" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetException">
    ///   Невозможно вызвать указанный член для <paramref name="target" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Несколько методов соответствуют критериям привязки.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Метод, представленный <paramref name="name" />, имеет один или несколько незаданных параметров универсального типа.
    ///    То есть свойство <see cref="P:System.Reflection.MethodInfo.ContainsGenericParameters" /> метода возвращает <see langword="true" />.
    /// </exception>
    public abstract object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

    /// <summary>
    ///   Вызывает указанный член, соответствующий заданным ограничениям привязки, списку аргументов, а также языку и региональным параметрам.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащая имя вызываемого элемента — конструктора, метода, свойства или поля.
    /// 
    ///   -или-
    /// 
    ///   Пустая строка ("") — в этом случае будет вызван член по умолчанию.
    /// 
    ///   -или-
    /// 
    ///   Для членов <see langword="IDispatch" /> — строка, представляющая идентификатор DispID, например "[DispID=3]".
    /// </param>
    /// <param name="invokeAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    ///    Тип доступа может быть обозначен одним из флагов <see langword="BindingFlags" />, например <see langword="Public" />, <see langword="NonPublic" />, <see langword="Private" />, <see langword="InvokeMethod" />, <see langword="GetField" /> и т.д.
    ///    Тип поиска указывать необязательно.
    ///    Если тип поиска не указан, используются флаги <see langword="BindingFlags.Public" /> | <see langword="BindingFlags.Instance" /> | <see langword="BindingFlags.Static" />.
    /// </param>
    /// <param name="binder">
    ///   Объект, определяющий набор свойств и разрешающий привязку, что может включать выбор перегруженных методов, приведение типов аргументов и вызов члена с помощью отражения.
    /// 
    ///   -или-
    /// 
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic) для использования свойства <see cref="P:System.Type.DefaultBinder" />.
    ///    Обратите внимание, что для успешного вызова перегруженных версий метода с переменными аргументами может потребоваться явное объявление объекта <see cref="T:System.Reflection.Binder" />.
    /// </param>
    /// <param name="target">
    ///   Объект, для которого следует вызвать указанный член.
    /// </param>
    /// <param name="args">
    ///   Массив с аргументами, передаваемыми вызываемому члену.
    /// </param>
    /// <param name="culture">
    ///   Объект, представляющий используемые языковые стандарты глобализации. Его задание может понадобиться для выполнения преобразований, зависящих от языкового стандарта, например преобразования числовой строки <see cref="T:System.String" /> к типу <see cref="T:System.Double" />.
    /// 
    ///   -или-
    /// 
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic) для использования объекта <see cref="T:System.Globalization.CultureInfo" /> текущего потока.
    /// </param>
    /// <returns>
    ///   Объект, представляющий возвращаемое значение вызываемого элемента.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="invokeAttr" /> не содержит <see langword="CreateInstance" />, а <paramref name="name" /> равно <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="invokeAttr" /> не является допустимым атрибутом <see cref="T:System.Reflection.BindingFlags" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="invokeAttr" /> не содержит одного из следующих флагов привязки: <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" /> или <see langword="SetProperty" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="invokeAttr" /> содержит <see langword="CreateInstance" /> в сочетании с <see langword="InvokeMethod" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" /> или <see langword="SetProperty" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="invokeAttr" /> содержит как <see langword="GetField" />, так и <see langword="SetField" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="invokeAttr" /> содержит как <see langword="GetProperty" />, так и <see langword="SetProperty" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="invokeAttr" /> содержит <see langword="InvokeMethod" /> в сочетании с <see langword="SetField" /> или <see langword="SetProperty" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="invokeAttr" /> содержит <see langword="SetField" />, а <paramref name="args" /> содержит более одного элемента.
    /// 
    ///   -или-
    /// 
    ///   Этот метод вызывается для объекта COM, и один из следующих флагов привязки не был передан: <see langword="BindingFlags.InvokeMethod" />, <see langword="BindingFlags.GetProperty" />, <see langword="BindingFlags.SetProperty" />, <see langword="BindingFlags.PutDispProperty" /> или <see langword="BindingFlags.PutRefDispProperty" />.
    /// 
    ///   -или-
    /// 
    ///   Один из массивов именованных параметров содержит строку, имеющую значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Указанный член является инициализатором класса.
    /// </exception>
    /// <exception cref="T:System.MissingFieldException">
    ///   Невозможно найти поле или свойство.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Невозможно найти метод, который соответствует аргументам в <paramref name="args" />.
    /// 
    ///   -или-
    /// 
    ///   Текущий объект <see cref="T:System.Type" /> представляет тип, содержащий параметры открытого типа, то есть <see cref="P:System.Type.ContainsGenericParameters" /> возвращает <see langword="true" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetException">
    ///   Невозможно вызвать указанный член для <paramref name="target" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Несколько методов соответствуют критериям привязки.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Метод, представленный <paramref name="name" />, имеет один или несколько незаданных параметров универсального типа.
    ///    То есть свойство <see cref="P:System.Reflection.MethodInfo.ContainsGenericParameters" /> метода возвращает <see langword="true" />.
    /// </exception>
    [DebuggerStepThrough]
    [DebuggerHidden]
    public object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, CultureInfo culture)
    {
      return this.InvokeMember(name, invokeAttr, binder, target, args, (ParameterModifier[]) null, culture, (string[]) null);
    }

    /// <summary>
    ///   Вызывает указанный член, соответствующий заданным ограничениям привязки и указанному списку аргументов.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащая имя вызываемого элемента — конструктора, метода, свойства или поля.
    /// 
    ///   -или-
    /// 
    ///   Пустая строка ("") — в этом случае будет вызван член по умолчанию.
    /// 
    ///   -или-
    /// 
    ///   Для членов <see langword="IDispatch" /> — строка, представляющая идентификатор DispID, например "[DispID=3]".
    /// </param>
    /// <param name="invokeAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    ///    Тип доступа может быть обозначен одним из флагов <see langword="BindingFlags" />, например <see langword="Public" />, <see langword="NonPublic" />, <see langword="Private" />, <see langword="InvokeMethod" />, <see langword="GetField" /> и т.д.
    ///    Тип поиска указывать необязательно.
    ///    Если тип поиска не указан, используются флаги <see langword="BindingFlags.Public" /> | <see langword="BindingFlags.Instance" /> | <see langword="BindingFlags.Static" />.
    /// </param>
    /// <param name="binder">
    ///   Объект, определяющий набор свойств и разрешающий привязку, что может включать выбор перегруженных методов, приведение типов аргументов и вызов члена с помощью отражения.
    /// 
    ///   -или-
    /// 
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic) для использования свойства <see cref="P:System.Type.DefaultBinder" />.
    ///    Обратите внимание, что для успешного вызова перегруженных версий метода с переменными аргументами может потребоваться явное объявление объекта <see cref="T:System.Reflection.Binder" />.
    /// </param>
    /// <param name="target">
    ///   Объект, для которого следует вызвать указанный член.
    /// </param>
    /// <param name="args">
    ///   Массив с аргументами, передаваемыми вызываемому члену.
    /// </param>
    /// <returns>
    ///   Объект, представляющий возвращаемое значение вызываемого элемента.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="invokeAttr" /> не содержит <see langword="CreateInstance" />, а <paramref name="name" /> равно <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="invokeAttr" /> не является допустимым атрибутом <see cref="T:System.Reflection.BindingFlags" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="invokeAttr" /> не содержит одного из следующих флагов привязки: <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" /> или <see langword="SetProperty" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="invokeAttr" /> содержит <see langword="CreateInstance" /> в сочетании с <see langword="InvokeMethod" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" /> или <see langword="SetProperty" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="invokeAttr" /> содержит как <see langword="GetField" />, так и <see langword="SetField" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="invokeAttr" /> содержит как <see langword="GetProperty" />, так и <see langword="SetProperty" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="invokeAttr" /> содержит <see langword="InvokeMethod" /> в сочетании с <see langword="SetField" /> или <see langword="SetProperty" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="invokeAttr" /> содержит <see langword="SetField" />, а <paramref name="args" /> содержит более одного элемента.
    /// 
    ///   -или-
    /// 
    ///   Этот метод вызывается для объекта COM, и один из следующих флагов привязки не был передан: <see langword="BindingFlags.InvokeMethod" />, <see langword="BindingFlags.GetProperty" />, <see langword="BindingFlags.SetProperty" />, <see langword="BindingFlags.PutDispProperty" /> или <see langword="BindingFlags.PutRefDispProperty" />.
    /// 
    ///   -или-
    /// 
    ///   Один из массивов именованных параметров содержит строку, имеющую значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Указанный член является инициализатором класса.
    /// </exception>
    /// <exception cref="T:System.MissingFieldException">
    ///   Невозможно найти поле или свойство.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Невозможно найти метод, который соответствует аргументам в <paramref name="args" />.
    /// 
    ///   -или-
    /// 
    ///   Текущий объект <see cref="T:System.Type" /> представляет тип, содержащий параметры открытого типа, то есть <see cref="P:System.Type.ContainsGenericParameters" /> возвращает <see langword="true" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetException">
    ///   Невозможно вызвать указанный член для <paramref name="target" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Несколько методов соответствуют критериям привязки.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Платформа .NET Compact Framework сейчас не поддерживает этот метод.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Метод, представленный <paramref name="name" />, имеет один или несколько незаданных параметров универсального типа.
    ///    То есть свойство <see cref="P:System.Reflection.MethodInfo.ContainsGenericParameters" /> метода возвращает <see langword="true" />.
    /// </exception>
    [DebuggerStepThrough]
    [DebuggerHidden]
    public object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args)
    {
      return this.InvokeMember(name, invokeAttr, binder, target, args, (ParameterModifier[]) null, (CultureInfo) null, (string[]) null);
    }

    /// <summary>
    ///   Возвращает модуль (DLL), в котором определен текущий объект <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Модуль, в котором определен текущий объект <see cref="T:System.Type" />.
    /// </returns>
    public new abstract Module Module { get; }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Reflection.Assembly" />, в котором объявлен тип.
    ///    Для универсальных типов возвращает объект сборки <see cref="T:System.Reflection.Assembly" />, в которой определен универсальный тип.
    /// </summary>
    /// <returns>
    ///   Экземпляр <see cref="T:System.Reflection.Assembly" />, описывающий сборку, которая содержит текущий тип.
    ///    Для универсальных типов экземпляр описывает сборку, содержащую определение универсального типа, а не сборку, которая создала и использует определенный сконструированный тип.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract Assembly Assembly { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Возвращает дескриптор текущего объекта <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Дескриптор текущего объекта <see cref="T:System.Type" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Платформа .NET Compact Framework в настоящее время не поддерживает это свойство.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual RuntimeTypeHandle TypeHandle
    {
      [__DynamicallyInvokable] get
      {
        throw new NotSupportedException();
      }
    }

    internal virtual RuntimeTypeHandle GetTypeHandleInternal()
    {
      return this.TypeHandle;
    }

    /// <summary>
    ///   Возвращает дескриптор <see cref="T:System.Type" /> для указанного объекта.
    /// </summary>
    /// <param name="o">
    ///   Объект, для которого требуется получить дескриптор типа.
    /// </param>
    /// <returns>
    ///   Дескриптор типа <see cref="T:System.Type" /> для указанного объекта <see cref="T:System.Object" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="o" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static RuntimeTypeHandle GetTypeHandle(object o)
    {
      if (o == null)
        throw new ArgumentNullException((string) null, Environment.GetResourceString("Arg_InvalidHandle"));
      return new RuntimeTypeHandle((RuntimeType) o.GetType());
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeType GetTypeFromHandleUnsafe(IntPtr handle);

    /// <summary>
    ///   Возвращает тип, на который ссылается указанный дескриптор типа.
    /// </summary>
    /// <param name="handle">Объект, который ссылается на тип.</param>
    /// <returns>
    ///   Тип, на который ссылается заданный дескриптор <see cref="T:System.RuntimeTypeHandle" />, или значение <see langword="null" />, если значение свойства <see cref="P:System.RuntimeTypeHandle.Value" /> параметра <paramref name="handle" /> равно <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Инициализатор класса вызывается и создает исключение.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern Type GetTypeFromHandle(RuntimeTypeHandle handle);

    /// <summary>
    ///   Возвращает полное имя типа, включая пространство имен, но не сборку.
    /// </summary>
    /// <returns>
    ///   Полное имя типа, включая пространство имен, но не сборку; или значение <see langword="null" />, если текущий экземпляр представляет параметр универсального типа, тип массива, тип указателя, тип <see langword="byref" /> на основе параметра типа либо универсальный тип, который, хотя и не является определением универсального типа, содержит неразрешенные параметры типа.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract string FullName { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Возвращает пространство имен объекта <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Пространство имен <see cref="T:System.Type" /> или значение <see langword="null" />, если текущий экземпляр не имеет пространства имен или представляет универсальный параметр.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract string Namespace { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Возвращает имя типа с указанием сборки, включающее имя сборки, из которой был загружен объект <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Имя объекта <see cref="T:System.Type" /> с указанием сборки, включающее имя сборки, из которой был загружен объект <see cref="T:System.Type" />, или значение <see langword="null" />, если текущий экземпляр представляет параметр универсального типа.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract string AssemblyQualifiedName { [__DynamicallyInvokable] get; }

    /// <summary>Возвращает размерность массива.</summary>
    /// <returns>
    ///   Целое число, указывающее на количество измерений текущего типа.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Функциональность этого метода не поддерживается в базовом классе и должна быть реализована в производном классе.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Текущий тип не является массивом.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int GetArrayRank()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    /// <summary>
    ///   Возвращает тип, для которого текущий объект <see cref="T:System.Type" /> является непосредственным наследником.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />, прямым наследником которого является текущий объект <see cref="T:System.Type" />, или <see langword="null" />, если текущий объект <see langword="Type" /> представляет класс <see cref="T:System.Object" /> или интерфейс.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract Type BaseType { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Выполняет поиск конструктора с параметрами, соответствующими указанным модификаторам и типам аргументов, с учетом заданных ограничений по связыванию и соглашений о вызовах.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <param name="binder">
    ///   Объект, определяющий набор свойств и разрешающий привязку, что может включать выбор перегруженных методов, приведение типов аргументов и вызов члена с помощью отражения.
    /// 
    ///   -или-
    /// 
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic) для использования свойства <see cref="P:System.Type.DefaultBinder" />.
    /// </param>
    /// <param name="callConvention">
    ///   Объект, определяющий набор применяемых правил, касающихся порядка и расположения аргументов, способа передачи возвращаемого значения, регистров, используемых для аргументов, и очистки стека.
    /// </param>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющий число, порядок и тип параметров, извлекаемых конструктором.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов типа <see cref="T:System.Type" /> (то есть Type[] types = new Type[0]), если требуется получить конструктор, который не имеет параметров.
    /// </param>
    /// <param name="modifiers">
    ///   Массив объектов <see cref="T:System.Reflection.ParameterModifier" />, представляющих атрибуты, связанные с соответствующим элементом в массиве <paramref name="types" />.
    ///    Связыватель по умолчанию не обрабатывает этот параметр.
    /// </param>
    /// <returns>
    ///   Если поиск выполнен удачно, возвращается объект, представляющий конструктор, который соответствует указанным требованиям; в противном случае возвращается значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="types" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один из элементов в <paramref name="types" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="types" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   Массив <paramref name="modifiers" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="types" /> и <paramref name="modifiers" /> имеют разную длину.
    /// </exception>
    [ComVisible(true)]
    public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      if (types == null)
        throw new ArgumentNullException(nameof (types));
      for (int index = 0; index < types.Length; ++index)
      {
        if (types[index] == (Type) null)
          throw new ArgumentNullException(nameof (types));
      }
      return this.GetConstructorImpl(bindingAttr, binder, callConvention, types, modifiers);
    }

    /// <summary>
    ///   Выполняет поиск конструктора, параметры которого соответствуют указанным типам аргументов и модификаторам, используя заданные ограничения привязки.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <param name="binder">
    ///   Объект, определяющий набор свойств и разрешающий привязку, что может включать выбор перегруженных методов, приведение типов аргументов и вызов члена с помощью отражения.
    /// 
    ///   -или-
    /// 
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic) для использования свойства <see cref="P:System.Type.DefaultBinder" />.
    /// </param>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющий число, порядок и тип параметров, извлекаемых конструктором.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов типа <see cref="T:System.Type" /> (то есть Type[] types = new Type[0]), если требуется получить конструктор, который не имеет параметров.
    /// 
    ///   -или-
    /// 
    ///   <see cref="F:System.Type.EmptyTypes" />.
    /// </param>
    /// <param name="modifiers">
    ///   Массив объектов <see cref="T:System.Reflection.ParameterModifier" />, представляющих атрибуты, связанные с соответствующим элементом в массиве типов параметра.
    ///    Связыватель по умолчанию не обрабатывает этот параметр.
    /// </param>
    /// <returns>
    ///   Если поиск выполнен удачно, возвращается объект <see cref="T:System.Reflection.ConstructorInfo" />, представляющий конструктор, который соответствует указанным требованиям; в противном случае возвращается значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="types" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один из элементов в <paramref name="types" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="types" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   Массив <paramref name="modifiers" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="types" /> и <paramref name="modifiers" /> имеют разную длину.
    /// </exception>
    [ComVisible(true)]
    public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
    {
      if (types == null)
        throw new ArgumentNullException(nameof (types));
      for (int index = 0; index < types.Length; ++index)
      {
        if (types[index] == (Type) null)
          throw new ArgumentNullException(nameof (types));
      }
      return this.GetConstructorImpl(bindingAttr, binder, CallingConventions.Any, types, modifiers);
    }

    /// <summary>
    ///   Выполняет поиск открытого конструктора экземпляра, параметры которого соответствуют типам, содержащимся в указанном массиве.
    /// </summary>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющих число, порядок и тип параметров нужного конструктора.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов <see cref="T:System.Type" /> для получения конструктора, не имеющего параметров.
    ///    Подобный пустой массив предоставляется полем <see langword="static" /> с описателем <see cref="F:System.Type.EmptyTypes" />.
    /// </param>
    /// <returns>
    ///   Объект, представляющий открытый конструктор экземпляра, параметры которого соответствуют типам, указанным в массиве типов параметров, если такой конструктор найден; в противном случае — <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="types" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один из элементов в <paramref name="types" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="types" /> является многомерным.
    /// </exception>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public ConstructorInfo GetConstructor(Type[] types)
    {
      return this.GetConstructor(BindingFlags.Instance | BindingFlags.Public, (Binder) null, types, (ParameterModifier[]) null);
    }

    /// <summary>
    ///   При переопределении в производном классе ищет конструктор, параметры которого соответствуют указанным типам аргументов и модификаторам, используя для этого заданные ограничения привязки и соглашение о вызовах.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <param name="binder">
    ///   Объект, определяющий набор свойств и разрешающий привязку, что может включать выбор перегруженных методов, приведение типов аргументов и вызов члена с помощью отражения.
    /// 
    ///   -или-
    /// 
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic) для использования свойства <see cref="P:System.Type.DefaultBinder" />.
    /// </param>
    /// <param name="callConvention">
    ///   Объект, определяющий набор применяемых правил, касающихся порядка и расположения аргументов, способа передачи возвращаемого значения, регистров, используемых для аргументов, и очистки стека.
    /// </param>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющий число, порядок и тип параметров, извлекаемых конструктором.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов типа <see cref="T:System.Type" /> (то есть Type[] types = new Type[0]), если требуется получить конструктор, который не имеет параметров.
    /// </param>
    /// <param name="modifiers">
    ///   Массив объектов <see cref="T:System.Reflection.ParameterModifier" />, представляющих атрибуты, связанные с соответствующим элементом в массиве <paramref name="types" />.
    ///    Связыватель по умолчанию не обрабатывает этот параметр.
    /// </param>
    /// <returns>
    ///   Если поиск выполнен удачно, возвращается объект <see cref="T:System.Reflection.ConstructorInfo" />, представляющий конструктор, который соответствует указанным требованиям; в противном случае возвращается значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="types" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один из элементов в <paramref name="types" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="types" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   Массив <paramref name="modifiers" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="types" /> и <paramref name="modifiers" /> имеют разную длину.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий тип — <see cref="T:System.Reflection.Emit.TypeBuilder" /> или <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />.
    /// </exception>
    protected abstract ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

    /// <summary>
    ///   Возвращает все открытые конструкторы, определенные для текущего объекта <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.ConstructorInfo" />, представляющий все открытые конструкторы экземпляров, определенные для текущего типа <see cref="T:System.Type" />, за исключением инициализатора типа (статический конструктор).
    ///    Если для текущего объекта <see cref="T:System.Type" /> открытые конструкторы экземпляров не определены или если текущий объект <see cref="T:System.Type" /> представляет параметр типа в определении универсального типа или метода, возвращается пустой массив типа <see cref="T:System.Reflection.ConstructorInfo" />.
    /// </returns>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public ConstructorInfo[] GetConstructors()
    {
      return this.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
    }

    /// <summary>
    ///   При переопределении в производном классе ищет конструкторы, определенные для текущего объекта <see cref="T:System.Type" />, с использованием указанного объекта <see langword="BindingFlags" />.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.ConstructorInfo" />, представляющий все конструкторы, определенные для текущего объекта <see cref="T:System.Type" /> и удовлетворяющие указанным ограничениям привязки, в том числе и инициализатор типа, если он определен.
    ///    Возвращает пустой массив типа <see cref="T:System.Reflection.ConstructorInfo" />, если для текущего типа <see cref="T:System.Type" /> не определены конструкторы, если ни один из определенных конструкторов не соответствует ограничениям привязки или если текущий тип <see cref="T:System.Type" /> представляет параметр типа в определении универсального типа или метода.
    /// </returns>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public abstract ConstructorInfo[] GetConstructors(BindingFlags bindingAttr);

    /// <summary>Возвращает инициализатор типа.</summary>
    /// <returns>
    ///   Объект, содержащий имя конструктора класса <see cref="T:System.Type" />.
    /// </returns>
    [ComVisible(true)]
    public ConstructorInfo TypeInitializer
    {
      get
      {
        return this.GetConstructorImpl(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, CallingConventions.Any, Type.EmptyTypes, (ParameterModifier[]) null);
      }
    }

    /// <summary>
    ///   Ищет метод с параметрами, соответствующими указанным модификаторам и типам аргументов, с учетом заданных ограничений привязки и соглашений о вызовах.
    /// </summary>
    /// <param name="name">Строка, содержащая имя искомого метода.</param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <param name="binder">
    ///   Объект, определяющий набор свойств и разрешающий привязку, что может включать выбор перегруженных методов, приведение типов аргументов и вызов члена с помощью отражения.
    /// 
    ///   -или-
    /// 
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic) для использования свойства <see cref="P:System.Type.DefaultBinder" />.
    /// </param>
    /// <param name="callConvention">
    ///   Объект, определяющий набор применяемых правил, касающихся порядка и расположения аргументов, способа передачи возвращаемого значения, регистров, используемых для аргументов, и способа очистки стека.
    /// </param>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющий число, порядок и тип параметров искомого метода.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов <see cref="T:System.Type" /> (в соответствии со значением поля <see cref="F:System.Type.EmptyTypes" />) для получения метода, не принимающего параметры.
    /// </param>
    /// <param name="modifiers">
    ///   Массив объектов <see cref="T:System.Reflection.ParameterModifier" />, представляющих атрибуты, связанные с соответствующим элементом в массиве <paramref name="types" />.
    ///    Используется только при вызове посредством COM-взаимодействия. При этом обрабатываются только параметры, переданные по ссылке.
    ///    Связыватель по умолчанию не обрабатывает этот параметр.
    /// </param>
    /// <returns>
    ///   Если поиск выполнен удачно, возвращается объект, предоставляющий метод, который соответствует указанным требованиям; в противном случае возвращается <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найдено несколько методов с указанным именем и соответствующих указанным ограничениям привязки.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="types" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один из элементов в <paramref name="types" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="types" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   Массив <paramref name="modifiers" /> является многомерным.
    /// </exception>
    public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (types == null)
        throw new ArgumentNullException(nameof (types));
      for (int index = 0; index < types.Length; ++index)
      {
        if (types[index] == (Type) null)
          throw new ArgumentNullException(nameof (types));
      }
      return this.GetMethodImpl(name, bindingAttr, binder, callConvention, types, modifiers);
    }

    /// <summary>
    ///   Ищет заданный метод, параметры которого соответствуют указанным типам аргументов и модификаторам, используя установленные ограничения привязки.
    /// </summary>
    /// <param name="name">Строка, содержащая имя искомого метода.</param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <param name="binder">
    ///   Объект, определяющий набор свойств и разрешающий привязку, что может включать выбор перегруженных методов, приведение типов аргументов и вызов члена с помощью отражения.
    /// 
    ///   -или-
    /// 
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic) для использования свойства <see cref="P:System.Type.DefaultBinder" />.
    /// </param>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющий число, порядок и тип параметров искомого метода.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов <see cref="T:System.Type" /> (в соответствии со значением поля <see cref="F:System.Type.EmptyTypes" />) для получения метода, не принимающего параметры.
    /// </param>
    /// <param name="modifiers">
    ///   Массив объектов <see cref="T:System.Reflection.ParameterModifier" />, представляющих атрибуты, связанные с соответствующим элементом в массиве <paramref name="types" />.
    ///    Используется только при вызове посредством COM-взаимодействия. При этом обрабатываются только параметры, переданные по ссылке.
    ///    Связыватель по умолчанию не обрабатывает этот параметр.
    /// </param>
    /// <returns>
    ///   Если поиск выполнен удачно, возвращается объект, предоставляющий метод, который соответствует указанным требованиям; в противном случае возвращается <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найдено несколько методов с указанным именем и соответствующих указанным ограничениям привязки.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="types" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один из элементов в <paramref name="types" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="types" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   Массив <paramref name="modifiers" /> является многомерным.
    /// </exception>
    public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (types == null)
        throw new ArgumentNullException(nameof (types));
      for (int index = 0; index < types.Length; ++index)
      {
        if (types[index] == (Type) null)
          throw new ArgumentNullException(nameof (types));
      }
      return this.GetMethodImpl(name, bindingAttr, binder, CallingConventions.Any, types, modifiers);
    }

    /// <summary>
    ///   Выполняет поиск указанного открытого метода, параметры которого соответствуют указанным типам аргументов и модификаторам.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащая имя искомого открытого метода.
    /// </param>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющий число, порядок и тип параметров искомого метода.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов <see cref="T:System.Type" /> (в соответствии со значением поля <see cref="F:System.Type.EmptyTypes" />) для получения метода, не принимающего параметры.
    /// </param>
    /// <param name="modifiers">
    ///   Массив объектов <see cref="T:System.Reflection.ParameterModifier" />, представляющих атрибуты, связанные с соответствующим элементом в массиве <paramref name="types" />.
    ///    Используется только при вызове посредством COM-взаимодействия. При этом обрабатываются только параметры, переданные по ссылке.
    ///    Связыватель по умолчанию не обрабатывает этот параметр.
    /// </param>
    /// <returns>
    ///   Если поиск выполнен успешно, возвращается объект, представляющий открытый метод, который соответствует указанным требованиям; в противном случае возвращается <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найдено несколько методов с указанным именем и заданными параметрами.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="types" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один из элементов в <paramref name="types" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="types" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   Массив <paramref name="modifiers" /> является многомерным.
    /// </exception>
    public MethodInfo GetMethod(string name, Type[] types, ParameterModifier[] modifiers)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (types == null)
        throw new ArgumentNullException(nameof (types));
      for (int index = 0; index < types.Length; ++index)
      {
        if (types[index] == (Type) null)
          throw new ArgumentNullException(nameof (types));
      }
      return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, CallingConventions.Any, types, modifiers);
    }

    /// <summary>
    ///   Ищет указанный открытый метод, параметры которого соответствуют заданным типам аргументов.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащая имя искомого открытого метода.
    /// </param>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющий число, порядок и тип параметров искомого метода.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов <see cref="T:System.Type" /> (в соответствии со значением поля <see cref="F:System.Type.EmptyTypes" />) для получения метода, не принимающего параметры.
    /// </param>
    /// <returns>
    ///   Объект, представляющий открытый метод, параметры которого соответствуют указанным типам аргументов, если они существуют, и <see langword="null" />, если их нет.
    /// </returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найдено несколько методов с указанным именем и заданными параметрами.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="types" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один из элементов в <paramref name="types" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="types" /> является многомерным.
    /// </exception>
    [__DynamicallyInvokable]
    public MethodInfo GetMethod(string name, Type[] types)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (types == null)
        throw new ArgumentNullException(nameof (types));
      for (int index = 0; index < types.Length; ++index)
      {
        if (types[index] == (Type) null)
          throw new ArgumentNullException(nameof (types));
      }
      return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, CallingConventions.Any, types, (ParameterModifier[]) null);
    }

    /// <summary>
    ///   Выполняет поиск указанного метода, используя заданные ограничения привязки.
    /// </summary>
    /// <param name="name">Строка, содержащая имя искомого метода.</param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Если поиск выполнен удачно, возвращается объект, предоставляющий метод, который соответствует указанным требованиям; в противном случае возвращается <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найдено несколько методов с указанным именем и соответствующих указанным ограничениям привязки.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public MethodInfo GetMethod(string name, BindingFlags bindingAttr)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      return this.GetMethodImpl(name, bindingAttr, (Binder) null, CallingConventions.Any, (Type[]) null, (ParameterModifier[]) null);
    }

    /// <summary>Выполняет поиск открытого метода с заданным именем.</summary>
    /// <param name="name">
    ///   Строка, содержащая имя искомого открытого метода.
    /// </param>
    /// <returns>
    ///   Объект, представляющий открытый метод с заданным именем, если такой метод есть, и <see langword="null" />, если такого метода нет.
    /// </returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найдено несколько методов с указанным именем.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public MethodInfo GetMethod(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, CallingConventions.Any, (Type[]) null, (ParameterModifier[]) null);
    }

    /// <summary>
    ///   При переопределении в производном классе ищет указанный метод, параметры которого соответствуют указанным типам аргументов и модификаторам, используя для этого заданные ограничения привязки и соглашение о вызовах.
    /// </summary>
    /// <param name="name">Строка, содержащая имя искомого метода.</param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <param name="binder">
    ///   Объект, определяющий набор свойств и разрешающий привязку, что может включать выбор перегруженных методов, приведение типов аргументов и вызов члена с помощью отражения.
    /// 
    ///   -или-
    /// 
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic) для использования свойства <see cref="P:System.Type.DefaultBinder" />.
    /// </param>
    /// <param name="callConvention">
    ///   Объект, который задает набор правил, используемых в зависимости от порядка и расположения аргументов, способа передачи возвращаемого значения, регистров, используемых для аргументов, и процесса очистки стека.
    /// </param>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющий число, порядок и тип параметров искомого метода.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов типа <see cref="T:System.Type" /> (то есть Type[] types = new Type[0]), если требуется получить метод, который не имеет параметров.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" />.
    ///    Если значение параметра <paramref name="types" /> равно <see langword="null" />, аргументы метода не проверяются на соответствие условиям.
    /// </param>
    /// <param name="modifiers">
    ///   Массив объектов <see cref="T:System.Reflection.ParameterModifier" />, представляющих атрибуты, связанные с соответствующим элементом в массиве <paramref name="types" />.
    ///    Связыватель по умолчанию не обрабатывает этот параметр.
    /// </param>
    /// <returns>
    ///   Если поиск выполнен удачно, возвращается объект, предоставляющий метод, который соответствует указанным требованиям; в противном случае возвращается <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найдено несколько методов с указанным именем и соответствующих указанным ограничениям привязки.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="types" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   Массив <paramref name="modifiers" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="types" /> и <paramref name="modifiers" /> имеют разную длину.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий тип — <see cref="T:System.Reflection.Emit.TypeBuilder" /> или <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />.
    /// </exception>
    protected abstract MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

    /// <summary>
    ///   Возвращает все открытые методы текущего объекта <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.MethodInfo" />, представляющий все открытые методы, определенные для текущего объекта <see cref="T:System.Type" />.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Reflection.MethodInfo" />, если для текущего типа <see cref="T:System.Type" /> открытые методы не определены.
    /// </returns>
    [__DynamicallyInvokable]
    public MethodInfo[] GetMethods()
    {
      return this.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>
    ///   При переопределении в производном классе ищет методы, определенные для текущего объекта <see cref="T:System.Type" />, используя указанные ограничения привязки.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.MethodInfo" />, представляющий все методы, определенные для текущего типа <see cref="T:System.Type" /> и удовлетворяющие указанным ограничениям привязки.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Reflection.MethodInfo" />, если для текущего объекта <see cref="T:System.Type" /> не определены методы или ни один из определенных методов не удовлетворяет ограничениям привязки.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract MethodInfo[] GetMethods(BindingFlags bindingAttr);

    /// <summary>
    ///   Выполняет поиск указанного поля, используя заданные ограничения привязки.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащая имя искомого поля данных.
    /// </param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Объект, предоставляющий поле, которое соответствует указанным требованиям, если такое свойство найдено; в противном случае возвращается <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract FieldInfo GetField(string name, BindingFlags bindingAttr);

    /// <summary>Выполняет поиск открытого поля с заданным именем.</summary>
    /// <param name="name">
    ///   Строка, содержащая имя искомого поля данных.
    /// </param>
    /// <returns>
    ///   Объект, представляющий открытое поле с указанным именем, если такое свойство есть, или <see langword="null" />, если такого свойства нет.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот объект <see cref="T:System.Type" /> является <see cref="T:System.Reflection.Emit.TypeBuilder" />, метод <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> которого еще не был вызван.
    /// </exception>
    [__DynamicallyInvokable]
    public FieldInfo GetField(string name)
    {
      return this.GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>
    ///   Возвращает все открытые поля текущего объекта <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.FieldInfo" />, представляющий все открытые поля, определенные для текущего объекта <see cref="T:System.Type" />.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Reflection.FieldInfo" />, если для текущего типа <see cref="T:System.Type" /> открытые поля не определены.
    /// </returns>
    [__DynamicallyInvokable]
    public FieldInfo[] GetFields()
    {
      return this.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>
    ///   При переопределении в производном классе ищет поля, определенные для текущего объекта <see cref="T:System.Type" />, используя указанные ограничения привязки.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.FieldInfo" />, представляющий все поля, определенные для текущего типа <see cref="T:System.Type" /> и удовлетворяющие указанным ограничениям привязки.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Reflection.FieldInfo" />, если для текущего объекта <see cref="T:System.Type" /> не определены поля или ни одно из определенных полей не удовлетворяет ограничениям привязки.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract FieldInfo[] GetFields(BindingFlags bindingAttr);

    /// <summary>Выполняет поиск интерфейса с заданным именем.</summary>
    /// <param name="name">
    ///   Строка, содержащая имя искомого интерфейса.
    ///    Для универсальных интерфейсов это искаженное имя.
    /// </param>
    /// <returns>
    ///   Объект, представляющий интерфейс с заданным именем, который реализуется или наследуется текущим объектом <see cref="T:System.Type" />, если такой интерфейс существует; в противном случае — значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Текущий <see cref="T:System.Type" /> представляет тип, реализующий тот же универсальный интерфейс с другими аргументами типа.
    /// </exception>
    public Type GetInterface(string name)
    {
      return this.GetInterface(name, false);
    }

    /// <summary>
    ///   При переопределении в производном классе ищет интерфейс с заданным именем, позволяющий определить, нужно ли выполнять поиск без учета регистра.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащая имя искомого интерфейса.
    ///    Для универсальных интерфейсов это искаженное имя.
    /// </param>
    /// <param name="ignoreCase">
    ///   Значение <see langword="true" />, чтобы игнорировать регистр той части параметра <paramref name="name" />, в которой задается простое имя интерфейса (регистр части, соответствующей пространству имен, должен быть надлежащим образом соблюден).
    /// 
    ///   -или-
    /// 
    ///   Значение <see langword="false" />, для поиска с учетом регистра всех частей параметра <paramref name="name" />.
    /// </param>
    /// <returns>
    ///   Объект, представляющий интерфейс с заданным именем, который реализуется или наследуется текущим объектом <see cref="T:System.Type" />, если такой интерфейс существует; в противном случае — значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Текущий <see cref="T:System.Type" /> представляет тип, реализующий тот же универсальный интерфейс с другими аргументами типа.
    /// </exception>
    public abstract Type GetInterface(string name, bool ignoreCase);

    /// <summary>
    ///   При переопределении в производном классе возвращает все интерфейсы, реализуемые или наследуемые текущим объектом <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Type" />, представляющий все интерфейсы, реализуемые или наследуемые текущим типом <see cref="T:System.Type" />.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Type" /> в случае отсутствия интерфейсов, реализуемых или наследуемых текущим типом <see cref="T:System.Type" />.
    /// </returns>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Статический инициализатор вызывается и создает исключение.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract Type[] GetInterfaces();

    /// <summary>
    ///   Возвращает массив объектов <see cref="T:System.Type" />, представляющий отфильтрованный список интерфейсов, реализованных или наследуемых текущим объектом <see cref="T:System.Type" />.
    /// </summary>
    /// <param name="filter">
    ///   Делегат, сравнивающий интерфейсы с параметром <paramref name="filterCriteria" />.
    /// </param>
    /// <param name="filterCriteria">
    ///   Критерий поиска, определяющий, должен ли тот или иной интерфейс включаться в возвращаемый массив.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Type" />, представляющий отфильтрованный список интерфейсов, которые реализует или наследует текущий объект <see cref="T:System.Type" />, или пустой массив типа <see cref="T:System.Type" />, если после применения фильтра для текущего объекта <see cref="T:System.Type" /> не удалось найти соответствующие интерфейсы.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="filter" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Статический инициализатор вызывается и создает исключение.
    /// </exception>
    public virtual Type[] FindInterfaces(TypeFilter filter, object filterCriteria)
    {
      if (filter == null)
        throw new ArgumentNullException(nameof (filter));
      Type[] interfaces = this.GetInterfaces();
      int length = 0;
      for (int index = 0; index < interfaces.Length; ++index)
      {
        if (!filter(interfaces[index], filterCriteria))
          interfaces[index] = (Type) null;
        else
          ++length;
      }
      if (length == interfaces.Length)
        return interfaces;
      Type[] typeArray = new Type[length];
      int num = 0;
      for (int index = 0; index < interfaces.Length; ++index)
      {
        if (interfaces[index] != (Type) null)
          typeArray[num++] = interfaces[index];
      }
      return typeArray;
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Reflection.EventInfo" />, представляющий указанное открытое событие.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащая имя события, которое объявлено или унаследовано текущим типом <see cref="T:System.Type" />.
    /// </param>
    /// <returns>
    ///   Объект, представляющий указанное открытое событие, которое объявлено или унаследовано в текущем объекте <see cref="T:System.Type" />, если такое событие найдено; в противном случае — <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public EventInfo GetEvent(string name)
    {
      return this.GetEvent(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>
    ///   При переопределении в производном классе возвращает объект <see cref="T:System.Reflection.EventInfo" />, представляющий указанное событие, используя для этого указанные ограничения привязки.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащая имя события, которое объявлено или унаследовано текущим типом <see cref="T:System.Type" />.
    /// </param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Объект, представляющий указанное событие, которое объявлено или унаследовано текущим типом <see cref="T:System.Type" />, если такое событие найдено; <see langword="null" /> в противном случае.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract EventInfo GetEvent(string name, BindingFlags bindingAttr);

    /// <summary>
    ///   Возвращает все открытые события, которые объявлены или унаследованы текущим объектом <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.EventInfo" />, представляющий все открытые события, которые объявлены или унаследованы текущим объектом <see cref="T:System.Type" />.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Reflection.EventInfo" />, если в текущем объекте <see cref="T:System.Type" /> нет открытых событий.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual EventInfo[] GetEvents()
    {
      return this.GetEvents(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>
    ///   При переопределении в производном классе ищет события, которые объявлены или унаследованы текущим объектом <see cref="T:System.Type" />, используя указанные ограничения привязки.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.EventInfo" />, представляющий все события, которые объявлены или унаследованы данным объектом <see cref="T:System.Type" /> и удовлетворяют указанным ограничениям привязки.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Reflection.EventInfo" />, если у текущего типа <see cref="T:System.Type" /> нет событий или ни одно событие не удовлетворяет ограничениям привязки.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract EventInfo[] GetEvents(BindingFlags bindingAttr);

    /// <summary>
    ///   Ищет свойство с параметрами, соответствующими указанным модификаторам и типам аргументов, с учетом заданных ограничений привязки.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащий имя искомого свойства.
    /// </param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <param name="binder">
    ///   Объект, определяющий набор свойств и разрешающий привязку, что может включать выбор перегруженных методов, приведение типов аргументов и вызов члена с помощью отражения.
    /// 
    ///   -или-
    /// 
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic) для использования свойства <see cref="P:System.Type.DefaultBinder" />.
    /// </param>
    /// <param name="returnType">Возвращаемый тип свойства.</param>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющий число, порядок и тип параметров искомого индексированного свойства.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов типа <see cref="T:System.Type" /> (то есть Type[] types = new Type[0]), если требуется получить неиндексированное свойство.
    /// </param>
    /// <param name="modifiers">
    ///   Массив объектов <see cref="T:System.Reflection.ParameterModifier" />, представляющих атрибуты, связанные с соответствующим элементом в массиве <paramref name="types" />.
    ///    Связыватель по умолчанию не обрабатывает этот параметр.
    /// </param>
    /// <returns>
    ///   Объект, предоставляющий свойство, которое соответствует указанным требованиям, если такое свойство найдено; в противном случае возвращается <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найдено несколько свойств с указанным именем, соответствующих указанным ограничениям привязки.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="types" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="types" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   Массив <paramref name="modifiers" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="types" /> и <paramref name="modifiers" /> имеют разную длину.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    ///   Элемент <paramref name="types" /> имеет значение <see langword="null" />.
    /// </exception>
    public PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (types == null)
        throw new ArgumentNullException(nameof (types));
      return this.GetPropertyImpl(name, bindingAttr, binder, returnType, types, modifiers);
    }

    /// <summary>
    ///   Ищет заданное открытое свойство, параметры которого соответствуют указанным типам аргументов и модификаторам.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащая искомое имя открытого свойства.
    /// </param>
    /// <param name="returnType">Возвращаемый тип свойства.</param>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющий число, порядок и тип параметров искомого индексированного свойства.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов типа <see cref="T:System.Type" /> (то есть Type[] types = new Type[0]), если требуется получить неиндексированное свойство.
    /// </param>
    /// <param name="modifiers">
    ///   Массив объектов <see cref="T:System.Reflection.ParameterModifier" />, представляющих атрибуты, связанные с соответствующим элементом в массиве <paramref name="types" />.
    ///    Связыватель по умолчанию не обрабатывает этот параметр.
    /// </param>
    /// <returns>
    ///   Объект, представляющий открытое свойство, которое соответствует указанным требованиям, если такое свойство найдено; в противном случае возвращается <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найдено несколько свойств с указанным именем и соответствующих указанным типам аргументов и модификаторам.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="types" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="types" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   Массив <paramref name="modifiers" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="types" /> и <paramref name="modifiers" /> имеют разную длину.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    ///   Элемент <paramref name="types" /> имеет значение <see langword="null" />.
    /// </exception>
    public PropertyInfo GetProperty(string name, Type returnType, Type[] types, ParameterModifier[] modifiers)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (types == null)
        throw new ArgumentNullException(nameof (types));
      return this.GetPropertyImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, returnType, types, modifiers);
    }

    /// <summary>
    ///   Ищет указанное свойство, используя заданные ограничения привязки.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащий имя искомого свойства.
    /// </param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Объект, предоставляющий свойство, которое соответствует указанным требованиям, если такое свойство найдено; в противном случае возвращается <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найдено несколько свойств с указанным именем и соответствующих указанным ограничениям привязки.
    ///    См. заметки.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public PropertyInfo GetProperty(string name, BindingFlags bindingAttr)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      return this.GetPropertyImpl(name, bindingAttr, (Binder) null, (Type) null, (Type[]) null, (ParameterModifier[]) null);
    }

    /// <summary>
    ///   Ищет указанное открытое свойство, параметры которого соответствуют указанным типам аргументов.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащая искомое имя открытого свойства.
    /// </param>
    /// <param name="returnType">Возвращаемый тип свойства.</param>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющий число, порядок и тип параметров искомого индексированного свойства.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов типа <see cref="T:System.Type" /> (то есть Type[] types = new Type[0]), если требуется получить неиндексированное свойство.
    /// </param>
    /// <returns>
    ///   Объект, представляющий открытое свойство, параметры которого соответствуют указанным условиям, если такое свойство существует, и <see langword="null" />, если такого свойства нет.
    /// </returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найдено несколько свойств с указанным именем и соответствующих указанным типам аргументов.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="types" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="types" /> является многомерным.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    ///   Элемент <paramref name="types" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public PropertyInfo GetProperty(string name, Type returnType, Type[] types)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (types == null)
        throw new ArgumentNullException(nameof (types));
      return this.GetPropertyImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, returnType, types, (ParameterModifier[]) null);
    }

    /// <summary>
    ///   Ищет указанное открытое свойство, параметры которого соответствуют указанным типам аргументов.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащая искомое имя открытого свойства.
    /// </param>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющий число, порядок и тип параметров искомого индексированного свойства.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов типа <see cref="T:System.Type" /> (то есть Type[] types = new Type[0]), если требуется получить неиндексированное свойство.
    /// </param>
    /// <returns>
    ///   Объект, представляющий открытое свойство, параметры которого соответствуют указанным условиям, если такое свойство существует, и <see langword="null" />, если такого свойства нет.
    /// </returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найдено несколько свойств с указанным именем и соответствующих указанным типам аргументов.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="types" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="types" /> является многомерным.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    ///   Элемент <paramref name="types" /> имеет значение <see langword="null" />.
    /// </exception>
    public PropertyInfo GetProperty(string name, Type[] types)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (types == null)
        throw new ArgumentNullException(nameof (types));
      return this.GetPropertyImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, (Type) null, types, (ParameterModifier[]) null);
    }

    /// <summary>
    ///   Выполняет поиск открытого свойства с заданным именем и типом возвращаемого значения.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащая искомое имя открытого свойства.
    /// </param>
    /// <param name="returnType">Возвращаемый тип свойства.</param>
    /// <returns>
    ///   Объект, представляющий открытое свойство с заданным именем, если такое свойство есть, и <see langword="null" />, если такого свойства нет.
    /// </returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найдено несколько свойств с указанным именем.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="name" /> имеет значение <see langword="null" />, или <paramref name="returnType" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public PropertyInfo GetProperty(string name, Type returnType)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (returnType == (Type) null)
        throw new ArgumentNullException(nameof (returnType));
      return this.GetPropertyImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, returnType, (Type[]) null, (ParameterModifier[]) null);
    }

    internal PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Type returnType)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (returnType == (Type) null)
        throw new ArgumentNullException(nameof (returnType));
      return this.GetPropertyImpl(name, bindingAttr, (Binder) null, returnType, (Type[]) null, (ParameterModifier[]) null);
    }

    /// <summary>
    ///   Выполняет поиск открытого свойства с заданным именем.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащая искомое имя открытого свойства.
    /// </param>
    /// <returns>
    ///   Объект, представляющий открытое свойство с заданным именем, если такое свойство есть, и <see langword="null" />, если такого свойства нет.
    /// </returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найдено несколько свойств с указанным именем.
    ///    См. заметки.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public PropertyInfo GetProperty(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      return this.GetPropertyImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, (Type) null, (Type[]) null, (ParameterModifier[]) null);
    }

    /// <summary>
    ///   При переопределении в производном классе выполняет поиск заданного свойства, параметры которого соответствуют типам и модификаторам заданных аргументов, с использованием заданных ограничений привязки.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащий имя искомого свойства.
    /// </param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <param name="binder">
    ///   Объект, определяющий набор свойств и разрешающий привязку, что может включать выбор перегруженного члена, приведение типов аргументов и вызов члена с помощью отражения.
    /// 
    ///   -или-
    /// 
    ///   Пустая ссылка (<see langword="Nothing" /> в Visual Basic) для использования свойства <see cref="P:System.Type.DefaultBinder" />.
    /// </param>
    /// <param name="returnType">Возвращаемый тип свойства.</param>
    /// <param name="types">
    ///   Массив объектов <see cref="T:System.Type" />, предоставляющий число, порядок и тип параметров искомого индексированного свойства.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов типа <see cref="T:System.Type" /> (то есть Type[] types = new Type[0]), если требуется получить неиндексированное свойство.
    /// </param>
    /// <param name="modifiers">
    ///   Массив объектов <see cref="T:System.Reflection.ParameterModifier" />, представляющих атрибуты, связанные с соответствующим элементом в массиве <paramref name="types" />.
    ///    Связыватель по умолчанию не обрабатывает этот параметр.
    /// </param>
    /// <returns>
    ///   Объект, предоставляющий свойство, которое соответствует указанным требованиям, если такое свойство найдено; в противном случае возвращается <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Найдено несколько свойств с указанным именем, соответствующих указанным ограничениям привязки.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="types" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один из элементов в <paramref name="types" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="types" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   Массив <paramref name="modifiers" /> является многомерным.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="types" /> и <paramref name="modifiers" /> имеют разную длину.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий тип — <see cref="T:System.Reflection.Emit.TypeBuilder" />, <see cref="T:System.Reflection.Emit.EnumBuilder" /> или <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />.
    /// </exception>
    protected abstract PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

    /// <summary>
    ///   При переопределении в производном классе ищет свойства текущего объекта <see cref="T:System.Type" />, используя указанные ограничения привязки.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.PropertyInfo" />, представляющий все свойства текущего типа <see cref="T:System.Type" />, которые удовлетворяют указанным ограничениям привязки.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Reflection.PropertyInfo" />, если у текущего типа <see cref="T:System.Type" /> нет свойств или ни одно свойство не удовлетворяет ограничениям привязки.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract PropertyInfo[] GetProperties(BindingFlags bindingAttr);

    /// <summary>
    ///   Возвращает все открытые свойства текущего объекта <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.PropertyInfo" />, представляющий все открытые свойства текущего типа <see cref="T:System.Type" />.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Reflection.PropertyInfo" />, если у текущего типа <see cref="T:System.Type" /> нет открытых свойств.
    /// </returns>
    [__DynamicallyInvokable]
    public PropertyInfo[] GetProperties()
    {
      return this.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>
    ///   Возвращает открытые типы, вложенные в текущий объект <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Type" />, представляющий открытые типы, вложенные в текущий объект <see cref="T:System.Type" /> (нерекурсивный поиск), или пустой массив типа <see cref="T:System.Type" />, если в текущий объект <see cref="T:System.Type" /> не вложен ни один открытый тип.
    /// </returns>
    public Type[] GetNestedTypes()
    {
      return this.GetNestedTypes(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>
    ///   При переопределении в производном классе ищет типы, вложенные в текущий объект <see cref="T:System.Type" />, используя заданные ограничения привязки.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Type" />, представляющий все типы, вложенные в текущий объект <see cref="T:System.Type" />, удовлетворяющий заданным ограничениям привязки (нерекурсивный поиск), или пустой массив типа <see cref="T:System.Type" />, если вложенные типы, удовлетворяющие ограничениям привязки, не найдены.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract Type[] GetNestedTypes(BindingFlags bindingAttr);

    /// <summary>
    ///   Выполняет поиск открытого вложенного типа с заданным именем.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащая имя искомого вложенного типа.
    /// </param>
    /// <returns>
    ///   Объект, представляющий открытый вложенный тип с указанным именем, если тип есть, и <see langword="null" />, если такого типа нет.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    public Type GetNestedType(string name)
    {
      return this.GetNestedType(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>
    ///   При переопределении в производном классе ищет указанный вложенный тип, используя заданные ограничения привязки.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащая имя искомого вложенного типа.
    /// </param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Если поиск выполнен успешно, возвращается объект, предоставляющий вложенный тип, который соответствует указанным требованиям; в противном случае возвращается <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract Type GetNestedType(string name, BindingFlags bindingAttr);

    /// <summary>Выполняет поиск открытого члена с заданным именем.</summary>
    /// <param name="name">
    ///   Строка, содержащая имя искомых открытых членов.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.MemberInfo" />, представляющий открытые члены с заданным именем, если такие члены есть, и пустой массив, если таких членов нет.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public MemberInfo[] GetMember(string name)
    {
      return this.GetMember(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>
    ///   Выполняет поиск указанных членов, используя заданные ограничения привязки.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащая имя для поиска элементов.
    /// </param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Ноль для возвращения пустого массива.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.MemberInfo" />, представляющий открытые члены с заданным именем, если такие члены есть, и пустой массив, если таких членов нет.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual MemberInfo[] GetMember(string name, BindingFlags bindingAttr)
    {
      return this.GetMember(name, MemberTypes.All, bindingAttr);
    }

    /// <summary>
    ///   Ищет указанные члены заданного типа, используя установленные ограничения привязки.
    /// </summary>
    /// <param name="name">
    ///   Строка, содержащая имя для поиска элементов.
    /// </param>
    /// <param name="type">Значение, которое нужно найти.</param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Ноль для возвращения пустого массива.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.MemberInfo" />, представляющий открытые члены с заданным именем, если такие члены есть, и пустой массив, если таких членов нет.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Реализацию должен обеспечивать производный класс.
    /// </exception>
    public virtual MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    /// <summary>
    ///   Возвращает все открытые члены текущего объекта <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.MemberInfo" />, представляющий все открытые члены текущего типа <see cref="T:System.Type" />.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Reflection.MemberInfo" />, если у текущего типа <see cref="T:System.Type" /> нет открытых членов.
    /// </returns>
    [__DynamicallyInvokable]
    public MemberInfo[] GetMembers()
    {
      return this.GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
    }

    /// <summary>
    ///   При переопределении в производном классе ищет члены, определенные для текущего объекта <see cref="T:System.Type" />, используя указанные ограничения привязки.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Ноль (<see cref="F:System.Reflection.BindingFlags.Default" />) для возвращения пустого массива.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.MemberInfo" />, представляющий все члены, определенные для текущего типа <see cref="T:System.Type" /> и удовлетворяющие указанным ограничениям привязки.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Reflection.MemberInfo" />, если для текущего объекта <see cref="T:System.Type" /> не определены члены или ни один из определенных членов не удовлетворяет ограничениям привязки.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract MemberInfo[] GetMembers(BindingFlags bindingAttr);

    /// <summary>
    ///   Выполняет поиск членов, определенных для текущего объекта <see cref="T:System.Type" />, для которого задан атрибут <see cref="T:System.Reflection.DefaultMemberAttribute" />.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.MemberInfo" />, представляющий все члены по умолчанию текущего объекта <see cref="T:System.Type" />.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив типа <see cref="T:System.Reflection.MemberInfo" />, если у текущего типа <see cref="T:System.Type" /> нет членов по умолчанию.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual MemberInfo[] GetDefaultMembers()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает отфильтрованный массив объектов <see cref="T:System.Reflection.MemberInfo" />, тип которого совпадает с указанным типом члена.
    /// </summary>
    /// <param name="memberType">
    ///   Объект, указывающий тип члена, который нужно найти.
    /// </param>
    /// <param name="bindingAttr">
    ///   Битовая маска, составленная из одного или нескольких объектов <see cref="T:System.Reflection.BindingFlags" /> и указывающая, как ведется поиск.
    /// 
    ///   -или-
    /// 
    ///   Нуль, чтобы было возвращено значение <see langword="null" />.
    /// </param>
    /// <param name="filter">
    ///   Делегат, выполняющий сравнение и возвращающий <see langword="true" />, если проверяемый член соответствует условиям, заданным в параметре <paramref name="filterCriteria" />, и <see langword="false" /> в противном случае.
    ///    Можно использовать делегаты <see langword="FilterAttribute" />, <see langword="FilterName" /> и <see langword="FilterNameIgnoreCase" />, предоставляемые этим классом.
    ///    Первый делегат в качестве условий поиска использует поля классов <see langword="FieldAttributes" />, <see langword="MethodAttributes" /> и <see langword="MethodImplAttributes" />, а два других делегата — объекты <see langword="String" />.
    /// </param>
    /// <param name="filterCriteria">
    ///   Условие поиска, определяющее, будет ли член включен в возвращаемый массив объектов <see langword="MemberInfo" />.
    /// 
    ///   Поля классов <see langword="FieldAttributes" />, <see langword="MethodAttributes" /> и <see langword="MethodImplAttributes" /> могут использоваться вместе с делегатом <see langword="FilterAttribute" />, предоставляемым этим классом.
    /// </param>
    /// <returns>
    ///   Отфильтрованный массив объектов <see cref="T:System.Reflection.MemberInfo" />, имеющих тип указанного члена.
    /// 
    ///   -или-
    /// 
    ///   Пустой массив объектов типа <see cref="T:System.Reflection.MemberInfo" />, если у текущего типа <see cref="T:System.Type" /> нет членов типа <paramref name="memberType" />, удовлетворяющих условиям фильтра.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="filter" /> имеет значение <see langword="null" />.
    /// </exception>
    public virtual MemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria)
    {
      MethodInfo[] methodInfoArray = (MethodInfo[]) null;
      ConstructorInfo[] constructorInfoArray = (ConstructorInfo[]) null;
      FieldInfo[] fieldInfoArray = (FieldInfo[]) null;
      PropertyInfo[] propertyInfoArray = (PropertyInfo[]) null;
      EventInfo[] eventInfoArray = (EventInfo[]) null;
      Type[] typeArray = (Type[]) null;
      int length = 0;
      if ((memberType & MemberTypes.Method) != (MemberTypes) 0)
      {
        methodInfoArray = this.GetMethods(bindingAttr);
        if (filter != null)
        {
          for (int index = 0; index < methodInfoArray.Length; ++index)
          {
            if (!filter((MemberInfo) methodInfoArray[index], filterCriteria))
              methodInfoArray[index] = (MethodInfo) null;
            else
              ++length;
          }
        }
        else
          length += methodInfoArray.Length;
      }
      if ((memberType & MemberTypes.Constructor) != (MemberTypes) 0)
      {
        constructorInfoArray = this.GetConstructors(bindingAttr);
        if (filter != null)
        {
          for (int index = 0; index < constructorInfoArray.Length; ++index)
          {
            if (!filter((MemberInfo) constructorInfoArray[index], filterCriteria))
              constructorInfoArray[index] = (ConstructorInfo) null;
            else
              ++length;
          }
        }
        else
          length += constructorInfoArray.Length;
      }
      if ((memberType & MemberTypes.Field) != (MemberTypes) 0)
      {
        fieldInfoArray = this.GetFields(bindingAttr);
        if (filter != null)
        {
          for (int index = 0; index < fieldInfoArray.Length; ++index)
          {
            if (!filter((MemberInfo) fieldInfoArray[index], filterCriteria))
              fieldInfoArray[index] = (FieldInfo) null;
            else
              ++length;
          }
        }
        else
          length += fieldInfoArray.Length;
      }
      if ((memberType & MemberTypes.Property) != (MemberTypes) 0)
      {
        propertyInfoArray = this.GetProperties(bindingAttr);
        if (filter != null)
        {
          for (int index = 0; index < propertyInfoArray.Length; ++index)
          {
            if (!filter((MemberInfo) propertyInfoArray[index], filterCriteria))
              propertyInfoArray[index] = (PropertyInfo) null;
            else
              ++length;
          }
        }
        else
          length += propertyInfoArray.Length;
      }
      if ((memberType & MemberTypes.Event) != (MemberTypes) 0)
      {
        eventInfoArray = this.GetEvents(bindingAttr);
        if (filter != null)
        {
          for (int index = 0; index < eventInfoArray.Length; ++index)
          {
            if (!filter((MemberInfo) eventInfoArray[index], filterCriteria))
              eventInfoArray[index] = (EventInfo) null;
            else
              ++length;
          }
        }
        else
          length += eventInfoArray.Length;
      }
      if ((memberType & MemberTypes.NestedType) != (MemberTypes) 0)
      {
        typeArray = this.GetNestedTypes(bindingAttr);
        if (filter != null)
        {
          for (int index = 0; index < typeArray.Length; ++index)
          {
            if (!filter((MemberInfo) typeArray[index], filterCriteria))
              typeArray[index] = (Type) null;
            else
              ++length;
          }
        }
        else
          length += typeArray.Length;
      }
      MemberInfo[] memberInfoArray = new MemberInfo[length];
      int num = 0;
      if (methodInfoArray != null)
      {
        for (int index = 0; index < methodInfoArray.Length; ++index)
        {
          if (methodInfoArray[index] != (MethodInfo) null)
            memberInfoArray[num++] = (MemberInfo) methodInfoArray[index];
        }
      }
      if (constructorInfoArray != null)
      {
        for (int index = 0; index < constructorInfoArray.Length; ++index)
        {
          if (constructorInfoArray[index] != (ConstructorInfo) null)
            memberInfoArray[num++] = (MemberInfo) constructorInfoArray[index];
        }
      }
      if (fieldInfoArray != null)
      {
        for (int index = 0; index < fieldInfoArray.Length; ++index)
        {
          if (fieldInfoArray[index] != (FieldInfo) null)
            memberInfoArray[num++] = (MemberInfo) fieldInfoArray[index];
        }
      }
      if (propertyInfoArray != null)
      {
        for (int index = 0; index < propertyInfoArray.Length; ++index)
        {
          if (propertyInfoArray[index] != (PropertyInfo) null)
            memberInfoArray[num++] = (MemberInfo) propertyInfoArray[index];
        }
      }
      if (eventInfoArray != null)
      {
        for (int index = 0; index < eventInfoArray.Length; ++index)
        {
          if (eventInfoArray[index] != (EventInfo) null)
            memberInfoArray[num++] = (MemberInfo) eventInfoArray[index];
        }
      }
      if (typeArray != null)
      {
        for (int index = 0; index < typeArray.Length; ++index)
        {
          if (typeArray[index] != (Type) null)
            memberInfoArray[num++] = (MemberInfo) typeArray[index];
        }
      }
      return memberInfoArray;
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, представляет ли текущий объект <see cref="T:System.Type" /> тип, определение которого вложено в определение другого типа.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> вложен в другой тип; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsNested
    {
      [__DynamicallyInvokable] get
      {
        return this.DeclaringType != (Type) null;
      }
    }

    /// <summary>
    ///   Возвращает атрибуты, связанные с объектом <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.TypeAttributes" />, представляющий набор атрибутов типа <see cref="T:System.Type" />, если <see cref="T:System.Type" /> не представляет параметр универсального типа. В противном случае это значение не определено.
    /// </returns>
    [__DynamicallyInvokable]
    public TypeAttributes Attributes
    {
      [__DynamicallyInvokable] get
      {
        return this.GetAttributeFlagsImpl();
      }
    }

    /// <summary>
    ///   Возвращает сочетание флагов <see cref="T:System.Reflection.GenericParameterAttributes" />, описывающих ковариацию и особые ограничения текущего параметра универсального типа.
    /// </summary>
    /// <returns>
    ///   Побитовое сочетание значений <see cref="T:System.Reflection.GenericParameterAttributes" />, которое описывает ковариацию и особые ограничения текущего параметра универсального типа.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий объект <see cref="T:System.Type" /> не является параметром универсального типа.
    ///    То есть свойство <see cref="P:System.Type.IsGenericParameter" /> возвращает значение <see langword="false" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызванный метод не поддерживается в базовом классе.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual GenericParameterAttributes GenericParameterAttributes
    {
      [__DynamicallyInvokable] get
      {
        throw new NotSupportedException();
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, можно ли получить доступ к объекту <see cref="T:System.Type" /> из кода за пределами сборки.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий объект <see cref="T:System.Type" /> является открытым типом или открытым вложенным типом, все включающие типы которого также являются открытыми; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsVisible
    {
      [__DynamicallyInvokable] get
      {
        RuntimeType type1 = this as RuntimeType;
        if (type1 != (RuntimeType) null)
          return RuntimeTypeHandle.IsVisible(type1);
        if (this.IsGenericParameter)
          return true;
        if (this.HasElementType)
          return this.GetElementType().IsVisible;
        Type type2;
        for (type2 = this; type2.IsNested; type2 = type2.DeclaringType)
        {
          if (!type2.IsNestedPublic)
            return false;
        }
        if (!type2.IsPublic)
          return false;
        if (this.IsGenericType && !this.IsGenericTypeDefinition)
        {
          foreach (Type genericArgument in this.GetGenericArguments())
          {
            if (!genericArgument.IsVisible)
              return false;
          }
        }
        return true;
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, не был ли объект <see cref="T:System.Type" /> объявлен как открытый.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> не объявлен как открытый и не является вложенным типом; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsNotPublic
    {
      [__DynamicallyInvokable] get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NotPublic;
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, был ли объект <see cref="T:System.Type" /> объявлен как открытый.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> объявлен как открытый и не является вложенным типом; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsPublic
    {
      [__DynamicallyInvokable] get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.Public;
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, является ли класс вложенным и объявленным как открытый.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если данный класс является вложенным и объявленным как открытый; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsNestedPublic
    {
      [__DynamicallyInvokable] get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPublic;
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, является ли объект <see cref="T:System.Type" /> вложенным и объявленным как закрытый.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> является вложенным и объявленным как закрытый; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsNestedPrivate
    {
      [__DynamicallyInvokable] get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPrivate;
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, является ли объект <see cref="T:System.Type" /> вложенным и видимым только в своем семействе.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> является вложенным и видимым только внутри собственного семейства; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsNestedFamily
    {
      [__DynamicallyInvokable] get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedFamily;
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, является ли объект <see cref="T:System.Type" /> вложенным и видимым только в своей сборке.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> является вложенным и видимым только в своей сборке; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsNestedAssembly
    {
      [__DynamicallyInvokable] get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedAssembly;
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, является ли объект <see cref="T:System.Type" /> вложенным и видимым только для классов, принадлежащих одновременно к семейству и сборке этого объекта.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> является вложенным и видимым только классам, принадлежащим одновременно к семейству и сборке этого объекта; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsNestedFamANDAssem
    {
      [__DynamicallyInvokable] get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedFamANDAssem;
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, является ли данный объект <see cref="T:System.Type" /> вложенным и видимым только для классов, принадлежащих либо к его семейству, либо к его сборке.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> является вложенным и видимым только классам, принадлежащим его семейству или его сборке; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsNestedFamORAssem
    {
      [__DynamicallyInvokable] get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.VisibilityMask;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, выкладываются ли поля текущего типа автоматически средой CLR.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если свойство <see cref="P:System.Type.Attributes" /> текущего типа включает <see cref="F:System.Reflection.TypeAttributes.AutoLayout" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsAutoLayout
    {
      get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.LayoutMask) == TypeAttributes.NotPublic;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, выкладываются ли поля текущего типа последовательно, в том порядке, в котором они были определены, или выдаются в метаданные.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если свойство <see cref="P:System.Type.Attributes" /> текущего типа включает <see cref="F:System.Reflection.TypeAttributes.SequentialLayout" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsLayoutSequential
    {
      get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.LayoutMask) == TypeAttributes.SequentialLayout;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, выкладываются ли поля текущего типа с явно заданными смещениями.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если свойство <see cref="P:System.Type.Attributes" /> текущего типа включает <see cref="F:System.Reflection.TypeAttributes.ExplicitLayout" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsExplicitLayout
    {
      get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.LayoutMask) == TypeAttributes.ExplicitLayout;
      }
    }

    /// <summary>
    ///   Получает значение, позволяющее определить, является объект <see cref="T:System.Type" /> классом или делегатом (иными словами, не является типом значения или интерфейсом).
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> является классом; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsClass
    {
      [__DynamicallyInvokable] get
      {
        if ((this.GetAttributeFlagsImpl() & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic)
          return !this.IsValueType;
        return false;
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, является ли объект <see cref="T:System.Type" /> интерфейсом (иными словами, не является классом или типом значения).
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> является интерфейсом; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsInterface
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        RuntimeType type = this as RuntimeType;
        if (type != (RuntimeType) null)
          return RuntimeTypeHandle.IsInterface(type);
        return (this.GetAttributeFlagsImpl() & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask;
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, является ли объект <see cref="T:System.Type" /> типом значения.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если тип <see cref="T:System.Type" /> является типом значения; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsValueType
    {
      [__DynamicallyInvokable] get
      {
        return this.IsValueTypeImpl();
      }
    }

    /// <summary>
    ///   Возвращает значение, показывающее, является ли данный объект <see cref="T:System.Type" /> абстрактным объектом, который должен быть переопределен.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если класс <see cref="T:System.Type" /> является абстрактным; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsAbstract
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.GetAttributeFlagsImpl() & TypeAttributes.Abstract) > 0U;
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, был ли объект <see cref="T:System.Type" /> объявлен как запечатанный.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> объявлен как запечатанный; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsSealed
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.GetAttributeFlagsImpl() & TypeAttributes.Sealed) > 0U;
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, представляет ли текущий объект <see cref="T:System.Type" /> перечисление.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий объект <see cref="T:System.Type" /> представляет перечисление; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool IsEnum
    {
      [__DynamicallyInvokable] get
      {
        return this.IsSubclassOf((Type) RuntimeType.EnumType);
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, требует ли имя данного объекта специальной обработки.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если имя типа требует специальной обработки; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsSpecialName
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.GetAttributeFlagsImpl() & TypeAttributes.SpecialName) > 0U;
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, есть ли у объекта <see cref="T:System.Type" /> атрибут <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />, свидетельствующий о том, что объект был импортирован из библиотеки COM-типов.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если у <see cref="T:System.Type" /> есть атрибут <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsImport
    {
      get
      {
        return (uint) (this.GetAttributeFlagsImpl() & TypeAttributes.Import) > 0U;
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, сериализуем ли объект <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> является сериализуемым; в противным случае — значение <see langword="false" />.
    /// </returns>
    public virtual bool IsSerializable
    {
      [__DynamicallyInvokable] get
      {
        if ((this.GetAttributeFlagsImpl() & TypeAttributes.Serializable) != TypeAttributes.NotPublic)
          return true;
        RuntimeType underlyingSystemType = this.UnderlyingSystemType as RuntimeType;
        if (underlyingSystemType != (RuntimeType) null)
          return underlyingSystemType.IsSpecialSerializableType();
        return false;
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, выбран ли для объекта <see langword="AnsiClass" /> атрибут формата строки <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если для данного объекта <see langword="AnsiClass" /> выбран атрибут формата строки <see cref="T:System.Type" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsAnsiClass
    {
      get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.StringFormatMask) == TypeAttributes.NotPublic;
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, выбран ли для объекта <see langword="UnicodeClass" /> атрибут формата строки <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если для данного объекта <see langword="UnicodeClass" /> выбран атрибут формата строки <see cref="T:System.Type" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsUnicodeClass
    {
      get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.StringFormatMask) == TypeAttributes.UnicodeClass;
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, выбран ли для объекта <see langword="AutoClass" /> атрибут формата строки <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если для данного объекта <see langword="AutoClass" /> выбран атрибут формата строки <see cref="T:System.Type" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsAutoClass
    {
      get
      {
        return (this.GetAttributeFlagsImpl() & TypeAttributes.StringFormatMask) == TypeAttributes.AutoClass;
      }
    }

    /// <summary>
    ///   Возвращает значение, показывающее, является ли тип массивом.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий тип является массивом; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsArray
    {
      [__DynamicallyInvokable] get
      {
        return this.IsArrayImpl();
      }
    }

    internal virtual bool IsSzArray
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущий тип универсальным.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий тип является универсальным; в противном случае — значение <see langword=" false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool IsGenericType
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, представляет ли текущий объект <see cref="T:System.Type" /> определение универсального типа, на основе которого можно сконструировать другие универсальные типы.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если этот объект <see cref="T:System.Type" /> представляет определение универсального типа; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool IsGenericTypeDefinition
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, представляет ли этот данный объект сконструированный универсальный тип.
    ///    Можно создать экземпляры сконструированного универсального типа.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если этот объект представляет сконструированный универсальный тип; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool IsConstructedGenericType
    {
      [__DynamicallyInvokable] get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, представляет ли текущий объект <see cref="T:System.Type" /> параметр типа в определении универсального типа или метода.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> представляет параметр определения универсального типа; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool IsGenericParameter
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает позицию параметра типа в списке параметров универсального типа или метода, который объявил параметр, если объект <see cref="T:System.Type" /> представляет параметр универсального типа или метода.
    /// </summary>
    /// <returns>
    ///   Позиция параметра типа в списке параметров типа универсального типа или метода, которые задали этот параметр.
    ///    Нумерация позиций начинается с 0.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий тип не представляет параметр типа.
    ///    То есть <see cref="P:System.Type.IsGenericParameter" /> возвращает <see langword="false" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int GenericParameterPosition
    {
      [__DynamicallyInvokable] get
      {
        throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericParameter"));
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, имеются ли у текущего объекта <see cref="T:System.Type" /> параметры типа, которые не были замещены указанными типами.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> сам является параметром универсального типа или если для его параметров типа не предоставлены определенные типы; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool ContainsGenericParameters
    {
      [__DynamicallyInvokable] get
      {
        if (this.HasElementType)
          return this.GetRootElementType().ContainsGenericParameters;
        if (this.IsGenericParameter)
          return true;
        if (!this.IsGenericType)
          return false;
        foreach (Type genericArgument in this.GetGenericArguments())
        {
          if (genericArgument.ContainsGenericParameters)
            return true;
        }
        return false;
      }
    }

    /// <summary>
    ///   Возвращает массив объектов <see cref="T:System.Type" />, которые представляют ограничения, накладываемые на параметр текущего универсального типа.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Type" />, которые представляют ограничения, накладываемые на параметр текущего универсального типа.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий объект <see cref="T:System.Type" /> не является параметром универсального типа.
    ///    То есть свойство <see cref="P:System.Type.IsGenericParameter" /> возвращает значение <see langword="false" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual Type[] GetGenericParameterConstraints()
    {
      if (!this.IsGenericParameter)
        throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericParameter"));
      throw new InvalidOperationException();
    }

    /// <summary>
    ///   Возвращает значение, указывающее, передан ли объект <see cref="T:System.Type" /> по ссылке.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> передан по ссылке; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsByRef
    {
      [__DynamicallyInvokable] get
      {
        return this.IsByRefImpl();
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли объект <see cref="T:System.Type" /> указателем.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если <see cref="T:System.Type" /> является указателем; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsPointer
    {
      [__DynamicallyInvokable] get
      {
        return this.IsPointerImpl();
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли <see cref="T:System.Type" /> одним из типов-примитивов.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> является одним из типов-примитивов; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsPrimitive
    {
      [__DynamicallyInvokable] get
      {
        return this.IsPrimitiveImpl();
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли объект <see cref="T:System.Type" /> COM-объектом.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если <see cref="T:System.Type" /> является COM-объектом, в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsCOMObject
    {
      get
      {
        return this.IsCOMObjectImpl();
      }
    }

    internal bool IsWindowsRuntimeObject
    {
      get
      {
        return this.IsWindowsRuntimeObjectImpl();
      }
    }

    internal bool IsExportedToWindowsRuntime
    {
      get
      {
        return this.IsExportedToWindowsRuntimeImpl();
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, содержит ли текущий объект <see cref="T:System.Type" /> в себе другой тип или ссылку на другой тип (иными словами, является ли текущий объект <see cref="T:System.Type" /> массивом, указателем либо параметром или же он передается по ссылке).
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> является массивом, указателем или параметром, переданным по ссылке; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool HasElementType
    {
      [__DynamicallyInvokable] get
      {
        return this.HasElementTypeImpl();
      }
    }

    /// <summary>
    ///   Возвращает значение, позволяющее определить, можно ли поместить в контекст объект <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> может быть помещен в контекст; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsContextful
    {
      get
      {
        return this.IsContextfulImpl();
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, маршалирован ли объект <see cref="T:System.Type" /> по ссылке.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> маршалируется по ссылке; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsMarshalByRef
    {
      get
      {
        return this.IsMarshalByRefImpl();
      }
    }

    internal bool HasProxyAttribute
    {
      get
      {
        return this.HasProxyAttributeImpl();
      }
    }

    /// <summary>
    ///   Реализует свойство <see cref="P:System.Type.IsValueType" /> и определяет, является ли объект <see cref="T:System.Type" /> типом значения (иными словами, не является классом или интерфейсом).
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если тип <see cref="T:System.Type" /> является типом значения; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    protected virtual bool IsValueTypeImpl()
    {
      return this.IsSubclassOf((Type) RuntimeType.ValueType);
    }

    /// <summary>
    ///   При переопределении в производном классе реализует свойство <see cref="P:System.Type.Attributes" /> и возвращает битовую маску, позволяющую определить атрибуты, связанные с объектом <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.TypeAttributes" />, представляющий набор атрибутов объекта <see cref="T:System.Type" />.
    /// </returns>
    protected abstract TypeAttributes GetAttributeFlagsImpl();

    /// <summary>
    ///   При переопределении в производном классе реализует свойство <see cref="P:System.Type.IsArray" /> и определяет, является ли данный объект <see cref="T:System.Type" /> массивом.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если <see cref="T:System.Type" /> является массивом; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    protected abstract bool IsArrayImpl();

    /// <summary>
    ///   При переопределении в производном классе реализует свойство <see cref="P:System.Type.IsByRef" /> и определяет, передается ли данный объект <see cref="T:System.Type" /> по ссылке.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> передан по ссылке; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    protected abstract bool IsByRefImpl();

    /// <summary>
    ///   При переопределении в производном классе реализует свойство <see cref="P:System.Type.IsPointer" /> и определяет, является ли объект <see cref="T:System.Type" /> указателем.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если <see cref="T:System.Type" /> является указателем; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    protected abstract bool IsPointerImpl();

    /// <summary>
    ///   При переопределении в производном классе реализует свойство <see cref="P:System.Type.IsPrimitive" /> и определяет, является ли объект <see cref="T:System.Type" /> одним из типов-примитивов.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> является одним из типов-примитивов; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    protected abstract bool IsPrimitiveImpl();

    /// <summary>
    ///   При переопределении в производном классе реализует свойство <see cref="P:System.Type.IsCOMObject" /> и определяет, является ли объект <see cref="T:System.Type" /> COM-объектом.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если <see cref="T:System.Type" /> является COM-объектом, в противном случае — значение <see langword="false" />.
    /// </returns>
    protected abstract bool IsCOMObjectImpl();

    internal virtual bool IsWindowsRuntimeObjectImpl()
    {
      throw new NotImplementedException();
    }

    internal virtual bool IsExportedToWindowsRuntimeImpl()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Замещает элементы массива типов для параметров определения текущего универсального типа и возвращает объект <see cref="T:System.Type" />, представляющий сконструированный результирующий тип.
    /// </summary>
    /// <param name="typeArguments">
    ///   Массив типов, который должен быть замещен параметрами типа текущего универсального типа.
    /// </param>
    /// <returns>
    ///   <see cref="T:System.Type" /> представляет сконструированный тип, сформированный путем замещения элементов объекта <paramref name="typeArguments" /> параметрами текущего универсального типа.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий тип не представляет определение универсального типа.
    ///    То есть <see cref="P:System.Type.IsGenericTypeDefinition" /> возвращает <see langword="false" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="typeArguments" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Любой элемент <paramref name="typeArguments" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Количество элементов в <paramref name="typeArguments" /> не совпадает с количеством параметров типа в текущем определении универсального типа.
    /// 
    ///   -или-
    /// 
    ///   Элементы <paramref name="typeArguments" /> не соответствуют ограничениям, указанным для соответствующего параметра типа текущего определения универсального типа.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="typeArguments" /> содержит элемент, который является типом указателя (<see cref="P:System.Type.IsPointer" /> возвращает <see langword="true" />), типом доступа по ссылке (<see cref="P:System.Type.IsByRef" /> возвращает <see langword="true" />) или <see cref="T:System.Void" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызванный метод не поддерживается в базовом классе.
    ///    Реализацию должны обеспечивать производные классы.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual Type MakeGenericType(params Type[] typeArguments)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    /// <summary>
    ///   Реализует свойство <see cref="P:System.Type.IsContextful" /> и определяет, можно ли поместить в контекст данный объект <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> может быть помещен в контекст; в противном случае — значение <see langword="false" />.
    /// </returns>
    protected virtual bool IsContextfulImpl()
    {
      return typeof (ContextBoundObject).IsAssignableFrom(this);
    }

    /// <summary>
    ///   Реализует свойство <see cref="P:System.Type.IsMarshalByRef" /> и определяет, маршалируется ли объект <see cref="T:System.Type" /> по ссылке.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> маршалируется по ссылке; в противном случае — значение <see langword="false" />.
    /// </returns>
    protected virtual bool IsMarshalByRefImpl()
    {
      return typeof (MarshalByRefObject).IsAssignableFrom(this);
    }

    internal virtual bool HasProxyAttributeImpl()
    {
      return false;
    }

    /// <summary>
    ///   При переопределении в производном классе возвращает тип <see cref="T:System.Type" /> объекта, на который ссылается данный массив, указатель или ссылка или который инкапсулирован в этих объектах.
    /// </summary>
    /// <returns>
    ///   Тип объекта <see cref="T:System.Type" />, на который ссылается данный массив, указатель или ссылка или который инкапсулирован в этих объектах, или значение <see langword="null" />, если текущий объект <see cref="T:System.Type" /> не является массивом или указателем, не передается по ссылке либо представляет универсальный тип или параметр типа в определении универсального типа или метода.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract Type GetElementType();

    /// <summary>
    ///   Возвращает массив объектов <see cref="T:System.Type" />, которые представляют аргументы закрытого универсального типа или параметры определения универсального типа.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Type" />, которые представляют аргументы универсального типа.
    ///    Возвращает пустой массив, если текущий тип не является универсальным.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызванный метод не поддерживается в базовом классе.
    ///    Реализацию должны обеспечивать производные классы.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual Type[] GetGenericArguments()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    /// <summary>
    ///   Возвращает массив аргументов универсального типа для этого типа.
    /// </summary>
    /// <returns>
    ///   Массив аргументов универсального типа для этого типа.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual Type[] GenericTypeArguments
    {
      [__DynamicallyInvokable] get
      {
        if (this.IsGenericType && !this.IsGenericTypeDefinition)
          return this.GetGenericArguments();
        return Type.EmptyTypes;
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Type" />, представляющий определение универсального типа, на основе которого можно сконструировать текущий универсальный тип.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Type" />, представляющий универсальный тип, на основе которого можно сконструировать текущий тип.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий тип не является универсальным.
    ///     То есть <see cref="P:System.Type.IsGenericType" /> возвращает <see langword="false" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызванный метод не поддерживается в базовом классе.
    ///    Реализацию должны обеспечивать производные классы.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual Type GetGenericTypeDefinition()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    /// <summary>
    ///   При переопределении в производном классе реализует свойство <see cref="P:System.Type.HasElementType" /> и определяет, что содержится в текущем объекте <see cref="T:System.Type" />: непосредственно другой тип или же указывающая на него ссылка (иными словами, является ли текущий объект <see cref="T:System.Type" /> массивом, указателем или параметром или же он передается по ссылке).
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Type" /> является массивом, указателем или параметром, переданным по ссылке; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    protected abstract bool HasElementTypeImpl();

    internal Type GetRootElementType()
    {
      Type type = this;
      while (type.HasElementType)
        type = type.GetElementType();
      return type;
    }

    /// <summary>Возвращает имена членов текущего типа перечисления.</summary>
    /// <returns>Массив, который содержит имена членов перечисления.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Текущий тип не является перечислением.
    /// </exception>
    public virtual string[] GetEnumNames()
    {
      if (!this.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      string[] enumNames;
      Array enumValues;
      this.GetEnumData(out enumNames, out enumValues);
      return enumNames;
    }

    /// <summary>
    ///   Возвращает массив значений констант в текущем типе перечисления.
    /// </summary>
    /// <returns>
    ///   Массив, содержащий значения.
    ///    Элементы массива сортируются по двоичным значениям (то есть значениям без знака) констант перечисления.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Текущий тип не является перечислением.
    /// </exception>
    public virtual Array GetEnumValues()
    {
      if (!this.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      throw new NotImplementedException();
    }

    private Array GetEnumRawConstantValues()
    {
      string[] enumNames;
      Array enumValues;
      this.GetEnumData(out enumNames, out enumValues);
      return enumValues;
    }

    private void GetEnumData(out string[] enumNames, out Array enumValues)
    {
      FieldInfo[] fields = this.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
      object[] objArray = new object[fields.Length];
      string[] strArray = new string[fields.Length];
      for (int index = 0; index < fields.Length; ++index)
      {
        strArray[index] = fields[index].Name;
        objArray[index] = fields[index].GetRawConstantValue();
      }
      IComparer comparer = (IComparer) Comparer.Default;
      for (int index1 = 1; index1 < objArray.Length; ++index1)
      {
        int index2 = index1;
        string str = strArray[index1];
        object y = objArray[index1];
        bool flag = false;
        while (comparer.Compare(objArray[index2 - 1], y) > 0)
        {
          strArray[index2] = strArray[index2 - 1];
          objArray[index2] = objArray[index2 - 1];
          --index2;
          flag = true;
          if (index2 == 0)
            break;
        }
        if (flag)
        {
          strArray[index2] = str;
          objArray[index2] = y;
        }
      }
      enumNames = strArray;
      enumValues = (Array) objArray;
    }

    /// <summary>Возвращает базовый тип текущего типа перечисления.</summary>
    /// <returns>Базовый тип текущего перечисления.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Текущий тип не является перечислением.
    /// 
    ///   -или-
    /// 
    ///   Тип перечисления не является допустимым, так как содержит более одного поля экземпляра.
    /// </exception>
    public virtual Type GetEnumUnderlyingType()
    {
      if (!this.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      FieldInfo[] fields = this.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
      if (fields == null || fields.Length != 1)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidEnum"), "enumType");
      return fields[0].FieldType;
    }

    /// <summary>
    ///   Возвращает значение, показывающее, имеется ли в текущем типе перечисления указанное значение.
    /// </summary>
    /// <param name="value">Проверяемое значение.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанное значение является членом текущего типа перечисления; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Текущий тип не является перечислением.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="value" /> имеет тип, который не может быть базовым типом перечисления.
    /// </exception>
    public virtual bool IsEnumDefined(object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (!this.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      Type t = value.GetType();
      if (t.IsEnum)
      {
        if (!t.IsEquivalentTo(this))
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumAndObjectMustBeSameType", (object) t.ToString(), (object) this.ToString()));
        t = t.GetEnumUnderlyingType();
      }
      if (t == typeof (string))
        return Array.IndexOf<object>((object[]) this.GetEnumNames(), value) >= 0;
      if (Type.IsIntegerType(t))
      {
        Type enumUnderlyingType = this.GetEnumUnderlyingType();
        if (enumUnderlyingType.GetTypeCodeImpl() != t.GetTypeCodeImpl())
          throw new ArgumentException(Environment.GetResourceString("Arg_EnumUnderlyingTypeAndObjectMustBeSameType", (object) t.ToString(), (object) enumUnderlyingType.ToString()));
        return Type.BinarySearch(this.GetEnumRawConstantValues(), value) >= 0;
      }
      if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
        throw new ArgumentException(Environment.GetResourceString("Arg_EnumUnderlyingTypeAndObjectMustBeSameType", (object) t.ToString(), (object) this.GetEnumUnderlyingType()));
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_UnknownEnumType"));
    }

    /// <summary>
    ///   Возвращает имя константы с заданным значением для текущего типа перечисления.
    /// </summary>
    /// <param name="value">
    ///   Значение, имя которой требуется извлечь.
    /// </param>
    /// <returns>
    ///   Имя члена текущего типа перечисления, имеющего указанное значение, или <see langword="null" />, если такая константа не найдена.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Текущий тип не является перечислением.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="value" /> не является ни текущим типом, ни имеющим базовый тип, совпадающий с текущим типом.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    public virtual string GetEnumName(object value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (!this.IsEnum)
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnum"), "enumType");
      Type type = value.GetType();
      if (!type.IsEnum && !Type.IsIntegerType(type))
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeEnumBaseTypeOrEnum"), nameof (value));
      int index = Type.BinarySearch(this.GetEnumRawConstantValues(), value);
      if (index >= 0)
        return this.GetEnumNames()[index];
      return (string) null;
    }

    private static int BinarySearch(Array array, object value)
    {
      ulong[] array1 = new ulong[array.Length];
      for (int index = 0; index < array.Length; ++index)
        array1[index] = Enum.ToUInt64(array.GetValue(index));
      ulong uint64 = Enum.ToUInt64(value);
      return Array.BinarySearch<ulong>(array1, uint64);
    }

    internal static bool IsIntegerType(Type t)
    {
      if (!(t == typeof (int)) && !(t == typeof (short)) && (!(t == typeof (ushort)) && !(t == typeof (byte))) && (!(t == typeof (sbyte)) && !(t == typeof (uint)) && (!(t == typeof (long)) && !(t == typeof (ulong)))) && !(t == typeof (char)))
        return t == typeof (bool);
      return true;
    }

    /// <summary>
    ///   Возвращает значение, которое указывает, является ли текущий тип критически важным для безопасности или защищенным критически важным для безопасности на данном уровне доверия и, следовательно, может ли он выполнять критические операции.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий тип является критически важным для безопасности или защищенным критически важным для безопасности на текущем уровне доверия; значение <see langword="false" />, если он является прозрачным.
    /// </returns>
    public virtual bool IsSecurityCritical
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Возвращает значение, которое указывает, является ли текущий тип защищенным критически важным для безопасности на текущем уровне доверия и, следовательно, может ли он выполнять критические операции и предоставлять доступ прозрачному коду.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий тип является защищенным критически важным для безопасности на текущем уровне доверия; значение <see langword="false" />, если он является критически важным для безопасности или прозрачным.
    /// </returns>
    public virtual bool IsSecuritySafeCritical
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Возвращает значение, которое указывает, является ли текущий тип прозрачным на текущем уровне доверия и, следовательно, не может выполнять критические операции.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий тип является прозрачным на текущем уровне доверия; в противном случае — значение <see langword="false" />.
    /// </returns>
    public virtual bool IsSecurityTransparent
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    internal bool NeedsReflectionSecurityCheck
    {
      get
      {
        if (!this.IsVisible || this.IsSecurityCritical && !this.IsSecuritySafeCritical)
          return true;
        if (this.IsGenericType)
        {
          foreach (Type genericArgument in this.GetGenericArguments())
          {
            if (genericArgument.NeedsReflectionSecurityCheck)
              return true;
          }
        }
        else if (this.IsArray || this.IsPointer)
          return this.GetElementType().NeedsReflectionSecurityCheck;
        return false;
      }
    }

    /// <summary>
    ///   Указывает на тип, предоставляемый средой CLR, представляющей этот тип.
    /// </summary>
    /// <returns>
    ///   Базовый системный тип текущего типа <see cref="T:System.Type" />.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract Type UnderlyingSystemType { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Определяет, является ли текущий <see cref="T:System.Type" /> производным от указанного <see cref="T:System.Type" />.
    /// </summary>
    /// <param name="c">Тип для сравнения с текущим типом.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий объект <see langword="Type" /> является производным от <paramref name="c" />; в противном случае — <see langword="false" />.
    ///    Этот метод также возвращает значение <see langword="false" />, если параметр <paramref name="c" /> и текущий объект <see langword="Type" /> равны.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="c" /> имеет значение <see langword="null" />.
    /// </exception>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public virtual bool IsSubclassOf(Type c)
    {
      Type type = this;
      if (type == c)
        return false;
      for (; type != (Type) null; type = type.BaseType)
      {
        if (type == c)
          return true;
      }
      return false;
    }

    /// <summary>
    ///   Определяет, является ли указанный объект экземпляром текущего типа <see cref="T:System.Type" />.
    /// </summary>
    /// <param name="o">
    ///   Объект, который требуется сравнить с текущим типом.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий объект <see langword="Type" /> входит в иерархию наследования объекта, представленного параметром <paramref name="o" /> или если текущий объект <see langword="Type" /> является интерфейсом, реализуемым параметром <paramref name="o" />.
    ///    Значение <see langword="false" />, если не выполняется ни одно из перечисленных условий, параметр <paramref name="o" /> имеет значение <see langword="null" /> или текущий объект <see langword="Type" /> является открытым универсальным типом (то есть свойство <see cref="P:System.Type.ContainsGenericParameters" /> возвращает значение <see langword="true" />).
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool IsInstanceOfType(object o)
    {
      if (o == null)
        return false;
      return this.IsAssignableFrom(o.GetType());
    }

    /// <summary>
    ///   Определяет, может ли экземпляр указанного типа, который назначен экземпляр текущего типа.
    /// </summary>
    /// <param name="c">Тип для сравнения с текущим типом.</param>
    /// <returns>
    ///   <see langword="true" />, если истинно любое из следующих условий:
    /// 
    ///       Параметр <paramref name="c" /> и текущий экземпляр принадлежат к одному типу.
    /// 
    ///       Параметр <paramref name="c" /> унаследован прямо или косвенно от текущего экземпляра.
    ///       <paramref name="c" /> является производным непосредственно от текущего экземпляра, если он наследует от текущего экземпляра; <paramref name="c" /> является неявно производным от текущего экземпляра, если он наследует из последовательности один или несколько классов, наследующих от текущего экземпляра.
    /// 
    ///       Текущий экземпляр является интерфейсом, который реализуется параметром <paramref name="c" />.
    /// 
    ///       <paramref name="c" /> является параметром универсального типа, а текущий экземпляр представляет одно из ограничений, наложенных на параметр <paramref name="c" />.
    /// 
    ///       В следующем примере текущий экземпляр является <see cref="T:System.Type" /> представляющий <see cref="T:System.IO.Stream" /> класса.
    ///       GenericWithConstraint является универсальным типом, параметр универсального типа должен иметь тип    <see cref="T:System.IO.Stream" />.
    ///        Передача параметра универсального типа для <see cref="M:System.Type.IsAssignableFrom(System.Type)" /> Указывает, что экземпляр параметра универсального типа могут назначаться <see cref="T:System.IO.Stream" /> объекта.
    ///     System.Type.IsAssignableFrom#2
    ///       <paramref name="c" /> представляет тип значения, а текущий экземпляр представляет Nullable&lt;c&gt; (Nullable(Of c) в Visual Basic).
    /// 
    ///   Значение <see langword="false" />, если не выполняется ни одно из этих условий или значение параметра <paramref name="c" /> равно <see langword="null" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool IsAssignableFrom(Type c)
    {
      if (c == (Type) null)
        return false;
      if (this == c)
        return true;
      RuntimeType underlyingSystemType = this.UnderlyingSystemType as RuntimeType;
      if (underlyingSystemType != (RuntimeType) null)
        return underlyingSystemType.IsAssignableFrom(c);
      if (c.IsSubclassOf(this))
        return true;
      if (this.IsInterface)
        return c.ImplementInterface(this);
      if (!this.IsGenericParameter)
        return false;
      foreach (Type parameterConstraint in this.GetGenericParameterConstraints())
      {
        if (!parameterConstraint.IsAssignableFrom(c))
          return false;
      }
      return true;
    }

    /// <summary>
    ///   Определяет, имеют ли два типа модели COM одинаковые удостоверения и могут ли они считаться эквивалентными.
    /// </summary>
    /// <param name="other">
    ///   Тип модели COM, который проверяется на эквивалентность текущему типу.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если типы модели COM эквивалентны; в противном случае — значение <see langword="false" />.
    ///    Этот метод также возвращает значение <see langword="false" />, если один тип находится в сборке, загружаемой для исполнения, а другой — в сборке, загружаемой в контекст, предназначенный только для отражения.
    /// </returns>
    public virtual bool IsEquivalentTo(Type other)
    {
      return this == other;
    }

    internal bool ImplementInterface(Type ifaceType)
    {
      for (Type type = this; type != (Type) null; type = type.BaseType)
      {
        Type[] interfaces = type.GetInterfaces();
        if (interfaces != null)
        {
          for (int index = 0; index < interfaces.Length; ++index)
          {
            if (interfaces[index] == ifaceType || interfaces[index] != (Type) null && interfaces[index].ImplementInterface(ifaceType))
              return true;
          }
        }
      }
      return false;
    }

    internal string FormatTypeName()
    {
      return this.FormatTypeName(false);
    }

    internal virtual string FormatTypeName(bool serialization)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает объект типа <see langword="String" />, представляющий имя текущего объекта <see langword="Type" />.
    /// </summary>
    /// <returns>
    ///   Объект типа <see cref="T:System.String" />, представляющий имя текущего объекта <see cref="T:System.Type" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return "Type: " + this.Name;
    }

    /// <summary>Возвращает типы объектов в указанном массиве.</summary>
    /// <param name="args">
    ///   Массив объектов, типы которых нужно определить.
    /// </param>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Type" />, представляющих типы соответствующих элементов в массиве <paramref name="args" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="args" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один или несколько элементов в <paramref name="args" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Вызываются инициализаторы класса, и по крайней мере один из них создает исключение.
    /// </exception>
    public static Type[] GetTypeArray(object[] args)
    {
      if (args == null)
        throw new ArgumentNullException(nameof (args));
      Type[] typeArray = new Type[args.Length];
      for (int index = 0; index < typeArray.Length; ++index)
      {
        if (args[index] == null)
          throw new ArgumentNullException();
        typeArray[index] = args[index].GetType();
      }
      return typeArray;
    }

    /// <summary>
    ///   Определяет, если базовый системный тип текущего <see cref="T:System.Type" /> объект совпадает с базовым системным типом указанного <see cref="T:System.Object" />.
    /// </summary>
    /// <param name="o">
    ///   Объект, базовый системный тип которого сравнивается с базовым системным типом текущего типа <see cref="T:System.Type" />.
    ///    Для их сравнение было успешным <paramref name="o" /> должен иметь возможность быть привести или преобразовать объект типа   <see cref="T:System.Type" />.
    /// </param>
    /// <returns>
    /// Значение <see langword="true" />, если базовый системный тип параметра <paramref name="o" /> совпадает с базовым системным типом текущего объекта <see cref="T:System.Type" />; в противном случае — значение <see langword="false" />.
    ///  Этот метод также возвращает <see langword="false" /> Если:.
    /// 
    ///     Свойство <paramref name="o" /> имеет значение <see langword="null" />.
    /// 
    ///     <paramref name="o" /> не удается привести или преобразовать <see cref="T:System.Type" /> объекта.
    ///   </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object o)
    {
      if (o == null)
        return false;
      return this.Equals(o as Type);
    }

    /// <summary>
    ///   Позволяет определить, совпадает ли базовый системный тип текущего объекта <see cref="T:System.Type" /> с базовым системным типом указанного объекта <see cref="T:System.Type" />.
    /// </summary>
    /// <param name="o">
    ///   Объект, базовый системный тип которого сравнивается с базовым системным типом текущего типа <see cref="T:System.Type" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если базовый системный тип параметра <paramref name="o" /> совпадает с базовым системным типом текущего объекта <see cref="T:System.Type" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool Equals(Type o)
    {
      if ((object) o == null)
        return false;
      return (object) this.UnderlyingSystemType == (object) o.UnderlyingSystemType;
    }

    /// <summary>
    ///   Определение равенства двух объектов <see cref="T:System.Type" />.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="left" /> и <paramref name="right" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern bool operator ==(Type left, Type right);

    /// <summary>
    ///   Определяет неравенство двух объектов <see cref="T:System.Type" />.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" />, если значения <paramref name="left" /> и <paramref name="right" /> не равны; в противном случае — <see langword="false" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern bool operator !=(Type left, Type right);

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>Хэш-код данного экземпляра.</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      Type underlyingSystemType = this.UnderlyingSystemType;
      if ((object) underlyingSystemType != (object) this)
        return underlyingSystemType.GetHashCode();
      return base.GetHashCode();
    }

    /// <summary>
    ///   Возвращает сопоставление для интерфейса заданного типа.
    /// </summary>
    /// <param name="interfaceType">
    ///   Тип интерфейса, для которого требуется извлечь сопоставление.
    /// </param>
    /// <returns>
    ///   Объект, представляющий сопоставление интерфейса для <paramref name="interfaceType" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Тип <paramref name="interfaceType" /> не реализован с помощью текущего типа.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="interfaceType" /> не ссылается на интерфейс.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="interfaceType" /> является универсальным интерфейсом, а текущий тип является типом массива.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="interfaceType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий тип <see cref="T:System.Type" /> представляет параметр универсального типа, то есть <see cref="P:System.Type.IsGenericParameter" /> имеет значение <see langword="true" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Вызванный метод не поддерживается в базовом классе.
    ///    Реализацию должны обеспечивать производные классы.
    /// </exception>
    [ComVisible(true)]
    public virtual InterfaceMapping GetInterfaceMap(Type interfaceType)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    /// <summary>
    ///   Возвращает текущий <see cref="T:System.Type" />.
    /// </summary>
    /// <returns>
    ///   Текущий контекст <see cref="T:System.Type" />.
    /// </returns>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Инициализатор класса вызывается и создает исключение.
    /// </exception>
    [__DynamicallyInvokable]
    public new Type GetType()
    {
      return base.GetType();
    }

    void _Type.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _Type.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _Type.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _Type.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}

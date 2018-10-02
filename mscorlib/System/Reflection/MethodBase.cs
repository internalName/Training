// Decompiled with JetBrains decompiler
// Type: System.Reflection.MethodBase
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace System.Reflection
{
  /// <summary>Предоставляет сведения о методах и конструкторах.</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_MethodBase))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
  public abstract class MethodBase : MemberInfo, _MethodBase
  {
    /// <summary>
    ///   Возвращает сведения о методе с помощью представления внутренних метаданных метода (дескриптора).
    /// </summary>
    /// <param name="handle">Дескриптор метода.</param>
    /// <returns>
    ///   Объект <see langword="MethodBase" /> со сведениями о методе.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="handle" /> недопустим.
    /// </exception>
    [__DynamicallyInvokable]
    public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle)
    {
      if (handle.IsNullHandle())
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHandle"));
      MethodBase methodBase = RuntimeType.GetMethodBase(handle.GetMethodInfo());
      Type declaringType = methodBase.DeclaringType;
      if (declaringType != (Type) null && declaringType.IsGenericType)
        throw new ArgumentException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_MethodDeclaringTypeGeneric"), (object) methodBase, (object) declaringType.GetGenericTypeDefinition()));
      return methodBase;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Reflection.MethodBase" /> объекта для конструктора или метода, представленного заданным дескриптором для заданного универсального типа.
    /// </summary>
    /// <param name="handle">
    ///   Дескриптор представления внутренних метаданных конструктора или метода.
    /// </param>
    /// <param name="declaringType">
    ///   Дескриптор универсального типа, определяющего конструктор или метод.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodBase" /> объект, представляющий метод или конструктор, определяемое <paramref name="handle" />, в универсальный тип, указанный в <paramref name="declaringType" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="handle" /> недопустим.
    /// </exception>
    [ComVisible(false)]
    [__DynamicallyInvokable]
    public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle, RuntimeTypeHandle declaringType)
    {
      if (handle.IsNullHandle())
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHandle"));
      return RuntimeType.GetMethodBase(declaringType.GetRuntimeType(), handle.GetMethodInfo());
    }

    /// <summary>
    ///   Возвращает объект <see langword="MethodBase" />, представляющий выполняющийся в текущий момент метод.
    /// </summary>
    /// <returns>
    ///   <see cref="M:System.Reflection.MethodBase.GetCurrentMethod" /> — это статический метод, который вызывается из выполняющегося метода и возвращает сведения об этом методе.
    /// 
    ///   Объект <see langword="MethodBase" />, представляющий выполняющийся в текущий момент метод.
    /// </returns>
    /// <exception cref="T:System.Reflection.TargetException">
    ///   Этот элемент был вызван при помощи механизма позднего связывания.
    /// </exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static MethodBase GetCurrentMethod()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return RuntimeMethodInfo.InternalGetCurrentMethod(ref stackMark);
    }

    /// <summary>
    ///   Определение равенства двух объектов <see cref="T:System.Reflection.MethodBase" />.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="left" /> и <paramref name="right" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(MethodBase left, MethodBase right)
    {
      if ((object) left == (object) right)
        return true;
      if ((object) left == null || (object) right == null)
        return false;
      MethodInfo methodInfo1;
      MethodInfo methodInfo2;
      if ((methodInfo1 = left as MethodInfo) != (MethodInfo) null && (methodInfo2 = right as MethodInfo) != (MethodInfo) null)
        return methodInfo1 == methodInfo2;
      ConstructorInfo constructorInfo1;
      ConstructorInfo constructorInfo2;
      if ((constructorInfo1 = left as ConstructorInfo) != (ConstructorInfo) null && (constructorInfo2 = right as ConstructorInfo) != (ConstructorInfo) null)
        return constructorInfo1 == constructorInfo2;
      return false;
    }

    /// <summary>
    ///   Определяет неравенство двух объектов <see cref="T:System.Reflection.MethodBase" />.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" />, если значения <paramref name="left" /> и <paramref name="right" /> не равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(MethodBase left, MethodBase right)
    {
      return !(left == right);
    }

    /// <summary>
    ///   Возвращает значение, показывающее, равен ли экземпляр указанному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с этим экземпляром, или значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значение параметра <paramref name="obj" /> равно типу и значению данного экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      return base.Equals(obj);
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    [SecurityCritical]
    private IntPtr GetMethodDesc()
    {
      return this.MethodHandle.Value;
    }

    internal virtual bool IsDynamicallyInvokable
    {
      [__DynamicallyInvokable] get
      {
        return true;
      }
    }

    internal virtual ParameterInfo[] GetParametersNoCopy()
    {
      return this.GetParameters();
    }

    /// <summary>
    ///   При переопределении в производном классе Возвращает параметры заданного метода или конструктора.
    /// </summary>
    /// <returns>
    ///   Массив объектов типа <see langword="ParameterInfo" /> данных, которые соответствуют подписи метода (или конструктора) отражаются в этом <see langword="MethodBase" /> экземпляр.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract ParameterInfo[] GetParameters();

    /// <summary>
    ///   Возвращает <see cref="T:System.Reflection.MethodImplAttributes" /> флаги, определяющие атрибуты реализации метода.
    /// </summary>
    /// <returns>Флаги реализации метода.</returns>
    [__DynamicallyInvokable]
    public virtual MethodImplAttributes MethodImplementationFlags
    {
      [__DynamicallyInvokable] get
      {
        return this.GetMethodImplementationFlags();
      }
    }

    /// <summary>
    ///   При переопределении в производном классе возвращает новые флаги <see cref="T:System.Reflection.MethodImplAttributes" />.
    /// </summary>
    /// <returns>
    ///   Флаги <see langword="MethodImplAttributes" />.
    /// </returns>
    public abstract MethodImplAttributes GetMethodImplementationFlags();

    /// <summary>
    ///   Получает дескриптор представления внутренних метаданных метода.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.RuntimeMethodHandle" />.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract RuntimeMethodHandle MethodHandle { [__DynamicallyInvokable] get; }

    /// <summary>Возвращает атрибуты, связанные с этим методом.</summary>
    /// <returns>
    ///   Одно из значений <see cref="T:System.Reflection.MethodAttributes" />.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract MethodAttributes Attributes { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   При переопределении в производном классе вызывает отражаемый метод или конструктор с заданными параметрами.
    /// </summary>
    /// <param name="obj">
    ///   Объект, для которого вызывается метод или конструктор.
    ///    Если метод является статическим, данный аргумент учитывается.
    ///    Если конструктор является статическим, этот аргумент должен быть <see langword="null" /> или экземпляр класса, определяющего конструктор.
    /// </param>
    /// <param name="invokeAttr">
    ///   Битовая маска, которая состоит из 0 или более битовых флагов из <see cref="T:System.Reflection.BindingFlags" />.
    ///    Если <paramref name="binder" /> является <see langword="null" />, этот параметр присваивается значение <see cref="F:System.Reflection.BindingFlags.Default" />; таким образом, все передаваемые игнорируется.
    /// </param>
    /// <param name="binder">
    ///   Объект, позволяющий осуществлять привязку, приведение типов аргументов, вызов элементов, а также поиск объектов <see langword="MemberInfo" /> с помощью отражения.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    /// </param>
    /// <param name="parameters">
    ///   Список аргументов для вызываемого метода или конструктора.
    ///    Это массив объектов, в число, порядок и тип как параметры метода или конструктора.
    ///    Если параметры не указаны, это должно быть <see langword="null" />.
    /// 
    ///   Если метод или конструктор, представленный этим экземпляром принимает параметр ByRef, есть специальные атрибуты не требуются для этого параметра для вызова метода или конструктора с использованием этой функции.
    ///    Любой объект этого массива, явно не инициализирован со значением будет содержать значение по умолчанию для данного типа объекта.
    ///    Для элементов ссылочного типа это значение равно <see langword="null" />.
    ///    Для элементов типа значения, это значение равно 0, 0,0 или <see langword="false" />, в зависимости от заданного типа элемента.
    /// </param>
    /// <param name="culture">
    ///   Экземпляр объекта <see langword="CultureInfo" />, используемого для управления приведением типов.
    ///    Если значение этого объекта — <see langword="null" />, для текущего потока используется <see langword="CultureInfo" />.
    ///    (Например, необходимо преобразовывать объект <see langword="String" />, представляющий 1000, в значение <see langword="Double" />, поскольку при разных языках и региональных параметрах 1000 представляется по-разному.)
    /// </param>
    /// <returns>
    /// <see langword="Object" /> Содержащий возвращаемое значение вызванного метода, или <see langword="null" /> для конструктора, или <see langword="null" /> Если метод возвращаемый тип — <see langword="void" />.
    ///  Перед вызовом метода или конструктора, <see langword="Invoke" /> проверяет, если пользователь имеет разрешение на доступ и допустимость параметров.
    /// 
    ///   Элементы <paramref name="parameters" /> представляют параметры массив, объявленный с <see langword="ref" /> или <see langword="out" /> ключевое слово также может быть изменен.
    /// </returns>
    /// <exception cref="T:System.Reflection.TargetException">
    ///   <paramref name="obj" /> Параметр <see langword="null" /> и метод не является статическим.
    /// 
    ///   -или-
    /// 
    ///   Метод не объявлено или унаследовано от класса <paramref name="obj" />.
    /// 
    ///   -или-
    /// 
    ///   Статический конструктор вызывается, и <paramref name="obj" /> не является ни <see langword="null" /> ни экземпляр класса, который объявлен конструктор.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Тип <paramref name="parameters" /> параметр не соответствует подписи метода или конструктора, отраженного этим экземпляром.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetParameterCountException">
    ///   <paramref name="parameters" /> Массив не имеет правильное число аргументов.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Вызванный метод или конструктор вызывает исключение.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет разрешения на выполнение метод или конструктор, представленный текущим экземпляром.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Тип, объявляющий метод является открытым универсальным типом.
    ///    То есть <see cref="P:System.Type.ContainsGenericParameters" /> возвращает <see langword="true" /> для объявляющего типа.
    /// </exception>
    public abstract object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

    /// <summary>
    ///   Возвращает значение, показывающее соглашения о вызовах для этого метода.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Reflection.CallingConventions" /> Для этого метода.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual CallingConventions CallingConvention
    {
      [__DynamicallyInvokable] get
      {
        return CallingConventions.Standard;
      }
    }

    /// <summary>
    ///   Возвращает массив объектов <see cref="T:System.Type" />, которые представляют аргументы универсального метода, относящиеся к типу, или параметры типа определения универсального метода.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Type" />, представляющих аргументы типа, относящиеся к универсальному методу, или параметры типа определения универсального метода.
    ///    Возвращает пустой массив, если текущий метод не является универсальным методом.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий объект <see cref="T:System.Reflection.ConstructorInfo" />.
    ///    Универсальные конструкторы не поддерживаются в платформе .NET Framework версии 2.0.
    ///    Это исключение является поведением по умолчанию, если этот метод не переопределен в производном классе.
    /// </exception>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public virtual Type[] GetGenericArguments()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли метод определения универсального метода.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если текущий <see cref="T:System.Reflection.MethodBase" /> объект представляет определение универсального метода; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool IsGenericMethodDefinition
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, содержит ли универсальный метод неприсвоенные параметры универсального типа.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если текущий <see cref="T:System.Reflection.MethodBase" /> объект представляет универсальный метод, который содержит неприсвоенные параметры универсального типа; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool ContainsGenericParameters
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли метод универсальным.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если текущий <see cref="T:System.Reflection.MethodBase" /> представляет универсальный метод; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual bool IsGenericMethod
    {
      [__DynamicallyInvokable] get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущий метод или конструктор критически важным для безопасности или защищенным критически важным для безопасности на текущем уровне доверия и выполнять критические операции.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если текущий метод или конструктор является критически важным для безопасности или защищенным критически важным для безопасности на текущем уровне доверия; <see langword="false" /> если он является прозрачным.
    /// </returns>
    public virtual bool IsSecurityCritical
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Возвращает значение, которое указывает, является ли текущий метод или конструктор защищенным критически важным для безопасности на текущем уровне доверия; то есть, может ли он выполнять критические операции и доступ прозрачному коду.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если метод или конструктор является защищенным критически важным для безопасности на текущем уровне доверия; <see langword="false" /> если он является критически важным для безопасности или прозрачным.
    /// </returns>
    public virtual bool IsSecuritySafeCritical
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли текущий метод или конструктор прозрачным на текущей доверия уровня и, следовательно, нельзя выполнять критические операции.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если метод или конструктор является прозрачным на текущем уровне доверия; в противном случае — <see langword="false" />.
    /// </returns>
    public virtual bool IsSecurityTransparent
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Вызывает метод или конструктор, представленный текущим экземпляром, используя указанные параметры.
    /// </summary>
    /// <param name="obj">
    ///   Объект, для которого нужно вызвать метод или конструктор.
    ///    Если метод является статическим, этот аргумент игнорируется.
    ///    Если конструктор является статическим, этот аргумент должен иметь значение <see langword="null" /> или представлять экземпляр класса, который определяет конструктор.
    /// </param>
    /// <param name="parameters">
    ///   Список аргументов для вызываемого метода или конструктора.
    ///    Это массив объектов, количество, порядок и тип которых должны соответствовать списку параметров вызываемого метода или конструктора.
    ///    Если параметров нет, для <paramref name="parameters" /> должно быть указано значение <see langword="null" />.
    /// 
    ///   Если метод или конструктор, представленный этим экземпляром, принимает параметр <see langword="ref" /> (<see langword="ByRef" /> в Visual Basic), не требуются никакие специальные атрибуты для вызова этого метода или конструктора с использованием этой функции.
    ///    Любой объект этого массива, которому не присвоено значение явным образом, будет содержать значение по умолчанию для своего типа объекта.
    ///    Для элементов ссылочного типа это значение равно <see langword="null" />.
    ///    Для элементов, хранящих значения, это значение равно 0, 0,0 или <see langword="false" /> (в зависимости от типа конкретного элемента).
    /// </param>
    /// <returns>
    /// Объект, который содержит возвращаемое значение вызываемого метода, или <see langword="null" /> при вызове конструктора.
    /// 
    ///   Элементы массива <paramref name="parameters" />, которые представляют параметры, объявленные с использованием ключевого слова <see langword="ref" /> или <see langword="out" />, также могут быть изменены.
    /// </returns>
    /// <exception cref="T:System.Reflection.TargetException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение <see cref="T:System.Exception" />.
    /// 
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" /> и метод не является статическим.
    /// 
    ///   -или-
    /// 
    ///   Этот метод не объявлен и не унаследован в классе <paramref name="obj" />.
    /// 
    ///   -или-
    /// 
    ///   Вызывается статический конструктор, а <paramref name="obj" /> не имеет значения <see langword="null" /> и не является экземпляром класса, в котором объявлен этот конструктор.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Элементы массива <paramref name="parameters" /> не соответствуют подписи метода или конструктора, которому соответствует этот экземпляр.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Вызванный метод или конструктор создает исключение.
    /// 
    ///   -или-
    /// 
    ///   Текущий экземпляр представляет <see cref="T:System.Reflection.Emit.DynamicMethod" />, который содержит непроверяемый код.
    ///    См. подраздел "Проверка" в разделе примечаний для <see cref="T:System.Reflection.Emit.DynamicMethod" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetParameterCountException">
    ///   Массив <paramref name="parameters" /> содержит неправильное число аргументов.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение базового класса <see cref="T:System.MemberAccessException" />.
    /// 
    ///   Вызывающий объект не имеет разрешение на выполнение метода или конструктора, представленного текущим экземпляром.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Тип, объявляющий метод, является открытым универсальным типом.
    ///    То есть свойство <see cref="P:System.Type.ContainsGenericParameters" /> для объявляющего типа возвращает значение <see langword="true" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий экземпляр представляет <see cref="T:System.Reflection.Emit.MethodBuilder" />.
    /// </exception>
    [DebuggerStepThrough]
    [DebuggerHidden]
    [__DynamicallyInvokable]
    public object Invoke(object obj, object[] parameters)
    {
      return this.Invoke(obj, BindingFlags.Default, (Binder) null, parameters, (CultureInfo) null);
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли открытый метод.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если этот метод является открытым; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsPublic
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли этот член закрытым.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если доступ к этому методу разрешен только элементам данного класса; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsPrivate
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Private;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, было ли видимость этот метод или конструктор описывается <see cref="F:System.Reflection.MethodAttributes.Family" />; метод или конструктор является видимым только в своем классе и производных классов.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если доступ к этому методу или конструктор точно описываемых <see cref="F:System.Reflection.MethodAttributes.Family" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsFamily
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Family;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, определяется ли потенциальные видимость этот метод или конструктор <see cref="F:System.Reflection.MethodAttributes.Assembly" />; то есть, метод или конструктор полностью доступен для других типов в одной сборке и является невидимым для производных типов за пределами сборки.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если этот метод или конструктор видимость точно описываемых <see cref="F:System.Reflection.MethodAttributes.Assembly" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsAssembly
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Assembly;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, было ли видимость этот метод или конструктор описывается <see cref="F:System.Reflection.MethodAttributes.FamANDAssem" />; то есть, метод или конструктор может вызываться в производных классах, но только если они находятся в той же сборке.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если доступ к этому методу или конструктор точно описываемых <see cref="F:System.Reflection.MethodAttributes.FamANDAssem" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsFamilyAndAssembly
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.FamANDAssem;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, определяется ли потенциальные видимость этот метод или конструктор <see cref="F:System.Reflection.MethodAttributes.FamORAssem" />; то есть, метод или конструктор может вызываться производными классами независимо от их типа и классами в той же сборке.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если доступ к этому методу или конструктор точно описываемых <see cref="F:System.Reflection.MethodAttributes.FamORAssem" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsFamilyOrAssembly
    {
      [__DynamicallyInvokable] get
      {
        return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.FamORAssem;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли метод <see langword="static" />.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если этот метод является <see langword="static" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsStatic
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & MethodAttributes.Static) > 0U;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли этот метод <see langword="final" />.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если этот метод является <see langword="final" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsFinal
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & MethodAttributes.Final) > 0U;
      }
    }

    /// <summary>
    ///   Получает значение, указывающее, имеет ли метод значение <see langword="virtual" />.
    /// </summary>
    /// <returns>
    ///   <see langword="true" />, если этот метод имеет значение <see langword="virtual" />. В противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsVirtual
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & MethodAttributes.Virtual) > 0U;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, скрывается ли в производном классе только член такого же вида с точно такой же сигнатурой.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если элемент скрыт на основе подписи; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsHideBySig
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & MethodAttributes.HideBySig) > 0U;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли метод абстрактным.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если метод является абстрактным. в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsAbstract
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & MethodAttributes.Abstract) > 0U;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, имеет ли этот метод специальное имя.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если этот метод имеет специальное имя; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsSpecialName
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & MethodAttributes.SpecialName) > 0U;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли метод конструктором.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если этот метод является конструктором, представленного <see cref="T:System.Reflection.ConstructorInfo" /> объекта (см. Примечание в примечания <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> объекты); в противном случае — <see langword="false" />.
    /// </returns>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public bool IsConstructor
    {
      [__DynamicallyInvokable] get
      {
        if ((object) (this as ConstructorInfo) != null && !this.IsStatic)
          return (this.Attributes & MethodAttributes.RTSpecialName) == MethodAttributes.RTSpecialName;
        return false;
      }
    }

    /// <summary>
    ///   При переопределении в производном классе получает <see cref="T:System.Reflection.MethodBody" /> объект, предоставляющий доступ к потока MSIL, локальные переменные и исключения для текущего метода.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodBody" /> объект, предоставляющий доступ к потока MSIL, локальные переменные и исключения для текущего метода.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот метод является недопустимым, если не переопределено в производном классе.
    /// </exception>
    [SecuritySafeCritical]
    [ReflectionPermission(SecurityAction.Demand, Flags = ReflectionPermissionFlag.MemberAccess)]
    public virtual MethodBody GetMethodBody()
    {
      throw new InvalidOperationException();
    }

    internal static string ConstructParameters(Type[] parameterTypes, CallingConventions callingConvention, bool serialization)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string str1 = "";
      for (int index = 0; index < parameterTypes.Length; ++index)
      {
        Type parameterType = parameterTypes[index];
        stringBuilder.Append(str1);
        string str2 = parameterType.FormatTypeName(serialization);
        if (parameterType.IsByRef && !serialization)
        {
          stringBuilder.Append(str2.TrimEnd('&'));
          stringBuilder.Append(" ByRef");
        }
        else
          stringBuilder.Append(str2);
        str1 = ", ";
      }
      if ((callingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
      {
        stringBuilder.Append(str1);
        stringBuilder.Append("...");
      }
      return stringBuilder.ToString();
    }

    internal string FullName
    {
      get
      {
        return string.Format("{0}.{1}", (object) this.DeclaringType.FullName, (object) this.FormatNameAndSig());
      }
    }

    internal string FormatNameAndSig()
    {
      return this.FormatNameAndSig(false);
    }

    internal virtual string FormatNameAndSig(bool serialization)
    {
      StringBuilder stringBuilder = new StringBuilder(this.Name);
      stringBuilder.Append("(");
      stringBuilder.Append(MethodBase.ConstructParameters(this.GetParameterTypes(), this.CallingConvention, serialization));
      stringBuilder.Append(")");
      return stringBuilder.ToString();
    }

    internal virtual Type[] GetParameterTypes()
    {
      ParameterInfo[] parametersNoCopy = this.GetParametersNoCopy();
      Type[] typeArray = new Type[parametersNoCopy.Length];
      for (int index = 0; index < parametersNoCopy.Length; ++index)
        typeArray[index] = parametersNoCopy[index].ParameterType;
      return typeArray;
    }

    [SecuritySafeCritical]
    internal object[] CheckArguments(object[] parameters, Binder binder, BindingFlags invokeAttr, CultureInfo culture, Signature sig)
    {
      object[] objArray = new object[parameters.Length];
      ParameterInfo[] parameterInfoArray = (ParameterInfo[]) null;
      for (int index = 0; index < parameters.Length; ++index)
      {
        object obj = parameters[index];
        RuntimeType runtimeType = sig.Arguments[index];
        if (obj == Type.Missing)
        {
          if (parameterInfoArray == null)
            parameterInfoArray = this.GetParametersNoCopy();
          if (parameterInfoArray[index].DefaultValue == DBNull.Value)
            throw new ArgumentException(Environment.GetResourceString("Arg_VarMissNull"), nameof (parameters));
          obj = parameterInfoArray[index].DefaultValue;
        }
        objArray[index] = runtimeType.CheckValue(obj, binder, culture, invokeAttr);
      }
      return objArray;
    }

    Type _MethodBase.GetType()
    {
      return this.GetType();
    }

    bool _MethodBase.IsPublic
    {
      get
      {
        return this.IsPublic;
      }
    }

    bool _MethodBase.IsPrivate
    {
      get
      {
        return this.IsPrivate;
      }
    }

    bool _MethodBase.IsFamily
    {
      get
      {
        return this.IsFamily;
      }
    }

    bool _MethodBase.IsAssembly
    {
      get
      {
        return this.IsAssembly;
      }
    }

    bool _MethodBase.IsFamilyAndAssembly
    {
      get
      {
        return this.IsFamilyAndAssembly;
      }
    }

    bool _MethodBase.IsFamilyOrAssembly
    {
      get
      {
        return this.IsFamilyOrAssembly;
      }
    }

    bool _MethodBase.IsStatic
    {
      get
      {
        return this.IsStatic;
      }
    }

    bool _MethodBase.IsFinal
    {
      get
      {
        return this.IsFinal;
      }
    }

    bool _MethodBase.IsVirtual
    {
      get
      {
        return this.IsVirtual;
      }
    }

    bool _MethodBase.IsHideBySig
    {
      get
      {
        return this.IsHideBySig;
      }
    }

    bool _MethodBase.IsAbstract
    {
      get
      {
        return this.IsAbstract;
      }
    }

    bool _MethodBase.IsSpecialName
    {
      get
      {
        return this.IsSpecialName;
      }
    }

    bool _MethodBase.IsConstructor
    {
      get
      {
        return this.IsConstructor;
      }
    }

    void _MethodBase.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _MethodBase.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _MethodBase.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _MethodBase.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}

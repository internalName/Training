// Decompiled with JetBrains decompiler
// Type: System.Delegate
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System
{
  /// <summary>
  ///   Представляет делегат — структуру данных, указывающую на статический метод или на экземпляр класса и метод экземпляра этого класса.
  /// </summary>
  [ClassInterface(ClassInterfaceType.AutoDual)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public abstract class Delegate : ICloneable, ISerializable
  {
    [SecurityCritical]
    internal object _target;
    [SecurityCritical]
    internal object _methodBase;
    [SecurityCritical]
    internal IntPtr _methodPtr;
    [SecurityCritical]
    internal IntPtr _methodPtrAux;

    /// <summary>
    ///   Инициализирует делегат, вызывающий заданный метод экземпляра указанного класса.
    /// </summary>
    /// <param name="target">
    ///   Экземпляр класса, из которого вызывает делегат <paramref name="method" />.
    /// </param>
    /// <param name="method">
    ///   Имя представленного делегатом метода экземпляра.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="target" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="method" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Произошла ошибка связывания целевого метода.
    /// </exception>
    [SecuritySafeCritical]
    protected Delegate(object target, string method)
    {
      if (target == null)
        throw new ArgumentNullException(nameof (target));
      if (method == null)
        throw new ArgumentNullException(nameof (method));
      if (!this.BindToMethodName(target, (RuntimeType) target.GetType(), method, DelegateBindingFlags.InstanceMethodOnly | DelegateBindingFlags.ClosedDelegateOnly))
        throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
    }

    /// <summary>
    ///   Инициализирует делегат, вызывающий заданный статистический метод указанного класса.
    /// </summary>
    /// <param name="target">
    ///   Тип <see cref="T:System.Type" />, представляющий класс, в котором определен метод <paramref name="method" />.
    /// </param>
    /// <param name="method">
    ///   Имя представленного делегатом статического метода.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="target" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="method" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="target" /> не является объектом <see langword="RuntimeType" />.
    ///    См. статью Типы среды выполнения в отражении.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="target" /> представляет открытый универсальный тип.
    /// </exception>
    [SecuritySafeCritical]
    protected Delegate(Type target, string method)
    {
      if (target == (Type) null)
        throw new ArgumentNullException(nameof (target));
      if (target.IsGenericType && target.ContainsGenericParameters)
        throw new ArgumentException(Environment.GetResourceString("Arg_UnboundGenParam"), nameof (target));
      if (method == null)
        throw new ArgumentNullException(nameof (method));
      RuntimeType methodType = target as RuntimeType;
      if (methodType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), nameof (target));
      this.BindToMethodName((object) null, methodType, method, DelegateBindingFlags.StaticMethodOnly | DelegateBindingFlags.OpenDelegateOnly | DelegateBindingFlags.CaselessMatching);
    }

    private Delegate()
    {
    }

    /// <summary>
    ///   Динамически (с поздней привязкой) вызывает метод, представленный текущим делегатом.
    /// </summary>
    /// <param name="args">
    ///   Массив объектов, которые передаются в качестве аргументов методу, представленному текущим делегатом.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" />, если метод, представленный текущим делегатом не требует аргументов.
    /// </param>
    /// <returns>
    ///   Объект, возвращаемый методом, представленным делегатом.
    /// </returns>
    /// <exception cref="T:System.MemberAccessException">
    ///   Вызывающий объект не имеет доступа к методу, представленному делегатом (например, если метод является закрытым).
    /// 
    ///   -или-
    /// 
    ///   Количество, порядок или тип параметров в списке <paramref name="args" /> является недопустимым.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   На объект или класс, который не поддерживает вызывается метод, представленный делегатом.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Представленный делегатом метод является методом экземпляра, а целевой объект — <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один из инкапсулированных методов создает исключение.
    /// </exception>
    [__DynamicallyInvokable]
    public object DynamicInvoke(params object[] args)
    {
      return this.DynamicInvokeImpl(args);
    }

    /// <summary>
    ///   Динамически (с поздней привязкой) вызывает метод, представленный текущим делегатом.
    /// </summary>
    /// <param name="args">
    ///   Массив объектов, которые передаются в качестве аргументов методу, представленному текущим делегатом.
    /// 
    ///   -или-
    /// 
    ///   <see langword="null" />, если метод, представленный текущим делегатом не требует аргументов.
    /// </param>
    /// <returns>
    ///   Объект, возвращаемый методом, представленным делегатом.
    /// </returns>
    /// <exception cref="T:System.MemberAccessException">
    ///   Вызывающий объект не имеет доступа к методу, представленному делегатом (например, если метод является закрытым).
    /// 
    ///   -или-
    /// 
    ///   Количество, порядок или тип параметров в списке <paramref name="args" /> является недопустимым.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   На объект или класс, который не поддерживает вызывается метод, представленный делегатом.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Представленный делегатом метод является методом экземпляра, а целевой объект — <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Один из инкапсулированных методов создает исключение.
    /// </exception>
    [SecuritySafeCritical]
    protected virtual object DynamicInvokeImpl(object[] args)
    {
      return ((RuntimeMethodInfo) RuntimeType.GetMethodBase((RuntimeType) this.GetType(), new RuntimeMethodHandleInternal(this.GetInvokeMethod()))).UnsafeInvoke((object) this, BindingFlags.Default, (Binder) null, args, (CultureInfo) null);
    }

    /// <summary>
    ///   Определяет, принадлежат ли заданный объект и текущий делегат к одному типу, и одинаковы ли их целевые объекты, методы и списки вызовов.
    /// </summary>
    /// <param name="obj">
    ///   Объект, который требуется сравнить с текущим делегатом.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="obj" /> и текущий делегат имеют одинаковые целевые объекты, методы и списки вызовов. В противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.MemberAccessException">
    ///   Вызывающий объект не имеет доступа к методу, представленному делегатом (например, если метод является закрытым).
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (obj == null || !Delegate.InternalEqualTypes((object) this, obj))
        return false;
      Delegate right = (Delegate) obj;
      if (this._target == right._target && this._methodPtr == right._methodPtr && this._methodPtrAux == right._methodPtrAux)
        return true;
      if (this._methodPtrAux.IsNull())
      {
        if (!right._methodPtrAux.IsNull() || this._target != right._target)
          return false;
      }
      else
      {
        if (right._methodPtrAux.IsNull())
          return false;
        if (this._methodPtrAux == right._methodPtrAux)
          return true;
      }
      if (this._methodBase == null || right._methodBase == null || ((object) (this._methodBase as MethodInfo) == null || (object) (right._methodBase as MethodInfo) == null))
        return Delegate.InternalEqualMethodHandles(this, right);
      return this._methodBase.Equals(right._methodBase);
    }

    /// <summary>Возвращает хэш-код делегата.</summary>
    /// <returns>Хэш-код делегата.</returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return this.GetType().GetHashCode();
    }

    /// <summary>Сцепляет списки вызовов двух делегатов.</summary>
    /// <param name="a">
    ///   Делегат, список вызова которого передан первым.
    /// </param>
    /// <param name="b">
    ///   Делегат, список вызова которого передан последним.
    /// </param>
    /// <returns>
    ///   Новый делегат со списком вызова, представляющим собой сцепление списков вызова, заданных в параметрах <paramref name="a" /> и <paramref name="b" /> в указанном порядке.
    ///    Возвращает <paramref name="a" />, если <paramref name="b" /> имеет значение <see langword="null" />; возвращает <paramref name="b" />, если <paramref name="a" /> является пустой ссылкой; возвращает пустую ссылку, если <paramref name="a" /> и <paramref name="b" /> являются пустыми ссылками.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="a" /> и <paramref name="b" /> не имеют значение <see langword="null" />, а <paramref name="a" /> и <paramref name="b" /> не являются экземплярами одного и того же типа делегата.
    /// </exception>
    [__DynamicallyInvokable]
    public static Delegate Combine(Delegate a, Delegate b)
    {
      if ((object) a == null)
        return b;
      return a.CombineImpl(b);
    }

    /// <summary>Сцепляет списки вызовов массива делегатов.</summary>
    /// <param name="delegates">Массив объединяемых делегатов.</param>
    /// <returns>
    ///   Новый делегат со списком вызова, представляющим собой сцепление списков вызова делегатов в массиве <paramref name="delegates" />.
    ///    Возвращает <see langword="null" />, если <paramref name="delegates" /> имеет значение <see langword="null" />, если <paramref name="delegates" /> содержит ноль элементов либо если каждая запись в <paramref name="delegates" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Не все ненулевые записи в <paramref name="delegates" /> являются экземплярами одного и того же типа делегата.
    /// </exception>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public static Delegate Combine(params Delegate[] delegates)
    {
      if (delegates == null || delegates.Length == 0)
        return (Delegate) null;
      Delegate a = delegates[0];
      for (int index = 1; index < delegates.Length; ++index)
        a = Delegate.Combine(a, delegates[index]);
      return a;
    }

    /// <summary>Возвращает список вызовов делегата.</summary>
    /// <returns>
    ///   Массив делегатов, представляющих список вызовов текущего делегата.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual Delegate[] GetInvocationList()
    {
      return new Delegate[1]{ this };
    }

    /// <summary>Возвращает метод, представленный делегатом.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" /> описывающий метод, представленный делегатом.
    /// </returns>
    /// <exception cref="T:System.MemberAccessException">
    ///   Вызывающий объект не имеет доступа к методу, представленному делегатом (например, если метод является закрытым).
    /// </exception>
    [__DynamicallyInvokable]
    public MethodInfo Method
    {
      [__DynamicallyInvokable] get
      {
        return this.GetMethodImpl();
      }
    }

    /// <summary>
    ///   Возвращает статический метод, представленный текущим делегатом.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Reflection.MethodInfo" /> описывающий статический метод, представленный текущим делегатом.
    /// </returns>
    /// <exception cref="T:System.MemberAccessException">
    ///   Вызывающий объект не имеет доступа к методу, представленному делегатом (например, если метод является закрытым).
    /// </exception>
    [SecuritySafeCritical]
    protected virtual MethodInfo GetMethodImpl()
    {
      if (this._methodBase == null || (object) (this._methodBase as MethodInfo) == null)
      {
        IRuntimeMethodInfo methodHandle = this.FindMethodHandle();
        RuntimeType runtimeType = RuntimeMethodHandle.GetDeclaringType(methodHandle);
        if ((RuntimeTypeHandle.IsGenericTypeDefinition(runtimeType) || RuntimeTypeHandle.HasInstantiation(runtimeType)) && (uint) (RuntimeMethodHandle.GetAttributes(methodHandle) & MethodAttributes.Static) <= 0U)
        {
          if (this._methodPtrAux == (IntPtr) 0)
          {
            Type type = this._target.GetType();
            Type genericTypeDefinition = runtimeType.GetGenericTypeDefinition();
            for (; type != (Type) null; type = type.BaseType)
            {
              if (type.IsGenericType && type.GetGenericTypeDefinition() == genericTypeDefinition)
              {
                runtimeType = type as RuntimeType;
                break;
              }
            }
          }
          else
            runtimeType = (RuntimeType) this.GetType().GetMethod("Invoke").GetParameters()[0].ParameterType;
        }
        this._methodBase = (object) (MethodInfo) RuntimeType.GetMethodBase(runtimeType, methodHandle);
      }
      return (MethodInfo) this._methodBase;
    }

    /// <summary>
    ///   Возвращает экземпляр класса, метод которого вызывает текущий делегат.
    /// </summary>
    /// <returns>
    ///   Объект, для которого вызывает текущий делегат метода экземпляра, если делегат представляет метод экземпляра; <see langword="null" /> Если делегат представляет статический метод.
    /// </returns>
    [__DynamicallyInvokable]
    public object Target
    {
      [__DynamicallyInvokable] get
      {
        return this.GetTarget();
      }
    }

    /// <summary>
    ///   Удаляет последнее вхождение списка вызовов делегата из списка вызовов другого делегата.
    /// </summary>
    /// <param name="source">
    ///   Делегат, из которого необходимо удалить список вызовов <paramref name="value" />.
    /// </param>
    /// <param name="value">
    ///   Делегат, представляющий список вызовов необходимо удалить из списка вызовов <paramref name="source" />.
    /// </param>
    /// <returns>
    ///   Новый делегат со списком вызовов, сформированным путем принятия списка вызовов <paramref name="source" /> и удаление последнего вхождения списка вызовов <paramref name="value" />, если список вызовов <paramref name="value" /> найден в списке вызовов <paramref name="source" />.
    ///    Возвращает <paramref name="source" /> Если <paramref name="value" /> — <see langword="null" /> или если список вызовов <paramref name="value" /> не найден в списке вызовов <paramref name="source" />.
    ///    Возвращает пустую ссылку, если список вызовов <paramref name="value" /> равен списку вызовов <paramref name="source" /> или, если <paramref name="source" /> является пустой ссылкой.
    /// </returns>
    /// <exception cref="T:System.MemberAccessException">
    ///   Вызывающий объект не имеет доступа к методу, представленному делегатом (например, если метод является закрытым).
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Типы делегатов не совпадают.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static Delegate Remove(Delegate source, Delegate value)
    {
      if ((object) source == null)
        return (Delegate) null;
      if ((object) value == null)
        return source;
      if (!Delegate.InternalEqualTypes((object) source, (object) value))
        throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTypeMis"));
      return source.RemoveImpl(value);
    }

    /// <summary>
    ///   Удаляет все вхождения списка вызовов одного делегата из списка вызовов другого делегата.
    /// </summary>
    /// <param name="source">
    ///   Делегат, из которого необходимо удалить список вызовов <paramref name="value" />.
    /// </param>
    /// <param name="value">
    ///   Делегат, представляющий список вызовов необходимо удалить из списка вызовов <paramref name="source" />.
    /// </param>
    /// <returns>
    ///   Новый делегат со списком вызовов, сформированным путем принятия списка вызовов <paramref name="source" /> и удаления все вхождения списка вызовов <paramref name="value" />, если список вызовов <paramref name="value" /> найден в списке вызовов <paramref name="source" />.
    ///    Возвращает <paramref name="source" /> Если <paramref name="value" /> — <see langword="null" /> или если список вызовов <paramref name="value" /> не найден в списке вызовов <paramref name="source" />.
    ///    Возвращает пустую ссылку, если список вызовов <paramref name="value" /> равен списку вызовов <paramref name="source" />, если <paramref name="source" /> содержит серию списки вызовов, которые равны список вызовов <paramref name="value" />, или если <paramref name="source" /> является пустой ссылкой.
    /// </returns>
    /// <exception cref="T:System.MemberAccessException">
    ///   Вызывающий объект не имеет доступа к методу, представленному делегатом (например, если метод является закрытым).
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Типы делегатов не совпадают.
    /// </exception>
    [__DynamicallyInvokable]
    public static Delegate RemoveAll(Delegate source, Delegate value)
    {
      Delegate @delegate;
      do
      {
        @delegate = source;
        source = Delegate.Remove(source, value);
      }
      while (@delegate != source);
      return @delegate;
    }

    /// <summary>
    ///   Сцепляет списки вызовов заданного группового (комбинируемого) делегата и текущего группового (комбинируемого) делегата.
    /// </summary>
    /// <param name="d">
    ///   Групповых (комбинируемых) делегат, чей список вызовов необходимо добавить в конец списка вызовов текущего группового (комбинируемого) делегата.
    /// </param>
    /// <returns>
    ///   Новый групповых (комбинируемых) делегат со списком вызова, объединяет список вызовов текущего группового (комбинируемого) делегата и список вызовов <paramref name="d" />, или текущего группового (комбинируемого) делегата, если <paramref name="d" /> — <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.MulticastNotSupportedException">
    ///   Создается всегда.
    /// </exception>
    protected virtual Delegate CombineImpl(Delegate d)
    {
      throw new MulticastNotSupportedException(Environment.GetResourceString("Multicast_Combine"));
    }

    /// <summary>
    ///   Удаляет список вызовов одного делегата из списка вызовов другого делегата.
    /// </summary>
    /// <param name="d">
    ///   Делегат, представляющий список вызовов, который необходимо удалить из списка вызовов текущего делегата.
    /// </param>
    /// <returns>
    ///   Новый делегат со списком вызовов, сформированным путем принятия списка вызовов текущего делегата и удаления списка вызовов <paramref name="value" />, если список вызовов <paramref name="value" /> найден в списке вызовов текущего делегата.
    ///    Возвращает текущий делегат, если <paramref name="value" /> является <see langword="null" /> или если список вызовов <paramref name="value" /> не найден в списке вызовов текущего делегата.
    ///    Возвращает <see langword="null" /> Если список вызовов <paramref name="value" /> равен списку вызовов текущего делегата.
    /// </returns>
    /// <exception cref="T:System.MemberAccessException">
    ///   Вызывающий объект не имеет доступа к методу, представленному делегатом (например, если метод является закрытым).
    /// </exception>
    protected virtual Delegate RemoveImpl(Delegate d)
    {
      if (!d.Equals((object) this))
        return this;
      return (Delegate) null;
    }

    /// <summary>Создает неполную копию делегата.</summary>
    /// <returns>Неполная копия делегата.</returns>
    public virtual object Clone()
    {
      return this.MemberwiseClone();
    }

    /// <summary>
    ///   Создает делегат указанного типа, представляющий заданный метод экземпляра, который вызывается для заданного экземпляра класса.
    /// </summary>
    /// <param name="type">
    ///   Тип <see cref="T:System.Type" /> делегата для создания.
    /// </param>
    /// <param name="target">
    ///   Экземпляр класса, для которого вызывается метод <paramref name="method" />.
    /// </param>
    /// <param name="method">
    ///   Имя метода экземпляра, который должен быть представлен делегатом.
    /// </param>
    /// <returns>
    ///   Создает делегат указанного типа, представляющий заданный метод экземпляра, который вызывается для заданного экземпляра класса.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="type" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="target" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="method" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" /> не является производным от <see cref="T:System.MulticastDelegate" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="type" /> не является объектом <see langword="RuntimeType" />.
    ///    В разделе типы среды выполнения в отражении.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="method" /> не является методом экземпляра.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="method" /> невозможно привязать, например, потому, что его не удалось найти.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Метод <see langword="Invoke" /> для <paramref name="type" /> не найден.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет необходимых разрешений для доступа к <paramref name="method" />.
    /// </exception>
    public static Delegate CreateDelegate(Type type, object target, string method)
    {
      return Delegate.CreateDelegate(type, target, method, false, true);
    }

    /// <summary>
    ///   Создает делегат указанного типа, представляющий заданный метод экземпляра, который вызывается из заданного экземпляра класса с заданной установкой учета регистра.
    /// </summary>
    /// <param name="type">
    ///   Тип <see cref="T:System.Type" /> делегата для создания.
    /// </param>
    /// <param name="target">
    ///   Экземпляр класса, для которого вызывается метод <paramref name="method" />.
    /// </param>
    /// <param name="method">
    ///   Имя метода экземпляра, который должен быть представлен делегатом.
    /// </param>
    /// <param name="ignoreCase">
    ///   Логическое значение, указывающее, следует ли учитывать регистр при сравнении имени метода.
    /// </param>
    /// <returns>
    ///   Создает делегат указанного типа, представляющий заданный метод экземпляра, который вызывается для заданного экземпляра класса.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="type" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="target" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="method" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" /> не является производным от <see cref="T:System.MulticastDelegate" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="type" /> не является объектом <see langword="RuntimeType" />.
    ///    В разделе типы среды выполнения в отражении.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="method" /> не является методом экземпляра.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="method" /> невозможно привязать, например, потому, что его не удалось найти.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Метод <see langword="Invoke" /> для <paramref name="type" /> не найден.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет необходимых разрешений для доступа к <paramref name="method" />.
    /// </exception>
    public static Delegate CreateDelegate(Type type, object target, string method, bool ignoreCase)
    {
      return Delegate.CreateDelegate(type, target, method, ignoreCase, true);
    }

    /// <summary>
    ///   Создает делегат указанного типа, представляющий заданный статический метод, вызываемый для заданного экземпляра класса с заданной установкой учета регистра и заданным поведением на случай, если операция связывания завершится неудачей.
    /// </summary>
    /// <param name="type">
    ///   Тип <see cref="T:System.Type" /> создаваемого делегата.
    /// </param>
    /// <param name="target">
    ///   Экземпляр класса, для которого вызывается метод <paramref name="method" />.
    /// </param>
    /// <param name="method">
    ///   Имя метода экземпляра, который должен быть представлен делегатом.
    /// </param>
    /// <param name="ignoreCase">
    ///   Логическое значение, указывающее, следует ли учитывать регистр при сравнении имени метода.
    /// </param>
    /// <param name="throwOnBindFailure">
    ///   Значение <see langword="true" /> для создания исключения, если метод <paramref name="method" /> невозможно привязать; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Создает делегат указанного типа, представляющий заданный метод экземпляра, который вызывается для заданного экземпляра класса.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="type" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="target" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="method" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" /> не является производным от <see cref="T:System.MulticastDelegate" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="type" /> не является объектом <see langword="RuntimeType" />.
    ///    В разделе типы среды выполнения в отражении.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="method" /> не является методом экземпляра.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="method" /> невозможно привязать, например, потому, что его невозможно найти, а <paramref name="throwOnBindFailure" /> имеет значение <see langword="true" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Метод <see langword="Invoke" /> для <paramref name="type" /> не найден.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет необходимых разрешений для доступа к <paramref name="method" />.
    /// </exception>
    [SecuritySafeCritical]
    public static Delegate CreateDelegate(Type type, object target, string method, bool ignoreCase, bool throwOnBindFailure)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (target == null)
        throw new ArgumentNullException(nameof (target));
      if (method == null)
        throw new ArgumentNullException(nameof (method));
      RuntimeType type1 = type as RuntimeType;
      if (type1 == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), nameof (type));
      if (!type1.IsDelegate())
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), nameof (type));
      Delegate @delegate = (Delegate) Delegate.InternalAlloc(type1);
      if (!@delegate.BindToMethodName(target, (RuntimeType) target.GetType(), method, (DelegateBindingFlags) (26 | (ignoreCase ? 32 : 0))))
      {
        if (throwOnBindFailure)
          throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
        @delegate = (Delegate) null;
      }
      return @delegate;
    }

    /// <summary>
    ///   Создает делегат указанного типа, представляющий заданный статический метод заданного класса.
    /// </summary>
    /// <param name="type">
    ///   Тип <see cref="T:System.Type" /> создаваемого делегата.
    /// </param>
    /// <param name="target">
    ///   Тип <see cref="T:System.Type" />, представляющий класс, в котором реализован метод <paramref name="method" />.
    /// </param>
    /// <param name="method">
    ///   Имя статического метода, который должен быть представлен делегатом.
    /// </param>
    /// <returns>
    ///   Делегат указанного типа, представляющий заданный статический метод заданного класса.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="type" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="target" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="method" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" /> не является производным от <see cref="T:System.MulticastDelegate" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="type" /> не является объектом <see langword="RuntimeType" />.
    ///    В разделе типы среды выполнения в отражении.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="target" /> не является объектом <see langword="RuntimeType" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="target" /> представляет собой открытый универсальный тип.
    ///    То есть свойство <see cref="P:System.Type.ContainsGenericParameters" /> имеет значение <see langword="true" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="method" /> не является методом <see langword="static" /> (метод <see langword="Shared" /> в Visual Basic).
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="method" /> невозможно привязать, например, потому, что его невозможно найти, а <paramref name="throwOnBindFailure" /> имеет значение <see langword="true" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Метод <see langword="Invoke" /> для <paramref name="type" /> не найден.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет необходимых разрешений для доступа к <paramref name="method" />.
    /// </exception>
    public static Delegate CreateDelegate(Type type, Type target, string method)
    {
      return Delegate.CreateDelegate(type, target, method, false, true);
    }

    /// <summary>
    ///   Создает делегат указанного типа, представляющий заданный статический метод заданного класса с заданной установкой учета регистра.
    /// </summary>
    /// <param name="type">
    ///   Тип <see cref="T:System.Type" /> создаваемого делегата.
    /// </param>
    /// <param name="target">
    ///   Тип <see cref="T:System.Type" />, представляющий класс, в котором реализован метод <paramref name="method" />.
    /// </param>
    /// <param name="method">
    ///   Имя статического метода, который должен быть представлен делегатом.
    /// </param>
    /// <param name="ignoreCase">
    ///   Логическое значение, указывающее, следует ли учитывать регистр при сравнении имени метода.
    /// </param>
    /// <returns>
    ///   Делегат указанного типа, представляющий заданный статический метод заданного класса.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="type" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="target" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="method" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" /> не является производным от <see cref="T:System.MulticastDelegate" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="type" /> не является объектом <see langword="RuntimeType" />.
    ///    В разделе типы среды выполнения в отражении.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="target" /> не является объектом <see langword="RuntimeType" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="target" /> представляет собой открытый универсальный тип.
    ///    То есть свойство <see cref="P:System.Type.ContainsGenericParameters" /> имеет значение <see langword="true" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="method" /> не является методом <see langword="static" /> (метод <see langword="Shared" /> в Visual Basic).
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="method" /> невозможно привязать, например, потому, что его не удалось найти.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Метод <see langword="Invoke" /> для <paramref name="type" /> не найден.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет необходимых разрешений для доступа к <paramref name="method" />.
    /// </exception>
    public static Delegate CreateDelegate(Type type, Type target, string method, bool ignoreCase)
    {
      return Delegate.CreateDelegate(type, target, method, ignoreCase, true);
    }

    /// <summary>
    ///   Создает делегат заданного типа, представляющий заданный статический метод заданного класса с заданными установками учета регистра и поведением на случай, если операция связывания завершится неудачей.
    /// </summary>
    /// <param name="type">
    ///   Тип <see cref="T:System.Type" /> создаваемого делегата.
    /// </param>
    /// <param name="target">
    ///   Тип <see cref="T:System.Type" />, представляющий класс, в котором реализован метод <paramref name="method" />.
    /// </param>
    /// <param name="method">
    ///   Имя статического метода, который должен быть представлен делегатом.
    /// </param>
    /// <param name="ignoreCase">
    ///   Логическое значение, указывающее, следует ли учитывать регистр при сравнении имени метода.
    /// </param>
    /// <param name="throwOnBindFailure">
    ///   Значение <see langword="true" /> для создания исключения, если метод <paramref name="method" /> невозможно привязать; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Делегат указанного типа, представляющий заданный статический метод заданного класса.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="type" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="target" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="method" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" /> не является производным от <see cref="T:System.MulticastDelegate" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="type" /> не является объектом <see langword="RuntimeType" />.
    ///    В разделе типы среды выполнения в отражении.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="target" /> не является объектом <see langword="RuntimeType" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="target" /> представляет собой открытый универсальный тип.
    ///    То есть свойство <see cref="P:System.Type.ContainsGenericParameters" /> имеет значение <see langword="true" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="method" /> не является методом <see langword="static" /> (метод <see langword="Shared" /> в Visual Basic).
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="method" /> невозможно привязать, например, потому, что его невозможно найти, а <paramref name="throwOnBindFailure" /> имеет значение <see langword="true" />.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Метод <see langword="Invoke" /> для <paramref name="type" /> не найден.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет необходимых разрешений для доступа к <paramref name="method" />.
    /// </exception>
    [SecuritySafeCritical]
    public static Delegate CreateDelegate(Type type, Type target, string method, bool ignoreCase, bool throwOnBindFailure)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (target == (Type) null)
        throw new ArgumentNullException(nameof (target));
      if (target.IsGenericType && target.ContainsGenericParameters)
        throw new ArgumentException(Environment.GetResourceString("Arg_UnboundGenParam"), nameof (target));
      if (method == null)
        throw new ArgumentNullException(nameof (method));
      RuntimeType type1 = type as RuntimeType;
      RuntimeType methodType = target as RuntimeType;
      if (type1 == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), nameof (type));
      if (methodType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), nameof (target));
      if (!type1.IsDelegate())
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), nameof (type));
      Delegate @delegate = (Delegate) Delegate.InternalAlloc(type1);
      if (!@delegate.BindToMethodName((object) null, methodType, method, (DelegateBindingFlags) (5 | (ignoreCase ? 32 : 0))))
      {
        if (throwOnBindFailure)
          throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
        @delegate = (Delegate) null;
      }
      return @delegate;
    }

    /// <summary>
    ///   Создает делегат указанного типа, представляющий заданный статический метод, с заданным поведением на случай, если операция связывания завершится неудачей.
    /// </summary>
    /// <param name="type">
    ///   Тип <see cref="T:System.Type" /> создаваемого делегата.
    /// </param>
    /// <param name="method">
    ///   Объект <see cref="T:System.Reflection.MethodInfo" />, описывающий статический метод или метод экземпляра, который будет представлен делегатом.
    /// </param>
    /// <param name="throwOnBindFailure">
    ///   Значение <see langword="true" /> для создания исключения, если метод <paramref name="method" /> невозможно привязать; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Делегат указанного типа, представляющий заданный статический метод.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="type" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="method" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" /> не является производным от <see cref="T:System.MulticastDelegate" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="type" /> не является объектом <see langword="RuntimeType" />.
    ///    В разделе типы среды выполнения в отражении.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="method" /> невозможно привязать, и <paramref name="throwOnBindFailure" /> имеет значение <see langword="true" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="method" /> не является объектом <see langword="RuntimeMethodInfo" />.
    ///    В разделе типы среды выполнения в отражении.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Метод <see langword="Invoke" /> для <paramref name="type" /> не найден.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет необходимых разрешений для доступа к <paramref name="method" />.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Delegate CreateDelegate(Type type, MethodInfo method, bool throwOnBindFailure)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (method == (MethodInfo) null)
        throw new ArgumentNullException(nameof (method));
      RuntimeType rtType = type as RuntimeType;
      if (rtType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), nameof (type));
      RuntimeMethodInfo rtMethod = method as RuntimeMethodInfo;
      if ((MethodInfo) rtMethod == (MethodInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), nameof (method));
      if (!rtType.IsDelegate())
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), nameof (type));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Delegate delegateInternal = Delegate.CreateDelegateInternal(rtType, rtMethod, (object) null, DelegateBindingFlags.OpenDelegateOnly | DelegateBindingFlags.RelaxedSignature, ref stackMark);
      if ((object) delegateInternal == null & throwOnBindFailure)
        throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
      return delegateInternal;
    }

    /// <summary>
    ///   Создает делегат указанного типа, представляющий заданный статический метод или метод экземпляра, с заданным первым аргументом.
    /// </summary>
    /// <param name="type">
    ///   Тип <see cref="T:System.Type" /> делегата для создания.
    /// </param>
    /// <param name="firstArgument">
    ///   Объект, с которым связан делегат, или значение <see langword="null" />, чтобы обработать <paramref name="method" /> как <see langword="static" /> (<see langword="Shared" /> в Visual Basic).
    /// </param>
    /// <param name="method">
    ///   Объект <see cref="T:System.Reflection.MethodInfo" />, описывающий статический метод или метод экземпляра, который будет представлен делегатом.
    /// </param>
    /// <returns>
    ///   Делегат указанного типа, представляющий заданный статический метод или метод экземпляра.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="type" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="method" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" /> не является производным от <see cref="T:System.MulticastDelegate" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="type" /> не является объектом <see langword="RuntimeType" />.
    ///    В разделе типы среды выполнения в отражении.
    /// 
    ///   -или-
    /// 
    ///   Метод <paramref name="method" /> невозможно привязать.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="method" /> не является объектом <see langword="RuntimeMethodInfo" />.
    ///    В разделе типы среды выполнения в отражении.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Метод <see langword="Invoke" /> для <paramref name="type" /> не найден.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет необходимых разрешений для доступа к <paramref name="method" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Delegate CreateDelegate(Type type, object firstArgument, MethodInfo method)
    {
      return Delegate.CreateDelegate(type, firstArgument, method, true);
    }

    /// <summary>
    ///   Создает делегат указанного типа, представляющий заданный статический метод или метод экземпляра, с заданным первым аргументом и поведением на случай, если операция связывания завершится неудачей.
    /// </summary>
    /// <param name="type">
    ///   Объект <see cref="T:System.Type" />, представляющий тип создаваемого делегата.
    /// </param>
    /// <param name="firstArgument">
    ///   Объект <see cref="T:System.Object" />, являющийся первым аргументом метода, представленного делегатом.
    ///    Для методов экземпляра он должен быть совместим с типом экземпляра.
    /// </param>
    /// <param name="method">
    ///   Объект <see cref="T:System.Reflection.MethodInfo" />, описывающий статический метод или метод экземпляра, который будет представлен делегатом.
    /// </param>
    /// <param name="throwOnBindFailure">
    ///   Значение <see langword="true" /> для создания исключения, если метод <paramref name="method" /> невозможно привязать; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Делегат заданного типа, представляющий указанный статический метод или метод экземпляра, либо значение <see langword="null" />, если значение <paramref name="throwOnBindFailure" /> равно <see langword="false" /> и делегат нельзя связать с методом <paramref name="method" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="type" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="method" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" /> не является производным от <see cref="T:System.MulticastDelegate" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="type" /> не является объектом <see langword="RuntimeType" />.
    ///    В разделе типы среды выполнения в отражении.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="method" /> невозможно привязать, и <paramref name="throwOnBindFailure" /> имеет значение <see langword="true" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="method" /> не является объектом <see langword="RuntimeMethodInfo" />.
    ///    В разделе типы среды выполнения в отражении.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Метод <see langword="Invoke" /> для <paramref name="type" /> не найден.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет необходимых разрешений для доступа к <paramref name="method" />.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static Delegate CreateDelegate(Type type, object firstArgument, MethodInfo method, bool throwOnBindFailure)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (method == (MethodInfo) null)
        throw new ArgumentNullException(nameof (method));
      RuntimeType rtType = type as RuntimeType;
      if (rtType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), nameof (type));
      RuntimeMethodInfo rtMethod = method as RuntimeMethodInfo;
      if ((MethodInfo) rtMethod == (MethodInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), nameof (method));
      if (!rtType.IsDelegate())
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), nameof (type));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      Delegate delegateInternal = Delegate.CreateDelegateInternal(rtType, rtMethod, firstArgument, DelegateBindingFlags.RelaxedSignature, ref stackMark);
      if ((object) delegateInternal == null & throwOnBindFailure)
        throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
      return delegateInternal;
    }

    /// <summary>Определяет, равны ли два заданных делегата.</summary>
    /// <param name="d1">Первый делегат для операции сравнения.</param>
    /// <param name="d2">Второй делегат для операции сравнения.</param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="d1" /> и <paramref name="d2" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(Delegate d1, Delegate d2)
    {
      if ((object) d1 == null)
        return (object) d2 == null;
      return d1.Equals((object) d2);
    }

    /// <summary>
    ///   Определяет, являются ли заданные делегаты неравными.
    /// </summary>
    /// <param name="d1">Первый делегат для операции сравнения.</param>
    /// <param name="d2">Второй делегат для операции сравнения.</param>
    /// <returns>
    ///   <see langword="true" />, если значения <paramref name="d1" /> и <paramref name="d2" /> не равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(Delegate d1, Delegate d2)
    {
      if ((object) d1 == null)
        return d2 != null;
      return !d1.Equals((object) d2);
    }

    /// <summary>Не поддерживается.</summary>
    /// <param name="info">Не поддерживается.</param>
    /// <param name="context">Не поддерживается.</param>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается.
    /// </exception>
    [SecurityCritical]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      throw new NotSupportedException();
    }

    [SecurityCritical]
    internal static Delegate CreateDelegateNoSecurityCheck(Type type, object target, RuntimeMethodHandle method)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (method.IsNullHandle())
        throw new ArgumentNullException(nameof (method));
      RuntimeType type1 = type as RuntimeType;
      if (type1 == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), nameof (type));
      if (!type1.IsDelegate())
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), nameof (type));
      Delegate @delegate = (Delegate) Delegate.InternalAlloc(type1);
      if (!@delegate.BindToMethodInfo(target, method.GetMethodInfo(), RuntimeMethodHandle.GetDeclaringType(method.GetMethodInfo()), DelegateBindingFlags.SkipSecurityChecks | DelegateBindingFlags.RelaxedSignature))
        throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
      return @delegate;
    }

    [SecurityCritical]
    internal static Delegate CreateDelegateNoSecurityCheck(RuntimeType type, object firstArgument, MethodInfo method)
    {
      if (type == (RuntimeType) null)
        throw new ArgumentNullException(nameof (type));
      if (method == (MethodInfo) null)
        throw new ArgumentNullException(nameof (method));
      RuntimeMethodInfo rtMethod = method as RuntimeMethodInfo;
      if ((MethodInfo) rtMethod == (MethodInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), nameof (method));
      if (!type.IsDelegate())
        throw new ArgumentException(Environment.GetResourceString("Arg_MustBeDelegate"), nameof (type));
      Delegate @delegate = Delegate.UnsafeCreateDelegate(type, rtMethod, firstArgument, DelegateBindingFlags.SkipSecurityChecks | DelegateBindingFlags.RelaxedSignature);
      if ((object) @delegate == null)
        throw new ArgumentException(Environment.GetResourceString("Arg_DlgtTargMeth"));
      return @delegate;
    }

    /// <summary>
    ///   Создает делегат указанного типа, представляющий заданный статический метод.
    /// </summary>
    /// <param name="type">
    ///   Тип <see cref="T:System.Type" /> создаваемого делегата.
    /// </param>
    /// <param name="method">
    ///   Объект <see cref="T:System.Reflection.MethodInfo" />, описывающий статический метод или метод экземпляра, который будет представлен делегатом.
    ///    На платформе .NET Framework версий 1.0 и 1.1 поддерживаются только статические методы.
    /// </param>
    /// <returns>
    ///   Делегат указанного типа, представляющий заданный статический метод.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="type" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="method" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" /> не является производным от <see cref="T:System.MulticastDelegate" />.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="type" /> не является объектом <see langword="RuntimeType" />.
    ///    В разделе типы среды выполнения в отражении.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="method" /> не является статическим методом, а версия платформы .NET Framework имеет значение 1.0 или 1.1.
    /// 
    ///   -или-
    /// 
    ///   Метод <paramref name="method" /> невозможно привязать.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="method" /> не является объектом <see langword="RuntimeMethodInfo" />.
    ///    В разделе типы среды выполнения в отражении.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Метод <see langword="Invoke" /> для <paramref name="type" /> не найден.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Вызывающий объект не имеет необходимых разрешений для доступа к <paramref name="method" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Delegate CreateDelegate(Type type, MethodInfo method)
    {
      return Delegate.CreateDelegate(type, method, true);
    }

    [SecuritySafeCritical]
    internal static Delegate CreateDelegateInternal(RuntimeType rtType, RuntimeMethodInfo rtMethod, object firstArgument, DelegateBindingFlags flags, ref StackCrawlMark stackMark)
    {
      bool flag1 = (rtMethod.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) > INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN;
      bool flag2 = (rtType.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) > INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN;
      if (flag1 | flag2)
      {
        RuntimeAssembly executingAssembly = RuntimeAssembly.GetExecutingAssembly(ref stackMark);
        if ((Assembly) executingAssembly != (Assembly) null && !executingAssembly.IsSafeForReflection())
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", flag1 ? (object) rtMethod.FullName : (object) rtType.FullName));
      }
      return Delegate.UnsafeCreateDelegate(rtType, rtMethod, firstArgument, flags);
    }

    [SecurityCritical]
    internal static Delegate UnsafeCreateDelegate(RuntimeType rtType, RuntimeMethodInfo rtMethod, object firstArgument, DelegateBindingFlags flags)
    {
      Delegate @delegate = (Delegate) Delegate.InternalAlloc(rtType);
      if (@delegate.BindToMethodInfo(firstArgument, (IRuntimeMethodInfo) rtMethod, rtMethod.GetDeclaringTypeInternal(), flags))
        return @delegate;
      return (Delegate) null;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern bool BindToMethodName(object target, RuntimeType methodType, string method, DelegateBindingFlags flags);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern bool BindToMethodInfo(object target, IRuntimeMethodInfo method, RuntimeType methodType, DelegateBindingFlags flags);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern MulticastDelegate InternalAlloc(RuntimeType type);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern MulticastDelegate InternalAllocLike(Delegate d);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool InternalEqualTypes(object a, object b);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern void DelegateConstruct(object target, IntPtr slot);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern IntPtr GetMulticastInvoke();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern IntPtr GetInvokeMethod();

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern IRuntimeMethodInfo FindMethodHandle();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool InternalEqualMethodHandles(Delegate left, Delegate right);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern IntPtr AdjustTarget(object target, IntPtr methodPtr);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern IntPtr GetCallStub(IntPtr methodPtr);

    [SecuritySafeCritical]
    internal virtual object GetTarget()
    {
      if (!this._methodPtrAux.IsNull())
        return (object) null;
      return this._target;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool CompareUnmanagedFunctionPtrs(Delegate d1, Delegate d2);
  }
}

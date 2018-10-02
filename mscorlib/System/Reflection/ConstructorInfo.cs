// Decompiled with JetBrains decompiler
// Type: System.Reflection.ConstructorInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection
{
  /// <summary>
  ///   Обнаруживает атрибуты конструктора класса и предоставляет доступ к метаданным конструктора.
  /// </summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_ConstructorInfo))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
  public abstract class ConstructorInfo : MethodBase, _ConstructorInfo
  {
    /// <summary>
    ///   Представляет имя метода-конструктора класса, как хранится в метаданных.
    ///    Это имя всегда является «.ctor».
    ///    Это поле доступно только для чтения.
    /// </summary>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public static readonly string ConstructorName = ".ctor";
    /// <summary>
    ///   Представляет имя метода конструктора типа, хранящегося в метаданных.
    ///    Это имя всегда является «.cctor».
    ///    Это свойство доступно только для чтения.
    /// </summary>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public static readonly string TypeConstructorName = ".cctor";

    /// <summary>
    ///   Определение равенства двух объектов <see cref="T:System.Reflection.ConstructorInfo" />.
    /// </summary>
    /// <param name="left">
    ///   Первый экземпляр <see cref="T:System.Reflection.ConstructorInfo" /> для сравнения.
    /// </param>
    /// <param name="right">
    ///   Второй экземпляр <see cref="T:System.Reflection.ConstructorInfo" /> для сравнения.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="left" /> равен <paramref name="right" />; в противном случае <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(ConstructorInfo left, ConstructorInfo right)
    {
      if ((object) left == (object) right)
        return true;
      if ((object) left == null || (object) right == null || (left is RuntimeConstructorInfo || right is RuntimeConstructorInfo))
        return false;
      return left.Equals((object) right);
    }

    /// <summary>
    ///   Определяет неравенство двух объектов <see cref="T:System.Reflection.ConstructorInfo" />.
    /// </summary>
    /// <param name="left">
    ///   Первый экземпляр <see cref="T:System.Reflection.ConstructorInfo" /> для сравнения.
    /// </param>
    /// <param name="right">
    ///   Второй экземпляр <see cref="T:System.Reflection.ConstructorInfo" /> для сравнения.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="left" /> не равно <paramref name="right" />; в противном случае <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(ConstructorInfo left, ConstructorInfo right)
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

    internal virtual Type GetReturnType()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Возвращает значение <see cref="T:System.Reflection.MemberTypes" />, указывающее, что этот элемент является конструктором.
    /// </summary>
    /// <returns>
    ///   Значение <see cref="T:System.Reflection.MemberTypes" />, указывающее, что этот элемент является конструктором.
    /// </returns>
    [ComVisible(true)]
    public override MemberTypes MemberType
    {
      get
      {
        return MemberTypes.Constructor;
      }
    }

    /// <summary>
    ///   При реализации в производном классе вызывает конструктор, отраженный этим <see langword="ConstructorInfo" /> с заданными аргументами с учетом ограничений заданного <see langword="Binder" />.
    /// </summary>
    /// <param name="invokeAttr">
    ///   Один из <see langword="BindingFlags" /> значений, определяющее тип привязки.
    /// </param>
    /// <param name="binder">
    ///   Параметр <see langword="Binder" />, который определяет набор свойств и обеспечивает привязку, приведение типов аргументов и вызов членов с помощью отражения.
    ///    Если <paramref name="binder" /> является <see langword="null" />, затем <see langword="Binder.DefaultBinding" /> используется.
    /// </param>
    /// <param name="parameters">
    ///   Массив объектов типа <see langword="Object" /> соответствует число, порядок и тип параметров для данного конструктора, с учетом ограничений <paramref name="binder" />.
    ///    Если этот конструктор не требует параметров, передается массив с нулевыми элементами, как в Object [] parameters = new Object [0].
    ///    Любой объект этого массива, явно не инициализирован со значением будет содержать значение по умолчанию для данного типа объекта.
    ///    Для элементов ссылочного типа это значение равно <see langword="null" />.
    ///    Для элементов типа значения, это значение равно 0, 0,0 или <see langword="false" />, в зависимости от заданного типа элемента.
    /// </param>
    /// <param name="culture">
    ///   Параметр <see cref="T:System.Globalization.CultureInfo" />, используемый для управления приведением типов.
    ///    Если значение этого объекта — <see langword="null" />, для текущего потока используется <see cref="T:System.Globalization.CultureInfo" />.
    /// </param>
    /// <returns>Экземпляр класса, связанный с конструктором.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="parameters" /> Массив не содержит значений, соответствующих типам, принимаемым этим конструктором, с учетом ограничений <paramref name="binder" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Вызываемый конструктор создает исключение.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetParameterCountException">
    ///   Передано неверное число параметров.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Создание <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" />, и <see cref="T:System.RuntimeArgumentHandle" /> типы не поддерживаются.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Вызывающий объект не имеет разрешения на доступ необходимый код.
    /// </exception>
    /// <exception cref="T:System.MemberAccessException">
    ///   Этот класс является абстрактным.
    /// 
    ///   -или-
    /// 
    ///   Конструктор является инициализатором класса.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Конструктор является закрытым или защищенным и вызывающий объект отсутствует <see cref="F:System.Security.Permissions.ReflectionPermissionFlag.MemberAccess" />.
    /// </exception>
    public abstract object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

    /// <summary>
    ///   Вызывает конструктор, который определяется экземпляром с указанными параметрами, чтобы этим параметрам присваивались стандартные значения, которые используются нечасто.
    /// </summary>
    /// <param name="parameters">
    ///   Массив объектов, число, порядок и тип которых (с учетом ограничений для считывателя по умолчанию) соответствуют списку параметров этого конструктора.
    ///    Если этот конструктор не принимает параметры, следует использовать массив с нулевым числом элементов или <see langword="null" />, например: Object[] parameters = new Object[0].
    ///    Любой объект этого массива, которому не присвоено значение явным образом, будет содержать значение по умолчанию для своего типа объекта.
    ///    Для элементов ссылочного типа это значение равно <see langword="null" />.
    ///    Для элементов, хранящих значения, это значение равно 0, 0,0 или <see langword="false" /> (в зависимости от типа конкретного элемента).
    /// </param>
    /// <returns>Экземпляр класса, связанный с конструктором.</returns>
    /// <exception cref="T:System.MemberAccessException">
    ///   Этот класс является абстрактным.
    /// 
    ///   -или-
    /// 
    ///   Конструктор является инициализатором класса.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение базового класса <see cref="T:System.MemberAccessException" />.
    /// 
    ///   Конструктор является закрытым или защищенным, а у вызывающего объекта отсутствует разрешение <see cref="F:System.Security.Permissions.ReflectionPermissionFlag.MemberAccess" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="parameters" /> не содержит значения, которые соответствуют типам, принимаемым этим конструктором.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Вызванный конструктор создает исключение.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetParameterCountException">
    ///   Передано неверное число параметров.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Не поддерживается создание типов <see cref="T:System.TypedReference" />, <see cref="T:System.ArgIterator" /> и <see cref="T:System.RuntimeArgumentHandle" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Вызывающий объект не имеет необходимые разрешения для доступа к коду.
    /// </exception>
    [DebuggerStepThrough]
    [DebuggerHidden]
    [__DynamicallyInvokable]
    public object Invoke(object[] parameters)
    {
      return this.Invoke(BindingFlags.Default, (Binder) null, parameters, (CultureInfo) null);
    }

    Type _ConstructorInfo.GetType()
    {
      return this.GetType();
    }

    object _ConstructorInfo.Invoke_2(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
    {
      return this.Invoke(obj, invokeAttr, binder, parameters, culture);
    }

    object _ConstructorInfo.Invoke_3(object obj, object[] parameters)
    {
      return this.Invoke(obj, parameters);
    }

    object _ConstructorInfo.Invoke_4(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
    {
      return this.Invoke(invokeAttr, binder, parameters, culture);
    }

    object _ConstructorInfo.Invoke_5(object[] parameters)
    {
      return this.Invoke(parameters);
    }

    void _ConstructorInfo.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _ConstructorInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _ConstructorInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _ConstructorInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}

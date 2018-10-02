// Decompiled with JetBrains decompiler
// Type: System.Reflection.MethodInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection
{
  /// <summary>
  ///   Выявляет атрибуты метода и обеспечивает доступ к его метаданным.
  /// </summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_MethodInfo))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
  public abstract class MethodInfo : MethodBase, _MethodInfo
  {
    /// <summary>
    ///   Определение равенства двух объектов <see cref="T:System.Reflection.MethodInfo" />.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="left" /> и <paramref name="right" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(MethodInfo left, MethodInfo right)
    {
      if ((object) left == (object) right)
        return true;
      if ((object) left == null || (object) right == null || (left is RuntimeMethodInfo || right is RuntimeMethodInfo))
        return false;
      return left.Equals((object) right);
    }

    /// <summary>
    ///   Определяет неравенство двух объектов <see cref="T:System.Reflection.MethodInfo" />.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" />, если значения <paramref name="left" /> и <paramref name="right" /> не равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(MethodInfo left, MethodInfo right)
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

    /// <summary>
    ///   Возвращает значение <see cref="T:System.Reflection.MemberTypes" />, указывающее, что этот элемент является методом.
    /// </summary>
    /// <returns>
    ///   Значение <see cref="T:System.Reflection.MemberTypes" />, указывающее, что этот элемент является методом.
    /// </returns>
    public override MemberTypes MemberType
    {
      get
      {
        return MemberTypes.Method;
      }
    }

    /// <summary>Возвращает тип значения, возвращаемый этим методом.</summary>
    /// <returns>Тип возвращаемого значения этого метода.</returns>
    [__DynamicallyInvokable]
    public virtual Type ReturnType
    {
      [__DynamicallyInvokable] get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Reflection.ParameterInfo" />, который содержит сведения о типе возвращаемого значения этого метода, например, имеет ли возвращаемый тип пользовательские модификаторы.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.ParameterInfo" />, содержащий сведения о типе возвращаемого значения.
    /// </returns>
    /// <exception cref="T:System.NotImplementedException">
    ///   Этот метод не реализован.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual ParameterInfo ReturnParameter
    {
      [__DynamicallyInvokable] get
      {
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Возвращает пользовательские атрибуты типа возвращаемого значения.
    /// </summary>
    /// <returns>
    ///   Объект <see langword="ICustomAttributeProvider" />, представляющий пользовательские атрибуты для возвращаемого типа.
    /// </returns>
    public abstract ICustomAttributeProvider ReturnTypeCustomAttributes { get; }

    /// <summary>
    ///   Если переопределено в производном классе, возвращает объект <see cref="T:System.Reflection.MethodInfo" /> для метода в прямом или косвенном базовом классе, в котором был первоначально объявлен метод, предоставляемый этим экземпляром.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" /> для первой реализации этого метода.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract MethodInfo GetBaseDefinition();

    /// <summary>
    ///   Возвращает массив объектов <see cref="T:System.Type" />, которые представляют аргументы универсального метода, относящиеся к типу, или параметры типа определения универсального метода.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Type" />, представляющих аргументы типа, относящиеся к универсальному методу, или параметры типа определения универсального метода.
    ///    Возвращает пустой массив, если текущий метод не является универсальным методом.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается.
    /// </exception>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public override Type[] GetGenericArguments()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Reflection.MethodInfo" />, представляющий определение универсального метода, на основе которого можно сконструировать текущий метод.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" />, представляющий определение универсального метода, на основе которого может быть сконструирован текущий метод.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий метод не является универсальным.
    ///    То есть <see cref="P:System.Reflection.MethodInfo.IsGenericMethod" /> возвращает <see langword="false" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается.
    /// </exception>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public virtual MethodInfo GetGenericMethodDefinition()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    /// <summary>
    ///   Заменяет параметры типа элементами массива типов для определения текущего универсального метода и возвращает объект <see cref="T:System.Reflection.MethodInfo" />, представляющий итоговый сконструированный метод.
    /// </summary>
    /// <param name="typeArguments">
    ///   Массив типов, который должен быть замещен параметрами типов текущего определения универсального метода.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.MethodInfo" /> который представляет сконструированный метод, сформированный путем замены элементами <paramref name="typeArguments" /> параметров типов текущего определения универсального метода.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий объект <see cref="T:System.Reflection.MethodInfo" /> не представляет определение универсального метода.
    ///    То есть <see cref="P:System.Reflection.MethodInfo.IsGenericMethodDefinition" /> возвращает <see langword="false" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="typeArguments" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Любой элемент <paramref name="typeArguments" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Количество элементов в <paramref name="typeArguments" /> не совпадает с количеством параметров типа текущего определения универсального метода.
    /// 
    ///   -или-
    /// 
    ///   Элемент <paramref name="typeArguments" /> не соответствует ограничениям, указанным для соответствующего параметра типа текущего определения универсального метода.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не поддерживается.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual MethodInfo MakeGenericMethod(params Type[] typeArguments)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    /// <summary>Создает делегат заданного типа из этого метода.</summary>
    /// <param name="delegateType">Тип создаваемого делегата.</param>
    /// <returns>Делегат для этого метода.</returns>
    [__DynamicallyInvokable]
    public virtual Delegate CreateDelegate(Type delegateType)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    /// <summary>
    ///   Создает делегат заданного типа с заданным целевым объектом из этого метода.
    /// </summary>
    /// <param name="delegateType">Тип создаваемого делегата.</param>
    /// <param name="target">Целевой объект для делегата.</param>
    /// <returns>Делегат для этого метода.</returns>
    [__DynamicallyInvokable]
    public virtual Delegate CreateDelegate(Type delegateType, object target)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SubclassOverride"));
    }

    Type _MethodInfo.GetType()
    {
      return this.GetType();
    }

    void _MethodInfo.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _MethodInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _MethodInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _MethodInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}

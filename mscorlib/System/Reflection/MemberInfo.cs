// Decompiled with JetBrains decompiler
// Type: System.Reflection.MemberInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection
{
  /// <summary>
  ///   Получает сведения об атрибутах члена и предоставляет доступ к его метаданным.
  /// </summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_MemberInfo))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
  public abstract class MemberInfo : ICustomAttributeProvider, _MemberInfo
  {
    internal virtual bool CacheEquals(object o)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   При переопределении в производном классе возвращает значение <see cref="T:System.Reflection.MemberTypes" />, определяющее тип элемента — метод, конструктор, событие и т. д.
    /// </summary>
    /// <returns>
    ///   Значение <see cref="T:System.Reflection.MemberTypes" />, указывающее тип элемента.
    /// </returns>
    public abstract MemberTypes MemberType { get; }

    /// <summary>Возвращает имя текущего элемента.</summary>
    /// <returns>
    ///   Объект <see cref="T:System.String" /> содержит имя этого элемента.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract string Name { [__DynamicallyInvokable] get; }

    /// <summary>Возвращает класс, объявивший этот член.</summary>
    /// <returns>
    ///   <see langword="Type" /> Объект для класса, который объявляет этот член.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract Type DeclaringType { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Получает объект класса, который использовался для извлечения данного экземпляра объекта <see langword="MemberInfo" />.
    /// </summary>
    /// <returns>
    ///   Объект <see langword="Type" />, с помощью которого был получен данный объект <see langword="MemberInfo" />.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract Type ReflectedType { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Возвращает коллекцию, содержащую пользовательские атрибуты этого элемента.
    /// </summary>
    /// <returns>
    ///   Коллекция, содержащая пользовательские атрибуты этого элемента.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual IEnumerable<CustomAttributeData> CustomAttributes
    {
      [__DynamicallyInvokable] get
      {
        return (IEnumerable<CustomAttributeData>) this.GetCustomAttributesData();
      }
    }

    /// <summary>
    ///   При переопределении в производном классе возвращает массив всех настраиваемых атрибутов, применяемых к этому члену.
    /// </summary>
    /// <param name="inherit">
    ///   <see langword="true" /> для поиска цепочки наследования этого элемента для поиска атрибутов; в противном случае — <see langword="false" />.
    ///    Этот параметр пропускается для свойств и событий; см. заметки.
    /// </param>
    /// <returns>
    ///   Массив, содержащий настраиваемые атрибуты, примененные к данному члену, или массив с нулем элементов, если атрибуты не определены.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот член принадлежит к типу, который загружается в контекст, предназначенный только для отражения.
    ///    См. раздел How to: Load Assemblies into the Reflection-Only Context.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract object[] GetCustomAttributes(bool inherit);

    /// <summary>
    ///   При переопределении в производном классе возвращает массив настраиваемых атрибутов, применяемых к этому элементу и определяемых параметром <see cref="T:System.Type" />.
    /// </summary>
    /// <param name="attributeType">
    ///   Тип атрибута для поиска.
    ///    Возвращаются только те атрибуты, которые можно назначить этому типу.
    /// </param>
    /// <param name="inherit">
    ///   <see langword="true" /> для поиска атрибутов в цепочке наследования этого элемента. В противном случае — <see langword="false" />.
    ///    Этот параметр игнорируется для свойств и событий. См. раздел "Примечания".
    /// </param>
    /// <returns>
    ///   Массив настраиваемых атрибутов, применяемых к этому элементу, или массив без элементов, если не применены атрибуты, которые можно назначить <paramref name="attributeType" />.
    /// </returns>
    /// <exception cref="T:System.TypeLoadException">
    ///   Не удалось загрузить тип настраиваемого атрибута.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Если <paramref name="attributeType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Этот элемент загружается в контекст, предназначенный только для отражения.
    ///    См. раздел How to: Load Assemblies into the Reflection-Only Context.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract object[] GetCustomAttributes(Type attributeType, bool inherit);

    /// <summary>
    ///   При переопределении в производном классе указывает, применяется ли к данному элементу один или несколько атрибутов указанного типа или его производных типов.
    /// </summary>
    /// <param name="attributeType">
    ///   Тип настраиваемого атрибута для поиска.
    ///    Поиск включает производные типы.
    /// </param>
    /// <param name="inherit">
    ///   <see langword="true" /> для поиска цепочки наследования этого элемента для поиска атрибутов; в противном случае — <see langword="false" />.
    ///    Этот параметр пропускается для свойств и событий; см. заметки.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если один или несколько экземпляров <paramref name="attributeType" /> или любого из его производных типов, примененные к данному члену, в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract bool IsDefined(Type attributeType, bool inherit);

    /// <summary>
    ///   Возвращает список <see cref="T:System.Reflection.CustomAttributeData" /> объекты, представляющие данные об атрибутах, которые были применены к целевой элемент.
    /// </summary>
    /// <returns>
    ///   Универсальный список <see cref="T:System.Reflection.CustomAttributeData" /> объекты, представляющие данные об атрибутах, которые были применены к целевой элемент.
    /// </returns>
    public virtual IList<CustomAttributeData> GetCustomAttributesData()
    {
      throw new NotImplementedException();
    }

    /// <summary>Получает значение, определяющее элемент метаданных.</summary>
    /// <returns>
    ///   Значение, которое в сочетании с параметром <see cref="P:System.Reflection.MemberInfo.Module" /> однозначно определяет элемент метаданных.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий параметр <see cref="T:System.Reflection.MemberInfo" /> представляет собой метод массива, например <see langword="Address" />, в типе массива с динамическим незавершенным типом элементов.
    ///    Чтобы получить токен метаданных в этом случае, передайте объект <see cref="T:System.Reflection.MemberInfo" /> в метод <see cref="M:System.Reflection.Emit.ModuleBuilder.GetMethodToken(System.Reflection.MethodInfo)" />. Или получите токен напрямую с помощью метода <see cref="M:System.Reflection.Emit.ModuleBuilder.GetArrayMethodToken(System.Type,System.String,System.Reflection.CallingConventions,System.Type,System.Type[])" />, вместо того чтобы сначала получить <see cref="T:System.Reflection.MethodInfo" /> с помощью метода <see cref="M:System.Reflection.Emit.ModuleBuilder.GetArrayMethod(System.Type,System.String,System.Reflection.CallingConventions,System.Type,System.Type[])" />.
    /// </exception>
    public virtual int MetadataToken
    {
      get
      {
        throw new InvalidOperationException();
      }
    }

    /// <summary>
    ///   Возвращает модуль, в котором тип, который объявляет член, представленный текущим <see cref="T:System.Reflection.MemberInfo" /> определен.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Reflection.Module" /> В тип, который объявляет член представленного текущим <see cref="T:System.Reflection.MemberInfo" /> определен.
    /// </returns>
    /// <exception cref="T:System.NotImplementedException">
    ///   Этот метод не реализован.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual Module Module
    {
      [__DynamicallyInvokable] get
      {
        if ((object) (this as Type) != null)
          return ((Type) this).Module;
        throw new NotImplementedException();
      }
    }

    /// <summary>
    ///   Определение равенства двух объектов <see cref="T:System.Reflection.MemberInfo" />.
    /// </summary>
    /// <param name="left">
    ///   <see cref="T:System.Reflection.MemberInfo" /> Для сравнения с <paramref name="right" />.
    /// </param>
    /// <param name="right">
    ///   <see cref="T:System.Reflection.MemberInfo" /> Для сравнения с <paramref name="left" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="left" /> равен <paramref name="right" />; в противном случае <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(MemberInfo left, MemberInfo right)
    {
      if ((object) left == (object) right)
        return true;
      if ((object) left == null || (object) right == null)
        return false;
      Type type1;
      Type type2;
      if ((type1 = left as Type) != (Type) null && (type2 = right as Type) != (Type) null)
        return type1 == type2;
      MethodBase methodBase1;
      MethodBase methodBase2;
      if ((methodBase1 = left as MethodBase) != (MethodBase) null && (methodBase2 = right as MethodBase) != (MethodBase) null)
        return methodBase1 == methodBase2;
      FieldInfo fieldInfo1;
      FieldInfo fieldInfo2;
      if ((fieldInfo1 = left as FieldInfo) != (FieldInfo) null && (fieldInfo2 = right as FieldInfo) != (FieldInfo) null)
        return fieldInfo1 == fieldInfo2;
      EventInfo eventInfo1;
      EventInfo eventInfo2;
      if ((eventInfo1 = left as EventInfo) != (EventInfo) null && (eventInfo2 = right as EventInfo) != (EventInfo) null)
        return eventInfo1 == eventInfo2;
      PropertyInfo propertyInfo1;
      PropertyInfo propertyInfo2;
      if ((propertyInfo1 = left as PropertyInfo) != (PropertyInfo) null && (propertyInfo2 = right as PropertyInfo) != (PropertyInfo) null)
        return propertyInfo1 == propertyInfo2;
      return false;
    }

    /// <summary>
    ///   Определяет неравенство двух объектов <see cref="T:System.Reflection.MemberInfo" />.
    /// </summary>
    /// <param name="left">
    ///   <see cref="T:System.Reflection.MemberInfo" /> Для сравнения с <paramref name="right" />.
    /// </param>
    /// <param name="right">
    ///   <see cref="T:System.Reflection.MemberInfo" /> Для сравнения с <paramref name="left" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="left" /> не равно <paramref name="right" />; в противном случае <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(MemberInfo left, MemberInfo right)
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

    Type _MemberInfo.GetType()
    {
      return this.GetType();
    }

    void _MemberInfo.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _MemberInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _MemberInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _MemberInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}

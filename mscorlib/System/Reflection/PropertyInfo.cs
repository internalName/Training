// Decompiled with JetBrains decompiler
// Type: System.Reflection.PropertyInfo
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
  ///   Выявляет атрибуты свойства и обеспечивает доступ к его метаданным.
  /// </summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_PropertyInfo))]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
  public abstract class PropertyInfo : MemberInfo, _PropertyInfo
  {
    /// <summary>
    ///   Определение равенства двух объектов <see cref="T:System.Reflection.PropertyInfo" />.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="left" /> и <paramref name="right" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(PropertyInfo left, PropertyInfo right)
    {
      if ((object) left == (object) right)
        return true;
      if ((object) left == null || (object) right == null || (left is RuntimePropertyInfo || right is RuntimePropertyInfo))
        return false;
      return left.Equals((object) right);
    }

    /// <summary>
    ///   Определяет неравенство двух объектов <see cref="T:System.Reflection.PropertyInfo" />.
    /// </summary>
    /// <param name="left">Первый из сравниваемых объектов.</param>
    /// <param name="right">Второй из сравниваемых объектов.</param>
    /// <returns>
    ///   <see langword="true" />, если значения <paramref name="left" /> и <paramref name="right" /> не равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(PropertyInfo left, PropertyInfo right)
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
    ///   Возвращает значение <see cref="T:System.Reflection.MemberTypes" />, указывающее, что этот член является свойством.
    /// </summary>
    /// <returns>
    ///   Значение <see cref="T:System.Reflection.MemberTypes" />, указывающее, что этот член является свойством.
    /// </returns>
    public override MemberTypes MemberType
    {
      get
      {
        return MemberTypes.Property;
      }
    }

    /// <summary>
    ///   Метод возвращает значение-литерал, связанное с этим свойством компилятором.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Object" />, содержащий значение-литерал, сопоставленное данному свойству.
    ///    Если значение литерала является типом класса и при этом значение элемента равно нулю, возвращается значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Таблица констант в неуправляемых метаданных не содержит значение константы для текущего свойства.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Тип значения не является одним из типов, разрешенных спецификацией CLS.
    ///    См. спецификацию ECMA раздел II, "Метаданные".
    /// </exception>
    [__DynamicallyInvokable]
    public virtual object GetConstantValue()
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Метод возвращает значение-литерал, связанное с этим свойством компилятором.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Object" />, содержащий значение-литерал, сопоставленное данному свойству.
    ///    Если значение литерала является типом класса и при этом значение элемента равно нулю, возвращается значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Таблица констант в неуправляемых метаданных не содержит значение константы для текущего свойства.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Тип значения не является одним из типов, разрешенных спецификацией CLS.
    ///    См. спецификацию ECMA, раздел II, логический формат метаданных (другие структуры, типы элементов, используемые в сигнатурах).
    /// </exception>
    public virtual object GetRawConstantValue()
    {
      throw new NotImplementedException();
    }

    /// <summary>Возвращает тип этого свойства.</summary>
    /// <returns>Тип этого свойства.</returns>
    [__DynamicallyInvokable]
    public abstract Type PropertyType { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   При переопределении в производном классе задает значение свойства для заданного объекта, имеющего указанные сведения о привязке, индексе и языке и региональных параметрах.
    /// </summary>
    /// <param name="obj">
    ///   Объект, значение свойства которого будет установлено.
    /// </param>
    /// <param name="value">Новое значение свойства.</param>
    /// <param name="invokeAttr">
    ///   Битовая комбинация следующих членов перечисления, определяющих атрибут вызова: <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="Static" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" /> или <see langword="SetProperty" />.
    ///    Необходимо указать подходящий атрибут вызова.
    ///    Например, чтобы вызвать статический член, установите флаг <see langword="Static" />.
    /// </param>
    /// <param name="binder">
    ///   Объект, который допускает привязку, приведение типов аргументов, вызов элементов и извлечение объектов <see cref="T:System.Reflection.MemberInfo" /> путем отражения.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    /// </param>
    /// <param name="index">
    ///   Необязательные значения индекса для индексированных свойств.
    ///    Для неиндексированных свойств это значение должно быть равно <see langword="null" />.
    /// </param>
    /// <param name="culture">
    ///   Язык и региональные параметры, для которых должен быть локализован данный ресурс.
    ///    Если ресурс не локализован для данного языка и региональных параметров, при поиске соответствия будет последовательно вызываться свойство <see cref="P:System.Globalization.CultureInfo.Parent" />.
    ///    Если это значение равно <see langword="null" />, из свойства <see cref="P:System.Globalization.CultureInfo.CurrentUICulture" /> получаются сведения, относящиеся к конкретному языку и региональным параметрам.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="index" /> не содержит необходимый тип аргументов.
    /// 
    ///   -или-
    /// 
    ///   Не найден метод доступа <see langword="set" /> свойства.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="value" /> невозможно преобразовать в тип <see cref="P:System.Reflection.PropertyInfo.PropertyType" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetException">
    ///   Объект не соответствует целевому типу, или свойство является свойством экземпляра, но <paramref name="obj" /> равен <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetParameterCountException">
    ///   Число параметров в <paramref name="index" /> не соответствует числу параметров, принимаемых индексированным свойством.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Недопустимая попытка доступа к частному или защищенному методу внутри класса.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Произошла ошибка при установке значения свойства.
    ///    Например, значение индекса, указанное для индексированного свойства, находится вне диапазона.
    ///    Свойство <see cref="P:System.Exception.InnerException" /> указывает причину данной ошибки.
    /// </exception>
    public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

    /// <summary>
    ///   Возвращает массив, элементы которого отражают открытые и, если задано, неоткрытые методы доступа <see langword="get" /> и <see langword="set" /> к свойству, отражаемому текущим экземпляром.
    /// </summary>
    /// <param name="nonPublic">
    ///   Указывает, должны ли возвращаться неоткрытые методы в возвращаемый массив.
    ///   <see langword="true" />, если неоткрытые методы должны быть включены; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Массив, элементы которого отражают методы доступа <see langword="get" /> и <see langword="set" /> к свойству, отражаемому текущим экземпляром.
    ///    Если свойство <paramref name="nonPublic" /> имеет значение <see langword="true" />, этот массив содержит открытые и неоткрытые методы доступа <see langword="get" /> и <see langword="set " />.
    ///    Если свойство <paramref name="nonPublic" /> имеет значение <see langword="false" />, этот массив содержит только открытые методы доступа <see langword="get" /> и <see langword="set" />.
    ///    Если методы доступа с указанным статусом видимости не найдены, этот метод возвращает массив с нулевым (0) числом элементов.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract MethodInfo[] GetAccessors(bool nonPublic);

    /// <summary>
    ///   При переопределении в производном классе возвращает для этого свойства открытый или неоткрытый метод доступа <see langword="get" />.
    /// </summary>
    /// <param name="nonPublic">
    ///   Указывает, должен ли возвращаться неоткрытый метод доступа <see langword="get" />.
    ///    Значение <see langword="true" />, если метод должен быть возвращен; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Объект <see langword="MethodInfo" />, предоставляющий метод доступа <see langword="get" /> для этого свойства, если значение <paramref name="nonPublic" /> равно <see langword="true" />.
    ///    Возвращает значение <see langword="null" />, если <paramref name="nonPublic" /> равен <see langword="false" /> и метод <see langword="get" /> не является открытым, либо если свойство <paramref name="nonPublic" /> равно <see langword="true" /> и методы <see langword="get" /> отсутствуют.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Запрошенный метод не является открытым, и вызывающая сторона не имеет <see cref="T:System.Security.Permissions.ReflectionPermission" /> для отражения в этом методе.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract MethodInfo GetGetMethod(bool nonPublic);

    /// <summary>
    ///   При переопределении в производном классе возвращает для этого свойства метод доступа <see langword="set" />.
    /// </summary>
    /// <param name="nonPublic">
    ///   Указывает, должен ли возвращаться метод доступа, если он не является открытым.
    ///    Значение <see langword="true" />, если метод должен быть возвращен; в противном случае — значение <see langword="false" />.
    /// </param>
    /// <returns>
    /// Метод свойства <see langword="Set" /> или <see langword="null" />, как показано в следующей таблице.
    /// 
    ///         Значение
    /// 
    ///         Условие
    /// 
    ///         Метод <see langword="Set" /> для этого свойства.
    /// 
    ///         Метод доступа <see langword="set" /> является открытым.
    /// 
    ///         -или-
    /// 
    ///         Параметр <paramref name="nonPublic" /> имеет значение <see langword="true" />, и метод доступа <see langword="set" /> не является открытым.
    /// 
    ///         <see langword="null" />
    /// 
    ///         Параметр <paramref name="nonPublic" /> имеет значение <see langword="true" />, но свойство доступно только для чтения.
    /// 
    ///         -или-
    /// 
    ///         Параметр <paramref name="nonPublic" /> имеет значение <see langword="false" />, и метод доступа <see langword="set" /> не является открытым.
    /// 
    ///         -или-
    /// 
    ///         Метод доступа <see langword="set" /> не существует.
    ///       </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Запрошенный метод не является открытым, и вызывающая сторона не имеет <see cref="T:System.Security.Permissions.ReflectionPermission" /> для отражения в этом методе.
    /// </exception>
    [__DynamicallyInvokable]
    public abstract MethodInfo GetSetMethod(bool nonPublic);

    /// <summary>
    ///   При переопределении в производном классе возвращает для этого свойства массив всех параметров индекса.
    /// </summary>
    /// <returns>
    ///   Массив элементов типа <see langword="ParameterInfo" />, содержащий параметры для индексов.
    ///    Если свойство не индексировано, массив содержит 0 (нуль) элементов.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract ParameterInfo[] GetIndexParameters();

    /// <summary>Получает атрибуты данного свойства.</summary>
    /// <returns>Атрибуты данного свойства.</returns>
    [__DynamicallyInvokable]
    public abstract PropertyAttributes Attributes { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Получает значение, указывающее, можно ли выполнить считывание данного свойства.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если свойство доступно для чтения; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract bool CanRead { [__DynamicallyInvokable] get; }

    /// <summary>
    ///   Получает значение, указывающее, можно ли производить запись в данное свойство.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если свойство доступно для записи; в обратном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public abstract bool CanWrite { [__DynamicallyInvokable] get; }

    /// <summary>Возвращает значение свойства указанного объекта.</summary>
    /// <param name="obj">
    ///   Объект, свойство которого будет возвращено.
    /// </param>
    /// <returns>Значение свойства указанного объекта.</returns>
    [DebuggerStepThrough]
    [DebuggerHidden]
    [__DynamicallyInvokable]
    public object GetValue(object obj)
    {
      return this.GetValue(obj, (object[]) null);
    }

    /// <summary>
    ///   Возвращает значение свойства заданного объекта с дополнительными значениями индекса для индексированных свойств.
    /// </summary>
    /// <param name="obj">
    ///   Объект, свойство которого будет возвращено.
    /// </param>
    /// <param name="index">
    ///   Необязательные значения индекса для индексированных свойств.
    ///    Индексы индексированных свойств отсчитываются от нуля.
    ///    Для неиндексированных свойств это значение должно быть равно <see langword="null" />.
    /// </param>
    /// <returns>Значение свойства указанного объекта.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="index" /> не содержит необходимого типа аргументов.
    /// 
    ///   -или-
    /// 
    ///   Не найден метод доступа <see langword="get" /> свойства.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение <see cref="T:System.Exception" />.
    /// 
    ///   Объект не соответствует целевому типу, или свойство является свойством экземпляра, но <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetParameterCountException">
    ///   Число параметров в <paramref name="index" /> не соответствует числу параметров, принимаемых индексированным свойством.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение базового класса <see cref="T:System.MemberAccessException" />.
    /// 
    ///   Возникла недопустимая попытка доступа к частному или защищенному методу внутри класса.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Ошибка при получении значения свойства.
    ///    Например, значение индекса, указанное для индексированного свойства, находится вне диапазона.
    ///    Свойство <see cref="P:System.Exception.InnerException" /> указывает причину данной ошибки.
    /// </exception>
    [DebuggerStepThrough]
    [DebuggerHidden]
    [__DynamicallyInvokable]
    public virtual object GetValue(object obj, object[] index)
    {
      return this.GetValue(obj, BindingFlags.Default, (Binder) null, index, (CultureInfo) null);
    }

    /// <summary>
    ///   При переопределении в производном классе возвращает значение свойства заданного объекта, имеющего указанные сведения о привязке, индексе и языке и региональных параметрах.
    /// </summary>
    /// <param name="obj">
    ///   Объект, свойство которого будет возвращено.
    /// </param>
    /// <param name="invokeAttr">
    ///   Битовая комбинация следующих членов перечисления, определяющих атрибут вызова: <see langword="InvokeMethod" />, <see langword="CreateInstance" />, <see langword="Static" />, <see langword="GetField" />, <see langword="SetField" />, <see langword="GetProperty" /> и <see langword="SetProperty" />.
    ///    Необходимо указать подходящий атрибут вызова.
    ///    Например, чтобы вызвать статический член, установите флаг <see langword="Static" />.
    /// </param>
    /// <param name="binder">
    ///   Объект, который допускает привязку, приведение типов аргументов, вызов элементов и извлечение объектов <see cref="T:System.Reflection.MemberInfo" /> путем отражения.
    ///    Если значение параметра <paramref name="binder" /> равно <see langword="null" />, используется связыватель по умолчанию.
    /// </param>
    /// <param name="index">
    ///   Необязательные значения индекса для индексированных свойств.
    ///    Для неиндексированных свойств это значение должно быть равно <see langword="null" />.
    /// </param>
    /// <param name="culture">
    ///   Язык и региональные параметры, для которых должен быть локализован данный ресурс.
    ///    Если ресурс не локализован для данного языка и региональных параметров, при поиске соответствия будет последовательно вызываться свойство <see cref="P:System.Globalization.CultureInfo.Parent" />.
    ///    Если это значение равно <see langword="null" />, из свойства <see cref="P:System.Globalization.CultureInfo.CurrentUICulture" /> получаются сведения, относящиеся к конкретному языку и региональным параметрам.
    /// </param>
    /// <returns>Значение свойства указанного объекта.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="index" /> не содержит необходимого типа аргументов.
    /// 
    ///   -или-
    /// 
    ///   Не найден метод доступа <see langword="get" /> свойства.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetException">
    ///   Объект не соответствует целевому типу, или свойство является свойством экземпляра, но <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetParameterCountException">
    ///   Число параметров в <paramref name="index" /> не соответствует числу параметров, принимаемых индексированным свойством.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///   Возникла недопустимая попытка доступа к частному или защищенному методу внутри класса.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Ошибка при получении значения свойства.
    ///    Например, значение индекса, указанное для индексированного свойства, находится вне диапазона.
    ///    Свойство <see cref="P:System.Exception.InnerException" /> указывает причину данной ошибки.
    /// </exception>
    public abstract object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

    /// <summary>Задает значение свойства для указанного объекта.</summary>
    /// <param name="obj">
    ///   Объект, значение свойства которого будет установлено.
    /// </param>
    /// <param name="value">Новое значение свойства.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Не найден метод доступа <see langword="set" /> свойства.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="value" /> невозможно преобразовать в тип <see cref="P:System.Reflection.PropertyInfo.PropertyType" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение <see cref="T:System.Exception" />.
    /// 
    ///   Тип <paramref name="obj" /> не соответствует целевому типу, или свойство является свойством экземпляра, но <paramref name="obj" /> равен <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение базового класса <see cref="T:System.MemberAccessException" />.
    /// 
    ///   Возникла недопустимая попытка доступа к частному или защищенному методу внутри класса.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Ошибка при установке значения свойства.
    ///    Свойство <see cref="P:System.Exception.InnerException" /> содержит причину данной ошибки.
    /// </exception>
    [DebuggerStepThrough]
    [DebuggerHidden]
    [__DynamicallyInvokable]
    public void SetValue(object obj, object value)
    {
      this.SetValue(obj, value, (object[]) null);
    }

    /// <summary>
    ///   Задает значение свойства заданного объекта с дополнительными значениями индекса для индексированных свойств.
    /// </summary>
    /// <param name="obj">
    ///   Объект, значение свойства которого будет установлено.
    /// </param>
    /// <param name="value">Новое значение свойства.</param>
    /// <param name="index">
    ///   Необязательные значения индекса для индексированных свойств.
    ///    Для неиндексированных свойств это значение должно быть равно <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="index" /> не содержит необходимый тип аргументов.
    /// 
    ///   -или-
    /// 
    ///   Не найден метод доступа <see langword="set" /> свойства.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="value" /> невозможно преобразовать в тип <see cref="P:System.Reflection.PropertyInfo.PropertyType" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение <see cref="T:System.Exception" />.
    /// 
    ///   Объект не соответствует целевому типу, или свойство является свойством экземпляра, но <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetParameterCountException">
    ///   Число параметров в <paramref name="index" /> не соответствует числу параметров, принимаемых индексированным свойством.
    /// </exception>
    /// <exception cref="T:System.MethodAccessException">
    ///     Вместо этого в .NET для приложений Магазина Windows или в переносимой библиотеке классов перехватите исключение базового класса <see cref="T:System.MemberAccessException" />.
    /// 
    ///   Возникла недопустимая попытка доступа к частному или защищенному методу внутри класса.
    /// </exception>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Произошла ошибка при установке значения свойства.
    ///    Например, значение индекса, указанное для индексированного свойства, находится вне диапазона.
    ///    Свойство <see cref="P:System.Exception.InnerException" /> указывает причину данной ошибки.
    /// </exception>
    [DebuggerStepThrough]
    [DebuggerHidden]
    [__DynamicallyInvokable]
    public virtual void SetValue(object obj, object value, object[] index)
    {
      this.SetValue(obj, value, BindingFlags.Default, (Binder) null, index, (CultureInfo) null);
    }

    /// <summary>
    ///   Возвращает массив типов, представляющих обязательные настраиваемые модификаторы для свойства.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Type" />, которые указывают обязательные настраиваемые модификаторы для текущего свойства, например <see cref="T:System.Runtime.CompilerServices.IsConst" /> или <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.
    /// </returns>
    public virtual Type[] GetRequiredCustomModifiers()
    {
      return EmptyArray<Type>.Value;
    }

    /// <summary>
    ///   Возвращает массив типов, представляющих необязательные настраиваемые модификаторы для свойства.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Type" />, которые указывают дополнительные настраиваемые модификаторы для текущего свойства, такие как <see cref="T:System.Runtime.CompilerServices.IsConst" /> или <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.
    /// </returns>
    public virtual Type[] GetOptionalCustomModifiers()
    {
      return EmptyArray<Type>.Value;
    }

    /// <summary>
    ///   Возвращает массив, элементы которого отражают открытые методы <see langword="get" /> и <see langword="set" /> доступа к свойству, отражаемому текущим экземпляром.
    /// </summary>
    /// <returns>
    ///   Массив объектов <see cref="T:System.Reflection.MethodInfo" />, которые отражают открытые методы <see langword="get" /> и <see langword="set" /> доступа к свойству, отображаемому текущим экземпляром, если эти методы существуют. В противном случае этот метод возвращает массив с нулевым (0) числом элементов.
    /// </returns>
    [__DynamicallyInvokable]
    public MethodInfo[] GetAccessors()
    {
      return this.GetAccessors(false);
    }

    /// <summary>
    ///   Получает метод доступа <see langword="get" /> для этого свойства.
    /// </summary>
    /// <returns>
    ///   Метод доступа <see langword="get" /> для этого свойства.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual MethodInfo GetMethod
    {
      [__DynamicallyInvokable] get
      {
        return this.GetGetMethod(true);
      }
    }

    /// <summary>
    ///   Получает метод доступа <see langword="set" /> для этого свойства.
    /// </summary>
    /// <returns>
    ///   Метод доступа <see langword="set" /> для этого свойства или значение <see langword="null" />, если свойство доступно только для чтения.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual MethodInfo SetMethod
    {
      [__DynamicallyInvokable] get
      {
        return this.GetSetMethod(true);
      }
    }

    /// <summary>
    ///   Возвращает открытый метод доступа <see langword="get" /> для данного свойства.
    /// </summary>
    /// <returns>
    ///   Объект <see langword="MethodInfo" />, предоставляющий открытый метод доступа <see langword="get" /> для этого свойства, или значение <see langword="null" />, если метод доступа <see langword="get" /> не является открытым либо не существует.
    /// </returns>
    [__DynamicallyInvokable]
    public MethodInfo GetGetMethod()
    {
      return this.GetGetMethod(false);
    }

    /// <summary>
    ///   Возвращает открытый метод доступа <see langword="set" /> для данного свойства.
    /// </summary>
    /// <returns>
    ///   Объект <see langword="MethodInfo" />, представляющий метод <see langword="Set" /> для этого свойства, если метод доступа <see langword="set" /> является открытым, или значение <see langword="null" />, если метод <see langword="set" /> не является открытым.
    /// </returns>
    [__DynamicallyInvokable]
    public MethodInfo GetSetMethod()
    {
      return this.GetSetMethod(false);
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли свойство специальным именем.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если свойство является особым именем; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsSpecialName
    {
      [__DynamicallyInvokable] get
      {
        return (uint) (this.Attributes & PropertyAttributes.SpecialName) > 0U;
      }
    }

    Type _PropertyInfo.GetType()
    {
      return this.GetType();
    }

    void _PropertyInfo.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _PropertyInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _PropertyInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _PropertyInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}

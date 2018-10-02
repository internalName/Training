// Decompiled with JetBrains decompiler
// Type: System.Reflection.Binder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Reflection
{
  /// <summary>
  ///   Выбирает члена из списка кандидатов и выполняет преобразование типов из действительного типа аргумента в тип формального аргумента.
  /// </summary>
  [ClassInterface(ClassInterfaceType.AutoDual)]
  [ComVisible(true)]
  [Serializable]
  public abstract class Binder
  {
    /// <summary>
    ///   Выбирает метод, вызываемый из данного набора методов в зависимости от предоставленных аргументов.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая комбинация значений <see cref="T:System.Reflection.BindingFlags" />.
    /// </param>
    /// <param name="match">
    ///   Набор методов, являющихся кандидатами для сопоставления.
    ///    Например, если <see cref="T:System.Reflection.Binder" /> объект используется в <see cref="Overload:System.Type.InvokeMember" />, этот параметр указывает, соответствует набор методов, которые определил отражения, возможна, обычно из-за наличия правильным именем члена.
    ///    Реализация по умолчанию, предоставляемые <see cref="P:System.Type.DefaultBinder" /> изменяет порядок элементов этого массива.
    /// </param>
    /// <param name="args">
    ///   Аргументы, передаваемые в.
    ///    Связыватель может изменить порядок аргументов в этом массиве; Например, связыватель по умолчанию изменяет порядок аргументов, если <paramref name="names" /> параметр используется, чтобы задать порядок, отличный от порядка по позиции.
    ///    Если в реализации связывателя выполняется приведение типов аргументов, типы и значения аргументов можно изменить также.
    /// </param>
    /// <param name="modifiers">
    ///   Массив модификаторов параметров, позволяющий привязке работать с подписями параметров, в которых были изменены типы.
    ///    Реализация связыватель по умолчанию этот параметр не используется.
    /// </param>
    /// <param name="culture">
    ///   Экземпляр <see cref="T:System.Globalization.CultureInfo" /> используемый для управления приведением типов данных в реализациях связывателя, выполняющих приведение типов.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, для текущего потока используется объект <see cref="T:System.Globalization.CultureInfo" />.
    /// 
    ///   Примечание. Например, если в реализации связывателя допускается приведение строковых значений в числовые типы, этот параметр необходим для преобразования <see langword="String" /> представляющий 1000, в <see langword="Double" /> значение, поскольку в разных культурах 1000 представляется по-разному.
    ///    Связыватель по умолчанию не выполняет подобного преобразования строковых типов.
    /// </param>
    /// <param name="names">
    ///   Имена параметров, если они должны учитываться при сопоставлении, или <see langword="null" /> Если аргументы должны считаться чисто позиционными.
    ///    Например имена параметров должны использоваться, если аргументы не передаются в порядковой позиции.
    /// </param>
    /// <param name="state">
    ///   После возврата из метода, <paramref name="state" /> содержит предоставленный связывателем объект, который отслеживает изменение порядка аргументов.
    ///    Связыватель создает этот объект и является единственным объектом-получателем данного объекта.
    ///    Если <paramref name="state" /> не <see langword="null" /> при <see langword="BindToMethod" /> возвращает значение, необходимо передать <paramref name="state" /> для <see cref="M:System.Reflection.Binder.ReorderArgumentArray(System.Object[]@,System.Object)" /> метод, если требуется восстановить <paramref name="args" /> исходного заказа, к примеру, чтобы вы могли получать значения <see langword="ref" /> параметров (<see langword="ByRef" /> в Visual Basic).
    /// </param>
    /// <returns>Соответствующий метод.</returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Для связыватель по умолчанию <paramref name="match" /> содержит несколько методов, которые одинаково хорошо соответствуют <paramref name="args" />.
    ///    Например <paramref name="args" /> содержит MyClass объект, реализующий интерфейс IMyClass интерфейс, и <paramref name="match" /> содержит метод, который принимает MyClass и метод, который принимает IMyClass.
    /// </exception>
    /// <exception cref="T:System.MissingMethodException">
    ///   Для связыватель по умолчанию <paramref name="match" /> не содержит методов, которые может принимать аргументы, переданные в <paramref name="args" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Для связыватель по умолчанию <paramref name="match" /> — <see langword="null" /> или пустой массив.
    /// </exception>
    public abstract MethodBase BindToMethod(BindingFlags bindingAttr, MethodBase[] match, ref object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] names, out object state);

    /// <summary>
    ///   Выбирает поле из заданного набора полей с учетом указанным критериям.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая комбинация значений <see cref="T:System.Reflection.BindingFlags" />.
    /// </param>
    /// <param name="match">
    ///   Набор полей, являющихся кандидатами для сопоставления.
    ///    Например, если <see cref="T:System.Reflection.Binder" /> объект используется в <see cref="Overload:System.Type.InvokeMember" />, этот параметр указывает, соответствует набор полей, которые определил отражения, возможна, обычно из-за наличия правильным именем члена.
    ///    Реализация по умолчанию, предоставляемые <see cref="P:System.Type.DefaultBinder" /> изменяет порядок элементов этого массива.
    /// </param>
    /// <param name="value">
    ///   Значение поля, используемые для нахождения соответствующего поля.
    /// </param>
    /// <param name="culture">
    ///   Экземпляр <see cref="T:System.Globalization.CultureInfo" /> используемый для управления приведением типов данных в реализациях связывателя, выполняющих приведение типов.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, для текущего потока используется объект <see cref="T:System.Globalization.CultureInfo" />.
    /// 
    ///   Примечание. Например, если в реализации связывателя допускается приведение строковых значений в числовые типы, этот параметр необходим для преобразования <see langword="String" /> представляющий 1000, в <see langword="Double" /> значение, поскольку в разных культурах 1000 представляется по-разному.
    ///    Связыватель по умолчанию не выполняет подобного преобразования строковых типов.
    /// </param>
    /// <returns>Соответствующее поле.</returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Для связыватель по умолчанию <paramref name="bindingAttr" /> включает в себя <see cref="F:System.Reflection.BindingFlags.SetField" />, и <paramref name="match" /> содержит несколько полей, которые одинаково хорошо соответствуют <paramref name="value" />.
    ///    Например <paramref name="value" /> содержит MyClass объект, реализующий интерфейс IMyClass интерфейс, и <paramref name="match" /> содержит поле типа MyClass и поле типа IMyClass.
    /// </exception>
    /// <exception cref="T:System.MissingFieldException">
    ///   Для связыватель по умолчанию <paramref name="bindingAttr" /> включает в себя <see cref="F:System.Reflection.BindingFlags.SetField" />, и <paramref name="match" /> не содержит полей, которые могут принимать <paramref name="value" />.
    /// </exception>
    /// <exception cref="T:System.NullReferenceException">
    ///   Для связыватель по умолчанию <paramref name="bindingAttr" /> включает в себя <see cref="F:System.Reflection.BindingFlags.SetField" />, и <paramref name="match" /> — <see langword="null" /> или пустой массив.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="bindingAttr" /> включает в себя <see cref="F:System.Reflection.BindingFlags.SetField" />, и <paramref name="value" /> является <see langword="null" />.
    /// </exception>
    public abstract FieldInfo BindToField(BindingFlags bindingAttr, FieldInfo[] match, object value, CultureInfo culture);

    /// <summary>
    ///   Выбирает метод из данного набора методов в зависимости от типа аргумента.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая комбинация значений <see cref="T:System.Reflection.BindingFlags" />.
    /// </param>
    /// <param name="match">
    ///   Набор методов, являющихся кандидатами для сопоставления.
    ///    Например, если <see cref="T:System.Reflection.Binder" /> объект используется в <see cref="Overload:System.Type.InvokeMember" />, этот параметр указывает, соответствует набор методов, которые определил отражения, возможна, обычно из-за наличия правильным именем члена.
    ///    Реализация по умолчанию, предоставляемые <see cref="P:System.Type.DefaultBinder" /> изменяет порядок элементов этого массива.
    /// </param>
    /// <param name="types">
    ///   Типы параметров, используемые для нахождения соответствующего метода.
    /// </param>
    /// <param name="modifiers">
    ///   Массив модификаторов параметров, позволяющий привязке работать с подписями параметров, в которых были изменены типы.
    /// </param>
    /// <returns>
    ///   Соответствующий метод, если найден; в противном случае — <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Для связыватель по умолчанию <paramref name="match" /> содержит несколько методов, которые одинаково хорошо соответствуют типы параметров, описываемых <paramref name="types" />.
    ///    Например, массив в <paramref name="types" /> содержит <see cref="T:System.Type" /> для объекта MyClass и массива в <paramref name="match" /> содержит метод, который принимает базовый класс MyClass и метод, который принимает интерфейс, MyClass реализует.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Для связыватель по умолчанию <paramref name="match" /> — <see langword="null" /> или пустой массив.
    /// 
    ///   -или-
    /// 
    ///   Элемент <paramref name="types" /> является производным от <see cref="T:System.Type" />, но не относится к типу <see langword="RuntimeType" />.
    /// </exception>
    public abstract MethodBase SelectMethod(BindingFlags bindingAttr, MethodBase[] match, Type[] types, ParameterModifier[] modifiers);

    /// <summary>
    ///   Выбирает свойство из заданного набора свойств в зависимости от заданных критериев.
    /// </summary>
    /// <param name="bindingAttr">
    ///   Битовая комбинация значений <see cref="T:System.Reflection.BindingFlags" />.
    /// </param>
    /// <param name="match">
    ///   Набор свойств, являющихся кандидатами для сопоставления.
    ///    Например, если <see cref="T:System.Reflection.Binder" /> объект используется в <see cref="Overload:System.Type.InvokeMember" />, этот параметр указывает, соответствует набор свойств, которые определил отражения, возможна, обычно из-за наличия правильным именем члена.
    ///    Реализация по умолчанию, предоставляемые <see cref="P:System.Type.DefaultBinder" /> изменяет порядок элементов этого массива.
    /// </param>
    /// <param name="returnType">
    ///   Должен иметь значение соответствующее свойство.
    /// </param>
    /// <param name="indexes">
    ///   Типы индексов свойства, поиск которого выполняется.
    ///    Используется для свойств индекса, например указателя для класса.
    /// </param>
    /// <param name="modifiers">
    ///   Массив модификаторов параметров, позволяющий привязке работать с подписями параметров, в которых были изменены типы.
    /// </param>
    /// <returns>Соответствующее свойство.</returns>
    /// <exception cref="T:System.Reflection.AmbiguousMatchException">
    ///   Для связыватель по умолчанию <paramref name="match" /> содержит несколько свойств, которые одинаково хорошо соответствуют <paramref name="returnType" /> и <paramref name="indexes" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Для связыватель по умолчанию <paramref name="match" /> — <see langword="null" /> или пустой массив.
    /// </exception>
    public abstract PropertyInfo SelectProperty(BindingFlags bindingAttr, PropertyInfo[] match, Type returnType, Type[] indexes, ParameterModifier[] modifiers);

    /// <summary>
    ///   Меняет тип заданного <see langword="Object" /> для данного <see langword="Type" />.
    /// </summary>
    /// <param name="value">
    ///   Для изменения в новый объект <see langword="Type" />.
    /// </param>
    /// <param name="type">
    ///   Новый <see langword="Type" /><paramref name="value" /> станет.
    /// </param>
    /// <param name="culture">
    ///   Экземпляр <see cref="T:System.Globalization.CultureInfo" /> используемый для управления приведением типов данных.
    ///    Если параметр <paramref name="culture" /> имеет значение <see langword="null" />, для текущего потока используется объект <see cref="T:System.Globalization.CultureInfo" />.
    /// 
    ///   Примечание. Например, этот параметр необходим для преобразования <see langword="String" /> представляющий 1000, в <see langword="Double" /> значение, поскольку в разных культурах 1000 представляется по-разному.
    /// </param>
    /// <returns>
    ///   Объект, содержащий заданное значение в качестве нового типа.
    /// </returns>
    public abstract object ChangeType(object value, Type type, CultureInfo culture);

    /// <summary>
    ///   После возврата из <see cref="M:System.Reflection.Binder.BindToMethod(System.Reflection.BindingFlags,System.Reflection.MethodBase[],System.Object[]@,System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[],System.Object@)" />, восстанавливает <paramref name="args" /> аргумента в том случае, если он поступил от <see langword="BindToMethod" />.
    /// </summary>
    /// <param name="args">
    ///   Фактические аргументы, передаваемые в.
    ///    Можно изменить типы и значения аргументов.
    /// </param>
    /// <param name="state">
    ///   Предоставленный связывателем объект, который отслеживает изменение порядка аргументов.
    /// </param>
    public abstract void ReorderArgumentArray(ref object[] args, object state);
  }
}

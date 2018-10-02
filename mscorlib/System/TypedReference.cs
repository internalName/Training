// Decompiled with JetBrains decompiler
// Type: System.TypedReference
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  /// <summary>
  ///   Описывает объекты, которые содержат одновременно управляемый указатель на местоположение и представление типа среды выполнения, который может храниться в этом местоположении.
  /// </summary>
  [CLSCompliant(false)]
  [ComVisible(true)]
  [NonVersionable]
  public struct TypedReference
  {
    private IntPtr Value;
    private IntPtr Type;

    /// <summary>
    ///   Создает объект типа <see langword="TypedReference" /> для поля, определяемого по указанному объекту и списку описаний полей.
    /// </summary>
    /// <param name="target">
    ///   Объект, содержащий поле, описываемое первым элементом параметра <paramref name="flds" />.
    /// </param>
    /// <param name="flds">
    ///   Список описаний полей, каждый элемент которого описывает поле, в котором содержится поле, описываемое следующим элементом.
    ///    Каждое описываемое поле должно относиться к типу значения.
    ///    Описания полей должны быть объектами типа <see langword="RuntimeFieldInfo" />, предоставляемыми системой типов.
    /// </param>
    /// <returns>
    ///   Объект типа <see cref="T:System.TypedReference" /> для поля, описываемого последним элементом массива <paramref name="flds" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="target" /> или <paramref name="flds" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Элемент <paramref name="flds" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Массив <paramref name="flds" /> не содержит элементов.
    /// 
    ///   -или-
    /// 
    ///   Элемент <paramref name="flds" /> не является объектом <see langword="RuntimeFieldInfo" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <see cref="P:System.Reflection.FieldInfo.IsInitOnly" /> или <see cref="P:System.Reflection.FieldInfo.IsStatic" /> любого элемента <paramref name="flds" /> имеет значение <see langword="true" />.
    /// </exception>
    /// <exception cref="T:System.MissingMemberException">
    ///   Параметр <paramref name="target" /> не содержит поле, описываемое первым элементом <paramref name="flds" />, или элемент <paramref name="flds" /> описывает поле, которое не содержится в поле, описываемом следующим элементом <paramref name="flds" />.
    /// 
    ///   -или-
    /// 
    ///   Поле, описываемое элементом <paramref name="flds" />, не является типом значения.
    /// </exception>
    [SecurityCritical]
    [CLSCompliant(false)]
    public static unsafe TypedReference MakeTypedReference(object target, FieldInfo[] flds)
    {
      if (target == null)
        throw new ArgumentNullException(nameof (target));
      if (flds == null)
        throw new ArgumentNullException(nameof (flds));
      if (flds.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_ArrayZeroError"));
      IntPtr[] flds1 = new IntPtr[flds.Length];
      RuntimeType lastFieldType = (RuntimeType) target.GetType();
      for (int index = 0; index < flds.Length; ++index)
      {
        RuntimeFieldInfo fld = flds[index] as RuntimeFieldInfo;
        if ((FieldInfo) fld == (FieldInfo) null)
          throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeFieldInfo"));
        if (fld.IsInitOnly || fld.IsStatic)
          throw new ArgumentException(Environment.GetResourceString("Argument_TypedReferenceInvalidField"));
        if (lastFieldType != fld.GetDeclaringTypeInternal() && !lastFieldType.IsSubclassOf((System.Type) fld.GetDeclaringTypeInternal()))
          throw new MissingMemberException(Environment.GetResourceString("MissingMemberTypeRef"));
        RuntimeType fieldType = (RuntimeType) fld.FieldType;
        if (fieldType.IsPrimitive)
          throw new ArgumentException(Environment.GetResourceString("Arg_TypeRefPrimitve"));
        if (index < flds.Length - 1 && !fieldType.IsValueType)
          throw new MissingMemberException(Environment.GetResourceString("MissingMemberNestErr"));
        flds1[index] = fld.FieldHandle.Value;
        lastFieldType = fieldType;
      }
      TypedReference typedReference = new TypedReference();
      TypedReference.InternalMakeTypedReference((void*) &typedReference, target, flds1, lastFieldType);
      return typedReference;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe void InternalMakeTypedReference(void* result, object target, IntPtr[] flds, RuntimeType lastFieldType);

    /// <summary>Возвращает хэш-код для этого объекта.</summary>
    /// <returns>Хэш-код данного объекта.</returns>
    public override int GetHashCode()
    {
      if (this.Type == IntPtr.Zero)
        return 0;
      return __reftype (this).GetHashCode();
    }

    /// <summary>
    ///   Проверяет, эквивалентен ли данный объект указанному объекту.
    /// </summary>
    /// <param name="o">Объект, сравниваемый с текущим объектом.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если этот объект эквивалентен заданному объекту; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Этот метод не реализован.
    /// </exception>
    public override bool Equals(object o)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_NYI"));
    }

    /// <summary>
    ///   Преобразует указанную ссылку <see langword="TypedReference" /> в <see langword="Object" />.
    /// </summary>
    /// <param name="value">
    ///   Преобразуемая структура <see langword="TypedReference" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Object" />, в который был преобразован объект типа <see langword="TypedReference" />.
    /// </returns>
    [SecuritySafeCritical]
    public static unsafe object ToObject(TypedReference value)
    {
      return TypedReference.InternalToObject((void*) &value);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern unsafe object InternalToObject(void* value);

    internal bool IsNull
    {
      get
      {
        if (this.Value.IsNull())
          return this.Type.IsNull();
        return false;
      }
    }

    /// <summary>
    ///   Возвращает тип целевого объекта для заданного объекта типа <see langword="TypedReference" />.
    /// </summary>
    /// <param name="value">
    ///   Значение, для которого необходимо вернуть тип целевого объекта.
    /// </param>
    /// <returns>
    ///   Тип целевого объекта для заданного объекта типа <see langword="TypedReference" />.
    /// </returns>
    public static System.Type GetTargetType(TypedReference value)
    {
      return __reftype (value);
    }

    /// <summary>
    ///   Возвращает внутренний дескриптор типа метаданных для указанного объекта типа <see langword="TypedReference" />.
    /// </summary>
    /// <param name="value">
    ///   Объект типа <see langword="TypedReference" />, для которого запрашивается дескриптор типа.
    /// </param>
    /// <returns>
    ///   Возвращает внутренний дескриптор типа метаданных для указанного объекта типа <see langword="TypedReference" />.
    /// </returns>
    public static RuntimeTypeHandle TargetTypeToken(TypedReference value)
    {
      return __reftype (value).TypeHandle;
    }

    /// <summary>
    ///   Преобразовывает указанное значение в объект типа <see langword="TypedReference" />.
    ///    Этот метод не поддерживается.
    /// </summary>
    /// <param name="target">Целевой объект преобразования.</param>
    /// <param name="value">Преобразуемое значение.</param>
    /// <exception cref="T:System.NotSupportedException">
    ///   Во всех случаях.
    /// </exception>
    [SecuritySafeCritical]
    [CLSCompliant(false)]
    public static unsafe void SetTypedReference(TypedReference target, object value)
    {
      TypedReference.InternalSetTypedReference((void*) &target, value);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern unsafe void InternalSetTypedReference(void* target, object value);
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.Expando.IExpando
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.InteropServices.Expando
{
  /// <summary>
  ///   Позволяет изменять объекты путем добавления и удаления элементов, представленных <see cref="T:System.Reflection.MemberInfo" /> объектов.
  /// </summary>
  [Guid("AFBF15E6-C37C-11d2-B88E-00A0C9B471B8")]
  [ComVisible(true)]
  public interface IExpando : IReflect
  {
    /// <summary>Добавляет именованный объект к объекту Reflection.</summary>
    /// <param name="name">Имя поля.</param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.FieldInfo" /> объект, представляющий добавленное поле.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   <see langword="IExpando" /> Объект не поддерживает этот метод.
    /// </exception>
    FieldInfo AddField(string name);

    /// <summary>
    ///   Добавляет именованное свойство к объекту Reflection.
    /// </summary>
    /// <param name="name">Имя свойства.</param>
    /// <returns>
    ///   Объект <see langword="PropertyInfo" /> объект, представляющий добавленное свойство.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   <see langword="IExpando" /> Объект не поддерживает этот метод.
    /// </exception>
    PropertyInfo AddProperty(string name);

    /// <summary>Добавляет именованный метод объекта отражения.</summary>
    /// <param name="name">Имя метода.</param>
    /// <param name="method">Делегат для метода.</param>
    /// <returns>
    ///   Объект <see langword="MethodInfo" /> объект, представляющий добавленный метод.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   <see langword="IExpando" /> Объект не поддерживает этот метод.
    /// </exception>
    MethodInfo AddMethod(string name, Delegate method);

    /// <summary>Удаляет заданный элемент.</summary>
    /// <param name="m">Удаляемый элемент.</param>
    /// <exception cref="T:System.NotSupportedException">
    ///   <see langword="IExpando" /> Объект не поддерживает этот метод.
    /// </exception>
    void RemoveMember(MemberInfo m);
  }
}

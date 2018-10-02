// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComTypes.ITypeComp
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices.ComTypes
{
  /// <summary>
  ///   Предоставляет управляемое определение <see langword="ITypeComp" /> интерфейса.
  /// </summary>
  [Guid("00020403-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [__DynamicallyInvokable]
  [ComImport]
  public interface ITypeComp
  {
    /// <summary>
    ///   Сопоставляет имя с типом члена или связывает глобальные переменные и функции, содержащиеся в библиотеке типов.
    /// </summary>
    /// <param name="szName">Имя для привязки.</param>
    /// <param name="lHashVal">
    ///   Хэш-значение для <paramref name="szName" /> вычисляется по <see langword="LHashValOfNameSys" />.
    /// </param>
    /// <param name="wFlags">
    ///   Слово флагов, содержащее один или несколько флагов, определенных в <see langword="INVOKEKIND" /> перечисления.
    /// </param>
    /// <param name="ppTInfo">
    ///   При возвращении данного метода содержит ссылку на описание типа, который содержит элемент, к которому он привязан, если <see langword="FUNCDESC" /> или <see langword="VARDESC" /> был возвращен.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="pDescKind">
    ///   При возвращении данного метода содержит ссылку на <see langword="DESCKIND" /> перечислителя, который указывает, является ли связанный с именем <see langword="VARDESC" />, <see langword="FUNCDESC" />, или <see langword="TYPECOMP" />.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="pBindPtr">
    ///   При возвращении данного метода содержит ссылку на связанный с <see langword="VARDESC" />, <see langword="FUNCDESC" />, или <see langword="ITypeComp" /> интерфейса.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void Bind([MarshalAs(UnmanagedType.LPWStr)] string szName, int lHashVal, short wFlags, out ITypeInfo ppTInfo, out DESCKIND pDescKind, out BINDPTR pBindPtr);

    /// <summary>
    ///   Выполняет привязку к описаниям типов, содержащимся в библиотеке типов.
    /// </summary>
    /// <param name="szName">Имя для привязки.</param>
    /// <param name="lHashVal">
    ///   Хэш-значение для <paramref name="szName" /> определяется <see langword="LHashValOfNameSys" />.
    /// </param>
    /// <param name="ppTInfo">
    ///   При возвращении данного метода содержит ссылку на <see langword="ITypeInfo" /> типа, к которому <paramref name="szName" /> была привязана.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="ppTComp">
    ///   При возвращении данного метода содержит ссылку на <see langword="ITypeComp" /> переменной.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    [__DynamicallyInvokable]
    void BindType([MarshalAs(UnmanagedType.LPWStr)] string szName, int lHashVal, out ITypeInfo ppTInfo, out ITypeComp ppTComp);
  }
}

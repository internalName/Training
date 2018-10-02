// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.UCOMITypeComp
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Взамен рекомендуется использовать <see cref="T:System.Runtime.InteropServices.ComTypes.ITypeComp" />.
  /// </summary>
  [Obsolete("Use System.Runtime.InteropServices.ComTypes.ITypeComp instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
  [Guid("00020403-0000-0000-C000-000000000046")]
  [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
  [ComImport]
  public interface UCOMITypeComp
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
    ///   При удачном возвращении — ссылка на описание типа, содержащий элемент, к которому он привязан, если <see langword="FUNCDESC" /> или <see langword="VARDESC" /> был возвращен.
    /// </param>
    /// <param name="pDescKind">
    ///   Ссылку на <see langword="DESCKIND" /> перечислитель, указывает ли привязка имени <see langword="VARDESC" />, <see langword="FUNCDESC" />, или <see langword="TYPECOMP" />.
    /// </param>
    /// <param name="pBindPtr">
    ///   Ссылку на связанный с <see langword="VARDESC" />, <see langword="FUNCDESC" />, или <see langword="ITypeComp" /> интерфейса.
    /// </param>
    void Bind([MarshalAs(UnmanagedType.LPWStr)] string szName, int lHashVal, short wFlags, out UCOMITypeInfo ppTInfo, out DESCKIND pDescKind, out BINDPTR pBindPtr);

    /// <summary>
    ///   Выполняет привязку к описаниям типов, содержащимся в библиотеке типов.
    /// </summary>
    /// <param name="szName">Имя для привязки.</param>
    /// <param name="lHashVal">
    ///   Хэш-значение для <paramref name="szName" /> определяется <see langword="LHashValOfNameSys" />.
    /// </param>
    /// <param name="ppTInfo">
    ///   При удачном возвращении ссылку на <see langword="ITypeInfo" /> типа, к которому <paramref name="szName" /> была привязана.
    /// </param>
    /// <param name="ppTComp">
    ///   При удачном возвращении ссылку на <see langword="ITypeComp" /> переменной.
    /// </param>
    void BindType([MarshalAs(UnmanagedType.LPWStr)] string szName, int lHashVal, out UCOMITypeInfo ppTInfo, out UCOMITypeComp ppTComp);
  }
}

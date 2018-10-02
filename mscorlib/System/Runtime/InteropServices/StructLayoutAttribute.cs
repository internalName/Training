// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.StructLayoutAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Позволяет управлять физическим размещением полей данных класса или структуры в памяти.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class StructLayoutAttribute : Attribute
  {
    private const int DEFAULT_PACKING_SIZE = 8;
    internal LayoutKind _val;
    /// <summary>
    ///   Управляет выравниванием полей данных для класса или структуры в памяти.
    /// </summary>
    [__DynamicallyInvokable]
    public int Pack;
    /// <summary>Указывает абсолютный размер класса или структуры.</summary>
    [__DynamicallyInvokable]
    public int Size;
    /// <summary>
    ///   Показывает, каким образом следует выполнять маршалирование по умолчанию для полей строковых данных в классе (как <see langword="LPWSTR" /> или как <see langword="LPSTR" />).
    /// </summary>
    [__DynamicallyInvokable]
    public CharSet CharSet;

    [SecurityCritical]
    internal static Attribute GetCustomAttribute(RuntimeType type)
    {
      if (!StructLayoutAttribute.IsDefined(type))
        return (Attribute) null;
      int packSize = 0;
      int classSize = 0;
      LayoutKind layoutKind = LayoutKind.Auto;
      switch (type.Attributes & TypeAttributes.LayoutMask)
      {
        case TypeAttributes.NotPublic:
          layoutKind = LayoutKind.Auto;
          break;
        case TypeAttributes.SequentialLayout:
          layoutKind = LayoutKind.Sequential;
          break;
        case TypeAttributes.ExplicitLayout:
          layoutKind = LayoutKind.Explicit;
          break;
      }
      CharSet charSet = CharSet.None;
      switch (type.Attributes & TypeAttributes.StringFormatMask)
      {
        case TypeAttributes.NotPublic:
          charSet = CharSet.Ansi;
          break;
        case TypeAttributes.UnicodeClass:
          charSet = CharSet.Unicode;
          break;
        case TypeAttributes.AutoClass:
          charSet = CharSet.Auto;
          break;
      }
      type.GetRuntimeModule().MetadataImport.GetClassLayout(type.MetadataToken, out packSize, out classSize);
      if (packSize == 0)
        packSize = 8;
      return (Attribute) new StructLayoutAttribute(layoutKind, packSize, classSize, charSet);
    }

    internal static bool IsDefined(RuntimeType type)
    {
      return !type.IsInterface && !type.HasElementType && !type.IsGenericParameter;
    }

    internal StructLayoutAttribute(LayoutKind layoutKind, int pack, int size, CharSet charSet)
    {
      this._val = layoutKind;
      this.Pack = pack;
      this.Size = size;
      this.CharSet = charSet;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" />, используя указанный элемент перечисления <see cref="T:System.Runtime.InteropServices.LayoutKind" />.
    /// </summary>
    /// <param name="layoutKind">
    ///   Одно из значений перечисления, определяющих компоновку класса или структуры.
    /// </param>
    [__DynamicallyInvokable]
    public StructLayoutAttribute(LayoutKind layoutKind)
    {
      this._val = layoutKind;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" />, используя указанный элемент перечисления <see cref="T:System.Runtime.InteropServices.LayoutKind" />.
    /// </summary>
    /// <param name="layoutKind">
    ///   16-разрядное целое число, представляющее одно из значений <see cref="T:System.Runtime.InteropServices.LayoutKind" />, определяющих компоновку класса или структуры.
    /// </param>
    public StructLayoutAttribute(short layoutKind)
    {
      this._val = (LayoutKind) layoutKind;
    }

    /// <summary>
    ///   Получает значение <see cref="T:System.Runtime.InteropServices.LayoutKind" />, определяющее компоновку класса или структуры.
    /// </summary>
    /// <returns>
    ///   Одно из значений перечисления, определяющих компоновку класса или структуры.
    /// </returns>
    [__DynamicallyInvokable]
    public LayoutKind Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }
  }
}

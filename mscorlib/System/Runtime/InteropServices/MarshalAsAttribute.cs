// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.MarshalAsAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает способ маршалинга данных между управляемым и неуправляемым кодом.
  /// </summary>
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class MarshalAsAttribute : Attribute
  {
    internal UnmanagedType _val;
    /// <summary>
    ///   Указывает тип элемента <see cref="F:System.Runtime.InteropServices.UnmanagedType.SafeArray" />.
    /// </summary>
    [__DynamicallyInvokable]
    public VarEnum SafeArraySubType;
    /// <summary>
    ///   Указывает тип определяемого пользователем элемента <see cref="F:System.Runtime.InteropServices.UnmanagedType.SafeArray" />.
    /// </summary>
    [__DynamicallyInvokable]
    public Type SafeArrayUserDefinedSubType;
    /// <summary>
    ///   Задает индекс параметра неуправляемого <see langword="iid_is" /> атрибута, используемого в COM.
    /// </summary>
    [__DynamicallyInvokable]
    public int IidParameterIndex;
    /// <summary>
    ///   Указывает тип элемента неуправляемого <see cref="F:System.Runtime.InteropServices.UnmanagedType.LPArray" /> или <see cref="F:System.Runtime.InteropServices.UnmanagedType.ByValArray" />.
    /// </summary>
    [__DynamicallyInvokable]
    public UnmanagedType ArraySubType;
    /// <summary>
    ///   Указывает (с нуля) параметра, который содержит число элементов массива, аналогично <see langword="size_is" /> в модели COM.
    /// </summary>
    [__DynamicallyInvokable]
    public short SizeParamIndex;
    /// <summary>
    ///   Указывает число элементов в массиве фиксированной длины или количество знаков (не байтов) в строке для импорта.
    /// </summary>
    [__DynamicallyInvokable]
    public int SizeConst;
    /// <summary>Задает полное имя настраиваемого модуля маршалинга.</summary>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public string MarshalType;
    /// <summary>
    ///   Реализует <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.MarshalType" /> как тип.
    /// </summary>
    [ComVisible(true)]
    [__DynamicallyInvokable]
    public Type MarshalTypeRef;
    /// <summary>
    ///   Дополнительные сведения для настраиваемого модуля маршалинга.
    /// </summary>
    [__DynamicallyInvokable]
    public string MarshalCookie;

    [SecurityCritical]
    internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
    {
      return MarshalAsAttribute.GetCustomAttribute(parameter.MetadataToken, parameter.GetRuntimeModule());
    }

    [SecurityCritical]
    internal static bool IsDefined(RuntimeParameterInfo parameter)
    {
      return MarshalAsAttribute.GetCustomAttribute(parameter) != null;
    }

    [SecurityCritical]
    internal static Attribute GetCustomAttribute(RuntimeFieldInfo field)
    {
      return MarshalAsAttribute.GetCustomAttribute(field.MetadataToken, field.GetRuntimeModule());
    }

    [SecurityCritical]
    internal static bool IsDefined(RuntimeFieldInfo field)
    {
      return MarshalAsAttribute.GetCustomAttribute(field) != null;
    }

    [SecurityCritical]
    internal static Attribute GetCustomAttribute(int token, RuntimeModule scope)
    {
      int sizeParamIndex = 0;
      int sizeConst = 0;
      string marshalType = (string) null;
      string marshalCookie = (string) null;
      string safeArrayUserDefinedSubType1 = (string) null;
      int iidParamIndex = 0;
      ConstArray fieldMarshal = ModuleHandle.GetMetadataImport(scope.GetNativeHandle()).GetFieldMarshal(token);
      if (fieldMarshal.Length == 0)
        return (Attribute) null;
      UnmanagedType unmanagedType;
      VarEnum safeArraySubType;
      UnmanagedType arraySubType;
      MetadataImport.GetMarshalAs(fieldMarshal, out unmanagedType, out safeArraySubType, out safeArrayUserDefinedSubType1, out arraySubType, out sizeParamIndex, out sizeConst, out marshalType, out marshalCookie, out iidParamIndex);
      RuntimeType safeArrayUserDefinedSubType2 = safeArrayUserDefinedSubType1 == null || safeArrayUserDefinedSubType1.Length == 0 ? (RuntimeType) null : RuntimeTypeHandle.GetTypeByNameUsingCARules(safeArrayUserDefinedSubType1, scope);
      RuntimeType marshalTypeRef = (RuntimeType) null;
      try
      {
        marshalTypeRef = marshalType == null ? (RuntimeType) null : RuntimeTypeHandle.GetTypeByNameUsingCARules(marshalType, scope);
      }
      catch (TypeLoadException ex)
      {
      }
      return (Attribute) new MarshalAsAttribute(unmanagedType, safeArraySubType, safeArrayUserDefinedSubType2, arraySubType, (short) sizeParamIndex, sizeConst, marshalType, marshalTypeRef, marshalCookie, iidParamIndex);
    }

    internal MarshalAsAttribute(UnmanagedType val, VarEnum safeArraySubType, RuntimeType safeArrayUserDefinedSubType, UnmanagedType arraySubType, short sizeParamIndex, int sizeConst, string marshalType, RuntimeType marshalTypeRef, string marshalCookie, int iidParamIndex)
    {
      this._val = val;
      this.SafeArraySubType = safeArraySubType;
      this.SafeArrayUserDefinedSubType = (Type) safeArrayUserDefinedSubType;
      this.IidParameterIndex = iidParamIndex;
      this.ArraySubType = arraySubType;
      this.SizeParamIndex = sizeParamIndex;
      this.SizeConst = sizeConst;
      this.MarshalType = marshalType;
      this.MarshalTypeRef = (Type) marshalTypeRef;
      this.MarshalCookie = marshalCookie;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.MarshalAsAttribute" /> с заданным <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> член перечисления.
    /// </summary>
    /// <param name="unmanagedType">
    ///   — Маршалироваться как значение данных.
    /// </param>
    [__DynamicallyInvokable]
    public MarshalAsAttribute(UnmanagedType unmanagedType)
    {
      this._val = unmanagedType;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.MarshalAsAttribute" /> заданным значением <see cref="T:System.Runtime.InteropServices.UnmanagedType" />.
    /// </summary>
    /// <param name="unmanagedType">
    ///   — Маршалироваться как значение данных.
    /// </param>
    [__DynamicallyInvokable]
    public MarshalAsAttribute(short unmanagedType)
    {
      this._val = (UnmanagedType) unmanagedType;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> значение, данные которого будут маршалированы как.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> Значение, данные которого будут маршалированы как.
    /// </returns>
    [__DynamicallyInvokable]
    public UnmanagedType Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }
  }
}

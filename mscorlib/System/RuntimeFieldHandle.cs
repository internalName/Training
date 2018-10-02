// Decompiled with JetBrains decompiler
// Type: System.RuntimeFieldHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
  /// <summary>
  ///   Представляет поле, в котором используется внутренний маркер метаданных.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct RuntimeFieldHandle : ISerializable
  {
    private IRuntimeFieldInfo m_ptr;

    internal RuntimeFieldHandle GetNativeHandle()
    {
      IRuntimeFieldInfo ptr = this.m_ptr;
      if (ptr == null)
        throw new ArgumentNullException((string) null, Environment.GetResourceString("Arg_InvalidHandle"));
      return new RuntimeFieldHandle(ptr);
    }

    internal RuntimeFieldHandle(IRuntimeFieldInfo fieldInfo)
    {
      this.m_ptr = fieldInfo;
    }

    internal IRuntimeFieldInfo GetRuntimeFieldInfo()
    {
      return this.m_ptr;
    }

    /// <summary>
    ///   Возвращает дескриптор поля, представленного текущим экземпляром.
    /// </summary>
    /// <returns>
    ///   <see cref="T:System.IntPtr" /> Содержащий дескриптор поля, представленного текущим экземпляром.
    /// </returns>
    public IntPtr Value
    {
      [SecurityCritical] get
      {
        if (this.m_ptr == null)
          return IntPtr.Zero;
        return this.m_ptr.Value.Value;
      }
    }

    internal bool IsNullHandle()
    {
      return this.m_ptr == null;
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   32-разрядное целое число со знаком, являющееся хэш-кодом для данного экземпляра.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return ValueType.GetHashCodeOfPtr(this.Value);
    }

    /// <summary>
    ///   Указывает, равен ли текущий экземпляр указанному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="obj" /> является <see cref="T:System.RuntimeFieldHandle" /> и равен значению текущего экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (!(obj is RuntimeFieldHandle))
        return false;
      return ((RuntimeFieldHandle) obj).Value == this.Value;
    }

    /// <summary>
    ///   Указывает, равен ли текущий экземпляр указанному <see cref="T:System.RuntimeFieldHandle" />.
    /// </summary>
    /// <param name="handle">
    ///   <see cref="T:System.RuntimeFieldHandle" /> Для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если значение <paramref name="handle" /> равно значению текущего экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public bool Equals(RuntimeFieldHandle handle)
    {
      return handle.Value == this.Value;
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.RuntimeFieldHandle" /> структуры равны.
    /// </summary>
    /// <param name="left">
    ///   <see cref="T:System.RuntimeFieldHandle" /> Для сравнения с <paramref name="right" />.
    /// </param>
    /// <param name="right">
    ///   <see cref="T:System.RuntimeFieldHandle" /> Для сравнения с <paramref name="left" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="left" /> и <paramref name="right" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(RuntimeFieldHandle left, RuntimeFieldHandle right)
    {
      return left.Equals(right);
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.RuntimeFieldHandle" /> структуры не равны.
    /// </summary>
    /// <param name="left">
    ///   <see cref="T:System.RuntimeFieldHandle" /> Для сравнения с <paramref name="right" />.
    /// </param>
    /// <param name="right">
    ///   <see cref="T:System.RuntimeFieldHandle" /> Для сравнения с <paramref name="left" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения <paramref name="left" /> и <paramref name="right" /> не равны; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(RuntimeFieldHandle left, RuntimeFieldHandle right)
    {
      return !left.Equals(right);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string GetName(RtFieldInfo field);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe void* _GetUtf8Name(RuntimeFieldHandleInternal field);

    [SecuritySafeCritical]
    internal static unsafe Utf8String GetUtf8Name(RuntimeFieldHandleInternal field)
    {
      return new Utf8String(RuntimeFieldHandle._GetUtf8Name(field));
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool MatchesNameHash(RuntimeFieldHandleInternal handle, uint hash);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern FieldAttributes GetAttributes(RuntimeFieldHandleInternal field);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeType GetApproxDeclaringType(RuntimeFieldHandleInternal field);

    [SecurityCritical]
    internal static RuntimeType GetApproxDeclaringType(IRuntimeFieldInfo field)
    {
      RuntimeType approxDeclaringType = RuntimeFieldHandle.GetApproxDeclaringType(field.Value);
      GC.KeepAlive((object) field);
      return approxDeclaringType;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetToken(RtFieldInfo field);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object GetValue(RtFieldInfo field, object instance, RuntimeType fieldType, RuntimeType declaringType, ref bool domainInitialized);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern unsafe object GetValueDirect(RtFieldInfo field, RuntimeType fieldType, void* pTypedRef, RuntimeType contextType);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void SetValue(RtFieldInfo field, object obj, object value, RuntimeType fieldType, FieldAttributes fieldAttr, RuntimeType declaringType, ref bool domainInitialized);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern unsafe void SetValueDirect(RtFieldInfo field, RuntimeType fieldType, void* pTypedRef, object value, RuntimeType contextType);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeFieldHandleInternal GetStaticFieldForGenericType(RuntimeFieldHandleInternal field, RuntimeType declaringType);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool AcquiresContextFromThis(RuntimeFieldHandleInternal field);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsSecurityCritical(RuntimeFieldHandle fieldHandle);

    [SecuritySafeCritical]
    internal bool IsSecurityCritical()
    {
      return RuntimeFieldHandle.IsSecurityCritical(this.GetNativeHandle());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsSecuritySafeCritical(RuntimeFieldHandle fieldHandle);

    [SecuritySafeCritical]
    internal bool IsSecuritySafeCritical()
    {
      return RuntimeFieldHandle.IsSecuritySafeCritical(this.GetNativeHandle());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsSecurityTransparent(RuntimeFieldHandle fieldHandle);

    [SecuritySafeCritical]
    internal bool IsSecurityTransparent()
    {
      return RuntimeFieldHandle.IsSecurityTransparent(this.GetNativeHandle());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void CheckAttributeAccess(RuntimeFieldHandle fieldHandle, RuntimeModule decoratedTarget);

    [SecurityCritical]
    private RuntimeFieldHandle(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      FieldInfo fieldInfo = (FieldInfo) info.GetValue("FieldObj", typeof (RuntimeFieldInfo));
      if (fieldInfo == (FieldInfo) null)
        throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
      this.m_ptr = fieldInfo.FieldHandle.m_ptr;
      if (this.m_ptr == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
    }

    /// <summary>
    ///   Заполняет <see cref="T:System.Runtime.Serialization.SerializationInfo" /> с данными, необходимыми для десериализации поля, представленного текущим экземпляром.
    /// </summary>
    /// <param name="info">
    ///   <see cref="T:System.Runtime.Serialization.SerializationInfo" /> Объекта для заполнения сведениями о сериализации.
    /// </param>
    /// <param name="context">
    ///   (Зарезервировано) Место для хранения и извлечения сериализованных данных.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   <see cref="P:System.RuntimeFieldHandle.Value" /> Свойство текущего экземпляра не является допустимым дескриптором.
    /// </exception>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      if (this.m_ptr == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidFieldState"));
      RuntimeFieldInfo fieldInfo = (RuntimeFieldInfo) RuntimeType.GetFieldInfo(this.GetRuntimeFieldInfo());
      info.AddValue("FieldObj", (object) fieldInfo, typeof (RuntimeFieldInfo));
    }
  }
}

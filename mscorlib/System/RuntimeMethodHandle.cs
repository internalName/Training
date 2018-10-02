// Decompiled with JetBrains decompiler
// Type: System.RuntimeMethodHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System
{
  /// <summary>
  ///   <see cref="T:System.RuntimeMethodHandle" />— Это дескриптор для представления внутренних метаданных метода.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct RuntimeMethodHandle : ISerializable
  {
    private IRuntimeMethodInfo m_value;

    internal static IRuntimeMethodInfo EnsureNonNullMethodInfo(IRuntimeMethodInfo method)
    {
      if (method == null)
        throw new ArgumentNullException((string) null, Environment.GetResourceString("Arg_InvalidHandle"));
      return method;
    }

    internal static RuntimeMethodHandle EmptyHandle
    {
      get
      {
        return new RuntimeMethodHandle();
      }
    }

    internal RuntimeMethodHandle(IRuntimeMethodInfo method)
    {
      this.m_value = method;
    }

    internal IRuntimeMethodInfo GetMethodInfo()
    {
      return this.m_value;
    }

    [SecurityCritical]
    private static IntPtr GetValueInternal(RuntimeMethodHandle rmh)
    {
      return rmh.Value;
    }

    [SecurityCritical]
    private RuntimeMethodHandle(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      this.m_value = ((MethodBase) info.GetValue("MethodObj", typeof (MethodBase))).MethodHandle.m_value;
      if (this.m_value == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
    }

    /// <summary>
    ///   Заполняет <see cref="T:System.Runtime.Serialization.SerializationInfo" /> данными, необходимыми для десериализации поля, представленного этим экземпляром.
    /// </summary>
    /// <param name="info">
    ///   Объект, который требуется заполнить информацией о сериализации.
    /// </param>
    /// <param name="context">
    ///   (Зарезервировано) Место для хранения и извлечения сериализованных данных.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   <see cref="P:System.RuntimeMethodHandle.Value" /> недопустим.
    /// </exception>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      if (this.m_value == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidFieldState"));
      MethodBase methodBase = RuntimeType.GetMethodBase(this.m_value);
      info.AddValue("MethodObj", (object) methodBase, typeof (MethodBase));
    }

    /// <summary>Возвращает значение данного экземпляра.</summary>
    /// <returns>
    ///   A <see cref="T:System.RuntimeMethodHandle" /> именно представления внутренних метаданных метода.
    /// </returns>
    public IntPtr Value
    {
      [SecurityCritical] get
      {
        if (this.m_value == null)
          return IntPtr.Zero;
        return this.m_value.Value.Value;
      }
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      return ValueType.GetHashCodeOfPtr(this.Value);
    }

    /// <summary>
    ///   Указывает, равен ли данный экземпляр указанному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект <see cref="T:System.Object" />, сравниваемый с данным экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="obj" /> является <see cref="T:System.RuntimeMethodHandle" /> и равен значению данного экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (!(obj is RuntimeMethodHandle))
        return false;
      return ((RuntimeMethodHandle) obj).Value == this.Value;
    }

    /// <summary>
    ///   Указывает, являются ли два экземпляра объекта <see cref="T:System.RuntimeMethodHandle" /> равны.
    /// </summary>
    /// <param name="left">
    ///   Объект <see cref="T:System.RuntimeMethodHandle" /> для сравнения с <paramref name="right" />.
    /// </param>
    /// <param name="right">
    ///   Объект <see cref="T:System.RuntimeMethodHandle" /> для сравнения с <paramref name="left" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если значение <paramref name="left" /> равно значению <paramref name="right" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(RuntimeMethodHandle left, RuntimeMethodHandle right)
    {
      return left.Equals(right);
    }

    /// <summary>
    ///   Указывает, являются ли два экземпляра объекта <see cref="T:System.RuntimeMethodHandle" /> не равны.
    /// </summary>
    /// <param name="left">
    ///   Объект <see cref="T:System.RuntimeMethodHandle" /> для сравнения с <paramref name="right" />.
    /// </param>
    /// <param name="right">
    ///   Объект <see cref="T:System.RuntimeMethodHandle" /> для сравнения с <paramref name="left" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если значение <paramref name="left" /> не равно значению <paramref name="right" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(RuntimeMethodHandle left, RuntimeMethodHandle right)
    {
      return !left.Equals(right);
    }

    /// <summary>
    ///   Указывает, равен ли данный экземпляр с заданным <see cref="T:System.RuntimeMethodHandle" />.
    /// </summary>
    /// <param name="handle">
    ///   Объект <see cref="T:System.RuntimeMethodHandle" />, сравниваемый с данным экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="handle" /> равно значению данного экземпляра; в противном случае, <see langword="false" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public bool Equals(RuntimeMethodHandle handle)
    {
      return handle.Value == this.Value;
    }

    internal bool IsNullHandle()
    {
      return this.m_value == null;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern IntPtr GetFunctionPointer(RuntimeMethodHandleInternal handle);

    /// <summary>
    ///   Получает указатель на метод, представленный этим экземпляром.
    /// </summary>
    /// <returns>
    ///   Указатель на метод, представленный этим экземпляром.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Вызывающий не имеет необходимых разрешений для выполнения этой операции.
    /// </exception>
    [SecurityCritical]
    public IntPtr GetFunctionPointer()
    {
      IntPtr functionPointer = RuntimeMethodHandle.GetFunctionPointer(RuntimeMethodHandle.EnsureNonNullMethodInfo(this.m_value).Value);
      GC.KeepAlive((object) this.m_value);
      return functionPointer;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void CheckLinktimeDemands(IRuntimeMethodInfo method, RuntimeModule module, bool isDecoratedTargetSecurityTransparent);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern bool IsCAVisibleFromDecoratedType(RuntimeTypeHandle attrTypeHandle, IRuntimeMethodInfo attrCtor, RuntimeTypeHandle sourceTypeHandle, RuntimeModule sourceModule);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern IRuntimeMethodInfo _GetCurrentMethod(ref StackCrawlMark stackMark);

    [SecuritySafeCritical]
    internal static IRuntimeMethodInfo GetCurrentMethod(ref StackCrawlMark stackMark)
    {
      return RuntimeMethodHandle._GetCurrentMethod(ref stackMark);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern MethodAttributes GetAttributes(RuntimeMethodHandleInternal method);

    [SecurityCritical]
    internal static MethodAttributes GetAttributes(IRuntimeMethodInfo method)
    {
      MethodAttributes attributes = RuntimeMethodHandle.GetAttributes(method.Value);
      GC.KeepAlive((object) method);
      return attributes;
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern MethodImplAttributes GetImplAttributes(IRuntimeMethodInfo method);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void ConstructInstantiation(IRuntimeMethodInfo method, TypeNameFormatFlags format, StringHandleOnStack retString);

    [SecuritySafeCritical]
    internal static string ConstructInstantiation(IRuntimeMethodInfo method, TypeNameFormatFlags format)
    {
      string s = (string) null;
      RuntimeMethodHandle.ConstructInstantiation(RuntimeMethodHandle.EnsureNonNullMethodInfo(method), format, JitHelpers.GetStringHandleOnStack(ref s));
      return s;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeType GetDeclaringType(RuntimeMethodHandleInternal method);

    [SecuritySafeCritical]
    internal static RuntimeType GetDeclaringType(IRuntimeMethodInfo method)
    {
      RuntimeType declaringType = RuntimeMethodHandle.GetDeclaringType(method.Value);
      GC.KeepAlive((object) method);
      return declaringType;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetSlot(RuntimeMethodHandleInternal method);

    [SecurityCritical]
    internal static int GetSlot(IRuntimeMethodInfo method)
    {
      int slot = RuntimeMethodHandle.GetSlot(method.Value);
      GC.KeepAlive((object) method);
      return slot;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetMethodDef(IRuntimeMethodInfo method);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string GetName(RuntimeMethodHandleInternal method);

    [SecurityCritical]
    internal static string GetName(IRuntimeMethodInfo method)
    {
      string name = RuntimeMethodHandle.GetName(method.Value);
      GC.KeepAlive((object) method);
      return name;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe void* _GetUtf8Name(RuntimeMethodHandleInternal method);

    [SecurityCritical]
    internal static unsafe Utf8String GetUtf8Name(RuntimeMethodHandleInternal method)
    {
      return new Utf8String(RuntimeMethodHandle._GetUtf8Name(method));
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool MatchesNameHash(RuntimeMethodHandleInternal method, uint hash);

    [SecuritySafeCritical]
    [DebuggerStepThrough]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object InvokeMethod(object target, object[] arguments, Signature sig, bool constructor);

    [SecurityCritical]
    internal static INVOCATION_FLAGS GetSecurityFlags(IRuntimeMethodInfo handle)
    {
      return (INVOCATION_FLAGS) RuntimeMethodHandle.GetSpecialSecurityFlags(handle);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern uint GetSpecialSecurityFlags(IRuntimeMethodInfo method);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void PerformSecurityCheck(object obj, RuntimeMethodHandleInternal method, RuntimeType parent, uint invocationFlags);

    [SecurityCritical]
    internal static void PerformSecurityCheck(object obj, IRuntimeMethodInfo method, RuntimeType parent, uint invocationFlags)
    {
      RuntimeMethodHandle.PerformSecurityCheck(obj, method.Value, parent, invocationFlags);
      GC.KeepAlive((object) method);
    }

    [SecuritySafeCritical]
    [DebuggerStepThrough]
    [DebuggerHidden]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern void SerializationInvoke(IRuntimeMethodInfo method, object target, SerializationInfo info, ref StreamingContext context);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool _IsTokenSecurityTransparent(RuntimeModule module, int metaDataToken);

    [SecurityCritical]
    internal static bool IsTokenSecurityTransparent(Module module, int metaDataToken)
    {
      return RuntimeMethodHandle._IsTokenSecurityTransparent(module.ModuleHandle.GetRuntimeModule(), metaDataToken);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool _IsSecurityCritical(IRuntimeMethodInfo method);

    [SecuritySafeCritical]
    internal static bool IsSecurityCritical(IRuntimeMethodInfo method)
    {
      return RuntimeMethodHandle._IsSecurityCritical(method);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool _IsSecuritySafeCritical(IRuntimeMethodInfo method);

    [SecuritySafeCritical]
    internal static bool IsSecuritySafeCritical(IRuntimeMethodInfo method)
    {
      return RuntimeMethodHandle._IsSecuritySafeCritical(method);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool _IsSecurityTransparent(IRuntimeMethodInfo method);

    [SecuritySafeCritical]
    internal static bool IsSecurityTransparent(IRuntimeMethodInfo method)
    {
      return RuntimeMethodHandle._IsSecurityTransparent(method);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetMethodInstantiation(RuntimeMethodHandleInternal method, ObjectHandleOnStack types, bool fAsRuntimeTypeArray);

    [SecuritySafeCritical]
    internal static RuntimeType[] GetMethodInstantiationInternal(IRuntimeMethodInfo method)
    {
      RuntimeType[] o = (RuntimeType[]) null;
      RuntimeMethodHandle.GetMethodInstantiation(RuntimeMethodHandle.EnsureNonNullMethodInfo(method).Value, JitHelpers.GetObjectHandleOnStack<RuntimeType[]>(ref o), true);
      GC.KeepAlive((object) method);
      return o;
    }

    [SecuritySafeCritical]
    internal static RuntimeType[] GetMethodInstantiationInternal(RuntimeMethodHandleInternal method)
    {
      RuntimeType[] o = (RuntimeType[]) null;
      RuntimeMethodHandle.GetMethodInstantiation(method, JitHelpers.GetObjectHandleOnStack<RuntimeType[]>(ref o), true);
      return o;
    }

    [SecuritySafeCritical]
    internal static Type[] GetMethodInstantiationPublic(IRuntimeMethodInfo method)
    {
      RuntimeType[] o = (RuntimeType[]) null;
      RuntimeMethodHandle.GetMethodInstantiation(RuntimeMethodHandle.EnsureNonNullMethodInfo(method).Value, JitHelpers.GetObjectHandleOnStack<RuntimeType[]>(ref o), false);
      GC.KeepAlive((object) method);
      return (Type[]) o;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool HasMethodInstantiation(RuntimeMethodHandleInternal method);

    [SecuritySafeCritical]
    internal static bool HasMethodInstantiation(IRuntimeMethodInfo method)
    {
      bool flag = RuntimeMethodHandle.HasMethodInstantiation(method.Value);
      GC.KeepAlive((object) method);
      return flag;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeMethodHandleInternal GetStubIfNeeded(RuntimeMethodHandleInternal method, RuntimeType declaringType, RuntimeType[] methodInstantiation);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeMethodHandleInternal GetMethodFromCanonical(RuntimeMethodHandleInternal method, RuntimeType declaringType);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsGenericMethodDefinition(RuntimeMethodHandleInternal method);

    [SecuritySafeCritical]
    internal static bool IsGenericMethodDefinition(IRuntimeMethodInfo method)
    {
      bool flag = RuntimeMethodHandle.IsGenericMethodDefinition(method.Value);
      GC.KeepAlive((object) method);
      return flag;
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsTypicalMethodDefinition(IRuntimeMethodInfo method);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetTypicalMethodDefinition(IRuntimeMethodInfo method, ObjectHandleOnStack outMethod);

    [SecuritySafeCritical]
    internal static IRuntimeMethodInfo GetTypicalMethodDefinition(IRuntimeMethodInfo method)
    {
      if (!RuntimeMethodHandle.IsTypicalMethodDefinition(method))
        RuntimeMethodHandle.GetTypicalMethodDefinition(method, JitHelpers.GetObjectHandleOnStack<IRuntimeMethodInfo>(ref method));
      return method;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void StripMethodInstantiation(IRuntimeMethodInfo method, ObjectHandleOnStack outMethod);

    [SecuritySafeCritical]
    internal static IRuntimeMethodInfo StripMethodInstantiation(IRuntimeMethodInfo method)
    {
      IRuntimeMethodInfo o = method;
      RuntimeMethodHandle.StripMethodInstantiation(method, JitHelpers.GetObjectHandleOnStack<IRuntimeMethodInfo>(ref o));
      return o;
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsDynamicMethod(RuntimeMethodHandleInternal method);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void Destroy(RuntimeMethodHandleInternal method);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern Resolver GetResolver(RuntimeMethodHandleInternal method);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetCallerType(StackCrawlMarkHandle stackMark, ObjectHandleOnStack retType);

    [SecuritySafeCritical]
    internal static RuntimeType GetCallerType(ref StackCrawlMark stackMark)
    {
      RuntimeType o = (RuntimeType) null;
      RuntimeMethodHandle.GetCallerType(JitHelpers.GetStackCrawlMarkHandle(ref stackMark), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
      return o;
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern MethodBody GetMethodBody(IRuntimeMethodInfo method, RuntimeType declaringType);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsConstructor(RuntimeMethodHandleInternal method);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern LoaderAllocator GetLoaderAllocator(RuntimeMethodHandleInternal method);
  }
}

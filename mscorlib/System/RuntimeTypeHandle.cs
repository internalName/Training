// Decompiled with JetBrains decompiler
// Type: System.RuntimeTypeHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

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
  ///   Представляет тип, в котором используется внутренний маркер метаданных.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public struct RuntimeTypeHandle : ISerializable
  {
    private RuntimeType m_type;

    internal RuntimeTypeHandle GetNativeHandle()
    {
      RuntimeType type = this.m_type;
      if (type == (RuntimeType) null)
        throw new ArgumentNullException((string) null, Environment.GetResourceString("Arg_InvalidHandle"));
      return new RuntimeTypeHandle(type);
    }

    internal RuntimeType GetTypeChecked()
    {
      RuntimeType type = this.m_type;
      if (type == (RuntimeType) null)
        throw new ArgumentNullException((string) null, Environment.GetResourceString("Arg_InvalidHandle"));
      return type;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsInstanceOfType(RuntimeType type, object o);

    [SecuritySafeCritical]
    internal static unsafe Type GetTypeHelper(Type typeStart, Type[] genericArgs, IntPtr pModifiers, int cModifiers)
    {
      Type type = typeStart;
      if (genericArgs != null)
        type = type.MakeGenericType(genericArgs);
      if (cModifiers > 0)
      {
        int* pointer = (int*) pModifiers.ToPointer();
        for (int index = 0; index < cModifiers; ++index)
          type = (byte) Marshal.ReadInt32((IntPtr) ((void*) pointer), index * 4) != (byte) 15 ? ((byte) Marshal.ReadInt32((IntPtr) ((void*) pointer), index * 4) != (byte) 16 ? ((byte) Marshal.ReadInt32((IntPtr) ((void*) pointer), index * 4) != (byte) 29 ? type.MakeArrayType(Marshal.ReadInt32((IntPtr) ((void*) pointer), ++index * 4)) : type.MakeArrayType()) : type.MakeByRefType()) : type.MakePointerType();
      }
      return type;
    }

    /// <summary>
    ///   Указывает, является ли <see cref="T:System.RuntimeTypeHandle" /> структуры равен объекту.
    /// </summary>
    /// <param name="left">
    ///   A <see cref="T:System.RuntimeTypeHandle" /> Структура для сравнения <paramref name="right" />.
    /// </param>
    /// <param name="right">
    ///   Объект, сравниваемый с <paramref name="left" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="right" /> является <see cref="T:System.RuntimeTypeHandle" /> и равняется <paramref name="left" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(RuntimeTypeHandle left, object right)
    {
      return left.Equals(right);
    }

    /// <summary>
    ///   Указывает, является ли объект и <see cref="T:System.RuntimeTypeHandle" /> структуры не равны.
    /// </summary>
    /// <param name="left">
    ///   Объект, сравниваемый с <paramref name="right" />.
    /// </param>
    /// <param name="right">
    ///   A <see cref="T:System.RuntimeTypeHandle" /> Структура для сравнения <paramref name="left" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="left" /> является <see cref="T:System.RuntimeTypeHandle" /> структуры и равен <paramref name="right" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator ==(object left, RuntimeTypeHandle right)
    {
      return right.Equals(left);
    }

    /// <summary>
    ///   Указывает, является ли <see cref="T:System.RuntimeTypeHandle" /> Структура не равен объекту.
    /// </summary>
    /// <param name="left">
    ///   A <see cref="T:System.RuntimeTypeHandle" /> Структура для сравнения <paramref name="right" />.
    /// </param>
    /// <param name="right">
    ///   Объект, сравниваемый с <paramref name="left" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="right" /> является <see cref="T:System.RuntimeTypeHandle" /> структуры и не равно <paramref name="left" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(RuntimeTypeHandle left, object right)
    {
      return !left.Equals(right);
    }

    /// <summary>
    ///   Указывает, является ли объект и <see cref="T:System.RuntimeTypeHandle" /> структуры не равны.
    /// </summary>
    /// <param name="left">
    ///   Объект, сравниваемый с <paramref name="right" />.
    /// </param>
    /// <param name="right">
    ///   A <see cref="T:System.RuntimeTypeHandle" /> Структура для сравнения <paramref name="left" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="left" /> является <see cref="T:System.RuntimeTypeHandle" /> и не равно <paramref name="right" />; в противном случае — <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static bool operator !=(object left, RuntimeTypeHandle right)
    {
      return !right.Equals(left);
    }

    internal static RuntimeTypeHandle EmptyHandle
    {
      get
      {
        return new RuntimeTypeHandle((RuntimeType) null);
      }
    }

    /// <summary>Возвращает хэш-код текущего экземпляра.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    [__DynamicallyInvokable]
    public override int GetHashCode()
    {
      if (!(this.m_type != (RuntimeType) null))
        return 0;
      return this.m_type.GetHashCode();
    }

    /// <summary>
    ///   Указывает, равен ли указанный объект текущему <see cref="T:System.RuntimeTypeHandle" /> структуры.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="obj" /> является <see cref="T:System.RuntimeTypeHandle" /> структуры и равен значению данного экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public override bool Equals(object obj)
    {
      if (!(obj is RuntimeTypeHandle))
        return false;
      return ((RuntimeTypeHandle) obj).m_type == this.m_type;
    }

    /// <summary>
    ///   Указывает ли указанный <see cref="T:System.RuntimeTypeHandle" /> структуры равен текущему объекту <see cref="T:System.RuntimeTypeHandle" /> структуры.
    /// </summary>
    /// <param name="handle">
    ///   <see cref="T:System.RuntimeTypeHandle" /> Структура для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если значение <paramref name="handle" /> равно значению данного экземпляра; в противном случае, <see langword="false" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public bool Equals(RuntimeTypeHandle handle)
    {
      return handle.m_type == this.m_type;
    }

    /// <summary>
    ///   Возвращает дескриптор типа, представленного этим экземпляром.
    /// </summary>
    /// <returns>Дескриптор типа, представленного этим экземпляром.</returns>
    public IntPtr Value
    {
      [SecurityCritical] get
      {
        if (!(this.m_type != (RuntimeType) null))
          return IntPtr.Zero;
        return this.m_type.m_handle;
      }
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IntPtr GetValueInternal(RuntimeTypeHandle handle);

    internal RuntimeTypeHandle(RuntimeType type)
    {
      this.m_type = type;
    }

    internal bool IsNullHandle()
    {
      return this.m_type == (RuntimeType) null;
    }

    [SecuritySafeCritical]
    internal static bool IsPrimitive(RuntimeType type)
    {
      CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
      if ((corElementType < CorElementType.Boolean || corElementType > CorElementType.R8) && corElementType != CorElementType.I)
        return corElementType == CorElementType.U;
      return true;
    }

    [SecuritySafeCritical]
    internal static bool IsByRef(RuntimeType type)
    {
      return RuntimeTypeHandle.GetCorElementType(type) == CorElementType.ByRef;
    }

    [SecuritySafeCritical]
    internal static bool IsPointer(RuntimeType type)
    {
      return RuntimeTypeHandle.GetCorElementType(type) == CorElementType.Ptr;
    }

    [SecuritySafeCritical]
    internal static bool IsArray(RuntimeType type)
    {
      CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
      if (corElementType != CorElementType.Array)
        return corElementType == CorElementType.SzArray;
      return true;
    }

    [SecuritySafeCritical]
    internal static bool IsSzArray(RuntimeType type)
    {
      return RuntimeTypeHandle.GetCorElementType(type) == CorElementType.SzArray;
    }

    [SecuritySafeCritical]
    internal static bool HasElementType(RuntimeType type)
    {
      CorElementType corElementType = RuntimeTypeHandle.GetCorElementType(type);
      switch (corElementType)
      {
        case CorElementType.Ptr:
        case CorElementType.Array:
        case CorElementType.SzArray:
          return true;
        default:
          return corElementType == CorElementType.ByRef;
      }
    }

    [SecurityCritical]
    internal static IntPtr[] CopyRuntimeTypeHandles(RuntimeTypeHandle[] inHandles, out int length)
    {
      if (inHandles == null || inHandles.Length == 0)
      {
        length = 0;
        return (IntPtr[]) null;
      }
      IntPtr[] numArray = new IntPtr[inHandles.Length];
      for (int index = 0; index < inHandles.Length; ++index)
        numArray[index] = inHandles[index].Value;
      length = numArray.Length;
      return numArray;
    }

    [SecurityCritical]
    internal static IntPtr[] CopyRuntimeTypeHandles(Type[] inHandles, out int length)
    {
      if (inHandles == null || inHandles.Length == 0)
      {
        length = 0;
        return (IntPtr[]) null;
      }
      IntPtr[] numArray = new IntPtr[inHandles.Length];
      for (int index = 0; index < inHandles.Length; ++index)
        numArray[index] = inHandles[index].GetTypeHandleInternal().Value;
      length = numArray.Length;
      return numArray;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object CreateInstance(RuntimeType type, bool publicOnly, bool noCheck, ref bool canBeCached, ref RuntimeMethodHandleInternal ctor, ref bool bNeedSecurityCheck);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object CreateCaInstance(RuntimeType type, IRuntimeMethodInfo ctor);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object Allocate(RuntimeType type);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern object CreateInstanceForAnotherGenericParameter(RuntimeType type, RuntimeType genericParameter);

    internal RuntimeType GetRuntimeType()
    {
      return this.m_type;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern CorElementType GetCorElementType(RuntimeType type);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeAssembly GetAssembly(RuntimeType type);

    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeModule GetModule(RuntimeType type);

    /// <summary>
    ///   Возвращает дескриптор модуля, содержащего тип, представленный текущим экземпляром.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.ModuleHandle" /> Структура, представляющая дескриптор модуля, содержащего тип, представленный текущим экземпляром.
    /// </returns>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public ModuleHandle GetModuleHandle()
    {
      return new ModuleHandle(RuntimeTypeHandle.GetModule(this.m_type));
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeType GetBaseType(RuntimeType type);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern TypeAttributes GetAttributes(RuntimeType type);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeType GetElementType(RuntimeType type);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool CompareCanonicalHandles(RuntimeType left, RuntimeType right);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetArrayRank(RuntimeType type);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetToken(RuntimeType type);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeMethodHandleInternal GetMethodAt(RuntimeType type, int slot);

    internal static RuntimeTypeHandle.IntroducedMethodEnumerator GetIntroducedMethods(RuntimeType type)
    {
      return new RuntimeTypeHandle.IntroducedMethodEnumerator(type);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern RuntimeMethodHandleInternal GetFirstIntroducedMethod(RuntimeType type);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void GetNextIntroducedMethod(ref RuntimeMethodHandleInternal method);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern unsafe bool GetFields(RuntimeType type, IntPtr* result, int* count);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern Type[] GetInterfaces(RuntimeType type);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetConstraints(RuntimeTypeHandle handle, ObjectHandleOnStack types);

    [SecuritySafeCritical]
    internal Type[] GetConstraints()
    {
      Type[] o = (Type[]) null;
      RuntimeTypeHandle.GetConstraints(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<Type[]>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern IntPtr GetGCHandle(RuntimeTypeHandle handle, GCHandleType type);

    [SecurityCritical]
    internal IntPtr GetGCHandle(GCHandleType type)
    {
      return RuntimeTypeHandle.GetGCHandle(this.GetNativeHandle(), type);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetNumVirtuals(RuntimeType type);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void VerifyInterfaceIsImplemented(RuntimeTypeHandle handle, RuntimeTypeHandle interfaceHandle);

    [SecuritySafeCritical]
    internal void VerifyInterfaceIsImplemented(RuntimeTypeHandle interfaceHandle)
    {
      RuntimeTypeHandle.VerifyInterfaceIsImplemented(this.GetNativeHandle(), interfaceHandle.GetNativeHandle());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern int GetInterfaceMethodImplementationSlot(RuntimeTypeHandle handle, RuntimeTypeHandle interfaceHandle, RuntimeMethodHandleInternal interfaceMethodHandle);

    [SecuritySafeCritical]
    internal int GetInterfaceMethodImplementationSlot(RuntimeTypeHandle interfaceHandle, RuntimeMethodHandleInternal interfaceMethodHandle)
    {
      return RuntimeTypeHandle.GetInterfaceMethodImplementationSlot(this.GetNativeHandle(), interfaceHandle.GetNativeHandle(), interfaceMethodHandle);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsComObject(RuntimeType type, bool isGenericCOM);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsContextful(RuntimeType type);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsInterface(RuntimeType type);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool _IsVisible(RuntimeTypeHandle typeHandle);

    [SecuritySafeCritical]
    internal static bool IsVisible(RuntimeType type)
    {
      return RuntimeTypeHandle._IsVisible(new RuntimeTypeHandle(type));
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsSecurityCritical(RuntimeTypeHandle typeHandle);

    [SecuritySafeCritical]
    internal bool IsSecurityCritical()
    {
      return RuntimeTypeHandle.IsSecurityCritical(this.GetNativeHandle());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsSecuritySafeCritical(RuntimeTypeHandle typeHandle);

    [SecuritySafeCritical]
    internal bool IsSecuritySafeCritical()
    {
      return RuntimeTypeHandle.IsSecuritySafeCritical(this.GetNativeHandle());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool IsSecurityTransparent(RuntimeTypeHandle typeHandle);

    [SecuritySafeCritical]
    internal bool IsSecurityTransparent()
    {
      return RuntimeTypeHandle.IsSecurityTransparent(this.GetNativeHandle());
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool HasProxyAttribute(RuntimeType type);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsValueType(RuntimeType type);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void ConstructName(RuntimeTypeHandle handle, TypeNameFormatFlags formatFlags, StringHandleOnStack retString);

    [SecuritySafeCritical]
    internal string ConstructName(TypeNameFormatFlags formatFlags)
    {
      string s = (string) null;
      RuntimeTypeHandle.ConstructName(this.GetNativeHandle(), formatFlags, JitHelpers.GetStringHandleOnStack(ref s));
      return s;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe void* _GetUtf8Name(RuntimeType type);

    [SecuritySafeCritical]
    internal static unsafe Utf8String GetUtf8Name(RuntimeType type)
    {
      return new Utf8String(RuntimeTypeHandle._GetUtf8Name(type));
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool CanCastTo(RuntimeType type, RuntimeType target);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern RuntimeType GetDeclaringType(RuntimeType type);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IRuntimeMethodInfo GetDeclaringMethod(RuntimeType type);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetDefaultConstructor(RuntimeTypeHandle handle, ObjectHandleOnStack method);

    [SecuritySafeCritical]
    internal IRuntimeMethodInfo GetDefaultConstructor()
    {
      IRuntimeMethodInfo o = (IRuntimeMethodInfo) null;
      RuntimeTypeHandle.GetDefaultConstructor(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<IRuntimeMethodInfo>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetTypeByName(string name, bool throwOnError, bool ignoreCase, bool reflectionOnly, StackCrawlMarkHandle stackMark, IntPtr pPrivHostBinder, bool loadTypeFromPartialName, ObjectHandleOnStack type);

    internal static RuntimeType GetTypeByName(string name, bool throwOnError, bool ignoreCase, bool reflectionOnly, ref StackCrawlMark stackMark, bool loadTypeFromPartialName)
    {
      return RuntimeTypeHandle.GetTypeByName(name, throwOnError, ignoreCase, reflectionOnly, ref stackMark, IntPtr.Zero, loadTypeFromPartialName);
    }

    [SecuritySafeCritical]
    internal static RuntimeType GetTypeByName(string name, bool throwOnError, bool ignoreCase, bool reflectionOnly, ref StackCrawlMark stackMark, IntPtr pPrivHostBinder, bool loadTypeFromPartialName)
    {
      if (name == null || name.Length == 0)
      {
        if (throwOnError)
          throw new TypeLoadException(Environment.GetResourceString("Arg_TypeLoadNullStr"));
        return (RuntimeType) null;
      }
      RuntimeType o = (RuntimeType) null;
      RuntimeTypeHandle.GetTypeByName(name, throwOnError, ignoreCase, reflectionOnly, JitHelpers.GetStackCrawlMarkHandle(ref stackMark), pPrivHostBinder, loadTypeFromPartialName, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
      return o;
    }

    internal static Type GetTypeByName(string name, ref StackCrawlMark stackMark)
    {
      return (Type) RuntimeTypeHandle.GetTypeByName(name, false, false, false, ref stackMark, false);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetTypeByNameUsingCARules(string name, RuntimeModule scope, ObjectHandleOnStack type);

    [SecuritySafeCritical]
    internal static RuntimeType GetTypeByNameUsingCARules(string name, RuntimeModule scope)
    {
      if (name == null || name.Length == 0)
        throw new ArgumentException(nameof (name));
      RuntimeType o = (RuntimeType) null;
      RuntimeTypeHandle.GetTypeByNameUsingCARules(name, scope.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void GetInstantiation(RuntimeTypeHandle type, ObjectHandleOnStack types, bool fAsRuntimeTypeArray);

    [SecuritySafeCritical]
    internal RuntimeType[] GetInstantiationInternal()
    {
      RuntimeType[] o = (RuntimeType[]) null;
      RuntimeTypeHandle.GetInstantiation(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType[]>(ref o), true);
      return o;
    }

    [SecuritySafeCritical]
    internal Type[] GetInstantiationPublic()
    {
      Type[] o = (Type[]) null;
      RuntimeTypeHandle.GetInstantiation(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<Type[]>(ref o), false);
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern unsafe void Instantiate(RuntimeTypeHandle handle, IntPtr* pInst, int numGenericArgs, ObjectHandleOnStack type);

    [SecurityCritical]
    internal unsafe RuntimeType Instantiate(Type[] inst)
    {
      int length;
      fixed (IntPtr* pInst = RuntimeTypeHandle.CopyRuntimeTypeHandles(inst, out length))
      {
        RuntimeType o = (RuntimeType) null;
        RuntimeTypeHandle.Instantiate(this.GetNativeHandle(), pInst, length, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
        GC.KeepAlive((object) inst);
        return o;
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void MakeArray(RuntimeTypeHandle handle, int rank, ObjectHandleOnStack type);

    [SecuritySafeCritical]
    internal RuntimeType MakeArray(int rank)
    {
      RuntimeType o = (RuntimeType) null;
      RuntimeTypeHandle.MakeArray(this.GetNativeHandle(), rank, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void MakeSZArray(RuntimeTypeHandle handle, ObjectHandleOnStack type);

    [SecuritySafeCritical]
    internal RuntimeType MakeSZArray()
    {
      RuntimeType o = (RuntimeType) null;
      RuntimeTypeHandle.MakeSZArray(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void MakeByRef(RuntimeTypeHandle handle, ObjectHandleOnStack type);

    [SecuritySafeCritical]
    internal RuntimeType MakeByRef()
    {
      RuntimeType o = (RuntimeType) null;
      RuntimeTypeHandle.MakeByRef(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void MakePointer(RuntimeTypeHandle handle, ObjectHandleOnStack type);

    [SecurityCritical]
    internal RuntimeType MakePointer()
    {
      RuntimeType o = (RuntimeType) null;
      RuntimeTypeHandle.MakePointer(this.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern bool IsCollectible(RuntimeTypeHandle handle);

    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool HasInstantiation(RuntimeType type);

    internal bool HasInstantiation()
    {
      return RuntimeTypeHandle.HasInstantiation(this.GetTypeChecked());
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetGenericTypeDefinition(RuntimeTypeHandle type, ObjectHandleOnStack retType);

    [SecuritySafeCritical]
    internal static RuntimeType GetGenericTypeDefinition(RuntimeType type)
    {
      RuntimeType o = type;
      if (RuntimeTypeHandle.HasInstantiation(o) && !RuntimeTypeHandle.IsGenericTypeDefinition(o))
        RuntimeTypeHandle.GetGenericTypeDefinition(o.GetTypeHandleInternal(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
      return o;
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsGenericTypeDefinition(RuntimeType type);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsGenericVariable(RuntimeType type);

    internal bool IsGenericVariable()
    {
      return RuntimeTypeHandle.IsGenericVariable(this.GetTypeChecked());
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern int GetGenericVariableIndex(RuntimeType type);

    [SecuritySafeCritical]
    internal int GetGenericVariableIndex()
    {
      RuntimeType typeChecked = this.GetTypeChecked();
      if (!RuntimeTypeHandle.IsGenericVariable(typeChecked))
        throw new InvalidOperationException(Environment.GetResourceString("Arg_NotGenericParameter"));
      return RuntimeTypeHandle.GetGenericVariableIndex(typeChecked);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool ContainsGenericVariables(RuntimeType handle);

    [SecuritySafeCritical]
    internal bool ContainsGenericVariables()
    {
      return RuntimeTypeHandle.ContainsGenericVariables(this.GetTypeChecked());
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe bool SatisfiesConstraints(RuntimeType paramType, IntPtr* pTypeContext, int typeContextLength, IntPtr* pMethodContext, int methodContextLength, RuntimeType toType);

    [SecurityCritical]
    internal static unsafe bool SatisfiesConstraints(RuntimeType paramType, RuntimeType[] typeContext, RuntimeType[] methodContext, RuntimeType toType)
    {
      int length1;
      IntPtr[] numArray1 = RuntimeTypeHandle.CopyRuntimeTypeHandles((Type[]) typeContext, out length1);
      int length2;
      IntPtr[] numArray2 = RuntimeTypeHandle.CopyRuntimeTypeHandles((Type[]) methodContext, out length2);
      fixed (IntPtr* pTypeContext = numArray1)
        fixed (IntPtr* pMethodContext = numArray2)
        {
          bool flag = RuntimeTypeHandle.SatisfiesConstraints(paramType, pTypeContext, length1, pMethodContext, length2, toType);
          GC.KeepAlive((object) typeContext);
          GC.KeepAlive((object) methodContext);
          return flag;
        }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern IntPtr _GetMetadataImport(RuntimeType type);

    [SecurityCritical]
    internal static MetadataImport GetMetadataImport(RuntimeType type)
    {
      return new MetadataImport(RuntimeTypeHandle._GetMetadataImport(type), (object) type);
    }

    [SecurityCritical]
    private RuntimeTypeHandle(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      this.m_type = (RuntimeType) info.GetValue("TypeObj", typeof (RuntimeType));
      if (this.m_type == (RuntimeType) null)
        throw new SerializationException(Environment.GetResourceString("Serialization_InsufficientState"));
    }

    /// <summary>
    ///   Заполняет <see cref="T:System.Runtime.Serialization.SerializationInfo" /> с данными, необходимыми для десериализации типа, представленного текущим экземпляром.
    /// </summary>
    /// <param name="info">
    ///   Объект, для которого будут заполнены сведения о сериализации.
    /// </param>
    /// <param name="context">
    ///   (Зарезервировано) Расположение, где хранятся и откуда извлекаются сериализованные данные.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="info" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   <see cref="P:System.RuntimeTypeHandle.Value" /> недопустим.
    /// </exception>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      if (this.m_type == (RuntimeType) null)
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidFieldState"));
      info.AddValue("TypeObj", (object) this.m_type, typeof (RuntimeType));
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsEquivalentTo(RuntimeType rtType1, RuntimeType rtType2);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool IsEquivalentType(RuntimeType type);

    internal struct IntroducedMethodEnumerator
    {
      private bool _firstCall;
      private RuntimeMethodHandleInternal _handle;

      [SecuritySafeCritical]
      internal IntroducedMethodEnumerator(RuntimeType type)
      {
        this._handle = RuntimeTypeHandle.GetFirstIntroducedMethod(type);
        this._firstCall = true;
      }

      [SecuritySafeCritical]
      public bool MoveNext()
      {
        if (this._firstCall)
          this._firstCall = false;
        else if (this._handle.Value != IntPtr.Zero)
          RuntimeTypeHandle.GetNextIntroducedMethod(ref this._handle);
        return !(this._handle.Value == IntPtr.Zero);
      }

      public RuntimeMethodHandleInternal Current
      {
        get
        {
          return this._handle;
        }
      }

      public RuntimeTypeHandle.IntroducedMethodEnumerator GetEnumerator()
      {
        return this;
      }
    }
  }
}

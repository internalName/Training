// Decompiled with JetBrains decompiler
// Type: System.ModuleHandle
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
  /// <summary>
  ///   Представляет дескриптор среды выполнения для модуля.
  /// </summary>
  [ComVisible(true)]
  public struct ModuleHandle
  {
    /// <summary>Представляет дескриптор пустого модуля.</summary>
    public static readonly ModuleHandle EmptyHandle = ModuleHandle.GetEmptyMH();
    private RuntimeModule m_ptr;

    private static ModuleHandle GetEmptyMH()
    {
      return new ModuleHandle();
    }

    internal ModuleHandle(RuntimeModule module)
    {
      this.m_ptr = module;
    }

    internal RuntimeModule GetRuntimeModule()
    {
      return this.m_ptr;
    }

    internal bool IsNullHandle()
    {
      return (Module) this.m_ptr == (Module) null;
    }

    /// <summary>Возвращает хэш-код данного экземпляра.</summary>
    /// <returns>
    ///   32-разрядное целое число со знаком, являющееся хэш-кодом для данного экземпляра.
    /// </returns>
    public override int GetHashCode()
    {
      if (!((Module) this.m_ptr != (Module) null))
        return 0;
      return this.m_ptr.GetHashCode();
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Boolean" /> значение, указывающее, является ли указанный объект <see cref="T:System.ModuleHandle" /> структуры и равен текущему <see cref="T:System.ModuleHandle" />.
    /// </summary>
    /// <param name="obj">
    ///   Объект для сравнения с текущим <see cref="T:System.ModuleHandle" /> структуры.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="obj" /> является <see cref="T:System.ModuleHandle" /> структуры и равен текущему объекту <see cref="T:System.ModuleHandle" /> структуры; в противном случае — <see langword="false" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public override bool Equals(object obj)
    {
      if (!(obj is ModuleHandle))
        return false;
      return (Module) ((ModuleHandle) obj).m_ptr == (Module) this.m_ptr;
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Boolean" /> значение, указывающее, является ли указанный <see cref="T:System.ModuleHandle" /> структуры равен текущему объекту <see cref="T:System.ModuleHandle" />.
    /// </summary>
    /// <param name="handle">
    ///   <see cref="T:System.ModuleHandle" /> Структура для сравнения с текущим <see cref="T:System.ModuleHandle" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="handle" /> равно текущему <see cref="T:System.ModuleHandle" /> структуры; в противном случае <see langword="false" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    public bool Equals(ModuleHandle handle)
    {
      return (Module) handle.m_ptr == (Module) this.m_ptr;
    }

    /// <summary>
    ///   Проверяет равенство двух <see cref="T:System.ModuleHandle" /> структуры равны.
    /// </summary>
    /// <param name="left">
    ///   <see cref="T:System.ModuleHandle" /> Структуры слева от оператора равенства.
    /// </param>
    /// <param name="right">
    ///   <see cref="T:System.ModuleHandle" /> Структура справа от оператора равенства.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="T:System.ModuleHandle" /> структуры являются равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator ==(ModuleHandle left, ModuleHandle right)
    {
      return left.Equals(right);
    }

    /// <summary>
    ///   Проверяет равенство двух <see cref="T:System.ModuleHandle" /> структуры не равны.
    /// </summary>
    /// <param name="left">
    ///   <see cref="T:System.ModuleHandle" /> Структуры слева от оператора неравенства.
    /// </param>
    /// <param name="right">
    ///   <see cref="T:System.ModuleHandle" /> Структура справа от оператора неравенства.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="T:System.ModuleHandle" /> структуры не совпадают; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator !=(ModuleHandle left, ModuleHandle right)
    {
      return !left.Equals(right);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern IRuntimeMethodInfo GetDynamicMethod(DynamicMethod method, RuntimeModule module, string name, byte[] sig, Resolver resolver);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetToken(RuntimeModule module);

    private static void ValidateModulePointer(RuntimeModule module)
    {
      if ((Module) module == (Module) null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NullModuleHandle"));
    }

    /// <summary>
    ///   Возвращает дескриптор типа среды выполнения для типа, который определяется заданным токеном метаданных.
    /// </summary>
    /// <param name="typeToken">
    ///   Токен метаданных, определяющий тип в модуле.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.RuntimeTypeHandle" /> для типа, определяемого <paramref name="typeToken" />.
    /// </returns>
    public RuntimeTypeHandle GetRuntimeTypeHandleFromMetadataToken(int typeToken)
    {
      return this.ResolveTypeHandle(typeToken);
    }

    /// <summary>
    ///   Возвращает дескриптор типа среды выполнения для типа, который определяется заданным токеном метаданных.
    /// </summary>
    /// <param name="typeToken">
    ///   Токен метаданных, определяющий тип в модуле.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.RuntimeTypeHandle" /> для типа, определяемого <paramref name="typeToken" />.
    /// </returns>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="typeToken" /> не является допустимым маркером метаданных для типа в текущем модуле.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> не является маркером для типа в области текущего модуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> — <see langword="TypeSpec" /> сигнатура которого содержит тип элемента <see langword="var" /> или <see langword="mvar" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Метод вызывается для пустой тип дескриптора.
    /// </exception>
    public RuntimeTypeHandle ResolveTypeHandle(int typeToken)
    {
      return new RuntimeTypeHandle(ModuleHandle.ResolveTypeHandleInternal(this.GetRuntimeModule(), typeToken, (RuntimeTypeHandle[]) null, (RuntimeTypeHandle[]) null));
    }

    /// <summary>
    ///   Возвращает дескриптор типа среды выполнения для тип, определенный заданным токеном метаданных, задающим аргументы универсального типа и метод, в котором токен остается в области.
    /// </summary>
    /// <param name="typeToken">
    ///   Токен метаданных, определяющий тип в модуле.
    /// </param>
    /// <param name="typeInstantiationContext">
    ///   Массив <see cref="T:System.RuntimeTypeHandle" /> структуры, представляющих аргументы универсального типа для типа, если маркер находится в области видимости, или <see langword="null" /> Если этот тип не является универсальным.
    /// </param>
    /// <param name="methodInstantiationContext">
    ///   Массив <see cref="T:System.RuntimeTypeHandle" /> структуры объектов, представляющих аргументы универсального типа для метода, если маркер находится в области видимости, или <see langword="null" /> Если этот метод не является универсальным.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.RuntimeTypeHandle" /> для типа, определяемого <paramref name="typeToken" />.
    /// </returns>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="typeToken" /> не является допустимым маркером метаданных для типа в текущем модуле.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> не является маркером для типа в области текущего модуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> — <see langword="TypeSpec" /> сигнатура которого содержит тип элемента <see langword="var" /> или <see langword="mvar" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Метод вызывается для пустой тип дескриптора.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="typeToken " />не является допустимым маркером.
    /// </exception>
    public RuntimeTypeHandle ResolveTypeHandle(int typeToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
    {
      return new RuntimeTypeHandle(ModuleHandle.ResolveTypeHandleInternal(this.GetRuntimeModule(), typeToken, typeInstantiationContext, methodInstantiationContext));
    }

    [SecuritySafeCritical]
    internal static unsafe RuntimeType ResolveTypeHandleInternal(RuntimeModule module, int typeToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
    {
      ModuleHandle.ValidateModulePointer(module);
      if (!ModuleHandle.GetMetadataImport(module).IsValidToken(typeToken))
        throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", (object) typeToken, (object) new ModuleHandle(module)));
      int length1;
      IntPtr[] numArray1 = RuntimeTypeHandle.CopyRuntimeTypeHandles(typeInstantiationContext, out length1);
      int length2;
      IntPtr[] numArray2 = RuntimeTypeHandle.CopyRuntimeTypeHandles(methodInstantiationContext, out length2);
      fixed (IntPtr* typeInstArgs = numArray1)
        fixed (IntPtr* methodInstArgs = numArray2)
        {
          RuntimeType o = (RuntimeType) null;
          ModuleHandle.ResolveType(module, typeToken, typeInstArgs, length1, methodInstArgs, length2, JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
          GC.KeepAlive((object) typeInstantiationContext);
          GC.KeepAlive((object) methodInstantiationContext);
          return o;
        }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern unsafe void ResolveType(RuntimeModule module, int typeToken, IntPtr* typeInstArgs, int typeInstCount, IntPtr* methodInstArgs, int methodInstCount, ObjectHandleOnStack type);

    /// <summary>
    ///   Возвращает дескриптор метода среды выполнения для метода или конструктор, определенный заданным токеном метаданных.
    /// </summary>
    /// <param name="methodToken">
    ///   Токен метаданных, определяющий метод или конструктор в модуле.
    /// </param>
    /// <returns>
    ///   A <see cref="T:System.RuntimeMethodHandle" /> метод или конструктор, определенный <paramref name="methodToken" />.
    /// </returns>
    public RuntimeMethodHandle GetRuntimeMethodHandleFromMetadataToken(int methodToken)
    {
      return this.ResolveMethodHandle(methodToken);
    }

    /// <summary>
    ///   Возвращает дескриптор метода среды выполнения для метода или конструктор, определенный заданным токеном метаданных.
    /// </summary>
    /// <param name="methodToken">
    ///   Токен метаданных, определяющий метод или конструктор в модуле.
    /// </param>
    /// <returns>
    ///   A <see cref="T:System.RuntimeMethodHandle" /> метод или конструктор, определенный <paramref name="methodToken" />.
    /// </returns>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="methodToken" /> не является допустимым маркером метаданных для метода в текущем модуле.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> не является маркером для метода или конструктора в области текущего модуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> — <see langword="MethodSpec" /> сигнатура которого содержит тип элемента <see langword="var" /> или <see langword="mvar" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Метод вызывается для дескриптора пустого метода.
    /// </exception>
    public RuntimeMethodHandle ResolveMethodHandle(int methodToken)
    {
      return this.ResolveMethodHandle(methodToken, (RuntimeTypeHandle[]) null, (RuntimeTypeHandle[]) null);
    }

    internal static IRuntimeMethodInfo ResolveMethodHandleInternal(RuntimeModule module, int methodToken)
    {
      return ModuleHandle.ResolveMethodHandleInternal(module, methodToken, (RuntimeTypeHandle[]) null, (RuntimeTypeHandle[]) null);
    }

    /// <summary>
    ///   Возвращает дескриптор метода среды выполнения для метода или конструктор, определенный заданным маркером метаданных, задающим аргументы универсального типа и метод, в котором токен остается в области.
    /// </summary>
    /// <param name="methodToken">
    ///   Токен метаданных, определяющий метод или конструктор в модуле.
    /// </param>
    /// <param name="typeInstantiationContext">
    ///   Массив <see cref="T:System.RuntimeTypeHandle" /> структуры, представляющих аргументы универсального типа для типа, если маркер находится в области видимости, или <see langword="null" /> Если этот тип не является универсальным.
    /// </param>
    /// <param name="methodInstantiationContext">
    ///   Массив <see cref="T:System.RuntimeTypeHandle" /> структуры, представляющих аргументы универсального типа для метода, если маркер находится в области видимости, или <see langword="null" /> Если этот метод не является универсальным.
    /// </param>
    /// <returns>
    ///   A <see cref="T:System.RuntimeMethodHandle" /> метод или конструктор, определенный <paramref name="methodToken" />.
    /// </returns>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="methodToken" /> не является допустимым маркером метаданных для метода в текущем модуле.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> не является маркером для метода или конструктора в области текущего модуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> — <see langword="MethodSpec" /> сигнатура которого содержит тип элемента <see langword="var" /> или <see langword="mvar" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Метод вызывается для дескриптора пустого метода.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="methodToken " />не является допустимым маркером.
    /// </exception>
    public RuntimeMethodHandle ResolveMethodHandle(int methodToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
    {
      return new RuntimeMethodHandle(ModuleHandle.ResolveMethodHandleInternal(this.GetRuntimeModule(), methodToken, typeInstantiationContext, methodInstantiationContext));
    }

    [SecuritySafeCritical]
    internal static IRuntimeMethodInfo ResolveMethodHandleInternal(RuntimeModule module, int methodToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
    {
      int length1;
      IntPtr[] typeInstantiationContext1 = RuntimeTypeHandle.CopyRuntimeTypeHandles(typeInstantiationContext, out length1);
      int length2;
      IntPtr[] methodInstantiationContext1 = RuntimeTypeHandle.CopyRuntimeTypeHandles(methodInstantiationContext, out length2);
      RuntimeMethodHandleInternal methodHandleInternal = ModuleHandle.ResolveMethodHandleInternalCore(module, methodToken, typeInstantiationContext1, length1, methodInstantiationContext1, length2);
      IRuntimeMethodInfo runtimeMethodInfo = (IRuntimeMethodInfo) new RuntimeMethodInfoStub(methodHandleInternal, (object) RuntimeMethodHandle.GetLoaderAllocator(methodHandleInternal));
      GC.KeepAlive((object) typeInstantiationContext);
      GC.KeepAlive((object) methodInstantiationContext);
      return runtimeMethodInfo;
    }

    [SecurityCritical]
    internal static unsafe RuntimeMethodHandleInternal ResolveMethodHandleInternalCore(RuntimeModule module, int methodToken, IntPtr[] typeInstantiationContext, int typeInstCount, IntPtr[] methodInstantiationContext, int methodInstCount)
    {
      ModuleHandle.ValidateModulePointer(module);
      if (!ModuleHandle.GetMetadataImport(module.GetNativeHandle()).IsValidToken(methodToken))
        throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", (object) methodToken, (object) new ModuleHandle(module)));
      fixed (IntPtr* typeInstArgs = typeInstantiationContext)
        fixed (IntPtr* methodInstArgs = methodInstantiationContext)
          return ModuleHandle.ResolveMethod(module.GetNativeHandle(), methodToken, typeInstArgs, typeInstCount, methodInstArgs, methodInstCount);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern unsafe RuntimeMethodHandleInternal ResolveMethod(RuntimeModule module, int methodToken, IntPtr* typeInstArgs, int typeInstCount, IntPtr* methodInstArgs, int methodInstCount);

    /// <summary>
    ///   Возвращает дескриптор среды выполнения для поля, определенного указанным токеном метаданных.
    /// </summary>
    /// <param name="fieldToken">
    ///   Токен метаданных, определяющий поле в модуле.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.RuntimeFieldHandle" /> для поля, определяемого по <paramref name="fieldToken" />.
    /// </returns>
    public RuntimeFieldHandle GetRuntimeFieldHandleFromMetadataToken(int fieldToken)
    {
      return this.ResolveFieldHandle(fieldToken);
    }

    /// <summary>
    ///   Возвращает дескриптор среды выполнения для поля, определенного указанным токеном метаданных.
    /// </summary>
    /// <param name="fieldToken">
    ///   Токен метаданных, определяющий поле в модуле.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.RuntimeFieldHandle" /> для поля, определяемого по <paramref name="fieldToken" />.
    /// </returns>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="metadataToken" /> не является допустимым маркером в области текущего модуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> не является маркером для поля в области текущего модуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> Определяет поле, родительская <see langword="TypeSpec" /> имеет подпись, содержащую тип элемента <see langword="var" /> или <see langword="mvar" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Метод вызывается для дескриптора пустого поля.
    /// </exception>
    public RuntimeFieldHandle ResolveFieldHandle(int fieldToken)
    {
      return new RuntimeFieldHandle(ModuleHandle.ResolveFieldHandleInternal(this.GetRuntimeModule(), fieldToken, (RuntimeTypeHandle[]) null, (RuntimeTypeHandle[]) null));
    }

    /// <summary>
    ///   Возвращает дескриптор поля среды выполнения для поля, определенного указанным маркером метаданных, задающим аргументы универсального типа и метод, в котором токен остается в области.
    /// </summary>
    /// <param name="fieldToken">
    ///   Токен метаданных, определяющий поле в модуле.
    /// </param>
    /// <param name="typeInstantiationContext">
    ///   Массив <see cref="T:System.RuntimeTypeHandle" /> структуры, представляющих аргументы универсального типа для типа, если маркер находится в области видимости, или <see langword="null" /> Если этот тип не является универсальным.
    /// </param>
    /// <param name="methodInstantiationContext">
    ///   Массив <see cref="T:System.RuntimeTypeHandle" /> структуры, представляющих аргументы универсального типа для метода, если маркер находится в области видимости, или <see langword="null" /> Если этот метод не является универсальным.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.RuntimeFieldHandle" /> для поля, определяемого по <paramref name="fieldToken" />.
    /// </returns>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="metadataToken" /> не является допустимым маркером в области текущего модуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> не является маркером для поля в области текущего модуля.
    /// 
    ///   -или-
    /// 
    ///   <paramref name="metadataToken" /> Определяет поле, родительская <see langword="TypeSpec" /> имеет подпись, содержащую тип элемента <see langword="var" /> или <see langword="mvar" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Метод вызывается для дескриптора пустого поля.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="fieldToken " />не является допустимым маркером.
    /// </exception>
    public RuntimeFieldHandle ResolveFieldHandle(int fieldToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
    {
      return new RuntimeFieldHandle(ModuleHandle.ResolveFieldHandleInternal(this.GetRuntimeModule(), fieldToken, typeInstantiationContext, methodInstantiationContext));
    }

    [SecuritySafeCritical]
    internal static unsafe IRuntimeFieldInfo ResolveFieldHandleInternal(RuntimeModule module, int fieldToken, RuntimeTypeHandle[] typeInstantiationContext, RuntimeTypeHandle[] methodInstantiationContext)
    {
      ModuleHandle.ValidateModulePointer(module);
      if (!ModuleHandle.GetMetadataImport(module.GetNativeHandle()).IsValidToken(fieldToken))
        throw new ArgumentOutOfRangeException("metadataToken", Environment.GetResourceString("Argument_InvalidToken", (object) fieldToken, (object) new ModuleHandle(module)));
      int length1;
      IntPtr[] numArray1 = RuntimeTypeHandle.CopyRuntimeTypeHandles(typeInstantiationContext, out length1);
      int length2;
      IntPtr[] numArray2 = RuntimeTypeHandle.CopyRuntimeTypeHandles(methodInstantiationContext, out length2);
      fixed (IntPtr* typeInstArgs = numArray1)
        fixed (IntPtr* methodInstArgs = numArray2)
        {
          IRuntimeFieldInfo o = (IRuntimeFieldInfo) null;
          ModuleHandle.ResolveField(module.GetNativeHandle(), fieldToken, typeInstArgs, length1, methodInstArgs, length2, JitHelpers.GetObjectHandleOnStack<IRuntimeFieldInfo>(ref o));
          GC.KeepAlive((object) typeInstantiationContext);
          GC.KeepAlive((object) methodInstantiationContext);
          return o;
        }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern unsafe void ResolveField(RuntimeModule module, int fieldToken, IntPtr* typeInstArgs, int typeInstCount, IntPtr* methodInstArgs, int methodInstCount, ObjectHandleOnStack retField);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern bool _ContainsPropertyMatchingHash(RuntimeModule module, int propertyToken, uint hash);

    [SecurityCritical]
    internal static bool ContainsPropertyMatchingHash(RuntimeModule module, int propertyToken, uint hash)
    {
      return ModuleHandle._ContainsPropertyMatchingHash(module.GetNativeHandle(), propertyToken, hash);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetAssembly(RuntimeModule handle, ObjectHandleOnStack retAssembly);

    [SecuritySafeCritical]
    internal static RuntimeAssembly GetAssembly(RuntimeModule module)
    {
      RuntimeAssembly o = (RuntimeAssembly) null;
      ModuleHandle.GetAssembly(module.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeAssembly>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void GetModuleType(RuntimeModule handle, ObjectHandleOnStack type);

    [SecuritySafeCritical]
    internal static RuntimeType GetModuleType(RuntimeModule module)
    {
      RuntimeType o = (RuntimeType) null;
      ModuleHandle.GetModuleType(module.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<RuntimeType>(ref o));
      return o;
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void GetPEKind(RuntimeModule handle, out int peKind, out int machine);

    [SecuritySafeCritical]
    internal static void GetPEKind(RuntimeModule module, out PortableExecutableKinds peKind, out ImageFileMachine machine)
    {
      int peKind1;
      int machine1;
      ModuleHandle.GetPEKind(module.GetNativeHandle(), out peKind1, out machine1);
      peKind = (PortableExecutableKinds) peKind1;
      machine = (ImageFileMachine) machine1;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern int GetMDStreamVersion(RuntimeModule module);

    /// <summary>Возвращает версию потока метаданных.</summary>
    /// <returns>
    ///   32-разрядное целое число, представляющее версию потока метаданных.
    ///    Старшие байты два представляют основной номер версии, а два байта низкого порядка дополнительный номер версии.
    /// </returns>
    public int MDStreamVersion
    {
      [SecuritySafeCritical] get
      {
        return ModuleHandle.GetMDStreamVersion(this.GetRuntimeModule().GetNativeHandle());
      }
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern IntPtr _GetMetadataImport(RuntimeModule module);

    [SecurityCritical]
    internal static MetadataImport GetMetadataImport(RuntimeModule module)
    {
      return new MetadataImport(ModuleHandle._GetMetadataImport(module.GetNativeHandle()), (object) module);
    }
  }
}

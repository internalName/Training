// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.MethodRental
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Reflection.Emit
{
  /// <summary>
  ///   Предоставляет быстрый способ замены реализации метода тело метода класса.
  /// </summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_MethodRental))]
  [ComVisible(true)]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  public sealed class MethodRental : _MethodRental
  {
    /// <summary>
    ///   Указывает, что метод должен быть just-in-time (JIT) компилируются при необходимости.
    /// </summary>
    public const int JitOnDemand = 0;
    /// <summary>
    ///   Указывает, что метод должен быть just-in-time (JIT) компилируются немедленно.
    /// </summary>
    public const int JitImmediate = 1;

    /// <summary>Меняет местами основной части метода.</summary>
    /// <param name="cls">Класс, содержащий метод.</param>
    /// <param name="methodtoken">Токен для метода.</param>
    /// <param name="rgIL">
    ///   Указатель на метод.
    ///    Он должен включать заголовок метода.
    /// </param>
    /// <param name="methodSize">Размер тело метода в байтах.</param>
    /// <param name="flags">
    ///   Флаги, управляющие заменой.
    ///    В разделе определения констант.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="cls" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Тип <paramref name="cls" /> не завершена.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="methodSize" /> меньше одного или больше, чем 4128767 (3effff в шестнадцатеричной системе).
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    [SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
    public static void SwapMethodBody(Type cls, int methodtoken, IntPtr rgIL, int methodSize, int flags)
    {
      if (methodSize <= 0 || methodSize >= 4128768)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadSizeForData"), nameof (methodSize));
      if (cls == (Type) null)
        throw new ArgumentNullException(nameof (cls));
      Module module = cls.Module;
      ModuleBuilder moduleBuilder = module as ModuleBuilder;
      InternalModuleBuilder internalModuleBuilder = !((Module) moduleBuilder != (Module) null) ? module as InternalModuleBuilder : moduleBuilder.InternalModule;
      if ((Module) internalModuleBuilder == (Module) null)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_NotDynamicModule"));
      RuntimeType runtimeType;
      if (cls is TypeBuilder)
      {
        TypeBuilder typeBuilder = (TypeBuilder) cls;
        if (!typeBuilder.IsCreated())
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_NotAllTypesAreBaked", (object) typeBuilder.Name));
        runtimeType = typeBuilder.BakedRuntimeType;
      }
      else
        runtimeType = cls as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"), nameof (cls));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      lock (internalModuleBuilder.GetRuntimeAssembly().SyncRoot)
        MethodRental.SwapMethodBody(runtimeType.GetTypeHandleInternal(), methodtoken, rgIL, methodSize, flags, JitHelpers.GetStackCrawlMarkHandle(ref stackMark));
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern void SwapMethodBody(RuntimeTypeHandle cls, int methodtoken, IntPtr rgIL, int methodSize, int flags, StackCrawlMarkHandle stackMark);

    private MethodRental()
    {
    }

    void _MethodRental.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _MethodRental.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _MethodRental.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _MethodRental.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}

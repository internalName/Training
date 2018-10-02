// Decompiled with JetBrains decompiler
// Type: System.Signature
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
  internal class Signature
  {
    internal RuntimeType[] m_arguments;
    internal RuntimeType m_declaringType;
    internal RuntimeType m_returnTypeORfieldType;
    internal object m_keepalive;
    [SecurityCritical]
    internal unsafe void* m_sig;
    internal int m_managedCallingConventionAndArgIteratorFlags;
    internal int m_nSizeOfArgStack;
    internal int m_csig;
    internal RuntimeMethodHandleInternal m_pMethod;

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private extern unsafe void GetSignature(void* pCorSig, int cCorSig, RuntimeFieldHandleInternal fieldHandle, IRuntimeMethodInfo methodHandle, RuntimeType declaringType);

    [SecuritySafeCritical]
    public unsafe Signature(IRuntimeMethodInfo method, RuntimeType[] arguments, RuntimeType returnType, CallingConventions callingConvention)
    {
      this.m_pMethod = method.Value;
      this.m_arguments = arguments;
      this.m_returnTypeORfieldType = returnType;
      this.m_managedCallingConventionAndArgIteratorFlags = (int) (byte) callingConvention;
      this.GetSignature((void*) null, 0, new RuntimeFieldHandleInternal(), method, (RuntimeType) null);
    }

    [SecuritySafeCritical]
    public unsafe Signature(IRuntimeMethodInfo methodHandle, RuntimeType declaringType)
    {
      this.GetSignature((void*) null, 0, new RuntimeFieldHandleInternal(), methodHandle, declaringType);
    }

    [SecurityCritical]
    public unsafe Signature(IRuntimeFieldInfo fieldHandle, RuntimeType declaringType)
    {
      this.GetSignature((void*) null, 0, fieldHandle.Value, (IRuntimeMethodInfo) null, declaringType);
      GC.KeepAlive((object) fieldHandle);
    }

    [SecurityCritical]
    public unsafe Signature(void* pCorSig, int cCorSig, RuntimeType declaringType)
    {
      this.GetSignature(pCorSig, cCorSig, new RuntimeFieldHandleInternal(), (IRuntimeMethodInfo) null, declaringType);
    }

    internal CallingConventions CallingConvention
    {
      get
      {
        return (CallingConventions) (byte) this.m_managedCallingConventionAndArgIteratorFlags;
      }
    }

    internal RuntimeType[] Arguments
    {
      get
      {
        return this.m_arguments;
      }
    }

    internal RuntimeType ReturnType
    {
      get
      {
        return this.m_returnTypeORfieldType;
      }
    }

    internal RuntimeType FieldType
    {
      get
      {
        return this.m_returnTypeORfieldType;
      }
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool CompareSig(Signature sig1, Signature sig2);

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern Type[] GetCustomModifiers(int position, bool required);

    internal enum MdSigCallingConvention : byte
    {
      Default = 0,
      C = 1,
      StdCall = 2,
      ThisCall = 3,
      FastCall = 4,
      Vararg = 5,
      Field = 6,
      LocalSig = 7,
      Property = 8,
      Unmgd = 9,
      GenericInst = 10, // 0x0A
      Max = 11, // 0x0B
      CallConvMask = 15, // 0x0F
      Generics = 16, // 0x10
      HasThis = 32, // 0x20
      ExplicitThis = 64, // 0x40
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.DynamicResolver
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;
using System.Threading;

namespace System.Reflection.Emit
{
  internal class DynamicResolver : Resolver
  {
    private __ExceptionInfo[] m_exceptions;
    private byte[] m_exceptionHeader;
    private DynamicMethod m_method;
    private byte[] m_code;
    private byte[] m_localSignature;
    private int m_stackSize;
    private DynamicScope m_scope;

    internal DynamicResolver(DynamicILGenerator ilGenerator)
    {
      this.m_stackSize = ilGenerator.GetMaxStackSize();
      this.m_exceptions = ilGenerator.GetExceptions();
      this.m_code = ilGenerator.BakeByteArray();
      this.m_localSignature = ilGenerator.m_localSignature.InternalGetSignatureArray();
      this.m_scope = ilGenerator.m_scope;
      this.m_method = (DynamicMethod) ilGenerator.m_methodBuilder;
      this.m_method.m_resolver = this;
    }

    internal DynamicResolver(DynamicILInfo dynamicILInfo)
    {
      this.m_stackSize = dynamicILInfo.MaxStackSize;
      this.m_code = dynamicILInfo.Code;
      this.m_localSignature = dynamicILInfo.LocalSignature;
      this.m_exceptionHeader = dynamicILInfo.Exceptions;
      this.m_scope = dynamicILInfo.DynamicScope;
      this.m_method = dynamicILInfo.DynamicMethod;
      this.m_method.m_resolver = this;
    }

    ~DynamicResolver()
    {
      DynamicMethod method = this.m_method;
      if ((MethodInfo) method == (MethodInfo) null || method.m_methodHandle == null)
        return;
      DynamicResolver.DestroyScout destroyScout;
      try
      {
        destroyScout = new DynamicResolver.DestroyScout();
      }
      catch
      {
        if (Environment.HasShutdownStarted || AppDomain.CurrentDomain.IsFinalizingForUnload())
          return;
        GC.ReRegisterForFinalize((object) this);
        return;
      }
      destroyScout.m_methodHandle = method.m_methodHandle.Value;
    }

    internal override RuntimeType GetJitContext(ref int securityControlFlags)
    {
      DynamicResolver.SecurityControlFlags securityControlFlags1 = DynamicResolver.SecurityControlFlags.Default;
      if (this.m_method.m_restrictedSkipVisibility)
        securityControlFlags1 |= DynamicResolver.SecurityControlFlags.RestrictedSkipVisibilityChecks;
      else if (this.m_method.m_skipVisibility)
        securityControlFlags1 |= DynamicResolver.SecurityControlFlags.SkipVisibilityChecks;
      RuntimeType typeOwner = this.m_method.m_typeOwner;
      if (this.m_method.m_creationContext != null)
      {
        securityControlFlags1 |= DynamicResolver.SecurityControlFlags.HasCreationContext;
        if (this.m_method.m_creationContext.CanSkipEvaluation)
          securityControlFlags1 |= DynamicResolver.SecurityControlFlags.CanSkipCSEvaluation;
      }
      securityControlFlags = (int) securityControlFlags1;
      return typeOwner;
    }

    private static int CalculateNumberOfExceptions(__ExceptionInfo[] excp)
    {
      int num = 0;
      if (excp == null)
        return 0;
      for (int index = 0; index < excp.Length; ++index)
        num += excp[index].GetNumberOfCatches();
      return num;
    }

    internal override byte[] GetCodeInfo(ref int stackSize, ref int initLocals, ref int EHCount)
    {
      stackSize = this.m_stackSize;
      if (this.m_exceptionHeader != null && this.m_exceptionHeader.Length != 0)
      {
        if (this.m_exceptionHeader.Length < 4)
          throw new FormatException();
        if (((int) this.m_exceptionHeader[0] & 64) != 0)
        {
          byte[] numArray = new byte[4];
          for (int index = 0; index < 3; ++index)
            numArray[index] = this.m_exceptionHeader[index + 1];
          EHCount = (BitConverter.ToInt32(numArray, 0) - 4) / 24;
        }
        else
          EHCount = ((int) this.m_exceptionHeader[1] - 2) / 12;
      }
      else
        EHCount = DynamicResolver.CalculateNumberOfExceptions(this.m_exceptions);
      initLocals = this.m_method.InitLocals ? 1 : 0;
      return this.m_code;
    }

    internal override byte[] GetLocalsSignature()
    {
      return this.m_localSignature;
    }

    internal override byte[] GetRawEHInfo()
    {
      return this.m_exceptionHeader;
    }

    [SecurityCritical]
    internal override unsafe void GetEHInfo(int excNumber, void* exc)
    {
      Resolver.CORINFO_EH_CLAUSE* corinfoEhClausePtr = (Resolver.CORINFO_EH_CLAUSE*) exc;
      for (int index = 0; index < this.m_exceptions.Length; ++index)
      {
        int numberOfCatches = this.m_exceptions[index].GetNumberOfCatches();
        if (excNumber < numberOfCatches)
        {
          corinfoEhClausePtr->Flags = this.m_exceptions[index].GetExceptionTypes()[excNumber];
          corinfoEhClausePtr->TryOffset = this.m_exceptions[index].GetStartAddress();
          corinfoEhClausePtr->TryLength = (corinfoEhClausePtr->Flags & 2) == 2 ? this.m_exceptions[index].GetFinallyEndAddress() - corinfoEhClausePtr->TryOffset : this.m_exceptions[index].GetEndAddress() - corinfoEhClausePtr->TryOffset;
          corinfoEhClausePtr->HandlerOffset = this.m_exceptions[index].GetCatchAddresses()[excNumber];
          corinfoEhClausePtr->HandlerLength = this.m_exceptions[index].GetCatchEndAddresses()[excNumber] - corinfoEhClausePtr->HandlerOffset;
          corinfoEhClausePtr->ClassTokenOrFilterOffset = this.m_exceptions[index].GetFilterAddresses()[excNumber];
          break;
        }
        excNumber -= numberOfCatches;
      }
    }

    internal override string GetStringLiteral(int token)
    {
      return this.m_scope.GetString(token);
    }

    internal override CompressedStack GetSecurityContext()
    {
      return this.m_method.m_creationContext;
    }

    [SecurityCritical]
    internal override void ResolveToken(int token, out IntPtr typeHandle, out IntPtr methodHandle, out IntPtr fieldHandle)
    {
      typeHandle = new IntPtr();
      methodHandle = new IntPtr();
      fieldHandle = new IntPtr();
      object obj = this.m_scope[token];
      if (obj == null)
        throw new InvalidProgramException();
      if (obj is RuntimeTypeHandle)
        typeHandle = ((RuntimeTypeHandle) obj).Value;
      else if (obj is RuntimeMethodHandle)
        methodHandle = ((RuntimeMethodHandle) obj).Value;
      else if (obj is RuntimeFieldHandle)
      {
        fieldHandle = ((RuntimeFieldHandle) obj).Value;
      }
      else
      {
        DynamicMethod dynamicMethod = obj as DynamicMethod;
        if ((MethodInfo) dynamicMethod != (MethodInfo) null)
        {
          methodHandle = dynamicMethod.GetMethodDescriptor().Value;
        }
        else
        {
          GenericMethodInfo genericMethodInfo = obj as GenericMethodInfo;
          if (genericMethodInfo != null)
          {
            methodHandle = genericMethodInfo.m_methodHandle.Value;
            typeHandle = genericMethodInfo.m_context.Value;
          }
          else
          {
            GenericFieldInfo genericFieldInfo = obj as GenericFieldInfo;
            if (genericFieldInfo != null)
            {
              fieldHandle = genericFieldInfo.m_fieldHandle.Value;
              typeHandle = genericFieldInfo.m_context.Value;
            }
            else
            {
              VarArgMethod varArgMethod = obj as VarArgMethod;
              if (varArgMethod == null)
                return;
              if ((MethodInfo) varArgMethod.m_dynamicMethod == (MethodInfo) null)
              {
                methodHandle = varArgMethod.m_method.MethodHandle.Value;
                typeHandle = varArgMethod.m_method.GetDeclaringTypeInternal().GetTypeHandleInternal().Value;
              }
              else
                methodHandle = varArgMethod.m_dynamicMethod.GetMethodDescriptor().Value;
            }
          }
        }
      }
    }

    internal override byte[] ResolveSignature(int token, int fromMethod)
    {
      return this.m_scope.ResolveSignature(token, fromMethod);
    }

    internal override MethodInfo GetDynamicMethod()
    {
      return this.m_method.GetMethodInfo();
    }

    private class DestroyScout
    {
      internal RuntimeMethodHandleInternal m_methodHandle;

      [SecuritySafeCritical]
      ~DestroyScout()
      {
        if (this.m_methodHandle.IsNullHandle())
          return;
        if (RuntimeMethodHandle.GetResolver(this.m_methodHandle) != null)
        {
          if (Environment.HasShutdownStarted || AppDomain.CurrentDomain.IsFinalizingForUnload())
            return;
          GC.ReRegisterForFinalize((object) this);
        }
        else
          RuntimeMethodHandle.Destroy(this.m_methodHandle);
      }
    }

    [Flags]
    internal enum SecurityControlFlags
    {
      Default = 0,
      SkipVisibilityChecks = 1,
      RestrictedSkipVisibilityChecks = 2,
      HasCreationContext = 4,
      CanSkipCSEvaluation = 8,
    }
  }
}

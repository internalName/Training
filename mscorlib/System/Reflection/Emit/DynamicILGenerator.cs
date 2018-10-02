// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.DynamicILGenerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics.SymbolStore;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Emit
{
  internal class DynamicILGenerator : ILGenerator
  {
    internal DynamicScope m_scope;
    private int m_methodSigToken;

    internal DynamicILGenerator(DynamicMethod method, byte[] methodSignature, int size)
      : base((MethodInfo) method, size)
    {
      this.m_scope = new DynamicScope();
      this.m_methodSigToken = this.m_scope.GetTokenFor(methodSignature);
    }

    [SecurityCritical]
    internal void GetCallableMethod(RuntimeModule module, DynamicMethod dm)
    {
      dm.m_methodHandle = ModuleHandle.GetDynamicMethod(dm, module, this.m_methodBuilder.Name, (byte[]) this.m_scope[this.m_methodSigToken], (Resolver) new DynamicResolver(this));
    }

    private bool ProfileAPICheck
    {
      get
      {
        return ((DynamicMethod) this.m_methodBuilder).ProfileAPICheck;
      }
    }

    public override LocalBuilder DeclareLocal(Type localType, bool pinned)
    {
      if (localType == (Type) null)
        throw new ArgumentNullException(nameof (localType));
      RuntimeType runtimeType = localType as RuntimeType;
      if (runtimeType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      if (this.ProfileAPICheck && (runtimeType.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) runtimeType.FullName));
      LocalBuilder localBuilder = new LocalBuilder(this.m_localCount, localType, this.m_methodBuilder);
      this.m_localSignature.AddArgument(localType, pinned);
      ++this.m_localCount;
      return localBuilder;
    }

    [SecuritySafeCritical]
    public override void Emit(OpCode opcode, MethodInfo meth)
    {
      if (meth == (MethodInfo) null)
        throw new ArgumentNullException(nameof (meth));
      int stackchange = 0;
      DynamicMethod dm = meth as DynamicMethod;
      int num;
      if ((MethodInfo) dm == (MethodInfo) null)
      {
        RuntimeMethodInfo rtMeth = meth as RuntimeMethodInfo;
        if ((MethodInfo) rtMeth == (MethodInfo) null)
          throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), nameof (meth));
        RuntimeType runtimeType = rtMeth.GetRuntimeType();
        num = !(runtimeType != (RuntimeType) null) || !runtimeType.IsGenericType && !runtimeType.IsArray ? this.GetTokenFor(rtMeth) : this.GetTokenFor(rtMeth, runtimeType);
      }
      else
      {
        if (opcode.Equals(OpCodes.Ldtoken) || opcode.Equals(OpCodes.Ldftn) || opcode.Equals(OpCodes.Ldvirtftn))
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidOpCodeOnDynamicMethod"));
        num = this.GetTokenFor(dm);
      }
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      if (opcode.StackBehaviourPush == StackBehaviour.Varpush && meth.ReturnType != typeof (void))
        ++stackchange;
      if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
        stackchange -= meth.GetParametersNoCopy().Length;
      if (!meth.IsStatic && !opcode.Equals(OpCodes.Newobj) && (!opcode.Equals(OpCodes.Ldtoken) && !opcode.Equals(OpCodes.Ldftn)))
        --stackchange;
      this.UpdateStackSize(opcode, stackchange);
      this.PutInteger4(num);
    }

    [ComVisible(true)]
    public override void Emit(OpCode opcode, ConstructorInfo con)
    {
      if (con == (ConstructorInfo) null)
        throw new ArgumentNullException(nameof (con));
      RuntimeConstructorInfo rtMeth = con as RuntimeConstructorInfo;
      if ((ConstructorInfo) rtMeth == (ConstructorInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), nameof (con));
      RuntimeType runtimeType = rtMeth.GetRuntimeType();
      int num = !(runtimeType != (RuntimeType) null) || !runtimeType.IsGenericType && !runtimeType.IsArray ? this.GetTokenFor(rtMeth) : this.GetTokenFor(rtMeth, runtimeType);
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      this.UpdateStackSize(opcode, 1);
      this.PutInteger4(num);
    }

    public override void Emit(OpCode opcode, Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      RuntimeType rtType = type as RuntimeType;
      if (rtType == (RuntimeType) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
      int tokenFor = this.GetTokenFor(rtType);
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      this.PutInteger4(tokenFor);
    }

    public override void Emit(OpCode opcode, FieldInfo field)
    {
      if (field == (FieldInfo) null)
        throw new ArgumentNullException(nameof (field));
      RuntimeFieldInfo runtimeField = field as RuntimeFieldInfo;
      if ((FieldInfo) runtimeField == (FieldInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeFieldInfo"), nameof (field));
      int num = !(field.DeclaringType == (Type) null) ? this.GetTokenFor(runtimeField, runtimeField.GetRuntimeType()) : this.GetTokenFor(runtimeField);
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      this.PutInteger4(num);
    }

    public override void Emit(OpCode opcode, string str)
    {
      if (str == null)
        throw new ArgumentNullException(nameof (str));
      int tokenForString = this.GetTokenForString(str);
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      this.PutInteger4(tokenForString);
    }

    [SecuritySafeCritical]
    public override void EmitCalli(OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
    {
      int num = 0;
      if (optionalParameterTypes != null && (callingConvention & CallingConventions.VarArgs) == (CallingConventions) 0)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAVarArgCallingConvention"));
      SignatureHelper memberRefSignature = this.GetMemberRefSignature(callingConvention, returnType, parameterTypes, optionalParameterTypes);
      this.EnsureCapacity(7);
      this.Emit(OpCodes.Calli);
      if (returnType != typeof (void))
        ++num;
      if (parameterTypes != null)
        num -= parameterTypes.Length;
      if (optionalParameterTypes != null)
        num -= optionalParameterTypes.Length;
      if ((callingConvention & CallingConventions.HasThis) == CallingConventions.HasThis)
        --num;
      int stackchange = num - 1;
      this.UpdateStackSize(OpCodes.Calli, stackchange);
      this.PutInteger4(this.GetTokenForSig(memberRefSignature.GetSignature(true)));
    }

    public override void EmitCalli(OpCode opcode, CallingConvention unmanagedCallConv, Type returnType, Type[] parameterTypes)
    {
      int num1 = 0;
      int num2 = 0;
      if (parameterTypes != null)
        num2 = parameterTypes.Length;
      SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(unmanagedCallConv, returnType);
      if (parameterTypes != null)
      {
        for (int index = 0; index < num2; ++index)
          methodSigHelper.AddArgument(parameterTypes[index]);
      }
      if (returnType != typeof (void))
        ++num1;
      if (parameterTypes != null)
        num1 -= num2;
      int stackchange = num1 - 1;
      this.UpdateStackSize(OpCodes.Calli, stackchange);
      this.EnsureCapacity(7);
      this.Emit(OpCodes.Calli);
      this.PutInteger4(this.GetTokenForSig(methodSigHelper.GetSignature(true)));
    }

    [SecuritySafeCritical]
    public override void EmitCall(OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes)
    {
      if (methodInfo == (MethodInfo) null)
        throw new ArgumentNullException(nameof (methodInfo));
      if (!opcode.Equals(OpCodes.Call) && !opcode.Equals(OpCodes.Callvirt) && !opcode.Equals(OpCodes.Newobj))
        throw new ArgumentException(Environment.GetResourceString("Argument_NotMethodCallOpcode"), nameof (opcode));
      if (methodInfo.ContainsGenericParameters)
        throw new ArgumentException(Environment.GetResourceString("Argument_GenericsInvalid"), nameof (methodInfo));
      if (methodInfo.DeclaringType != (Type) null && methodInfo.DeclaringType.ContainsGenericParameters)
        throw new ArgumentException(Environment.GetResourceString("Argument_GenericsInvalid"), nameof (methodInfo));
      int num = 0;
      int memberRefToken = this.GetMemberRefToken((MethodBase) methodInfo, optionalParameterTypes);
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      if (methodInfo.ReturnType != typeof (void))
        ++num;
      int stackchange = num - methodInfo.GetParameterTypes().Length;
      if (!(methodInfo is SymbolMethod) && !methodInfo.IsStatic && !opcode.Equals(OpCodes.Newobj))
        --stackchange;
      if (optionalParameterTypes != null)
        stackchange -= optionalParameterTypes.Length;
      this.UpdateStackSize(opcode, stackchange);
      this.PutInteger4(memberRefToken);
    }

    public override void Emit(OpCode opcode, SignatureHelper signature)
    {
      if (signature == null)
        throw new ArgumentNullException(nameof (signature));
      int num = 0;
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
      {
        int stackchange = num - signature.ArgumentCount - 1;
        this.UpdateStackSize(opcode, stackchange);
      }
      this.PutInteger4(this.GetTokenForSig(signature.GetSignature(true)));
    }

    public override Label BeginExceptionBlock()
    {
      return base.BeginExceptionBlock();
    }

    public override void EndExceptionBlock()
    {
      base.EndExceptionBlock();
    }

    public override void BeginExceptFilterBlock()
    {
      throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
    }

    public override void BeginCatchBlock(Type exceptionType)
    {
      if (this.CurrExcStackCount == 0)
        throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
      __ExceptionInfo currExc = this.CurrExcStack[this.CurrExcStackCount - 1];
      RuntimeType rtType = exceptionType as RuntimeType;
      if (currExc.GetCurrentState() == 1)
      {
        if (exceptionType != (Type) null)
          throw new ArgumentException(Environment.GetResourceString("Argument_ShouldNotSpecifyExceptionType"));
        this.Emit(OpCodes.Endfilter);
      }
      else
      {
        if (exceptionType == (Type) null)
          throw new ArgumentNullException(nameof (exceptionType));
        if (rtType == (RuntimeType) null)
          throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
        Label endLabel = currExc.GetEndLabel();
        this.Emit(OpCodes.Leave, endLabel);
        this.UpdateStackSize(OpCodes.Nop, 1);
      }
      currExc.MarkCatchAddr(this.ILOffset, exceptionType);
      currExc.m_filterAddr[currExc.m_currentCatch - 1] = this.GetTokenFor(rtType);
    }

    public override void BeginFaultBlock()
    {
      throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
    }

    public override void BeginFinallyBlock()
    {
      base.BeginFinallyBlock();
    }

    public override void UsingNamespace(string ns)
    {
      throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
    }

    public override void MarkSequencePoint(ISymbolDocumentWriter document, int startLine, int startColumn, int endLine, int endColumn)
    {
      throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
    }

    public override void BeginScope()
    {
      throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
    }

    public override void EndScope()
    {
      throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_NotAllowedInDynamicMethod"));
    }

    [SecurityCritical]
    private int GetMemberRefToken(MethodBase methodInfo, Type[] optionalParameterTypes)
    {
      if (optionalParameterTypes != null && (methodInfo.CallingConvention & CallingConventions.VarArgs) == (CallingConventions) 0)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAVarArgCallingConvention"));
      RuntimeMethodInfo rtMeth = methodInfo as RuntimeMethodInfo;
      DynamicMethod dm = methodInfo as DynamicMethod;
      if ((MethodInfo) rtMeth == (MethodInfo) null && (MethodInfo) dm == (MethodInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"), nameof (methodInfo));
      ParameterInfo[] parametersNoCopy = methodInfo.GetParametersNoCopy();
      Type[] parameterTypes;
      if (parametersNoCopy != null && parametersNoCopy.Length != 0)
      {
        parameterTypes = new Type[parametersNoCopy.Length];
        for (int index = 0; index < parametersNoCopy.Length; ++index)
          parameterTypes[index] = parametersNoCopy[index].ParameterType;
      }
      else
        parameterTypes = (Type[]) null;
      SignatureHelper memberRefSignature = this.GetMemberRefSignature(methodInfo.CallingConvention, MethodBuilder.GetMethodBaseReturnType(methodInfo), parameterTypes, optionalParameterTypes);
      if ((MethodInfo) rtMeth != (MethodInfo) null)
        return this.GetTokenForVarArgMethod(rtMeth, memberRefSignature);
      return this.GetTokenForVarArgMethod(dm, memberRefSignature);
    }

    [SecurityCritical]
    internal override SignatureHelper GetMemberRefSignature(CallingConventions call, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
    {
      int num = parameterTypes != null ? parameterTypes.Length : 0;
      SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(call, returnType);
      for (int index = 0; index < num; ++index)
        methodSigHelper.AddArgument(parameterTypes[index]);
      if (optionalParameterTypes != null && optionalParameterTypes.Length != 0)
      {
        methodSigHelper.AddSentinel();
        for (int index = 0; index < optionalParameterTypes.Length; ++index)
          methodSigHelper.AddArgument(optionalParameterTypes[index]);
      }
      return methodSigHelper;
    }

    internal override void RecordTokenFixup()
    {
    }

    private int GetTokenFor(RuntimeType rtType)
    {
      if (this.ProfileAPICheck && (rtType.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) rtType.FullName));
      return this.m_scope.GetTokenFor(rtType.TypeHandle);
    }

    private int GetTokenFor(RuntimeFieldInfo runtimeField)
    {
      if (this.ProfileAPICheck)
      {
        RtFieldInfo rtFieldInfo = runtimeField as RtFieldInfo;
        if ((FieldInfo) rtFieldInfo != (FieldInfo) null && (rtFieldInfo.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) rtFieldInfo.FullName));
      }
      return this.m_scope.GetTokenFor(runtimeField.FieldHandle);
    }

    private int GetTokenFor(RuntimeFieldInfo runtimeField, RuntimeType rtType)
    {
      if (this.ProfileAPICheck)
      {
        RtFieldInfo rtFieldInfo = runtimeField as RtFieldInfo;
        if ((FieldInfo) rtFieldInfo != (FieldInfo) null && (rtFieldInfo.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) rtFieldInfo.FullName));
        if ((rtType.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) rtType.FullName));
      }
      return this.m_scope.GetTokenFor(runtimeField.FieldHandle, rtType.TypeHandle);
    }

    private int GetTokenFor(RuntimeConstructorInfo rtMeth)
    {
      if (this.ProfileAPICheck && (rtMeth.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) rtMeth.FullName));
      return this.m_scope.GetTokenFor(rtMeth.MethodHandle);
    }

    private int GetTokenFor(RuntimeConstructorInfo rtMeth, RuntimeType rtType)
    {
      if (this.ProfileAPICheck)
      {
        if ((rtMeth.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) rtMeth.FullName));
        if ((rtType.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) rtType.FullName));
      }
      return this.m_scope.GetTokenFor(rtMeth.MethodHandle, rtType.TypeHandle);
    }

    private int GetTokenFor(RuntimeMethodInfo rtMeth)
    {
      if (this.ProfileAPICheck && (rtMeth.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) rtMeth.FullName));
      return this.m_scope.GetTokenFor(rtMeth.MethodHandle);
    }

    private int GetTokenFor(RuntimeMethodInfo rtMeth, RuntimeType rtType)
    {
      if (this.ProfileAPICheck)
      {
        if ((rtMeth.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) rtMeth.FullName));
        if ((rtType.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) rtType.FullName));
      }
      return this.m_scope.GetTokenFor(rtMeth.MethodHandle, rtType.TypeHandle);
    }

    private int GetTokenFor(DynamicMethod dm)
    {
      return this.m_scope.GetTokenFor(dm);
    }

    private int GetTokenForVarArgMethod(RuntimeMethodInfo rtMeth, SignatureHelper sig)
    {
      if (this.ProfileAPICheck && (rtMeth.InvocationFlags & INVOCATION_FLAGS.INVOCATION_FLAGS_NON_W8P_FX_API) != INVOCATION_FLAGS.INVOCATION_FLAGS_UNKNOWN)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_APIInvalidForCurrentContext", (object) rtMeth.FullName));
      return this.m_scope.GetTokenFor(new VarArgMethod(rtMeth, sig));
    }

    private int GetTokenForVarArgMethod(DynamicMethod dm, SignatureHelper sig)
    {
      return this.m_scope.GetTokenFor(new VarArgMethod(dm, sig));
    }

    private int GetTokenForString(string s)
    {
      return this.m_scope.GetTokenFor(s);
    }

    private int GetTokenForSig(byte[] sig)
    {
      return this.m_scope.GetTokenFor(sig);
    }
  }
}

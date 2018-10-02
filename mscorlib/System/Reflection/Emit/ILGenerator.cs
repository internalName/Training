// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.ILGenerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Emit
{
  /// <summary>Создает инструкции промежуточного языка MSIL.</summary>
  [ClassInterface(ClassInterfaceType.None)]
  [ComDefaultInterface(typeof (_ILGenerator))]
  [ComVisible(true)]
  public class ILGenerator : _ILGenerator
  {
    private const int defaultSize = 16;
    private const int DefaultFixupArraySize = 8;
    private const int DefaultLabelArraySize = 4;
    private const int DefaultExceptionArraySize = 2;
    private int m_length;
    private byte[] m_ILStream;
    private int[] m_labelList;
    private int m_labelCount;
    private __FixupData[] m_fixupData;
    private int m_fixupCount;
    private int[] m_RelocFixupList;
    private int m_RelocFixupCount;
    private int m_exceptionCount;
    private int m_currExcStackCount;
    private __ExceptionInfo[] m_exceptions;
    private __ExceptionInfo[] m_currExcStack;
    internal ScopeTree m_ScopeTree;
    internal LineNumberInfo m_LineNumberInfo;
    internal MethodInfo m_methodBuilder;
    internal int m_localCount;
    internal SignatureHelper m_localSignature;
    private int m_maxStackSize;
    private int m_maxMidStack;
    private int m_maxMidStackCur;

    internal static int[] EnlargeArray(int[] incoming)
    {
      int[] numArray = new int[incoming.Length * 2];
      Array.Copy((Array) incoming, (Array) numArray, incoming.Length);
      return numArray;
    }

    private static byte[] EnlargeArray(byte[] incoming)
    {
      byte[] numArray = new byte[incoming.Length * 2];
      Array.Copy((Array) incoming, (Array) numArray, incoming.Length);
      return numArray;
    }

    private static byte[] EnlargeArray(byte[] incoming, int requiredSize)
    {
      byte[] numArray = new byte[requiredSize];
      Array.Copy((Array) incoming, (Array) numArray, incoming.Length);
      return numArray;
    }

    private static __FixupData[] EnlargeArray(__FixupData[] incoming)
    {
      __FixupData[] fixupDataArray = new __FixupData[incoming.Length * 2];
      Array.Copy((Array) incoming, (Array) fixupDataArray, incoming.Length);
      return fixupDataArray;
    }

    private static __ExceptionInfo[] EnlargeArray(__ExceptionInfo[] incoming)
    {
      __ExceptionInfo[] exceptionInfoArray = new __ExceptionInfo[incoming.Length * 2];
      Array.Copy((Array) incoming, (Array) exceptionInfoArray, incoming.Length);
      return exceptionInfoArray;
    }

    internal int CurrExcStackCount
    {
      get
      {
        return this.m_currExcStackCount;
      }
    }

    internal __ExceptionInfo[] CurrExcStack
    {
      get
      {
        return this.m_currExcStack;
      }
    }

    internal ILGenerator(MethodInfo methodBuilder)
      : this(methodBuilder, 64)
    {
    }

    internal ILGenerator(MethodInfo methodBuilder, int size)
    {
      this.m_ILStream = size >= 16 ? new byte[size] : new byte[16];
      this.m_length = 0;
      this.m_labelCount = 0;
      this.m_fixupCount = 0;
      this.m_labelList = (int[]) null;
      this.m_fixupData = (__FixupData[]) null;
      this.m_exceptions = (__ExceptionInfo[]) null;
      this.m_exceptionCount = 0;
      this.m_currExcStack = (__ExceptionInfo[]) null;
      this.m_currExcStackCount = 0;
      this.m_RelocFixupList = (int[]) null;
      this.m_RelocFixupCount = 0;
      this.m_ScopeTree = new ScopeTree();
      this.m_LineNumberInfo = new LineNumberInfo();
      this.m_methodBuilder = methodBuilder;
      this.m_localCount = 0;
      MethodBuilder methodBuilder1 = this.m_methodBuilder as MethodBuilder;
      if ((MethodInfo) methodBuilder1 == (MethodInfo) null)
        this.m_localSignature = SignatureHelper.GetLocalVarSigHelper((Module) null);
      else
        this.m_localSignature = SignatureHelper.GetLocalVarSigHelper(methodBuilder1.GetTypeBuilder().Module);
    }

    internal virtual void RecordTokenFixup()
    {
      if (this.m_RelocFixupList == null)
        this.m_RelocFixupList = new int[8];
      else if (this.m_RelocFixupList.Length <= this.m_RelocFixupCount)
        this.m_RelocFixupList = ILGenerator.EnlargeArray(this.m_RelocFixupList);
      this.m_RelocFixupList[this.m_RelocFixupCount++] = this.m_length;
    }

    internal void InternalEmit(OpCode opcode)
    {
      if (opcode.Size != 1)
        this.m_ILStream[this.m_length++] = (byte) ((uint) opcode.Value >> 8);
      this.m_ILStream[this.m_length++] = (byte) opcode.Value;
      this.UpdateStackSize(opcode, opcode.StackChange());
    }

    internal void UpdateStackSize(OpCode opcode, int stackchange)
    {
      this.m_maxMidStackCur += stackchange;
      if (this.m_maxMidStackCur > this.m_maxMidStack)
        this.m_maxMidStack = this.m_maxMidStackCur;
      else if (this.m_maxMidStackCur < 0)
        this.m_maxMidStackCur = 0;
      if (!opcode.EndsUncondJmpBlk())
        return;
      this.m_maxStackSize += this.m_maxMidStack;
      this.m_maxMidStack = 0;
      this.m_maxMidStackCur = 0;
    }

    [SecurityCritical]
    private int GetMethodToken(MethodBase method, Type[] optionalParameterTypes, bool useMethodDef)
    {
      return ((ModuleBuilder) this.m_methodBuilder.Module).GetMethodTokenInternal(method, (IEnumerable<Type>) optionalParameterTypes, useMethodDef);
    }

    [SecurityCritical]
    internal virtual SignatureHelper GetMemberRefSignature(CallingConventions call, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
    {
      return this.GetMemberRefSignature(call, returnType, parameterTypes, optionalParameterTypes, 0);
    }

    [SecurityCritical]
    private SignatureHelper GetMemberRefSignature(CallingConventions call, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes, int cGenericParameters)
    {
      return ((ModuleBuilder) this.m_methodBuilder.Module).GetMemberRefSignature(call, returnType, parameterTypes, (IEnumerable<Type>) optionalParameterTypes, cGenericParameters);
    }

    internal byte[] BakeByteArray()
    {
      if (this.m_currExcStackCount != 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_UnclosedExceptionBlock"));
      if (this.m_length == 0)
        return (byte[]) null;
      int length = this.m_length;
      byte[] array = new byte[length];
      Array.Copy((Array) this.m_ILStream, (Array) array, length);
      for (int index = 0; index < this.m_fixupCount; ++index)
      {
        int num = this.GetLabelPos(this.m_fixupData[index].m_fixupLabel) - (this.m_fixupData[index].m_fixupPos + this.m_fixupData[index].m_fixupInstSize);
        if (this.m_fixupData[index].m_fixupInstSize == 1)
        {
          if (num < (int) sbyte.MinValue || num > (int) sbyte.MaxValue)
            throw new NotSupportedException(Environment.GetResourceString("NotSupported_IllegalOneByteBranch", (object) this.m_fixupData[index].m_fixupPos, (object) num));
          array[this.m_fixupData[index].m_fixupPos] = num >= 0 ? (byte) num : (byte) (256 + num);
        }
        else
          ILGenerator.PutInteger4InArray(num, this.m_fixupData[index].m_fixupPos, array);
      }
      return array;
    }

    internal __ExceptionInfo[] GetExceptions()
    {
      if (this.m_currExcStackCount != 0)
        throw new NotSupportedException(Environment.GetResourceString("Argument_UnclosedExceptionBlock"));
      if (this.m_exceptionCount == 0)
        return (__ExceptionInfo[]) null;
      __ExceptionInfo[] exceptions = new __ExceptionInfo[this.m_exceptionCount];
      Array.Copy((Array) this.m_exceptions, (Array) exceptions, this.m_exceptionCount);
      ILGenerator.SortExceptions(exceptions);
      return exceptions;
    }

    internal void EnsureCapacity(int size)
    {
      if (this.m_length + size < this.m_ILStream.Length)
        return;
      if (this.m_length + size >= 2 * this.m_ILStream.Length)
        this.m_ILStream = ILGenerator.EnlargeArray(this.m_ILStream, this.m_length + size);
      else
        this.m_ILStream = ILGenerator.EnlargeArray(this.m_ILStream);
    }

    internal void PutInteger4(int value)
    {
      this.m_length = ILGenerator.PutInteger4InArray(value, this.m_length, this.m_ILStream);
    }

    private static int PutInteger4InArray(int value, int startPos, byte[] array)
    {
      array[startPos++] = (byte) value;
      array[startPos++] = (byte) (value >> 8);
      array[startPos++] = (byte) (value >> 16);
      array[startPos++] = (byte) (value >> 24);
      return startPos;
    }

    private int GetLabelPos(Label lbl)
    {
      int labelValue = lbl.GetLabelValue();
      if (labelValue < 0 || labelValue >= this.m_labelCount)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadLabel"));
      if (this.m_labelList[labelValue] < 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_BadLabelContent"));
      return this.m_labelList[labelValue];
    }

    private void AddFixup(Label lbl, int pos, int instSize)
    {
      if (this.m_fixupData == null)
        this.m_fixupData = new __FixupData[8];
      else if (this.m_fixupData.Length <= this.m_fixupCount)
        this.m_fixupData = ILGenerator.EnlargeArray(this.m_fixupData);
      this.m_fixupData[this.m_fixupCount].m_fixupPos = pos;
      this.m_fixupData[this.m_fixupCount].m_fixupLabel = lbl;
      this.m_fixupData[this.m_fixupCount].m_fixupInstSize = instSize;
      ++this.m_fixupCount;
    }

    internal int GetMaxStackSize()
    {
      return this.m_maxStackSize;
    }

    private static void SortExceptions(__ExceptionInfo[] exceptions)
    {
      int length = exceptions.Length;
      for (int index1 = 0; index1 < length; ++index1)
      {
        int index2 = index1;
        for (int index3 = index1 + 1; index3 < length; ++index3)
        {
          if (exceptions[index2].IsInner(exceptions[index3]))
            index2 = index3;
        }
        __ExceptionInfo exception = exceptions[index1];
        exceptions[index1] = exceptions[index2];
        exceptions[index2] = exception;
      }
    }

    internal int[] GetTokenFixups()
    {
      if (this.m_RelocFixupCount == 0)
        return (int[]) null;
      int[] numArray = new int[this.m_RelocFixupCount];
      Array.Copy((Array) this.m_RelocFixupList, (Array) numArray, this.m_RelocFixupCount);
      return numArray;
    }

    /// <summary>Помещает указанную инструкцию в поток инструкций.</summary>
    /// <param name="opcode">
    ///   Инструкции промежуточного языка MSIL (Microsoft), помещаемая в поток.
    /// </param>
    public virtual void Emit(OpCode opcode)
    {
      this.EnsureCapacity(3);
      this.InternalEmit(opcode);
    }

    /// <summary>
    ///   Помещает заданную инструкцию и символьный аргумент поток инструкции промежуточного языка MSIL.
    /// </summary>
    /// <param name="opcode">Инструкции MSIL, помещаемая в поток.</param>
    /// <param name="arg">
    ///   Аргумент типа character, помещаемый в поток сразу после инструкции.
    /// </param>
    public virtual void Emit(OpCode opcode, byte arg)
    {
      this.EnsureCapacity(4);
      this.InternalEmit(opcode);
      this.m_ILStream[this.m_length++] = arg;
    }

    /// <summary>
    ///   Помещает заданную инструкцию и символьный аргумент поток инструкции промежуточного языка MSIL.
    /// </summary>
    /// <param name="opcode">Инструкции MSIL, помещаемая в поток.</param>
    /// <param name="arg">
    ///   Аргумент типа character, помещаемый в поток сразу после инструкции.
    /// </param>
    [CLSCompliant(false)]
    public void Emit(OpCode opcode, sbyte arg)
    {
      this.EnsureCapacity(4);
      this.InternalEmit(opcode);
      if (arg < (sbyte) 0)
        this.m_ILStream[this.m_length++] = (byte) (256U + (uint) arg);
      else
        this.m_ILStream[this.m_length++] = (byte) arg;
    }

    /// <summary>
    ///   Помещает заданную инструкцию и числовой аргумент поток инструкции промежуточного языка MSIL.
    /// </summary>
    /// <param name="opcode">Инструкции MSIL, включаемая в поток.</param>
    /// <param name="arg">
    ///   <see langword="Int" /> Аргумент помещаемый в поток сразу после инструкции.
    /// </param>
    public virtual void Emit(OpCode opcode, short arg)
    {
      this.EnsureCapacity(5);
      this.InternalEmit(opcode);
      this.m_ILStream[this.m_length++] = (byte) arg;
      this.m_ILStream[this.m_length++] = (byte) ((uint) arg >> 8);
    }

    /// <summary>
    ///   Помещает заданную инструкцию и числовой аргумент поток инструкции промежуточного языка MSIL.
    /// </summary>
    /// <param name="opcode">Инструкции MSIL, помещаемая в поток.</param>
    /// <param name="arg">
    ///   Числовой аргумент, помещаемый в поток сразу после инструкции.
    /// </param>
    public virtual void Emit(OpCode opcode, int arg)
    {
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      this.PutInteger4(arg);
    }

    /// <summary>
    ///   Помещает заданную инструкцию Microsoft поток промежуточного языка (MSIL) следуют токен метаданных указанного метода.
    /// </summary>
    /// <param name="opcode">Инструкции MSIL, включаемая в поток.</param>
    /// <param name="meth">
    ///   A <see langword="MethodInfo" /> представляющего метод.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="meth" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   <paramref name="meth" /> — Это универсальный метод, для которого <see cref="P:System.Reflection.MethodInfo.IsGenericMethodDefinition" /> свойство <see langword="false" />.
    /// </exception>
    [SecuritySafeCritical]
    public virtual void Emit(OpCode opcode, MethodInfo meth)
    {
      if (meth == (MethodInfo) null)
        throw new ArgumentNullException(nameof (meth));
      if (opcode.Equals(OpCodes.Call) || opcode.Equals(OpCodes.Callvirt) || opcode.Equals(OpCodes.Newobj))
      {
        this.EmitCall(opcode, meth, (Type[]) null);
      }
      else
      {
        int stackchange = 0;
        bool useMethodDef = opcode.Equals(OpCodes.Ldtoken) || opcode.Equals(OpCodes.Ldftn) || opcode.Equals(OpCodes.Ldvirtftn);
        int methodToken = this.GetMethodToken((MethodBase) meth, (Type[]) null, useMethodDef);
        this.EnsureCapacity(7);
        this.InternalEmit(opcode);
        this.UpdateStackSize(opcode, stackchange);
        this.RecordTokenFixup();
        this.PutInteger4(methodToken);
      }
    }

    /// <summary>
    ///   Помещает <see cref="F:System.Reflection.Emit.OpCodes.Calli" /> инструкции промежуточного языка MSIL поток, указав соглашений по управляемым вызовам для косвенного вызова.
    /// </summary>
    /// <param name="opcode">
    ///   Инструкции MSIL, включаемая в поток.
    ///    Должен быть <see cref="F:System.Reflection.Emit.OpCodes.Calli" />.
    /// </param>
    /// <param name="callingConvention">
    ///   Управляемые соглашение о вызовах для использования.
    /// </param>
    /// <param name="returnType">
    ///   <see cref="T:System.Type" /> Результата.
    /// </param>
    /// <param name="parameterTypes">
    ///   Типы обязательных аргументов инструкции.
    /// </param>
    /// <param name="optionalParameterTypes">
    ///   Типы необязательных аргументов для <see langword="varargs" /> вызовов.
    /// </param>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="optionalParameterTypes" /> не <see langword="null" />, но <paramref name="callingConvention" /> не включает <see cref="F:System.Reflection.CallingConventions.VarArgs" /> флаг.
    /// </exception>
    [SecuritySafeCritical]
    public virtual void EmitCalli(OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
    {
      int num = 0;
      if (optionalParameterTypes != null && (callingConvention & CallingConventions.VarArgs) == (CallingConventions) 0)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAVarArgCallingConvention"));
      ModuleBuilder module = (ModuleBuilder) this.m_methodBuilder.Module;
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
      this.RecordTokenFixup();
      this.PutInteger4(module.GetSignatureToken(memberRefSignature).Token);
    }

    /// <summary>
    ///   Помещает <see cref="F:System.Reflection.Emit.OpCodes.Calli" /> инструкции промежуточного языка MSIL поток, указав соглашений по неуправляемым вызовам для косвенного вызова.
    /// </summary>
    /// <param name="opcode">
    ///   Инструкции MSIL, включаемая в поток.
    ///    Должен быть <see cref="F:System.Reflection.Emit.OpCodes.Calli" />.
    /// </param>
    /// <param name="unmanagedCallConv">
    ///   Неуправляемые соглашение о вызовах для использования.
    /// </param>
    /// <param name="returnType">
    ///   <see cref="T:System.Type" /> Результата.
    /// </param>
    /// <param name="parameterTypes">
    ///   Типы обязательных аргументов инструкции.
    /// </param>
    public virtual void EmitCalli(OpCode opcode, CallingConvention unmanagedCallConv, Type returnType, Type[] parameterTypes)
    {
      int num1 = 0;
      int num2 = 0;
      ModuleBuilder module = (ModuleBuilder) this.m_methodBuilder.Module;
      if (parameterTypes != null)
        num2 = parameterTypes.Length;
      SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper((Module) module, unmanagedCallConv, returnType);
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
      this.RecordTokenFixup();
      this.PutInteger4(module.GetSignatureToken(methodSigHelper).Token);
    }

    /// <summary>
    ///   Помещает инструкцию <see langword="call" /> или <see langword="callvirt" /> в поток языка MSIL для вызова метода <see langword="varargs" />.
    /// </summary>
    /// <param name="opcode">
    ///   Инструкция языка MSIL, которую следует включить в поток.
    ///    Должно быть <see cref="F:System.Reflection.Emit.OpCodes.Call" />, <see cref="F:System.Reflection.Emit.OpCodes.Callvirt" /> или <see cref="F:System.Reflection.Emit.OpCodes.Newobj" />.
    /// </param>
    /// <param name="methodInfo">
    ///   Метод <see langword="varargs" />, который следует вызывать.
    /// </param>
    /// <param name="optionalParameterTypes">
    ///   Типы необязательных аргументов, если используется метод <see langword="varargs" />. В противном случае указывается значение <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="opcode" /> не задает вызов метода.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="methodInfo" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Соглашение о вызовах для метода отличается от <see langword="varargs" />. Однако при этом указываются типы необязательных параметров.
    ///    Это исключение возникает в .NET Framework версии 1.0 и 1.1. В последующих версиях исключение не возникает.
    /// </exception>
    [SecuritySafeCritical]
    public virtual void EmitCall(OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes)
    {
      if (methodInfo == (MethodInfo) null)
        throw new ArgumentNullException(nameof (methodInfo));
      if (!opcode.Equals(OpCodes.Call) && !opcode.Equals(OpCodes.Callvirt) && !opcode.Equals(OpCodes.Newobj))
        throw new ArgumentException(Environment.GetResourceString("Argument_NotMethodCallOpcode"), nameof (opcode));
      int stackchange = 0;
      int methodToken = this.GetMethodToken((MethodBase) methodInfo, optionalParameterTypes, false);
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      if (methodInfo.ReturnType != typeof (void))
        ++stackchange;
      Type[] parameterTypes = methodInfo.GetParameterTypes();
      if (parameterTypes != null)
        stackchange -= parameterTypes.Length;
      if (!(methodInfo is SymbolMethod) && !methodInfo.IsStatic && !opcode.Equals(OpCodes.Newobj))
        --stackchange;
      if (optionalParameterTypes != null)
        stackchange -= optionalParameterTypes.Length;
      this.UpdateStackSize(opcode, stackchange);
      this.RecordTokenFixup();
      this.PutInteger4(methodToken);
    }

    /// <summary>
    ///   Помещает заданную инструкцию и токен подписи поток инструкции промежуточного языка MSIL.
    /// </summary>
    /// <param name="opcode">Инструкции MSIL, включаемая в поток.</param>
    /// <param name="signature">
    ///   Вспомогательный класс для конструирования токена подписи.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="signature" /> имеет значение <see langword="null" />.
    /// </exception>
    public virtual void Emit(OpCode opcode, SignatureHelper signature)
    {
      if (signature == null)
        throw new ArgumentNullException(nameof (signature));
      int num = 0;
      int token = ((ModuleBuilder) this.m_methodBuilder.Module).GetSignatureToken(signature).Token;
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
      {
        int stackchange = num - signature.ArgumentCount - 1;
        this.UpdateStackSize(opcode, stackchange);
      }
      this.RecordTokenFixup();
      this.PutInteger4(token);
    }

    /// <summary>
    ///   Помещает заданную инструкцию и токен метаданных указанного конструктора в поток инструкции промежуточного языка MSIL.
    /// </summary>
    /// <param name="opcode">Инструкции MSIL, включаемая в поток.</param>
    /// <param name="con">
    ///   A <see langword="ConstructorInfo" /> предоставляющий конструктор.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="con" /> имеет значение <see langword="null" />.
    ///    Это исключение является нововведением в .NET Framework 4.
    /// </exception>
    [SecuritySafeCritical]
    [ComVisible(true)]
    public virtual void Emit(OpCode opcode, ConstructorInfo con)
    {
      if (con == (ConstructorInfo) null)
        throw new ArgumentNullException(nameof (con));
      int stackchange = 0;
      int methodToken = this.GetMethodToken((MethodBase) con, (Type[]) null, true);
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      if (opcode.StackBehaviourPush == StackBehaviour.Varpush)
        ++stackchange;
      if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
      {
        Type[] parameterTypes = con.GetParameterTypes();
        if (parameterTypes != null)
          stackchange -= parameterTypes.Length;
      }
      this.UpdateStackSize(opcode, stackchange);
      this.RecordTokenFixup();
      this.PutInteger4(methodToken);
    }

    /// <summary>
    ///   Помещает заданную инструкцию Microsoft поток промежуточного языка MSIL, а затем маркер метаданных для данного типа.
    /// </summary>
    /// <param name="opcode">Инструкции MSIL, помещаемая в поток.</param>
    /// <param name="cls">
    ///   Объект <see langword="Type" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="cls" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    public virtual void Emit(OpCode opcode, Type cls)
    {
      ModuleBuilder module = (ModuleBuilder) this.m_methodBuilder.Module;
      int num = !(opcode == OpCodes.Ldtoken) || !(cls != (Type) null) || !cls.IsGenericTypeDefinition ? module.GetTypeTokenInternal(cls).Token : module.GetTypeToken(cls).Token;
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      this.RecordTokenFixup();
      this.PutInteger4(num);
    }

    /// <summary>
    ///   Помещает заданную инструкцию и числовой аргумент поток инструкции промежуточного языка MSIL.
    /// </summary>
    /// <param name="opcode">Инструкции MSIL, помещаемая в поток.</param>
    /// <param name="arg">
    ///   Числовой аргумент, помещаемый в поток сразу после инструкции.
    /// </param>
    public virtual void Emit(OpCode opcode, long arg)
    {
      this.EnsureCapacity(11);
      this.InternalEmit(opcode);
      this.m_ILStream[this.m_length++] = (byte) arg;
      this.m_ILStream[this.m_length++] = (byte) (arg >> 8);
      this.m_ILStream[this.m_length++] = (byte) (arg >> 16);
      this.m_ILStream[this.m_length++] = (byte) (arg >> 24);
      this.m_ILStream[this.m_length++] = (byte) (arg >> 32);
      this.m_ILStream[this.m_length++] = (byte) (arg >> 40);
      this.m_ILStream[this.m_length++] = (byte) (arg >> 48);
      this.m_ILStream[this.m_length++] = (byte) (arg >> 56);
    }

    /// <summary>
    ///   Помещает заданную инструкцию и числовой аргумент поток инструкции промежуточного языка MSIL.
    /// </summary>
    /// <param name="opcode">Инструкции MSIL, помещаемая в поток.</param>
    /// <param name="arg">
    ///   <see langword="Single" /> Аргумент помещаемый в поток сразу после инструкции.
    /// </param>
    [SecuritySafeCritical]
    public virtual unsafe void Emit(OpCode opcode, float arg)
    {
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      uint num = *(uint*) &arg;
      this.m_ILStream[this.m_length++] = (byte) num;
      this.m_ILStream[this.m_length++] = (byte) (num >> 8);
      this.m_ILStream[this.m_length++] = (byte) (num >> 16);
      this.m_ILStream[this.m_length++] = (byte) (num >> 24);
    }

    /// <summary>
    ///   Помещает заданную инструкцию и числовой аргумент поток инструкции промежуточного языка MSIL.
    /// </summary>
    /// <param name="opcode">
    ///   Инструкции MSIL, помещаемая в поток.
    ///    Определено в <see langword="OpCodes" /> перечисления.
    /// </param>
    /// <param name="arg">
    ///   Числовой аргумент, помещаемый в поток сразу после инструкции.
    /// </param>
    [SecuritySafeCritical]
    public virtual unsafe void Emit(OpCode opcode, double arg)
    {
      this.EnsureCapacity(11);
      this.InternalEmit(opcode);
      ulong num = (ulong) *(long*) &arg;
      this.m_ILStream[this.m_length++] = (byte) num;
      this.m_ILStream[this.m_length++] = (byte) (num >> 8);
      this.m_ILStream[this.m_length++] = (byte) (num >> 16);
      this.m_ILStream[this.m_length++] = (byte) (num >> 24);
      this.m_ILStream[this.m_length++] = (byte) (num >> 32);
      this.m_ILStream[this.m_length++] = (byte) (num >> 40);
      this.m_ILStream[this.m_length++] = (byte) (num >> 48);
      this.m_ILStream[this.m_length++] = (byte) (num >> 56);
    }

    /// <summary>
    ///   Помещает указанную инструкцию в поток промежуточного языка MSIL и оставляет место, чтобы включить метку, после этого исправления.
    /// </summary>
    /// <param name="opcode">Инструкции MSIL, включаемая в поток.</param>
    /// <param name="label">
    ///   Метка, которые следует осуществлять переход из этого расположения.
    /// </param>
    public virtual void Emit(OpCode opcode, Label label)
    {
      label.GetLabelValue();
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      if (OpCodes.TakesSingleByteArgument(opcode))
      {
        this.AddFixup(label, this.m_length, 1);
        ++this.m_length;
      }
      else
      {
        this.AddFixup(label, this.m_length, 4);
        this.m_length += 4;
      }
    }

    /// <summary>
    ///   Помещает указанную инструкцию в поток промежуточного языка MSIL и оставляет место, чтобы включить метку, после этого исправления.
    /// </summary>
    /// <param name="opcode">Инструкции MSIL, включаемая в поток.</param>
    /// <param name="labels">
    ///   Массив объектов меток, на которые переход из этого расположения.
    ///    Все метки будут использоваться.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="con" /> имеет значение <see langword="null" />.
    ///    Это исключение является нововведением в .NET Framework 4.
    /// </exception>
    public virtual void Emit(OpCode opcode, Label[] labels)
    {
      if (labels == null)
        throw new ArgumentNullException(nameof (labels));
      int length = labels.Length;
      this.EnsureCapacity(length * 4 + 7);
      this.InternalEmit(opcode);
      this.PutInteger4(length);
      int instSize = length * 4;
      int index = 0;
      while (instSize > 0)
      {
        this.AddFixup(labels[index], this.m_length, instSize);
        this.m_length += 4;
        instSize -= 4;
        ++index;
      }
    }

    /// <summary>
    ///   Помещает заданную инструкцию и токен метаданных для указанного поля поток инструкции промежуточного языка MSIL.
    /// </summary>
    /// <param name="opcode">Инструкции MSIL, включаемая в поток.</param>
    /// <param name="field">
    ///   Объект <see langword="FieldInfo" /> представляет поле.
    /// </param>
    public virtual void Emit(OpCode opcode, FieldInfo field)
    {
      int token = ((ModuleBuilder) this.m_methodBuilder.Module).GetFieldToken(field).Token;
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      this.RecordTokenFixup();
      this.PutInteger4(token);
    }

    /// <summary>
    ///   Помещает заданную инструкцию Microsoft поток промежуточного языка MSIL, а затем маркер метаданных для заданной строки.
    /// </summary>
    /// <param name="opcode">Инструкции MSIL, включаемая в поток.</param>
    /// <param name="str">
    ///   <see langword="String" /> Может быть выпущен.
    /// </param>
    public virtual void Emit(OpCode opcode, string str)
    {
      int token = ((ModuleBuilder) this.m_methodBuilder.Module).GetStringConstant(str).Token;
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      this.PutInteger4(token);
    }

    /// <summary>
    ///   Помещает заданную инструкцию Microsoft поток промежуточного языка (MSIL) следуют индекс заданной локальной переменной.
    /// </summary>
    /// <param name="opcode">Инструкции MSIL, включаемая в поток.</param>
    /// <param name="local">Локальная переменная.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   Метод родительского <paramref name="local" /> параметр не соответствует метод, связанный с этим <see cref="T:System.Reflection.Emit.ILGenerator" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="local" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <paramref name="opcode" /> — Это инструкция однобайтовых и <paramref name="local" /> представляет локальную переменную с индексом большим, чем <see langword="Byte.MaxValue" />.
    /// </exception>
    public virtual void Emit(OpCode opcode, LocalBuilder local)
    {
      if (local == null)
        throw new ArgumentNullException(nameof (local));
      int localIndex = local.GetLocalIndex();
      if (local.GetMethodBuilder() != this.m_methodBuilder)
        throw new ArgumentException(Environment.GetResourceString("Argument_UnmatchedMethodForLocal"), nameof (local));
      if (opcode.Equals(OpCodes.Ldloc))
      {
        switch (localIndex)
        {
          case 0:
            opcode = OpCodes.Ldloc_0;
            break;
          case 1:
            opcode = OpCodes.Ldloc_1;
            break;
          case 2:
            opcode = OpCodes.Ldloc_2;
            break;
          case 3:
            opcode = OpCodes.Ldloc_3;
            break;
          default:
            if (localIndex <= (int) byte.MaxValue)
            {
              opcode = OpCodes.Ldloc_S;
              break;
            }
            break;
        }
      }
      else if (opcode.Equals(OpCodes.Stloc))
      {
        switch (localIndex)
        {
          case 0:
            opcode = OpCodes.Stloc_0;
            break;
          case 1:
            opcode = OpCodes.Stloc_1;
            break;
          case 2:
            opcode = OpCodes.Stloc_2;
            break;
          case 3:
            opcode = OpCodes.Stloc_3;
            break;
          default:
            if (localIndex <= (int) byte.MaxValue)
            {
              opcode = OpCodes.Stloc_S;
              break;
            }
            break;
        }
      }
      else if (opcode.Equals(OpCodes.Ldloca) && localIndex <= (int) byte.MaxValue)
        opcode = OpCodes.Ldloca_S;
      this.EnsureCapacity(7);
      this.InternalEmit(opcode);
      if (opcode.OperandType == OperandType.InlineNone)
        return;
      if (!OpCodes.TakesSingleByteArgument(opcode))
      {
        this.m_ILStream[this.m_length++] = (byte) localIndex;
        this.m_ILStream[this.m_length++] = (byte) (localIndex >> 8);
      }
      else
      {
        if (localIndex > (int) byte.MaxValue)
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadInstructionOrIndexOutOfBound"));
        this.m_ILStream[this.m_length++] = (byte) localIndex;
      }
    }

    /// <summary>
    ///   Задает начало блока исключения для нефильтруемого исключения.
    /// </summary>
    /// <returns>
    ///   Метка конца блока.
    ///    Это позволит оставить в нужное место для выполнения финальных блоков или прекращения данной попытки.
    /// </returns>
    public virtual Label BeginExceptionBlock()
    {
      if (this.m_exceptions == null)
        this.m_exceptions = new __ExceptionInfo[2];
      if (this.m_currExcStack == null)
        this.m_currExcStack = new __ExceptionInfo[2];
      if (this.m_exceptionCount >= this.m_exceptions.Length)
        this.m_exceptions = ILGenerator.EnlargeArray(this.m_exceptions);
      if (this.m_currExcStackCount >= this.m_currExcStack.Length)
        this.m_currExcStack = ILGenerator.EnlargeArray(this.m_currExcStack);
      Label endLabel = this.DefineLabel();
      __ExceptionInfo exceptionInfo = new __ExceptionInfo(this.m_length, endLabel);
      this.m_exceptions[this.m_exceptionCount++] = exceptionInfo;
      this.m_currExcStack[this.m_currExcStackCount++] = exceptionInfo;
      return endLabel;
    }

    /// <summary>Задает конец блока исключения.</summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Конец блока исключения находится в неподходящем месте в потоке кода.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Промежуточного языка MSIL в данный момент не находится в блоке исключения.
    /// </exception>
    public virtual void EndExceptionBlock()
    {
      if (this.m_currExcStackCount == 0)
        throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
      __ExceptionInfo currExc = this.m_currExcStack[this.m_currExcStackCount - 1];
      this.m_currExcStack[this.m_currExcStackCount - 1] = (__ExceptionInfo) null;
      --this.m_currExcStackCount;
      Label endLabel = currExc.GetEndLabel();
      switch (currExc.GetCurrentState())
      {
        case 0:
        case 1:
          throw new InvalidOperationException(Environment.GetResourceString("Argument_BadExceptionCodeGen"));
        case 2:
          this.Emit(OpCodes.Leave, endLabel);
          break;
        case 3:
        case 4:
          this.Emit(OpCodes.Endfinally);
          break;
      }
      if (this.m_labelList[endLabel.GetLabelValue()] == -1)
        this.MarkLabel(endLabel);
      else
        this.MarkLabel(currExc.GetFinallyEndLabel());
      currExc.Done(this.m_length);
    }

    /// <summary>
    ///   Задает начало блока исключения для фильтруемого исключения.
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">
    ///   Промежуточного языка MSIL в данный момент не находится в блоке исключения.
    /// 
    ///   -или-
    /// 
    ///   Это <see cref="T:System.Reflection.Emit.ILGenerator" /> принадлежит <see cref="T:System.Reflection.Emit.DynamicMethod" />.
    /// </exception>
    public virtual void BeginExceptFilterBlock()
    {
      if (this.m_currExcStackCount == 0)
        throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
      __ExceptionInfo currExc = this.m_currExcStack[this.m_currExcStackCount - 1];
      Label endLabel = currExc.GetEndLabel();
      this.Emit(OpCodes.Leave, endLabel);
      currExc.MarkFilterAddr(this.m_length);
    }

    /// <summary>Начинает блок catch.</summary>
    /// <param name="exceptionType">
    ///   <see cref="T:System.Type" /> Объект, представляющий исключение.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Блок catch находится внутри фильтруемого исключения.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   <paramref name="exceptionType" /> — <see langword="null" />, а блок фильтра исключений не вернул значение, указывающее, что наконец блоки должны выполняться, пока этот блок catch находится.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Промежуточного языка MSIL в данный момент не находится в блоке исключения.
    /// </exception>
    public virtual void BeginCatchBlock(Type exceptionType)
    {
      if (this.m_currExcStackCount == 0)
        throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
      __ExceptionInfo currExc = this.m_currExcStack[this.m_currExcStackCount - 1];
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
        Label endLabel = currExc.GetEndLabel();
        this.Emit(OpCodes.Leave, endLabel);
      }
      currExc.MarkCatchAddr(this.m_length, exceptionType);
    }

    /// <summary>
    ///   Задает начало блока ошибки исключения в потоке инструкций промежуточного языка MSIL.
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">
    ///   Созданные инструкции MSIL не находится в блоке исключения.
    /// 
    ///   -или-
    /// 
    ///   Это <see cref="T:System.Reflection.Emit.ILGenerator" /> принадлежит <see cref="T:System.Reflection.Emit.DynamicMethod" />.
    /// </exception>
    public virtual void BeginFaultBlock()
    {
      if (this.m_currExcStackCount == 0)
        throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
      __ExceptionInfo currExc = this.m_currExcStack[this.m_currExcStackCount - 1];
      Label endLabel = currExc.GetEndLabel();
      this.Emit(OpCodes.Leave, endLabel);
      currExc.MarkFaultAddr(this.m_length);
    }

    /// <summary>
    ///   Начинает блок finally в потоке инструкций промежуточного языка MSIL.
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">
    ///   Созданные инструкции MSIL не находится в блоке исключения.
    /// </exception>
    public virtual void BeginFinallyBlock()
    {
      if (this.m_currExcStackCount == 0)
        throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
      __ExceptionInfo currExc = this.m_currExcStack[this.m_currExcStackCount - 1];
      int currentState = currExc.GetCurrentState();
      Label endLabel = currExc.GetEndLabel();
      int endCatchAddr = 0;
      if (currentState != 0)
      {
        this.Emit(OpCodes.Leave, endLabel);
        endCatchAddr = this.m_length;
      }
      this.MarkLabel(endLabel);
      Label label = this.DefineLabel();
      currExc.SetFinallyEndLabel(label);
      this.Emit(OpCodes.Leave, label);
      if (endCatchAddr == 0)
        endCatchAddr = this.m_length;
      currExc.MarkFinallyAddr(this.m_length, endCatchAddr);
    }

    /// <summary>Объявляет новую метку.</summary>
    /// <returns>
    ///   Возвращает новую метку, которую можно использовать как маркер при переходах.
    /// </returns>
    public virtual Label DefineLabel()
    {
      if (this.m_labelList == null)
        this.m_labelList = new int[4];
      if (this.m_labelCount >= this.m_labelList.Length)
        this.m_labelList = ILGenerator.EnlargeArray(this.m_labelList);
      this.m_labelList[this.m_labelCount] = -1;
      return new Label(this.m_labelCount++);
    }

    /// <summary>
    ///   Отмечает текущую позицию потока Microsoft промежуточного языка MSIL с указанной меткой.
    /// </summary>
    /// <param name="loc">
    ///   Метка, для которой следует установить индекс.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="loc" /> Представляет недопустимый индекс массива меток.
    /// 
    ///   -или-
    /// 
    ///   Индекс для <paramref name="loc" /> уже определен.
    /// </exception>
    public virtual void MarkLabel(Label loc)
    {
      int labelValue = loc.GetLabelValue();
      if (labelValue < 0 || labelValue >= this.m_labelList.Length)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidLabel"));
      if (this.m_labelList[labelValue] != -1)
        throw new ArgumentException(Environment.GetResourceString("Argument_RedefinedLabel"));
      this.m_labelList[labelValue] = this.m_length;
    }

    /// <summary>Создает инструкцию вызова исключения.</summary>
    /// <param name="excType">Класс типа исключения.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="excType" /> не <see cref="T:System.Exception" /> класса или класса, производного от <see cref="T:System.Exception" />.
    /// 
    ///   -или-
    /// 
    ///   Тип не имеет конструктора по умолчанию.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="excType" /> имеет значение <see langword="null" />.
    /// </exception>
    public virtual void ThrowException(Type excType)
    {
      if (excType == (Type) null)
        throw new ArgumentNullException(nameof (excType));
      if (!excType.IsSubclassOf(typeof (Exception)) && excType != typeof (Exception))
        throw new ArgumentException(Environment.GetResourceString("Argument_NotExceptionType"));
      ConstructorInfo constructor = excType.GetConstructor(Type.EmptyTypes);
      if (constructor == (ConstructorInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MissingDefaultConstructor"));
      this.Emit(OpCodes.Newobj, constructor);
      this.Emit(OpCodes.Throw);
    }

    /// <summary>
    ///   Создает промежуточный язык Microsoft (MSIL), для вызова <see cref="Overload:System.Console.WriteLine" /> со строкой.
    /// </summary>
    /// <param name="value">Строка для печати.</param>
    public virtual void EmitWriteLine(string value)
    {
      this.Emit(OpCodes.Ldstr, value);
      MethodInfo method = typeof (Console).GetMethod("WriteLine", new Type[1]
      {
        typeof (string)
      });
      this.Emit(OpCodes.Call, method);
    }

    /// <summary>
    ///   Создает язык MSIL, требуемый для вызова <see cref="Overload:System.Console.WriteLine" /> с заданной локальной переменной.
    /// </summary>
    /// <param name="localBuilder">
    ///   Локальная переменная, значение которой выводится на консоль.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Тип <paramref name="localBuilder" /> представлен <see cref="T:System.Reflection.Emit.TypeBuilder" /> или <see cref="T:System.Reflection.Emit.EnumBuilder" /> (не поддерживается).
    /// 
    ///   -или-
    /// 
    ///   Отсутствует перегрузка <see cref="Overload:System.Console.WriteLine" /> (принимает тип <paramref name="localBuilder" />).
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="localBuilder" /> имеет значение <see langword="null" />.
    /// </exception>
    public virtual void EmitWriteLine(LocalBuilder localBuilder)
    {
      if (this.m_methodBuilder == (MethodInfo) null)
        throw new ArgumentException(Environment.GetResourceString("InvalidOperation_BadILGeneratorUsage"));
      MethodInfo method1 = typeof (Console).GetMethod("get_Out");
      this.Emit(OpCodes.Call, method1);
      this.Emit(OpCodes.Ldloc, localBuilder);
      Type[] types = new Type[1];
      object localType = (object) localBuilder.LocalType;
      if (localType is TypeBuilder || localType is EnumBuilder)
        throw new ArgumentException(Environment.GetResourceString("NotSupported_OutputStreamUsingTypeBuilder"));
      types[0] = (Type) localType;
      MethodInfo method2 = typeof (TextWriter).GetMethod("WriteLine", types);
      if (method2 == (MethodInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmitWriteLineType"), nameof (localBuilder));
      this.Emit(OpCodes.Callvirt, method2);
    }

    /// <summary>
    ///   Выдает промежуточного языка MSIL, необходимые для вызова метода <see cref="Overload:System.Console.WriteLine" /> с указанным полем.
    /// </summary>
    /// <param name="fld">
    ///   Поле, значение которого выводятся на консоль.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Нет ни одна перегрузка <see cref="Overload:System.Console.WriteLine" /> метода, принимающего тип указанного поля.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="fld" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Тип поля — <see cref="T:System.Reflection.Emit.TypeBuilder" /> или <see cref="T:System.Reflection.Emit.EnumBuilder" />, который не поддерживается.
    /// </exception>
    public virtual void EmitWriteLine(FieldInfo fld)
    {
      if (fld == (FieldInfo) null)
        throw new ArgumentNullException(nameof (fld));
      MethodInfo method1 = typeof (Console).GetMethod("get_Out");
      this.Emit(OpCodes.Call, method1);
      if ((fld.Attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope)
      {
        this.Emit(OpCodes.Ldsfld, fld);
      }
      else
      {
        this.Emit(OpCodes.Ldarg, (short) 0);
        this.Emit(OpCodes.Ldfld, fld);
      }
      Type[] types = new Type[1];
      object fieldType = (object) fld.FieldType;
      if (fieldType is TypeBuilder || fieldType is EnumBuilder)
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_OutputStreamUsingTypeBuilder"));
      types[0] = (Type) fieldType;
      MethodInfo method2 = typeof (TextWriter).GetMethod("WriteLine", types);
      if (method2 == (MethodInfo) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmitWriteLineType"), nameof (fld));
      this.Emit(OpCodes.Callvirt, method2);
    }

    /// <summary>Объявляет локальную переменную указанного типа.</summary>
    /// <param name="localType">
    ///   Объект <see cref="T:System.Type" /> представляющий тип локальной переменной.
    /// </param>
    /// <returns>Объявленная локальная переменная.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="localType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Содержащий тип был создан с <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> метод.
    /// </exception>
    public virtual LocalBuilder DeclareLocal(Type localType)
    {
      return this.DeclareLocal(localType, false);
    }

    /// <summary>
    ///   Объявляет локальную переменную указанного типа, при необходимости Закрепление объекта, на который ссылается переменная.
    /// </summary>
    /// <param name="localType">
    ///   Объект <see cref="T:System.Type" /> представляющий тип локальной переменной.
    /// </param>
    /// <param name="pinned">
    ///   <see langword="true" /> для закрепления объекта в памяти; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.Reflection.Emit.LocalBuilder" /> объект, который представляет локальную переменную.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="localType" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Содержащий тип был создан с <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> метод.
    /// 
    ///   -или-
    /// 
    ///   Созданные тело метода, включающего метода <see cref="M:System.Reflection.Emit.MethodBuilder.CreateMethodBody(System.Byte[],System.Int32)" /> метод.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Метод, с помощью которого <see cref="T:System.Reflection.Emit.ILGenerator" /> связан не представлены <see cref="T:System.Reflection.Emit.MethodBuilder" />.
    /// </exception>
    public virtual LocalBuilder DeclareLocal(Type localType, bool pinned)
    {
      MethodBuilder methodBuilder = this.m_methodBuilder as MethodBuilder;
      if ((MethodInfo) methodBuilder == (MethodInfo) null)
        throw new NotSupportedException();
      if (methodBuilder.IsTypeCreated())
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeHasBeenCreated"));
      if (localType == (Type) null)
        throw new ArgumentNullException(nameof (localType));
      if (methodBuilder.m_bIsBaked)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MethodBaked"));
      this.m_localSignature.AddArgument(localType, pinned);
      LocalBuilder localBuilder = new LocalBuilder(this.m_localCount, localType, (MethodInfo) methodBuilder, pinned);
      ++this.m_localCount;
      return localBuilder;
    }

    /// <summary>
    ///   Задает пространство имен для использования в определения значений локальных переменных и следит за текущей активной лексической области видимости.
    /// </summary>
    /// <param name="usingNamespace">
    ///   Пространство имен для использования в оценке локальных переменных и контрольных значений для текущей активной лексической области видимости
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   Длина параметра <paramref name="usingNamespace" /> равна нулю.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="usingNamespace" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Это <see cref="T:System.Reflection.Emit.ILGenerator" /> принадлежит <see cref="T:System.Reflection.Emit.DynamicMethod" />.
    /// </exception>
    public virtual void UsingNamespace(string usingNamespace)
    {
      if (usingNamespace == null)
        throw new ArgumentNullException(nameof (usingNamespace));
      if (usingNamespace.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), nameof (usingNamespace));
      MethodBuilder methodBuilder = this.m_methodBuilder as MethodBuilder;
      if ((MethodInfo) methodBuilder == (MethodInfo) null)
        throw new NotSupportedException();
      if (methodBuilder.GetILGenerator().m_ScopeTree.GetCurrentActiveScopeIndex() == -1)
        methodBuilder.m_localSymInfo.AddUsingNamespace(usingNamespace);
      else
        this.m_ScopeTree.AddUsingNamespaceToCurrentScope(usingNamespace);
    }

    /// <summary>
    ///   Задает точку следования в потоке инструкций промежуточного языка MSIL.
    /// </summary>
    /// <param name="document">
    ///   Документ, для которого определяется точка следования.
    /// </param>
    /// <param name="startLine">
    ///   Строка, где начинается точка следования.
    /// </param>
    /// <param name="startColumn">
    ///   Столбец в строке, где начинается точка следования.
    /// </param>
    /// <param name="endLine">
    ///   Строка, где заканчивается точка следования.
    /// </param>
    /// <param name="endColumn">
    ///   Столбец в строке, где заканчивается точка следования.
    /// </param>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="startLine" /> или <paramref name="endLine" /> — &lt; = 0.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    ///   Это <see cref="T:System.Reflection.Emit.ILGenerator" /> принадлежит <see cref="T:System.Reflection.Emit.DynamicMethod" />.
    /// </exception>
    public virtual void MarkSequencePoint(ISymbolDocumentWriter document, int startLine, int startColumn, int endLine, int endColumn)
    {
      if (startLine == 0 || startLine < 0 || (endLine == 0 || endLine < 0))
        throw new ArgumentOutOfRangeException(nameof (startLine));
      this.m_LineNumberInfo.AddLineNumberInfo(document, this.m_length, startLine, startColumn, endLine, endColumn);
    }

    /// <summary>Начинает лексической области видимости.</summary>
    /// <exception cref="T:System.NotSupportedException">
    ///   Это <see cref="T:System.Reflection.Emit.ILGenerator" /> принадлежит <see cref="T:System.Reflection.Emit.DynamicMethod" />.
    /// </exception>
    public virtual void BeginScope()
    {
      this.m_ScopeTree.AddScopeInfo(ScopeAction.Open, this.m_length);
    }

    /// <summary>Завершает лексической области видимости.</summary>
    /// <exception cref="T:System.NotSupportedException">
    ///   Это <see cref="T:System.Reflection.Emit.ILGenerator" /> принадлежит <see cref="T:System.Reflection.Emit.DynamicMethod" />.
    /// </exception>
    public virtual void EndScope()
    {
      this.m_ScopeTree.AddScopeInfo(ScopeAction.Close, this.m_length);
    }

    /// <summary>
    ///   Возвращает текущее смещение в байтах в потоке промежуточному языку (MSIL), выдаваемой по <see cref="T:System.Reflection.Emit.ILGenerator" />.
    /// </summary>
    /// <returns>
    ///   Смещение в потоке MSIL будут сформированы следующую инструкцию.
    /// </returns>
    public virtual int ILOffset
    {
      get
      {
        return this.m_length;
      }
    }

    void _ILGenerator.GetTypeInfoCount(out uint pcTInfo)
    {
      throw new NotImplementedException();
    }

    void _ILGenerator.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
    {
      throw new NotImplementedException();
    }

    void _ILGenerator.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
    {
      throw new NotImplementedException();
    }

    void _ILGenerator.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
    {
      throw new NotImplementedException();
    }
  }
}

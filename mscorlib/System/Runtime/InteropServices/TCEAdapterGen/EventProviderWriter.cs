// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TCEAdapterGen.EventProviderWriter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

namespace System.Runtime.InteropServices.TCEAdapterGen
{
  internal class EventProviderWriter
  {
    private readonly Type[] MonitorEnterParamTypes = new Type[2]
    {
      typeof (object),
      Type.GetType("System.Boolean&")
    };
    private const BindingFlags DefaultLookup = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
    private ModuleBuilder m_OutputModule;
    private string m_strDestTypeName;
    private Type m_EventItfType;
    private Type m_SrcItfType;
    private Type m_SinkHelperType;

    public EventProviderWriter(ModuleBuilder OutputModule, string strDestTypeName, Type EventItfType, Type SrcItfType, Type SinkHelperType)
    {
      this.m_OutputModule = OutputModule;
      this.m_strDestTypeName = strDestTypeName;
      this.m_EventItfType = EventItfType;
      this.m_SrcItfType = SrcItfType;
      this.m_SinkHelperType = SinkHelperType;
    }

    public Type Perform()
    {
      TypeBuilder OutputTypeBuilder = this.m_OutputModule.DefineType(this.m_strDestTypeName, TypeAttributes.Sealed, typeof (object), new Type[2]
      {
        this.m_EventItfType,
        typeof (IDisposable)
      });
      FieldBuilder fbCPC = OutputTypeBuilder.DefineField("m_ConnectionPointContainer", typeof (IConnectionPointContainer), FieldAttributes.Private);
      FieldBuilder fieldBuilder = OutputTypeBuilder.DefineField("m_aEventSinkHelpers", typeof (ArrayList), FieldAttributes.Private);
      FieldBuilder fbEventCP = OutputTypeBuilder.DefineField("m_ConnectionPoint", typeof (IConnectionPoint), FieldAttributes.Private);
      MethodBuilder mbInitSrcItf = this.DefineInitSrcItfMethod(OutputTypeBuilder, this.m_SrcItfType, fieldBuilder, fbEventCP, fbCPC);
      MethodInfo[] nonPropertyMethods = TCEAdapterGenerator.GetNonPropertyMethods(this.m_SrcItfType);
      for (int index = 0; index < nonPropertyMethods.Length; ++index)
      {
        if (this.m_SrcItfType == nonPropertyMethods[index].DeclaringType)
        {
          this.DefineAddEventMethod(OutputTypeBuilder, nonPropertyMethods[index], this.m_SinkHelperType, fieldBuilder, fbEventCP, mbInitSrcItf);
          this.DefineRemoveEventMethod(OutputTypeBuilder, nonPropertyMethods[index], this.m_SinkHelperType, fieldBuilder, fbEventCP);
        }
      }
      this.DefineConstructor(OutputTypeBuilder, fbCPC);
      MethodBuilder FinalizeMethod = this.DefineFinalizeMethod(OutputTypeBuilder, this.m_SinkHelperType, fieldBuilder, fbEventCP);
      this.DefineDisposeMethod(OutputTypeBuilder, FinalizeMethod);
      return OutputTypeBuilder.CreateType();
    }

    private MethodBuilder DefineAddEventMethod(TypeBuilder OutputTypeBuilder, MethodInfo SrcItfMethod, Type SinkHelperClass, FieldBuilder fbSinkHelperArray, FieldBuilder fbEventCP, MethodBuilder mbInitSrcItf)
    {
      FieldInfo field1 = SinkHelperClass.GetField("m_" + SrcItfMethod.Name + "Delegate");
      FieldInfo field2 = SinkHelperClass.GetField("m_dwCookie");
      ConstructorInfo constructor = SinkHelperClass.GetConstructor(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, (Binder) null, new Type[0], (ParameterModifier[]) null);
      MethodInfo method1 = typeof (IConnectionPoint).GetMethod("Advise");
      Type[] types = new Type[1]{ typeof (object) };
      MethodInfo method2 = typeof (ArrayList).GetMethod("Add", types, (ParameterModifier[]) null);
      MethodInfo method3 = typeof (Monitor).GetMethod("Enter", this.MonitorEnterParamTypes, (ParameterModifier[]) null);
      types[0] = typeof (object);
      MethodInfo method4 = typeof (Monitor).GetMethod("Exit", types, (ParameterModifier[]) null);
      Type[] parameterTypes = new Type[1]
      {
        field1.FieldType
      };
      MethodBuilder methodBuilder = OutputTypeBuilder.DefineMethod("add_" + SrcItfMethod.Name, MethodAttributes.Public | MethodAttributes.Virtual, (Type) null, parameterTypes);
      ILGenerator ilGenerator = methodBuilder.GetILGenerator();
      Label label1 = ilGenerator.DefineLabel();
      LocalBuilder local1 = ilGenerator.DeclareLocal(SinkHelperClass);
      LocalBuilder local2 = ilGenerator.DeclareLocal(typeof (int));
      LocalBuilder local3 = ilGenerator.DeclareLocal(typeof (bool));
      ilGenerator.BeginExceptionBlock();
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldloca_S, local3);
      ilGenerator.Emit(OpCodes.Call, method3);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldfld, (FieldInfo) fbEventCP);
      ilGenerator.Emit(OpCodes.Brtrue, label1);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Call, (MethodInfo) mbInitSrcItf);
      ilGenerator.MarkLabel(label1);
      ilGenerator.Emit(OpCodes.Newobj, constructor);
      ilGenerator.Emit(OpCodes.Stloc, local1);
      ilGenerator.Emit(OpCodes.Ldc_I4_0);
      ilGenerator.Emit(OpCodes.Stloc, local2);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldfld, (FieldInfo) fbEventCP);
      ilGenerator.Emit(OpCodes.Ldloc, local1);
      ilGenerator.Emit(OpCodes.Castclass, typeof (object));
      ilGenerator.Emit(OpCodes.Ldloca, local2);
      ilGenerator.Emit(OpCodes.Callvirt, method1);
      ilGenerator.Emit(OpCodes.Ldloc, local1);
      ilGenerator.Emit(OpCodes.Ldloc, local2);
      ilGenerator.Emit(OpCodes.Stfld, field2);
      ilGenerator.Emit(OpCodes.Ldloc, local1);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 1);
      ilGenerator.Emit(OpCodes.Stfld, field1);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldfld, (FieldInfo) fbSinkHelperArray);
      ilGenerator.Emit(OpCodes.Ldloc, local1);
      ilGenerator.Emit(OpCodes.Castclass, typeof (object));
      ilGenerator.Emit(OpCodes.Callvirt, method2);
      ilGenerator.Emit(OpCodes.Pop);
      ilGenerator.BeginFinallyBlock();
      Label label2 = ilGenerator.DefineLabel();
      ilGenerator.Emit(OpCodes.Ldloc, local3);
      ilGenerator.Emit(OpCodes.Brfalse_S, label2);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Call, method4);
      ilGenerator.MarkLabel(label2);
      ilGenerator.EndExceptionBlock();
      ilGenerator.Emit(OpCodes.Ret);
      return methodBuilder;
    }

    private MethodBuilder DefineRemoveEventMethod(TypeBuilder OutputTypeBuilder, MethodInfo SrcItfMethod, Type SinkHelperClass, FieldBuilder fbSinkHelperArray, FieldBuilder fbEventCP)
    {
      FieldInfo field1 = SinkHelperClass.GetField("m_" + SrcItfMethod.Name + "Delegate");
      FieldInfo field2 = SinkHelperClass.GetField("m_dwCookie");
      Type[] types = new Type[1]{ typeof (int) };
      MethodInfo method1 = typeof (ArrayList).GetMethod("RemoveAt", types, (ParameterModifier[]) null);
      MethodInfo getMethod1 = typeof (ArrayList).GetProperty("Item").GetGetMethod();
      MethodInfo getMethod2 = typeof (ArrayList).GetProperty("Count").GetGetMethod();
      types[0] = typeof (Delegate);
      MethodInfo method2 = typeof (Delegate).GetMethod("Equals", types, (ParameterModifier[]) null);
      MethodInfo method3 = typeof (Monitor).GetMethod("Enter", this.MonitorEnterParamTypes, (ParameterModifier[]) null);
      types[0] = typeof (object);
      MethodInfo method4 = typeof (Monitor).GetMethod("Exit", types, (ParameterModifier[]) null);
      MethodInfo method5 = typeof (IConnectionPoint).GetMethod("Unadvise");
      MethodInfo method6 = typeof (Marshal).GetMethod("ReleaseComObject");
      Type[] parameterTypes = new Type[1]
      {
        field1.FieldType
      };
      MethodBuilder methodBuilder = OutputTypeBuilder.DefineMethod("remove_" + SrcItfMethod.Name, MethodAttributes.Public | MethodAttributes.Virtual, (Type) null, parameterTypes);
      ILGenerator ilGenerator = methodBuilder.GetILGenerator();
      LocalBuilder local1 = ilGenerator.DeclareLocal(typeof (int));
      LocalBuilder local2 = ilGenerator.DeclareLocal(typeof (int));
      LocalBuilder local3 = ilGenerator.DeclareLocal(SinkHelperClass);
      LocalBuilder local4 = ilGenerator.DeclareLocal(typeof (bool));
      Label label1 = ilGenerator.DefineLabel();
      Label label2 = ilGenerator.DefineLabel();
      Label label3 = ilGenerator.DefineLabel();
      ilGenerator.DefineLabel();
      ilGenerator.BeginExceptionBlock();
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldloca_S, local4);
      ilGenerator.Emit(OpCodes.Call, method3);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldfld, (FieldInfo) fbSinkHelperArray);
      ilGenerator.Emit(OpCodes.Brfalse, label2);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldfld, (FieldInfo) fbSinkHelperArray);
      ilGenerator.Emit(OpCodes.Callvirt, getMethod2);
      ilGenerator.Emit(OpCodes.Stloc, local1);
      ilGenerator.Emit(OpCodes.Ldc_I4, 0);
      ilGenerator.Emit(OpCodes.Stloc, local2);
      ilGenerator.Emit(OpCodes.Ldc_I4, 0);
      ilGenerator.Emit(OpCodes.Ldloc, local1);
      ilGenerator.Emit(OpCodes.Bge, label2);
      ilGenerator.MarkLabel(label1);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldfld, (FieldInfo) fbSinkHelperArray);
      ilGenerator.Emit(OpCodes.Ldloc, local2);
      ilGenerator.Emit(OpCodes.Callvirt, getMethod1);
      ilGenerator.Emit(OpCodes.Castclass, SinkHelperClass);
      ilGenerator.Emit(OpCodes.Stloc, local3);
      ilGenerator.Emit(OpCodes.Ldloc, local3);
      ilGenerator.Emit(OpCodes.Ldfld, field1);
      ilGenerator.Emit(OpCodes.Ldnull);
      ilGenerator.Emit(OpCodes.Beq, label3);
      ilGenerator.Emit(OpCodes.Ldloc, local3);
      ilGenerator.Emit(OpCodes.Ldfld, field1);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 1);
      ilGenerator.Emit(OpCodes.Castclass, typeof (object));
      ilGenerator.Emit(OpCodes.Callvirt, method2);
      ilGenerator.Emit(OpCodes.Ldc_I4, (int) byte.MaxValue);
      ilGenerator.Emit(OpCodes.And);
      ilGenerator.Emit(OpCodes.Ldc_I4, 0);
      ilGenerator.Emit(OpCodes.Beq, label3);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldfld, (FieldInfo) fbSinkHelperArray);
      ilGenerator.Emit(OpCodes.Ldloc, local2);
      ilGenerator.Emit(OpCodes.Callvirt, method1);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldfld, (FieldInfo) fbEventCP);
      ilGenerator.Emit(OpCodes.Ldloc, local3);
      ilGenerator.Emit(OpCodes.Ldfld, field2);
      ilGenerator.Emit(OpCodes.Callvirt, method5);
      ilGenerator.Emit(OpCodes.Ldloc, local1);
      ilGenerator.Emit(OpCodes.Ldc_I4, 1);
      ilGenerator.Emit(OpCodes.Bgt, label2);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldfld, (FieldInfo) fbEventCP);
      ilGenerator.Emit(OpCodes.Call, method6);
      ilGenerator.Emit(OpCodes.Pop);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldnull);
      ilGenerator.Emit(OpCodes.Stfld, (FieldInfo) fbEventCP);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldnull);
      ilGenerator.Emit(OpCodes.Stfld, (FieldInfo) fbSinkHelperArray);
      ilGenerator.Emit(OpCodes.Br, label2);
      ilGenerator.MarkLabel(label3);
      ilGenerator.Emit(OpCodes.Ldloc, local2);
      ilGenerator.Emit(OpCodes.Ldc_I4, 1);
      ilGenerator.Emit(OpCodes.Add);
      ilGenerator.Emit(OpCodes.Stloc, local2);
      ilGenerator.Emit(OpCodes.Ldloc, local2);
      ilGenerator.Emit(OpCodes.Ldloc, local1);
      ilGenerator.Emit(OpCodes.Blt, label1);
      ilGenerator.MarkLabel(label2);
      ilGenerator.BeginFinallyBlock();
      Label label4 = ilGenerator.DefineLabel();
      ilGenerator.Emit(OpCodes.Ldloc, local4);
      ilGenerator.Emit(OpCodes.Brfalse_S, label4);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Call, method4);
      ilGenerator.MarkLabel(label4);
      ilGenerator.EndExceptionBlock();
      ilGenerator.Emit(OpCodes.Ret);
      return methodBuilder;
    }

    private MethodBuilder DefineInitSrcItfMethod(TypeBuilder OutputTypeBuilder, Type SourceInterface, FieldBuilder fbSinkHelperArray, FieldBuilder fbEventCP, FieldBuilder fbCPC)
    {
      ConstructorInfo constructor1 = typeof (ArrayList).GetConstructor(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, new Type[0], (ParameterModifier[]) null);
      byte[] numArray = new byte[16];
      ConstructorInfo constructor2 = typeof (Guid).GetConstructor(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, new Type[1]
      {
        typeof (byte[])
      }, (ParameterModifier[]) null);
      MethodInfo method = typeof (IConnectionPointContainer).GetMethod("FindConnectionPoint");
      MethodBuilder methodBuilder = OutputTypeBuilder.DefineMethod("Init", MethodAttributes.Private, (Type) null, (Type[]) null);
      ILGenerator ilGenerator = methodBuilder.GetILGenerator();
      LocalBuilder local1 = ilGenerator.DeclareLocal(typeof (IConnectionPoint));
      LocalBuilder local2 = ilGenerator.DeclareLocal(typeof (Guid));
      LocalBuilder local3 = ilGenerator.DeclareLocal(typeof (byte[]));
      ilGenerator.Emit(OpCodes.Ldnull);
      ilGenerator.Emit(OpCodes.Stloc, local1);
      byte[] byteArray = SourceInterface.GUID.ToByteArray();
      ilGenerator.Emit(OpCodes.Ldc_I4, 16);
      ilGenerator.Emit(OpCodes.Newarr, typeof (byte));
      ilGenerator.Emit(OpCodes.Stloc, local3);
      for (int index = 0; index < 16; ++index)
      {
        ilGenerator.Emit(OpCodes.Ldloc, local3);
        ilGenerator.Emit(OpCodes.Ldc_I4, index);
        ilGenerator.Emit(OpCodes.Ldc_I4, (int) byteArray[index]);
        ilGenerator.Emit(OpCodes.Stelem_I1);
      }
      ilGenerator.Emit(OpCodes.Ldloca, local2);
      ilGenerator.Emit(OpCodes.Ldloc, local3);
      ilGenerator.Emit(OpCodes.Call, constructor2);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldfld, (FieldInfo) fbCPC);
      ilGenerator.Emit(OpCodes.Ldloca, local2);
      ilGenerator.Emit(OpCodes.Ldloca, local1);
      ilGenerator.Emit(OpCodes.Callvirt, method);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldloc, local1);
      ilGenerator.Emit(OpCodes.Castclass, typeof (IConnectionPoint));
      ilGenerator.Emit(OpCodes.Stfld, (FieldInfo) fbEventCP);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Newobj, constructor1);
      ilGenerator.Emit(OpCodes.Stfld, (FieldInfo) fbSinkHelperArray);
      ilGenerator.Emit(OpCodes.Ret);
      return methodBuilder;
    }

    private void DefineConstructor(TypeBuilder OutputTypeBuilder, FieldBuilder fbCPC)
    {
      ConstructorInfo constructor = typeof (object).GetConstructor(BindingFlags.Instance | BindingFlags.Public, (Binder) null, new Type[0], (ParameterModifier[]) null);
      MethodAttributes attributes = MethodAttributes.SpecialName | constructor.Attributes & MethodAttributes.MemberAccessMask;
      ILGenerator ilGenerator = OutputTypeBuilder.DefineMethod(".ctor", attributes, (Type) null, new Type[1]
      {
        typeof (object)
      }).GetILGenerator();
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Call, constructor);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 1);
      ilGenerator.Emit(OpCodes.Castclass, typeof (IConnectionPointContainer));
      ilGenerator.Emit(OpCodes.Stfld, (FieldInfo) fbCPC);
      ilGenerator.Emit(OpCodes.Ret);
    }

    private MethodBuilder DefineFinalizeMethod(TypeBuilder OutputTypeBuilder, Type SinkHelperClass, FieldBuilder fbSinkHelper, FieldBuilder fbEventCP)
    {
      FieldInfo field = SinkHelperClass.GetField("m_dwCookie");
      MethodInfo getMethod1 = typeof (ArrayList).GetProperty("Item").GetGetMethod();
      MethodInfo getMethod2 = typeof (ArrayList).GetProperty("Count").GetGetMethod();
      MethodInfo method1 = typeof (IConnectionPoint).GetMethod("Unadvise");
      MethodInfo method2 = typeof (Marshal).GetMethod("ReleaseComObject");
      MethodInfo method3 = typeof (Monitor).GetMethod("Enter", this.MonitorEnterParamTypes, (ParameterModifier[]) null);
      MethodInfo method4 = typeof (Monitor).GetMethod("Exit", new Type[1]
      {
        typeof (object)
      }, (ParameterModifier[]) null);
      MethodBuilder methodBuilder = OutputTypeBuilder.DefineMethod("Finalize", MethodAttributes.Public | MethodAttributes.Virtual, (Type) null, (Type[]) null);
      ILGenerator ilGenerator = methodBuilder.GetILGenerator();
      LocalBuilder local1 = ilGenerator.DeclareLocal(typeof (int));
      LocalBuilder local2 = ilGenerator.DeclareLocal(typeof (int));
      LocalBuilder local3 = ilGenerator.DeclareLocal(SinkHelperClass);
      LocalBuilder local4 = ilGenerator.DeclareLocal(typeof (bool));
      ilGenerator.BeginExceptionBlock();
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldloca_S, local4);
      ilGenerator.Emit(OpCodes.Call, method3);
      Label label1 = ilGenerator.DefineLabel();
      Label label2 = ilGenerator.DefineLabel();
      Label label3 = ilGenerator.DefineLabel();
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldfld, (FieldInfo) fbEventCP);
      ilGenerator.Emit(OpCodes.Brfalse, label3);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldfld, (FieldInfo) fbSinkHelper);
      ilGenerator.Emit(OpCodes.Callvirt, getMethod2);
      ilGenerator.Emit(OpCodes.Stloc, local1);
      ilGenerator.Emit(OpCodes.Ldc_I4, 0);
      ilGenerator.Emit(OpCodes.Stloc, local2);
      ilGenerator.Emit(OpCodes.Ldc_I4, 0);
      ilGenerator.Emit(OpCodes.Ldloc, local1);
      ilGenerator.Emit(OpCodes.Bge, label2);
      ilGenerator.MarkLabel(label1);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldfld, (FieldInfo) fbSinkHelper);
      ilGenerator.Emit(OpCodes.Ldloc, local2);
      ilGenerator.Emit(OpCodes.Callvirt, getMethod1);
      ilGenerator.Emit(OpCodes.Castclass, SinkHelperClass);
      ilGenerator.Emit(OpCodes.Stloc, local3);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldfld, (FieldInfo) fbEventCP);
      ilGenerator.Emit(OpCodes.Ldloc, local3);
      ilGenerator.Emit(OpCodes.Ldfld, field);
      ilGenerator.Emit(OpCodes.Callvirt, method1);
      ilGenerator.Emit(OpCodes.Ldloc, local2);
      ilGenerator.Emit(OpCodes.Ldc_I4, 1);
      ilGenerator.Emit(OpCodes.Add);
      ilGenerator.Emit(OpCodes.Stloc, local2);
      ilGenerator.Emit(OpCodes.Ldloc, local2);
      ilGenerator.Emit(OpCodes.Ldloc, local1);
      ilGenerator.Emit(OpCodes.Blt, label1);
      ilGenerator.MarkLabel(label2);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldfld, (FieldInfo) fbEventCP);
      ilGenerator.Emit(OpCodes.Call, method2);
      ilGenerator.Emit(OpCodes.Pop);
      ilGenerator.MarkLabel(label3);
      ilGenerator.BeginCatchBlock(typeof (Exception));
      ilGenerator.Emit(OpCodes.Pop);
      ilGenerator.BeginFinallyBlock();
      Label label4 = ilGenerator.DefineLabel();
      ilGenerator.Emit(OpCodes.Ldloc, local4);
      ilGenerator.Emit(OpCodes.Brfalse_S, label4);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Call, method4);
      ilGenerator.MarkLabel(label4);
      ilGenerator.EndExceptionBlock();
      ilGenerator.Emit(OpCodes.Ret);
      return methodBuilder;
    }

    private void DefineDisposeMethod(TypeBuilder OutputTypeBuilder, MethodBuilder FinalizeMethod)
    {
      MethodInfo method = typeof (GC).GetMethod("SuppressFinalize");
      ILGenerator ilGenerator = OutputTypeBuilder.DefineMethod("Dispose", MethodAttributes.Public | MethodAttributes.Virtual, (Type) null, (Type[]) null).GetILGenerator();
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Callvirt, (MethodInfo) FinalizeMethod);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Call, method);
      ilGenerator.Emit(OpCodes.Ret);
    }
  }
}

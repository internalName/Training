// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TCEAdapterGen.EventSinkHelperWriter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices.TCEAdapterGen
{
  internal class EventSinkHelperWriter
  {
    public static readonly string GeneratedTypeNamePostfix = "_SinkHelper";
    private Type m_InputType;
    private Type m_EventItfType;
    private ModuleBuilder m_OutputModule;

    public EventSinkHelperWriter(ModuleBuilder OutputModule, Type InputType, Type EventItfType)
    {
      this.m_InputType = InputType;
      this.m_OutputModule = OutputModule;
      this.m_EventItfType = EventItfType;
    }

    public Type Perform()
    {
      Type[] aInterfaceTypes = new Type[1]
      {
        this.m_InputType
      };
      string str1 = (string) null;
      string str2 = NameSpaceExtractor.ExtractNameSpace(this.m_EventItfType.FullName);
      if (str2 != "")
        str1 = str2 + ".";
      TypeBuilder typeBuilder = TCEAdapterGenerator.DefineUniqueType(str1 + this.m_InputType.Name + EventSinkHelperWriter.GeneratedTypeNamePostfix, TypeAttributes.Public | TypeAttributes.Sealed, (Type) null, aInterfaceTypes, this.m_OutputModule);
      TCEAdapterGenerator.SetHiddenAttribute(typeBuilder);
      TCEAdapterGenerator.SetClassInterfaceTypeToNone(typeBuilder);
      foreach (MethodInfo propertyMethod in TCEAdapterGenerator.GetPropertyMethods(this.m_InputType))
        this.DefineBlankMethod(typeBuilder, propertyMethod);
      MethodInfo[] nonPropertyMethods = TCEAdapterGenerator.GetNonPropertyMethods(this.m_InputType);
      FieldBuilder[] afbDelegates = new FieldBuilder[nonPropertyMethods.Length];
      for (int index = 0; index < nonPropertyMethods.Length; ++index)
      {
        if (this.m_InputType == nonPropertyMethods[index].DeclaringType)
        {
          Type parameterType = this.m_EventItfType.GetMethod("add_" + nonPropertyMethods[index].Name).GetParameters()[0].ParameterType;
          afbDelegates[index] = typeBuilder.DefineField("m_" + nonPropertyMethods[index].Name + "Delegate", parameterType, FieldAttributes.Public);
          this.DefineEventMethod(typeBuilder, nonPropertyMethods[index], parameterType, afbDelegates[index]);
        }
      }
      FieldBuilder fbCookie = typeBuilder.DefineField("m_dwCookie", typeof (int), FieldAttributes.Public);
      this.DefineConstructor(typeBuilder, fbCookie, afbDelegates);
      return typeBuilder.CreateType();
    }

    private void DefineBlankMethod(TypeBuilder OutputTypeBuilder, MethodInfo Method)
    {
      ParameterInfo[] parameters = Method.GetParameters();
      Type[] parameterTypes = new Type[parameters.Length];
      for (int index = 0; index < parameters.Length; ++index)
        parameterTypes[index] = parameters[index].ParameterType;
      MethodBuilder Meth = OutputTypeBuilder.DefineMethod(Method.Name, Method.Attributes & ~MethodAttributes.Abstract, Method.CallingConvention, Method.ReturnType, parameterTypes);
      ILGenerator ilGenerator = Meth.GetILGenerator();
      this.AddReturn(Method.ReturnType, ilGenerator, Meth);
      ilGenerator.Emit(OpCodes.Ret);
    }

    private void DefineEventMethod(TypeBuilder OutputTypeBuilder, MethodInfo Method, Type DelegateCls, FieldBuilder fbDelegate)
    {
      MethodInfo method = DelegateCls.GetMethod("Invoke");
      Type returnType = Method.ReturnType;
      ParameterInfo[] parameters1 = Method.GetParameters();
      Type[] parameterTypes;
      if (parameters1 != null)
      {
        parameterTypes = new Type[parameters1.Length];
        for (int index = 0; index < parameters1.Length; ++index)
          parameterTypes[index] = parameters1[index].ParameterType;
      }
      else
        parameterTypes = (Type[]) null;
      MethodAttributes attributes = MethodAttributes.Public | MethodAttributes.Virtual;
      MethodBuilder Meth = OutputTypeBuilder.DefineMethod(Method.Name, attributes, CallingConventions.Standard, returnType, parameterTypes);
      ILGenerator ilGenerator = Meth.GetILGenerator();
      Label label = ilGenerator.DefineLabel();
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldfld, (FieldInfo) fbDelegate);
      ilGenerator.Emit(OpCodes.Brfalse, label);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldfld, (FieldInfo) fbDelegate);
      ParameterInfo[] parameters2 = Method.GetParameters();
      for (int index = 0; index < parameters2.Length; ++index)
        ilGenerator.Emit(OpCodes.Ldarg, (short) (index + 1));
      ilGenerator.Emit(OpCodes.Callvirt, method);
      ilGenerator.Emit(OpCodes.Ret);
      ilGenerator.MarkLabel(label);
      this.AddReturn(returnType, ilGenerator, Meth);
      ilGenerator.Emit(OpCodes.Ret);
    }

    private void AddReturn(Type ReturnType, ILGenerator il, MethodBuilder Meth)
    {
      if (ReturnType == typeof (void))
        return;
      if (ReturnType.IsPrimitive)
      {
        switch (Type.GetTypeCode(ReturnType))
        {
          case TypeCode.Boolean:
          case TypeCode.Char:
          case TypeCode.SByte:
          case TypeCode.Byte:
          case TypeCode.Int16:
          case TypeCode.UInt16:
          case TypeCode.Int32:
          case TypeCode.UInt32:
            il.Emit(OpCodes.Ldc_I4_0);
            break;
          case TypeCode.Int64:
          case TypeCode.UInt64:
            il.Emit(OpCodes.Ldc_I4_0);
            il.Emit(OpCodes.Conv_I8);
            break;
          case TypeCode.Single:
            il.Emit(OpCodes.Ldc_R4, 0);
            break;
          case TypeCode.Double:
            il.Emit(OpCodes.Ldc_R4, 0);
            il.Emit(OpCodes.Conv_R8);
            break;
          default:
            if (!(ReturnType == typeof (IntPtr)))
              break;
            il.Emit(OpCodes.Ldc_I4_0);
            break;
        }
      }
      else if (ReturnType.IsValueType)
      {
        Meth.InitLocals = true;
        LocalBuilder local = il.DeclareLocal(ReturnType);
        il.Emit(OpCodes.Ldloc_S, local);
      }
      else
        il.Emit(OpCodes.Ldnull);
    }

    private void DefineConstructor(TypeBuilder OutputTypeBuilder, FieldBuilder fbCookie, FieldBuilder[] afbDelegates)
    {
      ConstructorInfo constructor = typeof (object).GetConstructor(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, new Type[0], (ParameterModifier[]) null);
      ILGenerator ilGenerator = OutputTypeBuilder.DefineMethod(".ctor", MethodAttributes.Assembly | MethodAttributes.SpecialName, CallingConventions.Standard, (Type) null, (Type[]) null).GetILGenerator();
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Call, constructor);
      ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
      ilGenerator.Emit(OpCodes.Ldc_I4, 0);
      ilGenerator.Emit(OpCodes.Stfld, (FieldInfo) fbCookie);
      for (int index = 0; index < afbDelegates.Length; ++index)
      {
        if ((FieldInfo) afbDelegates[index] != (FieldInfo) null)
        {
          ilGenerator.Emit(OpCodes.Ldarg, (short) 0);
          ilGenerator.Emit(OpCodes.Ldnull);
          ilGenerator.Emit(OpCodes.Stfld, (FieldInfo) afbDelegates[index]);
        }
      }
      ilGenerator.Emit(OpCodes.Ret);
    }
  }
}

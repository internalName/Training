// Decompiled with JetBrains decompiler
// Type: System.DefaultBinder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System
{
  [Serializable]
  internal class DefaultBinder : Binder
  {
    [SecuritySafeCritical]
    public override MethodBase BindToMethod(BindingFlags bindingAttr, MethodBase[] match, ref object[] args, ParameterModifier[] modifiers, CultureInfo cultureInfo, string[] names, out object state)
    {
      if (match == null || match.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_EmptyArray"), nameof (match));
      MethodBase[] methodBaseArray = (MethodBase[]) match.Clone();
      state = (object) null;
      int[][] numArray = new int[methodBaseArray.Length][];
      for (int index1 = 0; index1 < methodBaseArray.Length; ++index1)
      {
        ParameterInfo[] parametersNoCopy = methodBaseArray[index1].GetParametersNoCopy();
        numArray[index1] = new int[parametersNoCopy.Length > args.Length ? parametersNoCopy.Length : args.Length];
        if (names == null)
        {
          for (int index2 = 0; index2 < args.Length; ++index2)
            numArray[index1][index2] = index2;
        }
        else if (!DefaultBinder.CreateParamOrder(numArray[index1], parametersNoCopy, names))
          methodBaseArray[index1] = (MethodBase) null;
      }
      Type[] typeArray = new Type[methodBaseArray.Length];
      Type[] types = new Type[args.Length];
      for (int index = 0; index < args.Length; ++index)
      {
        if (args[index] != null)
          types[index] = args[index].GetType();
      }
      int index3 = 0;
      bool flag1 = (uint) (bindingAttr & BindingFlags.OptionalParamBinding) > 0U;
      for (int index1 = 0; index1 < methodBaseArray.Length; ++index1)
      {
        Type type1 = (Type) null;
        if (!(methodBaseArray[index1] == (MethodBase) null))
        {
          ParameterInfo[] parametersNoCopy = methodBaseArray[index1].GetParametersNoCopy();
          if (parametersNoCopy.Length == 0)
          {
            if (args.Length == 0 || (methodBaseArray[index1].CallingConvention & CallingConventions.VarArgs) != (CallingConventions) 0)
            {
              numArray[index3] = numArray[index1];
              methodBaseArray[index3++] = methodBaseArray[index1];
            }
          }
          else
          {
            if (parametersNoCopy.Length > args.Length)
            {
              int length = args.Length;
              while (length < parametersNoCopy.Length - 1 && parametersNoCopy[length].DefaultValue != DBNull.Value)
                ++length;
              if (length == parametersNoCopy.Length - 1)
              {
                if (parametersNoCopy[length].DefaultValue == DBNull.Value)
                {
                  if (parametersNoCopy[length].ParameterType.IsArray && parametersNoCopy[length].IsDefined(typeof (ParamArrayAttribute), true))
                    type1 = parametersNoCopy[length].ParameterType.GetElementType();
                  else
                    continue;
                }
              }
              else
                continue;
            }
            else if (parametersNoCopy.Length < args.Length)
            {
              int index2 = parametersNoCopy.Length - 1;
              if (parametersNoCopy[index2].ParameterType.IsArray && parametersNoCopy[index2].IsDefined(typeof (ParamArrayAttribute), true) && numArray[index1][index2] == index2)
                type1 = parametersNoCopy[index2].ParameterType.GetElementType();
              else
                continue;
            }
            else
            {
              int index2 = parametersNoCopy.Length - 1;
              if (parametersNoCopy[index2].ParameterType.IsArray && parametersNoCopy[index2].IsDefined(typeof (ParamArrayAttribute), true) && (numArray[index1][index2] == index2 && !parametersNoCopy[index2].ParameterType.IsAssignableFrom(types[index2])))
                type1 = parametersNoCopy[index2].ParameterType.GetElementType();
            }
            int num = type1 != (Type) null ? parametersNoCopy.Length - 1 : args.Length;
            int index4;
            for (index4 = 0; index4 < num; ++index4)
            {
              Type type2 = parametersNoCopy[index4].ParameterType;
              if (type2.IsByRef)
                type2 = type2.GetElementType();
              if (!(type2 == types[numArray[index1][index4]]) && (!flag1 || args[numArray[index1][index4]] != Type.Missing) && (args[numArray[index1][index4]] != null && !(type2 == typeof (object))))
              {
                if (type2.IsPrimitive)
                {
                  if (types[numArray[index1][index4]] == (Type) null || !DefaultBinder.CanConvertPrimitiveObjectToType(args[numArray[index1][index4]], (RuntimeType) type2))
                    break;
                }
                else if (!(types[numArray[index1][index4]] == (Type) null) && !type2.IsAssignableFrom(types[numArray[index1][index4]]) && (!types[numArray[index1][index4]].IsCOMObject || !type2.IsInstanceOfType(args[numArray[index1][index4]])))
                  break;
              }
            }
            if (type1 != (Type) null && index4 == parametersNoCopy.Length - 1)
            {
              for (; index4 < args.Length; ++index4)
              {
                if (type1.IsPrimitive)
                {
                  if (types[index4] == (Type) null || !DefaultBinder.CanConvertPrimitiveObjectToType(args[index4], (RuntimeType) type1))
                    break;
                }
                else if (!(types[index4] == (Type) null) && !type1.IsAssignableFrom(types[index4]) && (!types[index4].IsCOMObject || !type1.IsInstanceOfType(args[index4])))
                  break;
              }
            }
            if (index4 == args.Length)
            {
              numArray[index3] = numArray[index1];
              typeArray[index3] = type1;
              methodBaseArray[index3++] = methodBaseArray[index1];
            }
          }
        }
      }
      if (index3 == 0)
        throw new MissingMethodException(Environment.GetResourceString("MissingMember"));
      if (index3 == 1)
      {
        if (names != null)
        {
          state = (object) new DefaultBinder.BinderState((int[]) numArray[0].Clone(), args.Length, typeArray[0] != (Type) null);
          DefaultBinder.ReorderParams(numArray[0], args);
        }
        ParameterInfo[] parametersNoCopy = methodBaseArray[0].GetParametersNoCopy();
        if (parametersNoCopy.Length == args.Length)
        {
          if (typeArray[0] != (Type) null)
          {
            object[] objArray = new object[parametersNoCopy.Length];
            int length = parametersNoCopy.Length - 1;
            Array.Copy((Array) args, 0, (Array) objArray, 0, length);
            objArray[length] = (object) Array.UnsafeCreateInstance(typeArray[0], 1);
            ((Array) objArray[length]).SetValue(args[length], 0);
            args = objArray;
          }
        }
        else if (parametersNoCopy.Length > args.Length)
        {
          object[] objArray = new object[parametersNoCopy.Length];
          int index1;
          for (index1 = 0; index1 < args.Length; ++index1)
            objArray[index1] = args[index1];
          for (; index1 < parametersNoCopy.Length - 1; ++index1)
            objArray[index1] = parametersNoCopy[index1].DefaultValue;
          objArray[index1] = !(typeArray[0] != (Type) null) ? parametersNoCopy[index1].DefaultValue : (object) Array.UnsafeCreateInstance(typeArray[0], 0);
          args = objArray;
        }
        else if ((methodBaseArray[0].CallingConvention & CallingConventions.VarArgs) == (CallingConventions) 0)
        {
          object[] objArray = new object[parametersNoCopy.Length];
          int index1 = parametersNoCopy.Length - 1;
          Array.Copy((Array) args, 0, (Array) objArray, 0, index1);
          objArray[index1] = (object) Array.UnsafeCreateInstance(typeArray[0], args.Length - index1);
          Array.Copy((Array) args, index1, (Array) objArray[index1], 0, args.Length - index1);
          args = objArray;
        }
        return methodBaseArray[0];
      }
      int index5 = 0;
      bool flag2 = false;
      for (int index1 = 1; index1 < index3; ++index1)
      {
        switch (DefaultBinder.FindMostSpecificMethod(methodBaseArray[index5], numArray[index5], typeArray[index5], methodBaseArray[index1], numArray[index1], typeArray[index1], types, args))
        {
          case 0:
            flag2 = true;
            break;
          case 2:
            index5 = index1;
            flag2 = false;
            break;
        }
      }
      if (flag2)
        throw new AmbiguousMatchException(Environment.GetResourceString("Arg_AmbiguousMatchException"));
      if (names != null)
      {
        state = (object) new DefaultBinder.BinderState((int[]) numArray[index5].Clone(), args.Length, typeArray[index5] != (Type) null);
        DefaultBinder.ReorderParams(numArray[index5], args);
      }
      ParameterInfo[] parametersNoCopy1 = methodBaseArray[index5].GetParametersNoCopy();
      if (parametersNoCopy1.Length == args.Length)
      {
        if (typeArray[index5] != (Type) null)
        {
          object[] objArray = new object[parametersNoCopy1.Length];
          int length = parametersNoCopy1.Length - 1;
          Array.Copy((Array) args, 0, (Array) objArray, 0, length);
          objArray[length] = (object) Array.UnsafeCreateInstance(typeArray[index5], 1);
          ((Array) objArray[length]).SetValue(args[length], 0);
          args = objArray;
        }
      }
      else if (parametersNoCopy1.Length > args.Length)
      {
        object[] objArray = new object[parametersNoCopy1.Length];
        int index1;
        for (index1 = 0; index1 < args.Length; ++index1)
          objArray[index1] = args[index1];
        for (; index1 < parametersNoCopy1.Length - 1; ++index1)
          objArray[index1] = parametersNoCopy1[index1].DefaultValue;
        objArray[index1] = !(typeArray[index5] != (Type) null) ? parametersNoCopy1[index1].DefaultValue : (object) Array.UnsafeCreateInstance(typeArray[index5], 0);
        args = objArray;
      }
      else if ((methodBaseArray[index5].CallingConvention & CallingConventions.VarArgs) == (CallingConventions) 0)
      {
        object[] objArray = new object[parametersNoCopy1.Length];
        int index1 = parametersNoCopy1.Length - 1;
        Array.Copy((Array) args, 0, (Array) objArray, 0, index1);
        objArray[index1] = (object) Array.UnsafeCreateInstance(typeArray[index5], args.Length - index1);
        Array.Copy((Array) args, index1, (Array) objArray[index1], 0, args.Length - index1);
        args = objArray;
      }
      return methodBaseArray[index5];
    }

    [SecuritySafeCritical]
    public override FieldInfo BindToField(BindingFlags bindingAttr, FieldInfo[] match, object value, CultureInfo cultureInfo)
    {
      if (match == null)
        throw new ArgumentNullException(nameof (match));
      int num = 0;
      FieldInfo[] fieldInfoArray = (FieldInfo[]) match.Clone();
      if ((bindingAttr & BindingFlags.SetField) != BindingFlags.Default)
      {
        Type type = value.GetType();
        for (int index = 0; index < fieldInfoArray.Length; ++index)
        {
          Type fieldType = fieldInfoArray[index].FieldType;
          if (fieldType == type)
            fieldInfoArray[num++] = fieldInfoArray[index];
          else if (value == Empty.Value && fieldType.IsClass)
            fieldInfoArray[num++] = fieldInfoArray[index];
          else if (fieldType == typeof (object))
            fieldInfoArray[num++] = fieldInfoArray[index];
          else if (fieldType.IsPrimitive)
          {
            if (DefaultBinder.CanConvertPrimitiveObjectToType(value, (RuntimeType) fieldType))
              fieldInfoArray[num++] = fieldInfoArray[index];
          }
          else if (fieldType.IsAssignableFrom(type))
            fieldInfoArray[num++] = fieldInfoArray[index];
        }
        if (num == 0)
          throw new MissingFieldException(Environment.GetResourceString("MissingField"));
        if (num == 1)
          return fieldInfoArray[0];
      }
      int index1 = 0;
      bool flag = false;
      for (int index2 = 1; index2 < num; ++index2)
      {
        switch (DefaultBinder.FindMostSpecificField(fieldInfoArray[index1], fieldInfoArray[index2]))
        {
          case 0:
            flag = true;
            break;
          case 2:
            index1 = index2;
            flag = false;
            break;
        }
      }
      if (flag)
        throw new AmbiguousMatchException(Environment.GetResourceString("Arg_AmbiguousMatchException"));
      return fieldInfoArray[index1];
    }

    [SecuritySafeCritical]
    public override MethodBase SelectMethod(BindingFlags bindingAttr, MethodBase[] match, Type[] types, ParameterModifier[] modifiers)
    {
      Type[] typeArray = new Type[types.Length];
      for (int index = 0; index < types.Length; ++index)
      {
        typeArray[index] = types[index].UnderlyingSystemType;
        if ((object) (typeArray[index] as RuntimeType) == null)
          throw new ArgumentException(Environment.GetResourceString("Arg_MustBeType"), nameof (types));
      }
      types = typeArray;
      if (match == null || match.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_EmptyArray"), nameof (match));
      MethodBase[] methodBaseArray = (MethodBase[]) match.Clone();
      int num = 0;
      for (int index1 = 0; index1 < methodBaseArray.Length; ++index1)
      {
        ParameterInfo[] parametersNoCopy = methodBaseArray[index1].GetParametersNoCopy();
        if (parametersNoCopy.Length == types.Length)
        {
          int index2;
          for (index2 = 0; index2 < types.Length; ++index2)
          {
            Type parameterType = parametersNoCopy[index2].ParameterType;
            if (!(parameterType == types[index2]) && !(parameterType == typeof (object)))
            {
              if (parameterType.IsPrimitive)
              {
                if ((object) (types[index2].UnderlyingSystemType as RuntimeType) == null || !DefaultBinder.CanConvertPrimitive((RuntimeType) types[index2].UnderlyingSystemType, (RuntimeType) parameterType.UnderlyingSystemType))
                  break;
              }
              else if (!parameterType.IsAssignableFrom(types[index2]))
                break;
            }
          }
          if (index2 == types.Length)
            methodBaseArray[num++] = methodBaseArray[index1];
        }
      }
      if (num == 0)
        return (MethodBase) null;
      if (num == 1)
        return methodBaseArray[0];
      int index3 = 0;
      bool flag = false;
      int[] numArray = new int[types.Length];
      for (int index1 = 0; index1 < types.Length; ++index1)
        numArray[index1] = index1;
      for (int index1 = 1; index1 < num; ++index1)
      {
        switch (DefaultBinder.FindMostSpecificMethod(methodBaseArray[index3], numArray, (Type) null, methodBaseArray[index1], numArray, (Type) null, types, (object[]) null))
        {
          case 0:
            flag = true;
            break;
          case 2:
            flag = false;
            index3 = index1;
            break;
        }
      }
      if (flag)
        throw new AmbiguousMatchException(Environment.GetResourceString("Arg_AmbiguousMatchException"));
      return methodBaseArray[index3];
    }

    [SecuritySafeCritical]
    public override PropertyInfo SelectProperty(BindingFlags bindingAttr, PropertyInfo[] match, Type returnType, Type[] indexes, ParameterModifier[] modifiers)
    {
      if (indexes != null)
      {
        Type[] typeArray = indexes;
        Predicate<Type> predicate1 = (Predicate<Type>) (t => t != (Type) null);
        Predicate<Type> predicate2;
        if (!Contract.ForAll<Type>((IEnumerable<Type>) typeArray, predicate2))
          throw (Exception) new ArgumentNullException(nameof (indexes));
      }
      if (match == null || match.Length == 0)
        throw new ArgumentException(Environment.GetResourceString("Arg_EmptyArray"), nameof (match));
      PropertyInfo[] propertyInfoArray = (PropertyInfo[]) match.Clone();
      int index1 = 0;
      int num1 = 0;
      int length = indexes != null ? indexes.Length : 0;
      for (int index2 = 0; index2 < propertyInfoArray.Length; ++index2)
      {
        if (indexes != null)
        {
          ParameterInfo[] indexParameters = propertyInfoArray[index2].GetIndexParameters();
          if (indexParameters.Length == length)
          {
            for (index1 = 0; index1 < length; ++index1)
            {
              Type parameterType = indexParameters[index1].ParameterType;
              if (!(parameterType == indexes[index1]) && !(parameterType == typeof (object)))
              {
                if (parameterType.IsPrimitive)
                {
                  if ((object) (indexes[index1].UnderlyingSystemType as RuntimeType) == null || !DefaultBinder.CanConvertPrimitive((RuntimeType) indexes[index1].UnderlyingSystemType, (RuntimeType) parameterType.UnderlyingSystemType))
                    break;
                }
                else if (!parameterType.IsAssignableFrom(indexes[index1]))
                  break;
              }
            }
          }
          else
            continue;
        }
        if (index1 == length)
        {
          if (returnType != (Type) null)
          {
            if (propertyInfoArray[index2].PropertyType.IsPrimitive)
            {
              if ((object) (returnType.UnderlyingSystemType as RuntimeType) == null || !DefaultBinder.CanConvertPrimitive((RuntimeType) returnType.UnderlyingSystemType, (RuntimeType) propertyInfoArray[index2].PropertyType.UnderlyingSystemType))
                continue;
            }
            else if (!propertyInfoArray[index2].PropertyType.IsAssignableFrom(returnType))
              continue;
          }
          propertyInfoArray[num1++] = propertyInfoArray[index2];
        }
      }
      if (num1 == 0)
        return (PropertyInfo) null;
      if (num1 == 1)
        return propertyInfoArray[0];
      int index3 = 0;
      bool flag = false;
      int[] numArray = new int[length];
      for (int index2 = 0; index2 < length; ++index2)
        numArray[index2] = index2;
      for (int index2 = 1; index2 < num1; ++index2)
      {
        int num2 = DefaultBinder.FindMostSpecificType(propertyInfoArray[index3].PropertyType, propertyInfoArray[index2].PropertyType, returnType);
        if (num2 == 0 && indexes != null)
          num2 = DefaultBinder.FindMostSpecific(propertyInfoArray[index3].GetIndexParameters(), numArray, (Type) null, propertyInfoArray[index2].GetIndexParameters(), numArray, (Type) null, indexes, (object[]) null);
        if (num2 == 0)
        {
          num2 = DefaultBinder.FindMostSpecificProperty(propertyInfoArray[index3], propertyInfoArray[index2]);
          if (num2 == 0)
            flag = true;
        }
        if (num2 == 2)
        {
          flag = false;
          index3 = index2;
        }
      }
      if (flag)
        throw new AmbiguousMatchException(Environment.GetResourceString("Arg_AmbiguousMatchException"));
      return propertyInfoArray[index3];
    }

    public override object ChangeType(object value, Type type, CultureInfo cultureInfo)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_ChangeType"));
    }

    public override void ReorderArgumentArray(ref object[] args, object state)
    {
      DefaultBinder.BinderState binderState = (DefaultBinder.BinderState) state;
      DefaultBinder.ReorderParams(binderState.m_argsMap, args);
      if (binderState.m_isParamArray)
      {
        int length = args.Length - 1;
        if (args.Length == binderState.m_originalSize)
        {
          args[length] = ((object[]) args[length])[0];
        }
        else
        {
          object[] objArray = new object[args.Length];
          Array.Copy((Array) args, 0, (Array) objArray, 0, length);
          int index1 = length;
          int index2 = 0;
          while (index1 < objArray.Length)
          {
            objArray[index1] = ((object[]) args[length])[index2];
            ++index1;
            ++index2;
          }
          args = objArray;
        }
      }
      else
      {
        if (args.Length <= binderState.m_originalSize)
          return;
        object[] objArray = new object[binderState.m_originalSize];
        Array.Copy((Array) args, 0, (Array) objArray, 0, binderState.m_originalSize);
        args = objArray;
      }
    }

    public static MethodBase ExactBinding(MethodBase[] match, Type[] types, ParameterModifier[] modifiers)
    {
      if (match == null)
        throw new ArgumentNullException(nameof (match));
      MethodBase[] match1 = new MethodBase[match.Length];
      int cMatches = 0;
      for (int index1 = 0; index1 < match.Length; ++index1)
      {
        ParameterInfo[] parametersNoCopy = match[index1].GetParametersNoCopy();
        if (parametersNoCopy.Length != 0)
        {
          int index2 = 0;
          while (index2 < types.Length && parametersNoCopy[index2].ParameterType.Equals(types[index2]))
            ++index2;
          if (index2 >= types.Length)
          {
            match1[cMatches] = match[index1];
            ++cMatches;
          }
        }
      }
      if (cMatches == 0)
        return (MethodBase) null;
      if (cMatches == 1)
        return match1[0];
      return DefaultBinder.FindMostDerivedNewSlotMeth(match1, cMatches);
    }

    public static PropertyInfo ExactPropertyBinding(PropertyInfo[] match, Type returnType, Type[] types, ParameterModifier[] modifiers)
    {
      if (match == null)
        throw new ArgumentNullException(nameof (match));
      PropertyInfo propertyInfo = (PropertyInfo) null;
      int num = types != null ? types.Length : 0;
      for (int index1 = 0; index1 < match.Length; ++index1)
      {
        ParameterInfo[] indexParameters = match[index1].GetIndexParameters();
        int index2 = 0;
        while (index2 < num && !(indexParameters[index2].ParameterType != types[index2]))
          ++index2;
        if (index2 >= num && (!(returnType != (Type) null) || !(returnType != match[index1].PropertyType)))
        {
          if (propertyInfo != (PropertyInfo) null)
            throw new AmbiguousMatchException(Environment.GetResourceString("Arg_AmbiguousMatchException"));
          propertyInfo = match[index1];
        }
      }
      return propertyInfo;
    }

    private static int FindMostSpecific(ParameterInfo[] p1, int[] paramOrder1, Type paramArrayType1, ParameterInfo[] p2, int[] paramOrder2, Type paramArrayType2, Type[] types, object[] args)
    {
      if (paramArrayType1 != (Type) null && paramArrayType2 == (Type) null)
        return 2;
      if (paramArrayType2 != (Type) null && paramArrayType1 == (Type) null)
        return 1;
      bool flag1 = false;
      bool flag2 = false;
      for (int index = 0; index < types.Length; ++index)
      {
        if (args == null || args[index] != Type.Missing)
        {
          Type c1 = !(paramArrayType1 != (Type) null) || paramOrder1[index] < p1.Length - 1 ? p1[paramOrder1[index]].ParameterType : paramArrayType1;
          Type c2 = !(paramArrayType2 != (Type) null) || paramOrder2[index] < p2.Length - 1 ? p2[paramOrder2[index]].ParameterType : paramArrayType2;
          if (!(c1 == c2))
          {
            switch (DefaultBinder.FindMostSpecificType(c1, c2, types[index]))
            {
              case 0:
                return 0;
              case 1:
                flag1 = true;
                continue;
              case 2:
                flag2 = true;
                continue;
              default:
                continue;
            }
          }
        }
      }
      if (flag1 == flag2)
      {
        if (!flag1 && args != null)
        {
          if (p1.Length > p2.Length)
            return 1;
          if (p2.Length > p1.Length)
            return 2;
        }
        return 0;
      }
      return !flag1 ? 2 : 1;
    }

    [SecuritySafeCritical]
    private static int FindMostSpecificType(Type c1, Type c2, Type t)
    {
      if (c1 == c2)
        return 0;
      if (c1 == t)
        return 1;
      if (c2 == t)
        return 2;
      if (c1.IsByRef || c2.IsByRef)
      {
        if (c1.IsByRef && c2.IsByRef)
        {
          c1 = c1.GetElementType();
          c2 = c2.GetElementType();
        }
        else if (c1.IsByRef)
        {
          if (c1.GetElementType() == c2)
            return 2;
          c1 = c1.GetElementType();
        }
        else
        {
          if (c2.GetElementType() == c1)
            return 1;
          c2 = c2.GetElementType();
        }
      }
      bool flag1;
      bool flag2;
      if (c1.IsPrimitive && c2.IsPrimitive)
      {
        flag1 = DefaultBinder.CanConvertPrimitive((RuntimeType) c2, (RuntimeType) c1);
        flag2 = DefaultBinder.CanConvertPrimitive((RuntimeType) c1, (RuntimeType) c2);
      }
      else
      {
        flag1 = c1.IsAssignableFrom(c2);
        flag2 = c2.IsAssignableFrom(c1);
      }
      if (flag1 == flag2)
        return 0;
      return flag1 ? 2 : 1;
    }

    private static int FindMostSpecificMethod(MethodBase m1, int[] paramOrder1, Type paramArrayType1, MethodBase m2, int[] paramOrder2, Type paramArrayType2, Type[] types, object[] args)
    {
      int mostSpecific = DefaultBinder.FindMostSpecific(m1.GetParametersNoCopy(), paramOrder1, paramArrayType1, m2.GetParametersNoCopy(), paramOrder2, paramArrayType2, types, args);
      if (mostSpecific != 0)
        return mostSpecific;
      if (!DefaultBinder.CompareMethodSigAndName(m1, m2))
        return 0;
      int hierarchyDepth1 = DefaultBinder.GetHierarchyDepth(m1.DeclaringType);
      int hierarchyDepth2 = DefaultBinder.GetHierarchyDepth(m2.DeclaringType);
      if (hierarchyDepth1 == hierarchyDepth2)
        return 0;
      return hierarchyDepth1 < hierarchyDepth2 ? 2 : 1;
    }

    private static int FindMostSpecificField(FieldInfo cur1, FieldInfo cur2)
    {
      if (!(cur1.Name == cur2.Name))
        return 0;
      int hierarchyDepth1 = DefaultBinder.GetHierarchyDepth(cur1.DeclaringType);
      int hierarchyDepth2 = DefaultBinder.GetHierarchyDepth(cur2.DeclaringType);
      if (hierarchyDepth1 == hierarchyDepth2)
        return 0;
      return hierarchyDepth1 < hierarchyDepth2 ? 2 : 1;
    }

    private static int FindMostSpecificProperty(PropertyInfo cur1, PropertyInfo cur2)
    {
      if (!(cur1.Name == cur2.Name))
        return 0;
      int hierarchyDepth1 = DefaultBinder.GetHierarchyDepth(cur1.DeclaringType);
      int hierarchyDepth2 = DefaultBinder.GetHierarchyDepth(cur2.DeclaringType);
      if (hierarchyDepth1 == hierarchyDepth2)
        return 0;
      return hierarchyDepth1 < hierarchyDepth2 ? 2 : 1;
    }

    internal static bool CompareMethodSigAndName(MethodBase m1, MethodBase m2)
    {
      ParameterInfo[] parametersNoCopy1 = m1.GetParametersNoCopy();
      ParameterInfo[] parametersNoCopy2 = m2.GetParametersNoCopy();
      if (parametersNoCopy1.Length != parametersNoCopy2.Length)
        return false;
      int length = parametersNoCopy1.Length;
      for (int index = 0; index < length; ++index)
      {
        if (parametersNoCopy1[index].ParameterType != parametersNoCopy2[index].ParameterType)
          return false;
      }
      return true;
    }

    internal static int GetHierarchyDepth(Type t)
    {
      int num = 0;
      Type type = t;
      do
      {
        ++num;
        type = type.BaseType;
      }
      while (type != (Type) null);
      return num;
    }

    internal static MethodBase FindMostDerivedNewSlotMeth(MethodBase[] match, int cMatches)
    {
      int num = 0;
      MethodBase methodBase = (MethodBase) null;
      for (int index = 0; index < cMatches; ++index)
      {
        int hierarchyDepth = DefaultBinder.GetHierarchyDepth(match[index].DeclaringType);
        if (hierarchyDepth == num)
          throw new AmbiguousMatchException(Environment.GetResourceString("Arg_AmbiguousMatchException"));
        if (hierarchyDepth > num)
        {
          num = hierarchyDepth;
          methodBase = match[index];
        }
      }
      return methodBase;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool CanConvertPrimitive(RuntimeType source, RuntimeType target);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern bool CanConvertPrimitiveObjectToType(object source, RuntimeType type);

    private static void ReorderParams(int[] paramOrder, object[] vars)
    {
      object[] objArray = new object[vars.Length];
      for (int index = 0; index < vars.Length; ++index)
        objArray[index] = vars[index];
      for (int index = 0; index < vars.Length; ++index)
        vars[index] = objArray[paramOrder[index]];
    }

    private static bool CreateParamOrder(int[] paramOrder, ParameterInfo[] pars, string[] names)
    {
      bool[] flagArray = new bool[pars.Length];
      for (int index = 0; index < pars.Length; ++index)
        paramOrder[index] = -1;
      for (int index1 = 0; index1 < names.Length; ++index1)
      {
        int index2;
        for (index2 = 0; index2 < pars.Length; ++index2)
        {
          if (names[index1].Equals(pars[index2].Name))
          {
            paramOrder[index2] = index1;
            flagArray[index1] = true;
            break;
          }
        }
        if (index2 == pars.Length)
          return false;
      }
      int index3 = 0;
      for (int index1 = 0; index1 < pars.Length; ++index1)
      {
        if (paramOrder[index1] == -1)
        {
          for (; index3 < pars.Length; ++index3)
          {
            if (!flagArray[index3])
            {
              paramOrder[index1] = index3;
              ++index3;
              break;
            }
          }
        }
      }
      return true;
    }

    internal class BinderState
    {
      internal int[] m_argsMap;
      internal int m_originalSize;
      internal bool m_isParamArray;

      internal BinderState(int[] argsMap, int originalSize, bool isParamArray)
      {
        this.m_argsMap = argsMap;
        this.m_originalSize = originalSize;
        this.m_isParamArray = isParamArray;
      }
    }
  }
}

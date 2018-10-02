// Decompiled with JetBrains decompiler
// Type: System.Reflection.MdConstant
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Reflection
{
  internal static class MdConstant
  {
    [SecurityCritical]
    public static unsafe object GetValue(MetadataImport scope, int token, RuntimeTypeHandle fieldTypeHandle, bool raw)
    {
      CorElementType corElementType = CorElementType.End;
      long num1 = 0;
      int length;
      string defaultValue = scope.GetDefaultValue(token, out num1, out length, out corElementType);
      RuntimeType runtimeType = fieldTypeHandle.GetRuntimeType();
      if (runtimeType.IsEnum && !raw)
      {
        long num2;
        switch (corElementType)
        {
          case CorElementType.Void:
            return (object) DBNull.Value;
          case CorElementType.Char:
            num2 = (long) *(ushort*) &num1;
            break;
          case CorElementType.I1:
            num2 = (long) *(sbyte*) &num1;
            break;
          case CorElementType.U1:
            num2 = (long) *(byte*) &num1;
            break;
          case CorElementType.I2:
            num2 = (long) *(short*) &num1;
            break;
          case CorElementType.U2:
            num2 = (long) *(ushort*) &num1;
            break;
          case CorElementType.I4:
            num2 = (long) *(int*) &num1;
            break;
          case CorElementType.U4:
            num2 = (long) *(uint*) &num1;
            break;
          case CorElementType.I8:
            num2 = num1;
            break;
          case CorElementType.U8:
            num2 = num1;
            break;
          default:
            throw new FormatException(Environment.GetResourceString("Arg_BadLiteralFormat"));
        }
        return RuntimeType.CreateEnum(runtimeType, num2);
      }
      if ((Type) runtimeType == typeof (DateTime))
      {
        long ticks;
        switch (corElementType)
        {
          case CorElementType.Void:
            return (object) DBNull.Value;
          case CorElementType.I8:
            ticks = num1;
            break;
          case CorElementType.U8:
            ticks = num1;
            break;
          default:
            throw new FormatException(Environment.GetResourceString("Arg_BadLiteralFormat"));
        }
        return (object) new DateTime(ticks);
      }
      switch (corElementType)
      {
        case CorElementType.Void:
          return (object) DBNull.Value;
        case CorElementType.Boolean:
          return (object) ((uint) *(int*) &num1 > 0U);
        case CorElementType.Char:
          return (object) (char) *(ushort*) &num1;
        case CorElementType.I1:
          return (object) *(sbyte*) &num1;
        case CorElementType.U1:
          return (object) *(byte*) &num1;
        case CorElementType.I2:
          return (object) *(short*) &num1;
        case CorElementType.U2:
          return (object) *(ushort*) &num1;
        case CorElementType.I4:
          return (object) *(int*) &num1;
        case CorElementType.U4:
          return (object) *(uint*) &num1;
        case CorElementType.I8:
          return (object) num1;
        case CorElementType.U8:
          return (object) (ulong) num1;
        case CorElementType.R4:
          return (object) *(float*) &num1;
        case CorElementType.R8:
          return (object) *(double*) &num1;
        case CorElementType.String:
          return (object) defaultValue ?? (object) string.Empty;
        case CorElementType.Class:
          return (object) null;
        default:
          throw new FormatException(Environment.GetResourceString("Arg_BadLiteralFormat"));
      }
    }
  }
}

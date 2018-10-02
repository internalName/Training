// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.IOUtil
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal static class IOUtil
  {
    internal static bool FlagTest(MessageEnum flag, MessageEnum target)
    {
      return (flag & target) == target;
    }

    internal static void WriteStringWithCode(string value, __BinaryWriter sout)
    {
      if (value == null)
      {
        sout.WriteByte((byte) 17);
      }
      else
      {
        sout.WriteByte((byte) 18);
        sout.WriteString(value);
      }
    }

    internal static void WriteWithCode(Type type, object value, __BinaryWriter sout)
    {
      if ((object) type == null)
        sout.WriteByte((byte) 17);
      else if ((object) type == (object) Converter.typeofString)
      {
        IOUtil.WriteStringWithCode((string) value, sout);
      }
      else
      {
        InternalPrimitiveTypeE code = Converter.ToCode(type);
        sout.WriteByte((byte) code);
        sout.WriteValue(code, value);
      }
    }

    internal static object ReadWithCode(__BinaryParser input)
    {
      InternalPrimitiveTypeE code = (InternalPrimitiveTypeE) input.ReadByte();
      switch (code)
      {
        case InternalPrimitiveTypeE.Null:
          return (object) null;
        case InternalPrimitiveTypeE.String:
          return (object) input.ReadString();
        default:
          return input.ReadValue(code);
      }
    }

    internal static object[] ReadArgs(__BinaryParser input)
    {
      int length = input.ReadInt32();
      object[] objArray = new object[length];
      for (int index = 0; index < length; ++index)
        objArray[index] = IOUtil.ReadWithCode(input);
      return objArray;
    }
  }
}

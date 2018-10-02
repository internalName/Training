// Decompiled with JetBrains decompiler
// Type: Microsoft.Reflection.ReflectionExtensions
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Reflection;

namespace Microsoft.Reflection
{
  internal static class ReflectionExtensions
  {
    public static bool IsEnum(this Type type)
    {
      return type.IsEnum;
    }

    public static bool IsAbstract(this Type type)
    {
      return type.IsAbstract;
    }

    public static bool IsSealed(this Type type)
    {
      return type.IsSealed;
    }

    public static Type BaseType(this Type type)
    {
      return type.BaseType;
    }

    public static Assembly Assembly(this Type type)
    {
      return type.Assembly;
    }

    public static TypeCode GetTypeCode(this Type type)
    {
      return Type.GetTypeCode(type);
    }

    public static bool ReflectionOnly(this Assembly assm)
    {
      return assm.ReflectionOnly;
    }
  }
}

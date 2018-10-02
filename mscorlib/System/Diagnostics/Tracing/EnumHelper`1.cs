// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EnumHelper`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Diagnostics.Tracing
{
  internal static class EnumHelper<UnderlyingType>
  {
    private static readonly MethodInfo IdentityInfo = Statics.GetDeclaredStaticMethod(typeof (EnumHelper<UnderlyingType>), "Identity");

    public static UnderlyingType Cast<ValueType>(ValueType value)
    {
      return EnumHelper<UnderlyingType>.Caster<ValueType>.Instance(value);
    }

    internal static UnderlyingType Identity(UnderlyingType value)
    {
      return value;
    }

    private delegate UnderlyingType Transformer<ValueType>(ValueType value);

    private static class Caster<ValueType>
    {
      public static readonly EnumHelper<UnderlyingType>.Transformer<ValueType> Instance = (EnumHelper<UnderlyingType>.Transformer<ValueType>) Statics.CreateDelegate(typeof (EnumHelper<UnderlyingType>.Transformer<ValueType>), EnumHelper<UnderlyingType>.IdentityInfo);
    }
  }
}

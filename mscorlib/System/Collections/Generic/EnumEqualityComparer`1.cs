// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.EnumEqualityComparer`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Collections.Generic
{
  [Serializable]
  internal class EnumEqualityComparer<T> : EqualityComparer<T>, ISerializable where T : struct
  {
    public override bool Equals(T x, T y)
    {
      return JitHelpers.UnsafeEnumCast<T>(x) == JitHelpers.UnsafeEnumCast<T>(y);
    }

    public override int GetHashCode(T obj)
    {
      return JitHelpers.UnsafeEnumCast<T>(obj).GetHashCode();
    }

    public EnumEqualityComparer()
    {
    }

    protected EnumEqualityComparer(SerializationInfo information, StreamingContext context)
    {
    }

    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (Type.GetTypeCode(Enum.GetUnderlyingType(typeof (T))) == TypeCode.Int32)
        return;
      info.SetType(typeof (ObjectEqualityComparer<T>));
    }

    public override bool Equals(object obj)
    {
      return obj is EnumEqualityComparer<T>;
    }

    public override int GetHashCode()
    {
      return this.GetType().Name.GetHashCode();
    }
  }
}

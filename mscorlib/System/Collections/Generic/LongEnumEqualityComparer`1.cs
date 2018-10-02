// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.LongEnumEqualityComparer`1
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Collections.Generic
{
  [Serializable]
  internal sealed class LongEnumEqualityComparer<T> : EqualityComparer<T>, ISerializable where T : struct
  {
    public override bool Equals(T x, T y)
    {
      return JitHelpers.UnsafeEnumCastLong<T>(x) == JitHelpers.UnsafeEnumCastLong<T>(y);
    }

    public override int GetHashCode(T obj)
    {
      return JitHelpers.UnsafeEnumCastLong<T>(obj).GetHashCode();
    }

    public override bool Equals(object obj)
    {
      return obj is LongEnumEqualityComparer<T>;
    }

    public override int GetHashCode()
    {
      return this.GetType().Name.GetHashCode();
    }

    public LongEnumEqualityComparer()
    {
    }

    public LongEnumEqualityComparer(SerializationInfo information, StreamingContext context)
    {
    }

    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.SetType(typeof (ObjectEqualityComparer<T>));
    }
  }
}

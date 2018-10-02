// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.MemberReference
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class MemberReference : IStreamable
  {
    internal int idRef;

    internal MemberReference()
    {
    }

    internal void Set(int idRef)
    {
      this.idRef = idRef;
    }

    public void Write(__BinaryWriter sout)
    {
      sout.WriteByte((byte) 9);
      sout.WriteInt32(this.idRef);
    }

    [SecurityCritical]
    public void Read(__BinaryParser input)
    {
      this.idRef = input.ReadInt32();
    }

    public void Dump()
    {
    }

    [Conditional("_LOGGING")]
    private void DumpInternal()
    {
      BCLDebug.CheckEnabled("BINARY");
    }
  }
}

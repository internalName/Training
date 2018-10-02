// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.BinaryCrossAppDomainMap
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class BinaryCrossAppDomainMap : IStreamable
  {
    internal int crossAppDomainArrayIndex;

    internal BinaryCrossAppDomainMap()
    {
    }

    public void Write(__BinaryWriter sout)
    {
      sout.WriteByte((byte) 18);
      sout.WriteInt32(this.crossAppDomainArrayIndex);
    }

    [SecurityCritical]
    public void Read(__BinaryParser input)
    {
      this.crossAppDomainArrayIndex = input.ReadInt32();
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

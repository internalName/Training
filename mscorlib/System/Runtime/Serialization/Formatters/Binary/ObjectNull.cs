// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.ObjectNull
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class ObjectNull : IStreamable
  {
    internal int nullCount;

    internal ObjectNull()
    {
    }

    internal void SetNullCount(int nullCount)
    {
      this.nullCount = nullCount;
    }

    public void Write(__BinaryWriter sout)
    {
      if (this.nullCount == 1)
        sout.WriteByte((byte) 10);
      else if (this.nullCount < 256)
      {
        sout.WriteByte((byte) 13);
        sout.WriteByte((byte) this.nullCount);
      }
      else
      {
        sout.WriteByte((byte) 14);
        sout.WriteInt32(this.nullCount);
      }
    }

    [SecurityCritical]
    public void Read(__BinaryParser input)
    {
      this.Read(input, BinaryHeaderEnum.ObjectNull);
    }

    public void Read(__BinaryParser input, BinaryHeaderEnum binaryHeaderEnum)
    {
      switch (binaryHeaderEnum)
      {
        case BinaryHeaderEnum.ObjectNull:
          this.nullCount = 1;
          break;
        case BinaryHeaderEnum.ObjectNullMultiple256:
          this.nullCount = (int) input.ReadByte();
          break;
        case BinaryHeaderEnum.ObjectNullMultiple:
          this.nullCount = input.ReadInt32();
          break;
      }
    }

    public void Dump()
    {
    }

    [Conditional("_LOGGING")]
    private void DumpInternal()
    {
      if (!BCLDebug.CheckEnabled("BINARY") || this.nullCount == 1)
        return;
      int nullCount = this.nullCount;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.BinaryObject
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class BinaryObject : IStreamable
  {
    internal int objectId;
    internal int mapId;

    internal BinaryObject()
    {
    }

    internal void Set(int objectId, int mapId)
    {
      this.objectId = objectId;
      this.mapId = mapId;
    }

    public void Write(__BinaryWriter sout)
    {
      sout.WriteByte((byte) 1);
      sout.WriteInt32(this.objectId);
      sout.WriteInt32(this.mapId);
    }

    [SecurityCritical]
    public void Read(__BinaryParser input)
    {
      this.objectId = input.ReadInt32();
      this.mapId = input.ReadInt32();
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

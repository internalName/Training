// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.BinaryObjectWithMap
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class BinaryObjectWithMap : IStreamable
  {
    internal BinaryHeaderEnum binaryHeaderEnum;
    internal int objectId;
    internal string name;
    internal int numMembers;
    internal string[] memberNames;
    internal int assemId;

    internal BinaryObjectWithMap()
    {
    }

    internal BinaryObjectWithMap(BinaryHeaderEnum binaryHeaderEnum)
    {
      this.binaryHeaderEnum = binaryHeaderEnum;
    }

    internal void Set(int objectId, string name, int numMembers, string[] memberNames, int assemId)
    {
      this.objectId = objectId;
      this.name = name;
      this.numMembers = numMembers;
      this.memberNames = memberNames;
      this.assemId = assemId;
      if (assemId > 0)
        this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMapAssemId;
      else
        this.binaryHeaderEnum = BinaryHeaderEnum.ObjectWithMap;
    }

    public void Write(__BinaryWriter sout)
    {
      sout.WriteByte((byte) this.binaryHeaderEnum);
      sout.WriteInt32(this.objectId);
      sout.WriteString(this.name);
      sout.WriteInt32(this.numMembers);
      for (int index = 0; index < this.numMembers; ++index)
        sout.WriteString(this.memberNames[index]);
      if (this.assemId <= 0)
        return;
      sout.WriteInt32(this.assemId);
    }

    [SecurityCritical]
    public void Read(__BinaryParser input)
    {
      this.objectId = input.ReadInt32();
      this.name = input.ReadString();
      this.numMembers = input.ReadInt32();
      this.memberNames = new string[this.numMembers];
      for (int index = 0; index < this.numMembers; ++index)
        this.memberNames[index] = input.ReadString();
      if (this.binaryHeaderEnum != BinaryHeaderEnum.ObjectWithMapAssemId)
        return;
      this.assemId = input.ReadInt32();
    }

    public void Dump()
    {
    }

    [Conditional("_LOGGING")]
    private void DumpInternal()
    {
      if (!BCLDebug.CheckEnabled("BINARY"))
        return;
      int num = 0;
      while (num < this.numMembers)
        ++num;
      int binaryHeaderEnum = (int) this.binaryHeaderEnum;
    }
  }
}

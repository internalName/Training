// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.SerializationHeaderRecord
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.IO;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class SerializationHeaderRecord : IStreamable
  {
    internal int binaryFormatterMajorVersion = 1;
    internal int binaryFormatterMinorVersion;
    internal BinaryHeaderEnum binaryHeaderEnum;
    internal int topId;
    internal int headerId;
    internal int majorVersion;
    internal int minorVersion;

    internal SerializationHeaderRecord()
    {
    }

    internal SerializationHeaderRecord(BinaryHeaderEnum binaryHeaderEnum, int topId, int headerId, int majorVersion, int minorVersion)
    {
      this.binaryHeaderEnum = binaryHeaderEnum;
      this.topId = topId;
      this.headerId = headerId;
      this.majorVersion = majorVersion;
      this.minorVersion = minorVersion;
    }

    public void Write(__BinaryWriter sout)
    {
      this.majorVersion = this.binaryFormatterMajorVersion;
      this.minorVersion = this.binaryFormatterMinorVersion;
      sout.WriteByte((byte) this.binaryHeaderEnum);
      sout.WriteInt32(this.topId);
      sout.WriteInt32(this.headerId);
      sout.WriteInt32(this.binaryFormatterMajorVersion);
      sout.WriteInt32(this.binaryFormatterMinorVersion);
    }

    private static int GetInt32(byte[] buffer, int index)
    {
      return (int) buffer[index] | (int) buffer[index + 1] << 8 | (int) buffer[index + 2] << 16 | (int) buffer[index + 3] << 24;
    }

    [SecurityCritical]
    public void Read(__BinaryParser input)
    {
      byte[] buffer = input.ReadBytes(17);
      if (buffer.Length < 17)
        __Error.EndOfFile();
      this.majorVersion = SerializationHeaderRecord.GetInt32(buffer, 9);
      if (this.majorVersion > this.binaryFormatterMajorVersion)
        throw new SerializationException(Environment.GetResourceString("Serialization_InvalidFormat", (object) BitConverter.ToString(buffer)));
      this.binaryHeaderEnum = (BinaryHeaderEnum) buffer[0];
      this.topId = SerializationHeaderRecord.GetInt32(buffer, 1);
      this.headerId = SerializationHeaderRecord.GetInt32(buffer, 5);
      this.minorVersion = SerializationHeaderRecord.GetInt32(buffer, 13);
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

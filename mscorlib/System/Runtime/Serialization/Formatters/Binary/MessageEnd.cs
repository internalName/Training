// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.MessageEnd
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.IO;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class MessageEnd : IStreamable
  {
    internal MessageEnd()
    {
    }

    public void Write(__BinaryWriter sout)
    {
      sout.WriteByte((byte) 11);
    }

    [SecurityCritical]
    public void Read(__BinaryParser input)
    {
    }

    public void Dump()
    {
    }

    public void Dump(Stream sout)
    {
    }

    [Conditional("_LOGGING")]
    private void DumpInternal(Stream sout)
    {
      if (!BCLDebug.CheckEnabled("BINARY"))
        return;
      long num = -1;
      if (sout == null || !sout.CanSeek)
        return;
      num = sout.Length;
    }
  }
}

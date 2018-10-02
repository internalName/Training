// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventDescriptor
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Diagnostics.Tracing
{
  [StructLayout(LayoutKind.Explicit, Size = 16)]
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  internal struct EventDescriptor
  {
    [FieldOffset(0)]
    private int m_traceloggingId;
    [FieldOffset(0)]
    private ushort m_id;
    [FieldOffset(2)]
    private byte m_version;
    [FieldOffset(3)]
    private byte m_channel;
    [FieldOffset(4)]
    private byte m_level;
    [FieldOffset(5)]
    private byte m_opcode;
    [FieldOffset(6)]
    private ushort m_task;
    [FieldOffset(8)]
    private long m_keywords;

    public EventDescriptor(int traceloggingId, byte level, byte opcode, long keywords)
    {
      this.m_id = (ushort) 0;
      this.m_version = (byte) 0;
      this.m_channel = (byte) 0;
      this.m_traceloggingId = traceloggingId;
      this.m_level = level;
      this.m_opcode = opcode;
      this.m_task = (ushort) 0;
      this.m_keywords = keywords;
    }

    public EventDescriptor(int id, byte version, byte channel, byte level, byte opcode, int task, long keywords)
    {
      if (id < 0)
        throw new ArgumentOutOfRangeException(nameof (id), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (id > (int) ushort.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (id), Environment.GetResourceString("ArgumentOutOfRange_NeedValidId", (object) 1, (object) ushort.MaxValue));
      this.m_traceloggingId = 0;
      this.m_id = (ushort) id;
      this.m_version = version;
      this.m_channel = channel;
      this.m_level = level;
      this.m_opcode = opcode;
      this.m_keywords = keywords;
      if (task < 0)
        throw new ArgumentOutOfRangeException(nameof (task), Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
      if (task > (int) ushort.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (task), Environment.GetResourceString("ArgumentOutOfRange_NeedValidId", (object) 1, (object) ushort.MaxValue));
      this.m_task = (ushort) task;
    }

    public int EventId
    {
      get
      {
        return (int) this.m_id;
      }
    }

    public byte Version
    {
      get
      {
        return this.m_version;
      }
    }

    public byte Channel
    {
      get
      {
        return this.m_channel;
      }
    }

    public byte Level
    {
      get
      {
        return this.m_level;
      }
    }

    public byte Opcode
    {
      get
      {
        return this.m_opcode;
      }
    }

    public int Task
    {
      get
      {
        return (int) this.m_task;
      }
    }

    public long Keywords
    {
      get
      {
        return this.m_keywords;
      }
    }

    public override bool Equals(object obj)
    {
      if (!(obj is EventDescriptor))
        return false;
      return this.Equals((EventDescriptor) obj);
    }

    public override int GetHashCode()
    {
      return (int) this.m_id ^ (int) this.m_version ^ (int) this.m_channel ^ (int) this.m_level ^ (int) this.m_opcode ^ (int) this.m_task ^ (int) this.m_keywords;
    }

    public bool Equals(EventDescriptor other)
    {
      return (int) this.m_id == (int) other.m_id && (int) this.m_version == (int) other.m_version && ((int) this.m_channel == (int) other.m_channel && (int) this.m_level == (int) other.m_level) && ((int) this.m_opcode == (int) other.m_opcode && (int) this.m_task == (int) other.m_task && this.m_keywords == other.m_keywords);
    }

    public static bool operator ==(EventDescriptor event1, EventDescriptor event2)
    {
      return event1.Equals(event2);
    }

    public static bool operator !=(EventDescriptor event1, EventDescriptor event2)
    {
      return !event1.Equals(event2);
    }
  }
}

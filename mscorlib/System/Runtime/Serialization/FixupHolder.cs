// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.FixupHolder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Serialization
{
  [Serializable]
  internal class FixupHolder
  {
    internal const int ArrayFixup = 1;
    internal const int MemberFixup = 2;
    internal const int DelayedFixup = 4;
    internal long m_id;
    internal object m_fixupInfo;
    internal int m_fixupType;

    internal FixupHolder(long id, object fixupInfo, int fixupType)
    {
      this.m_id = id;
      this.m_fixupInfo = fixupInfo;
      this.m_fixupType = fixupType;
    }
  }
}

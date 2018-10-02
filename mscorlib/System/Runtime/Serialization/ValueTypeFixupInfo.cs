// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.ValueTypeFixupInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;

namespace System.Runtime.Serialization
{
  internal class ValueTypeFixupInfo
  {
    private long m_containerID;
    private FieldInfo m_parentField;
    private int[] m_parentIndex;

    public ValueTypeFixupInfo(long containerID, FieldInfo member, int[] parentIndex)
    {
      if (member == (FieldInfo) null && parentIndex == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustSupplyParent"));
      if (containerID == 0L && member == (FieldInfo) null)
      {
        this.m_containerID = containerID;
        this.m_parentField = member;
        this.m_parentIndex = parentIndex;
      }
      if (member != (FieldInfo) null)
      {
        if (parentIndex != null)
          throw new ArgumentException(Environment.GetResourceString("Argument_MemberAndArray"));
        if (member.FieldType.IsValueType && containerID == 0L)
          throw new ArgumentException(Environment.GetResourceString("Argument_MustSupplyContainer"));
      }
      this.m_containerID = containerID;
      this.m_parentField = member;
      this.m_parentIndex = parentIndex;
    }

    public long ContainerID
    {
      get
      {
        return this.m_containerID;
      }
    }

    public FieldInfo ParentField
    {
      get
      {
        return this.m_parentField;
      }
    }

    public int[] ParentIndex
    {
      get
      {
        return this.m_parentIndex;
      }
    }
  }
}

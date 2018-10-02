// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.ObjectMapInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class ObjectMapInfo
  {
    internal int objectId;
    private int numMembers;
    private string[] memberNames;
    private Type[] memberTypes;

    internal ObjectMapInfo(int objectId, int numMembers, string[] memberNames, Type[] memberTypes)
    {
      this.objectId = objectId;
      this.numMembers = numMembers;
      this.memberNames = memberNames;
      this.memberTypes = memberTypes;
    }

    internal bool isCompatible(int numMembers, string[] memberNames, Type[] memberTypes)
    {
      bool flag = true;
      if (this.numMembers == numMembers)
      {
        for (int index = 0; index < numMembers; ++index)
        {
          if (!this.memberNames[index].Equals(memberNames[index]))
          {
            flag = false;
            break;
          }
          if (memberTypes != null && this.memberTypes[index] != memberTypes[index])
          {
            flag = false;
            break;
          }
        }
      }
      else
        flag = false;
      return flag;
    }
  }
}

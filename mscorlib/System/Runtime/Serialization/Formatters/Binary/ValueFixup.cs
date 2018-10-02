// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.ValueFixup
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class ValueFixup
  {
    internal ValueFixupEnum valueFixupEnum;
    internal Array arrayObj;
    internal int[] indexMap;
    internal object header;
    internal object memberObject;
    internal static volatile MemberInfo valueInfo;
    internal ReadObjectInfo objectInfo;
    internal string memberName;

    internal ValueFixup(Array arrayObj, int[] indexMap)
    {
      this.valueFixupEnum = ValueFixupEnum.Array;
      this.arrayObj = arrayObj;
      this.indexMap = indexMap;
    }

    internal ValueFixup(object memberObject, string memberName, ReadObjectInfo objectInfo)
    {
      this.valueFixupEnum = ValueFixupEnum.Member;
      this.memberObject = memberObject;
      this.memberName = memberName;
      this.objectInfo = objectInfo;
    }

    [SecurityCritical]
    internal void Fixup(ParseRecord record, ParseRecord parent)
    {
      object prnewObj = record.PRnewObj;
      switch (this.valueFixupEnum)
      {
        case ValueFixupEnum.Array:
          this.arrayObj.SetValue(prnewObj, this.indexMap);
          break;
        case ValueFixupEnum.Header:
          Type type = typeof (Header);
          if (ValueFixup.valueInfo == (MemberInfo) null)
          {
            MemberInfo[] member = type.GetMember("Value");
            if (member.Length != 1)
              throw new SerializationException(Environment.GetResourceString("Serialization_HeaderReflection", (object) member.Length));
            ValueFixup.valueInfo = member[0];
          }
          FormatterServices.SerializationSetValue(ValueFixup.valueInfo, this.header, prnewObj);
          break;
        case ValueFixupEnum.Member:
          if (this.objectInfo.isSi)
          {
            this.objectInfo.objectManager.RecordDelayedFixup(parent.PRobjectId, this.memberName, record.PRobjectId);
            break;
          }
          MemberInfo memberInfo = this.objectInfo.GetMemberInfo(this.memberName);
          if (!(memberInfo != (MemberInfo) null))
            break;
          this.objectInfo.objectManager.RecordFixup(parent.PRobjectId, memberInfo, record.PRobjectId);
          break;
      }
    }
  }
}

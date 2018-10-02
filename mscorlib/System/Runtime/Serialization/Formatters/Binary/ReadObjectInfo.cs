// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.ReadObjectInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Security;
using System.Threading;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class ReadObjectInfo
  {
    internal int objectInfoId;
    internal static int readObjectInfoCounter;
    internal Type objectType;
    internal ObjectManager objectManager;
    internal int count;
    internal bool isSi;
    internal bool isNamed;
    internal bool isTyped;
    internal bool bSimpleAssembly;
    internal SerObjectInfoCache cache;
    internal string[] wireMemberNames;
    internal Type[] wireMemberTypes;
    private int lastPosition;
    internal ISurrogateSelector surrogateSelector;
    internal ISerializationSurrogate serializationSurrogate;
    internal StreamingContext context;
    internal List<Type> memberTypesList;
    internal SerObjectInfoInit serObjectInfoInit;
    internal IFormatterConverter formatterConverter;

    internal ReadObjectInfo()
    {
    }

    internal void ObjectEnd()
    {
    }

    internal void PrepareForReuse()
    {
      this.lastPosition = 0;
    }

    [SecurityCritical]
    internal static ReadObjectInfo Create(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
    {
      ReadObjectInfo objectInfo = ReadObjectInfo.GetObjectInfo(serObjectInfoInit);
      objectInfo.Init(objectType, surrogateSelector, context, objectManager, serObjectInfoInit, converter, bSimpleAssembly);
      return objectInfo;
    }

    [SecurityCritical]
    internal void Init(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
    {
      this.objectType = objectType;
      this.objectManager = objectManager;
      this.context = context;
      this.serObjectInfoInit = serObjectInfoInit;
      this.formatterConverter = converter;
      this.bSimpleAssembly = bSimpleAssembly;
      this.InitReadConstructor(objectType, surrogateSelector, context);
    }

    [SecurityCritical]
    internal static ReadObjectInfo Create(Type objectType, string[] memberNames, Type[] memberTypes, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
    {
      ReadObjectInfo objectInfo = ReadObjectInfo.GetObjectInfo(serObjectInfoInit);
      objectInfo.Init(objectType, memberNames, memberTypes, surrogateSelector, context, objectManager, serObjectInfoInit, converter, bSimpleAssembly);
      return objectInfo;
    }

    [SecurityCritical]
    internal void Init(Type objectType, string[] memberNames, Type[] memberTypes, ISurrogateSelector surrogateSelector, StreamingContext context, ObjectManager objectManager, SerObjectInfoInit serObjectInfoInit, IFormatterConverter converter, bool bSimpleAssembly)
    {
      this.objectType = objectType;
      this.objectManager = objectManager;
      this.wireMemberNames = memberNames;
      this.wireMemberTypes = memberTypes;
      this.context = context;
      this.serObjectInfoInit = serObjectInfoInit;
      this.formatterConverter = converter;
      this.bSimpleAssembly = bSimpleAssembly;
      if (memberNames != null)
        this.isNamed = true;
      if (memberTypes != null)
        this.isTyped = true;
      if ((object) objectType == null)
        return;
      this.InitReadConstructor(objectType, surrogateSelector, context);
    }

    [SecurityCritical]
    private void InitReadConstructor(Type objectType, ISurrogateSelector surrogateSelector, StreamingContext context)
    {
      if (objectType.IsArray)
      {
        this.InitNoMembers();
      }
      else
      {
        ISurrogateSelector selector = (ISurrogateSelector) null;
        if (surrogateSelector != null)
          this.serializationSurrogate = surrogateSelector.GetSurrogate(objectType, context, out selector);
        if (this.serializationSurrogate != null)
          this.isSi = true;
        else if ((object) objectType != (object) Converter.typeofObject && Converter.typeofISerializable.IsAssignableFrom(objectType))
          this.isSi = true;
        if (this.isSi)
          this.InitSiRead();
        else
          this.InitMemberInfo();
      }
    }

    private void InitSiRead()
    {
      if (this.memberTypesList == null)
        return;
      this.memberTypesList = new List<Type>(20);
    }

    private void InitNoMembers()
    {
      this.cache = new SerObjectInfoCache(this.objectType);
    }

    [SecurityCritical]
    private void InitMemberInfo()
    {
      this.cache = new SerObjectInfoCache(this.objectType);
      this.cache.memberInfos = FormatterServices.GetSerializableMembers(this.objectType, this.context);
      this.count = this.cache.memberInfos.Length;
      this.cache.memberNames = new string[this.count];
      this.cache.memberTypes = new Type[this.count];
      for (int index = 0; index < this.count; ++index)
      {
        this.cache.memberNames[index] = this.cache.memberInfos[index].Name;
        this.cache.memberTypes[index] = this.GetMemberType(this.cache.memberInfos[index]);
      }
      this.isTyped = true;
      this.isNamed = true;
    }

    internal MemberInfo GetMemberInfo(string name)
    {
      if (this.cache == null)
        return (MemberInfo) null;
      if (this.isSi)
        throw new SerializationException(Environment.GetResourceString("Serialization_MemberInfo", (object) (this.objectType.ToString() + " " + name)));
      if (this.cache.memberInfos == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_NoMemberInfo", (object) (this.objectType.ToString() + " " + name)));
      if (this.Position(name) != -1)
        return this.cache.memberInfos[this.Position(name)];
      return (MemberInfo) null;
    }

    internal Type GetType(string name)
    {
      int index = this.Position(name);
      if (index == -1)
        return (Type) null;
      Type type = !this.isTyped ? this.memberTypesList[index] : this.cache.memberTypes[index];
      if ((object) type == null)
        throw new SerializationException(Environment.GetResourceString("Serialization_ISerializableTypes", (object) (this.objectType.ToString() + " " + name)));
      return type;
    }

    internal void AddValue(string name, object value, ref SerializationInfo si, ref object[] memberData)
    {
      if (this.isSi)
      {
        si.AddValue(name, value);
      }
      else
      {
        int index = this.Position(name);
        if (index == -1)
          return;
        memberData[index] = value;
      }
    }

    internal void InitDataStore(ref SerializationInfo si, ref object[] memberData)
    {
      if (this.isSi)
      {
        if (si != null)
          return;
        si = new SerializationInfo(this.objectType, this.formatterConverter);
      }
      else
      {
        if (memberData != null || this.cache == null)
          return;
        memberData = new object[this.cache.memberNames.Length];
      }
    }

    internal void RecordFixup(long objectId, string name, long idRef)
    {
      if (this.isSi)
      {
        this.objectManager.RecordDelayedFixup(objectId, name, idRef);
      }
      else
      {
        int index = this.Position(name);
        if (index == -1)
          return;
        this.objectManager.RecordFixup(objectId, this.cache.memberInfos[index], idRef);
      }
    }

    [SecurityCritical]
    internal void PopulateObjectMembers(object obj, object[] memberData)
    {
      if (this.isSi || memberData == null)
        return;
      FormatterServices.PopulateObjectMembers(obj, this.cache.memberInfos, memberData);
    }

    [Conditional("SER_LOGGING")]
    private void DumpPopulate(MemberInfo[] memberInfos, object[] memberData)
    {
      int num = 0;
      while (num < memberInfos.Length)
        ++num;
    }

    [Conditional("SER_LOGGING")]
    private void DumpPopulateSi()
    {
    }

    private int Position(string name)
    {
      if (this.cache == null)
        return -1;
      if (this.cache.memberNames.Length != 0 && this.cache.memberNames[this.lastPosition].Equals(name) || ++this.lastPosition < this.cache.memberNames.Length && this.cache.memberNames[this.lastPosition].Equals(name))
        return this.lastPosition;
      for (int index = 0; index < this.cache.memberNames.Length; ++index)
      {
        if (this.cache.memberNames[index].Equals(name))
        {
          this.lastPosition = index;
          return this.lastPosition;
        }
      }
      this.lastPosition = 0;
      return -1;
    }

    internal Type[] GetMemberTypes(string[] inMemberNames, Type objectType)
    {
      if (this.isSi)
        throw new SerializationException(Environment.GetResourceString("Serialization_ISerializableTypes", (object) objectType));
      if (this.cache == null)
        return (Type[]) null;
      if (this.cache.memberTypes == null)
      {
        this.cache.memberTypes = new Type[this.count];
        for (int index = 0; index < this.count; ++index)
          this.cache.memberTypes[index] = this.GetMemberType(this.cache.memberInfos[index]);
      }
      bool flag1 = false;
      if (inMemberNames.Length < this.cache.memberInfos.Length)
        flag1 = true;
      Type[] typeArray = new Type[this.cache.memberInfos.Length];
      for (int index1 = 0; index1 < this.cache.memberInfos.Length; ++index1)
      {
        if (!flag1 && inMemberNames[index1].Equals(this.cache.memberInfos[index1].Name))
        {
          typeArray[index1] = this.cache.memberTypes[index1];
        }
        else
        {
          bool flag2 = false;
          for (int index2 = 0; index2 < inMemberNames.Length; ++index2)
          {
            if (this.cache.memberInfos[index1].Name.Equals(inMemberNames[index2]))
            {
              typeArray[index1] = this.cache.memberTypes[index1];
              flag2 = true;
              break;
            }
          }
          if (!flag2)
          {
            object[] customAttributes = this.cache.memberInfos[index1].GetCustomAttributes(typeof (OptionalFieldAttribute), false);
            if ((customAttributes == null || customAttributes.Length == 0) && !this.bSimpleAssembly)
              throw new SerializationException(Environment.GetResourceString("Serialization_MissingMember", (object) this.cache.memberNames[index1], (object) objectType, (object) typeof (OptionalFieldAttribute).FullName));
          }
        }
      }
      return typeArray;
    }

    internal Type GetMemberType(MemberInfo objMember)
    {
      if ((object) (objMember as FieldInfo) != null)
        return ((FieldInfo) objMember).FieldType;
      if ((object) (objMember as PropertyInfo) != null)
        return ((PropertyInfo) objMember).PropertyType;
      throw new SerializationException(Environment.GetResourceString("Serialization_SerMemberInfo", (object) objMember.GetType()));
    }

    private static ReadObjectInfo GetObjectInfo(SerObjectInfoInit serObjectInfoInit)
    {
      return new ReadObjectInfo()
      {
        objectInfoId = Interlocked.Increment(ref ReadObjectInfo.readObjectInfoCounter)
      };
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.ObjectWriter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Text;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class ObjectWriter
  {
    private SerStack niPool = new SerStack("NameInfo Pool");
    private Queue m_objectQueue;
    private ObjectIDGenerator m_idGenerator;
    private int m_currentId;
    private ISurrogateSelector m_surrogates;
    private StreamingContext m_context;
    private __BinaryWriter serWriter;
    private SerializationObjectManager m_objectManager;
    private long topId;
    private string topName;
    private Header[] headers;
    private InternalFE formatterEnums;
    private SerializationBinder m_binder;
    private SerObjectInfoInit serObjectInfoInit;
    private IFormatterConverter m_formatterConverter;
    internal object[] crossAppDomainArray;
    internal ArrayList internalCrossAppDomainArray;
    private object previousObj;
    private long previousId;
    private Type previousType;
    private InternalPrimitiveTypeE previousCode;
    private Hashtable assemblyToIdTable;

    internal ObjectWriter(ISurrogateSelector selector, StreamingContext context, InternalFE formatterEnums, SerializationBinder binder)
    {
      this.m_currentId = 1;
      this.m_surrogates = selector;
      this.m_context = context;
      this.m_binder = binder;
      this.formatterEnums = formatterEnums;
      this.m_objectManager = new SerializationObjectManager(context);
    }

    [SecurityCritical]
    internal void Serialize(object graph, Header[] inHeaders, __BinaryWriter serWriter, bool fCheck)
    {
      if (graph == null)
        throw new ArgumentNullException(nameof (graph), Environment.GetResourceString("ArgumentNull_Graph"));
      if (serWriter == null)
        throw new ArgumentNullException(nameof (serWriter), Environment.GetResourceString("ArgumentNull_WithParamName", (object) nameof (serWriter)));
      if (fCheck)
        CodeAccessPermission.Demand(PermissionType.SecuritySerialization);
      this.serWriter = serWriter;
      this.headers = inHeaders;
      serWriter.WriteBegin();
      long headerId = 0;
      bool flag1 = false;
      bool flag2 = false;
      IMethodCallMessage mcm = graph as IMethodCallMessage;
      if (mcm != null)
      {
        flag1 = true;
        graph = (object) this.WriteMethodCall(mcm);
      }
      else
      {
        IMethodReturnMessage mrm = graph as IMethodReturnMessage;
        if (mrm != null)
        {
          flag2 = true;
          graph = (object) this.WriteMethodReturn(mrm);
        }
      }
      if (graph == null)
      {
        this.WriteSerializedStreamHeader(this.topId, headerId);
        if (flag1)
          serWriter.WriteMethodCall();
        else if (flag2)
          serWriter.WriteMethodReturn();
        serWriter.WriteSerializationHeaderEnd();
        serWriter.WriteEnd();
      }
      else
      {
        this.m_idGenerator = new ObjectIDGenerator();
        this.m_objectQueue = new Queue();
        this.m_formatterConverter = (IFormatterConverter) new FormatterConverter();
        this.serObjectInfoInit = new SerObjectInfoInit();
        bool isNew;
        this.topId = this.InternalGetId(graph, false, (Type) null, out isNew);
        this.WriteSerializedStreamHeader(this.topId, this.headers == null ? -1L : this.InternalGetId((object) this.headers, false, (Type) null, out isNew));
        if (flag1)
          serWriter.WriteMethodCall();
        else if (flag2)
          serWriter.WriteMethodReturn();
        if (this.headers != null && this.headers.Length != 0)
          this.m_objectQueue.Enqueue((object) this.headers);
        if (graph != null)
          this.m_objectQueue.Enqueue(graph);
        object next;
        long objID;
        while ((next = this.GetNext(out objID)) != null)
        {
          WriteObjectInfo objectInfo;
          if (next is WriteObjectInfo)
          {
            objectInfo = (WriteObjectInfo) next;
          }
          else
          {
            objectInfo = WriteObjectInfo.Serialize(next, this.m_surrogates, this.m_context, this.serObjectInfoInit, this.m_formatterConverter, this, this.m_binder);
            objectInfo.assemId = this.GetAssemblyId(objectInfo);
          }
          objectInfo.objectId = objID;
          NameInfo nameInfo = this.TypeToNameInfo(objectInfo);
          this.Write(objectInfo, nameInfo, nameInfo);
          this.PutNameInfo(nameInfo);
          objectInfo.ObjectEnd();
        }
        serWriter.WriteSerializationHeaderEnd();
        serWriter.WriteEnd();
        this.m_objectManager.RaiseOnSerializedEvent();
      }
    }

    [SecurityCritical]
    private object[] WriteMethodCall(IMethodCallMessage mcm)
    {
      string uri = mcm.Uri;
      string methodName = mcm.MethodName;
      string typeName = mcm.TypeName;
      object methodSignature = (object) null;
      object[] properties = (object[]) null;
      Type[] instArgs = (Type[]) null;
      if (mcm.MethodBase.IsGenericMethod)
        instArgs = mcm.MethodBase.GetGenericArguments();
      object[] args = mcm.Args;
      IInternalMessage internalMessage = mcm as IInternalMessage;
      if (internalMessage == null || internalMessage.HasProperties())
        properties = ObjectWriter.StoreUserPropertiesForMethodMessage((IMethodMessage) mcm);
      if (mcm.MethodSignature != null && RemotingServices.IsMethodOverloaded((IMethodMessage) mcm))
        methodSignature = mcm.MethodSignature;
      LogicalCallContext logicalCallContext = mcm.LogicalCallContext;
      object callContext = logicalCallContext != null ? (!logicalCallContext.HasInfo ? (object) logicalCallContext.RemotingData.LogicalCallID : (object) logicalCallContext) : (object) null;
      return this.serWriter.WriteCallArray(uri, methodName, typeName, instArgs, args, methodSignature, callContext, properties);
    }

    [SecurityCritical]
    private object[] WriteMethodReturn(IMethodReturnMessage mrm)
    {
      object returnValue = mrm.ReturnValue;
      object[] args = mrm.Args;
      Exception exception = mrm.Exception;
      object[] properties = (object[]) null;
      ReturnMessage returnMessage = mrm as ReturnMessage;
      if (returnMessage == null || returnMessage.HasProperties())
        properties = ObjectWriter.StoreUserPropertiesForMethodMessage((IMethodMessage) mrm);
      LogicalCallContext logicalCallContext = mrm.LogicalCallContext;
      object callContext = logicalCallContext != null ? (!logicalCallContext.HasInfo ? (object) logicalCallContext.RemotingData.LogicalCallID : (object) logicalCallContext) : (object) null;
      return this.serWriter.WriteReturnArray(returnValue, args, exception, callContext, properties);
    }

    [SecurityCritical]
    private static object[] StoreUserPropertiesForMethodMessage(IMethodMessage msg)
    {
      ArrayList arrayList = (ArrayList) null;
      IDictionary properties = msg.Properties;
      if (properties == null)
        return (object[]) null;
      MessageDictionary messageDictionary = properties as MessageDictionary;
      if (messageDictionary != null)
      {
        if (!messageDictionary.HasUserData())
          return (object[]) null;
        int num = 0;
        foreach (DictionaryEntry dictionaryEntry in messageDictionary.InternalDictionary)
        {
          if (arrayList == null)
            arrayList = new ArrayList();
          arrayList.Add((object) dictionaryEntry);
          ++num;
        }
        return arrayList.ToArray();
      }
      int num1 = 0;
      foreach (DictionaryEntry dictionaryEntry in properties)
      {
        if (arrayList == null)
          arrayList = new ArrayList();
        arrayList.Add((object) dictionaryEntry);
        ++num1;
      }
      return arrayList?.ToArray();
    }

    internal SerializationObjectManager ObjectManager
    {
      get
      {
        return this.m_objectManager;
      }
    }

    [SecurityCritical]
    private void Write(WriteObjectInfo objectInfo, NameInfo memberNameInfo, NameInfo typeNameInfo)
    {
      object obj = objectInfo.obj;
      if (obj == null)
        throw new ArgumentNullException("objectInfo.obj", Environment.GetResourceString("ArgumentNull_Obj"));
      Type objectType = objectInfo.objectType;
      long objectId = objectInfo.objectId;
      if ((object) objectType == (object) Converter.typeofString)
      {
        memberNameInfo.NIobjectId = objectId;
        this.serWriter.WriteObjectString((int) objectId, obj.ToString());
      }
      else if (objectInfo.isArray)
      {
        this.WriteArray(objectInfo, memberNameInfo, (WriteObjectInfo) null);
      }
      else
      {
        string[] outMemberNames;
        Type[] outMemberTypes;
        object[] outMemberData;
        objectInfo.GetMemberInfo(out outMemberNames, out outMemberTypes, out outMemberData);
        if (objectInfo.isSi || this.CheckTypeFormat(this.formatterEnums.FEtypeFormat, FormatterTypeStyle.TypesAlways))
        {
          memberNameInfo.NItransmitTypeOnObject = true;
          memberNameInfo.NIisParentTypeOnObject = true;
          typeNameInfo.NItransmitTypeOnObject = true;
          typeNameInfo.NIisParentTypeOnObject = true;
        }
        WriteObjectInfo[] memberObjectInfos = new WriteObjectInfo[outMemberNames.Length];
        for (int index = 0; index < outMemberTypes.Length; ++index)
        {
          Type type = (object) outMemberTypes[index] == null ? (outMemberData[index] == null ? Converter.typeofObject : this.GetType(outMemberData[index])) : outMemberTypes[index];
          if (this.ToCode(type) == InternalPrimitiveTypeE.Invalid && (object) type != (object) Converter.typeofString)
          {
            if (outMemberData[index] != null)
            {
              memberObjectInfos[index] = WriteObjectInfo.Serialize(outMemberData[index], this.m_surrogates, this.m_context, this.serObjectInfoInit, this.m_formatterConverter, this, this.m_binder);
              memberObjectInfos[index].assemId = this.GetAssemblyId(memberObjectInfos[index]);
            }
            else
            {
              memberObjectInfos[index] = WriteObjectInfo.Serialize(outMemberTypes[index], this.m_surrogates, this.m_context, this.serObjectInfoInit, this.m_formatterConverter, this.m_binder);
              memberObjectInfos[index].assemId = this.GetAssemblyId(memberObjectInfos[index]);
            }
          }
        }
        this.Write(objectInfo, memberNameInfo, typeNameInfo, outMemberNames, outMemberTypes, outMemberData, memberObjectInfos);
      }
    }

    [SecurityCritical]
    private void Write(WriteObjectInfo objectInfo, NameInfo memberNameInfo, NameInfo typeNameInfo, string[] memberNames, Type[] memberTypes, object[] memberData, WriteObjectInfo[] memberObjectInfos)
    {
      int length = memberNames.Length;
      NameInfo nameInfo = (NameInfo) null;
      if (memberNameInfo != null)
      {
        memberNameInfo.NIobjectId = objectInfo.objectId;
        this.serWriter.WriteObject(memberNameInfo, typeNameInfo, length, memberNames, memberTypes, memberObjectInfos);
      }
      else if (objectInfo.objectId == this.topId && this.topName != null)
      {
        nameInfo = this.MemberToNameInfo(this.topName);
        nameInfo.NIobjectId = objectInfo.objectId;
        this.serWriter.WriteObject(nameInfo, typeNameInfo, length, memberNames, memberTypes, memberObjectInfos);
      }
      else if ((object) objectInfo.objectType != (object) Converter.typeofString)
      {
        typeNameInfo.NIobjectId = objectInfo.objectId;
        this.serWriter.WriteObject(typeNameInfo, (NameInfo) null, length, memberNames, memberTypes, memberObjectInfos);
      }
      if (memberNameInfo.NIisParentTypeOnObject)
      {
        memberNameInfo.NItransmitTypeOnObject = true;
        memberNameInfo.NIisParentTypeOnObject = false;
      }
      else
        memberNameInfo.NItransmitTypeOnObject = false;
      for (int index = 0; index < length; ++index)
        this.WriteMemberSetup(objectInfo, memberNameInfo, typeNameInfo, memberNames[index], memberTypes[index], memberData[index], memberObjectInfos[index]);
      if (memberNameInfo != null)
      {
        memberNameInfo.NIobjectId = objectInfo.objectId;
        this.serWriter.WriteObjectEnd(memberNameInfo, typeNameInfo);
      }
      else if (objectInfo.objectId == this.topId && this.topName != null)
      {
        this.serWriter.WriteObjectEnd(nameInfo, typeNameInfo);
        this.PutNameInfo(nameInfo);
      }
      else
      {
        if ((object) objectInfo.objectType == (object) Converter.typeofString)
          return;
        this.serWriter.WriteObjectEnd(typeNameInfo, typeNameInfo);
      }
    }

    [SecurityCritical]
    private void WriteMemberSetup(WriteObjectInfo objectInfo, NameInfo memberNameInfo, NameInfo typeNameInfo, string memberName, Type memberType, object memberData, WriteObjectInfo memberObjectInfo)
    {
      NameInfo nameInfo1 = this.MemberToNameInfo(memberName);
      if (memberObjectInfo != null)
        nameInfo1.NIassemId = memberObjectInfo.assemId;
      nameInfo1.NItype = memberType;
      NameInfo nameInfo2 = memberObjectInfo != null ? this.TypeToNameInfo(memberObjectInfo) : this.TypeToNameInfo(memberType);
      nameInfo1.NItransmitTypeOnObject = memberNameInfo.NItransmitTypeOnObject;
      nameInfo1.NIisParentTypeOnObject = memberNameInfo.NIisParentTypeOnObject;
      this.WriteMembers(nameInfo1, nameInfo2, memberData, objectInfo, typeNameInfo, memberObjectInfo);
      this.PutNameInfo(nameInfo1);
      this.PutNameInfo(nameInfo2);
    }

    [SecurityCritical]
    private void WriteMembers(NameInfo memberNameInfo, NameInfo memberTypeNameInfo, object memberData, WriteObjectInfo objectInfo, NameInfo typeNameInfo, WriteObjectInfo memberObjectInfo)
    {
      Type nitype = memberNameInfo.NItype;
      bool assignUniqueIdToValueType = false;
      if ((object) nitype == (object) Converter.typeofObject || (object) Nullable.GetUnderlyingType(nitype) != null)
      {
        memberTypeNameInfo.NItransmitTypeOnMember = true;
        memberNameInfo.NItransmitTypeOnMember = true;
      }
      if (this.CheckTypeFormat(this.formatterEnums.FEtypeFormat, FormatterTypeStyle.TypesAlways) || objectInfo.isSi)
      {
        memberTypeNameInfo.NItransmitTypeOnObject = true;
        memberNameInfo.NItransmitTypeOnObject = true;
        memberNameInfo.NIisParentTypeOnObject = true;
      }
      if (this.CheckForNull(objectInfo, memberNameInfo, memberTypeNameInfo, memberData))
        return;
      object obj = memberData;
      Type type1 = (Type) null;
      if (memberTypeNameInfo.NIprimitiveTypeEnum == InternalPrimitiveTypeE.Invalid)
      {
        type1 = this.GetType(obj);
        if ((object) nitype != (object) type1)
        {
          memberTypeNameInfo.NItransmitTypeOnMember = true;
          memberNameInfo.NItransmitTypeOnMember = true;
        }
      }
      if ((object) nitype == (object) Converter.typeofObject)
      {
        assignUniqueIdToValueType = true;
        Type type2 = this.GetType(memberData);
        if (memberObjectInfo == null)
          this.TypeToNameInfo(type2, memberTypeNameInfo);
        else
          this.TypeToNameInfo(memberObjectInfo, memberTypeNameInfo);
      }
      if (memberObjectInfo != null && memberObjectInfo.isArray)
      {
        if ((object) type1 == null)
          this.GetType(obj);
        long objectId = this.Schedule(obj, false, (Type) null, memberObjectInfo);
        if (objectId > 0L)
        {
          memberNameInfo.NIobjectId = objectId;
          this.WriteObjectRef(memberNameInfo, objectId);
        }
        else
        {
          this.serWriter.WriteMemberNested(memberNameInfo);
          memberObjectInfo.objectId = objectId;
          memberNameInfo.NIobjectId = objectId;
          this.WriteArray(memberObjectInfo, memberNameInfo, memberObjectInfo);
          objectInfo.ObjectEnd();
        }
      }
      else
      {
        if (this.WriteKnownValueClass(memberNameInfo, memberTypeNameInfo, memberData))
          return;
        if ((object) type1 == null)
          type1 = this.GetType(obj);
        long objectId = this.Schedule(obj, assignUniqueIdToValueType, type1, memberObjectInfo);
        if (objectId < 0L)
        {
          memberObjectInfo.objectId = objectId;
          NameInfo nameInfo = this.TypeToNameInfo(memberObjectInfo);
          nameInfo.NIobjectId = objectId;
          this.Write(memberObjectInfo, memberNameInfo, nameInfo);
          this.PutNameInfo(nameInfo);
          memberObjectInfo.ObjectEnd();
        }
        else
        {
          memberNameInfo.NIobjectId = objectId;
          this.WriteObjectRef(memberNameInfo, objectId);
        }
      }
    }

    [SecurityCritical]
    private void WriteArray(WriteObjectInfo objectInfo, NameInfo memberNameInfo, WriteObjectInfo memberObjectInfo)
    {
      bool flag1 = false;
      if (memberNameInfo == null)
      {
        memberNameInfo = this.TypeToNameInfo(objectInfo);
        flag1 = true;
      }
      memberNameInfo.NIisArray = true;
      long objectId = objectInfo.objectId;
      memberNameInfo.NIobjectId = objectInfo.objectId;
      Array array = (Array) objectInfo.obj;
      Type elementType = objectInfo.objectType.GetElementType();
      WriteObjectInfo objectInfo1 = (WriteObjectInfo) null;
      if (!elementType.IsPrimitive)
      {
        objectInfo1 = WriteObjectInfo.Serialize(elementType, this.m_surrogates, this.m_context, this.serObjectInfoInit, this.m_formatterConverter, this.m_binder);
        objectInfo1.assemId = this.GetAssemblyId(objectInfo1);
      }
      NameInfo nameInfo1 = objectInfo1 != null ? this.TypeToNameInfo(objectInfo1) : this.TypeToNameInfo(elementType);
      nameInfo1.NIisArray = nameInfo1.NItype.IsArray;
      NameInfo nameInfo2 = memberNameInfo;
      nameInfo2.NIobjectId = objectId;
      nameInfo2.NIisArray = true;
      nameInfo1.NIobjectId = objectId;
      nameInfo1.NItransmitTypeOnMember = memberNameInfo.NItransmitTypeOnMember;
      nameInfo1.NItransmitTypeOnObject = memberNameInfo.NItransmitTypeOnObject;
      nameInfo1.NIisParentTypeOnObject = memberNameInfo.NIisParentTypeOnObject;
      int rank = array.Rank;
      int[] numArray1 = new int[rank];
      int[] lowerBoundA = new int[rank];
      int[] numArray2 = new int[rank];
      for (int dimension = 0; dimension < rank; ++dimension)
      {
        numArray1[dimension] = array.GetLength(dimension);
        lowerBoundA[dimension] = array.GetLowerBound(dimension);
        numArray2[dimension] = array.GetUpperBound(dimension);
      }
      InternalArrayTypeE internalArrayTypeE = !nameInfo1.NIisArray ? (rank != 1 ? InternalArrayTypeE.Rectangular : InternalArrayTypeE.Single) : (rank != 1 ? InternalArrayTypeE.Rectangular : InternalArrayTypeE.Jagged);
      nameInfo1.NIarrayEnum = internalArrayTypeE;
      if ((object) elementType == (object) Converter.typeofByte && rank == 1 && lowerBoundA[0] == 0)
      {
        this.serWriter.WriteObjectByteArray(memberNameInfo, nameInfo2, objectInfo1, nameInfo1, numArray1[0], lowerBoundA[0], (byte[]) array);
      }
      else
      {
        if ((object) elementType == (object) Converter.typeofObject || (object) Nullable.GetUnderlyingType(elementType) != null)
        {
          memberNameInfo.NItransmitTypeOnMember = true;
          nameInfo1.NItransmitTypeOnMember = true;
        }
        if (this.CheckTypeFormat(this.formatterEnums.FEtypeFormat, FormatterTypeStyle.TypesAlways))
        {
          memberNameInfo.NItransmitTypeOnObject = true;
          nameInfo1.NItransmitTypeOnObject = true;
        }
        switch (internalArrayTypeE)
        {
          case InternalArrayTypeE.Single:
            this.serWriter.WriteSingleArray(memberNameInfo, nameInfo2, objectInfo1, nameInfo1, numArray1[0], lowerBoundA[0], array);
            if (!Converter.IsWriteAsByteArray(nameInfo1.NIprimitiveTypeEnum) || lowerBoundA[0] != 0)
            {
              object[] objArray = (object[]) null;
              if (!elementType.IsValueType)
                objArray = (object[]) array;
              int num = numArray2[0] + 1;
              for (int index = lowerBoundA[0]; index < num; ++index)
              {
                if (objArray == null)
                  this.WriteArrayMember(objectInfo, nameInfo1, array.GetValue(index));
                else
                  this.WriteArrayMember(objectInfo, nameInfo1, objArray[index]);
              }
              this.serWriter.WriteItemEnd();
              break;
            }
            break;
          case InternalArrayTypeE.Jagged:
            nameInfo2.NIobjectId = objectId;
            this.serWriter.WriteJaggedArray(memberNameInfo, nameInfo2, objectInfo1, nameInfo1, numArray1[0], lowerBoundA[0]);
            object[] objArray1 = (object[]) array;
            for (int index = lowerBoundA[0]; index < numArray2[0] + 1; ++index)
              this.WriteArrayMember(objectInfo, nameInfo1, objArray1[index]);
            this.serWriter.WriteItemEnd();
            break;
          default:
            nameInfo2.NIobjectId = objectId;
            this.serWriter.WriteRectangleArray(memberNameInfo, nameInfo2, objectInfo1, nameInfo1, rank, numArray1, lowerBoundA);
            bool flag2 = false;
            for (int index = 0; index < rank; ++index)
            {
              if (numArray1[index] == 0)
              {
                flag2 = true;
                break;
              }
            }
            if (!flag2)
              this.WriteRectangle(objectInfo, rank, numArray1, array, nameInfo1, lowerBoundA);
            this.serWriter.WriteItemEnd();
            break;
        }
        this.serWriter.WriteObjectEnd(memberNameInfo, nameInfo2);
        this.PutNameInfo(nameInfo1);
        if (!flag1)
          return;
        this.PutNameInfo(memberNameInfo);
      }
    }

    [SecurityCritical]
    private void WriteArrayMember(WriteObjectInfo objectInfo, NameInfo arrayElemTypeNameInfo, object data)
    {
      arrayElemTypeNameInfo.NIisArrayItem = true;
      if (this.CheckForNull(objectInfo, arrayElemTypeNameInfo, arrayElemTypeNameInfo, data))
        return;
      Type type = (Type) null;
      bool flag = false;
      if (arrayElemTypeNameInfo.NItransmitTypeOnMember)
        flag = true;
      if (!flag && !arrayElemTypeNameInfo.IsSealed)
      {
        type = this.GetType(data);
        if ((object) arrayElemTypeNameInfo.NItype != (object) type)
          flag = true;
      }
      NameInfo nameInfo1;
      if (flag)
      {
        if ((object) type == null)
          type = this.GetType(data);
        nameInfo1 = this.TypeToNameInfo(type);
        nameInfo1.NItransmitTypeOnMember = true;
        nameInfo1.NIobjectId = arrayElemTypeNameInfo.NIobjectId;
        nameInfo1.NIassemId = arrayElemTypeNameInfo.NIassemId;
        nameInfo1.NIisArrayItem = true;
      }
      else
      {
        nameInfo1 = arrayElemTypeNameInfo;
        nameInfo1.NIisArrayItem = true;
      }
      if (!this.WriteKnownValueClass(arrayElemTypeNameInfo, nameInfo1, data))
      {
        object obj = data;
        bool assignUniqueIdToValueType = false;
        if ((object) arrayElemTypeNameInfo.NItype == (object) Converter.typeofObject)
          assignUniqueIdToValueType = true;
        long num = this.Schedule(obj, assignUniqueIdToValueType, nameInfo1.NItype);
        arrayElemTypeNameInfo.NIobjectId = num;
        nameInfo1.NIobjectId = num;
        if (num < 1L)
        {
          WriteObjectInfo objectInfo1 = WriteObjectInfo.Serialize(obj, this.m_surrogates, this.m_context, this.serObjectInfoInit, this.m_formatterConverter, this, this.m_binder);
          objectInfo1.objectId = num;
          objectInfo1.assemId = (object) arrayElemTypeNameInfo.NItype == (object) Converter.typeofObject || (object) Nullable.GetUnderlyingType(arrayElemTypeNameInfo.NItype) != null ? this.GetAssemblyId(objectInfo1) : nameInfo1.NIassemId;
          NameInfo nameInfo2 = this.TypeToNameInfo(objectInfo1);
          nameInfo2.NIobjectId = num;
          objectInfo1.objectId = num;
          this.Write(objectInfo1, nameInfo1, nameInfo2);
          objectInfo1.ObjectEnd();
        }
        else
          this.serWriter.WriteItemObjectRef(arrayElemTypeNameInfo, (int) num);
      }
      if (!arrayElemTypeNameInfo.NItransmitTypeOnMember)
        return;
      this.PutNameInfo(nameInfo1);
    }

    [SecurityCritical]
    private void WriteRectangle(WriteObjectInfo objectInfo, int rank, int[] maxA, Array array, NameInfo arrayElemNameTypeInfo, int[] lowerBoundA)
    {
      int[] numArray1 = new int[rank];
      int[] numArray2 = (int[]) null;
      bool flag1 = false;
      if (lowerBoundA != null)
      {
        for (int index = 0; index < rank; ++index)
        {
          if (lowerBoundA[index] != 0)
            flag1 = true;
        }
      }
      if (flag1)
        numArray2 = new int[rank];
      bool flag2 = true;
      while (flag2)
      {
        flag2 = false;
        if (flag1)
        {
          for (int index = 0; index < rank; ++index)
            numArray2[index] = numArray1[index] + lowerBoundA[index];
          this.WriteArrayMember(objectInfo, arrayElemNameTypeInfo, array.GetValue(numArray2));
        }
        else
          this.WriteArrayMember(objectInfo, arrayElemNameTypeInfo, array.GetValue(numArray1));
        for (int index1 = rank - 1; index1 > -1; --index1)
        {
          if (numArray1[index1] < maxA[index1] - 1)
          {
            ++numArray1[index1];
            if (index1 < rank - 1)
            {
              for (int index2 = index1 + 1; index2 < rank; ++index2)
                numArray1[index2] = 0;
            }
            flag2 = true;
            break;
          }
        }
      }
    }

    [Conditional("SER_LOGGING")]
    private void IndexTraceMessage(string message, int[] index)
    {
      StringBuilder stringBuilder = StringBuilderCache.Acquire(10);
      stringBuilder.Append("[");
      for (int index1 = 0; index1 < index.Length; ++index1)
      {
        stringBuilder.Append(index[index1]);
        if (index1 != index.Length - 1)
          stringBuilder.Append(",");
      }
      stringBuilder.Append("]");
    }

    private object GetNext(out long objID)
    {
      if (this.m_objectQueue.Count == 0)
      {
        objID = 0L;
        return (object) null;
      }
      object obj1 = this.m_objectQueue.Dequeue();
      object obj2 = !(obj1 is WriteObjectInfo) ? obj1 : ((WriteObjectInfo) obj1).obj;
      bool firstTime;
      objID = this.m_idGenerator.HasId(obj2, out firstTime);
      if (firstTime)
        throw new SerializationException(Environment.GetResourceString("Serialization_ObjNoID", obj2));
      return obj1;
    }

    private long InternalGetId(object obj, bool assignUniqueIdToValueType, Type type, out bool isNew)
    {
      if (obj == this.previousObj)
      {
        isNew = false;
        return this.previousId;
      }
      this.m_idGenerator.m_currentCount = this.m_currentId;
      if ((object) type != null && type.IsValueType && !assignUniqueIdToValueType)
      {
        isNew = false;
        return (long) (-1 * this.m_currentId++);
      }
      ++this.m_currentId;
      long id = this.m_idGenerator.GetId(obj, out isNew);
      this.previousObj = obj;
      this.previousId = id;
      return id;
    }

    private long Schedule(object obj, bool assignUniqueIdToValueType, Type type)
    {
      return this.Schedule(obj, assignUniqueIdToValueType, type, (WriteObjectInfo) null);
    }

    private long Schedule(object obj, bool assignUniqueIdToValueType, Type type, WriteObjectInfo objectInfo)
    {
      if (obj == null)
        return 0;
      bool isNew;
      long id = this.InternalGetId(obj, assignUniqueIdToValueType, type, out isNew);
      if (isNew && id > 0L)
      {
        if (objectInfo == null)
          this.m_objectQueue.Enqueue(obj);
        else
          this.m_objectQueue.Enqueue((object) objectInfo);
      }
      return id;
    }

    private bool WriteKnownValueClass(NameInfo memberNameInfo, NameInfo typeNameInfo, object data)
    {
      if ((object) typeNameInfo.NItype == (object) Converter.typeofString)
      {
        this.WriteString(memberNameInfo, typeNameInfo, data);
      }
      else
      {
        if (typeNameInfo.NIprimitiveTypeEnum == InternalPrimitiveTypeE.Invalid)
          return false;
        if (typeNameInfo.NIisArray)
          this.serWriter.WriteItem(memberNameInfo, typeNameInfo, data);
        else
          this.serWriter.WriteMember(memberNameInfo, typeNameInfo, data);
      }
      return true;
    }

    private void WriteObjectRef(NameInfo nameInfo, long objectId)
    {
      this.serWriter.WriteMemberObjectRef(nameInfo, (int) objectId);
    }

    private void WriteString(NameInfo memberNameInfo, NameInfo typeNameInfo, object stringObject)
    {
      bool isNew = true;
      long objectId = -1;
      if (!this.CheckTypeFormat(this.formatterEnums.FEtypeFormat, FormatterTypeStyle.XsdString))
        objectId = this.InternalGetId(stringObject, false, (Type) null, out isNew);
      typeNameInfo.NIobjectId = objectId;
      if (isNew || objectId < 0L)
        this.serWriter.WriteMemberString(memberNameInfo, typeNameInfo, (string) stringObject);
      else
        this.WriteObjectRef(memberNameInfo, objectId);
    }

    private bool CheckForNull(WriteObjectInfo objectInfo, NameInfo memberNameInfo, NameInfo typeNameInfo, object data)
    {
      bool flag = false;
      if (data == null)
        flag = true;
      if (flag && (this.formatterEnums.FEserializerTypeEnum == InternalSerializerTypeE.Binary || memberNameInfo.NIisArrayItem || (memberNameInfo.NItransmitTypeOnObject || memberNameInfo.NItransmitTypeOnMember) || (objectInfo.isSi || this.CheckTypeFormat(this.formatterEnums.FEtypeFormat, FormatterTypeStyle.TypesAlways))))
      {
        if (typeNameInfo.NIisArrayItem)
        {
          if (typeNameInfo.NIarrayEnum == InternalArrayTypeE.Single)
            this.serWriter.WriteDelayedNullItem();
          else
            this.serWriter.WriteNullItem(memberNameInfo, typeNameInfo);
        }
        else
          this.serWriter.WriteNullMember(memberNameInfo, typeNameInfo);
      }
      return flag;
    }

    private void WriteSerializedStreamHeader(long topId, long headerId)
    {
      this.serWriter.WriteSerializationHeader((int) topId, (int) headerId, 1, 0);
    }

    private NameInfo TypeToNameInfo(Type type, WriteObjectInfo objectInfo, InternalPrimitiveTypeE code, NameInfo nameInfo)
    {
      if (nameInfo == null)
        nameInfo = this.GetNameInfo();
      else
        nameInfo.Init();
      if (code == InternalPrimitiveTypeE.Invalid && objectInfo != null)
      {
        nameInfo.NIname = objectInfo.GetTypeFullName();
        nameInfo.NIassemId = objectInfo.assemId;
      }
      nameInfo.NIprimitiveTypeEnum = code;
      nameInfo.NItype = type;
      return nameInfo;
    }

    private NameInfo TypeToNameInfo(Type type)
    {
      return this.TypeToNameInfo(type, (WriteObjectInfo) null, this.ToCode(type), (NameInfo) null);
    }

    private NameInfo TypeToNameInfo(WriteObjectInfo objectInfo)
    {
      return this.TypeToNameInfo(objectInfo.objectType, objectInfo, this.ToCode(objectInfo.objectType), (NameInfo) null);
    }

    private NameInfo TypeToNameInfo(WriteObjectInfo objectInfo, NameInfo nameInfo)
    {
      return this.TypeToNameInfo(objectInfo.objectType, objectInfo, this.ToCode(objectInfo.objectType), nameInfo);
    }

    private void TypeToNameInfo(Type type, NameInfo nameInfo)
    {
      this.TypeToNameInfo(type, (WriteObjectInfo) null, this.ToCode(type), nameInfo);
    }

    private NameInfo MemberToNameInfo(string name)
    {
      NameInfo nameInfo = this.GetNameInfo();
      nameInfo.NIname = name;
      return nameInfo;
    }

    internal InternalPrimitiveTypeE ToCode(Type type)
    {
      if ((object) this.previousType == (object) type)
        return this.previousCode;
      InternalPrimitiveTypeE code = Converter.ToCode(type);
      if (code != InternalPrimitiveTypeE.Invalid)
      {
        this.previousType = type;
        this.previousCode = code;
      }
      return code;
    }

    private long GetAssemblyId(WriteObjectInfo objectInfo)
    {
      if (this.assemblyToIdTable == null)
        this.assemblyToIdTable = new Hashtable(5);
      bool isNew = false;
      string assemblyString1 = objectInfo.GetAssemblyString();
      string assemblyString2 = assemblyString1;
      long num;
      if (assemblyString1.Length == 0)
        num = 0L;
      else if (assemblyString1.Equals(Converter.urtAssemblyString))
      {
        num = 0L;
      }
      else
      {
        if (this.assemblyToIdTable.ContainsKey((object) assemblyString1))
        {
          num = (long) this.assemblyToIdTable[(object) assemblyString1];
          isNew = false;
        }
        else
        {
          num = this.InternalGetId((object) ("___AssemblyString___" + assemblyString1), false, (Type) null, out isNew);
          this.assemblyToIdTable[(object) assemblyString1] = (object) num;
        }
        this.serWriter.WriteAssembly(objectInfo.objectType, assemblyString2, (int) num, isNew);
      }
      return num;
    }

    [SecurityCritical]
    private Type GetType(object obj)
    {
      return !RemotingServices.IsTransparentProxy(obj) ? obj.GetType() : Converter.typeofMarshalByRefObject;
    }

    private NameInfo GetNameInfo()
    {
      NameInfo nameInfo;
      if (!this.niPool.IsEmpty())
      {
        nameInfo = (NameInfo) this.niPool.Pop();
        nameInfo.Init();
      }
      else
        nameInfo = new NameInfo();
      return nameInfo;
    }

    private bool CheckTypeFormat(FormatterTypeStyle test, FormatterTypeStyle want)
    {
      return (test & want) == want;
    }

    private void PutNameInfo(NameInfo nameInfo)
    {
      this.niPool.Push((object) nameInfo);
    }
  }
}

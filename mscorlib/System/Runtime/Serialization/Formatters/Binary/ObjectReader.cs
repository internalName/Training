// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.ObjectReader
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class ObjectReader
  {
    private static FileIOPermission sfileIOPermission = new FileIOPermission(PermissionState.Unrestricted);
    private NameCache typeCache = new NameCache();
    internal Stream m_stream;
    internal ISurrogateSelector m_surrogates;
    internal StreamingContext m_context;
    internal ObjectManager m_objectManager;
    internal InternalFE formatterEnums;
    internal SerializationBinder m_binder;
    internal long topId;
    internal bool bSimpleAssembly;
    internal object handlerObject;
    internal object m_topObject;
    internal Header[] headers;
    internal HeaderHandler handler;
    internal SerObjectInfoInit serObjectInfoInit;
    internal IFormatterConverter m_formatterConverter;
    internal SerStack stack;
    private SerStack valueFixupStack;
    internal object[] crossAppDomainArray;
    private bool bFullDeserialization;
    private bool bMethodCall;
    private bool bMethodReturn;
    private BinaryMethodCall binaryMethodCall;
    private BinaryMethodReturn binaryMethodReturn;
    private bool bIsCrossAppDomain;
    private const int THRESHOLD_FOR_VALUETYPE_IDS = 2147483647;
    private bool bOldFormatDetected;
    private IntSizedArray valTypeObjectIdTable;
    private string previousAssemblyString;
    private string previousName;
    private Type previousType;

    private SerStack ValueFixupStack
    {
      get
      {
        if (this.valueFixupStack == null)
          this.valueFixupStack = new SerStack("ValueType Fixup Stack");
        return this.valueFixupStack;
      }
    }

    internal object TopObject
    {
      get
      {
        return this.m_topObject;
      }
      set
      {
        this.m_topObject = value;
        if (this.m_objectManager == null)
          return;
        this.m_objectManager.TopObject = value;
      }
    }

    internal void SetMethodCall(BinaryMethodCall binaryMethodCall)
    {
      this.bMethodCall = true;
      this.binaryMethodCall = binaryMethodCall;
    }

    internal void SetMethodReturn(BinaryMethodReturn binaryMethodReturn)
    {
      this.bMethodReturn = true;
      this.binaryMethodReturn = binaryMethodReturn;
    }

    internal ObjectReader(Stream stream, ISurrogateSelector selector, StreamingContext context, InternalFE formatterEnums, SerializationBinder binder)
    {
      if (stream == null)
        throw new ArgumentNullException(nameof (stream), Environment.GetResourceString("ArgumentNull_Stream"));
      this.m_stream = stream;
      this.m_surrogates = selector;
      this.m_context = context;
      this.m_binder = binder;
      if (this.m_binder != null)
      {
        ResourceReader.TypeLimitingDeserializationBinder binder1 = this.m_binder as ResourceReader.TypeLimitingDeserializationBinder;
        if (binder1 != null)
          binder1.ObjectReader = this;
      }
      this.formatterEnums = formatterEnums;
    }

    [SecurityCritical]
    internal object Deserialize(HeaderHandler handler, __BinaryParser serParser, bool fCheck, bool isCrossAppDomain, IMethodCallMessage methodCallMessage)
    {
      if (serParser == null)
        throw new ArgumentNullException(nameof (serParser), Environment.GetResourceString("ArgumentNull_WithParamName", (object) serParser));
      this.bFullDeserialization = false;
      this.TopObject = (object) null;
      this.topId = 0L;
      this.bMethodCall = false;
      this.bMethodReturn = false;
      this.bIsCrossAppDomain = isCrossAppDomain;
      this.bSimpleAssembly = this.formatterEnums.FEassemblyFormat == FormatterAssemblyStyle.Simple;
      if (fCheck)
        CodeAccessPermission.Demand(PermissionType.SecuritySerialization);
      this.handler = handler;
      serParser.Run();
      if (this.bFullDeserialization)
        this.m_objectManager.DoFixups();
      if (!this.bMethodCall && !this.bMethodReturn)
      {
        if (this.TopObject == null)
          throw new SerializationException(Environment.GetResourceString("Serialization_TopObject"));
        if (this.HasSurrogate(this.TopObject.GetType()) && this.topId != 0L)
          this.TopObject = this.m_objectManager.GetObject(this.topId);
        if (this.TopObject is IObjectReference)
          this.TopObject = ((IObjectReference) this.TopObject).GetRealObject(this.m_context);
      }
      if (this.bFullDeserialization)
        this.m_objectManager.RaiseDeserializationEvent();
      if (handler != null)
        this.handlerObject = handler(this.headers);
      if (this.bMethodCall)
        this.TopObject = (object) this.binaryMethodCall.ReadArray(this.TopObject as object[], this.handlerObject);
      else if (this.bMethodReturn)
        this.TopObject = (object) this.binaryMethodReturn.ReadArray(this.TopObject as object[], methodCallMessage, this.handlerObject);
      return this.TopObject;
    }

    [SecurityCritical]
    private bool HasSurrogate(Type t)
    {
      if (this.m_surrogates == null)
        return false;
      ISurrogateSelector selector;
      return this.m_surrogates.GetSurrogate(t, this.m_context, out selector) != null;
    }

    [SecurityCritical]
    private void CheckSerializable(Type t)
    {
      if (!t.IsSerializable && !this.HasSurrogate(t))
        throw new SerializationException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Environment.GetResourceString("Serialization_NonSerType"), (object) t.FullName, (object) t.Assembly.FullName));
    }

    [SecurityCritical]
    private void InitFullDeserialization()
    {
      this.bFullDeserialization = true;
      this.stack = new SerStack("ObjectReader Object Stack");
      this.m_objectManager = new ObjectManager(this.m_surrogates, this.m_context, false, this.bIsCrossAppDomain);
      if (this.m_formatterConverter != null)
        return;
      this.m_formatterConverter = (IFormatterConverter) new FormatterConverter();
    }

    internal object CrossAppDomainArray(int index)
    {
      return this.crossAppDomainArray[index];
    }

    [SecurityCritical]
    internal ReadObjectInfo CreateReadObjectInfo(Type objectType)
    {
      return ReadObjectInfo.Create(objectType, this.m_surrogates, this.m_context, this.m_objectManager, this.serObjectInfoInit, this.m_formatterConverter, this.bSimpleAssembly);
    }

    [SecurityCritical]
    internal ReadObjectInfo CreateReadObjectInfo(Type objectType, string[] memberNames, Type[] memberTypes)
    {
      return ReadObjectInfo.Create(objectType, memberNames, memberTypes, this.m_surrogates, this.m_context, this.m_objectManager, this.serObjectInfoInit, this.m_formatterConverter, this.bSimpleAssembly);
    }

    [SecurityCritical]
    internal void Parse(ParseRecord pr)
    {
      switch (pr.PRparseTypeEnum)
      {
        case InternalParseTypeE.SerializedStreamHeader:
          this.ParseSerializedStreamHeader(pr);
          break;
        case InternalParseTypeE.Object:
          this.ParseObject(pr);
          break;
        case InternalParseTypeE.Member:
          this.ParseMember(pr);
          break;
        case InternalParseTypeE.ObjectEnd:
          this.ParseObjectEnd(pr);
          break;
        case InternalParseTypeE.MemberEnd:
          this.ParseMemberEnd(pr);
          break;
        case InternalParseTypeE.SerializedStreamHeaderEnd:
          this.ParseSerializedStreamHeaderEnd(pr);
          break;
        case InternalParseTypeE.Envelope:
          break;
        case InternalParseTypeE.EnvelopeEnd:
          break;
        case InternalParseTypeE.Body:
          break;
        case InternalParseTypeE.BodyEnd:
          break;
        default:
          throw new SerializationException(Environment.GetResourceString("Serialization_XMLElement", (object) pr.PRname));
      }
    }

    private void ParseError(ParseRecord processing, ParseRecord onStack)
    {
      throw new SerializationException(Environment.GetResourceString("Serialization_ParseError", (object) (onStack.PRname + " " + (object) onStack.PRparseTypeEnum + " " + processing.PRname + " " + (object) processing.PRparseTypeEnum)));
    }

    private void ParseSerializedStreamHeader(ParseRecord pr)
    {
      this.stack.Push((object) pr);
    }

    private void ParseSerializedStreamHeaderEnd(ParseRecord pr)
    {
      this.stack.Pop();
    }

    private bool IsRemoting
    {
      get
      {
        if (!this.bMethodCall)
          return this.bMethodReturn;
        return true;
      }
    }

    [SecurityCritical]
    internal void CheckSecurity(ParseRecord pr)
    {
      Type prdtType = pr.PRdtType;
      if ((object) prdtType == null || !this.IsRemoting)
        return;
      if (typeof (MarshalByRefObject).IsAssignableFrom(prdtType))
        throw new ArgumentException(Environment.GetResourceString("Serialization_MBRAsMBV", (object) prdtType.FullName));
      FormatterServices.CheckTypeSecurity(prdtType, this.formatterEnums.FEsecurityLevel);
    }

    [SecurityCritical]
    private void ParseObject(ParseRecord pr)
    {
      if (!this.bFullDeserialization)
        this.InitFullDeserialization();
      if (pr.PRobjectPositionEnum == InternalObjectPositionE.Top)
        this.topId = pr.PRobjectId;
      if (pr.PRparseTypeEnum == InternalParseTypeE.Object)
        this.stack.Push((object) pr);
      if (pr.PRobjectTypeEnum == InternalObjectTypeE.Array)
        this.ParseArray(pr);
      else if ((object) pr.PRdtType == null)
        pr.PRnewObj = (object) new TypeLoadExceptionHolder(pr.PRkeyDt);
      else if ((object) pr.PRdtType == (object) Converter.typeofString)
      {
        if (pr.PRvalue == null)
          return;
        pr.PRnewObj = (object) pr.PRvalue;
        if (pr.PRobjectPositionEnum == InternalObjectPositionE.Top)
        {
          this.TopObject = pr.PRnewObj;
        }
        else
        {
          this.stack.Pop();
          this.RegisterObject(pr.PRnewObj, pr, (ParseRecord) this.stack.Peek());
        }
      }
      else
      {
        this.CheckSerializable(pr.PRdtType);
        pr.PRnewObj = !this.IsRemoting || this.formatterEnums.FEsecurityLevel == TypeFilterLevel.Full ? FormatterServices.GetUninitializedObject(pr.PRdtType) : FormatterServices.GetSafeUninitializedObject(pr.PRdtType);
        this.m_objectManager.RaiseOnDeserializingEvent(pr.PRnewObj);
        if (pr.PRnewObj == null)
          throw new SerializationException(Environment.GetResourceString("Serialization_TopObjectInstantiate", (object) pr.PRdtType));
        if (pr.PRobjectPositionEnum == InternalObjectPositionE.Top)
          this.TopObject = pr.PRnewObj;
        if (pr.PRobjectInfo == null)
          pr.PRobjectInfo = ReadObjectInfo.Create(pr.PRdtType, this.m_surrogates, this.m_context, this.m_objectManager, this.serObjectInfoInit, this.m_formatterConverter, this.bSimpleAssembly);
        this.CheckSecurity(pr);
      }
    }

    [SecurityCritical]
    private void ParseObjectEnd(ParseRecord pr)
    {
      ParseRecord parseRecord1 = (ParseRecord) this.stack.Peek() ?? pr;
      if (parseRecord1.PRobjectPositionEnum == InternalObjectPositionE.Top && (object) parseRecord1.PRdtType == (object) Converter.typeofString)
      {
        parseRecord1.PRnewObj = (object) parseRecord1.PRvalue;
        this.TopObject = parseRecord1.PRnewObj;
      }
      else
      {
        this.stack.Pop();
        ParseRecord parseRecord2 = (ParseRecord) this.stack.Peek();
        if (parseRecord1.PRnewObj == null)
          return;
        if (parseRecord1.PRobjectTypeEnum == InternalObjectTypeE.Array)
        {
          if (parseRecord1.PRobjectPositionEnum == InternalObjectPositionE.Top)
            this.TopObject = parseRecord1.PRnewObj;
          this.RegisterObject(parseRecord1.PRnewObj, parseRecord1, parseRecord2);
        }
        else
        {
          parseRecord1.PRobjectInfo.PopulateObjectMembers(parseRecord1.PRnewObj, parseRecord1.PRmemberData);
          if (!parseRecord1.PRisRegistered && parseRecord1.PRobjectId > 0L)
            this.RegisterObject(parseRecord1.PRnewObj, parseRecord1, parseRecord2);
          if (parseRecord1.PRisValueTypeFixup)
            ((ValueFixup) this.ValueFixupStack.Pop()).Fixup(parseRecord1, parseRecord2);
          if (parseRecord1.PRobjectPositionEnum == InternalObjectPositionE.Top)
            this.TopObject = parseRecord1.PRnewObj;
          parseRecord1.PRobjectInfo.ObjectEnd();
        }
      }
    }

    [SecurityCritical]
    private void ParseArray(ParseRecord pr)
    {
      long probjectId = pr.PRobjectId;
      if (pr.PRarrayTypeEnum == InternalArrayTypeE.Base64)
      {
        pr.PRnewObj = pr.PRvalue.Length <= 0 ? (object) new byte[0] : (object) Convert.FromBase64String(pr.PRvalue);
        if (this.stack.Peek() == pr)
          this.stack.Pop();
        if (pr.PRobjectPositionEnum == InternalObjectPositionE.Top)
          this.TopObject = pr.PRnewObj;
        ParseRecord objectPr = (ParseRecord) this.stack.Peek();
        this.RegisterObject(pr.PRnewObj, pr, objectPr);
      }
      else if (pr.PRnewObj != null && Converter.IsWriteAsByteArray(pr.PRarrayElementTypeCode))
      {
        if (pr.PRobjectPositionEnum == InternalObjectPositionE.Top)
          this.TopObject = pr.PRnewObj;
        ParseRecord objectPr = (ParseRecord) this.stack.Peek();
        this.RegisterObject(pr.PRnewObj, pr, objectPr);
      }
      else if (pr.PRarrayTypeEnum == InternalArrayTypeE.Jagged || pr.PRarrayTypeEnum == InternalArrayTypeE.Single)
      {
        bool flag = true;
        if (pr.PRlowerBoundA == null || pr.PRlowerBoundA[0] == 0)
        {
          if ((object) pr.PRarrayElementType == (object) Converter.typeofString)
          {
            pr.PRobjectA = (object[]) new string[pr.PRlengthA[0]];
            pr.PRnewObj = (object) pr.PRobjectA;
            flag = false;
          }
          else if ((object) pr.PRarrayElementType == (object) Converter.typeofObject)
          {
            pr.PRobjectA = new object[pr.PRlengthA[0]];
            pr.PRnewObj = (object) pr.PRobjectA;
            flag = false;
          }
          else if ((object) pr.PRarrayElementType != null)
            pr.PRnewObj = (object) Array.UnsafeCreateInstance(pr.PRarrayElementType, pr.PRlengthA[0]);
          pr.PRisLowerBound = false;
        }
        else
        {
          if ((object) pr.PRarrayElementType != null)
            pr.PRnewObj = (object) Array.UnsafeCreateInstance(pr.PRarrayElementType, pr.PRlengthA, pr.PRlowerBoundA);
          pr.PRisLowerBound = true;
        }
        if (pr.PRarrayTypeEnum == InternalArrayTypeE.Single)
        {
          if (!pr.PRisLowerBound && Converter.IsWriteAsByteArray(pr.PRarrayElementTypeCode))
            pr.PRprimitiveArray = new PrimitiveArray(pr.PRarrayElementTypeCode, (Array) pr.PRnewObj);
          else if (flag && (object) pr.PRarrayElementType != null && (!pr.PRarrayElementType.IsValueType && !pr.PRisLowerBound))
            pr.PRobjectA = (object[]) pr.PRnewObj;
        }
        if (pr.PRobjectPositionEnum == InternalObjectPositionE.Headers)
          this.headers = (Header[]) pr.PRnewObj;
        pr.PRindexMap = new int[1];
      }
      else if (pr.PRarrayTypeEnum == InternalArrayTypeE.Rectangular)
      {
        pr.PRisLowerBound = false;
        if (pr.PRlowerBoundA != null)
        {
          for (int index = 0; index < pr.PRrank; ++index)
          {
            if (pr.PRlowerBoundA[index] != 0)
              pr.PRisLowerBound = true;
          }
        }
        if ((object) pr.PRarrayElementType != null)
          pr.PRnewObj = pr.PRisLowerBound ? (object) Array.UnsafeCreateInstance(pr.PRarrayElementType, pr.PRlengthA, pr.PRlowerBoundA) : (object) Array.UnsafeCreateInstance(pr.PRarrayElementType, pr.PRlengthA);
        int num = 1;
        for (int index = 0; index < pr.PRrank; ++index)
          num *= pr.PRlengthA[index];
        pr.PRindexMap = new int[pr.PRrank];
        pr.PRrectangularMap = new int[pr.PRrank];
        pr.PRlinearlength = num;
      }
      else
        throw new SerializationException(Environment.GetResourceString("Serialization_ArrayType", (object) pr.PRarrayTypeEnum));
      this.CheckSecurity(pr);
    }

    private void NextRectangleMap(ParseRecord pr)
    {
      for (int index1 = pr.PRrank - 1; index1 > -1; --index1)
      {
        if (pr.PRrectangularMap[index1] < pr.PRlengthA[index1] - 1)
        {
          ++pr.PRrectangularMap[index1];
          if (index1 < pr.PRrank - 1)
          {
            for (int index2 = index1 + 1; index2 < pr.PRrank; ++index2)
              pr.PRrectangularMap[index2] = 0;
          }
          Array.Copy((Array) pr.PRrectangularMap, (Array) pr.PRindexMap, pr.PRrank);
          break;
        }
      }
    }

    [SecurityCritical]
    private void ParseArrayMember(ParseRecord pr)
    {
      ParseRecord parseRecord = (ParseRecord) this.stack.Peek();
      if (parseRecord.PRarrayTypeEnum == InternalArrayTypeE.Rectangular)
      {
        if (parseRecord.PRmemberIndex > 0)
          this.NextRectangleMap(parseRecord);
        if (parseRecord.PRisLowerBound)
        {
          for (int index = 0; index < parseRecord.PRrank; ++index)
            parseRecord.PRindexMap[index] = parseRecord.PRrectangularMap[index] + parseRecord.PRlowerBoundA[index];
        }
      }
      else
        parseRecord.PRindexMap[0] = parseRecord.PRisLowerBound ? parseRecord.PRlowerBoundA[0] + parseRecord.PRmemberIndex : parseRecord.PRmemberIndex;
      if (pr.PRmemberValueEnum == InternalMemberValueE.Reference)
      {
        object obj = this.m_objectManager.GetObject(pr.PRidRef);
        if (obj == null)
        {
          int[] indices = new int[parseRecord.PRrank];
          Array.Copy((Array) parseRecord.PRindexMap, 0, (Array) indices, 0, parseRecord.PRrank);
          this.m_objectManager.RecordArrayElementFixup(parseRecord.PRobjectId, indices, pr.PRidRef);
        }
        else if (parseRecord.PRobjectA != null)
          parseRecord.PRobjectA[parseRecord.PRindexMap[0]] = obj;
        else
          ((Array) parseRecord.PRnewObj).SetValue(obj, parseRecord.PRindexMap);
      }
      else if (pr.PRmemberValueEnum == InternalMemberValueE.Nested)
      {
        if ((object) pr.PRdtType == null)
          pr.PRdtType = parseRecord.PRarrayElementType;
        this.ParseObject(pr);
        this.stack.Push((object) pr);
        if ((object) parseRecord.PRarrayElementType != null)
        {
          if (parseRecord.PRarrayElementType.IsValueType && pr.PRarrayElementTypeCode == InternalPrimitiveTypeE.Invalid)
          {
            pr.PRisValueTypeFixup = true;
            this.ValueFixupStack.Push((object) new ValueFixup((Array) parseRecord.PRnewObj, parseRecord.PRindexMap));
          }
          else if (parseRecord.PRobjectA != null)
            parseRecord.PRobjectA[parseRecord.PRindexMap[0]] = pr.PRnewObj;
          else
            ((Array) parseRecord.PRnewObj).SetValue(pr.PRnewObj, parseRecord.PRindexMap);
        }
      }
      else if (pr.PRmemberValueEnum == InternalMemberValueE.InlineValue)
      {
        if ((object) parseRecord.PRarrayElementType == (object) Converter.typeofString || (object) pr.PRdtType == (object) Converter.typeofString)
        {
          this.ParseString(pr, parseRecord);
          if (parseRecord.PRobjectA != null)
            parseRecord.PRobjectA[parseRecord.PRindexMap[0]] = (object) pr.PRvalue;
          else
            ((Array) parseRecord.PRnewObj).SetValue((object) pr.PRvalue, parseRecord.PRindexMap);
        }
        else if (parseRecord.PRisArrayVariant)
        {
          if (pr.PRkeyDt == null)
            throw new SerializationException(Environment.GetResourceString("Serialization_ArrayTypeObject"));
          object obj;
          if ((object) pr.PRdtType == (object) Converter.typeofString)
          {
            this.ParseString(pr, parseRecord);
            obj = (object) pr.PRvalue;
          }
          else if ((Enum) pr.PRdtTypeCode == (Enum) InternalPrimitiveTypeE.Invalid)
          {
            this.CheckSerializable(pr.PRdtType);
            obj = !this.IsRemoting || this.formatterEnums.FEsecurityLevel == TypeFilterLevel.Full ? FormatterServices.GetUninitializedObject(pr.PRdtType) : FormatterServices.GetSafeUninitializedObject(pr.PRdtType);
          }
          else
            obj = pr.PRvarValue == null ? Converter.FromString(pr.PRvalue, pr.PRdtTypeCode) : pr.PRvarValue;
          if (parseRecord.PRobjectA != null)
            parseRecord.PRobjectA[parseRecord.PRindexMap[0]] = obj;
          else
            ((Array) parseRecord.PRnewObj).SetValue(obj, parseRecord.PRindexMap);
        }
        else if (parseRecord.PRprimitiveArray != null)
        {
          parseRecord.PRprimitiveArray.SetValue(pr.PRvalue, parseRecord.PRindexMap[0]);
        }
        else
        {
          object obj = pr.PRvarValue == null ? Converter.FromString(pr.PRvalue, parseRecord.PRarrayElementTypeCode) : pr.PRvarValue;
          if (parseRecord.PRobjectA != null)
            parseRecord.PRobjectA[parseRecord.PRindexMap[0]] = obj;
          else
            ((Array) parseRecord.PRnewObj).SetValue(obj, parseRecord.PRindexMap);
        }
      }
      else if (pr.PRmemberValueEnum == InternalMemberValueE.Null)
        parseRecord.PRmemberIndex += pr.PRnullCount - 1;
      else
        this.ParseError(pr, parseRecord);
      ++parseRecord.PRmemberIndex;
    }

    [SecurityCritical]
    private void ParseArrayMemberEnd(ParseRecord pr)
    {
      if (pr.PRmemberValueEnum != InternalMemberValueE.Nested)
        return;
      this.ParseObjectEnd(pr);
    }

    [SecurityCritical]
    private void ParseMember(ParseRecord pr)
    {
      ParseRecord parseRecord = (ParseRecord) this.stack.Peek();
      string str = (string) null;
      if (parseRecord != null)
        str = parseRecord.PRname;
      switch (pr.PRmemberTypeEnum)
      {
        case InternalMemberTypeE.Item:
          this.ParseArrayMember(pr);
          break;
        default:
          if ((object) pr.PRdtType == null && parseRecord.PRobjectInfo.isTyped)
          {
            pr.PRdtType = parseRecord.PRobjectInfo.GetType(pr.PRname);
            if ((object) pr.PRdtType != null)
              pr.PRdtTypeCode = Converter.ToCode(pr.PRdtType);
          }
          if (pr.PRmemberValueEnum == InternalMemberValueE.Null)
          {
            parseRecord.PRobjectInfo.AddValue(pr.PRname, (object) null, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
            break;
          }
          if (pr.PRmemberValueEnum == InternalMemberValueE.Nested)
          {
            this.ParseObject(pr);
            this.stack.Push((object) pr);
            if (pr.PRobjectInfo != null && (object) pr.PRobjectInfo.objectType != null && pr.PRobjectInfo.objectType.IsValueType)
            {
              pr.PRisValueTypeFixup = true;
              this.ValueFixupStack.Push((object) new ValueFixup(parseRecord.PRnewObj, pr.PRname, parseRecord.PRobjectInfo));
              break;
            }
            parseRecord.PRobjectInfo.AddValue(pr.PRname, pr.PRnewObj, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
            break;
          }
          if (pr.PRmemberValueEnum == InternalMemberValueE.Reference)
          {
            object obj = this.m_objectManager.GetObject(pr.PRidRef);
            if (obj == null)
            {
              parseRecord.PRobjectInfo.AddValue(pr.PRname, (object) null, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
              parseRecord.PRobjectInfo.RecordFixup(parseRecord.PRobjectId, pr.PRname, pr.PRidRef);
              break;
            }
            parseRecord.PRobjectInfo.AddValue(pr.PRname, obj, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
            break;
          }
          if (pr.PRmemberValueEnum == InternalMemberValueE.InlineValue)
          {
            if ((object) pr.PRdtType == (object) Converter.typeofString)
            {
              this.ParseString(pr, parseRecord);
              parseRecord.PRobjectInfo.AddValue(pr.PRname, (object) pr.PRvalue, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
              break;
            }
            if (pr.PRdtTypeCode == InternalPrimitiveTypeE.Invalid)
            {
              if (pr.PRarrayTypeEnum == InternalArrayTypeE.Base64)
              {
                parseRecord.PRobjectInfo.AddValue(pr.PRname, (object) Convert.FromBase64String(pr.PRvalue), ref parseRecord.PRsi, ref parseRecord.PRmemberData);
                break;
              }
              if ((object) pr.PRdtType == (object) Converter.typeofObject)
                throw new SerializationException(Environment.GetResourceString("Serialization_TypeMissing", (object) pr.PRname));
              this.ParseString(pr, parseRecord);
              if ((object) pr.PRdtType == (object) Converter.typeofSystemVoid)
              {
                parseRecord.PRobjectInfo.AddValue(pr.PRname, (object) pr.PRdtType, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
                break;
              }
              if (!parseRecord.PRobjectInfo.isSi)
                break;
              parseRecord.PRobjectInfo.AddValue(pr.PRname, (object) pr.PRvalue, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
              break;
            }
            object obj = pr.PRvarValue == null ? Converter.FromString(pr.PRvalue, pr.PRdtTypeCode) : pr.PRvarValue;
            parseRecord.PRobjectInfo.AddValue(pr.PRname, obj, ref parseRecord.PRsi, ref parseRecord.PRmemberData);
            break;
          }
          this.ParseError(pr, parseRecord);
          break;
      }
    }

    [SecurityCritical]
    private void ParseMemberEnd(ParseRecord pr)
    {
      switch (pr.PRmemberTypeEnum)
      {
        case InternalMemberTypeE.Field:
          if (pr.PRmemberValueEnum != InternalMemberValueE.Nested)
            break;
          this.ParseObjectEnd(pr);
          break;
        case InternalMemberTypeE.Item:
          this.ParseArrayMemberEnd(pr);
          break;
        default:
          this.ParseError(pr, (ParseRecord) this.stack.Peek());
          break;
      }
    }

    [SecurityCritical]
    private void ParseString(ParseRecord pr, ParseRecord parentPr)
    {
      if (pr.PRisRegistered || pr.PRobjectId <= 0L)
        return;
      this.RegisterObject((object) pr.PRvalue, pr, parentPr, true);
    }

    [SecurityCritical]
    private void RegisterObject(object obj, ParseRecord pr, ParseRecord objectPr)
    {
      this.RegisterObject(obj, pr, objectPr, false);
    }

    [SecurityCritical]
    private void RegisterObject(object obj, ParseRecord pr, ParseRecord objectPr, bool bIsString)
    {
      if (pr.PRisRegistered)
        return;
      pr.PRisRegistered = true;
      long idOfContainingObj = 0;
      MemberInfo member = (MemberInfo) null;
      int[] arrayIndex = (int[]) null;
      if (objectPr != null)
      {
        arrayIndex = objectPr.PRindexMap;
        idOfContainingObj = objectPr.PRobjectId;
        if (objectPr.PRobjectInfo != null && !objectPr.PRobjectInfo.isSi)
          member = objectPr.PRobjectInfo.GetMemberInfo(pr.PRname);
      }
      SerializationInfo prsi = pr.PRsi;
      if (bIsString)
        this.m_objectManager.RegisterString((string) obj, pr.PRobjectId, prsi, idOfContainingObj, member);
      else
        this.m_objectManager.RegisterObject(obj, pr.PRobjectId, prsi, idOfContainingObj, member, arrayIndex);
    }

    [SecurityCritical]
    internal long GetId(long objectId)
    {
      if (!this.bFullDeserialization)
        this.InitFullDeserialization();
      if (objectId > 0L)
        return objectId;
      if (!this.bOldFormatDetected && objectId != -1L)
        return -1L * objectId;
      this.bOldFormatDetected = true;
      if (this.valTypeObjectIdTable == null)
        this.valTypeObjectIdTable = new IntSizedArray();
      long num;
      if ((num = (long) this.valTypeObjectIdTable[(int) objectId]) == 0L)
      {
        num = (long) int.MaxValue + objectId;
        this.valTypeObjectIdTable[(int) objectId] = (int) num;
      }
      return num;
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

    [SecurityCritical]
    internal Type Bind(string assemblyString, string typeString)
    {
      Type type = (Type) null;
      if (this.m_binder != null)
        type = this.m_binder.BindToType(assemblyString, typeString);
      if ((object) type == null)
        type = this.FastBindToType(assemblyString, typeString);
      return type;
    }

    [SecurityCritical]
    internal Type FastBindToType(string assemblyName, string typeName)
    {
      Type type = (Type) null;
      ObjectReader.TypeNAssembly typeNassembly = (ObjectReader.TypeNAssembly) this.typeCache.GetCachedValue(typeName);
      if (typeNassembly == null || typeNassembly.assemblyName != assemblyName)
      {
        Assembly assembly = (Assembly) null;
        if (this.bSimpleAssembly)
        {
          try
          {
            ObjectReader.sfileIOPermission.Assert();
            try
            {
              assembly = ObjectReader.ResolveSimpleAssemblyName(new AssemblyName(assemblyName));
            }
            finally
            {
              CodeAccessPermission.RevertAssert();
            }
          }
          catch (Exception ex)
          {
          }
          if (assembly == (Assembly) null)
            return (Type) null;
          ObjectReader.GetSimplyNamedTypeFromAssembly(assembly, typeName, ref type);
        }
        else
        {
          try
          {
            ObjectReader.sfileIOPermission.Assert();
            try
            {
              assembly = Assembly.Load(assemblyName);
            }
            finally
            {
              CodeAccessPermission.RevertAssert();
            }
          }
          catch (Exception ex)
          {
          }
          if (assembly == (Assembly) null)
            return (Type) null;
          type = FormatterServices.GetTypeFromAssembly(assembly, typeName);
        }
        if ((object) type == null)
          return (Type) null;
        ObjectReader.CheckTypeForwardedTo(assembly, type.Assembly, type);
        typeNassembly = new ObjectReader.TypeNAssembly();
        typeNassembly.type = type;
        typeNassembly.assemblyName = assemblyName;
        this.typeCache.SetCachedValue((object) typeNassembly);
      }
      return typeNassembly.type;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static Assembly ResolveSimpleAssemblyName(AssemblyName assemblyName)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMe;
      Assembly assembly = (Assembly) RuntimeAssembly.LoadWithPartialNameInternal(assemblyName, (Evidence) null, ref stackMark);
      if (assembly == (Assembly) null && assemblyName != null)
        assembly = (Assembly) RuntimeAssembly.LoadWithPartialNameInternal(assemblyName.Name, (Evidence) null, ref stackMark);
      return assembly;
    }

    [SecurityCritical]
    private static void GetSimplyNamedTypeFromAssembly(Assembly assm, string typeName, ref Type type)
    {
      try
      {
        type = FormatterServices.GetTypeFromAssembly(assm, typeName);
      }
      catch (TypeLoadException ex)
      {
      }
      catch (FileNotFoundException ex)
      {
      }
      catch (FileLoadException ex)
      {
      }
      catch (BadImageFormatException ex)
      {
      }
      if ((object) type != null)
        return;
      type = Type.GetType(typeName, new Func<AssemblyName, Assembly>(ObjectReader.ResolveSimpleAssemblyName), new Func<Assembly, string, bool, Type>(new ObjectReader.TopLevelAssemblyTypeResolver(assm).ResolveType), false);
    }

    [SecurityCritical]
    internal Type GetType(BinaryAssemblyInfo assemblyInfo, string name)
    {
      Type type;
      if (this.previousName != null && this.previousName.Length == name.Length && (this.previousName.Equals(name) && this.previousAssemblyString != null) && (this.previousAssemblyString.Length == assemblyInfo.assemblyString.Length && this.previousAssemblyString.Equals(assemblyInfo.assemblyString)))
      {
        type = this.previousType;
      }
      else
      {
        type = this.Bind(assemblyInfo.assemblyString, name);
        if ((object) type == null)
        {
          Assembly assembly = assemblyInfo.GetAssembly();
          if (this.bSimpleAssembly)
            ObjectReader.GetSimplyNamedTypeFromAssembly(assembly, name, ref type);
          else
            type = FormatterServices.GetTypeFromAssembly(assembly, name);
          if (type != (Type) null)
            ObjectReader.CheckTypeForwardedTo(assembly, type.Assembly, type);
        }
        this.previousAssemblyString = assemblyInfo.assemblyString;
        this.previousName = name;
        this.previousType = type;
      }
      return type;
    }

    [SecuritySafeCritical]
    private static void CheckTypeForwardedTo(Assembly sourceAssembly, Assembly destAssembly, Type resolvedType)
    {
      if (FormatterServices.UnsafeTypeForwardersIsEnabled() || !(sourceAssembly != destAssembly) || destAssembly.PermissionSet.IsSubsetOf(sourceAssembly.PermissionSet))
        return;
      TypeInformation typeInformation = BinaryFormatter.GetTypeInformation(resolvedType);
      if (typeInformation.HasTypeForwardedFrom)
      {
        Assembly assembly = (Assembly) null;
        try
        {
          assembly = Assembly.Load(typeInformation.AssemblyString);
        }
        catch
        {
        }
        if (assembly != sourceAssembly)
          throw new SecurityException()
          {
            Demanded = (object) sourceAssembly.PermissionSet
          };
      }
      else
        throw new SecurityException()
        {
          Demanded = (object) sourceAssembly.PermissionSet
        };
    }

    internal class TypeNAssembly
    {
      public Type type;
      public string assemblyName;
    }

    internal sealed class TopLevelAssemblyTypeResolver
    {
      private Assembly m_topLevelAssembly;

      public TopLevelAssemblyTypeResolver(Assembly topLevelAssembly)
      {
        this.m_topLevelAssembly = topLevelAssembly;
      }

      public Type ResolveType(Assembly assembly, string simpleTypeName, bool ignoreCase)
      {
        if (assembly == (Assembly) null)
          assembly = this.m_topLevelAssembly;
        return assembly.GetType(simpleTypeName, false, ignoreCase);
      }
    }
  }
}

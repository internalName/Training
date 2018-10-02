// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.ParseRecord
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class ParseRecord
  {
    internal static int parseRecordIdCount = 1;
    internal int PRparseRecordId;
    internal InternalParseTypeE PRparseTypeEnum;
    internal InternalObjectTypeE PRobjectTypeEnum;
    internal InternalArrayTypeE PRarrayTypeEnum;
    internal InternalMemberTypeE PRmemberTypeEnum;
    internal InternalMemberValueE PRmemberValueEnum;
    internal InternalObjectPositionE PRobjectPositionEnum;
    internal string PRname;
    internal string PRvalue;
    internal object PRvarValue;
    internal string PRkeyDt;
    internal Type PRdtType;
    internal InternalPrimitiveTypeE PRdtTypeCode;
    internal bool PRisVariant;
    internal bool PRisEnum;
    internal long PRobjectId;
    internal long PRidRef;
    internal string PRarrayElementTypeString;
    internal Type PRarrayElementType;
    internal bool PRisArrayVariant;
    internal InternalPrimitiveTypeE PRarrayElementTypeCode;
    internal int PRrank;
    internal int[] PRlengthA;
    internal int[] PRpositionA;
    internal int[] PRlowerBoundA;
    internal int[] PRupperBoundA;
    internal int[] PRindexMap;
    internal int PRmemberIndex;
    internal int PRlinearlength;
    internal int[] PRrectangularMap;
    internal bool PRisLowerBound;
    internal long PRtopId;
    internal long PRheaderId;
    internal ReadObjectInfo PRobjectInfo;
    internal bool PRisValueTypeFixup;
    internal object PRnewObj;
    internal object[] PRobjectA;
    internal PrimitiveArray PRprimitiveArray;
    internal bool PRisRegistered;
    internal object[] PRmemberData;
    internal SerializationInfo PRsi;
    internal int PRnullCount;

    internal ParseRecord()
    {
    }

    internal void Init()
    {
      this.PRparseTypeEnum = InternalParseTypeE.Empty;
      this.PRobjectTypeEnum = InternalObjectTypeE.Empty;
      this.PRarrayTypeEnum = InternalArrayTypeE.Empty;
      this.PRmemberTypeEnum = InternalMemberTypeE.Empty;
      this.PRmemberValueEnum = InternalMemberValueE.Empty;
      this.PRobjectPositionEnum = InternalObjectPositionE.Empty;
      this.PRname = (string) null;
      this.PRvalue = (string) null;
      this.PRkeyDt = (string) null;
      this.PRdtType = (Type) null;
      this.PRdtTypeCode = InternalPrimitiveTypeE.Invalid;
      this.PRisEnum = false;
      this.PRobjectId = 0L;
      this.PRidRef = 0L;
      this.PRarrayElementTypeString = (string) null;
      this.PRarrayElementType = (Type) null;
      this.PRisArrayVariant = false;
      this.PRarrayElementTypeCode = InternalPrimitiveTypeE.Invalid;
      this.PRrank = 0;
      this.PRlengthA = (int[]) null;
      this.PRpositionA = (int[]) null;
      this.PRlowerBoundA = (int[]) null;
      this.PRupperBoundA = (int[]) null;
      this.PRindexMap = (int[]) null;
      this.PRmemberIndex = 0;
      this.PRlinearlength = 0;
      this.PRrectangularMap = (int[]) null;
      this.PRisLowerBound = false;
      this.PRtopId = 0L;
      this.PRheaderId = 0L;
      this.PRisValueTypeFixup = false;
      this.PRnewObj = (object) null;
      this.PRobjectA = (object[]) null;
      this.PRprimitiveArray = (PrimitiveArray) null;
      this.PRobjectInfo = (ReadObjectInfo) null;
      this.PRisRegistered = false;
      this.PRmemberData = (object[]) null;
      this.PRsi = (SerializationInfo) null;
      this.PRnullCount = 0;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Reflection.MetadataImport
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
  internal struct MetadataImport
  {
    internal static readonly MetadataImport EmptyImport = new MetadataImport((IntPtr) 0, (object) null);
    private IntPtr m_metadataImport2;
    private object m_keepalive;

    public override int GetHashCode()
    {
      return ValueType.GetHashCodeOfPtr(this.m_metadataImport2);
    }

    public override bool Equals(object obj)
    {
      if (!(obj is MetadataImport))
        return false;
      return this.Equals((MetadataImport) obj);
    }

    private bool Equals(MetadataImport import)
    {
      return import.m_metadataImport2 == this.m_metadataImport2;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _GetMarshalAs(IntPtr pNativeType, int cNativeType, out int unmanagedType, out int safeArraySubType, out string safeArrayUserDefinedSubType, out int arraySubType, out int sizeParamIndex, out int sizeConst, out string marshalType, out string marshalCookie, out int iidParamIndex);

    [SecurityCritical]
    internal static void GetMarshalAs(ConstArray nativeType, out UnmanagedType unmanagedType, out VarEnum safeArraySubType, out string safeArrayUserDefinedSubType, out UnmanagedType arraySubType, out int sizeParamIndex, out int sizeConst, out string marshalType, out string marshalCookie, out int iidParamIndex)
    {
      int unmanagedType1;
      int safeArraySubType1;
      int arraySubType1;
      MetadataImport._GetMarshalAs(nativeType.Signature, nativeType.Length, out unmanagedType1, out safeArraySubType1, out safeArrayUserDefinedSubType, out arraySubType1, out sizeParamIndex, out sizeConst, out marshalType, out marshalCookie, out iidParamIndex);
      unmanagedType = (UnmanagedType) unmanagedType1;
      safeArraySubType = (VarEnum) safeArraySubType1;
      arraySubType = (UnmanagedType) arraySubType1;
    }

    internal static void ThrowError(int hResult)
    {
      throw new MetadataException(hResult);
    }

    internal MetadataImport(IntPtr metadataImport2, object keepalive)
    {
      this.m_metadataImport2 = metadataImport2;
      this.m_keepalive = keepalive;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _Enum(IntPtr scope, int type, int parent, out MetadataEnumResult result);

    [SecurityCritical]
    public void Enum(MetadataTokenType type, int parent, out MetadataEnumResult result)
    {
      MetadataImport._Enum(this.m_metadataImport2, (int) type, parent, out result);
    }

    [SecurityCritical]
    public void EnumNestedTypes(int mdTypeDef, out MetadataEnumResult result)
    {
      this.Enum(MetadataTokenType.TypeDef, mdTypeDef, out result);
    }

    [SecurityCritical]
    public void EnumCustomAttributes(int mdToken, out MetadataEnumResult result)
    {
      this.Enum(MetadataTokenType.CustomAttribute, mdToken, out result);
    }

    [SecurityCritical]
    public void EnumParams(int mdMethodDef, out MetadataEnumResult result)
    {
      this.Enum(MetadataTokenType.ParamDef, mdMethodDef, out result);
    }

    [SecurityCritical]
    public void EnumFields(int mdTypeDef, out MetadataEnumResult result)
    {
      this.Enum(MetadataTokenType.FieldDef, mdTypeDef, out result);
    }

    [SecurityCritical]
    public void EnumProperties(int mdTypeDef, out MetadataEnumResult result)
    {
      this.Enum(MetadataTokenType.Property, mdTypeDef, out result);
    }

    [SecurityCritical]
    public void EnumEvents(int mdTypeDef, out MetadataEnumResult result)
    {
      this.Enum(MetadataTokenType.Event, mdTypeDef, out result);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern string _GetDefaultValue(IntPtr scope, int mdToken, out long value, out int length, out int corElementType);

    [SecurityCritical]
    public string GetDefaultValue(int mdToken, out long value, out int length, out CorElementType corElementType)
    {
      int corElementType1;
      string defaultValue = MetadataImport._GetDefaultValue(this.m_metadataImport2, mdToken, out value, out length, out corElementType1);
      corElementType = (CorElementType) corElementType1;
      return defaultValue;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe void _GetUserString(IntPtr scope, int mdToken, void** name, out int length);

    [SecurityCritical]
    public unsafe string GetUserString(int mdToken)
    {
      void* voidPtr;
      int length;
      MetadataImport._GetUserString(this.m_metadataImport2, mdToken, &voidPtr, out length);
      if ((IntPtr) voidPtr == IntPtr.Zero)
        return (string) null;
      char[] chArray = new char[length];
      for (int index = 0; index < length; ++index)
        chArray[index] = (char) *(ushort*) ((IntPtr) voidPtr + (IntPtr) index * 2);
      return new string(chArray);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe void _GetName(IntPtr scope, int mdToken, void** name);

    [SecurityCritical]
    public unsafe Utf8String GetName(int mdToken)
    {
      void* pStringHeap;
      MetadataImport._GetName(this.m_metadataImport2, mdToken, &pStringHeap);
      return new Utf8String(pStringHeap);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe void _GetNamespace(IntPtr scope, int mdToken, void** namesp);

    [SecurityCritical]
    public unsafe Utf8String GetNamespace(int mdToken)
    {
      void* pStringHeap;
      MetadataImport._GetNamespace(this.m_metadataImport2, mdToken, &pStringHeap);
      return new Utf8String(pStringHeap);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe void _GetEventProps(IntPtr scope, int mdToken, void** name, out int eventAttributes);

    [SecurityCritical]
    public unsafe void GetEventProps(int mdToken, out void* name, out EventAttributes eventAttributes)
    {
      void* voidPtr;
      int eventAttributes1;
      MetadataImport._GetEventProps(this.m_metadataImport2, mdToken, &voidPtr, out eventAttributes1);
      name = voidPtr;
      eventAttributes = (EventAttributes) eventAttributes1;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _GetFieldDefProps(IntPtr scope, int mdToken, out int fieldAttributes);

    [SecurityCritical]
    public void GetFieldDefProps(int mdToken, out FieldAttributes fieldAttributes)
    {
      int fieldAttributes1;
      MetadataImport._GetFieldDefProps(this.m_metadataImport2, mdToken, out fieldAttributes1);
      fieldAttributes = (FieldAttributes) fieldAttributes1;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe void _GetPropertyProps(IntPtr scope, int mdToken, void** name, out int propertyAttributes, out ConstArray signature);

    [SecurityCritical]
    public unsafe void GetPropertyProps(int mdToken, out void* name, out PropertyAttributes propertyAttributes, out ConstArray signature)
    {
      void* voidPtr;
      int propertyAttributes1;
      MetadataImport._GetPropertyProps(this.m_metadataImport2, mdToken, &voidPtr, out propertyAttributes1, out signature);
      name = voidPtr;
      propertyAttributes = (PropertyAttributes) propertyAttributes1;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _GetParentToken(IntPtr scope, int mdToken, out int tkParent);

    [SecurityCritical]
    public int GetParentToken(int tkToken)
    {
      int tkParent;
      MetadataImport._GetParentToken(this.m_metadataImport2, tkToken, out tkParent);
      return tkParent;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _GetParamDefProps(IntPtr scope, int parameterToken, out int sequence, out int attributes);

    [SecurityCritical]
    public void GetParamDefProps(int parameterToken, out int sequence, out ParameterAttributes attributes)
    {
      int attributes1;
      MetadataImport._GetParamDefProps(this.m_metadataImport2, parameterToken, out sequence, out attributes1);
      attributes = (ParameterAttributes) attributes1;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _GetGenericParamProps(IntPtr scope, int genericParameter, out int flags);

    [SecurityCritical]
    public void GetGenericParamProps(int genericParameter, out GenericParameterAttributes attributes)
    {
      int flags;
      MetadataImport._GetGenericParamProps(this.m_metadataImport2, genericParameter, out flags);
      attributes = (GenericParameterAttributes) flags;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _GetScopeProps(IntPtr scope, out Guid mvid);

    [SecurityCritical]
    public void GetScopeProps(out Guid mvid)
    {
      MetadataImport._GetScopeProps(this.m_metadataImport2, out mvid);
    }

    [SecurityCritical]
    public ConstArray GetMethodSignature(MetadataToken token)
    {
      if (token.IsMemberRef)
        return this.GetMemberRefProps((int) token);
      return this.GetSigOfMethodDef((int) token);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _GetSigOfMethodDef(IntPtr scope, int methodToken, ref ConstArray signature);

    [SecurityCritical]
    public ConstArray GetSigOfMethodDef(int methodToken)
    {
      ConstArray signature = new ConstArray();
      MetadataImport._GetSigOfMethodDef(this.m_metadataImport2, methodToken, ref signature);
      return signature;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _GetSignatureFromToken(IntPtr scope, int methodToken, ref ConstArray signature);

    [SecurityCritical]
    public ConstArray GetSignatureFromToken(int token)
    {
      ConstArray signature = new ConstArray();
      MetadataImport._GetSignatureFromToken(this.m_metadataImport2, token, ref signature);
      return signature;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _GetMemberRefProps(IntPtr scope, int memberTokenRef, out ConstArray signature);

    [SecurityCritical]
    public ConstArray GetMemberRefProps(int memberTokenRef)
    {
      ConstArray signature = new ConstArray();
      MetadataImport._GetMemberRefProps(this.m_metadataImport2, memberTokenRef, out signature);
      return signature;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _GetCustomAttributeProps(IntPtr scope, int customAttributeToken, out int constructorToken, out ConstArray signature);

    [SecurityCritical]
    public void GetCustomAttributeProps(int customAttributeToken, out int constructorToken, out ConstArray signature)
    {
      MetadataImport._GetCustomAttributeProps(this.m_metadataImport2, customAttributeToken, out constructorToken, out signature);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _GetClassLayout(IntPtr scope, int typeTokenDef, out int packSize, out int classSize);

    [SecurityCritical]
    public void GetClassLayout(int typeTokenDef, out int packSize, out int classSize)
    {
      MetadataImport._GetClassLayout(this.m_metadataImport2, typeTokenDef, out packSize, out classSize);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool _GetFieldOffset(IntPtr scope, int typeTokenDef, int fieldTokenDef, out int offset);

    [SecurityCritical]
    public bool GetFieldOffset(int typeTokenDef, int fieldTokenDef, out int offset)
    {
      return MetadataImport._GetFieldOffset(this.m_metadataImport2, typeTokenDef, fieldTokenDef, out offset);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _GetSigOfFieldDef(IntPtr scope, int fieldToken, ref ConstArray fieldMarshal);

    [SecurityCritical]
    public ConstArray GetSigOfFieldDef(int fieldToken)
    {
      ConstArray fieldMarshal = new ConstArray();
      MetadataImport._GetSigOfFieldDef(this.m_metadataImport2, fieldToken, ref fieldMarshal);
      return fieldMarshal;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void _GetFieldMarshal(IntPtr scope, int fieldToken, ref ConstArray fieldMarshal);

    [SecurityCritical]
    public ConstArray GetFieldMarshal(int fieldToken)
    {
      ConstArray fieldMarshal = new ConstArray();
      MetadataImport._GetFieldMarshal(this.m_metadataImport2, fieldToken, ref fieldMarshal);
      return fieldMarshal;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe void _GetPInvokeMap(IntPtr scope, int token, out int attributes, void** importName, void** importDll);

    [SecurityCritical]
    public unsafe void GetPInvokeMap(int token, out PInvokeAttributes attributes, out string importName, out string importDll)
    {
      int attributes1;
      void* pStringHeap1;
      void* pStringHeap2;
      MetadataImport._GetPInvokeMap(this.m_metadataImport2, token, out attributes1, &pStringHeap1, &pStringHeap2);
      importName = new Utf8String(pStringHeap1).ToString();
      importDll = new Utf8String(pStringHeap2).ToString();
      attributes = (PInvokeAttributes) attributes1;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool _IsValidToken(IntPtr scope, int token);

    [SecurityCritical]
    public bool IsValidToken(int token)
    {
      return MetadataImport._IsValidToken(this.m_metadataImport2, token);
    }
  }
}

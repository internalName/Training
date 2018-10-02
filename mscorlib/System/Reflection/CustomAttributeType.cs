// Decompiled with JetBrains decompiler
// Type: System.Reflection.CustomAttributeType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection
{
  [Serializable]
  [StructLayout(LayoutKind.Auto)]
  internal struct CustomAttributeType
  {
    private string m_enumName;
    private CustomAttributeEncoding m_encodedType;
    private CustomAttributeEncoding m_encodedEnumType;
    private CustomAttributeEncoding m_encodedArrayType;
    private CustomAttributeEncoding m_padding;

    public CustomAttributeType(CustomAttributeEncoding encodedType, CustomAttributeEncoding encodedArrayType, CustomAttributeEncoding encodedEnumType, string enumName)
    {
      this.m_encodedType = encodedType;
      this.m_encodedArrayType = encodedArrayType;
      this.m_encodedEnumType = encodedEnumType;
      this.m_enumName = enumName;
      this.m_padding = this.m_encodedType;
    }

    public CustomAttributeEncoding EncodedType
    {
      get
      {
        return this.m_encodedType;
      }
    }

    public CustomAttributeEncoding EncodedEnumType
    {
      get
      {
        return this.m_encodedEnumType;
      }
    }

    public CustomAttributeEncoding EncodedArrayType
    {
      get
      {
        return this.m_encodedArrayType;
      }
    }

    [ComVisible(true)]
    public string EnumName
    {
      get
      {
        return this.m_enumName;
      }
    }
  }
}

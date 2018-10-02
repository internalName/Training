// Decompiled with JetBrains decompiler
// Type: System.Reflection.MetadataToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;

namespace System.Reflection
{
  [Serializable]
  internal struct MetadataToken
  {
    public int Value;

    public static implicit operator int(MetadataToken token)
    {
      return token.Value;
    }

    public static implicit operator MetadataToken(int token)
    {
      return new MetadataToken(token);
    }

    public static bool IsTokenOfType(int token, params MetadataTokenType[] types)
    {
      for (int index = 0; index < types.Length; ++index)
      {
        if ((MetadataTokenType) ((long) token & 4278190080L) == types[index])
          return true;
      }
      return false;
    }

    public static bool IsNullToken(int token)
    {
      return (token & 16777215) == 0;
    }

    public MetadataToken(int token)
    {
      this.Value = token;
    }

    public bool IsGlobalTypeDefToken
    {
      get
      {
        return this.Value == 33554433;
      }
    }

    public MetadataTokenType TokenType
    {
      get
      {
        return (MetadataTokenType) ((long) this.Value & 4278190080L);
      }
    }

    public bool IsTypeRef
    {
      get
      {
        return this.TokenType == MetadataTokenType.TypeRef;
      }
    }

    public bool IsTypeDef
    {
      get
      {
        return this.TokenType == MetadataTokenType.TypeDef;
      }
    }

    public bool IsFieldDef
    {
      get
      {
        return this.TokenType == MetadataTokenType.FieldDef;
      }
    }

    public bool IsMethodDef
    {
      get
      {
        return this.TokenType == MetadataTokenType.MethodDef;
      }
    }

    public bool IsMemberRef
    {
      get
      {
        return this.TokenType == MetadataTokenType.MemberRef;
      }
    }

    public bool IsEvent
    {
      get
      {
        return this.TokenType == MetadataTokenType.Event;
      }
    }

    public bool IsProperty
    {
      get
      {
        return this.TokenType == MetadataTokenType.Property;
      }
    }

    public bool IsParamDef
    {
      get
      {
        return this.TokenType == MetadataTokenType.ParamDef;
      }
    }

    public bool IsTypeSpec
    {
      get
      {
        return this.TokenType == MetadataTokenType.TypeSpec;
      }
    }

    public bool IsMethodSpec
    {
      get
      {
        return this.TokenType == MetadataTokenType.MethodSpec;
      }
    }

    public bool IsString
    {
      get
      {
        return this.TokenType == MetadataTokenType.String;
      }
    }

    public bool IsSignature
    {
      get
      {
        return this.TokenType == MetadataTokenType.Signature;
      }
    }

    public bool IsModule
    {
      get
      {
        return this.TokenType == MetadataTokenType.Module;
      }
    }

    public bool IsAssembly
    {
      get
      {
        return this.TokenType == MetadataTokenType.Assembly;
      }
    }

    public bool IsGenericPar
    {
      get
      {
        return this.TokenType == MetadataTokenType.GenericPar;
      }
    }

    public override string ToString()
    {
      return string.Format((IFormatProvider) CultureInfo.InvariantCulture, "0x{0:x8}", (object) this.Value);
    }
  }
}

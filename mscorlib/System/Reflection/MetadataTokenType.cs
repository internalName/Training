// Decompiled with JetBrains decompiler
// Type: System.Reflection.MetadataTokenType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  [Serializable]
  internal enum MetadataTokenType
  {
    Module = 0,
    TypeRef = 16777216, // 0x01000000
    TypeDef = 33554432, // 0x02000000
    FieldDef = 67108864, // 0x04000000
    MethodDef = 100663296, // 0x06000000
    ParamDef = 134217728, // 0x08000000
    InterfaceImpl = 150994944, // 0x09000000
    MemberRef = 167772160, // 0x0A000000
    CustomAttribute = 201326592, // 0x0C000000
    Permission = 234881024, // 0x0E000000
    Signature = 285212672, // 0x11000000
    Event = 335544320, // 0x14000000
    Property = 385875968, // 0x17000000
    ModuleRef = 436207616, // 0x1A000000
    TypeSpec = 452984832, // 0x1B000000
    Assembly = 536870912, // 0x20000000
    AssemblyRef = 587202560, // 0x23000000
    File = 637534208, // 0x26000000
    ExportedType = 654311424, // 0x27000000
    ManifestResource = 671088640, // 0x28000000
    GenericPar = 704643072, // 0x2A000000
    MethodSpec = 721420288, // 0x2B000000
    String = 1879048192, // 0x70000000
    Name = 1895825408, // 0x71000000
    BaseType = 1912602624, // 0x72000000
    Invalid = 2147483647, // 0x7FFFFFFF
  }
}

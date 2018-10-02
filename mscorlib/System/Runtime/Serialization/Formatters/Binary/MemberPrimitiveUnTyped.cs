// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.MemberPrimitiveUnTyped
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class MemberPrimitiveUnTyped : IStreamable
  {
    internal InternalPrimitiveTypeE typeInformation;
    internal object value;

    internal MemberPrimitiveUnTyped()
    {
    }

    internal void Set(InternalPrimitiveTypeE typeInformation, object value)
    {
      this.typeInformation = typeInformation;
      this.value = value;
    }

    internal void Set(InternalPrimitiveTypeE typeInformation)
    {
      this.typeInformation = typeInformation;
    }

    public void Write(__BinaryWriter sout)
    {
      sout.WriteValue(this.typeInformation, this.value);
    }

    [SecurityCritical]
    public void Read(__BinaryParser input)
    {
      this.value = input.ReadValue(this.typeInformation);
    }

    public void Dump()
    {
    }

    [Conditional("_LOGGING")]
    private void DumpInternal()
    {
      if (!BCLDebug.CheckEnabled("BINARY"))
        return;
      Converter.ToComType(this.typeInformation);
    }
  }
}

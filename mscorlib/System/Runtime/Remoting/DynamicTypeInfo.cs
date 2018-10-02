// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.DynamicTypeInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Runtime.Remoting
{
  [Serializable]
  internal class DynamicTypeInfo : TypeInfo
  {
    [SecurityCritical]
    internal DynamicTypeInfo(RuntimeType typeOfObj)
      : base(typeOfObj)
    {
    }

    [SecurityCritical]
    public override bool CanCastTo(Type castType, object o)
    {
      return ((MarshalByRefObject) o).IsInstanceOfType(castType);
    }
  }
}

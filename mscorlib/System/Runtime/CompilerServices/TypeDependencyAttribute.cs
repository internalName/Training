// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.TypeDependencyAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
  internal sealed class TypeDependencyAttribute : Attribute
  {
    private string typeName;

    public TypeDependencyAttribute(string typeName)
    {
      if (typeName == null)
        throw new ArgumentNullException(nameof (typeName));
      this.typeName = typeName;
    }
  }
}

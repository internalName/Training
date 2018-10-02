// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.InternalAssemblyBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  internal sealed class InternalAssemblyBuilder : RuntimeAssembly
  {
    private InternalAssemblyBuilder()
    {
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if (obj is InternalAssemblyBuilder)
        return this == obj;
      return obj.Equals((object) this);
    }

    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    public override string[] GetManifestResourceNames()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
    }

    public override FileStream GetFile(string name)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
    }

    public override FileStream[] GetFiles(bool getResourceModules)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
    }

    public override Stream GetManifestResourceStream(Type type, string name)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
    }

    public override Stream GetManifestResourceStream(string name)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
    }

    public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
    }

    public override string Location
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
      }
    }

    public override string CodeBase
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
      }
    }

    public override Type[] GetExportedTypes()
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
    }

    public override string ImageRuntimeVersion
    {
      get
      {
        return RuntimeEnvironment.GetSystemVersion();
      }
    }
  }
}

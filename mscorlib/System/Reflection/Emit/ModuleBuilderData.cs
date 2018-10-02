// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.ModuleBuilderData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Security;

namespace System.Reflection.Emit
{
  [Serializable]
  internal class ModuleBuilderData
  {
    internal string m_strModuleName;
    internal string m_strFileName;
    internal bool m_fGlobalBeenCreated;
    internal bool m_fHasGlobal;
    [NonSerialized]
    internal TypeBuilder m_globalTypeBuilder;
    [NonSerialized]
    internal ModuleBuilder m_module;
    private int m_tkFile;
    internal bool m_isSaved;
    [NonSerialized]
    internal ResWriterData m_embeddedRes;
    internal const string MULTI_BYTE_VALUE_CLASS = "$ArrayType$";
    internal string m_strResourceFileName;
    internal byte[] m_resourceBytes;

    [SecurityCritical]
    internal ModuleBuilderData(ModuleBuilder module, string strModuleName, string strFileName, int tkFile)
    {
      this.m_globalTypeBuilder = new TypeBuilder(module);
      this.m_module = module;
      this.m_tkFile = tkFile;
      this.InitNames(strModuleName, strFileName);
    }

    [SecurityCritical]
    private void InitNames(string strModuleName, string strFileName)
    {
      this.m_strModuleName = strModuleName;
      if (strFileName == null)
      {
        this.m_strFileName = strModuleName;
      }
      else
      {
        string extension = Path.GetExtension(strFileName);
        if (extension == null || extension == string.Empty)
          throw new ArgumentException(Environment.GetResourceString("Argument_NoModuleFileExtension", (object) strFileName));
        this.m_strFileName = strFileName;
      }
    }

    [SecurityCritical]
    internal virtual void ModifyModuleName(string strModuleName)
    {
      this.InitNames(strModuleName, (string) null);
    }

    internal int FileToken
    {
      get
      {
        return this.m_tkFile;
      }
      set
      {
        this.m_tkFile = value;
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.ResWriterData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Resources;

namespace System.Reflection.Emit
{
  internal class ResWriterData
  {
    internal ResourceWriter m_resWriter;
    internal string m_strName;
    internal string m_strFileName;
    internal string m_strFullFileName;
    internal Stream m_memoryStream;
    internal ResWriterData m_nextResWriter;
    internal ResourceAttributes m_attribute;

    internal ResWriterData(ResourceWriter resWriter, Stream memoryStream, string strName, string strFileName, string strFullFileName, ResourceAttributes attribute)
    {
      this.m_resWriter = resWriter;
      this.m_memoryStream = memoryStream;
      this.m_strName = strName;
      this.m_strFileName = strFileName;
      this.m_strFullFileName = strFullFileName;
      this.m_nextResWriter = (ResWriterData) null;
      this.m_attribute = attribute;
    }
  }
}

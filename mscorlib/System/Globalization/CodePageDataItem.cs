// Decompiled with JetBrains decompiler
// Type: System.Globalization.CodePageDataItem
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Security;

namespace System.Globalization
{
  [Serializable]
  internal class CodePageDataItem
  {
    internal int m_dataIndex;
    internal int m_uiFamilyCodePage;
    internal string m_webName;
    internal string m_headerName;
    internal string m_bodyName;
    internal uint m_flags;

    [SecurityCritical]
    internal unsafe CodePageDataItem(int dataIndex)
    {
      this.m_dataIndex = dataIndex;
      this.m_uiFamilyCodePage = (int) EncodingTable.codePageDataPtr[dataIndex].uiFamilyCodePage;
      this.m_flags = EncodingTable.codePageDataPtr[dataIndex].flags;
    }

    [SecurityCritical]
    internal static unsafe string CreateString(sbyte* pStrings, uint index)
    {
      if (*pStrings == (sbyte) 124)
      {
        int startIndex = 1;
        int index1 = 1;
        while (true)
        {
          sbyte num = pStrings[index1];
          switch (num)
          {
            case 0:
            case 124:
              if (index != 0U)
              {
                --index;
                startIndex = index1 + 1;
                if (num == (sbyte) 0)
                  goto label_7;
                else
                  break;
              }
              else
                goto label_4;
          }
          ++index1;
        }
label_4:
        return new string(pStrings, startIndex, index1 - startIndex);
label_7:
        throw new ArgumentException(nameof (pStrings));
      }
      return new string(pStrings);
    }

    public unsafe string WebName
    {
      [SecuritySafeCritical] get
      {
        if (this.m_webName == null)
          this.m_webName = CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[this.m_dataIndex].Names, 0U);
        return this.m_webName;
      }
    }

    public virtual int UIFamilyCodePage
    {
      get
      {
        return this.m_uiFamilyCodePage;
      }
    }

    public unsafe string HeaderName
    {
      [SecuritySafeCritical] get
      {
        if (this.m_headerName == null)
          this.m_headerName = CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[this.m_dataIndex].Names, 1U);
        return this.m_headerName;
      }
    }

    public unsafe string BodyName
    {
      [SecuritySafeCritical] get
      {
        if (this.m_bodyName == null)
          this.m_bodyName = CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[this.m_dataIndex].Names, 2U);
        return this.m_bodyName;
      }
    }

    public uint Flags
    {
      get
      {
        return this.m_flags;
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Security.SecurityDocument
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Security.Util;
using System.Text;

namespace System.Security
{
  [Serializable]
  internal sealed class SecurityDocument
  {
    internal byte[] m_data;
    internal const byte c_element = 1;
    internal const byte c_attribute = 2;
    internal const byte c_text = 3;
    internal const byte c_children = 4;
    internal const int c_growthSize = 32;

    public SecurityDocument(int numData)
    {
      this.m_data = new byte[numData];
    }

    public SecurityDocument(byte[] data)
    {
      this.m_data = data;
    }

    public SecurityDocument(SecurityElement elRoot)
    {
      this.m_data = new byte[32];
      int position = 0;
      this.ConvertElement(elRoot, ref position);
    }

    public void GuaranteeSize(int size)
    {
      if (this.m_data.Length >= size)
        return;
      byte[] numArray = new byte[(size / 32 + 1) * 32];
      Array.Copy((Array) this.m_data, 0, (Array) numArray, 0, this.m_data.Length);
      this.m_data = numArray;
    }

    public void AddString(string str, ref int position)
    {
      this.GuaranteeSize(position + str.Length * 2 + 2);
      for (int index = 0; index < str.Length; ++index)
      {
        this.m_data[position + 2 * index] = (byte) ((uint) str[index] >> 8);
        this.m_data[position + 2 * index + 1] = (byte) ((uint) str[index] & (uint) byte.MaxValue);
      }
      this.m_data[position + str.Length * 2] = (byte) 0;
      this.m_data[position + str.Length * 2 + 1] = (byte) 0;
      position += str.Length * 2 + 2;
    }

    public void AppendString(string str, ref int position)
    {
      if (position <= 1 || this.m_data[position - 1] != (byte) 0 || this.m_data[position - 2] != (byte) 0)
        throw new XmlSyntaxException();
      position -= 2;
      this.AddString(str, ref position);
    }

    public static int EncodedStringSize(string str)
    {
      return str.Length * 2 + 2;
    }

    public string GetString(ref int position)
    {
      return this.GetString(ref position, true);
    }

    public string GetString(ref int position, bool bCreate)
    {
      bool flag = false;
      int index1 = position;
      while (index1 < this.m_data.Length - 1)
      {
        if (this.m_data[index1] == (byte) 0 && this.m_data[index1 + 1] == (byte) 0)
        {
          flag = true;
          break;
        }
        index1 += 2;
      }
      Tokenizer.StringMaker sharedStringMaker = SharedStatics.GetSharedStringMaker();
      try
      {
        if (bCreate)
        {
          sharedStringMaker._outStringBuilder = (StringBuilder) null;
          sharedStringMaker._outIndex = 0;
          int index2 = position;
          while (index2 < index1)
          {
            char ch = (char) ((uint) this.m_data[index2] << 8 | (uint) this.m_data[index2 + 1]);
            if (sharedStringMaker._outIndex < 512)
            {
              sharedStringMaker._outChars[sharedStringMaker._outIndex++] = ch;
            }
            else
            {
              if (sharedStringMaker._outStringBuilder == null)
                sharedStringMaker._outStringBuilder = new StringBuilder();
              sharedStringMaker._outStringBuilder.Append(sharedStringMaker._outChars, 0, 512);
              sharedStringMaker._outChars[0] = ch;
              sharedStringMaker._outIndex = 1;
            }
            index2 += 2;
          }
        }
        position = index1 + 2;
        if (bCreate)
          return sharedStringMaker.MakeString();
        return (string) null;
      }
      finally
      {
        SharedStatics.ReleaseSharedStringMaker(ref sharedStringMaker);
      }
    }

    public void AddToken(byte b, ref int position)
    {
      this.GuaranteeSize(position + 1);
      this.m_data[position++] = b;
    }

    public void ConvertElement(SecurityElement elCurrent, ref int position)
    {
      this.AddToken((byte) 1, ref position);
      this.AddString(elCurrent.m_strTag, ref position);
      if (elCurrent.m_lAttributes != null)
      {
        int index = 0;
        while (index < elCurrent.m_lAttributes.Count)
        {
          this.AddToken((byte) 2, ref position);
          this.AddString((string) elCurrent.m_lAttributes[index], ref position);
          this.AddString((string) elCurrent.m_lAttributes[index + 1], ref position);
          index += 2;
        }
      }
      if (elCurrent.m_strText != null)
      {
        this.AddToken((byte) 3, ref position);
        this.AddString(elCurrent.m_strText, ref position);
      }
      if (elCurrent.InternalChildren != null)
      {
        for (int index = 0; index < elCurrent.InternalChildren.Count; ++index)
          this.ConvertElement((SecurityElement) elCurrent.Children[index], ref position);
      }
      this.AddToken((byte) 4, ref position);
    }

    public SecurityElement GetRootElement()
    {
      return this.GetElement(0, true);
    }

    public SecurityElement GetElement(int position, bool bCreate)
    {
      return this.InternalGetElement(ref position, bCreate);
    }

    internal SecurityElement InternalGetElement(ref int position, bool bCreate)
    {
      if (this.m_data.Length <= position)
        throw new XmlSyntaxException();
      if (this.m_data[position++] != (byte) 1)
        throw new XmlSyntaxException();
      SecurityElement securityElement = (SecurityElement) null;
      string tag = this.GetString(ref position, bCreate);
      if (bCreate)
        securityElement = new SecurityElement(tag);
      while (this.m_data[position] == (byte) 2)
      {
        ++position;
        string name = this.GetString(ref position, bCreate);
        string str = this.GetString(ref position, bCreate);
        if (bCreate)
          securityElement.AddAttribute(name, str);
      }
      if (this.m_data[position] == (byte) 3)
      {
        ++position;
        string str = this.GetString(ref position, bCreate);
        if (bCreate)
          securityElement.m_strText = str;
      }
      while (this.m_data[position] != (byte) 4)
      {
        SecurityElement element = this.InternalGetElement(ref position, bCreate);
        if (bCreate)
          securityElement.AddChild(element);
      }
      ++position;
      return securityElement;
    }

    public string GetTagForElement(int position)
    {
      if (this.m_data.Length <= position)
        throw new XmlSyntaxException();
      if (this.m_data[position++] != (byte) 1)
        throw new XmlSyntaxException();
      return this.GetString(ref position);
    }

    public ArrayList GetChildrenPositionForElement(int position)
    {
      if (this.m_data.Length <= position)
        throw new XmlSyntaxException();
      if (this.m_data[position++] != (byte) 1)
        throw new XmlSyntaxException();
      ArrayList arrayList = new ArrayList();
      this.GetString(ref position);
      while (this.m_data[position] == (byte) 2)
      {
        ++position;
        this.GetString(ref position, false);
        this.GetString(ref position, false);
      }
      if (this.m_data[position] == (byte) 3)
      {
        ++position;
        this.GetString(ref position, false);
      }
      while (this.m_data[position] != (byte) 4)
      {
        arrayList.Add((object) position);
        this.InternalGetElement(ref position, false);
      }
      ++position;
      return arrayList;
    }

    public string GetAttributeForElement(int position, string attributeName)
    {
      if (this.m_data.Length <= position)
        throw new XmlSyntaxException();
      if (this.m_data[position++] != (byte) 1)
        throw new XmlSyntaxException();
      string str1 = (string) null;
      this.GetString(ref position, false);
      while (this.m_data[position] == (byte) 2)
      {
        ++position;
        string a = this.GetString(ref position);
        string str2 = this.GetString(ref position);
        if (string.Equals(a, attributeName))
        {
          str1 = str2;
          break;
        }
      }
      return str1;
    }
  }
}

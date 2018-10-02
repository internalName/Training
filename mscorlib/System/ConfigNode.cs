﻿// Decompiled with JetBrains decompiler
// Type: System.ConfigNode
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;

namespace System
{
  internal class ConfigNode
  {
    private List<ConfigNode> m_children = new List<ConfigNode>(5);
    private List<DictionaryEntry> m_attributes = new List<DictionaryEntry>(5);
    private string m_name;
    private string m_value;
    private ConfigNode m_parent;

    internal ConfigNode(string name, ConfigNode parent)
    {
      this.m_name = name;
      this.m_parent = parent;
    }

    internal string Name
    {
      get
      {
        return this.m_name;
      }
    }

    internal string Value
    {
      get
      {
        return this.m_value;
      }
      set
      {
        this.m_value = value;
      }
    }

    internal ConfigNode Parent
    {
      get
      {
        return this.m_parent;
      }
    }

    internal List<ConfigNode> Children
    {
      get
      {
        return this.m_children;
      }
    }

    internal List<DictionaryEntry> Attributes
    {
      get
      {
        return this.m_attributes;
      }
    }

    internal void AddChild(ConfigNode child)
    {
      child.m_parent = this;
      this.m_children.Add(child);
    }

    internal int AddAttribute(string key, string value)
    {
      this.m_attributes.Add(new DictionaryEntry((object) key, (object) value));
      return this.m_attributes.Count - 1;
    }

    internal void ReplaceAttribute(int index, string key, string value)
    {
      this.m_attributes[index] = new DictionaryEntry((object) key, (object) value);
    }
  }
}

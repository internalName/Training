// Decompiled with JetBrains decompiler
// Type: System.Security.Util.TokenizerStream
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Util
{
  internal sealed class TokenizerStream
  {
    private int m_countTokens;
    private TokenizerShortBlock m_headTokens;
    private TokenizerShortBlock m_lastTokens;
    private TokenizerShortBlock m_currentTokens;
    private int m_indexTokens;
    private TokenizerStringBlock m_headStrings;
    private TokenizerStringBlock m_currentStrings;
    private int m_indexStrings;

    internal TokenizerStream()
    {
      this.m_countTokens = 0;
      this.m_headTokens = new TokenizerShortBlock();
      this.m_headStrings = new TokenizerStringBlock();
      this.Reset();
    }

    internal void AddToken(short token)
    {
      if (this.m_currentTokens.m_block.Length <= this.m_indexTokens)
      {
        this.m_currentTokens.m_next = new TokenizerShortBlock();
        this.m_currentTokens = this.m_currentTokens.m_next;
        this.m_indexTokens = 0;
      }
      ++this.m_countTokens;
      this.m_currentTokens.m_block[this.m_indexTokens++] = token;
    }

    internal void AddString(string str)
    {
      if (this.m_currentStrings.m_block.Length <= this.m_indexStrings)
      {
        this.m_currentStrings.m_next = new TokenizerStringBlock();
        this.m_currentStrings = this.m_currentStrings.m_next;
        this.m_indexStrings = 0;
      }
      this.m_currentStrings.m_block[this.m_indexStrings++] = str;
    }

    internal void Reset()
    {
      this.m_lastTokens = (TokenizerShortBlock) null;
      this.m_currentTokens = this.m_headTokens;
      this.m_currentStrings = this.m_headStrings;
      this.m_indexTokens = 0;
      this.m_indexStrings = 0;
    }

    internal short GetNextFullToken()
    {
      if (this.m_currentTokens.m_block.Length <= this.m_indexTokens)
      {
        this.m_lastTokens = this.m_currentTokens;
        this.m_currentTokens = this.m_currentTokens.m_next;
        this.m_indexTokens = 0;
      }
      return this.m_currentTokens.m_block[this.m_indexTokens++];
    }

    internal short GetNextToken()
    {
      return (short) ((int) this.GetNextFullToken() & (int) byte.MaxValue);
    }

    internal string GetNextString()
    {
      if (this.m_currentStrings.m_block.Length <= this.m_indexStrings)
      {
        this.m_currentStrings = this.m_currentStrings.m_next;
        this.m_indexStrings = 0;
      }
      return this.m_currentStrings.m_block[this.m_indexStrings++];
    }

    internal void ThrowAwayNextString()
    {
      this.GetNextString();
    }

    internal void TagLastToken(short tag)
    {
      if (this.m_indexTokens == 0)
        this.m_lastTokens.m_block[this.m_lastTokens.m_block.Length - 1] = (short) ((int) (ushort) this.m_lastTokens.m_block[this.m_lastTokens.m_block.Length - 1] | (int) (ushort) tag);
      else
        this.m_currentTokens.m_block[this.m_indexTokens - 1] = (short) ((int) (ushort) this.m_currentTokens.m_block[this.m_indexTokens - 1] | (int) (ushort) tag);
    }

    internal int GetTokenCount()
    {
      return this.m_countTokens;
    }

    internal void GoToPosition(int position)
    {
      this.Reset();
      for (int index = 0; index < position; ++index)
      {
        if (this.GetNextToken() == (short) 3)
          this.ThrowAwayNextString();
      }
    }
  }
}

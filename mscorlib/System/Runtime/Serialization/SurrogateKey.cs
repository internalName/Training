// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.SurrogateKey
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Serialization
{
  [Serializable]
  internal class SurrogateKey
  {
    internal Type m_type;
    internal StreamingContext m_context;

    internal SurrogateKey(Type type, StreamingContext context)
    {
      this.m_type = type;
      this.m_context = context;
    }

    public override int GetHashCode()
    {
      return this.m_type.GetHashCode();
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Text.MLangCodePageEncoding
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
  [Serializable]
  internal sealed class MLangCodePageEncoding : ISerializable, IObjectReference
  {
    [NonSerialized]
    private int m_codePage;
    [NonSerialized]
    private bool m_isReadOnly;
    [NonSerialized]
    private bool m_deserializedFromEverett;
    [NonSerialized]
    private EncoderFallback encoderFallback;
    [NonSerialized]
    private DecoderFallback decoderFallback;
    [NonSerialized]
    private Encoding realEncoding;

    internal MLangCodePageEncoding(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      this.m_codePage = (int) info.GetValue(nameof (m_codePage), typeof (int));
      try
      {
        this.m_isReadOnly = (bool) info.GetValue(nameof (m_isReadOnly), typeof (bool));
        this.encoderFallback = (EncoderFallback) info.GetValue(nameof (encoderFallback), typeof (EncoderFallback));
        this.decoderFallback = (DecoderFallback) info.GetValue(nameof (decoderFallback), typeof (DecoderFallback));
      }
      catch (SerializationException ex)
      {
        this.m_deserializedFromEverett = true;
        this.m_isReadOnly = true;
      }
    }

    [SecurityCritical]
    public object GetRealObject(StreamingContext context)
    {
      this.realEncoding = Encoding.GetEncoding(this.m_codePage);
      if (!this.m_deserializedFromEverett && !this.m_isReadOnly)
      {
        this.realEncoding = (Encoding) this.realEncoding.Clone();
        this.realEncoding.EncoderFallback = this.encoderFallback;
        this.realEncoding.DecoderFallback = this.decoderFallback;
      }
      return (object) this.realEncoding;
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
    }

    [Serializable]
    internal sealed class MLangEncoder : ISerializable, IObjectReference
    {
      [NonSerialized]
      private Encoding realEncoding;

      internal MLangEncoder(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException(nameof (info));
        this.realEncoding = (Encoding) info.GetValue("m_encoding", typeof (Encoding));
      }

      [SecurityCritical]
      public object GetRealObject(StreamingContext context)
      {
        return (object) this.realEncoding.GetEncoder();
      }

      [SecurityCritical]
      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
      {
        throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
      }
    }

    [Serializable]
    internal sealed class MLangDecoder : ISerializable, IObjectReference
    {
      [NonSerialized]
      private Encoding realEncoding;

      internal MLangDecoder(SerializationInfo info, StreamingContext context)
      {
        if (info == null)
          throw new ArgumentNullException(nameof (info));
        this.realEncoding = (Encoding) info.GetValue("m_encoding", typeof (Encoding));
      }

      [SecurityCritical]
      public object GetRealObject(StreamingContext context)
      {
        return (object) this.realEncoding.GetDecoder();
      }

      [SecurityCritical]
      void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
      {
        throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
      }
    }
  }
}

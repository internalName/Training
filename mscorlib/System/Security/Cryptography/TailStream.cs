// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.TailStream
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;

namespace System.Security.Cryptography
{
  internal sealed class TailStream : Stream
  {
    private byte[] _Buffer;
    private int _BufferSize;
    private int _BufferIndex;
    private bool _BufferFull;

    public TailStream(int bufferSize)
    {
      this._Buffer = new byte[bufferSize];
      this._BufferSize = bufferSize;
    }

    public void Clear()
    {
      this.Close();
    }

    protected override void Dispose(bool disposing)
    {
      try
      {
        if (!disposing)
          return;
        if (this._Buffer != null)
          Array.Clear((Array) this._Buffer, 0, this._Buffer.Length);
        this._Buffer = (byte[]) null;
      }
      finally
      {
        base.Dispose(disposing);
      }
    }

    public byte[] Buffer
    {
      get
      {
        return (byte[]) this._Buffer.Clone();
      }
    }

    public override bool CanRead
    {
      get
      {
        return false;
      }
    }

    public override bool CanSeek
    {
      get
      {
        return false;
      }
    }

    public override bool CanWrite
    {
      get
      {
        return this._Buffer != null;
      }
    }

    public override long Length
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
      }
    }

    public override long Position
    {
      get
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
      }
      set
      {
        throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
      }
    }

    public override void Flush()
    {
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
    }

    public override void SetLength(long value)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnseekableStream"));
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_UnreadableStream"));
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
      if (this._Buffer == null)
        throw new ObjectDisposedException(nameof (TailStream));
      if (count == 0)
        return;
      if (this._BufferFull)
      {
        if (count > this._BufferSize)
        {
          System.Buffer.InternalBlockCopy((Array) buffer, offset + count - this._BufferSize, (Array) this._Buffer, 0, this._BufferSize);
        }
        else
        {
          System.Buffer.InternalBlockCopy((Array) this._Buffer, this._BufferSize - count, (Array) this._Buffer, 0, this._BufferSize - count);
          System.Buffer.InternalBlockCopy((Array) buffer, offset, (Array) this._Buffer, this._BufferSize - count, count);
        }
      }
      else if (count > this._BufferSize)
      {
        System.Buffer.InternalBlockCopy((Array) buffer, offset + count - this._BufferSize, (Array) this._Buffer, 0, this._BufferSize);
        this._BufferFull = true;
      }
      else if (count + this._BufferIndex >= this._BufferSize)
      {
        System.Buffer.InternalBlockCopy((Array) this._Buffer, this._BufferIndex + count - this._BufferSize, (Array) this._Buffer, 0, this._BufferSize - count);
        System.Buffer.InternalBlockCopy((Array) buffer, offset, (Array) this._Buffer, this._BufferIndex, count);
        this._BufferFull = true;
      }
      else
      {
        System.Buffer.InternalBlockCopy((Array) buffer, offset, (Array) this._Buffer, this._BufferIndex, count);
        this._BufferIndex += count;
      }
    }
  }
}

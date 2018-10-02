// Decompiled with JetBrains decompiler
// Type: System.IO.ReadLinesIterator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Text;

namespace System.IO
{
  internal class ReadLinesIterator : Iterator<string>
  {
    private readonly string _path;
    private readonly Encoding _encoding;
    private StreamReader _reader;

    private ReadLinesIterator(string path, Encoding encoding, StreamReader reader)
    {
      this._path = path;
      this._encoding = encoding;
      this._reader = reader;
    }

    public override bool MoveNext()
    {
      if (this._reader != null)
      {
        this.current = this._reader.ReadLine();
        if (this.current != null)
          return true;
        this.Dispose();
      }
      return false;
    }

    protected override Iterator<string> Clone()
    {
      return (Iterator<string>) ReadLinesIterator.CreateIterator(this._path, this._encoding, this._reader);
    }

    protected override void Dispose(bool disposing)
    {
      try
      {
        if (!disposing || this._reader == null)
          return;
        this._reader.Dispose();
      }
      finally
      {
        this._reader = (StreamReader) null;
        base.Dispose(disposing);
      }
    }

    internal static ReadLinesIterator CreateIterator(string path, Encoding encoding)
    {
      return ReadLinesIterator.CreateIterator(path, encoding, (StreamReader) null);
    }

    private static ReadLinesIterator CreateIterator(string path, Encoding encoding, StreamReader reader)
    {
      return new ReadLinesIterator(path, encoding, reader ?? new StreamReader(path, encoding));
    }
  }
}

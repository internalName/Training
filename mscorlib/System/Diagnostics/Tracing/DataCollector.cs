// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.DataCollector
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Diagnostics.Tracing
{
  [SecurityCritical]
  internal struct DataCollector
  {
    [ThreadStatic]
    internal static DataCollector ThreadInstance;
    private unsafe byte* scratchEnd;
    private unsafe EventSource.EventData* datasEnd;
    private unsafe GCHandle* pinsEnd;
    private unsafe EventSource.EventData* datasStart;
    private unsafe byte* scratch;
    private unsafe EventSource.EventData* datas;
    private unsafe GCHandle* pins;
    private byte[] buffer;
    private int bufferPos;
    private int bufferNesting;
    private bool writingScalars;

    internal unsafe void Enable(byte* scratch, int scratchSize, EventSource.EventData* datas, int dataCount, GCHandle* pins, int pinCount)
    {
      this.datasStart = datas;
      this.scratchEnd = scratch + scratchSize;
      this.datasEnd = datas + dataCount;
      this.pinsEnd = pins + pinCount;
      this.scratch = scratch;
      this.datas = datas;
      this.pins = pins;
      this.writingScalars = false;
    }

    internal void Disable()
    {
      this = new DataCollector();
    }

    internal unsafe EventSource.EventData* Finish()
    {
      this.ScalarsEnd();
      return this.datas;
    }

    internal unsafe void AddScalar(void* value, int size)
    {
      byte* numPtr1 = (byte*) value;
      if (this.bufferNesting == 0)
      {
        byte* scratch = this.scratch;
        byte* numPtr2 = scratch + size;
        if (this.scratchEnd < numPtr2)
          throw new IndexOutOfRangeException(Environment.GetResourceString("EventSource_AddScalarOutOfRange"));
        this.ScalarsBegin();
        this.scratch = numPtr2;
        for (int index = 0; index != size; ++index)
          scratch[index] = numPtr1[index];
      }
      else
      {
        int bufferPos = this.bufferPos;
        checked { this.bufferPos += size; }
        this.EnsureBuffer();
        int index = 0;
        while (index != size)
        {
          this.buffer[bufferPos] = numPtr1[index];
          ++index;
          ++bufferPos;
        }
      }
    }

    internal unsafe void AddBinary(string value, int size)
    {
      if (size > (int) ushort.MaxValue)
        size = 65534;
      if (this.bufferNesting != 0)
        this.EnsureBuffer(size + 2);
      this.AddScalar((void*) &size, 2);
      if (size == 0)
        return;
      if (this.bufferNesting == 0)
      {
        this.ScalarsEnd();
        this.PinArray((object) value, size);
      }
      else
      {
        int bufferPos = this.bufferPos;
        checked { this.bufferPos += size; }
        this.EnsureBuffer();
        string str = value;
        void* voidPtr = (void*) str;
        if ((IntPtr) voidPtr != IntPtr.Zero)
          voidPtr += RuntimeHelpers.OffsetToStringData;
        Marshal.Copy((IntPtr) voidPtr, this.buffer, bufferPos, size);
        str = (string) null;
      }
    }

    internal void AddBinary(Array value, int size)
    {
      this.AddArray(value, size, 1);
    }

    internal unsafe void AddArray(Array value, int length, int itemSize)
    {
      if (length > (int) ushort.MaxValue)
        length = (int) ushort.MaxValue;
      int num = length * itemSize;
      if (this.bufferNesting != 0)
        this.EnsureBuffer(num + 2);
      this.AddScalar((void*) &length, 2);
      if (length == 0)
        return;
      if (this.bufferNesting == 0)
      {
        this.ScalarsEnd();
        this.PinArray((object) value, num);
      }
      else
      {
        int bufferPos = this.bufferPos;
        checked { this.bufferPos += num; }
        this.EnsureBuffer();
        Buffer.BlockCopy(value, 0, (Array) this.buffer, bufferPos, num);
      }
    }

    internal int BeginBufferedArray()
    {
      this.BeginBuffered();
      this.bufferPos += 2;
      return this.bufferPos;
    }

    internal void EndBufferedArray(int bookmark, int count)
    {
      this.EnsureBuffer();
      this.buffer[bookmark - 2] = (byte) count;
      this.buffer[bookmark - 1] = (byte) (count >> 8);
      this.EndBuffered();
    }

    internal void BeginBuffered()
    {
      this.ScalarsEnd();
      ++this.bufferNesting;
    }

    internal void EndBuffered()
    {
      --this.bufferNesting;
      if (this.bufferNesting != 0)
        return;
      this.EnsureBuffer();
      this.PinArray((object) this.buffer, this.bufferPos);
      this.buffer = (byte[]) null;
      this.bufferPos = 0;
    }

    private void EnsureBuffer()
    {
      int bufferPos = this.bufferPos;
      if (this.buffer != null && this.buffer.Length >= bufferPos)
        return;
      this.GrowBuffer(bufferPos);
    }

    private void EnsureBuffer(int additionalSize)
    {
      int required = this.bufferPos + additionalSize;
      if (this.buffer != null && this.buffer.Length >= required)
        return;
      this.GrowBuffer(required);
    }

    private void GrowBuffer(int required)
    {
      int newSize = this.buffer == null ? 64 : this.buffer.Length;
      do
      {
        newSize *= 2;
      }
      while (newSize < required);
      Array.Resize<byte>(ref this.buffer, newSize);
    }

    private unsafe void PinArray(object value, int size)
    {
      GCHandle* pins = this.pins;
      if (this.pinsEnd <= pins)
        throw new IndexOutOfRangeException(Environment.GetResourceString("EventSource_PinArrayOutOfRange"));
      EventSource.EventData* datas = this.datas;
      if (this.datasEnd <= datas)
        throw new IndexOutOfRangeException(Environment.GetResourceString("EventSource_DataDescriptorsOutOfRange"));
      this.pins = pins + 1;
      this.datas = datas + 1;
      *pins = GCHandle.Alloc(value, GCHandleType.Pinned);
      datas->DataPointer = pins->AddrOfPinnedObject();
      datas->m_Size = size;
    }

    private unsafe void ScalarsBegin()
    {
      if (this.writingScalars)
        return;
      EventSource.EventData* datas = this.datas;
      if (this.datasEnd <= datas)
        throw new IndexOutOfRangeException(Environment.GetResourceString("EventSource_DataDescriptorsOutOfRange"));
      datas->DataPointer = (IntPtr) ((void*) this.scratch);
      this.writingScalars = true;
    }

    private unsafe void ScalarsEnd()
    {
      if (!this.writingScalars)
        return;
      EventSource.EventData* datas = this.datas;
      datas->m_Size = checked ((int) unchecked ((long) ((IntPtr) (this.scratch - checked ((UIntPtr) datas->m_Ptr)) / 1)));
      this.datas = datas + 1;
      this.writingScalars = false;
    }
  }
}

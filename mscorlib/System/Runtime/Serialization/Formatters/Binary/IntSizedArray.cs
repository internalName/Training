// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.IntSizedArray
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Serialization.Formatters.Binary
{
  [Serializable]
  internal sealed class IntSizedArray : ICloneable
  {
    internal int[] objects = new int[16];
    internal int[] negObjects = new int[4];

    public IntSizedArray()
    {
    }

    private IntSizedArray(IntSizedArray sizedArray)
    {
      this.objects = new int[sizedArray.objects.Length];
      sizedArray.objects.CopyTo((Array) this.objects, 0);
      this.negObjects = new int[sizedArray.negObjects.Length];
      sizedArray.negObjects.CopyTo((Array) this.negObjects, 0);
    }

    public object Clone()
    {
      return (object) new IntSizedArray(this);
    }

    internal int this[int index]
    {
      get
      {
        if (index < 0)
        {
          if (-index > this.negObjects.Length - 1)
            return 0;
          return this.negObjects[-index];
        }
        if (index > this.objects.Length - 1)
          return 0;
        return this.objects[index];
      }
      set
      {
        if (index < 0)
        {
          if (-index > this.negObjects.Length - 1)
            this.IncreaseCapacity(index);
          this.negObjects[-index] = value;
        }
        else
        {
          if (index > this.objects.Length - 1)
            this.IncreaseCapacity(index);
          this.objects[index] = value;
        }
      }
    }

    internal void IncreaseCapacity(int index)
    {
      try
      {
        if (index < 0)
        {
          int[] numArray = new int[Math.Max(this.negObjects.Length * 2, -index + 1)];
          Array.Copy((Array) this.negObjects, 0, (Array) numArray, 0, this.negObjects.Length);
          this.negObjects = numArray;
        }
        else
        {
          int[] numArray = new int[Math.Max(this.objects.Length * 2, index + 1)];
          Array.Copy((Array) this.objects, 0, (Array) numArray, 0, this.objects.Length);
          this.objects = numArray;
        }
      }
      catch (Exception ex)
      {
        throw new SerializationException(Environment.GetResourceString("Serialization_CorruptedStream"));
      }
    }
  }
}

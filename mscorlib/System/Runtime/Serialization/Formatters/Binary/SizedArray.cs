// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.SizedArray
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Serialization.Formatters.Binary
{
  [Serializable]
  internal sealed class SizedArray : ICloneable
  {
    internal object[] objects;
    internal object[] negObjects;

    internal SizedArray()
    {
      this.objects = new object[16];
      this.negObjects = new object[4];
    }

    internal SizedArray(int length)
    {
      this.objects = new object[length];
      this.negObjects = new object[length];
    }

    private SizedArray(SizedArray sizedArray)
    {
      this.objects = new object[sizedArray.objects.Length];
      sizedArray.objects.CopyTo((Array) this.objects, 0);
      this.negObjects = new object[sizedArray.negObjects.Length];
      sizedArray.negObjects.CopyTo((Array) this.negObjects, 0);
    }

    public object Clone()
    {
      return (object) new SizedArray(this);
    }

    internal object this[int index]
    {
      get
      {
        if (index < 0)
        {
          if (-index > this.negObjects.Length - 1)
            return (object) null;
          return this.negObjects[-index];
        }
        if (index > this.objects.Length - 1)
          return (object) null;
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
          object obj = this.objects[index];
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
          object[] objArray = new object[Math.Max(this.negObjects.Length * 2, -index + 1)];
          Array.Copy((Array) this.negObjects, 0, (Array) objArray, 0, this.negObjects.Length);
          this.negObjects = objArray;
        }
        else
        {
          object[] objArray = new object[Math.Max(this.objects.Length * 2, index + 1)];
          Array.Copy((Array) this.objects, 0, (Array) objArray, 0, this.objects.Length);
          this.objects = objArray;
        }
      }
      catch (Exception ex)
      {
        throw new SerializationException(Environment.GetResourceString("Serialization_CorruptedStream"));
      }
    }
  }
}

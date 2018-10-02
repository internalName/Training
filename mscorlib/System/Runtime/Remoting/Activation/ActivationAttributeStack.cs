// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Activation.ActivationAttributeStack
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Remoting.Activation
{
  internal class ActivationAttributeStack
  {
    private object[] activationTypes;
    private object[] activationAttributes;
    private int freeIndex;

    internal ActivationAttributeStack()
    {
      this.activationTypes = new object[4];
      this.activationAttributes = new object[4];
      this.freeIndex = 0;
    }

    internal void Push(Type typ, object[] attr)
    {
      if (this.freeIndex == this.activationTypes.Length)
      {
        object[] objArray1 = new object[this.activationTypes.Length * 2];
        object[] objArray2 = new object[this.activationAttributes.Length * 2];
        Array.Copy((Array) this.activationTypes, (Array) objArray1, this.activationTypes.Length);
        Array.Copy((Array) this.activationAttributes, (Array) objArray2, this.activationAttributes.Length);
        this.activationTypes = objArray1;
        this.activationAttributes = objArray2;
      }
      this.activationTypes[this.freeIndex] = (object) typ;
      this.activationAttributes[this.freeIndex] = (object) attr;
      ++this.freeIndex;
    }

    internal object[] Peek(Type typ)
    {
      if (this.freeIndex == 0 || this.activationTypes[this.freeIndex - 1] != (object) typ)
        return (object[]) null;
      return (object[]) this.activationAttributes[this.freeIndex - 1];
    }

    internal void Pop(Type typ)
    {
      if (this.freeIndex == 0 || this.activationTypes[this.freeIndex - 1] != (object) typ)
        return;
      --this.freeIndex;
      this.activationTypes[this.freeIndex] = (object) null;
      this.activationAttributes[this.freeIndex] = (object) null;
    }
  }
}

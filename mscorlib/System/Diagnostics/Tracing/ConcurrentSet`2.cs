// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.ConcurrentSet`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Threading;

namespace System.Diagnostics.Tracing
{
  internal struct ConcurrentSet<KeyType, ItemType> where ItemType : ConcurrentSetItem<KeyType, ItemType>
  {
    private ItemType[] items;

    public ItemType TryGet(KeyType key)
    {
      ItemType[] items = this.items;
      ItemType itemType;
      if (items != null)
      {
        int num1 = 0;
        int num2 = items.Length;
        do
        {
          int index = (num1 + num2) / 2;
          itemType = items[index];
          int num3 = itemType.Compare(key);
          if (num3 != 0)
          {
            if (num3 < 0)
              num1 = index + 1;
            else
              num2 = index;
          }
          else
            goto label_8;
        }
        while (num1 != num2);
      }
      itemType = default (ItemType);
label_8:
      return itemType;
    }

    public ItemType GetOrAdd(ItemType newItem)
    {
      ItemType[] comparand = this.items;
      ItemType itemType;
      while (true)
      {
        ItemType[] itemTypeArray1;
        if (comparand == null)
        {
          itemTypeArray1 = new ItemType[1]{ newItem };
        }
        else
        {
          int index1 = 0;
          int num1 = comparand.Length;
          do
          {
            int index2 = (index1 + num1) / 2;
            itemType = comparand[index2];
            int num2 = itemType.Compare(newItem);
            if (num2 != 0)
            {
              if (num2 < 0)
                index1 = index2 + 1;
              else
                num1 = index2;
            }
            else
              goto label_13;
          }
          while (index1 != num1);
          int length = comparand.Length;
          itemTypeArray1 = new ItemType[length + 1];
          Array.Copy((Array) comparand, 0, (Array) itemTypeArray1, 0, index1);
          itemTypeArray1[index1] = newItem;
          Array.Copy((Array) comparand, index1, (Array) itemTypeArray1, index1 + 1, length - index1);
        }
        ItemType[] itemTypeArray2 = Interlocked.CompareExchange<ItemType[]>(ref this.items, itemTypeArray1, comparand);
        if (comparand != itemTypeArray2)
          comparand = itemTypeArray2;
        else
          break;
      }
      itemType = newItem;
label_13:
      return itemType;
    }
  }
}

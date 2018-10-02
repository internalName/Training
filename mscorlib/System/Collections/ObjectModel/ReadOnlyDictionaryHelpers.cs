// Decompiled with JetBrains decompiler
// Type: System.Collections.ObjectModel.ReadOnlyDictionaryHelpers
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;

namespace System.Collections.ObjectModel
{
  internal static class ReadOnlyDictionaryHelpers
  {
    internal static void CopyToNonGenericICollectionHelper<T>(ICollection<T> collection, Array array, int index)
    {
      if (array == null)
        ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
      if (array.Rank != 1)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
      if (array.GetLowerBound(0) != 0)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
      if (index < 0)
        ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.arrayIndex, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
      if (array.Length - index < collection.Count)
        ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
      ICollection collection1 = collection as ICollection;
      if (collection1 != null)
      {
        collection1.CopyTo(array, index);
      }
      else
      {
        T[] array1 = array as T[];
        if (array1 != null)
        {
          collection.CopyTo(array1, index);
        }
        else
        {
          Type elementType = array.GetType().GetElementType();
          Type c = typeof (T);
          if (!elementType.IsAssignableFrom(c) && !c.IsAssignableFrom(elementType))
            ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
          object[] objArray = array as object[];
          if (objArray == null)
            ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
          try
          {
            foreach (T obj in (IEnumerable<T>) collection)
              objArray[index++] = (object) obj;
          }
          catch (ArrayTypeMismatchException ex)
          {
            ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
          }
        }
      }
    }
  }
}

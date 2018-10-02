// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.ObjectIDGenerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>Создает идентификаторы для объектов.</summary>
  [ComVisible(true)]
  [Serializable]
  public class ObjectIDGenerator
  {
    private static readonly int[] sizes = new int[21]
    {
      5,
      11,
      29,
      47,
      97,
      197,
      397,
      797,
      1597,
      3203,
      6421,
      12853,
      25717,
      51437,
      102877,
      205759,
      411527,
      823117,
      1646237,
      3292489,
      6584983
    };
    private const int numbins = 4;
    internal int m_currentCount;
    internal int m_currentSize;
    internal long[] m_ids;
    internal object[] m_objs;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" />.
    /// </summary>
    public ObjectIDGenerator()
    {
      this.m_currentCount = 1;
      this.m_currentSize = ObjectIDGenerator.sizes[0];
      this.m_ids = new long[this.m_currentSize * 4];
      this.m_objs = new object[this.m_currentSize * 4];
    }

    private int FindElement(object obj, out bool found)
    {
      int hashCode = RuntimeHelpers.GetHashCode(obj);
      int num1 = 1 + (hashCode & int.MaxValue) % (this.m_currentSize - 2);
      while (true)
      {
        int num2 = (hashCode & int.MaxValue) % this.m_currentSize * 4;
        for (int index = num2; index < num2 + 4; ++index)
        {
          if (this.m_objs[index] == null)
          {
            found = false;
            return index;
          }
          if (this.m_objs[index] == obj)
          {
            found = true;
            return index;
          }
        }
        hashCode += num1;
      }
    }

    /// <summary>
    ///   Возвращает идентификатор для заданного объекта, создавая новый ID, если указанный объект уже не определена <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" />.
    /// </summary>
    /// <param name="obj">Объект, который требуется идентификатор.</param>
    /// <param name="firstTime">
    ///   <see langword="true" /> Если <paramref name="obj" /> не был ранее известен <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" />; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Идентификатор объекта используется для сериализации.
    ///   <paramref name="firstTime" /> имеет значение <see langword="true" /> Если это первый случай объекта была обнаружена; в противном случае — равным <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" /> Получила запрос для отслеживания слишком много объектов.
    /// </exception>
    public virtual long GetId(object obj, out bool firstTime)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj), Environment.GetResourceString("ArgumentNull_Obj"));
      bool found;
      int element = this.FindElement(obj, out found);
      long id;
      if (!found)
      {
        this.m_objs[element] = obj;
        this.m_ids[element] = (long) this.m_currentCount++;
        id = this.m_ids[element];
        if (this.m_currentCount > this.m_currentSize * 4 / 2)
          this.Rehash();
      }
      else
        id = this.m_ids[element];
      firstTime = !found;
      return id;
    }

    /// <summary>
    ///   Определяет, был ли объекту уже присвоен идентификатор.
    /// </summary>
    /// <param name="obj">Объект, который требует поддержки.</param>
    /// <param name="firstTime">
    ///   <see langword="true" /> Если <paramref name="obj" /> не был ранее известен <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" />; в противном случае — <see langword="false" />.
    /// </param>
    /// <returns>
    ///   Идентификатор объекта <paramref name="obj" /> Если ранее для <see cref="T:System.Runtime.Serialization.ObjectIDGenerator" />; в противном случае — нуль.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="obj" /> имеет значение <see langword="null" />.
    /// </exception>
    public virtual long HasId(object obj, out bool firstTime)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj), Environment.GetResourceString("ArgumentNull_Obj"));
      bool found;
      int element = this.FindElement(obj, out found);
      if (found)
      {
        firstTime = false;
        return this.m_ids[element];
      }
      firstTime = true;
      return 0;
    }

    private void Rehash()
    {
      int index1 = 0;
      int currentSize = this.m_currentSize;
      while (index1 < ObjectIDGenerator.sizes.Length && ObjectIDGenerator.sizes[index1] <= currentSize)
        ++index1;
      if (index1 == ObjectIDGenerator.sizes.Length)
        throw new SerializationException(Environment.GetResourceString("Serialization_TooManyElements"));
      this.m_currentSize = ObjectIDGenerator.sizes[index1];
      long[] numArray = new long[this.m_currentSize * 4];
      object[] objArray = new object[this.m_currentSize * 4];
      long[] ids = this.m_ids;
      object[] objs = this.m_objs;
      this.m_ids = numArray;
      this.m_objs = objArray;
      for (int index2 = 0; index2 < objs.Length; ++index2)
      {
        if (objs[index2] != null)
        {
          bool found;
          int element = this.FindElement(objs[index2], out found);
          this.m_objs[element] = objs[index2];
          this.m_ids[element] = ids[index2];
        }
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Random
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System
{
  /// <summary>
  ///   Представляет генератор псевдослучайных чисел, то есть устройство, которое выдает последовательность чисел, отвечающую определенным статистическим критериям случайности.
  /// 
  ///   Просмотреть исходный код .NET Framework для этого типа Reference Source.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public class Random
  {
    private int[] SeedArray = new int[56];
    private const int MBIG = 2147483647;
    private const int MSEED = 161803398;
    private const int MZ = 0;
    private int inext;
    private int inextp;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Random" /> класс с помощью начальное значение по умолчанию, зависящие от времени.
    /// </summary>
    [__DynamicallyInvokable]
    public Random()
      : this(Environment.TickCount)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Random" /> с помощью указанного начального значения.
    /// </summary>
    /// <param name="Seed">
    ///   Число, используемое для вычисления начального значения последовательности псевдослучайных чисел.
    ///    Если задано отрицательное число, используется его абсолютное значение.
    /// </param>
    [__DynamicallyInvokable]
    public Random(int Seed)
    {
      int num1 = 161803398 - (Seed == int.MinValue ? int.MaxValue : Math.Abs(Seed));
      this.SeedArray[55] = num1;
      int num2 = 1;
      for (int index1 = 1; index1 < 55; ++index1)
      {
        int index2 = 21 * index1 % 55;
        this.SeedArray[index2] = num2;
        num2 = num1 - num2;
        if (num2 < 0)
          num2 += int.MaxValue;
        num1 = this.SeedArray[index2];
      }
      for (int index1 = 1; index1 < 5; ++index1)
      {
        for (int index2 = 1; index2 < 56; ++index2)
        {
          this.SeedArray[index2] -= this.SeedArray[1 + (index2 + 30) % 55];
          if (this.SeedArray[index2] < 0)
            this.SeedArray[index2] += int.MaxValue;
        }
      }
      this.inext = 0;
      this.inextp = 21;
      Seed = 1;
    }

    /// <summary>
    ///   Возвращает случайное число с плавающей запятой в диапазоне от 0,0 до 1,0.
    /// </summary>
    /// <returns>
    ///   Число двойной точности с плавающей запятой, которое больше или равно 0,0, и меньше 1,0.
    /// </returns>
    [__DynamicallyInvokable]
    protected virtual double Sample()
    {
      return (double) this.InternalSample() * 4.6566128752458E-10;
    }

    private int InternalSample()
    {
      int inext = this.inext;
      int inextp = this.inextp;
      int index1;
      if ((index1 = inext + 1) >= 56)
        index1 = 1;
      int index2;
      if ((index2 = inextp + 1) >= 56)
        index2 = 1;
      int num = this.SeedArray[index1] - this.SeedArray[index2];
      if (num == int.MaxValue)
        --num;
      if (num < 0)
        num += int.MaxValue;
      this.SeedArray[index1] = num;
      this.inext = index1;
      this.inextp = index2;
      return num;
    }

    /// <summary>Возвращает неотрицательное случайное целое число.</summary>
    /// <returns>
    ///   32-разрядное целое число со знаком, которое больше или равно 0 и меньше чем <see cref="F:System.Int32.MaxValue" />.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual int Next()
    {
      return this.InternalSample();
    }

    private double GetSampleForLargeRange()
    {
      int num = this.InternalSample();
      if (this.InternalSample() % 2 == 0)
        num = -num;
      return ((double) num + 2147483646.0) / 4294967293.0;
    }

    /// <summary>
    ///   Возвращает случайное целое число в указанном диапазоне.
    /// </summary>
    /// <param name="minValue">
    ///   Возвращается включенной нижний предел создаваемого случайного числа.
    /// </param>
    /// <param name="maxValue">
    ///   Возвращается Исключенный верхний предел создаваемого случайного числа.
    ///   <paramref name="maxValue" />должно быть больше или равно <paramref name="minValue" />.
    /// </param>
    /// <returns>
    ///   32-разрядное знаковое целое число больше или равно <paramref name="minValue" /> и меньше, чем <paramref name="maxValue" />; диапазон возвращаемого значения включает <paramref name="minValue" /> , но не <paramref name="maxValue" />.
    ///    Если <paramref name="minValue" /> равняется <paramref name="maxValue" />, <paramref name="minValue" /> возвращается.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение <paramref name="minValue" /> больше значения <paramref name="maxValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int Next(int minValue, int maxValue)
    {
      if (minValue > maxValue)
        throw new ArgumentOutOfRangeException(nameof (minValue), Environment.GetResourceString("Argument_MinMaxValue", (object) nameof (minValue), (object) nameof (maxValue)));
      long num = (long) maxValue - (long) minValue;
      if (num <= (long) int.MaxValue)
        return (int) (this.Sample() * (double) num) + minValue;
      return (int) ((long) (this.GetSampleForLargeRange() * (double) num) + (long) minValue);
    }

    /// <summary>
    ///   Возвращает неотрицательное случайное целое число, которое меньше максимально допустимого значения.
    /// </summary>
    /// <param name="maxValue">
    ///   Исключенный верхний предел создаваемого случайного числа.
    ///   <paramref name="maxValue" />должно быть больше или равно 0.
    /// </param>
    /// <returns>
    ///   32-разрядное целое число со знаком, которое больше или равно 0 и меньше чем <paramref name="maxValue" />; диапазон возвращаемого значения обычно включает, 0, но не <paramref name="maxValue" />.
    ///    Однако если <paramref name="maxValue" /> равняется 0, <paramref name="maxValue" /> возвращается.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Значение параметра <paramref name="maxValue" /> меньше 0.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual int Next(int maxValue)
    {
      if (maxValue < 0)
        throw new ArgumentOutOfRangeException(nameof (maxValue), Environment.GetResourceString("ArgumentOutOfRange_MustBePositive", (object) nameof (maxValue)));
      return (int) (this.Sample() * (double) maxValue);
    }

    /// <summary>
    ///   Возвращает случайное число с плавающей запятой, которое больше или равно 0,0 и меньше 1,0.
    /// </summary>
    /// <returns>
    ///   Число двойной точности с плавающей запятой, которое больше или равно 0,0, и меньше 1,0.
    /// </returns>
    [__DynamicallyInvokable]
    public virtual double NextDouble()
    {
      return this.Sample();
    }

    /// <summary>
    ///   Заполняет элементы указанного массива байтов случайными числами.
    /// </summary>
    /// <param name="buffer">
    ///   Массив байтов, содержащий случайные числа.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="buffer" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public virtual void NextBytes(byte[] buffer)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      for (int index = 0; index < buffer.Length; ++index)
        buffer[index] = (byte) (this.InternalSample() % 256);
    }
  }
}

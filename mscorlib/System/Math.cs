// Decompiled with JetBrains decompiler
// Type: System.Math
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Versioning;
using System.Security;

namespace System
{
  /// <summary>
  ///   Предоставляет константы и статические методы для тригонометрических, логарифмических и иных общих математических функций.
  /// 
  ///   Для просмотра исходного кода .NET Framework для этого типа, в разделе Reference Source.
  /// </summary>
  [__DynamicallyInvokable]
  public static class Math
  {
    private static double doubleRoundLimit = 1E+16;
    private static double[] roundPower10Double = new double[16]
    {
      1.0,
      10.0,
      100.0,
      1000.0,
      10000.0,
      100000.0,
      1000000.0,
      10000000.0,
      100000000.0,
      1000000000.0,
      10000000000.0,
      100000000000.0,
      1000000000000.0,
      10000000000000.0,
      100000000000000.0,
      1E+15
    };
    private const int maxRoundingDigits = 15;
    /// <summary>
    ///   Представляет отношение длины окружности к ее диаметру, определяемое константой π.
    /// </summary>
    [__DynamicallyInvokable]
    public const double PI = 3.14159265358979;
    /// <summary>
    ///   Представляет основание натурального логарифма, определяемое константой <see langword="e" />.
    /// </summary>
    [__DynamicallyInvokable]
    public const double E = 2.71828182845905;

    /// <summary>
    ///   Возвращает угол, косинус которого равен указанному числу.
    /// </summary>
    /// <param name="d">
    ///   Число, представляющее косинус, где значение параметра <paramref name="d" /> должно быть больше или равно -1, но меньше или равно 1.
    /// </param>
    /// <returns>
    ///   Угол θ, измеренный в радианах, такой что 0 ≤θ≤π
    /// 
    ///   -или-
    /// 
    ///   значение <see cref="F:System.Double.NaN" />, если <paramref name="d" /> &lt; -1, <paramref name="d" /> &gt; 1 или значение параметра <paramref name="d" /> равно <see cref="F:System.Double.NaN" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Acos(double d);

    /// <summary>
    ///   Возвращает угол, синус которого равен указанному числу.
    /// </summary>
    /// <param name="d">
    ///   Число, представляющее синус, где значение параметра <paramref name="d" /> должно быть больше или равно -1, но меньше или равно 1.
    /// </param>
    /// <returns>
    ///   Угол θ, измеренный в радианах, такой что -π/2 ≤θ≤π/2
    /// 
    ///   -или-
    /// 
    ///   значение <see cref="F:System.Double.NaN" />, если <paramref name="d" /> &lt; -1, <paramref name="d" /> &gt; 1 или значение параметра <paramref name="d" /> равно <see cref="F:System.Double.NaN" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Asin(double d);

    /// <summary>
    ///   Возвращает угол, тангенс которого равен указанному числу.
    /// </summary>
    /// <param name="d">Число, представляющее тангенс.</param>
    /// <returns>
    ///   Угол θ, измеренный в радианах, такой что -π/2 ≤θ≤π/2.
    /// 
    ///   -или-
    /// 
    ///   значение <see cref="F:System.Double.NaN" />, если <paramref name="d" /> равно <see cref="F:System.Double.NaN" />, -π/2, округленное до двойной точности (-1,5707963267949), если <paramref name="d" /> равно <see cref="F:System.Double.NegativeInfinity" />, или π/2, округленное до двойной точности (1,5707963267949), если <paramref name="d" /> равно <see cref="F:System.Double.PositiveInfinity" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Atan(double d);

    /// <summary>
    ///   Возвращает угол, тангенс которого равен отношению двух указанных чисел.
    /// </summary>
    /// <param name="y">Координата y точки.</param>
    /// <param name="x">Координата х точки.</param>
    /// <returns>
    ///   Угол θ, измеренный в радианах, такой что -π≤θ≤π, и tg(θ) = <paramref name="y" />/<paramref name="x" />, где (<paramref name="x" />, <paramref name="y" />) — это точка в декартовой системе координат.
    ///    Обратите внимание на следующее.
    /// 
    ///       For (<paramref name="x" />, <paramref name="y" />) in quadrant 1, 0 &lt;&gt;θ &lt;&gt;π/2.
    /// 
    ///       For (<paramref name="x" />, <paramref name="y" />) in quadrant 2, π/2 &lt;&gt;θ≤π.
    /// 
    ///       For (<paramref name="x" />, <paramref name="y" />) in quadrant 3, -π &lt;&gt;θ &lt;&gt;π/2.
    /// 
    ///       For (<paramref name="x" />, <paramref name="y" />) in quadrant 4, -π/2 &lt;&gt;θ&lt; 0.&gt;&lt;/ 0.&gt;
    /// 
    ///   Для точек за пределами указанных квадрантов возвращаемое значение указано ниже.
    /// 
    ///       Если y равно 0 и x не является отрицательным, θ = 0.
    /// 
    ///       Если y равно 0 и x не является отрицательным, θ = π.
    /// 
    ///       Если y — положительное число, а x равно 0, θ = π/2.
    /// 
    ///       Если y является отрицательным и х равно 0, θ = -π/2.
    /// 
    ///       Если y равен 0 и х равен 0, то θ = -π/2.
    /// 
    ///   Если значение параметра <paramref name="x" /> или <paramref name="y" /> равно <see cref="F:System.Double.NaN" /> либо если значения параметров <paramref name="x" /> и <paramref name="y" /> равны значению <see cref="F:System.Double.PositiveInfinity" /> или <see cref="F:System.Double.NegativeInfinity" />, метод возвращает значение <see cref="F:System.Double.NaN" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Atan2(double y, double x);

    /// <summary>
    ///   Возвращает наименьшее целое число, которое больше или равно заданному десятичному числу.
    /// </summary>
    /// <param name="d">Десятичное число.</param>
    /// <returns>
    ///   Наименьшее целочисленное значение, которое больше или равно <paramref name="d" />.
    ///    Обратите внимание, что данный метод возвращает не целочисленное значение, а значение типа <see cref="T:System.Decimal" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static Decimal Ceiling(Decimal d)
    {
      return Decimal.Ceiling(d);
    }

    /// <summary>
    ///   Возвращает наименьшее целое число, которое больше или равно заданному числу с плавающей запятой двойной точности.
    /// </summary>
    /// <param name="a">
    ///   Число двойной точности с плавающей запятой.
    /// </param>
    /// <returns>
    ///   Наименьшее целочисленное значение, которое больше или равно <paramref name="a" />.
    ///    Если значение параметра <paramref name="a" /> равно  <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NegativeInfinity" /> или <see cref="F:System.Double.PositiveInfinity" />, возвращается это значение.
    ///    Обратите внимание, что данный метод возвращает не целочисленное значение, а значение типа <see cref="T:System.Double" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Ceiling(double a);

    /// <summary>Возвращает косинус указанного угла.</summary>
    /// <param name="d">Угол, измеряемый в радианах.</param>
    /// <returns>
    ///   Косинус <paramref name="d" />.
    ///    Если значение параметра <paramref name="d" /> равно <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NegativeInfinity" /> или <see cref="F:System.Double.PositiveInfinity" />, то данный метод возвращает <see cref="F:System.Double.NaN" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Cos(double d);

    /// <summary>Возвращает гиперболический косинус указанного угла.</summary>
    /// <param name="value">Угол, измеряемый в радианах.</param>
    /// <returns>
    ///   Гиперболический косинус <paramref name="value" />.
    ///    Если значение параметра <paramref name="value" /> равно <see cref="F:System.Double.NegativeInfinity" /> или <see cref="F:System.Double.PositiveInfinity" />, возвращается значение <see cref="F:System.Double.PositiveInfinity" />.
    ///    Если значение параметра <paramref name="value" /> равно <see cref="F:System.Double.NaN" />, возвращается значение <see cref="F:System.Double.NaN" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Cosh(double value);

    /// <summary>
    ///   Возвращает наибольшее целое число, которое меньше или равно указанному десятичному числу.
    /// </summary>
    /// <param name="d">Десятичное число.</param>
    /// <returns>
    ///   Наибольшее целое число, меньшее или равное <paramref name="d" />.
    ///     Обратите внимание, что этот метод возвращает целочисленное значение типа <see cref="T:System.Math" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static Decimal Floor(Decimal d)
    {
      return Decimal.Floor(d);
    }

    /// <summary>
    ///   Возвращает наибольшее целое число, которое меньше или равно заданному числу двойной точности с плавающей запятой.
    /// </summary>
    /// <param name="d">
    ///   Число двойной точности с плавающей запятой.
    /// </param>
    /// <returns>
    ///   Наибольшее целое число, меньшее или равное <paramref name="d" />.
    ///    Если значение параметра <paramref name="d" /> равно <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NegativeInfinity" /> или <see cref="F:System.Double.PositiveInfinity" />, возвращается это значение.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Floor(double d);

    [SecuritySafeCritical]
    private static unsafe double InternalRound(double value, int digits, MidpointRounding mode)
    {
      if (Math.Abs(value) < Math.doubleRoundLimit)
      {
        double num1 = Math.roundPower10Double[digits];
        value *= num1;
        if (mode == MidpointRounding.AwayFromZero)
        {
          double num2 = Math.SplitFractionDouble(&value);
          if (Math.Abs(num2) >= 0.5)
            value += (double) Math.Sign(num2);
        }
        else
          value = Math.Round(value);
        value /= num1;
      }
      return value;
    }

    [SecuritySafeCritical]
    private static unsafe double InternalTruncate(double d)
    {
      Math.SplitFractionDouble(&d);
      return d;
    }

    /// <summary>Возвращает синус указанного угла.</summary>
    /// <param name="a">Угол, измеряемый в радианах.</param>
    /// <returns>
    ///   Синус <paramref name="a" />.
    ///    Если значение параметра <paramref name="a" /> равно <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NegativeInfinity" /> или <see cref="F:System.Double.PositiveInfinity" />, то данный метод возвращает <see cref="F:System.Double.NaN" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Sin(double a);

    /// <summary>Возвращает тангенс указанного угла.</summary>
    /// <param name="a">Угол, измеряемый в радианах.</param>
    /// <returns>
    ///   Тангенс <paramref name="a" />.
    ///    Если значение параметра <paramref name="a" /> равно <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NegativeInfinity" /> или <see cref="F:System.Double.PositiveInfinity" />, то данный метод возвращает <see cref="F:System.Double.NaN" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Tan(double a);

    /// <summary>Возвращает гиперболический синус указанного угла.</summary>
    /// <param name="value">Угол, измеряемый в радианах.</param>
    /// <returns>
    ///   Гиперболический синус <paramref name="value" />.
    ///    Если значение параметра <paramref name="value" /> равно <see cref="F:System.Double.NegativeInfinity" />, <see cref="F:System.Double.PositiveInfinity" /> или <see cref="F:System.Double.NaN" />, то этот метод возвращает значение <see cref="T:System.Double" />, равное <paramref name="value" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Sinh(double value);

    /// <summary>Возвращает гиперболический тангенс указанного угла.</summary>
    /// <param name="value">Угол, измеряемый в радианах.</param>
    /// <returns>
    ///   Гиперболический тангенс <paramref name="value" />.
    ///    Если значение параметра <paramref name="value" /> равно <see cref="F:System.Double.NegativeInfinity" />, этот метод возвращает -1.
    ///    Если значение равно <see cref="F:System.Double.PositiveInfinity" />, этот метод возвращает 1.
    ///    Если значение параметра <paramref name="value" /> равно <see cref="F:System.Double.NaN" />, этот метод возвращает <see cref="F:System.Double.NaN" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Tanh(double value);

    /// <summary>
    ///   Округляет заданное число с плавающей запятой двойной точности до ближайшего целого.
    /// </summary>
    /// <param name="a">
    ///   Округляемое число двойной точности с плавающей запятой.
    /// </param>
    /// <returns>
    ///   Целое число, ближайшее к значению параметра <paramref name="a" />.
    ///    Если дробная часть <paramref name="a" /> находится на равном расстоянии от двух целых чисел (четного и нечетного), возвращается четное число.
    ///    Обратите внимание, что данный метод возвращает не целочисленное значение, а значение типа <see cref="T:System.Double" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Round(double a);

    /// <summary>
    ///   Округляет значение двойной точности с плавающей запятой до заданного количества дробных разрядов.
    /// </summary>
    /// <param name="value">
    ///   Округляемое число двойной точности с плавающей запятой.
    /// </param>
    /// <param name="digits">
    ///   Количество цифр дробной части в возвращаемом значении.
    /// </param>
    /// <returns>
    ///   Число, ближайшее к параметру <paramref name="value" />, количество цифр дробной части которого равно <paramref name="digits" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="digits" />значение меньше 0 или больше 15.
    /// </exception>
    [__DynamicallyInvokable]
    public static double Round(double value, int digits)
    {
      if (digits < 0 || digits > 15)
        throw new ArgumentOutOfRangeException(nameof (digits), Environment.GetResourceString("ArgumentOutOfRange_RoundingDigits"));
      return Math.InternalRound(value, digits, MidpointRounding.ToEven);
    }

    /// <summary>
    ///   Округляет заданное значение число двойной точности с плавающей запятой до ближайшего целого.
    ///    Параметр задает правило округления значения, если оно находится ровно посредине между двумя числами.
    /// </summary>
    /// <param name="value">
    ///   Округляемое число двойной точности с плавающей запятой.
    /// </param>
    /// <param name="mode">
    ///   Значение, задающее правило округления параметра <paramref name="value" />, если его значение находится ровно посредине между двумя другими числами.
    /// </param>
    /// <returns>
    ///   Целое число, ближайшее к значению параметра <paramref name="value" />.
    ///    Если <paramref name="value" /> находится на равном расстоянии от двух целых чисел (четного и нечетного), то возвращаемое число определяется по значению параметра <paramref name="mode" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="mode" />не является допустимым значением для <see cref="T:System.MidpointRounding" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static double Round(double value, MidpointRounding mode)
    {
      return Math.Round(value, 0, mode);
    }

    /// <summary>
    ///   Округляет значение двойной точности с плавающей запятой до заданного количества дробных разрядов.
    ///    Параметр задает правило округления значения, если оно находится ровно посредине между двумя числами.
    /// </summary>
    /// <param name="value">
    ///   Округляемое число двойной точности с плавающей запятой.
    /// </param>
    /// <param name="digits">
    ///   Количество цифр дробной части в возвращаемом значении.
    /// </param>
    /// <param name="mode">
    ///   Значение, задающее правило округления параметра <paramref name="value" />, если его значение находится ровно посредине между двумя другими числами.
    /// </param>
    /// <returns>
    ///   Число, ближайшее к параметру <paramref name="value" />, количество цифр дробной части которого равно <paramref name="digits" />.
    ///    Если <paramref name="value" /> имеет меньшее количество цифр дробной части, чем <paramref name="digits" />, то <paramref name="value" /> возвращается без изменений.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   <paramref name="digits" />значение меньше 0 или больше 15.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="mode" />не является допустимым значением для <see cref="T:System.MidpointRounding" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static double Round(double value, int digits, MidpointRounding mode)
    {
      if (digits < 0 || digits > 15)
        throw new ArgumentOutOfRangeException(nameof (digits), Environment.GetResourceString("ArgumentOutOfRange_RoundingDigits"));
      switch (mode)
      {
        case MidpointRounding.ToEven:
        case MidpointRounding.AwayFromZero:
          return Math.InternalRound(value, digits, mode);
        default:
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidEnumValue", (object) mode, (object) "MidpointRounding"), nameof (mode));
      }
    }

    /// <summary>Округляет десятичное значение до ближайшего целого.</summary>
    /// <param name="d">Округляемое десятичное число.</param>
    /// <returns>
    ///   Целое число, ближайшее к значению параметра <paramref name="d" />.
    ///    Если дробная часть <paramref name="d" /> находится на равном расстоянии от двух целых чисел (четного и нечетного), возвращается четное число.
    ///    Обратите внимание, что данный метод возвращает не целочисленное значение, а значение типа <see cref="T:System.Decimal" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   Результат находится вне диапазона <see cref="T:System.Decimal" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Decimal Round(Decimal d)
    {
      return Decimal.Round(d, 0);
    }

    /// <summary>
    ///   Округляет десятичное значение до указанного числа дробных разрядов.
    /// </summary>
    /// <param name="d">Округляемое десятичное число.</param>
    /// <param name="decimals">
    ///   Количество десятичных разрядов в возвращаемом значении.
    /// </param>
    /// <returns>
    ///   Число, ближайшее к параметру <paramref name="d" />, количество цифр дробной части которого равно <paramref name="decimals" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="decimals" /> имеет значение меньше 0 или больше 28.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Результат находится вне диапазона <see cref="T:System.Decimal" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Decimal Round(Decimal d, int decimals)
    {
      return Decimal.Round(d, decimals);
    }

    /// <summary>
    ///   Округляет десятичное значение до ближайшего целого.
    ///    Параметр задает правило округления значения, если оно находится ровно посредине между двумя числами.
    /// </summary>
    /// <param name="d">Округляемое десятичное число.</param>
    /// <param name="mode">
    ///   Значение, задающее правило округления параметра <paramref name="d" />, если его значение находится ровно посредине между двумя другими числами.
    /// </param>
    /// <returns>
    ///   Целое число, ближайшее к значению параметра <paramref name="d" />.
    ///    Если <paramref name="d" /> находится на равном расстоянии от двух чисел (четного и нечетного), то возвращаемое число определяется по значению параметра <paramref name="mode" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="mode" />не является допустимым значением для <see cref="T:System.MidpointRounding" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Результат находится вне диапазона <see cref="T:System.Decimal" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Decimal Round(Decimal d, MidpointRounding mode)
    {
      return Decimal.Round(d, 0, mode);
    }

    /// <summary>
    ///   Округляет десятичное значение до указанного числа дробных разрядов.
    ///    Параметр задает правило округления значения, если оно находится ровно посредине между двумя числами.
    /// </summary>
    /// <param name="d">Округляемое десятичное число.</param>
    /// <param name="decimals">
    ///   Количество десятичных разрядов в возвращаемом значении.
    /// </param>
    /// <param name="mode">
    ///   Значение, задающее правило округления параметра <paramref name="d" />, если его значение находится ровно посредине между двумя другими числами.
    /// </param>
    /// <returns>
    ///   Число, ближайшее к параметру <paramref name="d" />, количество цифр дробной части которого равно <paramref name="decimals" />.
    ///    Если <paramref name="d" /> имеет меньшее количество цифр дробной части, чем <paramref name="decimals" />, то <paramref name="d" /> возвращается без изменений.
    /// </returns>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    ///   Параметр <paramref name="decimals" /> имеет значение меньше 0 или больше 28.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="mode" />не является допустимым значением для <see cref="T:System.MidpointRounding" />.
    /// </exception>
    /// <exception cref="T:System.OverflowException">
    ///   Результат находится вне диапазона <see cref="T:System.Decimal" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static Decimal Round(Decimal d, int decimals, MidpointRounding mode)
    {
      return Decimal.Round(d, decimals, mode);
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern unsafe double SplitFractionDouble(double* value);

    /// <summary>Вычисляет целую часть заданного десятичного числа.</summary>
    /// <param name="d">Усекаемое число.</param>
    /// <returns>
    ///   Целая часть <paramref name="d" />, то есть число, остающееся после отбрасывания дробной части.
    /// </returns>
    [__DynamicallyInvokable]
    public static Decimal Truncate(Decimal d)
    {
      return Decimal.Truncate(d);
    }

    /// <summary>
    ///   Вычисляет целую часть заданного числа двойной точности с плавающей запятой.
    /// </summary>
    /// <param name="d">Усекаемое число.</param>
    /// <returns>
    /// Целая часть <paramref name="d" />, то есть число, которое остается после отбрасывания всех цифр дробной части, или одно из значений, перечисленных в следующей таблице.
    /// 
    ///         <paramref name="d" />
    /// 
    ///         Возвращаемое значение
    /// 
    ///         <see cref="F:System.Double.NaN" />
    /// 
    ///         <see cref="F:System.Double.NaN" />
    /// 
    ///         <see cref="F:System.Double.NegativeInfinity" />
    /// 
    ///         <see cref="F:System.Double.NegativeInfinity" />
    /// 
    ///         <see cref="F:System.Double.PositiveInfinity" />
    /// 
    ///         <see cref="F:System.Double.PositiveInfinity" />
    ///       </returns>
    [__DynamicallyInvokable]
    public static double Truncate(double d)
    {
      return Math.InternalTruncate(d);
    }

    /// <summary>Возвращает квадратный корень из указанного числа.</summary>
    /// <param name="d">
    ///   Число, квадратный корень которого требуется найти.
    /// </param>
    /// <returns>
    /// Одно из значений, перечисленных в следующей таблице.
    /// 
    ///         <paramref name="d" />параметр
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Нуль или положительное число
    /// 
    ///         Положительный квадратный корень из <paramref name="d" />.
    /// 
    ///         Отрицательное число
    /// 
    ///         <see cref="F:System.Double.NaN" />
    /// 
    ///         Равно<see cref="F:System.Double.NaN" />
    /// 
    ///         <see cref="F:System.Double.NaN" />
    /// 
    ///         Равно<see cref="F:System.Double.PositiveInfinity" />
    /// 
    ///         <see cref="F:System.Double.PositiveInfinity" />
    ///       </returns>
    [SecuritySafeCritical]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Sqrt(double d);

    /// <summary>
    ///   Возвращает натуральный логарифм (с основанием <see langword="e" />) указанного числа.
    /// </summary>
    /// <param name="d">Число, логарифм которого требуется найти.</param>
    /// <returns>
    /// Одно из значений, перечисленных в следующей таблице.
    /// 
    ///         <paramref name="d" />параметр
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Положительное число
    /// 
    ///         Натуральный логарифм <paramref name="d" />; то есть, ln <paramref name="d" />, или журнала e<paramref name="d" />
    /// 
    ///         Нуль
    /// 
    ///         <see cref="F:System.Double.NegativeInfinity" />
    /// 
    ///         Отрицательное число
    /// 
    ///         <see cref="F:System.Double.NaN" />
    /// 
    ///         Равно<see cref="F:System.Double.NaN" />
    /// 
    ///         <see cref="F:System.Double.NaN" />
    /// 
    ///         Равно<see cref="F:System.Double.PositiveInfinity" />
    /// 
    ///         <see cref="F:System.Double.PositiveInfinity" />
    ///       </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Log(double d);

    /// <summary>
    ///   Возвращает логарифм с основанием 10 указанного числа.
    /// </summary>
    /// <param name="d">
    ///   Число, логарифм которого должен быть найден.
    /// </param>
    /// <returns>
    /// Одно из значений, перечисленных в следующей таблице.
    /// 
    ///         <paramref name="d" />параметр
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Положительное число
    /// 
    ///         Основание логарифма 10 <paramref name="d" />; то есть журнал 10<paramref name="d" />.
    /// 
    ///         Нуль
    /// 
    ///         <see cref="F:System.Double.NegativeInfinity" />
    /// 
    ///         Отрицательное число
    /// 
    ///         <see cref="F:System.Double.NaN" />
    /// 
    ///         Равно<see cref="F:System.Double.NaN" />
    /// 
    ///         <see cref="F:System.Double.NaN" />
    /// 
    ///         Равно<see cref="F:System.Double.PositiveInfinity" />
    /// 
    ///         <see cref="F:System.Double.PositiveInfinity" />
    ///       </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Log10(double d);

    /// <summary>
    ///   Возвращает значение <see langword="e" />, возведенное в указанную степень.
    /// </summary>
    /// <param name="d">Число, определяющее степень.</param>
    /// <returns>
    ///   Число <see langword="e" />, возведенное в степень <paramref name="d" />.
    ///    Если значение параметра <paramref name="d" /> равно <see cref="F:System.Double.NaN" /> или <see cref="F:System.Double.PositiveInfinity" />, возвращается это значение.
    ///    Если значение параметра <paramref name="d" /> равно <see cref="F:System.Double.NegativeInfinity" />, возвращается значение 0.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Exp(double d);

    /// <summary>
    ///   Возвращает указанное число, возведенное в указанную степень.
    /// </summary>
    /// <param name="x">
    ///   Число двойной точности с плавающей запятой, возводимое в степень.
    /// </param>
    /// <param name="y">
    ///   Число двойной точности с плавающей запятой, задающее степень.
    /// </param>
    /// <returns>
    ///   Число <paramref name="x" />, возведенное в степень <paramref name="y" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Pow(double x, double y);

    /// <summary>
    ///   Возвращает остаток от деления одного указанного числа на другое указанное число.
    /// </summary>
    /// <param name="x">Делимое.</param>
    /// <param name="y">Делитель.</param>
    /// <returns>
    ///   Число, равное <paramref name="x" /> - (<paramref name="y" />Q), где Q является частным <paramref name="x" />/<paramref name="y" />, округленным до ближайшего целого числа (если <paramref name="x" />/<paramref name="y" /> находится на равном расстоянии от двух целых чисел, выбирается четное число).
    /// 
    ///   Если значение <paramref name="x" /> - (<paramref name="y" />Q) равно нулю, возвращается значение +0 при положительном <paramref name="x" /> или значение -0 при отрицательном <paramref name="x" />.
    /// 
    ///   Если значение параметра <paramref name="y" /> равно 0, возвращается значение <see cref="F:System.Double.NaN" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static double IEEERemainder(double x, double y)
    {
      if (double.IsNaN(x))
        return x;
      if (double.IsNaN(y))
        return y;
      double d = x % y;
      if (double.IsNaN(d))
        return double.NaN;
      if (d == 0.0 && double.IsNegative(x))
        return double.NegativeZero;
      double num = d - Math.Abs(y) * (double) Math.Sign(x);
      if (Math.Abs(num) == Math.Abs(d))
      {
        double a = x / y;
        if (Math.Abs(Math.Round(a)) > Math.Abs(a))
          return num;
        return d;
      }
      if (Math.Abs(num) < Math.Abs(d))
        return num;
      return d;
    }

    /// <summary>
    ///   Возвращает абсолютное значение 8-битового целого числа со знаком.
    /// </summary>
    /// <param name="value">
    ///   Число, которое больше значения <see cref="F:System.SByte.MinValue" />, но меньше или равно значению <see cref="F:System.SByte.MaxValue" />.
    /// </param>
    /// <returns>
    ///   8-разрядное целое число х со знаком, такое что 0 ≤ x ≤<see cref="F:System.SByte.MaxValue" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> равняется <see cref="F:System.SByte.MinValue" />.
    /// </exception>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static sbyte Abs(sbyte value)
    {
      if (value >= (sbyte) 0)
        return value;
      return Math.AbsHelper(value);
    }

    private static sbyte AbsHelper(sbyte value)
    {
      if (value == sbyte.MinValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_NegateTwosCompNum"));
      return -value;
    }

    /// <summary>
    ///   Возвращает абсолютное значение 16-битового целого числа со знаком.
    /// </summary>
    /// <param name="value">
    ///   Число, которое больше значения <see cref="F:System.Int16.MinValue" />, но меньше или равно значению <see cref="F:System.Int16.MaxValue" />.
    /// </param>
    /// <returns>
    ///   16-разрядное целое число х со знаком, такое что 0 ≤ x ≤<see cref="F:System.Int16.MaxValue" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> равняется <see cref="F:System.Int16.MinValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static short Abs(short value)
    {
      if (value >= (short) 0)
        return value;
      return Math.AbsHelper(value);
    }

    private static short AbsHelper(short value)
    {
      if (value == short.MinValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_NegateTwosCompNum"));
      return -value;
    }

    /// <summary>
    ///   Возвращает абсолютное значение 32-битового целого числа со знаком.
    /// </summary>
    /// <param name="value">
    ///   Число, которое больше значения <see cref="F:System.Int32.MinValue" />, но меньше или равно значению <see cref="F:System.Int32.MaxValue" />.
    /// </param>
    /// <returns>
    ///   32-разрядное целое число х со знаком, такое что 0 ≤ x ≤<see cref="F:System.Int32.MaxValue" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> равняется <see cref="F:System.Int32.MinValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static int Abs(int value)
    {
      if (value >= 0)
        return value;
      return Math.AbsHelper(value);
    }

    private static int AbsHelper(int value)
    {
      if (value == int.MinValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_NegateTwosCompNum"));
      return -value;
    }

    /// <summary>
    ///   Возвращает абсолютное значение 64-битового целого числа со знаком.
    /// </summary>
    /// <param name="value">
    ///   Число, которое больше значения <see cref="F:System.Int64.MinValue" />, но меньше или равно значению <see cref="F:System.Int64.MaxValue" />.
    /// </param>
    /// <returns>
    ///   64-разрядное целое число х со знаком, такое что 0 ≤ x ≤<see cref="F:System.Int64.MaxValue" />.
    /// </returns>
    /// <exception cref="T:System.OverflowException">
    ///   <paramref name="value" /> равняется <see cref="F:System.Int64.MinValue" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static long Abs(long value)
    {
      if (value >= 0L)
        return value;
      return Math.AbsHelper(value);
    }

    private static long AbsHelper(long value)
    {
      if (value == long.MinValue)
        throw new OverflowException(Environment.GetResourceString("Overflow_NegateTwosCompNum"));
      return -value;
    }

    /// <summary>
    ///   Возвращает абсолютное значение числа одинарной точности с плавающей запятой.
    /// </summary>
    /// <param name="value">
    ///   Число, которое больше или равно значению <see cref="F:System.Single.MinValue" />, но меньше или равно значению <see cref="F:System.Single.MaxValue" />.
    /// </param>
    /// <returns>
    ///   Число х одинарной точности с плавающей запятой, такое что 0 ≤ x ≤<see cref="F:System.Single.MaxValue" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern float Abs(float value);

    /// <summary>
    ///   Возвращает абсолютное значение числа двойной точности с плавающей запятой.
    /// </summary>
    /// <param name="value">
    ///   Число, которое больше или равно значению <see cref="F:System.Double.MinValue" />, но меньше или равно значению <see cref="F:System.Double.MaxValue" />.
    /// </param>
    /// <returns>
    ///   Число х двойной точности с плавающей запятой такое, что 0 ≤ x ≤<see cref="F:System.Double.MaxValue" />.
    /// </returns>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern double Abs(double value);

    /// <summary>
    ///   Возвращает абсолютное значение числа <see cref="T:System.Decimal" />.
    /// </summary>
    /// <param name="value">
    ///   Число, которое больше или равно значению <see cref="F:System.Decimal.MinValue" />, но меньше или равно значению <see cref="F:System.Decimal.MaxValue" />.
    /// </param>
    /// <returns>
    ///   Десятичное число x, такое что 0 ≤ x ≤<see cref="F:System.Decimal.MaxValue" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static Decimal Abs(Decimal value)
    {
      return Decimal.Abs(value);
    }

    /// <summary>
    ///   Возвращает большее из двух 8-битовых целых чисел со знаком.
    /// </summary>
    /// <param name="val1">
    ///   Первое из двух сравниваемых 8-разрядных целых чисел со знаком.
    /// </param>
    /// <param name="val2">
    ///   Второе из двух сравниваемых 8-разрядных целых чисел со знаком.
    /// </param>
    /// <returns>
    ///   Большее из значений двух параметров, <paramref name="val1" /> или <paramref name="val2" />.
    /// </returns>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static sbyte Max(sbyte val1, sbyte val2)
    {
      if ((int) val1 < (int) val2)
        return val2;
      return val1;
    }

    /// <summary>
    ///   Возвращает большее из двух 8-битовых целых чисел без знака.
    /// </summary>
    /// <param name="val1">
    ///   Первое из двух сравниваемых 8-разрядных целых чисел без знака.
    /// </param>
    /// <param name="val2">
    ///   Второе из двух сравниваемых 8-разрядных целых чисел без знака.
    /// </param>
    /// <returns>
    ///   Большее из значений двух параметров, <paramref name="val1" /> или <paramref name="val2" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static byte Max(byte val1, byte val2)
    {
      if ((int) val1 < (int) val2)
        return val2;
      return val1;
    }

    /// <summary>
    ///   Возвращает большее из двух 16-битовых целых чисел со знаком.
    /// </summary>
    /// <param name="val1">
    ///   Первое из двух сравниваемых 16-разрядных целых чисел со знаком.
    /// </param>
    /// <param name="val2">
    ///   Второе из двух сравниваемых 16-разрядных целых чисел со знаком.
    /// </param>
    /// <returns>
    ///   Большее из значений двух параметров, <paramref name="val1" /> или <paramref name="val2" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static short Max(short val1, short val2)
    {
      if ((int) val1 < (int) val2)
        return val2;
      return val1;
    }

    /// <summary>
    ///   Возвращает большее из двух 16-битовых целых чисел без знака.
    /// </summary>
    /// <param name="val1">
    ///   Первое из двух сравниваемых 16-разрядных целых чисел без знака.
    /// </param>
    /// <param name="val2">
    ///   Второе из двух сравниваемых 16-разрядных целых чисел без знака.
    /// </param>
    /// <returns>
    ///   Большее из значений двух параметров, <paramref name="val1" /> или <paramref name="val2" />.
    /// </returns>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static ushort Max(ushort val1, ushort val2)
    {
      if ((int) val1 < (int) val2)
        return val2;
      return val1;
    }

    /// <summary>
    ///   Возвращает большее из двух 32-битовых целых чисел со знаком.
    /// </summary>
    /// <param name="val1">
    ///   Первое из двух сравниваемых 32-разрядных целых чисел со знаком.
    /// </param>
    /// <param name="val2">
    ///   Второе из двух сравниваемых 32-разрядных целых чисел со знаком.
    /// </param>
    /// <returns>
    ///   Большее из значений двух параметров, <paramref name="val1" /> или <paramref name="val2" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static int Max(int val1, int val2)
    {
      if (val1 < val2)
        return val2;
      return val1;
    }

    /// <summary>
    ///   Возвращает большее из двух 32-битовых целых чисел без знака.
    /// </summary>
    /// <param name="val1">
    ///   Первое из двух сравниваемых 32-разрядных целых чисел без знака.
    /// </param>
    /// <param name="val2">
    ///   Второе из двух сравниваемых 32-разрядных целых чисел без знака.
    /// </param>
    /// <returns>
    ///   Большее из значений двух параметров, <paramref name="val1" /> или <paramref name="val2" />.
    /// </returns>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static uint Max(uint val1, uint val2)
    {
      if (val1 < val2)
        return val2;
      return val1;
    }

    /// <summary>
    ///   Возвращает большее из двух 64-битовых целых чисел со знаком.
    /// </summary>
    /// <param name="val1">
    ///   Первое из двух сравниваемых 64-разрядных целых чисел со знаком.
    /// </param>
    /// <param name="val2">
    ///   Второе из двух сравниваемых 64-разрядных целых чисел со знаком.
    /// </param>
    /// <returns>
    ///   Большее из значений двух параметров, <paramref name="val1" /> или <paramref name="val2" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static long Max(long val1, long val2)
    {
      if (val1 < val2)
        return val2;
      return val1;
    }

    /// <summary>
    ///   Возвращает большее из двух 64-битовых целых чисел без знака.
    /// </summary>
    /// <param name="val1">
    ///   Первое из двух сравниваемых 64-разрядных целых чисел без знака.
    /// </param>
    /// <param name="val2">
    ///   Второе из двух сравниваемых 64-разрядных целых чисел без знака.
    /// </param>
    /// <returns>
    ///   Большее из значений двух параметров, <paramref name="val1" /> или <paramref name="val2" />.
    /// </returns>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static ulong Max(ulong val1, ulong val2)
    {
      if (val1 < val2)
        return val2;
      return val1;
    }

    /// <summary>
    ///   Возвращает большее из двух чисел одинарной точности с плавающей запятой.
    /// </summary>
    /// <param name="val1">
    ///   Первое из двух сравниваемых чисел одинарной точности с плавающей запятой.
    /// </param>
    /// <param name="val2">
    ///   Второе из двух сравниваемых чисел одинарной точности с плавающей запятой.
    /// </param>
    /// <returns>
    ///   Большее из значений двух параметров, <paramref name="val1" /> или <paramref name="val2" />.
    ///    Если <paramref name="val1" />, <paramref name="val2" /> или оба параметра <paramref name="val1" /> и <paramref name="val2" /> равны <see cref="F:System.Single.NaN" />, возвращается значение <see cref="F:System.Single.NaN" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static float Max(float val1, float val2)
    {
      if ((double) val1 > (double) val2 || float.IsNaN(val1))
        return val1;
      return val2;
    }

    /// <summary>
    ///   Возвращает большее из двух чисел двойной точности с плавающей запятой.
    /// </summary>
    /// <param name="val1">
    ///   Первое из двух сравниваемых чисел двойной точности с плавающей запятой.
    /// </param>
    /// <param name="val2">
    ///   Второе из двух сравниваемых чисел двойной точности с плавающей запятой.
    /// </param>
    /// <returns>
    ///   Большее из значений двух параметров, <paramref name="val1" /> или <paramref name="val2" />.
    ///    Если <paramref name="val1" />, <paramref name="val2" /> или оба параметра <paramref name="val1" /> и <paramref name="val2" /> равны <see cref="F:System.Double.NaN" />, возвращается значение <see cref="F:System.Double.NaN" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static double Max(double val1, double val2)
    {
      if (val1 > val2 || double.IsNaN(val1))
        return val1;
      return val2;
    }

    /// <summary>Возвращает большее из двух десятичных чисел.</summary>
    /// <param name="val1">
    ///   Первое из двух сравниваемых десятичных чисел.
    /// </param>
    /// <param name="val2">
    ///   Второе из двух сравниваемых десятичных чисел.
    /// </param>
    /// <returns>
    ///   Большее из значений двух параметров, <paramref name="val1" /> или <paramref name="val2" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static Decimal Max(Decimal val1, Decimal val2)
    {
      return Decimal.Max(val1, val2);
    }

    /// <summary>
    ///   Возвращает меньшее из двух 8-битовых целых чисел со знаком.
    /// </summary>
    /// <param name="val1">
    ///   Первое из двух сравниваемых 8-разрядных целых чисел со знаком.
    /// </param>
    /// <param name="val2">
    ///   Второе из двух сравниваемых 8-разрядных целых чисел со знаком.
    /// </param>
    /// <returns>
    ///   Меньший из двух параметров, <paramref name="val1" /> или <paramref name="val2" />.
    /// </returns>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static sbyte Min(sbyte val1, sbyte val2)
    {
      if ((int) val1 > (int) val2)
        return val2;
      return val1;
    }

    /// <summary>
    ///   Возвращает меньшее из двух 8-битовых целых чисел без знака.
    /// </summary>
    /// <param name="val1">
    ///   Первое из двух сравниваемых 8-разрядных целых чисел без знака.
    /// </param>
    /// <param name="val2">
    ///   Второе из двух сравниваемых 8-разрядных целых чисел без знака.
    /// </param>
    /// <returns>
    ///   Меньший из двух параметров, <paramref name="val1" /> или <paramref name="val2" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static byte Min(byte val1, byte val2)
    {
      if ((int) val1 > (int) val2)
        return val2;
      return val1;
    }

    /// <summary>
    ///   Возвращает меньшее из двух 16-битовых целых чисел со знаком.
    /// </summary>
    /// <param name="val1">
    ///   Первое из двух сравниваемых 16-разрядных целых чисел со знаком.
    /// </param>
    /// <param name="val2">
    ///   Второе из двух сравниваемых 16-разрядных целых чисел со знаком.
    /// </param>
    /// <returns>
    ///   Меньший из двух параметров, <paramref name="val1" /> или <paramref name="val2" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static short Min(short val1, short val2)
    {
      if ((int) val1 > (int) val2)
        return val2;
      return val1;
    }

    /// <summary>
    ///   Возвращает меньшее из двух 16-битовых целых чисел без знака.
    /// </summary>
    /// <param name="val1">
    ///   Первое из двух сравниваемых 16-разрядных целых чисел без знака.
    /// </param>
    /// <param name="val2">
    ///   Второе из двух сравниваемых 16-разрядных целых чисел без знака.
    /// </param>
    /// <returns>
    ///   Меньший из двух параметров, <paramref name="val1" /> или <paramref name="val2" />.
    /// </returns>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static ushort Min(ushort val1, ushort val2)
    {
      if ((int) val1 > (int) val2)
        return val2;
      return val1;
    }

    /// <summary>
    ///   Возвращает меньшее из двух 32-битовых целых чисел со знаком.
    /// </summary>
    /// <param name="val1">
    ///   Первое из двух сравниваемых 32-разрядных целых чисел со знаком.
    /// </param>
    /// <param name="val2">
    ///   Второе из двух сравниваемых 32-разрядных целых чисел со знаком.
    /// </param>
    /// <returns>
    ///   Меньший из двух параметров, <paramref name="val1" /> или <paramref name="val2" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static int Min(int val1, int val2)
    {
      if (val1 > val2)
        return val2;
      return val1;
    }

    /// <summary>
    ///   Возвращает меньшее из двух 32-битовых целых чисел без знака.
    /// </summary>
    /// <param name="val1">
    ///   Первое из двух сравниваемых 32-разрядных целых чисел без знака.
    /// </param>
    /// <param name="val2">
    ///   Второе из двух сравниваемых 32-разрядных целых чисел без знака.
    /// </param>
    /// <returns>
    ///   Меньший из двух параметров, <paramref name="val1" /> или <paramref name="val2" />.
    /// </returns>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static uint Min(uint val1, uint val2)
    {
      if (val1 > val2)
        return val2;
      return val1;
    }

    /// <summary>
    ///   Возвращает меньшее из двух 64-битовых целых чисел со знаком.
    /// </summary>
    /// <param name="val1">
    ///   Первое из двух сравниваемых 64-разрядных целых чисел со знаком.
    /// </param>
    /// <param name="val2">
    ///   Второе из двух сравниваемых 64-разрядных целых чисел со знаком.
    /// </param>
    /// <returns>
    ///   Меньший из двух параметров, <paramref name="val1" /> или <paramref name="val2" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static long Min(long val1, long val2)
    {
      if (val1 > val2)
        return val2;
      return val1;
    }

    /// <summary>
    ///   Возвращает меньшее из двух 64-битовых целых чисел без знака.
    /// </summary>
    /// <param name="val1">
    ///   Первое из двух сравниваемых 64-разрядных целых чисел без знака.
    /// </param>
    /// <param name="val2">
    ///   Второе из двух сравниваемых 64-разрядных целых чисел без знака.
    /// </param>
    /// <returns>
    ///   Меньший из двух параметров, <paramref name="val1" /> или <paramref name="val2" />.
    /// </returns>
    [CLSCompliant(false)]
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [NonVersionable]
    [__DynamicallyInvokable]
    public static ulong Min(ulong val1, ulong val2)
    {
      if (val1 > val2)
        return val2;
      return val1;
    }

    /// <summary>
    ///   Возвращает меньшее из двух чисел одинарной точности с плавающей запятой.
    /// </summary>
    /// <param name="val1">
    ///   Первое из двух сравниваемых чисел одинарной точности с плавающей запятой.
    /// </param>
    /// <param name="val2">
    ///   Второе из двух сравниваемых чисел одинарной точности с плавающей запятой.
    /// </param>
    /// <returns>
    ///   Меньший из двух параметров, <paramref name="val1" /> или <paramref name="val2" />.
    ///    Если <paramref name="val1" />, <paramref name="val2" /> или оба параметра <paramref name="val1" /> и <paramref name="val2" /> равны <see cref="F:System.Single.NaN" />, возвращается значение <see cref="F:System.Single.NaN" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static float Min(float val1, float val2)
    {
      if ((double) val1 < (double) val2 || float.IsNaN(val1))
        return val1;
      return val2;
    }

    /// <summary>
    ///   Возвращает меньшее из двух чисел двойной точности с плавающей запятой.
    /// </summary>
    /// <param name="val1">
    ///   Первое из двух сравниваемых чисел двойной точности с плавающей запятой.
    /// </param>
    /// <param name="val2">
    ///   Второе из двух сравниваемых чисел двойной точности с плавающей запятой.
    /// </param>
    /// <returns>
    ///   Меньший из двух параметров, <paramref name="val1" /> или <paramref name="val2" />.
    ///    Если <paramref name="val1" />, <paramref name="val2" /> или оба параметра <paramref name="val1" /> и <paramref name="val2" /> равны <see cref="F:System.Double.NaN" />, возвращается значение <see cref="F:System.Double.NaN" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static double Min(double val1, double val2)
    {
      if (val1 < val2 || double.IsNaN(val1))
        return val1;
      return val2;
    }

    /// <summary>Возвращает меньшее из двух десятичных чисел.</summary>
    /// <param name="val1">
    ///   Первое из двух сравниваемых десятичных чисел.
    /// </param>
    /// <param name="val2">
    ///   Второе из двух сравниваемых десятичных чисел.
    /// </param>
    /// <returns>
    ///   Меньший из двух параметров, <paramref name="val1" /> или <paramref name="val2" />.
    /// </returns>
    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [__DynamicallyInvokable]
    public static Decimal Min(Decimal val1, Decimal val2)
    {
      return Decimal.Min(val1, val2);
    }

    /// <summary>
    ///   Возвращает логарифм указанного числа в системе счисления с указанным основанием.
    /// </summary>
    /// <param name="a">Число, логарифм которого требуется найти.</param>
    /// <param name="newBase">Основание логарифма.</param>
    /// <returns>
    /// Одно из значений, перечисленных в следующей таблице.
    ///  (+бесконечность обозначает <see cref="F:System.Double.PositiveInfinity" />, -бесконечность обозначает <see cref="F:System.Double.NegativeInfinity" />, а нечисловое значение обозначает <see cref="F:System.Double.NaN" />.)
    /// 
    ///         <paramref name="a" />
    /// 
    ///         <paramref name="newBase" />
    /// 
    ///         Возвращаемое значение
    /// 
    ///         <paramref name="a" />&gt; 0
    /// 
    ///         (0 &lt;<paramref name="newBase" />&lt; 1) -or-(<paramref name="newBase" />&gt; 1)
    /// 
    ///         ЖурналnewBase(a)
    /// 
    ///         <paramref name="a" />&lt; 0
    /// 
    ///         (любое значение)
    /// 
    ///         NaN
    /// 
    ///         (любое значение)
    /// 
    ///         <paramref name="newBase" />&lt; 0
    /// 
    ///         NaN
    /// 
    ///         <paramref name="a" /> != 1
    /// 
    ///         <paramref name="newBase" /> = 0
    /// 
    ///         NaN
    /// 
    ///         <paramref name="a" /> != 1
    /// 
    ///         <paramref name="newBase" />= + Бесконечность
    /// 
    ///         NaN
    /// 
    ///         <paramref name="a" /> = NaN
    /// 
    ///         (любое значение)
    /// 
    ///         NaN
    /// 
    ///         (любое значение)
    /// 
    ///         <paramref name="newBase" /> = NaN
    /// 
    ///         NaN
    /// 
    ///         (любое значение)
    /// 
    ///         <paramref name="newBase" /> = 1
    /// 
    ///         NaN
    /// 
    ///         <paramref name="a" /> = 0
    /// 
    ///         0 &lt;<paramref name="newBase" />&lt; 1
    /// 
    ///         +бесконечность
    /// 
    ///         <paramref name="a" /> = 0
    /// 
    ///         <paramref name="newBase" />&gt; 1
    /// 
    ///         -бесконечность
    /// 
    ///         <paramref name="a" />= + Бесконечность
    /// 
    ///         0 &lt;<paramref name="newBase" />&lt; 1
    /// 
    ///         -бесконечность
    /// 
    ///         <paramref name="a" />= + Бесконечность
    /// 
    ///         <paramref name="newBase" />&gt; 1
    /// 
    ///         +бесконечность
    /// 
    ///         <paramref name="a" /> = 1
    /// 
    ///         <paramref name="newBase" /> = 0
    /// 
    ///         0
    /// 
    ///         <paramref name="a" /> = 1
    /// 
    ///         <paramref name="newBase" />= + Бесконечность
    /// 
    ///         0
    ///       </returns>
    [__DynamicallyInvokable]
    public static double Log(double a, double newBase)
    {
      if (double.IsNaN(a))
        return a;
      if (double.IsNaN(newBase))
        return newBase;
      if (newBase == 1.0 || a != 1.0 && (newBase == 0.0 || double.IsPositiveInfinity(newBase)))
        return double.NaN;
      return Math.Log(a) / Math.Log(newBase);
    }

    /// <summary>
    ///   Возвращает целое число, указывающее знак 8-разрядного целого числа со знаком.
    /// </summary>
    /// <param name="value">Число со знаком.</param>
    /// <returns>
    /// Число, которое указывает знак значения <paramref name="value" />, как показано в следующей таблице.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Значение
    /// 
    ///         -1
    /// 
    ///         Значение параметра <paramref name="value" /> меньше нуля.
    /// 
    ///         0
    /// 
    ///         <paramref name="value" />равняется нулю.
    /// 
    ///         1
    /// 
    ///         <paramref name="value" />больше нуля.
    ///       </returns>
    [CLSCompliant(false)]
    [__DynamicallyInvokable]
    public static int Sign(sbyte value)
    {
      if (value < (sbyte) 0)
        return -1;
      return value > (sbyte) 0 ? 1 : 0;
    }

    /// <summary>
    ///   Возвращает целое число, указывающее знак 16-разрядного целого числа со знаком.
    /// </summary>
    /// <param name="value">Число со знаком.</param>
    /// <returns>
    /// Число, которое указывает знак значения <paramref name="value" />, как показано в следующей таблице.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Значение
    /// 
    ///         -1
    /// 
    ///         Значение параметра <paramref name="value" /> меньше нуля.
    /// 
    ///         0
    /// 
    ///         <paramref name="value" />равняется нулю.
    /// 
    ///         1
    /// 
    ///         <paramref name="value" />больше нуля.
    ///       </returns>
    [__DynamicallyInvokable]
    public static int Sign(short value)
    {
      if (value < (short) 0)
        return -1;
      return value > (short) 0 ? 1 : 0;
    }

    /// <summary>
    ///   Возвращает целое число, указывающее знак 32-разрядного целого числа со знаком.
    /// </summary>
    /// <param name="value">Число со знаком.</param>
    /// <returns>
    /// Число, которое указывает знак значения <paramref name="value" />, как показано в следующей таблице.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Значение
    /// 
    ///         -1
    /// 
    ///         Значение параметра <paramref name="value" /> меньше нуля.
    /// 
    ///         0
    /// 
    ///         <paramref name="value" />равняется нулю.
    /// 
    ///         1
    /// 
    ///         <paramref name="value" />больше нуля.
    ///       </returns>
    [__DynamicallyInvokable]
    public static int Sign(int value)
    {
      if (value < 0)
        return -1;
      return value > 0 ? 1 : 0;
    }

    /// <summary>
    ///   Возвращает целое число, указывающее знак 64-разрядного целого числа со знаком.
    /// </summary>
    /// <param name="value">Число со знаком.</param>
    /// <returns>
    /// Число, которое указывает знак значения <paramref name="value" />, как показано в следующей таблице.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Значение
    /// 
    ///         -1
    /// 
    ///         Значение параметра <paramref name="value" /> меньше нуля.
    /// 
    ///         0
    /// 
    ///         <paramref name="value" /> значение равно нулю.
    /// 
    ///         1
    /// 
    ///         <paramref name="value" /> больше нуля.
    ///       </returns>
    [__DynamicallyInvokable]
    public static int Sign(long value)
    {
      if (value < 0L)
        return -1;
      return value > 0L ? 1 : 0;
    }

    /// <summary>
    ///   Возвращает целое число, обозначающее знак числа с плавающей запятой одиночной точности.
    /// </summary>
    /// <param name="value">Число со знаком.</param>
    /// <returns>
    /// Число, которое указывает знак значения <paramref name="value" />, как показано в следующей таблице.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Значение
    /// 
    ///         -1
    /// 
    ///         Значение параметра <paramref name="value" /> меньше нуля.
    /// 
    ///         0
    /// 
    ///         <paramref name="value" />равняется нулю.
    /// 
    ///         1
    /// 
    ///         <paramref name="value" />больше нуля.
    ///       </returns>
    /// <exception cref="T:System.ArithmeticException">
    ///   <paramref name="value" /> равно <see cref="F:System.Single.NaN" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static int Sign(float value)
    {
      if ((double) value < 0.0)
        return -1;
      if ((double) value > 0.0)
        return 1;
      if ((double) value == 0.0)
        return 0;
      throw new ArithmeticException(Environment.GetResourceString("Arithmetic_NaN"));
    }

    /// <summary>
    ///   Возвращает целое число, обозначающее знак числа двойной точности с плавающей запятой.
    /// </summary>
    /// <param name="value">Число со знаком.</param>
    /// <returns>
    /// Число, которое указывает знак значения <paramref name="value" />, как показано в следующей таблице.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Значение
    /// 
    ///         -1
    /// 
    ///         Значение параметра <paramref name="value" /> меньше нуля.
    /// 
    ///         0
    /// 
    ///         <paramref name="value" />равняется нулю.
    /// 
    ///         1
    /// 
    ///         <paramref name="value" />больше нуля.
    ///       </returns>
    /// <exception cref="T:System.ArithmeticException">
    ///   <paramref name="value" /> равно <see cref="F:System.Double.NaN" />.
    /// </exception>
    [__DynamicallyInvokable]
    public static int Sign(double value)
    {
      if (value < 0.0)
        return -1;
      if (value > 0.0)
        return 1;
      if (value == 0.0)
        return 0;
      throw new ArithmeticException(Environment.GetResourceString("Arithmetic_NaN"));
    }

    /// <summary>
    ///   Возвращает целое число, указывающее знак десятичного числа.
    /// </summary>
    /// <param name="value">Десятичное число со знаком.</param>
    /// <returns>
    /// Число, которое указывает знак значения <paramref name="value" />, как показано в следующей таблице.
    /// 
    ///         Возвращаемое значение
    /// 
    ///         Значение
    /// 
    ///         -1
    /// 
    ///         Значение параметра <paramref name="value" /> меньше нуля.
    /// 
    ///         0
    /// 
    ///         <paramref name="value" />равняется нулю.
    /// 
    ///         1
    /// 
    ///         <paramref name="value" />больше нуля.
    ///       </returns>
    [__DynamicallyInvokable]
    public static int Sign(Decimal value)
    {
      if (value < Decimal.Zero)
        return -1;
      return value > Decimal.Zero ? 1 : 0;
    }

    /// <summary>Умножает два 32-битовых числа.</summary>
    /// <param name="a">Первое число для умножения.</param>
    /// <param name="b">Второе число для умножения.</param>
    /// <returns>Число, являющееся произведением указанных чисел.</returns>
    public static long BigMul(int a, int b)
    {
      return (long) a * (long) b;
    }

    /// <summary>
    ///   Вычисляет частное двух 32-битовых целых чисел со знаком и возвращает остаток в выходном параметре.
    /// </summary>
    /// <param name="a">Делимое.</param>
    /// <param name="b">Делитель.</param>
    /// <param name="result">Остаток.</param>
    /// <returns>Частное от деления указанных чисел.</returns>
    /// <exception cref="T:System.DivideByZeroException">
    ///   <paramref name="b" /> равен нулю.
    /// </exception>
    public static int DivRem(int a, int b, out int result)
    {
      result = a % b;
      return a / b;
    }

    /// <summary>
    ///   Вычисляет частное двух 64-битовых целых чисел со знаком и возвращает остаток в выходном параметре.
    /// </summary>
    /// <param name="a">Делимое.</param>
    /// <param name="b">Делитель.</param>
    /// <param name="result">Остаток.</param>
    /// <returns>Частное от деления указанных чисел.</returns>
    /// <exception cref="T:System.DivideByZeroException">
    ///   <paramref name="b" /> равен нулю.
    /// </exception>
    public static long DivRem(long a, long b, out long result)
    {
      result = a % b;
      return a / b;
    }
  }
}

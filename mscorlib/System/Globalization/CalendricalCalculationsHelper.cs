// Decompiled with JetBrains decompiler
// Type: System.Globalization.CalendricalCalculationsHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Globalization
{
  internal class CalendricalCalculationsHelper
  {
    private static long StartOf1810 = CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(1810, 1, 1));
    private static long StartOf1900Century = CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(1900, 1, 1));
    private static double[] Coefficients1900to1987 = new double[8]
    {
      -2E-05,
      0.000297,
      0.025184,
      -0.181133,
      0.55304,
      -0.861938,
      0.677066,
      -0.212591
    };
    private static double[] Coefficients1800to1899 = new double[11]
    {
      -9E-06,
      0.003844,
      0.083563,
      0.865736,
      4.867575,
      15.845535,
      31.332267,
      38.291999,
      28.316289,
      11.636204,
      2.043794
    };
    private static double[] Coefficients1700to1799 = new double[4]
    {
      8.118780842,
      -0.005092142,
      0.003336121,
      -2.66484E-05
    };
    private static double[] Coefficients1620to1699 = new double[3]
    {
      196.58333,
      -1627.0 / 400.0,
      0.0219167
    };
    private static double[] LambdaCoefficients = new double[3]
    {
      280.46645,
      36000.76983,
      0.0003032
    };
    private static double[] AnomalyCoefficients = new double[4]
    {
      357.5291,
      35999.0503,
      -0.0001559,
      -4.8E-07
    };
    private static double[] EccentricityCoefficients = new double[3]
    {
      0.016708617,
      -4.2037E-05,
      -1.236E-07
    };
    private static double[] Coefficients = new double[4]
    {
      CalendricalCalculationsHelper.Angle(23, 26, 21.448),
      CalendricalCalculationsHelper.Angle(0, 0, -46.815),
      CalendricalCalculationsHelper.Angle(0, 0, -0.00059),
      CalendricalCalculationsHelper.Angle(0, 0, 0.001813)
    };
    private static double[] CoefficientsA = new double[3]
    {
      124.9,
      -1934.134,
      0.002063
    };
    private static double[] CoefficientsB = new double[3]
    {
      201.11,
      72001.5377,
      0.00057
    };
    private static CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap[] EphemerisCorrectionTable = new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap[7]
    {
      new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(2020, CalendricalCalculationsHelper.CorrectionAlgorithm.Default),
      new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1988, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1988to2019),
      new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1900, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1900to1987),
      new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1800, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1800to1899),
      new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1700, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1700to1799),
      new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(1620, CalendricalCalculationsHelper.CorrectionAlgorithm.Year1620to1699),
      new CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap(int.MinValue, CalendricalCalculationsHelper.CorrectionAlgorithm.Default)
    };
    private const double FullCircleOfArc = 360.0;
    private const int HalfCircleOfArc = 180;
    private const double TwelveHours = 0.5;
    private const double Noon2000Jan01 = 730120.5;
    internal const double MeanTropicalYearInDays = 365.242189;
    private const double MeanSpeedOfSun = 1.01456163611111;
    private const double LongitudeSpring = 0.0;
    private const double TwoDegreesAfterSpring = 2.0;
    private const int SecondsPerDay = 86400;
    private const int DaysInUniformLengthCentury = 36525;
    private const int SecondsPerMinute = 60;
    private const int MinutesPerDegree = 60;

    private static double RadiansFromDegrees(double degree)
    {
      return degree * Math.PI / 180.0;
    }

    private static double SinOfDegree(double degree)
    {
      return Math.Sin(CalendricalCalculationsHelper.RadiansFromDegrees(degree));
    }

    private static double CosOfDegree(double degree)
    {
      return Math.Cos(CalendricalCalculationsHelper.RadiansFromDegrees(degree));
    }

    private static double TanOfDegree(double degree)
    {
      return Math.Tan(CalendricalCalculationsHelper.RadiansFromDegrees(degree));
    }

    public static double Angle(int degrees, int minutes, double seconds)
    {
      return (seconds / 60.0 + (double) minutes) / 60.0 + (double) degrees;
    }

    private static double Obliquity(double julianCenturies)
    {
      return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients, julianCenturies);
    }

    internal static long GetNumberOfDays(DateTime date)
    {
      return date.Ticks / 864000000000L;
    }

    private static int GetGregorianYear(double numberOfDays)
    {
      return new DateTime(Math.Min((long) (Math.Floor(numberOfDays) * 864000000000.0), DateTime.MaxValue.Ticks)).Year;
    }

    private static double Reminder(double divisor, double dividend)
    {
      double num = Math.Floor(divisor / dividend);
      return divisor - dividend * num;
    }

    private static double NormalizeLongitude(double longitude)
    {
      longitude = CalendricalCalculationsHelper.Reminder(longitude, 360.0);
      if (longitude < 0.0)
        longitude += 360.0;
      return longitude;
    }

    public static double AsDayFraction(double longitude)
    {
      return longitude / 360.0;
    }

    private static double PolynomialSum(double[] coefficients, double indeterminate)
    {
      double coefficient = coefficients[0];
      double num = 1.0;
      for (int index = 1; index < coefficients.Length; ++index)
      {
        num *= indeterminate;
        coefficient += coefficients[index] * num;
      }
      return coefficient;
    }

    private static double CenturiesFrom1900(int gregorianYear)
    {
      return (double) (CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(gregorianYear, 7, 1)) - CalendricalCalculationsHelper.StartOf1900Century) / 36525.0;
    }

    private static double DefaultEphemerisCorrection(int gregorianYear)
    {
      return (Math.Pow(0.5 + (double) (CalendricalCalculationsHelper.GetNumberOfDays(new DateTime(gregorianYear, 1, 1)) - CalendricalCalculationsHelper.StartOf1810), 2.0) / 41048480.0 - 15.0) / 86400.0;
    }

    private static double EphemerisCorrection1988to2019(int gregorianYear)
    {
      return (double) (gregorianYear - 1933) / 86400.0;
    }

    private static double EphemerisCorrection1900to1987(int gregorianYear)
    {
      double indeterminate = CalendricalCalculationsHelper.CenturiesFrom1900(gregorianYear);
      return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1900to1987, indeterminate);
    }

    private static double EphemerisCorrection1800to1899(int gregorianYear)
    {
      double indeterminate = CalendricalCalculationsHelper.CenturiesFrom1900(gregorianYear);
      return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1800to1899, indeterminate);
    }

    private static double EphemerisCorrection1700to1799(int gregorianYear)
    {
      double indeterminate = (double) (gregorianYear - 1700);
      return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1700to1799, indeterminate) / 86400.0;
    }

    private static double EphemerisCorrection1620to1699(int gregorianYear)
    {
      double indeterminate = (double) (gregorianYear - 1600);
      return CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.Coefficients1620to1699, indeterminate) / 86400.0;
    }

    private static double EphemerisCorrection(double time)
    {
      int gregorianYear = CalendricalCalculationsHelper.GetGregorianYear(time);
      foreach (CalendricalCalculationsHelper.EphemerisCorrectionAlgorithmMap correctionAlgorithmMap in CalendricalCalculationsHelper.EphemerisCorrectionTable)
      {
        if (correctionAlgorithmMap._lowestYear <= gregorianYear)
        {
          switch (correctionAlgorithmMap._algorithm)
          {
            case CalendricalCalculationsHelper.CorrectionAlgorithm.Default:
              return CalendricalCalculationsHelper.DefaultEphemerisCorrection(gregorianYear);
            case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1988to2019:
              return CalendricalCalculationsHelper.EphemerisCorrection1988to2019(gregorianYear);
            case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1900to1987:
              return CalendricalCalculationsHelper.EphemerisCorrection1900to1987(gregorianYear);
            case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1800to1899:
              return CalendricalCalculationsHelper.EphemerisCorrection1800to1899(gregorianYear);
            case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1700to1799:
              return CalendricalCalculationsHelper.EphemerisCorrection1700to1799(gregorianYear);
            case CalendricalCalculationsHelper.CorrectionAlgorithm.Year1620to1699:
              return CalendricalCalculationsHelper.EphemerisCorrection1620to1699(gregorianYear);
            default:
              goto label_11;
          }
        }
      }
label_11:
      return CalendricalCalculationsHelper.DefaultEphemerisCorrection(gregorianYear);
    }

    public static double JulianCenturies(double moment)
    {
      return (moment + CalendricalCalculationsHelper.EphemerisCorrection(moment) - 730120.5) / 36525.0;
    }

    private static bool IsNegative(double value)
    {
      return Math.Sign(value) == -1;
    }

    private static double CopySign(double value, double sign)
    {
      if (CalendricalCalculationsHelper.IsNegative(value) != CalendricalCalculationsHelper.IsNegative(sign))
        return -value;
      return value;
    }

    private static double EquationOfTime(double time)
    {
      double num1 = CalendricalCalculationsHelper.JulianCenturies(time);
      double num2 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.LambdaCoefficients, num1);
      double degree = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.AnomalyCoefficients, num1);
      double x1 = CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.EccentricityCoefficients, num1);
      double num3 = CalendricalCalculationsHelper.TanOfDegree(CalendricalCalculationsHelper.Obliquity(num1) / 2.0);
      double x2 = num3 * num3;
      double sign = (x2 * CalendricalCalculationsHelper.SinOfDegree(2.0 * num2) - 2.0 * x1 * CalendricalCalculationsHelper.SinOfDegree(degree) + 4.0 * x1 * x2 * CalendricalCalculationsHelper.SinOfDegree(degree) * CalendricalCalculationsHelper.CosOfDegree(2.0 * num2) - 0.5 * Math.Pow(x2, 2.0) * CalendricalCalculationsHelper.SinOfDegree(4.0 * num2) - 1.25 * Math.Pow(x1, 2.0) * CalendricalCalculationsHelper.SinOfDegree(2.0 * degree)) / (2.0 * Math.PI);
      return CalendricalCalculationsHelper.CopySign(Math.Min(Math.Abs(sign), 0.5), sign);
    }

    private static double AsLocalTime(double apparentMidday, double longitude)
    {
      double time = apparentMidday - CalendricalCalculationsHelper.AsDayFraction(longitude);
      return apparentMidday - CalendricalCalculationsHelper.EquationOfTime(time);
    }

    public static double Midday(double date, double longitude)
    {
      return CalendricalCalculationsHelper.AsLocalTime(date + 0.5, longitude) - CalendricalCalculationsHelper.AsDayFraction(longitude);
    }

    private static double InitLongitude(double longitude)
    {
      return CalendricalCalculationsHelper.NormalizeLongitude(longitude + 180.0) - 180.0;
    }

    public static double MiddayAtPersianObservationSite(double date)
    {
      return CalendricalCalculationsHelper.Midday(date, CalendricalCalculationsHelper.InitLongitude(52.5));
    }

    private static double PeriodicTerm(double julianCenturies, int x, double y, double z)
    {
      return (double) x * CalendricalCalculationsHelper.SinOfDegree(y + z * julianCenturies);
    }

    private static double SumLongSequenceOfPeriodicTerms(double julianCenturies)
    {
      return 0.0 + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 403406, 270.54861, 0.9287892) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 195207, 340.19128, 35999.1376958) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 119433, 63.91854, 35999.4089666) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 112392, 331.2622, 35998.7287385) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 3891, 317.843, 71998.20261) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 2819, 86.631, 71998.4403) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 1721, 240.052, 36000.35726) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 660, 310.26, 71997.4812) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 350, 247.23, 32964.4678) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 334, 260.87, -19.441) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 314, 297.82, 445267.1117) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 268, 343.14, 45036.884) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 242, 166.79, 1938.0 / 625.0) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 234, 81.53, 22518.4434) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 158, 3.5, -19.9739) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 132, 132.75, 65928.9345) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 129, 182.95, 9038.0293) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 114, 162.03, 3034.7684) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 99, 29.8, 33718.148) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 93, 266.4, 3034.448) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 86, 249.2, -2280.773) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 78, 157.6, 29929.992) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 72, 257.8, 31556.493) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 68, 185.1, 149.588) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 64, 69.9, 9037.75) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 46, 8.0, 107997.405) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 38, 197.1, -4444.176) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 37, 250.4, 151.771) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 32, 65.3, 67555.316) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 29, 162.7, 31556.08) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 28, 341.5, -4561.54) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 27, 291.6, 107996.706) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 27, 98.5, 1221.655) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 25, 146.7, 62894.167) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 24, 110.0, 31437.369) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 21, 5.2, 14578.298) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 21, 342.6, -31931.757) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 20, 230.9, 34777.243) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 18, 256.1, 1221.999) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 17, 45.3, 62894.511) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 14, 242.9, -4442.039) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 13, 115.2, 107997.909) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 13, 151.8, 119.066) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 13, 285.3, 16859.071) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 12, 53.3, -4.578) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 126.6, 26895.292) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 205.7, -39.127) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 85.9, 12297.536) + CalendricalCalculationsHelper.PeriodicTerm(julianCenturies, 10, 146.1, 90073.778);
    }

    private static double Aberration(double julianCenturies)
    {
      return 9.74E-05 * CalendricalCalculationsHelper.CosOfDegree(177.63 + 35999.01848 * julianCenturies) - 0.005575;
    }

    private static double Nutation(double julianCenturies)
    {
      return -0.004778 * CalendricalCalculationsHelper.SinOfDegree(CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.CoefficientsA, julianCenturies)) - 0.0003667 * CalendricalCalculationsHelper.SinOfDegree(CalendricalCalculationsHelper.PolynomialSum(CalendricalCalculationsHelper.CoefficientsB, julianCenturies));
    }

    public static double Compute(double time)
    {
      double julianCenturies = CalendricalCalculationsHelper.JulianCenturies(time);
      return CalendricalCalculationsHelper.InitLongitude(282.7771834 + 36000.76953744 * julianCenturies + 5.72957795130823E-06 * CalendricalCalculationsHelper.SumLongSequenceOfPeriodicTerms(julianCenturies) + CalendricalCalculationsHelper.Aberration(julianCenturies) + CalendricalCalculationsHelper.Nutation(julianCenturies));
    }

    public static double AsSeason(double longitude)
    {
      if (longitude >= 0.0)
        return longitude;
      return longitude + 360.0;
    }

    private static double EstimatePrior(double longitude, double time)
    {
      double time1 = time - 1.01456163611111 * CalendricalCalculationsHelper.AsSeason(CalendricalCalculationsHelper.InitLongitude(CalendricalCalculationsHelper.Compute(time) - longitude));
      double num = CalendricalCalculationsHelper.InitLongitude(CalendricalCalculationsHelper.Compute(time1) - longitude);
      return Math.Min(time, time1 - 1.01456163611111 * num);
    }

    internal static long PersianNewYearOnOrBefore(long numberOfDays)
    {
      long num1 = (long) Math.Floor(CalendricalCalculationsHelper.EstimatePrior(0.0, CalendricalCalculationsHelper.MiddayAtPersianObservationSite((double) numberOfDays))) - 1L;
      long num2 = num1 + 3L;
      long num3;
      for (num3 = num1; num3 != num2; ++num3)
      {
        double num4 = CalendricalCalculationsHelper.Compute(CalendricalCalculationsHelper.MiddayAtPersianObservationSite((double) num3));
        if (0.0 <= num4 && num4 <= 2.0)
          break;
      }
      return num3 - 1L;
    }

    private enum CorrectionAlgorithm
    {
      Default,
      Year1988to2019,
      Year1900to1987,
      Year1800to1899,
      Year1700to1799,
      Year1620to1699,
    }

    private struct EphemerisCorrectionAlgorithmMap
    {
      internal int _lowestYear;
      internal CalendricalCalculationsHelper.CorrectionAlgorithm _algorithm;

      public EphemerisCorrectionAlgorithmMap(int year, CalendricalCalculationsHelper.CorrectionAlgorithm algorithm)
      {
        this._lowestYear = year;
        this._algorithm = algorithm;
      }
    }
  }
}

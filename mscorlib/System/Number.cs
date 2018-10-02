// Decompiled with JetBrains decompiler
// Type: System.Number
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;

namespace System
{
  [FriendAccessAllowed]
  internal class Number
  {
    private const int NumberMaxDigits = 50;
    private const int Int32Precision = 10;
    private const int UInt32Precision = 10;
    private const int Int64Precision = 19;
    private const int UInt64Precision = 20;

    private Number()
    {
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern string FormatDecimal(Decimal value, string format, NumberFormatInfo info);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern string FormatDouble(double value, string format, NumberFormatInfo info);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern string FormatInt32(int value, string format, NumberFormatInfo info);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern string FormatUInt32(uint value, string format, NumberFormatInfo info);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern string FormatInt64(long value, string format, NumberFormatInfo info);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern string FormatUInt64(ulong value, string format, NumberFormatInfo info);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern string FormatSingle(float value, string format, NumberFormatInfo info);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern unsafe bool NumberBufferToDecimal(byte* number, ref Decimal value);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern unsafe bool NumberBufferToDouble(byte* number, ref double value);

    [FriendAccessAllowed]
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern unsafe string FormatNumberBuffer(byte* number, string format, NumberFormatInfo info, char* allDigits);

    private static bool HexNumberToInt32(ref Number.NumberBuffer number, ref int value)
    {
      uint num = 0;
      bool uint32 = Number.HexNumberToUInt32(ref number, ref num);
      value = (int) num;
      return uint32;
    }

    private static bool HexNumberToInt64(ref Number.NumberBuffer number, ref long value)
    {
      ulong num = 0;
      bool uint64 = Number.HexNumberToUInt64(ref number, ref num);
      value = (long) num;
      return uint64;
    }

    [SecuritySafeCritical]
    private static unsafe bool HexNumberToUInt32(ref Number.NumberBuffer number, ref uint value)
    {
      int scale = number.scale;
      if (scale > 10 || scale < number.precision)
        return false;
      char* digits = number.digits;
      uint num1 = 0;
      while (--scale >= 0)
      {
        if (num1 > 268435455U)
          return false;
        num1 *= 16U;
        if (*digits != char.MinValue)
        {
          uint num2 = num1;
          if (*digits != char.MinValue)
          {
            if (*digits >= '0' && *digits <= '9')
              num2 += (uint) *digits - 48U;
            else if (*digits >= 'A' && *digits <= 'F')
              num2 += (uint) ((int) *digits - 65 + 10);
            else
              num2 += (uint) ((int) *digits - 97 + 10);
            digits += 2;
          }
          if (num2 < num1)
            return false;
          num1 = num2;
        }
      }
      value = num1;
      return true;
    }

    [SecuritySafeCritical]
    private static unsafe bool HexNumberToUInt64(ref Number.NumberBuffer number, ref ulong value)
    {
      int scale = number.scale;
      if (scale > 20 || scale < number.precision)
        return false;
      char* digits = number.digits;
      ulong num1 = 0;
      while (--scale >= 0)
      {
        if (num1 > 1152921504606846975UL)
          return false;
        num1 *= 16UL;
        if (*digits != char.MinValue)
        {
          ulong num2 = num1;
          if (*digits != char.MinValue)
          {
            if (*digits >= '0' && *digits <= '9')
              num2 += (ulong) ((int) *digits - 48);
            else if (*digits >= 'A' && *digits <= 'F')
              num2 += (ulong) ((int) *digits - 65 + 10);
            else
              num2 += (ulong) ((int) *digits - 97 + 10);
            digits += 2;
          }
          if (num2 < num1)
            return false;
          num1 = num2;
        }
      }
      value = num1;
      return true;
    }

    private static bool IsWhite(char ch)
    {
      if (ch == ' ')
        return true;
      if (ch >= '\t')
        return ch <= '\r';
      return false;
    }

    [SecuritySafeCritical]
    private static unsafe bool NumberToInt32(ref Number.NumberBuffer number, ref int value)
    {
      int scale = number.scale;
      if (scale > 10 || scale < number.precision)
        return false;
      char* digits = number.digits;
      int num = 0;
      while (--scale >= 0)
      {
        if ((uint) num > 214748364U)
          return false;
        num *= 10;
        if (*digits != char.MinValue)
          num += (int) *digits++ - 48;
      }
      if (number.sign)
      {
        num = -num;
        if (num > 0)
          return false;
      }
      else if (num < 0)
        return false;
      value = num;
      return true;
    }

    [SecuritySafeCritical]
    private static unsafe bool NumberToInt64(ref Number.NumberBuffer number, ref long value)
    {
      int scale = number.scale;
      if (scale > 19 || scale < number.precision)
        return false;
      char* digits = number.digits;
      long num = 0;
      while (--scale >= 0)
      {
        if ((ulong) num > 922337203685477580UL)
          return false;
        num *= 10L;
        if (*digits != char.MinValue)
          num += (long) ((int) *digits++ - 48);
      }
      if (number.sign)
      {
        num = -num;
        if (num > 0L)
          return false;
      }
      else if (num < 0L)
        return false;
      value = num;
      return true;
    }

    [SecuritySafeCritical]
    private static unsafe bool NumberToUInt32(ref Number.NumberBuffer number, ref uint value)
    {
      int scale = number.scale;
      if (scale > 10 || scale < number.precision || number.sign)
        return false;
      char* digits = number.digits;
      uint num1 = 0;
      while (--scale >= 0)
      {
        if (num1 > 429496729U)
          return false;
        num1 *= 10U;
        if (*digits != char.MinValue)
        {
          uint num2 = num1 + ((uint) *digits++ - 48U);
          if (num2 < num1)
            return false;
          num1 = num2;
        }
      }
      value = num1;
      return true;
    }

    [SecuritySafeCritical]
    private static unsafe bool NumberToUInt64(ref Number.NumberBuffer number, ref ulong value)
    {
      int scale = number.scale;
      if (scale > 20 || scale < number.precision || number.sign)
        return false;
      char* digits = number.digits;
      ulong num1 = 0;
      while (--scale >= 0)
      {
        if (num1 > 1844674407370955161UL)
          return false;
        num1 *= 10UL;
        if (*digits != char.MinValue)
        {
          ulong num2 = num1 + (ulong) ((int) *digits++ - 48);
          if (num2 < num1)
            return false;
          num1 = num2;
        }
      }
      value = num1;
      return true;
    }

    [SecurityCritical]
    private static unsafe char* MatchChars(char* p, string str)
    {
      string str1 = str;
      char* str2 = (char*) str1;
      if ((IntPtr) str2 != IntPtr.Zero)
        str2 += RuntimeHelpers.OffsetToStringData;
      return Number.MatchChars(p, str2);
    }

    [SecurityCritical]
    private static unsafe char* MatchChars(char* p, char* str)
    {
      if (*str == char.MinValue)
        return (char*) null;
      while (*str != char.MinValue)
      {
        if ((int) *p != (int) *str && (*str != ' ' || *p != ' '))
          return (char*) null;
        p += 2;
        str += 2;
      }
      return p;
    }

    [SecuritySafeCritical]
    internal static unsafe Decimal ParseDecimal(string value, NumberStyles options, NumberFormatInfo numfmt)
    {
      byte* stackBuffer = stackalloc byte[Number.NumberBuffer.NumberBufferBytes];
      Number.NumberBuffer number = new Number.NumberBuffer(stackBuffer);
      Decimal num = new Decimal();
      Number.StringToNumber(value, options, ref number, numfmt, true);
      if (!Number.NumberBufferToDecimal(number.PackForNative(), ref num))
        throw new OverflowException(Environment.GetResourceString("Overflow_Decimal"));
      return num;
    }

    [SecuritySafeCritical]
    internal static unsafe double ParseDouble(string value, NumberStyles options, NumberFormatInfo numfmt)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      byte* stackBuffer = stackalloc byte[Number.NumberBuffer.NumberBufferBytes];
      Number.NumberBuffer number = new Number.NumberBuffer(stackBuffer);
      double num = 0.0;
      if (!Number.TryStringToNumber(value, options, ref number, numfmt, false))
      {
        string str = value.Trim();
        if (str.Equals(numfmt.PositiveInfinitySymbol))
          return double.PositiveInfinity;
        if (str.Equals(numfmt.NegativeInfinitySymbol))
          return double.NegativeInfinity;
        if (str.Equals(numfmt.NaNSymbol))
          return double.NaN;
        throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
      }
      if (!Number.NumberBufferToDouble(number.PackForNative(), ref num))
        throw new OverflowException(Environment.GetResourceString("Overflow_Double"));
      return num;
    }

    [SecuritySafeCritical]
    internal static unsafe int ParseInt32(string s, NumberStyles style, NumberFormatInfo info)
    {
      byte* stackBuffer = stackalloc byte[Number.NumberBuffer.NumberBufferBytes];
      Number.NumberBuffer number = new Number.NumberBuffer(stackBuffer);
      int num = 0;
      Number.StringToNumber(s, style, ref number, info, false);
      if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
      {
        if (!Number.HexNumberToInt32(ref number, ref num))
          throw new OverflowException(Environment.GetResourceString("Overflow_Int32"));
      }
      else if (!Number.NumberToInt32(ref number, ref num))
        throw new OverflowException(Environment.GetResourceString("Overflow_Int32"));
      return num;
    }

    [SecuritySafeCritical]
    internal static unsafe long ParseInt64(string value, NumberStyles options, NumberFormatInfo numfmt)
    {
      byte* stackBuffer = stackalloc byte[Number.NumberBuffer.NumberBufferBytes];
      Number.NumberBuffer number = new Number.NumberBuffer(stackBuffer);
      long num = 0;
      Number.StringToNumber(value, options, ref number, numfmt, false);
      if ((options & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
      {
        if (!Number.HexNumberToInt64(ref number, ref num))
          throw new OverflowException(Environment.GetResourceString("Overflow_Int64"));
      }
      else if (!Number.NumberToInt64(ref number, ref num))
        throw new OverflowException(Environment.GetResourceString("Overflow_Int64"));
      return num;
    }

    [SecurityCritical]
    private static unsafe bool ParseNumber(ref char* str, NumberStyles options, ref Number.NumberBuffer number, StringBuilder sb, NumberFormatInfo numfmt, bool parseDecimal)
    {
      number.scale = 0;
      number.sign = false;
      string str1 = (string) null;
      string str2 = (string) null;
      string str3 = (string) null;
      string str4 = (string) null;
      bool flag1 = false;
      string decimalSeparator;
      string str5;
      if ((options & NumberStyles.AllowCurrencySymbol) != NumberStyles.None)
      {
        str1 = numfmt.CurrencySymbol;
        if (numfmt.ansiCurrencySymbol != null)
          str2 = numfmt.ansiCurrencySymbol;
        str3 = numfmt.NumberDecimalSeparator;
        str4 = numfmt.NumberGroupSeparator;
        decimalSeparator = numfmt.CurrencyDecimalSeparator;
        str5 = numfmt.CurrencyGroupSeparator;
        flag1 = true;
      }
      else
      {
        decimalSeparator = numfmt.NumberDecimalSeparator;
        str5 = numfmt.NumberGroupSeparator;
      }
      int num1 = 0;
      bool flag2 = sb != null;
      bool flag3 = flag2 && (uint) (options & NumberStyles.AllowHexSpecifier) > 0U;
      int num2 = flag2 ? int.MaxValue : 50;
      char* p = str;
      char ch = *p;
      while (true)
      {
        if (!Number.IsWhite(ch) || (options & NumberStyles.AllowLeadingWhite) == NumberStyles.None || (num1 & 1) != 0 && ((num1 & 1) == 0 || (num1 & 32) == 0 && numfmt.numberNegativePattern != 2))
        {
          bool flag4;
          char* chPtr1;
          if ((flag4 = (options & NumberStyles.AllowLeadingSign) != NumberStyles.None && (num1 & 1) == 0) && (IntPtr) (chPtr1 = Number.MatchChars(p, numfmt.positiveSign)) != IntPtr.Zero)
          {
            num1 |= 1;
            p = (char*) ((IntPtr) chPtr1 - 2);
          }
          else
          {
            char* chPtr2;
            if (flag4 && (IntPtr) (chPtr2 = Number.MatchChars(p, numfmt.negativeSign)) != IntPtr.Zero)
            {
              num1 |= 1;
              number.sign = true;
              p = (char*) ((IntPtr) chPtr2 - 2);
            }
            else if (ch == '(' && (options & NumberStyles.AllowParentheses) != NumberStyles.None && (num1 & 1) == 0)
            {
              num1 |= 3;
              number.sign = true;
            }
            else
            {
              char* chPtr3;
              if (str1 != null && (IntPtr) (chPtr3 = Number.MatchChars(p, str1)) != IntPtr.Zero || str2 != null && (IntPtr) (chPtr3 = Number.MatchChars(p, str2)) != IntPtr.Zero)
              {
                num1 |= 32;
                str1 = (string) null;
                str2 = (string) null;
                p = (char*) ((IntPtr) chPtr3 - 2);
              }
              else
                break;
            }
          }
        }
        ch = *(p += 2);
      }
      int num3 = 0;
      int index = 0;
      while (true)
      {
        if (ch >= '0' && ch <= '9' || (options & NumberStyles.AllowHexSpecifier) != NumberStyles.None && (ch >= 'a' && ch <= 'f' || ch >= 'A' && ch <= 'F'))
        {
          num1 |= 4;
          if (((ch != '0' ? 1 : ((uint) (num1 & 8) > 0U ? 1 : 0)) | (flag3 ? 1 : 0)) != 0)
          {
            if (num3 < num2)
            {
              if (flag2)
                sb.Append(ch);
              else
                number.digits[num3++] = ch;
              if (ch != '0' | parseDecimal)
                index = num3;
            }
            if ((num1 & 16) == 0)
              ++number.scale;
            num1 |= 8;
          }
          else if ((num1 & 16) != 0)
            --number.scale;
        }
        else
        {
          char* chPtr1;
          if ((options & NumberStyles.AllowDecimalPoint) != NumberStyles.None && (num1 & 16) == 0 && ((IntPtr) (chPtr1 = Number.MatchChars(p, decimalSeparator)) != IntPtr.Zero || flag1 && (num1 & 32) == 0 && (IntPtr) (chPtr1 = Number.MatchChars(p, str3)) != IntPtr.Zero))
          {
            num1 |= 16;
            p = (char*) ((IntPtr) chPtr1 - 2);
          }
          else
          {
            char* chPtr2;
            if ((options & NumberStyles.AllowThousands) != NumberStyles.None && (num1 & 4) != 0 && (num1 & 16) == 0 && ((IntPtr) (chPtr2 = Number.MatchChars(p, str5)) != IntPtr.Zero || flag1 && (num1 & 32) == 0 && (IntPtr) (chPtr2 = Number.MatchChars(p, str4)) != IntPtr.Zero))
              p = (char*) ((IntPtr) chPtr2 - 2);
            else
              break;
          }
        }
        ch = *(p += 2);
      }
      bool flag5 = false;
      number.precision = index;
      if (flag2)
        sb.Append(char.MinValue);
      else
        number.digits[index] = char.MinValue;
      if ((num1 & 4) != 0)
      {
        if ((ch == 'E' || ch == 'e') && (options & NumberStyles.AllowExponent) != NumberStyles.None)
        {
          char* chPtr1 = p;
          ch = *(p += 2);
          char* chPtr2;
          if ((IntPtr) (chPtr2 = Number.MatchChars(p, numfmt.positiveSign)) != IntPtr.Zero)
          {
            ch = *(p = chPtr2);
          }
          else
          {
            char* chPtr3;
            if ((IntPtr) (chPtr3 = Number.MatchChars(p, numfmt.negativeSign)) != IntPtr.Zero)
            {
              ch = *(p = chPtr3);
              flag5 = true;
            }
          }
          if (ch >= '0' && ch <= '9')
          {
            int num4 = 0;
            do
            {
              num4 = num4 * 10 + ((int) ch - 48);
              ch = *(p += 2);
              if (num4 > 1000)
              {
                num4 = 9999;
                while (ch >= '0' && ch <= '9')
                  ch = *(p += 2);
              }
            }
            while (ch >= '0' && ch <= '9');
            if (flag5)
              num4 = -num4;
            number.scale += num4;
          }
          else
          {
            p = chPtr1;
            ch = *p;
          }
        }
        while (true)
        {
          if (!Number.IsWhite(ch) || (options & NumberStyles.AllowTrailingWhite) == NumberStyles.None)
          {
            bool flag4;
            char* chPtr1;
            if ((flag4 = (options & NumberStyles.AllowTrailingSign) != NumberStyles.None && (num1 & 1) == 0) && (IntPtr) (chPtr1 = Number.MatchChars(p, numfmt.positiveSign)) != IntPtr.Zero)
            {
              num1 |= 1;
              p = (char*) ((IntPtr) chPtr1 - 2);
            }
            else
            {
              char* chPtr2;
              if (flag4 && (IntPtr) (chPtr2 = Number.MatchChars(p, numfmt.negativeSign)) != IntPtr.Zero)
              {
                num1 |= 1;
                number.sign = true;
                p = (char*) ((IntPtr) chPtr2 - 2);
              }
              else if (ch == ')' && (num1 & 2) != 0)
              {
                num1 &= -3;
              }
              else
              {
                char* chPtr3;
                if (str1 != null && (IntPtr) (chPtr3 = Number.MatchChars(p, str1)) != IntPtr.Zero || str2 != null && (IntPtr) (chPtr3 = Number.MatchChars(p, str2)) != IntPtr.Zero)
                {
                  str1 = (string) null;
                  str2 = (string) null;
                  p = (char*) ((IntPtr) chPtr3 - 2);
                }
                else
                  break;
              }
            }
          }
          ch = *(p += 2);
        }
        if ((num1 & 2) == 0)
        {
          if ((num1 & 8) == 0)
          {
            if (!parseDecimal)
              number.scale = 0;
            if ((num1 & 16) == 0)
              number.sign = false;
          }
          str = p;
          return true;
        }
      }
      str = p;
      return false;
    }

    [SecuritySafeCritical]
    internal static unsafe float ParseSingle(string value, NumberStyles options, NumberFormatInfo numfmt)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      byte* stackBuffer = stackalloc byte[Number.NumberBuffer.NumberBufferBytes];
      Number.NumberBuffer number = new Number.NumberBuffer(stackBuffer);
      double num = 0.0;
      if (!Number.TryStringToNumber(value, options, ref number, numfmt, false))
      {
        string str = value.Trim();
        if (str.Equals(numfmt.PositiveInfinitySymbol))
          return float.PositiveInfinity;
        if (str.Equals(numfmt.NegativeInfinitySymbol))
          return float.NegativeInfinity;
        if (str.Equals(numfmt.NaNSymbol))
          return float.NaN;
        throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
      }
      if (!Number.NumberBufferToDouble(number.PackForNative(), ref num))
        throw new OverflowException(Environment.GetResourceString("Overflow_Single"));
      float f = (float) num;
      if (float.IsInfinity(f))
        throw new OverflowException(Environment.GetResourceString("Overflow_Single"));
      return f;
    }

    [SecuritySafeCritical]
    internal static unsafe uint ParseUInt32(string value, NumberStyles options, NumberFormatInfo numfmt)
    {
      byte* stackBuffer = stackalloc byte[Number.NumberBuffer.NumberBufferBytes];
      Number.NumberBuffer number = new Number.NumberBuffer(stackBuffer);
      uint num = 0;
      Number.StringToNumber(value, options, ref number, numfmt, false);
      if ((options & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
      {
        if (!Number.HexNumberToUInt32(ref number, ref num))
          throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
      }
      else if (!Number.NumberToUInt32(ref number, ref num))
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt32"));
      return num;
    }

    [SecuritySafeCritical]
    internal static unsafe ulong ParseUInt64(string value, NumberStyles options, NumberFormatInfo numfmt)
    {
      byte* stackBuffer = stackalloc byte[Number.NumberBuffer.NumberBufferBytes];
      Number.NumberBuffer number = new Number.NumberBuffer(stackBuffer);
      ulong num = 0;
      Number.StringToNumber(value, options, ref number, numfmt, false);
      if ((options & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
      {
        if (!Number.HexNumberToUInt64(ref number, ref num))
          throw new OverflowException(Environment.GetResourceString("Overflow_UInt64"));
      }
      else if (!Number.NumberToUInt64(ref number, ref num))
        throw new OverflowException(Environment.GetResourceString("Overflow_UInt64"));
      return num;
    }

    [SecuritySafeCritical]
    private static unsafe void StringToNumber(string str, NumberStyles options, ref Number.NumberBuffer number, NumberFormatInfo info, bool parseDecimal)
    {
      if (str == null)
        throw new ArgumentNullException("String");
      string str1 = str;
      char* chPtr = (char*) str1;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      char* str2 = chPtr;
      if (!Number.ParseNumber(ref str2, options, ref number, (StringBuilder) null, info, parseDecimal) || str2 - chPtr < (long) str.Length && !Number.TrailingZeros(str, (int) (str2 - chPtr)))
        throw new FormatException(Environment.GetResourceString("Format_InvalidString"));
      str1 = (string) null;
    }

    private static bool TrailingZeros(string s, int index)
    {
      for (int index1 = index; index1 < s.Length; ++index1)
      {
        if (s[index1] != char.MinValue)
          return false;
      }
      return true;
    }

    [SecuritySafeCritical]
    internal static unsafe bool TryParseDecimal(string value, NumberStyles options, NumberFormatInfo numfmt, out Decimal result)
    {
      byte* stackBuffer = stackalloc byte[Number.NumberBuffer.NumberBufferBytes];
      Number.NumberBuffer number = new Number.NumberBuffer(stackBuffer);
      result = new Decimal();
      return Number.TryStringToNumber(value, options, ref number, numfmt, true) && Number.NumberBufferToDecimal(number.PackForNative(), ref result);
    }

    [SecuritySafeCritical]
    internal static unsafe bool TryParseDouble(string value, NumberStyles options, NumberFormatInfo numfmt, out double result)
    {
      byte* stackBuffer = stackalloc byte[Number.NumberBuffer.NumberBufferBytes];
      Number.NumberBuffer number = new Number.NumberBuffer(stackBuffer);
      result = 0.0;
      return Number.TryStringToNumber(value, options, ref number, numfmt, false) && Number.NumberBufferToDouble(number.PackForNative(), ref result);
    }

    [SecuritySafeCritical]
    internal static unsafe bool TryParseInt32(string s, NumberStyles style, NumberFormatInfo info, out int result)
    {
      byte* stackBuffer = stackalloc byte[Number.NumberBuffer.NumberBufferBytes];
      Number.NumberBuffer number = new Number.NumberBuffer(stackBuffer);
      result = 0;
      if (!Number.TryStringToNumber(s, style, ref number, info, false))
        return false;
      if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
      {
        if (!Number.HexNumberToInt32(ref number, ref result))
          return false;
      }
      else if (!Number.NumberToInt32(ref number, ref result))
        return false;
      return true;
    }

    [SecuritySafeCritical]
    internal static unsafe bool TryParseInt64(string s, NumberStyles style, NumberFormatInfo info, out long result)
    {
      byte* stackBuffer = stackalloc byte[Number.NumberBuffer.NumberBufferBytes];
      Number.NumberBuffer number = new Number.NumberBuffer(stackBuffer);
      result = 0L;
      if (!Number.TryStringToNumber(s, style, ref number, info, false))
        return false;
      if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
      {
        if (!Number.HexNumberToInt64(ref number, ref result))
          return false;
      }
      else if (!Number.NumberToInt64(ref number, ref result))
        return false;
      return true;
    }

    [SecuritySafeCritical]
    internal static unsafe bool TryParseSingle(string value, NumberStyles options, NumberFormatInfo numfmt, out float result)
    {
      byte* stackBuffer = stackalloc byte[Number.NumberBuffer.NumberBufferBytes];
      Number.NumberBuffer number = new Number.NumberBuffer(stackBuffer);
      result = 0.0f;
      double num = 0.0;
      if (!Number.TryStringToNumber(value, options, ref number, numfmt, false) || !Number.NumberBufferToDouble(number.PackForNative(), ref num))
        return false;
      float f = (float) num;
      if (float.IsInfinity(f))
        return false;
      result = f;
      return true;
    }

    [SecuritySafeCritical]
    internal static unsafe bool TryParseUInt32(string s, NumberStyles style, NumberFormatInfo info, out uint result)
    {
      byte* stackBuffer = stackalloc byte[Number.NumberBuffer.NumberBufferBytes];
      Number.NumberBuffer number = new Number.NumberBuffer(stackBuffer);
      result = 0U;
      if (!Number.TryStringToNumber(s, style, ref number, info, false))
        return false;
      if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
      {
        if (!Number.HexNumberToUInt32(ref number, ref result))
          return false;
      }
      else if (!Number.NumberToUInt32(ref number, ref result))
        return false;
      return true;
    }

    [SecuritySafeCritical]
    internal static unsafe bool TryParseUInt64(string s, NumberStyles style, NumberFormatInfo info, out ulong result)
    {
      byte* stackBuffer = stackalloc byte[Number.NumberBuffer.NumberBufferBytes];
      Number.NumberBuffer number = new Number.NumberBuffer(stackBuffer);
      result = 0UL;
      if (!Number.TryStringToNumber(s, style, ref number, info, false))
        return false;
      if ((style & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
      {
        if (!Number.HexNumberToUInt64(ref number, ref result))
          return false;
      }
      else if (!Number.NumberToUInt64(ref number, ref result))
        return false;
      return true;
    }

    internal static bool TryStringToNumber(string str, NumberStyles options, ref Number.NumberBuffer number, NumberFormatInfo numfmt, bool parseDecimal)
    {
      return Number.TryStringToNumber(str, options, ref number, (StringBuilder) null, numfmt, parseDecimal);
    }

    [SecuritySafeCritical]
    [FriendAccessAllowed]
    internal static unsafe bool TryStringToNumber(string str, NumberStyles options, ref Number.NumberBuffer number, StringBuilder sb, NumberFormatInfo numfmt, bool parseDecimal)
    {
      if (str == null)
        return false;
      string str1 = str;
      char* chPtr = (char*) str1;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      char* str2 = chPtr;
      if (!Number.ParseNumber(ref str2, options, ref number, sb, numfmt, parseDecimal) || str2 - chPtr < (long) str.Length && !Number.TrailingZeros(str, (int) (str2 - chPtr)))
        return false;
      str1 = (string) null;
      return true;
    }

    [FriendAccessAllowed]
    internal struct NumberBuffer
    {
      public static readonly int NumberBufferBytes = 114 + IntPtr.Size;
      [SecurityCritical]
      private unsafe byte* baseAddress;
      [SecurityCritical]
      public unsafe char* digits;
      public int precision;
      public int scale;
      public bool sign;

      [SecurityCritical]
      public unsafe NumberBuffer(byte* stackBuffer)
      {
        this.baseAddress = stackBuffer;
        this.digits = (char*) (stackBuffer + (new IntPtr(6) * 2).ToInt64());
        this.precision = 0;
        this.scale = 0;
        this.sign = false;
      }

      [SecurityCritical]
      public unsafe byte* PackForNative()
      {
        int* baseAddress = (int*) this.baseAddress;
        *baseAddress = this.precision;
        *(int*) ((IntPtr) baseAddress + 4) = this.scale;
        baseAddress[2] = this.sign ? 1 : 0;
        return this.baseAddress;
      }
    }
  }
}

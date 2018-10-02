// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDuration
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
  /// <summary>
  ///   Предоставляет статические методы для сериализации и десериализации <see cref="T:System.TimeSpan" /> строку, форматируется как XSD <see langword="duration" />.
  /// </summary>
  [ComVisible(true)]
  public sealed class SoapDuration
  {
    /// <summary>
    ///   Возвращает язык определения схемы XML (XSD) текущего типа SOAP.
    /// </summary>
    /// <returns>
    ///   Значение <see cref="T:System.String" /> указывает XSD текущего типа SOAP.
    /// </returns>
    public static string XsdType
    {
      get
      {
        return "duration";
      }
    }

    private static void CarryOver(int inDays, out int years, out int months, out int days)
    {
      years = inDays / 360;
      int num1 = years * 360;
      months = Math.Max(0, inDays - num1) / 30;
      int num2 = months * 30;
      days = Math.Max(0, inDays - (num1 + num2));
      days = inDays % 30;
    }

    /// <summary>
    ///   Возвращает указанный <see cref="T:System.TimeSpan" /> объекта в виде <see cref="T:System.String" />.
    /// </summary>
    /// <param name="timeSpan">
    ///   <see cref="T:System.TimeSpan" /> Преобразуемый объект.
    /// </param>
    /// <returns>
    ///   A <see cref="T:System.String" /> представление <paramref name="timeSpan" /> в формате «PxxYxxDTxxHxxMxx.xxxS» или «PxxYxxDTxxHxxMxxS».
    ///    «PxxYxxDTxxHxxMxx.xxxS» используется в том случае, если <see cref="P:System.TimeSpan.Milliseconds" /> не равна нулю.
    /// </returns>
    [SecuritySafeCritical]
    public static string ToString(TimeSpan timeSpan)
    {
      StringBuilder stringBuilder = new StringBuilder(10);
      stringBuilder.Length = 0;
      if (TimeSpan.Compare(timeSpan, TimeSpan.Zero) < 1)
        stringBuilder.Append('-');
      int years = 0;
      int months = 0;
      int days = 0;
      SoapDuration.CarryOver(Math.Abs(timeSpan.Days), out years, out months, out days);
      stringBuilder.Append('P');
      stringBuilder.Append(years);
      stringBuilder.Append('Y');
      stringBuilder.Append(months);
      stringBuilder.Append('M');
      stringBuilder.Append(days);
      stringBuilder.Append("DT");
      stringBuilder.Append(Math.Abs(timeSpan.Hours));
      stringBuilder.Append('H');
      stringBuilder.Append(Math.Abs(timeSpan.Minutes));
      stringBuilder.Append('M');
      stringBuilder.Append(Math.Abs(timeSpan.Seconds));
      int l = (int) (Math.Abs(timeSpan.Ticks % 864000000000L) % 10000000L);
      if (l != 0)
      {
        string str = ParseNumbers.IntToString(l, 10, 7, '0', 0);
        stringBuilder.Append('.');
        stringBuilder.Append(str);
      }
      stringBuilder.Append('S');
      return stringBuilder.ToString();
    }

    /// <summary>
    ///   Преобразует указанный <see cref="T:System.String" /> в <see cref="T:System.TimeSpan" /> объекта.
    /// </summary>
    /// <param name="value">
    ///   Преобразуемый объект <see cref="T:System.String" />.
    /// </param>
    /// <returns>
    ///   Объект <see cref="T:System.TimeSpan" /> объект, который получен из <paramref name="value" />.
    /// </returns>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   <paramref name="value" /> не содержит дату и время, соответствующие каким-либо распознаваемым шаблонам формата.
    /// </exception>
    public static TimeSpan Parse(string value)
    {
      int num = 1;
      try
      {
        if (value == null)
          return TimeSpan.Zero;
        if (value[0] == '-')
          num = -1;
        char[] charArray = value.ToCharArray();
        int[] numArray = new int[7];
        string s1 = "0";
        string s2 = "0";
        string s3 = "0";
        string s4 = "0";
        string s5 = "0";
        string str1 = "0";
        string str2 = "0";
        bool flag1 = false;
        bool flag2 = false;
        int startIndex = 0;
        for (int index = 0; index < charArray.Length; ++index)
        {
          switch (charArray[index])
          {
            case '.':
              flag2 = true;
              str1 = new string(charArray, startIndex, index - startIndex);
              startIndex = index + 1;
              break;
            case 'D':
              s3 = new string(charArray, startIndex, index - startIndex);
              startIndex = index + 1;
              break;
            case 'H':
              s4 = new string(charArray, startIndex, index - startIndex);
              startIndex = index + 1;
              break;
            case 'M':
              if (flag1)
                s5 = new string(charArray, startIndex, index - startIndex);
              else
                s2 = new string(charArray, startIndex, index - startIndex);
              startIndex = index + 1;
              break;
            case 'P':
              startIndex = index + 1;
              break;
            case 'S':
              if (!flag2)
              {
                str1 = new string(charArray, startIndex, index - startIndex);
                break;
              }
              str2 = new string(charArray, startIndex, index - startIndex);
              break;
            case 'T':
              flag1 = true;
              startIndex = index + 1;
              break;
            case 'Y':
              s1 = new string(charArray, startIndex, index - startIndex);
              startIndex = index + 1;
              break;
          }
        }
        return new TimeSpan((long) num * ((long.Parse(s1, (IFormatProvider) CultureInfo.InvariantCulture) * 360L + long.Parse(s2, (IFormatProvider) CultureInfo.InvariantCulture) * 30L + long.Parse(s3, (IFormatProvider) CultureInfo.InvariantCulture)) * 864000000000L + long.Parse(s4, (IFormatProvider) CultureInfo.InvariantCulture) * 36000000000L + long.Parse(s5, (IFormatProvider) CultureInfo.InvariantCulture) * 600000000L + Convert.ToInt64(double.Parse(str1 + "." + str2, (IFormatProvider) CultureInfo.InvariantCulture) * 10000000.0)));
      }
      catch (Exception ex)
      {
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_SOAPInteropxsdInvalid"), (object) "xsd:duration", (object) value));
      }
    }
  }
}

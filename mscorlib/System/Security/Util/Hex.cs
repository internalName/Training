// Decompiled with JetBrains decompiler
// Type: System.Security.Util.Hex
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Security.Util
{
  internal static class Hex
  {
    private static char HexDigit(int num)
    {
      return num < 10 ? (char) (num + 48) : (char) (num + 55);
    }

    public static string EncodeHexString(byte[] sArray)
    {
      string str = (string) null;
      if (sArray != null)
      {
        char[] chArray1 = new char[sArray.Length * 2];
        int index1 = 0;
        int num1 = 0;
        for (; index1 < sArray.Length; ++index1)
        {
          int num2 = ((int) sArray[index1] & 240) >> 4;
          char[] chArray2 = chArray1;
          int index2 = num1;
          int num3 = index2 + 1;
          int num4 = (int) Hex.HexDigit(num2);
          chArray2[index2] = (char) num4;
          int num5 = (int) sArray[index1] & 15;
          char[] chArray3 = chArray1;
          int index3 = num3;
          num1 = index3 + 1;
          int num6 = (int) Hex.HexDigit(num5);
          chArray3[index3] = (char) num6;
        }
        str = new string(chArray1);
      }
      return str;
    }

    internal static string EncodeHexStringFromInt(byte[] sArray)
    {
      string str = (string) null;
      if (sArray != null)
      {
        char[] chArray1 = new char[sArray.Length * 2];
        int length = sArray.Length;
        int num1 = 0;
        while (length-- > 0)
        {
          int num2 = ((int) sArray[length] & 240) >> 4;
          char[] chArray2 = chArray1;
          int index1 = num1;
          int num3 = index1 + 1;
          int num4 = (int) Hex.HexDigit(num2);
          chArray2[index1] = (char) num4;
          int num5 = (int) sArray[length] & 15;
          char[] chArray3 = chArray1;
          int index2 = num3;
          num1 = index2 + 1;
          int num6 = (int) Hex.HexDigit(num5);
          chArray3[index2] = (char) num6;
        }
        str = new string(chArray1);
      }
      return str;
    }

    public static int ConvertHexDigit(char val)
    {
      if (val <= '9' && val >= '0')
        return (int) val - 48;
      if (val >= 'a' && val <= 'f')
        return (int) val - 97 + 10;
      if (val >= 'A' && val <= 'F')
        return (int) val - 65 + 10;
      throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_Index"));
    }

    public static byte[] DecodeHexString(string hexString)
    {
      if (hexString == null)
        throw new ArgumentNullException(nameof (hexString));
      bool flag = false;
      int index1 = 0;
      int num1 = hexString.Length;
      if (num1 >= 2 && hexString[0] == '0' && (hexString[1] == 'x' || hexString[1] == 'X'))
      {
        num1 = hexString.Length - 2;
        index1 = 2;
      }
      if (num1 % 2 != 0 && num1 % 3 != 2)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHexFormat"));
      byte[] numArray;
      if (num1 >= 3 && hexString[index1 + 2] == ' ')
      {
        flag = true;
        numArray = new byte[num1 / 3 + 1];
      }
      else
        numArray = new byte[num1 / 2];
      int index2 = 0;
      while (index1 < hexString.Length)
      {
        int num2 = Hex.ConvertHexDigit(hexString[index1]);
        int num3 = Hex.ConvertHexDigit(hexString[index1 + 1]);
        numArray[index2] = (byte) (num3 | num2 << 4);
        if (flag)
          ++index1;
        index1 += 2;
        ++index2;
      }
      return numArray;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: Microsoft.Win32.OAVariantLib
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace Microsoft.Win32
{
  internal static class OAVariantLib
  {
    internal static readonly Type[] ClassTypes = new Type[23]
    {
      typeof (Empty),
      typeof (void),
      typeof (bool),
      typeof (char),
      typeof (sbyte),
      typeof (byte),
      typeof (short),
      typeof (ushort),
      typeof (int),
      typeof (uint),
      typeof (long),
      typeof (ulong),
      typeof (float),
      typeof (double),
      typeof (string),
      typeof (void),
      typeof (DateTime),
      typeof (TimeSpan),
      typeof (object),
      typeof (Decimal),
      null,
      typeof (Missing),
      typeof (DBNull)
    };
    public const int NoValueProp = 1;
    public const int AlphaBool = 2;
    public const int NoUserOverride = 4;
    public const int CalendarHijri = 8;
    public const int LocalBool = 16;
    private const int CV_OBJECT = 18;

    [SecurityCritical]
    internal static Variant ChangeType(Variant source, Type targetClass, short options, CultureInfo culture)
    {
      if (targetClass == (Type) null)
        throw new ArgumentNullException(nameof (targetClass));
      if (culture == null)
        throw new ArgumentNullException(nameof (culture));
      Variant result = new Variant();
      OAVariantLib.ChangeTypeEx(ref result, ref source, culture.LCID, targetClass.TypeHandle.Value, OAVariantLib.GetCVTypeFromClass(targetClass), options);
      return result;
    }

    private static int GetCVTypeFromClass(Type ctype)
    {
      int num = -1;
      for (int index = 0; index < OAVariantLib.ClassTypes.Length; ++index)
      {
        if (ctype.Equals(OAVariantLib.ClassTypes[index]))
        {
          num = index;
          break;
        }
      }
      if (num == -1)
        num = 18;
      return num;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void ChangeTypeEx(ref Variant result, ref Variant source, int lcid, IntPtr typeHandle, int cvType, short flags);
  }
}

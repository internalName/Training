// Decompiled with JetBrains decompiler
// Type: System.OleAutBinder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
  [Serializable]
  internal class OleAutBinder : DefaultBinder
  {
    [SecuritySafeCritical]
    public override object ChangeType(object value, Type type, CultureInfo cultureInfo)
    {
      Variant source = new Variant(value);
      if (cultureInfo == null)
        cultureInfo = CultureInfo.CurrentCulture;
      if (type.IsByRef)
        type = type.GetElementType();
      if (!type.IsPrimitive && type.IsInstanceOfType(value))
        return value;
      Type type1 = value.GetType();
      if (type.IsEnum && type1.IsPrimitive)
        return Enum.Parse(type, value.ToString());
      if (type1 == typeof (DBNull))
      {
        if (type == typeof (DBNull))
          return value;
        if (type.IsClass && type != typeof (object) || type.IsInterface)
          return (object) null;
      }
      try
      {
        return OAVariantLib.ChangeType(source, type, (short) 16, cultureInfo).ToObject();
      }
      catch (NotSupportedException ex)
      {
        throw new COMException(Environment.GetResourceString("Interop.COM_TypeMismatch"), -2147352571);
      }
    }
  }
}

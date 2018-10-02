// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.OptionalFieldAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
  /// <summary>
  ///   Указывает, что поле может отсутствовать из потока сериализации таким образом, <see cref="T:System.Runtime.Serialization.Formatters.Binary.BinaryFormatter" /> и <see cref="T:System.Runtime.Serialization.Formatters.Soap.SoapFormatter" /> не вызывает исключение.
  /// </summary>
  [AttributeUsage(AttributeTargets.Field, Inherited = false)]
  [ComVisible(true)]
  public sealed class OptionalFieldAttribute : Attribute
  {
    private int versionAdded = 1;

    /// <summary>Это свойство не используется и зарезервировано.</summary>
    /// <returns>Это свойство зарезервировано.</returns>
    public int VersionAdded
    {
      get
      {
        return this.versionAdded;
      }
      set
      {
        if (value < 1)
          throw new ArgumentException(Environment.GetResourceString("Serialization_OptionalFieldVersionValue"));
        this.versionAdded = value;
      }
    }
  }
}

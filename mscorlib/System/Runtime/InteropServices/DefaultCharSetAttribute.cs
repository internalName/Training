// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.DefaultCharSetAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает значение <see cref="T:System.Runtime.InteropServices.CharSet" /> перечисления.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Module, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class DefaultCharSetAttribute : Attribute
  {
    internal CharSet _CharSet;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.DefaultCharSetAttribute" /> заданным значением <see cref="T:System.Runtime.InteropServices.CharSet" />.
    /// </summary>
    /// <param name="charSet">
    ///   Одно из значений <see cref="T:System.Runtime.InteropServices.CharSet" />.
    /// </param>
    [__DynamicallyInvokable]
    public DefaultCharSetAttribute(CharSet charSet)
    {
      this._CharSet = charSet;
    }

    /// <summary>
    ///   Возвращает значение по умолчанию <see cref="T:System.Runtime.InteropServices.CharSet" /> для любого вызова <see cref="T:System.Runtime.InteropServices.DllImportAttribute" />.
    /// </summary>
    /// <returns>
    ///   Значение по умолчанию <see cref="T:System.Runtime.InteropServices.CharSet" /> для любого вызова <see cref="T:System.Runtime.InteropServices.DllImportAttribute" />.
    /// </returns>
    [__DynamicallyInvokable]
    public CharSet CharSet
    {
      [__DynamicallyInvokable] get
      {
        return this._CharSet;
      }
    }
  }
}

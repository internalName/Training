// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComAliasNameAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает псевдоним COM для типа параметра или поля.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
  [ComVisible(true)]
  public sealed class ComAliasNameAttribute : Attribute
  {
    internal string _val;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.ComAliasNameAttribute" /> класса с псевдонимом для поля атрибута или параметра.
    /// </summary>
    /// <param name="alias">
    ///   Псевдоним поля или параметра, как найти в библиотеке типов при импортировании.
    /// </param>
    public ComAliasNameAttribute(string alias)
    {
      this._val = alias;
    }

    /// <summary>
    ///   Возвращает псевдоним поля или параметра, обнаруженный в библиотеке типов при импортировании.
    /// </summary>
    /// <returns>
    ///   Псевдоним поля или параметра, как найти в библиотеке типов при импортировании.
    /// </returns>
    public string Value
    {
      get
      {
        return this._val;
      }
    }
  }
}

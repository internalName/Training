// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.TypeLibImportClassAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Определяет, какая <see cref="T:System.Type" /> использует интерфейс исключительным.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Interface, Inherited = false)]
  [ComVisible(true)]
  public sealed class TypeLibImportClassAttribute : Attribute
  {
    internal string _importClassName;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.TypeLibImportClassAttribute" /> указания класса <see cref="T:System.Type" /> использует интерфейс исключительным.
    /// </summary>
    /// <param name="importClass">
    ///   <see cref="T:System.Type" /> Объект, который использует интерфейс исключительным.
    /// </param>
    public TypeLibImportClassAttribute(Type importClass)
    {
      this._importClassName = importClass.ToString();
    }

    /// <summary>
    ///   Возвращает имя <see cref="T:System.Type" /> объект, который использует интерфейс исключительным.
    /// </summary>
    /// <returns>
    ///   Имя <see cref="T:System.Type" /> объект, который использует интерфейс исключительным.
    /// </returns>
    public string Value
    {
      get
      {
        return this._importClassName;
      }
    }
  }
}

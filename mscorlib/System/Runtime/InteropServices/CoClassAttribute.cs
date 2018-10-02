// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.CoClassAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Указывает идентификатор компонентного класса, импортированного из библиотеки типов.
  /// </summary>
  [AttributeUsage(AttributeTargets.Interface, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class CoClassAttribute : Attribute
  {
    internal Type _CoClass;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.CoClassAttribute" /> с идентификатором класса исходного компонентного класса.
    /// </summary>
    /// <param name="coClass">
    ///   Объект <see cref="T:System.Type" /> содержащий идентификатор исходного совместного класса.
    /// </param>
    [__DynamicallyInvokable]
    public CoClassAttribute(Type coClass)
    {
      this._CoClass = coClass;
    }

    /// <summary>
    ///   Возвращает идентификатор исходного совместного класса.
    /// </summary>
    /// <returns>
    ///   A <see cref="T:System.Type" /> содержащий идентификатор исходного совместного класса.
    /// </returns>
    [__DynamicallyInvokable]
    public Type CoClass
    {
      [__DynamicallyInvokable] get
      {
        return this._CoClass;
      }
    }
  }
}

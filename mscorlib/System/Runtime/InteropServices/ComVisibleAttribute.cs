// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComVisibleAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Управляет доступностью отдельного управляемого типа или члена либо всех типов в сборки для модели COM.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class ComVisibleAttribute : Attribute
  {
    internal bool _val;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see langword="ComVisibleAttribute" />.
    /// </summary>
    /// <param name="visibility">
    ///   <see langword="true" /> Чтобы указать, что тип видим для COM; в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="true" />.
    /// </param>
    [__DynamicallyInvokable]
    public ComVisibleAttribute(bool visibility)
    {
      this._val = visibility;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, отображается ли COM-типа.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если тип является видимым. в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="true" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool Value
    {
      [__DynamicallyInvokable] get
      {
        return this._val;
      }
    }
  }
}

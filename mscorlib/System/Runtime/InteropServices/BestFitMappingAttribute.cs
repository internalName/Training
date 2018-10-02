// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.BestFitMappingAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Определяет необходимость преобразования символов Юникода в наиболее подходящие символы ANSI.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class BestFitMappingAttribute : Attribute
  {
    internal bool _bestFitMapping;
    /// <summary>
    ///   Включает или отключает возникновение исключения при появлении несопоставимого символа Юникода, который преобразуется в символ ANSI "?" символов.
    /// </summary>
    [__DynamicallyInvokable]
    public bool ThrowOnUnmappableChar;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.InteropServices.BestFitMappingAttribute" /> класса присвоено значение <see cref="P:System.Runtime.InteropServices.BestFitMappingAttribute.BestFitMapping" /> Свойства.
    /// </summary>
    /// <param name="BestFitMapping">
    ///   <see langword="true" /> Чтобы указать, что наилучшее сопоставление включено; в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="true" />.
    /// </param>
    [__DynamicallyInvokable]
    public BestFitMappingAttribute(bool BestFitMapping)
    {
      this._bestFitMapping = BestFitMapping;
    }

    /// <summary>
    ///   Возвращает поведение наилучшего сопоставления при преобразовании знаков Юникода в символы ANSI.
    /// </summary>
    /// <returns>
    ///   <see langword="true" /> Если включено наилучшее сопоставление; в противном случае — <see langword="false" />.
    ///    Значение по умолчанию — <see langword="true" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool BestFitMapping
    {
      [__DynamicallyInvokable] get
      {
        return this._bestFitMapping;
      }
    }
  }
}

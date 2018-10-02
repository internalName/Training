// Decompiled with JetBrains decompiler
// Type: System.Security.AllowPartiallyTrustedCallersAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security
{
  /// <summary>
  ///   Позволяет частично доверенному коду вызывать сборку.
  ///    Без этого объявления использовать сборку могут только полностью доверенные вызывающие объекты.
  ///    Этот класс не наследуется.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class AllowPartiallyTrustedCallersAttribute : Attribute
  {
    private PartialTrustVisibilityLevel _visibilityLevel;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" />.
    /// </summary>
    [__DynamicallyInvokable]
    public AllowPartiallyTrustedCallersAttribute()
    {
    }

    /// <summary>
    ///   Возвращает или задает видимость частичного доверия по умолчанию для кода, помеченный атрибутом <see cref="T:System.Security.AllowPartiallyTrustedCallersAttribute" /> атрибут (APTCA).
    /// </summary>
    /// <returns>
    ///   Одно из значений перечисления.
    ///    Значение по умолчанию — <see cref="F:System.Security.PartialTrustVisibilityLevel.VisibleToAllHosts" />.
    /// </returns>
    public PartialTrustVisibilityLevel PartialTrustVisibilityLevel
    {
      get
      {
        return this._visibilityLevel;
      }
      set
      {
        this._visibilityLevel = value;
      }
    }
  }
}

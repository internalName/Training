// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.RuntimeFeature
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.CompilerServices
{
  /// <summary>
  ///   Класс, статический метод <see cref="M:System.Runtime.CompilerServices.RuntimeFeature.IsSupported(System.String)" /> которого проверяет, поддерживается ли указанный компонент средой CLR.
  /// </summary>
  public static class RuntimeFeature
  {
    /// <summary>Получает имя компонента переносимого PDB-файла.</summary>
    /// <returns>
    ///   Имя компонента переносимого PDB-файла.
    ///    Этот метод всегда возвращает строку "PortablePdb".
    /// </returns>
    public const string PortablePdb = "PortablePdb";

    /// <summary>
    ///   Определяет, поддерживается ли указанный компонент средой CLR.
    /// </summary>
    /// <param name="feature">Имя компонента.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="feature" /> поддерживается; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool IsSupported(string feature)
    {
      return feature == "PortablePdb";
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.ISafeSerializationData
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Serialization
{
  /// <summary>
  ///   Позволяет сериализацию данных пользовательского исключения в прозрачный с точки зрения безопасности код.
  /// </summary>
  public interface ISafeSerializationData
  {
    /// <summary>
    ///   Этот метод вызывается при десериализации экземпляра.
    /// </summary>
    /// <param name="deserialized">
    ///   Объект, содержащий сведения о состоянии экземпляра.
    /// </param>
    void CompleteDeserialization(object deserialized);
  }
}

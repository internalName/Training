// Decompiled with JetBrains decompiler
// Type: System.Reflection.AssemblySignatureKeyAttribute
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Reflection
{
  /// <summary>
  ///   Предоставляет более надежный алгоритм хэширования миграции из ключа строгого имени, более старые, проще большего размера ключа.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
  [__DynamicallyInvokable]
  public sealed class AssemblySignatureKeyAttribute : Attribute
  {
    private string _publicKey;
    private string _countersignature;

    /// <summary>
    ///   Создает новый экземпляр <see cref="T:System.Reflection.AssemblySignatureKeyAttribute" /> класса с помощью указанного открытого ключа и скрепляющая подпись.
    /// </summary>
    /// <param name="publicKey">Ключ, public или удостоверение.</param>
    /// <param name="countersignature">
    ///   Скрепляющая подпись, являющийся подпись ключевую часть ключа строгого имени.
    /// </param>
    [__DynamicallyInvokable]
    public AssemblySignatureKeyAttribute(string publicKey, string countersignature)
    {
      this._publicKey = publicKey;
      this._countersignature = countersignature;
    }

    /// <summary>
    ///   Возвращает открытый ключ для строгое имя, используемое для подписания сборки.
    /// </summary>
    /// <returns>Открытый ключ для этой сборки.</returns>
    [__DynamicallyInvokable]
    public string PublicKey
    {
      [__DynamicallyInvokable] get
      {
        return this._publicKey;
      }
    }

    /// <summary>
    ///   Возвращает скрепляющая подпись строгого имени для этой сборки.
    /// </summary>
    /// <returns>Для этого ключа подписи скрепляющая подпись.</returns>
    [__DynamicallyInvokable]
    public string Countersignature
    {
      [__DynamicallyInvokable] get
      {
        return this._countersignature;
      }
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Configuration.Assemblies.AssemblyHash
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Configuration.Assemblies
{
  /// <summary>
  ///   Представляет хэш-значение содержимого манифеста сборки.
  /// </summary>
  [ComVisible(true)]
  [Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
  [Serializable]
  public struct AssemblyHash : ICloneable
  {
    /// <summary>
    ///   Пустой объект <see cref="T:System.Configuration.Assemblies.AssemblyHash" />
    /// </summary>
    [Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
    public static readonly AssemblyHash Empty = new AssemblyHash(AssemblyHashAlgorithm.None, (byte[]) null);
    private AssemblyHashAlgorithm _Algorithm;
    private byte[] _Value;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Configuration.Assemblies.AssemblyHash" /> структуры с помощью указанного хэш-значение.
    ///    Хэш-алгоритм по умолчанию <see cref="F:System.Configuration.Assemblies.AssemblyHashAlgorithm.SHA1" />.
    /// </summary>
    /// <param name="value">Хэш-значение.</param>
    [Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
    public AssemblyHash(byte[] value)
    {
      this._Algorithm = AssemblyHashAlgorithm.SHA1;
      this._Value = (byte[]) null;
      if (value == null)
        return;
      int length = value.Length;
      this._Value = new byte[length];
      Array.Copy((Array) value, (Array) this._Value, length);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Configuration.Assemblies.AssemblyHash" /> структуры с помощью указанного хэш-алгоритма и хэш-значение.
    /// </summary>
    /// <param name="algorithm">
    ///   Алгоритм, используемый для создания хеша.
    ///    Значения для этого параметра берутся из <see cref="T:System.Configuration.Assemblies.AssemblyHashAlgorithm" /> перечисления.
    /// </param>
    /// <param name="value">Хэш-значение.</param>
    [Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
    public AssemblyHash(AssemblyHashAlgorithm algorithm, byte[] value)
    {
      this._Algorithm = algorithm;
      this._Value = (byte[]) null;
      if (value == null)
        return;
      int length = value.Length;
      this._Value = new byte[length];
      Array.Copy((Array) value, (Array) this._Value, length);
    }

    /// <summary>Возвращает или задает хэш-алгоритм.</summary>
    /// <returns>Алгоритм хеширования сборки.</returns>
    [Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
    public AssemblyHashAlgorithm Algorithm
    {
      get
      {
        return this._Algorithm;
      }
      set
      {
        this._Algorithm = value;
      }
    }

    /// <summary>Возвращает хэш-значение.</summary>
    /// <returns>Хэш-значение.</returns>
    [Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
    public byte[] GetValue()
    {
      return this._Value;
    }

    /// <summary>Задает хэш-значение.</summary>
    /// <param name="value">Хэш-значение.</param>
    [Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
    public void SetValue(byte[] value)
    {
      this._Value = value;
    }

    /// <summary>Клонирует данный объект.</summary>
    /// <returns>Точную копию данного объекта.</returns>
    [Obsolete("The AssemblyHash class has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
    public object Clone()
    {
      return (object) new AssemblyHash(this._Algorithm, this._Value);
    }
  }
}

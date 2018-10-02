// Decompiled with JetBrains decompiler
// Type: System.Reflection.Emit.SignatureToken
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
  /// <summary>
  ///   Представляет <see langword="Token" /> возвращенный метаданными для представления подписи.
  /// </summary>
  [ComVisible(true)]
  public struct SignatureToken
  {
    /// <summary>
    ///   Значение по умолчанию <see langword="SignatureToken" /> с <see cref="P:System.Reflection.Emit.SignatureToken.Token" /> значение 0.
    /// </summary>
    public static readonly SignatureToken Empty;
    internal int m_signature;
    internal ModuleBuilder m_moduleBuilder;

    internal SignatureToken(int str, ModuleBuilder mod)
    {
      this.m_signature = str;
      this.m_moduleBuilder = mod;
    }

    /// <summary>
    ///   Извлекает маркер метаданных для подписи локальной переменной для этого метода.
    /// </summary>
    /// <returns>
    ///   Только для чтения.
    ///    Извлекает маркер метаданных этой подписи.
    /// </returns>
    public int Token
    {
      get
      {
        return this.m_signature;
      }
    }

    /// <summary>Создает хэш-код для этой подписи.</summary>
    /// <returns>Возвращает хэш-код для этой подписи.</returns>
    public override int GetHashCode()
    {
      return this.m_signature;
    }

    /// <summary>
    ///   Проверяет, является ли указанный объект экземпляром <see langword="SignatureToken" /> и эквивалентен данному экземпляру.
    /// </summary>
    /// <param name="obj">
    ///   Объект, сравниваемый с этим <see langword="SignatureToken" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="obj" /> является экземпляром класса <see langword="SignatureToken" /> и равен этому объекту, в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      if (obj is SignatureToken)
        return this.Equals((SignatureToken) obj);
      return false;
    }

    /// <summary>
    ///   Указывает, равен ли текущий экземпляр указанному <see cref="T:System.Reflection.Emit.SignatureToken" />.
    /// </summary>
    /// <param name="obj">
    ///   <see cref="T:System.Reflection.Emit.SignatureToken" /> Для сравнения с текущим экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если значение <paramref name="obj" /> равно значению текущего экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    public bool Equals(SignatureToken obj)
    {
      return obj.m_signature == this.m_signature;
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.Reflection.Emit.SignatureToken" /> структуры равны.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Reflection.Emit.SignatureToken" /> Для сравнения с <paramref name="b" />.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Reflection.Emit.SignatureToken" /> Для сравнения с <paramref name="a" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения параметров <paramref name="a" /> и <paramref name="b" /> равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator ==(SignatureToken a, SignatureToken b)
    {
      return a.Equals(b);
    }

    /// <summary>
    ///   Указывает, является ли два <see cref="T:System.Reflection.Emit.SignatureToken" /> структуры не равны.
    /// </summary>
    /// <param name="a">
    ///   <see cref="T:System.Reflection.Emit.SignatureToken" /> Для сравнения с <paramref name="b" />.
    /// </param>
    /// <param name="b">
    ///   <see cref="T:System.Reflection.Emit.SignatureToken" /> Для сравнения с <paramref name="a" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если значения <paramref name="a" /> и <paramref name="b" /> не равны; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool operator !=(SignatureToken a, SignatureToken b)
    {
      return !(a == b);
    }
  }
}

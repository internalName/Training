﻿// Decompiled with JetBrains decompiler
// Type: System.Security.Cryptography.SHA384
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
  /// <summary>
  ///   Вычисляет <see cref="T:System.Security.Cryptography.SHA384" /> хэш для входных данных.
  /// </summary>
  [ComVisible(true)]
  public abstract class SHA384 : HashAlgorithm
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Cryptography.SHA384" />.
    /// </summary>
    protected SHA384()
    {
      this.HashSizeValue = 384;
    }

    /// <summary>
    ///   Создает экземпляр реализации по умолчанию <see cref="T:System.Security.Cryptography.SHA384" />.
    /// </summary>
    /// <returns>
    ///   Новый экземпляр класса <see cref="T:System.Security.Cryptography.SHA384" />.
    /// </returns>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Этот алгоритм был использован с включенным режимом FIPS, однако он не совместим с FIPS.
    /// </exception>
    public static SHA384 Create()
    {
      return SHA384.Create("System.Security.Cryptography.SHA384");
    }

    /// <summary>
    ///   Создает экземпляр заданной реализации <see cref="T:System.Security.Cryptography.SHA384" />.
    /// </summary>
    /// <param name="hashName">
    ///   Имя конкретной реализации <see cref="T:System.Security.Cryptography.SHA384" /> для использования.
    /// </param>
    /// <returns>
    ///   Новый экземпляр <see cref="T:System.Security.Cryptography.SHA384" /> с помощью заданной реализации.
    /// </returns>
    /// <exception cref="T:System.Reflection.TargetInvocationException">
    ///   Алгоритм, описание которого <paramref name="hashName" /> параметр использовался с включенным режимом федеральным стандартам обработки информации (FIPS), но не является FIPS-совместимым.
    /// </exception>
    public static SHA384 Create(string hashName)
    {
      return (SHA384) CryptoConfig.CreateFromName(hashName);
    }
  }
}

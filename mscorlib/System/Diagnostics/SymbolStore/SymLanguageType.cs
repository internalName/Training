// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.SymbolStore.SymLanguageType
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
  /// <summary>
  ///   Хранит открытые идентификаторы GUID для типов языков, используемые в хранилище символов.
  /// </summary>
  [ComVisible(true)]
  public class SymLanguageType
  {
    /// <summary>
    ///   Задает идентификатор GUID типа языка C, используемый в хранилище символов.
    /// </summary>
    public static readonly Guid C = new Guid(1671464724, (short) -969, (short) 4562, (byte) 144, (byte) 76, (byte) 0, (byte) 192, (byte) 79, (byte) 163, (byte) 2, (byte) 161);
    /// <summary>
    ///   Задает идентификатор GUID типа языка C++ для использования в хранилище символов.
    /// </summary>
    public static readonly Guid CPlusPlus = new Guid(974311607, (short) -15764, (short) 4560, (byte) 180, (byte) 66, (byte) 0, (byte) 160, (byte) 36, (byte) 74, (byte) 29, (byte) 210);
    /// <summary>
    ///   Задает идентификатор GUID типа языка C# для использования в хранилище символов.
    /// </summary>
    public static readonly Guid CSharp = new Guid(1062298360, (short) 1990, (short) 4563, (byte) 144, (byte) 83, (byte) 0, (byte) 192, (byte) 79, (byte) 163, (byte) 2, (byte) 161);
    /// <summary>
    ///   Задает идентификатор GUID типа основной язык, используемый в хранилище символов.
    /// </summary>
    public static readonly Guid Basic = new Guid(974311608, (short) -15764, (short) 4560, (byte) 180, (byte) 66, (byte) 0, (byte) 160, (byte) 36, (byte) 74, (byte) 29, (byte) 210);
    /// <summary>
    ///   Задает идентификатор GUID типа языка Java, используемый в хранилище символов.
    /// </summary>
    public static readonly Guid Java = new Guid(974311604, (short) -15764, (short) 4560, (byte) 180, (byte) 66, (byte) 0, (byte) 160, (byte) 36, (byte) 74, (byte) 29, (byte) 210);
    /// <summary>
    ///   Задает идентификатор GUID типа языка Cobol, используемый в хранилище символов.
    /// </summary>
    public static readonly Guid Cobol = new Guid(-1358664495, (short) -12063, (short) 4562, (byte) 151, (byte) 124, (byte) 0, (byte) 160, (byte) 201, (byte) 180, (byte) 213, (byte) 12);
    /// <summary>
    ///   Задает идентификатор GUID типа языка Pascal, используемый в хранилище символов.
    /// </summary>
    public static readonly Guid Pascal = new Guid(-1358664494, (short) -12063, (short) 4562, (byte) 151, (byte) 124, (byte) 0, (byte) 160, (byte) 201, (byte) 180, (byte) 213, (byte) 12);
    /// <summary>
    ///   Задает идентификатор GUID типа языка ILAssembly, используемый в хранилище символов.
    /// </summary>
    public static readonly Guid ILAssembly = new Guid(-1358664493, (short) -12063, (short) 4562, (byte) 151, (byte) 124, (byte) 0, (byte) 160, (byte) 201, (byte) 180, (byte) 213, (byte) 12);
    /// <summary>
    ///   Задает идентификатор GUID типа языка JScript, используемый в хранилище символов.
    /// </summary>
    public static readonly Guid JScript = new Guid(974311606, (short) -15764, (short) 4560, (byte) 180, (byte) 66, (byte) 0, (byte) 160, (byte) 36, (byte) 74, (byte) 29, (byte) 210);
    /// <summary>
    ///   Задает идентификатор GUID типа языка SMC, используемый в хранилище символов.
    /// </summary>
    public static readonly Guid SMC = new Guid(228302715, (short) 26129, (short) 4563, (byte) 189, (byte) 42, (byte) 0, (byte) 0, (byte) 248, (byte) 8, (byte) 73, (byte) 189);
    /// <summary>
    ///   Задает идентификатор GUID типа языка C++ для использования в хранилище символов.
    /// </summary>
    public static readonly Guid MCPlusPlus = new Guid(1261829608, (short) 1990, (short) 4563, (byte) 144, (byte) 83, (byte) 0, (byte) 192, (byte) 79, (byte) 163, (byte) 2, (byte) 161);
  }
}

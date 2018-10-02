// Decompiled with JetBrains decompiler
// Type: System.Runtime.ProfileOptimization
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime
{
  /// <summary>
  ///   Повышает производительность при запуске доменов приложения в приложениях, требующих компилятор just-in-time (JIT), выполняя фоновая компиляция методов, которые, скорее всего, должны быть выполнены на основе профилей, созданный во время предыдущих компиляций.
  /// </summary>
  public static class ProfileOptimization
  {
    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void InternalSetProfileRoot(string directoryPath);

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void InternalStartProfile(string profile, IntPtr ptrNativeAssemblyLoadContext);

    /// <summary>
    ///   Включает оптимизацию профилирования для текущего домена приложения и задает папку, где хранятся файлы профилей оптимизации.
    ///    На компьютере с одним ядром метод игнорируется.
    /// </summary>
    /// <param name="directoryPath">
    ///   Полный путь к папке, где хранятся файлы профилей для текущего домена приложения.
    /// </param>
    [SecurityCritical]
    public static void SetProfileRoot(string directoryPath)
    {
      ProfileOptimization.InternalSetProfileRoot(directoryPath);
    }

    /// <summary>
    ///   Начинается в момент компиляции (JIT) методов, которые ранее были записаны в файле указанного профиля в фоновом потоке.
    ///    Запускает процесс записи текущее использование метод, который позже перезаписывает файл указанного профиля.
    /// </summary>
    /// <param name="profile">Имя файла профиля для использования.</param>
    [SecurityCritical]
    public static void StartProfile(string profile)
    {
      ProfileOptimization.InternalStartProfile(profile, IntPtr.Zero);
    }
  }
}

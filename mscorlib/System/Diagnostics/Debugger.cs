// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Debugger
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Diagnostics
{
  /// <summary>
  ///   Разрешает взаимодействие с отладчиком.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  public sealed class Debugger
  {
    /// <summary>
    ///   Представляет категорию сообщения с константой по умолчанию.
    /// </summary>
    public static readonly string DefaultCategory;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.Debugger" />.
    /// </summary>
    [Obsolete("Do not create instances of the Debugger class.  Call the static methods directly on this type instead", true)]
    public Debugger()
    {
    }

    /// <summary>Сообщает присоединенному отладчику точку останова.</summary>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Класс <see cref="T:System.Security.Permissions.UIPermission" /> не задан для переключения на отладчик.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void Break()
    {
      if (!Debugger.IsAttached)
      {
        try
        {
          new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
        }
        catch (SecurityException ex)
        {
          return;
        }
      }
      Debugger.BreakInternal();
    }

    [SecuritySafeCritical]
    private static void BreakCanThrow()
    {
      if (!Debugger.IsAttached)
        new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
      Debugger.BreakInternal();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void BreakInternal();

    /// <summary>Присоединяет и запускает отладчик для процесса.</summary>
    /// <returns>
    ///   Значение <see langword="true" />, если отладчик успешно запущен или уже присоединен; в противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.Security.SecurityException">
    ///   <see cref="T:System.Security.Permissions.UIPermission" /> Не настроена для запуска отладчика.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static bool Launch()
    {
      if (Debugger.IsAttached)
        return true;
      try
      {
        new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
      }
      catch (SecurityException ex)
      {
        return false;
      }
      return Debugger.LaunchInternal();
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void NotifyOfCrossThreadDependencySlow()
    {
      Debugger.CustomNotification((ICustomDebuggerNotification) new Debugger.CrossThreadDependencyNotification());
    }

    /// <summary>
    ///   Уведомляет отладчик о том, что выполнение пойдет по пути, включающему зависимость между потоками.
    /// </summary>
    [ComVisible(false)]
    public static void NotifyOfCrossThreadDependency()
    {
      if (!Debugger.IsAttached)
        return;
      Debugger.NotifyOfCrossThreadDependencySlow();
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern bool LaunchInternal();

    /// <summary>
    ///   Получает значение, показывающее, присоединен ли отладчик к процессу.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если отладчик присоединен; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public static extern bool IsAttached { [SecuritySafeCritical, __DynamicallyInvokable, MethodImpl(MethodImplOptions.InternalCall)] get; }

    /// <summary>Посылает сообщение присоединенному отладчику.</summary>
    /// <param name="level">Описание важности сообщения.</param>
    /// <param name="category">Категория сообщения.</param>
    /// <param name="message">Отображаемое сообщение.</param>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern void Log(int level, string category, string message);

    /// <summary>
    ///   Проверяет, включено ли ведение журнала для присоединенного отладчика.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если отладчик присоединен и ведение журнала включено; в противном случае — значение <see langword="false" />.
    ///    Присоединенным является управляемый отладчик, зарегистрированный в разделе реестра <see langword="DbgManagedDebugger" />.
    ///    Дополнительные сведения об этом реестре см. в разделе Enabling JIT-Attach Debugging.
    /// </returns>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    public static extern bool IsLogging();

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    private static extern void CustomNotification(ICustomDebuggerNotification data);

    private class CrossThreadDependencyNotification : ICustomDebuggerNotification
    {
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.RuntimeEnvironment
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Предоставляет коллекцию <see langword="static" /> методы, возвращающие сведения о среде CLR.
  /// </summary>
  [ComVisible(true)]
  public class RuntimeEnvironment
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Runtime.InteropServices.RuntimeEnvironment" />.
    /// </summary>
    [Obsolete("Do not create instances of the RuntimeEnvironment class.  Call the static methods directly on this type instead", true)]
    public RuntimeEnvironment()
    {
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string GetModuleFileName();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string GetDeveloperPath();

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string GetHostBindingFile();

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void _GetSystemVersion(StringHandleOnStack retVer);

    /// <summary>
    ///   Проверяет, является ли указанная сборка загружена в глобальный кэш сборок.
    /// </summary>
    /// <param name="a">Сборка для тестирования.</param>
    /// <returns>
    ///   <see langword="true" /> Если сборка загружается в глобальный кэш сборок; в противном случае — <see langword="false" />.
    /// </returns>
    public static bool FromGlobalAccessCache(Assembly a)
    {
      return a.GlobalAssemblyCache;
    }

    /// <summary>
    ///   Возвращает номер версии среды CLR, выполняющей текущий процесс.
    /// </summary>
    /// <returns>Строка, содержащая номер версии среды CLR.</returns>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string GetSystemVersion()
    {
      string s = (string) null;
      RuntimeEnvironment._GetSystemVersion(JitHelpers.GetStringHandleOnStack(ref s));
      return s;
    }

    /// <summary>
    ///   Возвращает каталог, в котором установлена общеязыковая среда выполнения.
    /// </summary>
    /// <returns>
    ///   Строка, содержащая путь к каталогу, в котором установлена общеязыковая среда выполнения.
    /// </returns>
    [SecuritySafeCritical]
    public static string GetRuntimeDirectory()
    {
      string runtimeDirectoryImpl = RuntimeEnvironment.GetRuntimeDirectoryImpl();
      new FileIOPermission(FileIOPermissionAccess.PathDiscovery, runtimeDirectoryImpl).Demand();
      return runtimeDirectoryImpl;
    }

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal static extern string GetRuntimeDirectoryImpl();

    /// <summary>Возвращает путь к файлу конфигурации системы.</summary>
    /// <returns>Путь к файлу конфигурации системы.</returns>
    public static string SystemConfigurationFile
    {
      [SecuritySafeCritical] get
      {
        StringBuilder stringBuilder = new StringBuilder(260);
        stringBuilder.Append(RuntimeEnvironment.GetRuntimeDirectory());
        stringBuilder.Append(AppDomainSetup.RuntimeConfigurationFile);
        string path = stringBuilder.ToString();
        new FileIOPermission(FileIOPermissionAccess.PathDiscovery, path).Demand();
        return path;
      }
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    private static extern IntPtr GetRuntimeInterfaceImpl([MarshalAs(UnmanagedType.LPStruct), In] Guid clsid, [MarshalAs(UnmanagedType.LPStruct), In] Guid riid);

    /// <summary>
    ///   Возвращает указанный интерфейс для указанного класса.
    /// </summary>
    /// <param name="clsid">Идентификатор требуемого класса.</param>
    /// <param name="riid">Идентификатор для нужного интерфейса.</param>
    /// <returns>Неуправляемый указатель на запрошенный интерфейс.</returns>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">
    ///   IUnknown::QueryInterface Произошла ошибка.
    /// </exception>
    [SecurityCritical]
    [ComVisible(false)]
    public static IntPtr GetRuntimeInterfaceAsIntPtr(Guid clsid, Guid riid)
    {
      return RuntimeEnvironment.GetRuntimeInterfaceImpl(clsid, riid);
    }

    /// <summary>
    ///   Возвращает экземпляр типа, представляющий COM-объект, указатель на его <see langword="IUnknown" /> интерфейса.
    /// </summary>
    /// <param name="clsid">Идентификатор требуемого класса.</param>
    /// <param name="riid">Идентификатор для нужного интерфейса.</param>
    /// <returns>
    ///   Объект, представляющий указанный неуправляемый COM-объект.
    /// </returns>
    /// <exception cref="T:System.Runtime.InteropServices.COMException">
    ///   IUnknown::QueryInterface Произошла ошибка.
    /// </exception>
    [SecurityCritical]
    [ComVisible(false)]
    public static object GetRuntimeInterfaceAsObject(Guid clsid, Guid riid)
    {
      IntPtr pUnk = IntPtr.Zero;
      try
      {
        pUnk = RuntimeEnvironment.GetRuntimeInterfaceImpl(clsid, riid);
        return Marshal.GetObjectForIUnknown(pUnk);
      }
      finally
      {
        if (pUnk != IntPtr.Zero)
          Marshal.Release(pUnk);
      }
    }
  }
}

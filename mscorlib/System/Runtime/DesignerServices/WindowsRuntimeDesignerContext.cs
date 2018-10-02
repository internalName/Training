// Decompiled with JetBrains decompiler
// Type: System.Runtime.DesignerServices.WindowsRuntimeDesignerContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Policy;
using System.Threading;

namespace System.Runtime.DesignerServices
{
  /// <summary>
  ///   Обеспечивает привязки для конструкторов, которые используются для создания настраиваемых сборок Магазин Windows 8.x приложений.
  /// </summary>
  public sealed class WindowsRuntimeDesignerContext
  {
    private static object s_lock = new object();
    private static IntPtr s_sharedContext;
    private IntPtr m_contextObject;
    private string m_name;

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern IntPtr CreateDesignerContext([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1, ArraySubType = UnmanagedType.LPWStr)] string[] paths, int count, bool shared);

    [SecurityCritical]
    internal static IntPtr CreateDesignerContext(IEnumerable<string> paths, [MarshalAs(UnmanagedType.Bool)] bool shared)
    {
      string[] array = new List<string>(paths).ToArray();
      foreach (string path in array)
      {
        if (path == null)
          throw new ArgumentNullException(Environment.GetResourceString("ArgumentNull_Path"));
        if (Path.IsRelative(path))
          throw new ArgumentException(Environment.GetResourceString("Argument_AbsolutePathRequired"));
      }
      return WindowsRuntimeDesignerContext.CreateDesignerContext(array, array.Length, shared);
    }

    [SecurityCritical]
    [SuppressUnmanagedCodeSecurity]
    [DllImport("QCall", CharSet = CharSet.Unicode)]
    internal static extern void SetCurrentContext([MarshalAs(UnmanagedType.Bool)] bool isDesignerContext, IntPtr context);

    [SecurityCritical]
    private WindowsRuntimeDesignerContext(IEnumerable<string> paths, string name, bool designModeRequired)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (paths == null)
        throw new ArgumentNullException(nameof (paths));
      if (!AppDomain.CurrentDomain.IsDefaultAppDomain())
        throw new NotSupportedException();
      if (!AppDomain.IsAppXModel())
        throw new NotSupportedException();
      if (designModeRequired && !AppDomain.IsAppXDesignMode())
        throw new NotSupportedException();
      this.m_name = name;
      lock (WindowsRuntimeDesignerContext.s_lock)
      {
        if (WindowsRuntimeDesignerContext.s_sharedContext == IntPtr.Zero)
          WindowsRuntimeDesignerContext.InitializeSharedContext((IEnumerable<string>) new string[0]);
      }
      this.m_contextObject = WindowsRuntimeDesignerContext.CreateDesignerContext(paths, false);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Runtime.DesignerServices.WindowsRuntimeDesignerContext" /> класса, указывая набор путей для поиска сторонних Среда выполнения Windows типов и для управляемых сборок и указав имя контекста.
    /// </summary>
    /// <param name="paths">Пути для поиска.</param>
    /// <param name="name">Имя контекста.</param>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий домен приложения не по умолчанию домен приложения.
    /// 
    ///   -или-
    /// 
    ///   Процесс не выполняется в контейнере приложения.
    /// 
    ///   -или-
    /// 
    ///   Компьютер не имеет лицензии разработчика.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> или <paramref name="paths" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public WindowsRuntimeDesignerContext(IEnumerable<string> paths, string name)
      : this(paths, name, true)
    {
    }

    /// <summary>
    ///   Создает контекст и устанавливает его в качестве общего контекста.
    /// </summary>
    /// <param name="paths">
    ///   Перечисляемая коллекция путей, которые используются для разрешения запроса привязки, которые не могут быть удовлетворены контекстов итераций.
    /// </param>
    /// <exception cref="T:System.NotSupportedException">
    ///   Общий контекст уже установлено в этом домене приложения.
    /// 
    ///   -или-
    /// 
    ///   Текущий домен приложения не по умолчанию домен приложения.
    /// 
    ///   -или-
    /// 
    ///   Процесс не выполняется в контейнере приложения.
    /// 
    ///   -или-
    /// 
    ///   Компьютер не имеет лицензии разработчика.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="paths" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void InitializeSharedContext(IEnumerable<string> paths)
    {
      if (!AppDomain.CurrentDomain.IsDefaultAppDomain())
        throw new NotSupportedException();
      if (paths == null)
        throw new ArgumentNullException(nameof (paths));
      lock (WindowsRuntimeDesignerContext.s_lock)
      {
        if (WindowsRuntimeDesignerContext.s_sharedContext != IntPtr.Zero)
          throw new NotSupportedException();
        IntPtr designerContext = WindowsRuntimeDesignerContext.CreateDesignerContext(paths, true);
        WindowsRuntimeDesignerContext.SetCurrentContext(false, designerContext);
        WindowsRuntimeDesignerContext.s_sharedContext = designerContext;
      }
    }

    /// <summary>
    ///   Задает контекст для обработки итерации запроса привязки к сборке, как сборки компилируются во время проектирования.
    /// </summary>
    /// <param name="context">
    ///   Контекст, который обрабатывает итераций запроса привязки к сборке.
    /// </param>
    /// <exception cref="T:System.NotSupportedException">
    ///   Текущий домен приложения не по умолчанию домен приложения.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="context" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void SetIterationContext(WindowsRuntimeDesignerContext context)
    {
      if (!AppDomain.CurrentDomain.IsDefaultAppDomain())
        throw new NotSupportedException();
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      lock (WindowsRuntimeDesignerContext.s_lock)
        WindowsRuntimeDesignerContext.SetCurrentContext(true, context.m_contextObject);
    }

    /// <summary>Загружает указанную сборку из текущего контекста.</summary>
    /// <param name="assemblyName">
    ///   Полное имя сборки для загрузки.
    ///    Описание имен полный сборок см. <see cref="P:System.Reflection.Assembly.FullName" /> свойство.
    /// </param>
    /// <returns>
    ///   Сборки, если он найден в текущем контексте; в противном случае — <see langword="null" />.
    /// </returns>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Assembly GetAssembly(string assemblyName)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Assembly) RuntimeAssembly.InternalLoad(assemblyName, (Evidence) null, ref stackMark, this.m_contextObject, false);
    }

    /// <summary>Загружает указанный тип в текущем контексте.</summary>
    /// <param name="typeName">
    ///   Квалифицированное имя типа для загрузки.
    ///    Описание имена с указанием сборки см. <see cref="P:System.Type.AssemblyQualifiedName" /> свойство.
    /// </param>
    /// <returns>
    ///   Тип, если он найден в текущем контексте; в противном случае — <see langword="null" />.
    /// </returns>
    [SecurityCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public Type GetType(string typeName)
    {
      if (typeName == null)
        throw new ArgumentNullException(nameof (typeName));
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      return (Type) RuntimeTypeHandle.GetTypeByName(typeName, false, false, false, ref stackMark, this.m_contextObject, false);
    }

    /// <summary>Возвращает имя контекста конструктора привязки.</summary>
    /// <returns>Имя контекста.</returns>
    public string Name
    {
      get
      {
        return this.m_name;
      }
    }
  }
}

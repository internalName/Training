// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMarshal
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Policy;
using System.Threading;

namespace System.Runtime.InteropServices.WindowsRuntime
{
  /// <summary>
  ///   Предоставляет вспомогательные методы для маршалинга данных между .NET Framework и Среда выполнения Windows.
  /// </summary>
  public static class WindowsRuntimeMarshal
  {
    private static bool s_haveBlueErrorApis = true;
    private static Guid s_iidIErrorInfo = new Guid(485667104, (short) 21629, (short) 4123, (byte) 142, (byte) 101, (byte) 8, (byte) 0, (byte) 43, (byte) 43, (byte) 209, (byte) 25);
    private static IntPtr s_pClassActivator = IntPtr.Zero;

    /// <summary>
    ///   Добавляет указанный обработчик событий для Среда выполнения Windows события.
    /// </summary>
    /// <param name="addMethod">
    ///   Делегат, который представляет метод, который добавляет обработчики событий для Среда выполнения Windows события.
    /// </param>
    /// <param name="removeMethod">
    ///   Делегат, который представляет метод, который удаляет обработчики событий из Среда выполнения Windows события.
    /// </param>
    /// <param name="handler">
    ///   Делегат представляет обработчик событий, который будет добавлен.
    /// </param>
    /// <typeparam name="T">
    ///   Тип делегата, который представляет обработчик события.
    /// </typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="addMethod" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Свойство <paramref name="removeMethod" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void AddEventHandler<T>(Func<T, EventRegistrationToken> addMethod, Action<EventRegistrationToken> removeMethod, T handler)
    {
      if (addMethod == null)
        throw new ArgumentNullException(nameof (addMethod));
      if (removeMethod == null)
        throw new ArgumentNullException(nameof (removeMethod));
      if ((object) handler == null)
        return;
      object target = removeMethod.Target;
      if (target == null || Marshal.IsComObject(target))
        WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.AddEventHandler<T>(addMethod, removeMethod, handler);
      else
        WindowsRuntimeMarshal.ManagedEventRegistrationImpl.AddEventHandler<T>(addMethod, removeMethod, handler);
    }

    /// <summary>
    ///   Удаляет заданный обработчик событий из Среда выполнения Windows события.
    /// </summary>
    /// <param name="removeMethod">
    ///   Делегат, который представляет метод, который удаляет обработчики событий из Среда выполнения Windows события.
    /// </param>
    /// <param name="handler">
    ///   Обработчик событий, который будет удален.
    /// </param>
    /// <typeparam name="T">
    ///   Тип делегата, который представляет обработчик события.
    /// </typeparam>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="removeMethod" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void RemoveEventHandler<T>(Action<EventRegistrationToken> removeMethod, T handler)
    {
      if (removeMethod == null)
        throw new ArgumentNullException(nameof (removeMethod));
      if ((object) handler == null)
        return;
      object target = removeMethod.Target;
      if (target == null || Marshal.IsComObject(target))
        WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.RemoveEventHandler<T>(removeMethod, handler);
      else
        WindowsRuntimeMarshal.ManagedEventRegistrationImpl.RemoveEventHandler<T>(removeMethod, handler);
    }

    /// <summary>
    ///   Удаляет все обработчики событий, которые могут быть удалены с помощью указанного метода.
    /// </summary>
    /// <param name="removeMethod">
    ///   Делегат, который представляет метод, который удаляет обработчики событий из Среда выполнения Windows события.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="removeMethod" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static void RemoveAllEventHandlers(Action<EventRegistrationToken> removeMethod)
    {
      if (removeMethod == null)
        throw new ArgumentNullException(nameof (removeMethod));
      object target = removeMethod.Target;
      if (target == null || Marshal.IsComObject(target))
        WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.RemoveAllEventHandlers(removeMethod);
      else
        WindowsRuntimeMarshal.ManagedEventRegistrationImpl.RemoveAllEventHandlers(removeMethod);
    }

    internal static int GetRegistrationTokenCacheSize()
    {
      int num = 0;
      if (WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations != null)
      {
        lock (WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations)
          num += WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations.Keys.Count;
      }
      if (WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations != null)
      {
        lock (WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations)
          num += WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.Count;
      }
      return num;
    }

    internal static void CallRemoveMethods(Action<EventRegistrationToken> removeMethod, List<EventRegistrationToken> tokensToRemove)
    {
      List<Exception> exceptionList = new List<Exception>();
      foreach (EventRegistrationToken registrationToken in tokensToRemove)
      {
        try
        {
          removeMethod(registrationToken);
        }
        catch (Exception ex)
        {
          exceptionList.Add(ex);
        }
      }
      if (exceptionList.Count > 0)
        throw new AggregateException(exceptionList.ToArray());
    }

    [SecurityCritical]
    internal static unsafe string HStringToString(IntPtr hstring)
    {
      if (hstring == IntPtr.Zero)
        return string.Empty;
      uint num;
      return new string(UnsafeNativeMethods.WindowsGetStringRawBuffer(hstring, &num), 0, checked ((int) num));
    }

    internal static Exception GetExceptionForHR(int hresult, Exception innerException, string messageResource)
    {
      Exception exception;
      if (innerException != null)
      {
        string message = innerException.Message;
        if (message == null && messageResource != null)
          message = Environment.GetResourceString(messageResource);
        exception = new Exception(message, innerException);
      }
      else
        exception = new Exception(messageResource != null ? Environment.GetResourceString(messageResource) : (string) null);
      exception.SetErrorCode(hresult);
      return exception;
    }

    internal static Exception GetExceptionForHR(int hresult, Exception innerException)
    {
      return WindowsRuntimeMarshal.GetExceptionForHR(hresult, innerException, (string) null);
    }

    [SecurityCritical]
    private static bool RoOriginateLanguageException(int error, string message, IntPtr languageException)
    {
      if (WindowsRuntimeMarshal.s_haveBlueErrorApis)
      {
        try
        {
          return UnsafeNativeMethods.RoOriginateLanguageException(error, message, languageException);
        }
        catch (EntryPointNotFoundException ex)
        {
          WindowsRuntimeMarshal.s_haveBlueErrorApis = false;
        }
      }
      return false;
    }

    [SecurityCritical]
    private static void RoReportUnhandledError(IRestrictedErrorInfo error)
    {
      if (!WindowsRuntimeMarshal.s_haveBlueErrorApis)
        return;
      try
      {
        UnsafeNativeMethods.RoReportUnhandledError(error);
      }
      catch (EntryPointNotFoundException ex)
      {
        WindowsRuntimeMarshal.s_haveBlueErrorApis = false;
      }
    }

    [FriendAccessAllowed]
    [SecuritySafeCritical]
    internal static bool ReportUnhandledError(Exception e)
    {
      if (!AppDomain.IsAppXModel() || !WindowsRuntimeMarshal.s_haveBlueErrorApis || e == null)
        return false;
      IntPtr pUnk = IntPtr.Zero;
      IntPtr ppv = IntPtr.Zero;
      try
      {
        pUnk = Marshal.GetIUnknownForObject((object) e);
        if (pUnk != IntPtr.Zero)
        {
          Marshal.QueryInterface(pUnk, ref WindowsRuntimeMarshal.s_iidIErrorInfo, out ppv);
          if (ppv != IntPtr.Zero)
          {
            if (WindowsRuntimeMarshal.RoOriginateLanguageException(Marshal.GetHRForException_WinRT(e), e.Message, ppv))
            {
              IRestrictedErrorInfo restrictedErrorInfo = UnsafeNativeMethods.GetRestrictedErrorInfo();
              if (restrictedErrorInfo != null)
              {
                WindowsRuntimeMarshal.RoReportUnhandledError(restrictedErrorInfo);
                return true;
              }
            }
          }
        }
      }
      finally
      {
        if (ppv != IntPtr.Zero)
          Marshal.Release(ppv);
        if (pUnk != IntPtr.Zero)
          Marshal.Release(pUnk);
      }
      return false;
    }

    [SecurityCritical]
    internal static IntPtr GetActivationFactoryForType(Type type)
    {
      return Marshal.GetComInterfaceForObject((object) WindowsRuntimeMarshal.GetManagedActivationFactory(type), typeof (IActivationFactory));
    }

    [SecurityCritical]
    internal static ManagedActivationFactory GetManagedActivationFactory(Type type)
    {
      ManagedActivationFactory activationFactory = new ManagedActivationFactory(type);
      Marshal.InitializeManagedWinRTFactoryObject((object) activationFactory, (RuntimeType) type);
      return activationFactory;
    }

    [SecurityCritical]
    internal static IntPtr GetClassActivatorForApplication(string appBase)
    {
      if (WindowsRuntimeMarshal.s_pClassActivator == IntPtr.Zero)
      {
        AppDomainSetup info = new AppDomainSetup()
        {
          ApplicationBase = appBase
        };
        AppDomain domain = AppDomain.CreateDomain(Environment.GetResourceString("WinRTHostDomainName", (object) appBase), (Evidence) null, info);
        IntPtr rtClassActivator = ((WinRTClassActivator) domain.CreateInstanceAndUnwrap(typeof (WinRTClassActivator).Assembly.FullName, typeof (WinRTClassActivator).FullName)).GetIWinRTClassActivator();
        if (Interlocked.CompareExchange(ref WindowsRuntimeMarshal.s_pClassActivator, rtClassActivator, IntPtr.Zero) != IntPtr.Zero)
        {
          Marshal.Release(rtClassActivator);
          try
          {
            AppDomain.Unload(domain);
          }
          catch (CannotUnloadAppDomainException ex)
          {
          }
        }
      }
      Marshal.AddRef(WindowsRuntimeMarshal.s_pClassActivator);
      return WindowsRuntimeMarshal.s_pClassActivator;
    }

    /// <summary>
    ///   Возвращает объект, реализующий интерфейс фабрику активации для указанного Среда выполнения Windows типа.
    /// </summary>
    /// <param name="type">
    /// 
    ///     Среда выполнения Windows Интерфейс фабрику активации для типа.
    /// </param>
    /// <returns>Объект, реализующий интерфейс фабрики активации.</returns>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="type" /> не представляет Среда выполнения Windows типа (то есть, принадлежащих Среда выполнения Windows сам или в определенных Среда выполнения Windows компонент).
    /// 
    ///   -или-
    /// 
    ///   Объект, указанный для <paramref name="type" /> не был предоставлен системой типов среды выполнения.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="type" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.TypeLoadException">
    ///   Указанный Среда выполнения Windows класс не зарегистрирован должным образом.
    ///    Например, winmd-файл был задан, но Среда выполнения Windows не удалось найти реализацию.
    /// </exception>
    [SecurityCritical]
    public static IActivationFactory GetActivationFactory(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (type.IsWindowsRuntimeObject && type.IsImport)
        return (IActivationFactory) Marshal.GetNativeActivationFactory(type);
      return (IActivationFactory) WindowsRuntimeMarshal.GetManagedActivationFactory(type);
    }

    /// <summary>
    ///   Выделяет элемент Среда выполнения WindowsHSTRING и копирует в него его указанную управляемую строку.
    /// </summary>
    /// <param name="s">Копируемая управляемая строка.</param>
    /// <returns>
    ///   Неуправляемый указатель на новый элемент HSTRING; или <see cref="F:System.IntPtr.Zero" />, если <paramref name="s" /> имеет значение <see cref="F:System.String.Empty" />.
    /// </returns>
    /// <exception cref="T:System.PlatformNotSupportedException">
    /// 
    ///     Среда выполнения Windows не поддерживается в текущей версии операционной системы.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="s" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public static unsafe IntPtr StringToHString(string s)
    {
      if (!Environment.IsWinRTSupported)
        throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
      if (s == null)
        throw new ArgumentNullException(nameof (s));
      IntPtr num;
      Marshal.ThrowExceptionForHR(UnsafeNativeMethods.WindowsCreateString(s, s.Length, &num), new IntPtr(-1));
      return num;
    }

    /// <summary>
    ///   Возвращает управляемую строку, содержащую копию указанной Среда выполнения WindowsHSTRING.
    /// </summary>
    /// <param name="ptr">
    ///   Неуправляемый указатель на строку HSTRING, которую нужно скопировать.
    /// </param>
    /// <returns>
    ///   Управляемая строка, содержащая копию HSTRING, если <paramref name="ptr" /> не соответствует <see cref="F:System.IntPtr.Zero" />, в противном случае — <see cref="F:System.String.Empty" />.
    /// </returns>
    /// <exception cref="T:System.PlatformNotSupportedException">
    /// 
    ///     Среда выполнения Windows не поддерживается в текущей версии операционной системы.
    /// </exception>
    [SecurityCritical]
    public static string PtrToStringHString(IntPtr ptr)
    {
      if (!Environment.IsWinRTSupported)
        throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
      return WindowsRuntimeMarshal.HStringToString(ptr);
    }

    /// <summary>
    ///   Освобождает указанный элемент Среда выполнения WindowsHSTRING.
    /// </summary>
    /// <param name="ptr">Адрес освобождаемого элемента HSTRING.</param>
    /// <exception cref="T:System.PlatformNotSupportedException">
    /// 
    ///     Среда выполнения Windows не поддерживается в текущей версии операционной системы.
    /// </exception>
    [SecurityCritical]
    public static void FreeHString(IntPtr ptr)
    {
      if (!Environment.IsWinRTSupported)
        throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
      if (!(ptr != IntPtr.Zero))
        return;
      UnsafeNativeMethods.WindowsDeleteString(ptr);
    }

    internal struct EventRegistrationTokenList
    {
      private EventRegistrationToken firstToken;
      private List<EventRegistrationToken> restTokens;

      internal EventRegistrationTokenList(EventRegistrationToken token)
      {
        this.firstToken = token;
        this.restTokens = (List<EventRegistrationToken>) null;
      }

      internal EventRegistrationTokenList(WindowsRuntimeMarshal.EventRegistrationTokenList list)
      {
        this.firstToken = list.firstToken;
        this.restTokens = list.restTokens;
      }

      public bool Push(EventRegistrationToken token)
      {
        bool flag = false;
        if (this.restTokens == null)
        {
          this.restTokens = new List<EventRegistrationToken>();
          flag = true;
        }
        this.restTokens.Add(token);
        return flag;
      }

      public bool Pop(out EventRegistrationToken token)
      {
        if (this.restTokens == null || this.restTokens.Count == 0)
        {
          token = this.firstToken;
          return false;
        }
        int index = this.restTokens.Count - 1;
        token = this.restTokens[index];
        this.restTokens.RemoveAt(index);
        return true;
      }

      public void CopyTo(List<EventRegistrationToken> tokens)
      {
        tokens.Add(this.firstToken);
        if (this.restTokens == null)
          return;
        tokens.AddRange((IEnumerable<EventRegistrationToken>) this.restTokens);
      }
    }

    internal static class ManagedEventRegistrationImpl
    {
      internal static volatile ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>> s_eventRegistrations = new ConditionalWeakTable<object, Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>>();

      [SecurityCritical]
      internal static void AddEventHandler<T>(Func<T, EventRegistrationToken> addMethod, Action<EventRegistrationToken> removeMethod, T handler)
      {
        Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> registrationTokenTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.GetEventRegistrationTokenTable(removeMethod.Target, removeMethod);
        EventRegistrationToken token = addMethod(handler);
        lock (registrationTokenTable)
        {
          WindowsRuntimeMarshal.EventRegistrationTokenList registrationTokenList;
          if (!registrationTokenTable.TryGetValue((object) handler, out registrationTokenList))
          {
            registrationTokenList = new WindowsRuntimeMarshal.EventRegistrationTokenList(token);
            registrationTokenTable[(object) handler] = registrationTokenList;
          }
          else
          {
            if (!registrationTokenList.Push(token))
              return;
            registrationTokenTable[(object) handler] = registrationTokenList;
          }
        }
      }

      private static Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> GetEventRegistrationTokenTable(object instance, Action<EventRegistrationToken> removeMethod)
      {
        lock (WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations)
        {
          Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>> dictionary1 = (Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>) null;
          if (!WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations.TryGetValue(instance, out dictionary1))
          {
            dictionary1 = new Dictionary<MethodInfo, Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>>();
            WindowsRuntimeMarshal.ManagedEventRegistrationImpl.s_eventRegistrations.Add(instance, dictionary1);
          }
          Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> dictionary2 = (Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>) null;
          if (!dictionary1.TryGetValue(removeMethod.Method, out dictionary2))
          {
            dictionary2 = new Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList>();
            dictionary1.Add(removeMethod.Method, dictionary2);
          }
          return dictionary2;
        }
      }

      [SecurityCritical]
      internal static void RemoveEventHandler<T>(Action<EventRegistrationToken> removeMethod, T handler)
      {
        Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> registrationTokenTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.GetEventRegistrationTokenTable(removeMethod.Target, removeMethod);
        EventRegistrationToken token;
        lock (registrationTokenTable)
        {
          WindowsRuntimeMarshal.EventRegistrationTokenList registrationTokenList;
          if (!registrationTokenTable.TryGetValue((object) handler, out registrationTokenList))
            return;
          if (!registrationTokenList.Pop(out token))
            registrationTokenTable.Remove((object) handler);
        }
        removeMethod(token);
      }

      [SecurityCritical]
      internal static void RemoveAllEventHandlers(Action<EventRegistrationToken> removeMethod)
      {
        Dictionary<object, WindowsRuntimeMarshal.EventRegistrationTokenList> registrationTokenTable = WindowsRuntimeMarshal.ManagedEventRegistrationImpl.GetEventRegistrationTokenTable(removeMethod.Target, removeMethod);
        List<EventRegistrationToken> registrationTokenList1 = new List<EventRegistrationToken>();
        lock (registrationTokenTable)
        {
          foreach (WindowsRuntimeMarshal.EventRegistrationTokenList registrationTokenList2 in registrationTokenTable.Values)
            registrationTokenList2.CopyTo(registrationTokenList1);
          registrationTokenTable.Clear();
        }
        WindowsRuntimeMarshal.CallRemoveMethods(removeMethod, registrationTokenList1);
      }
    }

    internal static class NativeOrStaticEventRegistrationImpl
    {
      internal static volatile Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry> s_eventRegistrations = new Dictionary<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry>((IEqualityComparer<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey>) new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKeyEqualityComparer());
      private static volatile WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.MyReaderWriterLock s_eventCacheRWLock = new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.MyReaderWriterLock();

      [SecuritySafeCritical]
      private static object GetInstanceKey(Action<EventRegistrationToken> removeMethod)
      {
        object target = removeMethod.Target;
        if (target == null)
          return (object) removeMethod.Method.DeclaringType;
        return (object) Marshal.GetRawIUnknownForComObjectNoAddRef(target);
      }

      [SecurityCritical]
      internal static void AddEventHandler<T>(Func<T, EventRegistrationToken> addMethod, Action<EventRegistrationToken> removeMethod, T handler)
      {
        object instanceKey = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetInstanceKey(removeMethod);
        EventRegistrationToken token = addMethod(handler);
        bool flag = false;
        try
        {
          WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireReaderLock(-1);
          try
          {
            WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
            ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> registrationTokenTable = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetOrCreateEventRegistrationTokenTable(instanceKey, removeMethod, out tokenListCount);
            lock (registrationTokenTable)
            {
              WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount tokenListWithCount;
              if (registrationTokenTable.FindEquivalentKeyUnsafe((object) handler, out tokenListWithCount) == null)
              {
                tokenListWithCount = new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount(tokenListCount, token);
                registrationTokenTable.Add((object) handler, tokenListWithCount);
              }
              else
                tokenListWithCount.Push(token);
              flag = true;
            }
          }
          finally
          {
            WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseReaderLock();
          }
        }
        catch (Exception ex)
        {
          if (!flag)
            removeMethod(token);
          throw;
        }
      }

      private static ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> GetEventRegistrationTokenTableNoCreate(object instance, Action<EventRegistrationToken> removeMethod, out WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount)
      {
        return WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableInternal(instance, removeMethod, out tokenListCount, false);
      }

      private static ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> GetOrCreateEventRegistrationTokenTable(object instance, Action<EventRegistrationToken> removeMethod, out WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount)
      {
        return WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableInternal(instance, removeMethod, out tokenListCount, true);
      }

      private static ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> GetEventRegistrationTokenTableInternal(object instance, Action<EventRegistrationToken> removeMethod, out WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount, bool createIfNotFound)
      {
        WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey key;
        key.target = instance;
        key.method = removeMethod.Method;
        lock (WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations)
        {
          WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry eventCacheEntry;
          if (!WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.TryGetValue(key, out eventCacheEntry))
          {
            if (!createIfNotFound)
            {
              tokenListCount = (WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount) null;
              return (ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount>) null;
            }
            eventCacheEntry = new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheEntry();
            eventCacheEntry.registrationTable = new ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount>();
            eventCacheEntry.tokenListCount = new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount(key);
            WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.Add(key, eventCacheEntry);
          }
          tokenListCount = eventCacheEntry.tokenListCount;
          return eventCacheEntry.registrationTable;
        }
      }

      [SecurityCritical]
      internal static void RemoveEventHandler<T>(Action<EventRegistrationToken> removeMethod, T handler)
      {
        object instanceKey = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetInstanceKey(removeMethod);
        WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireReaderLock(-1);
        EventRegistrationToken token;
        try
        {
          WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
          ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> tokenTableNoCreate = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableNoCreate(instanceKey, removeMethod, out tokenListCount);
          if (tokenTableNoCreate == null)
            return;
          lock (tokenTableNoCreate)
          {
            WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount tokenListWithCount;
            object equivalentKeyUnsafe = tokenTableNoCreate.FindEquivalentKeyUnsafe((object) handler, out tokenListWithCount);
            if (tokenListWithCount == null)
              return;
            if (!tokenListWithCount.Pop(out token))
              tokenTableNoCreate.Remove(equivalentKeyUnsafe);
          }
        }
        finally
        {
          WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseReaderLock();
        }
        removeMethod(token);
      }

      [SecurityCritical]
      internal static void RemoveAllEventHandlers(Action<EventRegistrationToken> removeMethod)
      {
        object instanceKey = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetInstanceKey(removeMethod);
        List<EventRegistrationToken> registrationTokenList = new List<EventRegistrationToken>();
        WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireReaderLock(-1);
        try
        {
          WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
          ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> tokenTableNoCreate = WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.GetEventRegistrationTokenTableNoCreate(instanceKey, removeMethod, out tokenListCount);
          if (tokenTableNoCreate == null)
            return;
          lock (tokenTableNoCreate)
          {
            foreach (WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount tokenListWithCount in (IEnumerable<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount>) tokenTableNoCreate.Values)
              tokenListWithCount.CopyTo(registrationTokenList);
            tokenTableNoCreate.Clear();
          }
        }
        finally
        {
          WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseReaderLock();
        }
        WindowsRuntimeMarshal.CallRemoveMethods(removeMethod, registrationTokenList);
      }

      internal struct EventCacheKey
      {
        internal object target;
        internal MethodInfo method;

        public override string ToString()
        {
          return "(" + this.target + ", " + (object) this.method + ")";
        }
      }

      internal class EventCacheKeyEqualityComparer : IEqualityComparer<WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey>
      {
        public bool Equals(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey lhs, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey rhs)
        {
          if (object.Equals(lhs.target, rhs.target))
            return object.Equals((object) lhs.method, (object) rhs.method);
          return false;
        }

        public int GetHashCode(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey key)
        {
          return key.target.GetHashCode() ^ key.method.GetHashCode();
        }
      }

      internal class EventRegistrationTokenListWithCount
      {
        private WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount _tokenListCount;
        private WindowsRuntimeMarshal.EventRegistrationTokenList _tokenList;

        internal EventRegistrationTokenListWithCount(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount, EventRegistrationToken token)
        {
          this._tokenListCount = tokenListCount;
          this._tokenListCount.Inc();
          this._tokenList = new WindowsRuntimeMarshal.EventRegistrationTokenList(token);
        }

        ~EventRegistrationTokenListWithCount()
        {
          this._tokenListCount.Dec();
        }

        public void Push(EventRegistrationToken token)
        {
          this._tokenList.Push(token);
        }

        public bool Pop(out EventRegistrationToken token)
        {
          return this._tokenList.Pop(out token);
        }

        public void CopyTo(List<EventRegistrationToken> tokens)
        {
          this._tokenList.CopyTo(tokens);
        }
      }

      internal class TokenListCount
      {
        private int _count;
        private WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey _key;

        internal TokenListCount(WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey key)
        {
          this._key = key;
        }

        internal WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventCacheKey Key
        {
          get
          {
            return this._key;
          }
        }

        internal void Inc()
        {
          Interlocked.Increment(ref this._count);
        }

        internal void Dec()
        {
          WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.AcquireWriterLock(-1);
          try
          {
            if (Interlocked.Decrement(ref this._count) != 0)
              return;
            this.CleanupCache();
          }
          finally
          {
            WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventCacheRWLock.ReleaseWriterLock();
          }
        }

        private void CleanupCache()
        {
          WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.s_eventRegistrations.Remove(this._key);
        }
      }

      internal struct EventCacheEntry
      {
        internal ConditionalWeakTable<object, WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.EventRegistrationTokenListWithCount> registrationTable;
        internal WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.TokenListCount tokenListCount;
      }

      internal class ReaderWriterLockTimedOutException : ApplicationException
      {
      }

      internal class MyReaderWriterLock
      {
        private int myLock;
        private int owners;
        private uint numWriteWaiters;
        private uint numReadWaiters;
        private EventWaitHandle writeEvent;
        private EventWaitHandle readEvent;

        internal MyReaderWriterLock()
        {
        }

        internal void AcquireReaderLock(int millisecondsTimeout)
        {
          this.EnterMyLock();
          while (this.owners < 0 || this.numWriteWaiters != 0U)
          {
            if (this.readEvent == null)
              this.LazyCreateEvent(ref this.readEvent, false);
            else
              this.WaitOnEvent(this.readEvent, ref this.numReadWaiters, millisecondsTimeout);
          }
          ++this.owners;
          this.ExitMyLock();
        }

        internal void AcquireWriterLock(int millisecondsTimeout)
        {
          this.EnterMyLock();
          while (this.owners != 0)
          {
            if (this.writeEvent == null)
              this.LazyCreateEvent(ref this.writeEvent, true);
            else
              this.WaitOnEvent(this.writeEvent, ref this.numWriteWaiters, millisecondsTimeout);
          }
          this.owners = -1;
          this.ExitMyLock();
        }

        internal void ReleaseReaderLock()
        {
          this.EnterMyLock();
          --this.owners;
          this.ExitAndWakeUpAppropriateWaiters();
        }

        internal void ReleaseWriterLock()
        {
          this.EnterMyLock();
          ++this.owners;
          this.ExitAndWakeUpAppropriateWaiters();
        }

        private void LazyCreateEvent(ref EventWaitHandle waitEvent, bool makeAutoResetEvent)
        {
          this.ExitMyLock();
          EventWaitHandle eventWaitHandle = !makeAutoResetEvent ? (EventWaitHandle) new ManualResetEvent(false) : (EventWaitHandle) new AutoResetEvent(false);
          this.EnterMyLock();
          if (waitEvent != null)
            return;
          waitEvent = eventWaitHandle;
        }

        private void WaitOnEvent(EventWaitHandle waitEvent, ref uint numWaiters, int millisecondsTimeout)
        {
          waitEvent.Reset();
          ++numWaiters;
          bool flag = false;
          this.ExitMyLock();
          try
          {
            if (!waitEvent.WaitOne(millisecondsTimeout, false))
              throw new WindowsRuntimeMarshal.NativeOrStaticEventRegistrationImpl.ReaderWriterLockTimedOutException();
            flag = true;
          }
          finally
          {
            this.EnterMyLock();
            --numWaiters;
            if (!flag)
              this.ExitMyLock();
          }
        }

        private void ExitAndWakeUpAppropriateWaiters()
        {
          if (this.owners == 0 && this.numWriteWaiters > 0U)
          {
            this.ExitMyLock();
            this.writeEvent.Set();
          }
          else if (this.owners >= 0 && this.numReadWaiters != 0U)
          {
            this.ExitMyLock();
            this.readEvent.Set();
          }
          else
            this.ExitMyLock();
        }

        private void EnterMyLock()
        {
          if (Interlocked.CompareExchange(ref this.myLock, 1, 0) == 0)
            return;
          this.EnterMyLockSpin();
        }

        private void EnterMyLockSpin()
        {
          int num = 0;
          while (true)
          {
            if (num < 3 && Environment.ProcessorCount > 1)
              Thread.SpinWait(20);
            else
              Thread.Sleep(0);
            if (Interlocked.CompareExchange(ref this.myLock, 1, 0) != 0)
              ++num;
            else
              break;
          }
        }

        private void ExitMyLock()
        {
          this.myLock = 0;
        }
      }
    }
  }
}

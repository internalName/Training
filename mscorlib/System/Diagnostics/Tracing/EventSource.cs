// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventSource
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Reflection;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Diagnostics.Tracing
{
  /// <summary>
  ///   Предоставляет возможность создания событий для трассировки событий Windows (ETW).
  /// </summary>
  [__DynamicallyInvokable]
  public class EventSource : IDisposable
  {
    [ThreadStatic]
    private static byte m_EventSourceExceptionRecurenceCount = 0;
    private static readonly byte[] namespaceBytes = new byte[16]
    {
      (byte) 72,
      (byte) 44,
      (byte) 45,
      (byte) 178,
      (byte) 195,
      (byte) 144,
      (byte) 71,
      (byte) 200,
      (byte) 135,
      (byte) 248,
      (byte) 26,
      (byte) 21,
      (byte) 191,
      (byte) 193,
      (byte) 48,
      (byte) 251
    };
    private static readonly Guid AspNetEventSourceGuid = new Guid("ee799f41-cfa5-550b-bf2c-344747c1c668");
    private string m_name;
    internal int m_id;
    private Guid m_guid;
    internal volatile EventSource.EventMetadata[] m_eventData;
    private volatile byte[] m_rawManifest;
    private EventHandler<EventCommandEventArgs> m_eventCommandExecuted;
    private EventSourceSettings m_config;
    private bool m_eventSourceEnabled;
    internal EventLevel m_level;
    internal EventKeywords m_matchAnyKeyword;
    internal volatile EventDispatcher m_Dispatchers;
    private volatile EventSource.OverideEventProvider m_provider;
    private bool m_completelyInited;
    private Exception m_constructionException;
    private byte m_outOfBandMessageCount;
    private EventCommandEventArgs m_deferredCommands;
    private string[] m_traits;
    internal static uint s_currentPid;
    internal volatile ulong[] m_channelData;
    private SessionMask m_curLiveSessions;
    private EtwSession[] m_etwSessionIdMap;
    private List<EtwSession> m_legacySessions;
    internal long m_keywordTriggers;
    internal SessionMask m_activityFilteringForETWEnabled;
    internal static Action<Guid> s_activityDying;
    private ActivityTracker m_activityTracker;
    internal const string s_ActivityStartSuffix = "Start";
    internal const string s_ActivityStopSuffix = "Stop";
    private byte[] providerMetadata;

    /// <summary>
    ///   Понятное имя класса, производного от источника события.
    /// </summary>
    /// <returns>
    ///   Понятное имя производного класса.
    ///     Значение по умолчанию — простое имя класса.
    /// </returns>
    [__DynamicallyInvokable]
    public string Name
    {
      [__DynamicallyInvokable] get
      {
        return this.m_name;
      }
    }

    /// <summary>Уникальный идентификатор источника события.</summary>
    /// <returns>Уникальный идентификатор источника события.</returns>
    [__DynamicallyInvokable]
    public Guid Guid
    {
      [__DynamicallyInvokable] get
      {
        return this.m_guid;
      }
    }

    /// <summary>Определяет, включен ли источник текущего события.</summary>
    /// <returns>
    ///   Значение <see langword="true" />, если текущий источник события включен; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsEnabled()
    {
      return this.m_eventSourceEnabled;
    }

    /// <summary>
    ///   Указывает, включен ли источник текущего события, который имеет заданный уровень и ключевое слово.
    /// </summary>
    /// <param name="level">Уровень источника события.</param>
    /// <param name="keywords">Ключевое слово источника события.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если источник события включен; в противном случае — значение <see langword="false" />.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsEnabled(EventLevel level, EventKeywords keywords)
    {
      return this.IsEnabled(level, keywords, EventChannel.None);
    }

    /// <summary>
    ///   Определяет, включен ли текущий источник для событий с указанным уровнем, ключевыми словами и каналом.
    /// </summary>
    /// <param name="level">
    ///   Проверяемый уровень событий.
    ///    Источник событий будет считаться включенным, если этот уровень равен или больше <paramref name="level" />.
    /// </param>
    /// <param name="keywords">Проверяемые ключевые слова события.</param>
    /// <param name="channel">Проверяемый канал событий.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если источник события включен для указанного уровня событий, ключевых слов и канала; в противном случае — значение <see langword="false" />.
    /// 
    ///   Результат выполнения этого метода только приблизительно показывает, активно ли определенное событие.
    ///     Используйте его, чтобы избежать ресурсоемких вычислений для ведения журнала, когда оно отключено.
    ///      Работа источников событий может определяться дополнительной фильтрацией.
    /// </returns>
    [__DynamicallyInvokable]
    public bool IsEnabled(EventLevel level, EventKeywords keywords, EventChannel channel)
    {
      return this.m_eventSourceEnabled && this.IsEnabledCommon(this.m_eventSourceEnabled, this.m_level, this.m_matchAnyKeyword, level, keywords, channel);
    }

    /// <summary>
    ///   Возвращает параметры, применяемые к этому источнику события.
    /// </summary>
    /// <returns>Параметры, применяемые к этому источнику события.</returns>
    [__DynamicallyInvokable]
    public EventSourceSettings Settings
    {
      [__DynamicallyInvokable] get
      {
        return this.m_config;
      }
    }

    /// <summary>
    ///   Получает уникальный идентификатор для данной реализации источника события.
    /// </summary>
    /// <param name="eventSourceType">Тип источника события.</param>
    /// <returns>
    ///   Уникальный идентификатор для данного типа источника события.
    /// </returns>
    [__DynamicallyInvokable]
    public static Guid GetGuid(Type eventSourceType)
    {
      if (eventSourceType == (Type) null)
        throw new ArgumentNullException(nameof (eventSourceType));
      EventSourceAttribute customAttributeHelper = (EventSourceAttribute) EventSource.GetCustomAttributeHelper((MemberInfo) eventSourceType, typeof (EventSourceAttribute), EventManifestOptions.None);
      string name = eventSourceType.Name;
      if (customAttributeHelper != null)
      {
        if (customAttributeHelper.Guid != null)
        {
          Guid result = Guid.Empty;
          if (Guid.TryParse(customAttributeHelper.Guid, out result))
            return result;
        }
        if (customAttributeHelper.Name != null)
          name = customAttributeHelper.Name;
      }
      if (name == null)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidTypeName"), nameof (eventSourceType));
      return EventSource.GenerateGuidFromName(name.ToUpperInvariant());
    }

    /// <summary>Возвращает понятное имя источника события.</summary>
    /// <param name="eventSourceType">Тип источника события.</param>
    /// <returns>
    ///   Понятное имя источника события.
    ///    Значение по умолчанию — простое имя класса.
    /// </returns>
    [__DynamicallyInvokable]
    public static string GetName(Type eventSourceType)
    {
      return EventSource.GetName(eventSourceType, EventManifestOptions.None);
    }

    /// <summary>
    ///   Возвращает строку манифеста XML, связанного с текущим источником события.
    /// </summary>
    /// <param name="eventSourceType">Тип источника события.</param>
    /// <param name="assemblyPathToIncludeInManifest">
    ///   Путь к файлу сборки (DLL) для включения в provider манифеста.
    /// </param>
    /// <returns>Строка XML-данных.</returns>
    [__DynamicallyInvokable]
    public static string GenerateManifest(Type eventSourceType, string assemblyPathToIncludeInManifest)
    {
      return EventSource.GenerateManifest(eventSourceType, assemblyPathToIncludeInManifest, EventManifestOptions.None);
    }

    /// <summary>
    ///   Возвращает строку манифеста XML, связанного с текущим источником события.
    /// </summary>
    /// <param name="eventSourceType">Тип источника события.</param>
    /// <param name="assemblyPathToIncludeInManifest">
    ///   Путь к файлу сборки файла (.dll) для включения в provider манифеста.
    /// </param>
    /// <param name="flags">
    ///   Побитовое сочетание значений перечисления, определяющее способ создания манифеста.
    /// </param>
    /// <returns>
    ///   Строка XML-данных или <see langword="null" /> (см. примечания).
    /// </returns>
    [__DynamicallyInvokable]
    public static string GenerateManifest(Type eventSourceType, string assemblyPathToIncludeInManifest, EventManifestOptions flags)
    {
      if (eventSourceType == (Type) null)
        throw new ArgumentNullException(nameof (eventSourceType));
      byte[] manifestAndDescriptors = EventSource.CreateManifestAndDescriptors(eventSourceType, assemblyPathToIncludeInManifest, (EventSource) null, flags);
      if (manifestAndDescriptors != null)
        return Encoding.UTF8.GetString(manifestAndDescriptors, 0, manifestAndDescriptors.Length);
      return (string) null;
    }

    /// <summary>
    ///   Возвращает снимок всех источников событий в домене приложения.
    /// </summary>
    /// <returns>
    ///   Перечисление всех источников событий в домене приложения.
    /// </returns>
    [__DynamicallyInvokable]
    public static IEnumerable<EventSource> GetSources()
    {
      List<EventSource> eventSourceList = new List<EventSource>();
      lock (EventListener.EventListenersLock)
      {
        foreach (WeakReference eventSource in EventListener.s_EventSources)
        {
          EventSource target = eventSource.Target as EventSource;
          if (target != null && !target.IsDisposed)
            eventSourceList.Add(target);
        }
      }
      return (IEnumerable<EventSource>) eventSourceList;
    }

    /// <summary>Отправляет команду указанному источнику события.</summary>
    /// <param name="eventSource">
    ///   Источник событий, которому требуется отправлять команду.
    /// </param>
    /// <param name="command">
    ///   Команда события, которую требуется отправить.
    /// </param>
    /// <param name="commandArguments">
    ///   Аргументы для команды события.
    /// </param>
    [__DynamicallyInvokable]
    public static void SendCommand(EventSource eventSource, EventCommand command, IDictionary<string, string> commandArguments)
    {
      if (eventSource == null)
        throw new ArgumentNullException(nameof (eventSource));
      if (command <= EventCommand.Update && command != EventCommand.SendManifest)
        throw new ArgumentException(Environment.GetResourceString("EventSource_InvalidCommand"), nameof (command));
      eventSource.SendCommand((EventListener) null, 0, 0, command, true, EventLevel.LogAlways, EventKeywords.None, commandArguments);
    }

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Задает идентификатор действия в текущем потоке.
    /// </summary>
    /// <param name="activityId">
    ///   Новый идентификатор действия текущего потока или <see cref="F:System.Guid.Empty" />, чтобы указать, что работа в этом потоке не связана ни с каким действием.
    /// </param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void SetCurrentThreadActivityId(Guid activityId)
    {
      Guid guid = activityId;
      if (UnsafeNativeMethods.ManifestEtw.EventActivityIdControl(UnsafeNativeMethods.ManifestEtw.ActivityControl.EVENT_ACTIVITY_CTRL_GET_SET_ID, ref activityId) == 0)
      {
        Action<Guid> activityDying = EventSource.s_activityDying;
        if (activityDying != null && guid != activityId)
        {
          if (activityId == Guid.Empty)
            activityId = EventSource.FallbackActivityId;
          activityDying(activityId);
        }
      }
      if (TplEtwProvider.Log == null)
        return;
      TplEtwProvider.Log.SetActivityId(activityId);
    }

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Задает идентификатор действия в текущем потоке и возвращает предыдущий идентификатор действия.
    /// </summary>
    /// <param name="activityId">
    ///   Новый идентификатор действия текущего потока или <see cref="F:System.Guid.Empty" />, чтобы указать, что работа в этом потоке не связана ни с каким действием.
    /// </param>
    /// <param name="oldActivityThatWillContinue">
    ///   При возврате из этого метода содержит идентификатор предыдущего действия в текущем потоке.
    /// </param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public static void SetCurrentThreadActivityId(Guid activityId, out Guid oldActivityThatWillContinue)
    {
      oldActivityThatWillContinue = activityId;
      UnsafeNativeMethods.ManifestEtw.EventActivityIdControl(UnsafeNativeMethods.ManifestEtw.ActivityControl.EVENT_ACTIVITY_CTRL_GET_SET_ID, ref oldActivityThatWillContinue);
      if (TplEtwProvider.Log == null)
        return;
      TplEtwProvider.Log.SetActivityId(activityId);
    }

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Получает идентификатор действия текущего потока.
    /// </summary>
    /// <returns>Идентификатор действия текущего потока.</returns>
    [__DynamicallyInvokable]
    public static Guid CurrentThreadActivityId
    {
      [SecuritySafeCritical, __DynamicallyInvokable] get
      {
        Guid ActivityId = new Guid();
        UnsafeNativeMethods.ManifestEtw.EventActivityIdControl(UnsafeNativeMethods.ManifestEtw.ActivityControl.EVENT_ACTIVITY_CTRL_GET_ID, ref ActivityId);
        return ActivityId;
      }
    }

    internal static Guid InternalCurrentThreadActivityId
    {
      [SecurityCritical] get
      {
        Guid guid = EventSource.CurrentThreadActivityId;
        if (guid == Guid.Empty)
          guid = EventSource.FallbackActivityId;
        return guid;
      }
    }

    internal static Guid FallbackActivityId
    {
      [SecurityCritical] get
      {
        return new Guid((uint) AppDomain.GetCurrentThreadId(), (ushort) EventSource.s_currentPid, (ushort) (EventSource.s_currentPid >> 16), (byte) 148, (byte) 27, (byte) 135, (byte) 213, (byte) 166, (byte) 92, (byte) 54, (byte) 100);
      }
    }

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Возвращает любое исключение, инициированное во время создания источника событий.
    /// </summary>
    /// <returns>
    ///   Исключение, инициированное во время создания источника событий, или <see langword="null" />, если исключение не создано.
    /// </returns>
    [__DynamicallyInvokable]
    public Exception ConstructionException
    {
      [__DynamicallyInvokable] get
      {
        return this.m_constructionException;
      }
    }

    /// <summary>
    ///   Получает значение признака, связанное с заданным ключом.
    /// </summary>
    /// <param name="key">
    ///   Ключ признака, который необходимо получить.
    /// </param>
    /// <returns>
    ///   Значение признака, связанное с указанным ключом.
    ///    Если ключ не найден, возвращает значение <see langword="null" />.
    /// </returns>
    [__DynamicallyInvokable]
    public string GetTrait(string key)
    {
      if (this.m_traits != null)
      {
        int index = 0;
        while (index < this.m_traits.Length - 1)
        {
          if (this.m_traits[index] == key)
            return this.m_traits[index + 1];
          index += 2;
        }
      }
      return (string) null;
    }

    /// <summary>
    ///   Получает строковое представление текущего экземпляра источника события.
    /// </summary>
    /// <returns>
    ///   Имя и уникальный идентификатор, определяющие источник текущего события.
    /// </returns>
    [__DynamicallyInvokable]
    public override string ToString()
    {
      return Environment.GetResourceString("EventSource_ToString", (object) this.Name, (object) this.Guid);
    }

    /// <summary>
    ///   Происходит, когда команда поступает от прослушивателя событий.
    /// </summary>
    public event EventHandler<EventCommandEventArgs> EventCommandExecuted
    {
      add
      {
        this.m_eventCommandExecuted += value;
        for (EventCommandEventArgs e = this.m_deferredCommands; e != null; e = e.nextCommand)
          value((object) this, e);
      }
      remove
      {
        this.m_eventCommandExecuted -= value;
      }
    }

    /// <summary>
    ///   Создает новый экземпляр класса <see cref="T:System.Diagnostics.Tracing.EventSource" />.
    /// </summary>
    [__DynamicallyInvokable]
    protected EventSource()
      : this(EventSourceSettings.EtwManifestEventFormat)
    {
    }

    /// <summary>
    ///   Создает экземпляр класса <see cref="T:System.Diagnostics.Tracing.EventSource" /> и определяет, следует ли создавать исключение при возникновении ошибки в базовом коде Windows.
    /// </summary>
    /// <param name="throwOnEventWriteErrors">
    ///   Значение <see langword="true" /> для создания исключения при возникновении ошибки в базовом коде Windows; в противном случае — значение <see langword="false" />.
    /// </param>
    [__DynamicallyInvokable]
    protected EventSource(bool throwOnEventWriteErrors)
      : this((EventSourceSettings) (4 | (throwOnEventWriteErrors ? 1 : 0)))
    {
    }

    /// <summary>
    ///   Создает экземпляр класса <see cref="T:System.Diagnostics.Tracing.EventSource" /> с указанными параметрами конфигурации.
    /// </summary>
    /// <param name="settings">
    ///   Побитовое сочетание значений перечисления, которое определяет параметры конфигурации, применяемые к источнику события.
    /// </param>
    [__DynamicallyInvokable]
    protected EventSource(EventSourceSettings settings)
      : this(settings, (string[]) null)
    {
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Diagnostics.Tracing.EventSource" /> для использования с неконтрактными событиями, который содержит указанные параметры и признаки.
    /// </summary>
    /// <param name="settings">
    ///   Побитовое сочетание значений перечисления, которое определяет параметры конфигурации, применяемые к источнику события.
    /// </param>
    /// <param name="traits">
    ///   Пары ключ-значение, определяющие признаки для источника события.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="traits" /> не указан в пары "ключ значение".
    /// </exception>
    [__DynamicallyInvokable]
    protected EventSource(EventSourceSettings settings, params string[] traits)
    {
      this.m_config = this.ValidateSettings(settings);
      Type type = this.GetType();
      this.Initialize(EventSource.GetGuid(type), EventSource.GetName(type), traits);
    }

    /// <summary>
    ///   Вызывается, когда источник текущего события обновляется контроллером.
    /// </summary>
    /// <param name="command">Аргументы для события.</param>
    [__DynamicallyInvokable]
    protected virtual void OnEventCommand(EventCommandEventArgs command)
    {
    }

    /// <summary>
    ///   Записывает событие, используя предоставленный идентификатор события.
    /// </summary>
    /// <param name="eventId">
    ///   Идентификатор события.
    ///    Это значение должно находиться в диапазоне от 0 до 65535.
    /// </param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId)
    {
      this.WriteEventCore(eventId, 0, (EventSource.EventData*) null);
    }

    /// <summary>
    ///   Записывает событие, используя предоставленные идентификатор события и 32-разрядный целочисленный аргумент.
    /// </summary>
    /// <param name="eventId">
    ///   Идентификатор события.
    ///     Это значение должно находиться в диапазоне от 0 до 65535.
    /// </param>
    /// <param name="arg1">Целочисленный аргумент.</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, int arg1)
    {
      if (!this.m_eventSourceEnabled)
        return;
      EventSource.EventData* data = stackalloc EventSource.EventData[1];
      data->DataPointer = (IntPtr) ((void*) &arg1);
      data->Size = 4;
      this.WriteEventCore(eventId, 1, data);
    }

    /// <summary>
    ///   Записывает событие, используя предоставленные идентификатор события и 32-разрядные целочисленные аргументы.
    /// </summary>
    /// <param name="eventId">
    ///   Идентификатор события.
    ///     Это значение должно находиться в диапазоне от 0 до 65535.
    /// </param>
    /// <param name="arg1">Целочисленный аргумент.</param>
    /// <param name="arg2">Целочисленный аргумент.</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, int arg1, int arg2)
    {
      if (!this.m_eventSourceEnabled)
        return;
      EventSource.EventData* data = stackalloc EventSource.EventData[2];
      data->DataPointer = (IntPtr) ((void*) &arg1);
      data->Size = 4;
      data[1].DataPointer = (IntPtr) ((void*) &arg2);
      data[1].Size = 4;
      this.WriteEventCore(eventId, 2, data);
    }

    /// <summary>
    ///   Записывает событие, используя предоставленные идентификатор события и 32-разрядные целочисленные аргументы.
    /// </summary>
    /// <param name="eventId">
    ///   Идентификатор события.
    ///     Это значение должно находиться в диапазоне от 0 до 65535.
    /// </param>
    /// <param name="arg1">Целочисленный аргумент.</param>
    /// <param name="arg2">Целочисленный аргумент.</param>
    /// <param name="arg3">Целочисленный аргумент.</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, int arg1, int arg2, int arg3)
    {
      if (!this.m_eventSourceEnabled)
        return;
      EventSource.EventData* data = stackalloc EventSource.EventData[3];
      data->DataPointer = (IntPtr) ((void*) &arg1);
      data->Size = 4;
      data[1].DataPointer = (IntPtr) ((void*) &arg2);
      data[1].Size = 4;
      data[2].DataPointer = (IntPtr) ((void*) &arg3);
      data[2].Size = 4;
      this.WriteEventCore(eventId, 3, data);
    }

    /// <summary>
    ///   Записывает событие, используя предоставленные идентификатор события и 64-разрядный целочисленный аргумент.
    /// </summary>
    /// <param name="eventId">
    ///   Идентификатор события.
    ///     Это значение должно находиться в диапазоне от 0 до 65535.
    /// </param>
    /// <param name="arg1">64-разрядный целочисленный аргумент.</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, long arg1)
    {
      if (!this.m_eventSourceEnabled)
        return;
      EventSource.EventData* data = stackalloc EventSource.EventData[1];
      data->DataPointer = (IntPtr) ((void*) &arg1);
      data->Size = 8;
      this.WriteEventCore(eventId, 1, data);
    }

    /// <summary>
    ///   Записывает событие, используя предоставленные идентификатор события и 64-разрядные аргументы.
    /// </summary>
    /// <param name="eventId">
    ///   Идентификатор события.
    ///     Это значение должно находиться в диапазоне от 0 до 65535.
    /// </param>
    /// <param name="arg1">64-разрядный целочисленный аргумент.</param>
    /// <param name="arg2">64-разрядный целочисленный аргумент.</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, long arg1, long arg2)
    {
      if (!this.m_eventSourceEnabled)
        return;
      EventSource.EventData* data = stackalloc EventSource.EventData[2];
      data->DataPointer = (IntPtr) ((void*) &arg1);
      data->Size = 8;
      data[1].DataPointer = (IntPtr) ((void*) &arg2);
      data[1].Size = 8;
      this.WriteEventCore(eventId, 2, data);
    }

    /// <summary>
    ///   Записывает событие, используя предоставленные идентификатор события и 64-разрядные аргументы.
    /// </summary>
    /// <param name="eventId">
    ///   Идентификатор события.
    ///     Это значение должно находиться в диапазоне от 0 до 65535.
    /// </param>
    /// <param name="arg1">64-разрядный целочисленный аргумент.</param>
    /// <param name="arg2">64-разрядный целочисленный аргумент.</param>
    /// <param name="arg3">64-разрядный целочисленный аргумент.</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, long arg1, long arg2, long arg3)
    {
      if (!this.m_eventSourceEnabled)
        return;
      EventSource.EventData* data = stackalloc EventSource.EventData[3];
      data->DataPointer = (IntPtr) ((void*) &arg1);
      data->Size = 8;
      data[1].DataPointer = (IntPtr) ((void*) &arg2);
      data[1].Size = 8;
      data[2].DataPointer = (IntPtr) ((void*) &arg3);
      data[2].Size = 8;
      this.WriteEventCore(eventId, 3, data);
    }

    /// <summary>
    ///   Записывает событие, используя предоставленные идентификатор события и строковый аргумент.
    /// </summary>
    /// <param name="eventId">
    ///   Идентификатор события.
    ///     Это значение должно находиться в диапазоне от 0 до 65535.
    /// </param>
    /// <param name="arg1">Строковый аргумент.</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, string arg1)
    {
      if (!this.m_eventSourceEnabled)
        return;
      if (arg1 == null)
        arg1 = "";
      string str = arg1;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      EventSource.EventData* data = stackalloc EventSource.EventData[1];
      data->DataPointer = (IntPtr) ((void*) chPtr);
      data->Size = (arg1.Length + 1) * 2;
      this.WriteEventCore(eventId, 1, data);
      str = (string) null;
    }

    /// <summary>
    ///   Записывает событие, используя предоставленные идентификатор события и строковые аргументы.
    /// </summary>
    /// <param name="eventId">
    ///   Идентификатор события.
    ///     Это значение должно находиться в диапазоне от 0 до 65535.
    /// </param>
    /// <param name="arg1">Строковый аргумент.</param>
    /// <param name="arg2">Строковый аргумент.</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, string arg1, string arg2)
    {
      if (!this.m_eventSourceEnabled)
        return;
      if (arg1 == null)
        arg1 = "";
      if (arg2 == null)
        arg2 = "";
      string str1 = arg1;
      char* chPtr1 = (char*) str1;
      if ((IntPtr) chPtr1 != IntPtr.Zero)
        chPtr1 += RuntimeHelpers.OffsetToStringData;
      string str2 = arg2;
      char* chPtr2 = (char*) str2;
      if ((IntPtr) chPtr2 != IntPtr.Zero)
        chPtr2 += RuntimeHelpers.OffsetToStringData;
      EventSource.EventData* data = stackalloc EventSource.EventData[2];
      data->DataPointer = (IntPtr) ((void*) chPtr1);
      data->Size = (arg1.Length + 1) * 2;
      data[1].DataPointer = (IntPtr) ((void*) chPtr2);
      data[1].Size = (arg2.Length + 1) * 2;
      this.WriteEventCore(eventId, 2, data);
      str2 = (string) null;
      str1 = (string) null;
    }

    /// <summary>
    ///   Записывает событие, используя предоставленные идентификатор события и строковые аргументы.
    /// </summary>
    /// <param name="eventId">
    ///   Идентификатор события.
    ///     Это значение должно находиться в диапазоне от 0 до 65535.
    /// </param>
    /// <param name="arg1">Строковый аргумент.</param>
    /// <param name="arg2">Строковый аргумент.</param>
    /// <param name="arg3">Строковый аргумент.</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, string arg1, string arg2, string arg3)
    {
      if (!this.m_eventSourceEnabled)
        return;
      if (arg1 == null)
        arg1 = "";
      if (arg2 == null)
        arg2 = "";
      if (arg3 == null)
        arg3 = "";
      string str1 = arg1;
      char* chPtr1 = (char*) str1;
      if ((IntPtr) chPtr1 != IntPtr.Zero)
        chPtr1 += RuntimeHelpers.OffsetToStringData;
      string str2 = arg2;
      char* chPtr2 = (char*) str2;
      if ((IntPtr) chPtr2 != IntPtr.Zero)
        chPtr2 += RuntimeHelpers.OffsetToStringData;
      string str3 = arg3;
      char* chPtr3 = (char*) str3;
      if ((IntPtr) chPtr3 != IntPtr.Zero)
        chPtr3 += RuntimeHelpers.OffsetToStringData;
      EventSource.EventData* data = stackalloc EventSource.EventData[3];
      data->DataPointer = (IntPtr) ((void*) chPtr1);
      data->Size = (arg1.Length + 1) * 2;
      data[1].DataPointer = (IntPtr) ((void*) chPtr2);
      data[1].Size = (arg2.Length + 1) * 2;
      data[2].DataPointer = (IntPtr) ((void*) chPtr3);
      data[2].Size = (arg3.Length + 1) * 2;
      this.WriteEventCore(eventId, 3, data);
      str3 = (string) null;
      str2 = (string) null;
      str1 = (string) null;
    }

    /// <summary>
    ///   Записывает событие, используя предоставленные идентификатор события и аргументы.
    /// </summary>
    /// <param name="eventId">
    ///   Идентификатор события.
    ///     Это значение должно находиться в диапазоне от 0 до 65535.
    /// </param>
    /// <param name="arg1">Строковый аргумент.</param>
    /// <param name="arg2">32-разрядный целочисленный аргумент.</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, string arg1, int arg2)
    {
      if (!this.m_eventSourceEnabled)
        return;
      if (arg1 == null)
        arg1 = "";
      string str = arg1;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      EventSource.EventData* data = stackalloc EventSource.EventData[2];
      data->DataPointer = (IntPtr) ((void*) chPtr);
      data->Size = (arg1.Length + 1) * 2;
      data[1].DataPointer = (IntPtr) ((void*) &arg2);
      data[1].Size = 4;
      this.WriteEventCore(eventId, 2, data);
      str = (string) null;
    }

    /// <summary>
    ///   Записывает событие, используя предоставленные идентификатор события и аргументы.
    /// </summary>
    /// <param name="eventId">
    ///   Идентификатор события.
    ///     Это значение должно находиться в диапазоне от 0 до 65535.
    /// </param>
    /// <param name="arg1">Строковый аргумент.</param>
    /// <param name="arg2">32-разрядный целочисленный аргумент.</param>
    /// <param name="arg3">32-разрядный целочисленный аргумент.</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, string arg1, int arg2, int arg3)
    {
      if (!this.m_eventSourceEnabled)
        return;
      if (arg1 == null)
        arg1 = "";
      string str = arg1;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      EventSource.EventData* data = stackalloc EventSource.EventData[3];
      data->DataPointer = (IntPtr) ((void*) chPtr);
      data->Size = (arg1.Length + 1) * 2;
      data[1].DataPointer = (IntPtr) ((void*) &arg2);
      data[1].Size = 4;
      data[2].DataPointer = (IntPtr) ((void*) &arg3);
      data[2].Size = 4;
      this.WriteEventCore(eventId, 3, data);
      str = (string) null;
    }

    /// <summary>
    ///   Записывает событие, используя предоставленные идентификатор события и аргументы.
    /// </summary>
    /// <param name="eventId">
    ///   Идентификатор события.
    ///     Это значение должно находиться в диапазоне от 0 до 65535.
    /// </param>
    /// <param name="arg1">Строковый аргумент.</param>
    /// <param name="arg2">64-разрядный целочисленный аргумент.</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, string arg1, long arg2)
    {
      if (!this.m_eventSourceEnabled)
        return;
      if (arg1 == null)
        arg1 = "";
      string str = arg1;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      EventSource.EventData* data = stackalloc EventSource.EventData[2];
      data->DataPointer = (IntPtr) ((void*) chPtr);
      data->Size = (arg1.Length + 1) * 2;
      data[1].DataPointer = (IntPtr) ((void*) &arg2);
      data[1].Size = 8;
      this.WriteEventCore(eventId, 2, data);
      str = (string) null;
    }

    /// <summary>
    ///   Записывает событие, используя предоставленный идентификатор, а также строковые и 64-разрядные целочисленные аргументы.
    /// </summary>
    /// <param name="eventId">
    ///   Идентификатор события.
    ///     Это значение должно находиться в диапазоне от 0 до 65535.
    /// </param>
    /// <param name="arg1">64-разрядный целочисленный аргумент.</param>
    /// <param name="arg2">Строковый аргумент.</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, long arg1, string arg2)
    {
      if (!this.m_eventSourceEnabled)
        return;
      if (arg2 == null)
        arg2 = "";
      string str = arg2;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      EventSource.EventData* data = stackalloc EventSource.EventData[2];
      data->DataPointer = (IntPtr) ((void*) &arg1);
      data->Size = 8;
      data[1].DataPointer = (IntPtr) ((void*) chPtr);
      data[1].Size = (arg2.Length + 1) * 2;
      this.WriteEventCore(eventId, 2, data);
      str = (string) null;
    }

    /// <summary>
    ///   Записывает событие, используя предоставленный идентификатор, а также строковые и 32-разрядные целочисленные аргументы.
    /// </summary>
    /// <param name="eventId">
    ///   Идентификатор события.
    ///    Это значение должно находиться в диапазоне от 0 до 65535.
    /// </param>
    /// <param name="arg1">32-разрядный целочисленный аргумент.</param>
    /// <param name="arg2">Строковый аргумент.</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, int arg1, string arg2)
    {
      if (!this.m_eventSourceEnabled)
        return;
      if (arg2 == null)
        arg2 = "";
      string str = arg2;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      EventSource.EventData* data = stackalloc EventSource.EventData[2];
      data->DataPointer = (IntPtr) ((void*) &arg1);
      data->Size = 4;
      data[1].DataPointer = (IntPtr) ((void*) chPtr);
      data[1].Size = (arg2.Length + 1) * 2;
      this.WriteEventCore(eventId, 2, data);
      str = (string) null;
    }

    /// <summary>
    ///   Записывает событие, используя предоставленные идентификатор события и аргумент в виде массива байтов.
    /// </summary>
    /// <param name="eventId">
    ///   Идентификатор события.
    ///     Это значение должно находиться в диапазоне от 0 до 65535.
    /// </param>
    /// <param name="arg1">Аргумент в виде массива байтов.</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, byte[] arg1)
    {
      if (!this.m_eventSourceEnabled)
        return;
      if (arg1 == null)
        arg1 = new byte[0];
      int length = arg1.Length;
      fixed (byte* numPtr = &arg1[0])
      {
        EventSource.EventData* data = stackalloc EventSource.EventData[2];
        data->DataPointer = (IntPtr) ((void*) &length);
        data->Size = 4;
        data[1].DataPointer = (IntPtr) ((void*) numPtr);
        data[1].Size = length;
        this.WriteEventCore(eventId, 2, data);
      }
    }

    /// <summary>
    ///   Записывает данные события, используя указанный идентификатор, а также 64-разрядные целочисленные аргументы и аргументы в виде массива байтов.
    /// </summary>
    /// <param name="eventId">
    ///   Идентификатор события.
    ///     Это значение должно находиться в диапазоне от 0 до 65535.
    /// </param>
    /// <param name="arg1">64-разрядный целочисленный аргумент.</param>
    /// <param name="arg2">Аргумент в виде массива байтов.</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, long arg1, byte[] arg2)
    {
      if (!this.m_eventSourceEnabled)
        return;
      if (arg2 == null)
        arg2 = new byte[0];
      int length = arg2.Length;
      fixed (byte* numPtr = &arg2[0])
      {
        EventSource.EventData* data = stackalloc EventSource.EventData[3];
        data->DataPointer = (IntPtr) ((void*) &arg1);
        data->Size = 8;
        data[1].DataPointer = (IntPtr) ((void*) &length);
        data[1].Size = 4;
        data[2].DataPointer = (IntPtr) ((void*) numPtr);
        data[2].Size = length;
        this.WriteEventCore(eventId, 3, data);
      }
    }

    /// <summary>
    ///   Создает перегрузку <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> с помощью предоставленных идентификатора и данных события.
    /// </summary>
    /// <param name="eventId">Идентификатор события.</param>
    /// <param name="eventDataCount">
    ///   Число элементов данных события.
    /// </param>
    /// <param name="data">Структура, содержащая данные события.</param>
    [SecurityCritical]
    [CLSCompliant(false)]
    protected unsafe void WriteEventCore(int eventId, int eventDataCount, EventSource.EventData* data)
    {
      this.WriteEventWithRelatedActivityIdCore(eventId, (Guid*) null, eventDataCount, data);
    }

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Записывает событие, которое указывает, что текущее действие связано с другим действием.
    /// </summary>
    /// <param name="eventId">
    ///   Идентификатор, который уникально идентифицирует это событие в источнике <see cref="T:System.Diagnostics.Tracing.EventSource" />.
    /// </param>
    /// <param name="relatedActivityId">
    ///   Указатель на GUID идентификатора связанного действия.
    /// </param>
    /// <param name="eventDataCount">
    ///   Число элементов в поле <paramref name="data" />.
    /// </param>
    /// <param name="data">
    ///   Указатель на первый элемент в поле данных события.
    /// </param>
    [SecurityCritical]
    [CLSCompliant(false)]
    protected unsafe void WriteEventWithRelatedActivityIdCore(int eventId, Guid* relatedActivityId, int eventDataCount, EventSource.EventData* data)
    {
      if (!this.m_eventSourceEnabled)
        return;
      try
      {
        if ((IntPtr) relatedActivityId != IntPtr.Zero)
          this.ValidateEventOpcodeForTransfer(ref this.m_eventData[eventId], this.m_eventData[eventId].Name);
        if (this.m_eventData[eventId].EnabledForETW)
        {
          EventOpcode opcode = (EventOpcode) this.m_eventData[eventId].Descriptor.Opcode;
          EventActivityOptions activityOptions = this.m_eventData[eventId].ActivityOptions;
          Guid* activityID = (Guid*) null;
          Guid empty1 = Guid.Empty;
          Guid empty2 = Guid.Empty;
          if (opcode != EventOpcode.Info && (IntPtr) relatedActivityId == IntPtr.Zero && (activityOptions & EventActivityOptions.Disable) == EventActivityOptions.None)
          {
            switch (opcode)
            {
              case EventOpcode.Start:
                this.m_activityTracker.OnStart(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref empty1, ref empty2, this.m_eventData[eventId].ActivityOptions);
                break;
              case EventOpcode.Stop:
                this.m_activityTracker.OnStop(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref empty1);
                break;
            }
            if (empty1 != Guid.Empty)
              activityID = &empty1;
            if (empty2 != Guid.Empty)
              relatedActivityId = &empty2;
          }
          SessionMask sessionMask = SessionMask.All;
          if ((ulong) this.m_curLiveSessions != 0UL)
            sessionMask = this.GetEtwSessionMask(eventId, relatedActivityId);
          if ((ulong) sessionMask != 0UL || this.m_legacySessions != null && this.m_legacySessions.Count > 0)
          {
            if (!this.SelfDescribingEvents)
            {
              if (sessionMask.IsEqualOrSupersetOf(this.m_curLiveSessions))
              {
                if (!this.m_provider.WriteEvent(ref this.m_eventData[eventId].Descriptor, activityID, relatedActivityId, eventDataCount, (IntPtr) ((void*) data)))
                  this.ThrowEventSourceException(this.m_eventData[eventId].Name, (Exception) null);
              }
              else
              {
                long num = this.m_eventData[eventId].Descriptor.Keywords & ~(long) SessionMask.All.ToEventKeywords();
                EventDescriptor eventDescriptor = new EventDescriptor(this.m_eventData[eventId].Descriptor.EventId, this.m_eventData[eventId].Descriptor.Version, this.m_eventData[eventId].Descriptor.Channel, this.m_eventData[eventId].Descriptor.Level, this.m_eventData[eventId].Descriptor.Opcode, this.m_eventData[eventId].Descriptor.Task, (long) sessionMask.ToEventKeywords() | num);
                if (!this.m_provider.WriteEvent(ref eventDescriptor, activityID, relatedActivityId, eventDataCount, (IntPtr) ((void*) data)))
                  this.ThrowEventSourceException(this.m_eventData[eventId].Name, (Exception) null);
              }
            }
            else
            {
              TraceLoggingEventTypes eventTypes = this.m_eventData[eventId].TraceLoggingEventTypes;
              if (eventTypes == null)
              {
                eventTypes = new TraceLoggingEventTypes(this.m_eventData[eventId].Name, EventTags.None, this.m_eventData[eventId].Parameters);
                Interlocked.CompareExchange<TraceLoggingEventTypes>(ref this.m_eventData[eventId].TraceLoggingEventTypes, eventTypes, (TraceLoggingEventTypes) null);
              }
              long num = this.m_eventData[eventId].Descriptor.Keywords & ~(long) SessionMask.All.ToEventKeywords();
              EventSourceOptions options = new EventSourceOptions()
              {
                Keywords = (EventKeywords) ((long) sessionMask.ToEventKeywords() | num),
                Level = (EventLevel) this.m_eventData[eventId].Descriptor.Level,
                Opcode = (EventOpcode) this.m_eventData[eventId].Descriptor.Opcode
              };
              this.WriteMultiMerge(this.m_eventData[eventId].Name, ref options, eventTypes, activityID, relatedActivityId, data);
            }
          }
        }
        if (this.m_Dispatchers == null || !this.m_eventData[eventId].EnabledForAnyListener)
          return;
        this.WriteToAllListeners(eventId, relatedActivityId, eventDataCount, data);
      }
      catch (Exception ex)
      {
        if (ex is EventSourceException)
          throw;
        else
          this.ThrowEventSourceException(this.m_eventData[eventId].Name, ex);
      }
    }

    /// <summary>
    ///   Записывает событие, используя предоставленные идентификатор события и массив аргументов.
    /// </summary>
    /// <param name="eventId">
    ///   Идентификатор события.
    ///     Это значение должно находиться в диапазоне от 0 до 65535.
    /// </param>
    /// <param name="args">Массив объектов.</param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEvent(int eventId, params object[] args)
    {
      this.WriteEventVarargs(eventId, (Guid*) null, args);
    }

    /// <summary>
    ///   [Поддерживается в .NET Framework 4.5.1 и более поздних версиях.]
    /// 
    ///   Записывает событие, которое указывает, что текущее действие связано с другим действием.
    /// </summary>
    /// <param name="eventId">
    ///   Идентификатор, который уникально идентифицирует это событие в источнике <see cref="T:System.Diagnostics.Tracing.EventSource" />.
    /// </param>
    /// <param name="relatedActivityId">
    ///   Идентификатор связанного действия.
    /// </param>
    /// <param name="args">
    ///   Массив объектов, которые содержат данные события.
    /// </param>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    protected unsafe void WriteEventWithRelatedActivityId(int eventId, Guid relatedActivityId, params object[] args)
    {
      this.WriteEventVarargs(eventId, &relatedActivityId, args);
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:System.Diagnostics.Tracing.EventSource" />.
    /// </summary>
    [__DynamicallyInvokable]
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>
    ///   Освобождает неуправляемые ресурсы, используемые классом <see cref="T:System.Diagnostics.Tracing.EventSource" /> (при необходимости освобождает и управляемые ресурсы).
    /// </summary>
    /// <param name="disposing">
    ///   Значение <see langword="true" /> позволяет освободить как управляемые, так и неуправляемые ресурсы; значение <see langword="false" /> освобождает только неуправляемые ресурсы.
    /// </param>
    [__DynamicallyInvokable]
    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this.m_eventSourceEnabled)
        {
          try
          {
            this.SendManifest(this.m_rawManifest);
          }
          catch (Exception ex)
          {
          }
          this.m_eventSourceEnabled = false;
        }
        if (this.m_provider != null)
        {
          this.m_provider.Dispose();
          this.m_provider = (EventSource.OverideEventProvider) null;
        }
      }
      this.m_eventSourceEnabled = false;
    }

    /// <summary>
    ///   Позволяет объекту <see cref="T:System.Diagnostics.Tracing.EventSource" /> предпринять попытку освободить ресурсы и выполнить другие операции очистки перед утилизацией объекта в процессе сборки мусора.
    /// </summary>
    [__DynamicallyInvokable]
    ~EventSource()
    {
      this.Dispose(false);
    }

    internal void WriteStringToListener(EventListener listener, string msg, SessionMask m)
    {
      if (!this.m_eventSourceEnabled)
        return;
      if (listener == null)
        this.WriteEventString(EventLevel.LogAlways, (long) m.ToEventKeywords(), msg);
      else
        listener.OnEventWritten(new EventWrittenEventArgs(this)
        {
          EventId = 0,
          Payload = new ReadOnlyCollection<object>((IList<object>) new List<object>()
          {
            (object) msg
          })
        });
    }

    [SecurityCritical]
    private unsafe void WriteEventRaw(string eventName, ref EventDescriptor eventDescriptor, Guid* activityID, Guid* relatedActivityID, int dataCount, IntPtr data)
    {
      if (this.m_provider == null)
      {
        this.ThrowEventSourceException(eventName, (Exception) null);
      }
      else
      {
        if (this.m_provider.WriteEventRaw(ref eventDescriptor, activityID, relatedActivityID, dataCount, data))
          return;
        this.ThrowEventSourceException(eventName, (Exception) null);
      }
    }

    internal EventSource(Guid eventSourceGuid, string eventSourceName)
      : this(eventSourceGuid, eventSourceName, EventSourceSettings.EtwManifestEventFormat, (string[]) null)
    {
    }

    internal EventSource(Guid eventSourceGuid, string eventSourceName, EventSourceSettings settings, string[] traits = null)
    {
      this.m_config = this.ValidateSettings(settings);
      this.Initialize(eventSourceGuid, eventSourceName, traits);
    }

    [SecuritySafeCritical]
    private unsafe void Initialize(Guid eventSourceGuid, string eventSourceName, string[] traits)
    {
      try
      {
        this.m_traits = traits;
        if (this.m_traits != null && this.m_traits.Length % 2 != 0)
          throw new ArgumentException(Environment.GetResourceString("TraitEven"), nameof (traits));
        if (eventSourceGuid == Guid.Empty)
          throw new ArgumentException(Environment.GetResourceString("EventSource_NeedGuid"));
        if (eventSourceName == null)
          throw new ArgumentException(Environment.GetResourceString("EventSource_NeedName"));
        this.m_name = eventSourceName;
        this.m_guid = eventSourceGuid;
        this.m_curLiveSessions = new SessionMask(0U);
        this.m_etwSessionIdMap = new EtwSession[4];
        this.m_activityTracker = ActivityTracker.Instance;
        this.InitializeProviderMetadata();
        EventSource.OverideEventProvider overideEventProvider = new EventSource.OverideEventProvider(this);
        overideEventProvider.Register(eventSourceGuid);
        EventListener.AddEventSource(this);
        this.m_provider = overideEventProvider;
        if (this.Name != "System.Diagnostics.Eventing.FrameworkEventSource" || Environment.OSVersion.Version.Major * 10 + Environment.OSVersion.Version.Minor >= 62)
        {
          fixed (byte* numPtr = this.providerMetadata)
            this.m_provider.SetInformation(UnsafeNativeMethods.ManifestEtw.EVENT_INFO_CLASS.SetTraits, (void*) numPtr, this.providerMetadata.Length);
        }
        this.m_completelyInited = true;
      }
      catch (Exception ex)
      {
        if (this.m_constructionException == null)
          this.m_constructionException = ex;
        this.ReportOutOfBandMessage("ERROR: Exception during construction of EventSource " + this.Name + ": " + ex.Message, true);
      }
      lock (EventListener.EventListenersLock)
      {
        for (EventCommandEventArgs commandArgs = this.m_deferredCommands; commandArgs != null; commandArgs = commandArgs.nextCommand)
          this.DoCommand(commandArgs);
      }
    }

    private static string GetName(Type eventSourceType, EventManifestOptions flags)
    {
      if (eventSourceType == (Type) null)
        throw new ArgumentNullException(nameof (eventSourceType));
      EventSourceAttribute customAttributeHelper = (EventSourceAttribute) EventSource.GetCustomAttributeHelper((MemberInfo) eventSourceType, typeof (EventSourceAttribute), flags);
      if (customAttributeHelper != null && customAttributeHelper.Name != null)
        return customAttributeHelper.Name;
      return eventSourceType.Name;
    }

    private static Guid GenerateGuidFromName(string name)
    {
      byte[] bytes = Encoding.BigEndianUnicode.GetBytes(name);
      EventSource.Sha1ForNonSecretPurposes nonSecretPurposes = new EventSource.Sha1ForNonSecretPurposes();
      nonSecretPurposes.Start();
      nonSecretPurposes.Append(EventSource.namespaceBytes);
      nonSecretPurposes.Append(bytes);
      Array.Resize<byte>(ref bytes, 16);
      nonSecretPurposes.Finish(bytes);
      bytes[7] = (byte) ((int) bytes[7] & 15 | 80);
      return new Guid(bytes);
    }

    [SecurityCritical]
    private unsafe object DecodeObject(int eventId, int parameterId, ref EventSource.EventData* data)
    {
      IntPtr dataPointer1 = data->DataPointer;
      ++data;
      for (Type type = this.m_eventData[eventId].Parameters[parameterId].ParameterType; !(type == typeof (IntPtr)); type = Enum.GetUnderlyingType(type))
      {
        if (type == typeof (int))
          return (object) *(int*) (void*) dataPointer1;
        if (type == typeof (uint))
          return (object) *(uint*) (void*) dataPointer1;
        if (type == typeof (long))
          return (object) *(long*) (void*) dataPointer1;
        if (type == typeof (ulong))
          return (object) (ulong) *(long*) (void*) dataPointer1;
        if (type == typeof (byte))
          return (object) *(byte*) (void*) dataPointer1;
        if (type == typeof (sbyte))
          return (object) *(sbyte*) (void*) dataPointer1;
        if (type == typeof (short))
          return (object) *(short*) (void*) dataPointer1;
        if (type == typeof (ushort))
          return (object) *(ushort*) (void*) dataPointer1;
        if (type == typeof (float))
          return (object) *(float*) (void*) dataPointer1;
        if (type == typeof (double))
          return (object) *(double*) (void*) dataPointer1;
        if (type == typeof (Decimal))
          return (object) *(Decimal*) (void*) dataPointer1;
        if (type == typeof (bool))
        {
          if (*(int*) (void*) dataPointer1 == 1)
            return (object) true;
          return (object) false;
        }
        if (type == typeof (Guid))
          return (object) *(Guid*) (void*) dataPointer1;
        if (type == typeof (char))
          return (object) (char) *(ushort*) (void*) dataPointer1;
        if (type == typeof (DateTime))
          return (object) DateTime.FromFileTimeUtc(*(long*) (void*) dataPointer1);
        if (type == typeof (byte[]))
        {
          int length = *(int*) (void*) dataPointer1;
          byte[] numArray = new byte[length];
          IntPtr dataPointer2 = data->DataPointer;
          ++data;
          for (int index = 0; index < length; ++index)
            numArray[index] = *(byte*) ((IntPtr) (void*) dataPointer2 + index);
          return (object) numArray;
        }
        if (type == typeof (byte*))
          return (object) null;
        if (!type.IsEnum())
          return (object) Marshal.PtrToStringUni(dataPointer1);
      }
      return (object) *(IntPtr*) (void*) dataPointer1;
    }

    private EventDispatcher GetDispatcher(EventListener listener)
    {
      EventDispatcher eventDispatcher = this.m_Dispatchers;
      while (eventDispatcher != null && eventDispatcher.m_Listener != listener)
        eventDispatcher = eventDispatcher.m_Next;
      return eventDispatcher;
    }

    [SecurityCritical]
    private unsafe void WriteEventVarargs(int eventId, Guid* childActivityID, object[] args)
    {
      if (!this.m_eventSourceEnabled)
        return;
      try
      {
        if ((IntPtr) childActivityID != IntPtr.Zero)
        {
          this.ValidateEventOpcodeForTransfer(ref this.m_eventData[eventId], this.m_eventData[eventId].Name);
          if (!this.m_eventData[eventId].HasRelatedActivityID)
            throw new ArgumentException(Environment.GetResourceString("EventSource_NoRelatedActivityId"));
        }
        this.LogEventArgsMismatches(this.m_eventData[eventId].Parameters, args);
        if (this.m_eventData[eventId].EnabledForETW)
        {
          Guid* activityID = (Guid*) null;
          Guid empty1 = Guid.Empty;
          Guid empty2 = Guid.Empty;
          EventOpcode opcode = (EventOpcode) this.m_eventData[eventId].Descriptor.Opcode;
          EventActivityOptions activityOptions = this.m_eventData[eventId].ActivityOptions;
          if ((IntPtr) childActivityID == IntPtr.Zero && (activityOptions & EventActivityOptions.Disable) == EventActivityOptions.None)
          {
            switch (opcode)
            {
              case EventOpcode.Start:
                this.m_activityTracker.OnStart(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref empty1, ref empty2, this.m_eventData[eventId].ActivityOptions);
                break;
              case EventOpcode.Stop:
                this.m_activityTracker.OnStop(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref empty1);
                break;
            }
            if (empty1 != Guid.Empty)
              activityID = &empty1;
            if (empty2 != Guid.Empty)
              childActivityID = &empty2;
          }
          SessionMask sessionMask = SessionMask.All;
          if ((ulong) this.m_curLiveSessions != 0UL)
            sessionMask = this.GetEtwSessionMask(eventId, childActivityID);
          if ((ulong) sessionMask != 0UL || this.m_legacySessions != null && this.m_legacySessions.Count > 0)
          {
            if (!this.SelfDescribingEvents)
            {
              if (sessionMask.IsEqualOrSupersetOf(this.m_curLiveSessions))
              {
                if (!this.m_provider.WriteEvent(ref this.m_eventData[eventId].Descriptor, activityID, childActivityID, args))
                  this.ThrowEventSourceException(this.m_eventData[eventId].Name, (Exception) null);
              }
              else
              {
                long num = this.m_eventData[eventId].Descriptor.Keywords & ~(long) SessionMask.All.ToEventKeywords();
                EventDescriptor eventDescriptor = new EventDescriptor(this.m_eventData[eventId].Descriptor.EventId, this.m_eventData[eventId].Descriptor.Version, this.m_eventData[eventId].Descriptor.Channel, this.m_eventData[eventId].Descriptor.Level, this.m_eventData[eventId].Descriptor.Opcode, this.m_eventData[eventId].Descriptor.Task, (long) sessionMask.ToEventKeywords() | num);
                if (!this.m_provider.WriteEvent(ref eventDescriptor, activityID, childActivityID, args))
                  this.ThrowEventSourceException(this.m_eventData[eventId].Name, (Exception) null);
              }
            }
            else
            {
              TraceLoggingEventTypes eventTypes = this.m_eventData[eventId].TraceLoggingEventTypes;
              if (eventTypes == null)
              {
                eventTypes = new TraceLoggingEventTypes(this.m_eventData[eventId].Name, EventTags.None, this.m_eventData[eventId].Parameters);
                Interlocked.CompareExchange<TraceLoggingEventTypes>(ref this.m_eventData[eventId].TraceLoggingEventTypes, eventTypes, (TraceLoggingEventTypes) null);
              }
              long num = this.m_eventData[eventId].Descriptor.Keywords & ~(long) SessionMask.All.ToEventKeywords();
              EventSourceOptions options = new EventSourceOptions()
              {
                Keywords = (EventKeywords) ((long) sessionMask.ToEventKeywords() | num),
                Level = (EventLevel) this.m_eventData[eventId].Descriptor.Level,
                Opcode = (EventOpcode) this.m_eventData[eventId].Descriptor.Opcode
              };
              this.WriteMultiMerge(this.m_eventData[eventId].Name, ref options, eventTypes, activityID, childActivityID, args);
            }
          }
        }
        if (this.m_Dispatchers == null || !this.m_eventData[eventId].EnabledForAnyListener)
          return;
        if (AppContextSwitches.PreserveEventListnerObjectIdentity)
        {
          this.WriteToAllListeners(eventId, childActivityID, args);
        }
        else
        {
          object[] objArray = this.SerializeEventArgs(eventId, args);
          this.WriteToAllListeners(eventId, childActivityID, objArray);
        }
      }
      catch (Exception ex)
      {
        if (ex is EventSourceException)
          throw;
        else
          this.ThrowEventSourceException(this.m_eventData[eventId].Name, ex);
      }
    }

    [SecurityCritical]
    private object[] SerializeEventArgs(int eventId, object[] args)
    {
      TraceLoggingEventTypes loggingEventTypes = this.m_eventData[eventId].TraceLoggingEventTypes;
      if (loggingEventTypes == null)
      {
        loggingEventTypes = new TraceLoggingEventTypes(this.m_eventData[eventId].Name, EventTags.None, this.m_eventData[eventId].Parameters);
        Interlocked.CompareExchange<TraceLoggingEventTypes>(ref this.m_eventData[eventId].TraceLoggingEventTypes, loggingEventTypes, (TraceLoggingEventTypes) null);
      }
      object[] objArray = new object[loggingEventTypes.typeInfos.Length];
      for (int index = 0; index < loggingEventTypes.typeInfos.Length; ++index)
        objArray[index] = loggingEventTypes.typeInfos[index].GetData(args[index]);
      return objArray;
    }

    private void LogEventArgsMismatches(ParameterInfo[] infos, object[] args)
    {
      bool flag = args.Length == infos.Length;
      for (int index = 0; flag && index < args.Length; ++index)
      {
        Type parameterType = infos[index].ParameterType;
        if (args[index] != null && args[index].GetType() != parameterType || args[index] == null && (!parameterType.IsGenericType || !(parameterType.GetGenericTypeDefinition() == typeof (Nullable<>))))
        {
          flag = false;
          break;
        }
      }
      if (flag)
        return;
      Debugger.Log(0, (string) null, Environment.GetResourceString("EventSource_VarArgsParameterMismatch") + "\r\n");
    }

    private int GetParamLengthIncludingByteArray(ParameterInfo[] parameters)
    {
      int num = 0;
      foreach (ParameterInfo parameter in parameters)
      {
        if (parameter.ParameterType == typeof (byte[]))
          num += 2;
        else
          ++num;
      }
      return num;
    }

    [SecurityCritical]
    private unsafe void WriteToAllListeners(int eventId, Guid* childActivityID, int eventDataCount, EventSource.EventData* data)
    {
      int val1 = this.m_eventData[eventId].Parameters.Length;
      int includingByteArray = this.GetParamLengthIncludingByteArray(this.m_eventData[eventId].Parameters);
      if (eventDataCount != includingByteArray)
      {
        this.ReportOutOfBandMessage(Environment.GetResourceString("EventSource_EventParametersMismatch", (object) eventId, (object) eventDataCount, (object) val1), true);
        val1 = Math.Min(val1, eventDataCount);
      }
      object[] objArray = new object[val1];
      EventSource.EventData* data1 = data;
      for (int parameterId = 0; parameterId < val1; ++parameterId)
        objArray[parameterId] = this.DecodeObject(eventId, parameterId, ref data1);
      this.WriteToAllListeners(eventId, childActivityID, objArray);
    }

    [SecurityCritical]
    private unsafe void WriteToAllListeners(int eventId, Guid* childActivityID, params object[] args)
    {
      EventWrittenEventArgs eventCallbackArgs = new EventWrittenEventArgs(this);
      eventCallbackArgs.EventId = eventId;
      if ((IntPtr) childActivityID != IntPtr.Zero)
        eventCallbackArgs.RelatedActivityId = *childActivityID;
      eventCallbackArgs.EventName = this.m_eventData[eventId].Name;
      eventCallbackArgs.Message = this.m_eventData[eventId].Message;
      eventCallbackArgs.Payload = new ReadOnlyCollection<object>((IList<object>) args);
      this.DispatchToAllListeners(eventId, childActivityID, eventCallbackArgs);
    }

    [SecurityCritical]
    private unsafe void DispatchToAllListeners(int eventId, Guid* childActivityID, EventWrittenEventArgs eventCallbackArgs)
    {
      Exception innerException = (Exception) null;
      for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
      {
        if (eventId == -1 || eventDispatcher.m_EventEnabled[eventId])
        {
          ActivityFilter activityFilter = eventDispatcher.m_Listener.m_activityFilter;
          if (activityFilter == null || ActivityFilter.PassesActivityFilter(activityFilter, childActivityID, this.m_eventData[eventId].TriggersActivityTracking > (byte) 0, this, eventId) || !eventDispatcher.m_activityFilteringEnabled)
          {
            try
            {
              eventDispatcher.m_Listener.OnEventWritten(eventCallbackArgs);
            }
            catch (Exception ex)
            {
              this.ReportOutOfBandMessage("ERROR: Exception during EventSource.OnEventWritten: " + ex.Message, false);
              innerException = ex;
            }
          }
        }
      }
      if (innerException != null)
        throw new EventSourceException(innerException);
    }

    [SecuritySafeCritical]
    private unsafe void WriteEventString(EventLevel level, long keywords, string msgString)
    {
      if (this.m_provider == null)
        return;
      string str1 = "EventSourceMessage";
      if (this.SelfDescribingEvents)
      {
        EventSourceOptions options = new EventSourceOptions()
        {
          Keywords = (EventKeywords) keywords,
          Level = level
        };
        var data = new{ message = msgString };
        TraceLoggingEventTypes eventTypes = new TraceLoggingEventTypes(str1, EventTags.None, new Type[1]
        {
          data.GetType()
        });
        this.WriteMultiMergeInner(str1, ref options, eventTypes, (Guid*) IntPtr.Zero, (Guid*) IntPtr.Zero, (object) data);
      }
      else
      {
        if (this.m_rawManifest == null && this.m_outOfBandMessageCount == (byte) 1)
        {
          ManifestBuilder manifestBuilder = new ManifestBuilder(this.Name, this.Guid, this.Name, (ResourceManager) null, EventManifestOptions.None);
          manifestBuilder.StartEvent(str1, new EventAttribute(0)
          {
            Level = EventLevel.LogAlways,
            Task = (EventTask) 65534
          });
          manifestBuilder.AddEventParameter(typeof (string), "message");
          manifestBuilder.EndEvent();
          this.SendManifest(manifestBuilder.CreateManifest());
        }
        string str2 = msgString;
        char* chPtr = (char*) str2;
        if ((IntPtr) chPtr != IntPtr.Zero)
          chPtr += RuntimeHelpers.OffsetToStringData;
        EventDescriptor eventDescriptor = new EventDescriptor(0, (byte) 0, (byte) 0, (byte) level, (byte) 0, 0, keywords);
        this.m_provider.WriteEvent(ref eventDescriptor, (Guid*) null, (Guid*) null, 1, (IntPtr) ((void*) &new EventProvider.EventData()
        {
          Ptr = (ulong) chPtr,
          Size = (uint) (2 * (msgString.Length + 1)),
          Reserved = 0U
        }));
        str2 = (string) null;
      }
    }

    private void WriteStringToAllListeners(string eventName, string msg)
    {
      EventWrittenEventArgs eventData = new EventWrittenEventArgs(this);
      eventData.EventId = 0;
      eventData.Message = msg;
      eventData.Payload = new ReadOnlyCollection<object>((IList<object>) new List<object>()
      {
        (object) msg
      });
      eventData.PayloadNames = new ReadOnlyCollection<string>((IList<string>) new List<string>()
      {
        "message"
      });
      eventData.EventName = eventName;
      for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
      {
        bool flag = false;
        if (eventDispatcher.m_EventEnabled == null)
        {
          flag = true;
        }
        else
        {
          for (int index = 0; index < eventDispatcher.m_EventEnabled.Length; ++index)
          {
            if (eventDispatcher.m_EventEnabled[index])
            {
              flag = true;
              break;
            }
          }
        }
        try
        {
          if (flag)
            eventDispatcher.m_Listener.OnEventWritten(eventData);
        }
        catch
        {
        }
      }
    }

    [SecurityCritical]
    private unsafe SessionMask GetEtwSessionMask(int eventId, Guid* childActivityID)
    {
      SessionMask sessionMask = new SessionMask();
      for (int index = 0; index < 4; ++index)
      {
        EtwSession etwSessionId = this.m_etwSessionIdMap[index];
        if (etwSessionId != null)
        {
          ActivityFilter activityFilter = etwSessionId.m_activityFilter;
          if (activityFilter == null && !this.m_activityFilteringForETWEnabled[index] || activityFilter != null && ActivityFilter.PassesActivityFilter(activityFilter, childActivityID, this.m_eventData[eventId].TriggersActivityTracking > (byte) 0, this, eventId) || !this.m_activityFilteringForETWEnabled[index])
            sessionMask[index] = true;
        }
      }
      if (this.m_legacySessions != null && this.m_legacySessions.Count > 0 && this.m_eventData[eventId].Descriptor.Opcode == (byte) 9)
      {
        Guid* currentActivityId = (Guid*) null;
        foreach (EtwSession legacySession in this.m_legacySessions)
        {
          if (legacySession != null)
          {
            ActivityFilter activityFilter = legacySession.m_activityFilter;
            if (activityFilter != null)
            {
              if ((IntPtr) currentActivityId == IntPtr.Zero)
                currentActivityId = &EventSource.InternalCurrentThreadActivityId;
              ActivityFilter.FlowActivityIfNeeded(activityFilter, currentActivityId, childActivityID);
            }
          }
        }
      }
      return sessionMask;
    }

    private bool IsEnabledByDefault(int eventNum, bool enable, EventLevel currentLevel, EventKeywords currentMatchAnyKeyword)
    {
      if (!enable)
        return false;
      EventLevel level = (EventLevel) this.m_eventData[eventNum].Descriptor.Level;
      EventKeywords eventKeywords = (EventKeywords) (this.m_eventData[eventNum].Descriptor.Keywords & ~(long) SessionMask.All.ToEventKeywords());
      EventChannel channel = (EventChannel) this.m_eventData[eventNum].Descriptor.Channel;
      return this.IsEnabledCommon(enable, currentLevel, currentMatchAnyKeyword, level, eventKeywords, channel);
    }

    private bool IsEnabledCommon(bool enabled, EventLevel currentLevel, EventKeywords currentMatchAnyKeyword, EventLevel eventLevel, EventKeywords eventKeywords, EventChannel eventChannel)
    {
      if (!enabled || currentLevel != EventLevel.LogAlways && currentLevel < eventLevel)
        return false;
      if (currentMatchAnyKeyword != EventKeywords.None && eventKeywords != EventKeywords.None)
      {
        if (eventChannel != EventChannel.None && this.m_channelData != null && (EventChannel) this.m_channelData.Length > eventChannel)
        {
          EventKeywords eventKeywords1 = (EventKeywords) this.m_channelData[(int) eventChannel] | eventKeywords;
          if (eventKeywords1 != EventKeywords.None && (eventKeywords1 & currentMatchAnyKeyword) == EventKeywords.None)
            return false;
        }
        else if ((eventKeywords & currentMatchAnyKeyword) == EventKeywords.None)
          return false;
      }
      return true;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void ThrowEventSourceException(string eventName, Exception innerEx = null)
    {
      if (EventSource.m_EventSourceExceptionRecurenceCount > (byte) 0)
        return;
      try
      {
        ++EventSource.m_EventSourceExceptionRecurenceCount;
        string msg = "EventSourceException";
        if (eventName != null)
          msg = msg + " while processing event \"" + eventName + "\"";
        switch (EventProvider.GetLastWriteEventError())
        {
          case EventProvider.WriteEventErrorCode.NoFreeBuffers:
            this.ReportOutOfBandMessage(msg + ": " + Environment.GetResourceString("EventSource_NoFreeBuffers"), true);
            if (!this.ThrowOnEventWriteErrors)
              break;
            throw new EventSourceException(Environment.GetResourceString("EventSource_NoFreeBuffers"), innerEx);
          case EventProvider.WriteEventErrorCode.EventTooBig:
            this.ReportOutOfBandMessage(msg + ": " + Environment.GetResourceString("EventSource_EventTooBig"), true);
            if (!this.ThrowOnEventWriteErrors)
              break;
            throw new EventSourceException(Environment.GetResourceString("EventSource_EventTooBig"), innerEx);
          case EventProvider.WriteEventErrorCode.NullInput:
            this.ReportOutOfBandMessage(msg + ": " + Environment.GetResourceString("EventSource_NullInput"), true);
            if (!this.ThrowOnEventWriteErrors)
              break;
            throw new EventSourceException(Environment.GetResourceString("EventSource_NullInput"), innerEx);
          case EventProvider.WriteEventErrorCode.TooManyArgs:
            this.ReportOutOfBandMessage(msg + ": " + Environment.GetResourceString("EventSource_TooManyArgs"), true);
            if (!this.ThrowOnEventWriteErrors)
              break;
            throw new EventSourceException(Environment.GetResourceString("EventSource_TooManyArgs"), innerEx);
          default:
            if (innerEx != null)
              this.ReportOutOfBandMessage(msg + ": " + (object) innerEx.GetType() + ":" + innerEx.Message, true);
            else
              this.ReportOutOfBandMessage(msg, true);
            if (!this.ThrowOnEventWriteErrors)
              break;
            throw new EventSourceException(innerEx);
        }
      }
      finally
      {
        --EventSource.m_EventSourceExceptionRecurenceCount;
      }
    }

    private void ValidateEventOpcodeForTransfer(ref EventSource.EventMetadata eventData, string eventName)
    {
      if (eventData.Descriptor.Opcode == (byte) 9 || eventData.Descriptor.Opcode == (byte) 240 || eventData.Descriptor.Opcode == (byte) 1)
        return;
      this.ThrowEventSourceException(eventName, (Exception) null);
    }

    internal static EventOpcode GetOpcodeWithDefault(EventOpcode opcode, string eventName)
    {
      if (opcode == EventOpcode.Info && eventName != null)
      {
        if (eventName.EndsWith("Start"))
          return EventOpcode.Start;
        if (eventName.EndsWith("Stop"))
          return EventOpcode.Stop;
      }
      return opcode;
    }

    internal void SendCommand(EventListener listener, int perEventSourceSessionId, int etwSessionId, EventCommand command, bool enable, EventLevel level, EventKeywords matchAnyKeyword, IDictionary<string, string> commandArguments)
    {
      EventCommandEventArgs commandArgs = new EventCommandEventArgs(command, commandArguments, this, listener, perEventSourceSessionId, etwSessionId, enable, level, matchAnyKeyword);
      lock (EventListener.EventListenersLock)
      {
        if (this.m_completelyInited)
        {
          this.m_deferredCommands = (EventCommandEventArgs) null;
          this.DoCommand(commandArgs);
        }
        else
        {
          commandArgs.nextCommand = this.m_deferredCommands;
          this.m_deferredCommands = commandArgs;
        }
      }
    }

    internal void DoCommand(EventCommandEventArgs commandArgs)
    {
      if (this.m_provider == null)
        return;
      this.m_outOfBandMessageCount = (byte) 0;
      bool flag1 = commandArgs.perEventSourceSessionId > 0 && commandArgs.perEventSourceSessionId <= 4;
      try
      {
        this.EnsureDescriptorsInitialized();
        commandArgs.dispatcher = this.GetDispatcher(commandArgs.listener);
        if (commandArgs.dispatcher == null && commandArgs.listener != null)
          throw new ArgumentException(Environment.GetResourceString("EventSource_ListenerNotFound"));
        if (commandArgs.Arguments == null)
          commandArgs.Arguments = (IDictionary<string, string>) new Dictionary<string, string>();
        if (commandArgs.Command == EventCommand.Update)
        {
          for (int index = 0; index < this.m_eventData.Length; ++index)
            this.EnableEventForDispatcher(commandArgs.dispatcher, index, this.IsEnabledByDefault(index, commandArgs.enable, commandArgs.level, commandArgs.matchAnyKeyword));
          if (commandArgs.enable)
          {
            if (!this.m_eventSourceEnabled)
            {
              this.m_level = commandArgs.level;
              this.m_matchAnyKeyword = commandArgs.matchAnyKeyword;
            }
            else
            {
              if (commandArgs.level > this.m_level)
                this.m_level = commandArgs.level;
              if (commandArgs.matchAnyKeyword == EventKeywords.None)
                this.m_matchAnyKeyword = EventKeywords.None;
              else if (this.m_matchAnyKeyword != EventKeywords.None)
                this.m_matchAnyKeyword |= commandArgs.matchAnyKeyword;
            }
          }
          bool flag2 = commandArgs.perEventSourceSessionId >= 0;
          if (commandArgs.perEventSourceSessionId == 0 && !commandArgs.enable)
            flag2 = false;
          if (commandArgs.listener == null)
          {
            if (!flag2)
              commandArgs.perEventSourceSessionId = -commandArgs.perEventSourceSessionId;
            --commandArgs.perEventSourceSessionId;
          }
          commandArgs.Command = flag2 ? EventCommand.Enable : EventCommand.Disable;
          if (flag2 && commandArgs.dispatcher == null && !this.SelfDescribingEvents)
            this.SendManifest(this.m_rawManifest);
          if (flag2 && commandArgs.perEventSourceSessionId != -1)
          {
            bool participateInSampling = false;
            string activityFilters;
            int sessionIdBit;
            EventSource.ParseCommandArgs(commandArgs.Arguments, out participateInSampling, out activityFilters, out sessionIdBit);
            if (commandArgs.listener == null && commandArgs.Arguments.Count > 0 && commandArgs.perEventSourceSessionId != sessionIdBit)
              throw new ArgumentException(Environment.GetResourceString("EventSource_SessionIdError", (object) (commandArgs.perEventSourceSessionId + 44), (object) (sessionIdBit + 44)));
            if (commandArgs.listener == null)
            {
              this.UpdateEtwSession(commandArgs.perEventSourceSessionId, commandArgs.etwSessionId, true, activityFilters, participateInSampling);
            }
            else
            {
              ActivityFilter.UpdateFilter(ref commandArgs.listener.m_activityFilter, this, 0, activityFilters);
              commandArgs.dispatcher.m_activityFilteringEnabled = participateInSampling;
            }
          }
          else if (!flag2 && commandArgs.listener == null && (commandArgs.perEventSourceSessionId >= 0 && commandArgs.perEventSourceSessionId < 4))
            commandArgs.Arguments["EtwSessionKeyword"] = (commandArgs.perEventSourceSessionId + 44).ToString((IFormatProvider) CultureInfo.InvariantCulture);
          if (commandArgs.enable)
            this.m_eventSourceEnabled = true;
          this.OnEventCommand(commandArgs);
          EventHandler<EventCommandEventArgs> eventCommandExecuted = this.m_eventCommandExecuted;
          if (eventCommandExecuted != null)
            eventCommandExecuted((object) this, commandArgs);
          if (commandArgs.listener == null && !flag2 && commandArgs.perEventSourceSessionId != -1)
            this.UpdateEtwSession(commandArgs.perEventSourceSessionId, commandArgs.etwSessionId, false, (string) null, false);
          if (!commandArgs.enable)
          {
            if (commandArgs.listener == null)
            {
              for (int index = 0; index < 4; ++index)
              {
                EtwSession etwSessionId = this.m_etwSessionIdMap[index];
                if (etwSessionId != null)
                  ActivityFilter.DisableFilter(ref etwSessionId.m_activityFilter, this);
              }
              this.m_activityFilteringForETWEnabled = new SessionMask(0U);
              this.m_curLiveSessions = new SessionMask(0U);
              if (this.m_etwSessionIdMap != null)
              {
                for (int index = 0; index < 4; ++index)
                  this.m_etwSessionIdMap[index] = (EtwSession) null;
              }
              if (this.m_legacySessions != null)
                this.m_legacySessions.Clear();
            }
            else
            {
              ActivityFilter.DisableFilter(ref commandArgs.listener.m_activityFilter, this);
              commandArgs.dispatcher.m_activityFilteringEnabled = false;
            }
            for (int index = 0; index < this.m_eventData.Length; ++index)
            {
              bool flag3 = false;
              for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
              {
                if (eventDispatcher.m_EventEnabled[index])
                {
                  flag3 = true;
                  break;
                }
              }
              this.m_eventData[index].EnabledForAnyListener = flag3;
            }
            if (!this.AnyEventEnabled())
            {
              this.m_level = EventLevel.LogAlways;
              this.m_matchAnyKeyword = EventKeywords.None;
              this.m_eventSourceEnabled = false;
            }
          }
          this.UpdateKwdTriggers(commandArgs.enable);
        }
        else
        {
          if (commandArgs.Command == EventCommand.SendManifest && this.m_rawManifest != null)
            this.SendManifest(this.m_rawManifest);
          this.OnEventCommand(commandArgs);
          EventHandler<EventCommandEventArgs> eventCommandExecuted = this.m_eventCommandExecuted;
          if (eventCommandExecuted != null)
            eventCommandExecuted((object) this, commandArgs);
        }
        if (!this.m_completelyInited || !(commandArgs.listener != null | flag1))
          return;
        SessionMask sessions = SessionMask.FromId(commandArgs.perEventSourceSessionId);
        this.ReportActivitySamplingInfo(commandArgs.listener, sessions);
      }
      catch (Exception ex)
      {
        this.ReportOutOfBandMessage("ERROR: Exception in Command Processing for EventSource " + this.Name + ": " + ex.Message, true);
      }
    }

    internal void UpdateEtwSession(int sessionIdBit, int etwSessionId, bool bEnable, string activityFilters, bool participateInSampling)
    {
      if (sessionIdBit < 4)
      {
        if (bEnable)
        {
          EtwSession etwSession = EtwSession.GetEtwSession(etwSessionId, true);
          ActivityFilter.UpdateFilter(ref etwSession.m_activityFilter, this, sessionIdBit, activityFilters);
          this.m_etwSessionIdMap[sessionIdBit] = etwSession;
          this.m_activityFilteringForETWEnabled[sessionIdBit] = participateInSampling;
        }
        else
        {
          EtwSession etwSession = EtwSession.GetEtwSession(etwSessionId, false);
          this.m_etwSessionIdMap[sessionIdBit] = (EtwSession) null;
          this.m_activityFilteringForETWEnabled[sessionIdBit] = false;
          if (etwSession != null)
          {
            ActivityFilter.DisableFilter(ref etwSession.m_activityFilter, this);
            EtwSession.RemoveEtwSession(etwSession);
          }
        }
        this.m_curLiveSessions[sessionIdBit] = bEnable;
      }
      else if (bEnable)
      {
        if (this.m_legacySessions == null)
          this.m_legacySessions = new List<EtwSession>(8);
        EtwSession etwSession = EtwSession.GetEtwSession(etwSessionId, true);
        if (this.m_legacySessions.Contains(etwSession))
          return;
        this.m_legacySessions.Add(etwSession);
      }
      else
      {
        EtwSession etwSession = EtwSession.GetEtwSession(etwSessionId, false);
        if (etwSession == null)
          return;
        if (this.m_legacySessions != null)
          this.m_legacySessions.Remove(etwSession);
        EtwSession.RemoveEtwSession(etwSession);
      }
    }

    internal static bool ParseCommandArgs(IDictionary<string, string> commandArguments, out bool participateInSampling, out string activityFilters, out int sessionIdBit)
    {
      bool flag = true;
      participateInSampling = false;
      if (commandArguments.TryGetValue("ActivitySamplingStartEvent", out activityFilters))
        participateInSampling = true;
      string strA;
      if (commandArguments.TryGetValue("ActivitySampling", out strA))
        participateInSampling = string.Compare(strA, "false", StringComparison.OrdinalIgnoreCase) != 0 && !(strA == "0");
      int result = -1;
      string s;
      if (!commandArguments.TryGetValue("EtwSessionKeyword", out s) || !int.TryParse(s, out result) || (result < 44 || result >= 48))
      {
        sessionIdBit = -1;
        flag = false;
      }
      else
        sessionIdBit = result - 44;
      return flag;
    }

    internal void UpdateKwdTriggers(bool enable)
    {
      if (enable)
      {
        ulong num = (ulong) this.m_matchAnyKeyword;
        if (num == 0UL)
          num = ulong.MaxValue;
        this.m_keywordTriggers = 0L;
        for (int index = 0; index < 4; ++index)
        {
          EtwSession etwSessionId = this.m_etwSessionIdMap[index];
          if (etwSessionId != null)
            ActivityFilter.UpdateKwdTriggers(etwSessionId.m_activityFilter, this.m_guid, this, (EventKeywords) num);
        }
      }
      else
        this.m_keywordTriggers = 0L;
    }

    internal bool EnableEventForDispatcher(EventDispatcher dispatcher, int eventId, bool value)
    {
      if (dispatcher == null)
      {
        if (eventId >= this.m_eventData.Length)
          return false;
        if (this.m_provider != null)
          this.m_eventData[eventId].EnabledForETW = value;
      }
      else
      {
        if (eventId >= dispatcher.m_EventEnabled.Length)
          return false;
        dispatcher.m_EventEnabled[eventId] = value;
        if (value)
          this.m_eventData[eventId].EnabledForAnyListener = true;
      }
      return true;
    }

    private bool AnyEventEnabled()
    {
      for (int index = 0; index < this.m_eventData.Length; ++index)
      {
        if (this.m_eventData[index].EnabledForETW || this.m_eventData[index].EnabledForAnyListener)
          return true;
      }
      return false;
    }

    private bool IsDisposed
    {
      get
      {
        if (this.m_provider != null)
          return this.m_provider.m_disposed;
        return true;
      }
    }

    [SecuritySafeCritical]
    private void EnsureDescriptorsInitialized()
    {
      if (this.m_eventData == null)
      {
        this.m_rawManifest = EventSource.CreateManifestAndDescriptors(this.GetType(), this.Name, this, EventManifestOptions.None);
        foreach (WeakReference eventSource in EventListener.s_EventSources)
        {
          EventSource target = eventSource.Target as EventSource;
          if (target != null && target.Guid == this.m_guid && (!target.IsDisposed && target != this))
            throw new ArgumentException(Environment.GetResourceString("EventSource_EventSourceGuidInUse", (object) this.m_guid));
        }
        for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
        {
          if (eventDispatcher.m_EventEnabled == null)
            eventDispatcher.m_EventEnabled = new bool[this.m_eventData.Length];
        }
      }
      if (EventSource.s_currentPid != 0U)
        return;
      EventSource.s_currentPid = Win32Native.GetCurrentProcessId();
    }

    [SecuritySafeCritical]
    private unsafe bool SendManifest(byte[] rawManifest)
    {
      bool flag = true;
      if (rawManifest == null)
        return false;
      fixed (byte* numPtr = rawManifest)
      {
        EventDescriptor eventDescriptor = new EventDescriptor(65534, (byte) 1, (byte) 0, (byte) 0, (byte) 254, 65534, 72057594037927935L);
        ManifestEnvelope manifestEnvelope = new ManifestEnvelope();
        manifestEnvelope.Format = ManifestEnvelope.ManifestFormats.SimpleXmlFormat;
        manifestEnvelope.MajorVersion = (byte) 1;
        manifestEnvelope.MinorVersion = (byte) 0;
        manifestEnvelope.Magic = (byte) 91;
        int length = rawManifest.Length;
        manifestEnvelope.ChunkNumber = (ushort) 0;
        EventProvider.EventData* eventDataPtr = stackalloc EventProvider.EventData[2];
        eventDataPtr->Ptr = (ulong) &manifestEnvelope;
        eventDataPtr->Size = (uint) sizeof (ManifestEnvelope);
        eventDataPtr->Reserved = 0U;
        eventDataPtr[1].Ptr = (ulong) numPtr;
        eventDataPtr[1].Reserved = 0U;
        int val2 = 65280;
label_3:
        manifestEnvelope.TotalChunks = (ushort) ((length + (val2 - 1)) / val2);
        while (length > 0)
        {
          eventDataPtr[1].Size = (uint) Math.Min(length, val2);
          if (this.m_provider != null && !this.m_provider.WriteEvent(ref eventDescriptor, (Guid*) null, (Guid*) null, 2, (IntPtr) ((void*) eventDataPtr)))
          {
            if (EventProvider.GetLastWriteEventError() == EventProvider.WriteEventErrorCode.EventTooBig && manifestEnvelope.ChunkNumber == (ushort) 0 && val2 > 256)
            {
              val2 /= 2;
              goto label_3;
            }
            else
            {
              flag = false;
              if (this.ThrowOnEventWriteErrors)
              {
                this.ThrowEventSourceException(nameof (SendManifest), (Exception) null);
                break;
              }
              break;
            }
          }
          else
          {
            length -= val2;
            eventDataPtr[1].Ptr += (ulong) (uint) val2;
            ++manifestEnvelope.ChunkNumber;
            if ((int) manifestEnvelope.ChunkNumber % 5 == 0)
              Thread.Sleep(15);
          }
        }
      }
      return flag;
    }

    internal static Attribute GetCustomAttributeHelper(MemberInfo member, Type attributeType, EventManifestOptions flags = EventManifestOptions.None)
    {
      if (!member.Module.Assembly.ReflectionOnly() && (flags & EventManifestOptions.AllowEventSourceOverride) == EventManifestOptions.None)
      {
        Attribute attribute = (Attribute) null;
        object[] customAttributes = member.GetCustomAttributes(attributeType, false);
        int index = 0;
        if (index < customAttributes.Length)
          attribute = (Attribute) customAttributes[index];
        return attribute;
      }
      string fullName = attributeType.FullName;
      foreach (CustomAttributeData customAttribute in (IEnumerable<CustomAttributeData>) CustomAttributeData.GetCustomAttributes(member))
      {
        if (EventSource.AttributeTypeNamesMatch(attributeType, customAttribute.Constructor.ReflectedType))
        {
          Attribute attribute = (Attribute) null;
          CustomAttributeTypedArgument attributeTypedArgument;
          if (customAttribute.ConstructorArguments.Count == 1)
          {
            Type type = attributeType;
            object[] objArray = new object[1];
            int index = 0;
            attributeTypedArgument = customAttribute.ConstructorArguments[0];
            object obj = attributeTypedArgument.Value;
            objArray[index] = obj;
            attribute = (Attribute) Activator.CreateInstance(type, objArray);
          }
          else if (customAttribute.ConstructorArguments.Count == 0)
            attribute = (Attribute) Activator.CreateInstance(attributeType);
          if (attribute != null)
          {
            Type type = attribute.GetType();
            foreach (CustomAttributeNamedArgument namedArgument in (IEnumerable<CustomAttributeNamedArgument>) customAttribute.NamedArguments)
            {
              PropertyInfo property = type.GetProperty(namedArgument.MemberInfo.Name, BindingFlags.Instance | BindingFlags.Public);
              attributeTypedArgument = namedArgument.TypedValue;
              object obj = attributeTypedArgument.Value;
              if (property.PropertyType.IsEnum)
                obj = Enum.Parse(property.PropertyType, obj.ToString());
              property.SetValue((object) attribute, obj, (object[]) null);
            }
            return attribute;
          }
        }
      }
      return (Attribute) null;
    }

    private static bool AttributeTypeNamesMatch(Type attributeType, Type reflectedAttributeType)
    {
      if (attributeType == reflectedAttributeType || string.Equals(attributeType.FullName, reflectedAttributeType.FullName, StringComparison.Ordinal))
        return true;
      if (string.Equals(attributeType.Name, reflectedAttributeType.Name, StringComparison.Ordinal) && attributeType.Namespace.EndsWith("Diagnostics.Tracing"))
        return reflectedAttributeType.Namespace.EndsWith("Diagnostics.Tracing");
      return false;
    }

    private static Type GetEventSourceBaseType(Type eventSourceType, bool allowEventSourceOverride, bool reflectionOnly)
    {
      if (eventSourceType.BaseType() == (Type) null)
        return (Type) null;
      do
      {
        eventSourceType = eventSourceType.BaseType();
      }
      while (eventSourceType != (Type) null && eventSourceType.IsAbstract());
      if (eventSourceType != (Type) null)
      {
        if (!allowEventSourceOverride)
        {
          if (reflectionOnly && eventSourceType.FullName != typeof (EventSource).FullName || !reflectionOnly && eventSourceType != typeof (EventSource))
            return (Type) null;
        }
        else if (eventSourceType.Name != nameof (EventSource))
          return (Type) null;
      }
      return eventSourceType;
    }

    private static byte[] CreateManifestAndDescriptors(Type eventSourceType, string eventSourceDllName, EventSource source, EventManifestOptions flags = EventManifestOptions.None)
    {
      ManifestBuilder manifest = (ManifestBuilder) null;
      bool flag1 = source == null || !source.SelfDescribingEvents;
      Exception innerException = (Exception) null;
      byte[] numArray = (byte[]) null;
      if (eventSourceType.IsAbstract() && (flags & EventManifestOptions.Strict) == EventManifestOptions.None)
        return (byte[]) null;
      try
      {
        MethodInfo[] methods = eventSourceType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        int eventId = 1;
        EventSource.EventMetadata[] eventData = (EventSource.EventMetadata[]) null;
        Dictionary<string, string> eventsByName = (Dictionary<string, string>) null;
        if (source != null || (flags & EventManifestOptions.Strict) != EventManifestOptions.None)
        {
          eventData = new EventSource.EventMetadata[methods.Length + 1];
          eventData[0].Name = "";
        }
        ResourceManager resources = (ResourceManager) null;
        EventSourceAttribute customAttributeHelper = (EventSourceAttribute) EventSource.GetCustomAttributeHelper((MemberInfo) eventSourceType, typeof (EventSourceAttribute), flags);
        if (customAttributeHelper != null && customAttributeHelper.LocalizationResources != null)
          resources = new ResourceManager(customAttributeHelper.LocalizationResources, eventSourceType.Assembly());
        manifest = new ManifestBuilder(EventSource.GetName(eventSourceType, flags), EventSource.GetGuid(eventSourceType), eventSourceDllName, resources, flags);
        manifest.StartEvent("EventSourceMessage", new EventAttribute(0)
        {
          Level = EventLevel.LogAlways,
          Task = (EventTask) 65534
        });
        manifest.AddEventParameter(typeof (string), "message");
        manifest.EndEvent();
        if ((flags & EventManifestOptions.Strict) != EventManifestOptions.None)
        {
          if (!(EventSource.GetEventSourceBaseType(eventSourceType, (uint) (flags & EventManifestOptions.AllowEventSourceOverride) > 0U, eventSourceType.Assembly().ReflectionOnly()) != (Type) null))
            manifest.ManifestError(Environment.GetResourceString("EventSource_TypeMustDeriveFromEventSource"), false);
          if (!eventSourceType.IsAbstract() && !eventSourceType.IsSealed())
            manifest.ManifestError(Environment.GetResourceString("EventSource_TypeMustBeSealedOrAbstract"), false);
        }
        string[] strArray = new string[3]
        {
          "Keywords",
          "Tasks",
          "Opcodes"
        };
        foreach (string str in strArray)
        {
          Type nestedType = eventSourceType.GetNestedType(str);
          if (nestedType != (Type) null)
          {
            if (eventSourceType.IsAbstract())
            {
              manifest.ManifestError(Environment.GetResourceString("EventSource_AbstractMustNotDeclareKTOC", (object) nestedType.Name), false);
            }
            else
            {
              foreach (FieldInfo field in nestedType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                EventSource.AddProviderEnumKind(manifest, field, str);
            }
          }
        }
        manifest.AddKeyword("Session3", 17592186044416UL);
        manifest.AddKeyword("Session2", 35184372088832UL);
        manifest.AddKeyword("Session1", 70368744177664UL);
        manifest.AddKeyword("Session0", 140737488355328UL);
        if (eventSourceType != typeof (EventSource))
        {
          for (int index1 = 0; index1 < methods.Length; ++index1)
          {
            MethodInfo method = methods[index1];
            ParameterInfo[] parameters = method.GetParameters();
            EventAttribute eventAttribute = (EventAttribute) EventSource.GetCustomAttributeHelper((MemberInfo) method, typeof (EventAttribute), flags);
            if (eventAttribute != null && source != null && eventAttribute.EventId <= 3 && source.Guid.Equals(EventSource.AspNetEventSourceGuid))
              eventAttribute.ActivityOptions |= EventActivityOptions.Disable;
            if (!method.IsStatic)
            {
              if (eventSourceType.IsAbstract())
              {
                if (eventAttribute != null)
                  manifest.ManifestError(Environment.GetResourceString("EventSource_AbstractMustNotDeclareEventMethods", (object) method.Name, (object) eventAttribute.EventId), false);
              }
              else
              {
                if (eventAttribute == null)
                {
                  if (!(method.ReturnType != typeof (void)) && !method.IsVirtual && EventSource.GetCustomAttributeHelper((MemberInfo) method, typeof (NonEventAttribute), flags) == null)
                    eventAttribute = new EventAttribute(eventId);
                  else
                    continue;
                }
                else if (eventAttribute.EventId <= 0)
                {
                  manifest.ManifestError(Environment.GetResourceString("EventSource_NeedPositiveId", (object) method.Name), true);
                  continue;
                }
                if (method.Name.LastIndexOf('.') >= 0)
                  manifest.ManifestError(Environment.GetResourceString("EventSource_EventMustNotBeExplicitImplementation", (object) method.Name, (object) eventAttribute.EventId), false);
                ++eventId;
                string name = method.Name;
                if (eventAttribute.Opcode == EventOpcode.Info)
                {
                  bool flag2 = eventAttribute.Task == EventTask.None;
                  if (flag2)
                    eventAttribute.Task = (EventTask) (65534 - eventAttribute.EventId);
                  if (!eventAttribute.IsOpcodeSet)
                    eventAttribute.Opcode = EventSource.GetOpcodeWithDefault(EventOpcode.Info, name);
                  if (flag2)
                  {
                    if (eventAttribute.Opcode == EventOpcode.Start)
                    {
                      string str = name.Substring(0, name.Length - "Start".Length);
                      if (string.Compare(name, 0, str, 0, str.Length) == 0 && string.Compare(name, str.Length, "Start", 0, Math.Max(name.Length - str.Length, "Start".Length)) == 0)
                        manifest.AddTask(str, (int) eventAttribute.Task);
                    }
                    else if (eventAttribute.Opcode == EventOpcode.Stop)
                    {
                      int index2 = eventAttribute.EventId - 1;
                      if (eventData != null && index2 < eventData.Length)
                      {
                        EventSource.EventMetadata eventMetadata = eventData[index2];
                        string strB = name.Substring(0, name.Length - "Stop".Length);
                        if (eventMetadata.Descriptor.Opcode == (byte) 1 && string.Compare(eventMetadata.Name, 0, strB, 0, strB.Length) == 0 && string.Compare(eventMetadata.Name, strB.Length, "Start", 0, Math.Max(eventMetadata.Name.Length - strB.Length, "Start".Length)) == 0)
                        {
                          eventAttribute.Task = (EventTask) eventMetadata.Descriptor.Task;
                          flag2 = false;
                        }
                      }
                      if (flag2 && (flags & EventManifestOptions.Strict) != EventManifestOptions.None)
                        throw new ArgumentException(Environment.GetResourceString("EventSource_StopsFollowStarts"));
                    }
                  }
                }
                bool hasRelatedActivityID = EventSource.RemoveFirstArgIfRelatedActivityId(ref parameters);
                if (source == null || !source.SelfDescribingEvents)
                {
                  manifest.StartEvent(name, eventAttribute);
                  for (int index2 = 0; index2 < parameters.Length; ++index2)
                    manifest.AddEventParameter(parameters[index2].ParameterType, parameters[index2].Name);
                  manifest.EndEvent();
                }
                if (source != null || (flags & EventManifestOptions.Strict) != EventManifestOptions.None)
                {
                  EventSource.DebugCheckEvent(ref eventsByName, eventData, method, eventAttribute, manifest, flags);
                  if (eventAttribute.Channel != EventChannel.None)
                    eventAttribute.Keywords |= (EventKeywords) manifest.GetChannelKeyword(eventAttribute.Channel);
                  string key = "event_" + name;
                  string localizedMessage = manifest.GetLocalizedMessage(key, CultureInfo.CurrentUICulture, false);
                  if (localizedMessage != null)
                    eventAttribute.Message = localizedMessage;
                  EventSource.AddEventDescriptor(ref eventData, name, eventAttribute, parameters, hasRelatedActivityID);
                }
              }
            }
          }
        }
        NameInfo.ReserveEventIDsBelow(eventId);
        if (source != null)
        {
          EventSource.TrimEventDescriptors(ref eventData);
          source.m_eventData = eventData;
          source.m_channelData = manifest.GetChannelData();
        }
        if (!eventSourceType.IsAbstract())
        {
          if (source != null)
          {
            if (source.SelfDescribingEvents)
              goto label_74;
          }
          flag1 = (flags & EventManifestOptions.OnlyIfNeededForRegistration) == EventManifestOptions.None || (uint) manifest.GetChannelData().Length > 0U;
          if (!flag1 && (flags & EventManifestOptions.Strict) == EventManifestOptions.None)
            return (byte[]) null;
          numArray = manifest.CreateManifest();
        }
      }
      catch (Exception ex)
      {
        if ((flags & EventManifestOptions.Strict) == EventManifestOptions.None)
          throw;
        else
          innerException = ex;
      }
label_74:
      if ((flags & EventManifestOptions.Strict) != EventManifestOptions.None && (manifest.Errors.Count > 0 || innerException != null))
      {
        string message = string.Empty;
        if (manifest.Errors.Count > 0)
        {
          bool flag2 = true;
          foreach (string error in (IEnumerable<string>) manifest.Errors)
          {
            if (!flag2)
              message += Environment.NewLine;
            flag2 = false;
            message += error;
          }
        }
        else
          message = "Unexpected error: " + innerException.Message;
        throw new ArgumentException(message, innerException);
      }
      if (!flag1)
        return (byte[]) null;
      return numArray;
    }

    private static bool RemoveFirstArgIfRelatedActivityId(ref ParameterInfo[] args)
    {
      if (args.Length == 0 || !(args[0].ParameterType == typeof (Guid)) || string.Compare(args[0].Name, "relatedActivityId", StringComparison.OrdinalIgnoreCase) != 0)
        return false;
      ParameterInfo[] parameterInfoArray = new ParameterInfo[args.Length - 1];
      Array.Copy((Array) args, 1, (Array) parameterInfoArray, 0, args.Length - 1);
      args = parameterInfoArray;
      return true;
    }

    private static void AddProviderEnumKind(ManifestBuilder manifest, FieldInfo staticField, string providerEnumKind)
    {
      bool flag = staticField.Module.Assembly.ReflectionOnly();
      Type fieldType = staticField.FieldType;
      if (!flag && fieldType == typeof (EventOpcode) || EventSource.AttributeTypeNamesMatch(fieldType, typeof (EventOpcode)))
      {
        if (!(providerEnumKind != "Opcodes"))
        {
          int rawConstantValue = (int) staticField.GetRawConstantValue();
          manifest.AddOpcode(staticField.Name, rawConstantValue);
          return;
        }
      }
      else if (!flag && fieldType == typeof (EventTask) || EventSource.AttributeTypeNamesMatch(fieldType, typeof (EventTask)))
      {
        if (!(providerEnumKind != "Tasks"))
        {
          int rawConstantValue = (int) staticField.GetRawConstantValue();
          manifest.AddTask(staticField.Name, rawConstantValue);
          return;
        }
      }
      else
      {
        if ((flag || !(fieldType == typeof (EventKeywords))) && !EventSource.AttributeTypeNamesMatch(fieldType, typeof (EventKeywords)))
          return;
        if (!(providerEnumKind != "Keywords"))
        {
          ulong rawConstantValue = (ulong) (long) staticField.GetRawConstantValue();
          manifest.AddKeyword(staticField.Name, rawConstantValue);
          return;
        }
      }
      manifest.ManifestError(Environment.GetResourceString("EventSource_EnumKindMismatch", (object) staticField.Name, (object) staticField.FieldType.Name, (object) providerEnumKind), false);
    }

    private static void AddEventDescriptor(ref EventSource.EventMetadata[] eventData, string eventName, EventAttribute eventAttribute, ParameterInfo[] eventParameters, bool hasRelatedActivityID)
    {
      if (eventData == null || eventData.Length <= eventAttribute.EventId)
      {
        EventSource.EventMetadata[] eventMetadataArray = new EventSource.EventMetadata[Math.Max(eventData.Length + 16, eventAttribute.EventId + 1)];
        Array.Copy((Array) eventData, (Array) eventMetadataArray, eventData.Length);
        eventData = eventMetadataArray;
      }
      eventData[eventAttribute.EventId].Descriptor = new EventDescriptor(eventAttribute.EventId, eventAttribute.Version, (byte) eventAttribute.Channel, (byte) eventAttribute.Level, (byte) eventAttribute.Opcode, (int) eventAttribute.Task, (long) (eventAttribute.Keywords | (EventKeywords) SessionMask.All.ToEventKeywords()));
      eventData[eventAttribute.EventId].Tags = eventAttribute.Tags;
      eventData[eventAttribute.EventId].Name = eventName;
      eventData[eventAttribute.EventId].Parameters = eventParameters;
      eventData[eventAttribute.EventId].Message = eventAttribute.Message;
      eventData[eventAttribute.EventId].ActivityOptions = eventAttribute.ActivityOptions;
      eventData[eventAttribute.EventId].HasRelatedActivityID = hasRelatedActivityID;
    }

    private static void TrimEventDescriptors(ref EventSource.EventMetadata[] eventData)
    {
      int length = eventData.Length;
      while (0 < length)
      {
        --length;
        if (eventData[length].Descriptor.EventId != 0)
          break;
      }
      if (eventData.Length - length <= 2)
        return;
      EventSource.EventMetadata[] eventMetadataArray = new EventSource.EventMetadata[length + 1];
      Array.Copy((Array) eventData, (Array) eventMetadataArray, eventMetadataArray.Length);
      eventData = eventMetadataArray;
    }

    internal void AddListener(EventListener listener)
    {
      lock (EventListener.EventListenersLock)
      {
        bool[] eventEnabled = (bool[]) null;
        if (this.m_eventData != null)
          eventEnabled = new bool[this.m_eventData.Length];
        this.m_Dispatchers = new EventDispatcher(this.m_Dispatchers, eventEnabled, listener);
        listener.OnEventSourceCreated(this);
      }
    }

    private static void DebugCheckEvent(ref Dictionary<string, string> eventsByName, EventSource.EventMetadata[] eventData, MethodInfo method, EventAttribute eventAttribute, ManifestBuilder manifest, EventManifestOptions options)
    {
      int eventId = eventAttribute.EventId;
      string name = method.Name;
      int helperCallFirstArg = EventSource.GetHelperCallFirstArg(method);
      if (helperCallFirstArg >= 0 && eventId != helperCallFirstArg)
        manifest.ManifestError(Environment.GetResourceString("EventSource_MismatchIdToWriteEvent", (object) name, (object) eventId, (object) helperCallFirstArg), true);
      if (eventId < eventData.Length && eventData[eventId].Descriptor.EventId != 0)
        manifest.ManifestError(Environment.GetResourceString("EventSource_EventIdReused", (object) name, (object) eventId, (object) eventData[eventId].Name), true);
      for (int index = 0; index < eventData.Length; ++index)
      {
        if (eventData[index].Name != null && (EventTask) eventData[index].Descriptor.Task == eventAttribute.Task && (EventOpcode) eventData[index].Descriptor.Opcode == eventAttribute.Opcode)
        {
          manifest.ManifestError(Environment.GetResourceString("EventSource_TaskOpcodePairReused", (object) name, (object) eventId, (object) eventData[index].Name, (object) index), false);
          if ((options & EventManifestOptions.Strict) == EventManifestOptions.None)
            break;
        }
      }
      if (eventAttribute.Opcode != EventOpcode.Info)
      {
        bool flag = false;
        if (eventAttribute.Task == EventTask.None)
        {
          flag = true;
        }
        else
        {
          EventTask eventTask = (EventTask) (65534 - eventId);
          if (eventAttribute.Opcode != EventOpcode.Start && eventAttribute.Opcode != EventOpcode.Stop && eventAttribute.Task == eventTask)
            flag = true;
        }
        if (flag)
          manifest.ManifestError(Environment.GetResourceString("EventSource_EventMustHaveTaskIfNonDefaultOpcode", (object) name, (object) eventId), false);
      }
      if (eventsByName == null)
        eventsByName = new Dictionary<string, string>();
      if (eventsByName.ContainsKey(name))
        manifest.ManifestError(Environment.GetResourceString("EventSource_EventNameReused", (object) name), true);
      eventsByName[name] = name;
    }

    [SecuritySafeCritical]
    private static int GetHelperCallFirstArg(MethodInfo method)
    {
      new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
      byte[] ilAsByteArray = method.GetMethodBody().GetILAsByteArray();
      int num = -1;
      for (int index1 = 0; index1 < ilAsByteArray.Length; ++index1)
      {
        switch (ilAsByteArray[index1])
        {
          case 0:
          case 1:
          case 2:
          case 3:
          case 4:
          case 5:
          case 6:
          case 7:
          case 8:
          case 9:
          case 10:
          case 11:
          case 12:
          case 13:
          case 20:
          case 37:
          case 103:
          case 104:
          case 105:
          case 106:
          case 109:
          case 110:
          case 162:
            continue;
          case 14:
          case 16:
            ++index1;
            continue;
          case 21:
          case 22:
          case 23:
          case 24:
          case 25:
          case 26:
          case 27:
          case 28:
          case 29:
          case 30:
            if (index1 > 0 && ilAsByteArray[index1 - 1] == (byte) 2)
            {
              num = (int) ilAsByteArray[index1] - 22;
              continue;
            }
            continue;
          case 31:
            if (index1 > 0 && ilAsByteArray[index1 - 1] == (byte) 2)
              num = (int) ilAsByteArray[index1 + 1];
            ++index1;
            continue;
          case 32:
            index1 += 4;
            continue;
          case 40:
            index1 += 4;
            if (num >= 0)
            {
              for (int index2 = index1 + 1; index2 < ilAsByteArray.Length; ++index2)
              {
                if (ilAsByteArray[index2] == (byte) 42)
                  return num;
                if (ilAsByteArray[index2] != (byte) 0)
                  break;
              }
            }
            num = -1;
            continue;
          case 44:
          case 45:
            num = -1;
            ++index1;
            continue;
          case 57:
          case 58:
            num = -1;
            index1 += 4;
            continue;
          case 140:
          case 141:
            index1 += 4;
            continue;
          case 254:
            ++index1;
            if (index1 >= ilAsByteArray.Length || ilAsByteArray[index1] >= (byte) 6)
              break;
            continue;
        }
        return -1;
      }
      return -1;
    }

    internal void ReportOutOfBandMessage(string msg, bool flush)
    {
      try
      {
        Debugger.Log(0, (string) null, msg + "\r\n");
        if (this.m_outOfBandMessageCount < (byte) 15)
        {
          ++this.m_outOfBandMessageCount;
        }
        else
        {
          if (this.m_outOfBandMessageCount == (byte) 16)
            return;
          this.m_outOfBandMessageCount = (byte) 16;
          msg = "Reached message limit.   End of EventSource error messages.";
        }
        this.WriteEventString(EventLevel.LogAlways, -1L, msg);
        this.WriteStringToAllListeners("EventSourceMessage", msg);
      }
      catch (Exception ex)
      {
      }
    }

    private EventSourceSettings ValidateSettings(EventSourceSettings settings)
    {
      EventSourceSettings eventSourceSettings = EventSourceSettings.EtwManifestEventFormat | EventSourceSettings.EtwSelfDescribingEventFormat;
      if ((settings & eventSourceSettings) == eventSourceSettings)
        throw new ArgumentException(Environment.GetResourceString("EventSource_InvalidEventFormat"), nameof (settings));
      if ((settings & eventSourceSettings) == EventSourceSettings.Default)
        settings |= EventSourceSettings.EtwSelfDescribingEventFormat;
      return settings;
    }

    private bool ThrowOnEventWriteErrors
    {
      get
      {
        return (uint) (this.m_config & EventSourceSettings.ThrowOnEventWriteErrors) > 0U;
      }
      set
      {
        if (value)
          this.m_config |= EventSourceSettings.ThrowOnEventWriteErrors;
        else
          this.m_config &= ~EventSourceSettings.ThrowOnEventWriteErrors;
      }
    }

    private bool SelfDescribingEvents
    {
      get
      {
        return (uint) (this.m_config & EventSourceSettings.EtwSelfDescribingEventFormat) > 0U;
      }
      set
      {
        if (!value)
        {
          this.m_config |= EventSourceSettings.EtwManifestEventFormat;
          this.m_config &= ~EventSourceSettings.EtwSelfDescribingEventFormat;
        }
        else
        {
          this.m_config |= EventSourceSettings.EtwSelfDescribingEventFormat;
          this.m_config &= ~EventSourceSettings.EtwManifestEventFormat;
        }
      }
    }

    private void ReportActivitySamplingInfo(EventListener listener, SessionMask sessions)
    {
      for (int index = 0; index < 4; ++index)
      {
        if (sessions[index])
        {
          ActivityFilter activityFilter = listener != null ? listener.m_activityFilter : this.m_etwSessionIdMap[index].m_activityFilter;
          if (activityFilter != null)
          {
            SessionMask m = new SessionMask();
            m[index] = true;
            foreach (Tuple<int, int> tuple in activityFilter.GetFilterAsTuple(this.m_guid))
              this.WriteStringToListener(listener, string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Session {0}: {1} = {2}", (object) index, (object) tuple.Item1, (object) tuple.Item2), m);
            bool flag = listener == null ? this.m_activityFilteringForETWEnabled[index] : this.GetDispatcher(listener).m_activityFilteringEnabled;
            this.WriteStringToListener(listener, string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Session {0}: Activity Sampling support: {1}", (object) index, flag ? (object) "enabled" : (object) "disabled"), m);
          }
        }
      }
    }

    /// <summary>
    ///   Создает экземпляр класса <see cref="T:System.Diagnostics.Tracing.EventSource" /> с указанным именем.
    /// </summary>
    /// <param name="eventSourceName">
    ///   Имя, назначаемое источнику событий.
    ///    Значение не должно быть равно <see langword="null" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="eventSourceName" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public EventSource(string eventSourceName)
      : this(eventSourceName, EventSourceSettings.EtwSelfDescribingEventFormat)
    {
    }

    /// <summary>
    ///   Создает экземпляр класса <see cref="T:System.Diagnostics.Tracing.EventSource" /> с указанным именем и параметрами.
    /// </summary>
    /// <param name="eventSourceName">
    ///   Имя, назначаемое источнику событий.
    ///    Значение не должно быть равно <see langword="null" />.
    /// </param>
    /// <param name="config">
    ///   Побитовое сочетание значений перечисления, которое определяет параметры конфигурации, применяемые к источнику события.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="eventSourceName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="eventSourceName" /> имеет значение <see langword="null" />.
    /// </exception>
    [__DynamicallyInvokable]
    public EventSource(string eventSourceName, EventSourceSettings config)
      : this(eventSourceName, config, (string[]) null)
    {
    }

    /// <summary>
    ///   Создает экземпляр класса <see cref="T:System.Diagnostics.Tracing.EventSource" /> с указанными параметрами конфигурации.
    /// </summary>
    /// <param name="eventSourceName">
    ///   Имя, назначаемое источнику событий.
    ///    Значение не должно быть равно <see langword="null" />.
    /// </param>
    /// <param name="config">
    ///   Побитовое сочетание значений перечисления, которое определяет параметры конфигурации, применяемые к источнику события.
    /// </param>
    /// <param name="traits">
    ///   Пары ключ-значение, определяющие признаки для источника события.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="eventSourceName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="traits" /> не указан в пары "ключ значение".
    /// </exception>
    [__DynamicallyInvokable]
    public EventSource(string eventSourceName, EventSourceSettings config, params string[] traits)
      : this(eventSourceName == null ? new Guid() : EventSource.GenerateGuidFromName(eventSourceName.ToUpperInvariant()), eventSourceName, config, traits)
    {
      if (eventSourceName == null)
        throw new ArgumentNullException(nameof (eventSourceName));
    }

    /// <summary>
    ///   Записывает событие без полей, но с указанным именем и параметрами по умолчанию.
    /// </summary>
    /// <param name="eventName">Имя записываемого события.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="eventName" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe void Write(string eventName)
    {
      if (eventName == null)
        throw new ArgumentNullException(nameof (eventName));
      if (!this.IsEnabled())
        return;
      EventSourceOptions options = new EventSourceOptions();
      EmptyStruct data = new EmptyStruct();
      this.WriteImpl<EmptyStruct>(eventName, ref options, ref data, (Guid*) null, (Guid*) null);
    }

    /// <summary>
    ///   Записывает событие без полей, но с указанными именем и параметрами.
    /// </summary>
    /// <param name="eventName">Имя записываемого события.</param>
    /// <param name="options">
    ///   Параметры события, такие как уровень, ключевые слова и код операции.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="eventName" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe void Write(string eventName, EventSourceOptions options)
    {
      if (eventName == null)
        throw new ArgumentNullException(nameof (eventName));
      if (!this.IsEnabled())
        return;
      EmptyStruct data = new EmptyStruct();
      this.WriteImpl<EmptyStruct>(eventName, ref options, ref data, (Guid*) null, (Guid*) null);
    }

    /// <summary>Записывает событие с указанными именем и данными.</summary>
    /// <param name="eventName">Имя события.</param>
    /// <param name="data">
    ///   Данные события.
    ///    Тип должен быть анонимным или помеченным атрибутом <see cref="T:System.Diagnostics.Tracing.EventDataAttribute" />.
    /// </param>
    /// <typeparam name="T">
    ///   Тип, определяющий событие и связанные данные.
    ///    Тип должен быть анонимным или помеченным атрибутом <see cref="T:System.Diagnostics.Tracing.EventSourceAttribute" />.
    /// </typeparam>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe void Write<T>(string eventName, T data)
    {
      if (!this.IsEnabled())
        return;
      EventSourceOptions options = new EventSourceOptions();
      this.WriteImpl<T>(eventName, ref options, ref data, (Guid*) null, (Guid*) null);
    }

    /// <summary>
    ///   Записывает событие с указанными именем, данными и параметрами.
    /// </summary>
    /// <param name="eventName">Имя события.</param>
    /// <param name="options">Параметры события.</param>
    /// <param name="data">
    ///   Данные события.
    ///    Тип должен быть анонимным или помеченным атрибутом <see cref="T:System.Diagnostics.Tracing.EventDataAttribute" />.
    /// </param>
    /// <typeparam name="T">
    ///   Тип, определяющий событие и связанные данные.
    ///    Тип должен быть анонимным или помеченным атрибутом <see cref="T:System.Diagnostics.Tracing.EventSourceAttribute" />.
    /// </typeparam>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe void Write<T>(string eventName, EventSourceOptions options, T data)
    {
      if (!this.IsEnabled())
        return;
      this.WriteImpl<T>(eventName, ref options, ref data, (Guid*) null, (Guid*) null);
    }

    /// <summary>
    ///   Записывает событие с указанными именем, параметрами и данными.
    /// </summary>
    /// <param name="eventName">Имя события.</param>
    /// <param name="options">Параметры события.</param>
    /// <param name="data">
    ///   Данные события.
    ///    Тип должен быть анонимным или помеченным атрибутом <see cref="T:System.Diagnostics.Tracing.EventDataAttribute" />.
    /// </param>
    /// <typeparam name="T">
    ///   Тип, определяющий событие и связанные данные.
    ///    Тип должен быть анонимным или помеченным атрибутом <see cref="T:System.Diagnostics.Tracing.EventSourceAttribute" />.
    /// </typeparam>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe void Write<T>(string eventName, ref EventSourceOptions options, ref T data)
    {
      if (!this.IsEnabled())
        return;
      this.WriteImpl<T>(eventName, ref options, ref data, (Guid*) null, (Guid*) null);
    }

    /// <summary>
    ///   Записывает событие с указанными именем, параметрами, связанным действием и данными.
    /// </summary>
    /// <param name="eventName">Имя события.</param>
    /// <param name="options">Параметры события.</param>
    /// <param name="activityId">
    ///   Идентификатор действия, связанного с событием.
    /// </param>
    /// <param name="relatedActivityId">
    ///   Идентификатор связанного действия либо значение <see cref="F:System.Guid.Empty" />, если связанное действие отсутствует.
    /// </param>
    /// <param name="data">
    ///   Данные события.
    ///    Тип должен быть анонимным или помеченным атрибутом <see cref="T:System.Diagnostics.Tracing.EventDataAttribute" />.
    /// </param>
    /// <typeparam name="T">
    ///   Тип, определяющий событие и связанные данные.
    ///    Тип должен быть анонимным или помеченным атрибутом <see cref="T:System.Diagnostics.Tracing.EventSourceAttribute" />.
    /// </typeparam>
    [SecuritySafeCritical]
    [__DynamicallyInvokable]
    public unsafe void Write<T>(string eventName, ref EventSourceOptions options, ref Guid activityId, ref Guid relatedActivityId, ref T data)
    {
      if (!this.IsEnabled())
        return;
      fixed (Guid* pActivityId = &activityId)
        fixed (Guid* guidPtr = &relatedActivityId)
          this.WriteImpl<T>(eventName, ref options, ref data, pActivityId, relatedActivityId == Guid.Empty ? (Guid*) null : guidPtr);
    }

    [SecuritySafeCritical]
    private unsafe void WriteMultiMerge(string eventName, ref EventSourceOptions options, TraceLoggingEventTypes eventTypes, Guid* activityID, Guid* childActivityID, params object[] values)
    {
      if (!this.IsEnabled() || !this.IsEnabled(((int) options.valuesSet & 4) != 0 ? (EventLevel) options.level : (EventLevel) eventTypes.level, ((int) options.valuesSet & 1) != 0 ? options.keywords : eventTypes.keywords))
        return;
      this.WriteMultiMergeInner(eventName, ref options, eventTypes, activityID, childActivityID, values);
    }

    [SecuritySafeCritical]
    private unsafe void WriteMultiMergeInner(string eventName, ref EventSourceOptions options, TraceLoggingEventTypes eventTypes, Guid* activityID, Guid* childActivityID, params object[] values)
    {
      byte level = ((int) options.valuesSet & 4) != 0 ? options.level : eventTypes.level;
      byte opcode = ((int) options.valuesSet & 8) != 0 ? options.opcode : eventTypes.opcode;
      EventTags tags = ((int) options.valuesSet & 2) != 0 ? options.tags : eventTypes.Tags;
      EventKeywords eventKeywords = ((int) options.valuesSet & 1) != 0 ? options.keywords : eventTypes.keywords;
      NameInfo nameInfo = eventTypes.GetNameInfo(eventName ?? eventTypes.Name, tags);
      if (nameInfo == null)
        return;
      EventDescriptor eventDescriptor = new EventDescriptor(nameInfo.identity, level, opcode, (long) eventKeywords);
      int pinCount = eventTypes.pinCount;
      byte* scratch = stackalloc byte[eventTypes.scratchSize];
      EventSource.EventData* eventDataPtr = stackalloc EventSource.EventData[eventTypes.dataCount + 3];
      GCHandle* gcHandlePtr = stackalloc GCHandle[pinCount];
      fixed (byte* pointer1 = this.providerMetadata)
        fixed (byte* pointer2 = nameInfo.nameMetadata)
          fixed (byte* pointer3 = eventTypes.typeMetadata)
          {
            eventDataPtr->SetMetadata(pointer1, this.providerMetadata.Length, 2);
            eventDataPtr[1].SetMetadata(pointer2, nameInfo.nameMetadata.Length, 1);
            eventDataPtr[2].SetMetadata(pointer3, eventTypes.typeMetadata.Length, 1);
            RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
              DataCollector.ThreadInstance.Enable(scratch, eventTypes.scratchSize, eventDataPtr + 3, eventTypes.dataCount, gcHandlePtr, pinCount);
              for (int index = 0; index < eventTypes.typeInfos.Length; ++index)
                eventTypes.typeInfos[index].WriteObjectData(TraceLoggingDataCollector.Instance, values[index]);
              this.WriteEventRaw(eventName, ref eventDescriptor, activityID, childActivityID, (int) (DataCollector.ThreadInstance.Finish() - eventDataPtr), (IntPtr) ((void*) eventDataPtr));
            }
            finally
            {
              this.WriteCleanup(gcHandlePtr, pinCount);
            }
          }
    }

    [SecuritySafeCritical]
    internal unsafe void WriteMultiMerge(string eventName, ref EventSourceOptions options, TraceLoggingEventTypes eventTypes, Guid* activityID, Guid* childActivityID, EventSource.EventData* data)
    {
      if (!this.IsEnabled())
        return;
      fixed (EventSourceOptions* eventSourceOptionsPtr = &options)
      {
        EventDescriptor descriptor;
        NameInfo nameInfo = this.UpdateDescriptor(eventName, eventTypes, ref options, out descriptor);
        if (nameInfo == null)
          return;
        EventSource.EventData* eventDataPtr = stackalloc EventSource.EventData[eventTypes.dataCount + eventTypes.typeInfos.Length * 2 + 3];
        fixed (byte* pointer1 = this.providerMetadata)
          fixed (byte* pointer2 = nameInfo.nameMetadata)
            fixed (byte* pointer3 = eventTypes.typeMetadata)
            {
              eventDataPtr->SetMetadata(pointer1, this.providerMetadata.Length, 2);
              eventDataPtr[1].SetMetadata(pointer2, nameInfo.nameMetadata.Length, 1);
              eventDataPtr[2].SetMetadata(pointer3, eventTypes.typeMetadata.Length, 1);
              int dataCount = 3;
              for (int index1 = 0; index1 < eventTypes.typeInfos.Length; ++index1)
              {
                if (eventTypes.typeInfos[index1].DataType == typeof (string))
                {
                  eventDataPtr[dataCount].DataPointer = (IntPtr) ((void*) &eventDataPtr[dataCount + 1].m_Size);
                  eventDataPtr[dataCount].m_Size = 2;
                  int index2 = dataCount + 1;
                  eventDataPtr[index2].m_Ptr = data[index1].m_Ptr;
                  eventDataPtr[index2].m_Size = data[index1].m_Size - 2;
                  dataCount = index2 + 1;
                }
                else
                {
                  eventDataPtr[dataCount].m_Ptr = data[index1].m_Ptr;
                  eventDataPtr[dataCount].m_Size = data[index1].m_Size;
                  if (data[index1].m_Size == 4 && eventTypes.typeInfos[index1].DataType == typeof (bool))
                    eventDataPtr[dataCount].m_Size = 1;
                  ++dataCount;
                }
              }
              this.WriteEventRaw(eventName, ref descriptor, activityID, childActivityID, dataCount, (IntPtr) ((void*) eventDataPtr));
            }
      }
    }

    [SecuritySafeCritical]
    private unsafe void WriteImpl<T>(string eventName, ref EventSourceOptions options, ref T data, Guid* pActivityId, Guid* pRelatedActivityId)
    {
      try
      {
        SimpleEventTypes<T> instance = SimpleEventTypes<T>.Instance;
        fixed (EventSourceOptions* eventSourceOptionsPtr = &options)
        {
          options.Opcode = options.IsOpcodeSet ? options.Opcode : EventSource.GetOpcodeWithDefault(options.Opcode, eventName);
          EventDescriptor descriptor;
          NameInfo nameInfo = this.UpdateDescriptor(eventName, (TraceLoggingEventTypes) instance, ref options, out descriptor);
          if (nameInfo == null)
            return;
          int pinCount = instance.pinCount;
          byte* scratch = stackalloc byte[instance.scratchSize];
          EventSource.EventData* eventDataPtr = stackalloc EventSource.EventData[instance.dataCount + 3];
          GCHandle* gcHandlePtr = stackalloc GCHandle[pinCount];
          fixed (byte* pointer1 = this.providerMetadata)
            fixed (byte* pointer2 = nameInfo.nameMetadata)
              fixed (byte* pointer3 = instance.typeMetadata)
              {
                eventDataPtr->SetMetadata(pointer1, this.providerMetadata.Length, 2);
                eventDataPtr[1].SetMetadata(pointer2, nameInfo.nameMetadata.Length, 1);
                eventDataPtr[2].SetMetadata(pointer3, instance.typeMetadata.Length, 1);
                RuntimeHelpers.PrepareConstrainedRegions();
                EventOpcode opcode = (EventOpcode) descriptor.Opcode;
                Guid empty1 = Guid.Empty;
                Guid empty2 = Guid.Empty;
                if ((IntPtr) pActivityId == IntPtr.Zero && (IntPtr) pRelatedActivityId == IntPtr.Zero && (options.ActivityOptions & EventActivityOptions.Disable) == EventActivityOptions.None)
                {
                  switch (opcode)
                  {
                    case EventOpcode.Start:
                      this.m_activityTracker.OnStart(this.m_name, eventName, 0, ref empty1, ref empty2, options.ActivityOptions);
                      break;
                    case EventOpcode.Stop:
                      this.m_activityTracker.OnStop(this.m_name, eventName, 0, ref empty1);
                      break;
                  }
                  if (empty1 != Guid.Empty)
                    pActivityId = &empty1;
                  if (empty2 != Guid.Empty)
                    pRelatedActivityId = &empty2;
                }
                try
                {
                  DataCollector.ThreadInstance.Enable(scratch, instance.scratchSize, eventDataPtr + 3, instance.dataCount, gcHandlePtr, pinCount);
                  instance.typeInfo.WriteData(TraceLoggingDataCollector.Instance, ref data);
                  this.WriteEventRaw(eventName, ref descriptor, pActivityId, pRelatedActivityId, (int) (DataCollector.ThreadInstance.Finish() - eventDataPtr), (IntPtr) ((void*) eventDataPtr));
                  if (this.m_Dispatchers == null)
                    return;
                  EventPayload data1 = (EventPayload) instance.typeInfo.GetData((object) data);
                  this.WriteToAllListeners(eventName, ref descriptor, nameInfo.tags, pActivityId, data1);
                }
                catch (Exception ex)
                {
                  if (ex is EventSourceException)
                    throw;
                  else
                    this.ThrowEventSourceException(eventName, ex);
                }
                finally
                {
                  this.WriteCleanup(gcHandlePtr, pinCount);
                }
              }
        }
      }
      catch (Exception ex)
      {
        if (ex is EventSourceException)
          throw;
        else
          this.ThrowEventSourceException(eventName, ex);
      }
    }

    [SecurityCritical]
    private unsafe void WriteToAllListeners(string eventName, ref EventDescriptor eventDescriptor, EventTags tags, Guid* pActivityId, EventPayload payload)
    {
      EventWrittenEventArgs eventCallbackArgs = new EventWrittenEventArgs(this);
      eventCallbackArgs.EventName = eventName;
      eventCallbackArgs.m_keywords = (EventKeywords) eventDescriptor.Keywords;
      eventCallbackArgs.m_opcode = (EventOpcode) eventDescriptor.Opcode;
      eventCallbackArgs.m_tags = tags;
      eventCallbackArgs.EventId = -1;
      if ((IntPtr) pActivityId != IntPtr.Zero)
        eventCallbackArgs.RelatedActivityId = *pActivityId;
      if (payload != null)
      {
        eventCallbackArgs.Payload = new ReadOnlyCollection<object>((IList<object>) payload.Values);
        eventCallbackArgs.PayloadNames = new ReadOnlyCollection<string>((IList<string>) payload.Keys);
      }
      this.DispatchToAllListeners(-1, pActivityId, eventCallbackArgs);
    }

    [ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
    [SecurityCritical]
    [NonEvent]
    private unsafe void WriteCleanup(GCHandle* pPins, int cPins)
    {
      DataCollector.ThreadInstance.Disable();
      for (int index = 0; index != cPins; ++index)
      {
        if (IntPtr.Zero != (IntPtr) pPins[index])
          pPins[index].Free();
      }
    }

    private void InitializeProviderMetadata()
    {
      if (this.m_traits != null)
      {
        List<byte> metaData = new List<byte>(100);
        int index = 0;
        while (index < this.m_traits.Length - 1)
        {
          if (this.m_traits[index].StartsWith("ETW_"))
          {
            string s = this.m_traits[index].Substring(4);
            byte result;
            if (!byte.TryParse(s, out result))
            {
              if (s == "GROUP")
                result = (byte) 1;
              else
                throw new ArgumentException(Environment.GetResourceString("UnknownEtwTrait", (object) s), "traits");
            }
            string trait = this.m_traits[index + 1];
            int count = metaData.Count;
            metaData.Add((byte) 0);
            metaData.Add((byte) 0);
            metaData.Add(result);
            int num = EventSource.AddValueToMetaData(metaData, trait) + 3;
            metaData[count] = (byte) num;
            metaData[count + 1] = (byte) (num >> 8);
          }
          index += 2;
        }
        this.providerMetadata = Statics.MetadataForString(this.Name, 0, metaData.Count, 0);
        int num1 = this.providerMetadata.Length - metaData.Count;
        foreach (byte num2 in metaData)
          this.providerMetadata[num1++] = num2;
      }
      else
        this.providerMetadata = Statics.MetadataForString(this.Name, 0, 0, 0);
    }

    private static int AddValueToMetaData(List<byte> metaData, string value)
    {
      if (value.Length == 0)
        return 0;
      int count = metaData.Count;
      char ch = value[0];
      switch (ch)
      {
        case '#':
          for (int index = 1; index < value.Length; ++index)
          {
            if (value[index] != ' ')
            {
              if (index + 1 >= value.Length)
                throw new ArgumentException(Environment.GetResourceString("EvenHexDigits"), "traits");
              metaData.Add((byte) (EventSource.HexDigit(value[index]) * 16 + EventSource.HexDigit(value[index + 1])));
              ++index;
            }
          }
          break;
        case '@':
          metaData.AddRange((IEnumerable<byte>) Encoding.UTF8.GetBytes(value.Substring(1)));
          break;
        case '{':
          metaData.AddRange((IEnumerable<byte>) new Guid(value).ToByteArray());
          break;
        default:
          if (' ' <= ch)
          {
            metaData.AddRange((IEnumerable<byte>) Encoding.UTF8.GetBytes(value));
            break;
          }
          throw new ArgumentException(Environment.GetResourceString("IllegalValue", (object) value), "traits");
      }
      return metaData.Count - count;
    }

    private static int HexDigit(char c)
    {
      if ('0' <= c && c <= '9')
        return (int) c - 48;
      if ('a' <= c)
        c -= ' ';
      if ('A' <= c && c <= 'F')
        return (int) c - 65 + 10;
      throw new ArgumentException(Environment.GetResourceString("BadHexDigit", (object) c), "traits");
    }

    private NameInfo UpdateDescriptor(string name, TraceLoggingEventTypes eventInfo, ref EventSourceOptions options, out EventDescriptor descriptor)
    {
      NameInfo nameInfo = (NameInfo) null;
      int traceloggingId = 0;
      byte level = ((int) options.valuesSet & 4) != 0 ? options.level : eventInfo.level;
      byte opcode = ((int) options.valuesSet & 8) != 0 ? options.opcode : eventInfo.opcode;
      EventTags tags = ((int) options.valuesSet & 2) != 0 ? options.tags : eventInfo.Tags;
      EventKeywords keywords = ((int) options.valuesSet & 1) != 0 ? options.keywords : eventInfo.keywords;
      if (this.IsEnabled((EventLevel) level, keywords))
      {
        nameInfo = eventInfo.GetNameInfo(name ?? eventInfo.Name, tags);
        traceloggingId = nameInfo.identity;
      }
      descriptor = new EventDescriptor(traceloggingId, level, opcode, (long) keywords);
      return nameInfo;
    }

    /// <summary>
    ///   Предоставляет данные события для быстрого создания <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> перегрузки с использованием <see cref="M:System.Diagnostics.Tracing.EventSource.WriteEventCore(System.Int32,System.Int32,System.Diagnostics.Tracing.EventSource.EventData*)" /> метод.
    /// </summary>
    [__DynamicallyInvokable]
    protected internal struct EventData
    {
      internal ulong m_Ptr;
      internal int m_Size;
      internal int m_Reserved;

      /// <summary>
      ///   Возвращает или задает указатель на данные для нового <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> перегрузки.
      /// </summary>
      /// <returns>Указатель на данные.</returns>
      [__DynamicallyInvokable]
      public unsafe IntPtr DataPointer
      {
        [SecuritySafeCritical] get
        {
          return (IntPtr) ((void*) this.m_Ptr);
        }
        set
        {
          this.m_Ptr = (ulong) (void*) value;
        }
      }

      /// <summary>
      ///   Возвращает или задает число элементов в полезных данных в новом <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> перегрузки.
      /// </summary>
      /// <returns>
      ///   Количество элементов полезных данных новая перегрузка метода.
      /// </returns>
      [__DynamicallyInvokable]
      public int Size
      {
        [__DynamicallyInvokable] get
        {
          return this.m_Size;
        }
        [__DynamicallyInvokable] set
        {
          this.m_Size = value;
        }
      }

      [SecurityCritical]
      internal unsafe void SetMetadata(byte* pointer, int size, int reserved)
      {
        this.m_Ptr = (ulong) pointer;
        this.m_Size = size;
        this.m_Reserved = reserved;
      }
    }

    private struct Sha1ForNonSecretPurposes
    {
      private long length;
      private uint[] w;
      private int pos;

      public void Start()
      {
        if (this.w == null)
          this.w = new uint[85];
        this.length = 0L;
        this.pos = 0;
        this.w[80] = 1732584193U;
        this.w[81] = 4023233417U;
        this.w[82] = 2562383102U;
        this.w[83] = 271733878U;
        this.w[84] = 3285377520U;
      }

      public void Append(byte input)
      {
        this.w[this.pos / 4] = this.w[this.pos / 4] << 8 | (uint) input;
        if (64 != ++this.pos)
          return;
        this.Drain();
      }

      public void Append(byte[] input)
      {
        foreach (byte input1 in input)
          this.Append(input1);
      }

      public void Finish(byte[] output)
      {
        long num1 = this.length + (long) (8 * this.pos);
        this.Append((byte) 128);
        while (this.pos != 56)
          this.Append((byte) 0);
        this.Append((byte) (num1 >> 56));
        this.Append((byte) (num1 >> 48));
        this.Append((byte) (num1 >> 40));
        this.Append((byte) (num1 >> 32));
        this.Append((byte) (num1 >> 24));
        this.Append((byte) (num1 >> 16));
        this.Append((byte) (num1 >> 8));
        this.Append((byte) num1);
        int num2 = output.Length < 20 ? output.Length : 20;
        for (int index = 0; index != num2; ++index)
        {
          uint num3 = this.w[80 + index / 4];
          output[index] = (byte) (num3 >> 24);
          this.w[80 + index / 4] = num3 << 8;
        }
      }

      private void Drain()
      {
        for (int index = 16; index != 80; ++index)
          this.w[index] = EventSource.Sha1ForNonSecretPurposes.Rol1(this.w[index - 3] ^ this.w[index - 8] ^ this.w[index - 14] ^ this.w[index - 16]);
        uint input1 = this.w[80];
        uint input2 = this.w[81];
        uint num1 = this.w[82];
        uint num2 = this.w[83];
        uint num3 = this.w[84];
        for (int index = 0; index != 20; ++index)
        {
          uint num4 = (uint) ((int) input2 & (int) num1 | ~(int) input2 & (int) num2);
          uint num5 = (uint) ((int) EventSource.Sha1ForNonSecretPurposes.Rol5(input1) + (int) num4 + (int) num3 + 1518500249) + this.w[index];
          num3 = num2;
          num2 = num1;
          num1 = EventSource.Sha1ForNonSecretPurposes.Rol30(input2);
          input2 = input1;
          input1 = num5;
        }
        for (int index = 20; index != 40; ++index)
        {
          uint num4 = input2 ^ num1 ^ num2;
          uint num5 = (uint) ((int) EventSource.Sha1ForNonSecretPurposes.Rol5(input1) + (int) num4 + (int) num3 + 1859775393) + this.w[index];
          num3 = num2;
          num2 = num1;
          num1 = EventSource.Sha1ForNonSecretPurposes.Rol30(input2);
          input2 = input1;
          input1 = num5;
        }
        for (int index = 40; index != 60; ++index)
        {
          uint num4 = (uint) ((int) input2 & (int) num1 | (int) input2 & (int) num2 | (int) num1 & (int) num2);
          uint num5 = (uint) ((int) EventSource.Sha1ForNonSecretPurposes.Rol5(input1) + (int) num4 + (int) num3 - 1894007588) + this.w[index];
          num3 = num2;
          num2 = num1;
          num1 = EventSource.Sha1ForNonSecretPurposes.Rol30(input2);
          input2 = input1;
          input1 = num5;
        }
        for (int index = 60; index != 80; ++index)
        {
          uint num4 = input2 ^ num1 ^ num2;
          uint num5 = (uint) ((int) EventSource.Sha1ForNonSecretPurposes.Rol5(input1) + (int) num4 + (int) num3 - 899497514) + this.w[index];
          num3 = num2;
          num2 = num1;
          num1 = EventSource.Sha1ForNonSecretPurposes.Rol30(input2);
          input2 = input1;
          input1 = num5;
        }
        this.w[80] += input1;
        this.w[81] += input2;
        this.w[82] += num1;
        this.w[83] += num2;
        this.w[84] += num3;
        this.length += 512L;
        this.pos = 0;
      }

      private static uint Rol1(uint input)
      {
        return input << 1 | input >> 31;
      }

      private static uint Rol5(uint input)
      {
        return input << 5 | input >> 27;
      }

      private static uint Rol30(uint input)
      {
        return input << 30 | input >> 2;
      }
    }

    private class OverideEventProvider : EventProvider
    {
      private EventSource m_eventSource;

      public OverideEventProvider(EventSource eventSource)
      {
        this.m_eventSource = eventSource;
      }

      protected override void OnControllerCommand(ControllerCommand command, IDictionary<string, string> arguments, int perEventSourceSessionId, int etwSessionId)
      {
        this.m_eventSource.SendCommand((EventListener) null, perEventSourceSessionId, etwSessionId, (EventCommand) command, this.IsEnabled(), this.Level, this.MatchAnyKeyword, arguments);
      }
    }

    internal struct EventMetadata
    {
      public EventDescriptor Descriptor;
      public EventTags Tags;
      public bool EnabledForAnyListener;
      public bool EnabledForETW;
      public bool HasRelatedActivityID;
      public byte TriggersActivityTracking;
      public string Name;
      public string Message;
      public ParameterInfo[] Parameters;
      public TraceLoggingEventTypes TraceLoggingEventTypes;
      public EventActivityOptions ActivityOptions;
    }
  }
}

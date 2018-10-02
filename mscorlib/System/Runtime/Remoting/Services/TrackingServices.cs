// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Services.TrackingServices
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Services
{
  /// <summary>
  ///   Предоставляет способ регистрации, отмены регистрации и получения списка дескрипторов отслеживания.
  /// </summary>
  [SecurityCritical]
  [ComVisible(true)]
  public class TrackingServices
  {
    private static volatile ITrackingHandler[] _Handlers = new ITrackingHandler[0];
    private static volatile int _Size = 0;
    private static object s_TrackingServicesSyncObject = (object) null;

    private static object TrackingServicesSyncObject
    {
      get
      {
        if (TrackingServices.s_TrackingServicesSyncObject == null)
        {
          object obj = new object();
          Interlocked.CompareExchange(ref TrackingServices.s_TrackingServicesSyncObject, obj, (object) null);
        }
        return TrackingServices.s_TrackingServicesSyncObject;
      }
    }

    /// <summary>
    ///   Регистрирует новый дескриптор отслеживания с <see cref="T:System.Runtime.Remoting.Services.TrackingServices" />.
    /// </summary>
    /// <param name="handler">
    ///   Дескриптор отслеживания для регистрации.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="handler" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   Обработчик, который указывается в <paramref name="handler" /> уже зарегистрирован параметр <see cref="T:System.Runtime.Remoting.Services.TrackingServices" />.
    /// </exception>
    [SecurityCritical]
    public static void RegisterTrackingHandler(ITrackingHandler handler)
    {
      if (handler == null)
        throw new ArgumentNullException(nameof (handler));
      lock (TrackingServices.TrackingServicesSyncObject)
      {
        if (-1 == TrackingServices.Match(handler))
        {
          if (TrackingServices._Handlers == null || TrackingServices._Size == TrackingServices._Handlers.Length)
          {
            ITrackingHandler[] trackingHandlerArray = new ITrackingHandler[TrackingServices._Size * 2 + 4];
            if (TrackingServices._Handlers != null)
              Array.Copy((Array) TrackingServices._Handlers, (Array) trackingHandlerArray, TrackingServices._Size);
            TrackingServices._Handlers = trackingHandlerArray;
          }
          Volatile.Write<ITrackingHandler>(ref TrackingServices._Handlers[TrackingServices._Size++], handler);
        }
        else
          throw new RemotingException(Environment.GetResourceString("Remoting_TrackingHandlerAlreadyRegistered", (object) nameof (handler)));
      }
    }

    /// <summary>
    ///   Отменяет регистрацию заданный дескриптор отслеживания из <see cref="T:System.Runtime.Remoting.Services.TrackingServices" />.
    /// </summary>
    /// <param name="handler">Обработчик для отмены регистрации.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="handler" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Runtime.Remoting.RemotingException">
    ///   Обработчик, который указывается в <paramref name="handler" /> не зарегистрирован параметр <see cref="T:System.Runtime.Remoting.Services.TrackingServices" />.
    /// </exception>
    [SecurityCritical]
    public static void UnregisterTrackingHandler(ITrackingHandler handler)
    {
      if (handler == null)
        throw new ArgumentNullException(nameof (handler));
      lock (TrackingServices.TrackingServicesSyncObject)
      {
        int destinationIndex = TrackingServices.Match(handler);
        if (-1 == destinationIndex)
          throw new RemotingException(Environment.GetResourceString("Remoting_HandlerNotRegistered", (object) handler));
        Array.Copy((Array) TrackingServices._Handlers, destinationIndex + 1, (Array) TrackingServices._Handlers, destinationIndex, TrackingServices._Size - destinationIndex - 1);
        --TrackingServices._Size;
      }
    }

    /// <summary>
    ///   Получает массив дескрипторов отслеживания, которые в настоящее время зарегистрированы с <see cref="T:System.Runtime.Remoting.Services.TrackingServices" /> в текущем <see cref="T:System.AppDomain" />.
    /// </summary>
    /// <returns>
    ///   Массив дескрипторов отслеживания, которые в настоящее время зарегистрированы с <see cref="T:System.Runtime.Remoting.Services.TrackingServices" /> в текущем <see cref="T:System.AppDomain" />.
    /// </returns>
    public static ITrackingHandler[] RegisteredHandlers
    {
      [SecurityCritical] get
      {
        lock (TrackingServices.TrackingServicesSyncObject)
        {
          if (TrackingServices._Size == 0)
            return new ITrackingHandler[0];
          ITrackingHandler[] trackingHandlerArray = new ITrackingHandler[TrackingServices._Size];
          for (int index = 0; index < TrackingServices._Size; ++index)
            trackingHandlerArray[index] = TrackingServices._Handlers[index];
          return trackingHandlerArray;
        }
      }
    }

    [SecurityCritical]
    internal static void MarshaledObject(object obj, ObjRef or)
    {
      try
      {
        ITrackingHandler[] handlers = TrackingServices._Handlers;
        for (int index = 0; index < TrackingServices._Size; ++index)
          Volatile.Read<ITrackingHandler>(ref handlers[index]).MarshaledObject(obj, or);
      }
      catch
      {
      }
    }

    [SecurityCritical]
    internal static void UnmarshaledObject(object obj, ObjRef or)
    {
      try
      {
        ITrackingHandler[] handlers = TrackingServices._Handlers;
        for (int index = 0; index < TrackingServices._Size; ++index)
          Volatile.Read<ITrackingHandler>(ref handlers[index]).UnmarshaledObject(obj, or);
      }
      catch
      {
      }
    }

    [SecurityCritical]
    internal static void DisconnectedObject(object obj)
    {
      try
      {
        ITrackingHandler[] handlers = TrackingServices._Handlers;
        for (int index = 0; index < TrackingServices._Size; ++index)
          Volatile.Read<ITrackingHandler>(ref handlers[index]).DisconnectedObject(obj);
      }
      catch
      {
      }
    }

    private static int Match(ITrackingHandler handler)
    {
      int num = -1;
      for (int index = 0; index < TrackingServices._Size; ++index)
      {
        if (TrackingServices._Handlers[index] == handler)
        {
          num = index;
          break;
        }
      }
      return num;
    }
  }
}

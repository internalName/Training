// Decompiled with JetBrains decompiler
// Type: System.Runtime.InteropServices.ComEventsHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.Remoting;
using System.Security;

namespace System.Runtime.InteropServices
{
  /// <summary>
  ///   Предоставляет методы, активирующие делегаты .NET Framework для обработки событий, которые нужно добавить или удалить из объектов COM.
  /// </summary>
  [__DynamicallyInvokable]
  public static class ComEventsHelper
  {
    /// <summary>
    ///   Добавляет делегат в список вызова событий, поступающих из COM-объекта.
    /// </summary>
    /// <param name="rcw">
    ///   COM-объект, инициирующий события вызывающему объекту требуется реагировать.
    /// </param>
    /// <param name="iid">
    ///   Идентификатор исходного интерфейса COM-объект инициирует события.
    /// </param>
    /// <param name="dispid">
    ///   Идентификатор метода на интерфейсе отправителя.
    /// </param>
    /// <param name="d">
    ///   Делегат, вызываемый при возникновении события COM.
    /// </param>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static void Combine(object rcw, Guid iid, int dispid, Delegate d)
    {
      rcw = ComEventsHelper.UnwrapIfTransparentProxy(rcw);
      lock (rcw)
      {
        ComEventsInfo comEventsInfo = ComEventsInfo.FromObject(rcw);
        ComEventsSink comEventsSink = comEventsInfo.FindSink(ref iid) ?? comEventsInfo.AddSink(ref iid);
        (comEventsSink.FindMethod(dispid) ?? comEventsSink.AddMethod(dispid)).AddDelegate(d);
      }
    }

    /// <summary>
    ///   Удаляет делегат из списка вызова событий, поступающих из COM-объекта.
    /// </summary>
    /// <param name="rcw">Присоединяется к COM-объект делегата.</param>
    /// <param name="iid">
    ///   Идентификатор исходного интерфейса COM-объект инициирует события.
    /// </param>
    /// <param name="dispid">
    ///   Идентификатор метода на интерфейсе отправителя.
    /// </param>
    /// <param name="d">Делегат, удаляемый из списка вызова.</param>
    /// <returns>Делегат, который был удален из списка вызова.</returns>
    [SecurityCritical]
    [__DynamicallyInvokable]
    public static Delegate Remove(object rcw, Guid iid, int dispid, Delegate d)
    {
      rcw = ComEventsHelper.UnwrapIfTransparentProxy(rcw);
      lock (rcw)
      {
        ComEventsInfo comEventsInfo = ComEventsInfo.Find(rcw);
        if (comEventsInfo == null)
          return (Delegate) null;
        ComEventsSink sink = comEventsInfo.FindSink(ref iid);
        if (sink == null)
          return (Delegate) null;
        ComEventsMethod method = sink.FindMethod(dispid);
        if (method == null)
          return (Delegate) null;
        method.RemoveDelegate(d);
        if (method.Empty)
          method = sink.RemoveMethod(method);
        if (method == null)
          sink = comEventsInfo.RemoveSink(sink);
        if (sink == null)
        {
          Marshal.SetComObjectData(rcw, (object) typeof (ComEventsInfo), (object) null);
          GC.SuppressFinalize((object) comEventsInfo);
        }
        return d;
      }
    }

    [SecurityCritical]
    internal static object UnwrapIfTransparentProxy(object rcw)
    {
      if (RemotingServices.IsTransparentProxy(rcw))
      {
        IntPtr iunknownForObject = Marshal.GetIUnknownForObject(rcw);
        try
        {
          rcw = Marshal.GetObjectForIUnknown(iunknownForObject);
        }
        finally
        {
          Marshal.Release(iunknownForObject);
        }
      }
      return rcw;
    }
  }
}

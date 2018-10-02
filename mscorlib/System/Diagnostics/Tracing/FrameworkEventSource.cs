// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.FrameworkEventSource
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics.Tracing
{
  [FriendAccessAllowed]
  [EventSource(Guid = "8E9F5090-2D75-4d03-8A81-E5AFBF85DAF1", Name = "System.Diagnostics.Eventing.FrameworkEventSource")]
  internal sealed class FrameworkEventSource : EventSource
  {
    public static readonly FrameworkEventSource Log = new FrameworkEventSource();

    public static bool IsInitialized
    {
      get
      {
        return FrameworkEventSource.Log != null;
      }
    }

    private FrameworkEventSource()
      : base(new Guid(2392805520U, (ushort) 11637, (ushort) 19715, (byte) 138, (byte) 129, (byte) 229, (byte) 175, (byte) 191, (byte) 133, (byte) 218, (byte) 241), "System.Diagnostics.Eventing.FrameworkEventSource")
    {
    }

    [NonEvent]
    [SecuritySafeCritical]
    private unsafe void WriteEvent(int eventId, long arg1, int arg2, string arg3, bool arg4)
    {
      if (!this.IsEnabled())
        return;
      if (arg3 == null)
        arg3 = "";
      string str = arg3;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      EventSource.EventData* data = stackalloc EventSource.EventData[4];
      data->DataPointer = (IntPtr) ((void*) &arg1);
      data->Size = 8;
      data[1].DataPointer = (IntPtr) ((void*) &arg2);
      data[1].Size = 4;
      data[2].DataPointer = (IntPtr) ((void*) chPtr);
      data[2].Size = (arg3.Length + 1) * 2;
      data[3].DataPointer = (IntPtr) ((void*) &arg4);
      data[3].Size = 4;
      this.WriteEventCore(eventId, 4, data);
      str = (string) null;
    }

    [NonEvent]
    [SecuritySafeCritical]
    private unsafe void WriteEvent(int eventId, long arg1, int arg2, string arg3)
    {
      if (!this.IsEnabled())
        return;
      if (arg3 == null)
        arg3 = "";
      string str = arg3;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      EventSource.EventData* data = stackalloc EventSource.EventData[3];
      data->DataPointer = (IntPtr) ((void*) &arg1);
      data->Size = 8;
      data[1].DataPointer = (IntPtr) ((void*) &arg2);
      data[1].Size = 4;
      data[2].DataPointer = (IntPtr) ((void*) chPtr);
      data[2].Size = (arg3.Length + 1) * 2;
      this.WriteEventCore(eventId, 3, data);
      str = (string) null;
    }

    [NonEvent]
    [SecuritySafeCritical]
    private unsafe void WriteEvent(int eventId, long arg1, string arg2, bool arg3, bool arg4)
    {
      if (!this.IsEnabled())
        return;
      if (arg2 == null)
        arg2 = "";
      string str = arg2;
      char* chPtr = (char*) str;
      if ((IntPtr) chPtr != IntPtr.Zero)
        chPtr += RuntimeHelpers.OffsetToStringData;
      EventSource.EventData* data = stackalloc EventSource.EventData[4];
      data->DataPointer = (IntPtr) ((void*) &arg1);
      data->Size = 8;
      data[1].DataPointer = (IntPtr) ((void*) chPtr);
      data[1].Size = (arg2.Length + 1) * 2;
      data[2].DataPointer = (IntPtr) ((void*) &arg3);
      data[2].Size = 4;
      data[3].DataPointer = (IntPtr) ((void*) &arg4);
      data[3].Size = 4;
      this.WriteEventCore(eventId, 4, data);
      str = (string) null;
    }

    [NonEvent]
    [SecuritySafeCritical]
    private unsafe void WriteEvent(int eventId, long arg1, bool arg2, bool arg3)
    {
      if (!this.IsEnabled())
        return;
      EventSource.EventData* data = stackalloc EventSource.EventData[3];
      data->DataPointer = (IntPtr) ((void*) &arg1);
      data->Size = 8;
      data[1].DataPointer = (IntPtr) ((void*) &arg2);
      data[1].Size = 4;
      data[2].DataPointer = (IntPtr) ((void*) &arg3);
      data[2].Size = 4;
      this.WriteEventCore(eventId, 3, data);
    }

    [NonEvent]
    [SecuritySafeCritical]
    private unsafe void WriteEvent(int eventId, long arg1, bool arg2, bool arg3, int arg4)
    {
      if (!this.IsEnabled())
        return;
      EventSource.EventData* data = stackalloc EventSource.EventData[4];
      data->DataPointer = (IntPtr) ((void*) &arg1);
      data->Size = 8;
      data[1].DataPointer = (IntPtr) ((void*) &arg2);
      data[1].Size = 4;
      data[2].DataPointer = (IntPtr) ((void*) &arg3);
      data[2].Size = 4;
      data[3].DataPointer = (IntPtr) ((void*) &arg4);
      data[3].Size = 4;
      this.WriteEventCore(eventId, 4, data);
    }

    [Event(1, Keywords = (EventKeywords) 1, Level = EventLevel.Informational)]
    public void ResourceManagerLookupStarted(string baseName, string mainAssemblyName, string cultureName)
    {
      this.WriteEvent(1, baseName, mainAssemblyName, cultureName);
    }

    [Event(2, Keywords = (EventKeywords) 1, Level = EventLevel.Informational)]
    public void ResourceManagerLookingForResourceSet(string baseName, string mainAssemblyName, string cultureName)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(2, baseName, mainAssemblyName, cultureName);
    }

    [Event(3, Keywords = (EventKeywords) 1, Level = EventLevel.Informational)]
    public void ResourceManagerFoundResourceSetInCache(string baseName, string mainAssemblyName, string cultureName)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(3, baseName, mainAssemblyName, cultureName);
    }

    [Event(4, Keywords = (EventKeywords) 1, Level = EventLevel.Warning)]
    public void ResourceManagerFoundResourceSetInCacheUnexpected(string baseName, string mainAssemblyName, string cultureName)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(4, baseName, mainAssemblyName, cultureName);
    }

    [Event(5, Keywords = (EventKeywords) 1, Level = EventLevel.Informational)]
    public void ResourceManagerStreamFound(string baseName, string mainAssemblyName, string cultureName, string loadedAssemblyName, string resourceFileName)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(5, (object) baseName, (object) mainAssemblyName, (object) cultureName, (object) loadedAssemblyName, (object) resourceFileName);
    }

    [Event(6, Keywords = (EventKeywords) 1, Level = EventLevel.Warning)]
    public void ResourceManagerStreamNotFound(string baseName, string mainAssemblyName, string cultureName, string loadedAssemblyName, string resourceFileName)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(6, (object) baseName, (object) mainAssemblyName, (object) cultureName, (object) loadedAssemblyName, (object) resourceFileName);
    }

    [Event(7, Keywords = (EventKeywords) 1, Level = EventLevel.Informational)]
    public void ResourceManagerGetSatelliteAssemblySucceeded(string baseName, string mainAssemblyName, string cultureName, string assemblyName)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(7, (object) baseName, (object) mainAssemblyName, (object) cultureName, (object) assemblyName);
    }

    [Event(8, Keywords = (EventKeywords) 1, Level = EventLevel.Warning)]
    public void ResourceManagerGetSatelliteAssemblyFailed(string baseName, string mainAssemblyName, string cultureName, string assemblyName)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(8, (object) baseName, (object) mainAssemblyName, (object) cultureName, (object) assemblyName);
    }

    [Event(9, Keywords = (EventKeywords) 1, Level = EventLevel.Informational)]
    public void ResourceManagerCaseInsensitiveResourceStreamLookupSucceeded(string baseName, string mainAssemblyName, string assemblyName, string resourceFileName)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(9, (object) baseName, (object) mainAssemblyName, (object) assemblyName, (object) resourceFileName);
    }

    [Event(10, Keywords = (EventKeywords) 1, Level = EventLevel.Warning)]
    public void ResourceManagerCaseInsensitiveResourceStreamLookupFailed(string baseName, string mainAssemblyName, string assemblyName, string resourceFileName)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(10, (object) baseName, (object) mainAssemblyName, (object) assemblyName, (object) resourceFileName);
    }

    [Event(11, Keywords = (EventKeywords) 1, Level = EventLevel.Error)]
    public void ResourceManagerManifestResourceAccessDenied(string baseName, string mainAssemblyName, string assemblyName, string canonicalName)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(11, (object) baseName, (object) mainAssemblyName, (object) assemblyName, (object) canonicalName);
    }

    [Event(12, Keywords = (EventKeywords) 1, Level = EventLevel.Informational)]
    public void ResourceManagerNeutralResourcesSufficient(string baseName, string mainAssemblyName, string cultureName)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(12, baseName, mainAssemblyName, cultureName);
    }

    [Event(13, Keywords = (EventKeywords) 1, Level = EventLevel.Warning)]
    public void ResourceManagerNeutralResourceAttributeMissing(string mainAssemblyName)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(13, mainAssemblyName);
    }

    [Event(14, Keywords = (EventKeywords) 1, Level = EventLevel.Informational)]
    public void ResourceManagerCreatingResourceSet(string baseName, string mainAssemblyName, string cultureName, string fileName)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(14, (object) baseName, (object) mainAssemblyName, (object) cultureName, (object) fileName);
    }

    [Event(15, Keywords = (EventKeywords) 1, Level = EventLevel.Informational)]
    public void ResourceManagerNotCreatingResourceSet(string baseName, string mainAssemblyName, string cultureName)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(15, baseName, mainAssemblyName, cultureName);
    }

    [Event(16, Keywords = (EventKeywords) 1, Level = EventLevel.Warning)]
    public void ResourceManagerLookupFailed(string baseName, string mainAssemblyName, string cultureName)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(16, baseName, mainAssemblyName, cultureName);
    }

    [Event(17, Keywords = (EventKeywords) 1, Level = EventLevel.Informational)]
    public void ResourceManagerReleasingResources(string baseName, string mainAssemblyName)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(17, baseName, mainAssemblyName);
    }

    [Event(18, Keywords = (EventKeywords) 1, Level = EventLevel.Warning)]
    public void ResourceManagerNeutralResourcesNotFound(string baseName, string mainAssemblyName, string resName)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(18, baseName, mainAssemblyName, resName);
    }

    [Event(19, Keywords = (EventKeywords) 1, Level = EventLevel.Informational)]
    public void ResourceManagerNeutralResourcesFound(string baseName, string mainAssemblyName, string resName)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(19, baseName, mainAssemblyName, resName);
    }

    [Event(20, Keywords = (EventKeywords) 1, Level = EventLevel.Informational)]
    public void ResourceManagerAddingCultureFromConfigFile(string baseName, string mainAssemblyName, string cultureName)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(20, baseName, mainAssemblyName, cultureName);
    }

    [Event(21, Keywords = (EventKeywords) 1, Level = EventLevel.Informational)]
    public void ResourceManagerCultureNotFoundInConfigFile(string baseName, string mainAssemblyName, string cultureName)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(21, baseName, mainAssemblyName, cultureName);
    }

    [Event(22, Keywords = (EventKeywords) 1, Level = EventLevel.Informational)]
    public void ResourceManagerCultureFoundInConfigFile(string baseName, string mainAssemblyName, string cultureName)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(22, baseName, mainAssemblyName, cultureName);
    }

    [NonEvent]
    public void ResourceManagerLookupStarted(string baseName, Assembly mainAssembly, string cultureName)
    {
      if (!this.IsEnabled())
        return;
      this.ResourceManagerLookupStarted(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
    }

    [NonEvent]
    public void ResourceManagerLookingForResourceSet(string baseName, Assembly mainAssembly, string cultureName)
    {
      if (!this.IsEnabled())
        return;
      this.ResourceManagerLookingForResourceSet(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
    }

    [NonEvent]
    public void ResourceManagerFoundResourceSetInCache(string baseName, Assembly mainAssembly, string cultureName)
    {
      if (!this.IsEnabled())
        return;
      this.ResourceManagerFoundResourceSetInCache(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
    }

    [NonEvent]
    public void ResourceManagerFoundResourceSetInCacheUnexpected(string baseName, Assembly mainAssembly, string cultureName)
    {
      if (!this.IsEnabled())
        return;
      this.ResourceManagerFoundResourceSetInCacheUnexpected(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
    }

    [NonEvent]
    public void ResourceManagerStreamFound(string baseName, Assembly mainAssembly, string cultureName, Assembly loadedAssembly, string resourceFileName)
    {
      if (!this.IsEnabled())
        return;
      this.ResourceManagerStreamFound(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName, FrameworkEventSource.GetName(loadedAssembly), resourceFileName);
    }

    [NonEvent]
    public void ResourceManagerStreamNotFound(string baseName, Assembly mainAssembly, string cultureName, Assembly loadedAssembly, string resourceFileName)
    {
      if (!this.IsEnabled())
        return;
      this.ResourceManagerStreamNotFound(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName, FrameworkEventSource.GetName(loadedAssembly), resourceFileName);
    }

    [NonEvent]
    public void ResourceManagerGetSatelliteAssemblySucceeded(string baseName, Assembly mainAssembly, string cultureName, string assemblyName)
    {
      if (!this.IsEnabled())
        return;
      this.ResourceManagerGetSatelliteAssemblySucceeded(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName, assemblyName);
    }

    [NonEvent]
    public void ResourceManagerGetSatelliteAssemblyFailed(string baseName, Assembly mainAssembly, string cultureName, string assemblyName)
    {
      if (!this.IsEnabled())
        return;
      this.ResourceManagerGetSatelliteAssemblyFailed(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName, assemblyName);
    }

    [NonEvent]
    public void ResourceManagerCaseInsensitiveResourceStreamLookupSucceeded(string baseName, Assembly mainAssembly, string assemblyName, string resourceFileName)
    {
      if (!this.IsEnabled())
        return;
      this.ResourceManagerCaseInsensitiveResourceStreamLookupSucceeded(baseName, FrameworkEventSource.GetName(mainAssembly), assemblyName, resourceFileName);
    }

    [NonEvent]
    public void ResourceManagerCaseInsensitiveResourceStreamLookupFailed(string baseName, Assembly mainAssembly, string assemblyName, string resourceFileName)
    {
      if (!this.IsEnabled())
        return;
      this.ResourceManagerCaseInsensitiveResourceStreamLookupFailed(baseName, FrameworkEventSource.GetName(mainAssembly), assemblyName, resourceFileName);
    }

    [NonEvent]
    public void ResourceManagerManifestResourceAccessDenied(string baseName, Assembly mainAssembly, string assemblyName, string canonicalName)
    {
      if (!this.IsEnabled())
        return;
      this.ResourceManagerManifestResourceAccessDenied(baseName, FrameworkEventSource.GetName(mainAssembly), assemblyName, canonicalName);
    }

    [NonEvent]
    public void ResourceManagerNeutralResourcesSufficient(string baseName, Assembly mainAssembly, string cultureName)
    {
      if (!this.IsEnabled())
        return;
      this.ResourceManagerNeutralResourcesSufficient(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
    }

    [NonEvent]
    public void ResourceManagerNeutralResourceAttributeMissing(Assembly mainAssembly)
    {
      if (!this.IsEnabled())
        return;
      this.ResourceManagerNeutralResourceAttributeMissing(FrameworkEventSource.GetName(mainAssembly));
    }

    [NonEvent]
    public void ResourceManagerCreatingResourceSet(string baseName, Assembly mainAssembly, string cultureName, string fileName)
    {
      if (!this.IsEnabled())
        return;
      this.ResourceManagerCreatingResourceSet(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName, fileName);
    }

    [NonEvent]
    public void ResourceManagerNotCreatingResourceSet(string baseName, Assembly mainAssembly, string cultureName)
    {
      if (!this.IsEnabled())
        return;
      this.ResourceManagerNotCreatingResourceSet(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
    }

    [NonEvent]
    public void ResourceManagerLookupFailed(string baseName, Assembly mainAssembly, string cultureName)
    {
      if (!this.IsEnabled())
        return;
      this.ResourceManagerLookupFailed(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
    }

    [NonEvent]
    public void ResourceManagerReleasingResources(string baseName, Assembly mainAssembly)
    {
      if (!this.IsEnabled())
        return;
      this.ResourceManagerReleasingResources(baseName, FrameworkEventSource.GetName(mainAssembly));
    }

    [NonEvent]
    public void ResourceManagerNeutralResourcesNotFound(string baseName, Assembly mainAssembly, string resName)
    {
      if (!this.IsEnabled())
        return;
      this.ResourceManagerNeutralResourcesNotFound(baseName, FrameworkEventSource.GetName(mainAssembly), resName);
    }

    [NonEvent]
    public void ResourceManagerNeutralResourcesFound(string baseName, Assembly mainAssembly, string resName)
    {
      if (!this.IsEnabled())
        return;
      this.ResourceManagerNeutralResourcesFound(baseName, FrameworkEventSource.GetName(mainAssembly), resName);
    }

    [NonEvent]
    public void ResourceManagerAddingCultureFromConfigFile(string baseName, Assembly mainAssembly, string cultureName)
    {
      if (!this.IsEnabled())
        return;
      this.ResourceManagerAddingCultureFromConfigFile(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
    }

    [NonEvent]
    public void ResourceManagerCultureNotFoundInConfigFile(string baseName, Assembly mainAssembly, string cultureName)
    {
      if (!this.IsEnabled())
        return;
      this.ResourceManagerCultureNotFoundInConfigFile(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
    }

    [NonEvent]
    public void ResourceManagerCultureFoundInConfigFile(string baseName, Assembly mainAssembly, string cultureName)
    {
      if (!this.IsEnabled())
        return;
      this.ResourceManagerCultureFoundInConfigFile(baseName, FrameworkEventSource.GetName(mainAssembly), cultureName);
    }

    private static string GetName(Assembly assembly)
    {
      if (assembly == (Assembly) null)
        return "<<NULL>>";
      return assembly.FullName;
    }

    [Event(30, Keywords = (EventKeywords) 18, Level = EventLevel.Verbose)]
    public void ThreadPoolEnqueueWork(long workID)
    {
      this.WriteEvent(30, workID);
    }

    [NonEvent]
    [SecuritySafeCritical]
    public unsafe void ThreadPoolEnqueueWorkObject(object workID)
    {
      this.ThreadPoolEnqueueWork((long) (ulong) *(IntPtr*) (void*) JitHelpers.UnsafeCastToStackPointer<object>(ref workID));
    }

    [Event(31, Keywords = (EventKeywords) 18, Level = EventLevel.Verbose)]
    public void ThreadPoolDequeueWork(long workID)
    {
      this.WriteEvent(31, workID);
    }

    [NonEvent]
    [SecuritySafeCritical]
    public unsafe void ThreadPoolDequeueWorkObject(object workID)
    {
      this.ThreadPoolDequeueWork((long) (ulong) *(IntPtr*) (void*) JitHelpers.UnsafeCastToStackPointer<object>(ref workID));
    }

    [Event(140, ActivityOptions = EventActivityOptions.Disable, Keywords = (EventKeywords) 4, Level = EventLevel.Informational, Opcode = EventOpcode.Start, Task = (EventTask) 1, Version = 1)]
    private void GetResponseStart(long id, string uri, bool success, bool synchronous)
    {
      this.WriteEvent(140, id, uri, success, synchronous);
    }

    [Event(141, ActivityOptions = EventActivityOptions.Disable, Keywords = (EventKeywords) 4, Level = EventLevel.Informational, Opcode = EventOpcode.Stop, Task = (EventTask) 1, Version = 1)]
    private void GetResponseStop(long id, bool success, bool synchronous, int statusCode)
    {
      this.WriteEvent(141, id, success, synchronous, statusCode);
    }

    [Event(142, ActivityOptions = EventActivityOptions.Disable, Keywords = (EventKeywords) 4, Level = EventLevel.Informational, Opcode = EventOpcode.Start, Task = (EventTask) 2, Version = 1)]
    private void GetRequestStreamStart(long id, string uri, bool success, bool synchronous)
    {
      this.WriteEvent(142, id, uri, success, synchronous);
    }

    [Event(143, ActivityOptions = EventActivityOptions.Disable, Keywords = (EventKeywords) 4, Level = EventLevel.Informational, Opcode = EventOpcode.Stop, Task = (EventTask) 2, Version = 1)]
    private void GetRequestStreamStop(long id, bool success, bool synchronous)
    {
      this.WriteEvent(143, id, success, synchronous);
    }

    [NonEvent]
    [SecuritySafeCritical]
    public void BeginGetResponse(object id, string uri, bool success, bool synchronous)
    {
      if (!this.IsEnabled())
        return;
      this.GetResponseStart(FrameworkEventSource.IdForObject(id), uri, success, synchronous);
    }

    [NonEvent]
    [SecuritySafeCritical]
    public void EndGetResponse(object id, bool success, bool synchronous, int statusCode)
    {
      if (!this.IsEnabled())
        return;
      this.GetResponseStop(FrameworkEventSource.IdForObject(id), success, synchronous, statusCode);
    }

    [NonEvent]
    [SecuritySafeCritical]
    public void BeginGetRequestStream(object id, string uri, bool success, bool synchronous)
    {
      if (!this.IsEnabled())
        return;
      this.GetRequestStreamStart(FrameworkEventSource.IdForObject(id), uri, success, synchronous);
    }

    [NonEvent]
    [SecuritySafeCritical]
    public void EndGetRequestStream(object id, bool success, bool synchronous)
    {
      if (!this.IsEnabled())
        return;
      this.GetRequestStreamStop(FrameworkEventSource.IdForObject(id), success, synchronous);
    }

    [Event(150, Keywords = (EventKeywords) 16, Level = EventLevel.Informational, Opcode = EventOpcode.Send, Task = (EventTask) 3)]
    public void ThreadTransferSend(long id, int kind, string info, bool multiDequeues)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(150, id, kind, info, multiDequeues);
    }

    [NonEvent]
    [SecuritySafeCritical]
    public unsafe void ThreadTransferSendObj(object id, int kind, string info, bool multiDequeues)
    {
      this.ThreadTransferSend((long) (ulong) *(IntPtr*) (void*) JitHelpers.UnsafeCastToStackPointer<object>(ref id), kind, info, multiDequeues);
    }

    [Event(151, Keywords = (EventKeywords) 16, Level = EventLevel.Informational, Opcode = EventOpcode.Receive, Task = (EventTask) 3)]
    public void ThreadTransferReceive(long id, int kind, string info)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(151, id, kind, info);
    }

    [NonEvent]
    [SecuritySafeCritical]
    public unsafe void ThreadTransferReceiveObj(object id, int kind, string info)
    {
      this.ThreadTransferReceive((long) (ulong) *(IntPtr*) (void*) JitHelpers.UnsafeCastToStackPointer<object>(ref id), kind, info);
    }

    [Event(152, Keywords = (EventKeywords) 16, Level = EventLevel.Informational, Opcode = EventOpcode.DataCollectionStart | EventOpcode.Suspend, Task = (EventTask) 3)]
    public void ThreadTransferReceiveHandled(long id, int kind, string info)
    {
      if (!this.IsEnabled())
        return;
      this.WriteEvent(152, id, kind, info);
    }

    [NonEvent]
    [SecuritySafeCritical]
    public unsafe void ThreadTransferReceiveHandledObj(object id, int kind, string info)
    {
      this.ThreadTransferReceive((long) (ulong) *(IntPtr*) (void*) JitHelpers.UnsafeCastToStackPointer<object>(ref id), kind, info);
    }

    private static long IdForObject(object obj)
    {
      return (long) obj.GetHashCode() + 9223372032559808512L;
    }

    public static class Keywords
    {
      public const EventKeywords Loader = (EventKeywords) 1;
      public const EventKeywords ThreadPool = (EventKeywords) 2;
      public const EventKeywords NetClient = (EventKeywords) 4;
      public const EventKeywords DynamicTypeUsage = (EventKeywords) 8;
      public const EventKeywords ThreadTransfer = (EventKeywords) 16;
    }

    [FriendAccessAllowed]
    public static class Tasks
    {
      public const EventTask GetResponse = (EventTask) 1;
      public const EventTask GetRequestStream = (EventTask) 2;
      public const EventTask ThreadTransfer = (EventTask) 3;
    }

    [FriendAccessAllowed]
    public static class Opcodes
    {
      public const EventOpcode ReceiveHandled = EventOpcode.DataCollectionStart | EventOpcode.Suspend;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.EventProvider
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Win32;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Diagnostics.Tracing
{
  [HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
  internal class EventProvider : IDisposable
  {
    private static int[] nibblebits = new int[16]
    {
      0,
      1,
      1,
      2,
      1,
      2,
      2,
      3,
      1,
      2,
      2,
      3,
      2,
      3,
      3,
      4
    };
    private static bool m_setInformationMissing;
    [SecurityCritical]
    private UnsafeNativeMethods.ManifestEtw.EtwEnableCallback m_etwCallback;
    private long m_regHandle;
    private byte m_level;
    private long m_anyKeywordMask;
    private long m_allKeywordMask;
    private List<EventProvider.SessionInfo> m_liveSessions;
    private bool m_enabled;
    private Guid m_providerId;
    internal bool m_disposed;
    [ThreadStatic]
    private static EventProvider.WriteEventErrorCode s_returnCode;
    private const int s_basicTypeAllocationBufferSize = 16;
    private const int s_etwMaxNumberArguments = 128;
    private const int s_etwAPIMaxRefObjCount = 8;
    private const int s_maxEventDataDescriptors = 128;
    private const int s_traceEventMaximumSize = 65482;
    private const int s_traceEventMaximumStringSize = 32724;

    [SecurityCritical]
    [PermissionSet(SecurityAction.Demand, Unrestricted = true)]
    protected EventProvider(Guid providerGuid)
    {
      this.m_providerId = providerGuid;
      this.Register(providerGuid);
    }

    internal EventProvider()
    {
    }

    [SecurityCritical]
    internal void Register(Guid providerGuid)
    {
      this.m_providerId = providerGuid;
      this.m_etwCallback = new UnsafeNativeMethods.ManifestEtw.EtwEnableCallback(this.EtwEnableCallBack);
      uint num = this.EventRegister(ref this.m_providerId, this.m_etwCallback);
      if (num != 0U)
        throw new ArgumentException(Win32Native.GetMessage((int) num));
    }

    [SecurityCritical]
    internal unsafe int SetInformation(UnsafeNativeMethods.ManifestEtw.EVENT_INFO_CLASS eventInfoClass, void* data, int dataSize)
    {
      int num = 50;
      if (!EventProvider.m_setInformationMissing)
      {
        try
        {
          num = UnsafeNativeMethods.ManifestEtw.EventSetInformation(this.m_regHandle, eventInfoClass, data, dataSize);
        }
        catch (TypeLoadException ex)
        {
          EventProvider.m_setInformationMissing = true;
        }
      }
      return num;
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    [SecuritySafeCritical]
    protected virtual void Dispose(bool disposing)
    {
      if (this.m_disposed)
        return;
      this.m_enabled = false;
      lock (EventListener.EventListenersLock)
      {
        if (this.m_disposed)
          return;
        this.Deregister();
        this.m_disposed = true;
      }
    }

    public virtual void Close()
    {
      this.Dispose();
    }

    ~EventProvider()
    {
      this.Dispose(false);
    }

    [SecurityCritical]
    private void Deregister()
    {
      if (this.m_regHandle == 0L)
        return;
      int num = (int) this.EventUnregister();
      this.m_regHandle = 0L;
    }

    [SecurityCritical]
    private unsafe void EtwEnableCallBack([In] ref Guid sourceId, [In] int controlCode, [In] byte setLevel, [In] long anyKeyword, [In] long allKeyword, [In] UnsafeNativeMethods.ManifestEtw.EVENT_FILTER_DESCRIPTOR* filterData, [In] void* callbackContext)
    {
      try
      {
        ControllerCommand command = ControllerCommand.Update;
        IDictionary<string, string> arguments = (IDictionary<string, string>) null;
        bool flag1 = false;
        switch (controlCode)
        {
          case 0:
            this.m_enabled = false;
            this.m_level = (byte) 0;
            this.m_anyKeywordMask = 0L;
            this.m_allKeywordMask = 0L;
            this.m_liveSessions = (List<EventProvider.SessionInfo>) null;
            break;
          case 1:
            this.m_enabled = true;
            this.m_level = setLevel;
            this.m_anyKeywordMask = anyKeyword;
            this.m_allKeywordMask = allKeyword;
            List<Tuple<EventProvider.SessionInfo, bool>> sessions = this.GetSessions();
            using (List<Tuple<EventProvider.SessionInfo, bool>>.Enumerator enumerator = sessions.GetEnumerator())
            {
              while (enumerator.MoveNext())
              {
                Tuple<EventProvider.SessionInfo, bool> current = enumerator.Current;
                int sessionIdBit = current.Item1.sessionIdBit;
                int etwSessionId = current.Item1.etwSessionId;
                bool flag2 = current.Item2;
                flag1 = true;
                arguments = (IDictionary<string, string>) null;
                if (sessions.Count > 1)
                  filterData = (UnsafeNativeMethods.ManifestEtw.EVENT_FILTER_DESCRIPTOR*) null;
                byte[] data;
                int dataStart;
                if (flag2 && this.GetDataFromController(etwSessionId, filterData, out command, out data, out dataStart))
                {
                  arguments = (IDictionary<string, string>) new Dictionary<string, string>(4);
                  int num1;
                  for (; dataStart < data.Length; dataStart = num1 + 1)
                  {
                    int num2 = EventProvider.FindNull(data, dataStart);
                    int num3 = num2 + 1;
                    num1 = EventProvider.FindNull(data, num3);
                    if (num1 < data.Length)
                    {
                      string index = Encoding.UTF8.GetString(data, dataStart, num2 - dataStart);
                      string str = Encoding.UTF8.GetString(data, num3, num1 - num3);
                      arguments[index] = str;
                    }
                  }
                }
                this.OnControllerCommand(command, arguments, flag2 ? sessionIdBit : -sessionIdBit, etwSessionId);
              }
              break;
            }
          case 2:
            command = ControllerCommand.SendManifest;
            break;
          default:
            return;
        }
        if (flag1)
          return;
        this.OnControllerCommand(command, arguments, 0, 0);
      }
      catch (Exception ex)
      {
      }
    }

    protected virtual void OnControllerCommand(ControllerCommand command, IDictionary<string, string> arguments, int sessionId, int etwSessionId)
    {
    }

    protected EventLevel Level
    {
      get
      {
        return (EventLevel) this.m_level;
      }
      set
      {
        this.m_level = (byte) value;
      }
    }

    protected EventKeywords MatchAnyKeyword
    {
      get
      {
        return (EventKeywords) this.m_anyKeywordMask;
      }
      set
      {
        this.m_anyKeywordMask = (long) value;
      }
    }

    protected EventKeywords MatchAllKeyword
    {
      get
      {
        return (EventKeywords) this.m_allKeywordMask;
      }
      set
      {
        this.m_allKeywordMask = (long) value;
      }
    }

    private static int FindNull(byte[] buffer, int idx)
    {
      while (idx < buffer.Length && buffer[idx] != (byte) 0)
        ++idx;
      return idx;
    }

    [SecuritySafeCritical]
    private List<Tuple<EventProvider.SessionInfo, bool>> GetSessions()
    {
      List<EventProvider.SessionInfo> liveSessionList = (List<EventProvider.SessionInfo>) null;
      this.GetSessionInfo((Action<int, long>) ((etwSessionId, matchAllKeywords) => EventProvider.GetSessionInfoCallback(etwSessionId, matchAllKeywords, ref liveSessionList)));
      List<Tuple<EventProvider.SessionInfo, bool>> tupleList = new List<Tuple<EventProvider.SessionInfo, bool>>();
      if (this.m_liveSessions != null)
      {
        foreach (EventProvider.SessionInfo liveSession in this.m_liveSessions)
        {
          int index;
          if ((index = EventProvider.IndexOfSessionInList(liveSessionList, liveSession.etwSessionId)) < 0 || liveSessionList[index].sessionIdBit != liveSession.sessionIdBit)
            tupleList.Add(Tuple.Create<EventProvider.SessionInfo, bool>(liveSession, false));
        }
      }
      if (liveSessionList != null)
      {
        foreach (EventProvider.SessionInfo sessionInfo in liveSessionList)
        {
          int index;
          if ((index = EventProvider.IndexOfSessionInList(this.m_liveSessions, sessionInfo.etwSessionId)) < 0 || this.m_liveSessions[index].sessionIdBit != sessionInfo.sessionIdBit)
            tupleList.Add(Tuple.Create<EventProvider.SessionInfo, bool>(sessionInfo, true));
        }
      }
      this.m_liveSessions = liveSessionList;
      return tupleList;
    }

    private static void GetSessionInfoCallback(int etwSessionId, long matchAllKeywords, ref List<EventProvider.SessionInfo> sessionList)
    {
      uint n = (uint) SessionMask.FromEventKeywords((ulong) matchAllKeywords);
      if (EventProvider.bitcount(n) > 1)
        return;
      if (sessionList == null)
        sessionList = new List<EventProvider.SessionInfo>(8);
      if (EventProvider.bitcount(n) == 1)
        sessionList.Add(new EventProvider.SessionInfo(EventProvider.bitindex(n) + 1, etwSessionId));
      else
        sessionList.Add(new EventProvider.SessionInfo(EventProvider.bitcount((uint) SessionMask.All) + 1, etwSessionId));
    }

    [SecurityCritical]
    private unsafe void GetSessionInfo(Action<int, long> action)
    {
      int ReturnLength = 256;
      byte* numPtr1;
      int num;
      do
      {
        byte* numPtr2 = stackalloc byte[ReturnLength];
        numPtr1 = numPtr2;
        fixed (Guid* guidPtr = &this.m_providerId)
          num = UnsafeNativeMethods.ManifestEtw.EnumerateTraceGuidsEx(UnsafeNativeMethods.ManifestEtw.TRACE_QUERY_INFO_CLASS.TraceGuidQueryInfo, (void*) guidPtr, sizeof (Guid), (void*) numPtr1, ReturnLength, ref ReturnLength);
        if (num == 0)
          goto label_4;
      }
      while (num == 122);
      return;
label_4:
      UnsafeNativeMethods.ManifestEtw.TRACE_GUID_INFO* traceGuidInfoPtr = (UnsafeNativeMethods.ManifestEtw.TRACE_GUID_INFO*) numPtr1;
      UnsafeNativeMethods.ManifestEtw.TRACE_PROVIDER_INSTANCE_INFO* providerInstanceInfoPtr = (UnsafeNativeMethods.ManifestEtw.TRACE_PROVIDER_INSTANCE_INFO*) (traceGuidInfoPtr + 1);
      int currentProcessId = (int) Win32Native.GetCurrentProcessId();
      for (int index1 = 0; index1 < traceGuidInfoPtr->InstanceCount; ++index1)
      {
        if (providerInstanceInfoPtr->Pid == currentProcessId)
        {
          UnsafeNativeMethods.ManifestEtw.TRACE_ENABLE_INFO* traceEnableInfoPtr = (UnsafeNativeMethods.ManifestEtw.TRACE_ENABLE_INFO*) (providerInstanceInfoPtr + 1);
          for (int index2 = 0; index2 < providerInstanceInfoPtr->EnableCount; ++index2)
            action((int) traceEnableInfoPtr[index2].LoggerId, traceEnableInfoPtr[index2].MatchAllKeyword);
        }
        if (providerInstanceInfoPtr->NextOffset == 0)
          break;
        providerInstanceInfoPtr = (UnsafeNativeMethods.ManifestEtw.TRACE_PROVIDER_INSTANCE_INFO*) ((UIntPtr) providerInstanceInfoPtr + (UIntPtr) providerInstanceInfoPtr->NextOffset);
      }
    }

    private static int IndexOfSessionInList(List<EventProvider.SessionInfo> sessions, int etwSessionId)
    {
      if (sessions == null)
        return -1;
      for (int index = 0; index < sessions.Count; ++index)
      {
        if (sessions[index].etwSessionId == etwSessionId)
          return index;
      }
      return -1;
    }

    [SecurityCritical]
    private unsafe bool GetDataFromController(int etwSessionId, UnsafeNativeMethods.ManifestEtw.EVENT_FILTER_DESCRIPTOR* filterData, out ControllerCommand command, out byte[] data, out int dataStart)
    {
      data = (byte[]) null;
      dataStart = 0;
      if ((IntPtr) filterData == IntPtr.Zero)
      {
        string str1 = "\\Microsoft\\Windows\\CurrentVersion\\Winevt\\Publishers\\{" + (object) this.m_providerId + "}";
        string str2 = Marshal.SizeOf(typeof (IntPtr)) != 8 ? "HKEY_LOCAL_MACHINE\\Software" + str1 : "HKEY_LOCAL_MACHINE\\Software\\Wow6432Node" + str1;
        string valueName = "ControllerData_Session_" + etwSessionId.ToString((IFormatProvider) CultureInfo.InvariantCulture);
        new RegistryPermission(RegistryPermissionAccess.Read, str2).Assert();
        data = Registry.GetValue(str2, valueName, (object) null) as byte[];
        if (data != null)
        {
          command = ControllerCommand.Update;
          return true;
        }
        command = ControllerCommand.Update;
        return false;
      }
      if (filterData->Ptr != 0L && 0 < filterData->Size && filterData->Size <= 1024)
      {
        data = new byte[filterData->Size];
        Marshal.Copy((IntPtr) filterData->Ptr, data, 0, data.Length);
      }
      command = (ControllerCommand) filterData->Type;
      return true;
    }

    public bool IsEnabled()
    {
      return this.m_enabled;
    }

    public bool IsEnabled(byte level, long keywords)
    {
      return this.m_enabled && ((int) level <= (int) this.m_level || this.m_level == (byte) 0) && (keywords == 0L || (keywords & this.m_anyKeywordMask) != 0L && (keywords & this.m_allKeywordMask) == this.m_allKeywordMask);
    }

    internal bool IsValid()
    {
      return (ulong) this.m_regHandle > 0UL;
    }

    public static EventProvider.WriteEventErrorCode GetLastWriteEventError()
    {
      return EventProvider.s_returnCode;
    }

    private static void SetLastError(int error)
    {
      switch (error)
      {
        case 8:
          EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.NoFreeBuffers;
          break;
        case 234:
        case 534:
          EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.EventTooBig;
          break;
      }
    }

    [SecurityCritical]
    private static unsafe object EncodeObject(ref object data, ref EventProvider.EventData* dataDescriptor, ref byte* dataBuffer, ref uint totalEventSize)
    {
      string str;
      byte[] numArray;
      while (true)
      {
        dataDescriptor->Reserved = 0U;
        str = data as string;
        numArray = (byte[]) null;
        if (str == null)
        {
          if ((numArray = data as byte[]) == null)
          {
            if (!(data is IntPtr))
            {
              if (!(data is int))
              {
                if (!(data is long))
                {
                  if (!(data is uint))
                  {
                    if (!(data is ulong))
                    {
                      if (!(data is char))
                      {
                        if (!(data is byte))
                        {
                          if (!(data is short))
                          {
                            if (!(data is sbyte))
                            {
                              if (!(data is ushort))
                              {
                                if (!(data is float))
                                {
                                  if (!(data is double))
                                  {
                                    if (!(data is bool))
                                    {
                                      if (!(data is Guid))
                                      {
                                        if (!(data is Decimal))
                                        {
                                          if (!(data is DateTime))
                                          {
                                            if (data is Enum)
                                            {
                                              Type underlyingType = Enum.GetUnderlyingType(data.GetType());
                                              if (underlyingType == typeof (int))
                                                data = (object) ((IConvertible) data).ToInt32((IFormatProvider) null);
                                              else if (underlyingType == typeof (long))
                                                data = (object) ((IConvertible) data).ToInt64((IFormatProvider) null);
                                              else
                                                goto label_43;
                                            }
                                            else
                                              goto label_43;
                                          }
                                          else
                                            goto label_35;
                                        }
                                        else
                                          goto label_33;
                                      }
                                      else
                                        goto label_31;
                                    }
                                    else
                                      goto label_29;
                                  }
                                  else
                                    goto label_27;
                                }
                                else
                                  goto label_25;
                              }
                              else
                                goto label_23;
                            }
                            else
                              goto label_21;
                          }
                          else
                            goto label_19;
                        }
                        else
                          goto label_17;
                      }
                      else
                        goto label_15;
                    }
                    else
                      goto label_13;
                  }
                  else
                    goto label_11;
                }
                else
                  goto label_9;
              }
              else
                goto label_7;
            }
            else
              goto label_5;
          }
          else
            goto label_3;
        }
        else
          break;
      }
      dataDescriptor->Size = (uint) ((str.Length + 1) * 2);
      goto label_44;
label_3:
      *(int*) dataBuffer = numArray.Length;
      dataDescriptor->Ptr = (ulong) dataBuffer;
      dataDescriptor->Size = 4U;
      totalEventSize += dataDescriptor->Size;
      ++dataDescriptor;
      dataBuffer += 16;
      dataDescriptor->Size = (uint) numArray.Length;
      goto label_44;
label_5:
      dataDescriptor->Size = (uint) sizeof (IntPtr);
      IntPtr* numPtr1 = (IntPtr*) dataBuffer;
      *numPtr1 = (IntPtr) data;
      dataDescriptor->Ptr = (ulong) numPtr1;
      goto label_44;
label_7:
      dataDescriptor->Size = 4U;
      int* numPtr2 = (int*) dataBuffer;
      *numPtr2 = (int) data;
      dataDescriptor->Ptr = (ulong) numPtr2;
      goto label_44;
label_9:
      dataDescriptor->Size = 8U;
      long* numPtr3 = (long*) dataBuffer;
      *numPtr3 = (long) data;
      dataDescriptor->Ptr = (ulong) numPtr3;
      goto label_44;
label_11:
      dataDescriptor->Size = 4U;
      uint* numPtr4 = (uint*) dataBuffer;
      *numPtr4 = (uint) data;
      dataDescriptor->Ptr = (ulong) numPtr4;
      goto label_44;
label_13:
      dataDescriptor->Size = 8U;
      ulong* numPtr5 = (ulong*) dataBuffer;
      *numPtr5 = (ulong) data;
      dataDescriptor->Ptr = (ulong) numPtr5;
      goto label_44;
label_15:
      dataDescriptor->Size = 2U;
      char* chPtr = (char*) dataBuffer;
      *chPtr = (char) data;
      dataDescriptor->Ptr = (ulong) chPtr;
      goto label_44;
label_17:
      dataDescriptor->Size = 1U;
      byte* numPtr6 = dataBuffer;
      *numPtr6 = (byte) data;
      dataDescriptor->Ptr = (ulong) numPtr6;
      goto label_44;
label_19:
      dataDescriptor->Size = 2U;
      short* numPtr7 = (short*) dataBuffer;
      *numPtr7 = (short) data;
      dataDescriptor->Ptr = (ulong) numPtr7;
      goto label_44;
label_21:
      dataDescriptor->Size = 1U;
      sbyte* numPtr8 = (sbyte*) dataBuffer;
      *numPtr8 = (sbyte) data;
      dataDescriptor->Ptr = (ulong) numPtr8;
      goto label_44;
label_23:
      dataDescriptor->Size = 2U;
      ushort* numPtr9 = (ushort*) dataBuffer;
      *numPtr9 = (ushort) data;
      dataDescriptor->Ptr = (ulong) numPtr9;
      goto label_44;
label_25:
      dataDescriptor->Size = 4U;
      float* numPtr10 = (float*) dataBuffer;
      *numPtr10 = (float) data;
      dataDescriptor->Ptr = (ulong) numPtr10;
      goto label_44;
label_27:
      dataDescriptor->Size = 8U;
      double* numPtr11 = (double*) dataBuffer;
      *numPtr11 = (double) data;
      dataDescriptor->Ptr = (ulong) numPtr11;
      goto label_44;
label_29:
      dataDescriptor->Size = 4U;
      int* numPtr12 = (int*) dataBuffer;
      *numPtr12 = !(bool) data ? 0 : 1;
      dataDescriptor->Ptr = (ulong) numPtr12;
      goto label_44;
label_31:
      dataDescriptor->Size = (uint) sizeof (Guid);
      Guid* guidPtr = (Guid*) dataBuffer;
      *guidPtr = (Guid) data;
      dataDescriptor->Ptr = (ulong) guidPtr;
      goto label_44;
label_33:
      dataDescriptor->Size = 16U;
      Decimal* numPtr13 = (Decimal*) dataBuffer;
      *numPtr13 = (Decimal) data;
      dataDescriptor->Ptr = (ulong) numPtr13;
      goto label_44;
label_35:
      long num = 0;
      DateTime dateTime = (DateTime) data;
      if (dateTime.Ticks > 504911232000000000L)
      {
        dateTime = (DateTime) data;
        num = dateTime.ToFileTimeUtc();
      }
      dataDescriptor->Size = 8U;
      long* numPtr14 = (long*) dataBuffer;
      *numPtr14 = num;
      dataDescriptor->Ptr = (ulong) numPtr14;
      goto label_44;
label_43:
      str = data != null ? data.ToString() : "";
      dataDescriptor->Size = (uint) ((str.Length + 1) * 2);
label_44:
      totalEventSize += dataDescriptor->Size;
      ++dataDescriptor;
      dataBuffer += 16;
      return (object) str ?? (object) numArray;
    }

    [SecurityCritical]
    internal unsafe bool WriteEvent(ref EventDescriptor eventDescriptor, Guid* activityID, Guid* childActivityID, params object[] eventPayload)
    {
      int error = 0;
      if (this.IsEnabled(eventDescriptor.Level, eventDescriptor.Keywords))
      {
        int length1 = eventPayload.Length;
        if (length1 > 128)
        {
          EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.TooManyArgs;
          return false;
        }
        uint totalEventSize = 0;
        int length2 = 0;
        List<int> intList = new List<int>(8);
        List<object> objectList = new List<object>(8);
        EventProvider.EventData* userData = stackalloc EventProvider.EventData[2 * length1];
        EventProvider.EventData* dataDescriptor = userData;
        byte* numPtr1 = stackalloc byte[32 * length1];
        byte* dataBuffer = numPtr1;
        bool flag = false;
        for (int index = 0; index < eventPayload.Length; ++index)
        {
          if (eventPayload[index] != null)
          {
            object obj = EventProvider.EncodeObject(ref eventPayload[index], ref dataDescriptor, ref dataBuffer, ref totalEventSize);
            if (obj != null)
            {
              int num = (int) (dataDescriptor - userData - 1L);
              if (!(obj is string))
              {
                if (eventPayload.Length + num + 1 - index > 128)
                {
                  EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.TooManyArgs;
                  return false;
                }
                flag = true;
              }
              objectList.Add(obj);
              intList.Add(num);
              ++length2;
            }
          }
          else
          {
            EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.NullInput;
            return false;
          }
        }
        int userDataCount = (int) (dataDescriptor - userData);
        if (totalEventSize > 65482U)
        {
          EventProvider.s_returnCode = EventProvider.WriteEventErrorCode.EventTooBig;
          return false;
        }
        if (!flag && length2 < 8)
        {
          for (; length2 < 8; ++length2)
            objectList.Add((object) null);
          string str1 = (string) objectList[0];
          char* chPtr1 = (char*) str1;
          if ((IntPtr) chPtr1 != IntPtr.Zero)
            chPtr1 += RuntimeHelpers.OffsetToStringData;
          string str2 = (string) objectList[1];
          char* chPtr2 = (char*) str2;
          if ((IntPtr) chPtr2 != IntPtr.Zero)
            chPtr2 += RuntimeHelpers.OffsetToStringData;
          string str3 = (string) objectList[2];
          char* chPtr3 = (char*) str3;
          if ((IntPtr) chPtr3 != IntPtr.Zero)
            chPtr3 += RuntimeHelpers.OffsetToStringData;
          string str4 = (string) objectList[3];
          char* chPtr4 = (char*) str4;
          if ((IntPtr) chPtr4 != IntPtr.Zero)
            chPtr4 += RuntimeHelpers.OffsetToStringData;
          string str5 = (string) objectList[4];
          char* chPtr5 = (char*) str5;
          if ((IntPtr) chPtr5 != IntPtr.Zero)
            chPtr5 += RuntimeHelpers.OffsetToStringData;
          string str6 = (string) objectList[5];
          char* chPtr6 = (char*) str6;
          if ((IntPtr) chPtr6 != IntPtr.Zero)
            chPtr6 += RuntimeHelpers.OffsetToStringData;
          string str7 = (string) objectList[6];
          char* chPtr7 = (char*) str7;
          if ((IntPtr) chPtr7 != IntPtr.Zero)
            chPtr7 += RuntimeHelpers.OffsetToStringData;
          string str8 = (string) objectList[7];
          char* chPtr8 = (char*) str8;
          if ((IntPtr) chPtr8 != IntPtr.Zero)
            chPtr8 += RuntimeHelpers.OffsetToStringData;
          EventProvider.EventData* eventDataPtr = userData;
          if (objectList[0] != null)
            eventDataPtr[intList[0]].Ptr = (ulong) chPtr1;
          if (objectList[1] != null)
            eventDataPtr[intList[1]].Ptr = (ulong) chPtr2;
          if (objectList[2] != null)
            eventDataPtr[intList[2]].Ptr = (ulong) chPtr3;
          if (objectList[3] != null)
            eventDataPtr[intList[3]].Ptr = (ulong) chPtr4;
          if (objectList[4] != null)
            eventDataPtr[intList[4]].Ptr = (ulong) chPtr5;
          if (objectList[5] != null)
            eventDataPtr[intList[5]].Ptr = (ulong) chPtr6;
          if (objectList[6] != null)
            eventDataPtr[intList[6]].Ptr = (ulong) chPtr7;
          if (objectList[7] != null)
            eventDataPtr[intList[7]].Ptr = (ulong) chPtr8;
          error = UnsafeNativeMethods.ManifestEtw.EventWriteTransferWrapper(this.m_regHandle, ref eventDescriptor, activityID, childActivityID, userDataCount, userData);
          str1 = (string) null;
          str2 = (string) null;
          str3 = (string) null;
          str4 = (string) null;
          str5 = (string) null;
          str6 = (string) null;
          str7 = (string) null;
          str8 = (string) null;
        }
        else
        {
          EventProvider.EventData* eventDataPtr = userData;
          GCHandle[] gcHandleArray = new GCHandle[length2];
          for (int index = 0; index < length2; ++index)
          {
            gcHandleArray[index] = GCHandle.Alloc(objectList[index], GCHandleType.Pinned);
            if (objectList[index] is string)
            {
              string str = (string) objectList[index];
              char* chPtr = (char*) str;
              if ((IntPtr) chPtr != IntPtr.Zero)
                chPtr += RuntimeHelpers.OffsetToStringData;
              eventDataPtr[intList[index]].Ptr = (ulong) chPtr;
              str = (string) null;
            }
            else
            {
              fixed (byte* numPtr2 = (byte[]) objectList[index])
                eventDataPtr[intList[index]].Ptr = (ulong) numPtr2;
            }
          }
          error = UnsafeNativeMethods.ManifestEtw.EventWriteTransferWrapper(this.m_regHandle, ref eventDescriptor, activityID, childActivityID, userDataCount, userData);
          for (int index = 0; index < length2; ++index)
            gcHandleArray[index].Free();
        }
      }
      if (error == 0)
        return true;
      EventProvider.SetLastError(error);
      return false;
    }

    [SecurityCritical]
    protected internal unsafe bool WriteEvent(ref EventDescriptor eventDescriptor, Guid* activityID, Guid* childActivityID, int dataCount, IntPtr data)
    {
      int error = UnsafeNativeMethods.ManifestEtw.EventWriteTransferWrapper(this.m_regHandle, ref eventDescriptor, activityID, childActivityID, dataCount, (EventProvider.EventData*) (void*) data);
      if (error == 0)
        return true;
      EventProvider.SetLastError(error);
      return false;
    }

    [SecurityCritical]
    internal unsafe bool WriteEventRaw(ref EventDescriptor eventDescriptor, Guid* activityID, Guid* relatedActivityID, int dataCount, IntPtr data)
    {
      int error = UnsafeNativeMethods.ManifestEtw.EventWriteTransferWrapper(this.m_regHandle, ref eventDescriptor, activityID, relatedActivityID, dataCount, (EventProvider.EventData*) (void*) data);
      if (error == 0)
        return true;
      EventProvider.SetLastError(error);
      return false;
    }

    [SecurityCritical]
    private unsafe uint EventRegister(ref Guid providerId, UnsafeNativeMethods.ManifestEtw.EtwEnableCallback enableCallback)
    {
      this.m_providerId = providerId;
      this.m_etwCallback = enableCallback;
      return UnsafeNativeMethods.ManifestEtw.EventRegister(ref providerId, enableCallback, (void*) null, ref this.m_regHandle);
    }

    [SecurityCritical]
    private uint EventUnregister()
    {
      uint num = UnsafeNativeMethods.ManifestEtw.EventUnregister(this.m_regHandle);
      this.m_regHandle = 0L;
      return num;
    }

    private static int bitcount(uint n)
    {
      int num = 0;
      while (n != 0U)
      {
        num += EventProvider.nibblebits[(int) n & 15];
        n >>= 4;
      }
      return num;
    }

    private static int bitindex(uint n)
    {
      int num = 0;
      while (((long) n & (long) (1 << num)) == 0L)
        ++num;
      return num;
    }

    public struct EventData
    {
      internal ulong Ptr;
      internal uint Size;
      internal uint Reserved;
    }

    public struct SessionInfo
    {
      internal int sessionIdBit;
      internal int etwSessionId;

      internal SessionInfo(int sessionIdBit_, int etwSessionId_)
      {
        this.sessionIdBit = sessionIdBit_;
        this.etwSessionId = etwSessionId_;
      }
    }

    public enum WriteEventErrorCode
    {
      NoError,
      NoFreeBuffers,
      EventTooBig,
      NullInput,
      TooManyArgs,
      Other,
    }
  }
}

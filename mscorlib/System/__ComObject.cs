// Decompiled with JetBrains decompiler
// Type: System.__ComObject
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;
using System.Security.Permissions;

namespace System
{
  [__DynamicallyInvokable]
  internal class __ComObject : MarshalByRefObject
  {
    private Hashtable m_ObjectToDataMap;

    protected __ComObject()
    {
    }

    public override string ToString()
    {
      if (AppDomain.IsAppXModel())
      {
        IStringable stringable = this as IStringable;
        if (stringable != null)
          return stringable.ToString();
      }
      return base.ToString();
    }

    [SecurityCritical]
    internal IntPtr GetIUnknown(out bool fIsURTAggregated)
    {
      fIsURTAggregated = !this.GetType().IsDefined(typeof (ComImportAttribute), false);
      return Marshal.GetIUnknownForObject((object) this);
    }

    internal object GetData(object key)
    {
      object obj = (object) null;
      lock (this)
      {
        if (this.m_ObjectToDataMap != null)
          obj = this.m_ObjectToDataMap[key];
      }
      return obj;
    }

    internal bool SetData(object key, object data)
    {
      bool flag = false;
      lock (this)
      {
        if (this.m_ObjectToDataMap == null)
          this.m_ObjectToDataMap = new Hashtable();
        if (this.m_ObjectToDataMap[key] == null)
        {
          this.m_ObjectToDataMap[key] = data;
          flag = true;
        }
      }
      return flag;
    }

    [SecurityCritical]
    internal void ReleaseAllData()
    {
      lock (this)
      {
        if (this.m_ObjectToDataMap == null)
          return;
        foreach (object obj in (IEnumerable) this.m_ObjectToDataMap.Values)
        {
          (obj as IDisposable)?.Dispose();
          __ComObject comObject = obj as __ComObject;
          if (comObject != null)
            Marshal.ReleaseComObject((object) comObject);
        }
        this.m_ObjectToDataMap = (Hashtable) null;
      }
    }

    [SecurityCritical]
    internal object GetEventProvider(RuntimeType t)
    {
      return this.GetData((object) t) ?? this.CreateEventProvider(t);
    }

    [SecurityCritical]
    internal int ReleaseSelf()
    {
      return Marshal.InternalReleaseComObject((object) this);
    }

    [SecurityCritical]
    internal void FinalReleaseSelf()
    {
      Marshal.InternalFinalReleaseComObject((object) this);
    }

    [SecurityCritical]
    [ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
    private object CreateEventProvider(RuntimeType t)
    {
      object data = Activator.CreateInstance((Type) t, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, (Binder) null, new object[1]
      {
        (object) this
      }, (CultureInfo) null);
      if (!this.SetData((object) t, data))
      {
        (data as IDisposable)?.Dispose();
        data = this.GetData((object) t);
      }
      return data;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Messaging.IllogicalCallContext
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
  internal class IllogicalCallContext
  {
    private Hashtable m_Datastore;
    private object m_HostContext;

    private Hashtable Datastore
    {
      get
      {
        if (this.m_Datastore == null)
          this.m_Datastore = new Hashtable();
        return this.m_Datastore;
      }
    }

    internal object HostContext
    {
      get
      {
        return this.m_HostContext;
      }
      set
      {
        this.m_HostContext = value;
      }
    }

    internal bool HasUserData
    {
      get
      {
        if (this.m_Datastore != null)
          return this.m_Datastore.Count > 0;
        return false;
      }
    }

    public void FreeNamedDataSlot(string name)
    {
      this.Datastore.Remove((object) name);
    }

    public object GetData(string name)
    {
      return this.Datastore[(object) name];
    }

    public void SetData(string name, object data)
    {
      this.Datastore[(object) name] = data;
    }

    public IllogicalCallContext CreateCopy()
    {
      IllogicalCallContext illogicalCallContext = new IllogicalCallContext();
      illogicalCallContext.HostContext = this.HostContext;
      if (this.HasUserData)
      {
        IDictionaryEnumerator enumerator = this.m_Datastore.GetEnumerator();
        while (enumerator.MoveNext())
          illogicalCallContext.Datastore[(object) (string) enumerator.Key] = enumerator.Value;
      }
      return illogicalCallContext;
    }

    internal struct Reader
    {
      private IllogicalCallContext m_ctx;

      public Reader(IllogicalCallContext ctx)
      {
        this.m_ctx = ctx;
      }

      public bool IsNull
      {
        get
        {
          return this.m_ctx == null;
        }
      }

      [SecurityCritical]
      public object GetData(string name)
      {
        if (!this.IsNull)
          return this.m_ctx.GetData(name);
        return (object) null;
      }

      public object HostContext
      {
        get
        {
          if (!this.IsNull)
            return this.m_ctx.HostContext;
          return (object) null;
        }
      }
    }
  }
}

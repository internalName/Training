// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.IdentityHolder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Proxies;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting
{
  internal sealed class IdentityHolder
  {
    private static volatile int SetIDCount = 0;
    private static Hashtable _URITable = new Hashtable();
    private static volatile Context _cachedDefaultContext = (Context) null;
    private const int CleanUpCountInterval = 64;
    private const int INFINITE = 2147483647;

    internal static Hashtable URITable
    {
      get
      {
        return IdentityHolder._URITable;
      }
    }

    internal static Context DefaultContext
    {
      [SecurityCritical] get
      {
        if (IdentityHolder._cachedDefaultContext == null)
          IdentityHolder._cachedDefaultContext = Thread.GetDomain().GetDefaultContext();
        return IdentityHolder._cachedDefaultContext;
      }
    }

    private static string MakeURIKey(string uri)
    {
      return Identity.RemoveAppNameOrAppGuidIfNecessary(uri.ToLower(CultureInfo.InvariantCulture));
    }

    private static string MakeURIKeyNoLower(string uri)
    {
      return Identity.RemoveAppNameOrAppGuidIfNecessary(uri);
    }

    internal static ReaderWriterLock TableLock
    {
      get
      {
        return Thread.GetDomain().RemotingData.IDTableLock;
      }
    }

    private static void CleanupIdentities(object state)
    {
      IDictionaryEnumerator enumerator = IdentityHolder.URITable.GetEnumerator();
      ArrayList arrayList = new ArrayList();
      while (enumerator.MoveNext())
      {
        WeakReference weakReference = enumerator.Value as WeakReference;
        if (weakReference != null && weakReference.Target == null)
          arrayList.Add(enumerator.Key);
      }
      foreach (string str in arrayList)
        IdentityHolder.URITable.Remove((object) str);
    }

    [SecurityCritical]
    internal static void FlushIdentityTable()
    {
      ReaderWriterLock tableLock = IdentityHolder.TableLock;
      bool flag = !tableLock.IsWriterLockHeld;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        if (flag)
          tableLock.AcquireWriterLock(int.MaxValue);
        IdentityHolder.CleanupIdentities((object) null);
      }
      finally
      {
        if (flag && tableLock.IsWriterLockHeld)
          tableLock.ReleaseWriterLock();
      }
    }

    private IdentityHolder()
    {
    }

    [SecurityCritical]
    internal static Identity ResolveIdentity(string URI)
    {
      if (URI == null)
        throw new ArgumentNullException(nameof (URI));
      ReaderWriterLock tableLock = IdentityHolder.TableLock;
      bool flag = !tableLock.IsReaderLockHeld;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        if (flag)
          tableLock.AcquireReaderLock(int.MaxValue);
        return IdentityHolder.ResolveReference(IdentityHolder.URITable[(object) IdentityHolder.MakeURIKey(URI)]);
      }
      finally
      {
        if (flag && tableLock.IsReaderLockHeld)
          tableLock.ReleaseReaderLock();
      }
    }

    [SecurityCritical]
    internal static Identity CasualResolveIdentity(string uri)
    {
      if (uri == null)
        return (Identity) null;
      Identity identity = IdentityHolder.CasualResolveReference(IdentityHolder.URITable[(object) IdentityHolder.MakeURIKeyNoLower(uri)]);
      if (identity == null)
      {
        identity = IdentityHolder.CasualResolveReference(IdentityHolder.URITable[(object) IdentityHolder.MakeURIKey(uri)]);
        if (identity == null || identity.IsInitializing)
          identity = (Identity) RemotingConfigHandler.CreateWellKnownObject(uri);
      }
      return identity;
    }

    private static Identity ResolveReference(object o)
    {
      WeakReference weakReference = o as WeakReference;
      if (weakReference != null)
        return (Identity) weakReference.Target;
      return (Identity) o;
    }

    private static Identity CasualResolveReference(object o)
    {
      WeakReference weakReference = o as WeakReference;
      if (weakReference != null)
        return (Identity) weakReference.Target;
      return (Identity) o;
    }

    [SecurityCritical]
    internal static ServerIdentity FindOrCreateServerIdentity(MarshalByRefObject obj, string objURI, int flags)
    {
      bool fServer;
      ServerIdentity serverIdentity = (ServerIdentity) MarshalByRefObject.GetIdentity(obj, out fServer);
      if (serverIdentity == null)
      {
        Context serverCtx = !(obj is ContextBoundObject) ? IdentityHolder.DefaultContext : Thread.CurrentContext;
        ServerIdentity id = new ServerIdentity(obj, serverCtx);
        if (fServer)
        {
          serverIdentity = obj.__RaceSetServerIdentity(id);
        }
        else
        {
          RealProxy realProxy = RemotingServices.GetRealProxy((object) obj);
          realProxy.IdentityObject = (Identity) id;
          serverIdentity = (ServerIdentity) realProxy.IdentityObject;
        }
        if (IdOps.bIsInitializing(flags))
          serverIdentity.IsInitializing = true;
      }
      if (IdOps.bStrongIdentity(flags))
      {
        ReaderWriterLock tableLock = IdentityHolder.TableLock;
        bool flag = !tableLock.IsWriterLockHeld;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
          if (flag)
            tableLock.AcquireWriterLock(int.MaxValue);
          if (serverIdentity.ObjURI == null || !serverIdentity.IsInIDTable())
            IdentityHolder.SetIdentity((Identity) serverIdentity, objURI, DuplicateIdentityOption.Unique);
          if (serverIdentity.IsDisconnected())
            serverIdentity.SetFullyConnected();
        }
        finally
        {
          if (flag && tableLock.IsWriterLockHeld)
            tableLock.ReleaseWriterLock();
        }
      }
      return serverIdentity;
    }

    [SecurityCritical]
    internal static Identity FindOrCreateIdentity(string objURI, string URL, ObjRef objectRef)
    {
      bool flag1 = URL != null;
      Identity idObj = IdentityHolder.ResolveIdentity(flag1 ? URL : objURI);
      if (flag1 && idObj != null && idObj is ServerIdentity)
        throw new RemotingException(string.Format((IFormatProvider) CultureInfo.CurrentCulture, Environment.GetResourceString("Remoting_WellKnown_CantDirectlyConnect"), (object) URL));
      if (idObj == null)
      {
        idObj = new Identity(objURI, URL);
        ReaderWriterLock tableLock = IdentityHolder.TableLock;
        bool flag2 = !tableLock.IsWriterLockHeld;
        RuntimeHelpers.PrepareConstrainedRegions();
        try
        {
          if (flag2)
            tableLock.AcquireWriterLock(int.MaxValue);
          idObj = IdentityHolder.SetIdentity(idObj, (string) null, DuplicateIdentityOption.UseExisting);
          idObj.RaceSetObjRef(objectRef);
        }
        finally
        {
          if (flag2 && tableLock.IsWriterLockHeld)
            tableLock.ReleaseWriterLock();
        }
      }
      return idObj;
    }

    [SecurityCritical]
    private static Identity SetIdentity(Identity idObj, string URI, DuplicateIdentityOption duplicateOption)
    {
      bool flag1 = idObj is ServerIdentity;
      if (idObj.URI == null)
      {
        idObj.SetOrCreateURI(URI);
        if (idObj.ObjectRef != null)
          idObj.ObjectRef.URI = idObj.URI;
      }
      string str = IdentityHolder.MakeURIKey(idObj.URI);
      object obj1 = IdentityHolder.URITable[(object) str];
      if (obj1 != null)
      {
        WeakReference weakReference = obj1 as WeakReference;
        Identity identity;
        bool flag2;
        if (weakReference != null)
        {
          identity = (Identity) weakReference.Target;
          flag2 = identity is ServerIdentity;
        }
        else
        {
          identity = (Identity) obj1;
          flag2 = identity is ServerIdentity;
        }
        if (identity != null && identity != idObj)
        {
          switch (duplicateOption)
          {
            case DuplicateIdentityOption.Unique:
              throw new RemotingException(Environment.GetResourceString("Remoting_URIClash", (object) idObj.URI));
            case DuplicateIdentityOption.UseExisting:
              idObj = identity;
              break;
          }
        }
        else if (weakReference != null)
        {
          if (flag2)
            IdentityHolder.URITable[(object) str] = (object) idObj;
          else
            weakReference.Target = (object) idObj;
        }
      }
      else
      {
        object obj2;
        if (flag1)
        {
          obj2 = (object) idObj;
          ((ServerIdentity) idObj).SetHandle();
        }
        else
          obj2 = (object) new WeakReference((object) idObj);
        IdentityHolder.URITable.Add((object) str, obj2);
        idObj.SetInIDTable();
        ++IdentityHolder.SetIDCount;
        if (IdentityHolder.SetIDCount % 64 == 0)
          IdentityHolder.CleanupIdentities((object) null);
      }
      return idObj;
    }

    [SecurityCritical]
    internal static void RemoveIdentity(string uri)
    {
      IdentityHolder.RemoveIdentity(uri, true);
    }

    [SecurityCritical]
    internal static void RemoveIdentity(string uri, bool bResetURI)
    {
      string str = IdentityHolder.MakeURIKey(uri);
      ReaderWriterLock tableLock = IdentityHolder.TableLock;
      bool flag = !tableLock.IsWriterLockHeld;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
        if (flag)
          tableLock.AcquireWriterLock(int.MaxValue);
        object obj = IdentityHolder.URITable[(object) str];
        WeakReference weakReference = obj as WeakReference;
        Identity identity;
        if (weakReference != null)
        {
          identity = (Identity) weakReference.Target;
          weakReference.Target = (object) null;
        }
        else
        {
          identity = (Identity) obj;
          ((ServerIdentity) identity)?.ResetHandle();
        }
        if (identity == null)
          return;
        IdentityHolder.URITable.Remove((object) str);
        identity.ResetInIDTable(bResetURI);
      }
      finally
      {
        if (flag && tableLock.IsWriterLockHeld)
          tableLock.ReleaseWriterLock();
      }
    }

    [SecurityCritical]
    internal static bool AddDynamicProperty(MarshalByRefObject obj, IDynamicProperty prop)
    {
      if (RemotingServices.IsObjectOutOfContext((object) obj))
        return RemotingServices.GetRealProxy((object) obj).IdentityObject.AddProxySideDynamicProperty(prop);
      ServerIdentity identity = (ServerIdentity) MarshalByRefObject.GetIdentity((MarshalByRefObject) RemotingServices.AlwaysUnwrap((ContextBoundObject) obj));
      if (identity != null)
        return identity.AddServerSideDynamicProperty(prop);
      throw new RemotingException(Environment.GetResourceString("Remoting_NoIdentityEntry"));
    }

    [SecurityCritical]
    internal static bool RemoveDynamicProperty(MarshalByRefObject obj, string name)
    {
      if (RemotingServices.IsObjectOutOfContext((object) obj))
        return RemotingServices.GetRealProxy((object) obj).IdentityObject.RemoveProxySideDynamicProperty(name);
      ServerIdentity identity = (ServerIdentity) MarshalByRefObject.GetIdentity((MarshalByRefObject) RemotingServices.AlwaysUnwrap((ContextBoundObject) obj));
      if (identity != null)
        return identity.RemoveServerSideDynamicProperty(name);
      throw new RemotingException(Environment.GetResourceString("Remoting_NoIdentityEntry"));
    }
  }
}

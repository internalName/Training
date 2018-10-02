// Decompiled with JetBrains decompiler
// Type: System.BaseConfigHandler
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System
{
  internal abstract class BaseConfigHandler
  {
    protected Delegate[] eventCallbacks;

    public BaseConfigHandler()
    {
      this.InitializeCallbacks();
    }

    private void InitializeCallbacks()
    {
      if (this.eventCallbacks != null)
        return;
      this.eventCallbacks = new Delegate[6];
      this.eventCallbacks[0] = (Delegate) new BaseConfigHandler.NotifyEventCallback(this.NotifyEvent);
      this.eventCallbacks[1] = (Delegate) new BaseConfigHandler.BeginChildrenCallback(this.BeginChildren);
      this.eventCallbacks[2] = (Delegate) new BaseConfigHandler.EndChildrenCallback(this.EndChildren);
      this.eventCallbacks[3] = (Delegate) new BaseConfigHandler.ErrorCallback(this.Error);
      this.eventCallbacks[4] = (Delegate) new BaseConfigHandler.CreateNodeCallback(this.CreateNode);
      this.eventCallbacks[5] = (Delegate) new BaseConfigHandler.CreateAttributeCallback(this.CreateAttribute);
    }

    public abstract void NotifyEvent(ConfigEvents nEvent);

    public abstract void BeginChildren(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

    public abstract void EndChildren(int fEmpty, int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

    public abstract void Error(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

    public abstract void CreateNode(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

    public abstract void CreateAttribute(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

    [SecurityCritical]
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern void RunParser(string fileName);

    private delegate void NotifyEventCallback(ConfigEvents nEvent);

    private delegate void BeginChildrenCallback(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

    private delegate void EndChildrenCallback(int fEmpty, int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

    private delegate void ErrorCallback(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

    private delegate void CreateNodeCallback(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);

    private delegate void CreateAttributeCallback(int size, ConfigNodeSubType subType, ConfigNodeType nType, int terminal, [MarshalAs(UnmanagedType.LPWStr)] string text, int textLength, int prefixLength);
  }
}

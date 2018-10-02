// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.StackFrameHelper
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Reflection;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Threading;

namespace System.Diagnostics
{
  [Serializable]
  internal class StackFrameHelper : IDisposable
  {
    [NonSerialized]
    private Thread targetThread;
    private int[] rgiOffset;
    private int[] rgiILOffset;
    private MethodBase[] rgMethodBase;
    private object dynamicMethods;
    [NonSerialized]
    private IntPtr[] rgMethodHandle;
    private string[] rgAssemblyPath;
    private IntPtr[] rgLoadedPeAddress;
    private int[] rgiLoadedPeSize;
    private IntPtr[] rgInMemoryPdbAddress;
    private int[] rgiInMemoryPdbSize;
    private int[] rgiMethodToken;
    private string[] rgFilename;
    private int[] rgiLineNumber;
    private int[] rgiColumnNumber;
    [OptionalField]
    private bool[] rgiLastFrameFromForeignExceptionStackTrace;
    private int iFrameCount;
    private static StackFrameHelper.GetSourceLineInfoDelegate s_getSourceLineInfo;
    [ThreadStatic]
    private static int t_reentrancy;

    public StackFrameHelper(Thread target)
    {
      this.targetThread = target;
      this.rgMethodBase = (MethodBase[]) null;
      this.rgMethodHandle = (IntPtr[]) null;
      this.rgiMethodToken = (int[]) null;
      this.rgiOffset = (int[]) null;
      this.rgiILOffset = (int[]) null;
      this.rgAssemblyPath = (string[]) null;
      this.rgLoadedPeAddress = (IntPtr[]) null;
      this.rgiLoadedPeSize = (int[]) null;
      this.rgInMemoryPdbAddress = (IntPtr[]) null;
      this.rgiInMemoryPdbSize = (int[]) null;
      this.dynamicMethods = (object) null;
      this.rgFilename = (string[]) null;
      this.rgiLineNumber = (int[]) null;
      this.rgiColumnNumber = (int[]) null;
      this.rgiLastFrameFromForeignExceptionStackTrace = (bool[]) null;
      this.iFrameCount = 0;
    }

    [SecuritySafeCritical]
    internal void InitializeSourceInfo(int iSkip, bool fNeedFileInfo, Exception exception)
    {
      StackTrace.GetStackFramesInternal(this, iSkip, fNeedFileInfo, exception);
      if (!fNeedFileInfo || AppContextSwitches.IgnorePortablePDBsInStackTraces || StackFrameHelper.t_reentrancy > 0)
        return;
      ++StackFrameHelper.t_reentrancy;
      try
      {
        if (!CodeAccessSecurityEngine.QuickCheckForAllDemands())
        {
          new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
          new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Assert();
        }
        if (StackFrameHelper.s_getSourceLineInfo == null)
        {
          Type type = Type.GetType("System.Diagnostics.StackTraceSymbols, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089", false);
          if (type == (Type) null)
            return;
          MethodInfo method = type.GetMethod("GetSourceLineInfoWithoutCasAssert", new Type[10]
          {
            typeof (string),
            typeof (IntPtr),
            typeof (int),
            typeof (IntPtr),
            typeof (int),
            typeof (int),
            typeof (int),
            typeof (string).MakeByRefType(),
            typeof (int).MakeByRefType(),
            typeof (int).MakeByRefType()
          });
          if (method == (MethodInfo) null)
            method = type.GetMethod("GetSourceLineInfo", new Type[10]
            {
              typeof (string),
              typeof (IntPtr),
              typeof (int),
              typeof (IntPtr),
              typeof (int),
              typeof (int),
              typeof (int),
              typeof (string).MakeByRefType(),
              typeof (int).MakeByRefType(),
              typeof (int).MakeByRefType()
            });
          if (method == (MethodInfo) null)
            return;
          object instance = Activator.CreateInstance(type);
          StackFrameHelper.GetSourceLineInfoDelegate lineInfoDelegate = (StackFrameHelper.GetSourceLineInfoDelegate) method.CreateDelegate(typeof (StackFrameHelper.GetSourceLineInfoDelegate), instance);
          Interlocked.CompareExchange<StackFrameHelper.GetSourceLineInfoDelegate>(ref StackFrameHelper.s_getSourceLineInfo, lineInfoDelegate, (StackFrameHelper.GetSourceLineInfoDelegate) null);
        }
        for (int index = 0; index < this.iFrameCount; ++index)
        {
          if (this.rgiMethodToken[index] != 0)
            StackFrameHelper.s_getSourceLineInfo(this.rgAssemblyPath[index], this.rgLoadedPeAddress[index], this.rgiLoadedPeSize[index], this.rgInMemoryPdbAddress[index], this.rgiInMemoryPdbSize[index], this.rgiMethodToken[index], this.rgiILOffset[index], out this.rgFilename[index], out this.rgiLineNumber[index], out this.rgiColumnNumber[index]);
        }
      }
      catch
      {
      }
      finally
      {
        --StackFrameHelper.t_reentrancy;
      }
    }

    void IDisposable.Dispose()
    {
    }

    [SecuritySafeCritical]
    public virtual MethodBase GetMethodBase(int i)
    {
      IntPtr methodHandleValue = this.rgMethodHandle[i];
      if (methodHandleValue.IsNull())
        return (MethodBase) null;
      return RuntimeType.GetMethodBase(RuntimeMethodHandle.GetTypicalMethodDefinition((IRuntimeMethodInfo) new RuntimeMethodInfoStub(methodHandleValue, (object) this)));
    }

    public virtual int GetOffset(int i)
    {
      return this.rgiOffset[i];
    }

    public virtual int GetILOffset(int i)
    {
      return this.rgiILOffset[i];
    }

    public virtual string GetFilename(int i)
    {
      if (this.rgFilename != null)
        return this.rgFilename[i];
      return (string) null;
    }

    public virtual int GetLineNumber(int i)
    {
      if (this.rgiLineNumber != null)
        return this.rgiLineNumber[i];
      return 0;
    }

    public virtual int GetColumnNumber(int i)
    {
      if (this.rgiColumnNumber != null)
        return this.rgiColumnNumber[i];
      return 0;
    }

    public virtual bool IsLastFrameFromForeignExceptionStackTrace(int i)
    {
      if (this.rgiLastFrameFromForeignExceptionStackTrace != null)
        return this.rgiLastFrameFromForeignExceptionStackTrace[i];
      return false;
    }

    public virtual int GetNumberOfFrames()
    {
      return this.iFrameCount;
    }

    public virtual void SetNumberOfFrames(int i)
    {
      this.iFrameCount = i;
    }

    [OnSerializing]
    [SecuritySafeCritical]
    private void OnSerializing(StreamingContext context)
    {
      this.rgMethodBase = this.rgMethodHandle == null ? (MethodBase[]) null : new MethodBase[this.rgMethodHandle.Length];
      if (this.rgMethodHandle == null)
        return;
      for (int index = 0; index < this.rgMethodHandle.Length; ++index)
      {
        if (!this.rgMethodHandle[index].IsNull())
          this.rgMethodBase[index] = RuntimeType.GetMethodBase((IRuntimeMethodInfo) new RuntimeMethodInfoStub(this.rgMethodHandle[index], (object) this));
      }
    }

    [OnSerialized]
    private void OnSerialized(StreamingContext context)
    {
      this.rgMethodBase = (MethodBase[]) null;
    }

    [OnDeserialized]
    [SecuritySafeCritical]
    private void OnDeserialized(StreamingContext context)
    {
      this.rgMethodHandle = this.rgMethodBase == null ? (IntPtr[]) null : new IntPtr[this.rgMethodBase.Length];
      if (this.rgMethodBase != null)
      {
        for (int index = 0; index < this.rgMethodBase.Length; ++index)
        {
          if (this.rgMethodBase[index] != (MethodBase) null)
            this.rgMethodHandle[index] = this.rgMethodBase[index].MethodHandle.Value;
        }
      }
      this.rgMethodBase = (MethodBase[]) null;
    }

    private delegate void GetSourceLineInfoDelegate(string assemblyPath, IntPtr loadedPeAddress, int loadedPeSize, IntPtr inMemoryPdbAddress, int inMemoryPdbSize, int methodToken, int ilOffset, out string sourceFile, out int sourceLine, out int sourceColumn);
  }
}

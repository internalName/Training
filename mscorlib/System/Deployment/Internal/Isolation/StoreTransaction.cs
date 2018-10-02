// Decompiled with JetBrains decompiler
// Type: System.Deployment.Internal.Isolation.StoreTransaction
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Deployment.Internal.Isolation
{
  internal class StoreTransaction : IDisposable
  {
    private ArrayList _list = new ArrayList();
    private StoreTransactionOperation[] _storeOps;

    public void Add(StoreOperationInstallDeployment o)
    {
      this._list.Add((object) o);
    }

    public void Add(StoreOperationPinDeployment o)
    {
      this._list.Add((object) o);
    }

    public void Add(StoreOperationSetCanonicalizationContext o)
    {
      this._list.Add((object) o);
    }

    public void Add(StoreOperationSetDeploymentMetadata o)
    {
      this._list.Add((object) o);
    }

    public void Add(StoreOperationStageComponent o)
    {
      this._list.Add((object) o);
    }

    public void Add(StoreOperationStageComponentFile o)
    {
      this._list.Add((object) o);
    }

    public void Add(StoreOperationUninstallDeployment o)
    {
      this._list.Add((object) o);
    }

    public void Add(StoreOperationUnpinDeployment o)
    {
      this._list.Add((object) o);
    }

    public void Add(StoreOperationScavenge o)
    {
      this._list.Add((object) o);
    }

    ~StoreTransaction()
    {
      this.Dispose(false);
    }

    void IDisposable.Dispose()
    {
      this.Dispose(true);
    }

    [SecuritySafeCritical]
    private void Dispose(bool fDisposing)
    {
      if (fDisposing)
        GC.SuppressFinalize((object) this);
      StoreTransactionOperation[] storeOps = this._storeOps;
      this._storeOps = (StoreTransactionOperation[]) null;
      if (storeOps == null)
        return;
      for (int index = 0; index != storeOps.Length; ++index)
      {
        StoreTransactionOperation transactionOperation = storeOps[index];
        if (transactionOperation.Data.DataPtr != IntPtr.Zero)
        {
          switch (transactionOperation.Operation)
          {
            case StoreTransactionOperationType.SetCanonicalizationContext:
              Marshal.DestroyStructure(transactionOperation.Data.DataPtr, typeof (StoreOperationSetCanonicalizationContext));
              break;
            case StoreTransactionOperationType.StageComponent:
              Marshal.DestroyStructure(transactionOperation.Data.DataPtr, typeof (StoreOperationStageComponent));
              break;
            case StoreTransactionOperationType.PinDeployment:
              Marshal.DestroyStructure(transactionOperation.Data.DataPtr, typeof (StoreOperationPinDeployment));
              break;
            case StoreTransactionOperationType.UnpinDeployment:
              Marshal.DestroyStructure(transactionOperation.Data.DataPtr, typeof (StoreOperationUnpinDeployment));
              break;
            case StoreTransactionOperationType.StageComponentFile:
              Marshal.DestroyStructure(transactionOperation.Data.DataPtr, typeof (StoreOperationStageComponentFile));
              break;
            case StoreTransactionOperationType.InstallDeployment:
              Marshal.DestroyStructure(transactionOperation.Data.DataPtr, typeof (StoreOperationInstallDeployment));
              break;
            case StoreTransactionOperationType.UninstallDeployment:
              Marshal.DestroyStructure(transactionOperation.Data.DataPtr, typeof (StoreOperationUninstallDeployment));
              break;
            case StoreTransactionOperationType.SetDeploymentMetadata:
              Marshal.DestroyStructure(transactionOperation.Data.DataPtr, typeof (StoreOperationSetDeploymentMetadata));
              break;
            case StoreTransactionOperationType.Scavenge:
              Marshal.DestroyStructure(transactionOperation.Data.DataPtr, typeof (StoreOperationScavenge));
              break;
          }
          Marshal.FreeCoTaskMem(transactionOperation.Data.DataPtr);
        }
      }
    }

    public StoreTransactionOperation[] Operations
    {
      get
      {
        if (this._storeOps == null)
          this._storeOps = this.GenerateStoreOpsList();
        return this._storeOps;
      }
    }

    [SecuritySafeCritical]
    private StoreTransactionOperation[] GenerateStoreOpsList()
    {
      StoreTransactionOperation[] transactionOperationArray = new StoreTransactionOperation[this._list.Count];
      for (int index = 0; index != this._list.Count; ++index)
      {
        object structure = this._list[index];
        Type type = structure.GetType();
        transactionOperationArray[index].Data.DataPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(structure));
        Marshal.StructureToPtr(structure, transactionOperationArray[index].Data.DataPtr, false);
        if (type == typeof (StoreOperationSetCanonicalizationContext))
          transactionOperationArray[index].Operation = StoreTransactionOperationType.SetCanonicalizationContext;
        else if (type == typeof (StoreOperationStageComponent))
          transactionOperationArray[index].Operation = StoreTransactionOperationType.StageComponent;
        else if (type == typeof (StoreOperationPinDeployment))
          transactionOperationArray[index].Operation = StoreTransactionOperationType.PinDeployment;
        else if (type == typeof (StoreOperationUnpinDeployment))
          transactionOperationArray[index].Operation = StoreTransactionOperationType.UnpinDeployment;
        else if (type == typeof (StoreOperationStageComponentFile))
          transactionOperationArray[index].Operation = StoreTransactionOperationType.StageComponentFile;
        else if (type == typeof (StoreOperationInstallDeployment))
          transactionOperationArray[index].Operation = StoreTransactionOperationType.InstallDeployment;
        else if (type == typeof (StoreOperationUninstallDeployment))
          transactionOperationArray[index].Operation = StoreTransactionOperationType.UninstallDeployment;
        else if (type == typeof (StoreOperationSetDeploymentMetadata))
        {
          transactionOperationArray[index].Operation = StoreTransactionOperationType.SetDeploymentMetadata;
        }
        else
        {
          if (!(type == typeof (StoreOperationScavenge)))
            throw new Exception("How did you get here?");
          transactionOperationArray[index].Operation = StoreTransactionOperationType.Scavenge;
        }
      }
      return transactionOperationArray;
    }
  }
}

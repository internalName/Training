// Decompiled with JetBrains decompiler
// Type: System.Runtime.CompilerServices.AsyncTaskCache
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
  internal static class AsyncTaskCache
  {
    internal static readonly Task<bool> TrueTask = AsyncTaskCache.CreateCacheableTask<bool>(true);
    internal static readonly Task<bool> FalseTask = AsyncTaskCache.CreateCacheableTask<bool>(false);
    internal static readonly Task<int>[] Int32Tasks = AsyncTaskCache.CreateInt32Tasks();
    internal const int INCLUSIVE_INT32_MIN = -1;
    internal const int EXCLUSIVE_INT32_MAX = 9;

    private static Task<int>[] CreateInt32Tasks()
    {
      Task<int>[] taskArray = new Task<int>[10];
      for (int index = 0; index < taskArray.Length; ++index)
        taskArray[index] = AsyncTaskCache.CreateCacheableTask<int>(index - 1);
      return taskArray;
    }

    internal static Task<TResult> CreateCacheableTask<TResult>(TResult result)
    {
      return new Task<TResult>(false, result, (TaskCreationOptions) 16384, new CancellationToken());
    }
  }
}

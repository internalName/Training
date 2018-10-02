// Decompiled with JetBrains decompiler
// Type: System.Reflection.Missing
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System.Reflection
{
  /// <summary>
  ///   Представляет отсутствующий объект <see cref="T:System.Object" />.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [__DynamicallyInvokable]
  [Serializable]
  public sealed class Missing : ISerializable
  {
    /// <summary>
    ///   Представляет единственный экземпляр класса <see cref="T:System.Reflection.Missing" /> класса.
    /// </summary>
    [__DynamicallyInvokable]
    public static readonly Missing Value = new Missing();

    private Missing()
    {
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      if (info == null)
        throw new ArgumentNullException(nameof (info));
      UnitySerializationHolder.GetUnitySerializationInfo(info, this);
    }
  }
}

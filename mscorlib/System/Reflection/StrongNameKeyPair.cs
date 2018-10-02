// Decompiled with JetBrains decompiler
// Type: System.Reflection.StrongNameKeyPair
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Runtime.Hosting;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Reflection
{
  /// <summary>
  ///   Инкапсулирует доступ к паре открытого или закрытого ключа, используется для подписи сборки строгим именем.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class StrongNameKeyPair : IDeserializationCallback, ISerializable
  {
    private bool _keyPairExported;
    private byte[] _keyPairArray;
    private string _keyPairContainer;
    private byte[] _publicKey;

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.StrongNameKeyPair" /> класс, который строит пару ключей из <see langword="FileStream" />.
    /// </summary>
    /// <param name="keyPairFile">
    ///   A <see langword="FileStream" /> содержащего пару ключей.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="keyPairFile" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public StrongNameKeyPair(FileStream keyPairFile)
    {
      if (keyPairFile == null)
        throw new ArgumentNullException(nameof (keyPairFile));
      int length = (int) keyPairFile.Length;
      this._keyPairArray = new byte[length];
      keyPairFile.Read(this._keyPairArray, 0, length);
      this._keyPairExported = true;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.StrongNameKeyPair" /> класс, который строит пару ключей из <see langword="byte" /> массива.
    /// </summary>
    /// <param name="keyPairArray">
    ///   Массив объектов типа <see langword="byte" /> содержащего пару ключей.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="keyPairArray" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public StrongNameKeyPair(byte[] keyPairArray)
    {
      if (keyPairArray == null)
        throw new ArgumentNullException(nameof (keyPairArray));
      this._keyPairArray = new byte[keyPairArray.Length];
      Array.Copy((Array) keyPairArray, (Array) this._keyPairArray, keyPairArray.Length);
      this._keyPairExported = true;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.StrongNameKeyPair" /> класс, который строит пару ключей из <see langword="String" />.
    /// </summary>
    /// <param name="keyPairContainer">
    ///   Строка, содержащая пару ключей.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="keyPairContainer" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего объекта отсутствует необходимое разрешение.
    /// </exception>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    public StrongNameKeyPair(string keyPairContainer)
    {
      if (keyPairContainer == null)
        throw new ArgumentNullException(nameof (keyPairContainer));
      this._keyPairContainer = keyPairContainer;
      this._keyPairExported = false;
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Reflection.StrongNameKeyPair" /> класс, который строит пару ключей из сериализованных данных.
    /// </summary>
    /// <param name="info">
    ///   Объект <see cref="T:System.Runtime.Serialization.SerializationInfo" /> объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Объект <see cref="T:System.Runtime.Serialization.StreamingContext" /> содержащий контекстные сведения об источнике или назначении.
    /// </param>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
    protected StrongNameKeyPair(SerializationInfo info, StreamingContext context)
    {
      this._keyPairExported = (bool) info.GetValue(nameof (_keyPairExported), typeof (bool));
      this._keyPairArray = (byte[]) info.GetValue(nameof (_keyPairArray), typeof (byte[]));
      this._keyPairContainer = (string) info.GetValue(nameof (_keyPairContainer), typeof (string));
      this._publicKey = (byte[]) info.GetValue(nameof (_publicKey), typeof (byte[]));
    }

    /// <summary>
    ///   Возвращает открытую часть открытого ключа или открытый ключ маркера пару ключей.
    /// </summary>
    /// <returns>
    ///   Массив объектов типа <see langword="byte" /> содержащий открытый ключ или маркер открытого ключа из пары ключей.
    /// </returns>
    public byte[] PublicKey
    {
      [SecuritySafeCritical] get
      {
        if (this._publicKey == null)
          this._publicKey = this.ComputePublicKey();
        byte[] numArray = new byte[this._publicKey.Length];
        Array.Copy((Array) this._publicKey, (Array) numArray, this._publicKey.Length);
        return numArray;
      }
    }

    [SecurityCritical]
    private unsafe byte[] ComputePublicKey()
    {
      byte[] dest = (byte[]) null;
      RuntimeHelpers.PrepareConstrainedRegions();
      try
      {
      }
      finally
      {
        IntPtr ppbPublicKeyBlob = IntPtr.Zero;
        int pcbPublicKeyBlob = 0;
        try
        {
          if (!(!this._keyPairExported ? StrongNameHelpers.StrongNameGetPublicKey(this._keyPairContainer, (byte[]) null, 0, out ppbPublicKeyBlob, out pcbPublicKeyBlob) : StrongNameHelpers.StrongNameGetPublicKey((string) null, this._keyPairArray, this._keyPairArray.Length, out ppbPublicKeyBlob, out pcbPublicKeyBlob)))
            throw new ArgumentException(Environment.GetResourceString("Argument_StrongNameGetPublicKey"));
          dest = new byte[pcbPublicKeyBlob];
          Buffer.Memcpy(dest, 0, (byte*) ppbPublicKeyBlob.ToPointer(), 0, pcbPublicKeyBlob);
        }
        finally
        {
          if (ppbPublicKeyBlob != IntPtr.Zero)
            StrongNameHelpers.StrongNameFreeBuffer(ppbPublicKeyBlob);
        }
      }
      return dest;
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue("_keyPairExported", this._keyPairExported);
      info.AddValue("_keyPairArray", (object) this._keyPairArray);
      info.AddValue("_keyPairContainer", (object) this._keyPairContainer);
      info.AddValue("_publicKey", (object) this._publicKey);
    }

    void IDeserializationCallback.OnDeserialization(object sender)
    {
    }

    private bool GetKeyPair(out object arrayOrContainer)
    {
      arrayOrContainer = this._keyPairExported ? (object) this._keyPairArray : (object) this._keyPairContainer;
      return this._keyPairExported;
    }
  }
}

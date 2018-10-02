// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.Hash
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Util;

namespace System.Security.Policy
{
  /// <summary>
  ///   Предоставляет свидетельство относительно хэш-значение для сборки.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class Hash : EvidenceBase, ISerializable
  {
    private RuntimeAssembly m_assembly;
    private Dictionary<Type, byte[]> m_hashes;
    private WeakReference m_rawData;

    [SecurityCritical]
    internal Hash(SerializationInfo info, StreamingContext context)
    {
      Dictionary<Type, byte[]> valueNoThrow1 = info.GetValueNoThrow("Hashes", typeof (Dictionary<Type, byte[]>)) as Dictionary<Type, byte[]>;
      if (valueNoThrow1 != null)
      {
        this.m_hashes = valueNoThrow1;
      }
      else
      {
        this.m_hashes = new Dictionary<Type, byte[]>();
        byte[] valueNoThrow2 = info.GetValueNoThrow("Md5", typeof (byte[])) as byte[];
        if (valueNoThrow2 != null)
          this.m_hashes[typeof (System.Security.Cryptography.MD5)] = valueNoThrow2;
        byte[] valueNoThrow3 = info.GetValueNoThrow("Sha1", typeof (byte[])) as byte[];
        if (valueNoThrow3 != null)
          this.m_hashes[typeof (System.Security.Cryptography.SHA1)] = valueNoThrow3;
        byte[] valueNoThrow4 = info.GetValueNoThrow("RawData", typeof (byte[])) as byte[];
        if (valueNoThrow4 == null)
          return;
        this.GenerateDefaultHashes(valueNoThrow4);
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Policy.Hash" />.
    /// </summary>
    /// <param name="assembly">
    ///   Сборка, для которых вычисляется хэш-значение.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="assembly" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Не удается создать хэш-код.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="assembly" /> время выполнения не является <see cref="T:System.Reflection.Assembly" /> объект.
    /// </exception>
    public Hash(Assembly assembly)
    {
      if (assembly == (Assembly) null)
        throw new ArgumentNullException(nameof (assembly));
      if (assembly.IsDynamic)
        throw new ArgumentException(Environment.GetResourceString("Security_CannotGenerateHash"), nameof (assembly));
      this.m_hashes = new Dictionary<Type, byte[]>();
      this.m_assembly = assembly as RuntimeAssembly;
      if ((Assembly) this.m_assembly == (Assembly) null)
        throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"), nameof (assembly));
    }

    private Hash(Hash hash)
    {
      this.m_assembly = hash.m_assembly;
      this.m_rawData = hash.m_rawData;
      this.m_hashes = new Dictionary<Type, byte[]>((IDictionary<Type, byte[]>) hash.m_hashes);
    }

    private Hash(Type hashType, byte[] hashValue)
    {
      this.m_hashes = new Dictionary<Type, byte[]>();
      byte[] numArray = new byte[hashValue.Length];
      Array.Copy((Array) hashValue, (Array) numArray, numArray.Length);
      this.m_hashes[hashType] = hashValue;
    }

    /// <summary>
    ///   Создает <see cref="T:System.Security.Policy.Hash" /> содержащий <see cref="T:System.Security.Cryptography.SHA1" /> значение хэша.
    /// </summary>
    /// <param name="sha1">
    ///   Массив байтов, содержащий <see cref="T:System.Security.Cryptography.SHA1" /> значение хэша.
    /// </param>
    /// <returns>
    ///   Объект, содержащий хэш-значение, предоставляемое <paramref name="sha1" /> параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="sha1" /> имеет значение <see langword="null" />.
    /// </exception>
    public static Hash CreateSHA1(byte[] sha1)
    {
      if (sha1 == null)
        throw new ArgumentNullException(nameof (sha1));
      return new Hash(typeof (System.Security.Cryptography.SHA1), sha1);
    }

    /// <summary>
    ///   Создает <see cref="T:System.Security.Policy.Hash" /> содержащий <see cref="T:System.Security.Cryptography.SHA256" /> значение хэша.
    /// </summary>
    /// <param name="sha256">
    ///   Массив байтов, содержащий <see cref="T:System.Security.Cryptography.SHA256" /> значение хэша.
    /// </param>
    /// <returns>
    ///   Хэш-объект, содержащий хэш-значение, предоставляемое <paramref name="sha256" /> параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="sha256" /> имеет значение <see langword="null" />.
    /// </exception>
    public static Hash CreateSHA256(byte[] sha256)
    {
      if (sha256 == null)
        throw new ArgumentNullException(nameof (sha256));
      return new Hash(typeof (System.Security.Cryptography.SHA256), sha256);
    }

    /// <summary>
    ///   Создает <see cref="T:System.Security.Policy.Hash" /> содержащий <see cref="T:System.Security.Cryptography.MD5" /> значение хэша.
    /// </summary>
    /// <param name="md5">
    ///   Массив байтов, содержащий <see cref="T:System.Security.Cryptography.MD5" /> значение хэша.
    /// </param>
    /// <returns>
    ///   Объект, содержащий хэш-значение, предоставляемое <paramref name="md5" /> параметр.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="md5" /> имеет значение <see langword="null" />.
    /// </exception>
    public static Hash CreateMD5(byte[] md5)
    {
      if (md5 == null)
        throw new ArgumentNullException(nameof (md5));
      return new Hash(typeof (System.Security.Cryptography.MD5), md5);
    }

    /// <summary>
    ///   Создает новый объект, являющийся копией текущего экземпляра.
    /// </summary>
    /// <returns>Новый объект, являющийся копией этого экземпляра.</returns>
    public override EvidenceBase Clone()
    {
      return (EvidenceBase) new Hash(this);
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      this.GenerateDefaultHashes();
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Runtime.Serialization.SerializationInfo" /> объект с именем параметра и дополнительными сведениями об исключении.
    /// </summary>
    /// <param name="info">
    ///   Объект, содержащий сериализованные данные объекта.
    /// </param>
    /// <param name="context">
    ///   Контекстные сведения об источнике или назначении.
    /// </param>
    [SecurityCritical]
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      this.GenerateDefaultHashes();
      byte[] numArray1;
      if (this.m_hashes.TryGetValue(typeof (System.Security.Cryptography.MD5), out numArray1))
        info.AddValue("Md5", (object) numArray1);
      byte[] numArray2;
      if (this.m_hashes.TryGetValue(typeof (System.Security.Cryptography.SHA1), out numArray2))
        info.AddValue("Sha1", (object) numArray2);
      info.AddValue("RawData", (object) null);
      info.AddValue("PEFile", (object) IntPtr.Zero);
      info.AddValue("Hashes", (object) this.m_hashes);
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Security.Cryptography.SHA1" /> хэш-значение для сборки.
    /// </summary>
    /// <returns>
    ///   Массив байтов, представляющий <see cref="T:System.Security.Cryptography.SHA1" /> хэш-значение для сборки.
    /// </returns>
    public byte[] SHA1
    {
      get
      {
        byte[] numArray1 = (byte[]) null;
        if (!this.m_hashes.TryGetValue(typeof (System.Security.Cryptography.SHA1), out numArray1))
          numArray1 = this.GenerateHash(Hash.GetDefaultHashImplementationOrFallback(typeof (System.Security.Cryptography.SHA1), typeof (System.Security.Cryptography.SHA1)));
        byte[] numArray2 = new byte[numArray1.Length];
        Array.Copy((Array) numArray1, (Array) numArray2, numArray2.Length);
        return numArray2;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Security.Cryptography.SHA256" /> хэш-значение для сборки.
    /// </summary>
    /// <returns>
    ///   Массив байтов, представляющий <see cref="T:System.Security.Cryptography.SHA256" /> хэш-значение для сборки.
    /// </returns>
    public byte[] SHA256
    {
      get
      {
        byte[] numArray1 = (byte[]) null;
        if (!this.m_hashes.TryGetValue(typeof (System.Security.Cryptography.SHA256), out numArray1))
          numArray1 = this.GenerateHash(Hash.GetDefaultHashImplementationOrFallback(typeof (System.Security.Cryptography.SHA256), typeof (System.Security.Cryptography.SHA256)));
        byte[] numArray2 = new byte[numArray1.Length];
        Array.Copy((Array) numArray1, (Array) numArray2, numArray2.Length);
        return numArray2;
      }
    }

    /// <summary>
    ///   Возвращает <see cref="T:System.Security.Cryptography.MD5" /> хэш-значение для сборки.
    /// </summary>
    /// <returns>
    ///   Массив байтов, представляющий <see cref="T:System.Security.Cryptography.MD5" /> хэш-значение для сборки.
    /// </returns>
    public byte[] MD5
    {
      get
      {
        byte[] numArray1 = (byte[]) null;
        if (!this.m_hashes.TryGetValue(typeof (System.Security.Cryptography.MD5), out numArray1))
          numArray1 = this.GenerateHash(Hash.GetDefaultHashImplementationOrFallback(typeof (System.Security.Cryptography.MD5), typeof (System.Security.Cryptography.MD5)));
        byte[] numArray2 = new byte[numArray1.Length];
        Array.Copy((Array) numArray1, (Array) numArray2, numArray2.Length);
        return numArray2;
      }
    }

    /// <summary>
    ///   Вычисляет хэш-значение для сборки с помощью указанного хэш-алгоритма.
    /// </summary>
    /// <param name="hashAlg">
    ///   Хэш-алгоритм для вычисления хэш-значение для сборки.
    /// </param>
    /// <returns>
    ///   Массив байтов, представляющий хэш-значение для сборки.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="hashAlg" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Не удается создать хэш-значение для сборки.
    /// </exception>
    public byte[] GenerateHash(HashAlgorithm hashAlg)
    {
      if (hashAlg == null)
        throw new ArgumentNullException(nameof (hashAlg));
      byte[] hash = this.GenerateHash(hashAlg.GetType());
      byte[] numArray = new byte[hash.Length];
      Array.Copy((Array) hash, (Array) numArray, numArray.Length);
      return numArray;
    }

    private byte[] GenerateHash(Type hashType)
    {
      Type hashIndexType = Hash.GetHashIndexType(hashType);
      byte[] numArray = (byte[]) null;
      if (!this.m_hashes.TryGetValue(hashIndexType, out numArray))
      {
        if ((Assembly) this.m_assembly == (Assembly) null)
          throw new InvalidOperationException(Environment.GetResourceString("Security_CannotGenerateHash"));
        numArray = Hash.GenerateHash(hashType, this.GetRawData());
        this.m_hashes[hashIndexType] = numArray;
      }
      return numArray;
    }

    private static byte[] GenerateHash(Type hashType, byte[] assemblyBytes)
    {
      using (HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashType.FullName))
        return hashAlgorithm.ComputeHash(assemblyBytes);
    }

    private void GenerateDefaultHashes()
    {
      if (!((Assembly) this.m_assembly != (Assembly) null))
        return;
      this.GenerateDefaultHashes(this.GetRawData());
    }

    private void GenerateDefaultHashes(byte[] assemblyBytes)
    {
      Type[] typeArray = new Type[3]
      {
        Hash.GetHashIndexType(typeof (System.Security.Cryptography.SHA1)),
        Hash.GetHashIndexType(typeof (System.Security.Cryptography.SHA256)),
        Hash.GetHashIndexType(typeof (System.Security.Cryptography.MD5))
      };
      foreach (Type index in typeArray)
      {
        Type hashImplementation = Hash.GetDefaultHashImplementation(index);
        if (hashImplementation != (Type) null && !this.m_hashes.ContainsKey(index))
          this.m_hashes[index] = Hash.GenerateHash(hashImplementation, assemblyBytes);
      }
    }

    private static Type GetDefaultHashImplementationOrFallback(Type hashAlgorithm, Type fallbackImplementation)
    {
      Type hashImplementation = Hash.GetDefaultHashImplementation(hashAlgorithm);
      if (!(hashImplementation != (Type) null))
        return fallbackImplementation;
      return hashImplementation;
    }

    private static Type GetDefaultHashImplementation(Type hashAlgorithm)
    {
      if (hashAlgorithm.IsAssignableFrom(typeof (System.Security.Cryptography.MD5)))
      {
        if (!CryptoConfig.AllowOnlyFipsAlgorithms)
          return typeof (MD5CryptoServiceProvider);
        return (Type) null;
      }
      if (hashAlgorithm.IsAssignableFrom(typeof (System.Security.Cryptography.SHA256)))
        return Type.GetType("System.Security.Cryptography.SHA256CryptoServiceProvider, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
      return hashAlgorithm;
    }

    private static Type GetHashIndexType(Type hashType)
    {
      Type type = hashType;
      while (type != (Type) null && type.BaseType != typeof (HashAlgorithm))
        type = type.BaseType;
      if (type == (Type) null)
        type = typeof (HashAlgorithm);
      return type;
    }

    private byte[] GetRawData()
    {
      byte[] numArray = (byte[]) null;
      if ((Assembly) this.m_assembly != (Assembly) null)
      {
        if (this.m_rawData != null)
          numArray = this.m_rawData.Target as byte[];
        if (numArray == null)
        {
          numArray = this.m_assembly.GetRawBytes();
          this.m_rawData = new WeakReference((object) numArray);
        }
      }
      return numArray;
    }

    private SecurityElement ToXml()
    {
      this.GenerateDefaultHashes();
      SecurityElement securityElement = new SecurityElement("System.Security.Policy.Hash");
      securityElement.AddAttribute("version", "2");
      foreach (KeyValuePair<Type, byte[]> hash in this.m_hashes)
      {
        SecurityElement child = new SecurityElement("hash");
        child.AddAttribute("algorithm", hash.Key.Name);
        child.AddAttribute("value", Hex.EncodeHexString(hash.Value));
        securityElement.AddChild(child);
      }
      return securityElement;
    }

    /// <summary>
    ///   Возвращает строковое представление текущего объекта <see cref="T:System.Security.Policy.Hash" />.
    /// </summary>
    /// <returns>
    ///   Представление текущего <see cref="T:System.Security.Policy.Hash" />.
    /// </returns>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.HashMembershipCondition
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Util;
using System.Threading;

namespace System.Security.Policy
{
  /// <summary>
  ///   Определяет принадлежность сборки к группе кода путем проверки ее хэш-значения.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class HashMembershipCondition : ISerializable, IDeserializationCallback, IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IReportMatchMembershipCondition
  {
    private byte[] m_value;
    private HashAlgorithm m_hashAlg;
    private SecurityElement m_element;
    private object s_InternalSyncObject;
    private const string s_tagHashValue = "HashValue";
    private const string s_tagHashAlgorithm = "HashAlgorithm";

    private object InternalSyncObject
    {
      get
      {
        if (this.s_InternalSyncObject == null)
          Interlocked.CompareExchange(ref this.s_InternalSyncObject, new object(), (object) null);
        return this.s_InternalSyncObject;
      }
    }

    internal HashMembershipCondition()
    {
    }

    private HashMembershipCondition(SerializationInfo info, StreamingContext context)
    {
      this.m_value = (byte[]) info.GetValue(nameof (HashValue), typeof (byte[]));
      string hashName = (string) info.GetValue(nameof (HashAlgorithm), typeof (string));
      if (hashName != null)
        this.m_hashAlg = HashAlgorithm.Create(hashName);
      else
        this.m_hashAlg = (HashAlgorithm) new SHA1Managed();
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Policy.HashMembershipCondition" /> с хэш-алгоритм и хэш-значение, определяющими членство.
    /// </summary>
    /// <param name="hashAlg">
    ///   Хэш-алгоритм для вычисления хэш-значение для сборки.
    /// </param>
    /// <param name="value">
    ///   Хэш-значение, используемое при проверке.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="hashAlg" /> имеет значение <see langword="null" />.
    /// 
    ///   -или-
    /// 
    ///   Параметр <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="hashAlg" /> Параметр не является допустимым хэш-алгоритмом.
    /// </exception>
    public HashMembershipCondition(HashAlgorithm hashAlg, byte[] value)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (hashAlg == null)
        throw new ArgumentNullException(nameof (hashAlg));
      this.m_value = new byte[value.Length];
      Array.Copy((Array) value, (Array) this.m_value, value.Length);
      this.m_hashAlg = hashAlg;
    }

    [SecurityCritical]
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue("HashValue", (object) this.HashValue);
      info.AddValue("HashAlgorithm", (object) this.HashAlgorithm.ToString());
    }

    void IDeserializationCallback.OnDeserialization(object sender)
    {
    }

    /// <summary>
    ///   Возвращает или задает хэш-алгоритм, который используется для условия членства.
    /// </summary>
    /// <returns>
    ///   Хэш-алгоритм, который используется для условия членства.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Попытка установить <see cref="P:System.Security.Policy.HashMembershipCondition.HashAlgorithm" /> для <see langword="null" />.
    /// </exception>
    public HashAlgorithm HashAlgorithm
    {
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (HashAlgorithm));
        this.m_hashAlg = value;
      }
      get
      {
        if (this.m_hashAlg == null && this.m_element != null)
          this.ParseHashAlgorithm();
        return this.m_hashAlg;
      }
    }

    /// <summary>
    ///   Возвращает или задает хэш-значение, что условия членства.
    /// </summary>
    /// <returns>Хэш-значение, условия членства.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Попытка установить <see cref="P:System.Security.Policy.HashMembershipCondition.HashValue" /> для <see langword="null" />.
    /// </exception>
    public byte[] HashValue
    {
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        this.m_value = new byte[value.Length];
        Array.Copy((Array) value, (Array) this.m_value, value.Length);
      }
      get
      {
        if (this.m_value == null && this.m_element != null)
          this.ParseHashValue();
        if (this.m_value == null)
          return (byte[]) null;
        byte[] numArray = new byte[this.m_value.Length];
        Array.Copy((Array) this.m_value, (Array) numArray, this.m_value.Length);
        return numArray;
      }
    }

    /// <summary>
    ///   Определяет, удовлетворяет ли указанное свидетельство условию членства.
    /// </summary>
    /// <param name="evidence">
    ///   Набор свидетельств, для которого производится проверка.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если указанное свидетельство удовлетворяет условию членства; в противном случае — <see langword="false" />.
    /// </returns>
    public bool Check(Evidence evidence)
    {
      object usedEvidence = (object) null;
      return ((IReportMatchMembershipCondition) this).Check(evidence, out usedEvidence);
    }

    bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
    {
      usedEvidence = (object) null;
      if (evidence == null)
        return false;
      Hash hostEvidence = evidence.GetHostEvidence<Hash>();
      if (hostEvidence != null)
      {
        if (this.m_value == null && this.m_element != null)
          this.ParseHashValue();
        if (this.m_hashAlg == null && this.m_element != null)
          this.ParseHashAlgorithm();
        byte[] first = (byte[]) null;
        lock (this.InternalSyncObject)
          first = hostEvidence.GenerateHash(this.m_hashAlg);
        if (first != null && HashMembershipCondition.CompareArrays(first, this.m_value))
        {
          usedEvidence = (object) hostEvidence;
          return true;
        }
      }
      return false;
    }

    /// <summary>Создает эквивалентную копию условия членства.</summary>
    /// <returns>Новая, идентичная копия текущего условия членства.</returns>
    public IMembershipCondition Copy()
    {
      if (this.m_value == null && this.m_element != null)
        this.ParseHashValue();
      if (this.m_hashAlg == null && this.m_element != null)
        this.ParseHashAlgorithm();
      return (IMembershipCondition) new HashMembershipCondition(this.m_hashAlg, this.m_value);
    }

    /// <summary>
    ///   Создает кодировку XML для объекта безопасности и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Кодировка XML объекта безопасности, включающая сведения о состоянии.
    /// </returns>
    public SecurityElement ToXml()
    {
      return this.ToXml((PolicyLevel) null);
    }

    /// <summary>
    ///   Восстанавливает объект безопасности с указанным состоянием из кодировки XML.
    /// </summary>
    /// <param name="e">
    ///   Кодировка XML, используемая для восстановления объекта безопасности.
    /// </param>
    public void FromXml(SecurityElement e)
    {
      this.FromXml(e, (PolicyLevel) null);
    }

    /// <summary>
    ///   Создает кодировку XML для объекта безопасности и его текущего состояния с указанным <see cref="T:System.Security.Policy.PolicyLevel" />.
    /// </summary>
    /// <param name="level">
    ///   Контекст уровня политики для разрешения именованного разрешение ссылок на наборы.
    /// </param>
    /// <returns>
    ///   Кодировка XML объекта безопасности, включающая сведения о состоянии.
    /// </returns>
    public SecurityElement ToXml(PolicyLevel level)
    {
      if (this.m_value == null && this.m_element != null)
        this.ParseHashValue();
      if (this.m_hashAlg == null && this.m_element != null)
        this.ParseHashAlgorithm();
      SecurityElement element = new SecurityElement("IMembershipCondition");
      XMLUtil.AddClassAttribute(element, this.GetType(), "System.Security.Policy.HashMembershipCondition");
      element.AddAttribute("version", "1");
      if (this.m_value != null)
        element.AddAttribute("HashValue", Hex.EncodeHexString(this.HashValue));
      if (this.m_hashAlg != null)
        element.AddAttribute("HashAlgorithm", this.HashAlgorithm.GetType().FullName);
      return element;
    }

    /// <summary>
    ///   Восстанавливает объект безопасности с указанным состоянием из кодировки XML.
    /// </summary>
    /// <param name="e">
    ///   Кодировка XML, используемая для восстановления объекта безопасности.
    /// </param>
    /// <param name="level">
    ///   Контекст уровня политики, используемый для разрешения именованные наборы разрешений.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="e" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="e" /> Параметр не недопустимый элемент условия членства.
    /// </exception>
    public void FromXml(SecurityElement e, PolicyLevel level)
    {
      if (e == null)
        throw new ArgumentNullException(nameof (e));
      if (!e.Tag.Equals("IMembershipCondition"))
        throw new ArgumentException(Environment.GetResourceString("Argument_MembershipConditionElement"));
      lock (this.InternalSyncObject)
      {
        this.m_element = e;
        this.m_value = (byte[]) null;
        this.m_hashAlg = (HashAlgorithm) null;
      }
    }

    /// <summary>
    ///   Определяет, является ли <see cref="P:System.Security.Policy.HashMembershipCondition.HashValue" /> и <see cref="P:System.Security.Policy.HashMembershipCondition.HashAlgorithm" /> из указанного объекта эквивалентны <see cref="P:System.Security.Policy.HashMembershipCondition.HashValue" /> и <see cref="P:System.Security.Policy.HashMembershipCondition.HashAlgorithm" /> содержится в текущем <see cref="T:System.Security.Policy.HashMembershipCondition" />.
    /// </summary>
    /// <param name="o">
    ///   Объект, сравниваемый с текущим <see cref="T:System.Security.Policy.HashMembershipCondition" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <see cref="P:System.Security.Policy.HashMembershipCondition.HashValue" /> и <see cref="P:System.Security.Policy.HashMembershipCondition.HashAlgorithm" /> из указанного объекта эквивалентно <see cref="P:System.Security.Policy.HashMembershipCondition.HashValue" /> и <see cref="P:System.Security.Policy.HashMembershipCondition.HashAlgorithm" /> содержится в текущем <see cref="T:System.Security.Policy.HashMembershipCondition" />; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object o)
    {
      HashMembershipCondition membershipCondition = o as HashMembershipCondition;
      if (membershipCondition != null)
      {
        if (this.m_hashAlg == null && this.m_element != null)
          this.ParseHashAlgorithm();
        if (membershipCondition.m_hashAlg == null && membershipCondition.m_element != null)
          membershipCondition.ParseHashAlgorithm();
        if (this.m_hashAlg != null && membershipCondition.m_hashAlg != null && this.m_hashAlg.GetType() == membershipCondition.m_hashAlg.GetType())
        {
          if (this.m_value == null && this.m_element != null)
            this.ParseHashValue();
          if (membershipCondition.m_value == null && membershipCondition.m_element != null)
            membershipCondition.ParseHashValue();
          if (this.m_value.Length != membershipCondition.m_value.Length)
            return false;
          for (int index = 0; index < this.m_value.Length; ++index)
          {
            if ((int) this.m_value[index] != (int) membershipCondition.m_value[index])
              return false;
          }
          return true;
        }
      }
      return false;
    }

    /// <summary>Возвращает хэш-код для текущего условия членства.</summary>
    /// <returns>Хэш-код для текущего условия членства.</returns>
    public override int GetHashCode()
    {
      if (this.m_hashAlg == null && this.m_element != null)
        this.ParseHashAlgorithm();
      int num = this.m_hashAlg != null ? this.m_hashAlg.GetType().GetHashCode() : 0;
      if (this.m_value == null && this.m_element != null)
        this.ParseHashValue();
      return num ^ HashMembershipCondition.GetByteArrayHashCode(this.m_value);
    }

    /// <summary>
    ///   Создает и возвращает строковое представление условия членства.
    /// </summary>
    /// <returns>Строковое представление состояния условия членства.</returns>
    public override string ToString()
    {
      if (this.m_hashAlg == null)
        this.ParseHashAlgorithm();
      return Environment.GetResourceString("Hash_ToString", (object) this.m_hashAlg.GetType().AssemblyQualifiedName, (object) Hex.EncodeHexString(this.HashValue));
    }

    private void ParseHashValue()
    {
      lock (this.InternalSyncObject)
      {
        if (this.m_element == null)
          return;
        string hexString = this.m_element.Attribute("HashValue");
        if (hexString != null)
        {
          this.m_value = Hex.DecodeHexString(hexString);
          if (this.m_value == null || this.m_hashAlg == null)
            return;
          this.m_element = (SecurityElement) null;
        }
        else
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXMLElement", (object) "HashValue", (object) this.GetType().FullName));
      }
    }

    private void ParseHashAlgorithm()
    {
      lock (this.InternalSyncObject)
      {
        if (this.m_element == null)
          return;
        string hashName = this.m_element.Attribute("HashAlgorithm");
        this.m_hashAlg = hashName == null ? (HashAlgorithm) new SHA1Managed() : HashAlgorithm.Create(hashName);
        if (this.m_value == null || this.m_hashAlg == null)
          return;
        this.m_element = (SecurityElement) null;
      }
    }

    private static bool CompareArrays(byte[] first, byte[] second)
    {
      if (first.Length != second.Length)
        return false;
      int length = first.Length;
      for (int index = 0; index < length; ++index)
      {
        if ((int) first[index] != (int) second[index])
          return false;
      }
      return true;
    }

    private static int GetByteArrayHashCode(byte[] baData)
    {
      if (baData == null)
        return 0;
      int num = 0;
      for (int index = 0; index < baData.Length; ++index)
        num = num << 8 ^ (int) baData[index] ^ num >> 24;
      return num;
    }
  }
}

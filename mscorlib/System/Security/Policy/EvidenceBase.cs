// Decompiled with JetBrains decompiler
// Type: System.Security.Policy.EvidenceBase
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;

namespace System.Security.Policy
{
  /// <summary>
  ///   Предоставляет базовый класс, от всех объектов для использования в качестве свидетельства должны наследоваться.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
  public abstract class EvidenceBase
  {
    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Policy.EvidenceBase" />.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Объект для использования в качестве свидетельства не является сериализуемым.
    /// </exception>
    protected EvidenceBase()
    {
      if (!this.GetType().IsSerializable)
        throw new InvalidOperationException(Environment.GetResourceString("Policy_EvidenceMustBeSerializable"));
    }

    /// <summary>
    ///   Создает новый объект, который представляет полную копию текущего экземпляра.
    /// </summary>
    /// <returns>Резервную копию данного объекта свидетельства.</returns>
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
    [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
    public virtual EvidenceBase Clone()
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize((Stream) memoryStream, (object) this);
        memoryStream.Position = 0L;
        return binaryFormatter.Deserialize((Stream) memoryStream) as EvidenceBase;
      }
    }
  }
}

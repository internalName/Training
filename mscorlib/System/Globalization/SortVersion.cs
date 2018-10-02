// Decompiled with JetBrains decompiler
// Type: System.Globalization.SortVersion
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Globalization
{
  /// <summary>
  ///   Предоставляет сведения о версии Юникода, используемой для сравнения и сортировки строк.
  /// </summary>
  [Serializable]
  public sealed class SortVersion : IEquatable<SortVersion>
  {
    private int m_NlsVersion;
    private Guid m_SortId;

    /// <summary>
    ///   Получает полный номер версии <see cref="T:System.Globalization.SortVersion" /> объекта.
    /// </summary>
    /// <returns>
    ///   Номер версии данного объекта <see cref="T:System.Globalization.SortVersion" /> объекта.
    /// </returns>
    public int FullVersion
    {
      get
      {
        return this.m_NlsVersion;
      }
    }

    /// <summary>
    ///   Возвращает глобальный уникальный идентификатор для этого <see cref="T:System.Globalization.SortVersion" /> объекта.
    /// </summary>
    /// <returns>
    ///   Глобальный уникальный идентификатор для этого <see cref="T:System.Globalization.SortVersion" /> объекта.
    /// </returns>
    public Guid SortId
    {
      get
      {
        return this.m_SortId;
      }
    }

    /// <summary>
    ///   Создает новый экземпляр класса <see cref="T:System.Globalization.SortVersion" />.
    /// </summary>
    /// <param name="fullVersion">Номер версии.</param>
    /// <param name="sortId">Идентификатор сортировки.</param>
    public SortVersion(int fullVersion, Guid sortId)
    {
      this.m_SortId = sortId;
      this.m_NlsVersion = fullVersion;
    }

    internal SortVersion(int nlsVersion, int effectiveId, Guid customVersion)
    {
      this.m_NlsVersion = nlsVersion;
      if (customVersion == Guid.Empty)
      {
        BitConverter.GetBytes(effectiveId);
        customVersion = new Guid(0, (short) 0, (short) 0, (byte) 0, (byte) 0, (byte) 0, (byte) 0, (byte) ((uint) effectiveId >> 24), (byte) ((effectiveId & 16711680) >> 16), (byte) ((effectiveId & 65280) >> 8), (byte) (effectiveId & (int) byte.MaxValue));
      }
      this.m_SortId = customVersion;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли это <see cref="T:System.Globalization.SortVersion" /> экземпляр равен заданному объекту.
    /// </summary>
    /// <param name="obj">
    ///   Объект для сравнения с данным экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="obj" /> является <see cref="T:System.Globalization.SortVersion" /> представляющий ту же версию, что и данный экземпляр; в противном случае — <see langword="false" />.
    /// </returns>
    public override bool Equals(object obj)
    {
      SortVersion other = obj as SortVersion;
      if (other != (SortVersion) null)
        return this.Equals(other);
      return false;
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли это <see cref="T:System.Globalization.SortVersion" /> экземпляр равен заданному <see cref="T:System.Globalization.SortVersion" /> объекта.
    /// </summary>
    /// <param name="other">
    ///   Объект, сравниваемый с данным экземпляром.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="other" /> представляет ту же версию, что и данный экземпляр; в противном случае — <see langword="false" />.
    /// </returns>
    public bool Equals(SortVersion other)
    {
      if (other == (SortVersion) null || this.m_NlsVersion != other.m_NlsVersion)
        return false;
      return this.m_SortId == other.m_SortId;
    }

    /// <summary>Возвращает хэш-код для данного экземпляра.</summary>
    /// <returns>
    ///   Хэш-код в виде 32-разрядного целого числа со знаком.
    /// </returns>
    public override int GetHashCode()
    {
      return this.m_NlsVersion * 7 | this.m_SortId.GetHashCode();
    }

    /// <summary>
    ///   Указывает, равны ли два экземпляра <see cref="T:System.Globalization.SortVersion" />.
    /// </summary>
    /// <param name="left">Первый экземпляр для сравнения.</param>
    /// <param name="right">Второй экземпляр для сравнения.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если значения параметров <paramref name="left" /> и <paramref name="right" /> равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool operator ==(SortVersion left, SortVersion right)
    {
      if ((object) left != null)
        return left.Equals(right);
      if ((object) right != null)
        return right.Equals(left);
      return true;
    }

    /// <summary>
    ///   Показывает, являются ли два экземпляра <see cref="T:System.Globalization.SortVersion" /> неравными.
    /// </summary>
    /// <param name="left">Первый экземпляр для сравнения.</param>
    /// <param name="right">Второй экземпляр для сравнения.</param>
    /// <returns>
    ///   Значение <see langword="true" />, если значения <paramref name="left" /> и <paramref name="right" /> не равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool operator !=(SortVersion left, SortVersion right)
    {
      return !(left == right);
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Security.Principal.IdentityReference
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Principal
{
  /// <summary>
  ///   Представляет удостоверение и является базовым классом для <see cref="T:System.Security.Principal.NTAccount" /> и <see cref="T:System.Security.Principal.SecurityIdentifier" /> классы.
  ///    Этот класс не предоставляет открытый конструктор и не может быть унаследован.
  /// </summary>
  [ComVisible(false)]
  public abstract class IdentityReference
  {
    internal IdentityReference()
    {
    }

    /// <summary>
    ///   Возвращает строковое значение идентификатора, представленного <see cref="T:System.Security.Principal.IdentityReference" /> объекта.
    /// </summary>
    /// <returns>
    ///   Строковое значение, представленное удостоверения <see cref="T:System.Security.Principal.IdentityReference" /> объекта.
    /// </returns>
    public abstract string Value { get; }

    /// <summary>
    ///   Возвращает значение, которое указывает, является ли указанный тип типом допустимых эквивалентов для <see cref="T:System.Security.Principal.IdentityReference" /> класса.
    /// </summary>
    /// <param name="targetType">
    ///   Запрашиваемый тип для действия в качестве преобразования из <see cref="T:System.Security.Principal.IdentityReference" />.
    ///    Допустимы следующие типы целевого объекта:
    /// 
    ///   <see cref="T:System.Security.Principal.NTAccount" />
    /// 
    ///   <see cref="T:System.Security.Principal.SecurityIdentifier" />
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="targetType" /> является допустимым перевода для <see cref="T:System.Security.Principal.IdentityReference" /> класса; в противном случае — <see langword="false" />.
    /// </returns>
    public abstract bool IsValidTargetType(Type targetType);

    /// <summary>
    ///   Преобразует имя учетной записи, представленной <see cref="T:System.Security.Principal.IdentityReference" /> объекта в другой <see cref="T:System.Security.Principal.IdentityReference" />-производный тип.
    /// </summary>
    /// <param name="targetType">
    ///   Целевой тип для преобразования из <see cref="T:System.Security.Principal.IdentityReference" />.
    /// </param>
    /// <returns>Преобразованное удостоверение.</returns>
    public abstract IdentityReference Translate(Type targetType);

    /// <summary>
    ///   Возвращает значение, указывающее, равен ли указанный объект этот экземпляр <see cref="T:System.Security.Principal.IdentityReference" /> класса.
    /// </summary>
    /// <param name="o">
    ///   Объект для сравнения с данным <see cref="T:System.Security.Principal.IdentityReference" /> экземпляра, или пустая ссылка.
    /// </param>
    /// <returns>
    ///   <see langword="true" /> Если <paramref name="o" /> является объект с тем же базовым типом и значением, что <see cref="T:System.Security.Principal.IdentityReference" /> экземпляра; в противном случае — <see langword="false" />.
    /// </returns>
    public abstract override bool Equals(object o);

    /// <summary>
    ///   Служит хэш-функцией для <see cref="T:System.Security.Principal.IdentityReference" />.
    ///    Метод <see cref="M:System.Security.Principal.IdentityReference.GetHashCode" /> подходит для использования в алгоритмах хэширования и структурах данных, например хэш-таблицах.
    /// </summary>
    /// <returns>
    ///   Хэш-код для этого <see cref="T:System.Security.Principal.IdentityReference" /> объекта.
    /// </returns>
    public abstract override int GetHashCode();

    /// <summary>
    ///   Возвращает строковое представление идентификатора, представленного <see cref="T:System.Security.Principal.IdentityReference" /> объекта.
    /// </summary>
    /// <returns>Удостоверение в строковом формате.</returns>
    public abstract override string ToString();

    /// <summary>
    ///   Сравнивает два <see cref="T:System.Security.Principal.IdentityReference" /> объектов, чтобы определить, равны ли они.
    ///    Они считаются равными, если они имеют одинаковое представление каноническое имя как возвращенного <see cref="P:System.Security.Principal.IdentityReference.Value" /> свойства или если они находятся <see langword="null" />.
    /// </summary>
    /// <param name="left">
    ///   Слева <see cref="T:System.Security.Principal.IdentityReference" /> операнд, используемый для определения равенства.
    ///    Этот параметр может иметь значение <see langword="null" />.
    /// </param>
    /// <param name="right">
    ///   Право <see cref="T:System.Security.Principal.IdentityReference" /> операнд, используемый для определения равенства.
    ///    Этот параметр может иметь значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="left" /> и <paramref name="right" /> равны; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool operator ==(IdentityReference left, IdentityReference right)
    {
      object obj1 = (object) left;
      object obj2 = (object) right;
      if (obj1 == null && obj2 == null)
        return true;
      if (obj1 == null || obj2 == null)
        return false;
      return left.Equals((object) right);
    }

    /// <summary>
    ///   Сравнивает два <see cref="T:System.Security.Principal.IdentityReference" /> объектов, чтобы определить, не равны ли они.
    ///    Они считаются неравными, если они имеют разные канонические представления имени возвращенного <see cref="P:System.Security.Principal.IdentityReference.Value" /> свойства или если один из объектов является <see langword="null" /> и другая — нет.
    /// </summary>
    /// <param name="left">
    ///   Слева <see cref="T:System.Security.Principal.IdentityReference" /> операнд, используемый для сравнения неравенства.
    ///    Этот параметр может иметь значение <see langword="null" />.
    /// </param>
    /// <param name="right">
    ///   Право <see cref="T:System.Security.Principal.IdentityReference" /> операнд, используемый для сравнения неравенства.
    ///    Этот параметр может иметь значение <see langword="null" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если <paramref name="left" /> и <paramref name="right" /> не равны друг другу; в противном случае — значение <see langword="false" />.
    /// </returns>
    public static bool operator !=(IdentityReference left, IdentityReference right)
    {
      return !(left == right);
    }
  }
}

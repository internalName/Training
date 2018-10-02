// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Представляет перечислитель для объектов <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> в <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntryCollection" />.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class KeyContainerPermissionAccessEntryEnumerator : IEnumerator
  {
    private KeyContainerPermissionAccessEntryCollection m_entries;
    private int m_current;

    private KeyContainerPermissionAccessEntryEnumerator()
    {
    }

    internal KeyContainerPermissionAccessEntryEnumerator(KeyContainerPermissionAccessEntryCollection entries)
    {
      this.m_entries = entries;
      this.m_current = -1;
    }

    /// <summary>Возвращает текущий элемент в коллекции.</summary>
    /// <returns>
    ///   Текущий <see cref="T:System.Security.Permissions.KeyContainerPermissionAccessEntry" /> объекта в коллекции.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.Current" /> Доступ к свойству до первого вызова <see cref="M:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.MoveNext" /> метод.
    ///    Курсор располагается перед первым объектом в коллекции.
    /// 
    ///   -или-
    /// 
    ///   <see cref="P:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.Current" /> Доступ к свойству после вызова <see cref="M:System.Security.Permissions.KeyContainerPermissionAccessEntryEnumerator.MoveNext" /> возвращает метод <see langword="false" />, который указывает, что курсор расположен после последнего объекта в коллекции.
    /// </exception>
    public KeyContainerPermissionAccessEntry Current
    {
      get
      {
        return this.m_entries[this.m_current];
      }
    }

    object IEnumerator.Current
    {
      get
      {
        return (object) this.m_entries[this.m_current];
      }
    }

    /// <summary>Переходит к следующему элементу в коллекции.</summary>
    /// <returns>
    ///   Значение <see langword="true" />, если перечислитель был успешно перемещен к следующему элементу; значение <see langword="false" />, если перечислитель достиг конца коллекции.
    /// </returns>
    public bool MoveNext()
    {
      if (this.m_current == this.m_entries.Count - 1)
        return false;
      ++this.m_current;
      return true;
    }

    /// <summary>Устанавливает перечислитель в начало коллекции.</summary>
    public void Reset()
    {
      this.m_current = -1;
    }
  }
}

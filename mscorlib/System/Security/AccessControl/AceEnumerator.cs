// Decompiled with JetBrains decompiler
// Type: System.Security.AccessControl.AceEnumerator
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;

namespace System.Security.AccessControl
{
  /// <summary>
  ///   Предоставляет возможность итерации по записям управления доступом (ACE) в списке управления доступом (ACL).
  /// </summary>
  public sealed class AceEnumerator : IEnumerator
  {
    private int _current;
    private readonly GenericAcl _acl;

    internal AceEnumerator(GenericAcl collection)
    {
      if (collection == null)
        throw new ArgumentNullException(nameof (collection));
      this._acl = collection;
      this.Reset();
    }

    object IEnumerator.Current
    {
      get
      {
        if (this._current == -1 || this._current >= this._acl.Count)
          throw new InvalidOperationException(Environment.GetResourceString("Arg_InvalidOperationException"));
        return (object) this._acl[this._current];
      }
    }

    /// <summary>
    ///   Получает текущий элемент <see cref="T:System.Security.AccessControl.GenericAce" /> коллекции.
    ///    Это свойство получает версию объекта удобного типа.
    /// </summary>
    /// <returns>
    ///   Текущий элемент в <see cref="T:System.Security.AccessControl.GenericAce" /> коллекции.
    /// </returns>
    public GenericAce Current
    {
      get
      {
        return ((IEnumerator) this).Current as GenericAce;
      }
    }

    /// <summary>
    ///   Перемещает перечислитель к следующему элементу <see cref="T:System.Security.AccessControl.GenericAce" /> коллекции.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если перечислитель был успешно перемещен к следующему элементу; значение <see langword="false" />, если перечислитель достиг конца коллекции.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Коллекция была изменена после создания перечислителя.
    /// </exception>
    public bool MoveNext()
    {
      ++this._current;
      return this._current < this._acl.Count;
    }

    /// <summary>
    ///   Устанавливает перечислитель в его начальное положение, перед первым элементом в <see cref="T:System.Security.AccessControl.GenericAce" /> коллекции.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Коллекция была изменена после создания перечислителя.
    /// </exception>
    public void Reset()
    {
      this._current = -1;
    }
  }
}

// Decompiled with JetBrains decompiler
// Type: System.Security.PermissionSet
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Policy;
using System.Security.Util;
using System.Text;
using System.Threading;

namespace System.Security
{
  /// <summary>
  ///   Представляет коллекцию, которая может содержать несколько разных типов разрешений.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  [StrongNameIdentityPermission(SecurityAction.InheritanceDemand, Name = "mscorlib", PublicKey = "0x00000000000000000400000000000000")]
  public class PermissionSet : ISecurityEncodable, ICollection, IEnumerable, IStackWalk, IDeserializationCallback
  {
    internal static readonly PermissionSet s_fullTrust = new PermissionSet(true);
    private bool m_Unrestricted;
    [OptionalField(VersionAdded = 2)]
    private bool m_allPermissionsDecoded;
    [OptionalField(VersionAdded = 2)]
    internal TokenBasedSet m_permSet;
    [OptionalField(VersionAdded = 2)]
    private bool m_ignoreTypeLoadFailures;
    [OptionalField(VersionAdded = 2)]
    private string m_serializedPermissionSet;
    [NonSerialized]
    private bool m_CheckedForNonCas;
    [NonSerialized]
    private bool m_ContainsCas;
    [NonSerialized]
    private bool m_ContainsNonCas;
    [NonSerialized]
    private TokenBasedSet m_permSetSaved;
    private bool readableonly;
    private TokenBasedSet m_unrestrictedPermSet;
    private TokenBasedSet m_normalPermSet;
    [OptionalField(VersionAdded = 2)]
    private bool m_canUnrestrictedOverride;
    private const string s_str_PermissionSet = "PermissionSet";
    private const string s_str_Permission = "Permission";
    private const string s_str_IPermission = "IPermission";
    private const string s_str_Unrestricted = "Unrestricted";
    private const string s_str_PermissionUnion = "PermissionUnion";
    private const string s_str_PermissionIntersection = "PermissionIntersection";
    private const string s_str_PermissionUnrestrictedUnion = "PermissionUnrestrictedUnion";
    private const string s_str_PermissionUnrestrictedIntersection = "PermissionUnrestrictedIntersection";

    [Conditional("_DEBUG")]
    private static void DEBUG_WRITE(string str)
    {
    }

    [Conditional("_DEBUG")]
    private static void DEBUG_COND_WRITE(bool exp, string str)
    {
    }

    [Conditional("_DEBUG")]
    private static void DEBUG_PRINTSTACK(Exception e)
    {
    }

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
    {
      this.Reset();
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      if (this.m_serializedPermissionSet != null)
        this.FromXml(SecurityElement.FromString(this.m_serializedPermissionSet));
      else if (this.m_normalPermSet != null)
        this.m_permSet = this.m_normalPermSet.SpecialUnion(this.m_unrestrictedPermSet);
      else if (this.m_unrestrictedPermSet != null)
        this.m_permSet = this.m_unrestrictedPermSet.SpecialUnion(this.m_normalPermSet);
      this.m_serializedPermissionSet = (string) null;
      this.m_normalPermSet = (TokenBasedSet) null;
      this.m_unrestrictedPermSet = (TokenBasedSet) null;
    }

    [OnSerializing]
    private void OnSerializing(StreamingContext ctx)
    {
      if ((ctx.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) == (StreamingContextStates) 0)
        return;
      this.m_serializedPermissionSet = this.ToString();
      if (this.m_permSet != null)
        this.m_permSet.SpecialSplit(ref this.m_unrestrictedPermSet, ref this.m_normalPermSet, this.m_ignoreTypeLoadFailures);
      this.m_permSetSaved = this.m_permSet;
      this.m_permSet = (TokenBasedSet) null;
    }

    [OnSerialized]
    private void OnSerialized(StreamingContext context)
    {
      if ((context.State & ~(StreamingContextStates.Clone | StreamingContextStates.CrossAppDomain)) == (StreamingContextStates) 0)
        return;
      this.m_serializedPermissionSet = (string) null;
      this.m_permSet = this.m_permSetSaved;
      this.m_permSetSaved = (TokenBasedSet) null;
      this.m_unrestrictedPermSet = (TokenBasedSet) null;
      this.m_normalPermSet = (TokenBasedSet) null;
    }

    internal PermissionSet()
    {
      this.Reset();
      this.m_Unrestricted = true;
    }

    internal PermissionSet(bool fUnrestricted)
      : this()
    {
      this.SetUnrestricted(fUnrestricted);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.PermissionSet" /> указанным значением <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </summary>
    /// <param name="state">
    ///   Одно из значений перечисления, определяющее набор разрешений доступа к ресурсам.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="state" /> Параметр не является допустимым <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </exception>
    public PermissionSet(PermissionState state)
      : this()
    {
      if (state == PermissionState.Unrestricted)
      {
        this.SetUnrestricted(true);
      }
      else
      {
        if (state != PermissionState.None)
          throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
        this.SetUnrestricted(false);
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.PermissionSet" /> с начальными значениями из параметра <paramref name="permSet" />.
    /// </summary>
    /// <param name="permSet">
    ///   Набор, из которого берется значение нового <see cref="T:System.Security.PermissionSet" /> или <see langword="null" /> для создания пустого <see cref="T:System.Security.PermissionSet" />.
    /// </param>
    public PermissionSet(PermissionSet permSet)
      : this()
    {
      if (permSet == null)
      {
        this.Reset();
      }
      else
      {
        this.m_Unrestricted = permSet.m_Unrestricted;
        this.m_CheckedForNonCas = permSet.m_CheckedForNonCas;
        this.m_ContainsCas = permSet.m_ContainsCas;
        this.m_ContainsNonCas = permSet.m_ContainsNonCas;
        this.m_ignoreTypeLoadFailures = permSet.m_ignoreTypeLoadFailures;
        if (permSet.m_permSet == null)
          return;
        this.m_permSet = new TokenBasedSet(permSet.m_permSet);
        for (int startingIndex = this.m_permSet.GetStartingIndex(); startingIndex <= this.m_permSet.GetMaxUsedIndex(); ++startingIndex)
        {
          object obj = this.m_permSet.GetItem(startingIndex);
          IPermission permission = obj as IPermission;
          ISecurityElementFactory securityElementFactory = obj as ISecurityElementFactory;
          if (permission != null)
            this.m_permSet.SetItem(startingIndex, (object) permission.Copy());
          else if (securityElementFactory != null)
            this.m_permSet.SetItem(startingIndex, securityElementFactory.Copy());
        }
      }
    }

    /// <summary>
    ///   Копирует объекты разрешений из набора в указанное место в <see cref="T:System.Array" />.
    /// </summary>
    /// <param name="array">
    ///   Целевой массив, в который выполняется копирование.
    /// </param>
    /// <param name="index">
    ///   Начальная позиция в массиве, с которой начинается копирование (от нуля).
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="array" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="array" /> имеет несколько измерений.
    /// </exception>
    /// <exception cref="T:System.IndexOutOfRangeException">
    ///   Параметр <paramref name="index" /> находится за пределами диапазона параметра <paramref name="array" />.
    /// </exception>
    public virtual void CopyTo(Array array, int index)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      PermissionSetEnumeratorInternal enumeratorInternal = new PermissionSetEnumeratorInternal(this);
      while (enumeratorInternal.MoveNext())
        array.SetValue(enumeratorInternal.Current, index++);
    }

    private PermissionSet(object trash, object junk)
    {
      this.m_Unrestricted = false;
    }

    /// <summary>Возвращает корневой объект текущей коллекции.</summary>
    /// <returns>Корневой объект текущей коллекции.</returns>
    public virtual object SyncRoot
    {
      get
      {
        return (object) this;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, гарантируется ли потокобезопасность коллекции.
    /// </summary>
    /// <returns>
    ///   Всегда <see langword="false" />.
    /// </returns>
    public virtual bool IsSynchronized
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///   Возвращает значение, указывающее, является ли коллекция доступной только для чтения.
    /// </summary>
    /// <returns>
    ///   Всегда <see langword="false" />.
    /// </returns>
    public virtual bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    internal void Reset()
    {
      this.m_Unrestricted = false;
      this.m_allPermissionsDecoded = true;
      this.m_permSet = (TokenBasedSet) null;
      this.m_ignoreTypeLoadFailures = false;
      this.m_CheckedForNonCas = false;
      this.m_ContainsCas = false;
      this.m_ContainsNonCas = false;
      this.m_permSetSaved = (TokenBasedSet) null;
    }

    internal void CheckSet()
    {
      if (this.m_permSet != null)
        return;
      this.m_permSet = new TokenBasedSet();
    }

    /// <summary>
    ///   Получает значение, указывающее, пуст ли объект <see cref="T:System.Security.PermissionSet" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если объект <see cref="T:System.Security.PermissionSet" /> пуст; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsEmpty()
    {
      if (this.m_Unrestricted)
        return false;
      if (this.m_permSet == null || this.m_permSet.FastIsEmpty())
        return true;
      PermissionSetEnumeratorInternal enumeratorInternal = new PermissionSetEnumeratorInternal(this);
      while (enumeratorInternal.MoveNext())
      {
        if (!((IPermission) enumeratorInternal.Current).IsSubsetOf((IPermission) null))
          return false;
      }
      return true;
    }

    internal bool FastIsEmpty()
    {
      return !this.m_Unrestricted && (this.m_permSet == null || this.m_permSet.FastIsEmpty());
    }

    /// <summary>
    ///   Получает число объектов разрешений, содержащихся в наборе разрешений.
    /// </summary>
    /// <returns>
    ///   Число объектов разрешений, содержащихся в <see cref="T:System.Security.PermissionSet" />.
    /// </returns>
    public virtual int Count
    {
      get
      {
        int num = 0;
        if (this.m_permSet != null)
          num += this.m_permSet.GetCount();
        return num;
      }
    }

    internal IPermission GetPermission(int index)
    {
      if (this.m_permSet == null)
        return (IPermission) null;
      object obj = this.m_permSet.GetItem(index);
      if (obj == null)
        return (IPermission) null;
      return obj as IPermission ?? this.CreatePermission(obj, index) ?? (IPermission) null;
    }

    internal IPermission GetPermission(PermissionToken permToken)
    {
      if (permToken == null)
        return (IPermission) null;
      return this.GetPermission(permToken.m_index);
    }

    internal IPermission GetPermission(IPermission perm)
    {
      if (perm == null)
        return (IPermission) null;
      return this.GetPermission(PermissionToken.GetToken(perm));
    }

    /// <summary>
    ///   Получает объект разрешений указанного типа, если он существует в наборе.
    /// </summary>
    /// <param name="permClass">Тип требуемого объекта разрешений.</param>
    /// <returns>
    ///   Копия объекта разрешений с типом, указанным параметром <paramref name="permClass" />, содержащимся в <see cref="T:System.Security.PermissionSet" />, или <see langword="null" />, если он не существует.
    /// </returns>
    public IPermission GetPermission(Type permClass)
    {
      return this.GetPermissionImpl(permClass);
    }

    /// <summary>
    ///   Получает объект разрешений указанного типа, если он существует в наборе.
    /// </summary>
    /// <param name="permClass">Тип объекта разрешений.</param>
    /// <returns>
    ///   Копия объекта разрешений с типом, указанным параметром <paramref name="permClass" />, содержащегося в <see cref="T:System.Security.PermissionSet" />, или <see langword="null" />, если он не существует.
    /// </returns>
    protected virtual IPermission GetPermissionImpl(Type permClass)
    {
      if (permClass == (Type) null)
        return (IPermission) null;
      return this.GetPermission(PermissionToken.FindToken(permClass));
    }

    /// <summary>
    ///   Устанавливает разрешение в <see cref="T:System.Security.PermissionSet" />, заменяя любые имеющиеся разрешения того же типа.
    /// </summary>
    /// <param name="perm">Разрешение для установки.</param>
    /// <returns>Установленное разрешение.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Метод вызывается из <see cref="T:System.Security.ReadOnlyPermissionSet" />.
    /// </exception>
    public IPermission SetPermission(IPermission perm)
    {
      return this.SetPermissionImpl(perm);
    }

    /// <summary>
    ///   Устанавливает разрешение в <see cref="T:System.Security.PermissionSet" />, заменяя любые имеющиеся разрешения того же типа.
    /// </summary>
    /// <param name="perm">Разрешение для установки.</param>
    /// <returns>Установленное разрешение.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Метод вызывается из <see cref="T:System.Security.ReadOnlyPermissionSet" />.
    /// </exception>
    protected virtual IPermission SetPermissionImpl(IPermission perm)
    {
      if (perm == null)
        return (IPermission) null;
      PermissionToken token = PermissionToken.GetToken(perm);
      if ((token.m_type & PermissionTokenType.IUnrestricted) != (PermissionTokenType) 0)
        this.m_Unrestricted = false;
      this.CheckSet();
      this.GetPermission(token.m_index);
      this.m_CheckedForNonCas = false;
      this.m_permSet.SetItem(token.m_index, (object) perm);
      return perm;
    }

    /// <summary>
    ///   Добавляет указанное разрешение в набор <see cref="T:System.Security.PermissionSet" />.
    /// </summary>
    /// <param name="perm">Разрешение для добавления.</param>
    /// <returns>
    ///   Объединение добавленного разрешения и любого разрешения того же типа, уже существующего в <see cref="T:System.Security.PermissionSet" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Метод вызывается из <see cref="T:System.Security.ReadOnlyPermissionSet" />.
    /// </exception>
    public IPermission AddPermission(IPermission perm)
    {
      return this.AddPermissionImpl(perm);
    }

    /// <summary>
    ///   Добавляет указанное разрешение в набор <see cref="T:System.Security.PermissionSet" />.
    /// </summary>
    /// <param name="perm">Разрешение для добавления.</param>
    /// <returns>
    ///   Объединение добавленного разрешения и любого разрешения того же типа, уже существующего в <see cref="T:System.Security.PermissionSet" />, или <see langword="null" />, если параметр <paramref name="perm" /> имеет значение <see langword="null" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Метод вызывается из <see cref="T:System.Security.ReadOnlyPermissionSet" />.
    /// </exception>
    protected virtual IPermission AddPermissionImpl(IPermission perm)
    {
      if (perm == null)
        return (IPermission) null;
      this.m_CheckedForNonCas = false;
      PermissionToken token = PermissionToken.GetToken(perm);
      if (this.IsUnrestricted() && (token.m_type & PermissionTokenType.IUnrestricted) != (PermissionTokenType) 0)
        return (IPermission) Activator.CreateInstance(perm.GetType(), BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, (Binder) null, new object[1]
        {
          (object) PermissionState.Unrestricted
        }, (CultureInfo) null);
      this.CheckSet();
      IPermission permission1 = this.GetPermission(token.m_index);
      if (permission1 != null)
      {
        IPermission permission2 = permission1.Union(perm);
        this.m_permSet.SetItem(token.m_index, (object) permission2);
        return permission2;
      }
      this.m_permSet.SetItem(token.m_index, (object) perm);
      return perm;
    }

    private IPermission RemovePermission(int index)
    {
      if (this.GetPermission(index) == null)
        return (IPermission) null;
      return (IPermission) this.m_permSet.RemoveItem(index);
    }

    /// <summary>Удаляет разрешение определенного типа из набора.</summary>
    /// <param name="permClass">Тип удаляемого разрешения.</param>
    /// <returns>Разрешение, удаленное из набора.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Метод вызывается из <see cref="T:System.Security.ReadOnlyPermissionSet" />.
    /// </exception>
    public IPermission RemovePermission(Type permClass)
    {
      return this.RemovePermissionImpl(permClass);
    }

    /// <summary>Удаляет разрешение определенного типа из набора.</summary>
    /// <param name="permClass">Тип удаляемого разрешения.</param>
    /// <returns>Разрешение, удаленное из набора.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Метод вызывается из <see cref="T:System.Security.ReadOnlyPermissionSet" />.
    /// </exception>
    protected virtual IPermission RemovePermissionImpl(Type permClass)
    {
      if (permClass == (Type) null)
        return (IPermission) null;
      PermissionToken token = PermissionToken.FindToken(permClass);
      if (token == null)
        return (IPermission) null;
      return this.RemovePermission(token.m_index);
    }

    internal void SetUnrestricted(bool unrestricted)
    {
      this.m_Unrestricted = unrestricted;
      if (!unrestricted)
        return;
      this.m_permSet = (TokenBasedSet) null;
    }

    /// <summary>
    ///   Определяет, имеет ли <see cref="T:System.Security.PermissionSet" /> тип <see langword="Unrestricted" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если <see cref="T:System.Security.PermissionSet" /> имеет тип <see langword="Unrestricted" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool IsUnrestricted()
    {
      return this.m_Unrestricted;
    }

    internal bool IsSubsetOfHelper(PermissionSet target, PermissionSet.IsSubsetOfType type, out IPermission firstPermThatFailed, bool ignoreNonCas)
    {
      firstPermThatFailed = (IPermission) null;
      if (target == null || target.FastIsEmpty())
      {
        if (this.IsEmpty())
          return true;
        firstPermThatFailed = this.GetFirstPerm();
        return false;
      }
      if (this.IsUnrestricted() && !target.IsUnrestricted())
        return false;
      if (this.m_permSet == null)
        return true;
      target.CheckSet();
      for (int startingIndex = this.m_permSet.GetStartingIndex(); startingIndex <= this.m_permSet.GetMaxUsedIndex(); ++startingIndex)
      {
        IPermission permission1 = this.GetPermission(startingIndex);
        if (permission1 != null && !permission1.IsSubsetOf((IPermission) null))
        {
          IPermission permission2 = target.GetPermission(startingIndex);
          if (!target.m_Unrestricted)
          {
            CodeAccessPermission accessPermission = permission1 as CodeAccessPermission;
            if (accessPermission == null)
            {
              if (!ignoreNonCas && !permission1.IsSubsetOf(permission2))
              {
                firstPermThatFailed = permission1;
                return false;
              }
            }
            else
            {
              firstPermThatFailed = permission1;
              switch (type)
              {
                case PermissionSet.IsSubsetOfType.Normal:
                  if (!permission1.IsSubsetOf(permission2))
                    return false;
                  break;
                case PermissionSet.IsSubsetOfType.CheckDemand:
                  if (!accessPermission.CheckDemand((CodeAccessPermission) permission2))
                    return false;
                  break;
                case PermissionSet.IsSubsetOfType.CheckPermitOnly:
                  if (!accessPermission.CheckPermitOnly((CodeAccessPermission) permission2))
                    return false;
                  break;
                case PermissionSet.IsSubsetOfType.CheckAssertion:
                  if (!accessPermission.CheckAssert((CodeAccessPermission) permission2))
                    return false;
                  break;
              }
              firstPermThatFailed = (IPermission) null;
            }
          }
        }
      }
      return true;
    }

    /// <summary>
    ///   Определяет, является ли текущий набор <see cref="T:System.Security.PermissionSet" /> подмножеством заданного набора <see cref="T:System.Security.PermissionSet" />.
    /// </summary>
    /// <param name="target">
    ///   Набор разрешений для проверки соотношения подмножеств.
    ///    Это должен быть либо <see cref="T:System.Security.PermissionSet" />, либо <see cref="T:System.Security.NamedPermissionSet" />.
    /// </param>
    /// <returns>
    ///   <see langword="true" />, если текущий <see cref="T:System.Security.PermissionSet" /> — подмножество параметра <paramref name="target" />; в противном случае — <see langword="false" />.
    /// </returns>
    public bool IsSubsetOf(PermissionSet target)
    {
      IPermission firstPermThatFailed;
      return this.IsSubsetOfHelper(target, PermissionSet.IsSubsetOfType.Normal, out firstPermThatFailed, false);
    }

    internal bool CheckDemand(PermissionSet target, out IPermission firstPermThatFailed)
    {
      return this.IsSubsetOfHelper(target, PermissionSet.IsSubsetOfType.CheckDemand, out firstPermThatFailed, true);
    }

    internal bool CheckPermitOnly(PermissionSet target, out IPermission firstPermThatFailed)
    {
      return this.IsSubsetOfHelper(target, PermissionSet.IsSubsetOfType.CheckPermitOnly, out firstPermThatFailed, true);
    }

    internal bool CheckAssertion(PermissionSet target)
    {
      IPermission firstPermThatFailed;
      return this.IsSubsetOfHelper(target, PermissionSet.IsSubsetOfType.CheckAssertion, out firstPermThatFailed, true);
    }

    internal bool CheckDeny(PermissionSet deniedSet, out IPermission firstPermThatFailed)
    {
      firstPermThatFailed = (IPermission) null;
      if (deniedSet == null || deniedSet.FastIsEmpty() || this.FastIsEmpty())
        return true;
      if (this.m_Unrestricted && deniedSet.m_Unrestricted)
        return false;
      PermissionSetEnumeratorInternal enumeratorInternal1 = new PermissionSetEnumeratorInternal(this);
      while (enumeratorInternal1.MoveNext())
      {
        CodeAccessPermission current = enumeratorInternal1.Current as CodeAccessPermission;
        if (current != null && !current.IsSubsetOf((IPermission) null))
        {
          if (deniedSet.m_Unrestricted)
          {
            firstPermThatFailed = (IPermission) current;
            return false;
          }
          CodeAccessPermission permission = (CodeAccessPermission) deniedSet.GetPermission(enumeratorInternal1.GetCurrentIndex());
          if (!current.CheckDeny(permission))
          {
            firstPermThatFailed = (IPermission) current;
            return false;
          }
        }
      }
      if (this.m_Unrestricted)
      {
        PermissionSetEnumeratorInternal enumeratorInternal2 = new PermissionSetEnumeratorInternal(deniedSet);
        while (enumeratorInternal2.MoveNext())
        {
          if (enumeratorInternal2.Current is IPermission)
            return false;
        }
      }
      return true;
    }

    internal void CheckDecoded(CodeAccessPermission demandedPerm, PermissionToken tokenDemandedPerm)
    {
      if (this.m_allPermissionsDecoded || this.m_permSet == null)
        return;
      if (tokenDemandedPerm == null)
        tokenDemandedPerm = PermissionToken.GetToken((IPermission) demandedPerm);
      this.CheckDecoded(tokenDemandedPerm.m_index);
    }

    internal void CheckDecoded(int index)
    {
      if (this.m_allPermissionsDecoded || this.m_permSet == null)
        return;
      this.GetPermission(index);
    }

    internal void CheckDecoded(PermissionSet demandedSet)
    {
      if (this.m_allPermissionsDecoded || this.m_permSet == null)
        return;
      PermissionSetEnumeratorInternal enumeratorInternal = demandedSet.GetEnumeratorInternal();
      while (enumeratorInternal.MoveNext())
        this.CheckDecoded(enumeratorInternal.GetCurrentIndex());
    }

    internal static void SafeChildAdd(SecurityElement parent, ISecurityElementFactory child, bool copy)
    {
      if (child == parent)
        return;
      if (child.GetTag().Equals("IPermission") || child.GetTag().Equals("Permission"))
        parent.AddChild(child);
      else if (parent.Tag.Equals(child.GetTag()))
      {
        SecurityElement securityElement = (SecurityElement) child;
        for (int index = 0; index < securityElement.InternalChildren.Count; ++index)
        {
          ISecurityElementFactory internalChild = (ISecurityElementFactory) securityElement.InternalChildren[index];
          parent.AddChildNoDuplicates(internalChild);
        }
      }
      else
        parent.AddChild(copy ? (ISecurityElementFactory) child.Copy() : child);
    }

    internal void InplaceIntersect(PermissionSet other)
    {
      Exception exception = (Exception) null;
      this.m_CheckedForNonCas = false;
      if (this == other)
        return;
      if (other == null || other.FastIsEmpty())
      {
        this.Reset();
      }
      else
      {
        if (this.FastIsEmpty())
          return;
        int num1 = this.m_permSet == null ? -1 : this.m_permSet.GetMaxUsedIndex();
        int num2 = other.m_permSet == null ? -1 : other.m_permSet.GetMaxUsedIndex();
        if (this.IsUnrestricted() && num1 < num2)
        {
          num1 = num2;
          this.CheckSet();
        }
        if (other.IsUnrestricted())
          other.CheckSet();
        for (int index = 0; index <= num1; ++index)
        {
          object obj1 = this.m_permSet.GetItem(index);
          IPermission permission1 = obj1 as IPermission;
          ISecurityElementFactory child1 = obj1 as ISecurityElementFactory;
          object obj2 = other.m_permSet.GetItem(index);
          IPermission target = obj2 as IPermission;
          ISecurityElementFactory child2 = obj2 as ISecurityElementFactory;
          if (obj1 != null || obj2 != null)
          {
            if (child1 != null && child2 != null)
            {
              if (child1.GetTag().Equals("PermissionIntersection") || child1.GetTag().Equals("PermissionUnrestrictedIntersection"))
              {
                PermissionSet.SafeChildAdd((SecurityElement) child1, child2, true);
              }
              else
              {
                bool copy = true;
                if (this.IsUnrestricted())
                {
                  SecurityElement parent = new SecurityElement("PermissionUnrestrictedUnion");
                  parent.AddAttribute("class", child1.Attribute("class"));
                  PermissionSet.SafeChildAdd(parent, child1, false);
                  child1 = (ISecurityElementFactory) parent;
                }
                if (other.IsUnrestricted())
                {
                  SecurityElement parent = new SecurityElement("PermissionUnrestrictedUnion");
                  parent.AddAttribute("class", child2.Attribute("class"));
                  PermissionSet.SafeChildAdd(parent, child2, true);
                  child2 = (ISecurityElementFactory) parent;
                  copy = false;
                }
                SecurityElement parent1 = new SecurityElement("PermissionIntersection");
                parent1.AddAttribute("class", child1.Attribute("class"));
                PermissionSet.SafeChildAdd(parent1, child1, false);
                PermissionSet.SafeChildAdd(parent1, child2, copy);
                this.m_permSet.SetItem(index, (object) parent1);
              }
            }
            else if (obj1 == null)
            {
              if (this.IsUnrestricted())
              {
                if (child2 != null)
                {
                  SecurityElement parent = new SecurityElement("PermissionUnrestrictedIntersection");
                  parent.AddAttribute("class", child2.Attribute("class"));
                  PermissionSet.SafeChildAdd(parent, child2, true);
                  this.m_permSet.SetItem(index, (object) parent);
                }
                else if ((((PermissionToken) PermissionToken.s_tokenSet.GetItem(index)).m_type & PermissionTokenType.IUnrestricted) != (PermissionTokenType) 0)
                  this.m_permSet.SetItem(index, (object) target.Copy());
              }
            }
            else if (obj2 == null)
            {
              if (other.IsUnrestricted())
              {
                if (child1 != null)
                {
                  SecurityElement parent = new SecurityElement("PermissionUnrestrictedIntersection");
                  parent.AddAttribute("class", child1.Attribute("class"));
                  PermissionSet.SafeChildAdd(parent, child1, false);
                  this.m_permSet.SetItem(index, (object) parent);
                }
                else if ((((PermissionToken) PermissionToken.s_tokenSet.GetItem(index)).m_type & PermissionTokenType.IUnrestricted) == (PermissionTokenType) 0)
                  this.m_permSet.SetItem(index, (object) null);
              }
              else
                this.m_permSet.SetItem(index, (object) null);
            }
            else
            {
              if (child1 != null)
                permission1 = this.CreatePermission((object) child1, index);
              if (child2 != null)
                target = other.CreatePermission((object) child2, index);
              try
              {
                IPermission permission2 = permission1 != null ? (target != null ? permission1.Intersect(target) : permission1) : target;
                this.m_permSet.SetItem(index, (object) permission2);
              }
              catch (Exception ex)
              {
                if (exception == null)
                  exception = ex;
              }
            }
          }
        }
        this.m_Unrestricted = this.m_Unrestricted && other.m_Unrestricted;
        if (exception != null)
          throw exception;
      }
    }

    /// <summary>
    ///   Создает и возвращает разрешение, представляющее собой пересечение текущего <see cref="T:System.Security.PermissionSet" /> и указанного <see cref="T:System.Security.PermissionSet" />.
    /// </summary>
    /// <param name="other">
    ///   Разрешение, пересекающееся с текущим <see cref="T:System.Security.PermissionSet" />.
    /// </param>
    /// <returns>
    ///   Новый набор разрешений, представляющий собой пересечение текущего <see cref="T:System.Security.PermissionSet" /> и указанного целевого объекта.
    ///    Этот объект является <see langword="null" />, если пересечение является пустым.
    /// </returns>
    public PermissionSet Intersect(PermissionSet other)
    {
      if (other == null || other.FastIsEmpty() || this.FastIsEmpty())
        return (PermissionSet) null;
      int num1 = this.m_permSet == null ? -1 : this.m_permSet.GetMaxUsedIndex();
      int num2 = other.m_permSet == null ? -1 : other.m_permSet.GetMaxUsedIndex();
      int num3 = num1 < num2 ? num1 : num2;
      if (this.IsUnrestricted() && num3 < num2)
      {
        num3 = num2;
        this.CheckSet();
      }
      if (other.IsUnrestricted() && num3 < num1)
      {
        num3 = num1;
        other.CheckSet();
      }
      PermissionSet permissionSet = new PermissionSet(false);
      if (num3 > -1)
        permissionSet.m_permSet = new TokenBasedSet();
      for (int index = 0; index <= num3; ++index)
      {
        object obj1 = this.m_permSet.GetItem(index);
        IPermission permission1 = obj1 as IPermission;
        ISecurityElementFactory child1 = obj1 as ISecurityElementFactory;
        object obj2 = other.m_permSet.GetItem(index);
        IPermission target = obj2 as IPermission;
        ISecurityElementFactory child2 = obj2 as ISecurityElementFactory;
        if (obj1 != null || obj2 != null)
        {
          if (child1 != null && child2 != null)
          {
            bool copy1 = true;
            bool copy2 = true;
            SecurityElement parent1 = new SecurityElement("PermissionIntersection");
            parent1.AddAttribute("class", child2.Attribute("class"));
            if (this.IsUnrestricted())
            {
              SecurityElement parent2 = new SecurityElement("PermissionUnrestrictedUnion");
              parent2.AddAttribute("class", child1.Attribute("class"));
              PermissionSet.SafeChildAdd(parent2, child1, true);
              copy2 = false;
              child1 = (ISecurityElementFactory) parent2;
            }
            if (other.IsUnrestricted())
            {
              SecurityElement parent2 = new SecurityElement("PermissionUnrestrictedUnion");
              parent2.AddAttribute("class", child2.Attribute("class"));
              PermissionSet.SafeChildAdd(parent2, child2, true);
              copy1 = false;
              child2 = (ISecurityElementFactory) parent2;
            }
            PermissionSet.SafeChildAdd(parent1, child2, copy1);
            PermissionSet.SafeChildAdd(parent1, child1, copy2);
            permissionSet.m_permSet.SetItem(index, (object) parent1);
          }
          else if (obj1 == null)
          {
            if (this.m_Unrestricted)
            {
              if (child2 != null)
              {
                SecurityElement parent = new SecurityElement("PermissionUnrestrictedIntersection");
                parent.AddAttribute("class", child2.Attribute("class"));
                PermissionSet.SafeChildAdd(parent, child2, true);
                permissionSet.m_permSet.SetItem(index, (object) parent);
              }
              else if (target != null && (((PermissionToken) PermissionToken.s_tokenSet.GetItem(index)).m_type & PermissionTokenType.IUnrestricted) != (PermissionTokenType) 0)
                permissionSet.m_permSet.SetItem(index, (object) target.Copy());
            }
          }
          else if (obj2 == null)
          {
            if (other.m_Unrestricted)
            {
              if (child1 != null)
              {
                SecurityElement parent = new SecurityElement("PermissionUnrestrictedIntersection");
                parent.AddAttribute("class", child1.Attribute("class"));
                PermissionSet.SafeChildAdd(parent, child1, true);
                permissionSet.m_permSet.SetItem(index, (object) parent);
              }
              else if (permission1 != null && (((PermissionToken) PermissionToken.s_tokenSet.GetItem(index)).m_type & PermissionTokenType.IUnrestricted) != (PermissionTokenType) 0)
                permissionSet.m_permSet.SetItem(index, (object) permission1.Copy());
            }
          }
          else
          {
            if (child1 != null)
              permission1 = this.CreatePermission((object) child1, index);
            if (child2 != null)
              target = other.CreatePermission((object) child2, index);
            IPermission permission2 = permission1 != null ? (target != null ? permission1.Intersect(target) : permission1) : target;
            permissionSet.m_permSet.SetItem(index, (object) permission2);
          }
        }
      }
      permissionSet.m_Unrestricted = this.m_Unrestricted && other.m_Unrestricted;
      if (permissionSet.FastIsEmpty())
        return (PermissionSet) null;
      return permissionSet;
    }

    internal void InplaceUnion(PermissionSet other)
    {
      if (this == other || other == null || other.FastIsEmpty())
        return;
      this.m_CheckedForNonCas = false;
      this.m_Unrestricted = this.m_Unrestricted || other.m_Unrestricted;
      if (this.m_Unrestricted)
      {
        this.m_permSet = (TokenBasedSet) null;
      }
      else
      {
        int num = -1;
        if (other.m_permSet != null)
        {
          num = other.m_permSet.GetMaxUsedIndex();
          this.CheckSet();
        }
        Exception exception = (Exception) null;
        for (int index = 0; index <= num; ++index)
        {
          object obj1 = this.m_permSet.GetItem(index);
          IPermission permission1 = obj1 as IPermission;
          ISecurityElementFactory child1 = obj1 as ISecurityElementFactory;
          object obj2 = other.m_permSet.GetItem(index);
          IPermission target = obj2 as IPermission;
          ISecurityElementFactory child2 = obj2 as ISecurityElementFactory;
          if (obj1 != null || obj2 != null)
          {
            if (child1 != null && child2 != null)
            {
              if (child1.GetTag().Equals("PermissionUnion") || child1.GetTag().Equals("PermissionUnrestrictedUnion"))
              {
                PermissionSet.SafeChildAdd((SecurityElement) child1, child2, true);
              }
              else
              {
                SecurityElement parent = this.IsUnrestricted() || other.IsUnrestricted() ? new SecurityElement("PermissionUnrestrictedUnion") : new SecurityElement("PermissionUnion");
                parent.AddAttribute("class", child1.Attribute("class"));
                PermissionSet.SafeChildAdd(parent, child1, false);
                PermissionSet.SafeChildAdd(parent, child2, true);
                this.m_permSet.SetItem(index, (object) parent);
              }
            }
            else if (obj1 == null)
            {
              if (child2 != null)
                this.m_permSet.SetItem(index, child2.Copy());
              else if (target != null && ((((PermissionToken) PermissionToken.s_tokenSet.GetItem(index)).m_type & PermissionTokenType.IUnrestricted) == (PermissionTokenType) 0 || !this.m_Unrestricted))
                this.m_permSet.SetItem(index, (object) target.Copy());
            }
            else if (obj2 != null)
            {
              if (child1 != null)
                permission1 = this.CreatePermission((object) child1, index);
              if (child2 != null)
                target = other.CreatePermission((object) child2, index);
              try
              {
                IPermission permission2 = permission1 != null ? (target != null ? permission1.Union(target) : permission1) : target;
                this.m_permSet.SetItem(index, (object) permission2);
              }
              catch (Exception ex)
              {
                if (exception == null)
                  exception = ex;
              }
            }
          }
        }
        if (exception != null)
          throw exception;
      }
    }

    /// <summary>
    ///   Создает <see cref="T:System.Security.PermissionSet" />, представляющий собой объединение текущего <see cref="T:System.Security.PermissionSet" /> и указанного <see cref="T:System.Security.PermissionSet" />.
    /// </summary>
    /// <param name="other">
    ///   Набор разрешений для создания объединения с текущим <see cref="T:System.Security.PermissionSet" />.
    /// </param>
    /// <returns>
    ///   Новый набор разрешений, представляющий собой объединение текущего <see cref="T:System.Security.PermissionSet" /> и указанного <see cref="T:System.Security.PermissionSet" />.
    /// </returns>
    public PermissionSet Union(PermissionSet other)
    {
      if (other == null || other.FastIsEmpty())
        return this.Copy();
      if (this.FastIsEmpty())
        return other.Copy();
      PermissionSet permissionSet = new PermissionSet();
      permissionSet.m_Unrestricted = this.m_Unrestricted || other.m_Unrestricted;
      if (permissionSet.m_Unrestricted)
        return permissionSet;
      this.CheckSet();
      other.CheckSet();
      int num = this.m_permSet.GetMaxUsedIndex() > other.m_permSet.GetMaxUsedIndex() ? this.m_permSet.GetMaxUsedIndex() : other.m_permSet.GetMaxUsedIndex();
      permissionSet.m_permSet = new TokenBasedSet();
      for (int index = 0; index <= num; ++index)
      {
        object obj1 = this.m_permSet.GetItem(index);
        IPermission permission1 = obj1 as IPermission;
        ISecurityElementFactory child1 = obj1 as ISecurityElementFactory;
        object obj2 = other.m_permSet.GetItem(index);
        IPermission target = obj2 as IPermission;
        ISecurityElementFactory child2 = obj2 as ISecurityElementFactory;
        if (obj1 != null || obj2 != null)
        {
          if (child1 != null && child2 != null)
          {
            SecurityElement parent = this.IsUnrestricted() || other.IsUnrestricted() ? new SecurityElement("PermissionUnrestrictedUnion") : new SecurityElement("PermissionUnion");
            parent.AddAttribute("class", child1.Attribute("class"));
            PermissionSet.SafeChildAdd(parent, child1, true);
            PermissionSet.SafeChildAdd(parent, child2, true);
            permissionSet.m_permSet.SetItem(index, (object) parent);
          }
          else if (obj1 == null)
          {
            if (child2 != null)
              permissionSet.m_permSet.SetItem(index, child2.Copy());
            else if (target != null && ((((PermissionToken) PermissionToken.s_tokenSet.GetItem(index)).m_type & PermissionTokenType.IUnrestricted) == (PermissionTokenType) 0 || !permissionSet.m_Unrestricted))
              permissionSet.m_permSet.SetItem(index, (object) target.Copy());
          }
          else if (obj2 == null)
          {
            if (child1 != null)
              permissionSet.m_permSet.SetItem(index, child1.Copy());
            else if (permission1 != null && ((((PermissionToken) PermissionToken.s_tokenSet.GetItem(index)).m_type & PermissionTokenType.IUnrestricted) == (PermissionTokenType) 0 || !permissionSet.m_Unrestricted))
              permissionSet.m_permSet.SetItem(index, (object) permission1.Copy());
          }
          else
          {
            if (child1 != null)
              permission1 = this.CreatePermission((object) child1, index);
            if (child2 != null)
              target = other.CreatePermission((object) child2, index);
            IPermission permission2 = permission1 != null ? (target != null ? permission1.Union(target) : permission1) : target;
            permissionSet.m_permSet.SetItem(index, (object) permission2);
          }
        }
      }
      return permissionSet;
    }

    internal void MergeDeniedSet(PermissionSet denied)
    {
      if (denied == null || denied.FastIsEmpty() || this.FastIsEmpty())
        return;
      this.m_CheckedForNonCas = false;
      if (this.m_permSet == null || denied.m_permSet == null)
        return;
      int num = denied.m_permSet.GetMaxUsedIndex() > this.m_permSet.GetMaxUsedIndex() ? this.m_permSet.GetMaxUsedIndex() : denied.m_permSet.GetMaxUsedIndex();
      for (int index = 0; index <= num; ++index)
      {
        IPermission target = denied.m_permSet.GetItem(index) as IPermission;
        if (target != null)
        {
          IPermission permission = this.m_permSet.GetItem(index) as IPermission;
          if (permission == null && !this.m_Unrestricted)
            denied.m_permSet.SetItem(index, (object) null);
          else if (permission != null && target != null && permission.IsSubsetOf(target))
          {
            this.m_permSet.SetItem(index, (object) null);
            denied.m_permSet.SetItem(index, (object) null);
          }
        }
      }
    }

    internal bool Contains(IPermission perm)
    {
      if (perm == null || this.m_Unrestricted)
        return true;
      if (this.FastIsEmpty())
        return false;
      PermissionToken token = PermissionToken.GetToken(perm);
      if (this.m_permSet.GetItem(token.m_index) == null)
        return perm.IsSubsetOf((IPermission) null);
      IPermission permission = this.GetPermission(token.m_index);
      if (permission != null)
        return perm.IsSubsetOf(permission);
      return perm.IsSubsetOf((IPermission) null);
    }

    /// <summary>
    ///   Определяет, равен ли заданный объект <see cref="T:System.Security.PermissionSet" /> или <see cref="T:System.Security.NamedPermissionSet" /> текущему объекту <see cref="T:System.Security.PermissionSet" />.
    /// </summary>
    /// <param name="obj">
    ///   Объект, который требуется сравнить с текущим объектом <see cref="T:System.Security.PermissionSet" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если указанный объект равен текущему объекту <see cref="T:System.Security.PermissionSet" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [ComVisible(false)]
    public override bool Equals(object obj)
    {
      PermissionSet permissionSet = obj as PermissionSet;
      if (permissionSet == null || this.m_Unrestricted != permissionSet.m_Unrestricted)
        return false;
      this.CheckSet();
      permissionSet.CheckSet();
      this.DecodeAllPermissions();
      permissionSet.DecodeAllPermissions();
      int num = Math.Max(this.m_permSet.GetMaxUsedIndex(), permissionSet.m_permSet.GetMaxUsedIndex());
      for (int index = 0; index <= num; ++index)
      {
        IPermission permission1 = (IPermission) this.m_permSet.GetItem(index);
        IPermission permission2 = (IPermission) permissionSet.m_permSet.GetItem(index);
        if (permission1 != null || permission2 != null)
        {
          if (permission1 == null)
          {
            if (!permission2.IsSubsetOf((IPermission) null))
              return false;
          }
          else if (permission2 == null)
          {
            if (!permission1.IsSubsetOf((IPermission) null))
              return false;
          }
          else if (!permission1.Equals((object) permission2))
            return false;
        }
      }
      return true;
    }

    /// <summary>
    ///   Возвращает хэш-код для объекта <see cref="T:System.Security.PermissionSet" />, который можно использовать в алгоритмах и структурах данных хэширования, например в хэш-таблице.
    /// </summary>
    /// <returns>
    ///   Хэш-код для текущего объекта <see cref="T:System.Security.PermissionSet" />.
    /// </returns>
    [ComVisible(false)]
    public override int GetHashCode()
    {
      int num = this.m_Unrestricted ? -1 : 0;
      if (this.m_permSet != null)
      {
        this.DecodeAllPermissions();
        int maxUsedIndex = this.m_permSet.GetMaxUsedIndex();
        for (int startingIndex = this.m_permSet.GetStartingIndex(); startingIndex <= maxUsedIndex; ++startingIndex)
        {
          IPermission permission = (IPermission) this.m_permSet.GetItem(startingIndex);
          if (permission != null)
            num ^= permission.GetHashCode();
        }
      }
      return num;
    }

    /// <summary>
    ///   Принудительно создает <see cref="T:System.Security.SecurityException" /> во время выполнения, если все вызывающие методы, расположенные выше в стеке вызовов, не получили разрешения, указанные текущим экземпляром.
    /// </summary>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего метода в цепочке вызовов нет требуемых разрешений.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Demand()
    {
      if (this.FastIsEmpty())
        return;
      this.ContainsNonCodeAccessPermissions();
      if (this.m_ContainsCas)
      {
        StackCrawlMark stackMark = StackCrawlMark.LookForMyCallersCaller;
        CodeAccessSecurityEngine.Check(this.GetCasOnlySet(), ref stackMark);
      }
      if (!this.m_ContainsNonCas)
        return;
      this.DemandNonCAS();
    }

    [SecurityCritical]
    internal void DemandNonCAS()
    {
      this.ContainsNonCodeAccessPermissions();
      if (!this.m_ContainsNonCas || this.m_permSet == null)
        return;
      this.CheckSet();
      for (int startingIndex = this.m_permSet.GetStartingIndex(); startingIndex <= this.m_permSet.GetMaxUsedIndex(); ++startingIndex)
      {
        IPermission permission = this.GetPermission(startingIndex);
        if (permission != null && !(permission is CodeAccessPermission))
          permission.Demand();
      }
    }

    /// <summary>
    ///   Объявляет, что вызывающий код может получить доступ к ресурсу, защищенному требованием разрешения, через код, вызывающий этот метод, даже если вызывающим объектам выше в стеке вызовов не предоставлено разрешение на доступ к ресурсу.
    ///    С помощью <see cref="M:System.Security.PermissionSet.Assert" /> можно создать уязвимости системы безопасности.
    /// </summary>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Утвержденный экземпляр <see cref="T:System.Security.PermissionSet" /> не был предоставлен утверждающему коду.
    /// 
    ///   -или-
    /// 
    ///   Уже имеется активное утверждение <see cref="M:System.Security.PermissionSet.Assert" /> для текущего кадра.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Assert()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      SecurityRuntime.Assert(this, ref stackMark);
    }

    /// <summary>
    ///   Вызывает сбой любого требования <see cref="M:System.Security.PermissionSet.Demand" />, проходящего через вызывающий код для получения разрешения, которое пересекается с разрешением типа, содержащегося в текущем наборе <see cref="T:System.Security.PermissionSet" />.
    /// </summary>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Предыдущий вызов <see cref="M:System.Security.PermissionSet.Deny" /> уже ограничил разрешения для текущего кадра стека.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("Deny is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Deny()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      SecurityRuntime.Deny(this, ref stackMark);
    }

    /// <summary>
    ///   Вызывает сбой любого требования <see cref="M:System.Security.PermissionSet.Demand" />, проходящего через вызывающий код для получения любого набора <see cref="T:System.Security.PermissionSet" />, который не является подмножеством текущего набора <see cref="T:System.Security.PermissionSet" />.
    /// </summary>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void PermitOnly()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      SecurityRuntime.PermitOnly(this, ref stackMark);
    }

    internal IPermission GetFirstPerm()
    {
      IEnumerator enumerator = this.GetEnumerator();
      if (!enumerator.MoveNext())
        return (IPermission) null;
      return enumerator.Current as IPermission;
    }

    /// <summary>
    ///   Создает копию объекта <see cref="T:System.Security.PermissionSet" />.
    /// </summary>
    /// <returns>
    ///   Копия объекта <see cref="T:System.Security.PermissionSet" />.
    /// </returns>
    public virtual PermissionSet Copy()
    {
      return new PermissionSet(this);
    }

    internal PermissionSet CopyWithNoIdentityPermissions()
    {
      PermissionSet permissionSet = new PermissionSet(this);
      permissionSet.RemovePermission(typeof (GacIdentityPermission));
      permissionSet.RemovePermission(typeof (PublisherIdentityPermission));
      permissionSet.RemovePermission(typeof (StrongNameIdentityPermission));
      permissionSet.RemovePermission(typeof (UrlIdentityPermission));
      permissionSet.RemovePermission(typeof (ZoneIdentityPermission));
      return permissionSet;
    }

    /// <summary>Возвращает перечислитель для разрешений в наборе.</summary>
    /// <returns>Объект-перечислитель для разрешений в наборе.</returns>
    public IEnumerator GetEnumerator()
    {
      return this.GetEnumeratorImpl();
    }

    /// <summary>Возвращает перечислитель для разрешений в наборе.</summary>
    /// <returns>Объект-перечислитель для разрешений в наборе.</returns>
    protected virtual IEnumerator GetEnumeratorImpl()
    {
      return (IEnumerator) new PermissionSetEnumerator(this);
    }

    internal PermissionSetEnumeratorInternal GetEnumeratorInternal()
    {
      return new PermissionSetEnumeratorInternal(this);
    }

    /// <summary>
    ///   Возвращает строковое представление объекта <see cref="T:System.Security.PermissionSet" />.
    /// </summary>
    /// <returns>
    ///   Представление объекта <see cref="T:System.Security.PermissionSet" />.
    /// </returns>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }

    private void NormalizePermissionSet()
    {
      PermissionSet permissionSet = new PermissionSet(false);
      permissionSet.m_Unrestricted = this.m_Unrestricted;
      if (this.m_permSet != null)
      {
        for (int startingIndex = this.m_permSet.GetStartingIndex(); startingIndex <= this.m_permSet.GetMaxUsedIndex(); ++startingIndex)
        {
          object obj = this.m_permSet.GetItem(startingIndex);
          IPermission perm = obj as IPermission;
          ISecurityElementFactory securityElementFactory = obj as ISecurityElementFactory;
          if (securityElementFactory != null)
            perm = this.CreatePerm((object) securityElementFactory);
          if (perm != null)
            permissionSet.SetPermission(perm);
        }
      }
      this.m_permSet = permissionSet.m_permSet;
    }

    private bool DecodeXml(byte[] data, HostProtectionResource fullTrustOnlyResources, HostProtectionResource inaccessibleResources)
    {
      if (data != null && data.Length != 0)
        this.FromXml(new Parser(data, Tokenizer.ByteTokenEncoding.UnicodeTokens).GetTopElement());
      this.FilterHostProtectionPermissions(fullTrustOnlyResources, inaccessibleResources);
      this.DecodeAllPermissions();
      return true;
    }

    private void DecodeAllPermissions()
    {
      if (this.m_permSet == null)
      {
        this.m_allPermissionsDecoded = true;
      }
      else
      {
        int maxUsedIndex = this.m_permSet.GetMaxUsedIndex();
        for (int index = 0; index <= maxUsedIndex; ++index)
          this.GetPermission(index);
        this.m_allPermissionsDecoded = true;
      }
    }

    internal void FilterHostProtectionPermissions(HostProtectionResource fullTrustOnly, HostProtectionResource inaccessible)
    {
      HostProtectionPermission.protectedResources = fullTrustOnly;
      HostProtectionPermission permission = (HostProtectionPermission) this.GetPermission(HostProtectionPermission.GetTokenIndex());
      if (permission == null)
        return;
      HostProtectionPermission protectionPermission = (HostProtectionPermission) permission.Intersect((IPermission) new HostProtectionPermission(fullTrustOnly));
      if (protectionPermission == null)
      {
        this.RemovePermission(typeof (HostProtectionPermission));
      }
      else
      {
        if (protectionPermission.Resources == permission.Resources)
          return;
        this.SetPermission((IPermission) protectionPermission);
      }
    }

    /// <summary>
    ///   Восстанавливает объект безопасности с указанным состоянием из кодировки XML.
    /// </summary>
    /// <param name="et">
    ///   Кодировка XML, используемая для восстановления объекта безопасности.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="et" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="et" /> не является допустимым элементом разрешения.
    /// 
    ///   -или-
    /// 
    ///   Номер версии параметра <paramref name="et" /> не поддерживается.
    /// </exception>
    public virtual void FromXml(SecurityElement et)
    {
      this.FromXml(et, false, false);
    }

    internal static bool IsPermissionTag(string tag, bool allowInternalOnly)
    {
      return tag.Equals("Permission") || tag.Equals("IPermission") || allowInternalOnly && (tag.Equals("PermissionUnion") || tag.Equals("PermissionIntersection") || (tag.Equals("PermissionUnrestrictedIntersection") || tag.Equals("PermissionUnrestrictedUnion")));
    }

    internal virtual void FromXml(SecurityElement et, bool allowInternalOnly, bool ignoreTypeLoadFailures)
    {
      if (et == null)
        throw new ArgumentNullException(nameof (et));
      if (!et.Tag.Equals(nameof (PermissionSet)))
        throw new ArgumentException(string.Format((IFormatProvider) null, Environment.GetResourceString("Argument_InvalidXMLElement"), (object) nameof (PermissionSet), (object) this.GetType().FullName));
      this.Reset();
      this.m_ignoreTypeLoadFailures = ignoreTypeLoadFailures;
      this.m_allPermissionsDecoded = false;
      this.m_Unrestricted = XMLUtil.IsUnrestricted(et);
      if (et.InternalChildren == null)
        return;
      int count = et.InternalChildren.Count;
      for (int index = 0; index < count; ++index)
      {
        SecurityElement child = (SecurityElement) et.Children[index];
        if (PermissionSet.IsPermissionTag(child.Tag, allowInternalOnly))
        {
          string typeStr = child.Attribute("class");
          PermissionToken permissionToken;
          object obj;
          if (typeStr != null)
          {
            permissionToken = PermissionToken.GetToken(typeStr);
            if (permissionToken == null)
            {
              obj = (object) this.CreatePerm((object) child);
              if (obj != null)
                permissionToken = PermissionToken.GetToken((IPermission) obj);
            }
            else
              obj = (object) child;
          }
          else
          {
            IPermission perm = this.CreatePerm((object) child);
            if (perm == null)
            {
              permissionToken = (PermissionToken) null;
              obj = (object) null;
            }
            else
            {
              permissionToken = PermissionToken.GetToken(perm);
              obj = (object) perm;
            }
          }
          if (permissionToken != null && obj != null)
          {
            if (this.m_permSet == null)
              this.m_permSet = new TokenBasedSet();
            if (this.m_permSet.GetItem(permissionToken.m_index) != null)
            {
              IPermission target = !(this.m_permSet.GetItem(permissionToken.m_index) is IPermission) ? this.CreatePerm((object) (SecurityElement) this.m_permSet.GetItem(permissionToken.m_index)) : (IPermission) this.m_permSet.GetItem(permissionToken.m_index);
              obj = !(obj is IPermission) ? (object) this.CreatePerm((object) (SecurityElement) obj).Union(target) : (object) ((IPermission) obj).Union(target);
            }
            if (this.m_Unrestricted && obj is IPermission)
              obj = (object) null;
            this.m_permSet.SetItem(permissionToken.m_index, obj);
          }
        }
      }
    }

    internal virtual void FromXml(SecurityDocument doc, int position, bool allowInternalOnly)
    {
      if (doc == null)
        throw new ArgumentNullException(nameof (doc));
      if (!doc.GetTagForElement(position).Equals(nameof (PermissionSet)))
        throw new ArgumentException(string.Format((IFormatProvider) null, Environment.GetResourceString("Argument_InvalidXMLElement"), (object) nameof (PermissionSet), (object) this.GetType().FullName));
      this.Reset();
      this.m_allPermissionsDecoded = false;
      Exception exception = (Exception) null;
      string attributeForElement1 = doc.GetAttributeForElement(position, "Unrestricted");
      this.m_Unrestricted = attributeForElement1 != null && (attributeForElement1.Equals("True") || attributeForElement1.Equals("true") || attributeForElement1.Equals("TRUE"));
      ArrayList positionForElement = doc.GetChildrenPositionForElement(position);
      int count = positionForElement.Count;
      for (int index = 0; index < count; ++index)
      {
        int position1 = (int) positionForElement[index];
        if (PermissionSet.IsPermissionTag(doc.GetTagForElement(position1), allowInternalOnly))
        {
          try
          {
            string attributeForElement2 = doc.GetAttributeForElement(position1, "class");
            PermissionToken permissionToken;
            object obj;
            if (attributeForElement2 != null)
            {
              permissionToken = PermissionToken.GetToken(attributeForElement2);
              if (permissionToken == null)
              {
                obj = (object) this.CreatePerm((object) doc.GetElement(position1, true));
                if (obj != null)
                  permissionToken = PermissionToken.GetToken((IPermission) obj);
              }
              else
                obj = (object) ((ISecurityElementFactory) new SecurityDocumentElement(doc, position1)).CreateSecurityElement();
            }
            else
            {
              IPermission perm = this.CreatePerm((object) doc.GetElement(position1, true));
              if (perm == null)
              {
                permissionToken = (PermissionToken) null;
                obj = (object) null;
              }
              else
              {
                permissionToken = PermissionToken.GetToken(perm);
                obj = (object) perm;
              }
            }
            if (permissionToken != null)
            {
              if (obj != null)
              {
                if (this.m_permSet == null)
                  this.m_permSet = new TokenBasedSet();
                IPermission permission = (IPermission) null;
                if (this.m_permSet.GetItem(permissionToken.m_index) != null)
                  permission = !(this.m_permSet.GetItem(permissionToken.m_index) is IPermission) ? this.CreatePerm(this.m_permSet.GetItem(permissionToken.m_index)) : (IPermission) this.m_permSet.GetItem(permissionToken.m_index);
                if (permission != null)
                  obj = !(obj is IPermission) ? (object) permission.Union(this.CreatePerm(obj)) : (object) permission.Union((IPermission) obj);
                if (this.m_Unrestricted && obj is IPermission)
                  obj = (object) null;
                this.m_permSet.SetItem(permissionToken.m_index, obj);
              }
            }
          }
          catch (Exception ex)
          {
            if (exception == null)
              exception = ex;
          }
        }
      }
      if (exception != null)
        throw exception;
    }

    private IPermission CreatePerm(object obj)
    {
      return PermissionSet.CreatePerm(obj, this.m_ignoreTypeLoadFailures);
    }

    internal static IPermission CreatePerm(object obj, bool ignoreTypeLoadFailures)
    {
      SecurityElement securityElement = obj as SecurityElement;
      ISecurityElementFactory securityElementFactory = obj as ISecurityElementFactory;
      if (securityElement == null && securityElementFactory != null)
        securityElement = securityElementFactory.CreateSecurityElement();
      IPermission target = (IPermission) null;
      string tag = securityElement.Tag;
      if (!(tag == "PermissionUnion"))
      {
        if (!(tag == "PermissionIntersection"))
        {
          if (!(tag == "PermissionUnrestrictedUnion"))
          {
            if (!(tag == "PermissionUnrestrictedIntersection"))
            {
              if (tag == "IPermission" || tag == "Permission")
                target = securityElement.ToPermission(ignoreTypeLoadFailures);
            }
            else
            {
              foreach (SecurityElement child in securityElement.Children)
              {
                IPermission perm = PermissionSet.CreatePerm((object) child, ignoreTypeLoadFailures);
                if (perm == null)
                  return (IPermission) null;
                target = (PermissionToken.GetToken(perm).m_type & PermissionTokenType.IUnrestricted) == (PermissionTokenType) 0 ? (IPermission) null : (target == null ? perm : perm.Intersect(target));
                if (target == null)
                  return (IPermission) null;
              }
            }
          }
          else
          {
            IEnumerator enumerator = securityElement.Children.GetEnumerator();
            bool flag = true;
            while (enumerator.MoveNext())
            {
              IPermission perm = PermissionSet.CreatePerm((object) (SecurityElement) enumerator.Current, ignoreTypeLoadFailures);
              if (perm != null)
              {
                if ((PermissionToken.GetToken(perm).m_type & PermissionTokenType.IUnrestricted) != (PermissionTokenType) 0)
                {
                  target = XMLUtil.CreatePermission(PermissionSet.GetPermissionElement((SecurityElement) enumerator.Current), PermissionState.Unrestricted, ignoreTypeLoadFailures);
                  break;
                }
                target = !flag ? perm.Union(target) : perm;
                flag = false;
              }
            }
          }
        }
        else
        {
          foreach (SecurityElement child in securityElement.Children)
          {
            IPermission perm = PermissionSet.CreatePerm((object) child, ignoreTypeLoadFailures);
            target = target == null ? perm : target.Intersect(perm);
            if (target == null)
              return (IPermission) null;
          }
        }
      }
      else
      {
        foreach (SecurityElement child in securityElement.Children)
        {
          IPermission perm = PermissionSet.CreatePerm((object) child, ignoreTypeLoadFailures);
          target = target == null ? perm : target.Union(perm);
        }
      }
      return target;
    }

    internal IPermission CreatePermission(object obj, int index)
    {
      IPermission perm = this.CreatePerm(obj);
      if (perm == null)
        return (IPermission) null;
      if (this.m_Unrestricted)
        perm = (IPermission) null;
      this.CheckSet();
      this.m_permSet.SetItem(index, (object) perm);
      if (perm != null)
      {
        PermissionToken token = PermissionToken.GetToken(perm);
        if (token != null && token.m_index != index)
          throw new ArgumentException(Environment.GetResourceString("Argument_UnableToGeneratePermissionSet"));
      }
      return perm;
    }

    private static SecurityElement GetPermissionElement(SecurityElement el)
    {
      string tag = el.Tag;
      if (tag == "IPermission" || tag == "Permission")
        return el;
      IEnumerator enumerator = el.Children.GetEnumerator();
      if (enumerator.MoveNext())
        return PermissionSet.GetPermissionElement((SecurityElement) enumerator.Current);
      return (SecurityElement) null;
    }

    internal static SecurityElement CreateEmptyPermissionSetXml()
    {
      SecurityElement securityElement = new SecurityElement(nameof (PermissionSet));
      securityElement.AddAttribute("class", "System.Security.PermissionSet");
      securityElement.AddAttribute("version", "1");
      return securityElement;
    }

    internal SecurityElement ToXml(string permName)
    {
      SecurityElement securityElement = new SecurityElement(nameof (PermissionSet));
      securityElement.AddAttribute("class", permName);
      securityElement.AddAttribute("version", "1");
      PermissionSetEnumeratorInternal enumeratorInternal = new PermissionSetEnumeratorInternal(this);
      if (this.m_Unrestricted)
        securityElement.AddAttribute("Unrestricted", "true");
      while (enumeratorInternal.MoveNext())
      {
        IPermission current = (IPermission) enumeratorInternal.Current;
        if (!this.m_Unrestricted)
          securityElement.AddChild(current.ToXml());
      }
      return securityElement;
    }

    internal SecurityElement InternalToXml()
    {
      SecurityElement securityElement = new SecurityElement(nameof (PermissionSet));
      securityElement.AddAttribute("class", this.GetType().FullName);
      securityElement.AddAttribute("version", "1");
      if (this.m_Unrestricted)
        securityElement.AddAttribute("Unrestricted", "true");
      if (this.m_permSet != null)
      {
        int maxUsedIndex = this.m_permSet.GetMaxUsedIndex();
        for (int startingIndex = this.m_permSet.GetStartingIndex(); startingIndex <= maxUsedIndex; ++startingIndex)
        {
          object obj = this.m_permSet.GetItem(startingIndex);
          if (obj != null)
          {
            if (obj is IPermission)
            {
              if (!this.m_Unrestricted)
                securityElement.AddChild(((ISecurityEncodable) obj).ToXml());
            }
            else
              securityElement.AddChild((SecurityElement) obj);
          }
        }
      }
      return securityElement;
    }

    /// <summary>
    ///   Создает кодировку XML для объекта безопасности и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Кодировка XML объекта безопасности, включающая сведения о состоянии.
    /// </returns>
    public virtual SecurityElement ToXml()
    {
      return this.ToXml("System.Security.PermissionSet");
    }

    internal byte[] EncodeXml()
    {
      MemoryStream memoryStream = new MemoryStream();
      BinaryWriter binaryWriter = new BinaryWriter((Stream) memoryStream, Encoding.Unicode);
      binaryWriter.Write(this.ToXml().ToString());
      binaryWriter.Flush();
      memoryStream.Position = 2L;
      byte[] buffer = new byte[(int) memoryStream.Length - 2];
      memoryStream.Read(buffer, 0, buffer.Length);
      return buffer;
    }

    /// <summary>
    ///   Преобразует закодированное значение <see cref="T:System.Security.PermissionSet" /> из одного формата кодировки XML в другой формат кодировки XML.
    /// </summary>
    /// <param name="inFormat">
    ///   Строка, представляющая один из следующих форматов кодировки: ASCII, Юникод или двоичный.
    ///    Возможные значения: XMLASCII или XML, XMLUNICODE и BINARY.
    /// </param>
    /// <param name="inData">Набор разрешений в XML-кодировке.</param>
    /// <param name="outFormat">
    ///   Строка, представляющая один из следующих форматов кодировки: ASCII, Юникод или двоичный.
    ///    Возможные значения: XMLASCII или XML, XMLUNICODE и BINARY.
    /// </param>
    /// <returns>
    ///   Зашифрованный набор разрешений с указанным выходным форматом.
    /// </returns>
    /// <exception cref="T:System.NotImplementedException">
    ///   Во всех случаях.
    /// </exception>
    [Obsolete("This method is obsolete and shoud no longer be used.")]
    public static byte[] ConvertPermissionSet(string inFormat, byte[] inData, string outFormat)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    ///   Получает значение, показывающее, содержит ли коллекция <see cref="T:System.Security.PermissionSet" /> разрешения, не являющиеся производными от <see cref="T:System.Security.CodeAccessPermission" />.
    /// </summary>
    /// <returns>
    ///   Значение <see langword="true" />, если <see cref="T:System.Security.PermissionSet" /> содержит разрешения, которые не являются производными от <see cref="T:System.Security.CodeAccessPermission" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    public bool ContainsNonCodeAccessPermissions()
    {
      if (this.m_CheckedForNonCas)
        return this.m_ContainsNonCas;
      lock (this)
      {
        if (this.m_CheckedForNonCas)
          return this.m_ContainsNonCas;
        this.m_ContainsCas = false;
        this.m_ContainsNonCas = false;
        if (this.IsUnrestricted())
          this.m_ContainsCas = true;
        if (this.m_permSet != null)
        {
          PermissionSetEnumeratorInternal enumeratorInternal = new PermissionSetEnumeratorInternal(this);
          while (enumeratorInternal.MoveNext() && (!this.m_ContainsCas || !this.m_ContainsNonCas))
          {
            IPermission current = enumeratorInternal.Current as IPermission;
            if (current != null)
            {
              if (current is CodeAccessPermission)
                this.m_ContainsCas = true;
              else
                this.m_ContainsNonCas = true;
            }
          }
        }
        this.m_CheckedForNonCas = true;
      }
      return this.m_ContainsNonCas;
    }

    private PermissionSet GetCasOnlySet()
    {
      if (!this.m_ContainsNonCas || this.IsUnrestricted())
        return this;
      PermissionSet permissionSet = new PermissionSet(false);
      PermissionSetEnumeratorInternal enumeratorInternal = new PermissionSetEnumeratorInternal(this);
      while (enumeratorInternal.MoveNext())
      {
        IPermission current = (IPermission) enumeratorInternal.Current;
        if (current is CodeAccessPermission)
          permissionSet.AddPermission(current);
      }
      permissionSet.m_CheckedForNonCas = true;
      permissionSet.m_ContainsCas = !permissionSet.IsEmpty();
      permissionSet.m_ContainsNonCas = false;
      return permissionSet;
    }

    [SecurityCritical]
    private static void SetupSecurity()
    {
      PolicyLevel appDomainLevel = PolicyLevel.CreateAppDomainLevel();
      CodeGroup codeGroup = (CodeGroup) new UnionCodeGroup((IMembershipCondition) new AllMembershipCondition(), (PermissionSet) appDomainLevel.GetNamedPermissionSet("Execution"));
      CodeGroup group1 = (CodeGroup) new UnionCodeGroup((IMembershipCondition) new StrongNameMembershipCondition(new StrongNamePublicKeyBlob("002400000480000094000000060200000024000052534131000400000100010007D1FA57C4AED9F0A32E84AA0FAEFD0DE9E8FD6AEC8F87FB03766C834C99921EB23BE79AD9D5DCC1DD9AD236132102900B723CF980957FC4E177108FC607774F29E8320E92EA05ECE4E821C0A5EFE8F1645C4C0C93C1AB99285D622CAA652C1DFAD63D745D6F2DE5F17E5EAF0FC4963D261C8A12436518206DC093344D5AD293"), (string) null, (Version) null), (PermissionSet) appDomainLevel.GetNamedPermissionSet("FullTrust"));
      CodeGroup group2 = (CodeGroup) new UnionCodeGroup((IMembershipCondition) new StrongNameMembershipCondition(new StrongNamePublicKeyBlob("00000000000000000400000000000000"), (string) null, (Version) null), (PermissionSet) appDomainLevel.GetNamedPermissionSet("FullTrust"));
      CodeGroup group3 = (CodeGroup) new UnionCodeGroup((IMembershipCondition) new GacMembershipCondition(), (PermissionSet) appDomainLevel.GetNamedPermissionSet("FullTrust"));
      codeGroup.AddChild(group1);
      codeGroup.AddChild(group2);
      codeGroup.AddChild(group3);
      appDomainLevel.RootCodeGroup = codeGroup;
      try
      {
        AppDomain.CurrentDomain.SetAppDomainPolicy(appDomainLevel);
      }
      catch (PolicyException ex)
      {
      }
    }

    private static void MergePermission(IPermission perm, bool separateCasFromNonCas, ref PermissionSet casPset, ref PermissionSet nonCasPset)
    {
      if (perm == null)
        return;
      if (!separateCasFromNonCas || perm is CodeAccessPermission)
      {
        if (casPset == null)
          casPset = new PermissionSet(false);
        IPermission permission = casPset.GetPermission(perm);
        IPermission target = casPset.AddPermission(perm);
        if (permission != null && !permission.IsSubsetOf(target))
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_DeclarativeUnion"));
      }
      else
      {
        if (nonCasPset == null)
          nonCasPset = new PermissionSet(false);
        IPermission permission = nonCasPset.GetPermission(perm);
        IPermission target = nonCasPset.AddPermission(perm);
        if (permission != null && !permission.IsSubsetOf(target))
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_DeclarativeUnion"));
      }
    }

    private static byte[] CreateSerialized(object[] attrs, bool serialize, ref byte[] nonCasBlob, out PermissionSet casPset, HostProtectionResource fullTrustOnlyResources, bool allowEmptyPermissionSets)
    {
      casPset = (PermissionSet) null;
      PermissionSet nonCasPset = (PermissionSet) null;
      for (int index = 0; index < attrs.Length; ++index)
      {
        if (attrs[index] is PermissionSetAttribute)
        {
          PermissionSet permissionSet = ((PermissionSetAttribute) attrs[index]).CreatePermissionSet();
          if (permissionSet == null)
            throw new ArgumentException(Environment.GetResourceString("Argument_UnableToGeneratePermissionSet"));
          PermissionSetEnumeratorInternal enumeratorInternal = new PermissionSetEnumeratorInternal(permissionSet);
          while (enumeratorInternal.MoveNext())
            PermissionSet.MergePermission((IPermission) enumeratorInternal.Current, serialize, ref casPset, ref nonCasPset);
          if (casPset == null)
            casPset = new PermissionSet(false);
          if (permissionSet.IsUnrestricted())
            casPset.SetUnrestricted(true);
        }
        else
          PermissionSet.MergePermission(((SecurityAttribute) attrs[index]).CreatePermission(), serialize, ref casPset, ref nonCasPset);
      }
      if (casPset != null)
      {
        casPset.FilterHostProtectionPermissions(fullTrustOnlyResources, HostProtectionResource.None);
        casPset.ContainsNonCodeAccessPermissions();
        if (allowEmptyPermissionSets && casPset.IsEmpty())
          casPset = (PermissionSet) null;
      }
      if (nonCasPset != null)
      {
        nonCasPset.FilterHostProtectionPermissions(fullTrustOnlyResources, HostProtectionResource.None);
        nonCasPset.ContainsNonCodeAccessPermissions();
        if (allowEmptyPermissionSets && nonCasPset.IsEmpty())
          nonCasPset = (PermissionSet) null;
      }
      byte[] numArray = (byte[]) null;
      nonCasBlob = (byte[]) null;
      if (serialize)
      {
        if (casPset != null)
          numArray = casPset.EncodeXml();
        if (nonCasPset != null)
          nonCasBlob = nonCasPset.EncodeXml();
      }
      return numArray;
    }

    void IDeserializationCallback.OnDeserialization(object sender)
    {
      this.NormalizePermissionSet();
      this.m_CheckedForNonCas = false;
    }

    /// <summary>
    ///   Приводит к удалению и выводу из действия всех предыдущих <see cref="M:System.Security.CodeAccessPermission.Assert" /> для текущего кадра.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Предыдущие значения <see cref="M:System.Security.CodeAccessPermission.Assert" /> для текущего кадра отсутствуют.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void RevertAssert()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      SecurityRuntime.RevertAssert(ref stackMark);
    }

    internal static PermissionSet RemoveRefusedPermissionSet(PermissionSet assertSet, PermissionSet refusedSet, out bool bFailedToCompress)
    {
      PermissionSet permissionSet = (PermissionSet) null;
      bFailedToCompress = false;
      if (assertSet == null)
        return (PermissionSet) null;
      if (refusedSet != null)
      {
        if (refusedSet.IsUnrestricted())
          return (PermissionSet) null;
        PermissionSetEnumeratorInternal enumeratorInternal = new PermissionSetEnumeratorInternal(refusedSet);
        while (enumeratorInternal.MoveNext())
        {
          CodeAccessPermission current = (CodeAccessPermission) enumeratorInternal.Current;
          int currentIndex = enumeratorInternal.GetCurrentIndex();
          if (current != null)
          {
            CodeAccessPermission permission = (CodeAccessPermission) assertSet.GetPermission(currentIndex);
            try
            {
              if (current.Intersect((IPermission) permission) != null)
              {
                if (current.Equals((object) permission))
                {
                  if (permissionSet == null)
                    permissionSet = assertSet.Copy();
                  permissionSet.RemovePermission(currentIndex);
                }
                else
                {
                  bFailedToCompress = true;
                  return assertSet;
                }
              }
            }
            catch (ArgumentException ex)
            {
              if (permissionSet == null)
                permissionSet = assertSet.Copy();
              permissionSet.RemovePermission(currentIndex);
            }
          }
        }
      }
      return permissionSet ?? assertSet;
    }

    internal static void RemoveAssertedPermissionSet(PermissionSet demandSet, PermissionSet assertSet, out PermissionSet alteredDemandSet)
    {
      alteredDemandSet = (PermissionSet) null;
      PermissionSetEnumeratorInternal enumeratorInternal = new PermissionSetEnumeratorInternal(demandSet);
      while (enumeratorInternal.MoveNext())
      {
        CodeAccessPermission current = (CodeAccessPermission) enumeratorInternal.Current;
        int currentIndex = enumeratorInternal.GetCurrentIndex();
        if (current != null)
        {
          CodeAccessPermission permission = (CodeAccessPermission) assertSet.GetPermission(currentIndex);
          try
          {
            if (current.CheckAssert(permission))
            {
              if (alteredDemandSet == null)
                alteredDemandSet = demandSet.Copy();
              alteredDemandSet.RemovePermission(currentIndex);
            }
          }
          catch (ArgumentException ex)
          {
          }
        }
      }
    }

    internal static bool IsIntersectingAssertedPermissions(PermissionSet assertSet1, PermissionSet assertSet2)
    {
      bool flag = false;
      if (assertSet1 != null && assertSet2 != null)
      {
        PermissionSetEnumeratorInternal enumeratorInternal = new PermissionSetEnumeratorInternal(assertSet2);
        while (enumeratorInternal.MoveNext())
        {
          CodeAccessPermission current = (CodeAccessPermission) enumeratorInternal.Current;
          int currentIndex = enumeratorInternal.GetCurrentIndex();
          if (current != null)
          {
            CodeAccessPermission permission = (CodeAccessPermission) assertSet1.GetPermission(currentIndex);
            try
            {
              if (permission != null)
              {
                if (!permission.Equals((object) current))
                  flag = true;
              }
            }
            catch (ArgumentException ex)
            {
              flag = true;
            }
          }
        }
      }
      return flag;
    }

    internal bool IgnoreTypeLoadFailures
    {
      set
      {
        this.m_ignoreTypeLoadFailures = value;
      }
    }

    internal enum IsSubsetOfType
    {
      Normal,
      CheckDemand,
      CheckPermitOnly,
      CheckAssertion,
    }
  }
}

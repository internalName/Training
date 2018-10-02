// Decompiled with JetBrains decompiler
// Type: System.Security.CodeAccessPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;
using System.Threading;

namespace System.Security
{
  /// <summary>
  ///   Определяет базовую структуру всех разрешений доступа к коду.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  [SecurityPermission(SecurityAction.InheritanceDemand, ControlEvidence = true, ControlPolicy = true)]
  public abstract class CodeAccessPermission : IPermission, ISecurityEncodable, IStackWalk
  {
    /// <summary>
    ///   Приводит к удалению и выводу из действия всех предыдущих значений <see cref="M:System.Security.CodeAccessPermission.Assert" /> для текущего кадра.
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

    /// <summary>
    ///   Приводит к удалению и выводу из действия всех предыдущих значений <see cref="M:System.Security.CodeAccessPermission.Deny" /> для текущего кадра.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Предыдущие значения <see cref="M:System.Security.CodeAccessPermission.Deny" /> для текущего кадра отсутствуют.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("Deny is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void RevertDeny()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      SecurityRuntime.RevertDeny(ref stackMark);
    }

    /// <summary>
    ///   Приводит к удалению и выводу из действия всех предыдущих значений <see cref="M:System.Security.CodeAccessPermission.PermitOnly" /> для текущего кадра.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Предыдущие значения <see cref="M:System.Security.CodeAccessPermission.PermitOnly" /> для текущего кадра отсутствуют.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void RevertPermitOnly()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      SecurityRuntime.RevertPermitOnly(ref stackMark);
    }

    /// <summary>
    ///   Приводит к удалению и выводу из действия всех предыдущих переопределений для текущего кадра.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Предыдущие значения <see cref="M:System.Security.CodeAccessPermission.Assert" />, <see cref="M:System.Security.CodeAccessPermission.Deny" /> или <see cref="M:System.Security.CodeAccessPermission.PermitOnly" /> для текущего кадра отсутствуют.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void RevertAll()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      SecurityRuntime.RevertAll(ref stackMark);
    }

    /// <summary>
    ///   Принудительно создает <see cref="T:System.Security.SecurityException" /> во время выполнения, если все вызывающие методы, расположенные выше в стеке вызовов, не получили разрешения, указанного текущим экземпляром.
    /// </summary>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Вызывающий метод, расположенный выше в стеке вызовов, не имеет разрешения, указанного текущим экземпляром.
    /// 
    ///   -или-
    /// 
    ///   Вызывающий метод, расположенный выше в стеке вызовов, вызвал <see cref="M:System.Security.CodeAccessPermission.Deny" /> в текущем объекте разрешений.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Demand()
    {
      if (this.CheckDemand((CodeAccessPermission) null))
        return;
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCallersCaller;
      CodeAccessSecurityEngine.Check(this, ref stackMark);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void Demand(PermissionType permissionType)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCallersCaller;
      CodeAccessSecurityEngine.SpecialDemand(permissionType, ref stackMark);
    }

    /// <summary>
    ///   Объявляет, что вызывающий код может получить доступ к ресурсу, защищенному требованием разрешения, через код, вызывающий этот метод, даже если вызывающим объектам выше в стеке вызовов не предоставлено разрешение на доступ к ресурсу.
    ///   <see cref="M:System.Security.CodeAccessPermission.Assert" /> может вызвать проблемы системы безопасности.
    /// </summary>
    /// <exception cref="T:System.Security.SecurityException">
    ///   У вызывающего кода отсутствует <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Assertion" />.
    /// 
    ///   -или-
    /// 
    ///   Уже имеется активное утверждение <see cref="M:System.Security.CodeAccessPermission.Assert" /> для текущего кадра.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Assert()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      CodeAccessSecurityEngine.Assert(this, ref stackMark);
    }

    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    internal static void Assert(bool allPossible)
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      SecurityRuntime.AssertAllPossible(ref stackMark);
    }

    /// <summary>
    ///   Запрещает вызывающим объектам выше в стеке вызовов использовать код, который вызывает этот метод для доступа к ресурсу, указанному текущим экземпляром.
    /// </summary>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Уже имеется активное утверждение <see cref="M:System.Security.CodeAccessPermission.Deny" /> для текущего кадра.
    /// </exception>
    [SecuritySafeCritical]
    [Obsolete("Deny is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void Deny()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      CodeAccessSecurityEngine.Deny(this, ref stackMark);
    }

    /// <summary>
    ///   Запрещает вызывающим объектам выше в стеке вызовов использовать код, который вызывает этот метод для доступа ко всем ресурсам, за исключением ресурса, указанного текущим экземпляром.
    /// </summary>
    /// <exception cref="T:System.Security.SecurityException">
    ///   Уже имеется активное утверждение <see cref="M:System.Security.CodeAccessPermission.PermitOnly" /> для текущего кадра.
    /// </exception>
    [SecuritySafeCritical]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public void PermitOnly()
    {
      StackCrawlMark stackMark = StackCrawlMark.LookForMyCaller;
      CodeAccessSecurityEngine.PermitOnly(this, ref stackMark);
    }

    /// <summary>
    ///   При переопределении в производном классе создает разрешение, представляющее собой объединение текущего и указанного разрешений.
    /// </summary>
    /// <param name="other">
    ///   Разрешение, которое требуется объединить с текущим разрешением.
    ///    Его тип должен совпадать с типом текущего разрешения.
    /// </param>
    /// <returns>
    ///   Новое разрешение, представляющее собой объединение текущего и указанного разрешений.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    ///   Параметр <paramref name="other" /> не является <see langword="null" />.
    ///    Метод поддерживается на этом уровне только при передаче <see langword="null" />.
    /// </exception>
    public virtual IPermission Union(IPermission other)
    {
      if (other == null)
        return this.Copy();
      throw new NotSupportedException(Environment.GetResourceString("NotSupported_SecurityPermissionUnion"));
    }

    internal static SecurityElement CreatePermissionElement(IPermission perm, string permname)
    {
      SecurityElement element = new SecurityElement("IPermission");
      XMLUtil.AddClassAttribute(element, perm.GetType(), permname);
      element.AddAttribute("version", "1");
      return element;
    }

    internal static void ValidateElement(SecurityElement elem, IPermission perm)
    {
      if (elem == null)
        throw new ArgumentNullException(nameof (elem));
      if (!XMLUtil.IsPermissionElement(perm, elem))
        throw new ArgumentException(Environment.GetResourceString("Argument_NotAPermissionElement"));
      string str = elem.Attribute("version");
      if (str != null && !str.Equals("1"))
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXMLBadVersion"));
    }

    /// <summary>
    ///   При переопределении в производном классе создает кодировку XML для объекта безопасности и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Кодировка XML объекта безопасности, включающая сведения о состоянии.
    /// </returns>
    public abstract SecurityElement ToXml();

    /// <summary>
    ///   При переопределении в производном классе восстанавливает объект безопасности с заданным состоянием из кодировки XML.
    /// </summary>
    /// <param name="elem">
    ///   Кодировка XML, используемая для восстановления объекта безопасности.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="elem" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="elem" /> не содержит кодировку XML для экземпляра того же типа, что и текущий экземпляр.
    /// 
    ///   -или-
    /// 
    ///   Номер версии параметра <paramref name="elem" /> не поддерживается.
    /// </exception>
    public abstract void FromXml(SecurityElement elem);

    /// <summary>
    ///   Создает и возвращает строковое представление текущего объекта разрешения.
    /// </summary>
    /// <returns>
    ///   Строковое представление текущего объекта разрешения.
    /// </returns>
    public override string ToString()
    {
      return this.ToXml().ToString();
    }

    internal bool VerifyType(IPermission perm)
    {
      return perm != null && !(perm.GetType() != this.GetType());
    }

    /// <summary>
    ///   При реализации производным классом создает и возвращает идентичную копию текущего объекта разрешения.
    /// </summary>
    /// <returns>Копия текущего объекта разрешения.</returns>
    public abstract IPermission Copy();

    /// <summary>
    ///   При реализации производным классом создает и возвращает разрешение, представляющее собой пересечение текущего и указанного разрешений.
    /// </summary>
    /// <param name="target">
    ///   Разрешение, пересекающееся с текущим разрешением.
    ///    Его тип должен совпадать с типом текущего разрешения.
    /// </param>
    /// <returns>
    ///   Новое разрешение, представляющее собой пересечение текущего и указанного разрешений.
    ///    Это новое разрешение равно <see langword="null" />, если пересечение является пустым.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="target" /> не равен <see langword="null" /> и не является экземпляром того же класса, что и текущее разрешение.
    /// </exception>
    public abstract IPermission Intersect(IPermission target);

    /// <summary>
    ///   Когда реализован производным классом, определяет, является ли текущее разрешение подмножеством указанного разрешения.
    /// </summary>
    /// <param name="target">
    ///   Разрешение, для которого требуется проверить отношение подмножества.
    ///    Его тип должен совпадать с типом текущего разрешения.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если текущее разрешение является подмножеством указанного разрешения. В противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="target" /> не равен <see langword="null" /> и имеет тип, не совпадающий с типом текущего разрешения.
    /// </exception>
    public abstract bool IsSubsetOf(IPermission target);

    /// <summary>
    ///   Определяет, равен ли заданный объект <see cref="T:System.Security.CodeAccessPermission" /> текущему объекту <see cref="T:System.Security.CodeAccessPermission" />.
    /// </summary>
    /// <param name="obj">
    ///   Объект <see cref="T:System.Security.CodeAccessPermission" />, который требуется сравнить с текущим объектом <see cref="T:System.Security.CodeAccessPermission" />.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если заданный объект <see cref="T:System.Security.CodeAccessPermission" /> равен текущему объекту <see cref="T:System.Security.CodeAccessPermission" />; в противном случае — значение <see langword="false" />.
    /// </returns>
    [ComVisible(false)]
    public override bool Equals(object obj)
    {
      IPermission target = obj as IPermission;
      if (obj != null && target == null)
        return false;
      try
      {
        if (!this.IsSubsetOf(target))
          return false;
        if (target != null)
        {
          if (!target.IsSubsetOf((IPermission) this))
            return false;
        }
      }
      catch (ArgumentException ex)
      {
        return false;
      }
      return true;
    }

    /// <summary>
    ///   Возвращает хэш-код для объекта <see cref="T:System.Security.CodeAccessPermission" />, который можно использовать в алгоритмах хэширования и структурах данных, например в хэш-таблице.
    /// </summary>
    /// <returns>
    ///   Хэш-код для текущего объекта <see cref="T:System.Security.CodeAccessPermission" />.
    /// </returns>
    [ComVisible(false)]
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }

    internal bool CheckDemand(CodeAccessPermission grant)
    {
      return this.IsSubsetOf((IPermission) grant);
    }

    internal bool CheckPermitOnly(CodeAccessPermission permitted)
    {
      return this.IsSubsetOf((IPermission) permitted);
    }

    internal bool CheckDeny(CodeAccessPermission denied)
    {
      IPermission permission = this.Intersect((IPermission) denied);
      if (permission != null)
        return permission.IsSubsetOf((IPermission) null);
      return true;
    }

    internal bool CheckAssert(CodeAccessPermission asserted)
    {
      return this.IsSubsetOf((IPermission) asserted);
    }
  }
}

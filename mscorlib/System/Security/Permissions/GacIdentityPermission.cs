// Decompiled with JetBrains decompiler
// Type: System.Security.Permissions.GacIdentityPermission
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
  /// <summary>
  ///   Определяет разрешение идентификации для файлов из глобального кэша сборок.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public sealed class GacIdentityPermission : CodeAccessPermission, IBuiltInPermission
  {
    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Security.Permissions.GacIdentityPermission" /> класса полностью ограниченный <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </summary>
    /// <param name="state">
    ///   Одно из значений <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="state" /> не является допустимым значением <see cref="T:System.Security.Permissions.PermissionState" />.
    /// </exception>
    public GacIdentityPermission(PermissionState state)
    {
      if (state != PermissionState.Unrestricted && state != PermissionState.None)
        throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.Permissions.GacIdentityPermission" />.
    /// </summary>
    public GacIdentityPermission()
    {
    }

    /// <summary>
    ///   Создает и возвращает идентичную копию текущего разрешения.
    /// </summary>
    /// <returns>Копия текущего разрешения.</returns>
    public override IPermission Copy()
    {
      return (IPermission) new GacIdentityPermission();
    }

    /// <summary>
    ///   Указывает, является ли текущее разрешение подмножеством указанного разрешения.
    /// </summary>
    /// <param name="target">
    ///   Объект разрешений для проверки соотношения подмножеств.
    ///    Разрешение должно иметь тот же тип, что и текущее разрешение.
    /// </param>
    /// <returns>
    ///   Значение <see langword="true" />, если текущее разрешение является подмножеством указанного разрешения. В противном случае — значение <see langword="false" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="target" /> не равен <see langword="null" /> и имеет тип, не совпадающий с типом текущего разрешения.
    /// </exception>
    public override bool IsSubsetOf(IPermission target)
    {
      if (target == null)
        return false;
      if (!(target is GacIdentityPermission))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      return true;
    }

    /// <summary>
    ///   Создает и возвращает разрешение, представляющее собой пересечение текущего и указанного разрешений.
    /// </summary>
    /// <param name="target">
    ///   Разрешение, пересекающееся с текущим разрешением.
    ///    Его тип должен совпадать с типом текущего разрешения.
    /// </param>
    /// <returns>
    ///   Новое разрешение, представляющее собой пересечение текущего и указанного разрешений.
    ///    Новое разрешение имеет <see langword="null" /> Если пересечение является пустым.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="target" /> не равен <see langword="null" /> и имеет тип, не совпадающий с типом текущего разрешения.
    /// </exception>
    public override IPermission Intersect(IPermission target)
    {
      if (target == null)
        return (IPermission) null;
      if (!(target is GacIdentityPermission))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      return this.Copy();
    }

    /// <summary>
    ///   Создает и возвращает разрешение, представляющее собой объединение текущего разрешения и указанного разрешения.
    /// </summary>
    /// <param name="target">
    ///   Разрешение, которое требуется объединить с текущим разрешением.
    ///    Его тип должен совпадать с типом текущего разрешения.
    /// </param>
    /// <returns>
    ///   Новое разрешение, представляющее собой объединение текущего и указанного разрешений.
    /// </returns>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="target" /> не равен <see langword="null" /> и имеет тип, не совпадающий с типом текущего разрешения.
    /// </exception>
    public override IPermission Union(IPermission target)
    {
      if (target == null)
        return this.Copy();
      if (!(target is GacIdentityPermission))
        throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", (object) this.GetType().FullName));
      return this.Copy();
    }

    /// <summary>
    ///   Создает кодировку XML для разрешения и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Объект <see cref="T:System.Security.SecurityElement" /> представляющий кодировку XML разрешения, включающая все сведения о состоянии.
    /// </returns>
    public override SecurityElement ToXml()
    {
      return CodeAccessPermission.CreatePermissionElement((IPermission) this, "System.Security.Permissions.GacIdentityPermission");
    }

    /// <summary>Создает разрешение из кодировки XML.</summary>
    /// <param name="securityElement">
    ///   Объект <see cref="T:System.Security.SecurityElement" />  содержащий XML-кодирование, используемое для создания разрешения.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="securityElement" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="securityElement" /> не является допустимым элементом разрешения.
    /// 
    ///   -или-
    /// 
    ///   Номер версии <paramref name="securityElement" /> является недопустимым.
    /// </exception>
    public override void FromXml(SecurityElement securityElement)
    {
      CodeAccessPermission.ValidateElement(securityElement, (IPermission) this);
    }

    int IBuiltInPermission.GetTokenIndex()
    {
      return GacIdentityPermission.GetTokenIndex();
    }

    internal static int GetTokenIndex()
    {
      return 15;
    }
  }
}

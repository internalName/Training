// Decompiled with JetBrains decompiler
// Type: System.Security.ReadOnlyPermissionSet
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Runtime.Serialization;

namespace System.Security
{
  /// <summary>
  ///   Представляет коллекцию только для чтения, который может содержать несколько различных типов разрешений.
  /// </summary>
  [Serializable]
  public sealed class ReadOnlyPermissionSet : PermissionSet
  {
    private SecurityElement m_originXml;
    [NonSerialized]
    private bool m_deserializing;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Security.ReadOnlyPermissionSet" />.
    /// </summary>
    /// <param name="permissionSetXml">
    ///   Элемент XML, из которого берется значение нового <see cref="T:System.Security.ReadOnlyPermissionSet" />.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="permissionSetXml" /> имеет значение <see langword="null" />.
    /// </exception>
    public ReadOnlyPermissionSet(SecurityElement permissionSetXml)
    {
      if (permissionSetXml == null)
        throw new ArgumentNullException(nameof (permissionSetXml));
      this.m_originXml = permissionSetXml.Copy();
      base.FromXml(this.m_originXml);
    }

    [OnDeserializing]
    private void OnDeserializing(StreamingContext ctx)
    {
      this.m_deserializing = true;
    }

    [OnDeserialized]
    private void OnDeserialized(StreamingContext ctx)
    {
      this.m_deserializing = false;
    }

    /// <summary>
    ///   Возвращает значение, указывающее на то, доступна ли коллекция только для чтения.
    /// </summary>
    /// <returns>
    ///   Всегда <see langword="true" />.
    /// </returns>
    public override bool IsReadOnly
    {
      get
      {
        return true;
      }
    }

    /// <summary>
    ///   Создает копию объекта <see cref="T:System.Security.ReadOnlyPermissionSet" />.
    /// </summary>
    /// <returns>Копировать разрешения только для чтения значение.</returns>
    public override PermissionSet Copy()
    {
      return (PermissionSet) new ReadOnlyPermissionSet(this.m_originXml);
    }

    /// <summary>
    ///   Создает кодировку XML для объекта безопасности и его текущего состояния.
    /// </summary>
    /// <returns>
    ///   Кодировка XML объекта безопасности, включающая сведения о состоянии.
    /// </returns>
    public override SecurityElement ToXml()
    {
      return this.m_originXml.Copy();
    }

    protected override IEnumerator GetEnumeratorImpl()
    {
      return (IEnumerator) new ReadOnlyPermissionSetEnumerator(base.GetEnumeratorImpl());
    }

    protected override IPermission GetPermissionImpl(Type permClass)
    {
      return base.GetPermissionImpl(permClass)?.Copy();
    }

    protected override IPermission AddPermissionImpl(IPermission perm)
    {
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ModifyROPermSet"));
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
    /// <exception cref="T:System.InvalidOperationException">
    ///   Объект не был десериализован; то есть <see cref="T:System.Security.PermissionSet" /> не вызвала обратно в <see cref="M:System.Security.ReadOnlyPermissionSet.FromXml(System.Security.SecurityElement)" /> во время десериализации.
    /// </exception>
    public override void FromXml(SecurityElement et)
    {
      if (!this.m_deserializing)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ModifyROPermSet"));
      base.FromXml(et);
    }

    protected override IPermission RemovePermissionImpl(Type permClass)
    {
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ModifyROPermSet"));
    }

    protected override IPermission SetPermissionImpl(IPermission perm)
    {
      throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ModifyROPermSet"));
    }
  }
}

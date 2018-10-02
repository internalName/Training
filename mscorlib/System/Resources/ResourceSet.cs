// Decompiled with JetBrains decompiler
// Type: System.Resources.ResourceSet
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Resources
{
  /// <summary>
  ///   Хранит все ресурсы, локализованные для одного определенного языка и региональных параметров, игнорируя все другие языки и региональные параметры, включая любые правила отката.
  /// 
  ///   Примечание по безопасности. Вызов методов в классе с недоверенными данными представляет угрозу безопасности.
  ///    Вызывайте методы только в классе с доверенными данными.
  ///    Дополнительные сведения см. в разделе Угрозы безопасности при работе с недоверенными данными.
  /// </summary>
  [ComVisible(true)]
  [Serializable]
  public class ResourceSet : IDisposable, IEnumerable
  {
    /// <summary>
    ///   Указывает объект <see cref="T:System.Resources.IResourceReader" />, используемый для чтения ресурсов.
    /// </summary>
    [NonSerialized]
    protected IResourceReader Reader;
    /// <summary>
    ///   Объект <see cref="T:System.Collections.Hashtable" />, в котором сохраняются ресурсы.
    /// </summary>
    protected Hashtable Table;
    private Hashtable _caseInsensitiveTable;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Resources.ResourceSet" /> стандартными свойствами.
    /// </summary>
    protected ResourceSet()
    {
      this.CommonInit();
    }

    internal ResourceSet(bool junk)
    {
    }

    /// <summary>
    ///   Создает новый экземпляр класса <see cref="T:System.Resources.ResourceSet" />, используя системное средство чтения по умолчанию <see cref="T:System.Resources.ResourceReader" />, которое открывает и считывает ресурсы из заданного файла.
    /// </summary>
    /// <param name="fileName">Файл ресурсов для чтения.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="fileName" /> имеет значение <see langword="null" />.
    /// </exception>
    public ResourceSet(string fileName)
    {
      this.Reader = (IResourceReader) new ResourceReader(fileName);
      this.CommonInit();
      this.ReadResources();
    }

    /// <summary>
    ///   Создает новый экземпляр класса <see cref="T:System.Resources.ResourceSet" />, используя системное средство чтения по умолчанию <see cref="T:System.Resources.ResourceReader" />, которое считывает ресурсы из заданного потока.
    /// </summary>
    /// <param name="stream">
    ///   Поток <see cref="T:System.IO.Stream" /> ресурсов для чтения.
    ///    Поток должен указывать на существующий файл ресурсов.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="stream" /> Недоступен для чтения.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="stream" /> имеет значение <see langword="null" />.
    /// </exception>
    [SecurityCritical]
    public ResourceSet(Stream stream)
    {
      this.Reader = (IResourceReader) new ResourceReader(stream);
      this.CommonInit();
      this.ReadResources();
    }

    /// <summary>
    ///   Создает новый экземпляр класса <see cref="T:System.Resources.ResourceSet" />, используя заданное средство чтения ресурсов.
    /// </summary>
    /// <param name="reader">
    ///   Средство чтения, которое будет использоваться.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="reader" /> имеет значение <see langword="null" />.
    /// </exception>
    public ResourceSet(IResourceReader reader)
    {
      if (reader == null)
        throw new ArgumentNullException(nameof (reader));
      this.Reader = reader;
      this.CommonInit();
      this.ReadResources();
    }

    private void CommonInit()
    {
      this.Table = new Hashtable();
    }

    /// <summary>
    ///   Закрывает и освобождает все ресурсы, используемые этим объектом <see cref="T:System.Resources.ResourceSet" />.
    /// </summary>
    public virtual void Close()
    {
      this.Dispose(true);
    }

    /// <summary>
    ///   Освобождает ресурсы (кроме памяти), связанные с текущим экземпляром, закрывая внутренние управляемые объекты, если имеется соответствующий запрос.
    /// </summary>
    /// <param name="disposing">
    ///   Указывает, следует ли явно закрыть объекты, содержащиеся в текущем экземпляре.
    /// </param>
    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        IResourceReader reader = this.Reader;
        this.Reader = (IResourceReader) null;
        reader?.Close();
      }
      this.Reader = (IResourceReader) null;
      this._caseInsensitiveTable = (Hashtable) null;
      this.Table = (Hashtable) null;
    }

    /// <summary>
    ///   Удаляет ресурсы (кроме памяти), используемые текущим экземпляром объекта <see cref="T:System.Resources.ResourceSet" />.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <summary>
    ///   Возвращает предпочтительный объект чтения ресурсов для этого типа объектов <see cref="T:System.Resources.ResourceSet" />.
    /// </summary>
    /// <returns>
    ///   Возвращает объект <see cref="T:System.Type" /> для предпочтительного средства чтения ресурсов для этого вида ресурсов <see cref="T:System.Resources.ResourceSet" />.
    /// </returns>
    public virtual Type GetDefaultReader()
    {
      return typeof (ResourceReader);
    }

    /// <summary>
    ///   Возвращает предпочтительный класс объектов записи ресурсов для этого вида ресурса <see cref="T:System.Resources.ResourceSet" />.
    /// </summary>
    /// <returns>
    ///   Возвращает объект <see cref="T:System.Type" /> для предпочтительного объекта записи ресурсов для этого вида ресурсов <see cref="T:System.Resources.ResourceSet" />.
    /// </returns>
    public virtual Type GetDefaultWriter()
    {
      return typeof (ResourceWriter);
    }

    /// <summary>
    ///   Возвращает объект <see cref="T:System.Collections.IDictionaryEnumerator" />, который может выполнить итерацию объекта <see cref="T:System.Resources.ResourceSet" />.
    /// </summary>
    /// <returns>
    ///   Перечислитель <see cref="T:System.Collections.IDictionaryEnumerator" /> для данного набора <see cref="T:System.Resources.ResourceSet" />.
    /// </returns>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Набор ресурсов был закрыт или удален.
    /// </exception>
    [ComVisible(false)]
    public virtual IDictionaryEnumerator GetEnumerator()
    {
      return this.GetEnumeratorHelper();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumeratorHelper();
    }

    private IDictionaryEnumerator GetEnumeratorHelper()
    {
      Hashtable table = this.Table;
      if (table == null)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
      return table.GetEnumerator();
    }

    /// <summary>
    ///   Выполняет поиск ресурса <see cref="T:System.String" /> с указанным именем.
    /// </summary>
    /// <param name="name">
    ///   Имя ресурса, для которого выполняется поиск.
    /// </param>
    /// <returns>
    ///   Значение ресурса, если значение равно <see cref="T:System.String" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Ресурс, заданный параметром <paramref name="name" /> не <see cref="T:System.String" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект закрыт или удален.
    /// </exception>
    public virtual string GetString(string name)
    {
      object objectInternal = this.GetObjectInternal(name);
      try
      {
        return (string) objectInternal;
      }
      catch (InvalidCastException ex)
      {
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Name", (object) name));
      }
    }

    /// <summary>
    ///   Выполняет поиск ресурса <see cref="T:System.String" /> с указанным именем без учета регистра, если это запрошено.
    /// </summary>
    /// <param name="name">
    ///   Имя ресурса, для которого выполняется поиск.
    /// </param>
    /// <param name="ignoreCase">
    ///   Указывает, следует ли игнорировать регистр знаков в указанном имени.
    /// </param>
    /// <returns>
    ///   Значение ресурса, если значение равно <see cref="T:System.String" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Ресурс, заданный параметром <paramref name="name" /> не <see cref="T:System.String" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект закрыт или удален.
    /// </exception>
    public virtual string GetString(string name, bool ignoreCase)
    {
      object objectInternal = this.GetObjectInternal(name);
      string str;
      try
      {
        str = (string) objectInternal;
      }
      catch (InvalidCastException ex)
      {
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Name", (object) name));
      }
      if (str != null || !ignoreCase)
        return str;
      object insensitiveObjectInternal = this.GetCaseInsensitiveObjectInternal(name);
      try
      {
        return (string) insensitiveObjectInternal;
      }
      catch (InvalidCastException ex)
      {
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Name", (object) name));
      }
    }

    /// <summary>
    ///   Выполняет поиск объекта ресурсов с указанным именем.
    /// </summary>
    /// <param name="name">
    ///   Имя ресурса с учетом регистра, для которого выполняется поиск.
    /// </param>
    /// <returns>Запрошенный ресурс.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект закрыт или удален.
    /// </exception>
    public virtual object GetObject(string name)
    {
      return this.GetObjectInternal(name);
    }

    /// <summary>
    ///   Ищет объект ресурса с указанным именем без учета регистра, если это запрашивается.
    /// </summary>
    /// <param name="name">
    ///   Имя ресурса, для которого выполняется поиск.
    /// </param>
    /// <param name="ignoreCase">
    ///   Указывает, следует ли игнорировать регистр знаков в указанном имени.
    /// </param>
    /// <returns>Запрошенный ресурс.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ObjectDisposedException">
    ///   Объект закрыт или удален.
    /// </exception>
    public virtual object GetObject(string name, bool ignoreCase)
    {
      object objectInternal = this.GetObjectInternal(name);
      if (objectInternal != null || !ignoreCase)
        return objectInternal;
      return this.GetCaseInsensitiveObjectInternal(name);
    }

    /// <summary>
    ///   Считывает все ресурсы и сохраняет их в объекте <see cref="T:System.Collections.Hashtable" />, указанном в свойстве <see cref="F:System.Resources.ResourceSet.Table" />.
    /// </summary>
    protected virtual void ReadResources()
    {
      IDictionaryEnumerator enumerator = this.Reader.GetEnumerator();
      while (enumerator.MoveNext())
      {
        object obj = enumerator.Value;
        this.Table.Add(enumerator.Key, obj);
      }
    }

    private object GetObjectInternal(string name)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      Hashtable table = this.Table;
      if (table == null)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
      return table[(object) name];
    }

    private object GetCaseInsensitiveObjectInternal(string name)
    {
      Hashtable table = this.Table;
      if (table == null)
        throw new ObjectDisposedException((string) null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
      Hashtable hashtable = this._caseInsensitiveTable;
      if (hashtable == null)
      {
        hashtable = new Hashtable((IEqualityComparer) StringComparer.OrdinalIgnoreCase);
        IDictionaryEnumerator enumerator = table.GetEnumerator();
        while (enumerator.MoveNext())
          hashtable.Add(enumerator.Key, enumerator.Value);
        this._caseInsensitiveTable = hashtable;
      }
      return hashtable[(object) name];
    }
  }
}

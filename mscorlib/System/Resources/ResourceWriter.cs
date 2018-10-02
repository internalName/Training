// Decompiled with JetBrains decompiler
// Type: System.Resources.ResourceWriter
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Resources
{
  /// <summary>
  ///   Записывает ресурсы в формате по умолчанию система выходной файл или поток.
  ///    Этот класс не наследуется.
  /// </summary>
  [ComVisible(true)]
  public sealed class ResourceWriter : IResourceWriter, IDisposable
  {
    private Func<Type, string> typeConverter;
    private const int _ExpectedNumberOfResources = 1000;
    private const int AverageNameSize = 40;
    private const int AverageValueSize = 40;
    private Dictionary<string, object> _resourceList;
    private Stream _output;
    private Dictionary<string, object> _caseInsensitiveDups;
    private Dictionary<string, ResourceWriter.PrecannedResource> _preserializedData;
    private const int _DefaultBufferSize = 4096;

    /// <summary>
    ///   Возвращает или задает делегат, позволяющий записывать, сборки ресурсов предназначены для версий платформы .NET Framework до версии .NET Framework 4 с помощью полных имен сборок.
    /// </summary>
    /// <returns>Тип, который инкапсулируется делегата.</returns>
    public Func<Type, string> TypeNameConverter
    {
      get
      {
        return this.typeConverter;
      }
      set
      {
        this.typeConverter = value;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Resources.ResourceWriter" />, который записывает ресурсы в указанный файл.
    /// </summary>
    /// <param name="fileName">Имя выходного файла.</param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="fileName" /> имеет значение <see langword="null" />.
    /// </exception>
    public ResourceWriter(string fileName)
    {
      if (fileName == null)
        throw new ArgumentNullException(nameof (fileName));
      this._output = (Stream) new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
      this._resourceList = new Dictionary<string, object>(1000, (IEqualityComparer<string>) FastResourceComparer.Default);
      this._caseInsensitiveDups = new Dictionary<string, object>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    ///   Инициализирует новый экземпляр <see cref="T:System.Resources.ResourceWriter" /> класс, записывающего ресурсы в указанный поток.
    /// </summary>
    /// <param name="stream">Выходной поток.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="stream" /> Параметр недоступен для записи.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="stream" /> имеет значение <see langword="null" />.
    /// </exception>
    public ResourceWriter(Stream stream)
    {
      if (stream == null)
        throw new ArgumentNullException(nameof (stream));
      if (!stream.CanWrite)
        throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotWritable"));
      this._output = stream;
      this._resourceList = new Dictionary<string, object>(1000, (IEqualityComparer<string>) FastResourceComparer.Default);
      this._caseInsensitiveDups = new Dictionary<string, object>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    ///   Добавляет строковый ресурс в список ресурсов для записи.
    /// </summary>
    /// <param name="name">Имя ресурса.</param>
    /// <param name="value">Значение ресурса.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> (или имена отличаются лишь регистром) уже был добавлен этот ResourceWriter.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Это <see cref="T:System.Resources.ResourceWriter" /> был закрыт, и его хэш-таблицы недоступен.
    /// </exception>
    public void AddResource(string name, string value)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (this._resourceList == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceWriterSaved"));
      this._caseInsensitiveDups.Add(name, (object) null);
      this._resourceList.Add(name, (object) value);
    }

    /// <summary>
    ///   Добавляет именованный ресурс, заданный в виде объекта в список ресурсов для записи.
    /// </summary>
    /// <param name="name">Имя ресурса.</param>
    /// <param name="value">Значение ресурса.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> (или имена отличаются лишь регистром) уже добавлен к этому <see cref="T:System.Resources.ResourceWriter" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Это <see cref="T:System.Resources.ResourceWriter" /> был закрыт, и его хэш-таблицы недоступен.
    /// </exception>
    public void AddResource(string name, object value)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (this._resourceList == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceWriterSaved"));
      if (value != null && value is Stream)
      {
        this.AddResourceInternal(name, (Stream) value, false);
      }
      else
      {
        this._caseInsensitiveDups.Add(name, (object) null);
        this._resourceList.Add(name, value);
      }
    }

    /// <summary>
    ///   Добавляет именованный ресурс, заданный в виде потока в список ресурсов для записи.
    /// </summary>
    /// <param name="name">Имя ресурса.</param>
    /// <param name="value">
    ///   Значение ресурса.
    ///    Ресурс должен поддерживать <see cref="P:System.IO.Stream.Length" /> свойство.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> (или имена отличаются лишь регистром) уже добавлен к этому <see cref="T:System.Resources.ResourceWriter" />.
    /// 
    ///   -или-
    /// 
    ///   Поток не поддерживает <see cref="P:System.IO.Stream.Length" /> свойство.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> или <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Это <see cref="T:System.Resources.ResourceWriter" /> был закрыт.
    /// </exception>
    public void AddResource(string name, Stream value)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (this._resourceList == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceWriterSaved"));
      this.AddResourceInternal(name, value, false);
    }

    /// <summary>
    ///   Добавляет именованный ресурс, заданный в виде потока в список ресурсов для записи и указывает, следует ли закрыть поток после <see cref="M:System.Resources.ResourceWriter.Generate" /> вызывается метод.
    /// </summary>
    /// <param name="name">Имя ресурса.</param>
    /// <param name="value">
    ///   Значение ресурса.
    ///    Ресурс должен поддерживать <see cref="P:System.IO.Stream.Length" /> свойство.
    /// </param>
    /// <param name="closeAfterWrite">
    ///   <see langword="true" /> Чтобы закрыть поток после <see cref="M:System.Resources.ResourceWriter.Generate" /> метод вызван; в противном случае — <see langword="false" />.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> (или имена отличаются лишь регистром) уже добавлен к этому <see cref="T:System.Resources.ResourceWriter" />.
    /// 
    ///   -или-
    /// 
    ///   Поток не поддерживает <see cref="P:System.IO.Stream.Length" /> свойство.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> или <paramref name="value" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Это <see cref="T:System.Resources.ResourceWriter" /> был закрыт.
    /// </exception>
    public void AddResource(string name, Stream value, bool closeAfterWrite)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (this._resourceList == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceWriterSaved"));
      this.AddResourceInternal(name, value, closeAfterWrite);
    }

    private void AddResourceInternal(string name, Stream value, bool closeAfterWrite)
    {
      if (value == null)
      {
        this._caseInsensitiveDups.Add(name, (object) null);
        this._resourceList.Add(name, (object) value);
      }
      else
      {
        if (!value.CanSeek)
          throw new ArgumentException(Environment.GetResourceString("NotSupported_UnseekableStream"));
        this._caseInsensitiveDups.Add(name, (object) null);
        this._resourceList.Add(name, (object) new ResourceWriter.StreamWrapper(value, closeAfterWrite));
      }
    }

    /// <summary>
    ///   Добавляет именованный ресурс, заданный в виде массива байтов в список ресурсов для записи.
    /// </summary>
    /// <param name="name">Имя ресурса.</param>
    /// <param name="value">
    ///   Значение ресурса в виде массива 8-разрядное целое число без знака.
    /// </param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="name" /> (или имена отличаются лишь регистром) уже добавлен к этому <see cref="T:System.Resources.ResourceWriter" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Это <see cref="T:System.Resources.ResourceWriter" /> был закрыт, и его хэш-таблицы недоступен.
    /// </exception>
    public void AddResource(string name, byte[] value)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (this._resourceList == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceWriterSaved"));
      this._caseInsensitiveDups.Add(name, (object) null);
      this._resourceList.Add(name, (object) value);
    }

    /// <summary>
    ///   Добавляет единицу данных как ресурс в список ресурсов для записи.
    /// </summary>
    /// <param name="name">
    ///   Имя, которое идентифицирует ресурс, содержащий добавленные данные.
    /// </param>
    /// <param name="typeName">
    ///   Имя типа добавленных данных.
    ///    Дополнительные сведения см. в разделе "Примечания".
    /// </param>
    /// <param name="serializedData">
    ///   Массив байтов, содержащий двоичное представление добавленных данных.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="name" />, <paramref name="typeName" /> или <paramref name="serializedData" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   Параметр <paramref name="name" /> (или имя, которое отличается лишь регистром) уже добавлен к этому объекту <see cref="T:System.Resources.ResourceWriter" />.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий объект <see cref="T:System.Resources.ResourceWriter" /> не инициализирован.
    ///    Вероятно, причина в том, что объект <see cref="T:System.Resources.ResourceWriter" /> закрыт.
    /// </exception>
    public void AddResourceData(string name, string typeName, byte[] serializedData)
    {
      if (name == null)
        throw new ArgumentNullException(nameof (name));
      if (typeName == null)
        throw new ArgumentNullException(nameof (typeName));
      if (serializedData == null)
        throw new ArgumentNullException(nameof (serializedData));
      if (this._resourceList == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceWriterSaved"));
      this._caseInsensitiveDups.Add(name, (object) null);
      if (this._preserializedData == null)
        this._preserializedData = new Dictionary<string, ResourceWriter.PrecannedResource>((IEqualityComparer<string>) FastResourceComparer.Default);
      this._preserializedData.Add(name, new ResourceWriter.PrecannedResource(typeName, serializedData));
    }

    /// <summary>Сохраняет ресурс в выходной поток и закрывает его.</summary>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Произошла ошибка при сериализации объекта.
    /// </exception>
    public void Close()
    {
      this.Dispose(true);
    }

    private void Dispose(bool disposing)
    {
      if (disposing)
      {
        if (this._resourceList != null)
          this.Generate();
        if (this._output != null)
          this._output.Close();
      }
      this._output = (Stream) null;
      this._caseInsensitiveDups = (Dictionary<string, object>) null;
    }

    /// <summary>
    ///   Позволяет пользователю закрыть файл или поток ресурсов, явно освобождая ресурсы.
    /// </summary>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Произошла ошибка при сериализации объекта.
    /// </exception>
    public void Dispose()
    {
      this.Dispose(true);
    }

    /// <summary>
    ///   Сохраняет все ресурсы в выходной поток в системном формате по умолчанию.
    /// </summary>
    /// <exception cref="T:System.IO.IOException">
    ///   Ошибка ввода/вывода.
    /// </exception>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">
    ///   Произошла ошибка при сериализации объекта.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Это <see cref="T:System.Resources.ResourceWriter" /> был закрыт, и его хэш-таблицы недоступен.
    /// </exception>
    [SecuritySafeCritical]
    public void Generate()
    {
      if (this._resourceList == null)
        throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceWriterSaved"));
      BinaryWriter binaryWriter1 = new BinaryWriter(this._output, Encoding.UTF8);
      List<string> types = new List<string>();
      binaryWriter1.Write(ResourceManager.MagicNumber);
      binaryWriter1.Write(ResourceManager.HeaderVersionNumber);
      MemoryStream memoryStream1 = new MemoryStream(240);
      BinaryWriter binaryWriter2 = new BinaryWriter((Stream) memoryStream1);
      binaryWriter2.Write(MultitargetingHelpers.GetAssemblyQualifiedName(typeof (ResourceReader), this.typeConverter));
      binaryWriter2.Write(ResourceManager.ResSetTypeName);
      binaryWriter2.Flush();
      binaryWriter1.Write((int) memoryStream1.Length);
      binaryWriter1.Write(memoryStream1.GetBuffer(), 0, (int) memoryStream1.Length);
      binaryWriter1.Write(2);
      int count = this._resourceList.Count;
      if (this._preserializedData != null)
        count += this._preserializedData.Count;
      binaryWriter1.Write(count);
      int[] keys = new int[count];
      int[] items = new int[count];
      int index1 = 0;
      MemoryStream memoryStream2 = new MemoryStream(count * 40);
      BinaryWriter binaryWriter3 = new BinaryWriter((Stream) memoryStream2, Encoding.Unicode);
      Stream output = (Stream) null;
      PermissionSet permissionSet = new PermissionSet(PermissionState.None);
      permissionSet.AddPermission((IPermission) new EnvironmentPermission(PermissionState.Unrestricted));
      permissionSet.AddPermission((IPermission) new FileIOPermission(PermissionState.Unrestricted));
      try
      {
        permissionSet.Assert();
        string tempFileName = Path.GetTempFileName();
        File.SetAttributes(tempFileName, FileAttributes.Temporary | FileAttributes.NotContentIndexed);
        output = (Stream) new FileStream(tempFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read, 4096, FileOptions.DeleteOnClose | FileOptions.SequentialScan);
      }
      catch (UnauthorizedAccessException ex)
      {
        output = (Stream) new MemoryStream();
      }
      catch (IOException ex)
      {
        output = (Stream) new MemoryStream();
      }
      finally
      {
        PermissionSet.RevertAssert();
      }
      using (output)
      {
        BinaryWriter binaryWriter4 = new BinaryWriter(output, Encoding.UTF8);
        IFormatter objFormatter = (IFormatter) new BinaryFormatter((ISurrogateSelector) null, new StreamingContext(StreamingContextStates.File | StreamingContextStates.Persistence));
        SortedList sortedList = new SortedList((IDictionary) this._resourceList, (IComparer) FastResourceComparer.Default);
        if (this._preserializedData != null)
        {
          foreach (KeyValuePair<string, ResourceWriter.PrecannedResource> keyValuePair in this._preserializedData)
            sortedList.Add((object) keyValuePair.Key, (object) keyValuePair.Value);
        }
        IDictionaryEnumerator enumerator = sortedList.GetEnumerator();
        while (enumerator.MoveNext())
        {
          keys[index1] = FastResourceComparer.HashFunction((string) enumerator.Key);
          items[index1++] = (int) binaryWriter3.Seek(0, SeekOrigin.Current);
          binaryWriter3.Write((string) enumerator.Key);
          binaryWriter3.Write((int) binaryWriter4.Seek(0, SeekOrigin.Current));
          object obj = enumerator.Value;
          ResourceTypeCode typeCode = this.FindTypeCode(obj, types);
          ResourceWriter.Write7BitEncodedInt(binaryWriter4, (int) typeCode);
          ResourceWriter.PrecannedResource precannedResource = obj as ResourceWriter.PrecannedResource;
          if (precannedResource != null)
            binaryWriter4.Write(precannedResource.Data);
          else
            this.WriteValue(typeCode, obj, binaryWriter4, objFormatter);
        }
        binaryWriter1.Write(types.Count);
        for (int index2 = 0; index2 < types.Count; ++index2)
          binaryWriter1.Write(types[index2]);
        Array.Sort<int, int>(keys, items);
        binaryWriter1.Flush();
        int num1 = (int) binaryWriter1.BaseStream.Position & 7;
        if (num1 > 0)
        {
          for (int index2 = 0; index2 < 8 - num1; ++index2)
            binaryWriter1.Write("PAD"[index2 % 3]);
        }
        foreach (int num2 in keys)
          binaryWriter1.Write(num2);
        foreach (int num2 in items)
          binaryWriter1.Write(num2);
        binaryWriter1.Flush();
        binaryWriter3.Flush();
        binaryWriter4.Flush();
        int num3 = (int) (binaryWriter1.Seek(0, SeekOrigin.Current) + memoryStream2.Length) + 4;
        binaryWriter1.Write(num3);
        binaryWriter1.Write(memoryStream2.GetBuffer(), 0, (int) memoryStream2.Length);
        binaryWriter3.Close();
        output.Position = 0L;
        output.CopyTo(binaryWriter1.BaseStream);
        binaryWriter4.Close();
      }
      binaryWriter1.Flush();
      this._resourceList = (Dictionary<string, object>) null;
    }

    private ResourceTypeCode FindTypeCode(object value, List<string> types)
    {
      if (value == null)
        return ResourceTypeCode.Null;
      Type type = value.GetType();
      if (type == typeof (string))
        return ResourceTypeCode.String;
      if (type == typeof (int))
        return ResourceTypeCode.Int32;
      if (type == typeof (bool))
        return ResourceTypeCode.Boolean;
      if (type == typeof (char))
        return ResourceTypeCode.Char;
      if (type == typeof (byte))
        return ResourceTypeCode.Byte;
      if (type == typeof (sbyte))
        return ResourceTypeCode.SByte;
      if (type == typeof (short))
        return ResourceTypeCode.Int16;
      if (type == typeof (long))
        return ResourceTypeCode.Int64;
      if (type == typeof (ushort))
        return ResourceTypeCode.UInt16;
      if (type == typeof (uint))
        return ResourceTypeCode.UInt32;
      if (type == typeof (ulong))
        return ResourceTypeCode.UInt64;
      if (type == typeof (float))
        return ResourceTypeCode.Single;
      if (type == typeof (double))
        return ResourceTypeCode.Double;
      if (type == typeof (Decimal))
        return ResourceTypeCode.Decimal;
      if (type == typeof (DateTime))
        return ResourceTypeCode.DateTime;
      if (type == typeof (TimeSpan))
        return ResourceTypeCode.TimeSpan;
      if (type == typeof (byte[]))
        return ResourceTypeCode.ByteArray;
      if (type == typeof (ResourceWriter.StreamWrapper))
        return ResourceTypeCode.Stream;
      string str;
      if (type == typeof (ResourceWriter.PrecannedResource))
      {
        str = ((ResourceWriter.PrecannedResource) value).TypeName;
        if (str.StartsWith("ResourceTypeCode.", StringComparison.Ordinal))
          return (ResourceTypeCode) Enum.Parse(typeof (ResourceTypeCode), str.Substring(17));
      }
      else
        str = MultitargetingHelpers.GetAssemblyQualifiedName(type, this.typeConverter);
      int num = types.IndexOf(str);
      if (num == -1)
      {
        num = types.Count;
        types.Add(str);
      }
      return (ResourceTypeCode) (num + 64);
    }

    private void WriteValue(ResourceTypeCode typeCode, object value, BinaryWriter writer, IFormatter objFormatter)
    {
      switch (typeCode)
      {
        case ResourceTypeCode.Null:
          break;
        case ResourceTypeCode.String:
          writer.Write((string) value);
          break;
        case ResourceTypeCode.Boolean:
          writer.Write((bool) value);
          break;
        case ResourceTypeCode.Char:
          writer.Write((ushort) (char) value);
          break;
        case ResourceTypeCode.Byte:
          writer.Write((byte) value);
          break;
        case ResourceTypeCode.SByte:
          writer.Write((sbyte) value);
          break;
        case ResourceTypeCode.Int16:
          writer.Write((short) value);
          break;
        case ResourceTypeCode.UInt16:
          writer.Write((ushort) value);
          break;
        case ResourceTypeCode.Int32:
          writer.Write((int) value);
          break;
        case ResourceTypeCode.UInt32:
          writer.Write((uint) value);
          break;
        case ResourceTypeCode.Int64:
          writer.Write((long) value);
          break;
        case ResourceTypeCode.UInt64:
          writer.Write((ulong) value);
          break;
        case ResourceTypeCode.Single:
          writer.Write((float) value);
          break;
        case ResourceTypeCode.Double:
          writer.Write((double) value);
          break;
        case ResourceTypeCode.Decimal:
          writer.Write((Decimal) value);
          break;
        case ResourceTypeCode.DateTime:
          long binary = ((DateTime) value).ToBinary();
          writer.Write(binary);
          break;
        case ResourceTypeCode.TimeSpan:
          writer.Write(((TimeSpan) value).Ticks);
          break;
        case ResourceTypeCode.ByteArray:
          byte[] buffer1 = (byte[]) value;
          writer.Write(buffer1.Length);
          writer.Write(buffer1, 0, buffer1.Length);
          break;
        case ResourceTypeCode.Stream:
          ResourceWriter.StreamWrapper streamWrapper = (ResourceWriter.StreamWrapper) value;
          if (streamWrapper.m_stream.GetType() == typeof (MemoryStream))
          {
            MemoryStream stream = (MemoryStream) streamWrapper.m_stream;
            if (stream.Length > (long) int.MaxValue)
              throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_StreamLength"));
            int origin;
            int length;
            stream.InternalGetOriginAndLength(out origin, out length);
            byte[] buffer2 = stream.InternalGetBuffer();
            writer.Write(length);
            writer.Write(buffer2, origin, length);
            break;
          }
          Stream stream1 = streamWrapper.m_stream;
          if (stream1.Length > (long) int.MaxValue)
            throw new ArgumentException(Environment.GetResourceString("ArgumentOutOfRange_StreamLength"));
          stream1.Position = 0L;
          writer.Write((int) stream1.Length);
          byte[] buffer3 = new byte[4096];
          int count;
          while ((count = stream1.Read(buffer3, 0, buffer3.Length)) != 0)
            writer.Write(buffer3, 0, count);
          if (!streamWrapper.m_closeAfterWrite)
            break;
          stream1.Close();
          break;
        default:
          objFormatter.Serialize(writer.BaseStream, value);
          break;
      }
    }

    private static void Write7BitEncodedInt(BinaryWriter store, int value)
    {
      uint num = (uint) value;
      while (num >= 128U)
      {
        store.Write((byte) (num | 128U));
        num >>= 7;
      }
      store.Write((byte) num);
    }

    private class PrecannedResource
    {
      internal string TypeName;
      internal byte[] Data;

      internal PrecannedResource(string typeName, byte[] data)
      {
        this.TypeName = typeName;
        this.Data = data;
      }
    }

    private class StreamWrapper
    {
      internal Stream m_stream;
      internal bool m_closeAfterWrite;

      internal StreamWrapper(Stream s, bool closeAfterWrite)
      {
        this.m_stream = s;
        this.m_closeAfterWrite = closeAfterWrite;
      }
    }
  }
}

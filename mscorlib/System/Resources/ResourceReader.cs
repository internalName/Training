// Decompiled with JetBrains decompiler
// Type: System.Resources.ResourceReader
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Text;

namespace System.Resources
{
  /// <summary>
  ///   Перечисляет ресурсы в двоичном файле ресурсов (RESOURCES) путем последовательного считывания пар "ключ-значение" ресурсов.
  /// 
  ///   Примечание по безопасности. Вызов методов в классе с недоверенными данными представляет угрозу безопасности.
  ///    Вызывайте методы только в классе с доверенными данными.
  ///    Дополнительные сведения см. в разделе Угрозы безопасности при работе с недоверенными данными.
  /// </summary>
  [ComVisible(true)]
  public sealed class ResourceReader : IResourceReader, IEnumerable, IDisposable
  {
    private static readonly string[] TypesSafeForDeserialization = new string[21]
    {
      "System.String[], mscorlib, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
      "System.DateTime[], mscorlib, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
      "System.Drawing.Bitmap, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
      "System.Drawing.Imaging.Metafile, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
      "System.Drawing.Point, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
      "System.Drawing.PointF, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
      "System.Drawing.Size, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
      "System.Drawing.SizeF, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
      "System.Drawing.Font, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
      "System.Drawing.Icon, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
      "System.Drawing.Color, System.Drawing, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
      "System.Windows.Forms.Cursor, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089",
      "System.Windows.Forms.Padding, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089",
      "System.Windows.Forms.LinkArea, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089",
      "System.Windows.Forms.ImageListStreamer, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089",
      "System.Windows.Forms.ListViewGroup, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089",
      "System.Windows.Forms.ListViewItem, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089",
      "System.Windows.Forms.ListViewItem+ListViewSubItem, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089",
      "System.Windows.Forms.ListViewItem+ListViewSubItem+SubItemStyle, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089",
      "System.Windows.Forms.OwnerDrawPropertyBag, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089",
      "System.Windows.Forms.TreeNode, System.Windows.Forms, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    };
    private const int DefaultFileStreamBufferSize = 4096;
    private BinaryReader _store;
    internal Dictionary<string, ResourceLocator> _resCache;
    private long _nameSectionOffset;
    private long _dataSectionOffset;
    private int[] _nameHashes;
    [SecurityCritical]
    private unsafe int* _nameHashesPtr;
    private int[] _namePositions;
    [SecurityCritical]
    private unsafe int* _namePositionsPtr;
    private RuntimeType[] _typeTable;
    private int[] _typeNamePositions;
    private BinaryFormatter _objFormatter;
    private int _numResources;
    private UnmanagedMemoryStream _ums;
    private int _version;
    private bool[] _safeToDeserialize;
    private ResourceReader.TypeLimitingDeserializationBinder _typeLimitingBinder;

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Resources.ResourceReader" /> для указанного именованного файла ресурсов.
    /// </summary>
    /// <param name="fileName">
    ///   Путь к файлу и имя файла ресурсов для чтения.
    ///   <paramref name="filename" /> не учитывает регистр.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="fileName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.FileNotFoundException">
    ///   Не удается найти файл.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   Файл ресурсов имеет недопустимый формат.
    ///    Например длина файла может быть нулевой.
    /// </exception>
    [SecuritySafeCritical]
    public ResourceReader(string fileName)
    {
      this._resCache = new Dictionary<string, ResourceLocator>((IEqualityComparer<string>) FastResourceComparer.Default);
      this._store = new BinaryReader((Stream) new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.RandomAccess, Path.GetFileName(fileName), false), Encoding.UTF8);
      try
      {
        this.ReadResources();
      }
      catch
      {
        this._store.Close();
        throw;
      }
    }

    /// <summary>
    ///   Инициализирует новый экземпляр класса <see cref="T:System.Resources.ResourceReader" /> для заданного потока.
    /// </summary>
    /// <param name="stream">Входной поток для чтения ресурсов.</param>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="stream" /> Параметр недоступен для чтения.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Параметр <paramref name="stream" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.IO.IOException">
    ///   Произошла ошибка ввода-вывода при доступе к <paramref name="stream" />.
    /// </exception>
    [SecurityCritical]
    public ResourceReader(Stream stream)
    {
      if (stream == null)
        throw new ArgumentNullException(nameof (stream));
      if (!stream.CanRead)
        throw new ArgumentException(Environment.GetResourceString("Argument_StreamNotReadable"));
      this._resCache = new Dictionary<string, ResourceLocator>((IEqualityComparer<string>) FastResourceComparer.Default);
      this._store = new BinaryReader(stream, Encoding.UTF8);
      this._ums = stream as UnmanagedMemoryStream;
      this.ReadResources();
    }

    [SecurityCritical]
    internal ResourceReader(Stream stream, Dictionary<string, ResourceLocator> resCache)
    {
      this._resCache = resCache;
      this._store = new BinaryReader(stream, Encoding.UTF8);
      this._ums = stream as UnmanagedMemoryStream;
      this.ReadResources();
    }

    /// <summary>
    ///   Освобождает все ресурсы операционной системы, связанные с этим объектом <see cref="T:System.Resources.ResourceReader" />.
    /// </summary>
    public void Close()
    {
      this.Dispose(true);
    }

    /// <summary>
    ///   Освобождает все ресурсы, используемые текущим экземпляром класса <see cref="T:System.Resources.ResourceReader" />.
    /// </summary>
    public void Dispose()
    {
      this.Close();
    }

    [SecuritySafeCritical]
    private unsafe void Dispose(bool disposing)
    {
      if (this._store == null)
        return;
      this._resCache = (Dictionary<string, ResourceLocator>) null;
      if (disposing)
      {
        BinaryReader store = this._store;
        this._store = (BinaryReader) null;
        store?.Close();
      }
      this._store = (BinaryReader) null;
      this._namePositions = (int[]) null;
      this._nameHashes = (int[]) null;
      this._ums = (UnmanagedMemoryStream) null;
      this._namePositionsPtr = (int*) null;
      this._nameHashesPtr = (int*) null;
    }

    [SecurityCritical]
    internal static unsafe int ReadUnalignedI4(int* p)
    {
      byte* numPtr = (byte*) p;
      return (int) *numPtr | (int) numPtr[1] << 8 | (int) numPtr[2] << 16 | (int) numPtr[3] << 24;
    }

    private void SkipInt32()
    {
      this._store.BaseStream.Seek(4L, SeekOrigin.Current);
    }

    private void SkipString()
    {
      int num = this._store.Read7BitEncodedInt();
      if (num < 0)
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_NegativeStringLength"));
      this._store.BaseStream.Seek((long) num, SeekOrigin.Current);
    }

    [SecuritySafeCritical]
    private unsafe int GetNameHash(int index)
    {
      if (this._ums == null)
        return this._nameHashes[index];
      return ResourceReader.ReadUnalignedI4(this._nameHashesPtr + index);
    }

    [SecuritySafeCritical]
    private unsafe int GetNamePosition(int index)
    {
      int num = this._ums != null ? ResourceReader.ReadUnalignedI4(this._namePositionsPtr + index) : this._namePositions[index];
      if (num < 0 || (long) num > this._dataSectionOffset - this._nameSectionOffset)
        throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesNameInvalidOffset", (object) num));
      return num;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) this.GetEnumerator();
    }

    /// <summary>
    ///   Возвращает перечислитель для данного объекта <see cref="T:System.Resources.ResourceReader" />.
    /// </summary>
    /// <returns>
    ///   Перечислитель для этого объекта <see cref="T:System.Resources.ResourceReader" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Модуль чтения закрыт или удален и его нельзя просмотреть.
    /// </exception>
    public IDictionaryEnumerator GetEnumerator()
    {
      if (this._resCache == null)
        throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
      return (IDictionaryEnumerator) new ResourceReader.ResourceEnumerator(this);
    }

    internal ResourceReader.ResourceEnumerator GetEnumeratorInternal()
    {
      return new ResourceReader.ResourceEnumerator(this);
    }

    internal int FindPosForResource(string name)
    {
      int num1 = FastResourceComparer.HashFunction(name);
      int num2 = 0;
      int num3 = this._numResources - 1;
      int index1 = -1;
      bool flag = false;
      while (num2 <= num3)
      {
        index1 = num2 + num3 >> 1;
        int nameHash = this.GetNameHash(index1);
        int num4 = nameHash != num1 ? (nameHash >= num1 ? 1 : -1) : 0;
        if (num4 == 0)
        {
          flag = true;
          break;
        }
        if (num4 < 0)
          num2 = index1 + 1;
        else
          num3 = index1 - 1;
      }
      if (!flag)
        return -1;
      if (num2 != index1)
      {
        num2 = index1;
        while (num2 > 0 && this.GetNameHash(num2 - 1) == num1)
          --num2;
      }
      if (num3 != index1)
      {
        num3 = index1;
        while (num3 < this._numResources - 1 && this.GetNameHash(num3 + 1) == num1)
          ++num3;
      }
      lock (this)
      {
        for (int index2 = num2; index2 <= num3; ++index2)
        {
          this._store.BaseStream.Seek(this._nameSectionOffset + (long) this.GetNamePosition(index2), SeekOrigin.Begin);
          if (this.CompareStringEqualsName(name))
          {
            int num4 = this._store.ReadInt32();
            if (num4 < 0 || (long) num4 >= this._store.BaseStream.Length - this._dataSectionOffset)
              throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", (object) num4));
            return num4;
          }
        }
      }
      return -1;
    }

    [SecuritySafeCritical]
    private unsafe bool CompareStringEqualsName(string name)
    {
      int byteLen = this._store.Read7BitEncodedInt();
      if (byteLen < 0)
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_NegativeStringLength"));
      if (this._ums != null)
      {
        byte* positionPointer = this._ums.PositionPointer;
        this._ums.Seek((long) byteLen, SeekOrigin.Current);
        if (this._ums.Position > this._ums.Length)
          throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesNameTooLong"));
        return FastResourceComparer.CompareOrdinal(positionPointer, byteLen, name) == 0;
      }
      byte[] numArray = new byte[byteLen];
      int count = byteLen;
      while (count > 0)
      {
        int num = this._store.Read(numArray, byteLen - count, count);
        if (num == 0)
          throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceNameCorrupted"));
        count -= num;
      }
      return FastResourceComparer.CompareOrdinal(numArray, byteLen / 2, name) == 0;
    }

    [SecurityCritical]
    private unsafe string AllocateStringForNameIndex(int index, out int dataOffset)
    {
      long namePosition = (long) this.GetNamePosition(index);
      int count1;
      byte[] numArray;
      lock (this)
      {
        this._store.BaseStream.Seek(namePosition + this._nameSectionOffset, SeekOrigin.Begin);
        count1 = this._store.Read7BitEncodedInt();
        if (count1 < 0)
          throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_NegativeStringLength"));
        if (this._ums != null)
        {
          if (this._ums.Position > this._ums.Length - (long) count1)
            throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesIndexTooLong", (object) index));
          string str = new string((char*) this._ums.PositionPointer, 0, count1 / 2);
          this._ums.Position += (long) count1;
          dataOffset = this._store.ReadInt32();
          if (dataOffset < 0 || (long) dataOffset >= this._store.BaseStream.Length - this._dataSectionOffset)
            throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", (object) dataOffset));
          return str;
        }
        numArray = new byte[count1];
        int count2 = count1;
        while (count2 > 0)
        {
          int num = this._store.Read(numArray, count1 - count2, count2);
          if (num == 0)
            throw new EndOfStreamException(Environment.GetResourceString("BadImageFormat_ResourceNameCorrupted_NameIndex", (object) index));
          count2 -= num;
        }
        dataOffset = this._store.ReadInt32();
        if (dataOffset >= 0)
        {
          if ((long) dataOffset < this._store.BaseStream.Length - this._dataSectionOffset)
            goto label_20;
        }
        throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", (object) dataOffset));
      }
label_20:
      return Encoding.Unicode.GetString(numArray, 0, count1);
    }

    private object GetValueForNameIndex(int index)
    {
      long namePosition = (long) this.GetNamePosition(index);
      lock (this)
      {
        this._store.BaseStream.Seek(namePosition + this._nameSectionOffset, SeekOrigin.Begin);
        this.SkipString();
        int pos = this._store.ReadInt32();
        if (pos < 0 || (long) pos >= this._store.BaseStream.Length - this._dataSectionOffset)
          throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", (object) pos));
        if (this._version == 1)
          return this.LoadObjectV1(pos);
        ResourceTypeCode typeCode;
        return this.LoadObjectV2(pos, out typeCode);
      }
    }

    internal string LoadString(int pos)
    {
      this._store.BaseStream.Seek(this._dataSectionOffset + (long) pos, SeekOrigin.Begin);
      string str = (string) null;
      int typeIndex = this._store.Read7BitEncodedInt();
      if (this._version == 1)
      {
        if (typeIndex == -1)
          return (string) null;
        if ((Type) this.FindType(typeIndex) != typeof (string))
          throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Type", (object) this.FindType(typeIndex).FullName));
        str = this._store.ReadString();
      }
      else
      {
        ResourceTypeCode resourceTypeCode = (ResourceTypeCode) typeIndex;
        switch (resourceTypeCode)
        {
          case ResourceTypeCode.Null:
          case ResourceTypeCode.String:
            if (resourceTypeCode == ResourceTypeCode.String)
            {
              str = this._store.ReadString();
              break;
            }
            break;
          default:
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Type", (object) (resourceTypeCode >= ResourceTypeCode.StartOfUserTypes ? this.FindType((int) (resourceTypeCode - 64)).FullName : resourceTypeCode.ToString())));
        }
      }
      return str;
    }

    internal object LoadObject(int pos)
    {
      if (this._version == 1)
        return this.LoadObjectV1(pos);
      ResourceTypeCode typeCode;
      return this.LoadObjectV2(pos, out typeCode);
    }

    internal object LoadObject(int pos, out ResourceTypeCode typeCode)
    {
      if (this._version != 1)
        return this.LoadObjectV2(pos, out typeCode);
      object obj = this.LoadObjectV1(pos);
      typeCode = obj is string ? ResourceTypeCode.String : ResourceTypeCode.StartOfUserTypes;
      return obj;
    }

    internal object LoadObjectV1(int pos)
    {
      try
      {
        return this._LoadObjectV1(pos);
      }
      catch (EndOfStreamException ex)
      {
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"), (Exception) ex);
      }
      catch (ArgumentOutOfRangeException ex)
      {
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"), (Exception) ex);
      }
    }

    [SecuritySafeCritical]
    private object _LoadObjectV1(int pos)
    {
      this._store.BaseStream.Seek(this._dataSectionOffset + (long) pos, SeekOrigin.Begin);
      int typeIndex = this._store.Read7BitEncodedInt();
      if (typeIndex == -1)
        return (object) null;
      RuntimeType type = this.FindType(typeIndex);
      if ((Type) type == typeof (string))
        return (object) this._store.ReadString();
      if ((Type) type == typeof (int))
        return (object) this._store.ReadInt32();
      if ((Type) type == typeof (byte))
        return (object) this._store.ReadByte();
      if ((Type) type == typeof (sbyte))
        return (object) this._store.ReadSByte();
      if ((Type) type == typeof (short))
        return (object) this._store.ReadInt16();
      if ((Type) type == typeof (long))
        return (object) this._store.ReadInt64();
      if ((Type) type == typeof (ushort))
        return (object) this._store.ReadUInt16();
      if ((Type) type == typeof (uint))
        return (object) this._store.ReadUInt32();
      if ((Type) type == typeof (ulong))
        return (object) this._store.ReadUInt64();
      if ((Type) type == typeof (float))
        return (object) this._store.ReadSingle();
      if ((Type) type == typeof (double))
        return (object) this._store.ReadDouble();
      if ((Type) type == typeof (DateTime))
        return (object) new DateTime(this._store.ReadInt64());
      if ((Type) type == typeof (TimeSpan))
        return (object) new TimeSpan(this._store.ReadInt64());
      if (!((Type) type == typeof (Decimal)))
        return this.DeserializeObject(typeIndex);
      int[] bits = new int[4];
      for (int index = 0; index < bits.Length; ++index)
        bits[index] = this._store.ReadInt32();
      return (object) new Decimal(bits);
    }

    internal object LoadObjectV2(int pos, out ResourceTypeCode typeCode)
    {
      try
      {
        return this._LoadObjectV2(pos, out typeCode);
      }
      catch (EndOfStreamException ex)
      {
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"), (Exception) ex);
      }
      catch (ArgumentOutOfRangeException ex)
      {
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"), (Exception) ex);
      }
    }

    [SecuritySafeCritical]
    private unsafe object _LoadObjectV2(int pos, out ResourceTypeCode typeCode)
    {
      this._store.BaseStream.Seek(this._dataSectionOffset + (long) pos, SeekOrigin.Begin);
      typeCode = (ResourceTypeCode) this._store.Read7BitEncodedInt();
      switch (typeCode)
      {
        case ResourceTypeCode.Null:
          return (object) null;
        case ResourceTypeCode.String:
          return (object) this._store.ReadString();
        case ResourceTypeCode.Boolean:
          return (object) this._store.ReadBoolean();
        case ResourceTypeCode.Char:
          return (object) (char) this._store.ReadUInt16();
        case ResourceTypeCode.Byte:
          return (object) this._store.ReadByte();
        case ResourceTypeCode.SByte:
          return (object) this._store.ReadSByte();
        case ResourceTypeCode.Int16:
          return (object) this._store.ReadInt16();
        case ResourceTypeCode.UInt16:
          return (object) this._store.ReadUInt16();
        case ResourceTypeCode.Int32:
          return (object) this._store.ReadInt32();
        case ResourceTypeCode.UInt32:
          return (object) this._store.ReadUInt32();
        case ResourceTypeCode.Int64:
          return (object) this._store.ReadInt64();
        case ResourceTypeCode.UInt64:
          return (object) this._store.ReadUInt64();
        case ResourceTypeCode.Single:
          return (object) this._store.ReadSingle();
        case ResourceTypeCode.Double:
          return (object) this._store.ReadDouble();
        case ResourceTypeCode.Decimal:
          return (object) this._store.ReadDecimal();
        case ResourceTypeCode.DateTime:
          return (object) DateTime.FromBinary(this._store.ReadInt64());
        case ResourceTypeCode.TimeSpan:
          return (object) new TimeSpan(this._store.ReadInt64());
        case ResourceTypeCode.ByteArray:
          int count1 = this._store.ReadInt32();
          if (count1 < 0)
            throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", (object) count1));
          if (this._ums == null)
          {
            if ((long) count1 > this._store.BaseStream.Length)
              throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", (object) count1));
            return (object) this._store.ReadBytes(count1);
          }
          if ((long) count1 > this._ums.Length - this._ums.Position)
            throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", (object) count1));
          byte[] buffer = new byte[count1];
          this._ums.Read(buffer, 0, count1);
          return (object) buffer;
        case ResourceTypeCode.Stream:
          int count2 = this._store.ReadInt32();
          if (count2 < 0)
            throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", (object) count2));
          if (this._ums == null)
            return (object) new PinnedBufferMemoryStream(this._store.ReadBytes(count2));
          if ((long) count2 > this._ums.Length - this._ums.Position)
            throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourceDataLengthInvalid", (object) count2));
          return (object) new UnmanagedMemoryStream(this._ums.PositionPointer, (long) count2, (long) count2, FileAccess.Read, true);
        default:
          if (typeCode < ResourceTypeCode.StartOfUserTypes)
            throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_TypeMismatch"));
          return this.DeserializeObject((int) (typeCode - 64));
      }
    }

    [SecurityCritical]
    private object DeserializeObject(int typeIndex)
    {
      RuntimeType type = this.FindType(typeIndex);
      if (this._safeToDeserialize == null)
        this.InitSafeToDeserializeArray();
      object obj;
      if (this._safeToDeserialize[typeIndex])
      {
        this._objFormatter.Binder = (SerializationBinder) this._typeLimitingBinder;
        this._typeLimitingBinder.ExpectingToDeserialize(type);
        obj = this._objFormatter.UnsafeDeserialize(this._store.BaseStream, (HeaderHandler) null);
      }
      else
      {
        this._objFormatter.Binder = (SerializationBinder) null;
        obj = this._objFormatter.Deserialize(this._store.BaseStream);
      }
      if (obj.GetType() != (Type) type)
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResType&SerBlobMismatch", (object) type.FullName, (object) obj.GetType().FullName));
      return obj;
    }

    [SecurityCritical]
    private void ReadResources()
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter((ISurrogateSelector) null, new StreamingContext(StreamingContextStates.File | StreamingContextStates.Persistence));
      this._typeLimitingBinder = new ResourceReader.TypeLimitingDeserializationBinder();
      binaryFormatter.Binder = (SerializationBinder) this._typeLimitingBinder;
      this._objFormatter = binaryFormatter;
      try
      {
        this._ReadResources();
      }
      catch (EndOfStreamException ex)
      {
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"), (Exception) ex);
      }
      catch (IndexOutOfRangeException ex)
      {
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"), (Exception) ex);
      }
    }

    [SecurityCritical]
    private unsafe void _ReadResources()
    {
      if (this._store.ReadInt32() != ResourceManager.MagicNumber)
        throw new ArgumentException(Environment.GetResourceString("Resources_StreamNotValid"));
      int num1 = this._store.ReadInt32();
      int num2 = this._store.ReadInt32();
      if (num2 < 0 || num1 < 0)
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
      if (num1 > 1)
      {
        this._store.BaseStream.Seek((long) num2, SeekOrigin.Current);
      }
      else
      {
        string asmTypeName1 = this._store.ReadString();
        AssemblyName asmName2 = new AssemblyName(ResourceManager.MscorlibName);
        if (!ResourceManager.CompareNames(asmTypeName1, ResourceManager.ResReaderTypeName, asmName2))
          throw new NotSupportedException(Environment.GetResourceString("NotSupported_WrongResourceReader_Type", (object) asmTypeName1));
        this.SkipString();
      }
      int num3 = this._store.ReadInt32();
      switch (num3)
      {
        case 1:
        case 2:
          this._version = num3;
          this._numResources = this._store.ReadInt32();
          if (this._numResources < 0)
            throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
          int length = this._store.ReadInt32();
          if (length < 0)
            throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
          this._typeTable = new RuntimeType[length];
          this._typeNamePositions = new int[length];
          for (int index = 0; index < length; ++index)
          {
            this._typeNamePositions[index] = (int) this._store.BaseStream.Position;
            this.SkipString();
          }
          int num4 = (int) this._store.BaseStream.Position & 7;
          if (num4 != 0)
          {
            for (int index = 0; index < 8 - num4; ++index)
            {
              int num5 = (int) this._store.ReadByte();
            }
          }
          if (this._ums == null)
          {
            this._nameHashes = new int[this._numResources];
            for (int index = 0; index < this._numResources; ++index)
              this._nameHashes[index] = this._store.ReadInt32();
          }
          else
          {
            if (((long) this._numResources & 3758096384L) != 0L)
              throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
            int num5 = 4 * this._numResources;
            this._nameHashesPtr = (int*) this._ums.PositionPointer;
            this._ums.Seek((long) num5, SeekOrigin.Current);
            byte* positionPointer = this._ums.PositionPointer;
          }
          if (this._ums == null)
          {
            this._namePositions = new int[this._numResources];
            for (int index = 0; index < this._numResources; ++index)
            {
              int num5 = this._store.ReadInt32();
              if (num5 < 0)
                throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
              this._namePositions[index] = num5;
            }
          }
          else
          {
            if (((long) this._numResources & 3758096384L) != 0L)
              throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
            int num5 = 4 * this._numResources;
            this._namePositionsPtr = (int*) this._ums.PositionPointer;
            this._ums.Seek((long) num5, SeekOrigin.Current);
            byte* positionPointer = this._ums.PositionPointer;
          }
          this._dataSectionOffset = (long) this._store.ReadInt32();
          if (this._dataSectionOffset < 0L)
            throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
          this._nameSectionOffset = this._store.BaseStream.Position;
          if (this._dataSectionOffset >= this._nameSectionOffset)
            break;
          throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResourcesHeaderCorrupted"));
        default:
          throw new ArgumentException(Environment.GetResourceString("Arg_ResourceFileUnsupportedVersion", (object) 2, (object) num3));
      }
    }

    private RuntimeType FindType(int typeIndex)
    {
      if (typeIndex < 0 || typeIndex >= this._typeTable.Length)
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_InvalidType"));
      if (this._typeTable[typeIndex] == (RuntimeType) null)
      {
        long position = this._store.BaseStream.Position;
        try
        {
          this._store.BaseStream.Position = (long) this._typeNamePositions[typeIndex];
          string typeName = this._store.ReadString();
          this._typeTable[typeIndex] = (RuntimeType) Type.GetType(typeName, true);
        }
        finally
        {
          this._store.BaseStream.Position = position;
        }
      }
      return this._typeTable[typeIndex];
    }

    [SecurityCritical]
    private void InitSafeToDeserializeArray()
    {
      this._safeToDeserialize = new bool[this._typeTable.Length];
      for (int index = 0; index < this._typeTable.Length; ++index)
      {
        long position = this._store.BaseStream.Position;
        string typeName;
        try
        {
          this._store.BaseStream.Position = (long) this._typeNamePositions[index];
          typeName = this._store.ReadString();
        }
        finally
        {
          this._store.BaseStream.Position = position;
        }
        RuntimeType type = (RuntimeType) Type.GetType(typeName, false);
        AssemblyName asmName2;
        string typeName2;
        if (type == (RuntimeType) null)
        {
          asmName2 = (AssemblyName) null;
          typeName2 = typeName;
        }
        else
        {
          if (type.BaseType == typeof (Enum))
          {
            this._safeToDeserialize[index] = true;
            continue;
          }
          typeName2 = type.FullName;
          asmName2 = new AssemblyName();
          RuntimeAssembly assembly = (RuntimeAssembly) type.Assembly;
          asmName2.Init(assembly.GetSimpleName(), assembly.GetPublicKey(), (byte[]) null, (Version) null, assembly.GetLocale(), AssemblyHashAlgorithm.None, AssemblyVersionCompatibility.SameMachine, (string) null, AssemblyNameFlags.PublicKey, (StrongNameKeyPair) null);
        }
        foreach (string asmTypeName1 in ResourceReader.TypesSafeForDeserialization)
        {
          if (ResourceManager.CompareNames(asmTypeName1, typeName2, asmName2))
            this._safeToDeserialize[index] = true;
        }
      }
    }

    /// <summary>
    ///   Возвращает имя типа и данные именованного ресурса из открытого файла ресурсов или потока.
    /// </summary>
    /// <param name="resourceName">Имя ресурса.</param>
    /// <param name="resourceType">
    ///   Когда выполнение этого метода завершается, содержит строку, представляющую имя типа извлеченного ресурса (подробные сведения см. в разделе "Комментарии").
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <param name="resourceData">
    ///   Когда этот метод возвращает значение, оно содержит массив байтов, являющийся двоичным представлением извлеченного типа.
    ///    Этот параметр передается неинициализированным.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    ///   Свойство <paramref name="resourceName" /> имеет значение <see langword="null" />.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    ///   <paramref name="resourceName" /> не существует.
    /// </exception>
    /// <exception cref="T:System.BadImageFormatException">
    ///   <paramref name="resourceName" /> имеет недопустимый тип.
    /// </exception>
    /// <exception cref="T:System.FormatException">
    ///   Извлеченные данные ресурсов повреждены.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    ///   Текущий <see cref="T:System.Resources.ResourceReader" /> объект не инициализирован, возможно, потому что он закрыт.
    /// </exception>
    public void GetResourceData(string resourceName, out string resourceType, out byte[] resourceData)
    {
      if (resourceName == null)
        throw new ArgumentNullException(nameof (resourceName));
      if (this._resCache == null)
        throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
      int[] array = new int[this._numResources];
      int posForResource = this.FindPosForResource(resourceName);
      if (posForResource == -1)
        throw new ArgumentException(Environment.GetResourceString("Arg_ResourceNameNotExist", (object) resourceName));
      lock (this)
      {
        for (int index = 0; index < this._numResources; ++index)
        {
          this._store.BaseStream.Position = this._nameSectionOffset + (long) this.GetNamePosition(index);
          int num1 = this._store.Read7BitEncodedInt();
          if (num1 < 0)
            throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesNameInvalidOffset", (object) num1));
          this._store.BaseStream.Position += (long) num1;
          int num2 = this._store.ReadInt32();
          if (num2 < 0 || (long) num2 >= this._store.BaseStream.Length - this._dataSectionOffset)
            throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourcesDataInvalidOffset", (object) num2));
          array[index] = num2;
        }
        Array.Sort<int>(array);
        int num3 = Array.BinarySearch<int>(array, posForResource);
        int num4 = (int) ((num3 < this._numResources - 1 ? (long) array[num3 + 1] + this._dataSectionOffset : this._store.BaseStream.Length) - ((long) posForResource + this._dataSectionOffset));
        this._store.BaseStream.Position = this._dataSectionOffset + (long) posForResource;
        ResourceTypeCode typeCode = (ResourceTypeCode) this._store.Read7BitEncodedInt();
        if (typeCode < ResourceTypeCode.Null || typeCode >= (ResourceTypeCode) (64 + this._typeTable.Length))
          throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_InvalidType"));
        resourceType = this.TypeNameFromTypeCode(typeCode);
        int count = num4 - (int) (this._store.BaseStream.Position - (this._dataSectionOffset + (long) posForResource));
        byte[] numArray = this._store.ReadBytes(count);
        if (numArray.Length != count)
          throw new FormatException(Environment.GetResourceString("BadImageFormat_ResourceNameCorrupted"));
        resourceData = numArray;
      }
    }

    private string TypeNameFromTypeCode(ResourceTypeCode typeCode)
    {
      if (typeCode < ResourceTypeCode.StartOfUserTypes)
        return "ResourceTypeCode." + typeCode.ToString();
      int index = (int) (typeCode - 64);
      long position = this._store.BaseStream.Position;
      try
      {
        this._store.BaseStream.Position = (long) this._typeNamePositions[index];
        return this._store.ReadString();
      }
      finally
      {
        this._store.BaseStream.Position = position;
      }
    }

    internal sealed class TypeLimitingDeserializationBinder : SerializationBinder
    {
      private RuntimeType _typeToDeserialize;
      private ObjectReader _objectReader;

      internal ObjectReader ObjectReader
      {
        get
        {
          return this._objectReader;
        }
        set
        {
          this._objectReader = value;
        }
      }

      internal void ExpectingToDeserialize(RuntimeType type)
      {
        this._typeToDeserialize = type;
      }

      [SecuritySafeCritical]
      public override Type BindToType(string assemblyName, string typeName)
      {
        AssemblyName asmName2 = new AssemblyName(assemblyName);
        bool flag = false;
        foreach (string asmTypeName1 in ResourceReader.TypesSafeForDeserialization)
        {
          if (ResourceManager.CompareNames(asmTypeName1, typeName, asmName2))
          {
            flag = true;
            break;
          }
        }
        if (this.ObjectReader.FastBindToType(assemblyName, typeName).IsEnum)
          flag = true;
        if (flag)
          return (Type) null;
        throw new BadImageFormatException(Environment.GetResourceString("BadImageFormat_ResType&SerBlobMismatch", (object) this._typeToDeserialize.FullName, (object) typeName));
      }
    }

    internal sealed class ResourceEnumerator : IDictionaryEnumerator, IEnumerator
    {
      private const int ENUM_DONE = -2147483648;
      private const int ENUM_NOT_STARTED = -1;
      private ResourceReader _reader;
      private bool _currentIsValid;
      private int _currentName;
      private int _dataPosition;

      internal ResourceEnumerator(ResourceReader reader)
      {
        this._currentName = -1;
        this._reader = reader;
        this._dataPosition = -2;
      }

      public bool MoveNext()
      {
        if (this._currentName == this._reader._numResources - 1 || this._currentName == int.MinValue)
        {
          this._currentIsValid = false;
          this._currentName = int.MinValue;
          return false;
        }
        this._currentIsValid = true;
        ++this._currentName;
        return true;
      }

      public object Key
      {
        [SecuritySafeCritical] get
        {
          if (this._currentName == int.MinValue)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
          if (!this._currentIsValid)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          if (this._reader._resCache == null)
            throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
          return (object) this._reader.AllocateStringForNameIndex(this._currentName, out this._dataPosition);
        }
      }

      public object Current
      {
        get
        {
          return (object) this.Entry;
        }
      }

      internal int DataPosition
      {
        get
        {
          return this._dataPosition;
        }
      }

      public DictionaryEntry Entry
      {
        [SecuritySafeCritical] get
        {
          if (this._currentName == int.MinValue)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
          if (!this._currentIsValid)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          if (this._reader._resCache == null)
            throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
          object obj = (object) null;
          string key;
          lock (this._reader)
          {
            lock (this._reader._resCache)
            {
              key = this._reader.AllocateStringForNameIndex(this._currentName, out this._dataPosition);
              ResourceLocator resourceLocator;
              if (this._reader._resCache.TryGetValue(key, out resourceLocator))
                obj = resourceLocator.Value;
              if (obj == null)
                obj = this._dataPosition != -1 ? this._reader.LoadObject(this._dataPosition) : this._reader.GetValueForNameIndex(this._currentName);
            }
          }
          return new DictionaryEntry((object) key, obj);
        }
      }

      public object Value
      {
        get
        {
          if (this._currentName == int.MinValue)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumEnded"));
          if (!this._currentIsValid)
            throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_EnumNotStarted"));
          if (this._reader._resCache == null)
            throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
          return this._reader.GetValueForNameIndex(this._currentName);
        }
      }

      public void Reset()
      {
        if (this._reader._resCache == null)
          throw new InvalidOperationException(Environment.GetResourceString("ResourceReaderIsClosed"));
        this._currentIsValid = false;
        this._currentName = -1;
      }
    }
  }
}

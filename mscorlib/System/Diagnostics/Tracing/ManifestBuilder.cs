// Decompiled with JetBrains decompiler
// Type: System.Diagnostics.Tracing.ManifestBuilder
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using Microsoft.Reflection;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text;

namespace System.Diagnostics.Tracing
{
  internal class ManifestBuilder
  {
    private ulong nextChannelKeywordBit = 9223372036854775808;
    private Dictionary<int, string> opcodeTab;
    private Dictionary<int, string> taskTab;
    private Dictionary<int, ManifestBuilder.ChannelInfo> channelTab;
    private Dictionary<ulong, string> keywordTab;
    private Dictionary<string, Type> mapsTab;
    private Dictionary<string, string> stringTab;
    private const int MaxCountChannels = 8;
    private StringBuilder sb;
    private StringBuilder events;
    private StringBuilder templates;
    private string providerName;
    private ResourceManager resources;
    private EventManifestOptions flags;
    private IList<string> errors;
    private Dictionary<string, List<int>> perEventByteArrayArgIndices;
    private string eventName;
    private int numParams;
    private List<int> byteArrArgIndices;

    public ManifestBuilder(string providerName, Guid providerGuid, string dllName, ResourceManager resources, EventManifestOptions flags)
    {
      this.providerName = providerName;
      this.flags = flags;
      this.resources = resources;
      this.sb = new StringBuilder();
      this.events = new StringBuilder();
      this.templates = new StringBuilder();
      this.opcodeTab = new Dictionary<int, string>();
      this.stringTab = new Dictionary<string, string>();
      this.errors = (IList<string>) new List<string>();
      this.perEventByteArrayArgIndices = new Dictionary<string, List<int>>();
      this.sb.AppendLine("<instrumentationManifest xmlns=\"http://schemas.microsoft.com/win/2004/08/events\">");
      this.sb.AppendLine(" <instrumentation xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:win=\"http://manifests.microsoft.com/win/2004/08/windows/events\">");
      this.sb.AppendLine("  <events xmlns=\"http://schemas.microsoft.com/win/2004/08/events\">");
      this.sb.Append("<provider name=\"").Append(providerName).Append("\" guid=\"{").Append(providerGuid.ToString()).Append("}");
      if (dllName != null)
        this.sb.Append("\" resourceFileName=\"").Append(dllName).Append("\" messageFileName=\"").Append(dllName);
      this.sb.Append("\" symbol=\"").Append(providerName.Replace("-", "").Replace(".", "_"));
      this.sb.Append("\">").AppendLine();
    }

    public void AddOpcode(string name, int value)
    {
      if ((this.flags & EventManifestOptions.Strict) != EventManifestOptions.None)
      {
        if (value <= 10 || value >= 239)
          this.ManifestError(Environment.GetResourceString("EventSource_IllegalOpcodeValue", (object) name, (object) value), false);
        string str;
        if (this.opcodeTab.TryGetValue(value, out str) && !name.Equals(str, StringComparison.Ordinal))
          this.ManifestError(Environment.GetResourceString("EventSource_OpcodeCollision", (object) name, (object) str, (object) value), false);
      }
      this.opcodeTab[value] = name;
    }

    public void AddTask(string name, int value)
    {
      if ((this.flags & EventManifestOptions.Strict) != EventManifestOptions.None)
      {
        if (value <= 0 || value >= (int) ushort.MaxValue)
          this.ManifestError(Environment.GetResourceString("EventSource_IllegalTaskValue", (object) name, (object) value), false);
        string str;
        if (this.taskTab != null && this.taskTab.TryGetValue(value, out str) && !name.Equals(str, StringComparison.Ordinal))
          this.ManifestError(Environment.GetResourceString("EventSource_TaskCollision", (object) name, (object) str, (object) value), false);
      }
      if (this.taskTab == null)
        this.taskTab = new Dictionary<int, string>();
      this.taskTab[value] = name;
    }

    public void AddKeyword(string name, ulong value)
    {
      if (((long) value & (long) value - 1L) != 0L)
        this.ManifestError(Environment.GetResourceString("EventSource_KeywordNeedPowerOfTwo", (object) ("0x" + value.ToString("x", (IFormatProvider) CultureInfo.CurrentCulture)), (object) name), true);
      if ((this.flags & EventManifestOptions.Strict) != EventManifestOptions.None)
      {
        if (value >= 17592186044416UL && !name.StartsWith("Session", StringComparison.Ordinal))
          this.ManifestError(Environment.GetResourceString("EventSource_IllegalKeywordsValue", (object) name, (object) ("0x" + value.ToString("x", (IFormatProvider) CultureInfo.CurrentCulture))), false);
        string str;
        if (this.keywordTab != null && this.keywordTab.TryGetValue(value, out str) && !name.Equals(str, StringComparison.Ordinal))
          this.ManifestError(Environment.GetResourceString("EventSource_KeywordCollision", (object) name, (object) str, (object) ("0x" + value.ToString("x", (IFormatProvider) CultureInfo.CurrentCulture))), false);
      }
      if (this.keywordTab == null)
        this.keywordTab = new Dictionary<ulong, string>();
      this.keywordTab[value] = name;
    }

    public void AddChannel(string name, int value, EventChannelAttribute channelAttribute)
    {
      EventChannel channel = (EventChannel) value;
      if (value < 16 || value > (int) byte.MaxValue)
        this.ManifestError(Environment.GetResourceString("EventSource_EventChannelOutOfRange", (object) name, (object) value), false);
      else if (channel >= EventChannel.Admin && channel <= EventChannel.Debug && (channelAttribute != null && this.EventChannelToChannelType(channel) != channelAttribute.EventChannelType))
        this.ManifestError(Environment.GetResourceString("EventSource_ChannelTypeDoesNotMatchEventChannelValue", (object) name, (object) ((EventChannel) value).ToString()), false);
      ulong channelKeyword = this.GetChannelKeyword(channel);
      if (this.channelTab == null)
        this.channelTab = new Dictionary<int, ManifestBuilder.ChannelInfo>(4);
      this.channelTab[value] = new ManifestBuilder.ChannelInfo()
      {
        Name = name,
        Keywords = channelKeyword,
        Attribs = channelAttribute
      };
    }

    private EventChannelType EventChannelToChannelType(EventChannel channel)
    {
      return (EventChannelType) (channel - (byte) 16 + (byte) 1);
    }

    private EventChannelAttribute GetDefaultChannelAttribute(EventChannel channel)
    {
      EventChannelAttribute channelAttribute = new EventChannelAttribute();
      channelAttribute.EventChannelType = this.EventChannelToChannelType(channel);
      if (channelAttribute.EventChannelType <= EventChannelType.Operational)
        channelAttribute.Enabled = true;
      return channelAttribute;
    }

    public ulong[] GetChannelData()
    {
      if (this.channelTab == null)
        return new ulong[0];
      int num = -1;
      foreach (int key in this.channelTab.Keys)
      {
        if (key > num)
          num = key;
      }
      ulong[] numArray = new ulong[num + 1];
      foreach (KeyValuePair<int, ManifestBuilder.ChannelInfo> keyValuePair in this.channelTab)
        numArray[keyValuePair.Key] = keyValuePair.Value.Keywords;
      return numArray;
    }

    public void StartEvent(string eventName, EventAttribute eventAttribute)
    {
      this.eventName = eventName;
      this.numParams = 0;
      this.byteArrArgIndices = (List<int>) null;
      this.events.Append("  <event").Append(" value=\"").Append(eventAttribute.EventId).Append("\"").Append(" version=\"").Append(eventAttribute.Version).Append("\"").Append(" level=\"").Append(ManifestBuilder.GetLevelName(eventAttribute.Level)).Append("\"").Append(" symbol=\"").Append(eventName).Append("\"");
      this.WriteMessageAttrib(this.events, "event", eventName, eventAttribute.Message);
      if (eventAttribute.Keywords != EventKeywords.None)
        this.events.Append(" keywords=\"").Append(this.GetKeywords((ulong) eventAttribute.Keywords, eventName)).Append("\"");
      if (eventAttribute.Opcode != EventOpcode.Info)
        this.events.Append(" opcode=\"").Append(this.GetOpcodeName(eventAttribute.Opcode, eventName)).Append("\"");
      if (eventAttribute.Task != EventTask.None)
        this.events.Append(" task=\"").Append(this.GetTaskName(eventAttribute.Task, eventName)).Append("\"");
      if (eventAttribute.Channel == EventChannel.None)
        return;
      this.events.Append(" channel=\"").Append(this.GetChannelName(eventAttribute.Channel, eventName, eventAttribute.Message)).Append("\"");
    }

    public void AddEventParameter(Type type, string name)
    {
      if (this.numParams == 0)
        this.templates.Append("  <template tid=\"").Append(this.eventName).Append("Args\">").AppendLine();
      if (type == typeof (byte[]))
      {
        if (this.byteArrArgIndices == null)
          this.byteArrArgIndices = new List<int>(4);
        this.byteArrArgIndices.Add(this.numParams);
        ++this.numParams;
        this.templates.Append("   <data name=\"").Append(name).Append("Size\" inType=\"win:UInt32\"/>").AppendLine();
      }
      ++this.numParams;
      this.templates.Append("   <data name=\"").Append(name).Append("\" inType=\"").Append(this.GetTypeName(type)).Append("\"");
      if ((type.IsArray || type.IsPointer) && type.GetElementType() == typeof (byte))
        this.templates.Append(" length=\"").Append(name).Append("Size\"");
      if (type.IsEnum() && Enum.GetUnderlyingType(type) != typeof (ulong) && Enum.GetUnderlyingType(type) != typeof (long))
      {
        this.templates.Append(" map=\"").Append(type.Name).Append("\"");
        if (this.mapsTab == null)
          this.mapsTab = new Dictionary<string, Type>();
        if (!this.mapsTab.ContainsKey(type.Name))
          this.mapsTab.Add(type.Name, type);
      }
      this.templates.Append("/>").AppendLine();
    }

    public void EndEvent()
    {
      if (this.numParams > 0)
      {
        this.templates.Append("  </template>").AppendLine();
        this.events.Append(" template=\"").Append(this.eventName).Append("Args\"");
      }
      this.events.Append("/>").AppendLine();
      if (this.byteArrArgIndices != null)
        this.perEventByteArrayArgIndices[this.eventName] = this.byteArrArgIndices;
      string eventMessage;
      if (this.stringTab.TryGetValue("event_" + this.eventName, out eventMessage))
        this.stringTab["event_" + this.eventName] = this.TranslateToManifestConvention(eventMessage, this.eventName);
      this.eventName = (string) null;
      this.numParams = 0;
      this.byteArrArgIndices = (List<int>) null;
    }

    public ulong GetChannelKeyword(EventChannel channel)
    {
      if (this.channelTab == null)
        this.channelTab = new Dictionary<int, ManifestBuilder.ChannelInfo>(4);
      if (this.channelTab.Count == 8)
        this.ManifestError(Environment.GetResourceString("EventSource_MaxChannelExceeded"), false);
      ManifestBuilder.ChannelInfo channelInfo;
      ulong num;
      if (!this.channelTab.TryGetValue((int) channel, out channelInfo))
      {
        num = this.nextChannelKeywordBit;
        this.nextChannelKeywordBit >>= 1;
      }
      else
        num = channelInfo.Keywords;
      return num;
    }

    public byte[] CreateManifest()
    {
      return Encoding.UTF8.GetBytes(this.CreateManifestString());
    }

    public IList<string> Errors
    {
      get
      {
        return this.errors;
      }
    }

    public void ManifestError(string msg, bool runtimeCritical = false)
    {
      if ((this.flags & EventManifestOptions.Strict) != EventManifestOptions.None)
        this.errors.Add(msg);
      else if (runtimeCritical)
        throw new ArgumentException(msg);
    }

    private string CreateManifestString()
    {
      if (this.channelTab != null)
      {
        this.sb.Append(" <channels>").AppendLine();
        List<KeyValuePair<int, ManifestBuilder.ChannelInfo>> keyValuePairList = new List<KeyValuePair<int, ManifestBuilder.ChannelInfo>>();
        foreach (KeyValuePair<int, ManifestBuilder.ChannelInfo> keyValuePair in this.channelTab)
          keyValuePairList.Add(keyValuePair);
        keyValuePairList.Sort((Comparison<KeyValuePair<int, ManifestBuilder.ChannelInfo>>) ((p1, p2) => -Comparer<ulong>.Default.Compare(p1.Value.Keywords, p2.Value.Keywords)));
        foreach (KeyValuePair<int, ManifestBuilder.ChannelInfo> keyValuePair in keyValuePairList)
        {
          int key = keyValuePair.Key;
          ManifestBuilder.ChannelInfo channelInfo = keyValuePair.Value;
          string str1 = (string) null;
          string str2 = "channel";
          bool flag = false;
          string str3 = (string) null;
          if (channelInfo.Attribs != null)
          {
            EventChannelAttribute attribs = channelInfo.Attribs;
            if (Enum.IsDefined(typeof (EventChannelType), (object) attribs.EventChannelType))
              str1 = attribs.EventChannelType.ToString();
            flag = attribs.Enabled;
          }
          if (str3 == null)
            str3 = this.providerName + "/" + channelInfo.Name;
          this.sb.Append("  <").Append(str2);
          this.sb.Append(" chid=\"").Append(channelInfo.Name).Append("\"");
          this.sb.Append(" name=\"").Append(str3).Append("\"");
          if (str2 == "channel")
          {
            this.WriteMessageAttrib(this.sb, "channel", channelInfo.Name, (string) null);
            this.sb.Append(" value=\"").Append(key).Append("\"");
            if (str1 != null)
              this.sb.Append(" type=\"").Append(str1).Append("\"");
            this.sb.Append(" enabled=\"").Append(flag.ToString().ToLower()).Append("\"");
          }
          this.sb.Append("/>").AppendLine();
        }
        this.sb.Append(" </channels>").AppendLine();
      }
      if (this.taskTab != null)
      {
        this.sb.Append(" <tasks>").AppendLine();
        List<int> intList = new List<int>((IEnumerable<int>) this.taskTab.Keys);
        intList.Sort();
        foreach (int index in intList)
        {
          this.sb.Append("  <task");
          this.WriteNameAndMessageAttribs(this.sb, "task", this.taskTab[index]);
          this.sb.Append(" value=\"").Append(index).Append("\"/>").AppendLine();
        }
        this.sb.Append(" </tasks>").AppendLine();
      }
      if (this.mapsTab != null)
      {
        this.sb.Append(" <maps>").AppendLine();
        foreach (Type type in this.mapsTab.Values)
        {
          bool flag = EventSource.GetCustomAttributeHelper((MemberInfo) type, typeof (FlagsAttribute), this.flags) != null;
          string str = flag ? "bitMap" : "valueMap";
          this.sb.Append("  <").Append(str).Append(" name=\"").Append(type.Name).Append("\">").AppendLine();
          foreach (FieldInfo field in type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public))
          {
            object rawConstantValue = field.GetRawConstantValue();
            if (rawConstantValue != null)
            {
              long num;
              if (rawConstantValue is int)
                num = (long) (int) rawConstantValue;
              else if (rawConstantValue is long)
                num = (long) rawConstantValue;
              else
                continue;
              if (!flag || (num & num - 1L) == 0L && num != 0L)
              {
                this.sb.Append("   <map value=\"0x").Append(num.ToString("x", (IFormatProvider) CultureInfo.InvariantCulture)).Append("\"");
                this.WriteMessageAttrib(this.sb, "map", type.Name + "." + field.Name, field.Name);
                this.sb.Append("/>").AppendLine();
              }
            }
          }
          this.sb.Append("  </").Append(str).Append(">").AppendLine();
        }
        this.sb.Append(" </maps>").AppendLine();
      }
      this.sb.Append(" <opcodes>").AppendLine();
      List<int> intList1 = new List<int>((IEnumerable<int>) this.opcodeTab.Keys);
      intList1.Sort();
      foreach (int index in intList1)
      {
        this.sb.Append("  <opcode");
        this.WriteNameAndMessageAttribs(this.sb, "opcode", this.opcodeTab[index]);
        this.sb.Append(" value=\"").Append(index).Append("\"/>").AppendLine();
      }
      this.sb.Append(" </opcodes>").AppendLine();
      if (this.keywordTab != null)
      {
        this.sb.Append(" <keywords>").AppendLine();
        List<ulong> ulongList = new List<ulong>((IEnumerable<ulong>) this.keywordTab.Keys);
        ulongList.Sort();
        foreach (ulong index in ulongList)
        {
          this.sb.Append("  <keyword");
          this.WriteNameAndMessageAttribs(this.sb, "keyword", this.keywordTab[index]);
          this.sb.Append(" mask=\"0x").Append(index.ToString("x", (IFormatProvider) CultureInfo.InvariantCulture)).Append("\"/>").AppendLine();
        }
        this.sb.Append(" </keywords>").AppendLine();
      }
      this.sb.Append(" <events>").AppendLine();
      this.sb.Append((object) this.events);
      this.sb.Append(" </events>").AppendLine();
      this.sb.Append(" <templates>").AppendLine();
      if (this.templates.Length > 0)
        this.sb.Append((object) this.templates);
      else
        this.sb.Append("    <template tid=\"_empty\"></template>").AppendLine();
      this.sb.Append(" </templates>").AppendLine();
      this.sb.Append("</provider>").AppendLine();
      this.sb.Append("</events>").AppendLine();
      this.sb.Append("</instrumentation>").AppendLine();
      this.sb.Append("<localization>").AppendLine();
      List<CultureInfo> cultureInfoList;
      if (this.resources != null && (this.flags & EventManifestOptions.AllCultures) != EventManifestOptions.None)
      {
        cultureInfoList = ManifestBuilder.GetSupportedCultures(this.resources);
      }
      else
      {
        cultureInfoList = new List<CultureInfo>();
        cultureInfoList.Add(CultureInfo.CurrentUICulture);
      }
      string[] strArray = new string[this.stringTab.Keys.Count];
      this.stringTab.Keys.CopyTo(strArray, 0);
      ArraySortHelper<string>.IntrospectiveSort(strArray, 0, strArray.Length, (IComparer<string>) Comparer<string>.Default);
      foreach (CultureInfo ci in cultureInfoList)
      {
        this.sb.Append(" <resources culture=\"").Append(ci.Name).Append("\">").AppendLine();
        this.sb.Append("  <stringTable>").AppendLine();
        foreach (string key in strArray)
        {
          string localizedMessage = this.GetLocalizedMessage(key, ci, true);
          this.sb.Append("   <string id=\"").Append(key).Append("\" value=\"").Append(localizedMessage).Append("\"/>").AppendLine();
        }
        this.sb.Append("  </stringTable>").AppendLine();
        this.sb.Append(" </resources>").AppendLine();
      }
      this.sb.Append("</localization>").AppendLine();
      this.sb.AppendLine("</instrumentationManifest>");
      return this.sb.ToString();
    }

    private void WriteNameAndMessageAttribs(StringBuilder stringBuilder, string elementName, string name)
    {
      stringBuilder.Append(" name=\"").Append(name).Append("\"");
      this.WriteMessageAttrib(this.sb, elementName, name, name);
    }

    private void WriteMessageAttrib(StringBuilder stringBuilder, string elementName, string name, string value)
    {
      string index = elementName + "_" + name;
      if (this.resources != null)
      {
        string str = this.resources.GetString(index, CultureInfo.InvariantCulture);
        if (str != null)
          value = str;
      }
      if (value == null)
        return;
      stringBuilder.Append(" message=\"$(string.").Append(index).Append(")\"");
      string str1;
      if (this.stringTab.TryGetValue(index, out str1) && !str1.Equals(value))
        this.ManifestError(Environment.GetResourceString("EventSource_DuplicateStringKey", (object) index), true);
      else
        this.stringTab[index] = value;
    }

    internal string GetLocalizedMessage(string key, CultureInfo ci, bool etwFormat)
    {
      string eventMessage = (string) null;
      if (this.resources != null)
      {
        string str = this.resources.GetString(key, ci);
        if (str != null)
        {
          eventMessage = str;
          if (etwFormat && key.StartsWith("event_"))
          {
            string evtName = key.Substring("event_".Length);
            eventMessage = this.TranslateToManifestConvention(eventMessage, evtName);
          }
        }
      }
      if (etwFormat && eventMessage == null)
        this.stringTab.TryGetValue(key, out eventMessage);
      return eventMessage;
    }

    private static List<CultureInfo> GetSupportedCultures(ResourceManager resources)
    {
      List<CultureInfo> cultureInfoList = new List<CultureInfo>();
      foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
      {
        if (resources.GetResourceSet(culture, true, false) != null)
          cultureInfoList.Add(culture);
      }
      if (!cultureInfoList.Contains(CultureInfo.CurrentUICulture))
        cultureInfoList.Insert(0, CultureInfo.CurrentUICulture);
      return cultureInfoList;
    }

    private static string GetLevelName(EventLevel level)
    {
      return (level >= (EventLevel) 16 ? "" : "win:") + level.ToString();
    }

    private string GetChannelName(EventChannel channel, string eventName, string eventMessage)
    {
      ManifestBuilder.ChannelInfo channelInfo = (ManifestBuilder.ChannelInfo) null;
      if (this.channelTab == null || !this.channelTab.TryGetValue((int) channel, out channelInfo))
      {
        if (channel < EventChannel.Admin)
          this.ManifestError(Environment.GetResourceString("EventSource_UndefinedChannel", (object) channel, (object) eventName), false);
        if (this.channelTab == null)
          this.channelTab = new Dictionary<int, ManifestBuilder.ChannelInfo>(4);
        string name = channel.ToString();
        if (EventChannel.Debug < channel)
          name = "Channel" + name;
        this.AddChannel(name, (int) channel, this.GetDefaultChannelAttribute(channel));
        if (!this.channelTab.TryGetValue((int) channel, out channelInfo))
          this.ManifestError(Environment.GetResourceString("EventSource_UndefinedChannel", (object) channel, (object) eventName), false);
      }
      if (this.resources != null && eventMessage == null)
        eventMessage = this.resources.GetString("event_" + eventName, CultureInfo.InvariantCulture);
      if (channelInfo.Attribs.EventChannelType == EventChannelType.Admin && eventMessage == null)
        this.ManifestError(Environment.GetResourceString("EventSource_EventWithAdminChannelMustHaveMessage", (object) eventName, (object) channelInfo.Name), false);
      return channelInfo.Name;
    }

    private string GetTaskName(EventTask task, string eventName)
    {
      if (task == EventTask.None)
        return "";
      if (this.taskTab == null)
        this.taskTab = new Dictionary<int, string>();
      string str;
      if (!this.taskTab.TryGetValue((int) task, out str))
        str = this.taskTab[(int) task] = eventName;
      return str;
    }

    private string GetOpcodeName(EventOpcode opcode, string eventName)
    {
      switch (opcode)
      {
        case EventOpcode.Info:
          return "win:Info";
        case EventOpcode.Start:
          return "win:Start";
        case EventOpcode.Stop:
          return "win:Stop";
        case EventOpcode.DataCollectionStart:
          return "win:DC_Start";
        case EventOpcode.DataCollectionStop:
          return "win:DC_Stop";
        case EventOpcode.Extension:
          return "win:Extension";
        case EventOpcode.Reply:
          return "win:Reply";
        case EventOpcode.Resume:
          return "win:Resume";
        case EventOpcode.Suspend:
          return "win:Suspend";
        case EventOpcode.Send:
          return "win:Send";
        case EventOpcode.Receive:
          return "win:Receive";
        default:
          string str;
          if (this.opcodeTab == null || !this.opcodeTab.TryGetValue((int) opcode, out str))
          {
            this.ManifestError(Environment.GetResourceString("EventSource_UndefinedOpcode", (object) opcode, (object) eventName), true);
            str = (string) null;
          }
          return str;
      }
    }

    private string GetKeywords(ulong keywords, string eventName)
    {
      string str1 = "";
      ulong key = 1;
      while (key != 0UL)
      {
        if (((long) keywords & (long) key) != 0L)
        {
          string str2 = (string) null;
          if ((this.keywordTab == null || !this.keywordTab.TryGetValue(key, out str2)) && key >= 281474976710656UL)
            str2 = string.Empty;
          if (str2 == null)
          {
            this.ManifestError(Environment.GetResourceString("EventSource_UndefinedKeyword", (object) ("0x" + key.ToString("x", (IFormatProvider) CultureInfo.CurrentCulture)), (object) eventName), true);
            str2 = string.Empty;
          }
          if (str1.Length != 0 && str2.Length != 0)
            str1 += " ";
          str1 += str2;
        }
        key <<= 1;
      }
      return str1;
    }

    private string GetTypeName(Type type)
    {
      if (type.IsEnum())
        return this.GetTypeName(type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)[0].FieldType).Replace("win:Int", "win:UInt");
      switch (type.GetTypeCode())
      {
        case TypeCode.Boolean:
          return "win:Boolean";
        case TypeCode.Char:
        case TypeCode.UInt16:
          return "win:UInt16";
        case TypeCode.SByte:
          return "win:Int8";
        case TypeCode.Byte:
          return "win:UInt8";
        case TypeCode.Int16:
          return "win:Int16";
        case TypeCode.Int32:
          return "win:Int32";
        case TypeCode.UInt32:
          return "win:UInt32";
        case TypeCode.Int64:
          return "win:Int64";
        case TypeCode.UInt64:
          return "win:UInt64";
        case TypeCode.Single:
          return "win:Float";
        case TypeCode.Double:
          return "win:Double";
        case TypeCode.DateTime:
          return "win:FILETIME";
        case TypeCode.String:
          return "win:UnicodeString";
        default:
          if (type == typeof (Guid))
            return "win:GUID";
          if (type == typeof (IntPtr))
            return "win:Pointer";
          if ((type.IsArray || type.IsPointer) && type.GetElementType() == typeof (byte))
            return "win:Binary";
          this.ManifestError(Environment.GetResourceString("EventSource_UnsupportedEventTypeInManifest", (object) type.Name), true);
          return string.Empty;
      }
    }

    private static void UpdateStringBuilder(ref StringBuilder stringBuilder, string eventMessage, int startIndex, int count)
    {
      if (stringBuilder == null)
        stringBuilder = new StringBuilder();
      stringBuilder.Append(eventMessage, startIndex, count);
    }

    private string TranslateToManifestConvention(string eventMessage, string evtName)
    {
      StringBuilder stringBuilder = (StringBuilder) null;
      int writtenSoFar = 0;
      int i = 0;
      while (i < eventMessage.Length)
      {
        if (eventMessage[i] == '%')
        {
          ManifestBuilder.UpdateStringBuilder(ref stringBuilder, eventMessage, writtenSoFar, i - writtenSoFar);
          stringBuilder.Append("%%");
          i++;
          writtenSoFar = i;
        }
        else if (i < eventMessage.Length - 1 && (eventMessage[i] == '{' && eventMessage[i + 1] == '{' || eventMessage[i] == '}' && eventMessage[i + 1] == '}'))
        {
          ManifestBuilder.UpdateStringBuilder(ref stringBuilder, eventMessage, writtenSoFar, i - writtenSoFar);
          stringBuilder.Append(eventMessage[i]);
          i++;
          i++;
          writtenSoFar = i;
        }
        else if (eventMessage[i] == '{')
        {
          int num = i;
          i++;
          int idx = 0;
          for (; i < eventMessage.Length && char.IsDigit(eventMessage[i]); i++)
            idx = idx * 10 + (int) eventMessage[i] - 48;
          if (i < eventMessage.Length && eventMessage[i] == '}')
          {
            i++;
            ManifestBuilder.UpdateStringBuilder(ref stringBuilder, eventMessage, writtenSoFar, num - writtenSoFar);
            int manifestConvention = this.TranslateIndexToManifestConvention(idx, evtName);
            stringBuilder.Append('%').Append(manifestConvention);
            if (i < eventMessage.Length && eventMessage[i] == '!')
            {
              i++;
              stringBuilder.Append("%!");
            }
            writtenSoFar = i;
          }
          else
            this.ManifestError(Environment.GetResourceString("EventSource_UnsupportedMessageProperty", (object) evtName, (object) eventMessage), false);
        }
        else
        {
          int index;
          if ((index = "&<>'\"\r\n\t".IndexOf(eventMessage[i])) >= 0)
          {
            string[] strArray = new string[8]
            {
              "&amp;",
              "&lt;",
              "&gt;",
              "&apos;",
              "&quot;",
              "%r",
              "%n",
              "%t"
            };
            ((Action<char, string>) ((ch, escape) =>
            {
              ManifestBuilder.UpdateStringBuilder(ref stringBuilder, eventMessage, writtenSoFar, i - writtenSoFar);
              ++i;
              stringBuilder.Append(escape);
              writtenSoFar = i;
            }))(eventMessage[i], strArray[index]);
          }
          else
            i++;
        }
      }
      if (stringBuilder == null)
        return eventMessage;
      ManifestBuilder.UpdateStringBuilder(ref stringBuilder, eventMessage, writtenSoFar, i - writtenSoFar);
      return stringBuilder.ToString();
    }

    private int TranslateIndexToManifestConvention(int idx, string evtName)
    {
      List<int> intList;
      if (this.perEventByteArrayArgIndices.TryGetValue(evtName, out intList))
      {
        foreach (int num in intList)
        {
          if (idx >= num)
            ++idx;
          else
            break;
        }
      }
      return idx + 1;
    }

    private class ChannelInfo
    {
      public string Name;
      public ulong Keywords;
      public EventChannelAttribute Attribs;
    }
  }
}

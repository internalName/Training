// Decompiled with JetBrains decompiler
// Type: System.Runtime.Remoting.Channels.CrossAppDomainSerializer
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Collections;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;

namespace System.Runtime.Remoting.Channels
{
  internal static class CrossAppDomainSerializer
  {
    [SecurityCritical]
    internal static MemoryStream SerializeMessage(IMessage msg)
    {
      MemoryStream memoryStream = new MemoryStream();
      RemotingSurrogateSelector surrogateSelector = new RemotingSurrogateSelector();
      new BinaryFormatter()
      {
        SurrogateSelector = ((ISurrogateSelector) surrogateSelector),
        Context = new StreamingContext(StreamingContextStates.CrossAppDomain)
      }.Serialize((Stream) memoryStream, (object) msg, (Header[]) null, false);
      memoryStream.Position = 0L;
      return memoryStream;
    }

    [SecurityCritical]
    internal static MemoryStream SerializeMessageParts(ArrayList argsToSerialize)
    {
      MemoryStream memoryStream = new MemoryStream();
      new BinaryFormatter()
      {
        SurrogateSelector = ((ISurrogateSelector) new RemotingSurrogateSelector()),
        Context = new StreamingContext(StreamingContextStates.CrossAppDomain)
      }.Serialize((Stream) memoryStream, (object) argsToSerialize, (Header[]) null, false);
      memoryStream.Position = 0L;
      return memoryStream;
    }

    [SecurityCritical]
    internal static void SerializeObject(object obj, MemoryStream stm)
    {
      new BinaryFormatter()
      {
        SurrogateSelector = ((ISurrogateSelector) new RemotingSurrogateSelector()),
        Context = new StreamingContext(StreamingContextStates.CrossAppDomain)
      }.Serialize((Stream) stm, obj, (Header[]) null, false);
    }

    [SecurityCritical]
    internal static MemoryStream SerializeObject(object obj)
    {
      MemoryStream stm = new MemoryStream();
      CrossAppDomainSerializer.SerializeObject(obj, stm);
      stm.Position = 0L;
      return stm;
    }

    [SecurityCritical]
    internal static IMessage DeserializeMessage(MemoryStream stm)
    {
      return CrossAppDomainSerializer.DeserializeMessage(stm, (IMethodCallMessage) null);
    }

    [SecurityCritical]
    internal static IMessage DeserializeMessage(MemoryStream stm, IMethodCallMessage reqMsg)
    {
      if (stm == null)
        throw new ArgumentNullException(nameof (stm));
      stm.Position = 0L;
      return (IMessage) new BinaryFormatter()
      {
        SurrogateSelector = ((ISurrogateSelector) null),
        Context = new StreamingContext(StreamingContextStates.CrossAppDomain)
      }.Deserialize((Stream) stm, (HeaderHandler) null, false, true, reqMsg);
    }

    [SecurityCritical]
    internal static ArrayList DeserializeMessageParts(MemoryStream stm)
    {
      return (ArrayList) CrossAppDomainSerializer.DeserializeObject(stm);
    }

    [SecurityCritical]
    internal static object DeserializeObject(MemoryStream stm)
    {
      stm.Position = 0L;
      return new BinaryFormatter()
      {
        Context = new StreamingContext(StreamingContextStates.CrossAppDomain)
      }.Deserialize((Stream) stm, (HeaderHandler) null, false, true, (IMethodCallMessage) null);
    }
  }
}

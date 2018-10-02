// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.BinaryMethodCall
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class BinaryMethodCall
  {
    private bool bArgsPrimitive = true;
    private string uri;
    private string methodName;
    private string typeName;
    private Type[] instArgs;
    private object[] args;
    private object methodSignature;
    private object callContext;
    private string scallContext;
    private object properties;
    private Type[] argTypes;
    private MessageEnum messageEnum;
    private object[] callA;

    internal object[] WriteArray(string uri, string methodName, string typeName, Type[] instArgs, object[] args, object methodSignature, object callContext, object[] properties)
    {
      this.uri = uri;
      this.methodName = methodName;
      this.typeName = typeName;
      this.instArgs = instArgs;
      this.args = args;
      this.methodSignature = methodSignature;
      this.callContext = callContext;
      this.properties = (object) properties;
      int length = 0;
      if (args == null || args.Length == 0)
      {
        this.messageEnum = MessageEnum.NoArgs;
      }
      else
      {
        this.argTypes = new Type[args.Length];
        this.bArgsPrimitive = true;
        for (int index = 0; index < args.Length; ++index)
        {
          if (args[index] != null)
          {
            this.argTypes[index] = args[index].GetType();
            if ((uint) Converter.ToCode(this.argTypes[index]) <= 0U && (object) this.argTypes[index] != (object) Converter.typeofString || args[index] is ISerializable)
            {
              this.bArgsPrimitive = false;
              break;
            }
          }
        }
        if (this.bArgsPrimitive)
        {
          this.messageEnum = MessageEnum.ArgsInline;
        }
        else
        {
          ++length;
          this.messageEnum = MessageEnum.ArgsInArray;
        }
      }
      if (instArgs != null)
      {
        ++length;
        this.messageEnum |= MessageEnum.GenericMethod;
      }
      if (methodSignature != null)
      {
        ++length;
        this.messageEnum |= MessageEnum.MethodSignatureInArray;
      }
      if (callContext == null)
        this.messageEnum |= MessageEnum.NoContext;
      else if (callContext is string)
      {
        this.messageEnum |= MessageEnum.ContextInline;
      }
      else
      {
        ++length;
        this.messageEnum |= MessageEnum.ContextInArray;
      }
      if (properties != null)
      {
        ++length;
        this.messageEnum |= MessageEnum.PropertyInArray;
      }
      if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInArray) && length == 1)
      {
        this.messageEnum ^= MessageEnum.ArgsInArray;
        this.messageEnum |= MessageEnum.ArgsIsArray;
        return args;
      }
      if (length <= 0)
        return (object[]) null;
      int index1 = 0;
      this.callA = new object[length];
      if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInArray))
        this.callA[index1++] = (object) args;
      if (IOUtil.FlagTest(this.messageEnum, MessageEnum.GenericMethod))
        this.callA[index1++] = (object) instArgs;
      if (IOUtil.FlagTest(this.messageEnum, MessageEnum.MethodSignatureInArray))
        this.callA[index1++] = methodSignature;
      if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInArray))
        this.callA[index1++] = callContext;
      if (IOUtil.FlagTest(this.messageEnum, MessageEnum.PropertyInArray))
        this.callA[index1] = (object) properties;
      return this.callA;
    }

    internal void Write(__BinaryWriter sout)
    {
      sout.WriteByte((byte) 21);
      sout.WriteInt32((int) this.messageEnum);
      IOUtil.WriteStringWithCode(this.methodName, sout);
      IOUtil.WriteStringWithCode(this.typeName, sout);
      if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInline))
        IOUtil.WriteStringWithCode((string) this.callContext, sout);
      if (!IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInline))
        return;
      sout.WriteInt32(this.args.Length);
      for (int index = 0; index < this.args.Length; ++index)
        IOUtil.WriteWithCode(this.argTypes[index], this.args[index], sout);
    }

    [SecurityCritical]
    internal void Read(__BinaryParser input)
    {
      this.messageEnum = (MessageEnum) input.ReadInt32();
      this.methodName = (string) IOUtil.ReadWithCode(input);
      this.typeName = (string) IOUtil.ReadWithCode(input);
      if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInline))
      {
        this.scallContext = (string) IOUtil.ReadWithCode(input);
        this.callContext = (object) new LogicalCallContext()
        {
          RemotingData = {
            LogicalCallID = this.scallContext
          }
        };
      }
      if (!IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInline))
        return;
      this.args = IOUtil.ReadArgs(input);
    }

    [SecurityCritical]
    internal IMethodCallMessage ReadArray(object[] callA, object handlerObject)
    {
      if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsIsArray))
      {
        this.args = callA;
      }
      else
      {
        int num1 = 0;
        if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInArray))
        {
          if (callA.Length < num1)
            throw new SerializationException(Environment.GetResourceString("Serialization_Method"));
          this.args = (object[]) callA[num1++];
        }
        if (IOUtil.FlagTest(this.messageEnum, MessageEnum.GenericMethod))
        {
          if (callA.Length < num1)
            throw new SerializationException(Environment.GetResourceString("Serialization_Method"));
          this.instArgs = (Type[]) callA[num1++];
        }
        if (IOUtil.FlagTest(this.messageEnum, MessageEnum.MethodSignatureInArray))
        {
          if (callA.Length < num1)
            throw new SerializationException(Environment.GetResourceString("Serialization_Method"));
          this.methodSignature = callA[num1++];
        }
        if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInArray))
        {
          if (callA.Length < num1)
            throw new SerializationException(Environment.GetResourceString("Serialization_Method"));
          this.callContext = callA[num1++];
        }
        if (IOUtil.FlagTest(this.messageEnum, MessageEnum.PropertyInArray))
        {
          if (callA.Length < num1)
            throw new SerializationException(Environment.GetResourceString("Serialization_Method"));
          object[] objArray = callA;
          int index = num1;
          int num2 = index + 1;
          this.properties = objArray[index];
        }
      }
      return (IMethodCallMessage) new MethodCall(handlerObject, new BinaryMethodCallMessage(this.uri, this.methodName, this.typeName, this.instArgs, this.args, this.methodSignature, (LogicalCallContext) this.callContext, (object[]) this.properties));
    }

    internal void Dump()
    {
    }

    [Conditional("_LOGGING")]
    private void DumpInternal()
    {
      if (!BCLDebug.CheckEnabled("BINARY"))
        return;
      if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInline))
      {
        string callContext = this.callContext as string;
      }
      if (!IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInline))
        return;
      int num = 0;
      while (num < this.args.Length)
        ++num;
    }
  }
}

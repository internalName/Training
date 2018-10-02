// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.BinaryMethodReturn
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

using System.Diagnostics;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class BinaryMethodReturn : IStreamable
  {
    private bool bArgsPrimitive = true;
    private static object instanceOfVoid = FormatterServices.GetUninitializedObject(Converter.typeofSystemVoid);
    private object returnValue;
    private object[] args;
    private Exception exception;
    private object callContext;
    private string scallContext;
    private object properties;
    private Type[] argTypes;
    private MessageEnum messageEnum;
    private object[] callA;
    private Type returnType;

    [SecuritySafeCritical]
    static BinaryMethodReturn()
    {
    }

    internal BinaryMethodReturn()
    {
    }

    internal object[] WriteArray(object returnValue, object[] args, Exception exception, object callContext, object[] properties)
    {
      this.returnValue = returnValue;
      this.args = args;
      this.exception = exception;
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
            if ((uint) Converter.ToCode(this.argTypes[index]) <= 0U && (object) this.argTypes[index] != (object) Converter.typeofString)
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
      if (returnValue == null)
        this.messageEnum |= MessageEnum.NoReturnValue;
      else if (returnValue.GetType() == typeof (void))
      {
        this.messageEnum |= MessageEnum.ReturnValueVoid;
      }
      else
      {
        this.returnType = returnValue.GetType();
        if ((uint) Converter.ToCode(this.returnType) > 0U || (object) this.returnType == (object) Converter.typeofString)
        {
          this.messageEnum |= MessageEnum.ReturnValueInline;
        }
        else
        {
          ++length;
          this.messageEnum |= MessageEnum.ReturnValueInArray;
        }
      }
      if (exception != null)
      {
        ++length;
        this.messageEnum |= MessageEnum.ExceptionInArray;
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
      if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ReturnValueInArray))
        this.callA[index1++] = returnValue;
      if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ExceptionInArray))
        this.callA[index1++] = (object) exception;
      if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInArray))
        this.callA[index1++] = callContext;
      if (IOUtil.FlagTest(this.messageEnum, MessageEnum.PropertyInArray))
        this.callA[index1] = (object) properties;
      return this.callA;
    }

    public void Write(__BinaryWriter sout)
    {
      sout.WriteByte((byte) 22);
      sout.WriteInt32((int) this.messageEnum);
      if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ReturnValueInline))
        IOUtil.WriteWithCode(this.returnType, this.returnValue, sout);
      if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInline))
        IOUtil.WriteStringWithCode((string) this.callContext, sout);
      if (!IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInline))
        return;
      sout.WriteInt32(this.args.Length);
      for (int index = 0; index < this.args.Length; ++index)
        IOUtil.WriteWithCode(this.argTypes[index], this.args[index], sout);
    }

    [SecurityCritical]
    public void Read(__BinaryParser input)
    {
      this.messageEnum = (MessageEnum) input.ReadInt32();
      if (IOUtil.FlagTest(this.messageEnum, MessageEnum.NoReturnValue))
        this.returnValue = (object) null;
      else if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ReturnValueVoid))
        this.returnValue = BinaryMethodReturn.instanceOfVoid;
      else if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ReturnValueInline))
        this.returnValue = IOUtil.ReadWithCode(input);
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
    internal IMethodReturnMessage ReadArray(object[] returnA, IMethodCallMessage methodCallMessage, object handlerObject)
    {
      if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsIsArray))
      {
        this.args = returnA;
      }
      else
      {
        int num1 = 0;
        if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ArgsInArray))
        {
          if (returnA.Length < num1)
            throw new SerializationException(Environment.GetResourceString("Serialization_Method"));
          this.args = (object[]) returnA[num1++];
        }
        if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ReturnValueInArray))
        {
          if (returnA.Length < num1)
            throw new SerializationException(Environment.GetResourceString("Serialization_Method"));
          this.returnValue = returnA[num1++];
        }
        if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ExceptionInArray))
        {
          if (returnA.Length < num1)
            throw new SerializationException(Environment.GetResourceString("Serialization_Method"));
          this.exception = (Exception) returnA[num1++];
        }
        if (IOUtil.FlagTest(this.messageEnum, MessageEnum.ContextInArray))
        {
          if (returnA.Length < num1)
            throw new SerializationException(Environment.GetResourceString("Serialization_Method"));
          this.callContext = returnA[num1++];
        }
        if (IOUtil.FlagTest(this.messageEnum, MessageEnum.PropertyInArray))
        {
          if (returnA.Length < num1)
            throw new SerializationException(Environment.GetResourceString("Serialization_Method"));
          object[] objArray = returnA;
          int index = num1;
          int num2 = index + 1;
          this.properties = objArray[index];
        }
      }
      return (IMethodReturnMessage) new MethodResponse(methodCallMessage, handlerObject, new BinaryMethodReturnMessage(this.returnValue, this.args, this.exception, (LogicalCallContext) this.callContext, (object[]) this.properties));
    }

    public void Dump()
    {
    }

    [Conditional("_LOGGING")]
    private void DumpInternal()
    {
      if (!BCLDebug.CheckEnabled("BINARY"))
        return;
      IOUtil.FlagTest(this.messageEnum, MessageEnum.ReturnValueInline);
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

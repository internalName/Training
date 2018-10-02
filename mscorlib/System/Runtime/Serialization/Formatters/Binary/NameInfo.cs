// Decompiled with JetBrains decompiler
// Type: System.Runtime.Serialization.Formatters.Binary.NameInfo
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 38C94DDE-2C5E-44AC-BCDB-DDA2D9F231CC
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

namespace System.Runtime.Serialization.Formatters.Binary
{
  internal sealed class NameInfo
  {
    internal string NIFullName;
    internal long NIobjectId;
    internal long NIassemId;
    internal InternalPrimitiveTypeE NIprimitiveTypeEnum;
    internal Type NItype;
    internal bool NIisSealed;
    internal bool NIisArray;
    internal bool NIisArrayItem;
    internal bool NItransmitTypeOnObject;
    internal bool NItransmitTypeOnMember;
    internal bool NIisParentTypeOnObject;
    internal InternalArrayTypeE NIarrayEnum;
    private bool NIsealedStatusChecked;

    internal NameInfo()
    {
    }

    internal void Init()
    {
      this.NIFullName = (string) null;
      this.NIobjectId = 0L;
      this.NIassemId = 0L;
      this.NIprimitiveTypeEnum = InternalPrimitiveTypeE.Invalid;
      this.NItype = (Type) null;
      this.NIisSealed = false;
      this.NItransmitTypeOnObject = false;
      this.NItransmitTypeOnMember = false;
      this.NIisParentTypeOnObject = false;
      this.NIisArray = false;
      this.NIisArrayItem = false;
      this.NIarrayEnum = InternalArrayTypeE.Empty;
      this.NIsealedStatusChecked = false;
    }

    public bool IsSealed
    {
      get
      {
        if (!this.NIsealedStatusChecked)
        {
          this.NIisSealed = this.NItype.IsSealed;
          this.NIsealedStatusChecked = true;
        }
        return this.NIisSealed;
      }
    }

    public string NIname
    {
      get
      {
        if (this.NIFullName == null)
          this.NIFullName = this.NItype.FullName;
        return this.NIFullName;
      }
      set
      {
        this.NIFullName = value;
      }
    }
  }
}

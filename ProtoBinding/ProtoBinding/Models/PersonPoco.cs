using System;
using ProtoBuf;

namespace ProtoBinding.Models
{
  [ProtoContract, Serializable]
  public class PersonPoco
  {
    [ProtoMember(2)]
    public int Id { get; set; }

    [ProtoMember(1)]
    public string Name { get; set; }

    [ProtoMember(3)]
    public string Email { get; set; }

  }
}

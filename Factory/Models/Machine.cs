using System.Collections.Generic;

namespace Factory.Models
{
  public class Machine
  {
    public int MachineId { get; set; }
    public int Name { get; set; }
    public virtual ICollection<EngineerMachine> JoinEntities { get; }

    public Machine()
    {
      this.JoinEntities = new HashSet<EngineerMachine>();
    }
  }
}
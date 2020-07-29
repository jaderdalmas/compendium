using System.Collections.Generic;
using System.Linq;

namespace Compendium.Repository.Model
{
  public class SyscatSync
  {
    public virtual string SyncId => TabName;
    public string TabName { get; set; }

    protected bool Exists(IEnumerable<SyncEntity> tables) => tables.Any(x => x.SyncId == SyncId);
  }
}
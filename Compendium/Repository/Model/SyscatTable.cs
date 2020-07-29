using System;
using System.Collections.Generic;
using System.Linq;

namespace Compendium.Repository.Model
{
  public class SyscatTable : SyscatSync
  {
    public char Type { get; set; }
    public short ColCount { get; set; }

    public TableEntity GetEntityInsert => new TableEntity(this);
    public TableExternalEntity GetEntityUpdate => new TableExternalEntity(this);

    public bool IsInsert(IEnumerable<TableEntity> list) => !Exists(list);
    public bool IsUpdate(IEnumerable<TableEntity> list) => Exists(list) && !Equals(list);

    private bool Validade(TableEntity list) => list != null
        && Type == list.Type;

    public override bool Equals(object obj)
    {
      if (obj is TableEntity)
        return Validade(obj as TableEntity);

      if (obj is IEnumerable<TableEntity>)
        return Validade((obj as IEnumerable<TableEntity>).FirstOrDefault(x => x.SyncId == SyncId));

      return base.Equals(obj);
    }
    public override int GetHashCode() => SyncId.GetHashCode(StringComparison.InvariantCulture);
  }
}

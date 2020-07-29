using System;
using System.Collections.Generic;
using System.Linq;

namespace Compendium.Repository.Model
{
  public class SyscatColumn : SyscatSync
  {
    public override string SyncId => $"{TabName}.{ColName}";
    public string ColName { get; set; }
    public short ColNo { get; set; }
    public string TypeName { get; set; }
    public int Length { get; set; }
    public string Default { get; set; }
    public char Nulls { get; set; }
    public char Identity { get; set; }

    public ColumnEntity GetEntityInsert => new ColumnEntity(this);
    public ColumnExternalEntity GetEntityUpdate => new ColumnExternalEntity(this);

    protected bool Exists(IEnumerable<ColumnEntity> list) => list.Any(x => x.SyncId == SyncId);
    public bool IsInsert(IEnumerable<ColumnEntity> list) => !Exists(list);
    public bool IsUpdate(IEnumerable<ColumnEntity> list) => Exists(list) && !Equals(list);

    private bool Validade(ColumnEntity entity) => entity != null
        && ColNo == entity.Number
        && TypeName == entity.Type
        && Length == entity.Length
        && Default == entity.Default
        && Nulls == entity.IsNull
        && Identity == entity.IsIdentity;

    public override bool Equals(object obj)
    {
      if (obj is ColumnEntity)
        return Validade(obj as ColumnEntity);

      if (obj is IEnumerable<ColumnEntity>)
        return Validade((obj as IEnumerable<ColumnEntity>).FirstOrDefault(x => x.SyncId == SyncId));

      return base.Equals(obj);
    }
    public override int GetHashCode() => SyncId.GetHashCode(StringComparison.InvariantCulture);
  }
}
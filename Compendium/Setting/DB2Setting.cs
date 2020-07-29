using IBM.Data.DB2.Core;

namespace Compendium.Setting
{
  public class DB2Setting
  {
    public string Server { get; set; }
    public string Database { get; set; }
    public string UserID { get; set; }
    public string Password { get; set; }
    public string CurrentSchema { get; set; }
    public string CurrentFunctionPath { get; set; }
    public string IsolationLevel { get; set; }
    public string ClientAcctStr { get; set; }

    private bool IsInvalid
    {
      get
      {
        return string.IsNullOrWhiteSpace(Server)
          || string.IsNullOrWhiteSpace(Database)
          || string.IsNullOrWhiteSpace(UserID)
          || string.IsNullOrWhiteSpace(Password)
          || string.IsNullOrWhiteSpace(CurrentSchema)
          || string.IsNullOrWhiteSpace(CurrentFunctionPath)
          || string.IsNullOrWhiteSpace(IsolationLevel)
          || string.IsNullOrWhiteSpace(ClientAcctStr);
      }
    }

    public DB2ConnectionStringBuilder GetBuilder
    {
      get
      {
        return IsInvalid ? null : new DB2ConnectionStringBuilder()
        {
          Server = Server,
          Database = Database,
          UserID = UserID,
          Password = Password,
          CurrentSchema = CurrentSchema,
          CurrentFunctionPath = CurrentFunctionPath,
          IsolationLevel = IsolationLevel,
          ClientAccountingString = ClientAcctStr
        };
      }
    }
  }
}

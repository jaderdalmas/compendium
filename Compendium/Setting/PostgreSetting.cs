using Npgsql;

namespace Compendium.Setting
{
  public class PostgreSetting
  {
    public string UserID { get; set; }
    public string Password { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string Database { get; set; }

    private bool IsInvalid
    {
      get
      {
        return string.IsNullOrWhiteSpace(UserID)
          || string.IsNullOrWhiteSpace(Password)
          || string.IsNullOrWhiteSpace(Host)
          || decimal.Zero.Equals(Port)
          || string.IsNullOrWhiteSpace(Database);
      }
    }

    public NpgsqlConnectionStringBuilder GetBuilder
    {
      get
      {
        return IsInvalid ? null : new NpgsqlConnectionStringBuilder
        {
          Username = UserID,
          Password = Password,
          Host = Host,
          Port = Port,
          Database = Database
        };
      }
    }
  }
}

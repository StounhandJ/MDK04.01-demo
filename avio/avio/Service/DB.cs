using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace avio.Service;

public class DB
{
    private MySqlConnection Connection { get; set; }

    private static DB? _instance = null;

    public DB(string server, string username, string password, string db)
    {
        string connStr = $"server={server};user={username};database={db};password={password};";
        Connection = new MySqlConnection(connStr);
        Connection.Open();

        _instance = this;
    }

    public bool IsConnect()
    {
        return Connection.Ping();
    }

    public List<Dictionary<string, object>> execute(string sql, List<object>? data = null)
    {
        List<Dictionary<string, object>> response = new List<Dictionary<string, object>>();

        MySqlCommand command = new MySqlCommand(sql, Connection);

        if (data != null)
        {
            foreach (var val in data)
            {
                command.Parameters.Add(new MySqlParameter("", val));
            }
        }

        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Dictionary<string, object?> outData = new Dictionary<string, object?>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                var value = reader.GetValue(i);
                if (value is DBNull)
                {
                    value = null;
                }
                outData.Add(reader.GetName(i), value);
            }
            response.Add(outData);
        }
        
        reader.Close();

        return response;
    }

    public void Close()
    {
        Connection.Close();
    }
}
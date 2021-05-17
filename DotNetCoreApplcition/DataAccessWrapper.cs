using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using IBM.Data.DB2.Core;
//using IBM.Data.Db2;

namespace DotNetCoreApplcition
{
    public class DataAccessWrapper
    {
        private static string InformixConnectionString = AppGlobal.ConnectionString;       
        public static T GetData<T>(string query, params object[] paramsList)
        {
            try
            {
                InformixConnectionString = "Server=10.90.18.232:1540;UID=ttester;PWD=Trc2019!;Database=TIRS;TimeOut =300";
                var val = (IDataObject)Activator.CreateInstance<T>();
                using (var db2connection = new DB2Connection(InformixConnectionString))
                {
                    using (var selectCommand = new DB2Command(query))
                    {
                        selectCommand.Connection = db2connection;
                        if (paramsList != null)
                        {
                            foreach (var param in paramsList)
                            {
                                switch ((param))
                                {
                                    case string _:
                                        selectCommand.Parameters.Add(new DB2Parameter()).Value = param.ToString();
                                        break;
                                    case int _:
                                        selectCommand.Parameters.Add(new DB2Parameter()).Value = int.Parse(param.ToString());
                                        break;
                                }
                            }
                        }
                        db2connection.Open();
                        using (var reader = selectCommand.ExecuteReader(CommandBehavior.Default))
                        {
                            while (reader.Read())
                            {
                                val.FillData(reader);
                            }
                            reader.Close();
                            reader.Dispose();
                        }
                    }
                    db2connection.Close();
                    db2connection.Dispose();

                }
                return (T)val;
            }
            catch (Exception ex)
            {

                return default(T);
            }
        }
        public static List<T> GetListDataUtil<T>(string query, params string[] paramsList)
        {
            
            try
            {
                
                var respone = new List<T>();
                           
                InformixConnectionString = "Server=10.90.18.232:1540;UID=ttester;PWD=Trc2019!;Database=TIRS;TimeOut =300";
                
                using (var informixConn = new DB2Connection(InformixConnectionString))
                {
                    
                    informixConn.Open();
                    
                    var selectCommand = new DB2Command(query) { Connection = informixConn };
                    
                    if (paramsList != null)
                    {
                        
                        foreach (var param in paramsList)
                        {                           
                            selectCommand.Parameters.Add(param);                            
                        }
                    }

                    using (var reader = selectCommand.ExecuteReader(CommandBehavior.Default))
                    {
                        
                        while (reader.Read())
                        {
                            
                            var val = (IDataObject)Activator.CreateInstance<T>();                            
                            val.FillData(reader);                           
                            respone.Add((T)val);
                        }
                        reader.Close();
                        reader.Dispose();
                    }
                    informixConn.Close(); informixConn.Dispose();
                }
                return respone;
            }
            catch (Exception ex)
            {                           
                return new List<T>();
            }

        }

        //public static T GetDataUtil<T>(string query, params object[] paramsList)
        //{
        //    try
        //    {
        //        var val = (IDataObject)Activator.CreateInstance<T>();
        //        using (var informixConn = new DB2Connection(InformixConnectionString))
        //        {
        //            using (var selectCommand = new IfxCommand(query))
        //            {
        //                selectCommand.Connection = informixConn;
        //                if (paramsList != null)
        //                {
        //                    foreach (var param in paramsList)
        //                    {
        //                        switch ((param))
        //                        {
        //                            case string _:
        //                                selectCommand.Parameters.Add(new IfxParameter()).Value = param.ToString();
        //                                break;
        //                            case int _:
        //                                selectCommand.Parameters.Add(new IfxParameter()).Value = int.Parse(param.ToString());
        //                                break;
        //                        }
        //                    }
        //                }
        //                informixConn.Open();
        //                using (var reader = selectCommand.ExecuteReader(CommandBehavior.Default))
        //                {
        //                    while (reader.Read())
        //                    {
        //                        val.FillData(reader);
        //                    }
        //                    reader.Close();
        //                    reader.Dispose();
        //                }
        //            }
        //            informixConn.Close(); informixConn.Dispose();
        //        }
        //        return (T)val;
        //    }
        //    catch (Exception ex)
        //    {
        //        return default(T);
        //    }

        //}
        //public static List<T> GetListData<T>(string query, params string[] paramsList)
        //{
        //    try
        //    {
        //        var respone = new List<T>();
        //        using (var informixConn = new IfxConnection(InformixConnectionString))
        //        {
        //            informixConn.Open();
        //            var selectCommand = new IfxCommand(query) { Connection = informixConn };
        //            if (paramsList != null)
        //            {
        //                foreach (var param in paramsList)
        //                {
        //                    selectCommand.Parameters.Add(param);
        //                }
        //            }

        //            using (var reader = selectCommand.ExecuteReader(CommandBehavior.Default))
        //            {
        //                while (reader.Read())
        //                {
        //                    var val = (IDataObject)Activator.CreateInstance<T>();
        //                    val.FillData(reader);
        //                    respone.Add((T)val);
        //                }
        //                reader.Close();
        //                reader.Dispose();
        //            }
        //            informixConn.Close(); informixConn.Dispose();

        //        }
        //        return respone;
        //    }
        //    catch (Exception ex)
        //    {

        //        return new List<T>();
        //    }

        //}

        //public static List<T> GetListDataUtil<T>(string query, params string[] paramsList)
        //{
        //    try
        //    {
        //        var respone = new List<T>();
        //        using (var informixConn = new IfxConnection(InformixConnectionString))
        //        {
        //            informixConn.Open();
        //            var selectCommand = new IfxCommand(query) { Connection = informixConn };
        //            if (paramsList != null)
        //            {
        //                foreach (var param in paramsList)
        //                {
        //                    selectCommand.Parameters.Add(param);
        //                }
        //            }

        //            using (var reader = selectCommand.ExecuteReader(CommandBehavior.Default))
        //            {
        //                while (reader.Read())
        //                {
        //                    var val = (IDataObject)Activator.CreateInstance<T>();
        //                    val.FillData(reader);
        //                    respone.Add((T)val);
        //                }
        //                reader.Close();
        //                reader.Dispose();
        //            }
        //            informixConn.Close(); informixConn.Dispose();
        //        }
        //        return respone;
        //    }
        //    catch (Exception ex)
        //    {
        //        return new List<T>();
        //    }

        //}

        //public static T GetStoredProcedureData<T>(string query, params KeyValuePair<string, string>[] paramsList)
        //{
        //    try
        //    {
        //        var val = (IDataObject)Activator.CreateInstance<T>();
        //        using (var informixConn = new IfxConnection(InformixConnectionString))
        //        {
        //            var selectCommand = new IfxCommand("", informixConn);
        //            selectCommand.CommandType = CommandType.StoredProcedure;
        //            selectCommand.CommandText = query;

        //            if (paramsList != null)
        //            {
        //                foreach (var param in paramsList)
        //                {
        //                    selectCommand.Parameters.Add(new IfxParameter(param.Key, param.Value));
        //                }
        //            }

        //            informixConn.Open();

        //            using (var reader = selectCommand.ExecuteReader(CommandBehavior.Default))
        //            {
        //                while (reader.Read())
        //                {
        //                    val.FillData(reader);

        //                }
        //                reader.Close();
        //                reader.Dispose();
        //            }
        //            informixConn.Close(); informixConn.Dispose();

        //        }
        //        return (T)val;
        //    }
        //    catch (Exception ex)
        //    {

        //        return default(T);
        //    }

        //}
        //public static void SaveData(string query, params string[] paramsList)
        //{
        //    try
        //    {
        //        using (var informixConn = new IfxConnection(InformixConnectionString))
        //        {
        //            using (var selectCommand = new IfxCommand(query))
        //            {
        //                selectCommand.Connection = informixConn;
        //                if (paramsList != null)
        //                {
        //                    foreach (var param in paramsList)
        //                    {
        //                        selectCommand.Parameters.Add(new IfxParameter()).Value = param;
        //                    }
        //                }
        //                informixConn.Open();
        //                selectCommand.ExecuteNonQuery();
        //                selectCommand.Parameters.Clear();
        //            }
        //            informixConn.Close(); informixConn.Dispose();

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}
        //public static void SaveDataWindowApp(string query, params string[] paramsList)
        //{
        //    try
        //    {
        //        using (var informixConn = new IfxConnection(InformixConnectionString))
        //        {
        //            using (var selectCommand = new IfxCommand(query))
        //            {
        //                selectCommand.Connection = informixConn;
        //                if (paramsList != null)
        //                {
        //                    foreach (var param in paramsList)
        //                    {
        //                        selectCommand.Parameters.Add(new IfxParameter()).Value = param;
        //                    }
        //                }
        //                informixConn.Open();
        //                selectCommand.ExecuteNonQuery();
        //                selectCommand.Parameters.Clear();
        //            }
        //            informixConn.Close(); informixConn.Dispose();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }

        //}
        public static void ExecuteQuery(string query, params string[] paramsList)
        {
            try
            {
                using (var informixConn = new DB2Connection(InformixConnectionString))
                {
                    using (var selectCommand = new DB2Command(query))
                    {
                        selectCommand.Connection = informixConn;
                        if (paramsList != null)
                        {
                            foreach (var param in paramsList)
                            {
                                selectCommand.Parameters.Add(new DB2Parameter()).Value = param;
                            }
                        }
                        informixConn.Open();
                        selectCommand.ExecuteNonQuery();
                        selectCommand.Parameters.Clear();
                    }
                    informixConn.Close(); informixConn.Dispose();

                }
            }
            catch (Exception ex)
            {

            }
        }

        //public static void ExecuteQueryNoException(string query, params string[] paramsList)
        //{
        //    using (var informixConn = new IfxConnection(InformixConnectionString))
        //    {
        //        using (var selectCommand = new IfxCommand(query))
        //        {
        //            selectCommand.Connection = informixConn;
        //            if (paramsList != null)
        //            {
        //                foreach (var param in paramsList)
        //                {
        //                    selectCommand.Parameters.Add(new IfxParameter()).Value = param;
        //                }
        //            }
        //            informixConn.Open();
        //            selectCommand.ExecuteNonQuery();
        //            selectCommand.Parameters.Clear();
        //        }
        //        informixConn.Close(); informixConn.Dispose();

        //    }
        //}
        //public static string ExecuteQueriesInTransaction(params string[] queries)
        //{
        //    //string[] arrQueries = queries.Split(querySplitter);
        //    string query = string.Empty;
        //    IfxConnection informixConn;
        //    IfxTransaction mySqlTransaction;
        //    string expMessage = string.Empty;

        //    using (informixConn = new IfxConnection(InformixConnectionString))
        //    {
        //        informixConn.Open();
        //        mySqlTransaction = informixConn.BeginTransaction();

        //        try
        //        {
        //            foreach (string q in queries)
        //            {
        //                query = q;
        //                using (var selectCommand = new IfxCommand(query))
        //                {
        //                    selectCommand.Connection = informixConn;
        //                    selectCommand.Transaction = mySqlTransaction;
        //                    selectCommand.ExecuteNonQuery();
        //                    selectCommand.Parameters.Clear();
        //                }

        //            }

        //            mySqlTransaction.Commit();
        //            informixConn.Close();
        //            informixConn.Dispose();
        //        }
        //        catch (Exception ex)
        //        {
        //            mySqlTransaction.Rollback();

        //            expMessage = ex.Message;
        //        }
        //        return expMessage;
        //    }
        //}

        //public static void ExecuteQueryUtil(string query, params string[] paramsList)
        //{
        //    try
        //    {
        //        using (var informixConn = new IfxConnection(InformixConnectionString))
        //        {
        //            using (var selectCommand = new IfxCommand(query))
        //            {
        //                selectCommand.Connection = informixConn;
        //                if (paramsList != null)
        //                {
        //                    foreach (var param in paramsList)
        //                    {
        //                        selectCommand.Parameters.Add(new IfxParameter()).Value = param;
        //                    }
        //                }
        //                informixConn.Open();
        //                selectCommand.ExecuteNonQuery();
        //                selectCommand.Parameters.Clear();
        //            }
        //            informixConn.Close(); informixConn.Dispose();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //public static List<T> GetStoredProcedureListData<T>(string query, params KeyValuePair<string, string>[] paramsList)
        //{
        //    try
        //    {
        //        var response = new List<T>();
        //        using (var informixConn = new IfxConnection(InformixConnectionString))
        //        {
        //            var selectCommand = new IfxCommand("", informixConn)
        //            {
        //                CommandType = CommandType.StoredProcedure,
        //                CommandText = query
        //            };

        //            if (paramsList != null)
        //            {
        //                foreach (var param in paramsList)

        //                {
        //                    selectCommand.Parameters.Add(new IfxParameter(param.Key, param.Value));
        //                }
        //            }


        //            informixConn.Open();

        //            using (var reader = selectCommand.ExecuteReader(CommandBehavior.Default))
        //            {
        //                while (reader.Read())
        //                {
        //                    var val = (IDataObject)Activator.CreateInstance<T>();
        //                    val.FillData(reader);
        //                    response.Add((T)val);

        //                }
        //                reader.Close();
        //                reader.Dispose();
        //            }
        //            informixConn.Close(); informixConn.Dispose();

        //        }
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {

        //        return new List<T>();
        //    }

        //}
        //public static T GetData<T>(IfxConnection informixConn, string query, params string[] paramsList)
        //{
        //    try
        //    {
        //        var val = (IDataObject)Activator.CreateInstance<T>();

        //        using (var selectCommand = new IfxCommand(query))
        //        {
        //            selectCommand.Connection = informixConn;
        //            if (paramsList != null)
        //            {
        //                foreach (var param in paramsList)
        //                {
        //                    selectCommand.Parameters.Add(new IfxParameter()).Value = param;
        //                }
        //            }

        //            using (var reader = selectCommand.ExecuteReader(CommandBehavior.Default))
        //            {
        //                while (reader.Read())
        //                {
        //                    val.FillData(reader);
        //                }
        //                reader.Close();
        //                reader.Dispose();

        //            }
        //        }

        //        return (T)val;
        //    }
        //    catch (Exception ex)
        //    {

        //        return default(T);
        //    }

        //}
        //public static void SaveData(IfxConnection informixConn, string query, params object[] paramsList)
        //{
        //    try
        //    {
        //        using (var selectCommand = new IfxCommand(query))
        //        {
        //            selectCommand.Connection = informixConn;
        //            if (paramsList != null)
        //            {
        //                foreach (var param in paramsList)
        //                {
        //                    if (param == null) continue;
        //                    switch ((param))
        //                    {
        //                        case string _:
        //                            selectCommand.Parameters.Add(new IfxParameter()).Value = param.ToString();
        //                            break;
        //                        case int _:
        //                            selectCommand.Parameters.Add(new IfxParameter()).Value = int.Parse(param.ToString());
        //                            break;
        //                    }

        //                }
        //            }

        //            selectCommand.ExecuteNonQuery();
        //            selectCommand.Parameters.Clear();

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }


        //}

        //public static T GetScalarData<T>(IfxConnection informixConn, string query, params string[] paramsList)
        //{
        //    try
        //    {

        //        using (var selectCommand = new IfxCommand(query))
        //        {
        //            selectCommand.Connection = informixConn;
        //            if (paramsList != null)
        //            {
        //                foreach (var param in paramsList)
        //                {
        //                    selectCommand.Parameters.Add(new IfxParameter()).Value = param;
        //                }
        //            }

        //            return (T)selectCommand.ExecuteScalar();

        //        }


        //    }
        //    catch (Exception ex)
        //    {

        //        return default(T);
        //    }

        //}
        //public static void ExecuteStoredProcedureData(IfxConnection informixConn, string query, params KeyValuePair<string, string>[] paramsList)
        //{
        //    try
        //    {

        //        var selectCommand = new IfxCommand("", informixConn)
        //        {
        //            CommandType = CommandType.StoredProcedure,
        //            CommandText = query
        //        };

        //        if (paramsList != null)
        //        {
        //            foreach (var param in paramsList)
        //            {
        //                selectCommand.Parameters.Add(new IfxParameter(param.Key, param.Value));
        //            }
        //        }

        //        selectCommand.ExecuteNonQuery();
        //        selectCommand.Parameters.Clear();

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //public static string GetSingleValue(string query)
        //{
        //    try
        //    {
        //        var val = string.Empty;
        //        using (var informixConn = new IfxConnection(InformixConnectionString))
        //        {
        //            using (var selectCommand = new IfxCommand(query))
        //            {
        //                selectCommand.Connection = informixConn;
        //                informixConn.Open();
        //                using (var reader = selectCommand.ExecuteReader(CommandBehavior.Default))
        //                {
        //                    if (reader.Read())
        //                    {
        //                        val = reader.GetString(0);
        //                    }
        //                    reader.Close();
        //                    reader.Dispose();
        //                }
        //            }
        //            informixConn.Close(); informixConn.Dispose();

        //        }
        //        return val;
        //    }
        //    catch (Exception ex)
        //    {

        //        return "";
        //    }
        //}

        //public static string GetSingleValueWA(string query)
        //{
        //    try
        //    {
        //        var val = string.Empty;
        //        using (var informixConn = new IfxConnection(InformixConnectionString))
        //        {
        //            using (var selectCommand = new IfxCommand(query))
        //            {
        //                selectCommand.Connection = informixConn;
        //                informixConn.Open();
        //                using (var reader = selectCommand.ExecuteReader(CommandBehavior.Default))
        //                {
        //                    if (reader.Read())
        //                    {
        //                        val = reader.GetString(0);
        //                    }
        //                    reader.Close();
        //                    reader.Dispose();
        //                }
        //            }
        //            informixConn.Close(); informixConn.Dispose();
        //        }
        //        return val;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.ToString();
        //    }

        //}

        //public static int ExecuteNonQuery(string query, params string[] paramsList)
        //{
        //    int affectedRows = 0;
        //    using (var informixConn = new IfxConnection(InformixConnectionString))
        //    {
        //        using (var selectCommand = new IfxCommand(query))
        //        {
        //            selectCommand.Connection = informixConn;
        //            if (paramsList != null)
        //            {
        //                foreach (var param in paramsList)
        //                {
        //                    selectCommand.Parameters.Add(new IfxParameter()).Value = param;
        //                }
        //            }
        //            informixConn.Open();
        //            affectedRows = selectCommand.ExecuteNonQuery();
        //            selectCommand.Parameters.Clear();
        //        }
        //        informixConn.Close(); informixConn.Dispose();

        //    }
        //    return affectedRows;
        //}
    }
}

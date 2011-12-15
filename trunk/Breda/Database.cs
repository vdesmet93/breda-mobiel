using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace View
{
    /// <summary>The database handles as a palce to save all the data neccesary for the application.</summary>
    /// <remarks>Firstly a connection string get's (Used in main and app) specified and afterwards the table for the data is specified.</remarks>
    public class Database : System.Data.Linq.DataContext
    {
        public static string DBConnectionString = "Data Source=isostore:/database.sdf";
        public Database() : base(DBConnectionString)
        {

        }
        public System.Data.Linq.Table<DatabaseTable> databaseTables;
    }
}

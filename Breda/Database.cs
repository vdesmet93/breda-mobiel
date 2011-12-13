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
    public class Database : System.Data.Linq.DataContext
    {
        // Specify the connection string as a static, used in main page and app.xaml.
        public static string DBConnectionString = "Data Source=isostore:/database.sdf";

        public Database()
            : base(DBConnectionString)
        {
        }

        // Specify a single table for the to-do items.
        public System.Data.Linq.Table<DatabaseTable> databaseTables;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Ets.Mobile.Support
{
    public class EtsMobileContext : DataContext
{
    // Specify the connection string as a static, used in main page and app.xaml.
    public static string DBConnectionString = "Data Source=isostore:/ToDo.sdf";

    // Pass the connection string to the base class.
    public ToDoDataContext(string connectionString)
        : base(connectionString)
    { }

    // Specify a single table for the to-do items.
    public Table<ToDoItem> ToDoItems;
}
    {
    }
}

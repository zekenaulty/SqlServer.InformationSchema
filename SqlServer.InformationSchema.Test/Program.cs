using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServer.InformationSchema.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            // change this connection string to a valid connection string to run your tests
            using (var db = new SchemaDbContext(@"Data Source=ZEKE-GPC-2016\SQLEXPRESS;Initial Catalog=AdventureWorks;uid=aw;pwd=1234;"))
            {

                Console.BufferHeight = 3000;
                Console.BufferWidth = 3000;

                var dbType = db.GetType();
                var dbProperties = dbType.GetProperties().OrderBy(pi => pi.Name).ToArray();
                for (int i = 0; i < dbProperties.Length; i++)
                {
                    Console.Clear();

                    var oe = dbProperties[i].GetValue(db);
                    try
                    {
                        //if this fails move on to the next property
                        var q = (IQueryable<object>)oe;

                        Console.WriteLine(q.ToString());
                        Console.WriteLine("The above SQL will be executed. Press enter to continue.");
                        Console.ReadLine();

                        var list = q.ToList();

                        Console.WriteLine();

                        if (list.Count > 0)
                        {
                            var et = list[0].GetType();
                            var p = et.GetProperties();

                            for (int j = 0; j < list.Count; j++)
                            {
                                for (int k = 0; k < p.Length; k++)
                                {
                                    Console.Write($"{p[k].Name}: {p[k].GetValue(list[j])}");
                                    if (k < p.Length - 1)
                                        Console.Write(", ");
                                }
                                Console.Write("\r\n");
                            }

                            Console.WriteLine("\r\nPress enter to continue.");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("No data! Press enter to continue;");
                            Console.ReadLine();
                        }
                    }
                    catch (Exception)
                    {
                        //nothing to see here
                    }
                }
            }
        }
    }
}

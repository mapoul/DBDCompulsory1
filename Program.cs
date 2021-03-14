using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace DBDCompulsory1
{
    class Program
    {
        private static string conString = "Data Source = MPOUL; Initial Catalog = Company; Integrated Security = True";
        static SqlConnection con = new SqlConnection(conString);

        private static bool programRun = true;
        static void Main(string[] args)
        {
            
            while (programRun)
            {
                Console.WriteLine("Please chose below what you wish to do");
                Console.WriteLine("0. Exit this menu");
                Console.WriteLine("1. Create a Department");
                Console.WriteLine("2. Update a Department Name");
                Console.WriteLine("3. Update a Department Manager");
                Console.WriteLine("4. Delete a Department");
                Console.WriteLine("5. Get a Department");
                Console.WriteLine("6. Get all Departments");
                int option = Int32.Parse(Console.ReadLine());
                Console.WriteLine("You have chosen: " + option);

                switch (option)
                {
                    case 0:
                        programRun = false;
                        break;
                    case 1:
                        Console.WriteLine("You have chosen to create a new department \n Please write the Department Name");
                        string Dname = Console.ReadLine();
                        Console.WriteLine("Please enter Manager SSN - MAX 9 digits");
                        string mgrSSN = Console.ReadLine();

                        if (mgrSSN.Trim().Length <= 9)
                        {
                            int MGRSSN = Int32.Parse(mgrSSN);
                            CreateDepartment(Dname, MGRSSN);
                        }
                        else
                        {
                            Console.WriteLine("Manager SSN was too long, please enter correct Manager SSN");
                            int MGRSSN = Int32.Parse(Console.ReadLine());
                            CreateDepartment(Dname, MGRSSN);
                        }

                        break;
                    case 2:
                        Console.WriteLine("You have chosen to Update a department name \n Please write the Department Name");
                        string dname = Console.ReadLine();
                        Console.WriteLine("Please enter The Department number ");
                        int Dnumber = Int32.Parse(Console.ReadLine());
                        UpdateDepartmentName(dname, Dnumber);
                        break;
                    case 3:
                        Console.WriteLine("You have chosen to create a new department \n Please write the Department Number");
                        int dNumber = Int32.Parse(Console.ReadLine());
                        Console.WriteLine("Please enter Manager SSN - MAX 9 digits");
                        string Mgrssn = Console.ReadLine();

                        if (Mgrssn.Trim().Length <= 9)
                        {
                             int MGRSSN = Int32.Parse(Mgrssn);
                            UpdateDepartmentManager(dNumber, MGRSSN);
                        }
                        else
                        {
                            Console.WriteLine("Manager SSN was too long, please enter correct Manager SSN");
                            int MGRSSN = Int32.Parse(Console.ReadLine());
                            UpdateDepartmentManager(dNumber, MGRSSN);
                        }
                        
                        break;
                    case 4:
                        Console.WriteLine("You have chosen to delete a  department \n Please write the Department Number");
                        int Number = Int32.Parse(Console.ReadLine());
                        DeleteDepartment(Number);
                        break;
                    case 5:
                        Console.WriteLine("You have chosen to get info about a department \n Please write the Department Number");
                        int number = Int32.Parse(Console.ReadLine());
                        GetDepartment(number);
                        break;
                    case 6:
                        Console.WriteLine("You have chosen to get info about all departments");
                        GetAllDepartments();
                        break;
                }

            }
           
        }

        private static void CreateDepartment(string DName, int MgrSSN)
        {
            
            using (var conn = new SqlConnection(conString))
            using (var command = new SqlCommand("dbo.usp_CreateDepartment", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                command.Parameters.AddWithValue("@DName", DName.Trim());
                command.Parameters.AddWithValue("@MgrSSN", MgrSSN);
                command.ExecuteNonQuery();
            }

        }

        private static void UpdateDepartmentName(string DName, int DNumber)
        {
            using (var conn = new SqlConnection(conString))
            using (var command = new SqlCommand("dbo.usp_UpdateDepartmentName", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                command.Parameters.AddWithValue("@DName", DName.Trim());
                command.Parameters.AddWithValue("@DNumber", DNumber);
                command.ExecuteNonQuery();
            }
        }

        private static void UpdateDepartmentManager(int DNumber, int MgrSSN)
        {
            using (var conn = new SqlConnection(conString))
            using (var command = new SqlCommand("dbo.usp_UpdateDepartmentManager", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                command.Parameters.AddWithValue("@DNumber", DNumber);
                command.Parameters.AddWithValue("@MgrSSN", MgrSSN);
                command.ExecuteNonQuery();
            }
        }

        private static void DeleteDepartment(int DNumber)
        {
            using (var conn = new SqlConnection(conString))
            using (var command = new SqlCommand("dbo.usp_DeleteDepartment", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                command.Parameters.AddWithValue("@DNumber", DNumber);
                command.ExecuteNonQuery();
            }
        }

        private static void GetDepartment(int DNumber)
        {
            using (var conn = new SqlConnection(conString))
            using (var command = new SqlCommand("dbo.usp_GetDepartment", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                command.Parameters.AddWithValue("@DNumber", DNumber);
                DataTable dt = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter(command);
                ad.Fill(dt);
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        Console.Write(dt.Columns[i].ColumnName + " : ");
                        Console.WriteLine(dt.Rows[j].ItemArray[i]);
                        Console.WriteLine();
                    }
                }
                command.ExecuteNonQuery();
            }
        }

        private static void GetAllDepartments()
        {
            using (var conn = new SqlConnection(conString))
            using (var command = new SqlCommand("usp_GetAllDepartments", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                DataTable dt = new DataTable();
                SqlDataAdapter ad = new SqlDataAdapter(command);
                ad.Fill(dt);
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        Console.Write(dt.Columns[i].ColumnName + " : ");
                        Console.WriteLine(dt.Rows[j].ItemArray[i]);
                        Console.WriteLine();
                    }
                }
                command.ExecuteNonQuery();
            }
        }
    }
}

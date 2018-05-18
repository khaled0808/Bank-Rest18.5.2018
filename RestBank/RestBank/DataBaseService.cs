using BankApplication;
using BankData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace RestBank
{
    public class DataBaseService
    {

        private static string ConnectionString = "Data Source=.\\SQLEXPRESS;Integrated security=true;database=Bankdb";

        public static void registPerson(Person person)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (var sqlCommand = new SqlCommand("  USE BankDB; INSERT INTO Person (FirstName,LastName,Address, Occupation, Marital_status,Childern,BankID,Salary,tel) VALUES(@FirstName,@LastName,@Adresse,@Occupation,@Marital_status,@Childern,@BankID,@Salary,@tel) ", connection))
                {
                    sqlCommand.Parameters.AddWithValue("@FirstName", person.FirstName);
                    sqlCommand.Parameters.AddWithValue("@LastName", person.LastName);
                    sqlCommand.Parameters.AddWithValue("@Adresse", person.address);
                    sqlCommand.Parameters.AddWithValue("@Occupation", person.Occupation);
                    sqlCommand.Parameters.AddWithValue("@Marital_status", person.Maritalstatus);
                    sqlCommand.Parameters.AddWithValue("@Childern", person.Noofchildren);
                    sqlCommand.Parameters.AddWithValue("@BankID", person.BankID);
                    sqlCommand.Parameters.AddWithValue("@Salary", person.Salary);
                    sqlCommand.Parameters.AddWithValue("@tel", person.Tel);

                    try
                    {
                        connection.Open();
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        public static decimal getPayIN(string acountId)
        {
            decimal payIN = 0.0m;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "USE BankDB; SELECT PayIN From Acount where AcountID='" + acountId + "'";
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        payIN = reader.GetDecimal(0);
                    }
                }
            }

            return payIN;


        }


        public static decimal getTakeOff(string acountId)
        {
            decimal payIN = 0.0m;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "USE BankDB; SELECT TakeOff From Acount where AcountID='" + acountId + "'";
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        payIN = reader.GetDecimal(0);
                    }
                }
            }
            return payIN;
        }

        public static List<ResultData> getfilterbank(string occupation, string marital_status, string operation)
        {
            string sql = "";
            var listOfFilterData = new List<ResultData>();

            SqlConnection connection = new SqlConnection(ConnectionString);
            {
                connection.Open();
                switch (operation)
                {
                    case "OR":
                        sql = " use BankDB; select Person.FirstName,Person.PersonID,Person.LastName,installation.Socket,installation.Heater ,installation.Jallousien,installation.sensoren,person.Salary,Person.Old,person.Health_status,person.BankID,person.Childern,  Person.Address,Person.tel,Person.Marital_status ,Person.Occupation,Acount.TakeOff,Acount.Amount,Acount.AcountID,Acount.PayIN from Person left join Acount Acount on Acount.PersonID = Person.PersonID left Join installation installation  on installation.PersonID = Person.PersonID  WHERE Person.Occupation ='" + occupation + "'OR Person.Marital_status ='" + marital_status + "' order by Person.PersonID";
                        break;
                    case "AND":

                        sql = " use BankDB; select Person.FirstName,Person.PersonID,Person.LastName,installation.Socket,installation.Heater ,installation.Jallousien,installation.sensoren,person.Salary,Person.Old,person.Health_status,person.BankID,person.Childern,  Person.Address,Person.tel,Person.Marital_status ,Person.Occupation,Acount.TakeOff,Acount.Amount,Acount.AcountID,Acount.PayIN from Person left join Acount Acount on Acount.PersonID = Person.PersonID left Join installation installation  on installation.PersonID = Person.PersonID  WHERE Person.Occupation ='" + occupation + "'AND Person.Marital_status ='" + marital_status + "' order by Person.PersonID";
                        break;
                }

                using (var command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var result = new ResultData();
                                result.FirstName = reader["FirstName"].ToString();
                                result.MaritalStatus = reader["Marital_status"].ToString();
                                result.Salary = reader["Salary"].ToString();
                                result.LastName = reader["LastName"].ToString();
                                result.address = reader["Address"].ToString();
                                result.Amount = reader["Amount"].ToString();
                                result.tel = reader["tel"].ToString();
                                result.Healthstatus = reader["Health_status"].ToString();
                                result.Noofchildren = reader["Childern"].ToString();
                                result.Old = reader["Old"].ToString();
                                result.TakeOff = reader["TakeOff"].ToString();
                                result.PersonID = reader["PersonID"].ToString();
                                result.AcountID = reader["AcountID"].ToString();
                                result.Occupation = reader["Occupation"].ToString();
                                result.sensoren = reader["sensoren"].ToString();
                                result.Socket = reader["Socket"].ToString();
                                result.Jallousien = reader["Jallousien"].ToString();
                                result.Heater = reader["Heater"].ToString();
                                result.PayIN = reader["PayIN"].ToString();
                                result.BankID = reader["BankID"].ToString();
                                listOfFilterData.Add(result);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return listOfFilterData;
        }

        public static string[] getOccupation()
        {
            List<string> listOccupation = new List<string>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "USE BankDB; SELECT DISTINCT  Occupation From Person  WHERE Occupation IS NOT NULL";
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listOccupation.Add(reader.GetString(0));
                    }
                }
            }
            return listOccupation.ToArray();
        }

        public static string[] getMaritalStatus()
        {
            List<string> listMaritalStatus = new List<string>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "USE BankDB; SELECT DISTINCT  Marital_status From Person  WHERE Marital_status IS NOT NULL";
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listMaritalStatus.Add(reader.GetString(0));
                    }
                }
            }
            return listMaritalStatus.ToArray();
        }

        public static string[] getBankName()
        {
            List<string> listBank = new List<string>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "USE BankDB; SELECT Name From Bank";
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listBank.Add(reader.GetString(0));
                    }
                }
            }
            return listBank.ToArray();
        }

        public static List<ResultData> getUSersBank(string bankID)
        {
            var listOfPerson = new List<ResultData>();

            SqlConnection connection = new SqlConnection(ConnectionString);
            {
                connection.Open();
                string sql = "     use BankDB; select Person.FirstName,Person.PersonID,Person.LastName,installation.Socket,installation.Heater ,installation.Jallousien,installation.sensoren,person.Salary,Person.Old,person.Health_status,person.BankID,person.Childern,Person.Address,Person.tel,Person.Marital_status ,Person.Occupation ,Acount.TakeOff,Acount.Amount,Acount.AcountID,Acount.PayIN from bank left join Person  Person on Person.BankID = bank.BankID left join Acount Acount on Acount.PersonID = Person.PersonID left Join installation installation  on installation.PersonID = Person.PersonID Where Person.BankID='" + bankID + "' order by Person.PersonID";
                using (var command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var result = new ResultData();
                                result.FirstName = reader["FirstName"].ToString();
                                result.MaritalStatus = reader["Marital_status"].ToString();
                                result.Salary = reader["Salary"].ToString();
                                result.LastName = reader["LastName"].ToString();
                                result.address = reader["Address"].ToString();
                                result.Amount = reader["Amount"].ToString();
                                result.tel = reader["tel"].ToString();
                                result.Healthstatus = reader["Health_status"].ToString();
                                result.Noofchildren = reader["Childern"].ToString();
                                result.Old = reader["Old"].ToString();
                                result.TakeOff = reader["TakeOff"].ToString();
                                result.PersonID = reader["PersonID"].ToString();
                                result.AcountID = reader["AcountID"].ToString();
                                result.Occupation = reader["Occupation"].ToString();
                                //result.BankName = reader["Name"].ToString();
                                result.PayIN = reader["PayIN"].ToString();
                                result.BankID = reader["BankID"].ToString();
                                result.Socket = reader["Socket"].ToString();
                                result.Jallousien = reader["Jallousien"].ToString();
                                result.sensoren = reader["sensoren"].ToString();
                                result.Heater = reader["Heater"].ToString();
                                listOfPerson.Add(result);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return listOfPerson;
        }



        public static List<ResultData> getAllAcounts()
        {
            var listOfPerson = new List<ResultData>();

            SqlConnection connection = new SqlConnection(ConnectionString);
            {
                connection.Open();
                string sql = " use BankDB; select Person.FirstName,Person.PersonID,Person.LastName,installation.Socket,installation.Heater ,installation.Jallousien,installation.sensoren,person.Salary,Person.Old,person.Health_status,person.BankID,person.Childern,  Person.Address,Person.tel,Person.Marital_status ,Person.Occupation,Acount.TakeOff,Acount.Amount,Acount.AcountID,Acount.PayIN from Person left join Acount Acount on Acount.PersonID = Person.PersonID left Join installation installation  on installation.PersonID = Person.PersonID order by Person.PersonID";
                using (var command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var result = new ResultData();
                                result.FirstName = reader["FirstName"].ToString();
                                result.MaritalStatus = reader["Marital_status"].ToString();
                                result.Salary = reader["Salary"].ToString();
                                result.LastName = reader["LastName"].ToString();
                                result.address = reader["Address"].ToString();
                                result.Amount = reader["Amount"].ToString();
                                result.tel = reader["tel"].ToString();
                                result.Healthstatus = reader["Health_status"].ToString();
                                result.Noofchildren = reader["Childern"].ToString();
                                result.Old = reader["Old"].ToString();
                                result.TakeOff = reader["TakeOff"].ToString();
                                result.PersonID = reader["PersonID"].ToString();
                                result.AcountID = reader["AcountID"].ToString();
                                result.Occupation = reader["Occupation"].ToString();
                                //result.BankName = reader["Name"].ToString();
                                result.PayIN = reader["PayIN"].ToString();
                                result.BankID = reader["BankID"].ToString();
                                result.Socket = reader["Socket"].ToString();
                                result.Jallousien = reader["Jallousien"].ToString();
                                result.sensoren = reader["sensoren"].ToString();
                                result.Heater = reader["Heater"].ToString();

                                listOfPerson.Add(result);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return listOfPerson;

        }




        public static void addBank(Bank bank)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (var sqlCommand = new SqlCommand("USE BankDB; INSERT INTO Bank (Address,Name,Telephone) VALUES (@Address,@Name,@Telephone) ", connection))
                {
                    sqlCommand.Parameters.AddWithValue("@Address", bank.Address ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@Name", bank.Name ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@Telephone", bank.Tel ?? (object)DBNull.Value);
                    try
                    {
                        connection.Open();
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                }
            }
        }


        public static bool login(Login login)
        {
            string userExit = "";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "USE BankDB; SELECT UserName,Password From Login  WHERE UserName='" + login.UserName + "' AND Password ='" + Encrypt(login.Password) + "'";
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userExit = reader.GetString(0);
                    }
                }
            }
            if (userExit != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool user(Login user)
        {
            string userExit = "";
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "USE BankDB; SELECT UserName,Password From Login  WHERE UserName='" + user.UserName + "' AND Password ='" + Encrypt(user.Password) + "'";
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userExit = reader.GetString(0);
                    }
                }
            }

            if (userExit == "")
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "USE bankdb; INSERT INTO Login ( UserName ,Password)values(@UserName,@Password)";
                    using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                    {
                        sqlCommand.Parameters.AddWithValue("@UserName", user.UserName);
                        sqlCommand.Parameters.AddWithValue("@Password", Encrypt(user.Password));
                        try
                        {
                            sqlCommand.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            if (userExit != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static string Encrypt(string clearText)
        {
            try
            {
                string EncryptionKey = "BankApplication";
                byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        clearText = Convert.ToBase64String(ms.ToArray());
                    }
                }
                return clearText;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string[] getHealthStatus()
        {
            try
            {
                List<string> listHealth_status = new List<string>();
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "USE BankDB; SELECT  DISTINCT Health_status From Person";
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            listHealth_status.Add(reader.GetString(0));
                        }
                    }
                }
                return listHealth_status.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void addCustomer(ResultData resultData)
        {
            int userExit = 0;
            int newCustomer = 0;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "USE BankDB; SELECT PersonID From Person  WHERE FirstName='" + resultData.FirstName + "' AND LastName ='" + resultData.LastName + "'";
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    try
                    {
                        while (reader.Read())
                        {
                            userExit = reader.GetInt32(0);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            if (userExit == 0)
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "   use BankDB; insert into Person(FirstName,LastName,Address,Old,Occupation,Marital_status,Salary,Childern,tel,BankID,Health_status)values(@FirstName,@LastName,@Address,@Old,@Occupation,@Marital_status,@Salary,@Childern,@tel,@BankID,@Health_status)";
                    using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                    {
                        sqlCommand.Parameters.AddWithValue("@FirstName", resultData.FirstName ?? (object)DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@LastName", resultData.LastName ?? (object)DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@Address", resultData.address ?? (object)DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@Old", resultData.Old ?? (object)DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@Occupation", resultData.Occupation ?? (object)DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@Marital_status", resultData.MaritalStatus ?? (object)DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@Salary", resultData.Salary ?? (object)DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@Childern", resultData.Noofchildren ?? (object)DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@tel", resultData.tel ?? (object)DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@BankID", Convert.ToString(resultData.BankID) ?? (object)DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@Health_status", resultData.Healthstatus ?? (object)DBNull.Value);
                        try
                        {
                            sqlCommand.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            throw ex;
                        }
                    }

                    using (SqlCommand command = connection.CreateCommand())
                    {
                        command.CommandText = "USE BankDB; SELECT PersonID From Person  WHERE FirstName='" + resultData.FirstName + "' AND LastName ='" + resultData.LastName + "'";
                        using (var reader = command.ExecuteReader())
                        {
                            try
                            {
                                while (reader.Read())
                                {
                                    newCustomer = reader.GetInt32(0);
                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
                    string queryacount = " use bankDB; insert into Acount(TakeOff, PersonID, PayIN, Amount)Values(@TakeOff, @PersonID, @PayIN, @Amount)";

                    using (SqlCommand sqlCommand = new SqlCommand(queryacount, connection))
                    {
                        sqlCommand.Parameters.AddWithValue("TakeOff", resultData.TakeOff ?? (object)DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("PersonID", Convert.ToString(newCustomer) ?? (object)DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@PayIN", resultData.PayIN ?? (object)DBNull.Value);
                        sqlCommand.Parameters.AddWithValue("@Amount", resultData.Amount ?? (object)DBNull.Value);
                        try
                        {
                            sqlCommand.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            throw ex;
                        }
                    }

                    string queryacountinstallation = " use bankDB;   insert into installation(Socket, Heater, Jallousien, sensoren,PersonID)Values(@Socket, @Heater, @Jallousien, @sensoren,@PersonID)";

                    using (SqlCommand sqlCommand = new SqlCommand(queryacountinstallation, connection))
                    {
                        sqlCommand.Parameters.AddWithValue("Socket", resultData.Socket);
                        sqlCommand.Parameters.AddWithValue("Heater", resultData.Heater);
                        sqlCommand.Parameters.AddWithValue("@Jallousien", resultData.Jallousien);
                        sqlCommand.Parameters.AddWithValue("@sensoren", resultData.sensoren);
                        sqlCommand.Parameters.AddWithValue("@PersonID", newCustomer);
                        try
                        {
                            sqlCommand.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            throw ex;
                        }
                    }
                }
            }
            else
            {

            }
        }

        public static DeleteAcount deleteAcount(DeleteAcount delete)
        {
            try
            {
                string Query = " delete from MovementsAccount where AcountID='" + delete.AcountID +
                    "' delete from AcountType where AcountID='" + delete.AcountID +
                    "'   delete from Acount where AcountID='" + delete.AcountID +
                    "' delete from Login where Login.PersonID='" + delete.PersonID +
                    "' delete from installation where PersonID='" + delete.PersonID +
                    "' delete from Person where PersonID='" + delete.PersonID +
                    "'";
                SqlConnection SQLConn = new SqlConnection(ConnectionString);
                SqlCommand MyCommand = new SqlCommand(Query, SQLConn);
                SqlDataReader MyReader;
                SQLConn.Open();
                MyReader = MyCommand.ExecuteReader();
                SQLConn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

        public static ResultData getCustomerDetails(string personId)
        {
            ResultData result = new ResultData();
            SqlConnection connection = new SqlConnection(ConnectionString);
            {
                string sql = " use BankDB; select Person.FirstName,Person.PersonID,Person.LastName,installation.Socket,installation.Heater ,installation.Jallousien,installation.sensoren,person.Salary,Person.Old,person.Health_status,person.BankID,person.Childern, bank.Name,Person.Address,Person.tel,Person.Marital_status ,bank.Name,Person.Occupation ,Acount.TakeOff,Acount.Amount,Acount.AcountID,Acount.PayIN from bank left join Person  Person on Person.BankID = bank.BankID left join Acount Acount on Acount.PersonID = Person.PersonID left Join installation installation  on installation.PersonID = Person.PersonID   WHERE Person.PersonID='" + personId + "'";
                using (var command = new SqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.FirstName = reader["FirstName"].ToString();
                                result.MaritalStatus = reader["Marital_status"].ToString();
                                result.Salary = reader["Salary"].ToString();
                                result.LastName = reader["LastName"].ToString();
                                result.address = reader["Address"].ToString();
                                result.Amount = reader["Amount"].ToString();
                                result.tel = reader["tel"].ToString();
                                result.Healthstatus = reader["Health_status"].ToString();
                                result.Noofchildren = reader["Childern"].ToString();
                                result.Old = reader["Old"].ToString();
                                result.TakeOff = reader["TakeOff"].ToString();
                                result.PersonID = reader["PersonID"].ToString();
                                result.AcountID = reader["AcountID"].ToString();
                                result.Occupation = reader["Occupation"].ToString();
                                //result.BankName = reader["Name"].ToString();
                                result.PayIN = reader["PayIN"].ToString();
                                result.BankID = reader["BankID"].ToString();
                                result.Socket = reader["Socket"].ToString();
                                result.sensoren = reader["sensoren"].ToString();
                                result.Jallousien = reader["Jallousien"].ToString();
                                result.Heater = reader["Heater"].ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return result;
        }

        public static void updateAcount(ResultData resultData)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                string query = " update Person set Occupation=@Occupation,tel=@Tel,Address=@Address,Salary=@Salary,Marital_status=@Marital_status,Health_status=@Health_status where PersonID='" + resultData.PersonID + "' ";
                using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@Address", resultData.address);
                    sqlCommand.Parameters.AddWithValue("@Occupation", resultData.Occupation);
                    sqlCommand.Parameters.AddWithValue("@Marital_status", resultData.MaritalStatus);
                    sqlCommand.Parameters.AddWithValue("@Salary", resultData.Salary);
                    sqlCommand.Parameters.AddWithValue("@Health_status", resultData.Healthstatus);
                    sqlCommand.Parameters.AddWithValue("@Tel", resultData.tel);

                    try
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                }
                query = "  use BankDB;update Acount set PayIN=@PayIN ,TakeOff=@TakeOff where PersonID=@PersonID";
                using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@PayIN", resultData.PayIN ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@TakeOff", resultData.TakeOff ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@PersonID", resultData.PersonID ?? (object)DBNull.Value);
                    try
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                }
                query = "  use BankDB;update installation set sensoren=@sensoren where PersonID=@PersonID";
                using (SqlCommand sqlCommand = new SqlCommand(query, connection))
                {
                    sqlCommand.Parameters.AddWithValue("@sensoren", resultData.sensoren ?? (object)DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@PersonID", resultData.PersonID ?? (object)DBNull.Value);
                    try
                    {
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                }
            }
        }
    }
}

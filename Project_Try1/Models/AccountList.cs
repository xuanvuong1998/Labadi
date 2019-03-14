using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Project_Try1.Models {
    public class Account {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

    }

    public class AccountData {

        public static List<Account> AccountList = new List<Account>();


       
        public bool CheckLoginAccount(string username, string password) {
            bool loginSuccessful = false;
            SqlConnection cnn = DBUtils.GetConnection();
            try {
                string SQL = "select *  from  Account where username = @username and password = @password";

                if (cnn.State == ConnectionState.Closed) {
                    cnn.Open();
                }

                SqlCommand cmd = new SqlCommand(SQL, cnn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows) loginSuccessful = true;
            } catch (Exception e) {
                Console.WriteLine(e);
                throw;
            } finally {
                cnn.Close();
            }

            return loginSuccessful;

        }

        public bool AddNewAccount(string username, string password, string name, string phone, string email) {
            bool Successful = false;
            SqlConnection cnn = DBUtils.GetConnection();

            try {
                string SQL = "insert into Account values(@username,@password,@fullname,@phone,@email)";
                if (cnn.State == ConnectionState.Closed) {
                    cnn.Open();
                }

                SqlCommand cmd = new SqlCommand(SQL, cnn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@fullname", name);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@email", email);
                int rowCount = cmd.ExecuteNonQuery();

                if (rowCount > 0) Successful = true;
            } catch (Exception e) {
                Console.WriteLine(e);
                throw;
            } finally {
                cnn.Close();
            }


            return Successful;

        }

        public bool checkDuplicateAccount(string username) {
            bool Successful = false;
            SqlConnection cnn = DBUtils.GetConnection();

            try {
                string SQL = "select *  from  Account where username = @username";



                if (cnn.State == ConnectionState.Closed) {
                    cnn.Open();
                }

                SqlCommand cmd = new SqlCommand(SQL, cnn);
                cmd.Parameters.AddWithValue("@username", username);



                Console.Write(cmd);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows) Successful = true;
            } catch (Exception e) {
                Console.WriteLine(e);
                throw;
            } finally {
                cnn.Close();
            }



            return Successful;
        }

        public bool checkDuplicateEmail(string email) {
            bool Successful = false;
            SqlConnection cnn = DBUtils.GetConnection();

            try {
                string SQL = "select *  from  Account where email = @email";


                if (cnn.State == ConnectionState.Closed) {
                    cnn.Open();
                }

                SqlCommand cmd = new SqlCommand(SQL, cnn);
                cmd.Parameters.AddWithValue("@email", email);



                Console.Write(cmd);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.HasRows) Successful = true;
            } catch (Exception e) {
                Console.WriteLine(e);
                throw;
            } finally {
                cnn.Close();
            }



            return Successful;
        }


    }
}
using System;
using System.Collections.Generic;
using Hospital.BaseClasses.Intefaces;
using Hospital.BaseClasses.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data.Common;

namespace Hospital.DataAccess.AzureSql
{
    public class AzHospital : IHospital
    {
        private readonly string connstring; 

        public AzHospital(string connectionString)
        {
            connstring = connectionString;
        }


    public List<HospitalCentre> GetHospitals()
        {
            if(string.IsNullOrEmpty(connstring))
                throw new ArgumentException("No connection string in config.json");

            List<HospitalCentre> HospitalCentreList = new List<HospitalCentre> ();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var sql = "SELECT * FROM dbo.Hospital";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            HospitalCentre hc = new HospitalCentre ();
                            hc.Id = reader.GetInt32(0);
                            hc.Name = reader.GetString(1);
                            hc.Address=reader.GetString(2);
                            hc.City=reader.GetString(3);
                            hc.Pincode=reader.GetInt32(4);
                            HospitalCentreList.Add(hc);
                            Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", reader.GetInt32(0), reader.GetString(1),reader.GetString(2),reader.GetString(3),reader.GetInt32(4));
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                        return null;
                    }
                    reader.Close();
                    conn.Close();
                }
                return HospitalCentreList;
            }
        }

    
    public List<HospitalCentre> GetHospitalName(string name)
        {
            if(string.IsNullOrEmpty(connstring))
                throw new ArgumentException("No connection string in config.json");

            List<HospitalCentre> HospitalCentreList = new List<HospitalCentre> ();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var sql = "SELECT * FROM dbo.Hospital where HospitalName like '%' + @hname + '%'";
                using (var cmd = new SqlCommand(sql, conn))
                {
                
                    cmd.Parameters.AddWithValue("@hname",name);
                    
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            HospitalCentre hc = new HospitalCentre ();
                            hc.Id=reader.GetInt32(0);
                            hc.Name = reader.GetString(1);
                            hc.Address=reader.GetString(2);
                            hc.City=reader.GetString(3);
                            hc.Pincode=reader.GetInt32(4);
                            HospitalCentreList.Add(hc);
                            Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}",reader.GetInt32(0), reader.GetString(1), reader.GetString(2),reader.GetString(3),reader.GetInt32(4));
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                        return null;
                    }
                    reader.Close();
                    conn.Close();
                }
                return HospitalCentreList;
            }
        }



    public List<HospitalCentre> PostHospitalName(int id1,string name1,string address1,string city1,int pincode1)
        {
            if(string.IsNullOrEmpty(connstring))
                throw new ArgumentException("No connection string in config.json");

            List<HospitalCentre> HospitalCentreList = new List<HospitalCentre> ();

            using (var conn = new SqlConnection(connstring))
            {                
                conn.Open();
                var sql = "BEGIN IF NOT EXISTS(SELECT * FROM Hospital where HospitalName='@HospitalName')BEGIN INSERT INTO Hospital (ID,HospitalName,Address,City,PinCode) VALUES(@ID,@HospitalName,@Address,@City,@PinCode) END END ";
                //"update [hospital] set [ID]=@Id,[HospitalName]=@HospitalName,[Address]=@Address,[City]=@City,[PinCode]=@Pincode where [HospitalName] != @HospitalName";
                //Insert into dbo.Hospital (ID,HospitalName,Address,City,PinCode) Values (@ID,@HospitalName,@Address,@City,@PinCode)" + "where @HospitalName !=HospitalName
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", id1);
                    cmd.Parameters.AddWithValue("@HospitalName", name1);
                    cmd.Parameters.AddWithValue("@Address", address1);
                    cmd.Parameters.AddWithValue("@City", city1);
                    cmd.Parameters.AddWithValue("@PinCode", pincode1);
                    
                    cmd.ExecuteNonQuery();    
                }
                conn.Close();
            }
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var sql = "SELECT * FROM dbo.Hospital";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            HospitalCentre hc = new HospitalCentre ();
                            hc.Id = reader.GetInt32(0);
                            hc.Name = reader.GetString(1);
                            hc.Address=reader.GetString(2);
                            hc.City=reader.GetString(3);
                            hc.Pincode=reader.GetInt32(4);
                            HospitalCentreList.Add(hc);
                            Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", reader.GetInt32(0), reader.GetString(1),reader.GetString(2),reader.GetString(3),reader.GetInt32(4));
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                        return null;
                    }
                    reader.Close();
                    conn.Close();
                }
                return HospitalCentreList;
            }
        }


   public List<HospitalCentre> PatchHospitalName(int id2,string name2)
        {
            if(string.IsNullOrEmpty(connstring))
                throw new ArgumentException("No connection string in config.json");

            List<HospitalCentre> HospitalCentreList = new List<HospitalCentre> ();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var sql = "UPDATE [Hospital] SET [HospitalName]=@HospitalName WHERE [ID]=@ID";
                using (var cmd = new SqlCommand(sql, conn))
                {

                    cmd.Parameters.AddWithValue("@ID", id2);
                    cmd.Parameters.AddWithValue("@HospitalName", name2);
                    
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var sql = "SELECT * FROM dbo.Hospital";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            HospitalCentre hc = new HospitalCentre ();
                            hc.Id = reader.GetInt32(0);
                            hc.Name = reader.GetString(1);
                            hc.Address=reader.GetString(2);
                            hc.City=reader.GetString(3);
                            hc.Pincode=reader.GetInt32(4);
                            HospitalCentreList.Add(hc);
                            Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", reader.GetInt32(0), reader.GetString(1),reader.GetString(2),reader.GetString(3),reader.GetInt32(4));
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                        return null;
                    }
                    reader.Close();
                    conn.Close();
                }
                return HospitalCentreList;
            }  
        }


    public List<HospitalCentre> DeleteHospitalName(int id1)
        {
            if(string.IsNullOrEmpty(connstring))
                throw new ArgumentException("No connection string in config.json");

            List<HospitalCentre> HospitalCentreList = new List<HospitalCentre> ();
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var sql = "delete from dbo.Hospital where id=@Id";
                using (var cmd = new SqlCommand(sql, conn))
                {

                    cmd.Parameters.AddWithValue("@ID", id1);
                    
                    cmd.ExecuteNonQuery();
                    
                    
                }
                conn.Close();
            }
            using (var conn = new SqlConnection(connstring))
            {
                conn.Open();
                var sql = "SELECT * FROM dbo.Hospital";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            HospitalCentre hc = new HospitalCentre ();
                            hc.Id = reader.GetInt32(0);
                            hc.Name = reader.GetString(1);
                            hc.Address=reader.GetString(2);
                            hc.City=reader.GetString(3);
                            hc.Pincode=reader.GetInt32(4);
                            HospitalCentreList.Add(hc);
                            Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", reader.GetInt32(0), reader.GetString(1),reader.GetString(2),reader.GetString(3),reader.GetInt32(4));
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found.");
                        return null;
                    }
                    reader.Close();
                    conn.Close();
                }
                return HospitalCentreList;
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Office.Interop.Excel;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using WebApplication1.Models;

namespace WebApplication1.DataAccess
{
    public class DataOwnerAccess
    {
        private SqlConnection _Conn;
        private string _ConnectionString;

        public DataOwnerAccess()
        {
            try
            {
                _ConnectionString = ConfigurationManager.ConnectionStrings["dataowner"].ConnectionString;
                _Conn = new SqlConnection(_ConnectionString);
                _Conn.Open();

            }
            catch (Exception ex)
            {

            }
        }

        public void InsertRecordDataFormat(dataFormat dataFormat)
        {
            try 
            {
                const string sqlQuery = @"INSERT INTO data_format
                                            (idDataFormat,dataFormatName) VALUES (@idDataFormat,@dataFormatName)";

                SqlCommand sqlCommand = new SqlCommand(sqlQuery, _Conn);

                sqlCommand.Parameters.Add(new SqlParameter("@idDataFormat", GetFormatById(dataFormat)));
                sqlCommand.Parameters.Add(new SqlParameter("@dataFormatName", dataFormat.dataFormatName));

                sqlCommand.ExecuteNonQuery();
            }
            catch(Exception e)
            {

            }

        }

        public void InsertRecordLanguage(dataLanguage dataLanguage)
        {
            try
            {
                const string sqlQuery = @"INSERT INTO data_language
                                           (idDataLanguage, dataLanguageName) VALUES (@idDataLanguage, @dataLanguageName)";

                SqlCommand sqlCommand = new SqlCommand(sqlQuery, _Conn);

                sqlCommand.Parameters.Add(new SqlParameter("@idDataLanguage", GetLanguageById(dataLanguage)));
                sqlCommand.Parameters.Add(new SqlParameter("@dataLanguageName", dataLanguage.dataLanguageName));

                sqlCommand.ExecuteNonQuery();
            }
            catch(Exception exept)
            {

            }
        }


        public void InsertRecordDataUsage(dataUsage dataUsage, int dataFormat, int openData, int languageID)
        {
           
            try
            {
                const string sqlQuery = @"INSERT INTO data_usage
                                            (id_data_usage, open_data_id, date_of_usage, data_format_id,is_downloaded, language_id)
                                      VALUES
                                             (@id_data_usage, @open_data_id, @date_of_usage, @data_format_id,@is_downloaded, @language_id)";


                SqlCommand sqlCommand = new SqlCommand(sqlQuery, _Conn);

                sqlCommand.Parameters.Add(new SqlParameter("@id_data_usage", GetDataUsageMaxId()));
                sqlCommand.Parameters.Add(new SqlParameter("@open_data_id", openData));
                sqlCommand.Parameters.Add(new SqlParameter("@date_of_usage", dataUsage.date_of_usage));
                sqlCommand.Parameters.Add(new SqlParameter("@data_format_id", dataFormat));
                sqlCommand.Parameters.Add(new SqlParameter("@is_downloaded", dataUsage.is_downloaded));
                sqlCommand.Parameters.Add(new SqlParameter("@language_id", languageID));

                sqlCommand.ExecuteNonQuery();
            }
            catch(Exception e)
            {

            }
        }

        public void InsertRecordOpenData(openData openData)
        {
            try
            {
                const string sqlQuery = @"INSERT INTO open_data
                                            (id_data,data_url) VALUES (@id_data,@data_url)";

                SqlCommand sqlCommand = new SqlCommand(sqlQuery, _Conn);

                sqlCommand.Parameters.Add(new SqlParameter("@id_data", GetOpenById(openData)));
                sqlCommand.Parameters.Add(new SqlParameter("@data_url", openData.data_url));

                sqlCommand.ExecuteNonQuery();
            }
            catch(Exception e)
            {

            }
        }

        public int GetOpenDataMaxId()
        {
            int maxId = 0;
            try
            {
                const string sqlQuery = @"SELECT IsNull(Max(id_data),0)+1 MaxId FROM open_data";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, _Conn);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        maxId = Convert.ToInt32(reader[0]);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return maxId;
        }

        public int GetDataUsageMaxId()
        {
            int maxId = 0;
            try
            {
                const string sqlQuery = @"SELECT IsNull(Max(id_data_usage),0)+1 MaxId FROM data_usage";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, _Conn);

               using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        maxId = Convert.ToInt32(reader[0]);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return maxId;
        }

        public int GetDataFormatMaxId()
        {
            int maxId = 0;
            try 
            {
                const string sqlQuery = @"SELECT IsNull(Max(id_data_format),0)+1 MaxId FROM data_format";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, _Conn);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        maxId = Convert.ToInt32(reader[0]);
                    }
                }
            }
            catch(Exception e)
            {

            }
            return maxId;
        }
        internal int GetLanguageById(dataLanguage dataLanguage)
          {
            int id = 0;
            try
            {

                bool check = IsLanguageExist(dataLanguage);

                if (check == true && dataLanguage.dataLanguageName == "Spanish")
                {
                    Console.WriteLine("Already Excist");
                    id = GetSpanishId(dataLanguage);

                }

                else if(check == true && dataLanguage.dataLanguageName== "English")
                {
                    id = GetEnglishId(dataLanguage);
                }

                else if(check== true && dataLanguage.dataLanguageName == "Swedish")
                {
                    id = GetSwedishId(dataLanguage);
                }
  
                else
                {
                    const string sqlQuery = @"SELECT IsNull(Max(idDataLanguage),0)+1 MaxId FROM data_language";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, _Conn);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = Convert.ToInt32(reader[0]);
                        }
                    }
                }
            }
            catch (Exception exe)
            {

            }
            return id;
        }

        private int GetSwedishId(dataLanguage dataLanguage)
        {
            int id = 0;
            try
            {
                const string sqlQuery = @"SELECT idDataLanguage FROM data_language WHERE dataLanguageName LIKE '%Swedish%'";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, _Conn);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = Convert.ToInt32(reader[0]);
                    }
                }
            }
            catch (Exception exep)
            {

            }
            return id;
        }

        private int GetEnglishId(dataLanguage dataLanguage)
        {
            int id = 0;
            try
            {
                const string sqlQuery = @"SELECT idDataLanguage FROM data_language WHERE dataLanguageName LIKE '%English%'";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, _Conn);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = Convert.ToInt32(reader[0]);
                    }
                }
            }
            catch (Exception exep)
            {

            }
            return id;
        }

        private int GetSpanishId(dataLanguage dataLanguage)
        {
            int id = 0;
            try
            {
                const string sqlQuery = @"SELECT idDataLanguage FROM data_language WHERE dataLanguageName LIKE '%Spanish%'";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, _Conn);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = Convert.ToInt32(reader[0]);
                    }
                }
            }
            catch (Exception exep)
            {

            }
            return id;
        }

        private bool IsLanguageExist(dataLanguage dataLanguage)
        {
            string data_language_name = dataLanguage.dataLanguageName;
            bool isExist = false;
            try
            {
                const string query = @"SELECT idDataLanguage FROM data_language WHERE dataLanguageName = @data_language_name";


                SqlCommand command = new SqlCommand(query, _Conn);
                command.Parameters.Add(new SqlParameter("@data_language_name", data_language_name));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        isExist = true;
                    }
                }
            }
            catch (Exception e)
            {

            }
            return isExist;
        }

        internal int GetFormatById(dataFormat format)
        {
           int id = 0;
           try
           {

              bool check =  IsFormatExist(format);

              if(check == true && format.dataFormatName == "JSON File")
                {
                    Console.WriteLine("Already Excist");
                    id = GetJsonFileId(format);
                                        
                }
              else if(check == true && format.dataFormatName == "HTML File")
                {
                    id = GetHtmlFileId(format);
                }
              else if(check == true && format.dataFormatName == "CSV File")
                {
                    id = GetCsvFileId(format);
                }
              else
                {
                    const string sqlQuery = @"SELECT IsNull(Max(idDataFormat),0)+1 MaxId FROM data_format";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, _Conn);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = Convert.ToInt32(reader[0]);
                        }
                    }
                }
            }
            catch(Exception exe)
            {

            }
            return id;
        }

        private int GetCsvFileId(dataFormat format)
        {
            int id = 0;
            try
            {
                const string sqlQuery = @"SELECT idDataFormat FROM data_format WHERE dataFormatName LIKE '%CSV%'";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, _Conn);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = Convert.ToInt32(reader[0]);
                    }
                }
            }
            catch (Exception exep)
            {

            }
            return id;
        }

        private int GetHtmlFileId(dataFormat format)
        {
            int id = 0;
            try
            {
                const string sqlQuery = @"SELECT idDataFormat FROM data_format WHERE dataFormatName LIKE '%HTML%'";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, _Conn);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = Convert.ToInt32(reader[0]);
                    }
                }
            }
            catch (Exception exep)
            {

            }
            return id;
        }

        private int GetJsonFileId(dataFormat format)
        {
            int id = 0;
            try
            {
                const string sqlQuery = @"SELECT idDataFormat FROM data_format WHERE dataFormatName LIKE '%JSON%'";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, _Conn);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = Convert.ToInt32(reader[0]);
                    }
                }
            }
            catch(Exception exep)
            {

            }
            return id;
        }

        public bool IsFormatExist(dataFormat format)
        {
            string dataFormatName = format.dataFormatName;
            bool isExist = false;
            try
            {
                const string query = @"SELECT idDataFormat FROM data_format WHERE dataFormatName = @dataFormatName";
                

                SqlCommand command = new SqlCommand(query, _Conn);
                command.Parameters.Add(new SqlParameter("@dataFormatName", dataFormatName));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        isExist = true;      
                    }
                }
            }
            catch(Exception e)
            {

            }
            return isExist;
        }

        internal int GetOpenById(openData open)
        {
             int id = 0;
           try
           {

              bool check =  IsOpenUrlExist(open);

              if(check == true && open.data_url== "http://localhost/server/phprest/api/read.php")
                {
                    Console.WriteLine("Already Excist");
                    id = GetJsonURLId(open);                      
                }

              else if(check == true && open.data_url == "https://opendata.smhi.se/apidocs/fireriskanalysis/")
                {
                    id = GetHtmlURLId(open);
                }
              else if(check == true && open.data_url == "https://skatteverket.entryscape.net/store/9/resource/667")
                {
                    id = GetCsvURLId(open);
                }
                else
                {
                    const string sqlQuery = @"SELECT IsNull(Max(id_data),0)+1 MaxId FROM open_data";
                    SqlCommand sqlCommand = new SqlCommand(sqlQuery, _Conn);

                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = Convert.ToInt32(reader[0]);
                        }
                    }
                }
            }
            catch(Exception exe)
            {

            }
            return id;
        }

        private int GetCsvURLId(openData open)
        {
            int id = 0;
            try
            {
                const string sqlQuery = @"SELECT id_data FROM open_data WHERE data_url LIKE '%https://skatteverket.entryscape.net/store/9/resource/667%'";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, _Conn);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = Convert.ToInt32(reader[0]);
                    }
                }
            }
            catch (Exception exep)
            {

            }
            return id;
        }

        private int GetHtmlURLId(openData open)
        {
            int id = 0;
            try
            {
                const string sqlQuery = @"SELECT id_data FROM open_data WHERE data_url LIKE '%https://opendata.smhi.se/apidocs/fireriskanalysis/%'";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, _Conn);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = Convert.ToInt32(reader[0]);
                    }
                }
            }
            catch (Exception exep)
            {

            }
            return id;

        }

        private int GetJsonURLId(openData open)
        {
            int id = 0;
            try
            {
                const string sqlQuery = @"SELECT id_data FROM open_data WHERE data_url LIKE '%http://localhost/server/phprest/api/read.php%'";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, _Conn);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = Convert.ToInt32(reader[0]);
                    }
                }
            }
            catch (Exception exep)
            {

            }
            return id;
        }

        private bool IsOpenUrlExist(openData open)
        {
            string data_url = open.data_url;
            bool isExist = false;
            try
            {
                const string query = @"SELECT id_data FROM open_data WHERE data_url = @data_url ";

                SqlCommand command = new SqlCommand(query, _Conn);
                command.Parameters.Add(new SqlParameter("@data_url", data_url));
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        isExist = true;
                    }
                }
            }
            catch (Exception e)
            {

            }
            return isExist;

        }

        internal int GetOwnerId(data_owner owner)
        {
            int id = 0;
            try
            {
                const string sqlQuery = @"SELECT idDataOwner FROM data_owner WHERE dataOwnerName LIKE '%Miun%'";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, _Conn);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = Convert.ToInt32(reader[0]);
                    }
                }
            }
            catch (Exception exep)
            {

            }
            return id;
        }

        internal int GetThemeId(data_theme theme)
        {
            int id = 0;
            try
            {
                const string sqlQuery = @"SELECT idDataTheme FROM data_theme WHERE dataThemeName LIKE '%Theme1%'";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, _Conn);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = Convert.ToInt32(reader[0]);
                    }
                }
            }
            catch (Exception exep)
            {

            }
            return id;
        }

        internal int GetFrequencyId(update_frequency frequency)
        {
            int id = 0;
            try
            {
                const string sqlQuery = @"SELECT idUpdateFrequency FROM update_frequency WHERE updateFrequencyName LIKE '%Monthly%'";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, _Conn);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = Convert.ToInt32(reader[0]);
                    }
                }
            }
            catch (Exception exep)
            {

            }
            return id;
        }

        internal int GetUsageById(dataUsage dataUsage)
        {
            int maxId = 0;
            try
            {
                const string sqlQuery = @"SELECT Max(id_data_usage) FROM data_usage";
                SqlCommand sqlCommand = new SqlCommand(sqlQuery, _Conn);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        maxId = Convert.ToInt32(reader[0]);
                    }
                }
            }
            catch (Exception e)
            {

            }
            return maxId;
        }
    }
}
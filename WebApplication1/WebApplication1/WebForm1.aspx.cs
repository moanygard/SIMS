using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json;
using WebApplication1.DataAccess;
using WebApplication1.Models;
using WebApplication1.RestPostEndPoint;



namespace WebApplication1
{
    
    public partial class WebForm1 : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

           try
            {
                Uri uri = new Uri("http://localhost/server/phprest/api/read.php");
                Process.Start(uri.ToString());

                DataOwnerAccess dataOwnerAccess = new DataOwnerAccess();
                var DataFormat = new dataFormat
                {
                    dataFormatName = "JSON File"
                };

                var DataUsage = new dataUsage
                {
                    date_of_usage = DateTime.Now,
                    is_downloaded = false
                };

                var OpenData = new openData
                {
                    //data_url = HttpContext.Current.Request.Url.AbsoluteUri
                    data_url = uri.ToString(),
                };

                var DataLanguage = new dataLanguage
                {
                    dataLanguageName = "Spanish"
                };

                var theme = new data_theme
                {
                    dataThemeName = "Theme1"
                };

                var frequency = new update_frequency
                {
                    updateFrequencyName = "Monthly"
                };

                var owner = new data_owner
                {
                    dataOwnerName = "Miun"
                };

                dataOwnerAccess.InsertRecordDataFormat(DataFormat);
                dataOwnerAccess.InsertRecordOpenData(OpenData);
                dataOwnerAccess.InsertRecordLanguage(DataLanguage);


                int id_usage = new DataOwnerAccess().GetUsageById(DataUsage);
                int id_format = new DataOwnerAccess().GetFormatById(DataFormat);
                int id_open = new DataOwnerAccess().GetOpenById(OpenData);
                int id_language = new DataOwnerAccess().GetLanguageById(DataLanguage);
                int id_owner = new DataOwnerAccess().GetOwnerId(owner);
                int id_theme = new DataOwnerAccess().GetThemeId(theme);
                int id_frequency = new DataOwnerAccess().GetFrequencyId(frequency);

                dataOwnerAccess.InsertRecordDataUsage(DataUsage, id_format, id_open,id_language);

                // Post request to DataUsage
                using (var client = new HttpClient())
                {
                    var endpoint = new Uri("https://localhost:7076/api/datausages");
                    var newPost = new UsagePost()
                    {
                        idDataUsage = 53,
                        openDataId = id_open,
                        dateOfUsage = DataUsage.date_of_usage,
                        dataFormatId = 1,
                        languageId = 1,
                        isDownloaded = 0,
                        dataFormat = new dataFormat()
                        {
                            idDataFormat = 1,
                            dataFormatName = DataFormat.dataFormatName,
                        },
                        language = new dataLanguage()
                        {
                            idDataLanguage = 1,
                            dataLanguageName = DataLanguage.dataLanguageName
                        },
                        openData = new OpenPost()
                        {
                            idData = 3,
                            dataUrl = OpenData.data_url,
                            dataOpenLicense = 1,
                            dataOwnerId = id_owner,
                            updateFrequencyId = id_frequency,
                            dataThemeId = id_theme,

                            dataOwner = new data_owner()
                            {
                                idDataOwner = 3,
                                dataOwnerName = owner.dataOwnerName,
                            },
                            dataTheme = new data_theme()
                            {
                                idDataTheme = id_theme,
                                dataThemeName = theme.dataThemeName,
                            },
                            updateFrequency = new update_frequency()
                            {
                                idUpdateFrequency = id_frequency,
                                updateFrequencyName = frequency.updateFrequencyName,
                            }
                        }
                    };
                    var newPostJson = JsonConvert.SerializeObject(newPost);
                    var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    System.Diagnostics.Debug.WriteLine(newPostJson.ToString());
                    var result = client.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                }


                //Post request to updateFrequency
                /* using (var client = new HttpClient())
                 {
                     var endpoint = new Uri("https://localhost:7076/api/updatefrequencies");
                     var newPost = new FrequencyPost()
                     {
                         updateFrequencyName = frequency.updateFrequencyName
                     };
                     var newPostJson = JsonConvert.SerializeObject(newPost);
                     var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                     var result = client.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                 }*/

                //Post request to Theme
                /*  using (var client = new HttpClient())
                  {
                      var endpoint = new Uri("https://localhost:7076/api/datathemes");
                      var newPost = new ThemePost()
                      {
                          dataThemeName = theme.dataThemeName
                      };
                      var newPostJson = JsonConvert.SerializeObject(newPost);
                      var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                      var result = client.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                  }*/

                //Post request to dataOwner
                /* using (var client = new HttpClient())
                 {
                     var endpoint = new Uri("https://localhost:7076/api/dataowners");
                     var newPost = new OwnerPost()
                     {
                         idDataOwner = 3,
                         dataOwnerName = owner.dataOwnerName
                     };
                     var newPostJson = JsonConvert.SerializeObject(newPost);
                     var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                     var result = client.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                 }*/


                // Post request to dataFormat
                /*  using (var client = new HttpClient())
                  {
                      var endpoint = new Uri("https://localhost:7076/api/dataformats");
                      var newPost = new FormatPost()
                      {
                          dataFormatName = DataFormat.dataFormatName
                      };
                      var newPostJson = JsonConvert.SerializeObject(newPost);
                      var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                      var result = client.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                  }*/


                // Post request to datalanguages
                /*  using (var client = new HttpClient())
                   {
                       var endpoint = new Uri("https://localhost:7076/api/datalanguages");
                       var newPost = new LanguagePost()
                       {
                          // idDataLanguage = new DataOwnerAccess().GetLanguageById(DataLanguage),
                           dataLanguageName = DataLanguage.dataLanguageName
                       };
                       var newPostJson = JsonConvert.SerializeObject(newPost);
                       var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                       var result = client.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                   }*/

                // Post request to openDatum
                /* using (var client = new HttpClient())
                 {
                     var endpoint = new Uri("https://localhost:7076/api/opendatum");

                     var newOpen = new OpenPost()
                     {
                         //idData = 13,
                         dataUrl = OpenData.data_url,
                         dataOpenLicense = 1,
                         dataOwnerId = id_owner,
                         dataThemeId = id_theme,
                         updateFrequencyId = id_frequency,

                         dataOwner = new data_owner()
                         {
                             idDataOwner = id_owner,
                             dataOwnerName = owner.dataOwnerName,
                         },
                         dataTheme = new data_theme()
                         {
                             idDataTheme = id_theme,
                             dataThemeName = theme.dataThemeName,
                         },
                         updateFrequency = new update_frequency()
                         {
                             idUpdateFrequency = id_frequency,
                             updateFrequencyName = frequency.updateFrequencyName,
                         }
                     }; 
                     var newPostJson = JsonConvert.SerializeObject(newOpen);
                     var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                     System.Diagnostics.Debug.WriteLine(newPostJson.ToString());
                     var result = client.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result; 
                 }*/
            }
            catch (Exception ex)
            {
                throw;
            }
        }
 
        protected void Button2_Click(object sender, EventArgs e)
        {

           Uri uri = new Uri("https://opendata.smhi.se/apidocs/fireriskanalysis/");
            Process.Start(uri.ToString());
            try
            {
                DataOwnerAccess dataOwnerAccess = new DataOwnerAccess();
                var DataFormat = new dataFormat
                {
                    dataFormatName = "HTML File"
                };

                var DataUsage = new dataUsage
                {
                    date_of_usage = DateTime.Now,
                    is_downloaded = false
                };

                var OpenData = new openData
                {
                    //data_url = HttpContext.Current.Request.Url.AbsoluteUri
                    data_url = uri.ToString(),
                };
                var DataLanguage = new dataLanguage
                {
                   dataLanguageName = "English"
                };
                var frequency = new update_frequency
                {
                    updateFrequencyName = "Daily"
                };

                var theme = new data_theme
                {
                    dataThemeName = "Theme2"
                };

                var owner = new data_owner
                {
                    dataOwnerName = "Miun"
                };


                dataOwnerAccess.InsertRecordDataFormat(DataFormat);
                dataOwnerAccess.InsertRecordOpenData(OpenData);
                dataOwnerAccess.InsertRecordLanguage(DataLanguage);
                



                int id_format = new DataOwnerAccess().GetFormatById(DataFormat);
                int id_open = new DataOwnerAccess().GetOpenById(OpenData);
                int id_language = new DataOwnerAccess().GetLanguageById(DataLanguage);

                int id_usage = new DataOwnerAccess().GetUsageById(DataUsage);               
                int id_owner = new DataOwnerAccess().GetOwnerId(owner);
                int id_theme = new DataOwnerAccess().GetThemeId(theme);
                int id_frequency = new DataOwnerAccess().GetFrequencyId(frequency);


                dataOwnerAccess.InsertRecordDataUsage(DataUsage, id_format, id_open, id_language);

                // Post request to DataUsage
               using (var client = new HttpClient())
                {
                    var endpoint = new Uri("https://localhost:7076/api/datausages");
                    var newPost = new UsagePost()
                    {
                        // idDataUsage = 46,
                        openDataId = id_open,
                        dateOfUsage = DataUsage.date_of_usage,
                        dataFormatId = 1,
                        languageId = 1,
                        isDownloaded = 0,
                        dataFormat = new dataFormat()
                        {
                            idDataFormat = 1,
                            dataFormatName = DataFormat.dataFormatName,
                        },
                        language = new dataLanguage()
                        {
                            idDataLanguage = 1,
                            dataLanguageName = DataLanguage.dataLanguageName
                        },
                        openData = new OpenPost()
                        {
                            idData = 2,
                            dataUrl = OpenData.data_url,
                            dataOpenLicense = 1,
                            dataOwnerId = id_owner,
                            updateFrequencyId = id_frequency,
                            dataThemeId = id_theme,

                            dataOwner = new data_owner()
                            {
                                idDataOwner = id_owner,
                                dataOwnerName = owner.dataOwnerName,
                            },
                            dataTheme = new data_theme()
                            {
                                idDataTheme = id_theme,
                                dataThemeName = theme.dataThemeName,
                            },
                            updateFrequency = new update_frequency()
                            {
                                idUpdateFrequency = id_frequency,
                                updateFrequencyName = frequency.updateFrequencyName,
                            }
                        }
                    };
                    var newPostJson = JsonConvert.SerializeObject(newPost);
                    var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    System.Diagnostics.Debug.WriteLine(newPostJson.ToString());
                    var result = client.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                }


                //Post request to updateFrequency
                /*using (var client = new HttpClient())
                {
                    var endpoint = new Uri("https://localhost:7076/api/updatefrequencies");
                    var newPost = new FrequencyPost()
                    {
                        updateFrequencyName = frequency.updateFrequencyName
                    };
                    var newPostJson = JsonConvert.SerializeObject(newPost);
                    var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                }*/

                //Post request to Theme
                /*  using (var client = new HttpClient())
                   {
                       var endpoint = new Uri("https://localhost:7076/api/datathemes");
                       var newPost = new ThemePost()
                       {
                           dataThemeName = theme.dataThemeName
                       };
                       var newPostJson = JsonConvert.SerializeObject(newPost);
                       var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                       var result = client.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                   }*/

                /*using (var client = new HttpClient())
                {
                    var endpoint = new Uri("https://localhost:7076/api/dataowners");
                    var newPost = new OwnerPost()
                    {
                        dataOwnerName = owner.dataOwnerName
                    };
                    var newPostJson = JsonConvert.SerializeObject(newPost);
                    var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                }*/

                /* using (var client = new HttpClient())
                 {
                     var endpoint = new Uri("https://localhost:7076/api/dataformats");
                     var newPost = new FormatPost()
                     {
                         dataFormatName = DataFormat.dataFormatName
                     };
                     var newPostJson = JsonConvert.SerializeObject(newPost);
                     var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                     var result = client.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                 }*/



                // Post request to datalanguages
                /*  using (var client = new HttpClient())
                  {
                      var endpoint = new Uri("https://localhost:7076/api/datalanguages");
                      var newPost = new LanguagePost()
                      {
                          // idDataLanguage = new DataOwnerAccess().GetLanguageById(DataLanguage),
                          dataLanguageName = DataLanguage.dataLanguageName
                      };
                      var newPostJson = JsonConvert.SerializeObject(newPost);
                      var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                      var result = client.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                  }*/

                // Post request to openDatum
                /* using (var client = new HttpClient())
                 {
                     var endpoint = new Uri("https://localhost:7076/api/opendatum");

                     var newOpen = new OpenPost()
                     {
                        // idData = 13,
                         dataUrl = OpenData.data_url,
                         dataOpenLicense = 1,
                         dataOwnerId = id_owner,
                         dataThemeId = id_theme,
                         updateFrequencyId = id_frequency,

                         dataOwner = new data_owner()
                         {
                             idDataOwner = id_owner,
                             dataOwnerName = owner.dataOwnerName,
                         },
                         dataTheme = new data_theme()
                         {
                             idDataTheme = id_theme,
                             dataThemeName = theme.dataThemeName,
                         },
                         updateFrequency = new update_frequency()
                         {
                             idUpdateFrequency = id_frequency,
                             updateFrequencyName = frequency.updateFrequencyName,
                         }
                     };
                     var newPostJson = JsonConvert.SerializeObject(newOpen);
                     var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                     System.Diagnostics.Debug.WriteLine(newPostJson.ToString());
                     var result = client.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                 }*/

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
        
            Uri uri = new Uri("https://skatteverket.entryscape.net/store/9/resource/667");
            Process.Start(uri.ToString());
            System.Net.WebClient client = new System.Net.WebClient();
            byte[] buffer = client.DownloadData(uri);
            string path = @"C:\Users\lazki\Downloads\myDownloads\data.csv";
            Stream stream = new FileStream(path, FileMode.Create);
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(buffer);
            stream.Close();
            Console.WriteLine("completed");
            try
            {
                DataOwnerAccess dataOwnerAccess = new DataOwnerAccess();
                var DataFormat = new dataFormat
                {
                    dataFormatName = "CSV File"
                };

                var DataUsage = new dataUsage
                {
                    date_of_usage = DateTime.Now,
                    is_downloaded = true
                };

                var OpenData = new openData
                {
                    //data_url = HttpContext.Current.Request.Url.AbsoluteUri
                    data_url = uri.ToString(),
                };

                var DataLanguage = new dataLanguage
                {
                    dataLanguageName = "Swedish"
                };

                var frequency = new update_frequency
                {
                    updateFrequencyName = "Yearly"
                };

                var theme = new data_theme
                {
                    dataThemeName = "Theme3"
                };

                var owner = new data_owner
                {
                    dataOwnerName = "Miun"
                };

                dataOwnerAccess.InsertRecordDataFormat(DataFormat);
                dataOwnerAccess.InsertRecordOpenData(OpenData);
                dataOwnerAccess.InsertRecordLanguage(DataLanguage);

                int id_format = new DataOwnerAccess().GetFormatById(DataFormat);
                int id_open = new DataOwnerAccess().GetOpenById(OpenData);
                int id_language = new DataOwnerAccess().GetLanguageById(DataLanguage);

                int id_usage = new DataOwnerAccess().GetUsageById(DataUsage);
                int id_owner = new DataOwnerAccess().GetOwnerId(owner);
                int id_theme = new DataOwnerAccess().GetThemeId(theme);
                int id_frequency = new DataOwnerAccess().GetFrequencyId(frequency);


                dataOwnerAccess.InsertRecordDataUsage(DataUsage, id_format, id_open,id_language);

                // Post request to DataUsage
               using (var client1 = new HttpClient())
                {
                    var endpoint = new Uri("https://localhost:7076/api/datausages");
                    var newPost = new UsagePost()
                    {
                        //idDataUsage = 42,
                        openDataId = id_open,
                        dateOfUsage = DataUsage.date_of_usage,
                        dataFormatId = 1,
                        languageId = 1,
                        isDownloaded = 1,
                        dataFormat = new dataFormat()
                        {
                            idDataFormat = 1,
                            dataFormatName = DataFormat.dataFormatName,
                        },
                        language = new dataLanguage()
                        {
                            idDataLanguage = 1,
                            dataLanguageName = DataLanguage.dataLanguageName
                        },
                        openData = new OpenPost()
                        {
                            idData = 2,
                            dataUrl = OpenData.data_url,
                            dataOpenLicense = 1,
                            dataOwnerId = id_owner,
                            updateFrequencyId = id_frequency,
                            dataThemeId = id_theme,

                            dataOwner = new data_owner()
                            {
                                idDataOwner = id_owner,
                                dataOwnerName = owner.dataOwnerName,
                            },
                            dataTheme = new data_theme()
                            {
                                idDataTheme = id_theme,
                                dataThemeName = theme.dataThemeName,
                            },
                            updateFrequency = new update_frequency()
                            {
                                idUpdateFrequency = id_frequency,
                                updateFrequencyName = frequency.updateFrequencyName,
                            }
                        }
                    };
                    var newPostJson = JsonConvert.SerializeObject(newPost);
                    var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    System.Diagnostics.Debug.WriteLine(newPostJson.ToString());
                    var result = client1.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                }


                //Post request to updateFrequency
                /*  using (var client1 = new HttpClient())
                  {
                      var endpoint = new Uri("https://localhost:7076/api/updatefrequencies");
                      var newPost = new FrequencyPost()
                      {
                          updateFrequencyName = frequency.updateFrequencyName
                      };
                      var newPostJson = JsonConvert.SerializeObject(newPost);
                      var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                      var result = client1.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                  }*/

                //Post request to Theme
                /* using (var client1 = new HttpClient())
                 {
                     var endpoint = new Uri("https://localhost:7076/api/datathemes");
                     var newPost = new ThemePost()
                     {
                         dataThemeName = theme.dataThemeName
                     };
                     var newPostJson = JsonConvert.SerializeObject(newPost);
                     var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                     var result = client1.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                 }*/

                /* using (var client1 = new HttpClient())
                 {
                     var endpoint = new Uri("https://localhost:7076/api/dataowners");
                     var newPost = new OwnerPost()
                     {
                         dataOwnerName = owner.dataOwnerName
                     };
                     var newPostJson = JsonConvert.SerializeObject(newPost);
                     var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                     var result = client1.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                 }*/

                /*using (var client1 = new HttpClient())
                {
                    var endpoint = new Uri("https://localhost:7076/api/dataformats");
                    var newPost = new FormatPost()
                    {
                        dataFormatName = DataFormat.dataFormatName
                    };
                    var newPostJson = JsonConvert.SerializeObject(newPost);
                    var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                    var result = client1.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                }*/


                // Post request to datalanguages
                /* using (var client1 = new HttpClient())
                 {
                     var endpoint = new Uri("https://localhost:7076/api/datalanguages");
                     var newPost = new LanguagePost()
                     {
                         // idDataLanguage = new DataOwnerAccess().GetLanguageById(DataLanguage),
                         dataLanguageName = DataLanguage.dataLanguageName
                     };
                     var newPostJson = JsonConvert.SerializeObject(newPost);
                     var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                     var result = client1.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                 }*/

                // Post request to openDatum
                /* using (var client1 = new HttpClient())
                 {
                     var endpoint = new Uri("https://localhost:7076/api/opendatum");

                     var newOpen = new OpenPost()
                     {
                         //idData = 18,
                         dataUrl = OpenData.data_url,
                         dataOpenLicense = 1,
                         dataOwnerId = id_owner,
                         dataThemeId = id_theme,
                         updateFrequencyId = id_frequency,

                         dataOwner = new data_owner()
                         {
                             idDataOwner = id_owner,
                             dataOwnerName = owner.dataOwnerName,
                         },
                         dataTheme = new data_theme()
                         {
                             idDataTheme = id_theme,
                             dataThemeName = theme.dataThemeName,
                         },
                         updateFrequency = new update_frequency()
                         {
                             idUpdateFrequency = id_frequency,
                             updateFrequencyName = frequency.updateFrequencyName,
                         }
                     };
                     var newPostJson = JsonConvert.SerializeObject(newOpen);
                     var payLoad = new StringContent(newPostJson, Encoding.UTF8, "application/json");
                     System.Diagnostics.Debug.WriteLine(newPostJson.ToString());
                     var result = client1.PostAsync(endpoint, payLoad).Result.Content.ReadAsStringAsync().Result;
                 }*/
            }
            catch (Exception ex)
            {
                throw;
            }          

        }

      
    }
}
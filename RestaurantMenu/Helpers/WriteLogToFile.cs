using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace RestaurantMenu.Helpers
{
    public class WriteLogToFile
    {

        public static void Write(Exception objException)
        {
            try
            {
                string strLogMessage = string.Empty;
                string strLogFile = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "Error_Logs.txt"; //System.Configuration.ConfigurationManager.AppSettings["logFilePath"].ToString();
                StreamWriter swLog;

                strLogMessage = string.Format("UTC : {0}" + Environment.NewLine + "Source : {1}" + Environment.NewLine + "Method : {2}" + Environment.NewLine + Environment.NewLine + "Error : {3}" + Environment.NewLine + "Stack Trace : {4}" + Environment.NewLine + "-------",
                    DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), objException.TargetSite.Name, objException.Source.Trim(), objException.Message.Trim(), objException.StackTrace.Trim());

                if (!File.Exists(strLogFile))
                {
                    swLog = new StreamWriter(strLogFile);
                }
                else
                {
                    swLog = File.AppendText(strLogFile);
                }

                swLog.WriteLine(strLogMessage);
                swLog.WriteLine();
                swLog.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(objException);
                Write(objException.Message);
                //Write(ex.Message);
            }
            finally
            {

            }
        }

        public static void Write(string logMessage)
        {
            try
            {
                string strLogMessage = string.Empty;
                string strLogFile = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "Error_Logs.txt"; //System.Configuration.ConfigurationManager.AppSettings["logFilePath"].ToString();
                StreamWriter swLog;

                strLogMessage = string.Format("UTC:{0}. Error:{1}" + Environment.NewLine + "-------", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), logMessage);

                if (!File.Exists(strLogFile))
                {
                    swLog = new StreamWriter(strLogFile);
                }
                else
                {
                    swLog = File.AppendText(strLogFile);
                }

                swLog.WriteLine(strLogMessage);
                swLog.WriteLine();

                swLog.Close();
            }
            catch
            {
                UltimateErrorLog(logMessage);
            }
        }
        public static void UltimateErrorLog(string logMessage)
        {
            try
            {
                string strLogMessage = string.Empty;
                string strLogFile= Path.Combine(HostingEnvironment.ApplicationPhysicalPath, @"Error_Logs.txt");
                StreamWriter swLog;

                strLogMessage = string.Format("UTC:{0}. Error:{1}" + Environment.NewLine + "-------", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), logMessage);

                if (!File.Exists(strLogFile))
                {
                    swLog = new StreamWriter(strLogFile);
                }
                else
                {
                    swLog = File.AppendText(strLogFile);
                }

                swLog.WriteLine(strLogMessage);
                swLog.WriteLine();

                swLog.Close();
            }
            catch
            {
                Console.WriteLine(logMessage);
            }
        }
    }
}
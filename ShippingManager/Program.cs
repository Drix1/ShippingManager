using ShippingManager.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShippingManager
{
    /// <summary>
    /// Avinash:
    /// Auto Version Updater That Automaticly updates the version From Http Server
    /// for shipping Manager applition Only.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Please wait.");
            //Application New Version Check from Text File
            String AppversionNo = CheckVersionNumber.ReadString("VRN").ToString();

            //Database New Version Check
            String latestVersion = getNewVersion.get();

            Console.WriteLine(" Checking Updates for version : " + latestVersion);

            string dirpath = Environment.CurrentDirectory + "\\ShippingApp";

            String Url = "http://192.168.1.16/FTPShippingManager/";
            String CopyTOLocation = Environment.CurrentDirectory + "\\ShippingApp\\";

            GetallFilesFromHttp.Url = Url;

            List<String> Files = new List<string>();
            Files = GetallFilesFromHttp.ListDiractory();

            if (AppversionNo != latestVersion)
            {
                //Get All Files from the location
                Directory.Delete(dirpath, true);
                Directory.CreateDirectory(dirpath);
                try
                {
                    Directory.CreateDirectory(dirpath + "\\Images");
                    Directory.CreateDirectory(dirpath + "\\Images\\MEDIA");
                    Directory.CreateDirectory(dirpath + "\\Language");
                    File.Delete(dirpath + "\\LocalSetting.txt");
                }
                catch (Exception)
                { }

                foreach (String item in Files)
                {
                    downloadFromFTP(item, Url, CopyTOLocation);
                }

                ///language Files..
                String LngUrl = "http://192.168.1.16/FTPShippingManager/Language/";
                String LngCopyToLocation = CopyTOLocation + "\\Language\\";
                GetallFilesFromHttp.Url = LngUrl;
                List<String> LngFiles = new List<string>();
                LngFiles = GetallFilesFromHttp.ListDiractory();
                foreach (String item in LngFiles)
                {
                    downloadFromFTP(item, LngUrl, LngCopyToLocation);
                }

                //Images Dir URL;
                String ImageUrl = "http://192.168.1.16/FTPShippingManager/Images/MEDIA/";
                String ImageCopyToLocation = CopyTOLocation + "\\Images\\MEDIA\\";
                GetallFilesFromHttp.Url = ImageUrl;
                List<String> ImgFiles = new List<string>();
                ImgFiles = GetallFilesFromHttp.ListDiractory();
                foreach (String item in ImgFiles)
                {
                    downloadFromFTP(item, ImageUrl, ImageCopyToLocation);
                }

                //Rename files ..
                File.Move(dirpath + "\\Language\\Chinese.sys.txt", dirpath + "\\Language\\Chinese.sys");
                File.Move(dirpath + "\\Language\\English_US.sys.txt", dirpath + "\\Language\\English_US.sys");
                File.Move(dirpath + "\\Language\\Russian.sys.txt", dirpath + "\\Language\\Russian.sys");
                File.Move(dirpath + "\\PackingNet.exe.config.txt", dirpath + "\\PackingNet.exe.config");
                File.Move(dirpath + "\\PackingClassLibrary.pdb.txt", dirpath + "\\PackingClassLibrary.pdb");
                File.Move(dirpath + "\\PackingNet.pdb.txt", dirpath + "\\PackingNet.pdb");
                File.Move(dirpath + "\\System.Windows.Controls.DataVisualization.Toolkit.pdb.txt", dirpath + "\\System.Windows.Controls.DataVisualization.Toolkit.pdb");
                File.Move(dirpath + "\\WPFToolkit.pdb.txt", dirpath + "\\WPFToolkit.pdb");
                File.Move(dirpath + "\\PackingNet.vshost.exe.config.txt", dirpath + "\\PackingNet.vshost.exe.config");
                File.Move(dirpath + "\\PackingNet.vshost.exe.manifest.txt", dirpath + "\\PackingNet.vshost.exe.manifest");

            }
            System.Diagnostics.Process.Start(dirpath + "\\PackingNet.exe");
        }

        /// <summary>
        /// Downloads Files From HTTP server.
        /// and Move that files from current diracotry To another Location.
        /// </summary>
        /// <param name="FileName">
        /// String File Name to Downlad
        /// </param>
        /// <param name="Url">
        /// string Url To download files
        /// </param>
        /// <param name="CopyToLocation">
        /// Copy that files to which Filder. (Move Files From Current Folder to another.)
        /// </param>
        private static void downloadFromFTP(String FileName, String Url, String CopyToLocation)
        {
            try
            {
                string remoteUri = Url;
                string fileName = FileName, myStringWebResource = null;
                // Create a new WebClient instance.
                WebClient myWebClient = new WebClient();

                // Concatenate the domain with the Web resource filename.
                myStringWebResource = remoteUri + fileName;

                // Download the Web resource and save it into the current filesystem folder.
                myWebClient.DownloadFile(myStringWebResource, fileName);
                try
                {
                    File.Move(Environment.CurrentDirectory + "\\" + fileName, CopyToLocation + fileName);
                    Console.WriteLine(DateTime.Now.ToString("hh:mm:ss tt ") + " Downloading File : " + fileName);

                }
                catch (Exception)
                { }
            }
            catch (Exception)
            { }

        }

        /// <summary>
        /// Copy new Diractory to new Location
        /// </summary>
        /// <param name="sourceDirName">
        /// String Diractory to move
        /// </param>
        /// <param name="destDirName">
        /// String Dir Path to copy 
        /// </param>
        /// <param name="copySubDirs">
        /// If any Sub Dirctory
        /// </param>
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            try
            {
                // Get the subdirectories for the specified directory.
                DirectoryInfo dir = new DirectoryInfo(sourceDirName);
                DirectoryInfo[] dirs = dir.GetDirectories();

                if (!dir.Exists)
                {
                    throw new DirectoryNotFoundException(
                        "Source directory does not exist or could not be found: "
                        + sourceDirName);
                }

                // If the destination directory doesn't exist, create it. 
                if (!Directory.Exists(destDirName))
                {
                    Directory.CreateDirectory(destDirName);
                }

                // Get the files in the directory and copy them to the new location.
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string temppath = Path.Combine(destDirName, file.Name);
                    file.CopyTo(temppath, false);
                }

                // If copying subdirectories, copy them and their contents to new location. 
                if (copySubDirs)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        string temppath = Path.Combine(destDirName, subdir.Name);
                        DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                    }
                }
            }
            catch (Exception)
            { }
        }
    }
}

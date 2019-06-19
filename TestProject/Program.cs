using System;
using System.IO;
using System.IO.Compression;

namespace TestProject
{
    public static class ZipArchiveExtensionMethods
    {
        public static void B (this ZipArchiveEntry zipArchiveEntry)
        {
            //Some code here
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Path to zip
            string zipPath = @"..\..\testZip.zip";

            //Path to temporary folder
            string folderPath = @"..\..\temporary";

            //Create test zip archive
            ZipFile.CreateFromDirectory(@"..\..\zip", zipPath);

            //Create temporary folder
            System.IO.Directory.CreateDirectory(folderPath);

            //using code block for ensuring disposing of opened streams
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                    {
                        //Get destination path
                        string destinationPath = Path.GetFullPath(Path.Combine(folderPath, entry.FullName));
                        
                        //Extract csv files to destination path
                        entry.ExtractToFile(destinationPath);

                        //Calling method B for every csv file in zip
                        entry.B();
                    }
                }
            }
            //Delete temporary folder and all files inside it
            System.IO.Directory.Delete(folderPath, true);

        }
    }
}

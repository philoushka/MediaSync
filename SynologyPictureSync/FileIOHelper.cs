﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MediaSync.Extensions;

namespace MediaSync
{
    public class FileIOHelper
    {

        public static bool FileSizesAreSame(string file1, string file2)
        {
            try
            {
                FileInfo f1 = new FileInfo(file1);
                FileInfo f2 = new FileInfo(file2);

                return f1.Length.Equals(f2.Length);
            }
            catch (Exception) { }
            return false;
        }

        public static void CreateDirectoryForFile(string filePath)
        {
            Directory.CreateDirectory(new FileInfo(filePath).DirectoryName);
        }

        public static IEnumerable<FileInfo> GetAllFilesWithExtensions(string directory, IEnumerable<string> fileExtensions)
        {
            DirectoryInfo sourceDir = new DirectoryInfo(directory);
            return fileExtensions
                    .SelectMany(fileExt => sourceDir.GetFiles("*." + fileExt, SearchOption.AllDirectories))
                    .Distinct()
                    .ToList();
        }

        /// <summary>
        /// Writes your string to the log file in the app directory on disk. Prefixes with current datetime.
        /// </summary>
        /// <param name="msg"></param>
        public static void WriteToErrLog(string msg)
        {
            msg = "{0}\t{1}".FormatWith(DateTime.Now.ToString("yyyy-MMM-dd HH:mm") ,msg); 
            using (var file = File.AppendText(ErrLogFile))
            {
                file.Write(msg);
            }
        }


        public static string ErrLogFile { get { return Path.Combine(AppDir, "ErrorLog.txt"); } }
        public static string AppDir { get { return Path.Combine(Machine.AppDataDirectory, Assembly.GetEntryAssembly().GetName().Name); } }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderBrowser.Models
{
    /// <summary>
    /// Statistic information of the folder and subfolders
    /// </summary>
    public class dirInfo
    {
        public const int mByte = 1024 * 1024;
        public const int const10mb = mByte * 10;
        public const int const50mb = mByte * 50;
        public const int const100mb = mByte * 100;

        public string Directory { get; set; }
        public long QuantitySize10 { get; set; }
        public long QuantitySize50 { get; set; }
        public long QuantitySize100 { get; set; }

        /// <summary>
        /// Counting statistic for directory and subdirectories
        /// </summary>
        /// <param name="startFolder"></param>
        /// <returns>dirCalcInfo</returns>
        public static dirInfo DirStatistic(string startFolder)
        {
            var resultCalc = new dirInfo();

            IEnumerable<string> fileList = System.IO.Directory.GetFiles(startFolder, "*.*", SearchOption.TopDirectoryOnly);
            var fileQuery = from file in fileList
                            select GetFileLength(file);
            foreach (var sizeFile in fileQuery)
            {
                if (sizeFile <= const10mb)
                {
                    resultCalc.QuantitySize10++;
                }
                else
                {
                    if (sizeFile <= const50mb)
                    {
                        resultCalc.QuantitySize50++;
                    }
                    else
                    {
                        if (sizeFile > const100mb) resultCalc.QuantitySize100++;
                    }
                }
            }

            IEnumerable<string> directoryList = System.IO.Directory.GetDirectories(startFolder, "*.*", SearchOption.TopDirectoryOnly);
            foreach (var nextNode in directoryList)
            {
                var nextStat = new dirInfo();
                bool success = true;

                try
                {
                    nextStat = DirStatistic(nextNode);
                }
                catch (Exception)
                {
                    success = false;
                }

                if (success)
                {
                    resultCalc.QuantitySize10 += nextStat.QuantitySize10;
                    resultCalc.QuantitySize50 += nextStat.QuantitySize50;
                    resultCalc.QuantitySize100 += nextStat.QuantitySize100;
                }
            }

            resultCalc.Directory = startFolder;
            return resultCalc;
        }

        /// <summary>
        /// Get lengt of the file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static long GetFileLength(string filename)
        {
            long retval;
            try
            {
                System.IO.FileInfo fi = new FileInfo(filename);
                retval = fi.Length;
            }
            catch (System.IO.FileNotFoundException)
            {
                retval = 0;
            }
            return retval;
        }
    }
}
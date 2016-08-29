using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderBrowser.Models
{
    /// <summary>
    /// Model of the current folders structure
    /// </summary>
    public class dirStructure
    {
        public string CurrentPath { get; set; }
        public List<string> Folders { get; set; }
        public List<string> Files { get; set; }
        public long QuantitySize10 { get; set; }
        public long QuantitySize50 { get; set; }
        public long QuantitySize100 { get; set; }

        public static dirStructure GetDir(string currentFolder)
        {
            if (!Directory.Exists(currentFolder))
            {
                return null;
            }

            dirStructure currentDirStruct = new dirStructure();
            currentDirStruct.Folders = new List<string>();
            currentDirStruct.Files = new List<string>();

            DirectoryInfo dirinfoFolder = new DirectoryInfo(currentFolder);

            try
            {
                currentDirStruct.CurrentPath = currentFolder;

                var Directories = dirinfoFolder.GetDirectories("*.*", SearchOption.TopDirectoryOnly);

                foreach (var dir in Directories)
                {
                    currentDirStruct.Folders.Add(dir.Name);
                }
            }
            catch (Exception)
            {
                return null;
            }
            var DirFiles = dirinfoFolder.GetFiles("*.*", SearchOption.TopDirectoryOnly);
            foreach (var file in DirFiles)
            {
                currentDirStruct.Files.Add(file.Name);
            }

            return currentDirStruct;
        }

        public static dirStructure GetRoot()
        {
            dirStructure currentDirStruct = new dirStructure();
            currentDirStruct.Folders = new List<string>();
            currentDirStruct.Files = new List<string>();

            currentDirStruct.CurrentPath = "";
            var currentDrives = DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Fixed && d.IsReady);
            foreach (var drive in currentDrives)
            {
                currentDirStruct.Folders.Add(drive.Name);
            }
            return currentDirStruct;
        }
    }
}
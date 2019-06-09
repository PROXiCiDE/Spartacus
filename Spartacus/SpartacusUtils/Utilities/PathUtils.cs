using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpartacusUtils.Utilities
{
    public static class PathUtils
    {
        /// <summary>
        /// Removes the leading dot in the file extension
        /// </summary>
        /// <param name="filename">file name</param>
        /// <returns>string of filename without a leading dot</returns>
        public static string GetExtensionWithoutDot(string filename)
        {
            return Path.GetExtension(filename)?.Replace(".", "");
        }

        /// <summary>
        /// Cleanse a directory fullPath
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string CleanPath(string path)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Split Root and Pathing information
        /// </summary>
        /// <param name="fullPath"></param>
        /// <param name="rootPath"></param>
        /// <param name="pathName"></param>
        /// <returns></returns>
        public static bool MakePathInformation(string fullPath, out string rootPath, out string pathName)
        {
            rootPath = null;
            pathName = null;

            if (fullPath == null) return false;

            var dropPath = DropPathRoot(fullPath);
            if (dropPath != null)
            {
                var dirSplit = dropPath.Split(new []{ Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);

                if (dirSplit.Length > 1)
                {
                    rootPath = dirSplit[0];
                    if (rootPath != null && !rootPath.EndsWith(@"\"))
                        rootPath = $@"{rootPath}\";

                    pathName = string.Join($"{Path.DirectorySeparatorChar}", dirSplit.Skip(1));
                }
                else
                    pathName = dirSplit[0];
            }

            return true;
        }

        /// <summary>
        /// Remove Drive / UNC from path
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static string DropPathRoot(string fullPath)
        {
            var result = fullPath;

            if (string.IsNullOrEmpty(fullPath)) return result;

            if (fullPath[0] == '\\' || fullPath[0] == '/')
            {
                // UNC name ?
                if (fullPath.Length > 1 && (fullPath[1] == '\\' || fullPath[1] == '/'))
                {
                    var i = 2;
                    var elements = 2;

                    // Scan for two separate elements \\Server01\user\docs\Letter.txt
                    while (i <= fullPath.Length &&
                           (fullPath[i] != '\\' && fullPath[i] != '/' || --elements > 0))
                        i++;
                    i++;
                    result = i < fullPath.Length ? fullPath.Substring(i) : "";
                }
            }
            else if (fullPath.Length > 1 && fullPath[1] == ':')
            {
                var dropCount = 2;
                if (fullPath.Length > 2 && (fullPath[2] == '\\' || fullPath[2] == '/'))
                    dropCount = 3;
                result = result.Remove(0, dropCount);
            }

            return result;
        }

        /// <summary>
        /// Iterate over a List of file extensions to see if they match
        /// </summary>
        /// <param name="fileExtension"></param>
        /// <param name="extensionList"></param>
        /// <returns></returns>
        public static bool ContainsExtension(string fileExtension, List<string> extensionList)
        {
            if (fileExtension.StartsWith("."))
                fileExtension = fileExtension.Replace(".", "");
            return extensionList.Contains(fileExtension.ToLower());
        }

        /// <summary>
        /// Validates the Game Path looking for specific directory / file patterns
        /// </summary>
        /// <param name="gamePath"></param>
        /// <returns></returns>
        public static bool IsValidGamePath(string gamePath)
        {
            var directories = new List<string>()
            {
                "ai",
                "art",
                "data",
                "fonts",
                "rm",
                "scenario",
                "sound",
            };

            var count = 0;
            directories.ForEach(dir =>
            {
                var path = Path.Combine(gamePath, dir);
                var files = Directory.GetFiles(path, "*.bar", SearchOption.TopDirectoryOnly);
                if (files.Any()) count++;
            });

            return count == directories.Count;
        }
    }
}
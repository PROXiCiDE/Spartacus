using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ProjectCeleste.GameFiles.XMLParser;
using SpartacusUtils.Bar;
using SpartacusUtils.Helpers;
using SpartacusUtils.Utilities;

namespace TestUnit
{
    internal class BarTest
    {
        private readonly string _gamePath = @"G:\MS Age Of Empires Online";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestLocalDir()
        {
            var barfile = new BarFileSystem(Path.Combine(_gamePath, @"data\data.bar"));
            var entries = barfile.ArchiveEntries;
            Debug.WriteLine(
                $"Root: {barfile.Root}, FileName: {barfile.FileName}, Number Files: {barfile.NumberOfFiles} == {entries.Count}");
            entries.Skip(500).Take(10).ForEach(x =>
            {
                Debug.WriteLine($"\"{barfile.Root.Replace("\\", "\\\\")}{x.FileName}\",");
            });
        }

        [Test]
        public void TestReadEntry()
        {
            var barfile = new BarFileSystem(Path.Combine(_gamePath, @"data\data.bar"));
            var entry = barfile.FindEntries(@"ai\C01\Thracian\*.character").First();
            var character = barfile.ReadEntry<AiCharacterXml>(entry);
            Debug.WriteLine(character.CivId);
        }


        [Test]
        public void PathTest()
        {
            var pathList = new List<string>
            {
                "C01_Bandit_T4_L36.",
                "C01_Bandit_T4_L36.character",
                "\\\\machine\\server\\ai\\C01\\Thracian\\Data\\C01_Bandit_T4_L38.character",
                "ai\\C01\\Thracian\\Data\\C01_Bandit_T4_L38.character",
                "Data\\C01_Bandit_T4_L40.character",
                "Data\\C01_Clearchus_T3_L28.character",
                "Data\\C01_Clearchus_T3_L32.character",
                "Data\\C01_Clearchus_T3_L34.character",
                "Data\\C01_Clearchus_T3_L36.character",
                "Data\\C01_Clearchus_T4_L38.character",
                "Data\\C01_Clearchus_T4_L40.character",
                "Data\\C01_CyprusRamp_T3_L28.character"
            };

            pathList.Take(6).ForEach(dir =>
            {
                var dir2 = Path.Combine(@"c:\ai\C01\Thracian", dir);
                PathUtils.MakePathInformation(dir, out var dirPath, out var filePath);
                Debug.WriteLine($"RootPath: {dirPath},\tFileName: {filePath}");
            });
        }

        [Test]
        public void ParseDirectoryExists()
        {
            Debug.WriteLine(PathUtils.IsValidGamePath(@"G:\MS Age Of Empires Online"));
        }


        [Test]
        public void TestReadTechTreeXNonLocal()
        {
            try
            {
                var barfile = new BarFileSystem(Path.Combine(_gamePath, @"data\data.bar"));
                var entry = barfile.FindEntries(@"data\techtreex.xml").First();
                var techTreeXml = barfile.ReadEntry<TechTreeXml>(entry);
                Debug.WriteLine(techTreeXml.TechArray.First()?.Name);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [Test]
        public void FindFileExtensionsUsed()
        {
            var extsUsed = new List<string>();
            foreach (var bar in Directory.GetFiles(_gamePath, "*.bar", SearchOption.AllDirectories))
            {
                var barFile = new BarFileSystem(bar);
                barFile.FindEntries("*").Any(x =>
                {
                    extsUsed.Add(Path.GetExtension(x.FileName));
                    return true;
                });
            }

            foreach (var ext in extsUsed.Distinct()) Debug.WriteLine(ext);
        }

        //Was used to see which files were XML, for ReadEntry<T> function
        //to create a collection of extensions used
        [Test]
        public void DetermineXMLFiles()
        {
            var path = @"G:\Age of Empires Online\data";
            var extList = new List<string>();

            var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            Parallel.ForEach(files, file =>
            {
                var ext = Path.GetExtension(file);
                var fileInfo = new FileInfo(file);
                if (!extList.Contains(ext))
                    if (FileUtils.IsXmlFile(file))
                        extList.Add(ext);
            });

            foreach (var ext in extList.Distinct()) Debug.WriteLine(ext);
        }
    }
}
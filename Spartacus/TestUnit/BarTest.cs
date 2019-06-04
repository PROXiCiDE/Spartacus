using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using NUnit.Framework;
using ProjectCeleste.GameFiles.XMLParser;
using SpartacusUtils.Bar;
using SpartacusUtils.Helpers;
using SpartacusUtils.Utilities;
using SpartacusUtils.Xml.Helpers;
using SpartacusUtils.Xml.Language;

namespace TestUnit
{
    class BarTest
    {
        private string _gamePath = @"G:\MS Age Of Empires Online";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestReadEntry()
        {
            var barfile = new BarFileReader(Path.Combine(_gamePath, @"data\data.bar"));
            var entry = barfile.FindEntries(@"ai\C01\Thracian\*.character").First();
            var character = barfile.ReadEntry<AiCharacterXml>(entry);
            Debug.WriteLine(character.CivId);
        }

        [Test]
        public void FindFileExtensionsUsed()
        {
            var extsUsed = new List<string>();
            foreach (var bar in Directory.GetFiles(_gamePath, "*.bar", SearchOption.AllDirectories))
            {
                var barFile = new BarFileReader(bar);
                barFile.FindEntries("*").Any(x =>
                {
                    extsUsed.Add(Path.GetExtension(x.FileName));
                    return true;
                });
            }

            foreach (var ext in extsUsed.Distinct())
            {
                Debug.WriteLine(ext);
            }
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

            foreach (var ext in extList.Distinct())
            {
                Debug.WriteLine(ext);
            }
        }
    }
}

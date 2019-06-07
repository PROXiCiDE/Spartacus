using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using ProjectCeleste.GameFiles.XMLParser;
using SpartacusUtils.Bar;

namespace SpartacusUtils.Xml.Language
{
    public static class LanguagesReader
    {
        public static StringTableXml FromBarEntry(this BarFileSystem barFileReader, string langFile)
        {
            var entry = barFileReader.GetEntry($"{langFile}.xmb");
            if (entry != null)
            {
                var languageXml = barFileReader.ReadEntry<StringTableXml>(entry);
                if (languageXml == null) return null;

                var filename = Path.GetFileNameWithoutExtension(langFile)?.ToLower();
                if (string.IsNullOrEmpty(filename))
                    return languageXml;

                languageXml.Id = filename.Replace("fr-fr-", "")
                    .Replace("de-de-", "").Replace("es-es-", "").Replace("it-it-", "").Replace("zh-cht-", "");

                if (filename.Contains("fr-fr"))
                    languageXml.Language.Values.First().Name = "French";
                else if (filename.Contains("de-de"))
                    languageXml.Language.Values.First().Name = "German";
                else if (filename.Contains("es-es"))
                    languageXml.Language.Values.First().Name = "Spanish";
                else if (filename.Contains("it-it"))
                    languageXml.Language.Values.First().Name = "Italian";
                else if (filename.Contains("zh-cht"))
                    languageXml.Language.Values.First().Name = "Chinese";
                else
                    languageXml.Language.Values.First().Name = "English";

                languageXml.LanguageArrayDoNotUse =
                    JsonConvert.DeserializeObject<LanguageXml[]>(
                        JsonConvert.SerializeObject(languageXml.Language.Values.ToArray()));

                return languageXml;
            }

            return null;
        }

        public static LanguagesXml FromBarFile(BarFileSystem barFileReader,
            out List<LanguageReaderError> languageReaderErrors, bool isDebug = false)
        {
            var listFile = new List<string>
            {
                "econstrings.xml",
                "equipmentstrings.xml",
                "queststringtable.xml",
                "stringtablex.xml"
            };

            languageReaderErrors = new List<LanguageReaderError>();

            var prefixLang = new List<string> {"de-DE", "fr-FR", "es-es", "it-it", "zh-cht"};
            var languages = new LanguagesXml();
            foreach (var langFile in listFile)
            {
                var newClass = barFileReader.FromBarEntry(langFile);
                if (newClass == null) continue;

                foreach (var newLang in newClass.Language.Values)
                    if (!languages.ContainsKey(newClass.Id))
                        languages.Add(newClass);
                    else
                        foreach (var languageString in newLang.LanguageString.Values.ToArray())
                            languages[newClass.Id].Language[newLang.Name]
                                .LanguageString
                                .Add(languageString.LocId, languageString);
            }

            if (isDebug)
                return languages;

            foreach (var prefix in prefixLang)
            foreach (var langFile in listFile)
            {
                var fileName = $"{prefix}-{langFile}";
                var newClass = barFileReader.FromBarEntry(fileName);
                if (newClass == null) continue;

                var languageXml = new LanguageXml();
                var lng = languages[newClass.Id].Language["English"];
                languageXml.Name = newClass.Language.Values.First().Name;
                foreach (var newLang in lng.LanguageString.Values.ToArray())
                {
                    var languageString = newClass.Language[languageXml.Name].LanguageString;
                    if (languageString.ContainsKey(newLang.LocId))
                        languageXml.LanguageString.Add(newLang.LocId, languageString[newLang.LocId]);
                    else
                        languageReaderErrors.Add(
                            new LanguageReaderError(
                                fileName, newLang.LocId.ToString(),
                                new KeyNotFoundException(newLang.LocId.ToString()).Message
                            )
                        );
                }

                if (!languages.ContainsKey(newClass.Id))
                    languages[newClass.Id].Language.Add(languageXml.Name, languageXml);
            }

            return languages;
        }
    }
}
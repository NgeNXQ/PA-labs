using System;
using System.Collections.Generic;
using System.IO;

namespace DataTable
{
    public static class FileWork
    {
        public static string GetValue(string path, int reference)
        {
            using (StreamReader sr = new StreamReader(File.OpenRead(path)))
            {
                sr.BaseStream.Position = GetPositionInFile(reference);
                return sr.ReadLine().Split(':')[2].Trim();
            }
        }

        public static void ChangeValue(string path, int key, int reference, string newValue)
        {
            using (StreamWriter sw = new StreamWriter(File.OpenWrite(path)))
            {
                sw.BaseStream.Position = GetPositionInFile(reference);
                MainRecord newRecord = new MainRecord(0, key, newValue);
                sw.Write(newRecord.ToString());
            }
        }

        public static void MarkRecordAsRemoved(string path, int reference)
        {
            string line;
            long positionInFile = GetPositionInFile(reference);

            using (StreamReader sr = new StreamReader(File.OpenRead(path)))
            {
                sr.BaseStream.Position = positionInFile;
                line = sr.ReadLine();
            }

            using (StreamWriter sw = new StreamWriter(File.OpenWrite(path)))
            {
                sw.BaseStream.Position = positionInFile;
                sw.Write("1" + line.Substring(1));
            }
        }

        private static long GetPositionInFile(int reference) => MainRecord.LENGTH * reference;

        public static void AppendToFile(string path, string line)
        {
            using (StreamWriter sw = File.AppendText(path))
                sw.WriteLine(line);
        }

        public static void WriteRecords(string path, List<IndexRecord> records)
        {
            File.WriteAllText(path, String.Empty);

            using (StreamWriter sw = new StreamWriter(File.OpenWrite(path)))
            {
                foreach (IndexRecord record in records)
                    sw.WriteLine(record);
            }
        }

        public static int GetNumberOfRecords(string path)
        {
            using (StreamReader sr = new StreamReader(File.OpenRead(path)))
                return sr.ReadToEnd().Split('\n').Length - 1;
        }
    }
}
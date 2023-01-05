using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace DataTable
{
    public sealed class DataTable
    {
        private readonly string mainPath;
        private readonly string indexPath;
        private readonly string overflowPath;

        private const int BLOCK_CAPACITY = 4;
        private const int NUMBER_OF_BLOCKS = 4;

        private HashSet<int> keys;
        private List<IndexRecord>[] index;
        private List<IndexRecord> overflow;

        private int lastReference;

        private int count;
        public int Count { get => count; }

        public DataTable(string mainPath, string indexPath, string overflowPath)
        {
            this.mainPath = mainPath;
            this.indexPath = indexPath;
            this.overflowPath = overflowPath;

            keys = new HashSet<int>();           
            lastReference = FileWork.GetNumberOfRecords(mainPath);

            this.InitializeIndex();
            this.InitializeOverflow();
        }

        public bool Get(int key, out string value)
        {
            if (keys.Contains(key))
            {
                int blockIndex = GetBlockIndex(key);
                int reference = blockIndex != -1 ? SearchingAlgorithms.GetRecordReference(index[blockIndex], key) : SearchingAlgorithms.GetRecordReference(overflow, key);

                if (reference != -1)
                {
                    value = FileWork.GetValue(mainPath, reference);
                    return true;
                }
            }

            value = null;
            return false;
        }

        public bool Add(int key, string value)
        {
            if (keys.Add(key))
            {
                IndexRecord newRecord = new IndexRecord(key, lastReference);
                MainRecord newMainRecord = new MainRecord(0, key, value);
                FileWork.AppendToFile(mainPath, newMainRecord.ToString());
                ++count;
                ++lastReference;

                int blockIndex = GetBlockIndex(key);

                if (blockIndex != -1)
                {
                    index[blockIndex][GetIndexInBlock(key)] = newRecord;
                    FileWork.WriteRecords(indexPath, IndexToRecords(index));
                    return true;
                }
                else
                {
                    overflow.Add(newRecord);
                    overflow = overflow.OrderBy(record => record.Key).ToList();
                    FileWork.WriteRecords(overflowPath, overflow);
                    return true;
                }
            }

            return false;
        }

        public bool Edit(int key, string newValue)
        {
            if (keys.Contains(key))
            {
                int blockIndex = GetBlockIndex(key);
                int reference = blockIndex != -1 ? SearchingAlgorithms.GetRecordReference(index[blockIndex], key) : SearchingAlgorithms.GetRecordReference(overflow, key);

                if (reference != -1)
                {
                    FileWork.ChangeValue(mainPath, key, reference, newValue);
                    return true;
                }
            }

            return false;
        }

        public bool Remove(int key)
        {
            if (keys.Contains(key))
            {
                int blockIndex = GetBlockIndex(key);

                if (blockIndex != -1)
                {
                    int indexInBlock = SearchingAlgorithms.GetRecordPosition(index[blockIndex], key);

                    if (indexInBlock != -1)
                    {
                        int reference = SearchingAlgorithms.GetRecordReference(index[blockIndex], key);
                        index[blockIndex][indexInBlock] = IndexRecord.EmptyRecord;
                        FileWork.WriteRecords(indexPath, IndexToRecords(index));
                        FileWork.MarkRecordAsRemoved(mainPath, reference);
                    }
                }
                else
                {
                    int index = SearchingAlgorithms.GetRecordPosition(overflow, key);

                    if (index != -1)
                    {
                        int reference = SearchingAlgorithms.GetRecordReference(overflow, key);
                        overflow.RemoveAt(index);
                        FileWork.WriteRecords(overflowPath, overflow);
                        FileWork.MarkRecordAsRemoved(mainPath, reference);
                    }
                }

                keys.Remove(key);
                return true;
            }

            return false;
        }

        public void Clear()
        {
            File.WriteAllText(mainPath, String.Empty);
            File.WriteAllText(indexPath, String.Empty);
            File.WriteAllText(overflowPath, String.Empty);

            lastReference = 0;
            count = 0;
            keys.Clear();
            overflow.Clear();
            
            foreach(List<IndexRecord> block in index)
            {
                for (int i = 0; i < BLOCK_CAPACITY; ++i)
                    block[i] = IndexRecord.EmptyRecord;
            }
        }

        public List<MainRecord> GetMainRecords(string path)
        {
            List<MainRecord> records = new List<MainRecord>();

            using (StreamReader sr = new StreamReader(File.OpenRead(path)))
            {
                while (!sr.EndOfStream)
                {
                    MainRecord currentRecord = MainRecord.ConvertLineToRecord(sr.ReadLine());

                    if (currentRecord.IsDeleted == 0)
                        records.Add(currentRecord);
                }
            }

            return records;
        }

        public List<IndexRecord> GetIndexRecords(string path)
        {
            List<IndexRecord> records = new List<IndexRecord>();

            using (StreamReader sr = new StreamReader(File.OpenRead(path)))
            {
                while (!sr.EndOfStream)
                    records.Add(IndexRecord.ConvertLineToRecord(sr.ReadLine()));
            }

            return records;
        }

        private int GetBlockIndex(int key)
        {
            int index = (key - 1) / BLOCK_CAPACITY;

            if (index < NUMBER_OF_BLOCKS)
                return index;

            return -1;
        }

        private int GetIndexInBlock(int key) => (key - 1) % BLOCK_CAPACITY;

        private List<IndexRecord> IndexToRecords(List<IndexRecord>[] index)
        {
            List<IndexRecord> records = new List<IndexRecord>();

            foreach (List<IndexRecord> block in index)
            {
                foreach (IndexRecord record in block)
                {
                    if (record != null)
                        records.Add(record);
                }
            }

            return records;
        }

        private void InitializeIndex()
        {
            List<IndexRecord> records = this.GetIndexRecords(indexPath);

            index = new List<IndexRecord>[NUMBER_OF_BLOCKS];

            for (int i = 0; i < NUMBER_OF_BLOCKS; ++i)
            {
                index[i] = new List<IndexRecord>(BLOCK_CAPACITY);

                for (int j = 0; j < BLOCK_CAPACITY; ++j)
                    index[i].Add(IndexRecord.EmptyRecord);
            }

            int blockIndex;

            foreach (IndexRecord record in records)
            {
                if (record.Key == 0)
                    continue;

                blockIndex = GetBlockIndex(record.Key);

                if (blockIndex != -1)
                    index[blockIndex][GetIndexInBlock(record.Key)] = record;
            }

            foreach (IndexRecord record in records)
                keys.Add(record.Key);
        }

        private void InitializeOverflow()
        {
            overflow = this.GetIndexRecords(overflowPath);

            foreach (IndexRecord record in overflow)
                keys.Add(record.Key);
        }
    }
}
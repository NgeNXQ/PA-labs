using System.Collections.Generic;

namespace DataTable
{
    public static class SearchingAlgorithms
    {
        public static int GetRecordReference(List<IndexRecord> records, int key)
        {
            List<IndexRecord> updatedBlock = RemoveEmptyRecords(records);

            int min = 0;
            int max = updatedBlock.Count - 1;

            int mid;
            int guess;

            while (min <= max)
            {
                mid = (min + max) / 2;
                guess = updatedBlock[mid].Key;

                if (guess == key)
                    return updatedBlock[mid].Reference;
                else if (guess < key)
                    min = mid + 1;
                else
                    max = mid - 1;
            }

            return -1;
        }

        public static int GetRecordPosition(List<IndexRecord> records, int key)
        {
            List<IndexRecord> updatedBlock = RemoveEmptyRecords(records);

            int min = 0;
            int max = updatedBlock.Count - 1;

            int mid;
            int guess;

            while (min <= max)
            {
                mid = (min + max) / 2;
                guess = updatedBlock[mid].Key;

                if (guess == key)
                    return mid;
                else if (guess < key)
                    min = mid + 1;
                else
                    max = mid - 1;
            }

            return -1;
        }

        private static List<IndexRecord> RemoveEmptyRecords(List<IndexRecord> block)
        {
            List<IndexRecord> result = new List<IndexRecord>();

            foreach (IndexRecord record in block)
            {
                if (record.Key != 0)
                    result.Add(record);
            }

            return result;
        }
    }
}
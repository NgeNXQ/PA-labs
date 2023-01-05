namespace DataTable
{
    public sealed class IndexRecord
    {
        public const int MAX_KEY_LENGTH = 6;

        public int Key { get; }

        public int Reference { get; }

        public static IndexRecord EmptyRecord { get => new IndexRecord(0, 0); }

        public IndexRecord(int key, int reference)
        {
            this.Key = key;
            this.Reference = reference;
        }

        public static IndexRecord ConvertLineToRecord(string line)
        {
            string[] values = line.Split(':');
            return new IndexRecord(int.Parse(values[0]), int.Parse(values[1]));
        }

        public override string ToString() => $"{Key}:{Reference}";
    }

    public sealed class MainRecord
    {
        public const int MAX_KEY_LENGTH = 6;
        public const int MAX_VALUE_LENGTH = 50;
        public const int LENGTH = 5 + MAX_KEY_LENGTH + MAX_VALUE_LENGTH;

        public byte IsDeleted { get; set; }

        public int Key { get; }

        public string Value { get; set; }

        public MainRecord(byte isDeleted, int key, string value)
        {
            this.IsDeleted = isDeleted;
            this.Key = key;
            this.Value = value;
        }

        public static MainRecord ConvertLineToRecord(string line)
        {
            string[] values = line.Split(':');
            return new MainRecord(byte.Parse(values[0]), int.Parse(values[1].Trim()), values[2].Trim());
        }

        public override string ToString() => $"{IsDeleted}:{Key.ToString().PadLeft(MAX_KEY_LENGTH)}:{Value.PadLeft(MAX_VALUE_LENGTH)}";
    }
}
namespace Tests
{
    [TestClass]
    public class ExternalSortingTests
    {
        [TestMethod]
        public void SelectApproach_Test()
        {
            const string input = "0";
            bool result = false;

            if (Int32.TryParse(input, out int value))
            {
                if (value > 1 || value < 0)
                    result = true;
            }

            Assert.IsTrue(!result, "Failed to select algorithm.");
        }

        [TestMethod]
        public void GetSize_Test()
        {
            const string input = "15";
            const long fileSize = 80L;
            int size;
            bool result = true;

            if (Int32.TryParse(input, out size))
            {
                if (size > fileSize)
                    result = false;
            }
            else
                result = false;

            Assert.IsTrue(result, "Failed to get size.");
        }

        [TestMethod]
        public void TryGetNumber_GettingNumber_Test()
        {
            string text;
            int expected;
            int actual;

            text = "245 11 13 24 9 67";
            expected = 245;

            using (StreamWriter sw = new StreamWriter(File.OpenWrite("test.txt")))
                sw.Write(text);

            using (StreamReader sr = new StreamReader(File.OpenRead("test.txt")))
                ExternalSorting.ExternalSorting.TryGetNumber(sr, out actual);

            Assert.AreEqual(expected, actual, "Failed to get a number from the file.");

            text = "";
            expected = 0;

            File.WriteAllText("test.txt", String.Empty);

            using (StreamWriter sw = new StreamWriter(File.OpenWrite("test.txt")))
                sw.Write(text);

            using (StreamReader sr = new StreamReader(File.OpenRead("test.txt")))
                ExternalSorting.ExternalSorting.TryGetNumber(sr, out actual);

            Assert.AreEqual(expected, actual, "Failed to detect the end of the file.");

            File.Delete("test.txt");
        }

        [TestMethod]
        public void TryGetNumber_ReturningBoolean_Test()
        {
            string text = "245 11 13 24 9 67";
            bool expected = true;
            bool actual;

            using (StreamWriter sw = new StreamWriter(File.OpenWrite("test.txt")))
                sw.Write(text);

            using (StreamReader sr = new StreamReader(File.OpenRead("test.txt")))
                actual = ExternalSorting.ExternalSorting.TryGetNumber(sr, out _);

            Assert.AreEqual(expected, actual, "Failed to return a number from the file.");

            File.WriteAllText("test.txt", String.Empty);

            text = "";
            expected = false;

            using (StreamWriter sw = new StreamWriter(File.OpenWrite("test.txt")))
                sw.Write(text);

            using (StreamReader sr = new StreamReader(File.OpenRead("test.txt")))
                actual = ExternalSorting.ExternalSorting.TryGetNumber(sr, out _);

            Assert.AreEqual(expected, actual, "Failed to detect the end of the file.");

            File.Delete("test.txt");
        }

        [TestMethod]
        public void ManageData_Test()
        {
            const string text = "5 3 7 4 18 2";

            const string expectedBFile = "5 4 18 ";
            const string expectedCFile = "3 7 2 ";

            string actualBFile = String.Empty;
            string actualCFile = String.Empty;

            using (StreamWriter sw = new StreamWriter(File.OpenWrite("A.txt")))
                sw.Write(text);

            ExternalSorting.ExternalSorting.ManageData("A.txt");

            using (StreamReader sr1 = new StreamReader(File.OpenRead("B.txt")), sr2 = new StreamReader(File.OpenRead("C.txt")))
            {
                actualBFile = sr1.ReadToEnd();
                actualCFile = sr2.ReadToEnd();
            }

            if (!actualBFile.Equals(expectedBFile))
                Assert.Fail("The content of B file is not correct.");

            if (!actualCFile.Equals(expectedCFile))
                Assert.Fail("The content of C file is not correct.");

            File.Delete("A.txt");
            File.Delete("B.txt");
            File.Delete("C.txt");
        }

        [TestMethod]
        public void MergeData_Test()
        {
            const string BFileText = "5 4 18";
            const string CFileText = "3 7 2";

            const string expected = "3 5 7 2 4 18 ";

            string actual = String.Empty;

            using (StreamWriter sw1 = new StreamWriter(File.OpenWrite("B.txt")), sw2 = new StreamWriter(File.OpenWrite("C.txt")))
            {
                sw1.Write(BFileText);
                sw2.Write(CFileText);
            }

            ExternalSorting.ExternalSorting.MergeData("A.txt");

            using (StreamReader sr = new StreamReader(File.OpenRead("A.txt")))
                actual = sr.ReadToEnd();

            Assert.AreEqual(expected, actual, "Failed to merge files.");

            File.Delete("A.txt");
            File.Delete("B.txt");
            File.Delete("C.txt");
        }

        [TestMethod]
        public void IsFileSorted_Test()
        {
            const string text = "1 2 3 4 5";
            const bool expected = true;
            bool actual;

            using (StreamWriter sw = new StreamWriter("test.txt"))
                sw.Write(text);

            actual = ExternalSorting.ExternalSorting.IsFileSorted("test.txt");

            Assert.AreEqual(expected, actual, "File is not sorted.");

            File.Delete("test.txt");
        }
            
        [TestMethod]
        public void PreSort_Test()
        {
            const string text = "3 2 1 4 5";
            string result;

            using (StreamWriter sw = new StreamWriter("D:\\Desktop\\A.txt"))
                sw.Write(text);

            ExternalSorting.ExternalSorting.PreSort("D:\\Desktop\\A.txt", 6);

            using (StreamReader sr = new StreamReader("D:\\Desktop\\A.txt"))
                result = sr.ReadToEnd();

            if (!result.Equals("1 2 3 4 5 "))
                Assert.Fail("File is not sorted properly!");

            File.Delete("D:\\Desktop\\A.txt");
        }
    }
}
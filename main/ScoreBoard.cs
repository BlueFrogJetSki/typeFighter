using System.Text.Json;

namespace typeFighter
{
    public class ScoreBoard
    {
        private static ScoreBoard? instance;
        private List<Record> records;
        public struct Record
        {

            public string id { get; set; }
            public string name { get; set; }
            public int score { get; set; }
            public string time { get; set; }


            public Record(string id, string name, int score, string time)
            {
                this.id = id;
                this.name = name;
                this.score = score;
                this.time = time;

            }

            // public string toString()
            // {
            //     return $"{id}, {name}, {score}, {time}";
            // }
        }

        // if there is a tie, put y before x
        private class RecordComparer : IComparer<Record>
        {
            public int Compare(Record x, Record y)
            {
                // First compare by score
                int scoreComparison = -x.score.CompareTo(y.score);

                // If scores are equal, compare by time
                if (scoreComparison == 0)
                {
                    // Compare time strings as HH:mm format
                    // Convert times to TimeSpan for comparison
                    TimeSpan xTime = TimeSpan.Parse(x.time);
                    TimeSpan yTime = TimeSpan.Parse(y.time);

                    // Compare by time (shorter times should come before longer times)
                    return xTime.CompareTo(yTime);
                }

                return scoreComparison;
            }
        }


        private ScoreBoard()
        {
            records = [];

        }

        private ScoreBoard(string filePath)
        {
            records = [];
            parseFromJson(filePath);
        }



        public static ScoreBoard getInstance()
        {
            if (instance == null)
            {
                instance = new ScoreBoard();
            }
            return instance;
        }


        public static ScoreBoard getInstance(string filePath)
        {
            if (instance == null)
            {
                instance = new ScoreBoard(filePath);
            }
            return instance;
        }

        // Post: new record is added to records, and records is sorted by score then time
        public void addRecord(string id, string name, int score, string time)
        {
            records.Add(new Record(id, name, score, time));
            sortRecords();
        }

        public void addRecord(Record record)
        {
            records.Add(record);
            sortRecords();
        }

        public void SaveAsJson(string filePath)
        {

            try
            {
                string jsonString = JsonSerializer.Serialize(records);
                File.WriteAllText(filePath, jsonString);
                Console.WriteLine($"Records saved to {filePath}\n");
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }

        }

        // records = JsonSerializer.Deserialize<List<Record>>(jsonString) ?? [];
        public void parseFromJson(string filePath)
        {
            try
            {
                // Read the entire file contents as a string
                string jsonString = File.ReadAllText(filePath);

                // Deserialize JSON array to a List<Record>
                List<Record> records = JsonSerializer.Deserialize<List<Record>>(jsonString) ?? [];

                //Erase previous records stored
                EraseRecords();

                // Print or use the deserialized data
                foreach (Record record in records)
                {
                    addRecord(record);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File not found: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading or deserializing JSON: {ex.Message}");
            }
        }


        public void printScoreBoard()
        {
            Console.WriteLine("--ScoreBoard--\n");
            Console.WriteLine("--Top 5 Records--\n");

            for (int i = 0; i < records.Count() && i < 5; i++)
            {
                Console.WriteLine($"Player:{records[i].name} - Score:{records[i].score} - Time:{records[i].time}\n");
            }


        }

        // sort records by score, then by time
        private void sortRecords()
        {
            // if tie, put y before x, refer to RecordComparer
            records.Sort(new RecordComparer());
        }

        public int GetRecordsCount()
        {
            return records.Count();
        }

        public List<Record> GetRecords()
        {
            return records;
        }

        public void EraseRecords()
        { records = new List<Record>(); }
    }
}
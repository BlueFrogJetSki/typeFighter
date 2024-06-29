namespace typeFighter
{
    public class Game
    {
        private string playerName;
        private bool running;
        private int turnCount;
        private int maxTurns;

        private int score;
        private ScoreBoard scoreBoard;

        private string? textFilePath;
        private List<string> words;
        private List<string> wordsSeen;

        private GameTimer gameTimer;


        public Game(string playerName, int maxTurns, string textFilePath)
        {
            this.playerName = playerName;
            running = true;
            turnCount = 1;
            this.maxTurns = maxTurns;
            this.textFilePath = textFilePath;
            words = GetWordsFromFile(textFilePath);
            wordsSeen = new List<string>();
            score = 0;
            scoreBoard = ScoreBoard.getInstance("./ScoreBoard.json");
            this.gameTimer = new GameTimer();

        }

        public void Play()
        {
            printIntroduction();

            Random random = new Random();

            while (running)
            {
                if (turnCount == 1)
                {
                    gameTimer.Start();
                }
                if (turnCount > maxTurns)
                {
                    gameTimer.Stop();
                    break;
                };

                int randomIdx = random.Next(words.Count);
                string wordSelected = words[randomIdx];

                while (wordsSeen.Contains(wordSelected))
                {
                    randomIdx++;
                    wordSelected = words[randomIdx];
                }

                wordsSeen.Add(wordSelected);

                Console.WriteLine($"target is \"{wordSelected}\"\n");

                string input = GetUserInput();

                Console.WriteLine("\n");

                AdjustScore(input, wordSelected);

                turnCount++;
            }

            printExitMessage();
            SaveToScoreBoard();
            SaveScoreBoardToJson("./ScoreBoard.json");
            this.scoreBoard.printScoreBoard();

        }

        private void printIntroduction()
        {
            Console.WriteLine("Welcome to TypeFighter\n");
            Console.WriteLine("Point System:");
            Console.WriteLine("-Correctly typed - 1 p");
            Console.WriteLine("-Incorrectly typed - 0 p\n");

        }

        private void printExitMessage()
        {
            Console.WriteLine("Thank you for playing \n");
            Console.WriteLine($"You total score is {score}/{maxTurns}\n");

            Console.WriteLine("Attempted:\n");
            foreach (string word in wordsSeen)
            {
                Console.Write($"{word}\n");
            }

        }
        private List<string> GetWordsFromFile(string path)

        {

            List<string> words = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    Console.WriteLine($"Reading from {path}\n");

                    string? word;

                    while ((word = reader.ReadLine()) != null)
                    {
                        word = word.Replace("\"", "");
                        words.Add(word);
                    }

                    Console.WriteLine("Finished reading file\n");

                }
            }
            catch (IOException e)
            {

                Console.WriteLine($"An error {e} occured while reading from {path}\n");
            }

            return words;

        }

        private void SaveToScoreBoard()
        {
            Guid uniqueID = Guid.NewGuid();

            this.scoreBoard.addRecord(uniqueID.ToString(), this.playerName, this.score, this.gameTimer.toString());
        }
        private void SaveScoreBoardToJson(string filePath)
        {
            scoreBoard.SaveAsJson(filePath);
        }
        public void AdjustScore(string input, string wordSelected)
        {
            if (CheckUserInput(input, wordSelected))

            {
                score++;
                Console.WriteLine("Correct!\n");
            }
            else
            {
                Console.WriteLine("Incorrect!\n");
            }

        }
        public string GetUserInput()
        {
            string? input = Console.ReadLine();

            if (input != null)
            {
                return input;
            }

            return "";
        }

        //returns input.ToUpper() == word.ToUpper()
        public bool CheckUserInput(string input, string wordSelected)

        {

            return (wordSelected.ToUpper().Equals(input.ToUpper()));

        }

        public string GetPlayerName()
        {
            return playerName;
        }

        public bool IsRunning()
        {
            return running;
        }

        public int GetTurnCount()
        {
            return turnCount;
        }

        public int GetMaxTurns()
        {
            return maxTurns;
        }

        public int GetScore()
        {
            return score;
        }

        public List<string> GetWords()
        {
            return words;
        }

        public List<string> GetWordsSeen()
        {
            return wordsSeen;
        }




    }


}


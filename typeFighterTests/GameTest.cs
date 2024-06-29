using typeFighter;

namespace typeFighterTest
{
    public class GameTest
    {
        public Game game;

        public GameTest()
        {
            this.game = new Game("player", 3, "testWords.txt");
        }

        [Fact]
        public void TestGetWordsFromFile()
        {
            List<string> testWords = ["apple", "banana", "exit"];
            Assert.Equal(testWords, game.GetWords());
        }

        [Fact]
        public void TestCheckUserInput()
        {
            Assert.True(game.CheckUserInput("apple", "apple"));
            Assert.True(game.CheckUserInput("APPLE", "apple"));
            Assert.True(game.CheckUserInput("apple", "APPLE"));

        }

        [Fact]
        public void TestAdjustScore()
        {
            game.AdjustScore("apple", "apple");
            game.AdjustScore("APPLE", "apple");
            game.AdjustScore("apple", "APPLE");

            Assert.Equal(3, game.GetScore());
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using typeFighter;

namespace typeFighterTest
{
    public class ScoreBoardTest
    {
        [Fact]
        public void AddRecord_ShouldAddRecord()
        {
            // Arrange
            ScoreBoard scoreboard = ScoreBoard.getInstance();
            scoreboard.EraseRecords();
            string id = "1";
            string name = "Player1";
            int score = 100;
            string time = "10:00";

            // Act
            scoreboard.addRecord(id, name, score, time);

            // Assert
            Assert.Equal(1, scoreboard.GetRecordsCount());
            Assert.Equal(id, scoreboard.GetRecords()[0].id);
            Assert.Equal(name, scoreboard.GetRecords()[0].name);
            Assert.Equal(score, scoreboard.GetRecords()[0].score);
            Assert.Equal(time, scoreboard.GetRecords()[0].time);


        }

        [Fact]
        public void AddRecords_SortByScoreThenTime()
        {
            // Arrange
            ScoreBoard scoreboard = ScoreBoard.getInstance();
            scoreboard.EraseRecords();

            scoreboard.addRecord("1", "Player1", 130, "10:00");
            scoreboard.addRecord("2", "Player2", 110, "09:30");
            scoreboard.addRecord("3", "Player3", 120, "11:15");

            // Assert
            // Assert.Equal("Player1", scoreboard.GetRecords()[0].name); // Highest score first
            Assert.Equal("Player3", scoreboard.GetRecords()[1].name);
            Assert.Equal("Player2", scoreboard.GetRecords()[2].name); // Lowest score last
        }
        [Fact]
        public void AddRecords_SortByScoreThenTime_ScoreTie()
        {
            // Arrange
            ScoreBoard scoreboard = ScoreBoard.getInstance();
            scoreboard.EraseRecords();

            scoreboard.addRecord("1", "Player1", 130, "10:00");
            scoreboard.addRecord("2", "Player2", 130, "09:30");
            scoreboard.addRecord("3", "Player3", 150, "11:15");

            // Assert
            Assert.Equal("Player3", scoreboard.GetRecords()[0].name); //highest score 
            Assert.Equal("Player2", scoreboard.GetRecords()[1].name); // faster than player 1
            Assert.Equal("Player1", scoreboard.GetRecords()[2].name); // Lowest score last
        }

        [Fact]
        public void AddRecords_SortByScoreThenTime_ScoreAndTimeTie()
        {
            // Arrange
            ScoreBoard scoreboard = ScoreBoard.getInstance();
            scoreboard.EraseRecords();

            scoreboard.addRecord("1", "Player1", 100, "10:00");
            scoreboard.addRecord("2", "Player2", 100, "10:00");
            scoreboard.addRecord("3", "Player3", 120, "11:15");

            // Assert
            Assert.Equal("Player3", scoreboard.GetRecords()[0].name);
            Assert.Equal("Player2", scoreboard.GetRecords()[1].name);
            Assert.Equal("Player1", scoreboard.GetRecords()[2].name); // Lowest score last
        }

        [Fact]
        public void JsonPersistenceTest()
        {
            ScoreBoard scoreboard = ScoreBoard.getInstance();
            scoreboard.EraseRecords();

            string filePath = "./testScoreBoard.json";

            scoreboard.addRecord("1", "Player1", 300, "10:00");
            scoreboard.addRecord("2", "Player2", 200, "10:00");
            scoreboard.addRecord("3", "Player3", 100, "11:15");

            scoreboard.SaveAsJson(filePath);
            Assert.Equal("Player1", scoreboard.GetRecords()[0].name);
            Assert.Equal("Player2", scoreboard.GetRecords()[1].name);
            Assert.Equal("Player3", scoreboard.GetRecords()[2].name); // Lowest score last


            scoreboard.parseFromJson(filePath);
            Assert.Equal("Player1", scoreboard.GetRecords()[0].name);
            Assert.Equal("Player2", scoreboard.GetRecords()[1].name);
            Assert.Equal("Player3", scoreboard.GetRecords()[2].name); // Lowest score last



        }
    }
}
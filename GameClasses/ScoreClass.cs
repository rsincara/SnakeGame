using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WpfApp1.GameClasses
{
    public static class ScoreClass
    {
        public static string[] GetFirstScoresArray()
        {
            var records = File.ReadAllLines("scores/scores.txt").OrderByDescending(int.Parse);
            return records.Count() > 10 ? records.Take(10).ToArray() : records.ToArray();
        }

        public static void AddScore(int score)
        {
            var list = new List<string>();
            list.Add(score.ToString());
            File.AppendAllLines("scores/scores.txt", list);
        }

        public static string GetBestScore()
        {
            return GetFirstScoresArray().First();
        }

        public static string GetGamesCount()
        {
            return File.ReadAllLines("scores/scores.txt").Length.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquirrelThePirate2
{   class LevelManager
    {
        private int currentLevel = -1;
        private int theEndLevel = 20;

        private static readonly Dictionary<int, string> levelsByNumber = new Dictionary<int, string>()
        {
            { 0, "levels/level0.level" },
            { 1, "levels/level1.level" },
            { 2, "levels/level2.level" },
            { 3, "levels/level3.level" },
            { 4, "levels/level4.level" },
            { 5, "levels/level5.level" },
            { 6, "levels/level6.level" },
            { 7, "levels/level7.level" },
            { 8, "levels/level8.level" },
            { 9, "levels/level9.level" },
            { 10, "levels/level10.level" },
            { 11, "levels/level11.level" },
            { 12, "levels/level12.level" },
            { 13, "levels/level13.level" },
            { 14, "levels/level14.level" },
            { 15, "levels/level15.level" },
            { 16, "levels/level16.level" },
            { 17, "levels/level17.level" },
            { 18, "levels/level18.level" },
            { 19, "levels/level19.level" },
            { 20, "levels/level20.level" }
        };

        private static readonly Dictionary<string, int> levelsByPassword = new Dictionary<string, int>()
        {
            { "easy", 0 },
            { "pie", 1 },
            { "cake", 2 },
            { "chocolate", 3 },
            { "mixing", 4 },
            { "luxury", 5 },
            { "postmaster", 6 },
            { "juno", 7 },
            { "alpine", 8 },
            { "summer", 9 },
            { "halifax", 10 },
            { "stop", 11 },
            { "attention", 12 },
            { "cup", 13 },
            { "marshmellow", 14 },
            { "paused", 15 },
            { "chosen", 16 },
            { "repeating", 17 },
            { "expired", 18 },
            { "stranger", 19 },
            { "homeward", 20 }
        };

        private static readonly Dictionary<int, string> levelPasswords = new Dictionary<int, string>()
        {
            { 0, "easy" },
            { 1, "pie" },
            { 2, "cake" },
            { 3, "chocolate" },
            { 4, "mixing" },
            { 5, "luxury" },
            { 6, "postmaster" },
            { 7, "juno" },
            { 8, "alpine" },
            { 9, "summer" },
            { 10, "halifax" },
            { 11, "stop" },
            { 12, "attention" },
            { 13, "cup" },
            { 14, "marshmellow" },
            { 15, "paused" },
            { 16, "chosen" },
            { 17, "repeating" },
            { 18, "expired" },
            { 19, "stranger" },
            { 20, "homeward" }
        };


        public Level GetLevel(int level)
        {
            string fileName;
            if (!levelsByNumber.TryGetValue(level, out fileName) )
            {
                return null;
            }
            currentLevel = level;
            return Level.GetLevel(fileName);
        }

        public Level GetLevel(string password)
        {
            int level;
            if (!levelsByPassword.TryGetValue(password.ToLower(), out level))
            {
                return null;
            }
            currentLevel = level;
            return Level.GetLevel(levelsByNumber[level]);
        }

        public Level GetNextLevel()
        {   currentLevel++;
            return GetLevel(currentLevel);
        }
        public Level GetCurrentLevel()
        {   return GetLevel(currentLevel);
        }

        public int LevelNumber
        {
            get { return currentLevel; }
        }

        public string GetPassword()
        {
            return levelPasswords[currentLevel+1];
        }

        public static bool IsPassword(string password)
        {
            int level;
            return levelsByPassword.TryGetValue(password, out level);
        }

        public bool IsGameOver()
        {
            return (currentLevel == theEndLevel);
        }
    }
}

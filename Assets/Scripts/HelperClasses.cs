using System;
using System.IO;
using UnityEngine;

namespace SnakeGame
{

  

    public enum FoodColor
    {
        Red,
        Blue,
        Green
    }
    [Serializable]
    public class FoodData
    {
        public GameObject Item;
        public FoodColor FColor;
        public int FoodValue;
    }

    public enum CollisionType
    {
        Wall,
        Food
    }

    #region "Player struct"
    [Serializable]
    struct Player
    {
        public string playerScore;
    }
    #endregion

    #region "Constants"
    struct DataConst
    {
        public const string userFilePath = "Playerdata";
        public const string GameSceneName = "GameScene";
        public const string MainSceneName = "MainScene";

        public const string FoodTag = "Food";
        public const string WallTag = "Wall";
    }
    #endregion


    #region "Helper - Save and Load player data"
    struct IOOperation
    {
        private string filPath;

        public IOOperation(string filName)
        {
            filPath = Path.Combine(Application.persistentDataPath, filName);
        }

        public void SavData(int score)
        {
            Player player = new Player
            {
                playerScore = score.ToString()
            };
            File.WriteAllText(filPath, JsonUtility.ToJson(player));
        }

        public string LoadData()
        {
            if (File.Exists(filPath))
            {
                string data = File.ReadAllText(filPath);
                Player player = JsonUtility.FromJson<Player>(data);
                return player.playerScore;
            }
            return "0";
        }
        #endregion



       
    }
}

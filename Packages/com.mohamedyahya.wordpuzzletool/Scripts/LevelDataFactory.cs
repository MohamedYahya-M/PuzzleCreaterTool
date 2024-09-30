using UnityEngine;

namespace com.mohamedyahya.wordpuzzletool
{
    public class LevelDataFactory
    {
        public static LevelData CreateLevelData(string levelName, Sprite scenarioImage, string[] wordChoices, int correctAnswerIndex, Sprite goodEndingImage, Sprite badEndingImage)
        {
            LevelData newLevel = ScriptableObject.CreateInstance<LevelData>();
            newLevel.levelName = levelName;
            newLevel.scenarioImage = scenarioImage;
            newLevel.words = wordChoices;
            newLevel.correctWordIndex = correctAnswerIndex;
            newLevel.goodEndingImage = goodEndingImage;
            newLevel.badEndingImage = badEndingImage;
            return newLevel;
        }
    }
}
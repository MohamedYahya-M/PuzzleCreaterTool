using UnityEngine;

public class LevelFactory
{
   
    public static LevelData CreateLevel(int levelNumber)
    {
       
        string levelAssetPath = $"Levels/Level_{levelNumber}"; 
        LevelData levelData = Resources.Load<LevelData>(levelAssetPath);

        if (levelData == null)
        {
            Debug.LogError($"Level data for level {levelNumber} could not be found at {levelAssetPath}");
            return null;
        }

        return levelData;
    }
}

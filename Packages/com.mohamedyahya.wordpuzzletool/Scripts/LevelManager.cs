using UnityEngine;
namespace com.mohamedyahya.wordpuzzletool
{

    public class LevelManager : MonoBehaviour
    {
        public LevelData currentLevelData;
        public PuzzleView puzzleView;
        private int currentLevelIndex = 0;
        public GameObject retryPanel;
        public int totalNumberOfLevels = 3;

        void Start()
        {
            LoadLevel(1); // Start with Level 1
        }

        public void LoadLevel(int levelNumber)
        {
            retryPanel.SetActive(false);
            // Load the next level by number (assets are stored as "Level_1", "Level_2", etc.)
            currentLevelData = Resources.Load<LevelData>($"Levels/Level_{levelNumber}");

            if (currentLevelData != null)
            {
                // Display the level in the UI
                puzzleView.DisplayLevel(currentLevelData);
            }
            else
            {
                Debug.LogError($"Level {levelNumber} not found!");
            }
        }

        public void LoadNextLevel()
        {
            currentLevelIndex++;
            LoadLevel(currentLevelIndex + 1); // Increment the level index and load the next
        }
        public bool IsLastLevel()
        {
            return currentLevelData.levelNumber == totalNumberOfLevels;
        }
    }
}

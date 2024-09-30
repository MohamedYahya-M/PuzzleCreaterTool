using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace com.mohamedyahya.wordpuzzletool
{

    public class PuzzleController : MonoBehaviour
    {
        public PuzzleView puzzleView;
        public LevelManager levelManager;
        public GameObject retryPanel;

        // Events for correct and incorrect answers
        public event Action OnCorrectAnswer;
        public event Action OnIncorrectAnswer;

        public void OnWordSelected(int wordIndex)
        {
            DisableAllButtons();
            bool isCorrect = wordIndex == levelManager.currentLevelData.correctWordIndex;

            // Trigger events based on whether the answer is correct or incorrect
            if (isCorrect)
            {
                OnCorrectAnswer?.Invoke(); // Trigger correct answer event
                HandleCorrectAnswer(wordIndex);
            }
            else
            {
                OnIncorrectAnswer?.Invoke(); // Trigger incorrect answer event
                HandleIncorrectAnswer(wordIndex);
            }
        }

        private void DisableAllButtons()
        {
            foreach (Button button in puzzleView.wordButtons)
            {
                button.interactable = false;
            }
        }

        private void HandleCorrectAnswer(int wordIndex)
        {
            puzzleView.scenarioImage.sprite = levelManager.currentLevelData.goodEndingImage;
            Transform panel = puzzleView.wordButtons[wordIndex].transform.Find("Tick_Panel");
            panel.gameObject.SetActive(true);
            Image correctTick = panel.Find("Correct").GetComponent<Image>();
            correctTick.gameObject.SetActive(true);
            StartCoroutine(LoadNextLevelAfterDelay(2));
        }

        private void HandleIncorrectAnswer(int wordIndex)
        {
            puzzleView.scenarioImage.sprite = levelManager.currentLevelData.badEndingImage;
            Transform panel = puzzleView.wordButtons[wordIndex].transform.Find("Tick_Panel");
            panel.gameObject.SetActive(true);
            Image wrongTick = panel.Find("Wrong").GetComponent<Image>();
            wrongTick.gameObject.SetActive(true);
            StartCoroutine(LoadGameOverAfterDelay(2));
        }

        private IEnumerator LoadNextLevelAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            if (levelManager.IsLastLevel())
            {
                SceneManager.LoadScene("GameOverScene");
            }
            else
            {
                int nextLevelNumber = levelManager.currentLevelData.levelNumber + 1;
                levelManager.LoadLevel(nextLevelNumber);
            }
        }

        private IEnumerator LoadGameOverAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            retryPanel.gameObject.SetActive(true);
        }
    }
}

//using System.Collections;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public class PuzzleController : MonoBehaviour
//{
//    public PuzzleView puzzleView;
//    public LevelManager levelManager;
//    public GameObject retryPanel;



//    public void OnWordSelected(int wordIndex)
//    {
//        DisableAllButtons();
//        bool isCorrect = wordIndex == levelManager.currentLevelData.correctWordIndex;


//        // Get the selected button's panel and tick images
//        GameObject selectedButton = puzzleView.wordButtons[wordIndex].gameObject;
//        Transform panel = selectedButton.transform.Find("Tick_Panel");
//        Image correctTick = panel.Find("Correct").GetComponent<Image>();
//        Image wrongTick = panel.Find("Wrong").GetComponent<Image>();

//        // Activate the panel and display the appropriate tick image
//        panel.gameObject.SetActive(true);
//        if (isCorrect)
//        {

//            // Show the level good ending image and then load the next level
//           HandleCorrectAnswer(wordIndex);
//        }
//        else
//        {

//            // Show the level bad ending image and handle the failure
//            HandleIncorrectAnswer(wordIndex);
//        }
//    }
//    private void DisableAllButtons()
//    {
//        // Disable all word buttons
//        foreach (Button button in puzzleView.wordButtons)
//        {
//            button.interactable = false;  // Disables the button so it can't be clicked
//        }
//    }
//    public void EnableAllButtons()
//    {
//        // Disable all word buttons
//        foreach (Button button in puzzleView.wordButtons)
//        {
//            button.interactable = true;  // Disables the button so it can't be clicked
//        }
//    }



//    private void HandleCorrectAnswer(int wordIndex)
//    {
//        // Set the scenarioImage to the good ending image for the current level
//        puzzleView.scenarioImage.sprite = levelManager.currentLevelData.goodEndingImage;
//        Transform panel = puzzleView.wordButtons[wordIndex].transform.Find("Tick_Panel");
//        panel.gameObject.SetActive(true); // Show the panel
//        Image correctTick = panel.Find("Correct").GetComponent<Image>();
//        correctTick.gameObject.SetActive(true);
//        // Wait for a short time before loading the next level
//        StartCoroutine(LoadNextLevelAfterDelay(2));
//    }

//    private void HandleIncorrectAnswer(int wordIndex)
//    {
//        // Set the scenarioImage to the bad ending image for the current level
//        puzzleView.scenarioImage.sprite = levelManager.currentLevelData.badEndingImage;
//        Transform panel = puzzleView.wordButtons[wordIndex].transform.Find("Tick_Panel");
//        panel.gameObject.SetActive(true); // Show the panel
//        Image wrongTick = panel.Find("Wrong").GetComponent<Image>();
//        wrongTick.gameObject.SetActive(true);
//        // Wait for a short time before going to the Game Over or retry screen
//        StartCoroutine(LoadGameOverAfterDelay(2));
//    }

//    private IEnumerator LoadNextLevelAfterDelay(float delay)
//    {
//        yield return new WaitForSeconds(delay);

//        // Load the next level
//        if (levelManager.IsLastLevel())
//        {
//            // If it's the last level and the answer is correct, load the GameOverScene
//            SceneManager.LoadScene("GameOverScene");
//        }
//        else
//        {
//            // Otherwise, load the next level
//            int nextLevelNumber = levelManager.currentLevelData.levelNumber + 1;
//            levelManager.LoadLevel(nextLevelNumber);
//        }
//    }

//    private IEnumerator LoadGameOverAfterDelay(float delay)
//    {
//        yield return new WaitForSeconds(delay);

//        // Load the Game Over scene
//        retryPanel.gameObject.SetActive(true);
//    }
//}

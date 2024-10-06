using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public interface IValidationStrategy
{
    bool ValidateAnswer(int selectedWordIndex, int correctWordIndex);
}

// Concrete Strategy for exact match validation
public class ExactMatchValidationStrategy : IValidationStrategy
{
    public bool ValidateAnswer(int selectedWordIndex, int correctWordIndex)
    {
        return selectedWordIndex == correctWordIndex;
    }
}
public class PuzzleController : MonoBehaviour
{
    public PuzzleView puzzleView;
    public LevelManager levelManager;
    public GameObject retryPanel;

    private DraggableItem currentDraggedItem;
    private Vector3 originalPosition;

    // Events for correct and incorrect answers
    public event Action OnCorrectAnswer;
    public event Action OnIncorrectAnswer;

    // Strategy Pattern: Hold the current validation strategy
    private IValidationStrategy validationStrategy;

    private void Start()
    {
        // Set a default strategy (for example, Exact Match Strategy)
        validationStrategy = new ExactMatchValidationStrategy(); // You can replace this with the factory if needed
    }

    public void OnWordSelected(int wordIndex)
    {
        DisableAllButtons();

        // Use the strategy to validate the answer
        bool isCorrect = validationStrategy.ValidateAnswer(wordIndex, levelManager.currentLevelData.correctWordIndex);

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
        // Disable all word buttons
        foreach (Button button in puzzleView.wordButtons)
        {
            button.interactable = false;  // Disables the button so it can't be clicked
        }
    }

    public void EnableAllButtons()
    {
        // Enable all word buttons
        foreach (Button button in puzzleView.wordButtons)
        {
            button.interactable = true;  // Enables the button so it can be clicked
        }
    }

    private void HandleCorrectAnswer(int wordIndex)
    {
        // Set the scenarioImage to the good ending image for the current level
        puzzleView.scenarioImage.sprite = levelManager.currentLevelData.goodEndingImage;
        Transform panel = puzzleView.wordButtons[wordIndex].transform.Find("Tick_Panel");
        panel.gameObject.SetActive(true); // Show the panel
        Image correctTick = panel.Find("Correct").GetComponent<Image>();
        correctTick.gameObject.SetActive(true);
        // Wait for a short time before loading the next level
        StartCoroutine(LoadNextLevelAfterDelay(2));
    }

    private void HandleIncorrectAnswer(int wordIndex)
    {
        // Set the scenarioImage to the bad ending image for the current level
        puzzleView.scenarioImage.sprite = levelManager.currentLevelData.badEndingImage;
        Transform panel = puzzleView.wordButtons[wordIndex].transform.Find("Tick_Panel");
        panel.gameObject.SetActive(true); // Show the panel
        Image wrongTick = panel.Find("Wrong").GetComponent<Image>();
        wrongTick.gameObject.SetActive(true);
        // Wait for a short time before going to the Game Over or retry screen
        StartCoroutine(LoadGameOverAfterDelay(2));
    }

    public void OnItemDragStart(DraggableItem draggableItem)
    {
        currentDraggedItem = draggableItem;
        originalPosition = draggableItem.transform.position;
    }

    public void OnItemDrop(DropZone dropSlot, DraggableItem droppedItem)
    {
        if (currentDraggedItem == null) return;

        bool isCorrect = dropSlot.IsDroppedCorrectly(droppedItem);

        if (isCorrect)
        {
            HandleCorrectDrop(dropSlot);
        }
        else
        {
            HandleIncorrectDrop();
        }

        currentDraggedItem.ResetPosition();  // Reset position regardless of correctness
        currentDraggedItem = null;
    }

    public void OnDragCancel()
    {
        if (currentDraggedItem != null)
        {
            currentDraggedItem.ResetPosition();  // Reset the item’s position to its original spot
        }
    }

    private void HandleCorrectDrop(DropZone dropSlot)
    {
        bool isDroppedCorrectly = dropSlot.IsDroppedCorrectly(currentDraggedItem);

        if (isDroppedCorrectly)
        {
            puzzleView.scenarioImage.sprite = levelManager.currentLevelData.goodEndingImage;

            dropSlot.gameObject.SetActive(false);
            currentDraggedItem.gameObject.SetActive(false);

            currentDraggedItem.SetDraggable(false);

            StartCoroutine(LoadNextLevelAfterDelay(2f));
        }
    }

    private void HandleIncorrectDrop()
    {
        puzzleView.scenarioImage.sprite = levelManager.currentLevelData.badEndingImage;

        puzzleView.dropSlot.SetActive(false);
        currentDraggedItem.gameObject.SetActive(false);

        StartCoroutine(LoadGameOverAfterDelay(2));

        OnDragCancel();  // Reset the dragged item’s position
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

        retryPanel.SetActive(true);
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

//    private DraggableItem currentDraggedItem;
//    private Vector3 originalPosition;

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
//            HandleCorrectAnswer(wordIndex);
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
//    public void OnItemDragStart(DraggableItem draggableItem)
//    {
//        currentDraggedItem = draggableItem;
//        originalPosition = draggableItem.transform.position;
//    }

//    public void OnItemDrop(DropZone dropSlot, DraggableItem droppedItem)
//    {
//        if (currentDraggedItem == null) return;

//        bool isCorrect = dropSlot.IsDroppedCorrectly(droppedItem);

//        if (isCorrect)
//        {
//            HandleCorrectDrop(dropSlot);
//        }
//        else
//        {
//            HandleIncorrectDrop();
//        }

//        currentDraggedItem.ResetPosition();  // Reset position regardless of correctness
//        currentDraggedItem = null;
//    }

//    public void OnDragCancel()
//    {
//        if (currentDraggedItem != null)
//        {
//            currentDraggedItem.ResetPosition();  // Reset the item’s position to its original spot
//        }
//    }

//    private void HandleCorrectDrop(DropZone dropSlot)
//    {
//        bool isDroppedCorrectly = dropSlot.IsDroppedCorrectly(currentDraggedItem);

//        if (isDroppedCorrectly)
//        {
//            puzzleView.scenarioImage.sprite = levelManager.currentLevelData.goodEndingImage;

//            dropSlot.gameObject.SetActive(false);
//            currentDraggedItem.gameObject.SetActive(false);

//            currentDraggedItem.SetDraggable(false);

//            StartCoroutine(LoadNextLevelAfterDelay(2f));
//        }
//    }

//    private void HandleIncorrectDrop()
//    {
//        puzzleView.scenarioImage.sprite = levelManager.currentLevelData.badEndingImage;

//        puzzleView.dropSlot.SetActive(false);
//        currentDraggedItem.gameObject.SetActive(false);

//        StartCoroutine(LoadGameOverAfterDelay(2f));

//        OnDragCancel();  // Reset the dragged item’s position
//    }

//    private IEnumerator LoadNextLevelAfterDelay(float delay)
//    {
//        yield return new WaitForSeconds(delay);

//        if (levelManager.IsLastLevel())
//        {
//            SceneManager.LoadScene("GameOverScene");
//        }
//        else
//        {
//            int nextLevelNumber = levelManager.currentLevelData.levelNumber + 1;
//            levelManager.LoadLevel(nextLevelNumber);
//        }
//    }

//    private IEnumerator LoadGameOverAfterDelay(float delay)
//    {
//        yield return new WaitForSeconds(delay);

//        retryPanel.SetActive(true);
//    }


//}
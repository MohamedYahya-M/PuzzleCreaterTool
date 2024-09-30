using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using com.mohamedyahya.wordpuzzletool;

namespace com.mohamedyahya.wordpuzzletool
{

    public class PuzzleView : MonoBehaviour
    {
        public Image scenarioImage;
        public Button[] wordButtons;
        public GameObject dialoguePanel;
        public TextMeshProUGUI dialogueText;

        public void DisplayLevel(LevelData levelData)
        {
            // Set the scenario image
            if (levelData.scenarioImage != null)
            {
                scenarioImage.sprite = levelData.scenarioImage;
                Debug.Log("Scenario image set successfully.");
            }
            else
            {
                Debug.LogError("Scenario image not found!");
            }

            // Hide and disable buttons initially
            foreach (var button in wordButtons)
            {
                button.gameObject.SetActive(false);  // Hide all buttons
                button.interactable = false;  // Disable interaction initially
            }

            // Starts the coroutine to show the dialogue after 1 second, followed by the buttons
            StartCoroutine(ShowDialogueWithDelay(levelData.dialogueText, levelData));
        }

        // Coroutine to display the dialogue after 1 second, followed by buttons
        private IEnumerator ShowDialogueWithDelay(string dialogue, LevelData levelData)
        {
            // Wait for 1 second before showing the dialogue panel
            yield return new WaitForSeconds(1f);

            // Show the dialogue if it's not empty
            if (!string.IsNullOrEmpty(dialogue))
            {
                dialoguePanel.SetActive(true);  // Show the panel
                dialogueText.text = dialogue;  // Set the dialogue text

                yield return new WaitForSeconds(4f);  // Wait for 3 seconds to show dialogue

                dialoguePanel.SetActive(false);  // Hide the panel
            }

            // After the dialogue, show the buttons with a 1-second delay
            StartCoroutine(ShowButtonsWithDelay(levelData));
        }

        // Coroutine to delay showing the buttons by 1 second after the dialogue
        private IEnumerator ShowButtonsWithDelay(LevelData levelData)
        {
            yield return new WaitForSeconds(1f); // Wait for 1 second after dialogue panel is hidden

            // Loop through the word buttons and update their text and icons
            for (int i = 0; i < wordButtons.Length; i++)
            {
                if (i < levelData.words.Length)
                {
                    // Set the button's text
                    TextMeshProUGUI wordText = wordButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                    wordText.text = levelData.words[i];


                    // Set the button's image
                    Image wordIcon = wordButtons[i].transform.Find("WordIcon").GetComponent<Image>();
                    Sprite wordSprite = Resources.Load<Sprite>($"Images/{levelData.words[i]}");

                    // Reset tick panel (ensure it is inactive at the start)
                    Transform panel = wordButtons[i].transform.Find("Tick_Panel");
                    panel.gameObject.SetActive(false);

                    // Reset correct and wrong ticks
                    Image correctTick = panel.Find("Correct").GetComponent<Image>();
                    Image wrongTick = panel.Find("Wrong").GetComponent<Image>();
                    correctTick.gameObject.SetActive(false); // Ensure correct tick is hidden
                    wrongTick.gameObject.SetActive(false);   // Ensure wrong tick is hidden

                    // Assign the image to the button's icon
                    if (wordSprite != null)
                    {
                        wordIcon.sprite = wordSprite;

                    }
                    else
                    {
                        Debug.LogError($"Image for word '{levelData.words[i]}' not found in Resources.");
                        wordIcon.sprite = null;
                    }

                    // Enable the button and make it interactable after the delay
                    wordButtons[i].gameObject.SetActive(true);  // Make the button visible
                    wordButtons[i].interactable = true;  // Ensure the button is interactable
                }
                else
                {
                    // Hide unused buttons
                    wordButtons[i].gameObject.SetActive(false);
                }
            }
        }
    }
}


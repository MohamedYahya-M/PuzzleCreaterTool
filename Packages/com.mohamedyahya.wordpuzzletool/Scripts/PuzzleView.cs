using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PuzzleView : MonoBehaviour
{
    public Image scenarioImage;
    public GameObject[] draggableItems;
    public Button[] wordButtons;// Array of draggable items (instead of buttons)
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public GameObject dropSlot;  // Drop slot for dragging


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

        // Check the current mechanic type and handle it accordingly
        switch (levelData.mechanicType)
        {
            case MechanicType.Click:
                // Hide draggable items and show word buttons for Click mechanic
                foreach (var button in wordButtons)
                {
                    button.gameObject.SetActive(false);  // Hide all buttons
                    button.interactable = false;  // Disable interaction initially
                }
                foreach (var item in draggableItems)
                {
                    var draggableComponent = item.GetComponent<DraggableItem>();
                    draggableComponent.SetDraggable(false);  // Ensure all items are draggable again
                    draggableComponent.ResetPosition();
                    item.SetActive(false);  // Hide all draggable items
                }
                break;

            case MechanicType.Drag:

                foreach (var button in wordButtons)
                {
                    button.gameObject.SetActive(false);  // Hide all buttons
                    button.interactable = false;  // Disable interaction initially
                }
                foreach (var item in draggableItems)
                {
                    var draggableComponent = item.GetComponent<DraggableItem>();
                    draggableComponent.SetDraggable(true);  // Ensure all items are draggable again
                    draggableComponent.ResetPosition();
                    item.SetActive(false);  // Hide all draggable items
                }
                break;
        }

        // Start the coroutine to show the dialogue after 1 second, followed by the appropriate mechanic elements
        StartCoroutine(ShowDialogueWithDelay(levelData.dialogueText, levelData));
    }
    //public void DisplayLevel(LevelData levelData)
    //{
    //    // Set the scenario image
    //    if (levelData.scenarioImage != null)
    //    {
    //        scenarioImage.sprite = levelData.scenarioImage;
    //        Debug.Log("Scenario image set successfully.");
    //    }
    //    else
    //    {
    //        Debug.LogError("Scenario image not found!");
    //    }


    //        foreach (var button in wordButtons)
    //        {
    //            button.gameObject.SetActive(false);  // Hide all buttons
    //            button.interactable = false;  // Disable interaction initially
    //        }


    //        foreach (var item in draggableItems)
    //        {
    //            var draggableComponent = item.GetComponent<DraggableItem>();
    //            draggableComponent.SetDraggable(true);  // Ensure all items are draggable again
    //            draggableComponent.ResetPosition();
    //            item.SetActive(false);  // Hide all draggable items
    //        }


    //    // Hide and disable draggable items initially


    //    // Start the coroutine to show the dialogue after 1 second, followed by the draggable items
    //    StartCoroutine(ShowDialogueWithDelay(levelData.dialogueText, levelData));
    //}



    // Coroutine to display the dialogue after 1 second, followed by draggable items
    private IEnumerator ShowDialogueWithDelay(string dialogue, LevelData levelData)
    {
        // Wait for 1 second before showing the dialogue panel
        yield return new WaitForSeconds(1f);

        // Show the dialogue if it's not empty
        if (!string.IsNullOrEmpty(dialogue))
        {
            dialoguePanel.SetActive(true);  // Show the panel
            dialogueText.text = dialogue;  // Set the dialogue text

            yield return new WaitForSeconds(4f);  // Wait for 4 seconds to show dialogue

            dialoguePanel.SetActive(false);  // Hide the panel
        }

        switch (levelData.mechanicType)
        {
            case MechanicType.Click:
                StartCoroutine(ShowButtonsWithDelay(levelData));
                break;
            case MechanicType.Drag:
                StartCoroutine(ShowDraggableItemsWithDelay(levelData));
                break;
        }


    }

    // Coroutine to delay showing the draggable items by 1 second after the dialogue
    private IEnumerator ShowDraggableItemsWithDelay(LevelData levelData)
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second after dialogue panel is hidden

        dropSlot.gameObject.SetActive(true);
        for (int i = 0; i < draggableItems.Length; i++)
        {
            if (i < levelData.words.Count)
            {
                // Activate the draggable item
                GameObject draggableItem = draggableItems[i];
                draggableItem.SetActive(true);

                // Set the word text
                TextMeshProUGUI wordText = draggableItem.GetComponentInChildren<TextMeshProUGUI>();
                wordText.text = levelData.words[i];

                // Set the word icon image
                Image wordIcon = draggableItem.transform.Find("WordIcon").GetComponent<Image>();
                Sprite wordSprite = Resources.Load<Sprite>($"Images/{levelData.words[i]}");

                if (wordSprite != null)
                {
                    wordIcon.sprite = wordSprite;  // Assign the image to the icon
                }
                else
                {
                    Debug.LogError($"Image for word '{levelData.words[i]}' not found in Resources.");
                    wordIcon.sprite = null;
                }

                // Set the item index for drag-drop identification
                DraggableItemIdentifier draggableItemIdentifier = draggableItem.GetComponent<DraggableItemIdentifier>();
                draggableItemIdentifier.itemIndex = i;

                // Make the drop slot visible
                dropSlot.SetActive(true);
            }
            else
            {
                // Hide unused draggable items
                draggableItems[i].SetActive(false);
            }
        }
    }




    // Coroutine to delay showing the buttons by 1 second after the dialogue
    private IEnumerator ShowButtonsWithDelay(LevelData levelData)
    {
        yield return new WaitForSeconds(1f); // Wait for 1 second after dialogue panel is hidden

        // Loop through the word buttons and update their text and icons
        for (int i = 0; i < wordButtons.Length; i++)
        {
            if (i < levelData.words.Count)
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
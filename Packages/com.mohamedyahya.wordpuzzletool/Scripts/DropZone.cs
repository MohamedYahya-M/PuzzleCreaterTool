
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    private LevelManager levelManager;

    private void Start()
    {
        // Initialize LevelManager to access the current level data
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        DraggableItem droppedItem = eventData.pointerDrag.GetComponent<DraggableItem>();

        if (droppedItem != null)
        {
            PuzzleController puzzleController = FindObjectOfType<PuzzleController>();
            puzzleController.OnItemDrop(this, droppedItem);  // Notify PuzzleController that an item was dropped
        }
    }

    public bool IsDroppedCorrectly(DraggableItem draggableItem)
    {
        // Compare the item's index with the correct index from the level data
        DraggableItemIdentifier identifier = draggableItem.GetComponent<DraggableItemIdentifier>();
        if (identifier != null)
        {
            // Check if the dragged item's index matches the correct index for the current level
            return identifier.itemIndex == levelManager.currentLevelData.correctWordIndex;
        }

        return false; // Return false if the identifier is not found
    }

}

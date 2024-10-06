using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
   // public int itemIndex;  // Assign this based on the level data for each item

    private CanvasGroup canvasGroup;
    private Vector3 originalPosition;
    private PuzzleController puzzleController;

    private void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        puzzleController = FindObjectOfType<PuzzleController>();
        originalPosition = transform.position;  // Save original position
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        puzzleController.OnItemDragStart(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        puzzleController.OnDragCancel();  // Reset if no valid drop occurred
    }

    public void ResetPosition()
    {
        canvasGroup.blocksRaycasts = true;
        transform.position = originalPosition;  // Reset to original position
    }

    public void SetDraggable(bool isDraggable)
    {
        canvasGroup.interactable = isDraggable;
    }
   

}

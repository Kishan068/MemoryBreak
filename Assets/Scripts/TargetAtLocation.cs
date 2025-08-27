using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TargetAtLocation : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private bool isDragging = false; // Flag to track if the target is being dragged.
    public RectTransform targetRectTransform; // Reference to the target's RectTransform.
    public int myLocationNumber = 0;          
    public TextMeshProUGUI targetNameText; // Reference to the target's name text field.
    public VirtualTarget virtualTarget;
    private void Awake()
    {
        targetRectTransform = GetComponent<RectTransform>();
        
    }

   //update my location number
    public void SetLocationNumber(int locationNumber)
    {
        myLocationNumber = locationNumber;
        targetNameText.text = myLocationNumber.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // When the target is clicked, set the dragging flag.
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            // Get the mouse position in world space.
            Vector3 worldMousePosition;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(
                targetRectTransform, eventData.position, eventData.pressEventCamera, out worldMousePosition);

            // Update the target's position.
            targetRectTransform.position = worldMousePosition;

        }
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        // Reset the dragging flag when the mouse button is released.
        isDragging = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        // Check if the collision object is a virtual target.
        
        if (other.GetComponent<VirtualTarget>() != null)
        {
            // Update the virtual target's player name to the station's name.
            virtualTarget = other.GetComponent<VirtualTarget>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the collision object is a virtual target.
        
        if (other.GetComponent<VirtualTarget>() != null )
        {
            // Update the virtual target's player name to "0" (outside any station).
            virtualTarget = null;
        }
    }
}

using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;   
public class VirtualTarget : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public int laneNumber; // The lane number assigned to this target.
    public TextMeshProUGUI targetNameText; // Reference to the target's name text field.
    public TextMeshProUGUI playerNumberText; // Reference to the player number text field.
    public string targetName; // The name of the target.
    public string playerName; // The player number assigned to this target.


    public RectTransform targetRectTransform; // Reference to the target's RectTransform.
    private Canvas canvas; // Reference to the canvas the target is on.
    private bool isDragging = false; // Flag to track if the target is being dragged.

    public GameObject VirtualPrefab;
    public GameObject myTagetAvatar;

    private bool isStartedWhileActive = false;

    public int rowNumber;
    public int columnNumber;
    private void Awake()
    {
        targetRectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    // Assign a player number to this target and update the player number text field.
    public void SetPlayerName(string PlayerName)
    {
        playerName = PlayerName;
        // Update the player number text field.
        playerNumberText.text = playerName;
    }

    // Set the target's name and update the target name text field.
    public void SetTargetName(string name)
    {
        // Set the target's name.
        targetNameText.text = name;
        targetName = name;
    }

    // Return the player number assigned to this target.
    public string GetPlayerName()
    {
        return playerName;
    }

    //get target name
    public string GetTargetName()
    {
        return targetName;
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

            //// Create a ray from the mouse position.
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;

            //// Perform a raycast to check for collisions with station panels.
            //if (Physics.Raycast(ray, out hit))
            //{
            //    Debug.Log(hit.collider);
            //    // Check if the hit object is a station panel.
            //    StationManager stationPanel = hit.collider.GetComponent<StationManager>();
            //    if (stationPanel != null)
            //    {
            //        // Update the virtual target's player name to match the station panel's name.
            //        SetPlayerName(stationPanel.gameObject.name);
            //    }
            //    else
            //    {
            //        // No station panel was hit, so update the player name to "0".
            //        SetPlayerName("0");
            //    }
            //}
            //else
            //{
            //    // No collision detected, update the player name to "0".
            //    SetPlayerName("0");
            //}
        }
    }

  
    public void OnPointerUp(PointerEventData eventData)
    {
        // Reset the dragging flag when the mouse button is released.
        isDragging = false;
    }

    public void StartTarget(Vector3 spawnLocation,GameObject spawnObjectPrefab)
    {
        Debug.Log("starting virtual target");
        VirtualPrefab = spawnObjectPrefab;
        if (myTagetAvatar != null)
        {
            Debug.Log("Stopping Target during start" + targetNameText.text);
            Destroy(myTagetAvatar);
        }
            //Convert the screenpoint location of the 
            myTagetAvatar = Instantiate(VirtualPrefab, spawnLocation, Quaternion.identity);
            VirtualPrefab.SetActive(true);
            Debug.Log("starting virtual avatar");
            myTagetAvatar.GetComponent<DroneTarget>().StartDrone(this.transform.position);
        
    }

   public void StopTarget()
    {
        if(myTagetAvatar != null)
        {
            Debug.Log("Stopping Target" + targetNameText.text);
            myTagetAvatar.GetComponent<DroneTarget>().StopDrone();

            myTagetAvatar = null;
        }
    }

    public void HitTarget()
    {
        if (myTagetAvatar != null)
        {
            myTagetAvatar.GetComponent<DroneTarget>().HitDrone();

            myTagetAvatar = null;
        }
    }
    
}

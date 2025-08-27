using Mechanics;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    #region Singleton
    public static UI_Manager Instance;
    #endregion

    #region UI References
    [Header("Core UI Containers")]

    public GameObject Grid;
    public List<GameObject> ObjectsToToggle = new List<GameObject>();




    [Header("Zone Leaderboard")]
    public GameObject ZoneLeaderboardListItem;
    public GameObject ZoneLeaderboardListContainer;

    [Header("Zone Current Game Score")]
    public GameObject ZoneCurrentGameScoreItem;
    public GameObject ZoneCurrentGameContainer;

    #endregion

    #region State & Collections
    public int stationsCount = 1;
    public int LanesCount = 1;
    public bool isUIVisible = true;



    /// <summary>
    /// A list of all instantiated station GameObjects.
    /// </summary>
    public List<GameObject> stationsList = new List<GameObject>();

    /// <summary>
    /// A list of all instantiated lane GameObjects, configured in the Inspector.
    /// </summary>
    public List<GameObject> LanesList = new List<GameObject>();

    /// <summary>
    /// A list of instantiated UI items for the zone-wide leaderboard.
    /// </summary>
    public List<GameObject> ZoneLeaderboardListItems = new List<GameObject>();

    /// <summary>
    /// A list of instantiated UI items for the current zone game's score display.
    /// </summary>
    public List<GameObject> ZoneCurrentGameItems = new List<GameObject>();
    #endregion

    #region Public References
    public LaneManager laneManager;
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        // Standard robust Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        InitializeLanes();
    }

    private void Start()
    {
        StartCoroutine(WaitAndToggleUI());
    }

    private void Update()
    {
        // Debug hotkey to toggle UI visibility.
        if (Input.GetKeyDown(KeyCode.T))
        {
            toggleUI();
        }

        // Debug hotkey to toggle the alignment grid.
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (Grid != null)
            {
                Grid.SetActive(!Grid.activeSelf);
            }
        }
    }
    #endregion

    #region Initialization & Instantiation

    /// <summary>
    /// Initializes the lanes by populating the LaneManager from the pre-configured list.
    /// </summary>
    public void InitializeLanes()
    {
        InitializeLanesFromList();
    }

    /// <summary>
    /// Populates the LaneManager's dictionary from the Inspector-assigned LanesList.
    /// </summary>
    public void InitializeLanesFromList()
    {
        foreach (GameObject laneGO in LanesList)
        {
            if (laneGO.TryGetComponent<Lane>(out var laneComponent))
            {
                laneManager.Lanes.Add(laneComponent.LaneId, laneComponent);
            }
        }
    }

   
  
    #endregion

    #region UI Control & Visibility

    /// <summary>
    /// Toggles the visibility of the primary user interface elements.
    /// </summary>
    public void toggleUI()
    {
        isUIVisible = !isUIVisible;

        // Toggle visibility for all registered objects
        foreach (GameObject go in ObjectsToToggle)
        {
            go.SetActive(isUIVisible);
        }

        // Toggle station-specific UI and targets
        if (isUIVisible)
        {

            TargetManager.Instance.showTargets();
        }
        else
        {

            TargetManager.Instance.hideTargets();
        }
    }

 
    #endregion

    #region Zone UI Management

    /// <summary>
    /// Adds a new item to the zone-wide leaderboard UI.
    /// </summary>
    /// <param name="playerTag">The player's tag to display.</param>
    /// <param name="position">The rank/position to display.</param>
    /// <param name="score">The score to display.</param>
    public void addZoneLeaderboardItem(string playerTag, string position, string score)
    {
        GameObject itemGO = Instantiate(ZoneLeaderboardListItem, ZoneLeaderboardListContainer.transform);
        var itemComponent = itemGO.GetComponent<leaderboardItem>();

        Player player = Players.Instance.players.Find(x => x.PlayerTag == playerTag);
        Sprite playerImage = (player != null)
            ? player.PlayerImage
            : UserImageManager.Instance.GetPlayerImage("");

        itemComponent.SetLeaderboardItem(position, playerTag, score, playerImage);
        ZoneLeaderboardListItems.Add(itemGO);
    }

    /// <summary>
    /// Adds a new item to the current game's score list UI.
    /// </summary>
    /// <param name="playerTag">The player's tag to display.</param>
    /// <param name="position">The rank/position (can be empty).</param>
    /// <param name="score">The score to display.</param>
    public void addZoneCurrentGameScoreItem(string playerTag, string position, string score)
    {
        GameObject itemGO = Instantiate(ZoneCurrentGameScoreItem, ZoneCurrentGameContainer.transform);
        var itemComponent = itemGO.GetComponent<leaderboardItem>();

        Player player = Players.Instance.players.Find(x => x.PlayerTag == playerTag);
        Sprite playerImage = (player != null)
            ? player.PlayerImage
            : UserImageManager.Instance.GetPlayerImage("");

        itemComponent.SetLeaderboardItem(position, playerTag, score, playerImage);

        // Find the player's lane and display it.
        ParticipatingPlayer pPlayer = Zone.instance.GameScreenParticipantMetrics.Find(x => x.playerName.text == playerTag);
        if (pPlayer != null)
        {
            itemComponent.SetLaneName("Lane " + pPlayer.laneNumber.ToString());
        }

        ZoneCurrentGameItems.Add(itemGO);
    }

    /// <summary>
    /// Sorts the current game score list based on the score format (time or points).
    /// </summary>
    public void SortGameboardList()
    {
        if (ZoneCurrentGameItems.Count == 0) return;

        Debug.Log("Sorting gameboard list...");

        // Sorts ascending for time (e.g., "01:234") and descending for points.
        if (ZoneCurrentGameItems[0].GetComponent<leaderboardItem>().playerScore.text.Contains(":"))
        {
            ZoneCurrentGameItems.Sort((a, b) => a.GetComponent<leaderboardItem>().playerScore.text.CompareTo(b.GetComponent<leaderboardItem>().playerScore.text));
        }
        else
        {
            ZoneCurrentGameItems.Sort((a, b) => int.Parse(b.GetComponent<leaderboardItem>().playerScore.text).CompareTo(int.Parse(a.GetComponent<leaderboardItem>().playerScore.text)));
        }

        // Update position numbers and re-order the UI elements
        for (int i = 0; i < ZoneCurrentGameItems.Count; i++)
        {
            ZoneCurrentGameItems[i].GetComponent<leaderboardItem>().playerPosition.text = (i + 1).ToString();
            // This ensures the visual order matches the sorted list order in a VerticalLayoutGroup.
            ZoneCurrentGameItems[i].transform.SetAsLastSibling();
        }
    }

    /// <summary>
    /// Clears all items from the zone leaderboard and current game score lists.
    /// </summary>
    public void clearZoneLeaderboardItems()
    {
        foreach (GameObject item in ZoneLeaderboardListItems)
        {
            Destroy(item);
        }
        ZoneLeaderboardListItems.Clear();

        foreach (GameObject item in ZoneCurrentGameItems)
        {
            Destroy(item);
        }
        ZoneCurrentGameItems.Clear();

       
    }
    #endregion

    #region Coroutines
    private IEnumerator WaitAndToggleUI()
    {
        yield return new WaitForSeconds(0f);
        toggleUI();
        yield return new WaitForSeconds(2f);
        TargetManager.Instance.loadTargets();
    }

  
    #endregion
}
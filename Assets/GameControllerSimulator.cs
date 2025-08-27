using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;

public class GameControllerSimulator : MonoBehaviour
{
    // 5 lanes, each with 9 targets (3x3 grid)
    public List<VirtualTarget> Lane1Targets = new List<VirtualTarget>();
    public List<VirtualTarget> Lane2Targets = new List<VirtualTarget>();
    public List<VirtualTarget> Lane3Targets = new List<VirtualTarget>();
    public List<VirtualTarget> Lane4Targets = new List<VirtualTarget>();
    public List<VirtualTarget> Lane5Targets = new List<VirtualTarget>();

    public TMPro.TMP_Dropdown GameDropdown; // Dropdown to select the game type

    public Button LaunchGameButton; // Button to launch the game

    public Button StartButton; // Button to start the Targets

    public Button StopButton; // Button to stop the Targets and the game



    // Track which background enemy/game is running per lane
    private Dictionary<int, int> laneGameId = new Dictionary<int, int>();
    // Track which targets are active per lane
    private Dictionary<int, HashSet<string>> laneActiveTargets = new Dictionary<int, HashSet<string>>();
    // Track running coroutines for WhackAMole per lane
    private Dictionary<int, Coroutine> whackAMoleCoroutines = new Dictionary<int, Coroutine>();

    void Start()
    {
        // Initialize dictionaries
        for (int lane = 1; lane <= 5; lane++)
        {
            laneActiveTargets[lane] = new HashSet<string>();
            laneGameId[lane] = 0;
        }

        // Setup UI listeners
        GameDropdown.onValueChanged.AddListener(OnGameSelected);
        LaunchGameButton.onClick.AddListener(OnLaunchGameClicked);
        StartButton.onClick.AddListener(OnStartButtonClicked);
        StopButton.onClick.AddListener(OnStopButtonClicked);

    }

    // Handle game selection from dropdown
    private void OnGameSelected(int index)
    {
        // You can handle game selection logic here if needed
        Debug.Log("Selected game index: " + index);
        
    }

    // Handle launch game button click
    private void OnLaunchGameClicked()
    {
        int selectedGameIndex = GameDropdown.value;
        Debug.Log("Launching game with index: " + selectedGameIndex);
        // You can add logic to launch the game based on the selected index
        // Start the game for each lane based on the selected index
      
            switch (selectedGameIndex)
            {
                case 0: // Clear the Field
                    StartClearTheField(1);
                    break;
                case 2: // Whack a Mole
                    StartWhackAMole(1);
                    break;
                default:
                    Debug.LogWarning("Unknown game index: " + selectedGameIndex);
                    break;
            }
       
    }

    // Handle start button click
    private void OnStartButtonClicked()
    {
        int selectedGameIndex = GameDropdown.value;
        Debug.Log("Starting targets for game with index: " + selectedGameIndex);

        // Start targets for  lane 1 based on the selected game
        switch (selectedGameIndex)
        {
            case 0: // Clear the Field
                StartTargetsForClearTheField(1);
                break;
            case 2: // Whack a Mole
                StartTargetsForWhackAMole(1);
                break;
            default:
                Debug.LogWarning("Unknown game index: " + selectedGameIndex);
                break;
        }
    }

    private void OnStopButtonClicked()
    {
        Debug.Log("Stopping all targets and games");
        // Stop all targets and games for each lane
       
            EndGameForLane(1);
      
    }

    // --- CLEAR THE FIELD ---

    public void StartClearTheField(int lane)
    {
        int gameId = 1; // Example: 1 for "Clear the Field"
        StartBackgroundEnemyForLane(lane, gameId);
       // StartTargetsForClearTheField(lane);
    }

    public void StartBackgroundEnemyForLane(int lane, int gameId)
    {
        TargetManager.Instance.StartBackgroundEnemy(lane, gameId);
        laneGameId[lane] = gameId;
    }

    public void StartTargetsForClearTheField(int lane)
    {
        List<VirtualTarget> targets = GetLaneTargets(lane);
        if (targets == null || targets.Count == 0) return;

        foreach (var vt in targets)
        {
            TargetManager.Instance.StartTargetNew(lane, vt.targetName);
            laneActiveTargets[lane].Add(vt.targetName);
        }
    }

    // --- WHACK A MOLE ---

    public void StartWhackAMole(int lane)
    {
        int gameId = 4; // Example: 4 for "Whack a Mole"
        StartBackgroundEnemyForLane(lane, gameId);
       // StartTargetsForWhackAMole(lane);
    }

    public void StartTargetsForWhackAMole(int lane)
    {
        List<VirtualTarget> targets = GetLaneTargets(lane);
        if (targets == null || targets.Count == 0) return;

        // Start coroutine for whack-a-mole logic
        if (whackAMoleCoroutines.ContainsKey(lane) && whackAMoleCoroutines[lane] != null)
        {
            StopCoroutine(whackAMoleCoroutines[lane]);
        }
        whackAMoleCoroutines[lane] = StartCoroutine(WhackAMoleRoutine(lane, targets));
    }

    private IEnumerator WhackAMoleRoutine(int lane, List<VirtualTarget> targets)
    {
        var rnd = new System.Random();
        int whacks = 0;
        int maxWhacks = 10; // Number of targets to activate before ending game

        while (whacks < maxWhacks)
        {
            // Pick a random target that is not currently active
            List<VirtualTarget> inactiveTargets = new List<VirtualTarget>();
            foreach (var vt in targets)
            {
                if (!laneActiveTargets[lane].Contains(vt.targetName))
                    inactiveTargets.Add(vt);
            }
            if (inactiveTargets.Count == 0)
            {
                // All targets are active, wait and try again
                yield return new WaitForSeconds(0.5f);
                continue;
            }
            int idx = rnd.Next(inactiveTargets.Count);
            var target = inactiveTargets[idx];

            // Activate the target
            TargetManager.Instance.StartTargetNew(lane, target.targetName);
            laneActiveTargets[lane].Add(target.targetName);

            // Wait for 1 second or until the target is stopped/hit
            float timer = 0f;
            float duration = 1.0f;
            while (timer < duration && laneActiveTargets[lane].Contains(target.targetName))
            {
                timer += Time.deltaTime;
                yield return null;
            }

            // Deactivate the target if still active
            if (laneActiveTargets[lane].Contains(target.targetName))
            {
                TargetManager.Instance.StopTargetNew(lane, target.targetName);
                laneActiveTargets[lane].Remove(target.targetName);
            }

            whacks++;
            yield return new WaitForSeconds(0.3f);
        }

        // Game over: stop all targets and background enemy
        EndGameForLane(lane);
    }

    // --- COMMONS ---

    // Manual stop/hit
    public void StopTarget(VirtualTarget target)
    {
        int lane = GetLaneOfTarget(target);
        if (lane == -1) return;

        TargetManager.Instance.StopTargetNew(lane, target.targetName);
        laneActiveTargets[lane].Remove(target.targetName);
    }

    // End game for a lane: stop all targets and background enemy
    public void EndGameForLane(int lane)
    {
        TargetManager.Instance.StopLaneTargets(lane);
        int gameId = laneGameId.ContainsKey(lane) ? laneGameId[lane] : 0;
        if (gameId != 0)
        {
            TargetManager.Instance.EndBackgroundEnemy(lane, gameId);
        }
        laneActiveTargets[lane].Clear();
        laneGameId[lane] = 0;
        if (whackAMoleCoroutines.ContainsKey(lane) && whackAMoleCoroutines[lane] != null)
        {
            StopCoroutine(whackAMoleCoroutines[lane]);
            whackAMoleCoroutines[lane] = null;
        }
    }

    // Helper: get targets list for a lane
    private List<VirtualTarget> GetLaneTargets(int lane)
    {
        switch (lane)
        {
            case 1: return Lane1Targets;
            case 2: return Lane2Targets;
            case 3: return Lane3Targets;
            case 4: return Lane4Targets;
            case 5: return Lane5Targets;
            default: return null;
        }
    }

    // Helper: find which lane a target belongs to
    private int GetLaneOfTarget(VirtualTarget target)
    {
        if (Lane1Targets.Contains(target)) return 1;
        if (Lane2Targets.Contains(target)) return 2;
        if (Lane3Targets.Contains(target)) return 3;
        if (Lane4Targets.Contains(target)) return 4;
        if (Lane5Targets.Contains(target)) return 5;
        return -1;
    }
}
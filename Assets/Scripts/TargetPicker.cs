using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPicker : MonoBehaviour
{
    public static TargetPicker instance;

    //awake
    private void Awake()
    {
        instance = this;
    }

    public Dictionary<int, List<Transform>> AdvancedMechRobotsLocations1 = new Dictionary<int, List<Transform>>();
    public Dictionary<int, List<Transform>> AdvancedMechRobotsLocations2 = new Dictionary<int, List<Transform>>();

    public Dictionary<int, List<Transform>> QuadLeg1 = new Dictionary<int, List<Transform>>();
    public Dictionary<int, List<Transform>> QuadLeg2 = new Dictionary<int, List<Transform>>();
    public Dictionary<int, List<Transform>> QuadLeg3 = new Dictionary<int, List<Transform>>();
    public Dictionary<int, List<Transform>> QuadLeg4 = new Dictionary<int, List<Transform>>();

    public Dictionary<int, List<Transform>> RandomGroundTargets = new Dictionary<int, List<Transform>>();
    public Dictionary<int, List<Transform>> RandomAirTargets = new Dictionary<int, List<Transform>>();

    public List<Transform> AllPlayersAdvancedMechRobotsLocations1 = new List<Transform>();
    public List<Transform> AllPlayersAdvancedMechRobotsLocations2 = new List<Transform>();

    public List<Transform> AllPlayersQuadLeg1 = new List<Transform>();
    public List<Transform> AllPlayersQuadLeg2 = new List<Transform>();
    public List<Transform> AllPlayersQuadLeg3 = new List<Transform>();
    public List<Transform> AllPlayersQuadLeg4 = new List<Transform>();

    public List<Transform> AllPlayersRandomGroundTargets = new List<Transform>();

    public List<Transform> AllPlayersRandomAirTargets = new List<Transform>();

   
}

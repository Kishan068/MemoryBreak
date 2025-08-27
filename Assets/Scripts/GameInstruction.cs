using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInstruction : MonoBehaviour
{
    public string type;
    public string selectionType;
    public string activationType;
    public List<string> elementIds;
    public List<string> targetLocations;
    public int cycle;
    public int hitTimeout;
    public int internalCycle;
}

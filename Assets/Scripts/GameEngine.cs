using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
   //This script will handle activation and deactivation of targets, and progression of instructions of a game.

    //singleton
    public static GameEngine instance;

    //awake
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void TriggerInstruction(int stationNumber, int gameId, int instructionNumber, string selectionType)
    {
        JObject data = new JObject();
        JArray instructions = new JArray();
        JObject instruction = new JObject();
        instruction.Add("id", 1);
        instruction.Add("type", GameInstructions.instance.InstructionsForGame[gameId][instructionNumber].type);
        JArray ElementIDs = new JArray();
        if (GameInstructions.instance.InstructionsForGame[gameId][instructionNumber].targetLocations.Count < 0)
        {
            return;
        }
        List<string> elementsAtLocation = TargetSelection.instance.GetMultipleSpecificTargets(stationNumber, GameInstructions.instance.InstructionsForGame[gameId][instructionNumber].targetLocations);

        if (selectionType == "TARGET_LOCATIONS")
        {
            foreach (string element in elementsAtLocation)
            {
                ElementIDs.Add(element);
                Debug.Log("added element ids" + element);
                //Start target on screen
                TargetManager.Instance.StartTarget(element, 1);
            }
        }
        else if (selectionType == "ELEMENT_IDS")
        {
            string element = TargetSelection.instance.GetSingleRandomTarget(stationNumber);
            ElementIDs.Add(element);
            TargetManager.Instance.StartTarget(element, 1);
        }
           

      
        instruction.Add("elementIds", ElementIDs);
            
        instruction.Add("cycle", GameInstructions.instance.InstructionsForGame[gameId][instructionNumber].cycle);
        instruction.Add("hitTimeout", GameInstructions.instance.InstructionsForGame[gameId][instructionNumber].hitTimeout);
        instruction.Add("totalTargets", GameInstructions.instance.InstructionsForGame[gameId][instructionNumber].targetLocations.Count);
        instructions.Add(instruction);
        //SocketCommunicator.Instance.SendGameInstruction("Station "+ stationNumber, instructions);

        
    }

}

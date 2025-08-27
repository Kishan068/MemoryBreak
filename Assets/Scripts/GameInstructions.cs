using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameInstructions :MonoBehaviour
{
    //In this script, we will define all the instructions for each game.
    //We will also maintain the current instruction, and the current target for the respective station.

    public static GameInstructions instance;
   //awake
   void Awake()
    {
        instance = this;
    }

    public int gameId;
    public Dictionary<int, List<GameInstruction>> InstructionsForGame = new Dictionary<int, List<GameInstruction>>();

    //on start, we will load all the instructions for each game ID.

    void Start()
    {
       // Debug.Log("gameId: " + gameId + " adding instruction");
        gameId = 1;

        //load instructions for game 2
        List<GameInstruction> instructions = new List<GameInstruction>();

        //instruction 1
        GameInstruction instruction = new GameInstruction();
        instruction.type = "ACTIVATE_SPECIFIC_TARGETS";
        instruction.selectionType = "TARGET_LOCATIONS";
        instruction.activationType = "AUTO";
        instruction.elementIds = new List<string>();
        instruction.elementIds.Add("STEEL_3");
       
        instruction.targetLocations = new List<string>();
        instruction.targetLocations.Add("1");
        instruction.cycle = 1;
        instruction.internalCycle = 40;
        instruction.hitTimeout = 30000;
        instructions.Add(instruction);
        InstructionsForGame.Add(gameId, instructions);

        gameId = 2;

        //load instructions for game 2
        instructions = new List<GameInstruction>();

        //instruction 1
        instruction = new GameInstruction();
        instruction.type = "ACTIVATE_SPECIFIC_TARGETS";
        instruction.selectionType = "TARGET_LOCATIONS";
        instruction.activationType = "AUTO";
        instruction.elementIds = new List<string>();
        instruction.elementIds.Add("1");
        instruction.elementIds.Add("2");
        instruction.targetLocations = new List<string>();
        instruction.targetLocations.Add("1");
        instruction.targetLocations.Add("2");   
        instruction.cycle = 1;
        instruction.hitTimeout = 30000;
        instructions.Add(instruction);

        //instruction 2
        instruction = new GameInstruction();
        instruction.type = "ACTIVATE_SPECIFIC_TARGETS";
        instruction.selectionType = "TARGET_LOCATIONS";
        instruction.activationType = "AUTO";
        instruction.elementIds = new List<string>();
        instruction.elementIds.Add("1");
        instruction.elementIds.Add("2");
        instruction.targetLocations = new List<string>();
        instruction.targetLocations.Add("3");
       
        instruction.cycle = 1;
        instruction.hitTimeout = 30000;
        instructions.Add(instruction);
        //Debug.Log("gameId: " + gameId + " adding instruction2");
        InstructionsForGame.Add(gameId, instructions);
       // Debug.Log("gameId: " + gameId + " instructions loaded");




        gameId = 3;

        //load instructions for game 3
        instructions = new List<GameInstruction>();

        //instruction 1
        instruction = new GameInstruction();
        instruction.type = "ACTIVATE_SPECIFIC_TARGETS";
        instruction.selectionType = "TARGET_LOCATIONS";
        instruction.activationType = "AUTO";
        instruction.elementIds = new List<string>();
        instruction.elementIds.Add("1");
        instruction.elementIds.Add("2");
        instruction.targetLocations = new List<string>();
        instruction.targetLocations.Add("1");
    
        instruction.cycle = 1;
        instruction.hitTimeout = 30000;
        instructions.Add(instruction);

        //instruction 2
        instruction = new GameInstruction();
        instruction.type = "ACTIVATE_SPECIFIC_TARGETS";
        instruction.selectionType = "TARGET_LOCATIONS";
        instruction.activationType = "AUTO";
        instruction.elementIds = new List<string>();
       
        instruction.elementIds.Add("2");
        instruction.targetLocations = new List<string>();
        instruction.targetLocations.Add("2");

        instruction.cycle = 1;
        instruction.hitTimeout = 30000;
        instructions.Add(instruction);

        //instruction 3
        instruction = new GameInstruction();
        instruction.type = "ACTIVATE_SPECIFIC_TARGETS";
        instruction.selectionType = "TARGET_LOCATIONS";
        instruction.activationType = "AUTO";
        instruction.elementIds = new List<string>();

        instruction.elementIds.Add("2");
        instruction.targetLocations = new List<string>();
        instruction.targetLocations.Add("3");
        
        instruction.cycle = 1;
        instruction.hitTimeout = 30000;
        instructions.Add(instruction);

        //instruction 4
        instruction = new GameInstruction();
        instruction.type = "ACTIVATE_SPECIFIC_TARGETS";
        instruction.selectionType = "TARGET_LOCATIONS";
        instruction.activationType = "AUTO";
        instruction.elementIds = new List<string>();

        instruction.elementIds.Add("2");
        instruction.targetLocations = new List<string>();
        instruction.targetLocations.Add("1");
        instruction.targetLocations.Add("2");

        instruction.cycle = 1;
        instruction.hitTimeout = 30000;
        instructions.Add(instruction);

        //instruction 5
        instruction = new GameInstruction();
        instruction.type = "ACTIVATE_SPECIFIC_TARGETS";
        instruction.selectionType = "TARGET_LOCATIONS";
        instruction.activationType = "AUTO";
        instruction.elementIds = new List<string>();

        instruction.elementIds.Add("2");
        instruction.targetLocations = new List<string>();
        instruction.targetLocations.Add("1");
        instruction.targetLocations.Add("2");
        instruction.targetLocations.Add("3");
        instruction.cycle = 1;
        instruction.hitTimeout = 30000;
        instructions.Add(instruction);
        //Debug.Log("gameId: " + gameId + " adding instruction2");
        InstructionsForGame.Add(gameId, instructions);
        //Debug.Log("gameId: " + gameId + " instructions loaded");
    }

}

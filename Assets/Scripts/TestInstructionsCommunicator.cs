using Firesplash.GameDevAssets.SocketIOPlus;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestInstructionsCommunicator : MonoBehaviour
{
    public SocketIOClient io;
    public TextMeshProUGUI uiStatus;
    public int instructionId;
    //singleton
    public static TestInstructionsCommunicator Instance;

    //Awake
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        io.D.On("connect", () =>
        {
            Debug.Log("Established socket connection with Game Controller!");
            RegisterClient();
        });

        //The internal event connect_error is fired, when the server rejects our authentication (or something else propagates an error on the server side).
        //The payload of the connect_error event is always a JObject, it represents the JS "Error" Object created on the server side.
        //You could also cast to a custom structure here. We're using the JObject to showcase this quite universal variant.
        io.D.On<JObject>("connect_error", (jsErrorObject) =>
        {
            Debug.Log("LOCAL: We received an error from the server: " + jsErrorObject.GetValue("message"));
            uiStatus.text = "Error: " + jsErrorObject.GetValue("message");
        });


        //When the conversation is done, the server will close our connection after 4 seconds
        io.D.On("disconnect", (reason) =>
        {
            uiStatus.text = "Finished: " + reason;
        });

        //Listen for incoming messages
        io.D.On<JObject>("CLIENT_EVENT", (payload) =>
        {
            Debug.Log("Received message from Game Controller: " + payload);

        });

        //Listen for target messages
        io.D.On<JObject>("TARGET", (payload) =>
        {
            Debug.Log("Received target message from Game Controller: " + payload);
            GameEventHandler.Instance.HandleTargetEvents(payload);
        });

        //We are now ready to actually connect
        //The simple way will use the parameters set in the inspector (or with a former call to Connect(...)):
        io.Connect(GlobalConfigs.Instance.GameControllerURL + ":" + GlobalConfigs.Instance.socketIoPort);

    }

    public void startGame(string gameState)
    {
        //Create a payload for the game event
        JObject payload = new JObject();
        //add the tag
        payload.Add("tag", GlobalConfigs.Instance.tag);
        //add the state
        payload.Add("command", "");
        payload.Add("zoneId", 1);
        payload.Add("gameState", gameState);
        payload.Add("gameMode", "LANE");
        //add playerPayloads
        JArray playerPayloads = new JArray();
        JObject playerPayload = new JObject();
        playerPayload.Add("playerId", 2);
        //add lanePayloads
        JArray lanePayloads = new JArray();
        JObject lanePayload = new JObject();
        lanePayload.Add("laneId", 1);
        //add activeStationIds
        JArray activeStationIds = new JArray();
        activeStationIds.Add(1);
        lanePayload.Add("activeStationIds", activeStationIds);
        lanePayloads.Add(lanePayload);
        playerPayload.Add("lanePayloads", lanePayloads);
       
        playerPayloads.Add(playerPayload);
        payload.Add("playerPayloads", playerPayloads);
        //Add Objective Payload
        JObject objectivePayload = new JObject();
        objectivePayload.Add("gameId", 1);
        payload.Add("objectivePayload", objectivePayload);

        //send the game event
        Debug.Log("Sending game event to Game Controller: " + payload);
        io.D.Emit<JObject>("CONTROLLER_EVENT", payload);
        
    }

    public void SetObjective(string gameState)
    {
        //Create a payload for the game event
        JObject payload = new JObject();
        //add the tag
        payload.Add("tag", GlobalConfigs.Instance.tag);
        //add the state
        payload.Add("command", "");
        payload.Add("zoneId", 1);
        payload.Add("gameState", gameState);
        payload.Add("gameMode", "LANE");
        //add playerPayloads
        JArray playerPayloads = new JArray();
        JObject playerPayload = new JObject();
        playerPayload.Add("playerId", 3);
        //add lanePayloads
        JArray lanePayloads = new JArray();
        JObject lanePayload = new JObject();
        lanePayload.Add("laneId", 1);
        //add activeStationIds
        JArray activeStationIds = new JArray();
        activeStationIds.Add(1);
        lanePayload.Add("activeStationIds", activeStationIds);
        lanePayloads.Add(lanePayload);
        playerPayload.Add("lanePayloads", lanePayloads);
        //add instructions
       
        playerPayloads.Add(playerPayload);
        payload.Add("playerPayloads", playerPayloads);
        //Add Objective Payload
        JObject objectivePayload = new JObject();
        objectivePayload.Add("gameId", 1);
        payload.Add("objectivePayload", objectivePayload);

        //send the game event
        Debug.Log("Sending game event to Game Controller: " + payload);
        io.D.Emit<JObject>("CONTROLLER_EVENT", payload);

    }

    public void SendGameState(string gameState)
    {
        //Create a payload for the game event
        JObject payload = new JObject();
        //add the tag
        payload.Add("tag", GlobalConfigs.Instance.tag);
        //add the state
        payload.Add("command", "");
        payload.Add("zoneId", 1);
        payload.Add("gameState", gameState);
        payload.Add("gameMode", "LANE");
        //add playerPayloads
        JArray playerPayloads = new JArray();
        JObject playerPayload = new JObject();
        playerPayload.Add("playerId", 2);
        //add lanePayloads
        JArray lanePayloads = new JArray();
        JObject lanePayload = new JObject();
        lanePayload.Add("laneId", 1);
        //add activeStationIds
        JArray activeStationIds = new JArray();
        activeStationIds.Add(1);
        lanePayload.Add("activeStationIds", activeStationIds);
        lanePayloads.Add(lanePayload);
        playerPayload.Add("lanePayloads", lanePayloads);

        JArray instructions = new JArray();
        JObject instruction = new JObject();
        instruction.Add("id", 1);
        instruction.Add("type", "ACTIVATE_RANDOM_TARGETS");
        instruction.Add("cycle", 10);
        instructions.Add(instruction);

        playerPayload.Add("instructions", instructions);

        playerPayloads.Add(playerPayload);
        //JObject playerPayload2 = new JObject();
        //playerPayload2.Add("playerId", 3);
        ////add lanePayloads
        //JArray lanePayloads2 = new JArray();
        //JObject lanePayload2 = new JObject();
        //lanePayload2.Add("laneId", 1);
        ////add activeStationIds
        //JArray activeStationIds2 = new JArray();

        //lanePayload2.Add("activeStationIds", activeStationIds2);
        //lanePayloads2.Add(lanePayload2);
        //playerPayload2.Add("lanePayloads", lanePayloads2);


        //playerPayloads.Add(playerPayload2);
        payload.Add("playerPayloads", playerPayloads);

        //Add Objective Payload

       
      

        JObject objectivePayload = new JObject();
        objectivePayload.Add("gameId", 1);
        payload.Add("objectivePayload", objectivePayload);

        //send the game event
        Debug.Log("Sending game event to Game Controller: " + payload);
        io.D.Emit<JObject>("CONTROLLER_EVENT", payload);

    }

    //Set Zone Mode objective
    public void SetZoneObjective(string gameState)
    {
        //Create a payload for the game event

        JObject payload = new JObject();
        //add the tag
        payload.Add("tag", GlobalConfigs.Instance.tag);
        //add the state
        payload.Add("command", "");
        payload.Add("zoneId", 2);
        payload.Add("gameState", gameState);
        payload.Add("gameMode", "ZONE");
        //add playerPayloads
        JArray playerPayloads = new JArray();
        JObject playerPayload = new JObject();
        playerPayload.Add("playerId", 2);
        //add lanePayloads
        JArray lanePayloads = new JArray();
        JObject lanePayload = new JObject();
        lanePayload.Add("laneId", 1);
        //add activeStationIds
        JArray activeStationIds = new JArray();
        activeStationIds.Add(1);
        lanePayload.Add("activeStationIds", activeStationIds);
        lanePayloads.Add(lanePayload);
        playerPayload.Add("lanePayloads", lanePayloads);
        //add instructions
        JArray instructions = new JArray();
        JObject instruction = new JObject();
        instruction.Add("id", instructionId);
        instruction.Add("type", "ACTIVATE_RANDOM_TARGETS");
        instruction.Add("cycle", 1);
        instruction.Add("hitTimeout", 30000);
        instruction.Add("totalTargets", 1);
        instructions.Add(instruction);
        playerPayload.Add("instructions", instructions);

        playerPayloads.Add(playerPayload);
        //JObject playerPayload2 = new JObject();
        //playerPayload2.Add("playerId", 3);
        ////add lanePayloads
        //JArray lanePayloads2 = new JArray();
        //JObject lanePayload2 = new JObject();
        //lanePayload2.Add("laneId", 1);
        ////add activeStationIds
        //JArray activeStationIds2 = new JArray();
        
        //lanePayload2.Add("activeStationIds", activeStationIds2);
        //lanePayloads2.Add(lanePayload2);
        //playerPayload2.Add("lanePayloads", lanePayloads2);

      
        //playerPayloads.Add(playerPayload2);
        payload.Add("playerPayloads", playerPayloads);
        //Add Objective Payload
        JObject objectivePayload = new JObject();
        objectivePayload.Add("gameId", 1);
        payload.Add("objectivePayload", objectivePayload);

        //send the game event
        Debug.Log("Sending game event to Game Controller: " + payload);
        io.D.Emit<JObject>("CONTROLLER_EVENT", payload);

    }




    // Register Client function to be called on successful connection
    public void RegisterClient()
    {
        JObject payload = new JObject();
        payload.Add("tag", "Test");
        payload.Add("command", "REGISTER");
        payload.Add("zoneId", 1);
        payload.Add("gameState", "IDLE");
        payload.Add("gameMode", "LANE");
        payload.Add("playerPayloads", "");
        payload.Add("leaderboard", "");
        payload.Add("instructions", "");
        payload.Add("objectivePayload", "");
        payload.Add("gameResults", "");
        io.D.Emit<JObject>("CONTROLLER_EVENT", payload);
    }


}

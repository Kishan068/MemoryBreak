using Firesplash.GameDevAssets.SocketIOPlus;
using Newtonsoft.Json.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SocketCommunicator : MonoBehaviour
{
    public SocketIOClient io;
    public TextMeshProUGUI uiStatus;

    //singleton
    public static SocketCommunicator Instance;

    //Awake
    void Awake()
    {
   Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        io.D.On("connect", () =>
        {
            Debug.Log("Established socket connection with Game Controller!");
            uiStatus.text = "Socket.IO Connected.";
            RegisterClient();
            //send http request to gamecontroller to get available games
            StartCoroutine(HTTPCommunicator.Instance.Get(GlobalConfigs.Instance.GameControllerURL + ":" + GlobalConfigs.Instance.httpPort, "/api/v1/gamecatalog"));
            //StartCoroutine(HTTPCommunicator.Instance.GetPlayers(GlobalConfigs.Instance.GameControllerURL + ":" + GlobalConfigs.Instance.httpPort + "/api/v1/players"));
            //set the game state to idle
            //TO DO
            Debug.Log("Registered client with Game Controller!");
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

            GameEventHandler.Instance.HandleReply(payload);
        });

        //io.D.On("NEW_PLAYER", (payload) =>
        //{

        //    StartCoroutine(HTTPCommunicator.Instance.GetPlayers(GlobalConfigs.Instance.GameControllerURL + ":" + GlobalConfigs.Instance.httpPort + "/api/v1/players"));
        //});

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

    // Register Client function to be called on successful connection
    public void RegisterClient()
    {
        JObject payload = new JObject();
        payload.Add("tag", GlobalConfigs.Instance.tag);
        payload.Add("command", "REGISTER");
        JObject data = new JObject();
        JArray stationsIds = new JArray();
        
        payload.Add("stationIds", stationsIds);
        payload.Add("zoneId", 1);
        
        data.Add("targetColor", "color");
        payload.Add("data", data);
        io.D.Emit<JObject>("CONSOLE", payload);
    }

    // Send Game Event function to Game Controller
    public void SendGameEvent(string tag, string state, JObject data)
    {
        //create a new game event
        JObject gameEvent = new JObject();
        //add the tag
        gameEvent.Add("tag", tag);
        //add the state
        gameEvent.Add("command", state);
        //add the data
        data.Add("station", tag);
        gameEvent.Add("data", data);


        //send the game event
        Debug.Log("Sending game event to Game Controller: " + gameEvent);
        io.D.Emit<JObject>("GAME", gameEvent);
    }

    public void SendSimulatedHit(string targetID)
    {

        JObject hitEvent = new JObject();
        hitEvent.Add("deviceId", targetID);
        io.D.Emit<JObject>("TARGET", hitEvent);
    }


    public void SendGameInstruction(string tag,  JObject data)
    {
        ////create a new game Instruction event
        //JObject gameInstructionEvent = new JObject();
        ////add the tag
        //gameInstructionEvent.Add("tag", tag);
        //gameInstructionEvent.Add("command", "GAME_INSTRUCTION");
        //gameInstructionEvent.Add("instructions", data);

        //send the game event
        Debug.Log("Sending game Instruction event to Game Controller: " + data );   
        io.D.Emit<JObject>("CONTROLLER_EVENT", data);

    }

    public void SendClearTargetDisplays()
    {
        //create a new game Instruction event
        JObject gameInstructionEvent = new JObject();
        //add the tag
        gameInstructionEvent.Add("tag", "MULTI_STATION");
        gameInstructionEvent.Add("command", "TEST_ALL_TARGETS");
            JObject testTargetsPayload = new JObject();
            testTargetsPayload.Add("targetDecimalColor", "0");
        gameInstructionEvent.Add("testAllTargetsPayload", testTargetsPayload);
       

        //send the game event
        
        io.D.Emit<JObject>("GAME", gameInstructionEvent);

    }

    public void SendStartGameCommand(JObject Data)
    {
        JObject payload = new JObject();
        payload = Data;
        payload["gameState"] = "GAME_START";
        //Debug.Log("Sending start game command" + Data);
        io.D.Emit<JObject>("CONTROLLER_EVENT", payload);
    }

    public void SendGameOverCommand(JObject Data)
    {
        JObject payload = new JObject();
        payload = Data;
        payload["gameState"] = "GAME_CANCELLED";
        //Debug.Log("Sending game over command" + Data);
        io.D.Emit<JObject>("CONTROLLER_EVENT", payload);
    }

    //close
    private void OnDestroy()
    {
        io.Disconnect();
    }
}
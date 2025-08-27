using JetBrains.Annotations;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TestInstructions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GameObject testTargetSpawnObject;

    public JObject PayloadBeforeGameStart = new JObject();
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            


            JObject data = new JObject();
          
            data.Add("command", null);
            data.Add("tag", "ALL_STATIONS");
            data.Add("zoneId", 1);
            data.Add("gameState", "GAME_RUNNING");
            data.Add("gameMode", "ZONE");
            JArray playerPayloads = new JArray();
            JObject playerPayload = new JObject();
            playerPayload.Add("playerId", 300001);
            JArray lanePayloads = new JArray();
            JObject lanePayload = new JObject();
            lanePayload.Add("laneId", 1);
            JArray activeStationIds = new JArray();
            activeStationIds.Add(1);
            lanePayload.Add("activeStationIds", activeStationIds);
            lanePayloads.Add(lanePayload);
            playerPayload.Add("lanePayloads", lanePayloads);



            JArray instructions = new JArray();
            JObject instruction = new JObject();
            instruction.Add("id", 1);
            instruction.Add("type", "ACTIVATE_SPECIFIC_TARGETS");
            JArray ElementIDs = new JArray();
            ElementIDs.Add("b4:a9:b7:f2:3a:08-SS");

            instruction.Add("deviceIds", ElementIDs);
            instruction.Add("cycle", 1);
            instruction.Add("hitTimeout", 1000);
            instructions.Add(instruction);
            playerPayload.Add("instructions", instructions);

            playerPayloads.Add(playerPayload);
            data.Add("playerPayloads", playerPayloads);
            SocketCommunicator.Instance.SendGameInstruction("ALL_STATIONS", data);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {



            JObject data = new JObject();

            data.Add("command", null);
            data.Add("tag", "ALL_STATIONS");
            data.Add("zoneId", 1);
            data.Add("gameState", "GAME_RUNNING");
            data.Add("gameMode", "ZONE");
            JArray playerPayloads = new JArray();
            JObject playerPayload = new JObject();
            playerPayload.Add("playerId", 300001);
            JArray lanePayloads = new JArray();
            JObject lanePayload = new JObject();
            lanePayload.Add("laneId", 1);
            JArray activeStationIds = new JArray();
            activeStationIds.Add(1);
            lanePayload.Add("activeStationIds", activeStationIds);
            lanePayloads.Add(lanePayload);
            playerPayload.Add("lanePayloads", lanePayloads);



            JArray instructions = new JArray();
            JObject instruction = new JObject();
            instruction.Add("id", 1);
            instruction.Add("type", "ACTIVATE_SPECIFIC_TARGETS");
            JArray ElementIDs = new JArray();
            
            ElementIDs.Add("bc:1d:b7:f2:3a:08-SS");

            instruction.Add("deviceIds", ElementIDs);
            instruction.Add("cycle", 1);
            instructions.Add(instruction);
            playerPayload.Add("instructions", instructions);

            playerPayloads.Add(playerPayload);
            data.Add("playerPayloads", playerPayloads);
            SocketCommunicator.Instance.SendGameInstruction("ALL_STATIONS", data);
        }



        
        if (Input.GetKeyDown(KeyCode.Y))
        {
            TargetManager.Instance.StopTargets();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            TargetManager.Instance.HitTarget("STEEL_3");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            TargetSelection.instance.GetMultipleSpecificTargets(3, new List<string> { "1", "2" });
        }

    }

//Payload is a JSON object that contains the following fields:
//    {
//  "command": null,
//  "gameState": "OBJECTIVE",
//  "tag": "ALL_STATIONS",
//  "zoneId": 1,
//  "gameMode": "ZONE",
//  "playerPayloads": [
//    {
//      "playerId": 1,
//      "lanePayloads": [
//        {
//          "laneId": 1,
//          "activeStationIds": []
//},
//        {
//    "laneId": 2,
//          "activeStationIds": []
//        },
//        {
//    "laneId": 3,
//          "activeStationIds": []
//        },
//        {
//    "laneId": 4,
//          "activeStationIds": []
//        }
//      ]
//    },
//    {
//    "playerId": 2,
//      "lanePayloads": [
//        {
//        "laneId": 1,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 2,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 3,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 4,
//          "activeStationIds": []
//        }
//      ]
//    },
//    {
//    "playerId": 3,
//      "lanePayloads": [
//        {
//        "laneId": 1,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 2,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 3,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 4,
//          "activeStationIds": []
//        }
//      ]
//    },
//    {
//    "playerId": 4,
//      "lanePayloads": [
//        {
//        "laneId": 1,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 2,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 3,
//          "activeStationIds": [
//            1
//          ]
//        },
//        {
//        "laneId": 4,
//          "activeStationIds": []
//        }
//      ]
//    },
//    {
//    "playerId": 5,
//      "lanePayloads": [
//        {
//        "laneId": 1,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 2,
//          "activeStationIds": [
//            1
//          ]
//        },
//        {
//        "laneId": 3,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 4,
//          "activeStationIds": []
//        }
//      ]
//    },
//    {
//    "playerId": 6,
//      "lanePayloads": [
//        {
//        "laneId": 1,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 2,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 3,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 4,
//          "activeStationIds": []
//        }
//      ]
//    },
//    {
//    "playerId": 7,
//      "lanePayloads": [
//        {
//        "laneId": 1,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 2,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 3,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 4,
//          "activeStationIds": []
//        }
//      ]
//    },
//    {
//    "playerId": 8,
//      "lanePayloads": [
//        {
//        "laneId": 1,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 2,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 3,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 4,
//          "activeStationIds": []
//        }
//      ]
//    },
//    {
//    "playerId": 9,
//      "lanePayloads": [
//        {
//        "laneId": 1,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 2,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 3,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 4,
//          "activeStationIds": []
//        }
//      ]
//    },
//    {
//    "playerId": 10,
//      "lanePayloads": [
//        {
//        "laneId": 1,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 2,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 3,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 4,
//          "activeStationIds": [
//            1
//          ]
//        }
//      ]
//    },
//    {
//    "playerId": 11,
//      "lanePayloads": [
//        {
//        "laneId": 1,
//          "activeStationIds": [
//            1
//          ]
//        },
//        {
//        "laneId": 2,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 3,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 4,
//          "activeStationIds": []
//        }
//      ]
//    },
//    {
//    "playerId": 12,
//      "lanePayloads": [
//        {
//        "laneId": 1,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 2,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 3,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 4,
//          "activeStationIds": []
//        }
//      ]
//    },
//    {
//    "playerId": 13,
//      "lanePayloads": [
//        {
//        "laneId": 1,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 2,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 3,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 4,
//          "activeStationIds": []
//        }
//      ]
//    },
//    {
//    "playerId": 14,
//      "lanePayloads": [
//        {
//        "laneId": 1,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 2,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 3,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 4,
//          "activeStationIds": []
//        }
//      ]
//    },
//    {
//    "playerId": 15,
//      "lanePayloads": [
//        {
//        "laneId": 1,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 2,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 3,
//          "activeStationIds": []
//        },
//        {
//        "laneId": 4,
//          "activeStationIds": []
//        }
//      ]
//    }
//  ],
//  "gameResults": [],
//  "leaderboard": [],
//  "objectivePayload": {
//    "gameId": 1
//  }
//}


    public void UpdatePayload(JObject payload)
    {
        PayloadBeforeGameStart = payload;
    }


    public void AddSingleInstructionPerPlayer(int playerId, List<string> targetIds)
    {

    }

    public void AddGroupInstructions(List<int> playerIds, Dictionary<int, List<string>> targetIds)
    {

    }
   

    public void OnDemandTarget(JArray targets, float UpTime)
    {

    }
}

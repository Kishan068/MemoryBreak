using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDemandTargetStarter : MonoBehaviour
{

    //singleton
    public static OnDemandTargetStarter Instance;

    private void Awake()
    {
        Instance = this;
    }

    public JObject PayloadBeforeGameStart = new JObject();

    //Sample PayloadBeforeGameStart is
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


    public void AddGroupInstructions(Dictionary<int, List<string>> LanesAndTargetIdsDictionary)
    {
        //In the Dictionary, the key is the lane number and the value is the list of target ids

        /* Example instruction format:
         *  playerPayloads: [
                {
                  playerId: 102,
                  lanePayloads: [
                    {
                      laneId: 1,
                      activeStationIds: [1]
                    }
                  ],
                  instructions: [
                    {
                      id: 1,
                      type: ACTIVATE_ALL_TARGETS,
                      cycle: 1
                    }
                  ]
                }
      ]

        */

        //parse the payload before game start
        JObject Payload = PayloadBeforeGameStart;
        JArray playerPayloads = (JArray)Payload["playerPayloads"];
        //If the laneId in lanePayloads matches the key in the dictionary, create instructions for that playerPayload with the target ids in the value of the dictionary
        foreach (KeyValuePair<int, List<string>> lane in LanesAndTargetIdsDictionary)
        {
            foreach (JObject playerPayload in playerPayloads)
            {
                JArray lanePayloads = (JArray)playerPayload["lanePayloads"];
                foreach (JObject lanePayload in lanePayloads)
                {
                    if (lanePayload["laneId"].ToString() == lane.Key.ToString())
                    {
                        JArray instructions = new JArray();
                        JObject instruction = new JObject();
                        instruction.Add("id", 1);
                        instruction.Add("type", "ACTIVATE_SPECIFIC_TARGETS");
                        JArray ElementIDs = new JArray();
                        foreach (string targetId in lane.Value)
                        {
                            ElementIDs.Add(targetId);
                        }
                        instruction.Add("deviceIds", ElementIDs);
                        instruction.Add("cycle", 1);
                        instructions.Add(instruction);
                        playerPayload.Add("instructions", instructions);
                    }
                }
            }
        }
        Payload["playerPayloads"] = playerPayloads;

        ////Sample Instruction code:
        //JArray instructions = new JArray();
        //JObject instruction = new JObject();
        //instruction.Add("id", 1);
        //instruction.Add("type", "ACTIVATE_SPECIFIC_TARGETS");
        //JArray ElementIDs = new JArray();
        ////ElementIDs.Add([TargetIds from LanesAndTargetIds Dictionary]);
        //foreach (KeyValuePair<int, List<string>> lane in LanesAndTargetIdsDictionary)
        //{
        //    foreach (string targetId in lane.Value)
        //    {
        //        ElementIDs.Add(targetId);
        //    }
        //}
       

        //instruction.Add("deviceIds", ElementIDs);
        //instruction.Add("cycle", 1);
        //instructions.Add(instruction);

        //For each lane in the dictionary, create a new instruction with 

    }

    public void ClearAndAddInstructions(Dictionary<int, List<string>> LanesAndTargetIdsDictionary, float timeOut)
     {
        //parse the payload before game start
        JObject Payload = PayloadBeforeGameStart;
        JArray playerPayloads = (JArray)Payload["playerPayloads"];
        foreach (JObject playerPayload in playerPayloads)
        {
            //Clear the instructions for each playerPayload
            JArray instructions = new JArray();
            //if instructions exist, then clear it
            if (playerPayload["instructions"] != null)
            {
                playerPayload["instructions"] = instructions;
            }

        }

            //If the laneId in lanePayloads matches the key in the dictionary, create instructions for that playerPayload with the target ids in the value of the dictionary
            foreach (KeyValuePair<int, List<string>> lane in LanesAndTargetIdsDictionary)
        {
            foreach (JObject playerPayload in playerPayloads)
            {
                JArray lanePayloads = (JArray)playerPayload["lanePayloads"];

                foreach (JObject lanePayload in lanePayloads)
                {
                    if (lanePayload["laneId"].ToString() == lane.Key.ToString())
                    {
                        //Check if the lane has any activeStationIds
                        if (lanePayload["activeStationIds"] != null)
                        {
                            JArray activeStationIds = (JArray)lanePayload["activeStationIds"];
                            //If the lane has activeStationIds, then add the target ids to the instructions
                            if (activeStationIds.Count > 0)
                            {
                                JArray instructions = new JArray();
                                JObject instruction = new JObject();
                                instruction.Add("id", 1);
                                instruction.Add("type", "ACTIVATE_SPECIFIC_TARGETS");
                                JArray ElementIDs = new JArray();
                                foreach (string targetId in lane.Value)
                                {
                                   ElementIDs.Add(targetId);
                                    // ElementIDs.Add("bc:1d:b7:f2:3a:08-SS");
                                }
                                instruction.Add("deviceIds", ElementIDs);
                                instruction.Add("cycle", 1);
                                //convert timeOut to milliseconds and remove decimal points
                                timeOut = timeOut * 1000;
                                timeOut = (int)timeOut;
                                
                                instruction.Add("hitTimeout", timeOut);
                                instructions.Add(instruction);
                                playerPayload["instructions"] = instructions;
                            }
                        }
                    }
                }
            }
        }
        Payload["playerPayloads"] = playerPayloads;
        Debug.Log("OnDemandTargetStarter: " + Payload.ToString());  
        SocketCommunicator.Instance.SendGameInstruction("ALL_STATIONS", Payload);
    }

    public void ClearAndDeactivateTargets(List<int> Lanes)
    {
        JObject Payload = PayloadBeforeGameStart;
        JArray playerPayloads = (JArray)Payload["playerPayloads"];
        foreach (JObject playerPayload in playerPayloads)
        {
            //Clear the instructions for each playerPayload
            JArray instructions = new JArray();
            //if instructions exist, then clear it
            if (playerPayload["instructions"] != null)
            {
                playerPayload["instructions"] = instructions;
            }

        }

        //If the laneId in lanePayloads matches the key in the dictionary, create instructions for that playerPayload with the target ids in the value of the dictionary
        foreach (int lane in Lanes)
        {
            foreach (JObject playerPayload in playerPayloads)
            {
                JArray lanePayloads = (JArray)playerPayload["lanePayloads"];

                foreach (JObject lanePayload in lanePayloads)
                {
                    if (lanePayload["laneId"].ToString() == lane.ToString())
                    {
                        //Check if the lane has any activeStationIds
                        if (lanePayload["activeStationIds"] != null)
                        {
                            JArray activeStationIds = (JArray)lanePayload["activeStationIds"];
                            //If the lane has activeStationIds, then add the target ids to the instructions
                            if (activeStationIds.Count > 0)
                            {
                                JArray instructions = new JArray();
                                JObject instruction = new JObject();
                                instruction.Add("id", 1);
                                instruction.Add("type", "CLEAR_ALL_TARGETS");
                                instructions.Add(instruction);
                                playerPayload["instructions"] = instructions;
                            }
                        }
                    }
                }
            }
        }
        Payload["playerPayloads"] = playerPayloads;
        Debug.Log("OnDemandTargetStarter: " + Payload.ToString());
        SocketCommunicator.Instance.SendGameInstruction("ALL_STATIONS", Payload);
    }

    public void EndGame()
    {
        PayloadBeforeGameStart["gameState"] = "GAME_CANCELLED";
        SocketCommunicator.Instance.SendGameInstruction("ALL_STATIONS", PayloadBeforeGameStart);
    }
}

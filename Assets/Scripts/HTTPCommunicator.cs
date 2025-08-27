using System.Collections;
using UnityEngine;
using SimpleHTTP;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;
using Mechanics;
using System.Collections.Generic;

public class HTTPCommunicator : MonoBehaviour
{
    public static HTTPCommunicator Instance;

    //singleton
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("HTTP_Communicator already exists!");
        }
    }

    // Get request
    public IEnumerator Get(string baseUrl, string req)
    {

        string uri = baseUrl + req;
       // Debug.Log(uri);
        Request request = new Request("http://" + uri);

        Client http = new Client();
        yield return http.Send(request);
        if (http.IsSuccessful())
        {
            Response resp = http.Response();
            //Debug.Log("status: " + resp.Status().ToString() + "\nbody: " + resp.Body());
            var json = resp.Body();
            //Debug.Log(json);
            // For every array element in the json, add it to the list of games.
            JArray games = JArray.Parse(resp.Body());
            //Debug.Log(games);
            //Debug.Log(games[0]["title"]);
            // add each item in the array to the list of games
            for (int i = 0; i < games.Count; i++)
            {
                //Debug.Log(games[i]);
                Game gameDefinition = new Game();
                gameDefinition.id = (int)games[i]["id"];
                gameDefinition.title = (string)games[i]["title"];
                gameDefinition.description = (string)games[i]["description"];
                gameDefinition.image = (string)games[i]["image"];
                gameDefinition.goal = (string)games[i]["goal"];
                gameDefinition.score = (string)games[i]["score"];
                gameDefinition.penalty = (string)games[i]["penalty"];
                gameDefinition.timeout = (int)games[i]["timeout"];

                // Match the game type to the enum
                if ((string)games[i]["type"] == "TIME_BASED")
                {
                    gameDefinition.type = GameDefinition.GameType.TIME_BASED;
                }
                else
                {
                    gameDefinition.type = GameDefinition.GameType.HIT_BASED;
                }
                // Store the targets required as a string
                gameDefinition.targetsRequired = (string)games[i]["targetsRequired"].ToString();
                // Store the instructions as a string
                gameDefinition.instructions = (string)games[i]["instructions"].ToString();
                gameDefinition.category = (string)games[i]["type"];
                //Add the game to the list of games
                Games.Instance.AddGame(gameDefinition);

            }

        }
        else
        {
            Debug.Log("error: " + http.Error());
        }
    }


    // Get the list of players from Mothership.
    public IEnumerator GetPlayers(string fullUrl)
    {
       
        string uri = fullUrl;

        Request request = new Request("http://" + uri);
        //Debug.Log("Getting players images");
        Client http = new Client();
        yield return http.Send(request);
        if (http.IsSuccessful())
        {
            Response resp = http.Response();
            //Debug.Log("status: " + resp.Status().ToString() + "\nbody: " + resp.Body());
           
            var json = resp.Body();
            // Debug.Log(json);

            //Replace content of Reurces/DemoPlayers  with the json response.
            //System.IO.File.WriteAllText(Application.dataPath + "/Resources/DemoPlayers.json", json);

            // For every array element in the json, add it to the list of games.

            //JArray players = JArray.Parse(resp.Body());
            JToken players = JToken.Parse(resp.Body())["data"];
            //Debug.Log("Getting players images" + players);
            //for every player, send a request to get the player image. Save the image under Resources/UserImages
            foreach (JToken player in players)
            {
                if (!Players.Instance.IsPlayerAvailable(player["id"].ToString()))
                {

                    Players.Instance.AddPlayer(player["id"].ToString(), player["gamerTag"].ToString(), player["firstName"].ToString(), player["lastName"].ToString(), "dummyImageURL");
                }
                else
                {
                    Players.Instance.UpdatePlayer(player["id"].ToString(), player["gamerTag"].ToString(), player["firstName"].ToString(), player["lastName"].ToString(), "dummyImageURL");
                }

            }

            //for every player, send a request to get the player image. Save the image under Resources/UserImages
            foreach (JToken player in players)
            {
                //Check if the player in player manager with the player id player["id"].ToString() has the imageURL same as the player["imageUrl"].ToString()
                //If it does not, get the player image
                //If it does, do nothing

                Player savedPlayer = Players.Instance.players.Find(x => x.PlayerID == player["id"].ToString());

                if (!Players.Instance.IsPlayerImageAvailable(player["id"].ToString()))
                {

                    StartCoroutine(GetPlayerImage(player["imageUrl"].ToString(), player["id"].ToString()));

                }
                else if (savedPlayer.ImageURL != player["imageUrl"].ToString())
                {
                    StartCoroutine(GetPlayerImage(player["imageUrl"].ToString(), player["id"].ToString()));

                }
                else
                {
                    //Debug.Log("Player image already available");
                }
                Players.Instance.players.Find(x => x.PlayerID == player["id"].ToString()).ImageURL = player["imageUrl"].ToString();
            }


            //Debug.Log(players);

        }
        else
        {
            Debug.Log("error: " + http.Error());
           
        }
    }

    public IEnumerator GetPlayerImage(string imageUrl, string playerId)
    {
        //Debug.Log("Getting player image");
        string uri = imageUrl;
      
        //string uri = "buffer.com/cdn-cgi/image/w=1000,fit=contain,q=90,f=auto/library/content/images/size/w600/2023/10/free-images.jpg";
        //This url returns an image. Save this image under Resources/Images with the name imageUrl
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(uri);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
        {
            Debug.Log(request.error);
           
        }

        else
        {
            //Save the image under Resources/UserImages
            // byte[] bytes = ((DownloadHandlerTexture)request.downloadHandler).texture.EncodeToPNG();
            //Save as a Sprite in the Resources folder

            //System.IO.File.WriteAllBytes(Application.dataPath + "/Resources/UserImages/" + PlayerTag + ".png", bytes);

            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());
            //if (!UserImageManager.Instance.userImages.ContainsKey(playerId))
            //{
                //Debug.Log("Added image to dictionary" + playerId);
                //UserImageManager.Instance.userImages.Add(playerId, sprite);
                //If a player card is already created for this player, update the image
               
            //}
            //find the player in Players whose id is playerId and update the image
            //foreach (Player player in Players.Instance.players)
            //{
            //    if (player.PlayerID == playerId)
            //    {
            //        player.PlayerImage = sprite;
            //    }
            //}
            //In every lane, call the updatePlayerImage function to update the image
         
            Players.Instance.UpdatePlayerImage(playerId, sprite);
            ////Create an entry in the dictionary with the player tag and the image
            //Sprite playerImage = Resources.Load<Sprite>("UserImages/" + imageUrl);
            //Debug.Log("player image url is" + imageUrl);
            //UserImageManager.Instance.userImages.Add(imageUrl, playerImage);
            //UserImageManager.Instance.playerImage = playerImage;
            //UserImageMa   nager.Instance.testImage .sprite = playerImage;

        }



    }
    public void TestDemoJSON()
    {

       
        TextAsset jsonFile = Resources.Load<TextAsset>("PlayersJSON");
        JArray players = JArray.Parse(jsonFile.text);
        for (int i = 0; i < players.Count; i++)
        {
            Debug.Log(players[i]["firstName"]);
            StartCoroutine(GetPlayerImage(players[i]["imageUrl"].ToString(), players[i]["gamerTag"].ToString()));
        }
       

    }
    public void refreshPlayers()
    {
        //TestDemoJSON();
        StartCoroutine(GetPlayers(GlobalConfigs.Instance.GameControllerURL + ":" + GlobalConfigs.Instance.httpPort + "/api/v1/players"));

    }
}

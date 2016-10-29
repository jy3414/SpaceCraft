using UnityEngine;
using System.Collections;
using Photon;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using System.Net;
using System.Collections.Specialized;
using System.Text;
//using UnityEditor;

public class RandomMatchmaker : Photon.PunBehaviour
{
	public float respawnTimer = 0;
    public float respawnTimerAI = 0;
    public float respawnTimerAI1 = 0;
    public float respawnTimerAI2 = 0;
    public float respawnTimerAI3 = 0;
    public float respawnTimerAI4 = 0;
    public string[] top = {"Top1", "Top2", "Top3", "Top4", "Top5"};
    public int[] topScore = {2000, 1500, 1000, 500, 200};
    private float change = 4.5f;

    public Camera mainCamera;
	private GameObject recoveryRegion;
	public bool createRecoveryRegion = false;
	private float fireRegionTimer = 20f;
	private float snowRegionTimer = 30f;
	private float erekiPowerTimer = 1f;
	private float firePowerTimer = 1f;
	private float invisPowerTimer = 1f;
	private float normalPowerTimer = 1f;
	private float starTimer = 0.5f;
    private float leaderBoardTimer = 5f;
    private float max = 100.0f;
	private float min = -100.0f;
    public int NumberOfPlayerInLeaderboard = 5;
	public Vector2 playerPos;

    private bool showNickNameText = true;
    private bool showPlayAsGuest = true;
    private bool showPlay = false;
    private bool showLogin = false;
    private bool showSignup = false;
    private bool showUserNameEmptyWarning = false;
    private bool showPasswordEmptyWarning = false;
    private bool showPasswordNotEqualWarning = false;
    private bool showPasswordNotCorrectWarning = false;
    private bool showFeedback = false;
    private bool showFeedbackFailureWarning = false;
    private bool showFeedbackSuccessfulMessage = false;

    private string username = "";
    private string password = "";
    private string confirmedPassword = "";
    private string feedback = "Please enter your feedback to our game here.";

    private string signUpURL = "http://www.spacewar.online/signUp.php";
    private string loginURL = "http://www.spacewar.online/login.php";
    private string feedbackURL = "http://www.spacewar.online/feedback.php";

    private GUIStyle headerStyle = new GUIStyle();
    private GUIStyle labelStyle = new GUIStyle();
    private GUIStyle warningStyle = new GUIStyle();

    private PhotonPlayer AI1 = new PhotonPlayer(false, 0, "hi1");
    private PhotonPlayer AI2 = new PhotonPlayer(false, 0, "hi2");
    private PhotonPlayer AI3 = new PhotonPlayer(false, 0, "hi3");
    private PhotonPlayer AI4 = new PhotonPlayer(false, 0, "hi4");
    private PhotonPlayer AI5 = new PhotonPlayer(false, 0, "hi5");

    

    // Update is called once per frame
    void Update () {

		if (PhotonNetwork.inRoom) {
            if (respawnTimer > 0) {
				respawnTimer -= Time.deltaTime;
				if (respawnTimer <= 0) {
					respawnPlayer ();
				}
			}

            if (respawnTimerAI > 0)
            {
                respawnTimerAI -= Time.deltaTime;
                if (respawnTimerAI <= 0)
                {
                    respawnPlayerAI(0);
                }
            }

            if (respawnTimerAI1 > 0)
            {
                respawnTimerAI1 -= Time.deltaTime;
                if (respawnTimerAI1 <= 0)
                {
                    respawnPlayerAI(1);
                }
            }

            if (respawnTimerAI2 > 0)
            {
                respawnTimerAI2 -= Time.deltaTime;
                if (respawnTimerAI2 <= 0)
                {
                    respawnPlayerAI(2);
                }
            }

            if (respawnTimerAI3 > 0)
            {
                respawnTimerAI3 -= Time.deltaTime;
                if (respawnTimerAI3 <= 0)
                {
                    respawnPlayerAI(3);
                }
            }

            if (respawnTimerAI4 > 0)
            {
                respawnTimerAI4 -= Time.deltaTime;
                if (respawnTimerAI4 <= 0)
                {
                    respawnPlayerAI(4);
                }
            }

            if (leaderBoardTimer > 0)
            {
                leaderBoardTimer -= Time.deltaTime;
                if (leaderBoardTimer <= 0)
                {
                    AI1.AddScore(10);
                    leaderBoardTimer = 5f;
                }
            }

/*            if (change <= 0)
            {

                change = 4.5f;
            } else
            {
                change -= Time.deltaTime;
            }
*/

            if (recoveryRegion == null && createRecoveryRegion) {
				Vector3 spawnPoint = new Vector3 (Random.Range (min, max), Random.Range (min, max), 0);
				recoveryRegion = PhotonNetwork.Instantiate ("RecoveryRegion",
					spawnPoint, Quaternion.identity, 0);
			}
			
			starTimer = createRegion (starTimer, "Star");
			if (starTimer <= 0) {
				starTimer = 0.5f;
			}

			erekiPowerTimer = createRegion (erekiPowerTimer, "Ereki Power");
			if (erekiPowerTimer <= 0) {
				erekiPowerTimer = 20f;
			}

			firePowerTimer = createRegion (firePowerTimer, "Fire Power");
			if (firePowerTimer <= 0) {
				firePowerTimer = 20f;
			}

			invisPowerTimer = createRegion (invisPowerTimer, "Invisible Power");
			if (invisPowerTimer <= 0) {
				invisPowerTimer = 20f;
			}

			normalPowerTimer = createRegion (normalPowerTimer, "Normal Power");
			if (normalPowerTimer <= 0) {
				normalPowerTimer = 20f;
			}
		}

	}

	private float createRegion(float time, string obj) {
		if (time > 0) {
			time -= Time.deltaTime;
			if (time <= 0) {
				Vector3 spawnPoint = new Vector3 (Random.Range (min, max), Random.Range (min, max), 0);
				GameObject fireRegion = PhotonNetwork.Instantiate(obj,
					spawnPoint, Quaternion.identity, 0);
			}
		}
		return time;
	}

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");

        AI1.SetScore(500);
        AI2.SetScore(200);
        AI3.SetScore(50);
        AI4.SetScore(10);
        AI5.SetScore(5);

    }

    void OnGUI()
    {

        //Set up the text style
        GUI.color = Color.cyan;

        headerStyle.fontSize = 25;
        headerStyle.fontStyle = FontStyle.Bold;
        headerStyle.normal.textColor = Color.cyan;
        headerStyle.alignment = TextAnchor.UpperCenter;

        labelStyle.fontSize = 15;
        labelStyle.normal.textColor = Color.cyan;
        labelStyle.alignment = TextAnchor.UpperCenter;

        warningStyle.normal.textColor = Color.yellow;
        warningStyle.alignment = TextAnchor.UpperCenter;


        if (PhotonNetwork.insideLobby == true)
        {

            GUI.Box(new Rect(Screen.width / 3.0f, Screen.height / 3f, Screen.width / 3.0f, Screen.height / 2.5f), "");

            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            GUILayout.BeginHorizontal();
            GUILayout.Label("SpaceCraft.io", headerStyle);
            GUILayout.EndHorizontal();

            if (showNickNameText)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Nick Name:");
                PhotonNetwork.player.name = GUILayout.TextField(PhotonNetwork.player.name, 12, GUILayout.Width(120));
                GUILayout.EndHorizontal();
            }

            if (showPlayAsGuest)
            {
                if (GUILayout.Button("Play as guest"))
                {
                    PhotonNetwork.JoinRandomRoom();
                }

                //Put login button and register button on the same line
                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Login"))
                {
                    showNickNameText = false;
                    showPlayAsGuest = false;
                    showLogin = true;
                    showFeedbackSuccessfulMessage = false;

                }

                if (GUILayout.Button("Register"))
                {
                    showNickNameText = false;
                    showPlayAsGuest = false;
                    showSignup = true;
                    showFeedbackSuccessfulMessage = false;
                }

                GUILayout.EndHorizontal();

                if (GUILayout.Button("Give feedback"))
                {
                    showNickNameText = false;
                    showPlayAsGuest = false;
                    showFeedback = true;
                    showFeedbackSuccessfulMessage = false;
                }

                GUILayout.Label("Press WASD to control the ship", labelStyle);
                GUILayout.Label("Press J to fire", labelStyle);
                GUILayout.Label("Defeat your opponents", labelStyle);
                GUILayout.Label("Earn high score!", labelStyle);


            }

            if (showPlay)
            {
                if (GUILayout.Button("Play"))
                {
                    PhotonNetwork.JoinRandomRoom();
                }
            }

            if (showLogin)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Username:");
                username = GUILayout.TextField(username, 12, GUILayout.Width(75));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Password:");
                password = GUILayout.PasswordField(password, "*"[0], 50, GUILayout.Width(75));
                GUILayout.EndHorizontal();

                if (GUILayout.Button("Login"))
                {
                    if (username.Equals(""))
                    {
                        showUserNameEmptyWarning = true;
                        showPasswordEmptyWarning = false;
                    }
                    else if (password.Equals(""))
                    {
                        showPasswordEmptyWarning = true;
                        showUserNameEmptyWarning = false;
                    }
                    else
                    {
                        showPasswordEmptyWarning = false;
                        showUserNameEmptyWarning = false;

                        WebClient webClient = new WebClient();
                        NameValueCollection formData = new NameValueCollection();
                        formData["username"] = username;
                        formData["password"] = password;
                        byte[] responseBytes = webClient.UploadValues(loginURL, "POST", formData);
                        string responsefromserver = Encoding.UTF8.GetString(responseBytes);
                        webClient.Dispose();

                        if (responsefromserver.Equals("login valid"))
                        {
                            showLogin = false;
                            showNickNameText = true;
                            showPlay = true;
                            showPasswordNotCorrectWarning = false;
                        }
                        else
                        {
                            showPasswordNotCorrectWarning = true;
                        }

                        Debug.Log(responsefromserver);

                    }

                }
            }

            if (showSignup)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Username:");
                username = GUILayout.TextField(username, 12, GUILayout.Width(75));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Password:");
                password = GUILayout.PasswordField(password, "*"[0], 50, GUILayout.Width(75));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                GUILayout.Label("Confirm Password:");
                confirmedPassword = GUILayout.PasswordField(confirmedPassword, "*"[0], 50, GUILayout.Width(75));
                GUILayout.EndHorizontal();

                if (GUILayout.Button("Sign up"))
                {
                    if (username.Equals(""))
                    {
                        showUserNameEmptyWarning = true;
                        showPasswordEmptyWarning = false;
                        showPasswordNotEqualWarning = false;
                    }
                    else if (password.Equals("") || confirmedPassword.Equals(""))
                    {
                        showPasswordEmptyWarning = true;
                        showUserNameEmptyWarning = false;
                        showPasswordNotEqualWarning = false;
                    }
                    else if (confirmedPassword != password)
                    {
                        showPasswordEmptyWarning = false;
                        showUserNameEmptyWarning = false;
                        showPasswordNotEqualWarning = true;
                    }
                    else
                    {
                        showUserNameEmptyWarning = false;
                        showPasswordEmptyWarning = false;
                        showPasswordNotEqualWarning = false;
                        showSignup = false;
                        showNickNameText = true;
                        showPlay = true;

                        WebClient webClient = new WebClient();
                        NameValueCollection formData = new NameValueCollection();
                        formData["username"] = username;
                        formData["password"] = password;
                        byte[] responseBytes = webClient.UploadValues(signUpURL, "POST", formData);
                        string responsefromserver = Encoding.UTF8.GetString(responseBytes);
                        Debug.Log(responsefromserver);
                        webClient.Dispose();
                    }
                }
            }

            if (showFeedback)
            {
                feedback = GUILayout.TextArea(feedback, 1000, GUILayout.Width(200), GUILayout.Height(100));
                if (GUILayout.Button("Submit feedback"))
                {
                    WebClient webClient = new WebClient();
                    NameValueCollection formData = new NameValueCollection();
                    formData["feedback"] = feedback;
                    byte[] responseBytes = webClient.UploadValues(feedbackURL, "POST", formData);
                    string responsefromserver = Encoding.UTF8.GetString(responseBytes);
                    webClient.Dispose();

                    if (responsefromserver.Equals("feedback added!"))
                    {
                        showNickNameText = true;
                        showPlayAsGuest = true;
                        showFeedback = false;
                        showFeedbackSuccessfulMessage = true;
                    }
                    else
                    {
                        showFeedbackFailureWarning = true;
                    }

                    Debug.Log(responsefromserver);

                }
            }

            if (showUserNameEmptyWarning)
            {

                GUILayout.BeginHorizontal();
                GUILayout.Label("Please enter username!", warningStyle);
                GUILayout.EndHorizontal();
            }

            if (showPasswordEmptyWarning)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Please enter password!", warningStyle);
                GUILayout.EndHorizontal();
            }

            if (showPasswordNotEqualWarning)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Passwords entered not match!", warningStyle);
                GUILayout.EndHorizontal();
            }

            if (showPasswordNotCorrectWarning)
            {

                GUILayout.BeginHorizontal();
                GUILayout.Label("Password not correct!", warningStyle);
                GUILayout.EndHorizontal();
            }

            if (showFeedbackFailureWarning)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Fail to submit feedback, please contact yh6714@ic.ac.uk", warningStyle);
                GUILayout.EndHorizontal();
            }

            if (showFeedbackSuccessfulMessage)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Feedback submitted!", warningStyle);
                GUILayout.EndHorizontal();
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        if (PhotonNetwork.inRoom)
        {
            //score display
            GUILayout.Label("Score: " + PhotonNetwork.player.GetScore());

            //LeaderBoard
            GUI.Box(new Rect(Screen.width - Screen.width / 5 - 10, 10, Screen.width / 5, Screen.height / 2.5f), "Leaderboard");
            GUILayout.BeginArea(new Rect(Screen.width - Screen.width / 5 - 10, 10, Screen.width / 5, Screen.height / 2.5f), "");
            GUILayout.BeginHorizontal();
            GUILayout.Label("        ");
            GUILayout.EndHorizontal();

            int rank = 1;
            List<PhotonPlayer> playerList = PhotonNetwork.playerList.ToList();

            playerList.Add(AI1);
            playerList.Add(AI2);
            playerList.Add(AI3);
            playerList.Add(AI4);
            playerList.Add(AI5);

            foreach (PhotonPlayer player in playerList.OrderByDescending(x => x.GetScore()).Take(NumberOfPlayerInLeaderboard))
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("  " + rank + ". " + player.name + " score:  " + player.GetScore());
                GUILayout.EndHorizontal();
                rank++;
            }
            GUILayout.EndArea();

            //player position
            GUILayout.BeginArea(new Rect(Screen.width - Screen.width / 5, 2 * Screen.height / 3, Screen.width / 5, Screen.height / 2), "");
            GUILayout.BeginHorizontal();
            GUILayout.Label(playerPos + "");
            GUILayout.EndHorizontal();
            GUILayout.EndArea();

        }
    }

    public override void OnJoinedLobby()
    {
 //       PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Can't join random room!");
        PhotonNetwork.CreateRoom(null);
    }

    public override void OnJoinedRoom()
    {
        respawnPlayer ();
        respawnPlayerAI(0);
        respawnPlayerAI(1);
        respawnPlayerAI(2);
        respawnPlayerAI(3);
        respawnPlayerAI(4);
    }

	void respawnPlayer() {
		Vector3 spawnPoint = new Vector3 (Random.Range (min, max), Random.Range (min, max), 0);
		float radius = 3;
		/* still need to work on how to check if there is object surrounding when respawning */
		while (Physics.CheckSphere (spawnPoint, radius)) {
			spawnPoint = new Vector3 (Random.Range (min, max), Random.Range (min, max), 0);
		}
		GameObject player = PhotonNetwork.Instantiate("Player",
			spawnPoint, Quaternion.identity, 0);
		mainCamera.enabled = false;
		player.GetComponent<ShipController>().enabled = true;
		player.GetComponentInChildren<Camera> ().enabled = true;
		player.GetComponentsInChildren<Camera> () [1].enabled = true;
		player.GetComponent<ShipController> ().Spacefield.GetComponent<MeshRenderer> ().enabled = true;
		player.GetComponent<ShipController> ().SpacefieldFront.GetComponent<MeshRenderer> ().enabled = true;
	}

    void respawnPlayerAI(int num)
    {
        Vector3 spawnPoint = new Vector3(Random.Range(min, max), Random.Range(min, max), 0);
        float radius = 3;
        /* still need to work on how to check if there is object surrounding when respawning */
        while (Physics.CheckSphere(spawnPoint, radius))
        {
            spawnPoint = new Vector3(Random.Range(min, max), Random.Range(min, max), 0);
        }
        GameObject player = PhotonNetwork.Instantiate("PlayerAI",
            spawnPoint, Quaternion.identity, 0);
        player.GetComponent<IMAI>().num = num;
    }

}


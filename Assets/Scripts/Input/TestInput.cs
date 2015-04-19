using UnityEngine;
using System.Collections;

public class TestInput : MonoBehaviour {

    bool isPlayerOneLocked;
    bool isPlayerTwoLocked;
    string playerOneController;
    string playerTwoController;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
        LockPlayerController();
	}

    void LockPlayerController() {
        if (Input.GetButtonDown("AttackJoy1")) 
        {
            if (!isPlayerOneLocked) { 
                isPlayerOneLocked = true; 
                playerOneController = "Joy1"; 
            }
            else if (!isPlayerTwoLocked) { 
                isPlayerTwoLocked = true;
                playerTwoController = "Joy1"; 
            }
        }

        if (Input.GetButtonDown("AttackJoy2"))
        {
            if (!isPlayerOneLocked) { 
                isPlayerOneLocked = true;
                playerOneController = "Joy2"; 
            }
            else if (!isPlayerTwoLocked) { 
                isPlayerTwoLocked = true;
                playerTwoController = "Joy2"; 
            }
        }
    }
}

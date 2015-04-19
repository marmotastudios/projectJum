using UnityEngine;
using System.Collections;

public class TestCharacterDeath : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            GameObject.Find("LifeManager").SendMessage("SetRespawnPosition", GameObject.Find("P1")); 
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameObject.Find("LifeManager").SendMessage("SetRespawnPosition", GameObject.Find("P2"));
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameObject.Find("LifeManager").SendMessage("SetRespawnPosition", GameObject.Find("P3"));
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GameObject.Find("LifeManager").SendMessage("SetRespawnPosition", GameObject.Find("P4"));
        }
	}
}

using UnityEngine;
using System.Collections;

public class LifeManager : MonoBehaviour {

    int livesP1;
    int livesP2;
    int livesP3;
    int livesP4;
    
    Vector3 respawnPositionP1;
    Vector3 respawnPositionP2;
    Vector3 respawnPositionP3;
    Vector3 respawnPositionP4;

	// Use this for initialization
	void Start () {
        InitializeRespawnPositions();
        LoadAmountOfLives();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    /// <summary>
    /// Coloca al jugador en la posición de Respawn correspondiente
    /// </summary>
    /// <param name="player">El objeto que contiene al jugador</param>
    void SetRespawnPosition(GameObject player) {

        string playerName;
        Vector3 respawnPosition;

        playerName = player.name;
        respawnPosition = Vector3.zero;

        if (playerName.Contains("P1"))
        {
            respawnPosition = respawnPositionP1;
        }
        else if (playerName.Contains("P2"))
        {
            respawnPosition = respawnPositionP2;
        }
        else if (playerName.Contains("P3"))
        {
            respawnPosition = respawnPositionP3;
        }
        else if (playerName.Contains("P4"))
        {
            respawnPosition = respawnPositionP4;
        }

        RespawnPlayer(player, respawnPosition);
    }

    /// <summary>
    /// Revive al jugador que perdió una vida
    /// </summary>
    /// <param name="player">El objeto que contiene al jugador</param>
    /// <param name="newPosition">La posicion donde aparecera el jugador</param>
    void RespawnPlayer(GameObject player, Vector3 newPosition) {
        
        //Colocar itween para que el personaje baje de forma suave
        
        //Se detiene el spawn de bloques
        SetPlayerInvincible();

        //Colocar plataforma
        SetRespawnPlatform(newPosition);

        player.transform.position = newPosition;   
    }

    /// <summary>
    /// Envia un mensaje para que se coloque una plataforma bajo el personaje que acaba de revivir
    /// </summary>
    void SetRespawnPlatform(Vector3 platformPosition) {

        Vector3 position;

        position = new Vector3(platformPosition.x, platformPosition.y - 1.0f, platformPosition.z);

        //Mensaje
    }

    /// <summary>
    /// Indica que debe hacerse al jugador invisible
    /// </summary>
    void SetPlayerInvincible() {
        
    }

    /// <summary>
    /// Carga desde archivo la cantidad de vidas que tendran los jugadores
    /// </summary>
    void LoadAmountOfLives() {
        livesP1 = PlayerPrefs.GetInt("lives", 1);
        livesP2 = livesP3 = livesP4 = livesP1;
    }

    /// <summary>
    /// Coloca las posiciones iniciales de respawn de los personajes
    /// </summary>
    void InitializeRespawnPositions() {
        respawnPositionP1 = new Vector3(-5.0f, 5.0f, 0);
        respawnPositionP2 = new Vector3(-2.0f, 5.0f, 0);
        respawnPositionP3 = new Vector3(2.0f, 5.0f, 0);
        respawnPositionP4 = new Vector3(5.0f, 5.0f, 0);
    }
}

using UnityEngine;
using System.Collections;

public class BlockSpawnManager : MonoBehaviour {

    //Posiciones de los bloques
    public float[] blockSpawnPosX;
    public float[] blockSpawnPosY;
    public float blockSpawnPosZ;
    
    //Bloques estilo push
    GameObject[] pushBlocks;

    //Tipo de bloques que apareceran en la partida
    public BlockType[] blockType;

    //Cantidad de bloques por escenario
    public int blockAmount;

    //Forma de caida/desplazamiento de los bloques
    BlockSpawn blockSpawnStyle;

    //Tiempo de salida entre bloque y bloque
    public float blockSpawnInterval;
    float spawnTimer;

    //Velocidad de caida/desplazamiento horizontal/desplazamiento en profundidad del bloque
    public float blockSpeed;

    //Padre de los bloques
    public Transform blockParent;

	// Use this for initialization
	void Start () {
        InitialValues();
	}

	// Update is called once per frame
	void Update () {
        SpawnBlock();
	}

    /// <summary>
    /// Hace aparecer un bloque en el escenario
    /// </summary>
    void SpawnBlock() {
        if (blockSpawnInterval > 0.0f) {
            blockSpawnInterval -= Time.deltaTime;
        }
        else if (blockSpawnInterval <= 0.0f) {
            blockSpawnInterval = spawnTimer;
            
            if (blockSpawnStyle == BlockSpawn.push) {
                RandomizePush();
            }
            else {
                InstantiateBlock();
            }
        }
    }

    void RandomizePush() {
        int resultIndex;
        GameObject block;

        resultIndex = Random.Range(0, pushBlocks.Length);
        
        block = pushBlocks[resultIndex];

        MoveBlockPush(block);
    }

    /// <summary>
    /// Obtiene la lista de bloques segun el nivel de dificultad
    /// </summary>
    void GetBlockTypeList() {
    
    }

    /// <summary>
    /// Obtiene desde archivo el estilo de caida/desplazamiento de los bloques
    /// </summary>
    void GetBlockSpawnStyle() {
        //blockSpawnStyle = BlockSpawn.fall;
        //blockSpawnStyle = BlockSpawn.horizontal;
        blockSpawnStyle = BlockSpawn.push;
    }

    /// <summary>
    /// Valores iniciales de la partida
    /// </summary>
    void InitialValues() {
        GetBlockSpawnStyle();

        if (blockSpawnStyle == BlockSpawn.push) {
            InstantiatePushBlock();
        }
        
        SetBlockAmount();
        SetBlockPositions();
        SetBlockSpeed();
        SetBlockSpawnInterval();
        
    }

    void SetBlockSpeed() {
        int spawnStyle;

        spawnStyle = (int)blockSpawnStyle;

        switch (spawnStyle)
        {
            case (int)BlockSpawn.fall:
                blockSpeed = 0.5f;
            break;

            case (int)BlockSpawn.horizontal:
                blockSpeed = 0.5f;
            break;

            case (int)BlockSpawn.push:
                blockSpeed = 4.0f;
            break;
        }
    }

    /// <summary>
    /// Coloca las posiciones de los bloques segun el tipo de movimiento de los mismos
    /// </summary>
    void SetBlockPositions() {
        int spawnStyle;

        spawnStyle = (int) blockSpawnStyle;

        switch (spawnStyle) {
            case (int) BlockSpawn.fall:
                SetBlockPositionsFall();
            break;

            case (int)BlockSpawn.horizontal:
                SetBlockPositionsHorizontal();
            break;

            case (int)BlockSpawn.push:
                SetBlockPositionsPush();
            break;
        }
    }

    /// <summary>
    /// Coloca la cantidad de bloques segun el escenario actual
    /// </summary>
    void SetBlockAmount() {

        int spawnStyle;

        spawnStyle = (int) blockSpawnStyle;

        switch (spawnStyle) {
            case (int)BlockSpawn.fall:
                blockAmount = 5;
            break;

            case (int)BlockSpawn.horizontal:
                blockAmount = 4;
            break;

        }
    }

    void SetBlockSpawnInterval() {

        int spawnStyle;

        spawnStyle = (int)blockSpawnStyle;

        switch (spawnStyle)
        {
            case (int)BlockSpawn.fall:
                blockSpawnInterval = 3.0f;
                break;

            case (int)BlockSpawn.horizontal:
                blockSpawnInterval = 3.0f;
                break;

            case (int)BlockSpawn.push:
                blockSpawnInterval = 4.0f;
                break;
        }
        
        spawnTimer = blockSpawnInterval;
    }

    void SetBlockPositionsFall() {

        blockSpawnPosX = new float[blockAmount];
        blockSpawnPosY = new float[1];

        blockSpawnPosX[0] = 0.0f;
        blockSpawnPosX[1] = 1.5f;
        blockSpawnPosX[2] = 3.0f;
        blockSpawnPosX[3] = -1.5f;
        blockSpawnPosX[4] = -3.0f;

        blockSpawnPosY[0] = 6.0f;

        blockSpawnPosZ = 0.0f;
    }

    void SetBlockPositionsHorizontal() {

        blockSpawnPosX = new float[2];
        blockSpawnPosY = new float[blockAmount];

        blockSpawnPosX[0] = -5.0f;
        blockSpawnPosX[1] = 5.0f;

        blockSpawnPosY[0] = 0.0f;
        blockSpawnPosY[1] = 1.5f;
        blockSpawnPosY[2] = 3.0f;
        blockSpawnPosY[3] = 4.5f;

        blockSpawnPosZ = 0.0f;
    }

    void SetBlockPositionsPush() {

        GameObject pushStack;
        int pushStackChildrenCount;

        pushStack = GameObject.Find("PushStack");
        pushStackChildrenCount = pushStack.transform.childCount;
        
        pushBlocks = new GameObject[pushStackChildrenCount];

        for (int i = 0; i < pushStackChildrenCount; i++) {
            pushBlocks[i] = pushStack.transform.GetChild(i).gameObject;
        }

    }

    void InstantiateBlock()
    {

        GameObject block;
        int spawnStyle;

        spawnStyle = (int)blockSpawnStyle;       
        block = (GameObject) Instantiate(Resources.Load("Block/Normal", typeof(GameObject)));

        block.transform.parent = blockParent;
        
        switch (spawnStyle)
        {
            case (int)BlockSpawn.fall:
                SelectAndSetBlockPositionFall(block);
            break;

            case (int)BlockSpawn.horizontal:
                block.GetComponent<Rigidbody>().useGravity = false;
                SelectAndSetBlockPositionHorizontal(block);
            break;

            case (int)BlockSpawn.push:
                block.GetComponent<Rigidbody>().useGravity = false;
                SelectAndSetBlockPositionPush(block);
            break;
        }
        
    }

    void InstantiatePushBlock() {
        GameObject block;

        block = (GameObject)Instantiate(Resources.Load("Block/PushStack", typeof(GameObject)));
        block.name = "PushStack";
    }
    /// <summary>
    /// Genera de forma aleatoria una posicion y la coloca al bloque instanciado
    /// Caso caida
    /// </summary>
    /// <param name="block"></param>
    void SelectAndSetBlockPositionFall(GameObject block) {

        int resultIndex;
        float positionX;
        float positionY;
        float positionZ;

        resultIndex = Random.Range(0, blockAmount);
        
        positionX = blockSpawnPosX[resultIndex];
        positionY = blockSpawnPosY[0];
        positionZ = blockSpawnPosZ;

        block.transform.position = new Vector3(positionX, positionY, positionZ);
    }

    /// <summary>
    /// Genera de forma aleatoria una posicion y la coloca al bloque instanciado
    /// Caso caida
    /// </summary>
    /// <param name="block"></param>
    void SelectAndSetBlockPositionHorizontal(GameObject block)
    {
        int resultIndexX;
        int resultIndexY;
        float positionX;
        float positionY;
        float positionZ;

        resultIndexX = Random.Range(0, 2);
        resultIndexY = Random.Range(0, blockAmount);

        positionX = blockSpawnPosX[resultIndexX];
        positionY = blockSpawnPosY[resultIndexY];
        positionZ = blockSpawnPosZ;

        block.transform.position = new Vector3(positionX, positionY, positionZ);

        MoveBlockHorizontal (block);
    }

    void SelectAndSetBlockPositionPush(GameObject block) {
    
    }

    void MoveBlockHorizontal(GameObject block) {
        if (block.transform.position.x > 0.0f) {
            iTween.MoveTo(block, new Hashtable() {  { "x", -10.0f }, 
                                                    { "time", 2.0f }, 
                                                    { "islocal", true },
                                                    { "speed" , blockSpeed }, 
                                                    { "easetype", "linear" } });
        }
        else if (block.transform.position.x < 0.0f) {
            iTween.MoveTo(block, new Hashtable() {  { "x", 10.0f }, 
                                                    { "time", 2.0f },
                                                    { "islocal", true },
                                                    { "speed" , blockSpeed }, 
                                                    { "easetype", "linear" } });
        }
    }

    void MoveBlockPush(GameObject block) {
        iTween.MoveTo(block, new Hashtable() {  { "z", -2.5f }, 
                                                { "time", 2.0f },
                                                { "islocal", true },
                                                { "speed" , blockSpeed }, 
                                                { "easetype", "linear" },
                                                { "oncomplete", "ReturnBlockPush"},
                                                { "oncompleteparams", block},
                                                { "oncompletetarget", this.gameObject}});
    }

    void ReturnBlockPush(GameObject block) {
        iTween.MoveTo(block, new Hashtable() {  { "z", 3.0f }, 
                                                { "time", 2.0f },
                                                { "islocal", true },
                                                { "delay", 1.0f },
                                                { "speed" , blockSpeed }, 
                                                { "easetype", "linear" } });
    }
}

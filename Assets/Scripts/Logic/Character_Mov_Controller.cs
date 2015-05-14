using UnityEngine;
using System.Collections;

public class Character_Mov_Controller : MonoBehaviour {


	private static float HORIZONTAL_RUNNING_SPEED = 6;
	private static float HORIZONTAL_WALKING_SPEED = 3.5F;
	private static float VERTICAL_MOV_SPEED = 12f;
	private static float MAX_X_MOMENTUM = 0.2f;
	private static float GRAVITY_VALUE = 0.6f;



	Vector3 Momentum = new Vector3(0,0,0);
	Vector3 Movement = new Vector3(0,0,0);
	CharacterController controller;

	//Flags
	bool secondJumpAviable = false;
	bool blockJump = false; //Se utiliza para evitar que se quede saltando cuando dejas precionado el boton de jump

	// Use this for initialization
	void Start () {
		controller = this.GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
		dummyInputManager ();
		gravity ();
		controller.Move (Movement*Time.deltaTime);
	}

	
	void dummyInputManager(){
		if (controller.isGrounded) {
			MoveOnGround (Input.GetAxis ("Horizontal"));
		} else {
			MoveOnAir (Input.GetAxis ("Horizontal"));
		}
		if (Input.GetButton ("Jump")) {
			if (!blockJump || secondJumpAviable) {
				Jump ();
			}
		} else if (blockJump) {
			blockJump = false;
		}
	}

	/// <summary>
	/// Funcion que recibe los parametros de entrada de los axis y determina el movimiento a realizar.
	/// Se debe llamar en cada update asi no se este precionando nada
	/// </summary>
	/// <param name="axisValue">Axis value.</param>
	public void MoveOnGround (float axisValue){
		if (axisValue != 0) {
			//Si el jugador mueve el axis
			if (Mathf.Abs (axisValue) < 0.5f) {
				//Si el valor del axis es menor a la mitad consideramos que el jugador desea caminar
				Walk (axisValue);
			} else {
				//Si es mayor a la mitad consideramos que el usuario desea correr
				Run (axisValue);
			}
		} else {
			//Si el jugador no esta moviendo el axis
			Stop();
		}
	}

	/// <summary>
	/// Funcion que recibe los parametros de entrada de los axis y determina el movimiento a realizar.
	/// Se debe llamar en cada update asi no se este precionando nada
	/// </summary>
	/// <param name="axisValue">Axis value.</param>
	public void MoveOnAir (float axisValue){
		if (axisValue != 0) {
			Walk (axisValue);
		}else{
			Stop();
		}

	}

	/// <summary>
	/// Mueve al personaje a la velocidad fixed de correr.
	/// </summary>
	/// <param name="direction">
	/// Valores negativos indican movimiento hacia la izquierda
	/// Valores positivos indican movimiento hacia la derecha
	/// </param>
	void Run (float direction){
		float translation;
		if (direction > 0) {
			translation = translation = HORIZONTAL_RUNNING_SPEED;
		} else {
			translation = -HORIZONTAL_RUNNING_SPEED;
		}
		Movement.x = translation;
	}

	/// <summary>
	/// Mueve al personaje a la velocidad fixed de caminar.
	/// </summary>
	/// <param name="direction">
	/// Valores negativos indican movimiento hacia la izquierda
	/// Valores positivos indican movimiento hacia la derecha
	/// </param>
	void Walk (float direction){
		float translation;
		if (direction > 0) {
			translation = (HORIZONTAL_WALKING_SPEED);
		} else {
			translation = (-HORIZONTAL_WALKING_SPEED);
		}
		Movement.x = translation;
	}

	void Stop (){
		Movement.x = 0;
	}

	/// <summary>
	/// Funcion que se llama cuando el jugador preciona el boton de saltar.
	/// </summary>
	public void Jump(){
		if (controller.isGrounded) {
			Momentum.y = VERTICAL_MOV_SPEED;
			Momentum.x = Movement.x;
			Movement.y = VERTICAL_MOV_SPEED;
			secondJumpAviable = true;
			blockJump = true;
		} else if (secondJumpAviable) {
			Momentum.y = VERTICAL_MOV_SPEED;
			Movement.y = VERTICAL_MOV_SPEED;
			secondJumpAviable = false;
		}
	}

	void gravity(){
		if (!controller.isGrounded) {
			Movement.y = Momentum.y - (GRAVITY_VALUE);
			Momentum.y = Movement.y;
		}
	}



}

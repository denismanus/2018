using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    Command commandToExecute;

    private Command buttonA, buttonD, buttonW;

    public Actor character;

    void Start()
    {
        character = new Actor(FindObjectOfType<Actor>().gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        commandToExecute = HandleInput();
        if (commandToExecute != null)
        {
            commandToExecute.Execute(character);
        }
    }

    Command HandleInput()
    {
        if (Input.GetKey(KeyCode.A))
        {
            return new MoveLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            return new MoveRight();
        }
        else if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("d");
            return new Jump();
        }
        return null;
    }

}
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CommandRunner : MonoBehaviour
{
    public RobotController robotController;

    public void RunCommands()
    {
        StartCoroutine(ExecuteCommands());

        Debug.Log("Executando comandos");
    }

    private IEnumerator ExecuteCommands()
    {
        foreach (Transform command in transform)
        {
            string commandName = command.name;

            switch (commandName)
            {
                case "Block_MoveForward_Inst(Clone)":
                    yield return StartCoroutine(robotController.ExecuteCommand("MoveForward"));
                    break;

                case "Block_TurnLeft_Inst(Clone)":
                    yield return StartCoroutine(robotController.ExecuteCommand("TurnLeft"));
                    break;

                case "Block_TurnRight_Inst(Clone)":
                    yield return StartCoroutine(robotController.ExecuteCommand("TurnRight"));
                    break;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}
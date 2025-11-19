using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class CommandRunner : MonoBehaviour
{
    public RobotController robotController;

    public void RunCommands()
    {
        StartCoroutine(ExecuteCommands());

        //Debug.Log("Executando comandos");
    }

    private IEnumerator ExecuteCommands()
    {
        foreach (Transform command in transform)
        {
            string commandName = command.name;

            if (commandName.StartsWith("Block_MoveForward"))
            {
                yield return StartCoroutine(robotController.ExecuteCommand("MoveForward"));
            }
            else if (commandName.StartsWith("Block_TurnLeft"))
            {
                yield return StartCoroutine(robotController.ExecuteCommand("TurnLeft"));
            }
            else if (commandName.StartsWith("Block_TurnRight"))
            {
                yield return StartCoroutine(robotController.ExecuteCommand("TurnRight"));
            }

            //ESTE CÓDIGO ABAIXO FOI SUBSTITUIDO PELO SCRIPT ACIMA, QUE UTILIZA "STARSWITH" PARA ENCONTRAR AS NOMENCLATURAS DE MOVIMENTOS
            //switch (commandName)
            //{
            //    case "Block_MoveForward_Inst(Clone)":
            //        yield return StartCoroutine(robotController.ExecuteCommand("MoveForward"));
            //        break;

            //    case "Block_TurnLeft_Inst(Copy)":
            //        yield return StartCoroutine(robotController.ExecuteCommand("TurnLeft"));
            //        break;

            //    case "Block_TurnRight_Inst(Copy)":
            //        yield return StartCoroutine(robotController.ExecuteCommand("TurnRight"));
            //        break;
            //}

            yield return new WaitForSeconds(0.2f);

            foreach (Transform child in transform) 
                Destroy(child.gameObject);

            Debug.Log("Lista de comandos limpa!");
        }
    }
}
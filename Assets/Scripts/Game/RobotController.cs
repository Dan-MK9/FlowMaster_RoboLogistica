using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class RobotController : MonoBehaviour
{
    public float moveDistance = 1f; //Distancia de movimento por comando
    public float moveSpeed = 2f; //Velocidade do movimento
    public float turnSpeed = 90f; //Velocidade de rotação (graus por segundo)

    private bool isMoving = false;
    public IEnumerator ExecuteCommand(string command)
    {
        if (isMoving) yield break;
        isMoving = true;
        
        switch (command)
        {
            case "MoveForward":
                yield return StartCoroutine(Move(Vector3.forward));
                break;

            case "TurnLeft":
                yield return StartCoroutine(Turn(-90));
                break;

            case "TurnRight":
                yield return StartCoroutine(Turn(90));
                break;
        }

        isMoving = false;
    }

    public IEnumerator MoveForward()
    {
        Vector3 starPos = transform.position;
        Vector3 targetPos = starPos + transform.forward * moveDistance;

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(starPos, targetPos, t);
            yield return null;
        }
    }

    private IEnumerator Turn(float angle)
    {
        Quaternion startRot = transform.rotation;
        Quaternion endRot = startRot * Quaternion.Euler(0, angle, 0);
        float elapsed = 0f;

        while (elapsed < 1f)
        {
            transform.rotation = Quaternion.Lerp(startRot, endRot, elapsed);
            elapsed += Time.deltaTime * moveSpeed / turnSpeed;
            yield return null;
        }

        transform.rotation = endRot;
    }

    public IEnumerator TurnLeft()
    {
        yield return Rotate(-90);
    }

    public IEnumerator TurnRight()
    {
        yield return Rotate(90);
    }

    private IEnumerator Move(Vector3 direction)
    {
        Vector3 starPos = transform.position;
        Vector3 endPos = starPos + transform.TransformDirection(direction) * moveDistance;
        float elapsed = 0f;

        while (elapsed < 1f)
        {
            transform.position = Vector3.Lerp(starPos, endPos, elapsed);
            elapsed += Time.deltaTime * moveSpeed;
            yield return null;
        }

        transform.position = endPos;
    }

    private IEnumerator Rotate(float angle)
    {
        Quaternion starRot = transform.rotation;
        Quaternion targetRot = starRot * Quaternion.Euler(0, angle, 0);

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * (turnSpeed / 90f);
            transform.rotation = Quaternion.Slerp(starRot, targetRot, t);
            yield return null;
        }
    }
}
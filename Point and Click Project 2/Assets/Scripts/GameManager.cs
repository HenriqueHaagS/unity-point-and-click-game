using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static List<int> collectedItems = new List<int>();
    static float moveSpeed = 3.5f, moveAccuracy = 0.15f;
    public AnimationData[] playerAnimations; 

    public IEnumerator MoveToPoint(Transform myObject, Vector2 point) //IEnumerator that receives "player, item.goToPoint.position" and passes object transform and point vector2
    {
        Vector2 positionDifference = point - (Vector2)myObject.position; // calculating position difference

        while (positionDifference.magnitude > moveAccuracy) // stop when we are near the point
        {
            myObject.Translate(moveSpeed * positionDifference.normalized * Time.deltaTime); //move in direction frame
            positionDifference = point - (Vector2)myObject.position; // recalculate position difference
            yield return null;
        }
        myObject.position = point; //snap to point

        if (myObject == FindObjectOfType<ClickManager>().player) // if object is player
        {
            FindObjectOfType<ClickManager>().playerWalking = false; // player stops walking

        }
        yield return null;
    }
}

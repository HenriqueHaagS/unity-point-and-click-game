using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static List<int> collectedItems = new List<int>();
    static float moveSpeed = 3.5f, moveAccuracy = 0.15f;
    public AnimationData[] playerAnimations;
    public RectTransform nameTagRT, hintMessageRT;

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
            yield return null;
        }
    }
    public void UpdateNameTag(ItemData item)
    {
        nameTagRT.GetComponentInChildren<TextMeshProUGUI>().text = item.objectName;
        nameTagRT.sizeDelta = item.nameTagSize;
        nameTagRT.localPosition = new Vector2(item.nameTagSize.x/2, -0.5f);
    }
    public void UpdateHintMessage(ItemData item, bool playerFlipped)
    {
        if (item == null)
        {
            hintMessageRT.gameObject.SetActive(false);
            return;
        }
        hintMessageRT.gameObject.SetActive(true);
        hintMessageRT.GetComponentInChildren<TextMeshProUGUI>().text = item.hintMessage;
        hintMessageRT.sizeDelta = item.hintMessageSize;
        if (playerFlipped)
        {
            hintMessageRT.parent.localPosition = new Vector2(-1, 0);
        }
        else
        {
            hintMessageRT.parent.localPosition = Vector2.zero;
        }        
    }
}

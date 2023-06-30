using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public Transform goToPoint;
    public int itemID, requiredItemID;
    public GameObject[] objectsToRemove;
    public string objectName;
    public Vector2 nameTagSize = new Vector2(3, 0.65f);
    [TextArea(3,3)]
    public string hintMessage;
    public Vector2 hintMessageSize = new Vector2(4, 0.65f);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public bool playerWalking;
    public Transform player;
    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public void GoToItem(ItemData item)
    {
        StartCoroutine(gameManager.MoveToPoint(player, item.goToPoint.position)); // starts coroutine passing player transform and goToPoint transform in gameManager
        player.GetComponent<SpriteAnimator>().PlayAnimation(gameManager.playerAnimations[1]); // play PlayAnimation function on SpriteAnimator
        playerWalking = true;
        TryGettingItem(item);
        StartCoroutine(UpdateSceneAfterAction(item));
    }
    public void TryGettingItem(ItemData item)
    {
        if (item.requiredItemID == -1 || GameManager.collectedItems.Contains(item.requiredItemID)) // check if item is pickable because of ItemID or if collectedItem list contais requiredItemID
        {   
            GameManager.collectedItems.Add(item.itemID); // collect item
        }
    }
    private IEnumerator UpdateSceneAfterAction(ItemData item)
    {
        while (playerWalking) //wait for player reaching target
        {
            yield return new WaitForSeconds(0.05f);
        }
        foreach (GameObject g in item.objectsToRemove)
        {
            Destroy(g);
        }
        player.GetComponent<SpriteAnimator>().PlayAnimation(null); //Play players base animation on Sprite Animator script
        Debug.Log("item collected");
        yield return null;
    }
}

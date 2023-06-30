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
        if (playerWalking == false)
        {
            // update hint box
            gameManager.UpdateHintMessage(null, false);
            // play PlayAnimation function on SpriteAnimator
            player.GetComponent<SpriteAnimator>().PlayAnimation(gameManager.playerAnimations[1]);
            playerWalking = true;
            // starts coroutine passing player transform and goToPoint transform in gameManager
            StartCoroutine(gameManager.MoveToPoint(player, item.goToPoint.position));
            //equipment stuff
            TryGettingItem(item);
        }
               
    }
    public void TryGettingItem(ItemData item)
    {
        bool canGetItem = item.requiredItemID == -1 || GameManager.collectedItems.Contains(item.requiredItemID);
        if (item.requiredItemID == -1 || GameManager.collectedItems.Contains(item.requiredItemID)) // check if item is pickable because of ItemID or if collectedItem list contais requiredItemID
        {   
            GameManager.collectedItems.Add(item.itemID); // collect item
        }
        StartCoroutine(UpdateSceneAfterAction(item, canGetItem));
    }
    private IEnumerator UpdateSceneAfterAction(ItemData item, bool canGetItem)
    {
        while (playerWalking) //wait for player reaching target
        {
            yield return new WaitForSeconds(0.05f);
        }
        if (canGetItem)
        {
            foreach (GameObject g in item.objectsToRemove)
            {
                Destroy(g);
                Debug.Log("item collected");
            }            
        }
        else
        {
            gameManager.UpdateHintMessage(item, player.GetComponentInChildren<SpriteRenderer>().flipX);
        }
        player.GetComponent<SpriteAnimator>().PlayAnimation(null); //Play players base animation on Sprite Animator script
        playerWalking = false;
        yield return null;
    }
}

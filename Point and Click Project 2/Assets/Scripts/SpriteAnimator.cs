using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public SpriteRenderer mySpriteRenderer;
    public AnimationData baseAnimation;
    Coroutine previousAnimation;

    public void PlayAnimation(AnimationData data) // function receives "playerAnimations[1]" passes as data
    {
        if (previousAnimation != null) // Coroutine not empty
        {
            Debug.Log("StopCoroutine(previousAnimation)");
            StopCoroutine(previousAnimation);
            StartCoroutine(PlayAnimationCoroutine(data));
        }
        else // Coroutine empty
        {
            Debug.Log("StartCoroutine(PlayAnimationCoroutine(data))");
            previousAnimation = StartCoroutine(PlayAnimationCoroutine(data)); // passes data to Coroutine, starts, and stores in previousAnimation variable
        }
    }
    public IEnumerator PlayAnimationCoroutine(AnimationData data)
    {
        if (data == null)
        {
            Debug.Log("PlayAnimationCoroutine(null)");
                data = baseAnimation;
        }

        int spritesAmount = data.sprites.Length, i = 0; // declares int variables
        float waitTime = data.framesOfGap * AnimationData.targetFrameTime; // calculates time between frame

        while (i < spritesAmount)
        {
            mySpriteRenderer.sprite = data.sprites[i++]; // change sprite and increase 1
            yield return new WaitForSeconds(waitTime); // wait
            
            if (data.loop && i >= spritesAmount) //check if animation is on last sprite
            {
                i = 0; //starts loop over
            }
        }
        yield return null;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelAnimations : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    [Header("Sprite Arrays")]
    public Sprite[] startupSprites; // Array of sprites of startup animation
    public Sprite[] programSprites; // Array of sprites of program animations
    public Sprite[] otherSprites; // Array of sprites of other animations

    public bool isPlaying = false;

    void Start()
    {
        spriteRenderer.enabled = false;
    }

    public void PlayStartupAnimation()
    {

        spriteRenderer.enabled = true;
        StartCoroutine(PlayAnimation(startupSprites, Random.Range(1f, 3f)));
        // Do something after the animation is done
    }

    // Method to check current sprite renderers sprite con tains a specific string
    public bool DoesRendererContainString(string sprite)
    {
        return spriteRenderer.sprite.name.Contains(sprite);
    }

    // Method to search other and program sprites for a specific sprite and set that to the sprite renderer
    public void SetSprite(string sprite)
    {
        foreach (Sprite s in otherSprites)
        {
            if (s.name == sprite)
            {
                spriteRenderer.sprite = s;
                return;
            }
        }
        foreach (Sprite s in programSprites)
        {
            if (s.name == sprite)
            {
                spriteRenderer.sprite = s;
                return;
            }
        }
    }

    // Method to set sprite renderer to next program sprite
    public void SetNextProgramSprite()
    {
        int currentIndex = GetProgramSpriteIndex();

        // If current index is -1, set first sprite in programSprites array
        if (currentIndex == -1)
        {
            spriteRenderer.sprite = programSprites[0];
        }
        else
        {
            // If current index is not the last index in the array, set the next sprite in the array
            if (currentIndex + 1 < programSprites.Length)
            {
                spriteRenderer.sprite = programSprites[currentIndex + 1];
            }
            else
            {
                spriteRenderer.sprite = programSprites[0];
            }
        }
    }

    // Hide the sprite renderer
    public void HideSpriteRenderer()
    {
        spriteRenderer.enabled = false;
    }

    IEnumerator PlayAnimation(Sprite[] sprites, float timeBetweenSprites = 1f)
    {
        // Show the sprite renderer
        spriteRenderer.enabled = true;  //TODO is this ideal way to do this?
        
        isPlaying = true;
        foreach (Sprite sprite in sprites)
        {
            // Wait for random time between 1-3 seconds
            yield return new WaitForSeconds(timeBetweenSprites);
            // set current sprite
            spriteRenderer.sprite = sprite;
        }
        isPlaying = false;
    }

    // Get current renderer program sprite index
    public int GetProgramSpriteIndex()
    {
        for (int i = 0; i < programSprites.Length; i++)
        {
            if (spriteRenderer.sprite == programSprites[i])
            {
                return i;
            }
        }
        return -1;
    }
}

﻿using UnityEngine.UI;
using UnityEngine;
using System.Collections;

/// <summary>
/// Taekos's lifebar in the top-left part of the screen.
/// <remarks>
/// By Joshua Rand
/// </remarks>
/// </summary>
public class Lifebar : MonoBehaviour {
	public Image lifebar;           ///< White Image
    public Image lifebarMask;       ///< Lifebar image
	public Sprite[] sprites;
    public Sprite[] maskSprites;
    public Text livesRemaining;
    public Text featherCounter;
    private float blinkTime = 0.1f;
    private Color normalColor;
    private Color redColor;         ///< When taking damage
    private Color greenColor;       ///< When restoring health
	private RectTransform rec;

    /// <summary>
    /// Assign colors
    /// </summary>
    void Awake()
    {
        normalColor = new Color(1f, 1f, 1f, 1f);
        redColor = new Color(1f, 0f, 0f, 0.5f);     //May need to fix
        greenColor = new Color(0f, 1f, 0f, 0.5f);   //May need to fix
    }

    /// <summary>
    /// Subscribe to events
    /// </summary>
    void OnEnable()
    {
        Controller.UpdateLifebar += UpdateLifebar;
        GameController.UpdateFeatherCounter += UpdateFeatherCounter;
    }

    /// <summary>
    /// Unsubscribe to events
    /// </summary>
    void OnDisable()
    {
        Controller.UpdateLifebar -= UpdateLifebar;
        GameController.UpdateFeatherCounter -= UpdateFeatherCounter;
    }

	void Start(){
        rec = lifebar.transform.GetComponent<RectTransform>();
	}

    /// <summary>
    /// Update the lifebar and life counter
    /// effectType:
    /// 0: No effect
    /// 1: Blink red
    /// 2: Blink green
    /// </summary>
    void UpdateLifebar(int currentLife, int lives, int effectType)
    {
        lifebarMask.color = normalColor;
        Sprite newMaskSprite = maskSprites[0];
        Sprite newSprite = sprites[0];

        switch (currentLife)
        {
            case 0:
                newMaskSprite = maskSprites[4];	//0% (dead)
                newSprite = sprites[4];
                break;

            case 1:
                newMaskSprite = maskSprites[3];	//25%
                newSprite = sprites[3];
                break;

            case 2:
                newMaskSprite = maskSprites[2];	//50%
                newSprite = sprites[2];
                break;

            case 3:
                newMaskSprite = maskSprites[1];	//75%
                newSprite = sprites[1];
                break;

            case 4:
                newMaskSprite = maskSprites[0]; //100%
                newSprite = sprites[0];
                break;
        }
        lifebarMask.sprite = newMaskSprite;
        lifebar.sprite = newSprite;
        StartCoroutine(Blink(effectType));

        livesRemaining.text = "x " + lives;
	}

    /// <summary>
    /// Update the feather counter
    /// </summary>
    void UpdateFeatherCounter(int featherCount, int maxFeathers)
    {
        featherCounter.text = "x " + featherCount + " / " + maxFeathers;
    }

    /// <summary>
    /// Have the lifebar blink
    /// 1: Red
    /// 2: Green
    /// </summary>
    IEnumerator Blink(int col)
    {
        if (col == 1)
        {
            lifebarMask.color = redColor;
        }
        else if (col == 2)
        {
            lifebarMask.color = greenColor;
        }
        yield return new WaitForSeconds(blinkTime);
        lifebarMask.color = normalColor;
    }
}
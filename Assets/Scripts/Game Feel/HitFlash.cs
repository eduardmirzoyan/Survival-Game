using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFlash : MonoBehaviour
{
    [Tooltip("Material to switch to during the flash.")]
    [SerializeField] private Material flashMaterial;

    [Tooltip("Duration of the flash.")]
    [SerializeField] private float duration;
    [SerializeField] private bool freezeTime;

    // The SpriteRenderer that should flash.
    private SpriteRenderer spriteRenderer;

    // The material that was in use, when the script started.
    private Material originalMaterial;
    private Color originalColor;

    // The currently running coroutine.
    private Coroutine flashRoutine;


    private void Start()
    {
        // Get the SpriteRenderer to be used,
        // alternatively you could set it from the inspector.
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        // Get the material that the SpriteRenderer uses, 
        // so we can switch back to it after the flash ended.
        originalMaterial = spriteRenderer.material;
        originalColor = spriteRenderer.color;
    }

    public void Flash()
    {
        // If the flashRoutine is not null, then it is currently running.
        if (flashRoutine != null)
        {
            // In this case, we should stop it first.
            // Multiple FlashRoutines the same time would cause bugs.
            StopCoroutine(flashRoutine);
        }

        // Start the Coroutine, and store the reference for it.
        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        // Swap to the flashMaterial.
        // spriteRenderer.material = flashMaterial;
        spriteRenderer.color = Color.white;

        // Freeze time
        if (freezeTime) Time.timeScale = 0f;

        // Pause the execution of this function for "duration" seconds.
        yield return new WaitForSecondsRealtime(duration);

        // Resume time
        if (freezeTime) Time.timeScale = 1f;

        // After the pause, swap back to the original material.
        // spriteRenderer.material = originalMaterial;
        spriteRenderer.color = originalColor;

        // Set the routine to null, signaling that it's finished.
        flashRoutine = null;
    }
}
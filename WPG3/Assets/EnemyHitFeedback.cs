using UnityEngine;
using System.Collections;

public class EnemyHitFeedback : MonoBehaviour
{
    private Renderer enemyRenderer;

    private Color originalColor;

    public float flashDuration = 0.1f;

    private void Start()
    {
        // Cari renderer di child object
        enemyRenderer = GetComponentInChildren<Renderer>();

        if (enemyRenderer != null)
        {
            originalColor = enemyRenderer.material.color;
        }
        else
        {
            Debug.LogWarning("Renderer tidak ditemukan!");
        }
    }

    public void TakeHit()
    {
        if (enemyRenderer != null)
        {
            StopAllCoroutines();
            StartCoroutine(FlashRed());
        }
    }

    IEnumerator FlashRed()
    {
        enemyRenderer.material.color = Color.red;

        yield return new WaitForSeconds(flashDuration);

        enemyRenderer.material.color = originalColor;
    }
}   
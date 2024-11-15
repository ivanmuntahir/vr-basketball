using UnityEngine;
using System.Collections;

public class MaterialBlinking : MonoBehaviour
{
    public GameObject[] targetObjects; // Array of target GameObjects
    private Renderer[] objectRenderers; // Array to store renderers
    private Material[][] originalMaterials; // 2D array for original materials
    private Material[][] blinkingMaterials; // 2D array for blinking materials
    private Coroutine blinkingCoroutine; // Coroutine reference for control

    public Color blinkColor = Color.yellow;
    public float blinkDuration = 0.5f;

    public void StartBlinking()
    {
        if (targetObjects.Length > 0)
        {
            objectRenderers = new Renderer[targetObjects.Length];
            originalMaterials = new Material[targetObjects.Length][];
            blinkingMaterials = new Material[targetObjects.Length][];

            for (int j = 0; j < targetObjects.Length; j++)
            {
                GameObject targetObject = targetObjects[j];
                objectRenderers[j] = targetObject.GetComponent<Renderer>();

                // Pastikan renderer tidak null
                if (objectRenderers[j] != null)
                {
                    originalMaterials[j] = objectRenderers[j].materials;
                    blinkingMaterials[j] = new Material[originalMaterials[j].Length];

                    for (int i = 0; i < originalMaterials[j].Length; i++)
                    {
                        blinkingMaterials[j][i] = new Material(originalMaterials[j][i]);
                        blinkingMaterials[j][i].color = blinkColor;
                    }
                }
                else
                {
                    Debug.LogError($"Renderer tidak ditemukan pada targetObject: {targetObject.name}!");
                }
            }

            // Hentikan coroutine yang mungkin sedang berjalan
            if (blinkingCoroutine != null)
            {
                StopCoroutine(blinkingCoroutine);
            }

            blinkingCoroutine = StartCoroutine(Blink());
        }
        else
        {
            Debug.LogError("TargetObjects belum diatur!");
        }
    }

    private IEnumerator Blink()
    {
        while (true) // Loop selamanya sampai dihentikan
        {
            // Set blinking materials
            for (int j = 0; j < targetObjects.Length; j++)
            {
                if (objectRenderers[j] != null)
                {
                    objectRenderers[j].materials = blinkingMaterials[j];
                }
            }
            yield return new WaitForSeconds(blinkDuration);

            // Set original materials
            for (int j = 0; j < targetObjects.Length; j++)
            {
                if (objectRenderers[j] != null)
                {
                    objectRenderers[j].materials = originalMaterials[j];
                }
            }
            yield return new WaitForSeconds(blinkDuration);
        }
    }

    public void StopBlinking()
    {
        if (blinkingCoroutine != null)
        {
            StopCoroutine(blinkingCoroutine);
            blinkingCoroutine = null;
        }

        // Kembalikan semua objek ke material asli
        for (int j = 0; j < targetObjects.Length; j++)
        {
            if (objectRenderers[j] != null)
            {
                objectRenderers[j].materials = originalMaterials[j];
            }
        }
    }
}

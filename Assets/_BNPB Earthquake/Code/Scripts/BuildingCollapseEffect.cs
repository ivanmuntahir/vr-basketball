using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingCollapseEffect : MonoBehaviour
{
    public List<GameObject> buildings;
    public float duration = 2.0f;
    public float collapseDistance = 10.0f;
    public List<AudioClip> collapseSounds;

    public void StartBuildingCollapse()
    {
        StartCoroutine(BuildingCollapseSequence());
    }

    private IEnumerator BuildingCollapseSequence()
    {
        foreach (GameObject building in buildings)
        {
            StartCoroutine(CollapseBuilding(building));
            yield return new WaitForSeconds(duration);
        }
    }

    private IEnumerator CollapseBuilding(GameObject building)
    {
        Vector3 initialPosition = building.transform.position;
        Vector3 targetPosition = initialPosition - new Vector3(0, collapseDistance, 0);

        float elapsedTime = 0;

        // Play a random collapse sound if the building has an AudioSource
        AudioSource audioSource = building.GetComponent<AudioSource>();
        if (audioSource != null && collapseSounds.Count > 0)
        {
            AudioClip randomClip = collapseSounds[Random.Range(0, collapseSounds.Count)];
            audioSource.PlayOneShot(randomClip);
        }

        while (elapsedTime < duration)
        {
            building.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        building.transform.position = targetPosition;
    }
}

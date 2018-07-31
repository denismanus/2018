using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	// Use this for initialization
    public IEnumerator Shake(float magnitude, float duration)
    {
        Vector3 originalPos = transform.position;

        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(originalPos.x+=x, originalPos.y+=y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;
    }
}

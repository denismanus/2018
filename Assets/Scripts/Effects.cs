using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Effects : MonoBehaviour {

    public Image curtain;

	void Start () {
		
	}

    public void FadeCurtain(bool direction, float time, StaticData.MethodToDelegate someFunction)
    {
        if (direction)
        {
            StartCoroutine(FadeIn(time, someFunction));
        }
        else
        {
            StartCoroutine(FadeOut(time, someFunction));
        }
    }

    IEnumerator FadeIn(float time, StaticData.MethodToDelegate someFunction)
    {
        float t = 1f;
        while (t > 0)
        {
            t -= Time.deltaTime / time;
            curtain.color = new Color(0f, 0f, 0f, t);
            yield return 0;
        }
        if (someFunction != null)
        {
            someFunction.Invoke();
        }
    }

    IEnumerator FadeOut(float time, StaticData.MethodToDelegate someFunction)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / time;
            curtain.color = new Color(0f, 0f, 0f, t);
            yield return 0;
        }
        if (someFunction != null)
        {
            someFunction.Invoke();
        }

    }

}

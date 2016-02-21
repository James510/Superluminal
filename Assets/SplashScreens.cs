using UnityEngine;
using System.Collections;

public class SplashScreens : MonoBehaviour
{
    public Sprite[] sprite;
    public float delay;
    public int nextLevel;
    private bool flag=false;
    private float transparency = 0;
	// Use this for initialization
	void Start ()
    {
        StartCoroutine("FadeNext", delay);
	}
    void Update()
    {
        GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 1.0f, transparency);
        if (flag)
            transparency += 0.1f;
        else
            transparency -= 0.1f;

        if (transparency > 1)
            transparency = 1;
        if (transparency < 0)
            transparency = 0;

    }

    IEnumerator FadeNext(float f)
    {
        yield return new WaitForSeconds(f);
        flag = true;
        yield return new WaitForSeconds(f);
        flag = false;
        yield return new WaitForSeconds(f);
        GetComponent<SpriteRenderer>().sprite = sprite[1];
        flag = true;
        yield return new WaitForSeconds(f);
        flag = false;
        yield return new WaitForSeconds(f);
        Application.LoadLevel(nextLevel);
    }
}

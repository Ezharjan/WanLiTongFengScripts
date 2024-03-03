using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInitMove : MonoBehaviour
{
    public GameObject lianzi;
    public GameObject mask;
    public float OriginPos;
    public float TargetPos;
    void Start()
    {
        OriginPos = lianzi.transform.localPosition.y;
        Time.timeScale = 0;
        StartCoroutine("movelianzi");
    }

    void Update()
    {
        
    }

    IEnumerator movelianzi()
    {
        for (int i = 0; i < 100; i++)
        {
            float x = i / 100f;
            lianzi.transform.localPosition = new Vector2(transform.position.x, Mathf.Lerp(OriginPos, TargetPos, x));
            mask.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, Mathf.Lerp(1, 0, x));
            yield return new WaitForSecondsRealtime(0.01f);
        }
        Time.timeScale = 1;
        yield break;
    }
}

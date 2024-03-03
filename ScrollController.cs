using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{
    public Button[] Scrolls;
    public float TargetY = 380f;
    public float OriginY = 80f;

    public float speed = 1f;
    public AnimationCurve CurveSpeed;

    void Start()
    {


        //StartCoroutine("ExpandScroll");
        //StartCoroutine("RollScroll");
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.A)) StartCoroutine("RollScroll");
        if (Input.GetKey(KeyCode.S)) StartCoroutine("ExpandScroll");
    }

    public void RollScroll()
    {
        AudiosController.ScrollUp();
        StartCoroutine("rollScroll");
    }
    public void ExpandScroll()
    {
        AudiosController.ScrollDown();
        StartCoroutine("expandScroll");
    }


    // 展开
    IEnumerator rollScroll()
    {
        float x = 0;
        Button btn1 = Scrolls[0];
        Button btn2 = Scrolls[1];
        RectTransform trans1 = btn1.GetComponent<RectTransform>();
        RectTransform trans2 = btn2.GetComponent<RectTransform>();
        while (trans1.localPosition.y < TargetY || trans2.localPosition.y < TargetY)
        {
            x += Time.deltaTime;
            trans1.Translate(Vector3.up * speed * CurveSpeed.Evaluate(x));
            trans2.Translate(Vector3.up * speed * CurveSpeed.Evaluate(x));
            yield return null;
        }
        GameObject.Find("EventController").GetComponent<EventController>().isWaitForNext =true;
    }

    // 卷起
    IEnumerator expandScroll()
    {
        float x = 0;        
        Button btn1 = Scrolls[0];
        Button btn2 = Scrolls[1];
        RectTransform trans1 = btn1.GetComponent<RectTransform>();
        RectTransform trans2 = btn2.GetComponent<RectTransform>();
        while (trans1.localPosition.y > OriginY || trans2.localPosition.y > OriginY)
        {
            x += Time.deltaTime;
            trans1.Translate(Vector3.down * speed * 0.1f);
            trans2.Translate(Vector3.down * speed * 0.1f);
            yield return null;
        }
    }
}

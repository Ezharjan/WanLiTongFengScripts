using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject bian;
    public GameObject lianzi;
    public Image mask;
    public float OriginPos;
    public float TargetPos;

    public float bianOrigin;
    public float bianTarget;
    private void Start()
    {
        OriginPos = lianzi.transform.localPosition.y;
        bianOrigin = bian.transform.localPosition.y;
    }
    public void QuitGame()
    {
//        #if UNITY_EDITOR
//            UnityEditor.EditorApplication.isPlaying = false;
//#else
		    StartCoroutine("quit");
//#endif
    }

    public void StartGame()
    {
        //AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        ////阻止当加载完成自动切换  
        //operation.allowSceneActivation = false;
        //operation.allowSceneActivation = true;
        StartCoroutine("movelianzi");
        //SceneManager.LoadScene(1);
        GameManager.isFail = false;
        GameManager.isWin = false;
    }

    IEnumerator movelianzi()
    {
        for(int i=0;i<100;i++ )
        {
            float x = i / 100f;
            lianzi.transform.localPosition = new Vector2(transform.position.x, Mathf.Lerp(OriginPos, TargetPos, x));
            bian.transform.localPosition = new Vector2(transform.position.x, Mathf.Lerp(bianOrigin, bianTarget, x));
            mask.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, x));
            yield return new WaitForSeconds(0.01f);
        }
        SceneManager.LoadScene(1);
        yield break;
    }

    IEnumerator quit()
    {
        for (int i = 0; i < 100; i++)
        {
            float x = i / 100f;
            lianzi.transform.localPosition = new Vector2(transform.position.x, Mathf.Lerp(OriginPos, TargetPos, x));
            bian.transform.localPosition = new Vector2(transform.position.x, Mathf.Lerp(bianOrigin, bianTarget, x));
            mask.color = new Color(0, 0, 0, Mathf.Lerp(0, 1, x));
            yield return new WaitForSeconds(0.01f);
        }
        Application.Quit();
        yield break;
    }


}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    public class Config {
        public int EventCount;
        public double SpecialEventRate;
        public double UpFactor;
        public double DownFactor;
        public int StartHP;
    }
    public GameObject[] FailImages;
    public GameObject WinImage;
    public static bool isFail;
    public static bool isWin;

    public GameObject[] Sliders;
    public static Config GameConfig;
    public Image winmask;
    public Image losemask;

    bool isPlaying;
    void Awake () {
        string ConfigJsonString = File.ReadAllText (Application.dataPath + "\\Resources\\config.json");
        GameConfig = JsonMapper.ToObject<Config> (ConfigJsonString);
        GameObject.Find ("ProgressBar").GetComponent<Slider> ().maxValue = GameConfig.EventCount;
        EventCreator.SerialEventRate = (float) GameConfig.SpecialEventRate;
        EventCreator.UpFactor = (float) GameConfig.UpFactor;
        EventCreator.DownFactor = (float) GameConfig.DownFactor;
        EventController.StartHP = GameConfig.StartHP;

        isFail = false;
        isWin = false;
        Player.ProgressValue = 0;

    }

    void Update () {
        if (Player.People <= 0 && isFail == false) {
            StartCoroutine ("PeopleFail");
        }
        if (Player.Policy <= 0 && isFail == false) {
            StartCoroutine ("PolicyFail");
        }
        if (Player.Economic <= 0 && isFail == false) {
            StartCoroutine ("EconomicFail");
        }
        if (Player.Military <= 0 && isFail == false) {
            StartCoroutine ("MilitaryFail");
        }
        if (isFail || isWin) {
            if (Input.GetMouseButton (0)) {
                SceneManager.LoadScene (0);
            }
        }

        if (Player.ProgressValue >= GameConfig.EventCount && EventCreator.NextId == -1 && isWin == false) {
            AudiosController.GameWin ();
            StartCoroutine ("Win");
        }

    }

    #region GameOver
    IEnumerator PeopleFail () {
        Debug.Log ("1");
        for (int i = 0; i < 100; i++) {
            float x = i / 100f;
            losemask.color = new Color (0, 0, 0, x);
            yield return new WaitForSeconds (0.01f);
        }
        FailImages[0].SetActive (true);
        isFail = true;
        AudiosController.GameOver ();
        yield break;
    }
    IEnumerator PolicyFail () {
        Debug.Log ("2");
        for (int i = 0; i < 100; i++) {
            float x = i / 100f;
            losemask.color = new Color (0, 0, 0, x);
            yield return new WaitForSeconds (0.01f);
        }
        isFail = true;
        FailImages[1].SetActive (true);
        AudiosController.GameOver ();
        yield break;
    }
    IEnumerator EconomicFail () {
        Debug.Log ("3");
        for (int i = 0; i < 100; i++) {
            float x = i / 100f;
            losemask.color = new Color (0, 0, 0, x);
            yield return new WaitForSeconds (0.01f);
        }
        isFail = true;
        FailImages[2].SetActive (true);
        AudiosController.GameOver ();
        yield break;

    }
    IEnumerator MilitaryFail () {
        Debug.Log ("4");
        for (int i = 0; i < 100; i++) {
            float x = i / 100f;
            losemask.color = new Color (0, 0, 0, x);
            yield return new WaitForSeconds (0.01f);
        }
        isFail = true;
        FailImages[3].SetActive (true);
        AudiosController.GameOver ();
        yield break;

    }
    #endregion

    #region
    IEnumerator Win () {
        Debug.Log ("5");
        for (int i = 0; i < 100; i++) {
            float x = i / 100f;
            winmask.color = new Color (1, 1, 1, x);
            print (x);
            yield return new WaitForSeconds (0.01f);
        }
        isWin = true;
        WinImage.SetActive (true);
        yield break;
    }
    #endregion

}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class EventController : MonoBehaviour/*事件控制器*/
{

    public TextMeshProUGUI ShowBox;
    public TextMeshProUGUI OptionAText;
    public TextMeshProUGUI OptionBText;
    public GameObject DoorLeft;
    public GameObject DoorRight;
    public Slider ProgressBar;
    public float DoorSpeed = 10f;
    public AnimationCurve ProgressBarEase;
    public AnimationCurve MaskCurve;
    public GameObject Mask;

    public static GameEvent currentEvent;//当前的事件
    private int currentEventID = 0;//当前事件ID号



    public GameObject ChracaterObj;
    public Sprite[] CharacterSprites;

    public bool isWaitForNext;

    public static int StartHP;



    private void Awake()
    {
        Player.SetAllValue(StartHP,StartHP,StartHP,StartHP);
        //Player.Economic = Player.People = Player.Policy = Player.Military = 80;
    }

    private void Start()
    {
        ShowNewEvent();
    }
    

    public void ShowNewEvent()
    {
        currentEvent = EventCreator.GetGameEvent();//获取事件
        AudiosController.ChangeEvent(currentEvent.ImageIndex);
        ShowBox.text = currentEvent.ShowText;
        OptionAText.text = currentEvent.OptionTextA;
        OptionBText.text = currentEvent.OptionTextB;
        ChracaterObj.GetComponent<SpriteRenderer>().sprite = CharacterSprites[currentEvent.ImageIndex];
        Camera.main.GetComponent<ScrollController>().ExpandScroll();

    }
    public void ShowResult(string buttonName)
    {
        Camera.main.GetComponent<ScrollController>().RollScroll();
        if (buttonName == "A")
            ShowBox.text = currentEvent.ResultTextA;
        else
            ShowBox.text = currentEvent.ResultTextB;
        //isWaitForNext = true;

        Player.ProgressValue++;
        UpdateProgressBar();
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.D))
        //{
        //    MoveDoor();
        //}
        if (isWaitForNext && Input.GetMouseButtonDown(0))//当鼠标按下后抬起
        {
            isWaitForNext = false;
            MoveDoor();
            Invoke("ShowNewEvent", 0.8f);

        }
    }

    void MoveDoor()
    {
        AudiosController.DoorMove();
        StartCoroutine("DoorAnimation");
    }
    void UpdateProgressBar()
    {
        StartCoroutine("ProgressBarAnimation");
    }

    IEnumerator DoorAnimation()
    {
        float leftOrigin = -10f;
        float leftTarget = -6.2f;
        float rightOrigin = -1.4f;
        float rightTarget = -2.4f;
        float x = 0;
        while(DoorLeft.transform.localPosition.x<leftTarget||DoorRight.transform.localPosition.x>rightTarget)
        {
            DoorLeft.transform.Translate(Vector2.right * DoorSpeed*Time.deltaTime);
            DoorRight.transform.Translate(Vector2.left * DoorSpeed*Time.deltaTime);
            Mask.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, MaskCurve.Evaluate(x));
            x += Time.deltaTime;
            yield return null;
        }

        //Mask.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);

        while (DoorLeft.transform.localPosition.x>leftOrigin||DoorRight.transform.localPosition.x<rightOrigin)
        {
            DoorLeft.transform.Translate(Vector2.left * DoorSpeed * Time.deltaTime);
            DoorRight.transform.Translate(Vector2.right * DoorSpeed * Time.deltaTime);
            Mask.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, MaskCurve.Evaluate(x));
            x += Time.deltaTime;

            yield return null;
        }
    }

    IEnumerator ProgressBarAnimation()
    {
        float _from = ProgressBar.value;
        float _to = ProgressBar.value+1;
        int _time = 50;
        for(int i =0;i<_time;i++)
        {
            float x = (float)i / (float)_time;
            ProgressBar.value = Mathf.Lerp(_from, _to, ProgressBarEase.Evaluate(x));
            yield return null;
        }
    }
}

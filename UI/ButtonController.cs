using TMPro;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 按钮控制器
/// </summary>
public class ButtonController : MonoBehaviour {
    private void Update () {
        //if (!EventController.eventUpdated)//当事件需要被更新
        //{
        //    buttonAText.text = buttonBText.text = null;//清空按钮选项
        //}
    }
    public void OnClick (string button) {
        GetComponent<EventController> ().ShowResult (button);
        if (EventCreator.inSeries) {
            //设置下一事件
            if (button == "A")
                EventCreator.NextId = EventCreator.CurrentEvent.NextID_A;
            if (button == "B")
                EventCreator.NextId = EventCreator.CurrentEvent.NextID_B;
        }
    }
}
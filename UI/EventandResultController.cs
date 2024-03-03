using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// <summary>
/// 事件和结果显示控制器
/// </summary>
public class EventandResultController : MonoBehaviour
{
    public TextMeshProUGUI currentEventandResult;//当前的事件及结果
    public static bool resultShowed = false;//结果是否已经展示

    /// <summary>
    /// 更新事件文本
    /// </summary>
    public void UpdateEventText()
    {
        currentEventandResult.text = EventController.currentEvent.ShowText;//更新事件文本
    }

    /// <summary>
    /// 更新结果文本
    /// </summary>
    /// <param name="button">选择的按钮是A还是B</param>
    public void UpdateResultText(string button)
    {
        currentEventandResult.text = (button == "A") ? EventController.currentEvent.ResultTextA : EventController.currentEvent.ResultTextB;//显示选择结果的文本
        resultShowed = true;//设定结果已展示
    }
}
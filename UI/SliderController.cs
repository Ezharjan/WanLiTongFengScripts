using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 数值条控制器
/// </summary>
public class SliderController : MonoBehaviour {

    public Slider[] Sliders; //四个数值条
    private GameEvent.Changes chosenButton; //选择的按钮
    private int[] targetValue = new int[4]; //需要到达的数值
    private float time; //关于速度曲线的时间点
    private bool targetValueReached = true; //是否到达了规定数值
    public AnimationCurve speed; //速度曲线
    private enum Value { people, economic, military, policy } //关于数值的枚举

 void Update () {
 UpdateSlider (Sliders[0], Player.People);
 UpdateSlider (Sliders[1], Player.Policy);
 UpdateSlider (Sliders[2], Player.Economic);
 UpdateSlider (Sliders[3], Player.Military);
    }

    // void UpdateSlider (Slider slider, float value) {
    //     if (Mathf.Abs (value - slider.value) < 1) {
    //         slider.value = value;
    //     } else {
    //         slider.value += (value - slider.value) * 0.1f * speed.Evaluate (Time.time);
    //     }
    // }

    private void UpdateSlider () {
        if (!targetValueReached) //当没有到达目标数值
        {
            time += Time.deltaTime; //速度曲线时间递增

            targetValueReached = true; //假定数值到达要求
            foreach (var slider in sliders) //使数值条进行变化，当数值没有到达指定值则进行变化并设定数值没有到达要求
            {
                switch (slider.name) {
                    case "People":
                        if (Mathf.Abs (slider.value - targetValue[(int) Value.people]) > 1) {
                            float _factor = Mathf.Abs (slider.value - targetValue[(int) Value.people]) / 10f;
                            slider.value += (slider.value < targetValue[(int) Value.people]) ? speed.Evaluate (time) * _factor : -speed.Evaluate (time) * _factor;
                            targetValueReached = false;
                        } else {
                            slider.value = targetValue[(int) Value.people];
                        }
                        break;
                    case "Economic":
                        if (Mathf.Abs (slider.value - targetValue[(int) Value.economic]) > 1) {
                            float _factor = Mathf.Abs (slider.value - targetValue[(int) Value.economic]) / 10f;
                            slider.value += (slider.value < targetValue[(int) Value.economic]) ? speed.Evaluate (time) * _factor : -speed.Evaluate (time) * _factor;
                            targetValueReached = false;
                        } else {
                            slider.value = targetValue[(int) Value.economic];
                        }
                        break;
                    case "Military":
                        if (Mathf.Abs (slider.value - targetValue[(int) Value.military]) > 1) {
                            print (Mathf.Abs (slider.value - targetValue[(int) Value.military]));
                            float _factor = Mathf.Abs (slider.value - targetValue[(int) Value.military]) / 10f;
                            slider.value += (slider.value < targetValue[(int) Value.military]) ? speed.Evaluate (time) * _factor : -speed.Evaluate (time) * _factor;
                            targetValueReached = false;
                        } else {
                            slider.value = targetValue[(int) Value.military];
                        }
                        break;
                    case "Policy":
                        if (Mathf.Abs (slider.value - targetValue[(int) Value.policy]) > 1) {
                            float _factor = Mathf.Abs (slider.value - targetValue[(int) Value.policy]) / 10f;
                            slider.value += (slider.value < targetValue[(int) Value.policy]) ? speed.Evaluate (time) * _factor : -speed.Evaluate (time) * _factor;
                            targetValueReached = false;
                        } else {
                            slider.value = targetValue[(int) Value.policy];
                        }
                        break;
                }
            }
        }
    }

    public void RecordValue (string button) {
        chosenButton = (button == "A") ? EventController.currentEvent.Changes_A : EventController.currentEvent.Changes_B; //记录选择情况

        Player.ChangeAllValue (chosenButton.people, chosenButton.policy, chosenButton.military, chosenButton.economic); //改变并记录数值

        /*记录数值的改动*/
        targetValue[(int) Value.people] = Player.People;
        targetValue[(int) Value.economic] = Player.Economic;
        targetValue[(int) Value.military] = Player.Military;
        targetValue[(int) Value.policy] = Player.Policy;

        targetValueReached = false; //设定数值条没有到达指定数值
        time = 0; //进行数值曲线时间的重置
    }
}
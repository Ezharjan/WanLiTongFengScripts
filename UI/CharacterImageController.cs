using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 角色图片控制器
/// </summary>
public class CharacterImageController : MonoBehaviour
{
    [Header("人物图片数组")] public Sprite[] images;
    public static bool imageUpdated = true;//人物图片是否已经更新
    [Header("门的动画控制器")] public Animator doorAnimator;

    private void Update()
    {
        if (doorAnimator.GetFloat("Time") > 0.5f && doorAnimator.GetFloat("Time") < 0.6f)//门的动画播放一半时
        {
            if (!imageUpdated)//如果没有更新人物图片
            {
                UpdateImage();
                imageUpdated = true;//设定已更新人物图片
            }
        }
    }

    /// <summary>
    /// 更新人物图片
    /// </summary>
    public void UpdateImage()
    {
        GetComponent<Image>().sprite = images[EventController.currentEvent.ImageIndex];//更新人物图片
    }
}
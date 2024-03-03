using UnityEngine;
public class DoorController : MonoBehaviour
{
    private float time = 0;//动画播放的时间
    private Animator animator;//门的动画控制器
    [Header("动画播放速度")] public float animatorSpeed;//动画播放速度

    private void Start()
    {
        animator = GetComponent<Animator>();//获取动画控制器
    }

    private void Update()//每帧更新的部分
    {
        if (animator.enabled)//如果需要播放动画，则对动画进行播放
        {
            time += animatorSpeed;
            animator.SetFloat("Time", time);
        }
        else
        {
            time = 0;//重置动画时间
        }
    }
}
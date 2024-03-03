using UnityEngine;

[RequireComponent (typeof (AudioSource))]
/// <summary>
/// 声音控制器
/// </summary>
public class AudiosController : MonoBehaviour {
    /// <summary>
    /// 背景音乐播放器
    /// </summary>
    public AudioSource BGMaudioSource;
    private static AudioSource _BGMaudioSource;

    private void Start () {
        PlayBGM ();
    }
    /// <summary>
    /// 背景音乐
    /// </summary>
    public AudioClip bgm;
    private static AudioClip _bgm;

    public AnimationCurve curve;
    private static AnimationCurve _curve;
    private static float time = 1;

    /// <summary>
    /// 其他音乐播放器
    /// </summary>
    public AudioSource audioSource;
    private static AudioSource _audioSource;

    /// <summary>
    /// 木门移动播放器
    /// </summary>
    public AudioSource doorAudioSource;
    private static AudioSource _doorAudioSource;

    /// <summary>
    /// 卷轴移动播放器
    /// </summary>
    public AudioSource scrollaudioSource;
    private static AudioSource _scrollaudioSource;

    /// <summary>
    /// 门移动音效
    /// </summary>
    public AudioClip doorMove;
    private static AudioClip _doorMove;

    /// <summary>
    /// 卷轴上卷的音效
    /// </summary>
    public AudioClip scrollUp;
    private static AudioClip _scrollUp;

    /// <summary>
    /// 卷轴下放的音效
    /// </summary>
    public AudioClip scrollDown;
    private static AudioClip _scrollDown;

    /// <summary>
    /// 用于区分角色的枚举
    /// </summary>
    public enum Character { primeMinister, general, noble, commonPeople, empress, foreignAmbassadors, dragon, qiongQi }

    /// <summary>
    /// 角色音效数组
    /// </summary>
    public AudioClip[] character;
    public static AudioClip[] _character;

    /// <summary>
    /// 游戏胜利的音效
    /// </summary>
    public AudioClip win;
    private static AudioClip _win;

    /// <summary>
    /// 游戏失败的音效
    /// </summary>
    public AudioClip fail;
    private static AudioClip _fail;

    private void Awake () //初始化
    {
        _BGMaudioSource = BGMaudioSource;
        _bgm = bgm;
        _curve = curve;
        _audioSource = audioSource;
        _doorAudioSource = doorAudioSource;
        _doorMove = doorMove;
        _scrollaudioSource = scrollaudioSource;
        _scrollDown = scrollDown;
        _scrollUp = scrollUp;
        _character = character;
        _win = win;
        _fail = fail;
    }

    /// <summary>
    /// 播放背景音乐，该方法应每帧运行
    /// </summary>
    public static void PlayBGM () {
        _BGMaudioSource.clip = _bgm; //设置要播放的声音为背景音乐
        if (!_BGMaudioSource.isPlaying) //当背景音乐没有在播放
        {
            _BGMaudioSource.Play (); //播放背景音乐
        }
        //if (!_audioSource.isPlaying)//当此时没有音效在播放
        //{
        //    if ((_BGMaudioSource.volume = _curve.Evaluate(time)) < 0.5f)//逐渐提高BGM的音量
        //    {
        //        time -= Time.deltaTime;
        //    }
        //}
        //else//当此时有音效在播放
        //{
        //    if ((_BGMaudioSource.volume = _curve.Evaluate(time)) > 0)//逐渐降低BGM的音量
        //    {
        //        time += Time.deltaTime;
        //    }
        //}
    }

    private static void PlayAudio (AudioClip audioClip) {
        _audioSource.clip = audioClip; //设置要播放的声音
        //if (!_audioSource.isPlaying)//当音效没有在播放
        //{
        _audioSource.Play (); //播放音效
        //}
    }

    private static void PlayDoorMove (AudioClip audioClip) {
        _doorAudioSource.clip = audioClip; //设置要播放的声音
        //if (!_audioSource.isPlaying)//当音效没有在播放
        //{
        _doorAudioSource.Play (); //播放音效
        //}
    }

    private static void PlayScrollMove (AudioClip audioClip) {
        _scrollaudioSource.clip = audioClip; //设置要播放的声音
        //if (!_audioSource.isPlaying)//当音效没有在播放
        //{
        _scrollaudioSource.Play (); //播放音效
        //}
    }

    /// <summary>
    /// 播放门移动的音效
    /// </summary>
    public static void DoorMove () {
        PlayDoorMove (_doorMove);
    }

    /// <summary>
    /// 卷轴上卷的音效
    /// </summary>
    public static void ScrollUp () {
        PlayScrollMove (_scrollUp);
    }

    /// <summary>
    /// 卷轴下放的音效
    /// </summary>
    public static void ScrollDown () {
        PlayScrollMove (_scrollDown);
    }

    /// <summary>
    /// 改变事件时的音效
    /// </summary>
    /// <param name="character">角色的索引值</param>
    public static void ChangeEvent (int character) {
        switch (character) {
            case 0:
                PlayAudio (_character[(int) Character.primeMinister]);
                break;
            case 1:
                PlayAudio (_character[(int) Character.general]);
                break;
            case 2:
                PlayAudio (_character[(int) Character.noble]);
                break;
            case 3:
                PlayAudio (_character[(int) Character.commonPeople]);
                break;
            case 4:
                PlayAudio (_character[(int) Character.empress]);
                break;
            case 5:
                PlayAudio (_character[(int) Character.foreignAmbassadors]);
                break;
            case 6:
                PlayAudio (_character[(int) Character.dragon]);
                break;
            case 7:
                PlayAudio (_character[(int) Character.qiongQi]);
                break;
            case 8:
                PlayAudio (_character[8]);
                break;
        }
    }

    /// <summary>
    /// 游戏胜利的音效
    /// </summary>
    public static void GameWin () {
        _audioSource.Pause ();
        _BGMaudioSource.volume = 0.5f;
        _BGMaudioSource.clip = _win; //设置要播放的声音为游戏胜利
        if (!_BGMaudioSource.isPlaying) //当游戏胜利的音效没有在播放
        {
            _BGMaudioSource.Play (); //播放游戏胜利的音效
        }
    }

    /// <summary>
    /// 游戏失败的音效
    /// </summary>
    public static void GameOver () {
        _audioSource.Pause ();
        _BGMaudioSource.volume = 0.5f;
        _BGMaudioSource.clip = _fail; //设置要播放的声音为游戏胜利
        if (!_BGMaudioSource.isPlaying) //当游戏胜利的音效没有在播放
        {
            _BGMaudioSource.Play (); //播放游戏胜利的音效
        }
    }
}
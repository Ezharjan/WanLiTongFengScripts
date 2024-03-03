using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using LitJson;
using UnityEngine;
using Random = UnityEngine.Random;

public class EventCreator : MonoBehaviour {

    public string JsonString;
    public static List<GameEvent> GameEvents = new List<GameEvent> (); //游戏事件池
    public static List<SerialGameEvent> SerialGameEvents = new List<SerialGameEvent> (); //连续事件池
    public static List<GameEvent> KeyEvents = new List<GameEvent> (); //关键事件池

    public static bool[] isEventUsed;
    public static bool[] isSerialEventUsed;

    public static int EventCount;
    public static int SerialEventCount;
    public static int KeyCount;

    public static float SerialEventRate = 1f; // 生成连续事件的概率

    public static int NextId = -1;
    public static bool inSeries;
    public static SerialGameEvent CurrentEvent;

    public static float UpFactor;
    public static float DownFactor;

    void Awake () {
        print (SerialEventRate);
        //生成普通事件池
        JsonString = File.ReadAllText (Application.dataPath + "\\Resources\\Events.json");
        // JsonData EventData = JsonMapper.ToObject(JsonString);
        GameEvents = JsonMapper.ToObject<List<GameEvent>> (JsonString);

        //生成连续事件的事件池
        JsonString = File.ReadAllText (Application.dataPath + "\\Resources\\SerialEvents.json");
        // JsonData SerialEventData = JsonMapper.ToObject(JsonString);
        SerialGameEvents = JsonMapper.ToObject<List<SerialGameEvent>> (JsonString);

        //生成关键事件的事件池
        if (File.Exists (Application.dataPath + "\\Resources\\KeyEvents.json")) {
            JsonString = File.ReadAllText (Application.dataPath + "\\Resources\\KeyEvents.json");
            JsonData KeyEventData = JsonMapper.ToObject (JsonString);
            KeyEvents = JsonMapper.ToObject<List<GameEvent>> (JsonString);
            KeyCount = KeyEvents.Count;
        }

        //乘上配置文件中的因数
        AddFactor ();

        EventCount = GameEvents.Count;
        SerialEventCount = SerialGameEvents.Count;

        isEventUsed = new bool[EventCount];
        isSerialEventUsed = new bool[SerialEventCount];

        foreach (GameEvent e in GameEvents) {
            if (e.Changes_A == null || e.Changes_B == null) {
                Debug.Log (e.EventID);
            }
        }
    }

    void AddFactor () {
        foreach (GameEvent e in GameEvents) {
            e.Changes_A.policy *= (int) (e.Changes_A.policy >= 0 ? UpFactor : DownFactor);
            e.Changes_A.people *= (int) (e.Changes_A.people >= 0 ? UpFactor : DownFactor);
            e.Changes_A.economic *= (int) (e.Changes_A.economic >= 0 ? UpFactor : DownFactor);
            e.Changes_A.military *= (int) (e.Changes_A.military >= 0 ? UpFactor : DownFactor);
        }
        foreach (SerialGameEvent e in SerialGameEvents) {
            e.Changes_A.policy *= (int) (e.Changes_A.policy >= 0 ? UpFactor : DownFactor);
            e.Changes_A.people *= (int) (e.Changes_A.people >= 0 ? UpFactor : DownFactor);
            e.Changes_A.economic *= (int) (e.Changes_A.economic >= 0 ? UpFactor : DownFactor);
            e.Changes_A.military *= (int) (e.Changes_A.military >= 0 ? UpFactor : DownFactor);
        }
    }

    public static GameEvent GetGameEvent () {
        if (NextId == -1) {
            inSeries = false;
        }

        if (inSeries) {
            CurrentEvent = SerialGameEvents[NextId];
            return SerialGameEvents[NextId];
        } else {
            if (Random.Range (0f, 1f) > SerialEventRate) {
                ////生成普通事件
                //int i = Random.Range(0, EventCount);
                //int tryCount = 0;
                //while (isEventUsed[i] && tryCount<=EventCount)
                //{
                //    i++;
                //    i %= EventCount;
                //    tryCount++;
                //}
                //isEventUsed[i] = true;
                //return GameEvents[i];
                return GetNormalEvent ();

            } else {
                ////生成系列事件

                //int i = Random.Range(0, SerialEventCount);
                //int tryCount = 0;
                //while (isSerialEventUsed[i] && tryCount <= SerialEventCount || SerialGameEvents[i].IsBegin ==false)
                //{
                //    i++;
                //    i %= SerialEventCount;
                //    tryCount++;
                //}
                //isSerialEventUsed[i] = true;

                //inSeries = true;
                //CurrentEvent = SerialGameEvents[i];
                //return SerialGameEvents[i];
                return GetSerialEvent ();
            }
        }

    }
    static GameEvent GetNormalEvent () {
        //生成普通事件
        int i = Random.Range (0, EventCount);
        int tryCount = 0;
        while (isEventUsed[i] && tryCount <= EventCount) {
            i++;
            i %= EventCount;
            tryCount++;
        }
        isEventUsed[i] = true;
        return GameEvents[i];
    }

    static GameEvent GetSerialEvent () {
        //生成系列事件

        int i = Random.Range (0, SerialEventCount);
        int tryCount = 0;
        while (isSerialEventUsed[i] && tryCount <= SerialEventCount || SerialGameEvents[i].IsBegin == false) {
            i++;
            i %= SerialEventCount;
            tryCount++;
            if (tryCount > SerialEventCount) {
                return GetNormalEvent ();
            }
        }
        isSerialEventUsed[i] = true;

        inSeries = true;
        CurrentEvent = SerialGameEvents[i];
        return SerialGameEvents[i];
    }
}
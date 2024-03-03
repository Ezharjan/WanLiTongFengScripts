using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent {

    public string ShowText { get; set; }
    public string ResultTextA { get; set; }
    public string ResultTextB { get; set; }

    public string OptionTextA { get; set; }
    public string OptionTextB { get; set; }

    public int EventID { get; set; }
    public int ImageIndex { get; set; }

    //public int[] Changes_A = new int[4];
    //public int[] Changes_B = new int[4];
    public Changes Changes_A;
    public Changes Changes_B;

    public class Changes {
        public int people;
        public int policy;
        public int military;
        public int economic;
    }

}

public class SerialGameEvent : GameEvent {
    public int NextID_A;
    public int NextID_B;
    public bool IsBegin;
}
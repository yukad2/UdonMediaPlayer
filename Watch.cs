//Author : Yukad2_
//MIT LICENSE
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;
using System;

public class Watch : UdonSharpBehaviour
{
    [SerializeField] private InputField watchIF1;
    [SerializeField] private InputField watchIF2;
    readonly private string[] thedays= { "Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat", };
    void Update(){
        var HHMM= DateTime.Now.Hour.ToString("00")+":"+DateTime.Now.Minute.ToString("00");
        var SS = DateTime.Now.Second.ToString("00");
        var MMDD = DateTime.Now.ToString("MM/dd");
        var DDD = thedays[(int)DateTime.Now.DayOfWeek];
        watchIF1.text = String.Format("{0} {1}",MMDD,DDD);
        watchIF2.text = String.Format("<size=36>{0}<size=8> <size=18>{1}",HHMM,SS);
        
    }
}

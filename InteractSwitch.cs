using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using UnityEngine.UI;
using System;
using VRC.Udon;
public class InteractSwitch : UdonSharpBehaviour
{
    public UdonBehaviour ObjUdon;
    public string EventStr;

    void Start(){
    }

    public override void Interact(){
        ObjUdon.SendCustomEvent(EventStr);
    }
}
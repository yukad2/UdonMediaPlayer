
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;
using System;

public class PlayerCount : UdonSharpBehaviour
{
    public InputField countif;
    int count = 0;
    public override void OnPlayerJoined(VRC.SDKBase.VRCPlayerApi player){
        count++;
        UIUpdate();
    }
    public override void OnPlayerLeft(VRC.SDKBase.VRCPlayerApi player){
        count--;
        UIUpdate();
    }
    private void UIUpdate(){
        countif.text = String.Format("現在の人数は<size=22>{0}<size=18>人",count.ToString("00"));
    }
}

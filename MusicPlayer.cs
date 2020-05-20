using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using UnityEngine.UI;
using System;
using VRC.Udon;
public class Musicrev2 : UdonSharpBehaviour
{
    [SerializeField] private AudioClip[] bgmsources;
    [SerializeField] private InputField owner;
    [SerializeField] private InputField title;
    [SerializeField] private InputField time;
    [SerializeField] private Slider timeslider;
    [SerializeField] private Slider volslider;
    [SerializeField] private Image playimg;
    [SerializeField] private Image pauseimg;
    
    [SerializeField] private AudioSource bgmplayer;

    [UdonSynced(UdonSyncMode.None)] private int _musicnumber;
    [UdonSynced(UdonSyncMode.None)] private float _musictime;
    [UdonSynced(UdonSyncMode.None)] private bool _musicisplay;
    private int musicnumber;
    private bool musicisplay;

    void Start()
    {
        //Initialize
        if (Networking.IsOwner(Networking.LocalPlayer, this.gameObject)){
            _musicnumber = 0;
            _musicisplay = false;
            _musictime  = 0.01f;
        }
        Sync();
        UIUpdate();
    }

    void FixedUpdate(){
        bgmplayer.volume = volslider.value*0.5f;
        //Owner
        if (Networking.IsOwner(Networking.LocalPlayer, this.gameObject)){
            _musictime = bgmplayer.time;
            if(bgmplayer.time==0.0f && musicisplay!=bgmplayer.isPlaying){
                Skip();
            }
        }
        UIUpdate();
    }

    public void Sync(){
        if (Networking.IsOwner(Networking.LocalPlayer, this.gameObject)){
            _musicnumber = musicnumber;
            _musicisplay = musicisplay;
            _musictime  = bgmplayer.time;
        }
        musicisplay = _musicisplay;
        musicnumber = _musicnumber;
        bgmplayer.clip = bgmsources[musicnumber];
        bgmplayer.time = _musictime;
        if(musicisplay){
            bgmplayer.Play();
        }else{
            bgmplayer.Pause();
        }
        ChangeImg();
    }
    public void Play(){
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All,"_Play");
    }

    public void _Play(){
        if (Networking.IsOwner(Networking.LocalPlayer, this.gameObject)){
            _musicisplay = !musicisplay;
        }
        if(musicisplay){
            musicisplay = false;
            bgmplayer.Pause();
        }else{
            musicisplay = true;
            bgmplayer.Play();
        }
        ChangeImg();
    }

    public void Skip(){
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All,"_Skip");
    }

    public void _Skip(){
        bgmplayer.Stop();
        musicnumber=(int)Mathf.Repeat(musicnumber+1,bgmsources.Length);
        bgmplayer.clip = bgmsources[musicnumber];
        bgmplayer.time = 0.01f;
        bgmplayer.Play();
        if (Networking.IsOwner(Networking.LocalPlayer, this.gameObject)){
            _musicnumber = musicnumber;
        }
        ChangeImg();
    }

    

    public void UIUpdate(){
        owner.text = Networking.GetOwner(this.gameObject).displayName;
        time.text = ((int)bgmplayer.time/60).ToString("00")+":"+((int)bgmplayer.time%60).ToString("00");
        title.text = bgmsources[musicnumber].name;
        timeslider.value = bgmplayer.time/bgmsources[musicnumber].length;
    }

    private void ChangeImg(){
        if(musicisplay){
            playimg.color = new Color(1,1,1,0);
            pauseimg.color = new Color(1,1,1,1);
        }else{
            playimg.color = new Color(1,1,1,1);
            pauseimg.color = new Color(1,1,1,0);
        }
    }
}

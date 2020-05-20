
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

public class SlideshowSomePics : UdonSharpBehaviour
{
    public float waittime = 5f;
    public RawImage[] imgs;
    private int cnt=0;
    private float currentTime = 0f;

    private bool iswaiting = true;
    
    void Start()
    {
        imgs[0].color = new Color(1,1,1,1);
        for(int i=1;i<imgs.Length;i++){
            imgs[i].color = new Color(1,1,1,0);
        }
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime>=waittime){
                currentTime=waittime;
        }
        if(!iswaiting){
            imgs[(int)Mathf.Repeat(cnt,imgs.Length)].color = new Color(1,1,1,1-(currentTime/waittime));
            imgs[(int)Mathf.Repeat(cnt+1,imgs.Length)].color = new Color(1,1,1,currentTime/waittime);
        }
        if(currentTime >= waittime){
            if(!iswaiting){
                cnt++;
            }
            iswaiting = !iswaiting;
            currentTime=0f;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Fade : MonoBehaviour
{
    public Image fade;
    public float durationTime=2.0f;
    public bool animating=false;
    public float alpha=0;
    public IEnumerator FadeOutIn(){
        IEnumerator fadeout=FadeOut();
        yield return fadeout;
        IEnumerator fadein=FadeIn();
        yield return fadein;
    }
    public IEnumerator FadeOut(){
        while(animating){
            yield return null;
        }
        Debug.Log("fadeOut");
        animating=true;
        float elapsedTime=0f;
        while(elapsedTime<durationTime){
            yield return null;
            elapsedTime+=Time.deltaTime;
            alpha=elapsedTime/durationTime;
            Color color=fade.color;
            color.a=alpha;
            fade.color=color;
        }
        animating=false;
    }
    public IEnumerator FadeIn(){
        while(animating){
            yield return null;
        }
        Debug.Log("fadeIn");
        animating=true;
        float elapsedTime=0f;
        alpha=1f;
        while(elapsedTime<durationTime){
            yield return null;
            elapsedTime+=Time.deltaTime;
            alpha=1f-(elapsedTime/durationTime);
            Color color=fade.color;
            color.a=alpha;
            fade.color=color;
        }
        animating=false;
    }
}

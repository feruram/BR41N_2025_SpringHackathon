using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighLight : MonoBehaviour
{
    [SerializeField]private Vector3 normal=new Vector3(1,1,1);
    [SerializeField]private Vector3 big=new Vector3(1.2f,1,1.2f);
    public void SetHighlight(){
        this.transform.GetChild(0).gameObject.SetActive(true);
        this.transform.localScale=big;
    }
    public void ClearHighlight(){
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.localScale=normal;
    }
}

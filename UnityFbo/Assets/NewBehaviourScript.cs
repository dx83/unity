using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    Material material;
    float iTime;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<RawImage>().material;
        iTime = 0f;    
    }

    // Update is called once per frame
    void Update()
    {
        iTime += Time.deltaTime;
        material.SetFloat("iTime", iTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class minusTxt : MonoBehaviour
{
    Text test;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 2, 0);
        
    }

    public void minusTimeClose()
    {
        Invoke("TimeClose", 0.5f);
    }

    public void TimeClose()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plusTimeText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(180, 400);
        Destroy(gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0,100,0) * Time.deltaTime;
    }
    //으아아 이것은 테스트 커밋을 위한 의미 없는 주석이다.
}

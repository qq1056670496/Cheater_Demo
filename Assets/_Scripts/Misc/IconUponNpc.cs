using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconUponNpc : MonoBehaviour
{
    private Transform cameraTrans;

    // Start is called before the first frame update
    void Start()
    {
        cameraTrans = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.forward = transform.position - cameraTrans.position;
    }
}

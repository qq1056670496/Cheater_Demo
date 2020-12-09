using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform player;
    private Vector3 offset;
    private bool isRotate = false;
    private float distance;
    public float ScrollSpeed = 10;
    public float RotateSpeed = 10;
    private Vector3 lookPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        lookPos = player.position + Vector3.up * 0.8f;
        offset = transform.position - lookPos;
        transform.LookAt(lookPos);
    }
    // Update is called once per frame
    void Update()
    {
        lookPos = player.position + Vector3.up * 0.8f;
        transform.position = offset +lookPos;
        //视野旋转
        RotateView();
        if (Input.GetMouseButtonUp(1))
        {
            isRotate = false;
        }
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) //在ui上不能旋转和拉
        {
            //isRotate = false;
            return;
        }
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(SetIsRotateDelay());
        }
        
        //视野远近
        ScrollView();
    }



    IEnumerator SetIsRotateDelay()
    {
        yield return new WaitForSeconds(0.02f);
        if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            isRotate = true;
    }
    void ScrollView()
    {
        distance = offset.magnitude;
        distance -= Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed;
        distance = Mathf.Clamp(distance, 3, 20);
        offset = offset.normalized * distance;
    }
    void RotateView()
    {
        
        if (isRotate)
        {
            transform.RotateAround(lookPos, Vector3.up, RotateSpeed * Input.GetAxis("Mouse X"));
            Quaternion originalRotation = transform.rotation;
            Vector3 originalPos = transform.position;
            transform.RotateAround(lookPos, transform.right, -RotateSpeed * Input.GetAxis("Mouse Y"));//竖直旋转 影响了x  的Rotation还有camera的位置
            if (transform.eulerAngles.x < 0 || transform.eulerAngles.x > 75)
            {
                transform.rotation = originalRotation;
                transform.position = originalPos;
            }
            offset = transform.position - lookPos;
        }
    }
}

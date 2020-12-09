using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIDamage : MonoBehaviour
{

    private float speed = 1.5f;

    /// <summary>
    /// 计时器
    /// </summary>
    private float timer = 0f;

    /// <summary>
    /// 销毁时间
    /// </summary>
    private float time = 0.8f;
    float _tempSize;
    private void Start()
    {
        _tempSize = this.GetComponent<Text>().fontSize;
    }
    private void Update()
    {
        Scroll();
    }

    /// <summary>
    /// 冒泡效果
    /// </summary>
    private void Scroll()
    {
        //字体滚动
        this.transform.Translate(Vector3.up * speed * Time.deltaTime/2);
        timer += Time.deltaTime/2;
        //字体缩小
        _tempSize -= 0.5f;

        this.GetComponent<Text>().fontSize = (int)_tempSize;
        
        //字体渐变透明
        this.GetComponent<Text>().color = new Color(1, 0, 0, 1 - timer);
        Destroy(gameObject, time*2);
    }

}

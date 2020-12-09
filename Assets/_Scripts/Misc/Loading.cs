using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public static Loading Instance;
    [SerializeField]private Slider _slider;
    [SerializeField]private Text _text;
    [SerializeField]private Image _img;
    private void Awake()
    {
        Instance = this;
        //_slider = GetComponentInChildren<Slider>();
        //_text = _slider.GetComponentInChildren<Text>();
        //_img = GetComponent<Image>();
    }
    public void OnLoading()
    {
        _img.enabled = true;
        _slider.gameObject.SetActive(true);
        SoundManager._instance.StopAllSounds();
        StartCoroutine(AddNum());
    }
    IEnumerator AddNum()
    {
        while (_slider.value<1f)
        {
            _slider.value += Time.deltaTime * 0.5f;
            _text.text = (_slider.value * 100).ToString("f0") + "%";
            yield return null;
        }
        _img.enabled = false;
        _slider.gameObject.SetActive(false);
        _slider.value = 0;
        _text.text = "0%";
        SoundManager._instance.PlayAudio("Bgm1", 0.2f).loop = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Fade : MonoBehaviour
{
    enum Mode
    {
        FadeIn,
        FadeOut,
    }

    [SerializeField, Header("フェードの時間")]
    private float _Fadetime;

    [SerializeField, Header("フェードの種類")]
    private Mode _Mode;

    private bool _IsFade;
    private float _FadeCount;
    private Image _FadeImage;
    private UnityEvent _OnFadeComplete = new UnityEvent();


    // Start is called before the first frame update
    void Start()
    {
        _FadeImage = GetComponent<Image>();
        switch(_Mode)
        {
            case Mode.FadeIn: _FadeCount = _Fadetime; break;
            case Mode.FadeOut: _FadeCount = 0; break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        _Fade();
    }

    private void _Fade()
    {
        if(!_IsFade) return;
        switch(_Mode)
        {
            case Mode.FadeIn: _FadeIn(); break;
            case Mode.FadeOut:_FadeOut(); break;
        }
    float alpha = _FadeCount / _Fadetime;
    _FadeImage.color = new Color(_FadeImage.color.r, _FadeImage.color.g, _FadeImage.color.b, alpha); 
    }

    private void _FadeIn()
    {
        // α値が小→透明度が上がり，通常の画面へ
        _FadeCount -= Time.deltaTime;
        if(_FadeCount <= 0)
        {
            _Mode = Mode.FadeOut;
            _IsFade = false;
            _OnFadeComplete.Invoke();
        }
    }

    private void _FadeOut()
    {
        // α値が大→透明度が下がり，画面が暗くなる
        _FadeCount += Time.deltaTime;
        if(_FadeCount >= _Fadetime)
        {
            _Mode = Mode.FadeIn;
            _IsFade = false;
            _OnFadeComplete.Invoke();
        }
    }
    //別のクラスのメソッドを任意のタイミングでInvokeするためのメソッド
    public void FadeStart(UnityAction listener)
    {
        if(_IsFade) return;
        _IsFade = true;
        _OnFadeComplete.AddListener(listener);
    }
}

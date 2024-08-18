using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    private bool _IsStart;
    private Fade _Fade;

    // Start is called before the first frame update
    void Start()
    {
        _IsStart = false;
        _Fade = FindObjectOfType<Fade>();
        _Fade.FadeStart(_TitleStart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void _TitleStart()
    {
        _IsStart = true;
    }

    private void _ChangeScene()
    {
        SceneManager.LoadScene("Main");
    }
    public void OnSpaceClick(InputAction.CallbackContext callbackContext)
    {
        if(!callbackContext.performed && _IsStart)
        {
            _Fade.FadeStart(_ChangeScene);
            _IsStart = false;
        }
    }
}

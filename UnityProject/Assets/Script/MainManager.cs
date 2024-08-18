using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    private GameObject _player;
    // Start is called before the first frame update
    void Start()
    {
        // _player = FindObjectOfType<Player>().gameObject;
        FindObjectOfType<Fade>().FadeStart(_MainStart);
        // _player.GetComponent<Player>().enebled = false;

    }
    private void _MainStart()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

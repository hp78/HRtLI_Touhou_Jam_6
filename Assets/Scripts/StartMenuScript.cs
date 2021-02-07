using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using JSAM;

public class StartMenuScript : MonoBehaviour
{
    public string nextScene;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent < Animator>();
        anim.Play("MenuMeiling");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(nextScene);
        }

    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextScene);
    }
}

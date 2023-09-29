using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button playButton;
    private float timer;
    public Camera cam;
    public GameObject positions;

    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(Play);
    }



    // Update is called once per frame
    void Play()
    {
        SceneManager.LoadScene("GameLoop");
    }

    void Loadout()
    {

    }

    IEnumerator ReturnToNormalPos()
    {
        while (cam.transform.TransformPoint(transform.position) != transform.TransformPoint(positions.transform.GetChild(0).transform.position))
        {
            cam.transform.position = Vector3.forward;
        }
        yield return null;

    }
}

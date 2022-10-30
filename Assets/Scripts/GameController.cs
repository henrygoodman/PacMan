using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject GhostText;
    [SerializeField]
    private Button ExitButton;

    void Start()
    {
        GhostText.SetActive(false);
        ExitButton.onClick.AddListener(ExitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ExitGame()
    {
        SceneManager.LoadSceneAsync("StartScene");
    }
}

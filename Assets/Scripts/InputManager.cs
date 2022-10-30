using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Button level1Button;

    private void Awake()
    {
        level1Button.onClick.AddListener(LoadLevel);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadLevel()
    {
        SceneManager.LoadSceneAsync("SampleScene");
    }
}

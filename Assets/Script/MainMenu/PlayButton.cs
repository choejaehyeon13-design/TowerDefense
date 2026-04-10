using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void GoToSelectStage()
    {
        SceneManager.LoadScene("SelectStages");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

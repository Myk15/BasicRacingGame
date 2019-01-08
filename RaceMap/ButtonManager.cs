using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonManager : MonoBehaviour {

    public bool AccelerateTrigger = false;
    public bool BreakTrigger = false;

    public GameObject Player;

    public void Accelerate()
    {
        Player.GetComponent<CarControlCS>().AccelerateTrigger = true;
        AccelerateTrigger = true;
    }

    public void Decelerate()
    {
        Player.GetComponent<CarControlCS>().AccelerateTrigger = false;
        AccelerateTrigger = false;
    }

    public void Break()
    {
        Player.GetComponent<CarControlCS>().BreakTrigger = true;
        BreakTrigger = true;
    }
    public void StopBreaking()
    {
        Player.GetComponent<CarControlCS>().BreakTrigger = false;
        BreakTrigger = false;
    }

    public void LoadScene(string Scene)
    {
        SceneManager.LoadScene(Scene,LoadSceneMode.Single);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void EnableAndDisable(GameObject Panel)
    {
        if (Panel.activeSelf == true)
        {
            Panel.SetActive(false);
        }
        else
            Panel.SetActive(true);
    }

}

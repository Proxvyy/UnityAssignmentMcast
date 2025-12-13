using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPlay : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }
}

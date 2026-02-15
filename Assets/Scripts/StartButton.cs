using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour, IPointerClickHandler
{
    [Header("Scene To Load")]
    public string sceneName = "Level_001";

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("START CLICKED on: " + gameObject.name);
        Debug.Log("Loading scene: " + sceneName);

        SceneManager.LoadScene(sceneName);
    }
}

using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjects;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter) ShowAll();
        else HideAll();
    }

    private void ShowAll()
    {
        foreach (GameObject visualGameObject in visualGameObjects)
        {
            visualGameObject.SetActive(true);
        }
    }
    private void HideAll()
    {
        foreach (GameObject visualGameObject in visualGameObjects)
        {
            visualGameObject.SetActive(false);
        }
    }
}
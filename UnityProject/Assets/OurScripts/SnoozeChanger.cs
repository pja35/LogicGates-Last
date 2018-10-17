using UnityEngine.UI;
using UnityEngine;

public class SnoozeChanger : MonoBehaviour
{

    public void Start()
    {
        ChangeColor(ParametersLoader.GetSnooze());
    }

    public void OnClick()
    {
        ParametersLoader.SetSnooze(!ParametersLoader.GetSnooze());
        Debug.Log("Snooze = " + ParametersLoader.GetSnooze());
        ChangeColor(ParametersLoader.GetSnooze());
    }

    private void ChangeColor(bool active)
    {
        if (!active)
        {
            gameObject.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            gameObject.GetComponent<Image>().color = Color.white;
        }
    }
}

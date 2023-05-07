using UnityEngine;

public class PlayerNamePreference : MonoBehaviour
{
    public string playerPreferredName = "";
    public TMPro.TMP_InputField input;

    private void Start()
    {
        playerPreferredName = "randomUser" + Random.Range(1000, 3000);
    }

    public void UpdatePreferredName()
    {
        playerPreferredName = input.text;
    }
}

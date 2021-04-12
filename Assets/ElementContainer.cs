using UnityEngine;

public class ElementContainer : MonoBehaviour
{
    private GameObject fire;
    private GameObject arcane;

    private string currentActive;

    // Start is called before the first frame update
    void Start()
    {
        fire = transform.Find("Fire").gameObject;
        arcane = transform.Find("Arcane").gameObject;
    }

    public void InvokeElement(string elementType)
    {
        switch (elementType)
        {
            case "Fire":
                fire.SetActive(true);
                currentActive = "Fire";
                break;
            case "Arcane":
                arcane.SetActive(true);
                currentActive = "Arcane";
                break;
        }
    }

    public void ResetSpellContainer()
    {
        switch (currentActive)
        {
            case "Fire":
                fire.SetActive(false);
                break;
            case "Arcane":
                arcane.SetActive(false);
                break;
        }

        currentActive = null;
    }
}

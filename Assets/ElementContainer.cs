using UnityEngine;

public class ElementContainer : MonoBehaviour
{
    public enum ElementType
    {
        fire,
        arcane,
        none,
    }

    private GameObject fire;
    private GameObject arcane;

    private ElementType currentActive;

    // Start is called before the first frame update
    void Start()
    {
        fire = transform.Find("Fire").gameObject;
        arcane = transform.Find("Arcane").gameObject;
    }

    public void InvokeElement(ElementType elementType)
    {
        switch (elementType)
        {
            case ElementType.fire:
                fire.SetActive(true);
                currentActive = ElementType.fire;
                break;
            case ElementType.arcane:
                arcane.SetActive(true);
                currentActive = ElementType.arcane;
                break;
        }
    }

    public void ResetSpellContainer()
    {
        switch (currentActive)
        {
            case ElementType.fire:
                fire.SetActive(false);
                break;
            case ElementType.arcane:
                arcane.SetActive(false);
                break;
        }
    }
}

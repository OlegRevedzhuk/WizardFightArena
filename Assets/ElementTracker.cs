using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementTracker : MonoBehaviour
{
    public ElementContainer[] elementContainers;
    public CastSpell spell;
    private int numberOfContainers;
    private int indexOfCurrentContainer = 0;

    public Dictionary<string, int> elements = new Dictionary<string, int>();
    public string primary;
    // Start is called before the first frame update
    void Start()
    {
        numberOfContainers = elementContainers.Length;
        elements.Add("Fire", 0);
        elements.Add("Arcane", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FireElement"))
            AddElement("Fire");

        else if (Input.GetButtonDown("ArcaneElement"))
            AddElement("Arcane");

        else if (Input.GetButtonDown("CastSpell"))
            CastSpell();
    }

    void AddElement(string element)
    {
        if(indexOfCurrentContainer == numberOfContainers)
        {
            //dosomething
            return;
        }
        elementContainers[indexOfCurrentContainer].InvokeElement(element);

        if (indexOfCurrentContainer == 0)
            primary = element;
        else
            ++elements[element];

        ++indexOfCurrentContainer;
    }

    void CastSpell()
    {
        spell.Cast(elements, primary);

        foreach (ElementContainer cont in elementContainers)
        {
            cont.ResetSpellContainer();
        }

        elements = new Dictionary<string, int>(numberOfContainers - 1);
        elements.Add("Fire", 0);
        elements.Add("Arcane", 0);

        indexOfCurrentContainer = 0;
    }
}

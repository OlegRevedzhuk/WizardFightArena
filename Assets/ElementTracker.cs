using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Etype = ElementContainer.ElementType;

public class ElementTracker : MonoBehaviour
{
    [SerializeField]
    private ElementContainer[] elementContainers;
    [SerializeField]
    private ChooseSpell spellPicker;

    private int numberOfContainers;
    private int indexOfCurrentContainer = 0;

    private Dictionary<Etype, int> elements;
    private Etype primary;
    // Start is called before the first frame update
    void Start()
    {
        numberOfContainers = elementContainers.Length;
        elements = new Dictionary<Etype, int>(numberOfContainers - 1);
        elements.Add(Etype.fire, 0);
        elements.Add(Etype.arcane, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FireElement"))
            AddElement(Etype.fire);

        if (Input.GetButtonDown("ArcaneElement"))
            AddElement(Etype.arcane);

        if (Input.GetButtonDown("CastSpell"))
            CastSpell();
    }

    void AddElement(Etype element)
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
        if(primary == Etype.none)
        {
            //dosomething
            return;
        }
        spellPicker.ElementsToSpell(elements, primary);

        foreach (ElementContainer cont in elementContainers)
        {
            cont.ResetSpellContainer();
        }

        primary = Etype.none;

        elements = new Dictionary<Etype, int>(numberOfContainers - 1);
        elements.Add(Etype.fire, 0);
        elements.Add(Etype.arcane, 0);

        indexOfCurrentContainer = 0;
    }
}

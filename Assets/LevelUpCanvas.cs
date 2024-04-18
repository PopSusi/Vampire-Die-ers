using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpCanvas : MonoBehaviour
{
    Abilities.EAbility option1, option2;
    GameObject panel;
    Text level, option1Item, option1Desc, option2Item, option2Desc;
    LevelUpCanvas instance;
    HashSet<Abilities.EAbility> chosen = new HashSet<Abilities.EAbility>();
    // Start is called before the first frame update
    void Awake()
    {
        instance = instance == null ? this : null;
    }
    public void GenerateAbility()
    {
        if(chosen.Count > 2)
        {
            LoadMenu();
            return;
        }
        Abilities.EAbility tempAbility = (Abilities.EAbility)Random.Range(0, 2);
        if (chosen.Contains(tempAbility))
        {
            GenerateAbility();
        } else
        {
            chosen.Add(tempAbility);
            GenerateAbility();
        }
    }

    private void LoadMenu()
    {
        Abilities[] array = EAbilityToArray();
        option1 = array[0].ability;
        option1Item.text = array[0].ability.ToString();
        option1Desc.text = array[0].description;
        option2 = array[1].ability;
        option2Item.text = array[1].ability.ToString();
        option2Desc.text = array[1].description;
    }

    private Abilities[] EAbilityToArray()
    {
        Abilities[] tempArray = new Abilities[2];
        return tempArray;
    }
    public void Option1()
    {
        ParseLibrary(option1);
    }

    public void Option2()
    {
        ParseLibrary(option2);
    }
    private void ParseLibrary(Abilities.EAbility ability)
    {
        switch (ability){
            case Abilities.EAbility.Onion:
                if(PlayerController.instance.gameObject.GetComponent<Onion>() == null)
                {
                    PlayerController.instance.gameObject.AddComponent<Onion>();
                } else
                {
                    PlayerController.instance.gameObject.GetComponent<Onion>().levelOfAbility++;
                }
                break;
            case Abilities.EAbility.Chain:
                if (PlayerController.instance.gameObject.GetComponent<Chain>() == null)
                {
                    PlayerController.instance.gameObject.AddComponent<Chain>();
                }
                else
                {
                    PlayerController.instance.gameObject.GetComponent<Chain>().levelOfAbility++;
                }
                break;
            case Abilities.EAbility.Necronomicon:
                if (PlayerController.instance.gameObject.GetComponent<Necronomicon>() == null)
                {
                    PlayerController.instance.gameObject.AddComponent<Necronomicon>();
                }
                else
                {
                    PlayerController.instance.gameObject.GetComponent<Necronomicon>().levelOfAbility++;
                }
                break;
            default:
                if (PlayerController.instance.gameObject.GetComponent<Onion>() == null)
                {
                    PlayerController.instance.gameObject.AddComponent<Onion>();
                }
                else
                {
                    PlayerController.instance.gameObject.GetComponent<Onion>().levelOfAbility++;
                }
                break;
        }
    }

}

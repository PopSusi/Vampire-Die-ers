using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpCanvas : MonoBehaviour
{
    Abilities.EAbility option1, option2;
    [SerializeField] TextMeshProUGUI level, option1Item, option1Desc, option2Item, option2Desc;
    [SerializeField] Image panel;
    public static LevelUpCanvas instance;
    HashSet<Abilities.EAbility> chosen = new HashSet<Abilities.EAbility>();
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        
    }
    public void GenerateAbility(int levelIn)
    {

        if(chosen.Count > 2)
        {
            LoadMenu(levelIn);
            return;
        }
        Abilities.EAbility tempAbility = (Abilities.EAbility)Random.Range(0, 2);
        if (chosen.Contains(tempAbility))
        {
            GenerateAbility(levelIn);
        } else
        {
            chosen.Add(tempAbility);
            GenerateAbility(levelIn);
        }
    }

    private void LoadMenu(int levelIn)
    {
        panel.gameObject.SetActive(true);
        Abilities[] array = EAbilityToArray();
        option1 = array[0].ability;
        option1Item.text = array[0].ability.ToString();
        option1Desc.text = array[0].description;
        option2 = array[1].ability;
        option2Item.text = array[1].ability.ToString();
        option2Desc.text = array[1].description;
        level.text = levelIn.ToString();
        gameObject.SetActive(true);
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
                if(PlayerController.instance.transform.TryGetComponent<Onion>(out Onion onion))
                {
                    onion.levelOfAbility++;
                } else
                {
                    Instantiate(Resources.Load<GameObject>("Assets/Abilities/Prefabs/Onion"),
                        PlayerController.instance.gameObject.transform);
                }
                break;
            case Abilities.EAbility.Chain:
                if (PlayerController.instance.transform.TryGetComponent<Chain>(out Chain chain))
                {
                    chain.levelOfAbility++;
                }
                else
                {
                    Instantiate(Resources.Load<GameObject>("Assets/Abilities/Prefabs/Chain"),
                        PlayerController.instance.gameObject.transform);
                }
                break;
            case Abilities.EAbility.Necronomicon:
                if (PlayerController.instance.transform.TryGetComponent<Necronomicon>(out Necronomicon necro))
                {
                    necro.levelOfAbility++;
                }
                else
                {
                    Instantiate(Resources.Load<GameObject>("Assets/Abilities/Prefabs/Necro"),
                        PlayerController.instance.gameObject.transform);
                }
                break;
            default:
                if (PlayerController.instance.transform.TryGetComponent<Onion>(out Onion defaultOnions))
                {
                    defaultOnions.levelOfAbility++;
                }
                else
                {
                    Instantiate(Resources.Load<GameObject>("Assets/Abilities/Prefabs/Onion"),
                        PlayerController.instance.gameObject.transform);
                }
                break;
        }
        panel.gameObject.SetActive(false);
    }

}

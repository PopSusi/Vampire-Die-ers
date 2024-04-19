using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpCanvas : MonoBehaviour
{
    Abilities option1, option2;
    [SerializeField] TextMeshProUGUI level, option1Item, option1Desc, option2Item, option2Desc;
    [SerializeField] Image panel;
    public static LevelUpCanvas instance;
    HashSet<Abilities.EAbility> chosen = new HashSet<Abilities.EAbility>();
    // Start is called before the first frame update
    void Awake()
    {
        Instance();
    }
    public void GenerateAbilities(int levelIn)
    {
        System.Array A = System.Enum.GetValues(typeof(Abilities.EAbility));
        while (chosen.Count < 2)
        {
            Abilities.EAbility tempAbility = (Abilities.EAbility)A.GetValue(Random.Range(0, A.Length));
            if (!chosen.Contains(tempAbility))
            {
                chosen.Add(tempAbility);
            }
        }
        LoadMenu(levelIn);
    }
    private void Instance()
    {
        instance = this;
        //Debug.Log("instaced " + instance.gameObject.name);
    }

    private void LoadMenu(int levelIn)
    {
        panel.gameObject.SetActive(true);
        Abilities[] array = EAbilityToArray();
        option1 = array[0];
        option1Item.text = array[0].ability.ToString();
        option1Desc.text = array[0].description;
        option2 = array[1];
        option2Item.text = array[1].ability.ToString();
        option2Desc.text = array[1].description;
        level.text = levelIn.ToString();
        gameObject.SetActive(true);
    }

    private Abilities[] EAbilityToArray()
    {
        Abilities.EAbility[] tempArray = new Abilities.EAbility[2];
        //Debug.Log(chosen.Count);
        chosen.CopyTo(tempArray);
        Abilities[] converted = new Abilities[2];
        for(int i = 0 ; i < converted.Length; i++)
        {
            converted[i] = ParseLibrary(tempArray[i]);
        }
        return converted;
    }
    public void Option1()
    {
        ParseLibrary(option1);
    }

    public void Option2()
    {
        ParseLibrary(option2);
    }

    private Abilities ParseLibrary(Abilities.EAbility ability)
    {

        switch (ability)
        {
            case Abilities.EAbility.Onion:
                return new Onion();
            case Abilities.EAbility.Chain:
                return new Chain();
            case Abilities.EAbility.Necronomicon:
                return new Necronomicon();
            default:
                return new Onion();
        }
        panel.gameObject.SetActive(false);
    }
    private void ParseLibrary(Abilities ability)
    {
        
        switch (ability.ability){
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

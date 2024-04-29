using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelUpCanvas : MonoBehaviour
{
    AbilityType option1, option2;
    [SerializeField] TextMeshProUGUI level, option1Item, option1Desc, option2Item, option2Desc;
    [SerializeField] Image panel;
    public static LevelUpCanvas instance;
    HashSet<AbilityType> chosen = new HashSet<AbilityType>();
    AbilityType[] array;
    // Start is called before the first frame update
    void Awake()
    {
        Instance();
        array = Resources.LoadAll<AbilityType>("Abilities/AbilityTypes");
        //Debug.Log(array.Length);
    }
    public void GenerateAbilities(int levelIn)
    {
        //Array A = Enum.GetValues(typeof(Abilities.EAbility));
        
        
        while (chosen.Count < array.Length)
        {
            AbilityType tempAbility = array[(Random.Range(0, array.Length))];
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
        Time.timeScale = 0f;
        panel.gameObject.SetActive(true);
        option1 = array[0];
        option1Item.text = option1.ability.ToString();
        option1Desc.text = option1.description;
        option2 = array[1];
        option2Item.text = option2.ability.ToString();
        option2Desc.text = option2.description;
        level.text = levelIn.ToString();
        gameObject.SetActive(true);
    } 

    public void Option1()
    {
        ParseChildren(option1);
    }

    public void Option2()
    {
        ParseChildren(option2);
    }
    private void ParseChildren(AbilityType ability)
    {
        PlayerController player = PlayerController.instance;
        //Debug.Log(player.AtoGO.ContainsKey(ability.ability));
        //Debug.Log(player.AtoGO.ContainsKey(Abilities.EAbility.Onion))
        if (player.AtoGO.TryGetValue(ability.ability, out GameObject GOTemp))
        {
            player.AtoGO.GetOrAdd(ability.ability, GOTemp);
            GOTemp.GetComponent<Abilities>().levelOfAbility++;
            panel.gameObject.SetActive(false);

            //Add to player collections
            player.AbilitiesSO.Add(ability.ability);

            Debug.Log("LEVELED UP " + ability.ability.ToString());
            Time.timeScale = 1f;
            return;
        }
        else
        { 
            //Instantiate
            GameObject GO = Instantiate(ability.prefab, player.transform);
            player.AtoGO.GetOrAdd(ability.ability, GO);
            player.AtoGO.TryGetValue(ability.ability, out GO);
            Debug.Log($"{GO.name} added from" +
                $" {ability.prefab.name} base" +
                $", initially {ability.ability}.");
            Time.timeScale = 1f;
        }
        
    }

}

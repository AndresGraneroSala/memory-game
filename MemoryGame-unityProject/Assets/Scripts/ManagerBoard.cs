using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ManagerBoard : MonoBehaviour
{
    [SerializeField] private GameObject prefabCard;
    [SerializeField] private Transform boardContent;
    [SerializeField] private int numTotalCards;

    [FormerlySerializedAs("cards")] [SerializeField] private List<Transform> cardsTransforms;

    public static ManagerBoard SharedInstance;

    [FormerlySerializedAs("couplesSprites")] [SerializeField] private Couple[] couplesCards;

    [SerializeField] private int coupleSelected=-1;

    public int CoupleSelected => coupleSelected;

    private void Awake()
    {
        if (SharedInstance != null)
        {
            Destroy(SharedInstance);
        }

        SharedInstance = this;
    }

    void Start()
    {
        InitBoard();
    }

    public void CompareId(int id)
    {
        if (coupleSelected >= 0)
        {
            //another selected
            if (coupleSelected == id)
            {
                print("win");
                CoupleWin(coupleSelected);
            }
            else
            {
                print("loose");
                CoupleLoose(id);

            }

            coupleSelected = -1;
        }
        else
        {
            //no selected
            coupleSelected = id;
        }
    }

    void CoupleLoose(int id)
    {
        couplesCards[id].card1.gameObject.GetComponent<EventTrigger>().enabled = true;
        couplesCards[id].card2.gameObject.GetComponent<EventTrigger>().enabled = true;        
        
        couplesCards[coupleSelected].card1.gameObject.GetComponent<EventTrigger>().enabled = true;
        couplesCards[coupleSelected].card2.gameObject.GetComponent<EventTrigger>().enabled = true;
        
        
        
        couplesCards[id].card1.gameObject.GetComponent<CardStat>().TurnDown();
        couplesCards[id].card2.gameObject.GetComponent<CardStat>().TurnDown();
        couplesCards[coupleSelected].card1.gameObject.GetComponent<CardStat>().TurnDown();
        couplesCards[coupleSelected].card2.gameObject.GetComponent<CardStat>().TurnDown();
        
        
        //couplesCards[coupleSelected].card1.gameObject.GetComponent<Image>().color= Color.white;
        //couplesCards[coupleSelected].card2.gameObject.GetComponent<Image>().color= Color.white;
        

    }
    
    void CoupleWin(int id)
    {
        Color disabled = Color.white;
        disabled.a = 0.5f;

        couplesCards[id].card1.color = disabled;
        //couplesCards[id].card1.gameObject.GetComponent<EventTrigger>().enabled=false;
        
        
        couplesCards[id].card2.color = disabled;
       // couplesCards[id].card2.gameObject.GetComponent<EventTrigger>().enabled=false;

        
        
    }
    
    
    void InitBoard()
    {
        for (int i = 0; i < numTotalCards/2; i++)
        {
            InstantiateCard(i,couplesCards[i].sp1);
            InstantiateCard(i,couplesCards[i].sp2);
            
        }

        //randomize
        foreach (var card in cardsTransforms)
        {
            card.SetSiblingIndex(Random.Range(0,cardsTransforms.Count));
        }
    }

    public void InstantiateCard(int id, Sprite sp)
    {
        GameObject card = Instantiate(prefabCard,boardContent);
        
        cardsTransforms.Add(card.transform);
        
        card.GetComponent<Image>().sprite = sp;
        card.GetComponent<CardStat>().SetID=id;

        if (couplesCards[id].card1 == null)
        {
            couplesCards[id].card1 = card.GetComponent<Image>();
        }
        else
        {
            couplesCards[id].card2 = card.GetComponent<Image>();
        }
        
        
    }
    
}

[Serializable]
public class Couple
{
    public Sprite sp1;
    public Sprite sp2;
    [HideInInspector] public Image card1;
    [HideInInspector] public Image card2;
}

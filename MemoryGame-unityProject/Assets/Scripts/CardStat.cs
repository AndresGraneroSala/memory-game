using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardStat : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private EventTrigger eventTrigger;
    [SerializeField] private RectTransform cover;

    private float _heightClose=0f;
    private float _heightOpen = 100f;
    [SerializeField] private float speedRotation;
    public int SetID
    {
        set => id = value;
    }

    public void CheckCouple()
    {
        
        
        eventTrigger.enabled = false;

        StartCoroutine(TurnUp());
        
        /*if (ManagerBoard.SharedInstance.CoupleSelected < 0)
        {
            gameObject.GetComponent<Image>().color=Color.green;
        }*/
       


    }

    
    
    IEnumerator TurnUp()
    {
        yield return CoroutineTurnUp();
        ManagerBoard.SharedInstance.CompareId(id);

    }

    private IEnumerator CoroutineTurnUp()
    {
        while (cover.sizeDelta.y > _heightClose)
        {
            cover.sizeDelta -= new Vector2(0,speedRotation) ;
            yield return new WaitForFixedUpdate();

        }
        
        cover.sizeDelta = new Vector2(100,_heightClose) ;

        
        
        yield return null;
    }
    

    public void TurnDown()
    {
        StartCoroutine(CoroutineTurnDown());
    }
    
    private IEnumerator CoroutineTurnDown()
    {
        
        while (cover.sizeDelta.y < _heightOpen)
        {
            cover.sizeDelta += new Vector2(0,speedRotation) ;
            yield return new WaitForFixedUpdate();

        }
        
        cover.sizeDelta = new Vector2(100,_heightOpen) ;

        
        
        yield return null;
    }
    
}

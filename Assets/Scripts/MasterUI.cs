using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class MasterUI : MonoBehaviour
{
    public GameObject bottomPanel;

    public GameObject[] trapsPrefabs;
    public string[] trapsNames;

    public string currentName;
    public GameObject currentTrap;
    public RawImage currentUITrapImage;

    public float distance = 10f;
    
    private GameObject currentPreviewTrap;

    private float currentMana = 265.6f;
    public float maxMana = 265.6f;

    public float manaSpeedPerTick = 0.5f;

    public RectTransform manaFieldImage;
    public GameObject manaMaxImage;
    
    /**
     * Нужно называть prefabName так же как и кнопку UI на табле
     */
    public void OnTrapsChoose(string prefabName)
    {
        if (currentPreviewTrap != null)
        {
            Destroy(currentPreviewTrap);
            currentPreviewTrap = null;
        }
        
        if (currentName != null && currentName.Equals(prefabName) && currentUITrapImage != null)
        {
            //Снимаем выделение
            currentUITrapImage.color = Color.white;
            currentName = null;
            currentUITrapImage = null;
            currentTrap = null;
        }
        else
        {
            if (currentUITrapImage != null)
            {
                currentUITrapImage.color = Color.white;
            }

            for (var i = 0; i < trapsNames.Length; i++)
            {
                if (!trapsNames[i].Equals(prefabName)) continue;
                currentTrap = trapsPrefabs[i];
                currentName = prefabName;
                currentUITrapImage = GameObject.Find(prefabName).GetComponent<RawImage>();
                currentUITrapImage.color = Color.green;
                InstantiateChoosenObject(currentTrap);
                break;
            }
        }
    }

    private void Update()
    {
        manaFieldImage.sizeDelta = new Vector2(currentMana, 12);
        manaMaxImage.SetActive(currentMana >= maxMana);


        if (Input.GetMouseButtonDown(0))
        {
            if (currentTrap != null)
            {
//                if (Camera.main == null) return;
//                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
//
//                if (hit.collider != null)
//                {
//                    Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
//                }
//                else
//                {
                var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3f);
//                    PhotonNetwork.Instantiate(currentTrap.name, Camera.main.ScreenToWorldPoint(mousePosition),
//                        Quaternion.identity);

                if (IsEnoughMana(currentTrap))
                {
                    var trapSetter = currentTrap.GetComponent<ITrapSetter>();
                    if (trapSetter == null)
                    {
                        trapSetter = currentTrap.GetComponentInChildren<ITrapSetter>();
                    }
                    
                    if (trapSetter.TryToInstantiateTrap(mousePosition))
                    {
                        ConsumeMana(currentTrap);
                    }
                }

//                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (currentMana + manaSpeedPerTick >= maxMana)
        {
            currentMana = maxMana;
        }
        else
        {
            currentMana += manaSpeedPerTick;
        }
    }

    private bool IsEnoughMana(GameObject currentTrap)
    {
        return currentTrap != null && currentTrap.GetComponent<TrapPrefs>() != null &&
               currentMana >= currentTrap.GetComponent<TrapPrefs>().manaCost;
    }

    private void ConsumeMana(GameObject currentTrap)
    {
        if (currentTrap != null && currentTrap.GetComponent<TrapPrefs>() != null)
        {
            currentMana -= currentTrap.GetComponent<TrapPrefs>().manaCost;
        }
    }
    private void InstantiateChoosenObject(GameObject prefabObject)
    {
        if (Camera.main == null) return;
        var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3f);

        currentPreviewTrap = Instantiate(new GameObject(), Camera.main.ScreenToWorldPoint(mousePosition), Quaternion.identity);
        
        currentPreviewTrap.AddComponent<SpriteRenderer>().sprite = prefabObject.GetComponentInChildren<SpriteRenderer>().sprite;
        currentPreviewTrap.GetComponent<SpriteRenderer>().sortingLayerName = "Traps0";
        currentPreviewTrap.AddComponent<PreviewTrap>();
    }

    public GameObject GetCurrentTrap()
    {
        return currentTrap;
    }

    public void ShowMasterPanels()
    {
        bottomPanel.SetActive(true);
    }
}

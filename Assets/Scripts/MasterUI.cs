using System;
using System.Collections;
using System.Collections.Generic;
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
    
    
    /**
     * Нужно называть prefabName так же как и кнопку UI на табле
     */
    public void OnTrapsChoose(string prefabName)
    {
        if (currentName != null && currentName.Equals(prefabName) && currentUITrapImage != null)
        {
            //Снимаем выделение
            currentUITrapImage.color = Color.white;
            currentUITrapImage = null;
            currentName = null;
            if (currentPreviewTrap != null)
            {
                Destroy(currentPreviewTrap);
                currentPreviewTrap = null;
            }
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
        if (Input.GetMouseButtonDown(0))
        {
            if (currentTrap != null)
            {
                if (Camera.main == null) return;
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit.collider != null)
                {
                    Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
                }
                else
                {
                    var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3f);
                    currentPreviewTrap = PhotonNetwork.Instantiate(currentTrap.name, Camera.main.ScreenToWorldPoint(mousePosition),
                        Quaternion.identity);
                }
            }
        }
    }

    private bool IsEnoughMana()
    {
        return true; //TODO
    }
    private void InstantiateChoosenObject(GameObject prefabObject)
    {
        if (Camera.main == null) return;
        var mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 3f);

        currentPreviewTrap = Instantiate(new GameObject(), Camera.main.ScreenToWorldPoint(mousePosition), Quaternion.identity);
        
        currentPreviewTrap.AddComponent<SpriteRenderer>().sprite = prefabObject.GetComponentInChildren<SpriteRenderer>().sprite;
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ÅúÁ¿²Ù×÷GO
/// </summary>
public class GameObjectGroup : MonoBehaviour
{
    public int currentIndex = 0;
    public List<GameObject> gameObjects= new List<GameObject>();

    private void Start()
    {
        Refreash();
    }

    public void ShowGO(int i)
    {
        currentIndex= i;
        Refreash();
    }

    private void Refreash()
    {
        for (int i = 0; i < gameObjects.Count;i++)
        {
            gameObjects[i].SetActive(i == currentIndex);
        }
    }
}

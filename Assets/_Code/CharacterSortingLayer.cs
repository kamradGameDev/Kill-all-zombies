using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSortingLayer : MonoBehaviour
{
    [SerializeField] private int sortingOrderBase = 5000;
    [SerializeField]private Renderer myRenderer;
    [SerializeField] private int offset;
    private void Start()
    {
        myRenderer = this.GetComponent<Renderer>();
    }
    private void LateUpdate()
    {
        myRenderer.sortingOrder = (int)(sortingOrderBase - this.transform.position.y - offset);
    }
}

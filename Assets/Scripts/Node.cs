using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;

    private Color startColor;
    private Renderer materialRenderer;
    
    private void OnMouseEnter()
    {
        materialRenderer.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        materialRenderer.material.color = startColor;
    }

    // Start is called before the first frame update
    void Awake()
    {
        materialRenderer = GetComponent<Renderer>();
        startColor = materialRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

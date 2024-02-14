using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WorldSpaceText : MonoBehaviour
{
    [SerializeField] private bool useLifeTime;
    [SerializeField] private float lifeTime;
    [SerializeField] private float movementSpeed;

    private float currentLifeTime;

    private static Camera mCam;
    private TextMeshProUGUI myText;
    
    private void Awake()
    {
        myText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        mCam = Camera.main;
    }

    public void ChangeText(string newText)
    {
        myText.text = newText;
    }

    private void Update()
    {
        transform.rotation = mCam.transform.rotation;
        transform.Translate(0, movementSpeed * Time.deltaTime, 0);

        if (!useLifeTime) return;
        
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime > lifeTime) Destroy(gameObject);
    }
}
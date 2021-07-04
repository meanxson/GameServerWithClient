using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] private Client _clientManager;
    
    private Button _button;
    private InputField _inputField;

    private void Awake() => _button = GetComponentInChildren<Button>();

    private void Start() => _button.onClick.AddListener(OnStartConnect);

    private void OnStartConnect() => _clientManager.ConnectedToServer();
}

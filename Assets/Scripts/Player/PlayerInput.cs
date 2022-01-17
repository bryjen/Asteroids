// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player/Player Input.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Player Input"",
    ""maps"": [
        {
            ""name"": ""Basic Movement"",
            ""id"": ""b37648a4-b9de-474c-b180-5049a5e8271c"",
            ""actions"": [
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""766b6ebc-e4ea-41e1-82dc-83920de5b886"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e3995235-ae13-42b5-99f8-3f426da11a08"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ebc0f73-70dc-4fa3-bec0-d353e837db42"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4adf14a1-6c98-4731-a29b-1f2746cda7eb"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Basic Movement
        m_BasicMovement = asset.FindActionMap("Basic Movement", throwIfNotFound: true);
        m_BasicMovement_Shoot = m_BasicMovement.FindAction("Shoot", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Basic Movement
    private readonly InputActionMap m_BasicMovement;
    private IBasicMovementActions m_BasicMovementActionsCallbackInterface;
    private readonly InputAction m_BasicMovement_Shoot;
    public struct BasicMovementActions
    {
        private @PlayerInput m_Wrapper;
        public BasicMovementActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Shoot => m_Wrapper.m_BasicMovement_Shoot;
        public InputActionMap Get() { return m_Wrapper.m_BasicMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BasicMovementActions set) { return set.Get(); }
        public void SetCallbacks(IBasicMovementActions instance)
        {
            if (m_Wrapper.m_BasicMovementActionsCallbackInterface != null)
            {
                @Shoot.started -= m_Wrapper.m_BasicMovementActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_BasicMovementActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_BasicMovementActionsCallbackInterface.OnShoot;
            }
            m_Wrapper.m_BasicMovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
            }
        }
    }
    public BasicMovementActions @BasicMovement => new BasicMovementActions(this);
    public interface IBasicMovementActions
    {
        void OnShoot(InputAction.CallbackContext context);
    }
}

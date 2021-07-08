// GENERATED AUTOMATICALLY FROM 'Assets/InputController/WispOfThyHeat_Input.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControl : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControl()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""WispOfThyHeat_Input"",
    ""maps"": [
        {
            ""name"": ""Control_Normal"",
            ""id"": ""2afef0cb-bc27-41ba-9c78-5ecd4eda28d5"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""c9239cdc-e71f-4755-8cd6-950607a30b65"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MithraFire"",
                    ""type"": ""Button"",
                    ""id"": ""14286257-f17e-4657-a776-cf65b98b0ab6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""Value"",
                    ""id"": ""1395b84b-5d7f-4fbd-b306-35cbcb93a67f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""92958aed-032c-4b7f-9d40-f2215f3c646e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme;Keyboard&Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ba65167b-3980-466b-92ae-786805af89a7"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fa1e54be-a74a-4750-bee4-3899bbc4e6e2"",
                    ""path"": ""<Mouse>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MithraFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c120833-034a-4f0b-b728-99f7eb273448"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Control_AutoAim"",
            ""id"": ""76bc494c-fe91-4ddd-8d1c-d4bc97f5e23a"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""9de37496-42b3-4888-902f-24371cb9e4ed"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MithraFire"",
                    ""type"": ""Button"",
                    ""id"": ""44094f42-672b-457b-bde4-d9640d114bd4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mouse"",
                    ""type"": ""Value"",
                    ""id"": ""9b3638d4-4957-4938-9b51-74c426ba5ef8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""d4e97cf6-0b1a-42cd-99c7-267576c5c5c6"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme;Keyboard&Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c3aaf3b-cccb-40eb-a3d1-4397257b970e"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4b1145e-5888-44ce-a5d8-732067962646"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MithraFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c4a6b9eb-b72b-40f1-b286-ef996243bdff"",
                    ""path"": ""<Mouse>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""MithraFire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3bde3903-ed7c-4c6d-bd30-9a3aba169bba"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Control_Global"",
            ""id"": ""a9941643-02de-4391-bda7-2905bd95e8a8"",
            ""actions"": [
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""953c9e5c-ffb2-4c7d-b783-aa2c0061139b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""b1f250a5-7436-4666-8aa9-0d9aeefedb05"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""PassThrough"",
                    ""id"": ""883511ab-fa40-4668-b92b-b8da6e312d2a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Confirm"",
                    ""type"": ""Button"",
                    ""id"": ""a901d673-6d86-4ed0-a03c-2d19d9d4fc84"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""f9419816-41f6-4d6d-8352-355b395f9eca"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e2d94cfa-e196-49dd-8f39-083029b04f2e"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""823dfcc0-99fc-44f1-8866-d0bc6dc10652"",
                    ""path"": ""*/{Back}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad;Keyboard&Mouse"",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3a08fce3-b345-4269-9514-b0ef38470c7e"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""372b875b-2c9d-4624-9c5e-60be33f4d345"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""467fbc25-044e-42bf-9704-d21b6287a0d9"",
                    ""path"": ""*/{Submit}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;Gamepad"",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d434323-6eca-476a-931b-c941601d8c42"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8f84e5b-b3a7-481d-96b1-2c15b15e6f99"",
                    ""path"": ""<DualShockGamepad>/touchpadButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""770838d9-6450-4877-901f-20f241060794"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""9783558c-411a-4c0a-9b4e-0ea90f57daa7"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""7672483f-579b-4116-a333-44efaefe4b48"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme;Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""613cb5fe-d5f9-452a-84b8-ccf6a71afc31"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme;Keyboard&Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Joystick_Analogico"",
                    ""id"": ""440dc04b-546a-40ee-81db-37e4ee9ef2c3"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme"",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""7af8da11-09fc-4d6a-a906-bf86cf90bacd"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme;Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""ca268df0-00db-41c3-878f-846a4dd698b2"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme;Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Joystick_DPad"",
                    ""id"": ""a11408be-8ebe-493d-ac06-6e661478ca9f"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme"",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""6f5fe97b-8460-4609-83b3-d85ddcdb7226"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme;Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""ec41e5b5-a0bc-466e-a63d-8f8a1fbf6005"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""ControlScheme;Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Control_Normal
        m_Control_Normal = asset.FindActionMap("Control_Normal", throwIfNotFound: true);
        m_Control_Normal_Jump = m_Control_Normal.FindAction("Jump", throwIfNotFound: true);
        m_Control_Normal_MithraFire = m_Control_Normal.FindAction("MithraFire", throwIfNotFound: true);
        m_Control_Normal_Aim = m_Control_Normal.FindAction("Aim", throwIfNotFound: true);
        // Control_AutoAim
        m_Control_AutoAim = asset.FindActionMap("Control_AutoAim", throwIfNotFound: true);
        m_Control_AutoAim_Jump = m_Control_AutoAim.FindAction("Jump", throwIfNotFound: true);
        m_Control_AutoAim_MithraFire = m_Control_AutoAim.FindAction("MithraFire", throwIfNotFound: true);
        m_Control_AutoAim_Mouse = m_Control_AutoAim.FindAction("Mouse", throwIfNotFound: true);
        // Control_Global
        m_Control_Global = asset.FindActionMap("Control_Global", throwIfNotFound: true);
        m_Control_Global_Back = m_Control_Global.FindAction("Back", throwIfNotFound: true);
        m_Control_Global_Interact = m_Control_Global.FindAction("Interact", throwIfNotFound: true);
        m_Control_Global_Pause = m_Control_Global.FindAction("Pause", throwIfNotFound: true);
        m_Control_Global_Confirm = m_Control_Global.FindAction("Confirm", throwIfNotFound: true);
        m_Control_Global_Move = m_Control_Global.FindAction("Move", throwIfNotFound: true);
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

    // Control_Normal
    private readonly InputActionMap m_Control_Normal;
    private IControl_NormalActions m_Control_NormalActionsCallbackInterface;
    private readonly InputAction m_Control_Normal_Jump;
    private readonly InputAction m_Control_Normal_MithraFire;
    private readonly InputAction m_Control_Normal_Aim;
    public struct Control_NormalActions
    {
        private @PlayerControl m_Wrapper;
        public Control_NormalActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Control_Normal_Jump;
        public InputAction @MithraFire => m_Wrapper.m_Control_Normal_MithraFire;
        public InputAction @Aim => m_Wrapper.m_Control_Normal_Aim;
        public InputActionMap Get() { return m_Wrapper.m_Control_Normal; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Control_NormalActions set) { return set.Get(); }
        public void SetCallbacks(IControl_NormalActions instance)
        {
            if (m_Wrapper.m_Control_NormalActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_Control_NormalActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_Control_NormalActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_Control_NormalActionsCallbackInterface.OnJump;
                @MithraFire.started -= m_Wrapper.m_Control_NormalActionsCallbackInterface.OnMithraFire;
                @MithraFire.performed -= m_Wrapper.m_Control_NormalActionsCallbackInterface.OnMithraFire;
                @MithraFire.canceled -= m_Wrapper.m_Control_NormalActionsCallbackInterface.OnMithraFire;
                @Aim.started -= m_Wrapper.m_Control_NormalActionsCallbackInterface.OnAim;
                @Aim.performed -= m_Wrapper.m_Control_NormalActionsCallbackInterface.OnAim;
                @Aim.canceled -= m_Wrapper.m_Control_NormalActionsCallbackInterface.OnAim;
            }
            m_Wrapper.m_Control_NormalActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @MithraFire.started += instance.OnMithraFire;
                @MithraFire.performed += instance.OnMithraFire;
                @MithraFire.canceled += instance.OnMithraFire;
                @Aim.started += instance.OnAim;
                @Aim.performed += instance.OnAim;
                @Aim.canceled += instance.OnAim;
            }
        }
    }
    public Control_NormalActions @Control_Normal => new Control_NormalActions(this);

    // Control_AutoAim
    private readonly InputActionMap m_Control_AutoAim;
    private IControl_AutoAimActions m_Control_AutoAimActionsCallbackInterface;
    private readonly InputAction m_Control_AutoAim_Jump;
    private readonly InputAction m_Control_AutoAim_MithraFire;
    private readonly InputAction m_Control_AutoAim_Mouse;
    public struct Control_AutoAimActions
    {
        private @PlayerControl m_Wrapper;
        public Control_AutoAimActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Control_AutoAim_Jump;
        public InputAction @MithraFire => m_Wrapper.m_Control_AutoAim_MithraFire;
        public InputAction @Mouse => m_Wrapper.m_Control_AutoAim_Mouse;
        public InputActionMap Get() { return m_Wrapper.m_Control_AutoAim; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Control_AutoAimActions set) { return set.Get(); }
        public void SetCallbacks(IControl_AutoAimActions instance)
        {
            if (m_Wrapper.m_Control_AutoAimActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_Control_AutoAimActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_Control_AutoAimActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_Control_AutoAimActionsCallbackInterface.OnJump;
                @MithraFire.started -= m_Wrapper.m_Control_AutoAimActionsCallbackInterface.OnMithraFire;
                @MithraFire.performed -= m_Wrapper.m_Control_AutoAimActionsCallbackInterface.OnMithraFire;
                @MithraFire.canceled -= m_Wrapper.m_Control_AutoAimActionsCallbackInterface.OnMithraFire;
                @Mouse.started -= m_Wrapper.m_Control_AutoAimActionsCallbackInterface.OnMouse;
                @Mouse.performed -= m_Wrapper.m_Control_AutoAimActionsCallbackInterface.OnMouse;
                @Mouse.canceled -= m_Wrapper.m_Control_AutoAimActionsCallbackInterface.OnMouse;
            }
            m_Wrapper.m_Control_AutoAimActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @MithraFire.started += instance.OnMithraFire;
                @MithraFire.performed += instance.OnMithraFire;
                @MithraFire.canceled += instance.OnMithraFire;
                @Mouse.started += instance.OnMouse;
                @Mouse.performed += instance.OnMouse;
                @Mouse.canceled += instance.OnMouse;
            }
        }
    }
    public Control_AutoAimActions @Control_AutoAim => new Control_AutoAimActions(this);

    // Control_Global
    private readonly InputActionMap m_Control_Global;
    private IControl_GlobalActions m_Control_GlobalActionsCallbackInterface;
    private readonly InputAction m_Control_Global_Back;
    private readonly InputAction m_Control_Global_Interact;
    private readonly InputAction m_Control_Global_Pause;
    private readonly InputAction m_Control_Global_Confirm;
    private readonly InputAction m_Control_Global_Move;
    public struct Control_GlobalActions
    {
        private @PlayerControl m_Wrapper;
        public Control_GlobalActions(@PlayerControl wrapper) { m_Wrapper = wrapper; }
        public InputAction @Back => m_Wrapper.m_Control_Global_Back;
        public InputAction @Interact => m_Wrapper.m_Control_Global_Interact;
        public InputAction @Pause => m_Wrapper.m_Control_Global_Pause;
        public InputAction @Confirm => m_Wrapper.m_Control_Global_Confirm;
        public InputAction @Move => m_Wrapper.m_Control_Global_Move;
        public InputActionMap Get() { return m_Wrapper.m_Control_Global; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Control_GlobalActions set) { return set.Get(); }
        public void SetCallbacks(IControl_GlobalActions instance)
        {
            if (m_Wrapper.m_Control_GlobalActionsCallbackInterface != null)
            {
                @Back.started -= m_Wrapper.m_Control_GlobalActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_Control_GlobalActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_Control_GlobalActionsCallbackInterface.OnBack;
                @Interact.started -= m_Wrapper.m_Control_GlobalActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_Control_GlobalActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_Control_GlobalActionsCallbackInterface.OnInteract;
                @Pause.started -= m_Wrapper.m_Control_GlobalActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_Control_GlobalActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_Control_GlobalActionsCallbackInterface.OnPause;
                @Confirm.started -= m_Wrapper.m_Control_GlobalActionsCallbackInterface.OnConfirm;
                @Confirm.performed -= m_Wrapper.m_Control_GlobalActionsCallbackInterface.OnConfirm;
                @Confirm.canceled -= m_Wrapper.m_Control_GlobalActionsCallbackInterface.OnConfirm;
                @Move.started -= m_Wrapper.m_Control_GlobalActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_Control_GlobalActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_Control_GlobalActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_Control_GlobalActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Confirm.started += instance.OnConfirm;
                @Confirm.performed += instance.OnConfirm;
                @Confirm.canceled += instance.OnConfirm;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public Control_GlobalActions @Control_Global => new Control_GlobalActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IControl_NormalActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMithraFire(InputAction.CallbackContext context);
        void OnAim(InputAction.CallbackContext context);
    }
    public interface IControl_AutoAimActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMithraFire(InputAction.CallbackContext context);
        void OnMouse(InputAction.CallbackContext context);
    }
    public interface IControl_GlobalActions
    {
        void OnBack(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnConfirm(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
    }
}

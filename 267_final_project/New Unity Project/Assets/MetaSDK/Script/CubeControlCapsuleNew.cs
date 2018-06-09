using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using System.Net.Sockets;
using System.Text;
//using System.Threading;
//using System.Linq;

namespace Meta.HandInput.Utility
{

    /// <summary>
    /// Script to update a MetaCube's material as it changes states.
    /// </summary>
    public class CubeControlCapsuleNew : MonoBehaviour
    {
        /// <summary>
        /// The Interaction script to listen to.
        /// </summary>
        public Interaction Interaction;

        /*
        public MaterialState[] Materials = new MaterialState[3]
        {
            new MaterialState(2, new Color(0.5f, 0.5f, 0.5f, 1f), Color.white, new Color(1f, 2f, 3f)),
            new MaterialState(4, new Color(0.5f, 0.5f, 0.5f, 1f), Color.white, new Color(1f, 2f, 3f)),
            new MaterialState(6, Color.clear, new Color(0f, 0.55f, 0.55f, 1f), Color.white),
        }; */

        //private Rigidbody rb;
        public GameObject cp0, cp1, cp2, cp3;

        //private bool cp0_on, cp1_on;

        // Use this for initialization
        void Start()
        {
            if (Interaction == null) { Interaction = GetComponent<Interaction>(); }
            if (Interaction == null)
            {
                Debug.LogWarning("ToggleVisualCube's Interaction Object has not been configured. CubeControlCapsule won't execute.");
                return;
            }
            //rb = GetComponent<Rigidbody>();
            //rb.SetVisibility(false);
            //cp0 = GameObject.FindWithTag("Capsule0");
            //cp0_on = cp0.activeSelf;
            //cp0.SetActive(false);
            //cp1 = GameObject.FindWithTag("Capsule1");
            //cp1_on = cp1.activeSelf;
            //cp1.SetActive(false);

            // -- Subscribe to the Interaction script's events
            Interaction.Events.HoverStart.AddListener(OnHoverStart);
            Interaction.Events.HoverEnd.AddListener(OnHoverEnd);
            Interaction.Events.Engaged.AddListener(OnGrabStart);
            Interaction.Events.Disengaged.AddListener(OnGrabEnd);

        }



        void Update()
        {
            //UpdateColors();
        }

        private void OnHoverStart(MetaInteractionData data)
        {
            //UpdateStateVisuals(PalmState.Hovering);
        }

        private void OnHoverEnd(MetaInteractionData data)
        {
            //UpdateStateVisuals(PalmState.Idle);
        }

        private void OnGrabStart(MetaInteractionData data)
        {
            //UpdateStateVisuals(PalmState.Grabbing);
        }

        private void OnGrabEnd(MetaInteractionData data)
        {
            //UpdateStateVisuals(PalmState.Hovering);
            Debug.Log("GrabEnd.....");
            /*cp0_on = !cp0_on;
            cp0.SetActive(cp0_on);
            cp1_on = !cp1_on;
            cp1.SetActive(cp1_on);*/
            cp0.SetActive(true);
            cp1.SetActive(true);
            cp2.SetActive(true);
            cp3.SetActive(true);
        }

        /*
        private void UpdateStateVisuals(PalmState newState)
        {
            foreach (var materialState in Materials)
            {
                materialState.SetColor(newState);
            }
        }

        private void UpdateColors()
        {
            foreach (var materialState in Materials)
            {
                materialState.Update();
            }
        }
        
        /*
        [System.Serializable]
        public class MaterialState
        {
            [Readonly]
            public int MaterialIndex;
            [Readonly]
            [ColorUsage(false, true, 0f, 5f, 0f, 5f)]
            public Color Idle;

            [Readonly]
            [ColorUsage(false, true, 0f, 5f, 0f, 5f)]
            public Color Highlight;

            [Readonly]
            [ColorUsage(false, true, 0f, 5f, 0f, 5f)]
            public Color Active;

            private Renderer _renderer;

            private Color _targetColor;
            private Color _previousColor;

            private float _interpAmount;
            private float _interpAmountVelocity;

            private const float SmoothTime = 0.05f;

            public MaterialState(int materialIndex, Color idle, Color highlight, Color active)
            {
                MaterialIndex = materialIndex;

                Idle = idle;
                Highlight = highlight;
                Active = active;
            }

            public void Initialize(MonoBehaviour behaviour)
            {
                _renderer = behaviour.GetComponent<Renderer>();

                // -- Initialize variables to default 'idle' state.
                SetColor(PalmState.Idle);
            }

            public void SetColor(PalmState state)
            {
                switch (state)
                {
                    case PalmState.Idle:
                        _targetColor = Idle;
                        break;
                    case PalmState.Hovering:
                        _targetColor = Highlight;
                        break;
                    case PalmState.Grabbing:
                        _targetColor = Active;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("state", state, null);
                }

                _previousColor = _renderer.materials[MaterialIndex].GetColor("_EmissionColor");
                _interpAmount = 0f;
            }

            public void Update()
            {
                _interpAmount = Mathf.Clamp01(Mathf.SmoothDamp(_interpAmount, 1f, ref _interpAmountVelocity, SmoothTime));
                _renderer.materials[MaterialIndex].SetColor("_EmissionColor", Color.Lerp(_previousColor, _targetColor, _interpAmount));
            }
        }
        */
    }
}

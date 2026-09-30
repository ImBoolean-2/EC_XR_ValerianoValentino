using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace ECXR
{
    /// <summary>
    /// Requisito 4 — Interacción a distancia.
    ///
    /// Se asigna a un objeto con un <see cref="XRSimpleInteractable"/> (el "Panel de Control").
    /// Cuando el rayo del controller lo selecciona a distancia, enciende o apaga la luz
    /// de la sala y cambia el material del propio panel para que el cambio sea visible
    /// desde cualquier punto de la escena.
    ///
    /// Se invoca automáticamente desde el evento SelectEntered/SelectExited del
    /// XRSimpleInteractable, de modo que no hay que cablear nada en el Inspector.
    /// </summary>
    [RequireComponent(typeof(XRSimpleInteractable))]
    public class ControlPanel : MonoBehaviour
    {
        [Header("Referencias de la escena")]
        [Tooltip("Luz puntual de la sala que se enciende y apaga.")]
        public Light luzSala;

        [Tooltip("Objeto cuyo material cambia de color al activar el panel.")]
        public Renderer objetoColor;

        [Header("Materiales")]
        [Tooltip("Material aplicado cuando el panel está APAGADO (estado inicial).")]
        public Material materialApagado;

        [Tooltip("Material aplicado cuando el panel está ENCENDIDO.")]
        public Material materialEncendido;

        [Header("Audio (opcional)")]
        [Tooltip("Clip corto que suena al cambiar de estado.")]
        public AudioClip sonido;

        /// <summary>True si la luz está encendida en este momento.</summary>
        public bool Encendido { get; private set; }

        XRSimpleInteractable _interactable;
        AudioSource _audio;

        void Awake()
        {
            _interactable = GetComponent<XRSimpleInteractable>();

            _audio = gameObject.GetComponent<AudioSource>();
            if (_audio == null && sonido != null)
                _audio = gameObject.AddComponent<AudioSource>();

            // Estado inicial: apagado.
            Encendido = false;
            AplicarEstado();
        }

        void OnEnable()
        {
            var xri = _interactable;
            if (xri == null) return;
            xri.selectEntered.AddListener(OnSelectEntered);
            xri.selectExited.AddListener(OnSelectExited);
        }

        void OnDisable()
        {
            var xri = _interactable;
            if (xri == null) return;
            xri.selectEntered.RemoveListener(OnSelectEntered);
            xri.selectExited.RemoveListener(OnSelectExited);
        }

        void OnSelectEntered(SelectEnterEventArgs args) => Activar();

        void OnSelectExited(SelectExitEventArgs args) => Desactivar();

        /// <summary>Enciende la luz y aplica el material "encendido". También se puede llamar desde un botón.</summary>
        public void Activar()
        {
            if (Encendido) return;
            Encendido = true;
            AplicarEstado();
            if (_audio != null && sonido != null) _audio.Play();
        }

        /// <summary>Apaga la luz y vuelve al material "apagado".</summary>
        public void Desactivar()
        {
            if (!Encendido) return;
            Encendido = false;
            AplicarEstado();
            if (_audio != null && sonido != null) _audio.Play();
        }

        /// <summary>Alterna el estado (encendido ↔ apagado).</summary>
        public void Alternar() => Encendido = !Encendido;

        void AplicarEstado()
        {
            if (luzSala != null)
                luzSala.enabled = Encendido;

            var mat = Encendido ? materialEncendido : materialApagado;

            if (objetoColor != null && mat != null)
                objetoColor.sharedMaterial = mat;

            if (mat != null)
            {
                var propio = GetComponent<Renderer>();
                if (propio != null) propio.sharedMaterial = mat;
            }
        }
    }
}

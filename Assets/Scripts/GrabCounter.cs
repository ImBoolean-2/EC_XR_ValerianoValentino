using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace ECXR
{
    /// <summary>
    /// Reto libre — Contador de objetos agarrados (UI espacial).
    ///
    /// Busca todos los <see cref="XRGrabInteractable"/> de la escena y lleva la cuenta de
    /// cuántos se han agarrado al menos una vez. Actualiza un <see cref="Text"/> de un
    /// Canvas en World Space, de modo que el progreso se ve dentro del entorno XR.
    ///
    /// No requiere cablear nada en el Inspector salvo el campo <see cref="texto"/>.
    /// </summary>
    public class GrabCounter : MonoBehaviour
    {
        [Header("UI")]
        [Tooltip("Texto (Canvas World Space) donde se muestra el progreso.")]
        public Text texto;

        [Tooltip("Formato del contador. {0} = agarrados, {1} = total.")]
        public string formato = "Objetos agarrados: {0} / {1}";

        [Tooltip("Texto mostrado cuando se agarran todos los objetos.")]
        public string mensajeCompleto = "\u00a1Reto completado!";

        readonly HashSet<XRGrabInteractable> _agarrados = new HashSet<XRGrabInteractable>();
        int _total;

        void Start()
        {
            var todos = Object.FindObjectsByType<XRGrabInteractable>(FindObjectsInactive.Exclude);

            foreach (var g in todos)
            {
                if (g == null) continue;
                _total++;

                var interactable = g;
                g.selectEntered.AddListener(_ =>
                {
                    if (_agarrados.Add(interactable))
                        Actualizar();
                });
            }

            Actualizar();
        }

        void Actualizar()
        {
            if (texto == null) return;

            texto.text = string.Format(formato, _agarrados.Count, _total);

            if (_total > 0 && _agarrados.Count >= _total)
                texto.text += "\n" + mensajeCompleto;
        }
    }
}

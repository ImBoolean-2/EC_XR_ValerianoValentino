#if UNITY_EDITOR
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ECXR
{
    /// <summary>
    /// Utilidad de desarrollo (solo Editor). Captura pantallas del Game view durante Play mode
    /// con los nombres exactos que pide la rubrica, para no tener que recortarlas ni renombrarlas.
    ///
    ///   F9  -> docs/screenshots/01-vista-general.png
    ///   F10 -> docs/screenshots/03-interaccion.png
    ///
    /// Solo se compila en el Editor: no entra en el APK ni afecta a la experiencia.
    /// </summary>
    public class CapturadorCapturas : MonoBehaviour
    {
        const string Carpeta = "docs/screenshots";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void Instalar()
        {
            var go = new GameObject("[CapturadorCapturas]") { hideFlags = HideFlags.HideInHierarchy };
            DontDestroyOnLoad(go);
            go.AddComponent<CapturadorCapturas>();
        }

        void Update()
        {
            var teclado = Keyboard.current;
            if (teclado == null) return;

            if (teclado.f9Key.wasPressedThisFrame) Capturar("01-vista-general");
            if (teclado.f10Key.wasPressedThisFrame) Capturar("03-interaccion");
        }

        static void Capturar(string nombre)
        {
            if (!Directory.Exists(Carpeta))
                Directory.CreateDirectory(Carpeta);

            var ruta = Path.Combine(Carpeta, nombre + ".png").Replace('\\', '/');
            ScreenCapture.CaptureScreenshot(ruta);
            Debug.Log("[EC XR] Captura guardada en " + ruta);
        }
    }
}
#endif

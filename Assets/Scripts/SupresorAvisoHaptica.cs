using System;
using UnityEngine;

namespace ECXR
{
    /// <summary>
    /// Silencia un aviso benigno y conocido del XR Interaction Toolkit.
    ///
    /// Los mandos simulados del XR Device Simulator (<c>XRSimulatedController</c>) no implementan
    /// la consulta de capacidades hápticas. Cuando XRI intenta consultarlas, registra:
    ///
    ///   "Failed to get haptic capabilities of XRSimulatedController ... error code -1.
    ///    Continuing assuming a single haptic channel."
    ///
    /// El propio mensaje indica que XRI continúa con un canal por defecto: no es un error ni
    /// afecta a la jugabilidad. Este filtro descarta únicamente ese texto concreto para no
    /// ensuciar la consola durante las pruebas y la grabación de evidencias.
    ///
    /// Solo se instala en el Editor: en un dispositivo real con mandos físicos la háptica
    /// funciona y no aparece este aviso, por lo que no hay nada que filtrar.
    /// </summary>
    public static class SupresorAvisoHaptica
    {
        /// <summary>Fragmento exacto del mensaje que se descarta.</summary>
        const string TextoFiltrado = "Failed to get haptic capabilities";

        static bool s_Instalado;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Instalar()
        {
#if UNITY_EDITOR
            if (s_Instalado || Debug.unityLogger == null) return;
            Debug.unityLogger.logHandler = new FiltroLog(Debug.unityLogger.logHandler);
            s_Instalado = true;
#endif
        }

        /// <summary>Envoltorio de <see cref="ILogHandler"/> que descarta el aviso de háptica y delega el resto.</summary>
        sealed class FiltroLog : ILogHandler
        {
            readonly ILogHandler _interno;

            public FiltroLog(ILogHandler interno) => _interno = interno;

            public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
            {
                if (EsElAvisoDeHaptica(format)) return;
                _interno.LogFormat(logType, context, format, args);
            }

            public void LogException(Exception exception, UnityEngine.Object context)
                => _interno.LogException(exception, context);

            static bool EsElAvisoDeHaptica(string format)
                => !string.IsNullOrEmpty(format) && format.Contains(TextoFiltrado);
        }
    }
}

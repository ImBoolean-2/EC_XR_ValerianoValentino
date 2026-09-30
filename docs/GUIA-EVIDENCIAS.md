# Guía para generar las evidencias

Esta guía explica cómo producir las **3 capturas de pantalla** y el **video demostrativo** que exige
la rúbrica. Las capturas son obligatorias; el video debe durar **como máximo 1 minuto**.

---

## Antes de empezar: abrir el proyecto

1. Abrir **Unity Hub** → `Add` → `Add project from disk` → `EC_XR_ValerianoValentino`.
2. Abrir con **Unity 6000.6.3f1**.
3. En la ventana **Project**, abrir `Assets/Scenes/EC_XR_ValerianoValentino.unity`.
4. Pulsar **Play**.

> Si la cámara arranca rara, comprobar que en `Edit > Project Settings > XR Plug-in Management`
> está activo **OpenXR** en la pestaña de escritorio. El **XR Device Simulator** ya está incluido
> en la escena, así que no hace falta visor.

### Controles del XR Device Simulator

| Acción | Tecla / Ratón |
|---|---|
| **Caminar (mueve cámara y manos juntas)** | **`W` `A` `S` `D`** |
| Mirar alrededor | **mover el ratón** (sin pulsar nada) |
| **AGARRAR un objeto / pulsar el panel** | **Clic izquierdo** (o **`G`**) |
| Soltar el objeto | soltar el clic izquierdo |
| Manipular mando derecho (MANTENER) | `Espacio` |
| Manipular mando izquierdo (MANTENER) | `Shift izquierdo` |
| Fijar / liberar mando derecho | `Y` (toggle) |
| Fijar / liberar mando izquierdo | `T` (toggle) |
| Botón primario | `B` |

> ℹ️ El agarre corresponde a la acción **`Select`**, que en los Starter Assets está ligada a
> **`{GripButton}`** (grip), no al gatillo. El clic izquierdo está vinculado al `Grip` y a
> `Manipulate Right`, así que **apuntar y hacer clic agarra** directamente. En un proyecto limpio
> sería: mantener `Espacio` + pulsar `G`.

> ⚠️ **No uses la tecla `3`** (modo "posición del dispositivo"): traslada solo la cabeza y las
> manos se quedan atrás. Deja el modo por defecto (`1`).



---

## Captura 1 — Vista general del escenario

**Qué debe verse:** la sala completa, con piso, muros, techo, iluminación, la mesa y los objetos 3D.

**Cómo (con el atajo incluido):**
1. Pulsa **Play**.
2. Colócate mirando la sala desde una esquina (muévete con `WASD` y gira moviendo el ratón).
3. Pulsa **`F9`** → se guarda automáticamente como `docs/screenshots/01-vista-general.png`.

> El script `Assets/Scripts/CapturadorCapturas.cs` (solo Editor) hace la captura con el nombre
> correcto. Necesitas una resolución de Game view razonable (por ejemplo 1920x1080 o 1280x720).

---

## Captura 2 — Configuración XR / componentes en el Inspector

**Qué debe verse:** la configuración del proyecto XR y un componente `XRGrabInteractable`.

**Cómo (dos opciones, elige la que prefieras):**

- **Opción A — Configuración XR:** salir de Play, abrir
  `Edit > Project Settings > XR Plug-in Management` y capturar la pantalla con **OpenXR** activo.
- **Opción B — Componentes:** en la jerarquía seleccionar `Props > Objeto_Cubo` y capturar el
  **Inspector** mostrando `Rigidbody` y `XRGrabInteractable`.

Guarda como `docs/screenshots/02-configuracion-xr.png`.

> Consejo: captura **las dos**, la rúbrica valora que se vea la configuración XR real.

---

## Captura 3 — Una interacción funcionando

**Qué debe verse:** el rayo del mando apuntando al `Panel_Control` con la luz de la sala encendida
(el panel se pone **verde** y la cápsula cambia de color).

**Cómo (con el atajo incluido):**
1. En Play, **mira hacia el muro oeste** (a la izquierda) moviendo el ratón hasta ver el
   `Panel_Control`.
2. Apunta al panel (el rayo del mando derecho sale hacia donde miras).
3. **Haz clic izquierdo** sobre el panel: la luz de sala se enciende y el panel se pone verde.
   **Mantén el clic pulsado** para que se quede encendido.
4. Pulsa **`F10`** → se guarda automáticamente como `docs/screenshots/03-interaccion.png`.

> Alternativa por si prefieres capturar también el mando y el rayo: `Windows + Shift + S`.

Guarda como `docs/screenshots/03-interaccion.png`.

---

## Video demostrativo (máximo 1 minuto)

**Guion sugerido:**

| Tiempo | Qué mostrar |
|---|---|
| 0:00 – 0:08 | Vista general de la escena y del proyecto abierto en Unity (Project + Hierarchy). |
| 0:08 – 0:25 | Agarrar el **cubo** y la **esfera**, moverlos y soltarlos. Mostrar que el **contador** sube. |
| 0:25 – 0:40 | Apuntar con el rayo al **Panel de Control** y encender/apagar la luz (el panel cambia a verde). |
| 0:40 – 0:52 | **Teletransportarse** por el piso: apuntar al suelo con el rayo y soltar el gatillo. |
| 0:52 – 1:00 | Seleccionar un objeto en la jerarquía y mostrar el Inspector con `Rigidbody` + `XRGrabInteractable`. |

**Recomendaciones de grabación:**

- Graba la ventana de **Game view** a pantalla completa (no hace falta la UI del Editor todo el tiempo).
- Usa la grabadora de Windows: `Windows + G` (Xbox Game Bar) o una herramienta como OBS.
- Sube el video a **YouTube** (puede ser *no listado*) o a Google Drive con enlace público.
- Pega el enlace en la sección **6. Video demostrativo** del `README.md`, sustituyendo el texto
  `[PENDIENTE — insertar aquí el enlace al video]`.

---

## Después de capturar

1. Comprobar que los tres archivos están en `docs/screenshots/` con los nombres exactos:
   - `01-vista-general.png`
   - `02-configuracion-xr.png`
   - `03-interaccion.png`
2. Pegar el enlace del video en el `README.md`.
3. Subir los cambios:

   ```bash
   git add -A
   git commit -m "docs: capturas y video demostrativo"
   git push
   ```

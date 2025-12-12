// Previene el envío automático del formulario al presionar Enter
// Se aplica sólo cuando el foco está en campos que no deben provocar submit.
// Permite Enter en: botones de tipo submit, textareas y combinaciones con modificadores (Ctrl/Shift/Alt/Meta).
// También permite opt-in agregando la clase ".allow-enter" (o cualquier selector pasado en options.allowSelectors).

export function attachPreventEnterSubmit(form, options) {
  if (!form) return;

  const allowSelectors = Array.isArray(options?.allowSelectors)
    ? options.allowSelectors
    : ["textarea", ".allow-enter"];

  const shouldAllow = (target, event) => {
    const tag = (target?.tagName || "").toLowerCase();
    const type = (target?.type || "").toLowerCase();

    // Accesibilidad: permitir Enter en botones y textareas
    if (tag === "button" || type === "submit" || tag === "textarea") return true;

    // Permitir combinaciones con modificadores (ej. Ctrl+Enter)
    if (event.ctrlKey || event.shiftKey || event.altKey || event.metaKey) return true;

    // Opt-in explícito mediante selectores
    if (typeof target?.matches === "function") {
      for (const sel of allowSelectors) {
        try {
          if (sel && target.matches(sel)) return true;
        } catch (_) {
          // Ignorar selectores inválidos
        }
      }
    }
    return false;
  };

  const handler = (e) => {
    const isEnter = e.key === "Enter" || e.keyCode === 13;
    if (!isEnter) return;

    const target = e.target;
    if (shouldAllow(target, e)) return;

    // Prevenir sólo cuando es necesario
    e.preventDefault();
  };

  form.addEventListener("keydown", handler, { passive: false });
}


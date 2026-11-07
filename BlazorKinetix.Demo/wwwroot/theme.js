const themeCookie = "blazorkinetix-demo-theme";

function savedTheme() {
  const value = document.cookie.split("; ").find(item => item.startsWith(`${themeCookie}=`));
  return value?.split("=")[1] || localStorage.getItem(themeCookie)
    || (matchMedia("(prefers-color-scheme: dark)").matches ? "dark" : "light");
}

function applyTheme() {
  document.documentElement.dataset.theme = savedTheme();
}

applyTheme();
document.addEventListener("DOMContentLoaded", () => {
  window.Blazor?.addEventListener("enhancedload", applyTheme);
});

window.blazorKinetixTheme = {
  toggle() {
    const next = document.documentElement.dataset.theme === "dark" ? "light" : "dark";
    document.cookie = `${themeCookie}=${next}; Path=/; Max-Age=31536000; SameSite=Lax`;
    localStorage.setItem(themeCookie, next);
    applyTheme();
  }
};

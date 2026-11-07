window.blazorKinetixLanguage = {
  set(language) {
    const returnUrl = location.pathname + location.search + location.hash;
    location.assign(`/culture/${language}?returnUrl=${encodeURIComponent(returnUrl)}`);
  }
};

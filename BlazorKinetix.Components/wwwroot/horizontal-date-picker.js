export function scrollDays(track, direction) {
  const day = track.querySelector('.bk-horizontal-date-day');
  const gap = parseFloat(getComputedStyle(track).columnGap) || 0;
  const distance = day ? day.getBoundingClientRect().width + gap : track.clientWidth;
  track.scrollBy({ left: direction * distance, behavior: 'smooth' });
}

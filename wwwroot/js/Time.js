﻿window.onload = function () {
    setDurationField();
}

function setDurationField() {
    const durationTimeItems = document.querySelectorAll('[data-duration="durationToUser"]');

    durationTimeItems.forEach((item) => {
        item.innerHTML = changeSecoundOnStringTime(item.innerHTML);
    })
}


function changeSecoundOnStringTime(value) {
    const hours = Math.floor(value / 3600);
    const minutes = Math.floor((value - hours * 3600) / 60);

    return `${hours}h ${minutes}min`;
}
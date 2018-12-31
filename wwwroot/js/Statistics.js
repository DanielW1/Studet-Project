document.querySelectorAll(".c-tab--link")
document.getElementById('generalStatisticLink').addEventListener("click", function () {
    addHiddenClassToPanels();
    removeSelectedClass();
    document.querySelector("#generalStatistic").classList.remove("u-hidden");
    document.getElementById('generalStatisticLink').classList.add("c-tab--link--selected");

});
document.getElementById('latWeekStatisticLink').addEventListener("click", function () {
    addHiddenClassToPanels();
    removeSelectedClass();
    document.querySelector("#lastWeekStatistic").classList.remove("u-hidden");
    document.getElementById('latWeekStatisticLink').classList.add("c-tab--link--selected");
});
document.getElementById('yearAgoStatisticList').addEventListener("click", function () {
    addHiddenClassToPanels();
    removeSelectedClass();
    document.querySelector("#yearAgoStatistic").classList.remove("u-hidden");
    document.getElementById('yearAgoStatisticList').classList.add("c-tab--link--selected");
});

function addHiddenClassToPanels() {
    document.querySelectorAll(".c-tab--content").forEach((elem) => {
        elem.classList.add("u-hidden");
    })
}

function removeSelectedClass() {
    document.querySelectorAll(".c-tab--link").forEach((elem) => {
        elem.classList.remove("c-tab--link--selected");
    })
}

export function changeTime(time) {

    var year = time.split("-")[0];
    var month = time.split("-")[1];
    var day = time.split("-")[2].split("T")[0];
    var hours = time.split("T")[1].split(":")[0];
    var minutes = time.split("T")[1].split(":")[1];
    var newTime = year + "年" + month + "月" + day + "日 " + hours + "时" + minutes + "分";

    return newTime;
}
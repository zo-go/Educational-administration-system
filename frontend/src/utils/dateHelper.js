// 给Date类型的原型上添加一个format方法，以后所有的Date类型都具体有这个格式化方法
Date.prototype.format = function (format) {
    const date = {
        "M+": this.getMonth() + 1,

        "d+": this.getDate(),

        "h+": this.getHours(),

        "m+": this.getMinutes(),

        "s+": this.getSeconds(),

        "q+": Math.floor((this.getMonth() + 3) / 3),

        "S+": this.getMilliseconds(),
    };

    if (/(y+)/i.test(format)) {
        format = format.replace(
            RegExp.$1,
            (this.getFullYear() + "").slice(4 - RegExp.$1.length)
        );
    }

    for (const k in date) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(
                RegExp.$1,
                RegExp.$1.length == 1
                    ? date[k]
                    : ("00" + date[k]).slice(("" + date[k]).length)
            );
        }
    }

    return format;
};

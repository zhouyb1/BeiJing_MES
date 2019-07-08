(function flexible(window, document) {
    var docEl = document.documentElement
    var dpr = window.devicePixelRatio || 1

    // adjust body font size
    function setBodyFontSize() {
        if (document.body) {
            document.body.style.fontSize = (12 * dpr) + 'px'
        } else {
            document.addEventListener('DOMContentLoaded', setBodyFontSize)
        }
    }
    setBodyFontSize();

    // set 1rem = viewWidth / 10
    function setRemUnit() {
        var rem = docEl.clientWidth / 10
        docEl.style.fontSize = rem + 'px'
    }

    setRemUnit()

    // reset rem unit on page resize
    window.addEventListener('resize', setRemUnit)
    window.addEventListener('pageshow', function (e) {
        if (e.persisted) {
            setRemUnit()
        }
    })

    // detect 0.5px supports
    if (dpr >= 2) {
        var fakeBody = document.createElement('body')
        var testElement = document.createElement('div')
        testElement.style.border = '.5px solid transparent'
        fakeBody.appendChild(testElement)
        docEl.appendChild(fakeBody)
        if (testElement.offsetHeight === 1) {
            docEl.classList.add('hairlines')
        }
        docEl.removeChild(fakeBody)
    }
}(window, document));

window.onload = function () {
    var str = window.location.href;
    var qr_code = str.includes('?q=') ? str.split('?q=')[1] : false;
    if (qr_code && qr_code.includes('&d=')) {
        var code = qr_code.split('&d=')[0];
        var timer = qr_code.split('&d=')[1];
        // console.log(code, timer);

        var xmlhttp = null;
        if (window.XMLHttpRequest) {
            xmlhttp = new XMLHttpRequest();
        } else {
            xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
        };
        xmlhttp.onreadystatechange = function () {
            if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                var data = JSON.parse(xmlhttp.responseText);
                var li = document.querySelectorAll('#list li span');
                // console.log(data);
                if (data.State === 400) {
                    alts('没有获取到数据，请重新扫码~');
                } else {
                    var loading = document.querySelector('.ball-pulse-sync');
                    var timer_str = '';
                    for (var i = 0; i < timer.length; i++) {
                        timer_str += timer[i];
                        if (i === 3) {
                            timer_str += '-';
                        };
                        if (i === 5) {
                            timer_str += '-';
                        };
                        if (i === 7) {
                            timer_str += ' ';
                        };

                    };
                    loading.style = 'opacity:0;z-index:-999;';
                    li[0].innerText = data.data.S_Name || '--';
                    li[1].innerText = data.data.S_MaterName || '--';
                    li[2].innerText = timer_str || '--';
                    li[3].innerText = data.data.S_Producer || '--';
                    li[4].innerText = data.data.S_Team || '--';
                    li[5].innerText = data.data.S_Quality || '--';
                    li[6].innerText = data.data.S_Standard || '--';
                    li[7].innerText = data.data.S_Storage || '--';
                    li[8].innerText = '第 ' + data.data.S_ScanRecord + ' 次' || '--';
                    li[9].innerText = data.data.S_ScanTime || '--';
                    var show_alert = document.querySelector("#show_alert");
                    show_alert.addEventListener('click', function () {
                        alts(data.data.S_MaterName, true);
                    }, false);

                    var day1 = new Date().getTime();
                    var day2 = new Date(timer_str).getTime();
                    if (day1 > day2) {
                        var num = Math.floor((day1 - day2) / 1000 / 3600);
                        // console.log((day1 - day2));
                        li[5].innerText = data.data.S_Quality + ' ( 已过期 ' + num + ' 小时)' || '--';
                        li[5].style = 'color:red;';
                    };//已过期
                };
            };
        };
        xmlhttp.open("POST", 'https://aymaoto.jtlf.cn/webapi/Goods/GoodsInfo', true);
        xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded;charset=UTF-8");
        var str = 'barCode=' + code + '&printfTime=' + timer;
        xmlhttp.send(str);

    } else {
        // alert('没有获取到数据，请重新扫码~');
        alts('没有获取到数据，请重新扫码~');
    };
};




function alts(obj, flag) {
    if (obj) {
        var alerts = document.querySelector('.alerts');
        var box = document.querySelector('.alerts-box');
        var btn = document.querySelector('#btn');
        var alert_hide = document.querySelector('.alerts-box');
        var msg = document.querySelector('#mag');

        box.style = 'opacity:1;z-index:999;';
        console.log(1);
        if (flag) {
            msg.innerHTML = "配料表：" + obj;
            alerts.style = 'height:60%;';
        } else {
            msg.style = "height:80%;display:flex;align-items:center;justify-content:center;";
            box.style = 'display:flex;align-items:center;justify-content:center;opacity:1;z-index:999;';
            alerts.style = 'height:30%;width:80%;top:50%;left:50%;margin-left:-40%;margin-top:-25%;';
            btn.style = 'height:20%';
            msg.innerHTML = obj;
        };

        btn.addEventListener('click', function () {
            box.style = 'opacity:0;;z-index:-999;';
            alerts.style = 'height:0%;';
        }, false);

        alert_hide.addEventListener('click', function (e) {
            window.event ? window.event.cancelBubble = true : e.stopPropagation();
            box.style = 'opacity:0;;z-index:-999;';
            alerts.style = 'height:0%;';
        }, false);

        alerts.addEventListener('click', function (e) {
            window.event ? window.event.cancelBubble = true : e.stopPropagation();
        }, false);
    };
};
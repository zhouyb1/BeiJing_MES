

//获取url参数值
function getQueryVariable(variable) {

    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        if (pair[0] == variable) {
            return pair[1];
        }
    }
    return false;
}

// 调用方法
var qrcode = getQueryVariable("QR");
var urlstr;

var MD5_text = "qrcode=" + qrcode + "&key=ltRsjkiM8IRbC80Ni1jzU5jiO6pJvbKd";

var md5_qrcode = hex_md5(MD5_text);

$(function () {
  
    if (qrcode == 'false') {
        //mui.toast('暂无乘客信息，请重新扫码进入!', { duration: 'short', type: 'div' })
        $(".have_info").hide();
        $(".none_info").hide();
        $(".watting_info").show();
    }


    $.ajax({
        type: "post",
        url: "https://aymaoto.jtlf.cn/webapi/otoshopping/ewh_getqrcodetrainnoinfo",
        data: {
            qrCode: qrcode,
            sign: md5_qrcode
        },
        success: function (res) {
            var resjson = JSON.parse(res);
            //console.log(resjson)

            if (resjson.State == 200) {
                var data = resjson.data.TrainInfo;
                urlstr = resjson.data.UrlStr;

                var outdata_str = data.Out_Date.trim().split(" ")[0];
                
                var trainnoId = data.TrainnoId;//车次
                var carriageno = data.CarriageNo; //车厢
                var seatno = data.SeatNo; //座位
                var seatorder = data.Seatorder;//排号
                var train_no = data.OutTrainno; //出乘车次
                var out_date = outdata_str;//出乘日期
                var company = data.Company;//公司名字
             
                //点击跳转二维火
                $("#tap_ewf").click(function () {
                    window.location.href = "http://gateway.2dfire.com?" + urlstr
                });

                //点击跳转积分兑换
                $("#jfdh_click").click(function () {
                    if (company == null) {

                        //window.location.href = "https://wx.jifenh.com/CRH/distributeCarrierForList?entrance=h5&carriageNo=" + carriageno + "&seatOrder=" + seatorder + "&seatNo=" + seatno + "&trainnoId=" + trainnoId + "&train_no=" + train_no + "&out_date=" + out_date + "&Company=" + "BJFC1";

                        mui.alert('系统异常！', '提示', function () {}, 'div');


                    } else {

                        window.location.href = "https://wx.jifenh.com/CRH/distributeCarrierForList?entrance=h5&carriageNo=" + carriageno + "&seatOrder=" + seatorder + "&seatNo=" + seatno + "&trainnoId=" + trainnoId + "&train_no=" + train_no + "&out_date=" + out_date + "&Company=" + company;

                    }
                });

                var re_html = $("#re_html").tmpl(data);
                $(".jt_time").html(re_html);

            } else if (resjson.State == 400) {
                $("#tap_ewf").click(function () {
                    mui.alert('点餐码正在开放中，请稍候……', '提示', function () {
                    }, 'div');
                });

                $(".have_info").hide();
                $(".none_info").hide();
                $(".watting_info").show();

                //点击跳转积分兑换
                $("#jfdh_click").click(function () {
                    mui.alert('暂无乘客信息，不能积分兑换!', '提示', function () {
                    }, 'div');
                });

            } else if (resjson.State == 500) {
                mui.alert('服务器异常...', '异常', function () {
                }, 'div');
                //点击跳转二维火
                if (qrcode == 'false') {
                    $("#tap_ewf").click(function () {
                        mui.alert('暂无乘客信息，请重新扫码进入!', '提示', function () {
                        }, 'div');
                    });
                } else {
                    $("#tap_ewf").click(function () {
                        mui.alert('座位码数据不全，请关掉重新扫码进入!', '提示', function () {
                        }, 'div');
                    });
                }

            }

        }

    });


    //$("#zsfw_click,#gtsb_click,#gtsc_click,#dzfw_click,#yjfk_click").click(function () {
    //    mui.alert('该功能暂未开放，敬请期待!', '提示', function () {
    //    }, 'div');

    //})

})




   




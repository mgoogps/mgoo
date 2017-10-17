$(function () {

    require.config({
        paths: {
            echarts: '/Scripts/echarts-2.2.7/dist'
        }
    });
    var mydate = new Date();
    loadEcharts([mydate.getFullYear() + "-" + (mydate.getMonth() + 1) + "-" + mydate.getDay() + " " + mydate.getHours() + ":" + mydate.getMinutes() + ":" + mydate.getSeconds()], [1], ["请选择设备"]);
  //  tabEcWidth();

})
function formSubmit() {
    var id = $('#sDevice').selectpicker('val');

    var name = $("#sDevice option[value='" + id + "']").text();
    console.log(name);
    var time = $("#txtEndDate").val();
    if (id == "" || time == "" || name == "" || name == "请选择用户") {
        $(this).alertmsg('info', '请选择用户与设备名称');
        return ;
    }
    loading();
    $.ajax({
        url: "/AjaxService/Statistics.asmx/GetEcharts",
        data: "{DeviceID:'" + id + "',Time:'" + time + "'}",
        success: function (r) {
            var d = JSON.parse(r.d);
       
            var xAxis = []; var speeds = [];
            for (var i = 0; i < d.length; i++) {
                xAxis.push(d[i].DeviceTime);
                speeds.push(parseInt(d[i].Speed));
            }
            loadEcharts(xAxis, speeds, [name]);
            removeloading();
        }
        , error: function (err) {
          
            removeloading();
            $($("body")).alertmsg('info','没有数据或数据异常');

        }
    });
     
}
function tabEcWidth() {
    $("#divEchartsLine").css("width", $("#home").width() + "px");
}
function loadEcharts(xData, Speed, DeviceName) {
    var xAxis24 = [];
    var jg = parseInt(Speed.length / 24);
    
    require(
        [
            'echarts',
            'echarts/chart/line' // 使用柱状图就加载bar模块，按需加载
          // 'Scripts/echarts/chart/line'
        ], function (ec) {
            // 基于准备好的dom，初始化echarts图表
            var myChart = ec.init(document.getElementById('divEchartsLine'), 'macarons'); // 主题：macarons       
            option = {
                tooltip: {
                    trigger: 'axis',
                    formatter: function (params, ticket, callback) {
                        var res = params[0].seriesName;
                        for (var i = 0, l = params.length; i < l; i++) {
                            res += '<br/>' + params[i].name + ' : ' + params[i].value + "KM/H";
                        }
                        setTimeout(function () {
                            // 仅为了模拟异步回调
                            callback(ticket, res);
                        }, 200)
                        return "loading";
                    }
                },
                legend: {
                    data: function () {
                        var data = [];
                        for (var i = 0; i < DeviceName.length; i++) {
                            data.push(DeviceName[i]);
                        }
                        return data;
                    }()
                },
                toolbox: {
                    show: true,
                    feature: {
                        //  mark: { show: true },
                        dataZoom: { show: true },
                        //  dataView: { show: true },
                        //  magicType: { show: true, type: ['line', 'bar', 'stack', 'tiled'] },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                calculable: true,
                dataZoom: {
                    show: false,
                    realtime: true,
                    start: 0,
                    end: 100
                },
                xAxis: [
                    {
                        type: 'category',
                        boundaryGap: false,
                        data: function () {
                            var list = [];
                            for (var i = 0; i < xData.length; i++) {
                                list.push(xData[i]);
                            }
                            return list;
                        }(),
                        name: "时间",
                        axisLabel: {
                            interval: jg,
                            formatter: function (value) {
                                var xTime = value.split(' ')[1];

                                var hh = xTime.split(':')[0];
                                var mm = xTime.split(':')[1]; 
                                if (value.substring(10, value.length - 3) != "0:00") {
                                    if (mm > 40) {
                                        hh++;
                                        mm = "00";
                                    } else if (mm > 20 && mm < 40) {
                                        mm = "30";
                                    } else
                                        mm = "00";
                                }

                                return hh + ":" + mm;
                            }
                        }
                    }
                ],
                yAxis: [
                    {
                        type: 'value',
                        // min: 20,
                        // max: 150,
                        name: "时速",
                        scale: true,
                        axisLabel: { formatter: '{value} KM' }
                    }
                ],
                series:
                    function () {
                        var data = [];
                   
                        for (var i = 0; i < DeviceName.length; i++) {
                            var obj = {
                                name: DeviceName[i],
                                type: 'line',
                                symbol: "none",
                                clickable: false,
                                showAllSymbol: true,
                                markLine: {
                                    data: [
                                     //  { type: 'average', name: '平均时速' },
                                          // { name: '标线1起点', value: 50, xAxis: '00:00', yAxis: "20" }, { name: '标线2点', xAxis: '23:59', yAxis: '20' },
                                         //{ name: '标线1终点', xAxis: 150, yAxis: 120 }
                                    ]
                                },
                                markPoint: {
                                    data: [
                                        { type: 'max', name: '最大时速' }
                                        //,{ type: 'min', name: '最小时速' }
                                    ]
                                },
                                data:
                                    function () {
                                        var list = [];
                                        for (var j = 0; j < Speed.length; j++) {
                                            if (parseFloat(Speed[j]) <= 7) {
                                                Speed[j] = 0;
                                            }
                                            list.push(Speed[j]);
                                        }
                                        return list;
                                    }()
                            };
                        
                            data.push(obj);
                        }
                        return data;
                   
                    }()

            };
            // 为echarts对象加载数据 
            myChart.setOption(option);
        }
           );
};


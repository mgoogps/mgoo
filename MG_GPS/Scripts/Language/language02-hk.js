function writePage(msg) {
    //繁体
    document.write(msg);
}

var allPage = { tab1: "運行總覽", tab2: "報警總覽", tab3: "運行統計", tab4: "報警統計", startTime: "開始時間", endTime: "結束時間", search: "查 詢", num: "序號", deviceName: "設備名",
    time: "時間", distance: "里程(公里)", overspeed: "超速", noData: "沒有查詢到資料", lat: "緯度", lng: "經度", speed: "速度",  speedKM: "公里/小時", day: "天", hour: "小時",
    minute: "分鐘", pleSel: "請選擇", date: "日期", plsDeviceMsg: "請選擇設備", address: "地址", moreDevice: "更多設備", msg: "消息提醒", myAccount: "我的帳號", changePass: "修改密碼",
    tracking: "即時跟蹤", playback: "歷史軌跡", monitor: "定位監控", home: "主頁", customer: "客戶", report: "統計報表", more: "更多",  no: "序號", name: "名稱", carNum: "車牌號",
    imeiNo: "IMEI號", activeTime: "啟動時間", hireExpireTime: "到期時間", operation: "操作",  edit: "修改", divicesInfo: "設備資訊", cellName: "連絡人",
    phone: "電話", timezone: "時區", save: "保存", confirm: "確定", updateUserSuccess: "修改資料成功!", updateUserFailed: "修改資料失敗!", modelName: "型號", state: "狀態",
    drection: "方向", baidu: "百度", google: "穀歌", day: "天", hour: "小時", minute: "分鐘", stopTime: "停留時間", desc: "備註", cancel: "取消", del: "刪除", delSuccess: "刪除成功!",
    delFaild: "刪除失敗!", accStr: "ACC狀態", acc0: "關", acc1: "開", positionType: "定位方式", manDevice: "設備管理", type: "類型", acc2: "未接", resolve: "解析", startStopTime: "停留開始",
    endStopTime: "停留結束", status1: "未啟用", moving: "行駛", stopCar: "靜止",  arrears: "欠費", primaryEmail: "聯繫郵箱", positionTime: "定位時間", clear: "清除", targetName: "設備名",
    toExcel: "匯出Excel", distance2: "里程", km: "公里", m: "米", event: "事件記錄", inTime: "進入時間", outTime: "離開時間", noJur: "沒有許可權!", moneyCount: "點數", deviceHireDay: "代充天數", belongTo: "所屬用戶",
    updateTime: "更新時間", userInfo: "使用者資訊", userType2: "經銷商", changePassword: "修改密碼", service: "服務商", clearAll: "清除全部",

    deviceNo: "設備號(IMEI)", search1: "搜索",    message: "消息", exit: "退出",
    selSizeStr: "查詢到", sizeStr: "條記錄", createTime: "創建日期",    device: "設備", purchase: "進貨", stock: "庫存",
    mobileWebsite: "手機訪問",  simNo: "設備電話", expireTime: "設備到期", remark: "備註",  deviceInfo: "設備資訊", 
    dataError: "數據異常!", softFailed: "出現錯誤!", password: "密碼",  
    searchNull: "搜索條件為空!", initPass: "重置密碼",   loginAccount: "登陸帳號", passLengthMsg: "密碼長度不得大於20個字元", userType1: "用戶",
    add: "新增", information: "資料", deviceType: "設備類型", overspeed: "超速", kmHour: "公里/每小時", devicePhone: "設備手機卡號", modelName: "型號",
   deletes: "刪除", cusName: "客戶名", plsSelUser: "請選擇一個用戶!",
    plsSel: "請選擇", all: "全部", online: "線上", offline: "離線",   speedLimit: "超速限制",  
    allDistance: "總里程",  edit2: "編輯", deliveryTime: "出廠時間",
    cellPhone: "聯繫電話", type2: "類型", view: "查看", myDevice: "我的設備", StatisticalAnalysis: "分析統計"
};

//Dealer.aspx
var dealerPage = {
    warnTitle: "系統報警資訊提醒", warnSound: "開啟報警聲音", username: "客戶名/帳號", hello: "您好", changePassword: "修改密碼", searchDevice: "搜設備", searchUser: "搜客戶",
    searchDevice2: "搜索設備", searchUser2: "搜索用戶", name: "名稱", belongTo: "所屬用戶", sim: "設備手機卡號", activeTime: "開通日期", customerName: "客戶名稱", nowSearch: "正在搜索...",
    name2: "名稱", name3: "設備名稱"
};

//Home.aspx
var homePage = {
    quickSearch: "快速搜索", searchTxt: "IMEI號/設備名/車牌號", stage: "我的工作臺", deviceCount: "設備數量", operations: "更多操作", novice: "新手上路", quickSale: "快速銷售",
    batchSale: "批量銷售", customer: "銷售給", sale: "銷售", customerList: "客戶清單", addDevice: "添加設備", add: "添加", imeiNullMsg: "請選擇設備!", expireTimeNullMsg: "請選擇到期時間!",
    saleSuccess: "銷售成功!"
};


var courseName = { dueNorth: "正北", northeast: "東北", dueEast: "正東", southeast: "東南", dueSouth: "正南", southwest: "西南", dueWest: "正西", northwest: "西北" };

var reportPage = { title: "運行統計總覽", warnCount: "報警", stopCount: "停留" };

var alarmSumPage = { title: "報警統計總覽", lowCount: "低電報警", cutPowerCount: "斷電報警", vibCount: "震動報警", sosCount: "求救報警" }

var overSpeedPage = { continueTime: "持續時間", speedlimit: "超速值", distancePage: "里程統計", overspeedDetail: "超速詳單", stopDetail: "停留詳單" };

var alarmIndexPage = { geofenceIn: "進電子柵欄", geofenceOut: "出電子柵欄", moved: "位移報警", lowBattery: "低電報警", sos: "求救報警", cutPower: "斷電報警", vibration: "震動報警",
    overSpeed: "超速報警", offline: "離線報警"
};

var runindexPage = { statistics: "統計方式", statistics2: "按天統計", oilCoefficient: "百公里油耗係數", L: "升", oil: "油耗" };

var alarmDetailPage = { alarmTime: "報警時間", alarmType: "報警類型", alarmCount: "報警統計", alarmDetail: "報警詳單" };

var userPage = { warnTitle: "系統報警資訊提醒", warnSound: "開啟報警聲音", day7Exp: "7天內過期設備", day60Exp: "60天內過期設備", alreadyExp: "已過期設備",
    username: "客戶名/帳號", hello: "您好", searchDevice: "搜設備", searchUser: "搜客戶", exit: "退出", message: "消息", allDeivce: "全部設備", moneyMove: "點數轉讓", moneyHistory: "消費記錄"
};

var userInfoPage = { myAccount: "我的帳號", changePassword: "修改密碼", userMsg: "請完善以下資訊，比如聯繫電話、連絡人（若包含該項）", customerName: "客戶名稱",
    account: "登陸帳號", oldPass: "舊密碼", newPass: "新密碼", confirmPass: "確認密碼", passLengthMsg: "密碼長度不得大於20個字元", passNull: "密碼輸入不能為空!",
    passError: "2次密碼輸入不一致!", changePassSuccess: "修改密碼成功!", changePassError: "修改密碼失敗!", oldPassError: "舊密碼不對!", warnSendMsg: "報警附加通知方式",
    sendEmail: "郵件通知", service: "服務商"
};

var warnMessagePage = { warnMsg: "報警消息", handle1: "未處理", handle2: "已處理", alarmType: "報警類型", alarmTime: "報警時間" };

var trackingPage = { secondMsg: "秒後刷新!" };

var playbackPage = { from: "從", to: "到", play: "播 放", pause: "暫 停", next: "繼 續", fast: "快", slow: "慢", timeMsg: "結束時間大於開始時間!", nowLoading: "正在載入資料!",
    playOver: "播放完畢!", searchNull: "沒搜索到數據!", showLBS: "顯示LBS"
};

var geofencesPage = { geofence: "電子柵欄", addGeofence: "新增電子柵欄", geoNameNull: "電子柵欄名稱不能為空!", radius: "半徑(米)", delGeoConfirm: "確認刪除", delGeoConfirm2: "這個電子柵欄嗎?" ,
    addSuccess: "添加成功!", addFaild: "添加失敗!"
};

var iframeMapPage = { baiduMap: "百度地圖", googleMap: "穀歌地圖", deviceName: "設備名稱" };

var userUpdatePage = { account: "登錄名" };


var mapPage = { searchInput: '請輸入裝置名/IMEI號', divicesInfo: "設備資訊", geofence: "電子柵欄", cutOffPetrol: "遠程斷油電", restorePetrol: "遠程恢復油電", checkLocation: "查詢定位", checkCommand: "查詢指令記錄",
    sendConfirm: "發指令前請再次確認登錄帳號", passNull: "請輸入密碼!", passError: "密碼輸入錯誤!", sendMsg1: "正在發送指令,請稍後...", sendSuccess: "命令已經成功下發,等待設備回應...",
    sendMsg2: "命令無效", sendMsg3: "設備不存在", sendMsg4: "設備與伺服器已經斷開連接", sendMsg5: "下發不成功", responseSuccess: "設備成功返回!", responseNull: "還沒有獲取到設備返回資料!",
    checkLocatoin: "線上檢查位置", checkCommandTitle: "查詢指令記錄", cmdType: "指令類型", cmdState: "指令狀態", responseText: "回應信息", responseTime: "回應時間", sendTime: "發送時間",
    dyd: "斷油電", hfyd: "恢復油電", deviceResponse: "設備已返回", sendSuccess2: "命令已發送", noSend: "命令未發送", deviceDetailList: "設備詳細資訊清單", addGroup: "添加組", defaultGroup: "默認組",
    moveToGroup: "移至分組", delGroupConfirm: "這個分組嗎?", downloadLocation: "下載軌跡", deviceFortify: "終端設防", deviceDismiss: "終端撤防", accOn: "ACC開", accOff: "ACC關", fortify: "設防", dismiss: "撤防", carOpen: "車門開", carClose: "車門關", zdlj: "主電連接", zddk: "主電斷開",
    obdChecking: "OBD透傳", uploadTime: "上傳間隔", setQinqing: "設置親情號碼", setSOS: "設置SOS號碼", setZhukong: "主控號碼", setPassword: "修改密碼", setAutoFortify: "自動設防開啟",
    setAutoFortifyClose: "自動設防關閉", setCutFortifyAuto: "外部電源斷電自動設防開啟", setCutFortifyAutoClose: "外部電源斷電自動設防關閉", setVIBTime: "震動報警間隔時間設置", setVIBLmd: "震動靈敏度", setSOSType: "SOS報警方式",
    setWeiyiWarn: "移位報警設置", setOverspeed: "超速設置", setSMSGPRS: "短信/GPRS模式", setJianting: "監聽", setYccq: "遠程重啟", setHfcc: "恢復出廠", setLanguage: "語言設置",
    setTimezone: "時區設置", setXiumian: "休眠設置", setJiantingType: "監聽模式", setDingweiType: "定位模式", setParam: "參數查詢", setAutoFortifyTime: "自動設防時間", setAutoDismissTime: "自動撤防時間",
    setUploadMoveTime: "運動上傳頻率", setUploadStopTime: "靜止上傳頻率", setYcqd: "遠程啟動", setYcxh: "遠程熄火", setGeofence: "電子柵欄", setOBDUploadTime: "OBD上傳間隔",
    setOBDCMD: "OBD透傳命令", setOBDGg: "OBD透傳廣告", setDeviceUploadTime: "設置設備上傳間隔", danwei5s: "單位秒,最小10秒", obdTab1: "車輛狀態", obdTab2: "歷史資料", obdTab3: "OBD命令",
    inputCmdStr: "輸入命令內容", inputGgStr: "輸入廣告內容", secondsMsg1: "間隔不能小於5秒!", secondsMsg2: "間隔不能大於9999秒!", startTime: "開始時間", endTime: "結束時間",
    pidCount: "故障數量", pidStr: "故障說明", noGuzhang: "沒有故障", deetail: "詳細", obdDataTime: "資料時間", obdFdjfh: "發動機負荷", obdFdjsw: "發動機水溫", obdDsryxz: "短時燃油修正",
    obdCsryxz: "長時燃油修正", obdJqqgjdyl: "進氣歧管絕對壓力", obdFdjzs: "發動機轉速", obdClsd: "車輛速度", obdQgdhtqj: "1號汽缸點火提前角", obdJqwd: "進氣溫度", obdKqll: "空氣流量", obdJqmjdwz: "節氣門絕對位置",
    obdMILGzd: "MIL(故障燈)亮起後的行駛距離", obdRylsr: "燃油量輸入", obdDqyl: "大氣壓力", obdKzmkdy: "控制模組電壓", obdSsyh: "暫態油耗", obdMl: "馬力", obdZlc: "總里程", obdBglyh: "百公里油耗",
    obdDpdy: "電瓶電壓", obdGzdm: "故障代碼"
};

var mapPage2 = { searchInput: '請輸入裝置名/IMEI號', divicesInfo: "設備資訊", geofence: "電子柵欄", cutOffPetrol: "遠程斷油電", restorePetrol: "遠程恢復油電", checkLocation: "查詢定位", checkCommand: "查詢指令記錄",
    sendConfirm: "發指令前請再次確認登錄帳號", passNull: "請輸入密碼!", passError: "密碼輸入錯誤!", sendMsg1: "正在發送指令,請稍後...", sendSuccess: "命令已經成功下發,等待設備回應...",
    sendMsg2: "命令無效", sendMsg3: "設備不存在", sendMsg4: "設備與伺服器已經斷開連接", sendMsg5: "下發不成功", responseSuccess: "設備成功返回!", responseNull: "還沒有獲取到設備返回資料!",
    checkLocatoin: "線上檢查位置", checkCommandTitle: "查詢指令記錄", cmdType: "指令類型", cmdState: "指令狀態", responseText: "回應信息", responseTime: "回應時間", sendTime: "發送時間",
    dyd: "斷油電", hfyd: "恢復油電", deviceResponse: "設備已返回", sendSuccess2: "命令已發送", noSend: "命令未發送", deviceDetailList: "設備詳細資訊清單", addGroup: "添加組", defaultGroup: "默認組",
    moveToGroup: "移至分組", delGroupConfirm: "這個分組嗎?", downloadLocation: "下載軌跡", deviceFortify: "終端設防", deviceDismiss: "終端撤防", accOn: "ACC開", accOff: "ACC關", fortify: "設防", dismiss: "撤防", carOpen: "車門開", carClose: "車門關", zdlj: "主電連接", zddk: "主電斷開",
    obdChecking: "OBD檢測", uploadTime: "上傳間隔", setQinqing: "設置親情號碼", setSOS: "設置SOS號碼", setZhukong: "主控號碼", setPassword: "修改密碼", setAutoFortify: "自動設防開啟",
    setAutoFortifyClose: "自動設防關閉", setCutFortifyAuto: "外部電源斷電自動設防開啟", setCutFortifyAutoClose: "外部電源斷電自動設防關閉", setVIBTime: "震動報警間隔時間設置", setVIBLmd: "震動靈敏度", setSOSType: "SOS報警方式",
    setWeiyiWarn: "移位報警設置", setOverspeed: "超速設置", setSMSGPRS: "短信/GPRS模式", setJianting: "監聽", setYccq: "遠程重啟", setHfcc: "恢復出廠", setLanguage: "語言設置",
    setTimezone: "時區設置", setXiumian: "休眠設置", setJiantingType: "監聽模式", setDingweiType: "定位模式", setParam: "參數查詢", setAutoFortifyTime: "自動設防時間", setAutoDismissTime: "自動撤防時間",
    setUploadMoveTime: "運動上傳頻率", setUploadStopTime: "靜止上傳頻率", setYcqd: "遠程啟動", setYcxh: "遠程熄火", setGeofence: "電子柵欄", setOBDUploadTime: "OBD上傳間隔",
    setOBDCMD: "OBD透傳命令", setOBDGg: "OBD透傳廣告", setDeviceUploadTime: "設置設備上傳間隔", danwei5s: "單位秒,最小10秒", obdTab1: "車輛狀態", obdTab2: "歷史資料", obdTab3: "OBD命令",
    inputCmdStr: "輸入命令內容", inputGgStr: "輸入廣告內容", secondsMsg1: "間隔不能小於5秒!", secondsMsg2: "間隔不能大於9999秒!", startTime: "開始時間", endTime: "結束時間",
    pidCount: "故障數量", pidStr: "故障說明", noGuzhang: "沒有故障", deetail: "詳細", obdDataTime: "資料時間", obdFdjfh: "發動機負荷", obdFdjsw: "發動機水溫", obdDsryxz: "短時燃油修正",
    obdCsryxz: "長時燃油修正", obdJqqgjdyl: "進氣歧管絕對壓力", obdFdjzs: "發動機轉速", obdClsd: "車輛速度", obdQgdhtqj: "1號汽缸點火提前角", obdJqwd: "進氣溫度", obdKqll: "空氣流量", obdJqmjdwz: "節氣門絕對位置",
    obdMILGzd: "MIL(故障燈)亮起後的行駛距離", obdRylsr: "燃油量輸入", obdDqyl: "大氣壓力", obdKzmkdy: "控制模組電壓", obdSsyh: "暫態油耗", obdMl: "馬力", obdZlc: "總里程", obdBglyh: "百公里油耗",
    obdDpdy: "電瓶電壓", obdGzdm: "故障代碼"
};

var downloadLocation = { download: "下 載", help: "幫助", step: "步驟", step1: "1.選擇需要下載的指定設備。", step2: "2.輸入需要下載的具體日期。", step3: "3.按一下“下載KML軌跡檔”按鈕。",
    msg1: "注意：如果按一下“下載”按鈕， 出現“沒有找到有效的軌跡資料!”提示資訊，表示當前沒有你需要下載的資料。", msg2: "下載的軌跡檔的格式是Google KML格式，如:“檔案名.kml”。安裝 Google Earth後。",
    msg3: "按兩下KML檔，會通過Google Earth工具打開。", msg4: "KML軌跡檔會將設備的移動痕跡以紅線描繪在Google地圖上。", msg5: "附注:下載 Google Earth 請點擊", here: "這裡",
    msg6: "您可以通過“下載軌跡資料”功能，下載指定設備某一日期內的移動痕跡。"
};

//CustomersList.aspx
var cusPage = {
    delUserConfirm: "確認刪除", delUserConfirm2: "這個用戶嗎?", delUserMsg: "該使用者下有設備,不能刪除!", delUserMsg2: "該用戶下有別的用戶,不能刪除!", msgFailed: "刪除失敗!",
    updateUserSuccess: "修改資料成功!", updateUserFailed: "修改資料失敗!", delDeviceConfirm2: "這個設備嗎?", addCus: "新增客戶", manCus: "管理客戶", delCus: "刪除客戶", saleTo: "銷售給..",
    beDevice: "設備屬於", updateExpTime: "更新到期時間", deviceChange: "設備轉移", cusInfo: "客戶資訊", toCus: "目標客戶", newAddChildrenCus: "新增下級客戶", parentCus: "上級客戶",
    confirmPass: "確認密碼", allCus: "全部客戶", addCusTitle: "新增下級客戶", loginToUser: "登陸到終端平臺", childCus: "下級客戶", changeDevices: "批量轉移", updateExpDevices: "批量更新到期時間",
    addUserMsg: "請完善以下資訊，比如聯繫電話、連絡人（若包含該項）", plsParentCusMsg: "請選擇上級客戶!", msg1: "名稱,登陸帳號,密碼均不能為空!", passError: "2次密碼輸入不一致!",
    existAccount: "登陸名已經存在!", dataError: "資料登錄不合法!", dataNull: "名稱,登陸帳號,密碼均不能為空!", imeisNull: "IMEI號不能為空!", saveDevuceMsg1: "你一起輸入:", saveDevuceMsg2: "組IMEI號,",
    saveDevuceMsg3: "全部都已導入成功!", saveDevuceMsg4: "導入成功:", saveDevuceMsg5: "組", saveDevuceMsg6: "失敗:", saveDevuceMsg7: "發生錯誤!", updateExpSuccess: "修改到期時間成功!",
    updateError: "修改失敗!", changeDeviceSuccess: "設備轉移成功!", changeDeviceError: "設備轉移失敗!", confirmInitPassMsg1: "確定重置設備:", confirmInitPassMsg2: "的密碼嗎:",
    initPassSuccess: "密碼重置成功!", initPassError: "密碼重置失敗!", confirmInitUserPassMsg1: "確定重置用戶:", page: "每頁顯示", records: "條"
};

var moneyPage = { moveToAccount: "轉入登陸帳號", moveToUser: "轉入用戶名", moveCount: "轉入點數", check: "檢測", noLoginName: "不存在此登陸名!", inputLoginNameIn: "請輸入轉入登錄名!",
    inputLoginNameOut: "請檢測轉入登錄名!", noMoveSelf: "不能轉入給自己!", moneyLack: "點數不夠!", moneyError: "轉入異常!", moveSuccess: "轉入成功,當前點數:", inputMoneyMsg: "請輸入轉入點數!",
    uToUser: "你給用戶", money: "點", user: "用戶", moveMoneyMsg1: "給你轉入點數", give: "給", moneyMsg2: "台設備充值,使用總點數", day: "天", msg1: "1個點數1天", oneYeah: "一年", twoYeah: "二年",
    lifelong: "終身", msg2: "增加到期天數", msg3: "設備充值天數成功!", msg4: "點數不夠,請充值!", msg5: "天數必須大於0!", pointManagement: "點數管理"
};

var productUpdatePage = { oilCoefficient: "百公里油耗係數", updateIcon: "更換圖示", carNumMsg1: "車牌號長度過長!", sccuess: "更新成功!", faild: "更新失敗!", isExistMsg: "車牌號已經存在,請換一個!", filterLBS: "過濾LBS", photo: "圖片" };

var yiwen201405 = { fsManger: "4S店管理", battery: "電池電量", realCar: "即時車況", obdData: "OBD資料記錄", oilInfo: "油耗信息", carBaoyang: "車輛保養", carBaoyangSet: "里程保養設置",
    carBaoxian: "車輛保險", carNianshen: "車輛年審", userData: "使用者資料", maintainCar: "需保養車輛", fsSet: "4S店設置", baoyangData: "保養資料錄入", baoyangItem: "保養名目",
    baoyangPrice: "保養價格", baoyangDate: "保養日期", maintenanceofmiles: "保養時里程(公里)", repairShop: "修理廠", notes: "備註", fuelInput: "加油量資訊錄入", restFuel: "還剩油量(升)",
    lastrefuel: "上次加油行駛里程(公里)", fuelMsg: "當前加油時間時設備總里程減去上次加油時間時設備總里程", automatic: "自動計算", fuelDate: "加油時間", oilLabel: "機油標號",
    refuelLiter: "加油(升)", unitPrice: "每升單價(元)", refuelAddress: "加油地點", fsInfoSet: "4S店資訊設置", fsName: "4S店名稱", fsZuchePhone: "4S租車服務電話", fsSalePhone: "4S二手車銷售電話",
    fsSOSPhone: "4S救援電話", fsDaijiaPhone: "4S代駕電話", fsBaoxianPhone: "4S報險電話", fsUsername: "4S店連絡人", fsPhone: "聯繫電話", fsCarSalePhone: "銷售電話", fsServicePhone: "客服電話",
    location: "所在位置", about: "簡介", carByInterval: "車輛保養間隔提示設置", byIntervalMileage: "保養間隔里程", byLastMileage: "平臺上次提示時里程", byMsg2: "公里(設備在平臺'未啟用',設置總里程無效)",
    byMsg3: "溫馨提示:非新車轉移至平臺,或沒按規定里程去保養,或資料不正確,可通過手動修改\"平臺上次提示時里程\"和\"總里程\"來達到糾正資料的作用.", byMsg4: "保養提示間隔需大於500公里!", dayDistance: "當日里程",
    dayOil: "當日油耗", baoxianCompany: "車險的投保公司", baoxianNo: "車險保單的號碼", baoxianStart: "保單生效啟用日期", baoxianEnd: "保單到期日期", baoxianUsername: "車險理賠員姓名", baoxianUserPhone: "車險理賠員電話",
    baoxianDetail: "保單的詳細資訊", baoxianDesc: "保單的其他說明", displayType: "顯示方式", detailList: "詳細列表", dailyCount: "日計", monthlyCount: "月計", yearlyCount: "年計",
    nianshenDate: "年審時間", nextNianshenDate: "下次年審時間", addRefuel: "新增加油記錄", baoyangList: "需要保養的車輛清單", byMsg6: "顯示距離下次保養里程500公里內的車輛列表", msg1: "請輸入加*必填項",
    baoyangMsg7: "確定刪除這條保養資訊嗎?", baoyangAllPrice: "保養總價格", lastRest: "上次剩餘(升)", userL: "本記錄使用(升)", moveDistance: "行駛里程", oilMsg1: "百公里平均油耗(升)",
    kilometerPrice: "每公里費用", oilMsg2: "確定刪除這條加油資訊嗎", baoyangInterval: "保養間隔(公里)", lastBaoyang: "上次提示里程(公里)", nextBaoyang: "距下次保養里程(公里)", nowDistance: "獲取當前里程", addBaoyang: "新增保養記錄", luruDate: "錄入日期",
    price: "金額"
};

var yiwen201407 = { singleMileage: "單次里程", fuel_consumption: "單程耗油量", engineSpeed: "單次行程最大轉速", max_speed: "單次行程最大速度", hard_acceleration_times: "急加速次數",
    engine_running_time: "發動機啟動後執行時間", distance_after_fault_lights: "故障燈亮後行駛距離", fuel_level: "燃油液位", single_idle_fuel_consumption: "單次怠速耗油量",
    average_fuel_consumption: "平均油耗", fuel_consumption2: "總耗油量", mileage_after_install: "安裝後行駛里程", hot_start_time: "暖開機時間", the_current_speed: "當前速度下行駛百公里油耗",
    singel_driving_time: "單次駕駛時間", singel_idle_time: "單次怠速時間", resolved_failure_code: "未確定故障碼", Unresolved_failure_code: "已確定故障碼",second:"秒",times:"次",examRecord:"體檢記錄"
};

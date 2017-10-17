function writePage(msg) {
    //繁体
    document.write(msg);
}

var allPage = { deviceNo: "設備號(IMEI)", search: "搜索", tracking: "即時跟蹤", playback: "歷史軌跡", monitor: "定位監控", home: "主頁", customer: "我的客戶", message: "消息", exit: "退出",
    selSizeStr: "查詢到", sizeStr: "條記錄", createTime: "創建日期", operation: "操作", no: "序號", cellName: "連絡人", phone: "電話", device: "設備", purchase: "進貨", stock: "庫存",
    mobileWebsite: "手機訪問", deviceName: "設備名", simNo: "設備電話", expireTime: "設備到期", remark: "備註", cancel: "取消", deviceInfo: "設備資訊", imeiNo: "IMEI號",
    dataError: "數據異常!", softFailed: "出現錯誤!", password: "密碼", confirm: "確定", num: "序號", noData: "沒有查詢到資料", acc0: "關", acc1: "開",
    acc2: "未接", searchNull: "搜索條件為空!", initPass: "重置密碼", updateTime: "更新時間", loginAccount: "登陸帳號", passLengthMsg: "密碼長度不得大於20個字元", type: "類型", userType1: "用戶",
    userType2: "經銷商", address: "地址", add: "新增", information: "資料", deviceType: "設備類型", overspeed: "超速", kmHour: "公里/每小時", devicePhone: "設備手機卡號", modelName: "型號",
    createTime: "創建時間", activeTime: "啟動時間", hireExpireTime: "到期時間", edit: "修改", more: "更多", deletes: "刪除", cusName: "客戶名", userInfo: "使用者資訊", plsSelUser: "請選擇一個用戶!",
    km: "公里", plsSel: "請選擇", all: "全部", online: "線上", offline: "離線", arrears: "欠費", carNum: "車牌號", speedLimit: "超速限制", lat: "緯度", lng: "經度", speed: "速度", drection: "方向",
    allDistance: "總里程", state: "狀態", positionTime: "定位時間", status1: "未啟用", moving: "行駛", stopCar: "靜止", accStr: "ACC狀態", edit2: "編輯", deliveryTime: "出廠時間",
    cellPhone: "聯繫電話", save: "保存", clear: "清除", type2: "類型", view: "查看", noJur: "沒有許可權!", deviceHireDay: "代充天數", service: "服務商",myDevice:"我的設備"
};

//login.aspx
var loginPage = { title: "GPS定位平臺", accountTab: " 帳號/手機/IMEI ", imeiTab: "車牌號/IMEI號", account: "登錄名", password: "密碼", loginSubmit: "登陸", languageMsg: "我們提供" ,loginMsg: "請輸入賬號.",
    loginMsg2: "請輸入密碼", loginMsg3: "輸入不能為空！", wytx: "我要體驗", androidBaidu: "android(百度地圖)下載", androidGoogle: "android(google地圖)下載", iphone: "iphone越獄版下載",
    welcome: "歡迎您，請登錄！", loginErrorMsg: "賬號或者密碼輸入錯誤", loginFailure: "登錄失敗", ForgotPassword: "忘記密碼？", RememberPassword: "記住用戶名和密碼", Register: "註冊"
};


//Dealer.aspx
var dealerPage = { warnTitle: "系統報警資訊提醒", warnSound: "開啟報警聲音", username: "客戶名/帳號", hello: "您好", changePassword: "修改密碼", searchDevice: "搜設備", searchUser: "搜客戶",
    searchDevice2: "搜索設備", searchUser2: "搜索用戶", name: "名稱", belongTo: "所屬用戶", sim: "設備手機卡號", activeTime: "開通日期", customerName: "客戶名稱", nowSearch: "正在搜索...",
    name2: "名稱", name3: "設備名稱"
};

//Home.aspx
var homePage = { quickSearch: "快速搜索", searchTxt: "IMEI號/設備名/車牌號" ,stage:"我的工作臺",deviceCount:"設備數量",operations:"更多操作",novice:"新手上路",quickSale:"快速銷售",
    batchSale: "批量銷售", customer: "銷售給", sale: "銷售", customerList: "客戶清單", addDevice: "添加設備", add: "添加", imeiNullMsg: "請選擇設備!", expireTimeNullMsg: "請選擇到期時間!",
    saleSuccess:"銷售成功!"
};

var warnMessagePage = { alarmType: "報警類型", alarmTime: "報警時間" };

var alarmIndexPage = { geofenceIn: "進電子柵欄", geofenceOut: "出電子柵欄", moved: "位移報警", lowBattery: "低電報警", sos: "求救報警", cutPower: "斷電報警", vibration: "震動報警",
    overSpeed: "超速報警", offline: "離線報警"
};

//map.aspx
var mapPage = { searchInput: '請輸入裝置名/IMEI號', divicesInfo: "設備資訊", geofence: "電子柵欄", cutOffPetrol: "遠程斷油電", restorePetrol: "遠程恢復油電", checkLocation: "查詢定位", checkCommand: "查詢指令記錄",
    sendConfirm: "發指令前請再次確認登錄帳號", passNull: "請輸入密碼!", passError: "密碼輸入錯誤!", sendMsg1: "正在發送指令,請稍後...", sendSuccess: "命令已經成功下發,等待設備回應...",
    sendMsg2: "命令無效", sendMsg3: "設備不存在", sendMsg4: "設備與伺服器已經斷開連接", sendMsg5: "下發不成功", responseSuccess: "設備成功返回!", responseNull: "還沒有獲取到設備返回資料!",
    checkLocatoin: "線上檢查位置", checkCommandTitle: "查詢指令記錄", cmdType: "指令類型", cmdState: "指令狀態", responseText: "回應信息", responseTime: "回應時間", sendTime: "發送時間",
    dyd: "斷油電", hfyd: "恢復油電", deviceResponse: "設備已返回", sendSuccess2: "命令已發送", noSend: "命令未發送", deviceDetailList: "設備詳細資訊清單", addGroup: "添加組", defaultGroup: "默認組",
    moveToGroup: "移至分組", delGroupConfirm: "這個分組嗎?", downloadLocation: "下載軌跡", deviceFortify: "終端設防", deviceDismiss: "終端撤防", accOn: "ACC開", accOff: "ACC關", fortify: "設防", dismiss: "撤防", carOpen: "車門開", carClose: "車門關", zdlj: "主電連接", zddk: "主電斷開",
    obdChecking: "OBD透傳", uploadTime: "上傳間隔", setQinqing: "設置親情號碼",setSOS: "設置SOS號碼",setZhukong: "主控號碼",setPassword: "修改密碼",setAutoFortify: "自動設防開啟",
    setAutoFortifyClose: "自動設防關閉",setCutFortifyAuto: "外部電源斷電自動設防開啟",setCutFortifyAutoClose: "外部電源斷電自動設防關閉",setVIBTime: "震動報警間隔時間設置",setVIBLmd: "震動靈敏度",setSOSType: "SOS報警方式",
    setWeiyiWarn: "移位報警設置",setOverspeed: "超速設置",setSMSGPRS: "短信/GPRS模式",setJianting: "監聽",setYccq: "遠程重啟",setHfcc: "恢復出廠",setLanguage: "語言設置",
    setTimezone: "時區設置",setXiumian: "休眠設置",setJiantingType: "監聽模式",setDingweiType: "定位模式",setParam: "參數查詢",setAutoFortifyTime: "自動設防時間",setAutoDismissTime: "自動撤防時間",
    setUploadMoveTime: "運動上傳頻率",setUploadStopTime: "靜止上傳頻率",setYcqd: "遠程啟動",setYcxh: "遠程熄火",setGeofence: "電子柵欄",setOBDUploadTime: "OBD上傳間隔",
    setOBDCMD: "OBD透傳命令", setOBDGg: "OBD透傳廣告", setDeviceUploadTime: "設置設備上傳間隔", danwei5s: "單位秒,最小10秒", obdTab1: "車輛狀態", obdTab2: "歷史資料", obdTab3: "OBD命令",
    inputCmdStr: "輸入命令內容", inputGgStr: "輸入廣告內容", secondsMsg1: "間隔不能小於5秒!", secondsMsg2: "間隔不能大於9999秒!", startTime: "開始時間", endTime: "結束時間",
    pidCount: "故障數量", pidStr: "故障說明", noGuzhang: "沒有故障", deetail: "詳細", obdDataTime: "資料時間", obdFdjfh: "發動機負荷", obdFdjsw: "發動機水溫", obdDsryxz: "短時燃油修正",
    obdCsryxz: "長時燃油修正", obdJqqgjdyl: "進氣歧管絕對壓力", obdFdjzs: "發動機轉速", obdClsd: "車輛速度", obdQgdhtqj: "1號汽缸點火提前角", obdJqwd: "進氣溫度", obdKqll: "空氣流量", obdJqmjdwz: "節氣門絕對位置",
    obdMILGzd: "MIL(故障燈)亮起後的行駛距離", obdRylsr: "燃油量輸入", obdDqyl: "大氣壓力", obdKzmkdy: "控制模組電壓", obdSsyh: "暫態油耗", obdMl: "發動機執行時間", obdZlc: "總里程", obdBglyh: "百公里油耗",
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
    setOBDCMD: "OBD透傳命令", setOBDGg: "OBD透傳廣告", setDeviceUploadTime: "設置設備上傳間隔", danwei5s: "單位秒,最小5秒", obdTab1: "車輛狀態", obdTab2: "歷史資料", obdTab3: "OBD命令",
    inputCmdStr: "輸入命令內容", inputGgStr: "輸入廣告內容", secondsMsg1: "間隔不能小於5秒!", secondsMsg2: "間隔不能大於9999秒!", startTime: "開始時間", endTime: "結束時間",
    pidCount: "故障數量", pidStr: "故障說明", noGuzhang: "沒有故障", deetail: "詳細", obdDataTime: "資料時間", obdFdjfh: "發動機負荷", obdFdjsw: "發動機水溫", obdDsryxz: "短時燃油修正",
    obdCsryxz: "長時燃油修正", obdJqqgjdyl: "進氣歧管絕對壓力", obdFdjzs: "發動機轉速", obdClsd: "車輛速度", obdQgdhtqj: "1號汽缸點火提前角", obdJqwd: "進氣溫度", obdKqll: "空氣流量", obdJqmjdwz: "節氣門絕對位置",
    obdMILGzd: "MIL(故障燈)亮起後的行駛距離", obdRylsr: "燃油量輸入", obdDqyl: "大氣壓力", obdKzmkdy: "控制模組電壓", obdSsyh: "暫態油耗", obdMl: "馬力", obdZlc: "總里程", obdBglyh: "百公里油耗",
    obdDpdy: "電瓶電壓", obdGzdm: "故障代碼"
};

var courseName = { dueNorth: "正北", northeast: "東北", dueEast: "正東", southeast: "東南", dueSouth: "正南", southwest: "西南", dueWest: "正西", northwest: "西北" };

//CustomersList.aspx
var cusPage = { delUserConfirm: "確認刪除", delUserConfirm2: "這個用戶嗎?", delUserMsg: "該使用者下有設備,不能刪除!", delUserMsg2: "該用戶下有別的用戶,不能刪除!", msgFailed: "刪除失敗!",
    updateUserSuccess: "修改資料成功!", updateUserFailed: "修改資料失敗!", delDeviceConfirm2: "這個設備嗎?", addCus: "新增客戶", manCus: "管理客戶", delCus: "刪除客戶", saleTo: "銷售給..",
    beDevice: "設備屬於", updateExpTime: "更新到期時間", deviceChange: "設備轉移", cusInfo: "客戶資訊", toCus: "目標客戶", newAddChildrenCus: "新增下級客戶", parentCus: "上級客戶",
    confirmPass: "確認密碼", allCus: "全部客戶", addCusTitle: "新增下級客戶", loginToUser: "登陸到終端平臺", childCus: "下級客戶", changeDevices: "批量轉移", updateExpDevices: "批量更新到期時間",
    addUserMsg: "請完善以下資訊，比如聯繫電話、連絡人（若包含該項）", plsParentCusMsg: "請選擇上級客戶!", msg1: "名稱,登陸帳號,密碼均不能為空!", passError: "2次密碼輸入不一致!",
    existAccount: "登陸名已經存在!", dataError: "資料登錄不合法!", dataNull: "名稱,登陸帳號,密碼均不能為空!", imeisNull: "IMEI號不能為空!", saveDevuceMsg1: "你一起輸入:", saveDevuceMsg2: "組IMEI號,",
    saveDevuceMsg3: "全部都已導入成功!", saveDevuceMsg4: "導入成功:", saveDevuceMsg5: "組", saveDevuceMsg6: "失敗:", saveDevuceMsg7: "發生錯誤!", updateExpSuccess: "修改到期時間成功!",
    updateError: "修改失敗!", changeDeviceSuccess: "設備轉移成功!", changeDeviceError: "設備轉移失敗!", confirmInitPassMsg1: "確定重置設備:", confirmInitPassMsg2: "的密碼嗎:",
    initPassSuccess: "密碼重置成功!", initPassError: "密碼重置失敗!", confirmInitUserPassMsg1: "確定重置用戶:", page: "每頁顯示", records: "條"
};

var productUpdatePage = { oilCoefficient: "百公里油耗係數", updateIcon: "更換圖示", carNumMsg1: "車牌號長度過長!", sccuess: "更新成功!", faild: "更新失敗!", isExistMsg: "車牌號已經存在,請換一個!", filterLBS: "過濾LBS", photo: "圖片" };

var moneyPage = { day: "天", msg1: "1個點數1天", oneYeah: "一年", twoYeah: "二年", lifelong: "終身", msg2: "增加到期天數", msg3: "設備充值天數成功!", msg4: "點數不夠,請充值!", msg5: "天數必須大於0!", msg6: "不能轉入給自己!", pointGive: "點數轉讓", pointConsumptionLog: "點數消費記錄", loginNameNotExist: "不存在此登錄名", intoLoginID: "轉入登陸帳號", intoUsername: "轉入用戶名", intoPoint: "轉入點數", detect: "檢測", pleaseDetectLoginName: "請檢測轉入登錄名", pleaseInputIntoPoint: "請輸入轉入點數" };

var yiwen201405 = { battery: "電池電量" };

var yiwen201407 = { nowPosition: "當前位置", message: "消息中心", moveCustomer: "客戶轉移", importExcel: "匯出Excel", moveMsg1: "不能將客戶轉移給自己或下級客戶!",
    moveMsg2: "你確定要將客戶", moveMsg3: "轉移到", moveMsg4: "用戶下嗎?", moveMsg5: "請選擇上級用戶!", moveMsg6: "轉移失敗!", moveMsg7: "不能將客戶轉移給自己或下級客戶!",
    moveMsg8: "轉移成功!", userList: "用戶列表", setSOS: "設置SOS號碼", sosPhone: "SOS號碼", setQinqing: "設置親情號碼", qqPhone: "親情號碼", elec: "電量",
    gsm0: "無信號", gsm1: "信號極弱", gsm2: "信號較弱", gsm3: "信號良好", gsm4: "信號強", deviceStatus: "狀態查詢", restart: "遠程重啟", setUploadTimeMsg: "間隔不能小於10秒!",
    moreCommand: "更多指令", centerPhone: "中心號碼", version: "版本查詢", touchuan: "透傳"
}

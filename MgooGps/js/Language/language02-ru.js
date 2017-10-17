function writePage(msg) {
    //俄语
    document.write(msg);
}

var allPage = { tab1: "Перемещение краткого обзора", tab2: "датчиков тревоги", tab3: "Статистика устройства", tab4: "Сигнальный граф", startTime: "От", endTime: "К", search: "Поиск", num: "Нет.", 
    time: "Время", distance: "Пробег (км)", overspeed: "Превышение скорости", noData: "Никакой data！", lat: "Широта", lng: "Longtitude", speed: "Скорость",   speedKM: "км/ч", day: "День", hour: "Час",
    minute: "Минута", pleSel: "Пожалуйста, выберите", date: "Дата", plsDeviceMsg: "Пожалуйста, выберите цель",   moreDevice: "Более оборудования", msg: "Сообщение", myAccount: "Мой счет", changePass: "Пароль изменения",
    tracking: "Прослеживание", playback: "Воспроизведение", monitor: "Монитор", home: "Главная страница", customer: "Клиент", report: "Статистика", more: "Больше",   no: "Нет.", name: "Имя", 
    imeiNo: "IMEI нет.", activeTime: "Время активации", hireExpireTime: "Из-за",   edit: "Править", divicesInfo: "Информация об устройстве", cellName: "Контакты",
    phone: "Телефон/Моб", timezone: "Timezone", save: "Сохранить",   updateUserSuccess: "Отредактируйте успешно!", updateUserFailed: "Отредактируйте неудачу!", modelName: "Напечатать", state: "Статус",
    drection: "Руководство", baidu: "Baidu", google: "Google", day: "День", hour: "Час", minute: "Минута", stopTime: "Остановите время", desc: "Замечание",  del: "Удалить", delSuccess: "Удалите успешно!",
    delFaild: "Удалите неудачу!", accStr: "Статус ACC", acc0: "Прочь", acc1: "На", positionType: "Тип положения", manDevice: "Управление устройством", type: "Напечатать", resolve: "Решение", startStopTime: "Остановиться",
    endStopTime: "Начать", status1: "Вышедший", moving: "Перемещение", stopCar: "Остановиться",  primaryEmail: "Электронная почта", positionTime: "Время положения", clear: "Ясный", targetName: "Целевое имя",
    arrears: "overdue", toExcel: "To Excel", distance2: "Пробег", km: "km", m: "M", event: "Event recording", inTime: "Enter time", outTime: "Departure time", moneyCount: "点数", deviceHireDay: "代充点数", belongTo: "所属用户",  

    deviceNo: "Число IMEI", message: "Сообщение", exit: "Выход",
    selSizeStr: "Проверить", sizeStr: "отчеты", createTime: "Задайте время", operation: "Операция", no: "Нет.",   device: "Цель", purchase: "Покупка", stock: "Запас",
    mobileWebsite: "Доступ по мобильному телефону", deviceName: "Целевое имя", simNo: "Сим-карта НЕТ.", expireTime: "Из-за", remark: "Замечание", cancel: "Отменить", deviceInfo: "Информация об устройстве", 
    dataError: "Ошибка данных!", softFailed: "Ошибка!", password: "Пароль", confirm: "Сохранить", 
    acc2: "Несвязанный", searchNull: "IMEI/Имя/Cat NO./Имя/Аккаунт требуется!", initPass: "Сброс", updateTime: "Обновить время", loginAccount: "Счет логина", passLengthMsg: "Пароль должен быть меньше чем 20 цифр",  userType1: "Конечный пользователь",
    userType2: "Дистрибьютор", address: "Адрес", add: "Новый", information: "Информация", deviceType: "Напечатать",   kmHour: "Км/ч", devicePhone: "Сим-карта НЕТ.", 
   hireExpireTime2: "Из-за",  deletes: "Удалить", cusName: "Имя клиента", userInfo: "Информация о клиенте", plsSelUser: "Пожалуйста, выберите пользователя!",
   plsSel: "Выбрать", all: "Все", online: "Онлайн", offline: "Офлайн", carNum: "Номерной знак нет.", speedLimit: "Ограничение скорости", 
    allDistance: "Полный пробег" ,    edit2: "Править", deliveryTime: "Время активации",
    cellPhone: "Телефон/Моб", type2: "Роли", view: "View", StatisticalAnalysis: "статистический анализ"
};
 
var dealerPage = {
    warnTitle: "Напоминание", warnSound: "Сигнальный звук на", username: "Имя клиента / Счет", hello: "Здравствуйте", changePassword: "Пароль изменения", searchDevice: "Цель", searchUser: "Клиент",
    searchDevice2: "Целевая проверка", searchUser2: "Клиентская проверка", name: "Имя клиента", belongTo: "Членство", sim: "Сим-карта НЕТ.", activeTime: "Дата активации", customerName: "Имя клиента", nowSearch: "Поиск …",
    name2: "Имя клиента", name3: "Целевое имя"
};
 
var homePage = {
    quickSearch: "Поиск", searchTxt: "IMEI Номер/T/Авто Номер.", stage: "Стадия", deviceCount: "Целевой граф", operations: "Больше", novice: "новичок", quickSale: "Быстрая продажа",
    batchSale: "Продажа партии", customer: "Клиент", sale: "Продажа", customerList: "потребительский Список", addDevice: "Дополнительный", add: "Новый", imeiNullMsg: "Пожалуйста, выберите цель!", expireTimeNullMsg: "Пожалуйста, выберите дату с истекшим сроком!",
    saleSuccess: "Успех!", editDeviceType: "Редактор Тип устройства"
};
 

var courseName = { dueNorth: "Север", northeast: "Северо-восток", dueEast: "Восток", southeast: "Юго-восток", dueSouth: "Юг", southwest: "Юго-запад", dueWest: "Запад", northwest: "Северо-запад" };

var reportPage = { title: "Полная статистика движения", warnCount: "Сигнализация", stopCount: "Стоянка" };

var alarmSumPage = { title: "Полная сигнальная статистика", lowCount: "Звуковой сигнал", cutPowerCount1: "Виброзвонок", vibCount: "Дверной датчик сигнализации", cutPowerCount: "Тревога сокращения", sosCount: "Тревога SOS" }

var overSpeedPage = { continueTime: "Продолжите время", speedlimit: "Ограничение скорости", distancePage: "Отчет о пробеге", overspeedDetail: "Детали превышения скорости", stopDetail: "Остановите детали" };

var alarmIndexPage = { geofenceIn: "Геозона в", geofenceOut: "Геозона", moved: "Автомобиль перемещен", lowBattery: "Тревога низкого уровня заряда", sos: "Тревога SOS", cutPower: "Тревога сокращения", vibration: "Тревога вибрации",
    overSpeed: "Превышение скорости", offline: "Offline"
};

var runindexPage = { statistics: "Вопрос", statistics2: "Ежедневные детали", oilCoefficient: "Содействующие/100 Километры Потребления топлива", L: "L", oil: "Потребление топлива" };

var alarmDetailPage = { alarmTime: "Сигнальное время", alarmType: "Сигнальный тип", alarmCount: "Сигнальная статистика", alarmDetail: "Сигнальные детали", deviceTime: "время", Power: "власть", deviceStatus: "состояние устройства", workModel: "режим работы", 撤防: "разоружать", 设防: "укрепление", 静音: "Отключение", 免提: "громкая" };

var userPage = { warnTitle: "Сигнальный краткий обзор", warnSound: "Сигнальный звук на", day7Exp: "Истекли 7 Дней", day60Exp: "Истекли 60 Дней", alreadyExp: "С истекшим сроком",
    username: "Имя/Счет", hello: "Здравствуйте", searchDevice: "Цель", searchUser: "Клиент", exit: "Выход", message: "Сообщение", allDeivce: "Вся цель", moneyMove: "点数转让", moneyHistory: "点数消费记录"
};

var userInfoPage = { myAccount: "Мой счет", changePassword: "Пароль изменения", userMsg: "Наконечник: Полная информация, такая как номер телефона, связаться.", customerName: "Имя клиента",
    account: "Счет логина", oldPass: "Существовавший пароль", newPass: "Новый пароль", confirmPass: "Подтверждение пароля", passLengthMsg: "Пароль должен быть меньше чем 20 цифрами", passNull: "Вход пароля не может быть чистым!",
    passError: "Эти два входа паролей отличаются!", changePassSuccess: "Сброс пароля успешно!", changePassError: "Пароль перезагрузил неудачу!", oldPassError: "Старая ошибка пароля!", warnSendMsg: "Дополнительные сигнальные способы",
    sendEmail: "Электронная почта", service: "My Service"
};
var productUpdatePage = { oilCoefficient: "Содействующие/100 Километры Потребления топлива", updateIcon: "Выберите символ", carNumMsg1: "Автомобиль НЕТ. слишком длинное!", sccuess: "Успешно обновилось!", faild: "Неудавшееся обновление!", isExistMsg: "Автомобильный номер существовал, пожалуйста попробуйте другого!", filterLBS: "filter LBS", photo: "Photo" };
 
var warnMessagePage = { warnMsg: "Сигнальное сообщение", handle1: "Неимевший дело", handle2: "Ручной", alarmType: "Сигнальный тип", alarmTime: "Сигнальное время" };

var trackingPage = { secondMsg: "Перегрузите через пару секунд!" };

var playbackPage = { from: "От", to: "К", play: "Игра", pause: "Пауза", next: "Продолжить", fast: "Быстро", slow: "Медленный", timeMsg: "Время окончания должно быть позже, чем Начальное время！", nowLoading: "Загрузка файлов！",
    playOver: "Конец！", searchNull: "Никаких файлов！", showLBS: "Show LBS"
};

var geofencesPage = { geofence: "Геозона", addGeofence: "Добавить", geoNameNull: "Имя геозоны не может быть чистым!", radius: "R (m)", delGeoConfirm: "Подтвердите, чтобы удалить", delGeoConfirm2: "Эта Геозона?" };

var iframeMapPage = { baiduMap: "Карта Байду", googleMap: "Карта Гугл" };

var userUpdatePage = { account: "Счет" };

//map.aspx
var mapPage = { searchInput: 'Пожалуйста, вход name/IMEI Нет.', divicesInfo: "Целевая информация", geofence: "Геозона", cutOffPetrol: "Отключите бензин", restorePetrol: "Восстановите бензин", checkLocation: "Проверьте местоположение", checkCommand: "Проверьте команду",
    sendConfirm: "Пожалуйста, подтвердите счет прежде, чем послать команду", passNull: "Пожалуйста, входной пароль!", passError: "Ошибка пароля!", sendMsg1: "Посылая сообщение, пожалуйста, ждите …", sendSuccess: "Успех! Пожалуйста, ждите ответа …",
    sendMsg2: "Пустой указатель команды", sendMsg3: "Цель не существует", sendMsg4: "Цель разъединила сервер.", sendMsg5: "Пошлите команду успешно!", responseSuccess: "Сообщение ответа", responseNull: "Пустой указатель ответа!",
    checkLocatoin: "Проверьте местоположение", checkCommandTitle: "Проверьте команду", cmdType: "Тип команды", cmdState: "Статус команды", responseText: "Сообщение ответа", responseTime: "Время отклика", sendTime: "Пошлите время",
    dyd: "Отключите бензин", hfyd: "Восстановите бензин", deviceResponse: "Ответ устройства", sendSuccess2: "Команду послали!", noSend: "Команду не послали", deviceDetailList: "Список детали устройства", addGroup: "Добавьте группу", defaultGroup: "Неверная группа",
    moveToGroup: "Движение группы", delGroupConfirm: "Удалите подтверждение?",
    changeLanguage: "Установить устройство языка", remoteFortification: "Удаленная Постановка / снятие", remoteCutOil: "Удаленная от нефти / выключения", remoteRestorationOil: "Удаленная добычи нефти / электроэнергии", familyNumber: "Установить число семей", speedLimit: "Установить ограничения скорости",
    uploadTimeInterval: "Установите интервал загрузки", downloadLocation: "Tracking Report", deviceFortify: "GPS Fortify", deviceDismiss: "GPS Dismiss", accOn: "ACC ON", accOff: "ACC OFF", fortify: "ARM", dismiss: "DimARM", carOpen: "Door Open", carClose: "Door Close", zdlj: "engine connect",
    zddk: "engine cut", obdChecking: "OBD check", uploadTime: "upload interval", setQinqing: "Set family number", setSOS: "Set SOS number", setZhukong: "Set master number", setPassword: "change password", setAutoFortify: "Auto ARM ON",
    setAutoFortifyClose: "Auto ARM OFF", setCutFortifyAuto: "external power cut engine, auto ARM ON", setCutFortifyAutoClose: "external power cut engine, auto ARM Off", setVIBTime: "Set Shake alarm timespan", setVIBLmd: "Set Shake Sensitivity", setSOSType: "SOS alarm",
    setWeiyiWarn: "Set Move Alarm", setOverspeed: "Set OverSpeed alarm", setSMSGPRS: "SMS/GPRS mode", setJianting: "Monitor", setYccq: "Remote Reboot", setHfcc: "factory reset", setLanguage: "Set language",
    setTimezone: "Set time zone", setXiumian: "Set Sleep Model", setJiantingType: "monitor mode", setDingweiType: "positioning mode", setParam: "data check", setAutoFortifyTime: "Auto ARM time", setAutoDismissTime: "Auto DisARM time",
    setUploadMoveTime: "Move upload frequency", setUploadStopTime: "Rest upload frequency", setYcqd: "remote start", setYcxh: "remote shut down", setGeofence: "GEO-Fence", setOBDUploadTime: "OBD upload interval",
    setOBDCMD: "OBD upload command", setOBDGg: "OBD upload advertisement", setDeviceUploadTime: "Set upload interval", danwei5s: "unit:second, min: 5seconds", obdTab1: "vehicle state", obdTab2: "History data", obdTab3: "OBD command",
    inputCmdStr: "Input command", inputGgStr: "Input advertisement", secondsMsg1: "Interval cann't less than 5 seconds!", secondsMsg2: "Interval cann't more than 9999 seconds!", startTime: "start time", endTime: "end time",
    pidCount: "Fault quantity", pidStr: "Fault description", noGuzhang: "No faults", deetail: "Detail", obdDataTime: "Data Time", obdFdjfh: "engine load", obdFdjsw: "engine coolant temperature", obdDsryxz: "Short time fuel correction",
    obdCsryxz: "Long time fuel correction", obdJqqgjdyl: "manifold absolute pressure", obdFdjzs: "engine speed", obdClsd: "vehicle speed", obdQgdhtqj: "No. 1 cylinder ignition advance Angle", obdJqwd: "air inlet temperature", obdKqll: "Air flow", obdJqmjdwz: "Throttle absolute position",
    obdMILGzd: "Drive distance after MIL (trouble light) on", obdRylsr: "Fuel Qty input", obdDqyl: "barometric pressure", obdKzmkdy: "control module Voltage", obdSsyh: "instantaneous fuel consumption", obdMl: "horsepower", obdZlc: "total distance", obdBglyh: "fuel consumption per hundred kilometers",
    obdDpdy: "Battery voltage", obdGzdm: "Fault code"
};
var mapPage2 = { searchInput: "Please input name/IMEI No.", divicesInfo: "Target Info", geofence: "Geo-fence", cutOffPetrol: "Cut off Petrol", restorePetrol: "Restore Petrol ", checkLocation: " Check Location ", checkCommand: " Check Command ",
    sendConfirm: "Please confirm the account before sending command", passNull: "Please input password!", passError: "Password Error!", sendMsg1: "Sending message, please wait…", sendSuccess: "Success! Please wait for response...",
    sendMsg2: "Command null", sendMsg3: "Target not exist ", sendMsg4: "Please make device online first.", sendMsg5: "Send command successfully", responseSuccess: " Respond Successfully!", responseNull: " Response Null!",
    checkLocatoin: " Check Location ", checkCommandTitle: "Check Command", cmdType: "Command Type", cmdState: "Command Status", responseText: "Response Message", responseTime: " Response Time ", sendTime: "Send Time",
    dyd: "Cut off Petrol", hfyd: " Restore Petrol ", deviceResponse: "Device Response", sendSuccess2: "Command has been sent!", noSend: "Command has not been sent", deviceDetailList: " Device Detail List ", addGroup: "Add Group",
    defaultGroup: "Default", moveToGroup: "Group Move", delGroupConfirm: "Delete Confirmation?", downloadLocation: "Tracking Report", deviceFortify: "GPS Fortify", deviceDismiss: "GPS Dismiss", accOn: "ACC ON", accOff: "ACC OFF", fortify: "ARM", dismiss: "DimARM", carOpen: "Door Open", carClose: "Door Close", zdlj: "engine connect", zddk: "engine cut",
    obdChecking: "OBD check", uploadTime: "upload interval", setQinqing: "Set family number", setSOS: "Set SOS number", setZhukong: "Set master number", setPassword: "change password", setAutoFortify: "Auto ARM ON",
    setAutoFortifyClose: "Auto ARM OFF", setCutFortifyAuto: "external power cut engine, auto ARM ON", setCutFortifyAutoClose: "external power cut engine, auto ARM Off", setVIBTime: "Set Shake alarm timespan", setVIBLmd: "Set Shake Sensitivity", setSOSType: "SOS alarm",
    setWeiyiWarn: "Set Move Alarm", setOverspeed: "Set OverSpeed alarm", setSMSGPRS: "SMS/GPRS mode", setJianting: "Monitor", setYccq: "Remote Reboot", setHfcc: "factory reset", setLanguage: "Set language",
    setTimezone: "Set time zone", setXiumian: "Set Sleep Model", setJiantingType: "monitor mode", setDingweiType: "positioning mode", setParam: "data check", setAutoFortifyTime: "Auto ARM time", setAutoDismissTime: "Auto DisARM time",
    setUploadMoveTime: "Move upload frequency", setUploadStopTime: "Rest upload frequency", setYcqd: "remote start", setYcxh: "remote shut down", setGeofence: "GEO-Fence", setOBDUploadTime: "OBD upload interval",
    setOBDCMD: "OBD upload command", setOBDGg: "OBD upload advertisement", setDeviceUploadTime: "Set upload interval", danwei5s: "unit:second, min: 5seconds", obdTab1: "vehicle state", obdTab2: "History data", obdTab3: "OBD command",
    inputCmdStr: "Input command", inputGgStr: "Input advertisement", secondsMsg1: "Interval cann't less than 5 seconds!", secondsMsg2: "Interval cann't more than 9999 seconds!", startTime: "start time", endTime: "end time",
    pidCount: "Fault quantity", pidStr: "Fault description", noGuzhang: "No faults", deetail: "Detail", obdDataTime: "Data Time", obdFdjfh: "engine load", obdFdjsw: "engine coolant temperature", obdDsryxz: "Short time fuel correction",
    obdCsryxz: "Long time fuel correction", obdJqqgjdyl: "manifold absolute pressure", obdFdjzs: "engine speed", obdClsd: "vehicle speed", obdQgdhtqj: "No. 1 cylinder ignition advance Angle", obdJqwd: "air inlet temperature", obdKqll: "Air flow", obdJqmjdwz: "Throttle absolute position",
    obdMILGzd: "Drive distance after MIL (trouble light) on", obdRylsr: "Fuel Qty input", obdDqyl: "barometric pressure", obdKzmkdy: "control module Voltage", obdSsyh: "instantaneous fuel consumption", obdMl: "horsepower", obdZlc: "total distance", obdBglyh: "fuel consumption per hundred kilometers",
    obdDpdy: "Battery voltage", obdGzdm: "Fault code"
};


var downloadLocation = { download: "Download", help: "Help", step: "Steps", step1: "1.Select the Targets.", step2: "2.Enter date.", step3: "3.Click 'Download'.",
    msg1: "Note：If you get a 'No Data!' That means there is no valid data for your tracker in this certain period.", msg2: "Tracking download format is Google KML, e.g.:'file name.kml'. ",
    msg3: "Double click KML file to open it after installing Google Earth", msg4: "KML Track File will display the track with dynamical red line on google maps .", msg5: "Tips:Download Google Earth Click", here: "here",
    msg6: "You can download the track file for a certain period through the function 'download Track file'。Type of track file 'KML Track File' AND 'KML Anchor File'"
};

var cusPage = {
    delUserConfirm: "Подтвердите, чтобы удалить", delUserConfirm2: "Этот пользователь?", delUserMsg: "У этого счета есть устройства. Это не может быть удалено!", delUserMsg2: "У этого счета есть субсчет. Это не может быть, удаляют!", msgFailed: "Был не в состоянии удалить!",
    updateUserSuccess: "Успех!", updateUserFailed: "Неудавшийся!", delDeviceConfirm2: "это - цель?", addCus: "Добавьте клиента", manCus: "Управляйте клиентом", delCus: "Удалите клиента", saleTo: "Потребительское движение",
    beDevice: "Членство", updateExpTime: "Обновите истекшее время", deviceChange: "Двинуться", cusInfo: "Информация о клиенте", toCus: "Целевые клиенты", newAddChildrenCus: "Добавьте клиентов", parentCus: "Превосходящий клиент",
    confirmPass: "Подтверждение пароля", allCus: "Все клиенты", addCusTitle: "Добавьте клиента", loginToUser: "Логин терминал", childCus: "Субсчет", changeDevices: "Движение партии", updateExpDevices: "Обновление партии",
    addUserMsg: "Наконечник: Полная информация, такая как номер телефона, связаться.", plsParentCusMsg: "Пожалуйста, выберите превосходящего клиента!", msg1: "Имя, Счет, Пароль не может быть чистым!", passError: "Эти два входа паролей отличаются, пожалуйста, введите их снова!",
    existAccount: "Счет существовался!", dataError: "Пустой указатель данных!", dataNull: "Имя, Счет, Пароль не может быть чистым!", imeisNull: "Номер IMEI не может быть чистым!", saveDevuceMsg1: "Вход вместе:", saveDevuceMsg2: "Группа IMEI нет.",
    saveDevuceMsg3: "Успех!", saveDevuceMsg4: "Успех:", saveDevuceMsg5: "Группа", saveDevuceMsg6: "Подведенный:", saveDevuceMsg7: "Ошибка!", updateExpSuccess: "Успех!",
    updateError: "Отредактируйте неудачу!", changeDeviceSuccess: "Успех!", changeDeviceError: "Целевая неудача движения!", confirmInitPassMsg1: "Вы убеждающийся перезагрузить:", confirmInitPassMsg2: "пароль?:",
    initPassSuccess: "Сброс пароля успешно!", initPassError: "Пароль перезагрузил неудачу!", confirmInitUserPassMsg1: "Подтвердите, чтобы перезагрузить счет?:", page: "Page", records: "Records"
};


//var moneyPage = { moveToAccount: "转入登陆账号", moveToUser: "转入用户名", moveCount: "转入点数", check: "检测", noLoginName: "不存在此登陆名!", inputLoginNameIn: "请输入转入登录名!",
//    inputLoginNameOut: "请检测转入登录名!", noMoveSelf: "不能转入给自己!", moneyLack: "点数不够!", moneyError: "转入异常!", moveSuccess: "转入成功,当前点数:", inputMoneyMsg: "请输入转入点数!",
//    uToUser: "你给用户", money: "点", user: "用户", moveMoneyMsg1: "给你转入点数", give: "给", moneyMsg2: "台设备充值,使用总点数", day: "天", msg1: "1个点数1天", oneYeah: "一年", twoYeah: "二年",
//    lifelong: "终身", msg2: "增加到期天数", msg3: "设备充值天数成功!", msg4: "点数不够,请充值!", msg5: "天数必须大于0!"
//};
var moneyPage = { moveToAccount: "the login account to transfer into", moveToUser: "the username to transfer into", moveCount: "the points to transfer into", check: "detect", noLoginName: "this name does not exist!", inputLoginNameIn: "please test the login name!",
    inputLoginNameOut: "please test the login name!", noMoveSelf: "can not transfer to oneself!", moneyLack: "the point is not enough!", moneyError: "fail to transfer!", moveSuccess: "transfer the points successfully, the current points is:", inputMoneyMsg: "please input the points!",
    uToUser: "for the user", money: "point", user: "user", moveMoneyMsg1: "the points been transferred ", give: "for", moneyMsg2: "charge for the device, the total points used", day: "day", msg1: "1 point for 1 day", oneYeah: "one year", twoYeah: "two years",
    lifelong: "lifelong", msg2: "Extend the maturity time", msg3: "the device is charged successfully!", msg4: "the points is not enough, please charge!!", msg5: "days must be greater then 0!"
};

var yiwen201405 = { fsManger: "4S management", battery: "Battery Level", realCar: "Real-time Vehicle Condition", obdData: "OBD Record", oilInfo: "Fuel Information", carBaoyang: "Vehicle Maintenance", carBaoyangSet: "Mileage Maintenance Set",
    carBaoxian: "Vehicle Insurance", carNianshen: "Vehicle Annual Inspection", userData: "User Data", maintainCar: "Maintain Vehicle", fsSet: "4S store set up", baoyangData: "Maintenance data input", baoyangItem: "Maintenance items",
    baoyangPrice: "Maintenance price", baoyangDate: "Maintenance date", maintenanceofmiles: "Maintenance of miles(km)", repairShop: "Repair shop", notes: "Notes", fuelInput: "Fuel charge data input", restFuel: "Rest fuel capacity (Liter)",
    lastrefuel: "Last refuel mileage (km)", fuelMsg: "Total mileage at this refuel time minus the total mileage last refuel time", automatic: "Automatic calculation", fuelDate: "Refuel time", oilLabel: "Oil label",
    refuelLiter: "Refuel(Liter)", unitPrice: "Unit price/liter ", refuelAddress: "Refuel address", fsInfoSet: "4S store information set up", fsName: "4S store name", fsZuchePhone: "4S car rental service call", fsSalePhone: "4S second-hand car sales call",
    fsSOSPhone: "4S rescue call", fsDaijiaPhone: "4S designated driver call", fsBaoxianPhone: "4S risk call", fsUsername: "4S store contact person", fsPhone: "Contact phone number", fsCarSalePhone: "Sales line", fsServicePhone: "Server line",
    location: "Location", about: "About", carByInterval: "Vehicle maintenance interval tip Settings", byIntervalMileage: "Maintenance interval mileage", byLastMileage: "Mileage when platform remind last time", byMsg2: "Device on “no-enabled” status on platform, total mileage setting invalid",
    byMsg3: "Tips: old car need to transfer on platform, or does not maintain with stipulated mileage, or with wrong data, can correct it Manually by “mileage when platform remind last time ”and “Total  mileage”", byMsg4: "Maintenance reminder interval must over 500km!", dayDistance: "Day mileage",
    dayOil: "Day mileage", baoxianCompany: "Insurance company", baoxianNo: "Car insurance policy number", baoxianStart: "Policy effective date", baoxianEnd: "Policy due date", baoxianUsername: "Insurance adjuster's name", baoxianUserPhone: "Insurance adjuster's tel number",
    baoxianDetail: "Policy details", baoxianDesc: "Policy other instruction", displayType: "Display method", detailList: "Detailed list", dailyCount: "Daily count", monthlyCount: "Monthly count", yearlyCount: "Yearly count",
    nianshenDate: "Annual inspection time", nextNianshenDate: "Next annual inspection time", addRefuel: "Add new refuel record", baoyangList: "List of vehicle need Maintenance", byMsg6: "Show Vehicle to next maintenance within 500 kilometers", msg1: "Please fill item mark “*”",
    baoyangMsg7: "Are you sure to delete this maintenance information?", baoyangAllPrice: "Maintenance total value", lastRest: "Last rest (L)", userL: "The use of(L)", moveDistance: "Mileage ", oilMsg1: "Average fuel consumption/100KM (liters)",
    kilometerPrice: "Cost per kilometer", oilMsg2: "Input date", baoyangInterval: "Maintenance interval Mileage (KM)", lastBaoyang: "Last remind Mileage (km)", nextBaoyang: "Next Maintenance Mileage (km)", nowDistance: "Get now distance", addBaoyang: "Add new maintenance record", luruDate: "Input date",
    price: "Price"
};

var yiwen201407 = { singleMileage: "single mileage", fuel_consumption: "one way fuel consumption", engineSpeed: "one way engine speed", max_speed: "one way max speed", hard_acceleration_times: "hard acceleration times",
    engine_running_time: "engine running time", distance_after_fault_lights: "driving distance after fault lights", fuel_level: "fuel level", single_idle_fuel_consumption2: "single idle fuel consumption",
    average_fuel_consumption: "average fuel consumption", fuel_consumption: "total fuel consumption", mileage_after_install: "mileage after install", hot_start_time: "hot start time", the_current_speed: "hunderd fuel under the current speed",
    singel_driving_time: "singel driving time", singel_idle_time: "singel idle time", resolved_failure_code: "Uncertain fault code", Unresolved_failure_code: "identified failure code", second: "seconds", times: "times", examRecord: "Exam record"
};
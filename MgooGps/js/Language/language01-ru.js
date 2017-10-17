function writePage(msg) {
    //俄语
    document.write(msg);
}

var allPage = { deviceNo: "Число IMEI", search: "Поиск", tracking: "Прослеживание", playback: "Воспроизведение", monitor: "Монитор", home: "Главная страница", customer: "Бизнес", message: "Сообщение", exit: "Выход",
    selSizeStr: "Проверить", sizeStr: "отчеты", createTime: "Задайте время", operation: "Операция", no: "Нет.", cellName: "Контакты", phone: "Телефон/Моб", device: "Цель", purchase: "Покупка", stock: "Запас",
    mobileWebsite: "Доступ по мобильному телефону", deviceName: "Целевое имя", simNo: "Сим-карта НЕТ.", expireTime: "Из-за", remark: "Замечание", cancel: "Отменить", deviceInfo: "Информация об устройстве", imeiNo: "Число IMEI",
    dataError: "Ошибка данных!", softFailed: "Ошибка!", password: "Пароль", confirm: "Сохранить", num: "НЕТ.", noData: "Никакие данные!", acc0: "Выкл", acc1: "Вкл",
    acc2: "Несвязанный", searchNull: "IMEI/Имя/Cat NO./Имя/Аккаунт требуется!", initPass: "Сброс", updateTime: "Обновить время", loginAccount: "Счет логина", passLengthMsg: "Пароль должен быть меньше чем 20 цифр", type: "Напечатать", userType1: "Конечный пользователь",
    userType2: "Дистрибьютор", address: "Адрес", add: "Новый", information: "Информация", deviceType: "Напечатать", overspeed: "Превышение скорости", kmHour: "Км/ч", devicePhone: "Сим-карта НЕТ.", modelName: "Напечатать",
    createTime: "Создайте время", activeTime: "Время активации", hireExpireTime: "Время с истекшим сроком", hireExpireTime2: "Из-за", edit: "Править", more: "Больше", deletes: "Удалить", cusName: "Имя клиента", userInfo: "Информация о клиенте", plsSelUser: "Пожалуйста, выберите пользователя!",
    km: "Километр", plsSel: "Выбрать", all: "Все", online: "Онлайн", offline: "Офлайн", carNum: "Номерной знак нет.", speedLimit: "Ограничение скорости", lat: "Широта", lng: "Долгота", speed: "Скорость", drection: "Руководство",
    allDistance: "Полный пробег", state: "Статус", positionTime: "Время положения", status1: "Вышедший", moving: "Перемещение", stopCar: "Остановить", accStr: "Статус ACC", edit2: "Править", deliveryTime: "Время активации",
    cellPhone: "Телефон/Моб", save: "Сохранить", clear: "Ясный", type2: "Роли", view: "View", deviceHireDay: "代充点数"
};

//login.aspx
var loginPage = {
    title: "Станция прослеживания GPS", accountTab: " номер счета/телефон ", imeiTab: "IMEI нет.", account: "Счет логина", password: "Пароль", loginSubmit: "Искать", languageMsg: "Мы предложили в", loginMsg: "Пожалуйста, введите учетную запись.",
    loginMsg2: "Пожалуйста, введите пароль.", loginMsg3: "Вход не может быть осуществлен!", wytx: "I want try", androidBaidu: "android(Baidu map) Download", androidGoogle: "android(Google map) Download", iphone: "iphone jailbreak Download",
    welcome: "Добро пожаловать, пожалуйста, войдите!", loginErrorMsg: "Счета или пароль Ошибка.", loginFailure: "Войти не удалось.", ForgotPassword: "Забыли пароль？", RememberPassword: "Запомнить имя пользователя и пароль.", Register: "Регистр"
};


//Dealer.aspx
var dealerPage = { warnTitle: "Напоминание", warnSound: "Сигнальный звук на", username: "Имя клиента / Счет", hello: "Здравствуйте", changePassword: "Пароль изменения", searchDevice: "Цель", searchUser: "Клиент",
    searchDevice2: "Целевая проверка", searchUser2: "Клиентская проверка", name: "Имя клиента", belongTo: "Членство", sim: "Сим-карта НЕТ.", activeTime: "Дата активации", customerName: "Имя клиента", nowSearch: "Поиск …",
    name2: "Имя клиента", name3: "Целевое имя"
};

//Home.aspx
var homePage = { quickSearch: "Поиск", searchTxt: "IMEI Номер/T/Авто Номер.", stage: "Стадия", deviceCount: "Целевой граф", operations: "Больше", novice: "новичок", quickSale: "Быстрая продажа",
    batchSale: "Продажа партии", customer: "Клиент", sale: "Продажа", customerList: "потребительский Список", addDevice: "Дополнительный", add: "Новый", imeiNullMsg: "Пожалуйста, выберите цель!", expireTimeNullMsg: "Пожалуйста, выберите дату с истекшим сроком!",
    saleSuccess: "Успех!", editDeviceType: "Редактор Тип устройства"
};

var warnMessagePage = { alarmType: "Тип сигнализации", alarmTime: "Время сигнализации" };

var alarmIndexPage = { geofenceIn: "Геозона в", geofenceOut: "Геозона", moved: "Автомобиль перемещен", lowBattery: "Батарея низкого уровня заряда", sos: "Тревога SOS", cutPower: "Тревога сокращения", vibration: "Тревога вибрации",
    overSpeed: "Превышение скорости", voice: "Звуковой сигнал", doorSensor: "Дверной датчик сигнализации", offline: "Offline"
};

//map.aspx
var mapPage = { searchInput: 'Пожалуйста, вход name/IMEI Нет.', divicesInfo: "Целевая информация", geofence: "Геозона", cutOffPetrol: "Отключите бензин", restorePetrol: "Восстановите бензин", checkLocation: "Проверьте местоположение", checkCommand: "Проверьте команду",
    sendConfirm: "Пожалуйста, подтвердите счет прежде, чем послать команду", passNull: "Пожалуйста, входной пароль!", passError: "Ошибка пароля!", sendMsg1: "Посылая сообщение, пожалуйста, ждите …", sendSuccess: "Успех! Пожалуйста, ждите ответа …",
    sendMsg2: "Пустой указатель команды", sendMsg3: "Цель не существует", sendMsg4: "Цель разъединила сервер.", sendMsg5: "Пошлите команду успешно!", responseSuccess: "Сообщение ответа", responseNull: "Пустой указатель ответа!",
    checkLocatoin: "Проверьте местоположение", checkCommandTitle: "Проверьте команду", cmdType: "Тип команды", cmdState: "Статус команды", responseText: "Сообщение ответа", responseTime: "Время отклика", sendTime: "Пошлите время",
    dyd: "Отключите бензин", hfyd: "Восстановите бензин", deviceResponse: "Ответ устройства", sendSuccess2: "Команду послали!", noSend: "Команду не послали", deviceDetailList: "Список детали устройства", addGroup: "Добавьте группу", defaultGroup: "Неверная группа",
    moveToGroup: "Движение группы", delGroupConfirm: "Удалите подтверждение?",
    changeLanguage: "Установить устройство языка", remoteFortification: "Удаленная Постановка / снятие", remoteCutOil: "Удаленная от нефти / выключения", remoteRestorationOil: "Удаленная добычи нефти / электроэнергии", familyNumber: "Установить число семей", speedLimit: "Установить ограничения скорости",
    uploadTimeInterval: "Установите интервал загрузки", downloadLocation: "Tracking Report", deviceFortify: "GPS Fortify", deviceDismiss: "GPS Dismiss", accOn: "ACC ON", accOff: "ACC OFF", fortify: "ARM", dismiss: "DimARM", carOpen: "Door Open", carClose: "Door Close", zdlj: "engine connect", zddk: "engine cut",
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

var courseName = { dueNorth: "Север", northeast: "Северо-восток", dueEast: "Восток", southeast: "Юго-восток", dueSouth: "Юг", southwest: "Юго-запад", dueWest: "Запад", northwest: "Северо-запад" };

//CustomersList.aspx
var cusPage = { delUserConfirm: "Подтвердите, чтобы удалить", delUserConfirm2: "Этот пользователь?", delUserMsg: "У этого счета есть устройства. Это не может быть удалено!", delUserMsg2: "У этого счета есть субсчет. Это не может быть, удаляют!", msgFailed: "Был не в состоянии удалить!",
    updateUserSuccess: "Успех!", updateUserFailed: "Неудавшийся!", delDeviceConfirm2: "это - цель?", addCus: "Добавьте клиента", manCus: "Управляйте клиентом", delCus: "Удалите клиента", saleTo: "Потребительское движение",
    beDevice: "Членство", updateExpTime: "Обновите истекшее время", deviceChange: "Двинуться", cusInfo: "Информация о клиенте", toCus: "Целевые клиенты", newAddChildrenCus: "Добавьте клиентов", parentCus: "Превосходящий клиент",
    confirmPass: "Подтверждение пароля", allCus: "Все клиенты", addCusTitle: "Добавьте клиента", loginToUser: "Логин терминал", childCus: "Субсчет", changeDevices: "Движение партии", updateExpDevices: "Обновление партии",
    addUserMsg: "Наконечник: Полная информация, такая как номер телефона, связаться.", plsParentCusMsg: "Пожалуйста, выберите превосходящего клиента!", msg1: "Имя, Счет, Пароль не может быть чистым!", passError: "Эти два входа паролей отличаются, пожалуйста, введите их снова!",
    existAccount: "Счет существовался!", dataError: "Пустой указатель данных!", dataNull: "Имя, Счет, Пароль не может быть чистым!", imeisNull: "Номер IMEI не может быть чистым!", saveDevuceMsg1: "Вход вместе:", saveDevuceMsg2: "Группа IMEI нет.",
    saveDevuceMsg3: "Успех!", saveDevuceMsg4: "Успех:", saveDevuceMsg5: "Группа", saveDevuceMsg6: "Подведенный:", saveDevuceMsg7: "Ошибка!", updateExpSuccess: "Успех!",
    updateError: "Отредактируйте неудачу!", changeDeviceSuccess: "Успех!", changeDeviceError: "Целевая неудача движения!", confirmInitPassMsg1: "Вы убеждающийся перезагрузить:", confirmInitPassMsg2: "пароль?:",
    initPassSuccess: "Сброс пароля успешно!", initPassError: "Пароль перезагрузил неудачу!", confirmInitUserPassMsg1: "Подтвердите, чтобы перезагрузить счет?:", page: "Page", records: "Records"
};

var productUpdatePage = { oilCoefficient: "Содействующие/100 Километры Потребления топлива", updateIcon: "Выберите символ", carNumMsg1: "Автомобиль НЕТ. слишком длинное!", sccuess: "Успешно обновилось!", faild: "Неудавшееся обновление!", isExistMsg: "Автомобильный номер существовал, пожалуйста попробуйте другого!", filterLBS: "filter LBS", photo: "Photo" };



var moneyPage = { day: "day", msg1: "1 point for 1 day", oneYeah: "one year", twoYeah: "two years", lifelong: "lifelong", msg2: "Extend the maturity time", msg3: "the device is charged successfully!", msg4: "the points is not enough, please charge!!", msg5: "days must be greater then 0!", msg6: "can not transfer to oneself!", pointGive: "points for sale", pointConsumptionLog: "points consumer records", loginNameNotExist: "this name does not exist", intoLoginID: "the login account to transfer into", intoUsername: "the username to transfer into", intoPoint: "the points to transfer into", detect: "detect", pleaseDetectLoginName: "please test the login name", pleaseInputIntoPoint: "please input the points" };

//var moneyPage = { day: "天", msg1: "1个点数1天", oneYeah: "一年", twoYeah: "二年", lifelong: "终身", msg2: "增加到期天数", msg3: "设备充值天数成功!", msg4: "点数不够,请充值!", msg5: "天数必须大于0!" };

var yiwen201405 = { battery: "Battery Level" };
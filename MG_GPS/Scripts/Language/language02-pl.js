function writePage(msg) {
    //波兰语
    document.write(msg);
}

var allPage = {
    tab1: "Przemieszczenia", tab2: "Alarmy", tab3: "Statystyki urzadzen", tab4: "Szczegoly ostrzezen", startTime: "Od", endTime: "Do", search: "Szukaj", num: "Numer", deviceName: "Nazwa",
    time: "Czas", distance: "Dystans", overspeed: "Przekroczenie predkosci", noData: "Brak danych", lat: "Szerokosc geogr.", lng: "Dlugosc geogr.", speed: "Predkosc", speedKM: "Km/h", day: "Dzien", hour: "Godzina",
    minute: "Minuty", pleSel: "Wybierz", date: "Data", plsDeviceMsg: "Wybierz urzadzenie", moreDevice: "więcej", msg: "Wiadomosc", myAccount: "Moje konto", changePass: "Zmien haslo",
    tracking: "Akt. Pozycja", playback: "Odtwarzanie", monitor: "Monitorowanie", home: "Str. glowna", customer: "Klient", report: "Statystyki", more: "więcej", no: "Numer", name: "Nazwa", carNum: "Nr rejestracyjny",
    imeiNo: "IMEI", activeTime: "Czas aktywacji", hireExpireTime: "Oplacone do", operation: "Operacje", edit: "Edycja", divicesInfo: "Informacje o urzadzeniu", cellName: "Osoba kontaktowa",
    phone: "Telefon", timezone: "Strefa czasowa", save: "Zapisz", confirm: "Potwierdz", updateUserSuccess: "OK", updateUserFailed: "Blad", modelName: "Typ", state: "Status",
    drection: "Kierunek", baidu: "Wyjdz", google: "Google", day: "Dzien", hour: "Godzina", minute: "Minuta", stopTime: "Czas postoju", desc: "Uwagi", cancel: "Anuluj", del: "Usun", delSuccess: "OK",
    delFaild: "Blad", accStr: "ACC", acc0: "Wylaczony", acc1: "Wlaczony", positionType: "Pozycja wg", manDevice: "Zarzadzanie urzadzeniem", type: "Typ", acc2: "Rozlaczone", resolve: "Rozwiazano", startStopTime: "Od",
    endStopTime: "Do", status1: "Wylogowany", moving: "W ruchu", stopCar: "Stop", arrears: "Zalegly", primaryEmail: "Email", positionTime: "Czas pozycjonowania", clear: "Wyczysc", targetName: "Nazwa",
    toExcel: "Export Excel", distance2: "Dystans", km: "km", m: "M", event: "Event recording", inTime: "Enter time", outTime: "Departure time", moneyCount: "点数", deviceHireDay: "代充点数", belongTo: "所属用户",

    deviceNo: "IMEI", message: "Wiadomosc", exit: "Wyjscie",
    selSizeStr: "Wybierz", sizeStr: "Zapisy", createTime: "Data utworzenia", device: "Urzadzenie", purchase: "Urz. na stanie", stock: "Dostepne",
    mobileWebsite: "Dostęp mobilny", simNo: "Nr karty SIM", expireTime: "Oplacone do", remark: "Uwagi", deviceInfo: "Informacje o urz.",
    dataError: "Blad!", softFailed: "Blad!", password: "Haslo",
    searchNull: "Wpisz dane urz. lub klienta", initPass: "Resetuj haslo", updateTime: "Czas aktualizacji", loginAccount: "Login", passLengthMsg: "Haslo może mieć max. 20 znakow", userType1: "Uzytkownik",
    userType2: "Dealer", address: "Adres", add: "Dodaj", information: "Informacje", deviceType: "Typ urzadzenia", kmHour: "Km/h", devicePhone: "Nr karty SIM",
    deletes: "Usun", cusName: "Nazwa klienta", userInfo: "Info o kliencie", plsSelUser: "Wybierz uzytkownika",
    plsSel: "Wybierz", all: "Wszystko", online: "Online", offline: "Offline", speedLimit: "Limit predkosci",
    allDistance: "Dystans calkowity", edit2: "Edytuj", deliveryTime: "Czas aktywacji",
    cellPhone: "Telefon", type2: "Rola", view: "View", StatisticalAnalysis: "analiza statystyczna"
};


//Dealer.aspx
var dealerPage = {
    warnTitle: "Przypomnienie", warnSound: "Wlacz dźwięk alarmu", username: "Nazwa konta", hello: "Witaj", changePassword: "Zmien haslo", searchDevice: "Szukaj urz.", searchUser: "Szukaj klienta",
    searchDevice2: "Znaleziono", searchUser2: "Znaleziono", name: "Klient", belongTo: "Sprzedawca", sim: "Nr karty SIM", activeTime: "Data aktywacji", customerName: "Klient", nowSearch: "Szukanie...",
    name2: "Klient", name3: "Urzadzenie"
};

//Home.aspx
var homePage = {
    quickSearch: "Szukaj", searchTxt: "IMEI", stage: "Status", deviceCount: "Liczba urzadzen", operations: "więcej", novice: "Nowosci", quickSale: "Szybka sprzedaz",
    batchSale: "Sprzedaz hurtowa", customer: "Klient", sale: "Sprzedaj", customerList: "Lista klientow", addDevice: "Urzadzenia", add: "Dodaj kolejne", imeiNullMsg: "Wybierz klienta", expireTimeNullMsg: "Wpisz date waznosci",
    saleSuccess: "OK"
};

var productUpdatePage = { oilCoefficient: "Zuzycie paliwa/100km", updateIcon: "Ikona", carNumMsg1: "Wprowadzona wartosc jest za dluga", sccuess: "OK", faild: "Blad", isExistMsg: "Numer już istnieje", filterLBS: "filter LBS", photo: "Photo" };

var courseName = { dueNorth: "Polnoc", northeast: "Polnocny wschod", dueEast: "Wschod", southeast: "Poludniowy wschod", dueSouth: "Poludnie", southwest: "Poludniowy zachod", dueWest: "Zachod", northwest: "Polnocny zachod" };

var reportPage = { title: "Szczegoly przemieszczen", warnCount: "Alarm", stopCount: "Zatrzymanie" };

var alarmSumPage = { title: "Szczegoly alarmow", lowCount: "Slaba bateria", cutPowerCount: "Odciecie", vibCount: "Alarm wibracyjny", sosCount: "Alarm SOS" }

var overSpeedPage = { continueTime: "Czas kontynuacji", speedlimit: "Ograniczenie predkosci", distancePage: "Dystans", overspeedDetail: "Przekroczenia predkosci", stopDetail: "Szczegoly zatrzymania" };

var alarmIndexPage = { geofenceIn: "Wjazd do strefy", geofenceOut: "Wyjazd ze strefy", moved: "Przemieszczenie", lowBattery: "Slaba bateria", sos: "Alarm SOS", cutPower: "Alarm odciecia", vibration: "Alarm wibracyjny",
    overSpeed: "Przekroczenie predkosci", offline: "Offline"
};

var runindexPage = { statistics: "Zapytanie", statistics2: "Dzienne szczegoly", oilCoefficient: "Zuzycie paliwa/100km", L: "l", oil: "Zuzycie paliwa" };

var alarmDetailPage = { alarmTime: "Czas alarmu", alarmType: "Typ alarmu", alarmCount: "Statystyki alarmow", alarmDetail: "Szczegoly alarmu" };

var userPage = { warnTitle: "Przeglad alarmow", warnSound: "Wlacz dźwięk alarmu", day7Exp: "Wazne 7 dni", day60Exp: "Wazne 60 dni", alreadyExp: "Zalegle",
    username: "Nazwa", hello: "Witaj", searchDevice: "Szukaj urzadzenia", searchUser: "Szukaj klienta", exit: "Wyjscie", message: "Wiadomosc", allDeivce: "Wszystkie urzadzenia", moneyMove: "点数转让", moneyHistory: "点数消费记录"
};

var userInfoPage = { myAccount: "Moje konto", changePassword: "Zmien haslo", userMsg: "Uzupelnij telefon i osobe kontaktowa", customerName: "Nazwa klienta",
    account: "Login", oldPass: "Stare haslo", newPass: "Nowe haslo", confirmPass: "Potwierdzenie hasla", passLengthMsg: "Haslo musi być krotsze niż 20 znakow", passNull: "Wpisz haslo",
    passError: "Hasla roznia się", changePassSuccess: "Reset hasla udany", changePassError: "Reset hasla nieudany!", oldPassError: "Stare haslo bledne", warnSendMsg: "Wybierz rodzaj informacji o alarmach",
    sendEmail: "Email", service: "usługodawcy"
};

var warnMessagePage = { warnMsg: "Wiadomosc alarmowa", handle1: "Nie sprzedane", handle2: "Sprzedane", alarmType: "Typ alarmu", alarmTime: "Czas alarmu" };

var trackingPage = { secondMsg: "Odswiezenie pozycji za sekund!" };

var playbackPage = { from: "Od", to: "Do", play: "Odtworz", pause: "Pazuza", next: "Kontynuuj", fast: "Szybko", slow: "Wolno", timeMsg: "Czas zakonczenia musi być pozniejszy niż czas startu", nowLoading: "Ladowanie danych",
    playOver: "Zakonczono", searchNull: "Brak danych", showLBS: "Show LBS"
};

var geofencesPage = { geofence: "Strefa zasiegu", addGeofence: "Dodaj", geoNameNull: "Nadaj nazwe", radius: "Promien w m", delGeoConfirm: "Potwierdz usuniecie", delGeoConfirm2: "Czy to ta strefa?",
    addSuccess: "pomyślnie dodana!", addFaild: "Dodaj Failed!"
};

var iframeMapPage = { baiduMap: "Baidu Map", googleMap: "Google Map", deviceName: "Nazwa urzadzenia" };

var userUpdatePage = { account: "Login" };

//map.aspx
var mapPage = { searchInput: 'Podaj IMEI lub nazwe', divicesInfo: "Informacje", geofence: "Strefa zasiegu", cutOffPetrol: "Odetnij paliwo", restorePetrol: "Przywroc paliwo", checkLocation: "Sprawdz lokalizacje", checkCommand: "Polecenie",
    sendConfirm: "Potwierdz przed sprawdzeniem urzadzenia", passNull: "Podaj haslo", passError: "Bledne haslo", sendMsg1: "Wysylam wiadomosc...", sendSuccess: "OK. Czekam na odpowiedz",
    sendMsg2: "Blad", sendMsg3: "Urzadzenie niedostepne", sendMsg4: "Najpierw ustaw urzadzenie online.", sendMsg5: "Wyslano", responseSuccess: "OK", responseNull: "Blad",
    checkLocatoin: "Sprawdz lokalizacje", checkCommandTitle: "Polecenia", cmdType: "Typ polecenia", cmdState: "Status", responseText: "Wiadomosc zwrotna", responseTime: "Czas odpowiedzi", sendTime: "Czas wyslania",
    dyd: "Odetnij paliwo", hfyd: "Przywroc paliwo", deviceResponse: "Odpowiedz urzadzenia", sendSuccess2: "Polecenie zostalo wyslane", noSend: "Polecenie nie zostalo wyslane", deviceDetailList: "Szczegoly urzadzen", addGroup: "Dodaj grupe", defaultGroup: "Grupa domyslna",
    moveToGroup: "Przenies do innej grupy", delGroupConfirm: "Czy na pewno usunac?", downloadLocation: "Tracking Report", deviceFortify: "GPS Fortify", deviceDismiss: "GPS Dismiss", accOn: "ACC ON", accOff: "ACC OFF", fortify: "ARM", dismiss: "DimARM", carOpen: "Door Open", carClose: "Door Close", zdlj: "engine connect",
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
    delUserConfirm: "Confirm to delete", delUserConfirm2: "Is this user?", delUserMsg: "This account has devices. It cannot be deleted!", delUserMsg2: "This account has sub-account. It cannot be delete!", msgFailed: "Failed to delete!",
    updateUserSuccess: "Success!", updateUserFailed: "Failed!", delDeviceConfirm2: "is this target?", addCus: "Add Customer", manCus: "Manage Customer", delCus: "Delete Customer", saleTo: "Customer Move",
    beDevice: "Membership", updateExpTime: "Update Expired Time", deviceChange: "Move", cusInfo: "Customer information", toCus: "Target Customers", newAddChildrenCus: "Add Customers", parentCus: "Superior Customer",
    confirmPass: "Password Confirmation", allCus: "All Customers", addCusTitle: "Add Customer", loginToUser: "Monitor", childCus: "Sub-account", changeDevices: "Batch Move", updateExpDevices: "Batch update",
    addUserMsg: "Tip: Complete information such as telephone number, contact.", plsParentCusMsg: "Please select the superior customer!", msg1: "Pola nie mogą być puste", passError: "Wpisane hasla się roznia!",
    existAccount: "Konto o takiej nazwie już istnieje!", dataError: "Blad!", dataNull: "Pola nie mogą być puste!", imeisNull: "Wpisz IMEI", saveDevuceMsg1: "Wprowadz zbiorczo:", saveDevuceMsg2: "IMEI",
    saveDevuceMsg3: "OK", saveDevuceMsg4: "OK", saveDevuceMsg5: "Grupa", saveDevuceMsg6: "Blad!", saveDevuceMsg7: "Blad!", updateExpSuccess: "OK",
    updateError: "Blad!", changeDeviceSuccess: "OK", changeDeviceError: "Nieudane", confirmInitPassMsg1: "Czy chcesz skasowac haslo?", confirmInitPassMsg2: "Czy chcesz usunac?",
    initPassSuccess: "OK", initPassError: "Blad!", confirmInitUserPassMsg1: "Czy chcesz skasowac konto?", page: "Page", records: "Records"
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
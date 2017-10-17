function writePage(msg) {
    //波兰语
    document.write(msg);
}

var allPage = { deviceNo: "IMEI", search: "Szukaj", tracking: "Akt. Pozycja", playback: "Odtwarzanie", monitor: "Monitorowanie", home: "Home", customer: "Biznes", message: "Wiadomosc", exit: "Wyjscie",
    selSizeStr: "Wybierz", sizeStr: "Zapisy", createTime: "Data utworzenia", operation: "Operacje", no: "Numer", cellName: "Osoba kontaktowa", phone: "Telefon", device: "Urzadzenie", purchase: "Urz. na stanie", stock: "Dostepne",
    mobileWebsite: "Dostęp mobilny", deviceName: "Urzadzenie", simNo: "Nr karty SIM", expireTime: "Oplacone do", remark: "Uwagi", cancel: "Anuluj", deviceInfo: "Informacje o urz.", imeiNo: "IMEI",
    dataError: "Blad!", softFailed: "Blad!", password: "Haslo", confirm: "Zapisz", num: "Numer", noData: "Blad!", acc0: "Wylaczony", acc1: "Wlaczony",
    acc2: "Rozlaczony", searchNull: "Wpisz dane urz. lub klienta", initPass: "Resetuj haslo", updateTime: "Czas aktualizacji", loginAccount: "Login", passLengthMsg: "Haslo może mieć max. 20 znakow", type: "Typ", userType1: "Uzytkownik",
    userType2: "Dealer", address: "Adres", add: "Dodaj", information: "Informacje", deviceType: "Typ urzadzenia", overspeed: "Przekroczenie predkosci", kmHour: "Km/h", devicePhone: "Nr karty SIM", modelName: "Typ",
    createTime: "Data utworzenia", activeTime: "Data aktywacji", hireExpireTime: "Oplacone do", edit: "Edytuj", more: "więcej", deletes: "Usun", cusName: "Nazwa klienta", userInfo: "Info o kliencie", plsSelUser: "Wybierz uzytkownika",
    km: "Km", plsSel: "Wybierz", all: "Wszystko", online: "Online", offline: "Offline", arrears: "Zalegly", carNum: "Nr rej. Pojazdu", speedLimit: "Limit predkosci", lat: "Szerokosc geogr.", lng: "Dlugosc geogr.", speed: "Predkosc", drection: "Kierunek",
    allDistance: "Dystans calkowity", state: "Status", positionTime: "Czas pozycjonowania", status1: "Nieaktywny", moving: "W ruchu", stopCar: "Stop", accStr: "ACC", edit2: "Edytuj", deliveryTime: "Czas aktywacji",
    cellPhone: "Telefon", save: "Zapisz", clear: "Wyczysc", type2: "Rola", view: "View", deviceHireDay: "代充点数"
};

//login.aspx
var loginPage = {
    title: "Monitorowanie GPS", accountTab: " telefony/konto/IMEI ", imeiTab: "IMEI", account: "Login", password: "Haslo", loginSubmit: "Wejdz", languageMsg: "Dostepne jezyki:", loginMsg: "proszę podać numer konta.",
    loginMsg2: "proszę podać hasło.", loginMsg3: "Pole nie może być puste!", wytx: "I want try", androidBaidu: "android(Baidu map) Download", androidGoogle: "android(Google map) Download", iphone: "iphone jailbreak Download",
    welcome: "Witaj, zaloguj się!", loginErrorMsg: "numer konta i hasło jest źle", loginFailure: "Logowanie nie powiodło się", ForgotPassword: "zapomniał hasła？", RememberPassword: "Zapamiętaj hasło", Register: "Rejestracja"
};


//Dealer.aspx
var dealerPage = { warnTitle: "Przypomnienie", warnSound: "Wlacz dźwięk alarmu", username: "Nazwa konta", hello: "Witaj", changePassword: "Zmien haslo", searchDevice: "Szukaj urz.", searchUser: "Szukaj klienta",
    searchDevice2: "Znaleziono", searchUser2: "Znaleziono", name: "Klient", belongTo: "Sprzedawca", sim: "Nr karty SIM", activeTime: "Data aktywacji", customerName: "Klient", nowSearch: "Szukanie...",
    name2: "Klient", name3: "Urzadzenie"
};

//Home.aspx
var homePage = { quickSearch: "Szukaj", searchTxt: "IMEI", stage: "Status", deviceCount: "Liczba urzadzen", operations: "więcej", novice: "Nowosci", quickSale: "Szybka sprzedaz",
    batchSale: "Sprzedaz hurtowa", customer: "Klient", sale: "Sprzedaj", customerList: "Lista klientow", addDevice: "Urzadzenia", add: "Dodaj kolejne", imeiNullMsg: "Wybierz klienta", expireTimeNullMsg: "Wpisz date waznosci",
    saleSuccess: "OK"
};

var warnMessagePage = { alarmType: "Rodzaj alarmu", alarmTime: "Data" };

var alarmIndexPage = { geofenceIn: "Wjazd do strefy", geofenceOut: "Wyjazd ze strefy", moved: "Przemieszczenie", lowBattery: "Slaba bateria", sos: "Alarm SOS", cutPower: "Alarm odciecia", vibration: "Alarm wibracyjny",
    overSpeed: "Przekroczenie predkosci", offline: "Offline"
};

//map.aspx
var mapPage = { searchInput: 'Podaj IMEI lub nazwe', divicesInfo: "Informacje", geofence: "Strefa zasiegu", cutOffPetrol: "Odetnij paliwo", restorePetrol: "Przywroc paliwo", checkLocation: "Sprawdz lokalizacje", checkCommand: "Polecenie",
    sendConfirm: "Potwierdz przed sprawdzeniem urzadzenia", passNull: "Podaj haslo", passError: "Bledne haslo", sendMsg1: "Wysylam wiadomosc...", sendSuccess: "OK. Czekam na odpowiedz",
    sendMsg2: "Blad", sendMsg3: "Urzadzenie niedostepne", sendMsg4: "Najpierw ustaw urzadzenie online.", sendMsg5: "Wyslano", responseSuccess: "OK", responseNull: "Blad",
    checkLocatoin: "Sprawdz lokalizacje", checkCommandTitle: "Polecenia", cmdType: "Typ polecenia", cmdState: "Status", responseText: "Wiadomosc zwrotna", responseTime: "Czas odpowiedzi", sendTime: "Czas wyslania",
    dyd: "Odetnij paliwo", hfyd: "Przywroc paliwo", deviceResponse: "Odpowiedz urzadzenia", sendSuccess2: "Polecenie zostalo wyslane", noSend: "Polecenie nie zostalo wyslane", deviceDetailList: "Szczegoly urzadzen", addGroup: "Dodaj grupe", defaultGroup: "Grupa domyslna",
    moveToGroup: "Przenies do innej grupy", delGroupConfirm: "Czy na pewno usunac?", downloadLocation: "Tracking Report", deviceFortify: "GPS Fortify", deviceDismiss: "GPS Dismiss", accOn: "ACC ON", accOff: "ACC OFF", fortify: "ARM", dismiss: "DimARM", carOpen: "Door Open", carClose: "Door Close", zdlj: "engine connect", zddk: "engine cut",
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

var courseName = { dueNorth: "Polnoc", northeast: "Polnocny wschod", dueEast: "Wschod", southeast: "Poludniowy wschod", dueSouth: "Poludnie", southwest: "Poludniowy zachod", dueWest: "Zachod", northwest: "Polnocny zachod" };

//CustomersList.aspx
var cusPage = { delUserConfirm: "Confirm to delete", delUserConfirm2: "Is this user?", delUserMsg: "This account has devices. It cannot be deleted!", delUserMsg2: "This account has sub-account. It cannot be delete!", msgFailed: "Failed to delete!",
    updateUserSuccess: "Success!", updateUserFailed: "Failed!", delDeviceConfirm2: "is this target?", addCus: "Add Customer", manCus: "Manage Customer", delCus: "Delete Customer", saleTo: "Customer Move",
    beDevice: "Membership", updateExpTime: "Update Expired Time", deviceChange: "Move", cusInfo: "Customer information", toCus: "Target Customers", newAddChildrenCus: "Add Customers", parentCus: "Superior Customer",
    confirmPass: "Password Confirmation", allCus: "All Customers", addCusTitle: "Add Customer", loginToUser: "Monitor", childCus: "Sub-account", changeDevices: "Batch Move", updateExpDevices: "Batch update",
    addUserMsg: "Tip: Complete information such as telephone number, contact.", plsParentCusMsg: "Please select the superior customer!", msg1: "Pola nie mogą być puste", passError: "Wpisane hasla się roznia!",
    existAccount: "Konto o takiej nazwie już istnieje!", dataError: "Blad!", dataNull: "Pola nie mogą być puste!", imeisNull: "Wpisz IMEI", saveDevuceMsg1: "Wprowadz zbiorczo:", saveDevuceMsg2: "IMEI",
    saveDevuceMsg3: "OK", saveDevuceMsg4: "OK", saveDevuceMsg5: "Grupa", saveDevuceMsg6: "Blad!", saveDevuceMsg7: "Blad!", updateExpSuccess: "OK",
    updateError: "Blad!", changeDeviceSuccess: "OK", changeDeviceError: "Nieudane", confirmInitPassMsg1: "Czy chcesz skasowac haslo?", confirmInitPassMsg2: "Czy chcesz usunac?",
    initPassSuccess: "OK", initPassError: "Blad!", confirmInitUserPassMsg1: "Czy chcesz skasowac konto?", page: "Page", records: "Records"
};

var productUpdatePage = { oilCoefficient: "Zuzycie paliwa/100km", updateIcon: "Ikona", carNumMsg1: "Wprowadzona wartosc jest za dluga", sccuess: "OK", faild: "Blad", isExistMsg: "Numer już istnieje", filterLBS: "filter LBS", photo: "Photo" };




var moneyPage = { day: "day", msg1: "1 point for 1 day", oneYeah: "one year", twoYeah: "two years", lifelong: "lifelong", msg2: "Extend the maturity time", msg3: "the device is charged successfully!", msg4: "the points is not enough, please charge!!", msg5: "days must be greater then 0!", msg6: "can not transfer to oneself!", pointGive: "points for sale", pointConsumptionLog: "points consumer records", loginNameNotExist: "this name does not exist", intoLoginID: "the login account to transfer into", intoUsername: "the username to transfer into", intoPoint: "the points to transfer into", detect: "detect", pleaseDetectLoginName: "please test the login name", pleaseInputIntoPoint: "please input the points" };
//var moneyPage = { day: "天", msg1: "1个点数1天", oneYeah: "一年", twoYeah: "二年", lifelong: "终身", msg2: "增加到期天数", msg3: "设备充值天数成功!", msg4: "点数不够,请充值!", msg5: "天数必须大于0!" };

var yiwen201405 = { battery: "Battery Level" };
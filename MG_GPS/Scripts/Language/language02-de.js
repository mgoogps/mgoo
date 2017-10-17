function writePage(msg) {
    //德语
    document.write(msg);
}

var allPage = {
    tab1: "Eine Übersicht führen", tab2: "Übersicht Alarm", tab3: "Gerätestatistik", tab4: "Alarm - Statistiken", startTime: "Von", endTime: "Zu", search: "Suche", num: "Nr.", deviceName: "Gerätebezeichnung",
    time: "Zeit", distance: "Kilometerstand (km)", overspeed: "Drehzahlbegrenzung", noData: "Keine Datei!", lat: "Breitengrad", lng: "Längengrad", speed: "Geschwindigkeit", address: "Adresse", speedKM: "km/h", day: "Tag", hour: "Stunde",
    minute: "Minuten", pleSel: "Bitte wählen", date: "Datum", plsDeviceMsg: "Wählen Sie bitte das Gerät", moreDevice: "Weiter", msg: "Nachricht", myAccount: "Mein Konto", changePass: "Passwort ändern",
    tracking: "Verfolgen", playback: "Wiedergabe", monitor: "Monitor", home: "Start", customer: "Kunden", report: "Statistiken", more: "Weiter", no: "Nr.", name: "Kundenname",
    imeiNo: "IMEI Nr.", activeTime: "Aktivierungszeit", operation: "Betrieb", edit: "Bearbeiten", divicesInfo: "Geräteinformation", cellName: "Kontakte",
    phone: "Handy", timezone: "Zeitzone", save: "Speichern", confirm: "Bestätigen", updateUserSuccess: "Erfolgreich bearbeitet!", updateUserFailed: "Bearbeitung fehlgeschlagen!", modelName: "Modell", state: "Status",
    drection: "Richtung", baidu: "Baidu", google: "Google", day: "Tag", hour: "Stunde", minute: "Minute", stopTime: "Standzeit:", desc: "Bemerkung", cancel: "Stornieren", del: "Löschen", delSuccess: "Erfolgreich gelöscht!",
    delFaild: "Löschen fehlgeschlagen!", accStr: "Zündung:", acc0: "Aus", acc1: "An", positionType: "Positioniermodus", manDevice: "Geräteverwaltung", type: "Modell", acc2: "Nicht verbunden", resolve: "Beheben", startStopTime: "Anfang",
    endStopTime: "Ende", status1: "Abgemeldet", moving: "Fahren", stopCar: "Stopp", primaryEmail: "E-mail", positionTime: "Geparkt:", clear: "Entfernen", targetName: "Gerätename",
    toExcel: "Export Excel", distance2: "Kilometerstand", km: "km", m: "M", event: "Event recording", inTime: "Enter time", outTime: "Departure time", moneyCount: "点数", deviceHireDay: "代充点数", belongTo: "所属用户",

    deviceNo: "IMEI Nr.", search1: "Suche", message: "Nachricht", exit: "Abmelden",
    selSizeStr: "Überprüfung", sizeStr: "Aufzeichnungen", createTime: "Zeit einstellen", device: "Gerät", purchase: "Kauf", stock: "Lager",
    mobileWebsite: "Mit Handy zugang", simNo: "SIM Kartennr.", expireTime: "Fälligkeit", remark: "Bemerkung", deviceInfo: "Geräteinformation",
    dataError: "Dateifehler!", softFailed: "Fehler!", password: "Passwort",
    searchNull: "IMEI / Name / Auto Nr./ Konto ist erforderlicht!", initPass: "Passwort zurücksetzen", updateTime: "Aktualisierung", loginAccount: "Login Name", passLengthMsg: "Maximal 20 Zeichen", userType1: "Benutzer",
    userType2: "Vertrieb", add: "Neu", information: "Information", deviceType: "Modell", kmHour: "Km/h", devicePhone: "SIM Kartennr.",
    hireExpireTime: "Ablaufzeit", deletes: "Löschen", cusName: "Kundenname", userInfo: "Kundeninformation", plsSelUser: "Benutzer auswählen",
    plsSel: "Auswählen", all: "Alle", online: "Online", offline: "Offline", arrears: "Überfällig", carNum: "Autonr.", speedLimit: "Geschwindigkeitsbegrenzung",
    allDistance: "Gesamtstrecke", edit2: "Bearbeiten", deliveryTime: "Herstellungsdatum",
    cellPhone: "Handy", type2: "Funktionen", view: "View", StatisticalAnalysis: "Statistische Analyse"
};

//Dealer.aspx
var dealerPage = {
    warnTitle: "Schlummerfunktion", warnSound: "Alarm an", username: "Kundenname / Konto", hello: "Hallo", changePassword: "Passwort ändern", searchDevice: "Suche Gerät", searchUser: "Kunde",
    searchDevice2: "Search Appliance", searchUser2: "Benutzer - Suche", name: "Kundenname", belongTo: "Mitgliedschaft", sim: "SIM-Karten-Nr.", activeTime: "Aktivierungsdatum", customerName: "Name des Kunden", nowSearch: "Suche ...",
    name2: "Name", name3: "Gerätebezeichnung"
};

//Home.aspx
var homePage = {
    quickSearch: "Schnellsuche", searchTxt: "IMEI Nr./ Geräte / Auto Nr.", stage: "Plattform", deviceCount: "Anzahl der Geräte", operations: "Weiter", novice: "Neukunde", quickSale: "Verkauf",
    batchSale: "Verschieben", customer: "Kunde", sale: "Verkauf", customerList: "Kundenliste", addDevice: "Gerätetreiber hinzufügen", add: "Hinzufügen", imeiNullMsg: "Bitte wählen Sie das Ziel!", expireTimeNullMsg: "Bitte wählen Sie die Ablaufzeit!",
    saleSuccess: "Erfolg!"
};

var courseName = { dueNorth: "Norden", northeast: "Nordosten", dueEast: "Osten", southeast: "Südosten", dueSouth: "Süden", southwest: "Südwesten", dueWest: "Westen", northwest: "Nordwesten" };

var reportPage = { title: "Gesamtstatistik", warnCount: "Alarm", stopCount: "Stopp" };

var alarmSumPage = { title: "Überblick der Alarmstatistik", lowCount: "Alarm, der Akku ist schwach", cutPowerCount: "Alarm, die Stromversorgung ist unterbrochen", vibCount: "Vibrationsalarm", sosCount: "SOS Alarm" }

var overSpeedPage = { continueTime: "Dauer", speedlimit: "Geschwindigkeits - Werte", distancePage: "Meilenzahlstatistiken", overspeedDetail: "Speeding Details eines einzelnen", stopDetail: "Einzelheiten" };

var alarmIndexPage = { geofenceIn: "Virtueller Zaun An", geofenceOut: "Virtueller Zaun Aus", moved: "Verschieben", lowBattery: "Alarm, der Akku ist schwach", sos: "SOS Alarm", cutPower: "Alarm, die Stromversorgung ist unterbrochen", vibration: "Vibrationsalarm",
    overSpeed: "Drehzahlbegrenzung", offline: "Offline"
};

var runindexPage = { statistics: "Statistik von", statistics2: "Tägliche Statistiken", oilCoefficient: "Kraftstoffverbrauch / 100 km", L: "Liter", oil: "Kraftstoffverbrauch" };

var alarmDetailPage = { alarmTime: "Alarmzeit", alarmType: "Alarmtyp", alarmCount: "Alarmstatistik", alarmDetail: "Alarm Detail" };

var userPage = { warnTitle: "Alarm - Übersicht", warnSound: "Alarmton an", day7Exp: "Ablauf 7 Tage", day60Exp: "Ablauf 6 Tage", alreadyExp: "Abgelaufen",
    username: "Nutzername / Konto", hello: "Hallo", searchDevice: "Suche Gerät", searchUser: "Kunde", exit: "Abmelden", message: "Nachricht", allDeivce: "Gesamte Geräte", moneyMove: "点数转让", moneyHistory: "点数消费记录"
};

var userInfoPage = { myAccount: "Mein konto", changePassword: "Passwort ändern", userMsg: " Tip: Complete information such as telephone number, contact.", customerName: " Customer Name ",
    account: "Login Account", oldPass: "Altes Passwort", newPass: "Neues Passwort", confirmPass: "Neues Passwort", passLengthMsg: " Maximal 20 Zeichen", passNull: "Password input cannot be blank!",
    passError: "The two passwords input are different!", changePassSuccess: "Password reset successfully!", changePassError: "Password reset failure!", oldPassError: "Old password error!",
    warnSendMsg: "Additional Alarm Inform Mode", sendEmail: "Email", service: "Meine Daten"
};

var warnMessagePage = { warnMsg: "Problemmeldungen", handle1: "Unbehandelt", handle2: "Behandelt", alarmType: "Art des Alarms", alarmTime: "Alarmzeit" };

var trackingPage = { secondMsg: "Sekunden aktualisiert" };

var playbackPage = { from: "von", to: "auf", play: "Sendung", pause: "Pause", next: "Weiter", fast: "Schnell", slow: "Langsam", timeMsg: "Die Endzeit sollte größer als die Startzeit sein!", nowLoading: "Dateien werden geladen!",
    playOver: "Spiel ist beendet!", searchNull: "Keine Daten!", showLBS: "Anzeige LBS"
};

var geofencesPage = { geofence: "Virtueller Zaun", addGeofence: "Virtueller Zaun hinzufügen", geoNameNull: "Name, des virtuellen Zaunes darf nicht leer sein!", radius: "Radius", delGeoConfirm: "Löschen bestätigen", delGeoConfirm2: "Diesen Virtuellen Zaun?",
    addSuccess: "erfolgreich hinzugefügt!", addFaild: "hinzufügen fehlgeschlagen!"
};

var iframeMapPage = { baiduMap: "Baidu Karte", googleMap: "Google Karten", deviceName: "Gerätename" };

var userUpdatePage = { account: "Konto" };

//map.aspx
var mapPage = { searchInput: 'Namen des Gerätes / IMEI Nr. Eingeben', divicesInfo: "Geräteinformation", geofence: "GeoFence", cutOffPetrol: "Schaltausgang Aktivieren", restorePetrol: "Schaltausgang Deaktivieren", checkLocation: "Positions - Abfrage", checkCommand: "Befehl prüfen",
    sendConfirm: "Vor Befehl absenden, Konto bestätigen", passNull: "Passwort eingeben!", passError: "Passwort falsch!", sendMsg1: "Nachricht wird gesendet, bitte warten Sie", sendSuccess: "Erfogreich! Bitte warten Sie auf Antwort",
    sendMsg2: "Befehl ist ungültig", sendMsg3: "Gerät ist nicht vorhanden", sendMsg4: "Das Gerät und der Server wurde getrennt", sendMsg5: "Befehl erfolgreich gesendet", responseSuccess: "Erfogreiche Antwort!", responseNull: "Keine Antwort!",
    checkLocatoin: "Position prüfen", checkCommandTitle: "Befehl prüfen", cmdType: "Art des Befehls", cmdState: "Befehl Status", responseText: "Antwortnachricht", responseTime: "Antwortzeit", sendTime: "Übertragungszeit",
    dyd: "Kraftstoff aus", hfyd: "Kraftstoff zurückgeben", deviceResponse: "Geräteantwort", sendSuccess2: "Befehl wurde gesendet!", noSend: "Befehl wurde nicht gesendet", deviceDetailList: "Geräte Detail - Liste", addGroup: "Gruppe hinzufügen", defaultGroup: "Standard",
    moveToGroup: "Gruppe verschieben", delGroupConfirm: "Löschen bestätigt?", downloadLocation: "Download Track", deviceFortify: "GPS Fortify", deviceDismiss: "GPS Dismiss", accOn: "ACC ON", accOff: "ACC OFF", fortify: "ARM", dismiss: "DimARM", carOpen: "Door Open", carClose: "Door Close", zdlj: "engine connect",
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

//CustomersList.aspx 
var cusPage = {
    delUserConfirm: "Löschen bestätigen", delUserConfirm2: "Dieser Benutzer?", delUserMsg: "Das Benutzergerät kann nicht gelöscht werden!", delUserMsg2: "Dieses Konto hat Unter - Konto. Es kann nicht gelöscht werden!", msgFailed: "Löschen fehlgeschlagen!",
    updateUserSuccess: "Erfolg!", updateUserFailed: "Fehlschlag!", delDeviceConfirm2: "Gerät bestätigt?", addCus: "Kunde hinzufügen", manCus: "Kunden verwalten", delCus: "Kunde entfernen", saleTo: "Verschieben Nach:",
    beDevice: "Hauptkonto", updateExpTime: "Fälligkeit aktualisieren", deviceChange: "Verschieben", cusInfo: "Kundeninformationen", toCus: "Zielkunden", newAddChildrenCus: "Kunde hinzufügen", parentCus: "Hauptkonto",
    confirmPass: "Passwort bestätigen", allCus: "Alle Kunden", addCusTitle: "Kunde hinzufügen", loginToUser: "Überwachen", childCus: "Unterkonto", changeDevices: "Gesamtübertragung", updateExpDevices: "Gesamtaktualisierung",
    addUserMsg: "Tipp: Informationen, wie Telefonkontakte vervollständigen", plsParentCusMsg: "Bitte wählen Sie den erstklassigen Kundenservice!", msg1: "Name, Konto, Passwort darf nicht leer sein!", passError: "Die Passwörter stimmen nicht überein, neu eingeben!",
    existAccount: "Konto hat bestanden!", dataError: "Keine Daten!", dataNull: "Name, Konto, Passwort darf nicht leer sein!", imeisNull: "IMEI Nr. darf nicht leer sein!", saveDevuceMsg1: "Zusammen eingeben:", saveDevuceMsg2: "IMEI Nr.- Gruppe,",
    saveDevuceMsg3: "Erfolg!", saveDevuceMsg4: "Erfolg:", saveDevuceMsg5: "Gruppe", saveDevuceMsg6: "Fehlschlag:", saveDevuceMsg7: "Fehler!", updateExpSuccess: "Erfolg!",
    updateError: "Fehlschlag bearbeiten!", changeDeviceSuccess: "Erfolg!", changeDeviceError: "Technischer Defekt!", confirmInitPassMsg1: "Zurücksetzung bestätigen:", confirmInitPassMsg2: "Passwort?",
    initPassSuccess: "Passwort erfolgreich zurückgesetzt!", initPassError: "Fehlschlag Passwort zurücksetzen!", confirmInitUserPassMsg1: "Zurücksetzung des Kontos bestätigen?", page: "Page", records: "Records"
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

var productUpdatePage = { oilCoefficient: "Kraftstoff/100 km", updateIcon: "Symbol", carNumMsg1: "Autonr. ist zu lang!", sccuess: "Aktualisierung erfolgreich!", faild: "Aktualisierung fehlgeschlagen!", isExistMsg: "Autonr. gibt es bereits, bitte ändern!", filterLBS: "filter LBS", photo: "Photo" };


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
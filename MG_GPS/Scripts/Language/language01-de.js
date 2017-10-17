function writePage(msg) {
    //德语
    document.write(msg);
}

var allPage = { deviceNo: "IMEI Nr.", search: "Suche", tracking: "Verfolgung", playback: "Wiedergabe", monitor: "Monitor", home: "Start", customer: "Kunden", message: "Nachricht", exit: "Abmelden",
    selSizeStr: "Überprüfung", sizeStr: "Aufzeichnungen", createTime: "Zeit einstellen", operation: "Betrieb", no: "Nr.", cellName: "Kontakte", phone: "Handy", device: "Gerät", purchase: "Kauf", stock: "Lager",
    mobileWebsite: "Mit Handy zugang", deviceName: "Geräte - Namen", simNo: "SIM Kartennr.", expireTime: "Fälligkeit", remark: "Bemerkung", cancel: "Stornieren", deviceInfo: "Geräteinformation", imeiNo: "IMEI Nummer",
    dataError: "Dateifehler!", softFailed: "Fehler!", password: "Passwort", confirm: "Speichern", num: "Nummer", noData: "Keine Dateiabfrage möglich!", acc0: "Aus", acc1: "An",
    acc2: "Nicht verbunden", searchNull: "IMEI / Name / Auto Nr./ Konto ist erforderlicht!", initPass: "Passwort zurücksetzen", updateTime: "Aktualisierung", loginAccount: "Login Name", passLengthMsg: "Maximal 20 Zeichen", type: "Typ", userType1: "Benutzer",
    userType2: "Vertrieb", address: "Adresse", add: "Neu", information: "Information", deviceType: "Modell", overspeed: "Geschwindigkeitsbegrenzung", kmHour: "Km/h", devicePhone: "SIM Kartennr.", modelName: "Modell",
    createTime: "Erstellungszeit", activeTime: "Aktivierungszeit", hireExpireTime: "Ablaufzeit", edit: "Bearbeiten", more: "Weiter", deletes: "Löschen", cusName: "Kundenname", userInfo: "Kundeninformation", plsSelUser: "Benutzer auswählen",
    km: "Kilometer", plsSel: "Auswählen", all: "Alle", online: "Online", offline: "Offline", arrears: "Überfällig", carNum: "Autonr.", speedLimit: "Geschwindigkeitsbegrenzung", lat: "Breitengrad", lng: "Längengrad", speed: "Geschwindigkeit", drection: "Richtung",
    allDistance: "Gesamtstrecke", state: "Status", positionTime: "Stellzeit", status1: "Abgemeldet", moving: "Fahren", stopCar: "Stopp", accStr: "ACC - Status", edit2: "Bearbeiten", deliveryTime: "Herstellungsdatum",
    cellPhone: "Handy", save: "Speichern", clear: "Entfernen", type2: "Funktionen", view: "View", deviceHireDay: "代充点数"
};

//login.aspx
var loginPage = {
    title: "GPS Beobachtungsstation", accountTab: " Konto/Telefon/IMEI ", imeiTab: "IMEI Nr.", account: "Konto", password: "Passwort", loginSubmit: "Anmelden", languageMsg: "Wir bieten", loginMsg: "Bitte geben Sie Konto.",
    loginMsg2: "Bitte geben Sie ihr Passwort ein.", loginMsg3: "Eingabe darf nicht leer sein!", wytx: "I want try", androidBaidu: "android(Baidu map) Download", androidGoogle: "android(Google map) Download", iphone: "iphone jailbreak Download",
    welcome: "Herzlich willkommen, bitte anmelden!", loginErrorMsg: "Account oder Passwort Fehler", loginFailure: "Fehler bei der Anmeldung", ForgotPassword: "Passwort vergessen?", RememberPassword: "Passwort merken", Register: "Registrieren"
};


//Dealer.aspx
var dealerPage = { warnTitle: "Schlummerfunktion", warnSound: "Alarm an", username: "Kundenname / Konto", hello: "Hallo", changePassword: "Passwort ändern", searchDevice: "Suche Gerät", searchUser: "Kunde",
    searchDevice2: "Search Appliance", searchUser2: "Benutzer - Suche", name: "Kundenname", belongTo: "Mitgliedschaft", sim: "SIM-Karten-Nr.", activeTime: "Aktivierungsdatum", customerName: "Name des Kunden", nowSearch: "Suche ...",
    name2: "Name", name3: "Gerätebezeichnung"
};

//Home.aspx
var homePage = { quickSearch: "Schnellsuche", searchTxt: "IMEI Nr./ Geräte / Auto Nr.", stage: "Plattform", deviceCount: "Anzahl der Geräte", operations: "Weiter", novice: "Neukunde", quickSale: "Verkauf",
    batchSale: "Verschieben", customer: "Kunde", sale: "Verkauf", customerList: "Kundenliste", addDevice: "Gerätetreiber hinzufügen", add: "Hinzufügen", imeiNullMsg: "Bitte wählen Sie das Ziel!", expireTimeNullMsg: "Bitte wählen Sie die Ablaufzeit!",
    saleSuccess: "Erfolg!"
};

var warnMessagePage = { alarmType: "Alarmtyp", alarmTime: "Alarmzeit" };

var alarmIndexPage = { geofenceIn: "Virtueller Zaun An", geofenceOut: "Virtueller Zaun Aus", moved: "Verschieben", lowBattery: "Alarm, der Akku ist schwach", sos: "SOS Alarm", cutPower: "Alarm, die Stromversorgung ist unterbrochen", vibration: "Vibrationsalarm",
    overSpeed: "Drehzahlbegrenzung", offline: "Offline"
};

//map.aspx
var mapPage = { searchInput: 'Namen des Gerätes / IMEI Nr. Eingeben', divicesInfo: "Geräteinformation", geofence: "GeoFence", cutOffPetrol: "Schaltausgang Aktivieren", restorePetrol: "Schaltausgang Deaktivieren", checkLocation: "Positions - Abfrage", checkCommand: "Befehl prüfen",
    sendConfirm: "Vor Befehl absenden, Konto bestätigen", passNull: "Passwort eingeben!", passError: "Passwort falsch!", sendMsg1: "Nachricht wird gesendet, bitte warten Sie", sendSuccess: "Erfogreich! Bitte warten Sie auf Antwort",
    sendMsg2: "Befehl ist ungültig", sendMsg3: "Gerät ist nicht vorhanden", sendMsg4: "Das Gerät und der Server wurde getrennt", sendMsg5: "Befehl erfolgreich gesendet", responseSuccess: "Erfogreiche Antwort!", responseNull: "Keine Antwort!",
    checkLocatoin: "Position prüfen", checkCommandTitle: "Befehl prüfen", cmdType: "Art des Befehls", cmdState: "Befehl Status", responseText: "Antwortnachricht", responseTime: "Antwortzeit", sendTime: "Übertragungszeit",
    dyd: "Kraftstoff aus", hfyd: "Kraftstoff zurückgeben", deviceResponse: "Geräteantwort", sendSuccess2: "Befehl wurde gesendet!", noSend: "Befehl wurde nicht gesendet", deviceDetailList: "Geräte Detail - Liste", addGroup: "Gruppe hinzufügen", defaultGroup: "Standard",
    moveToGroup: "Gruppe verschieben", delGroupConfirm: "Löschen bestätigt?", downloadLocation: "Download Track", deviceFortify: "GPS Fortify", deviceDismiss: "GPS Dismiss", accOn: "ACC ON", accOff: "ACC OFF", fortify: "ARM", dismiss: "DimARM", carOpen: "Door Open", carClose: "Door Close", zdlj: "engine connect", zddk: "engine cut",
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

var courseName = { dueNorth: "Norden", northeast: "Nordosten", dueEast: "Osten", southeast: "Südosten", dueSouth: "Süden", southwest: "Südwesten", dueWest: "Westen", northwest: "Nordwesten" };

//CustomersList.aspx
var cusPage = { delUserConfirm: "Löschen bestätigen", delUserConfirm2: "Dieser Benutzer?", delUserMsg: "Das Benutzergerät kann nicht gelöscht werden!", delUserMsg2: "Dieses Konto hat Unter - Konto. Es kann nicht gelöscht werden!", msgFailed: "Löschen fehlgeschlagen!",
    updateUserSuccess: "Erfolg!", updateUserFailed: "Fehlschlag!", delDeviceConfirm2: "Gerät bestätigt?", addCus: "Kunde hinzufügen", manCus: "Kunden verwalten", delCus: "Kunde entfernen", saleTo: "Verschieben Nach:",
    beDevice: "Hauptkonto", updateExpTime: "Fälligkeit aktualisieren", deviceChange: "Verschieben", cusInfo: "Kundeninformationen", toCus: "Zielkunden", newAddChildrenCus: "Kunde hinzufügen", parentCus: "Hauptkonto",
    confirmPass: "Passwort bestätigen", allCus: "Alle Kunden", addCusTitle: "Kunde hinzufügen", loginToUser: "Überwachen", childCus: "Unterkonto", changeDevices: "Gesamtübertragung", updateExpDevices: "Gesamtaktualisierung",
    addUserMsg: "Tipp: Informationen, wie Telefonkontakte vervollständigen", plsParentCusMsg: "Bitte wählen Sie den erstklassigen Kundenservice!", msg1: "Name, Konto, Passwort darf nicht leer sein!", passError: "Die Passwörter stimmen nicht überein, neu eingeben!",
    existAccount: "Konto hat bestanden!", dataError: "Keine Daten!", dataNull: "Name, Konto, Passwort darf nicht leer sein!", imeisNull: "IMEI Nr. darf nicht leer sein!", saveDevuceMsg1: "Zusammen eingeben:", saveDevuceMsg2: "IMEI Nr.- Gruppe,",
    saveDevuceMsg3: "Erfolg!", saveDevuceMsg4: "Erfolg:", saveDevuceMsg5: "Gruppe", saveDevuceMsg6: "Fehlschlag:", saveDevuceMsg7: "Fehler!", updateExpSuccess: "Erfolg!",
    updateError: "Fehlschlag bearbeiten!", changeDeviceSuccess: "Erfolg!", changeDeviceError: "Technischer Defekt!", confirmInitPassMsg1: "Zurücksetzung bestätigen:", confirmInitPassMsg2: "Passwort?",
    initPassSuccess: "Passwort erfolgreich zurückgesetzt!", initPassError: "Fehlschlag Passwort zurücksetzen!", confirmInitUserPassMsg1: "Zurücksetzung des Kontos bestätigen?", page: "Page", records: "Records"
};

var productUpdatePage = { oilCoefficient: "Kraftstoff/100 km", updateIcon: "Symbol", carNumMsg1: "Autonr. ist zu lang!", sccuess: "Aktualisierung erfolgreich!", faild: "Aktualisierung fehlgeschlagen!", isExistMsg: "Autonr. gibt es bereits, bitte ändern!", filterLBS: "filter LBS", photo: "Photo" };



var moneyPage = { day: "day", msg1: "1 point for 1 day", oneYeah: "one year", twoYeah: "two years", lifelong: "lifelong", msg2: "Extend the maturity time", msg3: "the device is charged successfully!", msg4: "the points is not enough, please charge!!", msg5: "days must be greater then 0!", msg6: "can not transfer to oneself!", pointGive: "points for sale", pointConsumptionLog: "points consumer records", loginNameNotExist: "this name does not exist", intoLoginID: "the login account to transfer into", intoUsername: "the username to transfer into", intoPoint: "the points to transfer into", detect: "detect", pleaseDetectLoginName: "please test the login name", pleaseInputIntoPoint: "please input the points" };
//var moneyPage = { day: "天", msg1: "1个点数1天", oneYeah: "一年", twoYeah: "二年", lifelong: "终身", msg2: "增加到期天数", msg3: "设备充值天数成功!", msg4: "点数不够,请充值!", msg5: "天数必须大于0!" };

var yiwen201405 = { battery: "Battery Level" };
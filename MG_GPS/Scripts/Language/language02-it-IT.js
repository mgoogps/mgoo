
function writePage(msg) {
    //意大利
    document.write(msg);
}
var allPage = {
    tab1: "Visualizza Movimenti", tab2: "Visualizza Allarmi", tab3: "Statistiche Dispositivo", tab4: "Dettaglio Allarmi", startTime: "Da", endTime: "A", search: "Cerca", num: "N.", deviceName: "Nome Device",
    time: "Data", distance: "Distanza (km)", overspeed: "Supero Velocità", noData: "Nessun dato!", lat: "Lat", lng: "Lon", speed: "Velocità", address: "Indirizzo", speedKM: "km/h", day: "Giorno", hour: "Ora",
    minute: "Minuti", pleSel: "Seleziona", date: "Data", plsDeviceMsg: "Seleziona il Dispositivo", moreDevice: "altro", msg: "Messaggio", myAccount: "Mio conto", changePass: "Cambia password",
    tracking: "Tracciamento", playback: "Rivedi", monitor: "Visualizza", home: "Home", customer: "Cliente", report: "Statistiche", more: "altro", no: "N.", name: "Nome", carNum: "Targa",
    imeiNo: "N. IMEI", activeTime: "Data Attivazione", hireExpireTime: "Data Scadenza", operation: "Esegui", edit: "Modifica", divicesInfo: "Informazioni Dispositivo", cellName: "Contatti",
    phone: "Tel/Mob", timezone: "Fuso Orario", save: "Salva", confirm: "Conferma", updateUserSuccess: "Modifica completata!", updateUserFailed: "Errore modifica!", modelName: "Tipo", state: "Stato",
    drection: "Direzione", baidu: "Baidu", google: "Google", day: "Giorno", hour: "Ora", minute: "Minuto", stopTime: "Sosta", desc: "Commento", cancel: "Cancella", del: "Elimina", delSuccess: "Eliminato!",
    delFaild: "Errore eliminazione!", accStr: "Stato ACC", acc0: "Spento", acc1: "Acceso", positionType: "Tipo Posizione", manDevice: "Manutenzione Dispositivo", type: "Tipo", acc2: "Non connesso", clear: "Pulisci", positionTime: "Data Posizione",
    targetName: "Nome Dispositivo", primaryEmail: "Email", resolve: "Risolvi", startStopTime: "Inizio", endStopTime: "Fine", status1: "Disconnesso", moving: "In movimento", stopCar: "Stop", arrears: "ritardo",
    toExcel: "A Excel", distance2: "Distanza", km: "km", m: "M", event: "Registrazione Evento", inTime: "Imposta Tempo", outTime: "Data Partenza", moneyCount: "Punto", belongTo: "Abbonamento", updateTime: "Data Aggiornamento",
    userInfo: " Informazioni Cliente ", userType2: "Distributore", changePassword: " Cambia Password", service: "Mio Servizio", clearAll: "Pulisci tutto",

    deviceNo: "Numero IMEI", message: " Messaggio ", exit: " Uscita ",
    selSizeStr: "Seleziona", sizeStr: "registra", createTime: " Ora Creazione", device: "Dispositivo", purchase: "Acquista ", stock: "Magazzino",
    mobileWebsite: "Accedi da mobile", simNo: "N. SIM", expireTime: "fino al", remark: " Commento ", deviceInfo: "Informazioni Dispositivo",
    dataError: "Errore Dati!", softFailed: "Errore!", password: " Password ",
    searchNull: "IMEI/Nome/N. Auto/Nome/Utente è richiesto!", initPass: "Azzera Pwd", loginAccount: "Utente", passLengthMsg: " La password deve essere massimo 20 caratteri ", userType1: "Utilizzatore",
    add: "Nuovo", information: "Informazioni", deviceType: "Tipo", kmHour: "Km/h", devicePhone: "N. SIM",
    deletes: "Cancella", cusName: "Nome cliente", plsSelUser: "Inserire un utente!",
    plsSel: "Seleziona", all: "Tutti", online: "In linea ", offline: "Non in linea ", speedLimit: " Limite Velocità ",
    allDistance: "Totale kilometraggio", edit2: "Modifica", type2: "Regole",
    deliveryTime: " Data Attivazione ", cellPhone: "Tel/Mob", view: "Vista", deviceHireDay: "punti da caricare", myDevice: "Mio Apparecchio", StatisticalAnalysis: "analisi statistiche"
};

//Dealer.aspx
var dealerPage = {
    warnTitle: "Alarm Remind", warnSound: "Suono Allarme acceso", username: "Nome Cliente/Utente", hello: "Ciao", changePassword: " Cambio Password", searchDevice: "Dispositivo", searchUser: "Cliente",
    searchDevice2: "Controllo Dispositivo", searchUser2: "Controllo Cliente", name: "Nome Cliente", belongTo: "Gruppo", sim: "N. SIM", activeTime: "Data Attivazione", customerName: "Nome Cliente", nowSearch: "ricerca...", name2: "Nome Cliente",
    name3: "Nome Dispositivo"
};

//Home.aspx
var homePage = {
    quickSearch: "Cerca", searchTxt: "N.IMEI./T/Targa", stage: "Fase", deviceCount: "Contatore", operations: "più", novice: " nuovo", quickSale: " Vendita veloce ",
    batchSale: " Vendita Blocchi", customer: "Cliente", sale: "Vendita", customerList: " Elenco Clienti  ", addDevice: "Aggiunta", add: "+nuovo", imeiNullMsg: "Selezionare il Dispositivo!", expireTimeNullMsg: "Selezionare la data di scadenza!",
    saleSuccess: " Completato!"
};

var courseName = { dueNorth: "Nord", northeast: " Nordest ", dueEast: "Est", southeast: " Sudest", dueSouth: "Sud", southwest: "Sudovest", dueWest: "Ovest", northwest: "Nordovest " };

var reportPage = { title: "Totale Statistiche Movimento", warnCount: "Allarmi", stopCount: "Soste" };

var alarmSumPage = { title: "Totale Statistiche Allarmi", lowCount: "Allarme Batteria scarica", cutPowerCount: "Allarme Spegnimento", vibCount: "Allarme Vibrazione", sosCount: "Allarme SOS" }

var overSpeedPage = { continueTime: "Durata", speedlimit: "Limite Velocità", distancePage: "Distanza", overspeedDetail: "Dettagli supero velocità", stopDetail: "Dettagli Soste" };

var alarmIndexPage = { geofenceIn: "Geo-recinto In", geofenceOut: "Geo-recinto Out", moved: "Raggio", lowBattery: "Allarme batteria scarica", sos: "Allarme SOS", cutPower: "Allarme spegnimento", vibration: "Allarme Vibrazione",
    overSpeed: "Supero Velocità", offline: "Non in linea"
};

var runindexPage = { statistics: "Ricerca per", statistics2: "Dettagli giornalieri", oilCoefficient: "Coefficiente consumo carburante/100 Kilometri", L: "L", oil: "Consumo" };

var alarmDetailPage = { alarmTime: "Data Allarme", alarmType: "Tipo Allarme", alarmCount: "Statistiche Allarme", alarmDetail: "Dettagli Allarme" };

var userPage = { warnTitle: "Allarmi", warnSound: "Suono Allarme acceso", day7Exp: "Scaduto 7 Giorni", day60Exp: "Scaduto 60 Giorni", alreadyExp: "Scaduto",
    username: "Nome/Utente", hello: "Ciao", searchDevice: "Dispositivo", searchUser: "Cliente", exit: "Esci", message: "Messaggio", allDeivce: "Tutti Dispositivi", moneyMove: "punti in vendita", moneyHistory: "registra punti cliente"
};

var productUpdatePage = { oilCoefficient: "Consumo/100km", updateIcon: "Icona", carNumMsg1: "Targa troppo lunga!", sccuess: "Aggiornamento riuscito!", faild: "Aggiornamento mancato!", isExistMsg: "Targa esistente， riprovare!", filterLBS: "filtro LBS", photo: "Foto" };

var userInfoPage = { myAccount: "Mio conto", changePassword: "Cambia Password ", userMsg: " Consiglio: Completa le informazioni come numero telefono, contatto.", customerName: " Nome Cliente ",
    account: "Utente", oldPass: "password attuale", newPass: "Nuova password", confirmPass: "Conferma Password", passLengthMsg: " Password deve essere meno di 20 caratteri ", passNull: "Password non può essere vuota!",
    passError: "Le due passwords non coincidono!", changePassSuccess: "ripristino Password completato!", changePassError: "errore ripristino Password!", oldPassError: "password attuale errata!",
    warnSendMsg: "Modalità altra notifica Allarme", sendEmail: "Email", service: "Mio Servizio"
};

var warnMessagePage = { warnMsg: "Messaggio Allarme", handle1: "Undealed ", handle2: " handled ", alarmType: "Tipo Allarme", alarmTime: "Data Allarme" };

var trackingPage = { secondMsg: "Aggiorna dopo secondi!" };

var playbackPage = { from: "Da", to: "A", play: "Riproduci", pause: "Pausa", next: "Continua", fast: "Veloce", slow: "Lento", timeMsg: "Data fine deve essere maggiore di Data inizio!", nowLoading: "Caricamento..!",
    playOver: "Finito!", searchNull: "Nessun dato!", showLBS: "Mostra LBS"
};

var geofencesPage = { geofence: " Geo-recinto ", addGeofence: "Aggiungi", geoNameNull: "nome Geo-recinto non può essere vuoto!", radius: "R(m)", delGeoConfirm: "Conferma cancellazione", delGeoConfirm2: "Questo Geo-recinto?" ,
    addSuccess: "Aggiunto!", addFaild: "Errore inserimento!"
};

var iframeMapPage = { baiduMap: "Baidu Map", googleMap: "Google Map", deviceName: "Nome Dispositivo" };

var userUpdatePage = { account: "Utente" };

//map.aspx
var mapPage = { searchInput: "Inserire Nome/N.IMEI", divicesInfo:"Info Dispositivo", geofence: "Geo-recinto", cutOffPetrol: "Cut off Petrol", restorePetrol: "Restore Petrol ", checkLocation: " Verifica Località ", checkCommand: " Comandi Verifica ",
    sendConfirm: "Confermare l'utente prima di inviare comandi", passNull: "Inserire password!", passError: "Errore Password!", sendMsg1: "Invio messaggio, attenndere prego…", sendSuccess: "Completato! Attendere risposta...",
    sendMsg2: "Comando assente", sendMsg3: "Dispositivo non esiste ", sendMsg4: "Prima accendere il dispositivo.", sendMsg5: "Comando inviato correttamente", responseSuccess: " Risposta ricevuta!", responseNull: " Risposta assente!",
    checkLocatoin: " Verifica Località ", checkCommandTitle: "Comando Controllo", cmdType: "Tipo Comando", cmdState: "Stato Comando", responseText: "Messaggio di risposta", responseTime: " Data Risposta ", sendTime: "Data Invio",
    dyd: "Cut off Petrol", hfyd: " Restore Petrol ", deviceResponse: "Risposta Dispositivo", sendSuccess2: "Il comandoè stato inviato!", noSend: "Il comando non è stato inviato", deviceDetailList: " Elenco dettagli Dispositivo ", addGroup: "Aggiungi Gruppo",
    defaultGroup: "Default", moveToGroup: "Sposta Gruppo", delGroupConfirm: "Conferma cancellazione?", downloadLocation: "Tracking Report", deviceFortify: "GPS Fortify", deviceDismiss: "GPS Dismiss", accOn: "ACC ON", accOff: "ACC OFF", fortify: "ARM", dismiss: "DimARM", carOpen: "Portiera aperta", carClose: "Portiera chiusa", zdlj: "motore connesso",
    zddk: "motore spento", obdChecking: "controllo OBD", uploadTime: "intervallo aggiornamento", setQinqing: "Imposta numero familiare", setSOS: "Imposta numero SOS", setZhukong: "Imposta numero principale", setPassword: "cambia password", setAutoFortify: "Auto ARM ON",
    setAutoFortifyClose: "Auto ARM OFF", setCutFortifyAuto: "external power cut engine, auto ARM ON", setCutFortifyAutoClose: "external power cut engine, auto ARM Off", setVIBTime: "Imposta valore inattività allarme movimento", setVIBLmd: "Imposta sensibilità allarme movimento", setSOSType: "Allarme SOS",
    setWeiyiWarn: "Imposta Allarme Movimento", setOverspeed: "Imposta Allarme Supero Velocità", setSMSGPRS: "modalità SMS/GPRS", setJianting: "Visualizza", setYccq: "Riavvio da remoto", setHfcc: "ripristina impostazioni fabbrica", setLanguage: "Imposta lingua",
    setTimezone: "Imposta Fuso Orario", setXiumian: "Imposta modalità risparmio", setJiantingType: "modalità visualizzazione", setDingweiType: "modalità positionamento", setParam: "controllo dati", setAutoFortifyTime: "Auto ARM time", setAutoDismissTime: "Auto DisARM time",
    setUploadMoveTime: "Move upload frequency", setUploadStopTime: "Rest upload frequency", setYcqd: "accensione da remoto", setYcxh: "spegnimento da remoto", setGeofence: "GEO-recinto", setOBDUploadTime: "intervallo aggiornamento OBD",
    setOBDCMD: "comando aggiornamento OBD", setOBDGg: "messaggio aggiornamento OBD", setDeviceUploadTime: "Imposta intervallo aggiornamento", danwei5s: "unità:secondi, minimo: 5secondi", obdTab1: "stato veicolo", obdTab2: "Cronologia", obdTab3: "Comando OBD",
    inputCmdStr: "Comando Input", inputGgStr: "Messaggio Input", secondsMsg1: "L'intervallo non può essere meno di 5 secondi!", secondsMsg2: "L'intervallo non può essere più di 9999 secondi!", startTime: "Data inizio", endTime: "Data fine",
    pidCount: "Quantità errata", pidStr: "Descrizione errata", noGuzhang: "Nessun errore", deetail: "Detagli", obdDataTime: "Data Ora", obdFdjfh: "carica motore", obdFdjsw: "tempeatura raffreddamento motore", obdDsryxz: "Correzione carburante brevi percorsi",
    obdCsryxz: "Correzione carburante lunghi percorsi", obdJqqgjdyl: "pressione assoluta manifold", obdFdjzs: "velocità motore", obdClsd: "velocità veicolo", obdQgdhtqj: "No. 1 cylinder ignition advance Angle", obdJqwd: "air inlet temperature", obdKqll: "Air flow", obdJqmjdwz: "Throttle absolute position",
    obdMILGzd: "Drive distance after MIL (trouble light) on", obdRylsr: "Fuel Qty input", obdDqyl: "barometric pressure", obdKzmkdy: "control module Voltage", obdSsyh: "instantaneous fuel consumption", obdMl: "horsepower", obdZlc: "total distance", obdBglyh: "fuel consumption per hundred kilometers",
    obdDpdy: "Voltaggio Batteria", obdGzdm: "Codice errore"
};

var mapPage2 = { searchInput: "Inserire Nome/N.IMEI", divicesInfo:"Info Dispositivo", geofence: "Geo-recinto", cutOffPetrol: "Cut off Petrol", restorePetrol: "Restore Petrol ", checkLocation: " Verifica Località ", checkCommand: " Comandi Verifica ",
    sendConfirm: "Confermare l'utente prima di inviare comandi", passNull: "Inserire password!", passError: "Errore Password!", sendMsg1: "Invio messaggio, attenndere prego…", sendSuccess: "Completato! Attendere risposta...",
    sendMsg2: "Comando assente", sendMsg3: "Dispositivo non esiste ", sendMsg4: "Prima accendere il dispositivo.", sendMsg5: "Comando inviato correttamente", responseSuccess: " Risposta ricevuta!", responseNull: " Risposta assente!",
    checkLocatoin: " Verifica Località ", checkCommandTitle: "Comando Controllo", cmdType: "Tipo Comando", cmdState: "Stato Comando", responseText: "Messaggio di risposta", responseTime: " Data Risposta ", sendTime: "Data Invio",
    dyd: "Cut off Petrol", hfyd: " Restore Petrol ", deviceResponse: "Risposta Dispositivo", sendSuccess2: "Il comandoè stato inviato!", noSend: "Il comando non è stato inviato", deviceDetailList: " Elenco dettagli Dispositivo ", addGroup: "Aggiungi Gruppo",
    defaultGroup: "Default", moveToGroup: "Sposta Gruppo", delGroupConfirm: "Conferma Cancellazione?", downloadLocation: "Tracking Report", deviceFortify: "GPS Fortify", deviceDismiss: "GPS Dismiss", accOn: "ACC ON", accOff: "ACC OFF", fortify: "ARM", dismiss: "DimARM", carOpen: "Portiera aperta", carClose: "Portiera chiusa", zdlj: "Collega motore", zddk: "Spegni motore",
    obdChecking: "Verifica OBD", uploadTime: "intervallo aggiornamento", setQinqing: "Imposta numero familiare", setSOS: "Imposta numero SOS", setZhukong: "Imposta numero principale", setPassword: "cambia password", setAutoFortify: "Auto ARM ON",
    setAutoFortifyClose: "Auto ARM OFF", setCutFortifyAuto: "external power cut engine, auto ARM ON", setCutFortifyAutoClose: "external power cut engine, auto ARM Off", setVIBTime: "Imposta valore inattività allarme movimento", setVIBLmd: "Imposta sensibilità allarme movimento", setSOSType: "Allarme SOS",
    setWeiyiWarn: "Imposta Allarme Movimento", setOverspeed: "Imposta Allarme Supero Velocità", setSMSGPRS: "modalità SMS/GPRS", setJianting: "Visualizza", setYccq: "Riavvio da remoto", setHfcc: "ripristina impostazioni fabbrica", setLanguage: "Imposta lingua",
    setTimezone: "Imposta Fuso Orario", setXiumian: "Imposta modalità risparmio", setJiantingType: "modalità visualizzazione", setDingweiType: "modalità positionamento", setParam: "controllo dati", setAutoFortifyTime: "Auto ARM time", setAutoDismissTime: "Auto DisARM time",
    setUploadMoveTime: "Move upload frequency", setUploadStopTime: "Rest upload frequency", setYcqd: "accensione da remoto", setYcxh: "spegnimento da remoto", setGeofence: "GEO-recinto", setOBDUploadTime: "intervallo aggiornamento OBD",
    setOBDCMD: "comando aggiornamento OBD", setOBDGg: "messaggio aggiornamento OBD", setDeviceUploadTime: "Imposta intervallo aggiornamento", danwei5s: "unità:secondi, minimo: 5secondi", obdTab1: "stato veicolo", obdTab2: "Cronologia", obdTab3: "Comando OBD",
    inputCmdStr: "Comando Input", inputGgStr: "Messaggio Input", secondsMsg1: "L'intervallo non può essere meno di 5 secondi!", secondsMsg2: "L'intervallo non può essere più di 9999 secondi!", startTime: "Data inizio", endTime: "Data fine",
    pidCount: "Quantità errata", pidStr: "Descrizione errata", noGuzhang: "Nessun errore", deetail: "Detagli", obdDataTime: "Data Ora", obdFdjfh: "carica motore", obdFdjsw: "tempeatura raffreddamento motore", obdDsryxz: "Correzione carburante brevi percorsi",
    obdCsryxz: "Correzione carburante lunghi percorsi", obdJqqgjdyl: "pressione assoluta manifold", obdFdjzs: "velocità motore", obdClsd: "velocità veicolo", obdQgdhtqj: "No. 1 cylinder ignition advance Angle", obdJqwd: "air inlet temperature", obdKqll: "Air flow", obdJqmjdwz: "Throttle absolute position",
    obdMILGzd: "Drive distance after MIL (trouble light) on", obdRylsr: "Fuel Qty input", obdDqyl: "barometric pressure", obdKzmkdy: "control module Voltage", obdSsyh: "instantaneous fuel consumption", obdMl: "horsepower", obdZlc: "total distance", obdBglyh: "fuel consumption per hundred kilometers",
    obdDpdy: "Voltaggio Batteria", obdGzdm: "Codice errore"
};

var downloadLocation = { download: "Scarica", help: "Aiuto", step: "Passi", step1: "1.Selezionare dispositivo.", step2: "2.Data.", step3: "3.Premi 'Scarica'.",
    msg1: "Note：Se ricevi 'Nessun dato!' Vuol dire che non esistono dati validi per il periodo selezionato.", msg2: "Il formato di scaricamento del tracciamento è Google KML, es.:'nome file.kml'. ",
    msg3: "Doppio click sul file KML per aprirlo dopo aver installato Google Earth", msg4: "KML Il file visualizza il tracciamento con linee rosse dinamiche in google maps .", msg5: "Consiglio:Per scaricare  Google Earth Click", here: "qui",
    msg6: "E' possibile scaricare il file di tracciamento di un periodo selezionato mediante la funzione 'scarica file tracciamento'。Tipo di file 'KML Track File' e 'KML Anchor File'"
};

//CustomersList.aspx
var cusPage = {
    delUserConfirm: "Conferma cancellazione", delUserConfirm2: "Questo utente?", delUserMsg: "Utente con dispositivi. Non può essere cancellato!", delUserMsg2: "Questo utente ha un sub-utente. Non può essere cancellato! ", msgFailed: "Errore cancellazione!",
    updateUserSuccess: "Completato!", updateUserFailed: "Errore!!", delDeviceConfirm2: "questo Dispositivo?", addCus: "Aggiungi Cliente", manCus: "Modifica Cliente", delCus: "Cancella Cliente", saleTo: "Sposta Cliente ",
    beDevice: "Abbonamento", updateExpTime: "Aggiorna scadenza", deviceChange: "Sposta", cusInfo: "Informazioni Cliente", toCus: "Dispositivo Cliente", newAddChildrenCus: "Aggiungi Cliente", parentCus: " Cliente Superiore ",
    confirmPass: "Conferma Password ", allCus: "Tutti Clienti", addCusTitle: " Aggiungi Cliente", loginToUser: "Visualizza", childCus: "Sottoconto", changeDevices: "Spostamento differito", updateExpDevices: "Aggiornamento differito",
    addUserMsg: "Consiglio: Completare informazioni tipo numero telefono,contatto. ", plsParentCusMsg: "Seleziona il cliente superiore!", msg1: "Nome, Utente, Password non può essere vuota!", passError: "Le password inserite non corrispondono, ripetere prego!",
    existAccount: "Utente esistente!", dataError: "Nessun dato!", dataNull: "Nome, Utente, Password non può essere vuota!", imeisNull: "N.IMEI non può essere vuoto!", saveDevuceMsg1: "Input insieme:", saveDevuceMsg2: "N. Gruppo di IMEI",
    saveDevuceMsg3: "Completato!", saveDevuceMsg4: "Completato:", saveDevuceMsg5: "Gruppo", saveDevuceMsg6: "Errore:", saveDevuceMsg7: "Error!", updateExpSuccess: "Completato!",
    updateError: "Errore Modifica!", changeDeviceSuccess: "Completato!", changeDeviceError: "Errore sposta Dispositivo!", confirmInitPassMsg1: "Sicuro di voler azzerare:", confirmInitPassMsg2: "password?:",
    initPassSuccess: "Password azzera con successo!", initPassError: "Errore azzeramento Password!", confirmInitUserPassMsg1: "Conferma azzeramento utente?:", page: "Page", records: "Records"
};


var moneyPage = { moveToAccount: "il conto a cui trasferire", moveToUser: "l'utente a cui trasferire", moveCount: "punti da trasferire", check: "rileva", noLoginName: "questo nome non esiste!", inputLoginNameIn: "controlla nome utente!",
    inputLoginNameOut: "controlla nome utente!", noMoveSelf: "non è possibile trasferire a se stessi!", moneyLack: "punti insufficienti!", moneyError: "errore trasferimento!", moveSuccess: "punti trasferiti, il totale aggiornato è:", inputMoneyMsg: "inserire i punti!",
    uToUser: "per l'utente", money: "punto", user: "utente", moveMoneyMsg1: "i punti sono stati trasferiti ", give: "a", moneyMsg2: "carica per il dispositivo, totale punti utilizzati", day: "giorno", msg1: "1 punto 1 giorno", oneYeah: "un anno", twoYeah: "due anni",
    lifelong: "validità", msg2: "Aumenta tempo maturazione", msg3: "il dispositivo è stato caricato!", msg4: "punti insufficienti, ricarica!!", msg5: "i giorni devono essere più di 0!", pointManagement: "manutenzione punti"
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
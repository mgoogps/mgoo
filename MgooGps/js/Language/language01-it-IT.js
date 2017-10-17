
function writePage(msg) {
    //意大利
    document.write(msg);
}

var allPage = { deviceNo: "Numero IMEI", search: "Cerca", tracking: "Traccciamento", playback: " Rivedi ", monitor: " Visualizza", home: " Inizio ", customer: " Affari ", message: " Messaggio ", exit: " Uscita ",
    selSizeStr: "Seleziona", sizeStr: "registra", createTime: " Ora Creazione", operation: " Opera", no: "NO.", cellName: "Contatti", phone: "Tel/Mob", device: "Dispositivo", purchase: "Acquista ", stock: "Magazzino",
    mobileWebsite: "Accedi da mobile", deviceName: "Nome Dispositivo", simNo: "N. SIM", expireTime: "fino al", remark: " Commento ", cancel: "Cancella ", deviceInfo: "Informazioni Dispositivo", imeiNo: "Numero IMEI ",
    dataError: "Errore Dati!", softFailed: "Errore!", password: " Password ", confirm: "Salva", num: "N.", noData: "Nessuno Dato!", acc0: "Spento", acc1: "Acceso",
    acc2: "Disconesso", searchNull: "IMEI/Nome/N. Auto/Nome/Utente è richiesto!", initPass: "Azzera Pwd", updateTime: "Tempo di Aggiornamento ", loginAccount: "Utente", passLengthMsg: " La password deve essere massimo 20 caratteri ", type: "Tipo", userType1: "Utilizzatore",
    userType2: "Distributore", address: "Indirizzo", add: "Nuovo", information: "Informazioni", deviceType: "Tipo", overspeed: "Supero velocità", kmHour: "Km/h", devicePhone: "N. SIM", modelName: "Tipo",
    createTime: "Data Creazione", activeTime: "Data Attivazione", hireExpireTime: "Data Scadenza", edit: " Modifica", more: " Espandi ", deletes: "Cancella", cusName: "Nome cliente", userInfo: " Informazioni cliente ", plsSelUser: "Inserire un utente!",
    km: "Kilometri", plsSel: "Seleziona", all: "Tutti", online: "In linea ", offline: "Non in linea ", carNum: "N. Licenza", speedLimit: " Limite Velocità ", lat: "Latitudine", lng: "Longitudine", speed: "Velocità ", drection: "Direzione",
    allDistance: "Totale kilometraggio", state: "Stato", positionTime: "Data Posizione", status1: "Disconnesso", moving: "In movimento", stopCar: "Stop", accStr: "stato ACC", edit2: "Modifica", type2: "Regole",
    deliveryTime: " Data Attivazione ", cellPhone: "Tel/Mob", save: "Salva", clear: "Cancella", arrears: "ritardo", view: "Vista", deviceHireDay: "punti da caricare", service: "Mio Servizio", myDevice: "Mio Apparecchio"
};

//login.aspx
var loginPage = {
    title: "Sistema di tracciamento GPS ", accountTab: "Utente/telefono/IMEI ", imeiTab: "N. IMEI", account: "Utente", password: "Password ", loginSubmit: "Vai", languageMsg: " Disponibile in ", loginMsg: "Inserisci il conto.",
    loginMsg2: "Per favore inserisci LA TUA password.", loginMsg3: "il campo non può essere vuoto!", wytx: "Voglio provare", androidBaidu: "android(Baidu map) Scarica", androidGoogle: "android(Google map) Scarica", iphone: "iphone jailbreak Scarica",
    welcome: "Benvenuto, fai il login!", loginErrorMsg: "Conto o la password di errore", loginFailure: "Accesso non riuscito", ForgotPassword: "Password dimenticata?", RememberPassword: "Ricorda nome utente e password", Register: "Register"
};


//Dealer.aspx
var dealerPage = { warnTitle: "Alarm Remind", warnSound: "Suono Allarme acceso", username: "Nome Cliente/Utente", hello:"Ciao", changePassword:" Cambio Password", searchDevice:"Dispositivo", searchUser:"Cliente",
    searchDevice2: "Controllo Dispositivo", searchUser2: "Controllo Cliente", name: "Nome Cliente", belongTo: "Gruppo", sim: "N. SIM", activeTime: "Data Attivazione", customerName: "Nome Cliente", nowSearch: "ricerca...", name2: "Nome Cliente",
    name3: "Nome Dispositivo"
};

//Home.aspx
var homePage = { quickSearch: "Cerca", searchTxt: "N.IMEI./T/Targa", stage: "Fase", deviceCount: "Contatore", operations: "più", novice: " nuovo", quickSale: " Vendita veloce ",
    batchSale: " Vendita Blocchi", customer: "Cliente", sale: "Vendita", customerList: " Elenco Clienti  ", addDevice: "Aggiunta", add: "+nuovo", imeiNullMsg: "Selezionare il Dispositivo!", expireTimeNullMsg: "Selezionare la data di scadenza!",
    saleSuccess:" Completato!"
};

var warnMessagePage = { alarmType: "Tipo Allarme", alarmTime: "Data Allarme" };

var alarmIndexPage = { geofenceIn: "GEO-recinto In ", geofenceOut: " GEO-recinto Out ", moved: "Distanza", lowBattery: "Allarme batteria scarica", sos: "Allarme SOS", cutPower: "Allarme Interruzione", vibration: "Allarme Vibrazione",
    overSpeed: "Supero velocità", offline: "Spento"
};

//map.aspx
var mapPage = { searchInput: "Inserire Nome/N.IMEI", divicesInfo:"Info Dispositivo", geofence: "Geo-recinto", cutOffPetrol: "Cut off Petrol", restorePetrol: "Restore Petrol ", checkLocation: " Verifica Località ", checkCommand: " Comandi Verifica ",
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

var courseName = { dueNorth: "Nord", northeast: "Nord-est", dueEast: "Est", southeast: " Sud-est ", dueSouth: "Sud", southwest: " Sud-ovest ", dueWest: "Ovest", northwest: " Nord-ovest " };

//CustomersList.aspx
var cusPage = { delUserConfirm: "Conferma cancellazione", delUserConfirm2: "Questo utente?", delUserMsg: "Utente con dispositivi. Non può essere cancellato!", delUserMsg2: "Questo utente ha un sub-utente. Non può essere cancellato! ", msgFailed: "Errore cancellazione!",
    updateUserSuccess: "Completato!", updateUserFailed: "Errore!!", delDeviceConfirm2: "questo Dispositivo?", addCus: "Aggiungi Cliente", manCus: "Modifica Cliente", delCus: "Cancella Cliente", saleTo: "Sposta Cliente ",
    beDevice: "Abbonamento", updateExpTime: "Aggiorna scadenza", deviceChange: "Sposta", cusInfo: "Informazioni Cliente", toCus: "Dispositivo Cliente", newAddChildrenCus: "Aggiungi Cliente", parentCus: " Cliente Superiore ",
    confirmPass: "Conferma Password ", allCus: "Tutti Clienti", addCusTitle: " Aggiungi Cliente", loginToUser: "Visualizza", childCus: "Sottoconto", changeDevices: "Spostamento differito", updateExpDevices: "Aggiornamento differito",
    addUserMsg: "Consiglio: Completare informazioni tipo numero telefono,contatto. ", plsParentCusMsg: "Seleziona il cliente superiore!", msg1: "Nome, Utente, Password non può essere vuota!", passError: "Le password inserite non corrispondono, ripetere prego!",
    existAccount: "Utente esistente!", dataError: "Nessun dato!", dataNull: "Nome, Utente, Password non può essere vuota!", imeisNull: "N.IMEI non può essere vuoto!", saveDevuceMsg1: "Input insieme:", saveDevuceMsg2: "N. Gruppo di IMEI",
    saveDevuceMsg3: "Completato!", saveDevuceMsg4: "Completato:", saveDevuceMsg5: "Gruppo", saveDevuceMsg6: "Errore:", saveDevuceMsg7: "Error!", updateExpSuccess: "Completato!",
    updateError: "Errore Modifica!", changeDeviceSuccess: "Completato!", changeDeviceError: "Errore sposta Dispositivo!", confirmInitPassMsg1: "Sicuro di voler azzerare:", confirmInitPassMsg2: "password?:",
    initPassSuccess: "Password azzera con successo!", initPassError: "Errore azzeramento Password!", confirmInitUserPassMsg1: "Conferma azzeramento utente?:", page: "Page", records: "Records"
};


var productUpdatePage = { oilCoefficient: "Consumo/100km", updateIcon: "Icona", carNumMsg1: "Targa troppo lunga!", sccuess: "Aggiornamento riuscito!", faild: "Aggiornamento mancato!", isExistMsg: "Targa esistente， riprovare!", filterLBS: "filtro LBS", photo: "Foto" };

var moneyPage = { day: "giorno", msg1: "1 punto per 1 giorno", oneYeah: "un anno", twoYeah: "due anni", lifelong: "durata", msg2: "Estendi tempo maturazione", msg3: "Il dispositivo è stato caricato!", msg4: "punti insufficienti, ricarica!!", msg5: "i giorni devono essere più di 0!", msg6: "impossibile autocaricare!", pointGive: "punti in vendita", pointConsumptionLog: "registrazione punti cliente", loginNameNotExist: "questo nome non esiste", intoLoginID: "trasferire sull'utente", intoUsername: "trasferire sull'utente", intoPoint: "punti da trasferire", detect: "rileva", pleaseDetectLoginName: "verifica nome utente", pleaseInputIntoPoint: "inserire i punti" };

var yiwen201405 = { battery: "Battery Level" };

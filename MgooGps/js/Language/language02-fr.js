function writePage(msg) {
    //法语
    document.write(msg);
}

var allPage = { tab1: "Aperçu du Déplacement", tab2: "Aperçu de l'alarme", tab3: "Statistiques l'appareil", tab4: "Détails de l'alarme", startTime: "De", endTime: "A", search: "Rechercher", num: "No.", deviceName: "Nom de la cible",
    time: "Temps", distance: "Kilométrage (km)", overspeed: "Survitesse", noData: "Pas de données!", lat: "Lat", lng: "Lon", speed: "Vitesse",   speedKM: "km/h", day: "Jour", hour: "Heure",
    minute: "Minutes", pleSel: "S'il vous plaît sélectionnez", date: "Date", plsDeviceMsg: "S'il vous plaît sélectionnez la cible",  moreDevice: "Plus", msg: "Message", myAccount: "Mon compte", changePass: "changer le mot de passe",
    tracking: "Localisation", playback: "Lecture", monitor: "Moniteur", home: "Acceuil", customer: "Client", report: "Statistiques", more: "Plus",  no: "No.", name: "Nom", carNum: "N ° de la Plaque d'Immatriculation",
    imeiNo: "No.IMEI", activeTime: "Temps d'activation", hireExpireTime: "Grâce à", operation: "Opération", edit: "Modifier", divicesInfo: "Informations sur l'appareil", cellName: "Contacts",
    phone: "Tél / Mob", timezone: "Fuseau horaire", save: "Sauvegarder", confirm: "Confirmer", updateUserSuccess: "Modifier avec succès!", updateUserFailed: "Echec de la modification!", modelName: "Genre", state: "Etat",
    drection: "Direction", baidu: "Baidu", google: "Google", day: "Jour", hour: "Heure", minute: "Minute", stopTime: "Temps d'arrêter", desc: "Remarque", cancel: "Annuler", del: "Supprimer", delSuccess: "Supprimer avec succès!",
    delFaild: "Echec de la suppression!", accStr: "Statut du ACC", acc0: "Eteindre", acc1: "Allumer", positionType: "Genre de poste", manDevice: "Gestion de l'appareil", type: "Genre", acc2: "non-connecté", resolve: "Résoudre", startStopTime: "Commencer",
    endStopTime: "Terminer", status1: "Déconnecter", moving: "En mouvement", stopCar: "Arrêter",    primaryEmail: "Email", positionTime: "Temps de positionnement", clear: "Effacer", targetName: "Nom de la cible",
    toExcel: "Export Excel", distance2: "Kilométrage", km: "km", m: "M", event: "Event recording", inTime: "Enter time", outTime: "Departure time", moneyCount: "点数", deviceHireDay: "代充点数", belongTo: "所属用户", 

    deviceNo: "Numéro IMEI",   exit: "Quitter",
    selSizeStr: "Vérifier", sizeStr: "Archives", createTime: "Temps de Création" ,   device: "Cible", purchase: "Acheter", stock: "Inventaire",
    mobileWebsite: "Accès par le mobile",  simNo: "NO. de la carte SIM", expireTime: "Grâce à", remark: "Remarque", deviceInfo: "Informations sur le périphérique", 
    dataError: "Erreur de données !", softFailed: "Erreur !", password: "Mot de passe",   
      searchNull: "IMEI / Nom / NO.Cat  / Nom / Un compte est requis !", initPass: "Réinitialiser le mot de passe", updateTime: "Temps de mise à jour", loginAccount: "Compte d'identification", passLengthMsg: "Mot de passe doit être inférieur à 20 chiffres",   userType1: "Utilisateur",
    userType2: "Distributeur", address: "Adresse", add: "Nouveau", information: "Informations", deviceType: "Genre",  kmHour: "Km / h", devicePhone: "NO. de la Carte SIM", 
    deletes: "Supprimer", cusName: "Nom du client", userInfo: "Informations sur le client", plsSelUser: "S'il vous plaît sélectionnez un utilisateur !",
    plsSel: "Sélectionner", all: "Tous", online: "En ligne", offline: "Hors ligne", arrears: "En retard",  speedLimit: "Limite de la vitesse",  
    allDistance: "Kilométrage total" ,    edit2: "Modifier", deliveryTime: "Temps d'activation",
    cellPhone: "Tél / Mob", type2: "Rôles", view: "View", StatisticalAnalysis: "analyse statistique"
};

//Dealer.aspx
var dealerPage = {
    warnTitle: "Rappel d'alarme", warnSound: "Son de l'alarme activé", username: "Nom/Compte du client", hello: "Bonjour", changePassword: "Changer le mot de passe", searchDevice: "Cible", searchUser: "Client",
    searchDevice2: "VérificationCible", searchUser2: "VérificationClient", name: "Nom du Client", belongTo: "Adhésion", sim: "NO. de la carte SIM", activeTime: "Date d'activation", customerName: "Nom du client", nowSearch: "Recherche en cours ...",
    name2: "Nom du Client", name3: "Nom de la Cible"
};

//Home.aspx
var homePage = {
    quickSearch: "Rechercher", searchTxt: "NO. IMEI/ T /No.de voiture", stage: "Niveau", deviceCount: "Comptage de Cible", operations: "Plus", novice: "Débutant", quickSale: "Vente Rapide",
    batchSale: "Vente par Lots", customer: "Client", sale: "Vente", customerList: "Liste des Clients", addDevice: "Supplémentaire", add: " +nouveau", imeiNullMsg: "S'il vous plaît sélectionnez la cible !", expireTimeNullMsg: "S'il vous plaît sélectionnez la date expirée !",
    saleSuccess: "Succès !"
};
var productUpdatePage = { oilCoefficient: "Carburant/100km", updateIcon: "Icône", carNumMsg1: "NO.de la voiture est trop long !", sccuess: "Mise à jour réussi!", faild: "Mise à jour échoué!", isExistMsg: "NO.de la voiture a existé, svp essayez un autre !", filterLBS: "filter LBS", photo: "Photo" };



var courseName = { dueNorth: "Nord", northeast: "Nord-est", dueEast: "Est", southeast: "Sud-est", dueSouth: "Sud", southwest: "Sud-ouest", dueWest: "Ouest", northwest: "Nord-ouest" };

var reportPage = { title: "Statistiques Complets de Déplacement", warnCount: "Alarme", stopCount: "Rester" };

var alarmSumPage = { title: "Statistiques Complets d'Alarme", lowCount: "Alarme de Batterie faible", cutPowerCount: "Alarme de Coupure", vibCount: "Alarme Vibrante", sosCount: "Alarme SOS" }

var overSpeedPage = { continueTime: "Temps de Continuation", speedlimit: "Limite de la Vitesse", distancePage: "Rapport kilométrique", overspeedDetail: "Détails de Survitesse", stopDetail: "Détails d'Arrêt" };

var alarmIndexPage = { geofenceIn: "Entrée Geo-barrière", geofenceOut: "Sortie Geo-barrière", moved: "Déplacement", lowBattery: "Alarme de batterie faible", sos: "Alarme SOS", cutPower: "Alarme de Coupure", vibration: "Alarme Vibrante",
    overSpeed: "Survitesse", offline: "Offline"
};

var runindexPage = { statistics: "Posée Par", statistics2: "Détails quotidiens", oilCoefficient: "Coefficient de Consommation de Carburant /100 Kilomètres", L: "L", oil: "Consommation de Carburant" };

var alarmDetailPage = { alarmTime: "Temps d'alarme", alarmType: "Genre d'alarme", alarmCount: "Statistiques d'alarme", alarmDetail: "Détails d'alarme" };

var userPage = { warnTitle: "Aperçu de l'alarme", warnSound: "Son d'alarme activé", day7Exp: "7Jours expirés", day60Exp: "60 Jours expirés", alreadyExp: "Expiré",
    username: "Nom / Compte", hello: "Bonjour", searchDevice: "Cible", searchUser: "Client", exit: "Quitter", message: "Message", allDeivce: "Toutes les Cibles", moneyMove: "点数转让", moneyHistory: "点数消费记录"
};

var userInfoPage = { myAccount: "Mon compte", changePassword: "Changer de mot de passe", userMsg: "Astuce : Informations complètes comme numéro de téléphone, contact.", customerName: "Nom du client",
    account: "Compte d'identification", oldPass: "Ancien Mot de passe", newPass: "Nouveau Mot de passe", confirmPass: "Confirmation Mot de passe", passLengthMsg: "Mot de passe doit être inférieur à 20 chiffres", passNull: "La case d'entrée du mot de passe  ne peut pas être vide !",
    passError: "Les deux mots de passe entrés sont différents !", changePassSuccess: "Mot de passe réinitialisé avec succès !", changePassError: "Echec de réinitialisation du Mot de passe!", oldPassError: "Ancien mot de passe erroné!", warnSendMsg: "Mode d'Avertissement Supplémentaire d'Alarme",
    sendEmail: "Email", service: "Les fournisseurs de services"
};

var warnMessagePage = { warnMsg: "Message d'alarme", handle1: "Non-traité", handle2: "Traité", alarmType: "Genre d'alarme", alarmTime: "Temps d'alarme" };

var trackingPage = { secondMsg: "Rafraîchissir après quelques secondes !" };

var playbackPage = { from: "De", to: "A", play: "Lire", pause: "Pause", next: "Continuer", fast: "Rapide", slow: "Ralenti", timeMsg: "Le temps d'arrêt doit être postérieur au temps de début!", nowLoading: "Chargement des données !",
    playOver: "Terminer!", searchNull: "Pas de données!", showLBS: "afficher LBS"
};

var geofencesPage = { geofence: "Geo-barrière", addGeofence: "Ajouter", geoNameNull: "Nom du Geo-barrièrene ne peut pas être vide !", radius: "R (m)", delGeoConfirm: "Confirmer pour suppression", delGeoConfirm2: "Est-ce ce Geo-barrière ?",
    addSuccess: "ajouté avec succès!", addFaild: "Échec de l'ajout!"
};

var iframeMapPage = { baiduMap: "Carte Baidu", googleMap: "Carte Google", deviceName: "Nom de la cible" };

var userUpdatePage = { account: "Compte" };

//map.aspx
var mapPage = { searchInput: "S'il vous plaît entrez le nom/No.IMEI", divicesInfo: "Info de la cible", geofence: "Geo-barrière", cutOffPetrol: "Couper l' Essence", restorePetrol: "Restaurer l'Essence", checkLocation: "Vérifier l'Emplacement", checkCommand: "Vérifier la Commande",
    sendConfirm: "S'il vous plaît confirmezle compte avant d'envoyer la commande", passNull: "S'il vous plaît entrez le mot de passe!", passError: "Erreur de mot de passe!", sendMsg1: "Envoi du message, s'il vous plaît patientez ...", sendSuccess: "Succès ! S'il vous plaît attendez la réponse...",
    sendMsg2: "Commande nulle", sendMsg3: "Cible non-existante", sendMsg4: "S'il vous plaît mettez d'abord l'appareil en ligne", sendMsg5: "Envoi de la commande avec succès", responseSuccess: "Réponse avec Succès!", responseNull: "Réponse Nulle!",
    checkLocatoin: "Vérifier l'Emplacement", checkCommandTitle: "Vérifier la Commande", cmdType: "Genre de Commande", cmdState: "Etat de la Commande", responseText: "Message en Réponse", responseTime: "Temps de Réponse", sendTime: "Temps d'Envoi",
    dyd: "Couper l' Essence", hfyd: "Restaurer l'Essence", deviceResponse: "Réponse de l'appareil", sendSuccess2: "La commande a été envoyée !", noSend: "La commande n'a pas été envoyée", deviceDetailList: "Liste détaillée de l'appareil", addGroup: "Ajouter un groupe", defaultGroup: "Défaut",
    moveToGroup: "Déplacer le Groupe", delGroupConfirm: "Confirmer la Suppression?", downloadLocation: "Télécharger le titre", deviceFortify: "GPS Fortify", deviceDismiss: "GPS Dismiss", accOn: "ACC ON", accOff: "ACC OFF", fortify: "ARM", dismiss: "DimARM", carOpen: "Door Open", carClose: "Door Close", zdlj: "engine connect",
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
    delUserConfirm: "Confirmez pour supprimer", delUserConfirm2: "Est-ce l'utilisateur ?", delUserMsg: "Ce compte est doté de dispositifs. Il ne peut pas être supprimé !", delUserMsg2: "Ce compte dispose de sous-compte. Il ne peut pas être supprimé !", msgFailed: "Impossible de supprimer !",
    updateUserSuccess: "Succès !", updateUserFailed: "Échec !", delDeviceConfirm2: "est-ce la cible ?", addCus: "Ajouter le Client", manCus: "Gérer le Client", delCus: "Supprimer le Client", saleTo: "Déplacer le Client",
    beDevice: "Adhésion", updateExpTime: "Temps de mise à jour expiré", deviceChange: "Déplacer", cusInfo: "Informations sur la clientèle", toCus: "Clientèle cible", newAddChildrenCus: "Ajouter des clients", parentCus: "Client supérieur",
    confirmPass: "Confirmation du Mot de passe", allCus: "Tous les clients", addCusTitle: "Ajouter le Client", loginToUser: "Moniteur", childCus: "Sous-compte", changeDevices: "Déplacer le Groupe", updateExpDevices: "Mise à jour du groupe",
    addUserMsg: "Astuce : Les informations complètes comme numéro de téléphone, contact.", plsParentCusMsg: "S'il vous plaît sélectionner le client de qualité supérieure !", msg1: "Nom, Compte, Mot de passe ne peuvent pas être vide !", passError: "Les deux mots de passe saisis sont différents, s'il vous plaît saisissez à nouveau !",
    existAccount: "Compte avait existé!", dataError: "Données nulles !", dataNull: "Nom, Compte, Mot de passe ne peuvent pas être vide !", imeisNull: "No.IMEI ne peut pas être vide !", saveDevuceMsg1: "Entrer ensemble:", saveDevuceMsg2: "Groupe de No.IMEI,",
    saveDevuceMsg3: "Succès !", saveDevuceMsg4: "Succès :", saveDevuceMsg5: "Groupe", saveDevuceMsg6: "Échec :", saveDevuceMsg7: "Erreur !", updateExpSuccess: "Succès !",
    updateError: "Modifier l'échec!", changeDeviceSuccess: "Succès !", changeDeviceError: "Echec de déplacement de la cible!!", confirmInitPassMsg1: "Etes-vous sûr de réinitialiser :", confirmInitPassMsg2: "Mot de passe?",
    initPassSuccess: "Password réinitialisé avec succès !", initPassError: "Echec de réinitialisation du mot de passe!", confirmInitUserPassMsg1: "Confirmer pour réinitialiser le compte?", page: "Page", records: "Records"
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
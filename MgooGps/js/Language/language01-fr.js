function writePage(msg) {
    //法语
    document.write(msg);
}

var allPage = { deviceNo: "Numéro IMEI", search: "Rechercher", tracking: "Localisation", playback: "Lecture", monitor: "Moniteur", home: "Acceuil", customer: "Client", message: "Message", exit: "Quitter",
    selSizeStr: "Vérifier", sizeStr: "Archives", createTime: "Temps de Création", operation: "Opération", no: "NO.", cellName: "Contacts", phone: "Tél / Mob", device: "Cible", purchase: "Acheter", stock: "Inventaire",
    mobileWebsite: "Accès par le mobile", deviceName: "Nom de la cible", simNo: "NO. de la carte SIM", expireTime: "Grâce à", remark: "Remarque", cancel: "Annuler", deviceInfo: "Informations sur le périphérique", imeiNo: "Numéro IMEI",
    dataError: "Erreur de données !", softFailed: "Erreur !", password: "Mot de passe", confirm: "Sauvegarder", num: "NO.", noData: "Pas de données !", acc0: "Eteindre", acc1: "Allumer",
    acc2: "Non connecté", searchNull: "IMEI / Nom / NO.Cat  / Nom / Un compte est requis !", initPass: "Réinitialiser le mot de passe", updateTime: "Temps de mise à jour", loginAccount: "Compte d'identification", passLengthMsg: "Mot de passe doit être inférieur à 20 chiffres", type: "Genre", userType1: "Utilisateur",
    userType2: "Distributeur", address: "Adresse", add: "Nouveau", information: "Informations", deviceType: "Genre", overspeed: "Survitesse", kmHour: "Km / h", devicePhone: "NO. de la Carte SIM", modelName: "Genre",
    createTime: "Temps de création", activeTime: "Temps d'activation", hireExpireTime: "Temps écoulé", edit: "Modifier", more: "Plus", deletes: "Supprimer", cusName: "Nom du client", userInfo: "Informations sur le client", plsSelUser: "S'il vous plaît sélectionnez un utilisateur !",
    km: "kilomètre", plsSel: "Sélectionner", all: "Tous", online: "En ligne", offline: "Hors ligne", arrears: "En retard", carNum: "NO. de la plaque d'immatriculation", speedLimit: "Limite de la vitesse", lat: "Latitude", lng: "Longitude", speed: "Vitesse", drection: "Direction",
    allDistance: "Kilométrage total", state: "Statut", positionTime: "Poste à temps", status1: "Déconnecté", moving: "En mouvement", stopCar: "Arrêt", accStr: "Statut ACC", edit2: "Modifier", deliveryTime: "Temps d'activation",
    cellPhone: "Tél / Mob", save: "Sauvegarder", clear: "Effacer", type2: "Rôles", view: "View", deviceHireDay: "代充点数"
};

//login.aspx
var loginPage = {
    title: "Station de localisation par GPS", accountTab: " Compte/téléphone/IMEI ", imeiTab: "No.IMEI", account: "Compte", password: "Mot de passe", loginSubmit: "Acceder", languageMsg: "Nous offrons", loginMsg: "S'il vous plaît entrez compte.",
    loginMsg2: "S'il vous plaît, entrez votre mot de passe.", loginMsg3: "L'entrée ne peut pas être vide !", wytx: "I want try", androidBaidu: "android(Baidu map) Download", androidGoogle: "android(Google map) Download", iphone: "iphone jailbreak Download",
    welcome: "Bienvenue, s'il vous plaît connectez-vous!", loginErrorMsg: "Compte ou mot de passe erreur", loginFailure: "échec de la connexion", ForgotPassword: "Mot de passe oubli?", RememberPassword: "se souvenir du mot de passe", Register: "Inscription"
};


//Dealer.aspx
var dealerPage = { warnTitle: "Rappel d'alarme", warnSound: "Son de l'alarme activé", username: "Nom/Compte du client", hello: "Bonjour", changePassword: "Changer le mot de passe", searchDevice: "Cible", searchUser: "Client",
    searchDevice2: "VérificationCible", searchUser2: "VérificationClient", name: "Nom du Client", belongTo: "Adhésion", sim: "NO. de la carte SIM", activeTime: "Date d'activation", customerName: "Nom du client", nowSearch: "Recherche en cours ...",
    name2: "Nom du Client", name3: "Nom de la Cible"
};

//Home.aspx
var homePage = { quickSearch: "Rechercher", searchTxt: "NO. IMEI/ T /No.de voiture", stage: "Niveau", deviceCount: "Comptage de Cible", operations: "Plus", novice: "Débutant", quickSale: "Vente Rapide",
    batchSale: "Vente par Lots", customer: "Client", sale: "Vente", customerList: "Liste des Clients", addDevice: "Supplémentaire", add: " +nouveau", imeiNullMsg: "S'il vous plaît sélectionnez la cible !", expireTimeNullMsg: "S'il vous plaît sélectionnez la date expirée !",
    saleSuccess: "Succès !"
};

var warnMessagePage = { alarmType: "Genre d'Alarme", alarmTime: "Temps d'Alarme" };

var alarmIndexPage = { geofenceIn: "Entrée Geo-barrière", geofenceOut: "Sortie Geo-barrière", moved: "Déplacement", lowBattery: "Alarme de batterie faible", sos: "Alarme SOS", cutPower: "Alarme de Coupure", vibration: "Alarme Vibrante",
    overSpeed: "Survitesse", offline: "Offline"
};

//map.aspx
var mapPage = { searchInput: "S'il vous plaît entrez le nom/No.IMEI", divicesInfo: "Info de la cible", geofence: "Geo-barrière", cutOffPetrol: "Couper l' Essence", restorePetrol: "Restaurer l'Essence", checkLocation: "Vérifier l'Emplacement", checkCommand: "Vérifier la Commande",
    sendConfirm: "S'il vous plaît confirmezle compte avant d'envoyer la commande", passNull: "S'il vous plaît entrez le mot de passe!", passError: "Erreur de mot de passe!", sendMsg1: "Envoi du message, s'il vous plaît patientez ...", sendSuccess: "Succès ! S'il vous plaît attendez la réponse...",
    sendMsg2: "Commande nulle", sendMsg3: "Cible non-existante", sendMsg4: "S'il vous plaît mettez d'abord l'appareil en ligne", sendMsg5: "Envoi de la commande avec succès", responseSuccess: "Réponse avec Succès!", responseNull: "Réponse Nulle!",
    checkLocatoin: "Vérifier l'Emplacement", checkCommandTitle: "Vérifier la Commande", cmdType: "Genre de Commande", cmdState: "Etat de la Commande", responseText: "Message en Réponse", responseTime: "Temps de Réponse", sendTime: "Temps d'Envoi",
    dyd: "Couper l' Essence", hfyd: "Restaurer l'Essence", deviceResponse: "Réponse de l'appareil", sendSuccess2: "La commande a été envoyée !", noSend: "La commande n'a pas été envoyée", deviceDetailList: "Liste détaillée de l'appareil", addGroup: "Ajouter un groupe", defaultGroup: "Défaut",
    moveToGroup: "Déplacer le Groupe", delGroupConfirm: "Confirmer la Suppression?", downloadLocation: "Télécharger le titre", deviceFortify: "GPS Fortify", deviceDismiss: "GPS Dismiss", accOn: "ACC ON", accOff: "ACC OFF", fortify: "ARM", dismiss: "DimARM", carOpen: "Door Open", carClose: "Door Close", zdlj: "engine connect", zddk: "engine cut",
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

var courseName = { dueNorth: "Nord", northeast: "Nord-est", dueEast: "Est", southeast: "Sud-Est", dueSouth: "Sud", southwest: "Sud-ouest", dueWest: "Ouest", northwest: "Nord-ouest" };

//CustomersList.aspx
var cusPage = { delUserConfirm: "Confirmez pour supprimer", delUserConfirm2: "Est-ce l'utilisateur ?", delUserMsg: "Ce compte est doté de dispositifs. Il ne peut pas être supprimé !", delUserMsg2: "Ce compte dispose de sous-compte. Il ne peut pas être supprimé !", msgFailed: "Impossible de supprimer !",
    updateUserSuccess: "Succès !", updateUserFailed: "Échec !", delDeviceConfirm2: "est-ce la cible ?", addCus: "Ajouter le Client", manCus: "Gérer le Client", delCus: "Supprimer le Client", saleTo: "Déplacer le Client",
    beDevice: "Adhésion", updateExpTime: "Temps de mise à jour expiré", deviceChange: "Déplacer", cusInfo: "Informations sur la clientèle", toCus: "Clientèle cible", newAddChildrenCus: "Ajouter des clients", parentCus: "Client supérieur",
    confirmPass: "Confirmation du Mot de passe", allCus: "Tous les clients", addCusTitle: "Ajouter le Client", loginToUser: "Moniteur", childCus: "Sous-compte", changeDevices: "Déplacer le Groupe", updateExpDevices: "Mise à jour du groupe",
    addUserMsg: "Astuce : Les informations complètes comme numéro de téléphone, contact.", plsParentCusMsg: "S'il vous plaît sélectionner le client de qualité supérieure !", msg1: "Nom, Compte, Mot de passe ne peuvent pas être vide !", passError: "Les deux mots de passe saisis sont différents, s'il vous plaît saisissez à nouveau !",
    existAccount: "Compte avait existé!", dataError: "Données nulles !", dataNull: "Nom, Compte, Mot de passe ne peuvent pas être vide !", imeisNull: "No.IMEI ne peut pas être vide !", saveDevuceMsg1: "Entrer ensemble:", saveDevuceMsg2: "Groupe de No.IMEI,",
    saveDevuceMsg3: "Succès !", saveDevuceMsg4: "Succès :", saveDevuceMsg5: "Groupe", saveDevuceMsg6: "Échec :", saveDevuceMsg7: "Erreur !", updateExpSuccess: "Succès !",
    updateError: "Modifier l'échec!", changeDeviceSuccess: "Succès !", changeDeviceError: "Echec de déplacement de la cible!!", confirmInitPassMsg1: "Etes-vous sûr de réinitialiser :", confirmInitPassMsg2: "Mot de passe?",
    initPassSuccess: "Password réinitialisé avec succès !", initPassError: "Echec de réinitialisation du mot de passe!", confirmInitUserPassMsg1: "Confirmer pour réinitialiser le compte?", page: "Page", records: "Records"
};

var productUpdatePage = { oilCoefficient: "Carburant/100km", updateIcon: "Icône", carNumMsg1: "NO.de la voiture est trop long !", sccuess: "Mise à jour réussi!", faild: "Mise à jour échoué!", isExistMsg: "NO.de la voiture a existé, svp essayez un autre !", filterLBS: "filter LBS", photo: "Photo" };




var moneyPage = { day: "day", msg1: "1 point for 1 day", oneYeah: "one year", twoYeah: "two years", lifelong: "lifelong", msg2: "Extend the maturity time", msg3: "the device is charged successfully!", msg4: "the points is not enough, please charge!!", msg5: "days must be greater then 0!", msg6: "can not transfer to oneself!", pointGive: "points for sale", pointConsumptionLog: "points consumer records", loginNameNotExist: "this name does not exist", intoLoginID: "the login account to transfer into", intoUsername: "the username to transfer into", intoPoint: "the points to transfer into", detect: "detect", pleaseDetectLoginName: "please test the login name", pleaseInputIntoPoint: "please input the points" };
//var moneyPage = { day: "天", msg1: "1个点数1天", oneYeah: "一年", twoYeah: "二年", lifelong: "终身", msg2: "增加到期天数", msg3: "设备充值天数成功!", msg4: "点数不够,请充值!", msg5: "天数必须大于0!" };

var yiwen201405 = { battery: "Battery Level" };
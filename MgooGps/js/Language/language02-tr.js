function writePage(msg) {
    //土耳其
    document.write(msg);
}

var allPage = { tab1: "Genel Bakış Hareketli", tab2: "alarm bakış", tab3: "Aygıt İstatistikleri", tab4: "alarm Sayısı", startTime: "itibaren", endTime: "için", search: "ara", num: "Hayır.", deviceName: "hedef Adı",
    time: "zaman", distance: "Kilometre (km)", overspeed: "aşırı hız", noData: "Veri yok!", lat: "enlem", lng: "longtitude", speed: "hızlandırmak",  speedKM: "km / saat", day: "gün", hour: "saat",
    minute: "dakika", pleSel: "Lütfen seçin", date: "tarih", plsDeviceMsg: "Hedef seçin",  moreDevice: "daha fazla", msg: "mesaj", myAccount: "Hesabım", changePass: "şifrenizi değiştirin",
    tracking: "Takip", playback: "playback", monitor: "izlemek", home: "ev", customer: "müşteri", report: "istatistik", more: "daha fazla",   no: "Hayır.", name: "isim", 
    imeiNo: "IMEI No", activeTime: "aktivasyon zamanı", hireExpireTime: "dolayı", operation: "operasyon",  edit: "düzenle", divicesInfo: "cihaz bilgileri", cellName: "Kişiler",
    phone: "Tel / Mob", timezone: "Takım", save: "Kaydet", confirm: "Onay", updateUserSuccess: "Başarılı bir şekilde düzenlendi !", updateUserFailed: "Düzeltilmedi !", modelName: "tip", state: "Durum",
    drection: "Yön", baidu: "Baidu", google: "Google", day: "Gün", hour: "saat", minute: "Dakika", stopTime: "zamanı durdur", desc: "dikkat", cancel: "iptal", del: "Sil", delSuccess: "Silindi !",
    delFaild: "Silinemedi!", accStr: "ACC durumu", acc0: "kapalı", acc1: "Açık", positionType: "Pozisyon Türü", manDevice: "Aygıt yönetimi", type: "tip", acc2: "Bağlanamadı", resolve: "Çöz", startStopTime: "Dur",
    endStopTime: "Başla", status1: "Kapalı Logged", moving: "hareketli", stopCar: "Dur",  primaryEmail: "E-posta", positionTime: "Pozisyon zaman", clear: "Temizle", targetName: "Hedef Adı",
    toExcel: "To Excel", distance2: "Kilometre", km: "km", m: "M", event: "Event recording", inTime: "Enter time", outTime: "Departure time", moneyCount: "点数", deviceHireDay: "代充点数", belongTo: "所属用户", 

    deviceNo: "IMEI Numarası",     message: "Mesaj", exit: "Çıkış",
    selSizeStr: "Kontrol", sizeStr: "Kayıtlar", createTime: "Oluşturulma tarihi",    device: "Hedef", purchase: "Satın alma", stock: "Stok",
    mobileWebsite: "Mobil erişim",   simNo: "SIM Kart YOK.", expireTime: "dolayı", remark: "dikkat",   deviceInfo: "Cihaz Bilgileri",  
    userType12: "dağıtıcı", userType3: "Son kullanıcı", dataError: "Veri Hatası!", softFailed: "Hata!", password: "şifre",   
   searchNull: "IMEI / Ad / Cat NO. / Ad / Hesap gereklidir!", initPass: "Şifre sıfırlama", updateTime: "Güncelleme süresi", loginAccount: "Hesap Girişi", passLengthMsg: "Şifre az 20 basamaklı olmalıdır",  userType1: "Son Kullanıcı",
    userType2: "Dağıtıcı", address: "Adres", add: "Yeni", information: "Bilgi", deviceType: "tip",  kmHour: "Km / h", devicePhone: "SIM Kart YOK.", 
   hireExpireTime2: "Tarih",   deletes: "Sil", cusName: "Müşteri adı", userInfo: "Müşteri Bilgileri", plsSelUser: "Bir kullanıcı seçin!",
    plsSel: "Seçim", all: "Tümü", online: "Çevrimiçi", offline: "Çevrimdışı", carNum: "Plaka No", speedLimit: "Hız limiti" ,
    allDistance: "Toplam kilometre",  edit2: "Düzenle", deliveryTime: "Aktivasyon Süresi",
    cellPhone: "Tel / Mob", type2: "Roller", view: "View", StatisticalAnalysis: "istatistiksel analiz"
};
 
var dealerPage = {
    warnTitle: "Hatırlatma Alarmı", warnSound: "Alarm sesi", username: "Müşteri Adı / Hesap", hello: "Merhaba", changePassword: "Şifrenizi Değiştirin", searchDevice: "Hedef", searchUser: "Müşteri",
    searchDevice2: "Hedef kontrolü", searchUser2: "Müşteri kontrolü", name: "Müşteri Adı", belongTo: "Üyelik", sim: "SIM Kart YOK.", activeTime: "Aktivasyon tarihi", customerName: "Müşteri adı", nowSearch: "Aranıyor ...",
    name2: "Müşteri Adı", name3: "Hedef Adı"
};
  

var homePage = {
    quickSearch: "ara", searchTxt: "IMEI NO. / T / Araba No", stage: "sahne", deviceCount: "hedef Sayısı", operations: "daha fazla", novice: "newbie", quickSale: "Hızlı Satış",
    batchSale: "Toplu Satış", customer: "müşteri", sale: "satış", customerList: "Müşteri Listesi", addDevice: "Aygıt Ekle", add: "Ekle", imeiNullMsg: "Hedef seçin!", expireTimeNullMsg: "Süresi dolan günü seçiniz!",
    saleSuccess: "Başarılı !"
};
var productUpdatePage = { oilCoefficient: "Yakıt Tüketimi 0/100 Kilometre", updateIcon: "Simge seçin", carNumMsg1: "Araç no çok uzun!!", sccuess: "Güncelleme başarılı !", faild: "Güncelleme başarısız!", isExistMsg: "Araç no mevcut değil, başka deneyin !", filterLBS: "filter LBS", photo: "Photo" };


var courseName = { dueNorth: "kuzey", northeast: "kuzeydoğu", dueEast: "Doğu", southeast: "güneydoğu", dueSouth: "güney", southwest: "güneybatı", dueWest: "Batı", northwest: "kuzeybatı" };

var reportPage = { title: "Toplam Hareket İstatistikleri", warnCount: "alarm", stopCount: "kalmak" };

var alarmSumPage = { title: "Toplam Alarm İstatistikleri", lowCount: "Düşük pil alarmı", cutPowerCount: "Cut-off alarmı", vibCount: "Titreşim alarmı", sosCount: "SOS alarmı" }

var overSpeedPage = { continueTime: "Zaman Devam", speedlimit: "hız limiti", distancePage: "Kilometre Raporu", overspeedDetail: "Aşırı Hız Detayları", stopDetail: "Detayları Durdur" };

var alarmIndexPage = { geofenceIn: "Geo-çit olarak", geofenceOut: "Geo-çit Out", moved: "Araba taşınır", lowBattery: "Düşük pil Alarmı", sos: "SOS Alarm", cutPower: "Cut-off Alarm", vibration: "Titreşim Alarmı",
    overSpeed: "Aşırı Hız", offline: "Offline"
};

var runindexPage = { statistics: "Sorgu", statistics2: "Günlük Detaylar", oilCoefficient: "Yakıt Tüketimi Coefficient/100 Kilometre", L: "L", oil: "Yakıt Tüketimi" };

var alarmDetailPage = { alarmTime: "alarm Saati", alarmType: "alarm Tipi", alarmCount: "alarm İstatistikleri", alarmDetail: "alarm Detayları" };

var userPage = { warnTitle: "Alarma bakış", warnSound: "Alarm sesi", day7Exp: "7gün doldu", day60Exp: "60 günlük süre doldu", alreadyExp: "Süre Doldu",
    username: "Adı / Hesap", hello: "Merhaba", searchDevice: "Hedef", searchUser: "Müşteri", exit: "Çıkış", message: "Mesaj", allDeivce: "Bütün Hedef", moneyMove: "点数转让", moneyHistory: "点数消费记录"
};

var userInfoPage = { myAccount: "Hesabım", changePassword: "Şifrenizi Değiştirin", userMsg: "İpucu: Bu telefon numarası gibi komple bilgi, başvurun.", customerName: "Müşteri Adı",
    account: "Hesap Giriş", oldPass: "Yaşadığını Şifre", newPass: "Yeni Şifre", confirmPass: "Şifre Onayı", passLengthMsg: "Şifre az 20 basamaklı olmalıdır", passNull: "Şifre girişi boş olamaz!",
    passError: "Giriş iki şifre farklıdır!", changePassSuccess: "Şifre sıfırlandı !", changePassError: "Şifre sıfırlanmadı !", oldPassError: "Eski şifre hatası!", warnSendMsg: "Ek Alarm Modu",
    sendEmail: "E-posta", service: "My Service"
};

var warnMessagePage = { warnMsg: "alarm mesajı", handle1: "Undealed", handle2: "İşlendi", alarmType: "alarm Tipi", alarmTime: "alarm Saati" };

var trackingPage = { secondMsg: "Saniye sonra yenileyin!!" };

var playbackPage = { from: "itibaren", to: "için", play: "Oynat", pause: "Duraklat", next: "Devam", fast: "hızlı", slow: "yavaş", timeMsg: "Bitiş Zamanı sonra başlama saati daha olmalı!", nowLoading: "Veri yükleme!",
    playOver: "Bitir!", searchNull: "Veri yok!", showLBS: "Show LBS"
};

var geofencesPage = { geofence: "Geo-çit", addGeofence: "eklemek", geoNameNull: "Geo-çit adı boş olamaz!", radius: "R (m)", delGeoConfirm: "Silme işlemini onaylayın", delGeoConfirm2: "Bu Geo-çit mi?" ,
    addSuccess: "Add Success!", addFaild: "Add Failed!"
};

var iframeMapPage = { baiduMap: "Baidu Map", googleMap: "Google Map", deviceName: "Target name" };

var userUpdatePage = { account: "Hesap" };
//map.aspx
var mapPage = { searchInput: 'Lütfen IMEI ve no girin', divicesInfo: "Hedef bilgisi", geofence: "Geo-çit", cutOffPetrol: "Yakıtı Kes", restorePetrol: "Yakıtı Aç", checkLocation: "Bölge Kontrolü", checkCommand: "Komut kontrolü",
    sendConfirm: "Komutu göndermeden önce  onaylayın", passNull: "Lütfen şifrenizi girin !", passError: "Şifre Hatası!", sendMsg1: "Mesajı gönderiliyor, lütfen bekleyin ...", sendSuccess: "Başarılı! Cevap için Lütfen bekleyin ...",
    sendMsg2: "Komut boş", sendMsg3: "Hedeflenemiyor", sendMsg4: "Hedef sunucu bağlantısını kesti.", sendMsg5: "Komut başarı ile gönderildi !", responseSuccess: "Mesaj cevabı", responseNull: "Tepki Yok !",
    checkLocatoin: "Yeri kontrol edin", checkCommandTitle: "Komut kontrol", cmdType: "Komut Türü", cmdState: "Komut Durum", responseText: "mesaj cevabı", responseTime: "Tepki Süresi", sendTime: "Zaman gönder",
    dyd: "Yakıtı kes", hfyd: "Yakıtı Aç", deviceResponse: "cihaz Tepkisi", sendSuccess2: "Komut gönderildi!", noSend: "Komut gönderilmedi", deviceDetailList: "Cihaz Detay Listesi", addGroup: "Grup Ekle", defaultGroup: "varsayılan Grup",
    moveToGroup: "Grubu Taşı", delGroupConfirm: "silme Onayı?", downloadLocation: "Tracking Report", deviceFortify: "GPS Fortify", deviceDismiss: "GPS Dismiss", accOn: "ACC ON", accOff: "ACC OFF", fortify: "ARM", dismiss: "DimARM", carOpen: "Door Open", carClose: "Door Close", zdlj: "engine connect",
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
    delUserConfirm: "Silmek için Onayla", delUserConfirm2: "Bu kullanıcı mı?", delUserMsg: "Bu hesapta cihaz var. Bu silinemez!", delUserMsg2: "Bu hesabın alt hesabı vardır. Silinemez!", msgFailed: "Silinemedi!",
    updateUserSuccess: "Başarılı!", updateUserFailed: "Başarısız!", delDeviceConfirm2: "Bu hedef mi?", addCus: "Müşteri ekle", manCus: "Müşteri Yönet", delCus: "Müşteri Sil", saleTo: "Müşteri Taşı",
    beDevice: "Üyelik", updateExpTime: "Güncelleme Süresi doldu", deviceChange: "Hareket", cusInfo: "Müşteri bilgileri", toCus: "Hedef müşteriler", newAddChildrenCus: "Müşteri Ekle", parentCus: "Üstün Müşteri",
    confirmPass: "Şifre Onayı", allCus: "Tüm Müşteriler", addCusTitle: "Müşteri ekle", loginToUser: "Terminal Girişi", childCus: "Alt hesap", changeDevices: "Toplu taşıma", updateExpDevices: "Toplu güncelleme",
    addUserMsg: "İpucu: Bu telefon numarası gibi komple bilgi, başvurun.", plsParentCusMsg: "Üstün müşteri seçiniz!", msg1: "Ad, Hesap, Şifre boş olamaz!", passError: "Girilen şifreler birbirinden farklı, tekrar deneyin!",
    existAccount: "Hesap oluşturuldu!", dataError: "Veri Boş!", dataNull: "Ad, Hesap, Şifre boş olamaz!", imeisNull: "IMEI No boş olamaz!", saveDevuceMsg1: "Giriş birlikte:", saveDevuceMsg2: "IMEI No Grubu",
    saveDevuceMsg3: "Başarı!", saveDevuceMsg4: "Başarı:", saveDevuceMsg5: "grup", saveDevuceMsg6: "Başarısız:", saveDevuceMsg7: "Hata!", updateExpSuccess: "Başarı!",
    updateError: "düzenleme başarısız!", changeDeviceSuccess: "Başarı!", changeDeviceError: "Hareket yetmezliği hedefleyin!", confirmInitPassMsg1: "Sıfırlamak istediğinizden emin misiniz:", confirmInitPassMsg2: "şifre?:",
    initPassSuccess: "Şifre sıfırlandı!", initPassError: "Şifre sıfırlanmadı!", confirmInitUserPassMsg1: "Hesabı sıfırlamak istiyor musunuz?:", page: "Page", records: "Records"
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
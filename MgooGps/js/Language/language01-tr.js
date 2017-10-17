function writePage(msg) {
    //土耳其
    document.write(msg);
}

var allPage = { deviceNo: "IMEI Numarası", search: "Ara", tracking: "Takip", playback: "Playback", monitor: "İzle", home: "Anasayfa", customer: "Müşteri", message: "Mesaj", exit: "Çıkış",
    selSizeStr: "Kontrol", sizeStr: "Kayıtlar", createTime: "Oluşturulma tarihi", operation: "Operasyon", no: "Hayır.", cellName: "Kişiler", phone: "Tel / Mob", device: "Hedef", purchase: "Satın alma", stock: "Stok",
    mobileWebsite: "Mobil erişim", deviceName: "hedef Adı", simNo: "SIM Kart YOK.", expireTime: "dolayı", remark: "dikkat", cancel: "İptal", deviceInfo: "Cihaz Bilgileri", imeiNo: "IMEI Numarası", userType1: "yönetici",
    userType12: "dağıtıcı", userType3: "Son kullanıcı", dataError: "Veri Hatası!", softFailed: "Hata!", password: "şifre", confirm: "Kaydet", num: "NO.", noData: "Veri yok!", acc0: "Kapalı", acc1: "Açık",
    acc2: "Bağlı değil", searchNull: "IMEI / Ad / Cat NO. / Ad / Hesap gereklidir!", initPass: "Şifre sıfırlama", updateTime: "Güncelleme süresi", loginAccount: "Hesap Girişi", passLengthMsg: "Şifre az 20 basamaklı olmalıdır", type: "Tip", userType1: "Son Kullanıcı",
    userType2: "Dağıtıcı", address: "Adres", add: "Yeni", information: "Bilgi", deviceType: "tip", overspeed: "Aşırı hız", kmHour: "Km / h", devicePhone: "SIM Kart YOK.", modelName: "tip",
    createTime: "Oluşturma tarihi", activeTime: "Aktivasyon zamanı", hireExpireTime: "Zaman aşımı", hireExpireTime2: "Tarih", hireExpireTime2: "Tarih", edit: "Düzenle", more: "Daha fazla", deletes: "Sil", cusName: "Müşteri adı", userInfo: "Müşteri Bilgileri", plsSelUser: "Bir kullanıcı seçin!",
    km: "kilometre", plsSel: "Seçim", all: "Tümü", online: "Çevrimiçi", offline: "Çevrimdışı", carNum: "Plaka No", speedLimit: "Hız limiti", lat: "Enlem", lng: "Boylam", speed: "Hız", drection: "Yön",
    allDistance: "Toplam kilometre", state: "Durum", positionTime: "Pozisyon zamanı", status1: "Log", moving: "Hareketli", stopCar: "Dur", accStr: "ACC Durum", edit2: "Düzenle", deliveryTime: "Aktivasyon Süresi",
    cellPhone: "Tel / Mob", save: "Kaydet", clear: "Temizle", type2: "Roller", view: "View", deviceHireDay: "代充点数"
};

//login.aspx
var loginPage = {
    title: "Piwo GPS izleme istasyonu", accountTab: "Hesap/telefonları/IMEI ", imeiTab: "IMEI No", account: "Kullanıcı girişi", password: "şifre", loginSubmit: "Ok", languageMsg: "Dil", loginMsg: "Hesap giriniz.",
    loginMsg2: "Lütfen şifrenizi girin.", loginMsg3: "Giriş boş olamaz!", wytx: "I want try", androidBaidu: "android(Baidu map) Download", androidGoogle: "android(Google map) Download", iphone: "iphone jailbreak Download",
    welcome: "Hoşgeldiniz, lütfen giriş yapınız!", loginErrorMsg: "Hesap veya şifre hatası", loginFailure: "giriş başarısız oldu", ForgotPassword: "Şifreni mi unuttun？", RememberPassword: "şifre hatırlamak", Register: "Kayıt"
};


//Dealer.aspx
var dealerPage = { warnTitle: "Hatırlatma Alarmı", warnSound: "Alarm sesi", username: "Müşteri Adı / Hesap", hello: "Merhaba", changePassword: "Şifrenizi Değiştirin", searchDevice: "Hedef", searchUser: "Müşteri",
    searchDevice2: "Hedef kontrolü", searchUser2: "Müşteri kontrolü", name: "Müşteri Adı", belongTo: "Üyelik", sim: "SIM Kart YOK.", activeTime: "Aktivasyon tarihi", customerName: "Müşteri adı", nowSearch: "Aranıyor ...",
    name2: "Müşteri Adı", name3: "Hedef Adı"
};

//Home.aspx
var homePage = { quickSearch: "ara", searchTxt: "IMEI NO. / T / Araba No" ,stage:"sahne",deviceCount:"hedef Sayısı",operations:"daha fazla",novice:"newbie",quickSale:"Hızlı Satış",
    batchSale: "Toplu Satış", customer: "müşteri", sale: "satış", customerList: "Müşteri Listesi", addDevice: "Aygıt Ekle", add: "Ekle", imeiNullMsg: "Hedef seçin!", expireTimeNullMsg: "Süresi dolan günü seçiniz!",
    saleSuccess:"Başarılı !"
};

var warnMessagePage = { alarmType: "alarm Tipi", alarmTime: "alarm Saati" };

var alarmIndexPage = { geofenceIn: "Geo-çit olarak", geofenceOut: "Geo-çit Out", moved: "Araç ayrıldı", lowBattery: "Düşük pil Alarm", sos: "SOS Alarm", cutPower: "Cut-off Alarm", vibration: "Titreşim Alarmı",
    overSpeed: "Aşırı hız", offline: "Offline"
};

//map.aspx
var mapPage = { searchInput: 'Lütfen IMEI ve no girin', divicesInfo: "Hedef bilgisi", geofence: "Geo-çit", cutOffPetrol: "Yakıtı Kes", restorePetrol: "Yakıtı Aç", checkLocation: "Bölge Kontrolü", checkCommand: "Komut kontrolü",
    sendConfirm: "Komutu göndermeden önce  onaylayın", passNull: "Lütfen şifrenizi girin !", passError: "Şifre Hatası!", sendMsg1: "Mesajı gönderiliyor, lütfen bekleyin ...", sendSuccess: "Başarılı! Cevap için Lütfen bekleyin ...",
    sendMsg2: "Komut boş", sendMsg3: "Hedeflenemiyor", sendMsg4: "Hedef sunucu bağlantısını kesti.", sendMsg5: "Komut başarı ile gönderildi !", responseSuccess: "Mesaj cevabı", responseNull: "Tepki Yok !",
    checkLocatoin: "Yeri kontrol edin", checkCommandTitle: "Komut kontrol", cmdType: "Komut Türü", cmdState: "Komut Durum", responseText: "mesaj cevabı", responseTime: "Tepki Süresi", sendTime: "Zaman gönder",
    dyd: "Yakıtı kes", hfyd: "Yakıtı Aç", deviceResponse: "cihaz Tepkisi", sendSuccess2: "Komut gönderildi!", noSend: "Komut gönderilmedi", deviceDetailList: "Cihaz Detay Listesi", addGroup: "Grup Ekle", defaultGroup: "varsayılan Grup",
    moveToGroup: "Grubu Taşı", delGroupConfirm: "silme Onayı?", downloadLocation: "Tracking Report", deviceFortify: "GPS Fortify", deviceDismiss: "GPS Dismiss", accOn: "ACC ON", accOff: "ACC OFF", fortify: "ARM", dismiss: "DimARM", carOpen: "Door Open", carClose: "Door Close", zdlj: "engine connect", zddk: "engine cut",
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

var courseName = { dueNorth: "kuzey", northeast: "Kuzey-doğu", dueEast: "Doğu", southeast: "Güney-doğu", dueSouth: "güney", southwest: "güneybatı", dueWest: "Batı", northwest: "Kuzey-batı" };

//CustomersList.aspx
var cusPage = { delUserConfirm: "Silmek için Onayla", delUserConfirm2: "Bu kullanıcı mı?", delUserMsg: "Bu hesapta cihaz var. Bu silinemez!", delUserMsg2: "Bu hesabın alt hesabı vardır. Silinemez!", msgFailed: "Silinemedi!",
    updateUserSuccess: "Başarılı!", updateUserFailed: "Başarısız!", delDeviceConfirm2: "Bu hedef mi?", addCus: "Müşteri ekle", manCus: "Müşteri Yönet", delCus: "Müşteri Sil", saleTo: "Müşteri Taşı",
    beDevice: "Üyelik", updateExpTime: "Güncelleme Süresi doldu", deviceChange: "Hareket", cusInfo: "Müşteri bilgileri", toCus: "Hedef müşteriler", newAddChildrenCus: "Müşteri Ekle", parentCus: "Üstün Müşteri",
    confirmPass: "Şifre Onayı", allCus: "Tüm Müşteriler", addCusTitle: "Müşteri ekle", loginToUser: "Terminal Girişi", childCus: "Alt hesap", changeDevices: "Toplu taşıma", updateExpDevices: "Toplu güncelleme",
    addUserMsg: "İpucu: Bu telefon numarası gibi komple bilgi, başvurun.", plsParentCusMsg: "Üstün müşteri seçiniz!", msg1: "Ad, Hesap, Şifre boş olamaz!", passError: "Girilen şifreler birbirinden farklı, tekrar deneyin!",
    existAccount: "Hesap oluşturuldu!", dataError: "Veri Boş!", dataNull: "Ad, Hesap, Şifre boş olamaz!", imeisNull: "IMEI No boş olamaz!", saveDevuceMsg1: "Giriş birlikte:", saveDevuceMsg2: "IMEI No Grubu",
    saveDevuceMsg3: "Başarı!", saveDevuceMsg4: "Başarı:", saveDevuceMsg5: "grup", saveDevuceMsg6: "Başarısız:", saveDevuceMsg7: "Hata!", updateExpSuccess: "Başarı!",
    updateError: "düzenleme başarısız!", changeDeviceSuccess: "Başarı!", changeDeviceError: "Hareket yetmezliği hedefleyin!", confirmInitPassMsg1: "Sıfırlamak istediğinizden emin misiniz:", confirmInitPassMsg2: "şifre?:",
    initPassSuccess: "Şifre sıfırlandı!", initPassError: "Şifre sıfırlanmadı!", confirmInitUserPassMsg1: "Hesabı sıfırlamak istiyor musunuz?:", page: "Page", records: "Records"
};

var productUpdatePage = { oilCoefficient: "Yakıt Tüketimi 0/100 Kilometre", updateIcon: "Simge seçin", carNumMsg1: "Araç no çok uzun!!", sccuess: "Güncelleme başarılı !", faild: "Güncelleme başarısız!", isExistMsg: "Araç no mevcut değil, başka deneyin !", filterLBS: "filter LBS", photo: "Photo" };



var moneyPage = { day: "day", msg1: "1 point for 1 day", oneYeah: "one year", twoYeah: "two years", lifelong: "lifelong", msg2: "Extend the maturity time", msg3: "the device is charged successfully!", msg4: "the points is not enough, please charge!!", msg5: "days must be greater then 0!", msg6: "can not transfer to oneself!", pointGive: "points for sale", pointConsumptionLog: "points consumer records", loginNameNotExist: "this name does not exist", intoLoginID: "the login account to transfer into", intoUsername: "the username to transfer into", intoPoint: "the points to transfer into", detect: "detect", pleaseDetectLoginName: "please test the login name", pleaseInputIntoPoint: "please input the points" };
//var moneyPage = { day: "天", msg1: "1个点数1天", oneYeah: "一年", twoYeah: "二年", lifelong: "终身", msg2: "增加到期天数", msg3: "设备充值天数成功!", msg4: "点数不够,请充值!", msg5: "天数必须大于0!" };

var yiwen201405 = { battery: "Battery Level" };
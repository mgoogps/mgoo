function writePage(msg) {
    //沙特阿拉伯
    document.write(msg);
}

var allPage = { deviceNo: "رقم الـIMEI", search: "بحث", tracking: "تتبع", playback: "إعادة التشغيل", monitor: "عرض", home: "لصفحة الرئيسية", customer: "العميل", message: "الرسائل", exit: "خروج",
    selSizeStr: "فحص", sizeStr: "السجلات", createTime: "وقت الإنشاء", operation: "العملية", no: "رقم ", cellName: "جهات الاتصال", phone: "الهاتف/الموبايل", device: "الجهاز", purchase: "شراء", stock: "المخزن",
    mobileWebsite: "الدخول عبرالموبايل", deviceName: "اسم الجهاز", simNo: "رقم الشريحة", expireTime: "موعد الاستحقاق", remark: "ملاحظة", cancel: "إلغاء", deviceInfo: "معلومات الجهاز", imeiNo: "رقم الـIMEI",
    dataError: "خطأ في البيانات", softFailed: "خطأ", password: "كلمة المرور", confirm: "حفظ", num: "رقم", noData: "لاتوجد بيانات", acc0: "قفل", acc1: "فتح",
    acc2: "غير متصل", searchNull: "مطلوب تعيين: IMEI/الاسم/ رقم الفئة/ اسم الحساب", initPass: "إعادة تعيين كلمة المرور", updateTime: "تاريخ التحديث", loginAccount: "دخول الحساب", passLengthMsg: "كلمة المرور يجب ان لا  تكون أكثر من 20 خانة", type: "النوع", userType1: "المستخدم النهائي",
    userType2: "الموزع", address: "العنوان", add: "جديد", information: "معلومات", deviceType: "نوع الجهاز", overspeed: "السرعة الزائدة", kmHour: "كيلومتر في الساعة", devicePhone: "رقم شريحة الهاتف", modelName: "النوع",
    createTime: "وقت الإنشاء", activeTime: "وقت التفعيل", hireExpireTime: "وقت الإنتهاء", edit: "تعديل", more: "المزيد", deletes: "حذف", cusName: "اسم العميل", userInfo: "معلومات العميل", plsSelUser: "رجاء إختر عميل",
    km: "كيلومتر ", plsSel: "إختيار", all: "الكل", online: "متصل", offline: "غير متصل", arrears: "المستحقات", carNum: "رقم لوحة الترخيص", speedLimit: "حدود السرعة", lat: "خط العرض", lng: "خط الطول", speed: "السرعة", drection: "الإتجاه",
    allDistance: "إجمالي المسافة", state: "الحالة", positionTime: "وقت تحديد الموقع", status1: "تسجيل الخروج", moving: "متحرك", stopCar: "توقف", accStr: "حالة الحساب", edit2: "تعديل", deliveryTime: "وقت التفعيل",
    cellPhone: "الموبايل", save: "حفظ", clear: "مسح", type2: "النوع", view: "نظرة", noJur: "غير مصرح له", deviceHireDay: "代充点数"
};
//login.aspx
var loginPage = {
    title: "محطة تتبع الـ GPS", accountTab: " الحساب/هاتف/IMEI ", imeiTab: "رقم الـIMEI", account: "الحساب", password: "كلمة المرور", loginSubmit: "اذهب", languageMsg: "متاح عبر", loginMsg: "يرجى إدخال حساب",
    loginMsg2: "يرجى إدخال كلمة المرور", loginMsg3: "لا يمكن ان يكون المحتوى فارغ", wytx: "I want try", androidBaidu: "android(Baidu map) Download", androidGoogle: "android(Google map) Download", iphone: "iphone jailbreak Download",
    welcome: "ترحيب، يرجى تسجيل الدخول", loginErrorMsg: "الحساب أو كلمة المرور خطأ", loginFailure: "فشل تسجيل الدخول", ForgotPassword: "هل نسيت كلمة المرور؟", RememberPassword: "تذكر اسم المستخدم وكلمة المرور", Register: "تسجيل"
};


//Dealer.aspx
var dealerPage = { warnTitle: "التذكير بالمنبه", warnSound: "صوت المنبه", username: "اسم العميل/الحساب", hello: "مرحبا", changePassword: "تغيير كلمة المرور", searchDevice: "الجهاز", searchUser: "العميل",
    searchDevice2: "فحص الجهاز", searchUser2: "فحص العميل", name: "اسم العميل", belongTo: "الاشتراك", sim: "رقم شريحة الموبايل", activeTime: "تاريخ التنشيط", customerName: "اسم العميل", nowSearch: "جاري البحث",
    name2: "اسم العميل", name3: "اسم الجهاز"
};

//Home.aspx
var homePage = { quickSearch: "البحث", searchTxt: "حقول البحث عن الـIMEI /الهدف/رقم المركبة" ,stage:"المرحلة",deviceCount:"عدد الأجهزة",operations:"المزيد",novice:"الفاتورة",quickSale:"مبيع سريع",
    batchSale: "مبيع مجزاء", customer: "العميل", sale: "مبيع", customerList: "قائمة العملاء", addDevice: "اضافة", add: "جديد", imeiNullMsg: "الرجاء اختيار الهدف", expireTimeNullMsg: "الرجاء اختيار تاريخ نهاية الصلاحية",
    saleSuccess:"تم بنجاح"
};

var warnMessagePage = { alarmType: "نوع التنبيه", alarmTime: "وقت التنبيه" };

var alarmIndexPage = { geofenceIn: "داخل المنطقة الجغرافية", geofenceOut: "خارج المنطقة الجغرافية", moved: "تمركز غير صحيح", lowBattery: "تنبيه شحن البطارية منخفض", sos: "تنبيه الاستغاثة", cutPower: "تنبيه الانتهاء", vibration: "تنبيه الاهتزاز",
    overSpeed: "تخطي حدود السرعة", offline: "تنبه الاطفاء"
};

//map.aspx
var mapPage = { searchInput: 'الرجاء إدخال الاسم / رقم الـ IMEI', divicesInfo: "معلومات الجهاز", geofence: "المنظقة الجغرافية", cutOffPetrol: "تعظيل المحرك", restorePetrol: "إعادة تشغيل المحرك", checkLocation: "التحقق من الموقع", checkCommand: "التحقق من الأمر",
    sendConfirm: "الرجاء تأكيد الحساب قبل إرسال الأمر", passNull: "الرجاء إدخال كلمة المرور", passError: "كلمة المرور خطأ", sendMsg1: "جاري إرسال الرسالة, الرجاء الانتظار ...", sendSuccess: "تمت بنجاح! الرجاء الانتظار للاستجابة ...",
    sendMsg2: "الأمر غير معرف", sendMsg3: "الجهاز غير موجود", sendMsg4: "الرجاء تشغيل الجهاز أولا", sendMsg5: "تم إرسال الأمر بنجاح", responseSuccess: "تمت الاستجابة بنجاح", responseNull: "الاستجابة غير معرفة",
    checkLocatoin: "التحقق من الموقع", checkCommandTitle: "التحقق من الأمر", cmdType: "نوع الأمر", cmdState: "حالة الأمر", responseText: "رسالة الاستجابة", responseTime: "وقت الاستجابة", sendTime: "وقت الإرسال",
    dyd: "تعظيل المحرك", hfyd: "إعادة تشغيل المحرك", deviceResponse: "استجابة الجهاز", sendSuccess2: "تم إرسال الأمر!", noSend: "لم يتم إرسال الأمر", deviceDetailList: "قائمة تفاصيل الجهاز", addGroup: "إضافة مجموعة", defaultGroup: "الأساسي",
    moveToGroup: "تحويل مجموعة", delGroupConfirm: "حذف التاكيد؟", downloadLocation: "تحميل المسار", deviceFortify: "GPS Fortify", deviceDismiss: "GPS Dismiss", accOn: "ACC ON", accOff: "ACC OFF", fortify: "ARM", dismiss: "DimARM", carOpen: "Door Open", carClose: "Door Close", zdlj: "engine connect", zddk: "engine cut",
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

var courseName = { dueNorth: "الشمال", northeast: "الشمال الشرقي", dueEast: "الشرق", southeast: "الجنوب الشرقي", dueSouth: "الجنوب", southwest: "الجنوب الغربي", dueWest: "الغرب", northwest: "الشمال الغربي" };

//CustomersList.aspx
var cusPage = { delUserConfirm: "تأكيد الإلغاء", delUserConfirm2: "تأكيد المستخدم؟", delUserMsg: "لا يمكن إالغاء الحساب, يوجد أجهزة", delUserMsg2: "لا يمكن إلغاء الحساب, يوجد حساب فرعي", msgFailed: "فشل الإلغاء",
    updateUserSuccess: "نجاح!", updateUserFailed: "فشل!", delDeviceConfirm2: "تأكيد الجهاز؟", addCus: "إضافة عميل", manCus: "إدارة العملاء", delCus: "إلغاء عميل", saleTo: "تحويل عميل",
    beDevice: "اشتراك", updateExpTime: "تحديث وقت انتهاء الصلاحية", deviceChange: "تحويل", cusInfo: "معلومات العميل", toCus: "العملاء ", newAddChildrenCus: "إضافة عملاء", parentCus: "عميل مهم",
    confirmPass: "تأكيد كلمة المرور", allCus: "جميع العملاء", addCusTitle: "إضافة عميل", loginToUser: "عرض", childCus: "حساب فرعي", changeDevices: "تغيير أجهزة", updateExpDevices: "تحديث جهاز",
    addUserMsg: "مساعدة: الرجاء إكمال المعلومات كرقم الموبايل, جهات الاتصال", plsParentCusMsg: "الرجاء اختيار عميل مهم!", msg1: "لا يمكن ترك الاسم, الحساب وكلمة المرور فارغة!", passError: "كلمة المرور غير مطابقة, الرجاء إعادة الإدخال",
    existAccount: "الحساب منتهي الصلاحية", dataError: "البيانات غير موجودة", dataNull: "لا يمكن ترك الاسم, الحساب وكلمة المرور فارغة!", imeisNull: "لا يمكن ترك رقم الـ IMEI فارغ", saveDevuceMsg1: "ادخال مشترك:", saveDevuceMsg2: "مجموعة من أرقام الـ IMEI,",
    saveDevuceMsg3: "نجاح!", saveDevuceMsg4: "نجاح:", saveDevuceMsg5: "مجموعة", saveDevuceMsg6: "فشل:", saveDevuceMsg7: "خطاء!", updateExpSuccess: "نجاح!",
    updateError: "فشل التعديل!", changeDeviceSuccess: "نجاح!", changeDeviceError: "فشل تحويل الهدف!", confirmInitPassMsg1: "هل انت متاكد من عمل اعادة الضبط؟", confirmInitPassMsg2: "كلمة المرور؟",
    initPassSuccess: "تمت إعادة ضبط كلمة المرور بنجاح!", initPassError: "فشل إعادة ضبط كلمة المرور!", confirmInitUserPassMsg1: "تاكيد اعادة ضبط الحساب؟", page: "Page", records: "Records"
};

var productUpdatePage = { oilCoefficient: "الوقود/100 كلم", updateIcon: "الرمز", carNumMsg1: "رقم المركبة طويل جدا!", sccuess: "تم التحديث بنجاح!", faild: "فشل التحديث!", isExistMsg: "رقم المركبة موجود, يرجى ادخال رقم آخر!", filterLBS: "filter LBS", photo: "Photo" };

//var moneyPage = { day: "天", msg1: "1个点数1天", oneYeah: "一年", twoYeah: "二年", lifelong: "终身", msg2: "增加到期天数", msg3: "设备充值天数成功!", msg4: "点数不够,请充值!", msg5: "天数必须大于0!" };

var moneyPage = { day: "day", msg1: "1 point for 1 day", oneYeah: "one year", twoYeah: "two years", lifelong: "lifelong", msg2: "Extend the maturity time", msg3: "the device is charged successfully!", msg4: "the points is not enough, please charge!!", msg5: "days must be greater then 0!", msg6: "can not transfer to oneself!", pointGive: "points for sale", pointConsumptionLog: "points consumer records", loginNameNotExist: "this name does not exist", intoLoginID: "the login account to transfer into", intoUsername: "the username to transfer into", intoPoint: "the points to transfer into", detect: "detect", pleaseDetectLoginName: "please test the login name", pleaseInputIntoPoint: "please input the points" };


var yiwen201405 = { battery: "Battery Level" };

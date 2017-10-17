function writePage(msg) {
    //沙特阿拉伯
    document.write(msg);
}

var allPage = { tab1: "نظرة عامة عن الحركة", tab2: "نظرة عامة عن التنبيه", tab3: "إحصائيات الجهاز", tab4: "تفاصيل التنبيه", startTime: "من", endTime: "إلى", search: "بحث", num: "رقم", deviceName: "اسم الجهاز",
    time: "الوقت", distance: "ميل (كم)", overspeed: "سرعة زائدة", noData: "لا توجد بيانات", lat: "خطوط الطول", lng: "خطوط العرض", speed: "السرعة",   speedKM: "كم/الساعة", day: "اليوم", hour: "الساعة",
    minute: "الدقائق", pleSel: "الرجاء الاختيار", date: "التاريخ", plsDeviceMsg: "الرجاء تحديد الجهاز",  moreDevice: "المزيد", msg: "رسالة", myAccount: "حسابي", changePass: "تغيير كلمة المرور",
    tracking: "متابعة", playback: "إعادة", monitor: "عرض", home: "الرئيسية", customer: "العميل", report: "إحصائيات", more: "المزيد",  no: "رقم", name: "الاسم", carNum: "رقم اللوحة",
    imeiNo: "رقم الـ IMEI", activeTime: " بداية تنشيط الخدمة", hireExpireTime: "وقت انتهاء الصلاحية", operation: "العملية",  edit: "تعديل", divicesInfo: "معلومات الجهاز", cellName: "جهات الاتصال",
    phone: "تلفون/ موبايل", timezone: "التوقيت", save: "حفظ", confirm: "تأكيد", updateUserSuccess: "تم التعديل بنجاح", updateUserFailed: "فشل التعديل", modelName: "النوع", state: "الحالة",
    drection: "الاتجاه", baidu: "موقع بايدو", google: "موقع جوجل", day: "اليوم", hour: "الساعة", minute: "الدقيقة", stopTime: "وقت الوقوف", desc: "ملاحظة", cancel: "إلغاء", del: "حذف ", delSuccess: "تم الحذف بنجاح",
    delFaild: "فشل الحذف", accStr: "حالة الحساب", acc0: "إغلاق", acc1: "تشغيل", positionType: "نوع الوضعية", manDevice: "إدارة  الأجهزة", type: "نوع", acc2: "غير متصل", resolve: "معالجة", startStopTime: "بداية",
    endStopTime: "نهاية",   moving: "متحرك", stopCar: "توقف المركبة",  arrears: "استحقاقات", primaryEmail: "البريد الالكتروني", positionTime: "وقت تحديد الموقع", clear: "مسح", targetName: "اسم الجهاز",
    toExcel: "تصدير الي اكسل", distance2: "وجهة", km: "كلم", m: "متر", event: "حدث", inTime: "وقت الدخول", outTime: "وقت الخروج", noJur: "غير مصرح له", moneyCount: "点数", deviceHireDay: "代充点数", belongTo: "所属用户", 

    deviceNo: "رقم الـIMEI", search1: "بحث",  message: "الرسائل", exit: "خروج",
    selSizeStr: "فحص", sizeStr: "السجلات",   device: "الجهاز", purchase: "شراء", stock: "المخزن",
    mobileWebsite: "الدخول عبرالموبايل",  simNo: "رقم الشريحة", expireTime: "موعد الاستحقاق", remark: "ملاحظة",  deviceInfo: "معلومات الجهاز",  
    dataError: "خطأ في البيانات", softFailed: "خطأ", password: "كلمة المرور",  
    searchNull: "مطلوب تعيين: IMEI/الاسم/ رقم الفئة/ اسم الحساب", initPass: "إعادة تعيين كلمة المرور", updateTime: "تاريخ التحديث", loginAccount: "دخول الحساب", passLengthMsg: "كلمة المرور يجب ان لا  تكون أكثر من 20 خانة",   userType1: "المستخدم النهائي",
    userType2: "الموزع", address: "العنوان", add: "جديد", information: "معلومات", deviceType: "نوع الجهاز",  kmHour: "كيلومتر في الساعة", devicePhone: "رقم شريحة الهاتف", 
    createTime: "وقت الإنشاء",    deletes: "حذف", cusName: "اسم العميل", userInfo: "معلومات العميل", plsSelUser: "رجاء إختر عميل",
      plsSel: "إختيار", all: "الكل", online: "متصل", offline: "غير متصل",  speedLimit: "حدود السرعة",  
    allDistance: "إجمالي المسافة",    status1: "تسجيل الخروج",    edit2: "تعديل", deliveryTime: "وقت التفعيل",
    cellPhone: "الموبايل", type2: "النوع", view: "نظرة", StatisticalAnalysis: "التحليل الاحصائي"
};

//Dealer.aspx
var dealerPage = {
    warnTitle: "التذكير بالمنبه", warnSound: "صوت المنبه", username: "اسم العميل/الحساب", hello: "مرحبا", changePassword: "تغيير كلمة المرور", searchDevice: "الجهاز", searchUser: "العميل",
    searchDevice2: "فحص الجهاز", searchUser2: "فحص العميل", name: "اسم العميل", belongTo: "الاشتراك", sim: "رقم شريحة الموبايل", activeTime: "تاريخ التنشيط", customerName: "اسم العميل", nowSearch: "جاري البحث",
    name2: "اسم العميل", name3: "اسم الجهاز"
};
//Home.aspx
var homePage = {
    quickSearch: "البحث", searchTxt: "حقول البحث عن الـIMEI /الهدف/رقم المركبة", stage: "المرحلة", deviceCount: "عدد الأجهزة", operations: "المزيد", novice: "الفاتورة", quickSale: "مبيع سريع",
    batchSale: "مبيع مجزاء", customer: "العميل", sale: "مبيع", customerList: "قائمة العملاء", addDevice: "اضافة", add: "جديد", imeiNullMsg: "الرجاء اختيار الهدف", expireTimeNullMsg: "الرجاء اختيار تاريخ نهاية الصلاحية",
    saleSuccess: "تم بنجاح"
};

var courseName = { dueNorth: "الشمال", northeast: "لشمال الشرقي", dueEast: "الشرق", southeast: "الجنوب الشرقي", dueSouth: "الجنوب", southwest: "الجنوب الغربي", dueWest: "الغرب", northwest: "الشمال الغربي" };

var reportPage = { title: "إحصائيات إجمالي التحركات", warnCount: "تعداد التنبيه", stopCount: "تعداد التوقف" };

var alarmSumPage = { title: "إحصائيات إجمالي التنبيه", lowCount: "تنبيه انخفاض شحن البطارية", cutPowerCount: "تنبيه تعطيل المحرك", vibCount: "تنبيه اهتزاز", sosCount: "تنبيه استغاثة" }

var overSpeedPage = { continueTime: "وقت الاستمرار", speedlimit: "حدود السرعة", distancePage: "تقرير الأميال", overspeedDetail: "تفاصيل السرعة الزائدة", stopDetail: "تفاصيل التوقف" };

var alarmIndexPage = { geofenceIn: "داخل الحدود الجغرافية", geofenceOut: "خارج الحدود الجغرافية", moved: "الإزاحة", lowBattery: "تنبيه شحن البطارية منخفض", sos: "تنبيه استغاثة", cutPower: "تنبيه تعطيل المحرك", vibration: "تنبيه الاهتزاز",
    overSpeed: "السرعة الزائدة", offline: "تنبه الاطفاء"
};

var runindexPage = { statistics: "الطلب بواسطة", statistics2: "التفاصيل اليومية", oilCoefficient: "معدل استهلاك الوقود /100 كم", L: "لتر", oil: "استهلاك الوقود" };

var alarmDetailPage = { alarmTime: "وقت التنبيه", alarmType: "نوع التنبيه", alarmCount: "إحصائيات التنبيه", alarmDetail: "تفاصيل التنبيه" };

var userPage = { warnTitle: "عنوان التنبيه", warnSound: "التنبيه يعمل", day7Exp: "صلاحية منتهية لسبعة أيام", day60Exp: "صلاحية منتهية لستين يوم", alreadyExp: "انتهت الصلاحية",
    username: "الاسم/ الحساب", hello: "مرحبا", searchDevice: "بحث عن جهاز", searchUser: "بحث عن عميل", exit: "خروج", message: "رسالة", allDeivce: "كل الأجهزة", moneyMove: "点数转让", moneyHistory: "点数消费记录"
};

var userInfoPage = { myAccount: "حسابي", changePassword: "تغيير كلمة المرور", userMsg: "مساعدة: الرجاء إكمال المعلومات كرقم الموبايل, جهة الاتصال", customerName: "اسم العميل",
    account: "حساب الدخول", oldPass: "كلمة المرور الحالية", newPass: "كلمة مرور جديدة", confirmPass: "تأكيد كلمة المرور", passLengthMsg: "يجب أن تكون كلمة المرور أقل من 20 حرف", passNull: "لا يجب أن تكون كلمة المرور فارغة!",
    passError: "كلمة المرور غير مطابقة!", changePassSuccess: "تم إعادة ضبط كلمة المرور بنجاح!", changePassError: "فشل إعادة ضبط كلمة المرور!", oldPassError: "خطأ في كلمة المرور السابقة!", warnSendMsg: "وضع التنبيه الإضافي",
    sendEmail: "البريد الالكتروني", service: "خدمات"
};

var warnMessagePage = { warnMsg: "رسالة تنبيه", handle1: "لم تتم الإستجابة", handle2: "تمت الإستجابة", alarmType: "نوع التنبيه", alarmTime: "وقت التنبيه" };

var trackingPage = { secondMsg: "الرجاء التحديث بعد قليل!" };

var playbackPage = { from: "من", to: "إلى", play: "تشغيل", pause: "توقف", next: "استمرار", fast: "سريع", slow: "بطئ", timeMsg: "يجب أن يكون وقت النهاية تابع لوقت البداية", nowLoading: "تحميل البيانات!",
    playOver: "النهاية!", searchNull: "لا توجد بيانات!", showLBS: "اظهار الموقع"
};

var geofencesPage = { geofence: "المنطقة الجغرافية", addGeofence: "إضافة", geoNameNull: "لا يمكن أن يكون اسم المنطقة الجغرافية فارغ", radius: "نصف القطر (م)", delGeoConfirm: "تأكيد الحذف", delGeoConfirm2: "تأكيد المنطقة الجغرافية" ,
    addSuccess: "تمت الإضافة بنجاح", addFaild: "فشل  الإضافة"
};

var iframeMapPage = { baiduMap: "خرائط موقع بايدو", googleMap: "خرائط موقع جوجل", deviceName: "اسم الجهاز" };

var userUpdatePage = { account: "الحساب" };

//map.aspx
var mapPage = { searchInput: 'الرجاء إدخال الاسم / رقم الـ IMEI', divicesInfo: "معلومات الجهاز", geofence: "المنظقة الجغرافية", cutOffPetrol: "تعظيل المحرك", restorePetrol: "إعادة تشغيل المحرك", checkLocation: "التحقق من الموقع", checkCommand: "التحقق من الأمر",
    sendConfirm: "الرجاء تأكيد الحساب قبل إرسال الأمر", passNull: "الرجاء إدخال كلمة المرور", passError: "كلمة المرور خطأ", sendMsg1: "جاري إرسال الرسالة, الرجاء الانتظار ...", sendSuccess: "تمت بنجاح! الرجاء الانتظار للاستجابة ...",
    sendMsg2: "الأمر غير معرف", sendMsg3: "الجهاز غير موجود", sendMsg4: "الرجاء تشغيل الجهاز أولا", sendMsg5: "تم إرسال الأمر بنجاح", responseSuccess: "تمت الاستجابة بنجاح", responseNull: "الاستجابة غير معرفة",
    checkLocatoin: "التحقق من الموقع", checkCommandTitle: "التحقق من الأمر", cmdType: "نوع الأمر", cmdState: "حالة الأمر", responseText: "رسالة الاستجابة", responseTime: "وقت الاستجابة", sendTime: "وقت الإرسال",
    dyd: "تعظيل المحرك", hfyd: "إعادة تشغيل المحرك", deviceResponse: "استجابة الجهاز", sendSuccess2: "تم إرسال الأمر!", noSend: "لم يتم إرسال الأمر", deviceDetailList: "قائمة تفاصيل الجهاز", addGroup: "إضافة مجموعة", defaultGroup: "الأساسي",
    moveToGroup: "تحويل مجموعة", delGroupConfirm: "حذف التاكيد؟", downloadLocation: "تحميل المسار", deviceFortify: "GPS Fortify", deviceDismiss: "GPS Dismiss", accOn: "ACC ON", accOff: "ACC OFF", fortify: "ARM", dismiss: "DimARM", carOpen: "Door Open", carClose: "Door Close", zdlj: "engine connect",
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


var downloadLocation = { download: "تحميل", help: "مساعدة", step: "خطوة", step1: "1.الرجاء اختيار الوحدة للتحميل", step2: "2.الرجاء اختيار التاريخ للتحميل", step3: "3.الرجاء ضغط زر “تحميل موقع ملف KML”ضغط على زر ما",
    msg1: "ملاحظة: إذا عرض ليست هناك معلومات صحيحة من الطريق!”يشير ليست هناك معلومات من تريد في الوقت الحاضر", msg2: "صيغة ملف التحميل هي بملف google بامتداد document.kml",
    msg3: "الضغط مرتين علة ملف KML يمكن من عرضه بواسطة برنامج  google earth", msg4: "مسار حركة الجهاز سيظهر باللون الاحمر", msg5: "تنزيل تطبيق  Google earth", here: "هنا",
    msg6: "يمكن  تنزيل مسار حركة جهاز خلال فترة زمنية محددة بواسطة زر download data of path"
};

var cusPage = { 
    delUserConfirm: "تأكيد الإلغاء", delUserConfirm2: "تأكيد المستخدم؟", delUserMsg: "لا يمكن إالغاء الحساب, يوجد أجهزة", delUserMsg2: "لا يمكن إلغاء الحساب, يوجد حساب فرعي", msgFailed: "فشل الإلغاء",
    updateUserSuccess: "نجاح!", updateUserFailed: "فشل!", delDeviceConfirm2: "تأكيد الجهاز؟", addCus: "إضافة عميل", manCus: "إدارة العملاء", delCus: "إلغاء عميل", saleTo: "تحويل عميل",
    beDevice: "اشتراك", updateExpTime: "تحديث وقت انتهاء الصلاحية", deviceChange: "تحويل", cusInfo: "معلومات العميل", toCus: "العملاء ", newAddChildrenCus: "إضافة عملاء", parentCus: "عميل مهم",
    confirmPass: "تأكيد كلمة المرور", allCus: "جميع العملاء", addCusTitle: "إضافة عميل", loginToUser: "عرض", childCus: "حساب فرعي", changeDevices: "تغيير أجهزة", updateExpDevices: "تحديث جهاز",
    addUserMsg: "مساعدة: الرجاء إكمال المعلومات كرقم الموبايل, جهات الاتصال", plsParentCusMsg: "الرجاء اختيار عميل مهم!", msg1: "لا يمكن ترك الاسم, الحساب وكلمة المرور فارغة!", passError: "كلمة المرور غير مطابقة, الرجاء إعادة الإدخال",
    existAccount: "الحساب منتهي الصلاحية", dataError: "البيانات غير موجودة", dataNull: "لا يمكن ترك الاسم, الحساب وكلمة المرور فارغة!", imeisNull: "لا يمكن ترك رقم الـ IMEI فارغ", saveDevuceMsg1: "ادخال مشترك:", saveDevuceMsg2: "مجموعة من أرقام الـ IMEI,",
    saveDevuceMsg3: "نجاح!", saveDevuceMsg4: "نجاح:", saveDevuceMsg5: "مجموعة", saveDevuceMsg6: "فشل:", saveDevuceMsg7: "خطاء!", updateExpSuccess: "نجاح!",
    updateError: "فشل التعديل!", changeDeviceSuccess: "نجاح!", changeDeviceError: "فشل تحويل الهدف!", confirmInitPassMsg1: "هل انت متاكد من عمل اعادة الضبط؟", confirmInitPassMsg2: "كلمة المرور؟",
    initPassSuccess: "تمت إعادة ضبط كلمة المرور بنجاح!", initPassError: "فشل إعادة ضبط كلمة المرور!", confirmInitUserPassMsg1: "تاكيد اعادة ضبط الحساب؟", page: "Page", records: "Records"
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
var productUpdatePage = { oilCoefficient: "الوقود/100 كلم", updateIcon: "الرمز", carNumMsg1: "رقم المركبة طويل جدا!", sccuess: "تم التحديث بنجاح!", faild: "فشل التحديث!", isExistMsg: "رقم المركبة موجود, يرجى ادخال رقم آخر!", filterLBS: "filter LBS", photo: "Photo" };

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
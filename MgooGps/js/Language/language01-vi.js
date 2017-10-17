function writePage(msg) {
     //越南语
    document.write(msg);
}

var allPage = { deviceNo: "Số IMEI", search: "Tìm kiếm ", tracking: "Theo dõi", playback: "Xem lại", monitor: "Giám sát", home: "Trang chủ", customer: "Kinh doanh", message: "Tin nhắn", exit: "Thoát",
    selSizeStr: "Kiểm tra", sizeStr: "Records", createTime: "Thời gian tạo", operation: "Hoạt động", no: "Số.", cellName: "Liên hệ", phone: "Số điện thoại", device: "Thiết bị", purchase: "Mua", stock: "Kho",
    mobileWebsite: "Truy cập bằng điện thoại di động", deviceName: "Tên thiết bị", simNo: "Số SIM điện thoại", expireTime: "Đến", remark: "Chú ý", cancel: "Hủy", deviceInfo: "Thông tin thiết bị", imeiNo: "Số IMEI",
    dataError: "Dữ liệu lỗi !", softFailed: "Lỗi !", password: "Mật khẩu", confirm: "Lưu", num: "Số.", noData: "Không có dữ liệu!", acc0: "Off", acc1: "On",
    acc2: "Không kết nối", searchNull: "IMEI/ Tên/ Số xe/ Tên/ Số tài khoản !", initPass: "Khôi phục mật khẩu mặc định", updateTime: "Cập nhật thời gian", loginAccount: "Tài khoản đăng nhập", passLengthMsg: "Mật khẩu phải nhỏ hơn 20 số", type: "Kiểu", userType1: "Người dùng",
    userType2: "đại lý", address: " Địa chỉ", add: "Thêm mới", information: "Thông tin", deviceType: "Kiểu", overspeed: "Vượt quá tốc độ cho phép", kmHour: "Km/ giờ", devicePhone: "Số SIM điện thoại", modelName: "Kiểu",
    createTime: "Thời gian tạo", activeTime: "Thời gian hoạt động", hireExpireTime: "Thời gian hết hạn", edit: "Chỉnh sửa", more: "Thêm", deletes: "Xóa", cusName: "Tên khách hàng", userInfo: "Thông tin khách hàng", plsSelUser: "Chọn một khách hàng !",
    km: "Ki lô mét", plsSel: "Chọn", all: "Tất cả", online: "Online", offline: "Offline", arrears: "Quá hạn", carNum: "Biển số xe", speedLimit: "Tốc độ giới hạn", lat: "Kinh độ", lng: "Vĩ độ", speed: "Tốc độ", drection: "Hướng",
    allDistance: "Tổng quãng đường", state: "Trạng thái", positionTime: "Position time", status1: "Đăng xuất", moving: "Đang di chuyển", stopCar: "Dừng", accStr: "Trạng thái ACC", edit2: "Chỉnh sửa", deliveryTime: "Thời gian hoạt động",
    cellPhone: "Số điện thoại", save: "Lưu", clear: "Xóa", type2: "Vai trò", view: "View", deviceHireDay: "代充点数"
};

//login.aspx
var loginPage = {
    title: "Hệ thống giám sát phương tiện GPS", accountTab: "tài khoản/điện thoại/IMEI", imeiTab: "Số IMEI", account: "Tài khoản", password: "Mật khẩu", loginSubmit: "Đăng nhập", languageMsg: "Các ngôn ngữ hỗ trợ", loginMsg: "Xin vui lòng nhập tài khoản.",
    loginMsg2: "Vui lòng nhập mật khẩu của bạn.", loginMsg3: "Không thể bỏ trống!", wytx: "I want try", androidBaidu: "android(Baidu map) Download", androidGoogle: "android(Google map) Download", iphone: "iphone jailbreak Download",
    welcome: "Chào mừng bạn, vui lòng đăng nhập!", loginErrorMsg: "Tài khoản hoặc mật khẩu sai", loginFailure: "đăng nhập thất bại", ForgotPassword: "Quên mật khẩu？", RememberPassword: "nhớ mật khẩu", Register: "Đăng ký"
};


//Dealer.aspx
var dealerPage = { warnTitle: "Mở cảnh báo bản tin", warnSound: "Mở cảnh báo âm thanh", username: "Tên khách hàng/ Tài khoản", hello: "Xin chào", changePassword: "Đổi mật khẩu", searchDevice: "Thiết bị", searchUser: "Khách hàng",
    searchDevice2: "Kiểm tra thiết bị", searchUser2: "Kiểm tra khách hàng", name: "Tên khách hàng", belongTo: "Thành viên", sim: "Số điện thoại", activeTime: "Ngày khởi tạo", customerName: "Tên khách hàng", nowSearch: "Đang tìm kiếm…",
    name2: "Tên khách hàng", name3: "Tên thiết bị"
};

//Home.aspx
var homePage = { quickSearch: "Tìm kiếm", searchTxt: "Số IMEI/ T/ Số xe", stage: "Stage", deviceCount: "Tống số thiết bị", operations: "Xem thêm", novice: "newbie", quickSale: "Bán nhanh",
    batchSale: "Bán hàng loạt", customer: "Khách hàng", sale: "Bán", customerList: "Danh sách khách hàng", addDevice: "Thêm thiết bị", add: "+Thêm mới", imeiNullMsg: "Xin hãy chọn thiết bị!", expireTimeNullMsg: "Xin hãy chọn thiết bị hết hạn!",
    saleSuccess: "Thành công!"
};

var warnMessagePage = { alarmType: "Loại cảnh báo", alarmTime: "Thời gian cảnh báo" };

var alarmIndexPage = { geofenceIn: "Đi vào hàng rào", geofenceOut: "Ra khỏi hàng rào", moved: "Đã thay thế", lowBattery: "Cảnh báo pin yếu", sos: "Cảnh báo khẩn cấp", cutPower: "Cảnh báo mất nguồn ngoài", vibration: "Cảnh báo rung động",
    overSpeed: "Vượt quá tốc độ cho phép", offline: "Offline"
};

//map.aspx
var mapPage = { searchInput: 'Xin hãy nhập tên/ Số IMEI', divicesInfo: "Thông tin thiết bị", geofence: "Hàng rào địa lý", cutOffPetrol: "Ngắt xăng dầu", restorePetrol: "Khôi phục ngắt xăng dầu", checkLocation: "Kiểm tra vị trí", checkCommand: "Kiểm tra lệnh",
    sendConfirm: "Xin hãy xác nhận tài khoản trước khi gửi lệnh", passNull: "Xin hãy nhập mật khẩu!", passError: "Sai mật khẩu !", sendMsg1: "Đang gửi tin nhắn, xin hãy chờ…", sendSuccess: "Thành công! Xin hãy chờ phản hồi…",
    sendMsg2: "Lệnh rỗng", sendMsg3: "Thiết bị không tồn tại", sendMsg4: "Xin hãy cho thiết bị online", sendMsg5: "Gửi lệnh thành công", responseSuccess: "Phản hồi thành công!", responseNull: "Phản hồi rỗng!",
    checkLocatoin: "Kiểm tra vị trí", checkCommandTitle: "Kiểm tra lệnh", cmdType: "Kiểu lệnh", cmdState: "Trạng thái lệnh", responseText: "Tin nhắn phản hồi", responseTime: "Thời gian phản hồi", sendTime: "Thời gian gửi",
    dyd: "Ngắt xăng dầu", hfyd: "Khôi phục ngắt xăng dầu", deviceResponse: "Thiết bị phản hồi", sendSuccess2: "Lệnh đã được gửi đi !", noSend: "Lệnh chưa được gửi đi", deviceDetailList: "Danh sách thiết bị chi tiết", addGroup: "Thêm nhóm", defaultGroup: "Mặc định",
    moveToGroup: "Di chuyển nhóm", delGroupConfirm: "Đồng ý xóa ?", downloadLocation: "Tracking Report", deviceFortify: "GPS Fortify", deviceDismiss: "GPS Dismiss", accOn: "ACC ON", accOff: "ACC OFF", fortify: "ARM", dismiss: "DimARM", carOpen: "Door Open", carClose: "Door Close", zdlj: "engine connect", zddk: "engine cut",
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

var courseName = { dueNorth: "Bắc", northeast: "Đông- Bắc", dueEast: "Đông", southeast: "Đông- Nam", dueSouth: "Nam", southwest: "Tây Nam", dueWest: "Tây", northwest: "Tây-Bắc" };

//CustomersList.aspx
var cusPage = { delUserConfirm: "Đồng ý xóa", delUserConfirm2: "Có phải người dùng này?", delUserMsg: "Tài khoản này có thiết bị. Không thể xóa!", delUserMsg2: "Tài khoản này có tài khoản con. Không thể xóa!", msgFailed: "Xóa thất bại!",
    updateUserSuccess: "Thành công!", updateUserFailed: "Thất bại!", delDeviceConfirm2: "Có phải thiết bị này?", addCus: "Thêm khách hàng", manCus: "Quản lý khách hàng", delCus: "Xóa khách hàng", saleTo: "Di chuyển khách hàng",
    beDevice: "Thành viên", updateExpTime: "Cập nhật thời gian quá hạn", deviceChange: "Di chuyển", cusInfo: "Thông tin khách hàng", toCus: "Khách hàng", newAddChildrenCus: "Thêm khách hàng", parentCus: "Khách hàng cấp cao",
    confirmPass: "Xác nhận lại mật khẩu", allCus: "Tất cả khách hàng", addCusTitle: "Thêm khách hàng", loginToUser: "Giám sát", childCus: "Tài khoản con", changeDevices: "Di chuyển hàng loạt", updateExpDevices: "Cập nhật hàng loạt",
    addUserMsg: "Chú ý: Xin hãy hoàn thành các thông tin như số điện thoại, thông tin liên hệ.", plsParentCusMsg: "Xin hãy chọn khách hàng cấp cao !", msg1: "Tên, tài khoản, mật khẩu không thể để trống !", passError: "Mật khẩu nhập trong hai lần không giống nhau, xin hãy nhập lại !",
    existAccount: "Tài khoản đã tồn tại !", dataError: "Dữ liệu rỗng !", dataNull: "Tên, tài khoản, mật khẩu không thể để trống !", imeisNull: "Số IMEI không thể trống !", saveDevuceMsg1: "Input together:", saveDevuceMsg2: "Nhóm số IMEI,",
    saveDevuceMsg3: "Thành công!", saveDevuceMsg4: "Thành công :", saveDevuceMsg5: "Nhóm", saveDevuceMsg6: "Thất bại :", saveDevuceMsg7: "Lỗi !", updateExpSuccess: "Thành công !",
    updateError: "Chỉnh sửa không thành công !", changeDeviceSuccess: "Thành công !", changeDeviceError: "Di chuyển thiết bị không thành công !", confirmInitPassMsg1: "Bạn có chắc chắn muốn reset:", confirmInitPassMsg2: "Mật khẩu?:",
    initPassSuccess: "Reset mật khẩu thành công !", initPassError: "Reset mật khẩu thất bại !", confirmInitUserPassMsg1: "Xác nhận reset tài khoản?:", page: "Page", records: "Records"
};

var productUpdatePage = { oilCoefficient: "Nhiên liệu/ 100 km", updateIcon: "Biểu tượng", carNumMsg1: "Số xe quá dài !", sccuess: "Cập nhật thành công !", faild: "Cập nhật thất bại !", isExistMsg: "Số xe đã tồn tại, xin hãy thử số khác!", filterLBS: "filter LBS", photo: "Photo" };



var moneyPage = { day: "day", msg1: "1 point for 1 day", oneYeah: "one year", twoYeah: "two years", lifelong: "lifelong", msg2: "Extend the maturity time", msg3: "the device is charged successfully!", msg4: "the points is not enough, please charge!!", msg5: "days must be greater then 0!", msg6: "can not transfer to oneself!", pointGive: "points for sale", pointConsumptionLog: "points consumer records", loginNameNotExist: "this name does not exist", intoLoginID: "the login account to transfer into", intoUsername: "the username to transfer into", intoPoint: "the points to transfer into", detect: "detect", pleaseDetectLoginName: "please test the login name", pleaseInputIntoPoint: "please input the points" };

//var moneyPage = { day: "天", msg1: "1个点数1天", oneYeah: "一年", twoYeah: "二年", lifelong: "终身", msg2: "增加到期天数", msg3: "设备充值天数成功!", msg4: "点数不够,请充值!", msg5: "天数必须大于0!" };

var yiwen201405 = { battery: "Battery Level" };
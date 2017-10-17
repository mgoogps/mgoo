function writePage(msg) {
    //越南语
    document.write(msg);
}

var allPage = { tab1: "Tổng quan về di chuyển", tab2: "Tổng quan về cảnh báo", tab3: "Thống kê thiết bị", tab4: "Chi tiết cảnh báo", startTime: "Từ", endTime: "Đến", search: "Tìm kiếm", num: "Số.", deviceName: "Tên thiết bị",
    time: "Thời gian", distance: "Quãng đường ( km)", overspeed: "Quá tốc độ cho phép", noData: "Không có dữ liệu!", lat: "Kinh độ", lng: "Vĩ độ", speed: "Tốc độ", address: "Địa chỉ", speedKM: "km/ giờ", day: "Ngày", hour: "Giờ",
    minute: "Phút", pleSel: "Xin hãy chọn", date: "Ngày", plsDeviceMsg: "Xin hãy chọn thiết bị",   moreDevice: "Xem thêm", msg: "Tin nhắn", myAccount: "Tài khoản của tôi", changePass: "Đổi mật khẩu",
    tracking: "Theo dõi", playback: "Xem lại", monitor: "Giám sát", home: "Trang trủ", customer: "Khách hàng", report: "Thống kê", more: "Xem thêm",  no: "Số.", name: "Tên", carNum: "Số xe",
    imeiNo: "Số IMEI", activeTime: "Thời gian kích hoạt", hireExpireTime: "Đến ngày", operation: "Hoạt động",  edit: "Chỉnh sửa", divicesInfo: "Thông tin thiết bị", cellName: "Liên hệ",
    phone: "Số điện thoại", timezone: "Múi giờ", save: "Lưu", confirm: "Xác nhận", updateUserSuccess: "Chỉnh sửa thành công !", updateUserFailed: "Chính sửa không thành công !", modelName: "Kiểu", state: "Trạng thái",
    drection: "Hướng", baidu: "Bai du", google: "Google", day: "Ngày", hour: "Giờ", minute: "Phút", stopTime: "Thời gian dừng", desc: "Ghi chú", cancel: "Hủy", del: "Xóa", delSuccess: "Xóa thành công !",
    delFaild: "Xóa thất bại !", accStr: "Trạng thái ACC", acc0: "Tắt", acc1: "Mở", positionType: "Kiệu vị trí", manDevice: "Quản lí thiết bị", type: "Kiểu", acc2: "Không kết nối", resolve: "Phân giải", startStopTime: "Bắt đầu",
    endStopTime: "Kết thúc", status1: "Đăng xuất", moving: "Đang di chuyển", stopCar: "Dừng",   arrears: "Quá hạn", primaryEmail: "Email", positionTime: "Position time", clear: "Xóa", targetName: "Tên thiết bị",
    toExcel: "Excel xuất khẩu", distance2: "Quãng đường", km: "km", m: "M", event: "Event recording", inTime: "Enter time", outTime: "Departure time", moneyCount: "点数", deviceHireDay: "代充点数", belongTo: "所属用户",  

    deviceNo: "Số IMEI",  message: "Tin nhắn", exit: "Thoát",
    selSizeStr: "Kiểm tra", sizeStr: "Records", createTime: "Thời gian tạo",    device: "Thiết bị", purchase: "Mua", stock: "Kho",
    mobileWebsite: "Truy cập bằng điện thoại di động", simNo: "Số SIM điện thoại", expireTime: "Đến", remark: "Chú ý",  deviceInfo: "Thông tin thiết bị", 
    dataError: "Dữ liệu lỗi !", softFailed: "Lỗi !", password: "Mật khẩu",  
    searchNull: "IMEI/ Tên/ Số xe/ Tên/ Số tài khoản !", initPass: "Khôi phục mật khẩu mặc định", updateTime: "Cập nhật thời gian", loginAccount: "Tài khoản đăng nhập", passLengthMsg: "Mật khẩu phải nhỏ hơn 20 số",   userType1: "Người dùng",
    userType2: "đại lý", add: "Thêm mới", information: "Thông tin", deviceType: "Kiểu",   kmHour: "Km/ giờ", devicePhone: "Số SIM điện thoại",  
    deletes: "Xóa", cusName: "Tên khách hàng", userInfo: "Thông tin khách hàng", plsSelUser: "Chọn một khách hàng !",
    plsSel: "Chọn", all: "Tất cả", online: "Online", offline: "Offline",   speedLimit: "Tốc độ giới hạn" ,
    allDistance: "Tổng quãng đường",  edit2: "Chỉnh sửa", deliveryTime: "Thời gian hoạt động",
    cellPhone: "Số điện thoại", type2: "Vai trò", view: "View", StatisticalAnalysis: "phân tích thống kê"
};
 
var dealerPage = {
    warnTitle: "Mở cảnh báo bản tin", warnSound: "Mở cảnh báo âm thanh", username: "Tên khách hàng/ Tài khoản", hello: "Xin chào", changePassword: "Đổi mật khẩu", searchDevice: "Thiết bị", searchUser: "Khách hàng",
    searchDevice2: "Kiểm tra thiết bị", searchUser2: "Kiểm tra khách hàng", name: "Tên khách hàng", belongTo: "Thành viên", sim: "Số điện thoại", activeTime: "Ngày khởi tạo", customerName: "Tên khách hàng", nowSearch: "Đang tìm kiếm…",
    name2: "Tên khách hàng", name3: "Tên thiết bị"
};

 
var homePage = {
    quickSearch: "Tìm kiếm", searchTxt: "Số IMEI/ T/ Số xe", stage: "Stage", deviceCount: "Tống số thiết bị", operations: "Xem thêm", novice: "newbie", quickSale: "Bán nhanh",
    batchSale: "Bán hàng loạt", customer: "Khách hàng", sale: "Bán", customerList: "Danh sách khách hàng", addDevice: "Thêm thiết bị", add: "+Thêm mới", imeiNullMsg: "Xin hãy chọn thiết bị!", expireTimeNullMsg: "Xin hãy chọn thiết bị hết hạn!",
    saleSuccess: "Thành công!"
};


var courseName = { dueNorth: "Bắc", northeast: "Đông Bắc", dueEast: "Đông", southeast: "Đông Nam", dueSouth: "Nam", southwest: "Tây Nam", dueWest: "Tây", northwest: "Tây Bắc" };

var reportPage = { title: "Thống kê tổng quãng đường di chuyển", warnCount: "Cảnh báo", stopCount: "Dừng đỗ" };

var alarmSumPage = { title: "Thống kê tổng quãng đường di chuyển", lowCount: "Cảnh báo pin yếu", cutPowerCount: "Cảnh báo cắt nguồn ngoài", vibCount: "Cảnh báo rung động", sosCount: "Cánh báo khẩn cấp SOS" }

var overSpeedPage = { continueTime: "Thời gian liên tục", speedlimit: "Tốc độ giới hạn", distancePage: "Báo cáo quãng đường", overspeedDetail: "Chi tiết quá tốc độ", stopDetail: "Chi tiết dừng đỗ" };

var alarmIndexPage = { geofenceIn: "Vào khu khoanh vùng", geofenceOut: "Ra khỏi khu khoanh vùng", moved: "Đã thay thế", lowBattery: "Cảnh báo pin yếu", sos: "Cảnh báo khẩn cấp SOS", cutPower: "Cảnh báo cắt nguồn ngoài", vibration: "Cảnh báo rung động",
    overSpeed: "Quá tốc đô cho phép", offline: "Offline"
};

var runindexPage = { statistics: "Truy vấn bởi", statistics2: "Thông tin chi tiết hàng ngày", oilCoefficient: "Nhiên liệu tiêu thụ / 100 km", L: "Lít", oil: "Công suất tiêu thụ nhiên liệu" };

var alarmDetailPage = { alarmTime: "Thời gian cảnh báo", alarmType: "Kiểu cảnh báo", alarmCount: "Thống kê cảnh báo", alarmDetail: "Chi tiết cảnh báo" };

var userPage = { warnTitle: "Tổng quan về cảnh báo", warnSound: "Mở chức năng cảnh báo âm thanh", day7Exp: "Đã quá hạn 7 ngày", day60Exp: "Đã quá hạn 60 ngày", alreadyExp: "Quá hạn",
    username: "Tên/ Tài khoản", hello: "Xin chào", searchDevice: "Thiết bị", searchUser: "Khách hàng", exit: "Thoát", message: "Tin nhắn", allDeivce: "Tất cả thiết bị", moneyMove: "点数转让", moneyHistory: "点数消费记录"
};

var userInfoPage = { myAccount: "Tài khoản của tôi", changePassword: "Đổi mật khẩu", userMsg: "Chú ý: Hoàn thiện các thông tin như số điện thoại, liên hệ", customerName: "Tên khách hàng",
    account: "Tài khoản đăng nhập", oldPass: "Mật khẩu cũ", newPass: "Mật khẩu mới", confirmPass: "Xác nhận mật khẩu", passLengthMsg: "Mật khẩu phải ít hơn 20 số", passNull: "Mật khẩu không được để trống",
    passError: "Mật khẩu trong hai lần nhập khác nhau !", changePassSuccess: "Reset mật khẩu thành công !", changePassError: "Reset mật khẩu không thành công !", oldPassError: "Mật khẩu cũ lỗi !", warnSendMsg: "Chế độ thông báo cảnh báo bổ xung",
    sendEmail: "Email", service: "cung cấp dịch vụ"
};

var warnMessagePage = { warnMsg: "Tin nhắn cảnh báo", handle1: "Undealed", handle2: "Handled", alarmType: "Kiểu cảnh báo", alarmTime: "Thời gian cảnh báo" };

var trackingPage = { secondMsg: "Cập nhật vị trí sau giây !" };

var playbackPage = { from: "From", to: "To", play: "Play", pause: "Pause", next: "Continue", fast: "Fast", slow: "Slow", timeMsg: "End Time should be later than Start Time!", nowLoading: "Loading data!",
    playOver: "Finish!", searchNull: "No data!", showLBS: "Show LBS"
};

var geofencesPage = { geofence: "Geo-fence", addGeofence: "Add", geoNameNull: "Geo-fence name cannot be blank!", radius: "R(m)", delGeoConfirm: "Confirm to delete", delGeoConfirm2: "Is this Geo-fence?",
    addSuccess: "Đã thêm thành công!", addFaild: "Thêm Không!"
};

var iframeMapPage = { baiduMap: "Baidu Map", googleMap: "Google Map", deviceName: "Target name" };

var userUpdatePage = { account: "Account" };

//map.aspx
var mapPage = { searchInput: 'Xin hãy nhập tên/ Số IMEI', divicesInfo: "Thông tin thiết bị", geofence: "Hàng rào địa lý", cutOffPetrol: "Ngắt xăng dầu", restorePetrol: "Khôi phục ngắt xăng dầu", checkLocation: "Kiểm tra vị trí", checkCommand: "Kiểm tra lệnh",
    sendConfirm: "Xin hãy xác nhận tài khoản trước khi gửi lệnh", passNull: "Xin hãy nhập mật khẩu!", passError: "Sai mật khẩu !", sendMsg1: "Đang gửi tin nhắn, xin hãy chờ…", sendSuccess: "Thành công! Xin hãy chờ phản hồi…",
    sendMsg2: "Lệnh rỗng", sendMsg3: "Thiết bị không tồn tại", sendMsg4: "Xin hãy cho thiết bị online", sendMsg5: "Gửi lệnh thành công", responseSuccess: "Phản hồi thành công!", responseNull: "Phản hồi rỗng!",
    checkLocatoin: "Kiểm tra vị trí", checkCommandTitle: "Kiểm tra lệnh", cmdType: "Kiểu lệnh", cmdState: "Trạng thái lệnh", responseText: "Tin nhắn phản hồi", responseTime: "Thời gian phản hồi", sendTime: "Thời gian gửi",
    dyd: "Ngắt xăng dầu", hfyd: "Khôi phục ngắt xăng dầu", deviceResponse: "Thiết bị phản hồi", sendSuccess2: "Lệnh đã được gửi đi !", noSend: "Lệnh chưa được gửi đi", deviceDetailList: "Danh sách thiết bị chi tiết", addGroup: "Thêm nhóm", defaultGroup: "Mặc định",
    moveToGroup: "Di chuyển nhóm", delGroupConfirm: "Đồng ý xóa ?", downloadLocation: "Tracking Report", deviceFortify: "GPS Fortify", deviceDismiss: "GPS Dismiss", accOn: "ACC ON", accOff: "ACC OFF", fortify: "ARM", dismiss: "DimARM", carOpen: "Door Open", carClose: "Door Close", zdlj: "engine connect",
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
    delUserConfirm: "Đồng ý xóa", delUserConfirm2: "Có phải người dùng này?", delUserMsg: "Tài khoản này có thiết bị. Không thể xóa!", delUserMsg2: "Tài khoản này có tài khoản con. Không thể xóa!", msgFailed: "Xóa thất bại!",
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
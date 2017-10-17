function writePage(msg) {
    //西班牙
    document.write(msg);
}

var allPage = { tab1: "Moviendo Vista Global", tab2: "Vista Global de Alarma", tab3: "Estadísticas de Dispositivo", tab4: "Detalles de Alarma", startTime: "De", endTime: "Para", search: "Búsqueda", num: "Núm.", deviceName: "Nombre de Objetivo",
    time: "Tiempo", distance: "Kilometraje (km)", overspeed: "Exceso de Velocidad", noData: "¡No hay datos!", lat: "Lat", lng: "Lon", speed: "Velocidad", address: "Dirección", speedKM: "km/h", day: "Día", hour: "Hora",
    minute: "Minutos", pleSel: "Por favor seleccione", date: "Datos", plsDeviceMsg: "Por favor seleccione el objectivo",  moreDevice: "Más", msg: "Mensaje", myAccount: "Mi cuenta", changePass: "Cambiar contraseña",
    tracking: "Seguimiento", playback: "Reproducción", monitor: "Monitor", home: "Inicio", customer: "Cliente", report: "Estadísticas", more: "Más",   no: "Núm.", name: "Nombre", carNum: "Núm. De Matrícula",
    imeiNo: "Núm. IMEI", activeTime: "Hora de activación", hireExpireTime: "Debido hasta", operation: "Operación",  edit: "Modificar", divicesInfo: "Información del dispositivo", cellName: "Contactos",
    phone: "Tel/Móv", timezone: "Zona horaria", save: "Guardar", confirm: "Confirmación", updateUserSuccess: "¡Modificación exitosa!", updateUserFailed: "¡Fallo al modificar!", modelName: "Tipo", state: "Estado",
    drection: "Orientación", baidu: "Baidu", google: "Google", day: "Día", hour: "Hora", minute: "Minutos", stopTime: "Tiempo de Parada", desc: "Observación", cancel: "Cancelar", del: "Eliminar", delSuccess: "¡Eliminación Exitosa!",
    delFaild: "¡Fallo al eliminar!", accStr: "Estado ACC", acc0: "Apagado", acc1: "Encendido", positionType: "Tipo de Posición", manDevice: "Gestión de dispositivo", type: "Tipo", acc2: "Desconectado", resolve: "Resolver", startStopTime: "Iniciar",
    endStopTime: "Acabar", status1: "Sesión Cerrada", moving: "Moviendo", stopCar: "Parar",   arrears: "Restraso", primaryEmail: "Correo electrónico", positionTime: "Hora de posición", clear: "Limpiar", targetName: "Nombre de Objetivo",
    toExcel: "Export Excel", distance2: "Kilometraje", km: "km", m: "M", event: "Event recording", inTime: "Enter time", outTime: "Departure time", moneyCount: "点数", deviceHireDay: "代充点数", belongTo: "所属用户",  

    deviceNo: "Número IMEI",    message: "Mensaje", exit: "Salir",
    selSizeStr: "Revisar", sizeStr: "registros", createTime: "Tiempo de Creación",  device: "Objetivo", purchase: "Compra", stock: "Mercancía",
    mobileWebsite: "Acceso mediante móvil",  simNo: "Núm. De Tarjeta SIM", expireTime: "Debido hasta", remark: "Observación",  deviceInfo: "Información del Dispositivo", 
    dataError: "¡Error de Datos!", softFailed: "¡Error!", password: "Contraseña",    
    searchNull: "¡Se requiere IMEI/Nombre/Núm. De Cat/Nombre/Cuenta!", initPass: "Restablecer contraseña", updateTime: "Tiempo de Actualización", loginAccount: "Cuenta de Acceso", passLengthMsg: "La contraseña tiene que ser menos de 20 dígitos",  userType1: "Usuario Final",
    userType2: "Distribuidor",   add: "Nuevo", information: "Información", deviceType: "Tipo", kmHour: "Km/h", devicePhone: "Núm. De Tarjeta SIM",  
  deletes: "Eliminar", cusName: "Nombre del Cliente", userInfo: "Información del cliente", plsSelUser: "¡Por favor seleccione a un usuario!",
  plsSel: "Seleccionar", all: "Todos", online: "En línea", offline: "Fuera de línea",   speedLimit: "Límite de Velocidad" ,  
    allDistance: "Total kiómetros",   edit2: "Modificar", deliveryTime: "Tiempo de Activación",
    cellPhone: "Tel/Móv.", type2: "Papeles", view: "View", StatisticalAnalysis: "El análisis estadístico"
};

//Dealer.aspx
var dealerPage = {
    warnTitle: "Alarma para Recordar", warnSound: "Sonido de Alarma Activado", username: "Nombre/Cuenta del Cliente", hello: "Hola", changePassword: "Cambiar Contraseña", searchDevice: "Objetivo", searchUser: "Cliente",
    searchDevice2: "VerificacónObjectivo", searchUser2: "VerificacónCliente", name: "Nombre del Cliente", belongTo: "Membresía", sim: "Núm. De Tarjeta SIM", activeTime: "Fecha de Activación", customerName: "nombre del Cliente", nowSearch: "Buscando…",
    name2: "Nombre del Cliente", name3: "Nombre del Objetivo"
};

//Home.aspx
var homePage = {
    quickSearch: "Búsqueda", searchTxt: "Núm. IMEI/T/Núm. De Coche", stage: "Etapa", deviceCount: "Valor Final", operations: "Más", novice: "novato", quickSale: "Venta Rápida",
    batchSale: "Lote de Venta", customer: "Cliente", sale: "Venta", customerList: "Lista de clientes", addDevice: "Adicional", add: "+nuevo", imeiNullMsg: "¡Por favor seleccione el ojectivo!", expireTimeNullMsg: "¡Por favor seleccione la fecha de expiración!",
    saleSuccess: "¡Éxito!"
};

var courseName = { dueNorth: "Norte", northeast: "Nordeste", dueEast: "Este", southeast: "Sudeste", dueSouth: "Sur", southwest: "Sudoeste", dueWest: "Oeste", northwest: "Noroeste" };

var reportPage = { title: "Estadiísticas Totales de Movimiento", warnCount: "Alarma", stopCount: "Quedar" };

var alarmSumPage = { title: "Estadísticas Totales de Alarma", lowCount: "Alarma de batería baja", cutPowerCount: "Apagar la Alarma", vibCount: "Alarma por Vibración", sosCount: "alarma SOS" }

var overSpeedPage = { continueTime: "Tiempo de Conducción", speedlimit: "Límite de Velocidad", distancePage: "Informe Sobre Kilometraje", overspeedDetail: "Detalles de Exceso de Velocidad", stopDetail: "Detalles de Parada" };

var alarmIndexPage = { geofenceIn: "Geo-fense Dentro", geofenceOut: "Geo-fense Fuera", moved: "Desplazamiento", lowBattery: "Alarma de batería baja", sos: "Alarma SOS", cutPower: "Apagar la Alarma", vibration: "Alarma por Vibración",
    overSpeed: "Exceso de Velocidad", offline: "Offline"
};

var runindexPage = { statistics: "Filtrar Por", statistics2: "Detalles Diarios", oilCoefficient: "Coeficiente de Consumo de Combustible/100 Kilómetros", L: "L", oil: "Consumo de Combustible" };

var alarmDetailPage = { alarmTime: "Hora de Alarma", alarmType: "Tipo de Alarma", alarmCount: "Estadísticas de Alarma", alarmDetail: "Detalles de Alarma" };

var userPage = { warnTitle: "Vista general de alarma", warnSound: "Sonido de alarma activado", day7Exp: "Expirado 7Días", day60Exp: "Expirado 60Días", alreadyExp: "Expirado",
    username: "Nombre/Cuenta", hello: "Hola", searchDevice: "Objetivo", searchUser: "Cliente", exit: "Salir", message: "Mensaje", allDeivce: "Todos los objectivos", moneyMove: "点数转让", moneyHistory: "点数消费记录"
};

var userInfoPage = { myAccount: "Mi cuenta", changePassword: "Cambiar Contraseña", userMsg: "Consejo: Complete la información, tal como número de teléfono, contacto.", customerName: "Nombre del Cliente",
    account: "Cuenta de Acceso", oldPass: "Contraseña Actual", newPass: "Nueva Contraseña", confirmPass: "Confirmación de Contraseña", passLengthMsg: "La contraseña tiene que ser menos de 20 dígitos", passNull: "¡La entrada de contraseña no puede estar en blanco!",
    passError: "¡Las dos entradas de la contraseña son diferentes!", changePassSuccess: "¡Contraseña restablecida con éxito!", changePassError: "¡Fallo al restablecer contraseña!", oldPassError: "¡Error de contraseña antigua!", warnSendMsg: "Modo Inform de Alarma Adicional",
    sendEmail: "Correo Electrónico", service: "Los proveedores de servicios"
};

var warnMessagePage = { warnMsg: "Mensaje de Alarma", handle1: "No manejado", handle2: "manejado", alarmType: "Tipo de Alarma", alarmTime: "Hora de Alarma" };

var trackingPage = { secondMsg: "¡Refrescar dentro de segundos!" };

var playbackPage = { from: "De", to: "Para", play: "Reproducir", pause: "Pausar", next: "Continuar", fast: "Rápido", slow: "Lento", timeMsg: "¡El Tiempo Final tiene que ser posterior al Tiempo Inicial!", nowLoading: "¡Cargando Datos!",
    playOver: "¡Finalizar!", searchNull: "¡No hay datos!", showLBS: "Mostrar LBS"
};

var geofencesPage = { geofence: "Geo-fence", addGeofence: "Añadir", geoNameNull: "¡El nombre de Geo-fence no puede estar en blanco!", radius: "R(m)", delGeoConfirm: "Confirmar eliminación", delGeoConfirm2: "¿Es éste Geo-fence?",
    addSuccess: "Agregado con éxito!", addFaild: "Añadir Error!"
};

var iframeMapPage = { baiduMap: "Mapa Baidu", googleMap: "Mapa Google", deviceName: "Nombre de Objetivo" };

var productUpdatePage = { oilCoefficient: "Combustible/100 km", updateIcon: "Icono", carNumMsg1: "¡Núm. Del Coche demasiado largo!", sccuess: "¡Actualización exitosa!", faild: "¡Actualización fallida!", isExistMsg: "¡Núm. Del Coche ya existe, por favor intente con otro!", filterLBS: "filter LBS", photo: "Photo" };



var userUpdatePage = { account: "Cuenta" };

//map.aspx
var mapPage = { searchInput: "Por favor introduzca nombre/Núm. De IMEI", divicesInfo: "Información de Objetivo", geofence: "Geo-fence", cutOffPetrol: "Cortar suministro de petróleo", restorePetrol: "Restablecer suministro de petróleo", checkLocation: "Verificar ubicación", checkCommand: "Verificar comando",
    sendConfirm: "Por favor confirme la cuenta antes de enviar el comando", passNull: "¡Por favor introduzca la contraseña!", passError: "¡Error de contraseña!", sendMsg1: "Enviando mensaje, espere por favor…", sendSuccess: "¡Éxito! Por favor espere por una respuesta...",
    sendMsg2: "Comando nulo", sendMsg3: "El objetivo no existe", sendMsg4: "Por favor conecte el dispositivo en línea", sendMsg5: "Comando enviado con éxito", responseSuccess: "¡Respuesta Exitosa!", responseNull: "¡Responsa nula!",
    checkLocatoin: "Verificar ubicación", checkCommandTitle: "Verificar comando", cmdType: "Tipo de Comando", cmdState: "Estado de Comandp", responseText: "Mensaje de Respuesta", responseTime: "Tiempo de Respuesta", sendTime: "Hora de Envío",
    dyd: "Cortar suministro de petróleo", hfyd: "Restablecer suministro de petróleo", deviceResponse: "Respuesta del Dispositivo", sendSuccess2: "¡El comando se ha enviado!", noSend: "El comando no se ha enviado", deviceDetailList: "Lista de Detalles del Dispositivo", addGroup: "Añadir Grupo", defaultGroup: "Por Defecto",
    moveToGroup: "Mover Grupo", delGroupConfirm: "¿Confirmar Eliminación?", downloadLocation: "descarga de la pista", deviceFortify: "GPS Fortify", deviceDismiss: "GPS Dismiss", accOn: "ACC ON", accOff: "ACC OFF", fortify: "ARM", dismiss: "DimARM", carOpen: "Door Open", carClose: "Door Close", zdlj: "engine connect",
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

var cusPage = { updateExpTime: "Update Expired Time", updateError: "Edit failure!" };

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
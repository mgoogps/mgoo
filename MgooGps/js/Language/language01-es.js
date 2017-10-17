function writePage(msg) {
    //西班牙
    document.write(msg);
}

var allPage = { deviceNo: "Número IMEI", search: "Búsqueda", tracking: "Seguimiento", playback: "Reproducción", monitor: "Monitorizar", home: "Inicio", customer: "Negocio", message: "Mensaje", exit: "Salir",
    selSizeStr: "Revisar", sizeStr: "registros", createTime: "Tiempo de Creación", operation: "Operación", no: "Núm.", cellName: "Contactos", phone: "Tel/Móv.", device: "Objetivo", purchase: "Compra", stock: "Mercancía",
    mobileWebsite: "Acceso mediante móvil", deviceName: "Nombre del Objetivo", simNo: "Núm. De Tarjeta SIM", expireTime: "Debido hasta", remark: "Observación", cancel: "Cancelar", deviceInfo: "Información del Dispositivo", imeiNo: "Número IMEI",
    dataError: "¡Error de Datos!", softFailed: "¡Error!", password: "Contraseña", confirm: "Guardar", num: "Núm.", noData: "¡No hay datos!", acc0: "Apagado", acc1: "Encendido",
    acc2: "Desconectado", searchNull: "¡Se requiere IMEI/Nombre/Núm. De Cat/Nombre/Cuenta!", initPass: "Restablecer contraseña", updateTime: "Tiempo de Actualización", loginAccount: "Cuenta de Acceso", passLengthMsg: "La contraseña tiene que ser menos de 20 dígitos", type: "Tipo", userType1: "Usuario Final",
    userType2: "Distribuidor", address: "Dirección", add: "Nuevo", information: "Información", deviceType: "Tipo", overspeed: "Exceso de Velocidad", kmHour: "Km/h", devicePhone: "Núm. De Tarjeta SIM", modelName: "Tipo",
    createTime: "Tiempo de Creación", activeTime: "Tiempo de Activación", hireExpireTime: "Tiempo de Expiración", edit: "Modificar", more: "Más", deletes: "Eliminar", cusName: "Nombre del Cliente", userInfo: "Información del cliente", plsSelUser: "¡Por favor seleccione a un usuario!",
    km: "Kilómetro", plsSel: "Seleccionar", all: "Todos", online: "En línea", offline: "Fuera de línea", arrears: "restraso", carNum: "Núm. De Matrícula", speedLimit: "Límite de Velocidad", lat: "Latitud", lng: "Longitud", speed: "Velocidad", drection: "Orientación",
    allDistance: "Total kiómetros", state: "Estado", positionTime: "Tiempo de Posición", status1: "Sesión cerrada", moving: "Moviendo", stopCar: "Parar", accStr: "Estado ACC", edit2: "Modificar", deliveryTime: "Tiempo de Activación",
    cellPhone: "Tel/Móv.", save: "Guardar", clear: "Borrar", type2: "Papeles", view: "View", deviceHireDay: "代充点数"
};

//login.aspx
var loginPage = {
    title: "Estación de segumiento GPS", accountTab: "Cuenta/teléfono/IMEI", imeiTab: "Núm. IMEI", account: "Cuenta", password: "Contraseña", loginSubmit: "Ir", languageMsg: "Hemos ofrecido en", loginMsg: "Por favor introduzca cuenta.",
    loginMsg2: "Por favor, introduzca su contraseña.", loginMsg3: "¡La entrada no puede estar en blanco!", wytx: "I want try", androidBaidu: "android(Baidu map) Download", androidGoogle: "android(Google map) Download", iphone: "iphone jailbreak Download",
    welcome: "Bienvenido, por favor conectarse!", loginErrorMsg: "Cuenta o introducción de la contraseña de error", loginFailure: "error de inicio de sesion", ForgotPassword: "Has olvidado tu contraseña?", RememberPassword: "recordar contraseña", Register: "Registrarse"
};


//Dealer.aspx
var dealerPage = { warnTitle: "Alarma para Recordar", warnSound: "Sonido de Alarma Activado", username: "Nombre/Cuenta del Cliente", hello: "Hola", changePassword: "Cambiar Contraseña", searchDevice: "Objetivo", searchUser: "Cliente",
    searchDevice2: "VerificacónObjectivo", searchUser2: "VerificacónCliente", name: "Nombre del Cliente", belongTo: "Membresía", sim: "Núm. De Tarjeta SIM", activeTime: "Fecha de Activación", customerName: "nombre del Cliente", nowSearch: "Buscando…",
    name2: "Nombre del Cliente", name3: "Nombre del Objetivo"
};

//Home.aspx
var homePage = { quickSearch: "Búsqueda", searchTxt: "Núm. IMEI/T/Núm. De Coche", stage: "Etapa", deviceCount: "Valor Final", operations: "Más", novice: "novato", quickSale: "Venta Rápida",
    batchSale: "Lote de Venta", customer: "Cliente", sale: "Venta", customerList: "Lista de clientes", addDevice: "Adicional", add: "+nuevo", imeiNullMsg: "¡Por favor seleccione el ojectivo!", expireTimeNullMsg: "¡Por favor seleccione la fecha de expiración!",
    saleSuccess: "¡Éxito!"
};

var warnMessagePage = { alarmType: "Tipo de Alarma", alarmTime: "Hora de Alarma" };

var alarmIndexPage = { geofenceIn: "Geo-fense Entrada", geofenceOut: "Geo-fense Salida", moved: "Desplazamiento", lowBattery: "Alarma de batería baja", sos: "Alarma SOS", cutPower: "Apagar la Alarma", vibration: "Alarma por Vibración",
    overSpeed: "Exceso de Velocidad", offline: "Offline"
};

//map.aspx
var mapPage = { searchInput: "Por favor introduzca nombre/Núm. De IMEI", divicesInfo: "Información de Objetivo", geofence: "Geo-fence", cutOffPetrol: "Cortar suministro de petróleo", restorePetrol: "Restablecer suministro de petróleo", checkLocation: "Verificar ubicación", checkCommand: "Verificar comando",
    sendConfirm: "Por favor confirme la cuenta antes de enviar el comando", passNull: "¡Por favor introduzca la contraseña!", passError: "¡Error de contraseña!", sendMsg1: "Enviando mensaje, espere por favor…", sendSuccess: "¡Éxito! Por favor espere por una respuesta...",
    sendMsg2: "Comando nulo", sendMsg3: "El objetivo no existe", sendMsg4: "Por favor conecte el dispositivo en línea", sendMsg5: "Comando enviado con éxito", responseSuccess: "¡Respuesta Exitosa!", responseNull: "¡Responsa nula!",
    checkLocatoin: "Verificar ubicación", checkCommandTitle: "Verificar comando", cmdType: "Tipo de Comando", cmdState: "Estado de Comandp", responseText: "Mensaje de Respuesta", responseTime: "Tiempo de Respuesta", sendTime: "Hora de Envío",
    dyd: "Cortar suministro de petróleo", hfyd: "Restablecer suministro de petróleo", deviceResponse: "Respuesta del Dispositivo", sendSuccess2: "¡El comando se ha enviado!", noSend: "El comando no se ha enviado", deviceDetailList: "Lista de Detalles del Dispositivo", addGroup: "Añadir Grupo", defaultGroup: "Por Defecto",
    moveToGroup: "Mover Grupo", delGroupConfirm: "¿Confirmar Eliminación?", downloadLocation: "descarga de la pista", deviceFortify: "GPS Fortify", deviceDismiss: "GPS Dismiss", accOn: "ACC ON", accOff: "ACC OFF", fortify: "ARM", dismiss: "DimARM", carOpen: "Door Open", carClose: "Door Close", zdlj: "engine connect", zddk: "engine cut",
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

var courseName = { dueNorth: "Norte", northeast: "Noreste", dueEast: "Este", southeast: "Sudeste", dueSouth: "Sur", southwest: "Sudoeste", dueWest: "Oeste", northwest: "Noroeste" };

//CustomersList.aspx
var cusPage = { delUserConfirm: "Confirme para eliminar", delUserConfirm2: "¿Es éste el usuario?", delUserMsg: "Esta cuenta posee dispositivos. ¡No se puede eliminar!", delUserMsg2: "Esta cuenta posee sub-cuentas. ¡No se puede eliminar!", msgFailed: "¡Fallo en eliminar!",
    updateUserSuccess: "¡Éxito!", updateUserFailed: "¡Fallo!", delDeviceConfirm2: "¿es éste el objetivo?", addCus: "Añadir Cliente", manCus: "Gestionar Cliente", delCus: "Eliminar Cliente", saleTo: "Mover Cliente",
    beDevice: "Membresía", updateExpTime: "Actualizar Tiempo Expirado", deviceChange: "Mover", cusInfo: "Información del Cliente", toCus: "Clientes Meta", newAddChildrenCus: "Añadir Clientes", parentCus: "Cliente Superior",
    confirmPass: "Confirmación de Contraseña", allCus: "Todos los Clientes", addCusTitle: "Añadir Cliente", loginToUser: "Monitorizar", childCus: "Sub-cuenta", changeDevices: "Movimiento Lote", updateExpDevices: "Actualización Lote",
    addUserMsg: "Consejo: Complete la información, tal como número de teléfono, contacto.", plsParentCusMsg: "¡Por favor seleccione el cliente superior!", msg1: "¡El Nombre, la Cuenta, la Contraseña no pueden estar en blanco!", passError: "¡Las dos entradas de la contraseña son diferentes, por favor introduzca una vés más!",
    existAccount: "¡La Cuenta ya existe!", dataError: "¡Datos nulos!", dataNull: "¡El Nombre, la Cuenta, la Contraseña no pueden estar en blanco!", imeisNull: "¡El núm. De IMEI no puede estar en blanco!", saveDevuceMsg1: "Introduzca juntos:", saveDevuceMsg2: "Grupo del núm. De IMEI,",
    saveDevuceMsg3: "¡Éxito!", saveDevuceMsg4: "Éxito:", saveDevuceMsg5: "Grupo", saveDevuceMsg6: "Fallido:", saveDevuceMsg7: "¡Error!", updateExpSuccess: "¡Éxito!",
    updateError: "¡Fallo al modificar!", changeDeviceSuccess: "¡Éxito!", changeDeviceError: "¡Fallo al mover el objetivo!", confirmInitPassMsg1: "Está seguro de restablecer:", confirmInitPassMsg2: "¿contraseña?:",
    initPassSuccess: "¡Contraseña reestablecida con éxito!", initPassError: "¡Fallo al restablecer contraseña!", confirmInitUserPassMsg1: "¿Confirme el restablecimiento de cuenta?:", page: "Page", records: "Records"
};

var productUpdatePage = { oilCoefficient: "Combustible/100 km", updateIcon: "Icono", carNumMsg1: "¡Núm. Del Coche demasiado largo!", sccuess: "¡Actualización exitosa!", faild: "¡Actualización fallida!", isExistMsg: "¡Núm. Del Coche ya existe, por favor intente con otro!", filterLBS: "filter LBS", photo: "Photo" };



var moneyPage = { day: "day", msg1: "1 point for 1 day", oneYeah: "one year", twoYeah: "two years", lifelong: "lifelong", msg2: "Extend the maturity time", msg3: "the device is charged successfully!", msg4: "the points is not enough, please charge!!", msg5: "days must be greater then 0!", msg6: "can not transfer to oneself!", pointGive: "points for sale", pointConsumptionLog: "points consumer records", loginNameNotExist: "this name does not exist", intoLoginID: "the login account to transfer into", intoUsername: "the username to transfer into", intoPoint: "the points to transfer into", detect: "detect", pleaseDetectLoginName: "please test the login name", pleaseInputIntoPoint: "please input the points" };
//var moneyPage = { day: "天", msg1: "1个点数1天", oneYeah: "一年", twoYeah: "二年", lifelong: "终身", msg2: "增加到期天数", msg3: "设备充值天数成功!", msg4: "点数不够,请充值!", msg5: "天数必须大于0!" };

var yiwen201405 = { battery: "Battery Level" };
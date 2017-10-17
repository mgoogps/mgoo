function writePage(msg) {
    //葡萄牙
    document.write(msg);
}

var allPage = { tab1: "Visão Geral para Deslocamento", tab2: "Visão Geral para Alarme", tab3: "Estatísticas do Rastreador", tab4: "Detalhes do Alarme", startTime: "De", endTime: "Para", search: "Procurar", num: "No.",  
    time: "Data", distance: "Quilometragem (km)", overspeed: "Acima da Velocidade", noData: "Sem dados!", lat: "Lat", lng: "Lon", speed: "Velocidade", address: "Endereço", speedKM: "km/h", day: "Dia", hour: "Hora",
    minute: "Minutos", pleSel: "Favor selecionar", date: "Data", plsDeviceMsg: "Por favor, selecione um rastreador",   moreDevice: "Mais", msg: "Mensagem", myAccount: "Minha conta", changePass: "Alterar senha",
    tracking: "Rastrear", playback: "Reproduzir", monitor: "Reproduzir", home: "Início", customer: "Cliente", report: "Estatísticas", more: "Mais",  no: "No.", name: "Nome", carNum: "No. da Placa",
    imeiNo: "No. IMEI", activeTime: "Data da ativação", hireExpireTime: "Válido até", operation: "Operação",  edit: "Editar", divicesInfo: "Informação do rastreador", cellName: "Contatos",
    phone: "Tel/Cel", timezone: "Fuso horário", save: "Salvar", confirm: "Confirmar", updateUserSuccess: "Editado com sucesso!", updateUserFailed: "Falha na edição!", modelName: "Tipo", state: "Situação",
    drection: "Direção", baidu: "Baidu", google: "Google", day: "Dia", hour: "Hora", minute: "Minuto", stopTime: "Tempo parado", desc: "Observação", cancel: "Cancelar", del: "Apagar", delSuccess: "Apagado com sucesso!",
    delFaild: "Falha ao apagar!", accStr: "Situação ACC", acc0: "Desligado", acc1: "Ligado", positionType: "Tipo de posição", manDevice: "Gerenciamento do rastreador", type: "Tipo", acc2: "Desconectado", resolve: "Resolver", startStopTime: "Iniciar",
    endStopTime: "Terminar", status1: "Desconectado", moving: "Movendo", stopCar: "Parado",   arrears: "em aberto", primaryEmail: "E-mail", positionTime: "Tempo da posição", clear: "Limpar", targetName: "Nome do Rastreador",
    toExcel: "Para Excel", distance2: "Quilometragem", km: "KM", m: "M", event: "Evento gravado", inTime: "Entre com a data", outTime: "Data de saída", moneyCount: "Point",  belongTo: "Membership",  
  userType2: "Distributor", changePassword: " Change Password", service: "My Service", clearAll: "Clear all",

    deviceNo: "Número IMEI",   message: "Mensagem", exit: "Sair",
    selSizeStr: "Verificar", sizeStr: "Gravações", createTime: "Data da Criação",   device: "Rastreador", purchase: "Comprado", stock: "Estoque",
    mobileWebsite: "Acesso via celular", deviceName: "Nome do Rastreador", simNo: "No. SIM Card", expireTime: "Válido até", remark: "Observação",  deviceInfo: "Informação do rastreador",  
    dataError: "Erro de Dados!", softFailed: "Erro!", password: "Senha", 
     searchNull: "IMEI/Nome/Cat No./Nome/Credencial requerida!", initPass: "Limpar Senha", updateTime: "Tempo de atualização", loginAccount: "Conta de Acesso", passLengthMsg: "A senha deve conter menos que 20 digitos",  userType1: "Usuário Final",
    add: "Novo", information: "Informação", deviceType: "Tipo",  kmHour: "Km/h", devicePhone: "No. SIM Card", 
    deletes: "Apagar", cusName: "Nome do Cliente", userInfo: "Informação do Cliente", plsSelUser: "Por favor, selecione um usuário!",
    km: "Quilômetro", plsSel: "Selecionar", all: "Todos", online: "Conectado", offline: "Desconectado", arrears: "em aberto",  speedLimit: "Limite de Velocidade", 
    allDistance: "Total percorrido",  deliveryTime: "Data da Ativação",
    cellPhone: "Tel/Cel", type2: "Tipo", view: "Ver", deviceHireDay: "the points to charge", service: "My Service", myDevice: "My Device", StatisticalAnalysis: "análise estatística"
};
//Dealer.aspx
var dealerPage = {
    warnTitle: "Lembrar o Alarme", warnSound: "Ativar som do Alarme", username: "Nome do Cliente/Conta", hello: "Olá", changePassword: "Alterar Senha", searchDevice: "Rastreador", searchUser: "Cliente",
    searchDevice2: "Verificar Rastreador", searchUser2: "Verificar Cliente", name: "Nome do Cliente", belongTo: "Membro", sim: "No. do SIM CARD", activeTime: "Data de ativação", customerName: "Nome do cliente", nowSearch: "Localizando...",
    name2: "Nome do Cliente", name3: "Nome do Rastreador"
};

//Home.aspx
var homePage = {
    quickSearch: "Procurar", searchTxt: "No. IMEI/T/No Veículo", stage: "Estado", deviceCount: "Número de Rastreadores", operations: "Mais", novice: "Novato", quickSale: "Venda Rápida",
    batchSale: "Venda Atacado", customer: "Cliente", sale: "Venda", customerList: "Lista de Clientes", addDevice: "Adicional", add: "+novo", imeiNullMsg: "Por favor, selecionar o Rastreador", expireTimeNullMsg: "Por favor, selecionar o data de Vencimento",
    saleSuccess: "Sucesso!"
};
 
var courseName = { dueNorth: "Norte", northeast: "Nordeste", dueEast: "Leste", southeast: "Sudeste", dueSouth: "Sul", southwest: "Sudoeste", dueWest: "Oeste", northwest: "Noroeste" };

var reportPage = { title: "Estatísticas Totais de Desclocamento", warnCount: "Alarme", stopCount: "Parada" };

var alarmSumPage = { title: "Estatísticas Totais de Alarmes", lowCount: "Alarme de Bateria Fraca", cutPowerCount: "Alarme de Corte", vibCount: "Alarme de Vibração", sosCount: "Alarme de SOS" }

var overSpeedPage = { continueTime: "Tempo Contínuo", speedlimit: "Limite de Velocidade", distancePage: "Relatório de Quilometragem", overspeedDetail: "Detalhes de Alta Velocidade", stopDetail: "Detalhes de Parada" };

var alarmIndexPage = { geofenceIn: "Dentro da Geo-cerca", geofenceOut: "Fora da Geo-cerca", moved: "Deslocamento", lowBattery: "Alarme de Bateria Baixa", sos: "Alarme de SOS", cutPower: "Alarme de Corte", vibration: "Alarme de Vibração",
    overSpeed: "Acima da Velocidade"
};
var productUpdatePage = { oilCoefficient: "Combustível/100 km", updateIcon: "Ícone", carNumMsg1: "No. Veículo é muito extenso!", sccuess: "Atualizado com Sucesso!", faild: "Falha ao Atualizar!", isExistMsg: "No. do Veículo já existe, por favor tentar outro!", filterLBS: "filter LBS" };

var runindexPage = { statistics: "Constuda por", statistics2: "Detalhes Diários", oilCoefficient: "Coeficiente de Consumo de Combusível/100 Quilômetros", L: "L", oil: "Consumo de Combustível" };

var alarmDetailPage = { alarmTime: "Tempo do Alarme", alarmType: "Tipo do Alarme", alarmCount: "Estatísticas do Alarme", alarmDetail: "Detalhes do Alarme" };

var userPage = { warnTitle: "Visão Geral do Alarme", warnSound: "Ativar Som do Alarme", day7Exp: "Expirado em 7 dias", day60Exp: "Expirado em 60 dias", alreadyExp: "Expirado",
    username: "Nome/Conta", hello: "Olá", searchDevice: "Rastreador", searchUser: "Cliente", exit: "Sair", message: "Mensagem", allDeivce: "Todos Rasteradores", moneyMove: "点数转让", moneyHistory: "点数消费记录"
};

var userInfoPage = { myAccount: "Minha conta", changePassword: "Alterar Senha", userMsg: "Dica: Complete as informações como o nº de telefone, contato.", customerName: "Nome do Cliente",
    account: "Conta de Acesso", oldPass: "Senha Existente", newPass: "Nova Senha", confirmPass: "Confirmação de Senha", passLengthMsg: "A senha deve ser menos que 20 digitos", passNull: "A senha não pode ser vazia!",
    passError: "As senhas não conferem!", changePassSuccess: "Senha limpa com sucesso!", changePassError: "Falha na limpeza de senha!", oldPassError: "Erro na senha antiga!", warnSendMsg: "Modo de informação adicional de alarme",
    sendEmail: "E-mail", service: "Meu Serviço"
};

var warnMessagePage = { warnMsg: "Messagem de Alarme", handle1: "Não negociado", handle2: "Negociado", alarmType: "Tipo do Alarme", alarmTime: "Tempo do Alarme" };

var trackingPage = { secondMsg: "Atualizar após segundos!" };

var playbackPage = { from: "De", to: "Para", play: "Reproduzir", pause: "Pausar", next: "Seguir", fast: "Rápido", slow: "Lento", timeMsg: "Data Final deve ser mais antiga que Data Continuar", nowLoading: "Caregando dados!",
    playOver: "Finalizado!", searchNull: "Sem dados!", showLBS: "Mostrar LBS"
};

var geofencesPage = { geofence: "Geo-Cerca", addGeofence: "Adicionar", geoNameNull: "O nome da Geo-cerca não pode ser vazio!", radius: "R(m)", delGeoConfirm: "Confirma para apagar", delGeoConfirm2: "Está é a Geo-cerca?",
    addSuccess: "Adicionado com Sucesso!", addFaild: "Falha ao Adicionar!"
};

var iframeMapPage = { baiduMap: "Mapa Baidu", googleMap: "Mapa Google", deviceName: "Nome do Rastreador" };

var userUpdatePage = { account: "Conta" };

//map.aspx
var mapPage = { divicesInfo: "Informação do Rastreador", cutOffPetrol: "Cortar Combustível", restorePetrol: "Ativar Combustível", checkLocation: "Verificar Localização", checkCommand: "Verificar Comando", downloadLocation: "Relatório de Rastreamento" };

var downloadLocation = { download: "Baixar", help: "Ajuda", step: "Passos", step1: "1.Selecionar o Rastreador.", step2: "2.Entrar com a data.", step3: "3.Clique 'Baixar'.",
    msg1: "Nota: Se você receber 'Sem Dados!' Significa que não existe dados registrados para o período selecionado.", msg2: "O formato do download é Google KLM, ex: 'nome do arquivo.kml'.",
    msg3: "Duplo clique no arquivo KLM para abrí-lo e em seguida instalar o Google Earth.", msg4: "O aquivo KLM traçará a rota dinamicamente como uma linha vermelha no Google maps.", msg5: "Dicas: Clique em baixar Google Earth.", here: "aqui",
    msg6: "Você pode baixar o arquivo com a rota traçada para um determinado período, através da função 'baixar arquivo traçado'. Tipo de arquivo traçado 'Arquivo KML Traçado' e 'Arquivo KML Anchor'"
};
var cusPage = {
    delUserConfirm: "Confirma para apagar", delUserConfirm2: "É usuario?", delUserMsg: "Esta conta tem rastreador registrado. Não pode ser apagada!", delUserMsg2: "Esta conta tem sub-conta. Não pode ser apagada!", msgFailed: "Falha ao apagar!",
    updateUserSuccess: "Sucesso!", updateUserFailed: "Falha!", delDeviceConfirm2: "Este é um rastreador?", addCus: "Adicioinar Cliente", manCus: "Gerenciar Cliente", delCus: "Apagar Cliente", saleTo: "Mover Cliente",
    beDevice: "Membro", updateExpTime: "Atualizar Tempo de Expiração", deviceChange: "Mover", cusInfo: "Informação do Cliente", toCus: "Clientes com Rastreador", newAddChildrenCus: "Adicioinar Clientes", parentCus: "Cliente Master",
    confirmPass: "Confirmação de Senha", allCus: "Todos os Clientes", addCusTitle: "Adicionar Cliente", loginToUser: "Monitorar", childCus: "Sub-conta", changeDevices: "Mover Atacado", updateExpDevices: "Atualizar Atacado",
    addUserMsg: "Dica: Complete a informação com o número do telefone, contato.", plsParentCusMsg: "Por favor, selecione o cliente master!", msg1: "Nome, Conta, Senha não podem ser vazio!", passError: "As duas senhas digitadas são diferentes, por favor, digite novamente!",
    existAccount: "A conta já existe!", dataError: "Dados nulos!", dataNull: "Nome, Conta, Senha não podem ser vazio!", imeisNull: "No. IMEI não pode ser vazio!", saveDevuceMsg1: "Entrada junta:", saveDevuceMsg2: "Grupo de No. IMEI",
    saveDevuceMsg3: "Sucesso!", saveDevuceMsg4: "Sucesso:", saveDevuceMsg5: "Grupo", saveDevuceMsg6: "Falha:", saveDevuceMsg7: "Erro!", updateExpSuccess: "Sucesso!",
    updateError: "Editar falha!", changeDeviceSuccess: "Sucesso!", changeDeviceError: "Falha ao mover o Rastreador!", confirmInitPassMsg1: "Você tem certeza que quer resetar?", confirmInitPassMsg2: "Senha?:",
    initPassSuccess: "Senha limpa com sucesso!", initPassError: "Falha ao limpar a senha!", confirmInitUserPassMsg1: "Você realmente deseja limpar a conta?", page: "Page", records: "Records"
};


var moneyPage = { moveToAccount: "the login account to transfer into", moveToUser: "the username to transfer into", moveCount: "the points to transfer into", check: "detect", noLoginName: "this name does not exist!", inputLoginNameIn: "please test the login name!",
    inputLoginNameOut: "please test the login name!", noMoveSelf: "can not transfer to oneself!", moneyLack: "the point is not enough!", moneyError: "fail to transfer!", moveSuccess: "transfer the points successfully, the current points is:", inputMoneyMsg: "please input the points!",
    uToUser: "for the user", money: "point", user: "user", moveMoneyMsg1: "the points been transferred ", give: "for", moneyMsg2: "charge for the device, the total points used", day: "day", msg1: "1 point for 1 day", oneYeah: "one year", twoYeah: "two years",
    lifelong: "lifelong", msg2: "Extend the maturity time", msg3: "the device is charged successfully!", msg4: "the points is not enough, please charge!!", msg5: "days must be greater then 0!", pointManagement: "points management"
};

var yiwen201312 = { monthCard: "Month card", yearCard: "Year card", lifeLong: "Lifelong card", monthCardnod: "your account month card not enough!", yearCardnod: "your account year card not enough!", lifelongCardnod: "your account lifelong card not enough!",
    oneMonth: "one Month", reManagement: "Recharge Management", cardTransfer: "Credit Card Transfer", cardMsg1: "Select type of rechargeable card", cardMsg2: "Input amounts of rechargeable card", cardMsg3: "rechargeable card not enough", cardMsg4: "Transferred successfully,the left is",
    cardMsg5: "Input amounts of rechargeable card!", cardMsg6: "transfer to Month card", cardMsg7: "Transfer to your Month card", cardMsg8: "Transfer to Year card", cardMsg9: "Transfer to your Year card", cardMsg10: "Transfer to Lifelong card", cardMsg11: "Transfer to your Lifelong card",
    cardMsg12: "pcs of devices using Month card", cardMsg13: "pcs of devices using Year card", cardMsg14: "pcs of devices using Lifelong card", cardMsg15: "", cardMsg16: "Sent one sms", accOn: "ACC ON", accOff: "ACC OFF", fortify: "ARM", dismiss: "Disarm", carOpen: "Door Open", carClose: "Door Close", zdlj: "Power connected",
    zddk: "Power cut"
}

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
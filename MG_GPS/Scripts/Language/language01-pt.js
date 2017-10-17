function writePage(msg) {
    //葡萄牙
    document.write(msg);
}

var allPage = { deviceNo: "Número IMEI", search: "Procurar", tracking: "Rastrear", playback: "Reproduzir", monitor: "Monitorar", home: "Início", customer: "Negócios", message: "Mensagem", exit: "Sair",
    selSizeStr: "Verificar", sizeStr: "Gravações", createTime: "Data da Criação", operation: "Operação", no: "No.", cellName: "Contatos", phone: "Te/Cel", device: "Rastreador", purchase: "Comprado", stock: "Estoque",
    mobileWebsite: "Acesso via celular", deviceName: "Nome do Rastreador", simNo: "No. SIM Card", expireTime: "Válido até", remark: "Observação", cancel: "Cancelar", deviceInfo: "Informação do rastreador", imeiNo: "Número do IMEI",
    dataError: "Erro de Dados!", softFailed: "Erro!", password: "Senha", confirm: "Salvar", num: "No.", noData: "Sem Dados!", acc0: "Desligado", acc1: "Ligado",
    acc2: "Desconenctado", searchNull: "IMEI/Nome/Cat No./Nome/Credencial requerida!", initPass: "Limpar Senha", updateTime: "Tempo de atualização", loginAccount: "Conta de Acesso", passLengthMsg: "A senha deve conter menos que 20 digitos", type: "Tipo", userType1: "Usuário Final",
    userType2: "Distribuidor", address: "Endereço", add: "Novo", information: "Informação", deviceType: "Tipo", overspeed: "Acima da Velocidade", kmHour: "Km/h", devicePhone: "No. SIM Card", modelName: "Tipo",
    createTime: "Data da Criação", activeTime: "Data da Ativação", hireExpireTime: "Válido atéVálido at", edit: "Editar", more: "Mais", deletes: "Apagar", cusName: "Nome do Cliente", userInfo: "Informação do Cliente", plsSelUser: "Por favor, selecione um usuário!",
    km: "Quilômetro", plsSel: "Selecionar", all: "Todos", online: "Conectado", offline: "Desconectado", arrears: "em aberto", carNum: "No. da Placa", speedLimit: "Limite de Velocidade", lat: "Latitude", lng: "Longitude", speed: "Velocidade", drection: "Direção",
    allDistance: "Total percorrido", state: "Situação", positionTime: "Tempo da Posição", status1: "Desconectado", moving: "Movendo", stopCar: "Parado", accStr: "Situação ACC", edit2: "Editar", deliveryTime: "Data da Ativação",
    cellPhone: "Tel/Cel", save: "Salvar", clear: "Limpar", type2: "Tipo", view: "Ver", deviceHireDay: "the points to charge", service: "My Service", myDevice: "My Device"
};

//login.aspx
var loginPage = {
    title: "Estação Rastreando GPS", accountTab: " Conta ", imeiTab: "telefones/Conta/IMEI", account: "Conta", password: "Senha", loginSubmit: "Acessar", languageMsg: "Oferecemos em", loginMsg: "Digite conta.",
    loginMsg2: "Por favor, insira sua senha.", loginMsg3: "Input cannot be blank!", wytx: "I want try", androidBaidu: "android(Baidu map) Download", androidGoogle: "android(Google map) Download", iphone: "iphone jailbreak Download",
    welcome: "Bem-vindo, por favor acessar!", loginErrorMsg: "Conta ou senha de erro.", loginFailure: "falha no login.", ForgotPassword: "Esqueceu sua senha?", RememberPassword: "Lembre-se de senha.", Register: "Registrar"
};


//Dealer.aspx
var dealerPage = { warnTitle: "Lembrar o Alarme", warnSound: "Ativar som do Alarme", username: "Nome do Cliente/Conta", hello: "Olá", changePassword: "Alterar Senha", searchDevice: "Rastreador", searchUser: "Cliente",
    searchDevice2: "Verificar Rastreador", searchUser2: "Verificar Cliente", name: "Nome do Cliente", belongTo: "Membro", sim: "No. do SIM CARD", activeTime: "Data de ativação", customerName: "Nome do cliente", nowSearch: "Localizando...",
    name2: "Nome do Cliente", name3: "Nome do Rastreador"
};

//Home.aspx
var homePage = { quickSearch: "Procurar", searchTxt: "No. IMEI/T/No Veículo", stage: "Estado", deviceCount: "Número de Rastreadores", operations: "Mais", novice: "Novato", quickSale: "Venda Rápida",
    batchSale: "Venda Atacado", customer: "Cliente", sale: "Venda", customerList: "Lista de Clientes", addDevice: "Adicional", add: "+novo", imeiNullMsg: "Por favor, selecionar o Rastreador", expireTimeNullMsg: "Por favor, selecionar o data de Vencimento",
    saleSuccess: "Sucesso!"
};

var warnMessagePage = { alarmType: "Tipo do Alarme", alarmTime: "Tempo do Alarme" };

var alarmIndexPage = { geofenceIn: "Dentro da Geo-cerca", geofenceOut: "Fora da Geo-cerca", moved: "Deslocamento", lowBattery: "Alarme de Bateria Baixa", sos: "Alarme de SOS", cutPower: "Alarme de Corte", vibration: "Alarme de Vibração",
    overSpeed: "Acima da Velocidade"
};

//map.aspx
var mapPage = { searchInput: 'Por favor entre com o nome/No. IMEI', divicesInfo: "Informação do Rastreador", geofence: "Geo-cerca", cutOffPetrol: "Cortar Combustível", restorePetrol: "Ativar Combustível", checkLocation: "Verificar Localização", checkCommand: "Verificar Comando",
    sendConfirm: "Por favor, confirme a senha antes de enviar o comando", passNull: "Por favor, entre com a senha!", passError: "Senha Inválida!", sendMsg1: "Enviando mensagem, por favor aguarde...", sendSuccess: "Sucesso! Por favor aguarde pela resposta...",
    sendMsg2: "Comando nulo", sendMsg3: "Rastreador inexistente", sendMsg4: "Por favor, faça o equipamento conectado (on-line) antes.", sendMsg5: "Enviar comando com sucesso", responseSuccess: "Resposta com Sucesso!", responseNull: "Resposta Nula!",
    checkLocatoin: "Verificar Localização", checkCommandTitle: "Verificar Comando", cmdType: "Tipo de Comando", cmdState: "Estado do Comando", responseText: "Resposta da Mensagem", responseTime: "Tempo de Resposta", sendTime: "Enviar Tempo",
    dyd: "Cortar Combustível", hfyd: "Ativar Combustível", deviceResponse: "Resposta do Rastreador", sendSuccess2: "Comando enviado!", noSend: "Comando não enviado!", deviceDetailList: "Lista de Detalhes do Rastreador", addGroup: "Adicionar Grupo", defaultGroup: "Padrão",
    moveToGroup: "Mover Grupo", delGroupConfirm: "Confirma apagar?", downloadLocation: "Relatório de Rastreamento"
};

var courseName = { dueNorth: "Norte", northeast: "Nordeste", dueEast: "Leste", southeast: "Sudeste", dueSouth: "Sul", southwest: "Sudoeste", dueWest: "Oeste", northwest: "Noroeste" };

//CustomersList.aspx
var cusPage = { delUserConfirm: "Confirma para apagar", delUserConfirm2: "É usuario?", delUserMsg: "Esta conta tem rastreador registrado. Não pode ser apagada!", delUserMsg2: "Esta conta tem sub-conta. Não pode ser apagada!", msgFailed: "Falha ao apagar!",
    updateUserSuccess: "Sucesso!", updateUserFailed: "Falha!", delDeviceConfirm2: "Este é um rastreador?", addCus: "Adicioinar Cliente", manCus: "Gerenciar Cliente", delCus: "Apagar Cliente", saleTo: "Mover Cliente",
    beDevice: "Membro", updateExpTime: "Atualizar Tempo de Expiração", deviceChange: "Mover", cusInfo: "Informação do Cliente", toCus: "Clientes com Rastreador", newAddChildrenCus: "Adicioinar Clientes", parentCus: "Cliente Master",
    confirmPass: "Confirmação de Senha", allCus: "Todos os Clientes", addCusTitle: "Adicionar Cliente", loginToUser: "Monitorar", childCus: "Sub-conta", changeDevices: "Mover Atacado", updateExpDevices: "Atualizar Atacado",
    addUserMsg: "Dica: Complete a informação com o número do telefone, contato.", plsParentCusMsg: "Por favor, selecione o cliente master!", msg1: "Nome, Conta, Senha não podem ser vazio!", passError: "As duas senhas digitadas são diferentes, por favor, digite novamente!",
    existAccount: "A conta já existe!", dataError: "Dados nulos!", dataNull: "Nome, Conta, Senha não podem ser vazio!", imeisNull: "No. IMEI não pode ser vazio!", saveDevuceMsg1: "Entrada junta:", saveDevuceMsg2: "Grupo de No. IMEI",
    saveDevuceMsg3: "Sucesso!", saveDevuceMsg4: "Sucesso:", saveDevuceMsg5: "Grupo", saveDevuceMsg6: "Falha:", saveDevuceMsg7: "Erro!", updateExpSuccess: "Sucesso!",
    updateError: "Editar falha!", changeDeviceSuccess: "Sucesso!", changeDeviceError: "Falha ao mover o Rastreador!", confirmInitPassMsg1: "Você tem certeza que quer resetar?", confirmInitPassMsg2: "Senha?:",
    initPassSuccess: "Senha limpa com sucesso!", initPassError: "Falha ao limpar a senha!", confirmInitUserPassMsg1: "Você realmente deseja limpar a conta?", page: "Page", records: "Records"
};

var productUpdatePage = { oilCoefficient: "Combustível/100 km", updateIcon: "Ícone", carNumMsg1: "No. Veículo é muito extenso!", sccuess: "Atualizado com Sucesso!", faild: "Falha ao Atualizar!", isExistMsg: "No. do Veículo já existe, por favor tentar outro!", filterLBS: "filter LBS" };
var moneyPage = { day: "day", msg1: "1 point for 1 day", oneYeah: "one year", twoYeah: "two years", lifelong: "lifelong", msg2: "Extend the maturity time", msg3: "the device is charged successfully!", msg4: "the points is not enough, please charge!!", msg5: "days must be greater then 0!", msg6: "can not transfer to oneself!", pointGive: "points for sale", pointConsumptionLog: "points consumer records", loginNameNotExist: "this name does not exist", intoLoginID: "the login account to transfer into", intoUsername: "the username to transfer into", intoPoint: "the points to transfer into", detect: "detect", pleaseDetectLoginName: "please test the login name", pleaseInputIntoPoint: "please input the points" };

var yiwen201312 = { oneMonth: "one month", monthCardnod: "your account month card not enough!", yearCardnod: "your account year card not enough!", lifelongCardnod: "your account lifelong card not enough!", searchDeviceMsg1: "Already listed in the transferred List", searchDeviceMsg2: "The searching result is not specific,please input more details", accOn: "ACC ON", accOff: "ACC OFF", fortify: "ARM", dismiss: "Disarm", carOpen: "Door Open", carClose: "Door Close", zdlj: "Power connected",
    zddk: "Power cut", uploadInterval: "Upload Interval", msg1: "Set device Upload Interval", msg2: "Unit:Seconds", msg3: "Not less than 15 seconds!", msg4: "Can not be greater than 9999 seconds!"
}

var yiwen201405 = { battery: "Battery Level" };
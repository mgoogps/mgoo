﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace MgooGps.CommandQueue {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CommandQueue.CommandQueueAjaxSoap")]
    public interface CommandQueueAjaxSoap {
        
        // CODEGEN: 命名空间 http://tempuri.org/ 的元素名称 SN 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SendCommand", ReplyAction="*")]
        MgooGps.CommandQueue.SendCommandResponse SendCommand(MgooGps.CommandQueue.SendCommandRequest request);
        
        // CODEGEN: 命名空间 http://tempuri.org/ 的元素名称 SN 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SendCommandByPhone", ReplyAction="*")]
        MgooGps.CommandQueue.SendCommandByPhoneResponse SendCommandByPhone(MgooGps.CommandQueue.SendCommandByPhoneRequest request);
        
        // CODEGEN: 命名空间 http://tempuri.org/ 的元素名称 SN 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/SendCommandMD", ReplyAction="*")]
        MgooGps.CommandQueue.SendCommandMDResponse SendCommandMD(MgooGps.CommandQueue.SendCommandMDRequest request);
        
        // CODEGEN: 命名空间 http://tempuri.org/ 的元素名称 TimeZones 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetResponse", ReplyAction="*")]
        MgooGps.CommandQueue.GetResponseResponse GetResponse(MgooGps.CommandQueue.GetResponseRequest request);
        
        // CODEGEN: 命名空间 http://tempuri.org/ 的元素名称 SN 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetCommandList", ReplyAction="*")]
        MgooGps.CommandQueue.GetCommandListResponse GetCommandList(MgooGps.CommandQueue.GetCommandListRequest request);
        
        // CODEGEN: 命名空间 http://tempuri.org/ 的元素名称 testResult 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/test", ReplyAction="*")]
        MgooGps.CommandQueue.testResponse test(MgooGps.CommandQueue.testRequest request);
        
        // CODEGEN: 命名空间 http://tempuri.org/ 的元素名称 SN 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/sendPhoto", ReplyAction="*")]
        MgooGps.CommandQueue.sendPhotoResponse sendPhoto(MgooGps.CommandQueue.sendPhotoRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SendCommandRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SendCommand", Namespace="http://tempuri.org/", Order=0)]
        public MgooGps.CommandQueue.SendCommandRequestBody Body;
        
        public SendCommandRequest() {
        }
        
        public SendCommandRequest(MgooGps.CommandQueue.SendCommandRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class SendCommandRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string SN;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int DeviceID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string CommandType;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string TrueOrFalse;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public int Model;
        
        public SendCommandRequestBody() {
        }
        
        public SendCommandRequestBody(string SN, int DeviceID, string CommandType, string TrueOrFalse, int Model) {
            this.SN = SN;
            this.DeviceID = DeviceID;
            this.CommandType = CommandType;
            this.TrueOrFalse = TrueOrFalse;
            this.Model = Model;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SendCommandResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SendCommandResponse", Namespace="http://tempuri.org/", Order=0)]
        public MgooGps.CommandQueue.SendCommandResponseBody Body;
        
        public SendCommandResponse() {
        }
        
        public SendCommandResponse(MgooGps.CommandQueue.SendCommandResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class SendCommandResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int SendCommandResult;
        
        public SendCommandResponseBody() {
        }
        
        public SendCommandResponseBody(int SendCommandResult) {
            this.SendCommandResult = SendCommandResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SendCommandByPhoneRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SendCommandByPhone", Namespace="http://tempuri.org/", Order=0)]
        public MgooGps.CommandQueue.SendCommandByPhoneRequestBody Body;
        
        public SendCommandByPhoneRequest() {
        }
        
        public SendCommandByPhoneRequest(MgooGps.CommandQueue.SendCommandByPhoneRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class SendCommandByPhoneRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string SN;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int DeviceID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string CommandType;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public int Model;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string Phones;
        
        public SendCommandByPhoneRequestBody() {
        }
        
        public SendCommandByPhoneRequestBody(string SN, int DeviceID, string CommandType, int Model, string Phones) {
            this.SN = SN;
            this.DeviceID = DeviceID;
            this.CommandType = CommandType;
            this.Model = Model;
            this.Phones = Phones;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SendCommandByPhoneResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SendCommandByPhoneResponse", Namespace="http://tempuri.org/", Order=0)]
        public MgooGps.CommandQueue.SendCommandByPhoneResponseBody Body;
        
        public SendCommandByPhoneResponse() {
        }
        
        public SendCommandByPhoneResponse(MgooGps.CommandQueue.SendCommandByPhoneResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class SendCommandByPhoneResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int SendCommandByPhoneResult;
        
        public SendCommandByPhoneResponseBody() {
        }
        
        public SendCommandByPhoneResponseBody(int SendCommandByPhoneResult) {
            this.SendCommandByPhoneResult = SendCommandByPhoneResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SendCommandMDRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SendCommandMD", Namespace="http://tempuri.org/", Order=0)]
        public MgooGps.CommandQueue.SendCommandMDRequestBody Body;
        
        public SendCommandMDRequest() {
        }
        
        public SendCommandMDRequest(MgooGps.CommandQueue.SendCommandMDRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class SendCommandMDRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string SN;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int DeviceID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string CommandType;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string TrueOrFalse;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public int Model;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public int Port;
        
        public SendCommandMDRequestBody() {
        }
        
        public SendCommandMDRequestBody(string SN, int DeviceID, string CommandType, string TrueOrFalse, int Model, int Port) {
            this.SN = SN;
            this.DeviceID = DeviceID;
            this.CommandType = CommandType;
            this.TrueOrFalse = TrueOrFalse;
            this.Model = Model;
            this.Port = Port;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SendCommandMDResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SendCommandMDResponse", Namespace="http://tempuri.org/", Order=0)]
        public MgooGps.CommandQueue.SendCommandMDResponseBody Body;
        
        public SendCommandMDResponse() {
        }
        
        public SendCommandMDResponse(MgooGps.CommandQueue.SendCommandMDResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class SendCommandMDResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int SendCommandMDResult;
        
        public SendCommandMDResponseBody() {
        }
        
        public SendCommandMDResponseBody(int SendCommandMDResult) {
            this.SendCommandMDResult = SendCommandMDResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetResponseRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetResponse", Namespace="http://tempuri.org/", Order=0)]
        public MgooGps.CommandQueue.GetResponseRequestBody Body;
        
        public GetResponseRequest() {
        }
        
        public GetResponseRequest(MgooGps.CommandQueue.GetResponseRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetResponseRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int CommandID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string TimeZones;
        
        public GetResponseRequestBody() {
        }
        
        public GetResponseRequestBody(int CommandID, string TimeZones) {
            this.CommandID = CommandID;
            this.TimeZones = TimeZones;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetResponseResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetResponseResponse", Namespace="http://tempuri.org/", Order=0)]
        public MgooGps.CommandQueue.GetResponseResponseBody Body;
        
        public GetResponseResponse() {
        }
        
        public GetResponseResponse(MgooGps.CommandQueue.GetResponseResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetResponseResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetResponseResult;
        
        public GetResponseResponseBody() {
        }
        
        public GetResponseResponseBody(string GetResponseResult) {
            this.GetResponseResult = GetResponseResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetCommandListRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetCommandList", Namespace="http://tempuri.org/", Order=0)]
        public MgooGps.CommandQueue.GetCommandListRequestBody Body;
        
        public GetCommandListRequest() {
        }
        
        public GetCommandListRequest(MgooGps.CommandQueue.GetCommandListRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetCommandListRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string SN;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int DeviceID;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public int PageNo;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public int PageCount;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string TimeZones;
        
        public GetCommandListRequestBody() {
        }
        
        public GetCommandListRequestBody(string SN, int DeviceID, int PageNo, int PageCount, string TimeZones) {
            this.SN = SN;
            this.DeviceID = DeviceID;
            this.PageNo = PageNo;
            this.PageCount = PageCount;
            this.TimeZones = TimeZones;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetCommandListResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetCommandListResponse", Namespace="http://tempuri.org/", Order=0)]
        public MgooGps.CommandQueue.GetCommandListResponseBody Body;
        
        public GetCommandListResponse() {
        }
        
        public GetCommandListResponse(MgooGps.CommandQueue.GetCommandListResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetCommandListResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetCommandListResult;
        
        public GetCommandListResponseBody() {
        }
        
        public GetCommandListResponseBody(string GetCommandListResult) {
            this.GetCommandListResult = GetCommandListResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class testRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="test", Namespace="http://tempuri.org/", Order=0)]
        public MgooGps.CommandQueue.testRequestBody Body;
        
        public testRequest() {
        }
        
        public testRequest(MgooGps.CommandQueue.testRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class testRequestBody {
        
        public testRequestBody() {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class testResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="testResponse", Namespace="http://tempuri.org/", Order=0)]
        public MgooGps.CommandQueue.testResponseBody Body;
        
        public testResponse() {
        }
        
        public testResponse(MgooGps.CommandQueue.testResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class testResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string testResult;
        
        public testResponseBody() {
        }
        
        public testResponseBody(string testResult) {
            this.testResult = testResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class sendPhotoRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="sendPhoto", Namespace="http://tempuri.org/", Order=0)]
        public MgooGps.CommandQueue.sendPhotoRequestBody Body;
        
        public sendPhotoRequest() {
        }
        
        public sendPhotoRequest(MgooGps.CommandQueue.sendPhotoRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class sendPhotoRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string SN;
        
        public sendPhotoRequestBody() {
        }
        
        public sendPhotoRequestBody(string SN) {
            this.SN = SN;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class sendPhotoResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="sendPhotoResponse", Namespace="http://tempuri.org/", Order=0)]
        public MgooGps.CommandQueue.sendPhotoResponseBody Body;
        
        public sendPhotoResponse() {
        }
        
        public sendPhotoResponse(MgooGps.CommandQueue.sendPhotoResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class sendPhotoResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string sendPhotoResult;
        
        public sendPhotoResponseBody() {
        }
        
        public sendPhotoResponseBody(string sendPhotoResult) {
            this.sendPhotoResult = sendPhotoResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface CommandQueueAjaxSoapChannel : MgooGps.CommandQueue.CommandQueueAjaxSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CommandQueueAjaxSoapClient : System.ServiceModel.ClientBase<MgooGps.CommandQueue.CommandQueueAjaxSoap>, MgooGps.CommandQueue.CommandQueueAjaxSoap {
        
        public CommandQueueAjaxSoapClient() {
        }
        
        public CommandQueueAjaxSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CommandQueueAjaxSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CommandQueueAjaxSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CommandQueueAjaxSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        MgooGps.CommandQueue.SendCommandResponse MgooGps.CommandQueue.CommandQueueAjaxSoap.SendCommand(MgooGps.CommandQueue.SendCommandRequest request) {
            return base.Channel.SendCommand(request);
        }
        
        public int SendCommand(string SN, int DeviceID, string CommandType, string TrueOrFalse, int Model) {
            MgooGps.CommandQueue.SendCommandRequest inValue = new MgooGps.CommandQueue.SendCommandRequest();
            inValue.Body = new MgooGps.CommandQueue.SendCommandRequestBody();
            inValue.Body.SN = SN;
            inValue.Body.DeviceID = DeviceID;
            inValue.Body.CommandType = CommandType;
            inValue.Body.TrueOrFalse = TrueOrFalse;
            inValue.Body.Model = Model;
            MgooGps.CommandQueue.SendCommandResponse retVal = ((MgooGps.CommandQueue.CommandQueueAjaxSoap)(this)).SendCommand(inValue);
            return retVal.Body.SendCommandResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        MgooGps.CommandQueue.SendCommandByPhoneResponse MgooGps.CommandQueue.CommandQueueAjaxSoap.SendCommandByPhone(MgooGps.CommandQueue.SendCommandByPhoneRequest request) {
            return base.Channel.SendCommandByPhone(request);
        }
        
        public int SendCommandByPhone(string SN, int DeviceID, string CommandType, int Model, string Phones) {
            MgooGps.CommandQueue.SendCommandByPhoneRequest inValue = new MgooGps.CommandQueue.SendCommandByPhoneRequest();
            inValue.Body = new MgooGps.CommandQueue.SendCommandByPhoneRequestBody();
            inValue.Body.SN = SN;
            inValue.Body.DeviceID = DeviceID;
            inValue.Body.CommandType = CommandType;
            inValue.Body.Model = Model;
            inValue.Body.Phones = Phones;
            MgooGps.CommandQueue.SendCommandByPhoneResponse retVal = ((MgooGps.CommandQueue.CommandQueueAjaxSoap)(this)).SendCommandByPhone(inValue);
            return retVal.Body.SendCommandByPhoneResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        MgooGps.CommandQueue.SendCommandMDResponse MgooGps.CommandQueue.CommandQueueAjaxSoap.SendCommandMD(MgooGps.CommandQueue.SendCommandMDRequest request) {
            return base.Channel.SendCommandMD(request);
        }
        
        public int SendCommandMD(string SN, int DeviceID, string CommandType, string TrueOrFalse, int Model, int Port) {
            MgooGps.CommandQueue.SendCommandMDRequest inValue = new MgooGps.CommandQueue.SendCommandMDRequest();
            inValue.Body = new MgooGps.CommandQueue.SendCommandMDRequestBody();
            inValue.Body.SN = SN;
            inValue.Body.DeviceID = DeviceID;
            inValue.Body.CommandType = CommandType;
            inValue.Body.TrueOrFalse = TrueOrFalse;
            inValue.Body.Model = Model;
            inValue.Body.Port = Port;
            MgooGps.CommandQueue.SendCommandMDResponse retVal = ((MgooGps.CommandQueue.CommandQueueAjaxSoap)(this)).SendCommandMD(inValue);
            return retVal.Body.SendCommandMDResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        MgooGps.CommandQueue.GetResponseResponse MgooGps.CommandQueue.CommandQueueAjaxSoap.GetResponse(MgooGps.CommandQueue.GetResponseRequest request) {
            return base.Channel.GetResponse(request);
        }
        
        public string GetResponse(int CommandID, string TimeZones) {
            MgooGps.CommandQueue.GetResponseRequest inValue = new MgooGps.CommandQueue.GetResponseRequest();
            inValue.Body = new MgooGps.CommandQueue.GetResponseRequestBody();
            inValue.Body.CommandID = CommandID;
            inValue.Body.TimeZones = TimeZones;
            MgooGps.CommandQueue.GetResponseResponse retVal = ((MgooGps.CommandQueue.CommandQueueAjaxSoap)(this)).GetResponse(inValue);
            return retVal.Body.GetResponseResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        MgooGps.CommandQueue.GetCommandListResponse MgooGps.CommandQueue.CommandQueueAjaxSoap.GetCommandList(MgooGps.CommandQueue.GetCommandListRequest request) {
            return base.Channel.GetCommandList(request);
        }
        
        public string GetCommandList(string SN, int DeviceID, int PageNo, int PageCount, string TimeZones) {
            MgooGps.CommandQueue.GetCommandListRequest inValue = new MgooGps.CommandQueue.GetCommandListRequest();
            inValue.Body = new MgooGps.CommandQueue.GetCommandListRequestBody();
            inValue.Body.SN = SN;
            inValue.Body.DeviceID = DeviceID;
            inValue.Body.PageNo = PageNo;
            inValue.Body.PageCount = PageCount;
            inValue.Body.TimeZones = TimeZones;
            MgooGps.CommandQueue.GetCommandListResponse retVal = ((MgooGps.CommandQueue.CommandQueueAjaxSoap)(this)).GetCommandList(inValue);
            return retVal.Body.GetCommandListResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        MgooGps.CommandQueue.testResponse MgooGps.CommandQueue.CommandQueueAjaxSoap.test(MgooGps.CommandQueue.testRequest request) {
            return base.Channel.test(request);
        }
        
        public string test() {
            MgooGps.CommandQueue.testRequest inValue = new MgooGps.CommandQueue.testRequest();
            inValue.Body = new MgooGps.CommandQueue.testRequestBody();
            MgooGps.CommandQueue.testResponse retVal = ((MgooGps.CommandQueue.CommandQueueAjaxSoap)(this)).test(inValue);
            return retVal.Body.testResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        MgooGps.CommandQueue.sendPhotoResponse MgooGps.CommandQueue.CommandQueueAjaxSoap.sendPhoto(MgooGps.CommandQueue.sendPhotoRequest request) {
            return base.Channel.sendPhoto(request);
        }
        
        public string sendPhoto(string SN) {
            MgooGps.CommandQueue.sendPhotoRequest inValue = new MgooGps.CommandQueue.sendPhotoRequest();
            inValue.Body = new MgooGps.CommandQueue.sendPhotoRequestBody();
            inValue.Body.SN = SN;
            MgooGps.CommandQueue.sendPhotoResponse retVal = ((MgooGps.CommandQueue.CommandQueueAjaxSoap)(this)).sendPhoto(inValue);
            return retVal.Body.sendPhotoResult;
        }
    }
}

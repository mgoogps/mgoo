﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ExpiredPush.POIService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="POIService.POIServiceSoap")]
    public interface POIServiceSoap {
        
        // CODEGEN: 命名空间 http://tempuri.org/ 的元素名称 MapType 以后生成的消息协定未标记为 nillable
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAddressByLatlng", ReplyAction="*")]
        ExpiredPush.POIService.GetAddressByLatlngResponse GetAddressByLatlng(ExpiredPush.POIService.GetAddressByLatlngRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetAddressByLatlng", ReplyAction="*")]
        System.Threading.Tasks.Task<ExpiredPush.POIService.GetAddressByLatlngResponse> GetAddressByLatlngAsync(ExpiredPush.POIService.GetAddressByLatlngRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetAddressByLatlngRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetAddressByLatlng", Namespace="http://tempuri.org/", Order=0)]
        public ExpiredPush.POIService.GetAddressByLatlngRequestBody Body;
        
        public GetAddressByLatlngRequest() {
        }
        
        public GetAddressByLatlngRequest(ExpiredPush.POIService.GetAddressByLatlngRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetAddressByLatlngRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public decimal Lat;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public decimal Lng;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string MapType;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string Language;
        
        public GetAddressByLatlngRequestBody() {
        }
        
        public GetAddressByLatlngRequestBody(decimal Lat, decimal Lng, string MapType, string Language) {
            this.Lat = Lat;
            this.Lng = Lng;
            this.MapType = MapType;
            this.Language = Language;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetAddressByLatlngResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetAddressByLatlngResponse", Namespace="http://tempuri.org/", Order=0)]
        public ExpiredPush.POIService.GetAddressByLatlngResponseBody Body;
        
        public GetAddressByLatlngResponse() {
        }
        
        public GetAddressByLatlngResponse(ExpiredPush.POIService.GetAddressByLatlngResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://tempuri.org/")]
    public partial class GetAddressByLatlngResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetAddressByLatlngResult;
        
        public GetAddressByLatlngResponseBody() {
        }
        
        public GetAddressByLatlngResponseBody(string GetAddressByLatlngResult) {
            this.GetAddressByLatlngResult = GetAddressByLatlngResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface POIServiceSoapChannel : ExpiredPush.POIService.POIServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class POIServiceSoapClient : System.ServiceModel.ClientBase<ExpiredPush.POIService.POIServiceSoap>, ExpiredPush.POIService.POIServiceSoap {
        
        public POIServiceSoapClient() {
        }
        
        public POIServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public POIServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public POIServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public POIServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ExpiredPush.POIService.GetAddressByLatlngResponse ExpiredPush.POIService.POIServiceSoap.GetAddressByLatlng(ExpiredPush.POIService.GetAddressByLatlngRequest request) {
            return base.Channel.GetAddressByLatlng(request);
        }
        
        public string GetAddressByLatlng(decimal Lat, decimal Lng, string MapType, string Language) {
            ExpiredPush.POIService.GetAddressByLatlngRequest inValue = new ExpiredPush.POIService.GetAddressByLatlngRequest();
            inValue.Body = new ExpiredPush.POIService.GetAddressByLatlngRequestBody();
            inValue.Body.Lat = Lat;
            inValue.Body.Lng = Lng;
            inValue.Body.MapType = MapType;
            inValue.Body.Language = Language;
            ExpiredPush.POIService.GetAddressByLatlngResponse retVal = ((ExpiredPush.POIService.POIServiceSoap)(this)).GetAddressByLatlng(inValue);
            return retVal.Body.GetAddressByLatlngResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ExpiredPush.POIService.GetAddressByLatlngResponse> ExpiredPush.POIService.POIServiceSoap.GetAddressByLatlngAsync(ExpiredPush.POIService.GetAddressByLatlngRequest request) {
            return base.Channel.GetAddressByLatlngAsync(request);
        }
        
        public System.Threading.Tasks.Task<ExpiredPush.POIService.GetAddressByLatlngResponse> GetAddressByLatlngAsync(decimal Lat, decimal Lng, string MapType, string Language) {
            ExpiredPush.POIService.GetAddressByLatlngRequest inValue = new ExpiredPush.POIService.GetAddressByLatlngRequest();
            inValue.Body = new ExpiredPush.POIService.GetAddressByLatlngRequestBody();
            inValue.Body.Lat = Lat;
            inValue.Body.Lng = Lng;
            inValue.Body.MapType = MapType;
            inValue.Body.Language = Language;
            return ((ExpiredPush.POIService.POIServiceSoap)(this)).GetAddressByLatlngAsync(inValue);
        }
    }
}

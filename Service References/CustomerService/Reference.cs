﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AirlineApplication.CustomerService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="AbstractPerson", Namespace="http://schemas.datacontract.org/2004/07/AirlineService")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(AirlineApplication.CustomerService.CustomerClass))]
    public partial class AbstractPerson : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Add1Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Add2Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DobField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EmailField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FnameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private char GenderField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LnameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NicField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string PhoneField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Add1 {
            get {
                return this.Add1Field;
            }
            set {
                if ((object.ReferenceEquals(this.Add1Field, value) != true)) {
                    this.Add1Field = value;
                    this.RaisePropertyChanged("Add1");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Add2 {
            get {
                return this.Add2Field;
            }
            set {
                if ((object.ReferenceEquals(this.Add2Field, value) != true)) {
                    this.Add2Field = value;
                    this.RaisePropertyChanged("Add2");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Dob {
            get {
                return this.DobField;
            }
            set {
                if ((this.DobField.Equals(value) != true)) {
                    this.DobField = value;
                    this.RaisePropertyChanged("Dob");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Email {
            get {
                return this.EmailField;
            }
            set {
                if ((object.ReferenceEquals(this.EmailField, value) != true)) {
                    this.EmailField = value;
                    this.RaisePropertyChanged("Email");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Fname {
            get {
                return this.FnameField;
            }
            set {
                if ((object.ReferenceEquals(this.FnameField, value) != true)) {
                    this.FnameField = value;
                    this.RaisePropertyChanged("Fname");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public char Gender {
            get {
                return this.GenderField;
            }
            set {
                if ((this.GenderField.Equals(value) != true)) {
                    this.GenderField = value;
                    this.RaisePropertyChanged("Gender");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Lname {
            get {
                return this.LnameField;
            }
            set {
                if ((object.ReferenceEquals(this.LnameField, value) != true)) {
                    this.LnameField = value;
                    this.RaisePropertyChanged("Lname");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Nic {
            get {
                return this.NicField;
            }
            set {
                if ((object.ReferenceEquals(this.NicField, value) != true)) {
                    this.NicField = value;
                    this.RaisePropertyChanged("Nic");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Phone {
            get {
                return this.PhoneField;
            }
            set {
                if ((object.ReferenceEquals(this.PhoneField, value) != true)) {
                    this.PhoneField = value;
                    this.RaisePropertyChanged("Phone");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="CustomerClass", Namespace="http://Nimaz")]
    [System.SerializableAttribute()]
    public partial class CustomerClass : AirlineApplication.CustomerService.AbstractPerson {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CidField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private AirlineApplication.CustomerService.CustomerClass[] custObjectField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int customerCountField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Cid {
            get {
                return this.CidField;
            }
            set {
                if ((this.CidField.Equals(value) != true)) {
                    this.CidField = value;
                    this.RaisePropertyChanged("Cid");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public AirlineApplication.CustomerService.CustomerClass[] custObject {
            get {
                return this.custObjectField;
            }
            set {
                if ((object.ReferenceEquals(this.custObjectField, value) != true)) {
                    this.custObjectField = value;
                    this.RaisePropertyChanged("custObject");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int customerCount {
            get {
                return this.customerCountField;
            }
            set {
                if ((this.customerCountField.Equals(value) != true)) {
                    this.customerCountField = value;
                    this.RaisePropertyChanged("customerCount");
                }
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CustomerService.ICustomerService")]
    public interface ICustomerService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/addDetails", ReplyAction="http://tempuri.org/ICustomerService/addDetailsResponse")]
        int addDetails(AirlineApplication.CustomerService.CustomerClass customer);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ICustomerService/loadCustomers", ReplyAction="http://tempuri.org/ICustomerService/loadCustomersResponse")]
        AirlineApplication.CustomerService.CustomerClass loadCustomers();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ICustomerServiceChannel : AirlineApplication.CustomerService.ICustomerService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class CustomerServiceClient : System.ServiceModel.ClientBase<AirlineApplication.CustomerService.ICustomerService>, AirlineApplication.CustomerService.ICustomerService {
        
        public CustomerServiceClient() {
        }
        
        public CustomerServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public CustomerServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CustomerServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public CustomerServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int addDetails(AirlineApplication.CustomerService.CustomerClass customer) {
            return base.Channel.addDetails(customer);
        }
        
        public AirlineApplication.CustomerService.CustomerClass loadCustomers() {
            return base.Channel.loadCustomers();
        }
    }
}

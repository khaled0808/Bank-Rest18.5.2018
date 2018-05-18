using BankApplication;
using BankData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace RestBank
{
    [ServiceContract]
    public interface IRestService
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "bank", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ResultData addBank(Bank bank);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Person", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ResultData addCustomer(ResultData person);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "updateAcount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ResultData updateAcount(ResultData person);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "bank")]
        List<ResultData> getAllAcounts();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Person/", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void registPerson(Person person);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "getPayIN/{AcountID}")]
        decimal getPayIN(string AcountID);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "getTakeOff/{AcountID}")]
        decimal getTakeOff(string AcountID);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "getfilterbank/{name}/{marital_status}/{operation}")]
        List<ResultData> getfilterbank(string name, string marital_status, string operation);



        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "getUsersBank/{bankID}")]
        List<ResultData> getUSersBank(string bankID);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "delete", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        DeleteAcount deleteAcount(DeleteAcount delete);

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "getOccupation")]
        string[] getOccupation();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "getMaritalStatus")]
        string[] getMaritalStatus();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "getHealthStatus")]
        string[] getHealthStatus();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "getBankName")]
        string[] getBankName();

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "getCustomerDetails/{personId}")]
        ResultData getCustomerDetails(string personId);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "user", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool AddUser(Login user);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "login", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool login(Login user);

    }
}

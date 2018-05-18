using BankApplication;
using BankData;
using System;
using System.Collections.Generic;
namespace RestBank
{

    public class RestService : IRestService
    {

        public List<Acount> acount { get; private set; }

        public ResultData addBank(Bank bank)
        {

            DataBaseService.addBank(bank);

            return null;
        }

        public ResultData updateAcount(ResultData resultData)
        {
            DataBaseService.updateAcount(resultData);
            return null;
        }

        public ResultData addCustomer(ResultData resultData)
        {

            DataBaseService.addCustomer(resultData);

            return null;
        }

        public List<ResultData> getAllAcounts()
        {
            return DataBaseService.getAllAcounts();
        }

        public decimal getPayIN(string acountId)
        {
            return DataBaseService.getPayIN(acountId);
        }


        public decimal getTakeOff(string acountId)
        {
            return DataBaseService.getTakeOff(acountId);
        }

        public List<ResultData> getUSersBank(string bankID)
        {
            return DataBaseService.getUSersBank(bankID);
        }


        public List<ResultData> getfilterbank(string name, string marital_status, string operation)
        {
            string guid = Guid.NewGuid().ToString();
            return DataBaseService.getfilterbank(name, marital_status, operation);
        }

        public void registPerson(Person person)
        {
            DataBaseService.registPerson(person);
        }


        public DeleteAcount deleteAcount(DeleteAcount delete)
        {
            string guid = Guid.NewGuid().ToString();
            return DataBaseService.deleteAcount(delete);
        }

        public ResultData getCustomerDetails(string personId)
        {
            return DataBaseService.getCustomerDetails(personId);
        }

        public string[] getBankName()
        {
            return DataBaseService.getBankName();
        }

        public string[] getOccupation()
        {
            return DataBaseService.getOccupation();
        }
        public string[] getHealthStatus()
        {
            return DataBaseService.getHealthStatus();
        }
        public string[] getMaritalStatus()
        {
            return DataBaseService.getMaritalStatus();
        }

        public bool login(Login login)
        {
            return DataBaseService.login(login);
        }

        public bool AddUser(Login user)
        {
            try
            {
                return DataBaseService.user(user);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
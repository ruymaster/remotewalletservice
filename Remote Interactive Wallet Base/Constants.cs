using System;
using System.Collections.Generic;
using System.Text;

namespace Remote_Interactive_Wallet_Base
{
    public static class Constants
    {
        public const string GetAddressesByVaultIndex = "{coinSymbol}/get-addresses/{vaultIndex}/";
        public const string GetAllDepositsForAddress = "{coinSymbol}/get-deposits/{publicAddress}/{vaultIndex}/";
        public const string GetRecentDeposits = "{coinSymbol}/get-recent-deposits/{pageIndex}/{vaultIndex}/";
        public const string GetBlockchainHeight = "{coinSymbol}/get-blockchain-height/";
        public const string UpdateWatchedAddresses = "{coinSymbol}/update-watched-addresses/{addr}/{vaultIndex}/";
        public const string UpdateWatchedTXs = "{coinSymbol}/update-watched-txs/";
        public const string GetAvailableBalance = "{coinSymbol}/get-available-balance/{vaultIndex}/{index}/";
        public const string ExecuteTransfersPackage = "{coinSymbol}/execute-transfers-package/{moveAll}/{vaultIndex}/";
        public const string GetTXConfirmationsCount = "{coinSymbol}/get-tx-confirmations-count/{txHash}/{vaultIndex}/";
        public const string GetFeeForTX = "{coinSymbol}/get-fee-for-tx/{tx}/{vaultIndex}/{when}/";

        public const int NumberOfAddressesPerVault = 1000;

        public static string FormatKnownParameter(this string original, string parameterName, object parameterValue)
        {
            string value = "";
            try
            {
                if (parameterValue != null)
                {
                    value = parameterValue.ToString();
          if (parameterValue.GetType() == DateTime.Now.GetType())
          {
            value = ((DateTime)parameterValue).ToString("yyyy-MM-dd");
          }
                }
            }
            catch (Exception)
            {
                //datetime notnullable!
            }
            return original.Replace("{" + parameterName + "}", value);
        }
    }
}

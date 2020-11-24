using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace Remote_Interactive_Wallet_Base
{
  public class Client : WalletEndpoints
  {
    public string CoinSymbol { get; }

    public Client(string coinSymbol)
    {
      this.CoinSymbol = coinSymbol;
    }

    public string ServerURL { get; set; }


    public IEnumerable<Deposit> GetRecentDeposits(int pageIndex = 0, int vaultIndex = 0)
    {
      var ret =
          BasicWalletAPI.ExecuteRequest<WalletResponseDataWrapper<IEnumerable<Deposit>>>(
              ServerURL,
              System.Net.Http.HttpMethod.Get,
              Constants.GetRecentDeposits.FormatKnownParameter("pageIndex", pageIndex)
                  .FormatKnownParameter("coinSymbol", CoinSymbol)
                  .FormatKnownParameter("vaultIndex", vaultIndex),
              null);
      return ret.WrappedData;
    }


    public IEnumerable<Deposit> GetAllDepositsForAddress(string publicAddress, int vaultIndex = 0)
    {
      var ret =
          BasicWalletAPI.ExecuteRequest<WalletResponseDataWrapper<IEnumerable<Deposit>>>(
              ServerURL,
              System.Net.Http.HttpMethod.Get,
              Constants.GetAllDepositsForAddress.FormatKnownParameter("publicAddress", publicAddress)
                  .FormatKnownParameter("coinSymbol", CoinSymbol)
                  .FormatKnownParameter("vaultIndex", vaultIndex),
              null);
      return ret.WrappedData;
    }

    public IEnumerable<string> GetPublicAddresses(int vaultIndex = 0)
    {
      WalletResponseDataWrapper<List<string>> ret =
          BasicWalletAPI.ExecuteRequest<WalletResponseDataWrapper<List<string>>>(
              ServerURL,
              System.Net.Http.HttpMethod.Get,
              Constants.GetAddressesByVaultIndex.FormatKnownParameter("vaultIndex", vaultIndex)
                  .FormatKnownParameter("coinSymbol", CoinSymbol),
              null);


      return ret.WrappedData;
    }


    public long GetBlockchainHeight()
    {
      WalletResponseDataWrapper<long> ret =
          BasicWalletAPI.ExecuteRequest<WalletResponseDataWrapper<long>>(
              ServerURL,
              System.Net.Http.HttpMethod.Get,
              Constants.GetBlockchainHeight
                  .FormatKnownParameter("coinSymbol", CoinSymbol),
              null);


      return ret.WrappedData;
    }

    public bool UpdateWatchedAddresses(string addr, int vaultIndex)
    {

      WalletResponseDataWrapper<bool> ret =
          BasicWalletAPI.ExecuteRequest<WalletResponseDataWrapper<bool>>(
              ServerURL,
              System.Net.Http.HttpMethod.Get,
              Constants.UpdateWatchedAddresses
                  .FormatKnownParameter("coinSymbol", CoinSymbol)
                  .FormatKnownParameter("addr", addr)
                  .FormatKnownParameter("vaultIndex", vaultIndex),
              null);


      return ret.WrappedData;
    }

    public bool UpdateWatchedTXs()
    {

      WalletResponseDataWrapper<bool> ret =
          BasicWalletAPI.ExecuteRequest<WalletResponseDataWrapper<bool>>(
              ServerURL,
              System.Net.Http.HttpMethod.Get,
              Constants.UpdateWatchedTXs
                  .FormatKnownParameter("coinSymbol", CoinSymbol),
              null);


      return ret.WrappedData;
    }

    private decimal RawGetAvailableBalance(int vaultIndex, int index)
    {
      WalletResponseDataWrapper<decimal> ret =
                      BasicWalletAPI.ExecuteRequest<WalletResponseDataWrapper<decimal>>(
                          ServerURL,
                          System.Net.Http.HttpMethod.Get,
                          Constants.GetAvailableBalance.FormatKnownParameter("vaultIndex", vaultIndex)
                              .FormatKnownParameter("coinSymbol", CoinSymbol)
                              .FormatKnownParameter("index", index),
                          null);


      return ret.WrappedData;
    }
    public decimal GetAvailableBalance(int vaultIndex, int index)
    {
      decimal ret = 0;
      try
      {
        ret = RawGetAvailableBalance(vaultIndex, index);
      }
      catch (Exception)
      {
        try
        {
          System.Threading.Thread.Sleep(2000);
          ret = RawGetAvailableBalance(vaultIndex, index);

        }
        catch (Exception) { }
      }
      return ret;
    }
    public decimal GetFeeForTX(string tx, int vaultIndex, DateTime when)
    {
      WalletResponseDataWrapper<decimal> ret =
          BasicWalletAPI.ExecuteRequest<WalletResponseDataWrapper<decimal>>(
              ServerURL,
              System.Net.Http.HttpMethod.Get,
              Constants.GetFeeForTX.FormatKnownParameter("vaultIndex", vaultIndex)
                  .FormatKnownParameter("coinSymbol", CoinSymbol)
                  .FormatKnownParameter("when", when)
                  .FormatKnownParameter("tx", tx),
              null);


      return ret.WrappedData;
    }

    public IEnumerable<OutgoingTransferOrder> ExecuteTransfersPackage(IEnumerable<OutgoingTransferOrder> orders, int moveAll, int vaultIndex)
    {

      WalletResponseDataWrapper<IEnumerable<OutgoingTransferOrder>> ret =
          BasicWalletAPI.ExecuteRequest<WalletResponseDataWrapper<IEnumerable<OutgoingTransferOrder>>>(
              ServerURL,
              System.Net.Http.HttpMethod.Post,
              Constants.ExecuteTransfersPackage.FormatKnownParameter("moveAll", moveAll)
                  .FormatKnownParameter("coinSymbol", CoinSymbol)
                  .FormatKnownParameter("vaultIndex", vaultIndex),
              orders);


      return ret.WrappedData;
    }

    public long GetTXConfirmationsCount(string txHash, int vaultIndex)
    {
      WalletResponseDataWrapper<long> ret =
          BasicWalletAPI.ExecuteRequest<WalletResponseDataWrapper<long>>(
              ServerURL,
              System.Net.Http.HttpMethod.Post,
              Constants.GetTXConfirmationsCount.FormatKnownParameter("txHash", txHash)
                  .FormatKnownParameter("coinSymbol", CoinSymbol)
                  .FormatKnownParameter("vaultIndex", vaultIndex),
              null);


      return ret.WrappedData;
    }
    private int ConvertToUnixTimestamp(DateTime date)
    {
      DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
      TimeSpan diff = date - origin;
      return (int)diff.TotalSeconds;
    }
    public string GetServiceRawRequestResponse(string path, Dictionary<string, string> queryParams)
    {
      return GetServiceRawRequestResponse(path, queryParams, System.Net.Http.HttpMethod.Get);
    }
    public string GetServiceRawRequestResponse(string path, Dictionary<string, string> queryParams, HttpMethod method)
    {
      if (method == null)
      {
        method = System.Net.Http.HttpMethod.Get;
      }
      //WalletResponseDataWrapper<long> ret =
      //    BasicWalletAPI.ExecuteRequest<WalletResponseDataWrapper<long>>(
      //        ServerURL,
      //        System.Net.Http.HttpMethod.Post,
      //        Constants.GetTXConfirmationsCount.FormatKnownParameter("txHash", txHash)
      //            .FormatKnownParameter("coinSymbol", CoinSymbol)
      //            .FormatKnownParameter("vaultIndex", vaultIndex),
      //        null);


      //return ret.WrappedData;

      string ret = "";

      using (var client = new HttpClient())
      {
        string nonce = ConvertToUnixTimestamp(DateTime.UtcNow).ToString().PadRight(13, '0'); // "154664555733150000"; //DateTime.UtcNow.Ticks;

        FormUrlEncodedContent content = new FormUrlEncodedContent(queryParams);

        string paramData = content.ReadAsStringAsync().Result;

        string hashable = "";
        foreach (var x in queryParams)
        {
          hashable += "&" + x.Key + "=" + x.Value;
        }
        hashable = hashable.Substring(1);




        //paramData = paramData;
        //string pathPlusParams = path;

        if (method.Method == HttpMethod.Get.Method && queryParams.Count > 0)
        {
          path = path + "?" + paramData;
        }
        //if (queryParams.Count > 0)
        //{
        //    pathPlusParams = pathPlusParams + "?" + paramData;
        //}

        string message = ConfigurationManager.AppSettings["apiKey"] + "&" + nonce + path;// PlusParams;

        if (method.Method == HttpMethod.Post.Method)
        {
          message += "?" + hashable; //
        }
        var encoding = Encoding.UTF8;

        //message = "";

        byte[] messageBytes = encoding.GetBytes(message);
        byte[] secret = encoding.GetBytes(ConfigurationManager.AppSettings["messageSecret"]);
        HMACSHA512 hmac = new HMACSHA512(secret);
        byte[] hmacResult = hmac.ComputeHash(messageBytes);
        string hex = String.Concat(Array.ConvertAll(hmacResult, x => x.ToString("x2"))); // https://github.com/danharper/hmac-examples

        string signature = hex.ToLower();

        client.DefaultRequestHeaders.Add("X-API-KEY", ConfigurationManager.AppSettings["apiKey"]);
        client.DefaultRequestHeaders.Add("X-Nonce", nonce.ToString());
        client.DefaultRequestHeaders.Add("X-Signature", signature);

        HttpResponseMessage resp;
        if (method.Method == HttpMethod.Post.Method)
        {
          var request = new HttpRequestMessage(HttpMethod.Post, ConfigurationManager.AppSettings["NexPayServer"] + path); //

          request.Content = new StringContent(paramData, encoding);
          request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded")
          {
            CharSet = encoding.HeaderName
          };
          request.Content.Headers.ContentLength = paramData.Length;
          resp = client.SendAsync(request).Result;
        }
        else
        {
          resp = client.GetAsync(ConfigurationManager.AppSettings["NexPayServer"] + path).Result;
        }
        ret = resp.Content.ReadAsStringAsync().Result;

      }
      return ret;
    }

  }
}

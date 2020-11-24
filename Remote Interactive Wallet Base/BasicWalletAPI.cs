using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Remote_Interactive_Wallet_Base
{
    class BasicWalletAPI
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="relativeURI">Simple relative URI, without /api-v1/</param>
        /// <param name="requestData"></param>
        /// <returns></returns>
        public static T ExecuteRequest<T>(string host, HttpMethod verb, string relativeURI, object requestData) where T : WalletResponseData, new()
        {
            T ret = null;
            using (var client = new HttpClient())
            {
                try
                {

                    client.BaseAddress = new Uri(host); //"http://ldvwalletbtc.azurewebsites.net/");

                    client.Timeout = new TimeSpan(0, 30, 0);
                    string walletRequest = "";
                    //Type tSource = requestData.GetType();

                    HttpResponseMessage result;
                    if (verb == HttpMethod.Post)
                    {
                        EncryptedPayloadWrapper<object> wrapper = new EncryptedPayloadWrapper<object>(requestData);
                        walletRequest = wrapper.ToString();
                        var data = new StringContent(walletRequest, System.Text.Encoding.UTF8);

                        result = client.PostAsync(relativeURI, data).Result;
                    }
                    else if (verb == HttpMethod.Get)
                    {
                        result = client.GetAsync(relativeURI).Result;
                    }
                    else
                    {
                        throw new NotImplementedException("Verb not implemented yet");
                    }
                    string content = result.Content.ReadAsStringAsync().Result;
                    
                    try
                    {
                        EncryptedPayloadWrapper<T> retWrap = new EncryptedPayloadWrapper<T>(content);
                        ret = retWrap.WrappedObject;
                        ret.IsSuccessStatusCode = result.IsSuccessStatusCode;
                        //ret.StatusCode = (int)result.StatusCode;
                        ret.DateTimeResponseArrived = new DateTime(DateTime.UtcNow.Ticks, DateTimeKind.Utc);
                    }
                    catch (Exception exx)
                    {
                        exx.ToString();
                        if (ret == null)
                        {
                            ret = new T();
                        }
                        ret.IsSuccessStatusCode = false;
                        ret.DateTimeResponseArrived = new DateTime(DateTime.UtcNow.Ticks, DateTimeKind.Utc);
                        ret.StatusCode = -1;
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    if (ret == null)
                    {
                        ret = new T();
                    }
                    ret.IsSuccessStatusCode = false;
                    ret.DateTimeResponseArrived = new DateTime(DateTime.UtcNow.Ticks, DateTimeKind.Utc);
                    ret.StatusCode = -1;
                }

            }
            return ret;
        }

    }
}

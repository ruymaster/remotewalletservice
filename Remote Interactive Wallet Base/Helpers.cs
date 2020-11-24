using System;
using System.Collections.Generic;
using System.Text;

namespace Remote_Interactive_Wallet_Base
{
  public static class BTCWalletHelpers
  {

    public static byte[] HexStringToBytes(string data)
    {
      List<byte> result = new List<byte>();
      for (int i = 0; i <= data.Length - 1; i += 2)
        result.Add(Convert.ToByte(data.Substring(i, 2), 16));
      return result.ToArray();
    }


    public static string ObjectToEncryptedJSON(object obj)
    {
      string ret = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

      WalletEncryptionV2 enc = new WalletEncryptionV2();
      string secured = "";
      if (!string.IsNullOrEmpty(ret))
      {
        try
        {
          secured = enc.encrypt(ret);

        }
        catch (Exception ex)
        {
          ex.ToString();
        }
      }

      return secured;
    }
    public static object ObjectFromEncryptedJSON(string data)
    {
      WalletEncryptionV2 enc = new WalletEncryptionV2();
      string secured = enc.Decrypt(data);

      return Newtonsoft.Json.JsonConvert.DeserializeObject(secured);
    }
    public static T ObjectFromEncryptedJSON<T>(string data)
    {
      WalletEncryptionV2 enc = new WalletEncryptionV2();
      try
      {
        string secured = enc.Decrypt(data);
        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(secured);
      }
      catch (Exception ex)
      {
        ex.ToString();

      }
      return default(T);
    }

    //public static EncryptedActionResult<T> EncryptedObjectAsJSON<T>(this Controller controller, T obj)
    //{
    //    return new EncryptedActionResult<T>(obj);
    //}

  }

  public class EncryptedPayloadWrapper<T>
  {
    public string AuthKey { get; set; }

    public string Payload { get; set; }

    //[NonSerialized]
    public T WrappedObject { get; set; }

    /// <summary>
    /// Used by JSON deserializer
    /// </summary>
    public EncryptedPayloadWrapper() { }

    public EncryptedPayloadWrapper(T obj)
    {
      WrappedObject = obj;
      Payload = BTCWalletHelpers.ObjectToEncryptedJSON(obj);
      AuthKey = "";
    }

    public EncryptedPayloadWrapper(string received)
    {
      if (!string.IsNullOrEmpty(received))
      {
        EncryptedPayloadWrapper<T> enc = BTCWalletHelpers.ObjectFromEncryptedJSON<EncryptedPayloadWrapper<T>>(received);
        this.AuthKey = enc.AuthKey;
        this.Payload = enc.Payload;
        this.WrappedObject = BTCWalletHelpers.ObjectFromEncryptedJSON<T>(this.Payload);
      }
    }

    public override string ToString()
    {
      return BTCWalletHelpers.ObjectToEncryptedJSON(this);
    }
  }





}

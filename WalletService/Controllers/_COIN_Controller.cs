using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Remote_Interactive_Wallet_Base;
using Remote_Interactive_Wallet_Base.Models;

namespace Interactive_Wallet_Skeleton.Controllers
{

  public class _COIN_WalletEndpoints : WalletEndpoints
  {
    public string ServerURL { get; set; }

    public IEnumerable<Deposit> GetRecentDeposits(int pageIndex, int vaultIndex)
    {
      List<Deposit> ret = new List<Deposit>();

      return ret;
    }

    public IEnumerable<Deposit> GetAllDepositsForAddress(string publicAddress, int vaultIndex)
    {
      throw new NotImplementedException();
    }

    public long GetBlockchainHeight()
    {
      throw new NotImplementedException();
    }

    public IEnumerable<string> GetPublicAddresses(int vaultIndex)
    {
      List<string> ret = new List<string>();

      return ret;
    }

    public bool UpdateWatchedAddresses(string addr, int vaultIndex)
    {
      throw new NotImplementedException();
    }

    public bool UpdateWatchedTXs()
    {
      throw new NotImplementedException();
    }

    public decimal GetAvailableBalance(int vaultIndex, int index)
    {
      return 0;
    }
    public IEnumerable<OutgoingTransferOrder> ExecuteTransfersPackage(IEnumerable<OutgoingTransferOrder> orders, int moveAll, int vaultIndex)
    {
      List<OutgoingTransferOrder> doable = new List<OutgoingTransferOrder>();

      return doable;
    }

    public decimal GetFeeForTX(string tx, int vaultIndex, DateTime when)
    {
      decimal ret = 0m;

      return ret;
    }

    public long GetTXConfirmationsCount(string txHash, int vaultIndex)
    {
      return 0;
    }
  }

  public class _COIN_Controller : Controller
  {
    private _COIN_WalletEndpoints _coinw_ = new _COIN_WalletEndpoints();


    public _COIN_Controller() { }

    #region GetRecentDeposits

    [HttpGet]
    [Route(Constants.GetRecentDeposits)]
    public EncryptedActionResult<WalletResponseDataWrapper<IEnumerable<Deposit>>> GetRecentDeposits(string coinSymbol, int pageIndex, int vaultIndex)
    {
      if (coinSymbol == "eur")
      {
        return new EncryptedActionResult<WalletResponseDataWrapper<IEnumerable<Deposit>>>(
          new WalletResponseDataWrapper<IEnumerable<Deposit>>(
            _coinw_
            .GetRecentDeposits(pageIndex, vaultIndex)
          )
        );
      }
      else
      {
        return null;
      }
    }

    #endregion

    #region GetPublicAddresses

    [HttpGet]
    [Route(Constants.GetAddressesByVaultIndex)]
    public EncryptedActionResult<WalletResponseDataWrapper<IEnumerable<string>>> APIGetPublicAddresses(string coinSymbol, int vaultIndex)
    {
      if (coinSymbol == "eur")
      {
        IEnumerable<string> addresses = _coinw_.GetPublicAddresses(vaultIndex);

        return new EncryptedActionResult<WalletResponseDataWrapper<IEnumerable<string>>>(
          new WalletResponseDataWrapper<IEnumerable<string>>(addresses)
        );
      }
      else
      {
        return null;
      }
    }

    #endregion

    #region GetAllDepositsForAddress

    [HttpGet]
    [Route(Constants.GetAllDepositsForAddress)]
    public EncryptedActionResult<WalletResponseDataWrapper<IEnumerable<Deposit>>> APIGetAllDepositsForAddress(string coinSymbol, string publicAddress, int vaultIndex)
    {
      if (coinSymbol == "eur")
      {
        return new EncryptedActionResult<WalletResponseDataWrapper<IEnumerable<Deposit>>>(
          new WalletResponseDataWrapper<IEnumerable<Deposit>>(
           _coinw_
           .GetAllDepositsForAddress(publicAddress, vaultIndex)
          )
        );
      }
      else
      {
        return null;
      }
    }

    #endregion

    #region GetBlockchainHeight

    [HttpGet]
    [Route(Constants.GetBlockchainHeight)]
    public EncryptedActionResult<WalletResponseDataWrapper<long>> APIGetBlockchainHeight(string coinSymbol)
    {
      if (coinSymbol == "eur")
      {
        return new EncryptedActionResult<WalletResponseDataWrapper<long>>(new WalletResponseDataWrapper<long>(0));
      }
      else
      {
        return null;
      }
    }


    #endregion

    #region UpdateWatchedAddresses

    [HttpGet]
    [Route(Constants.UpdateWatchedAddresses)]
    public EncryptedActionResult<WalletResponseDataWrapper<bool>> APIUpdateWatchedAddresses(string coinSymbol, string addr, int vaultIndex)
    {
      if (coinSymbol == "eur")
      {
        return new EncryptedActionResult<WalletResponseDataWrapper<bool>>(new WalletResponseDataWrapper<bool>(
          _coinw_
          .UpdateWatchedAddresses(addr, vaultIndex)));
      }
      else
      {
        return null;
      }
    }


    #endregion

    #region UpdateWatchedTXs

    [HttpGet]
    [Route(Constants.UpdateWatchedTXs)]
    public EncryptedActionResult<WalletResponseDataWrapper<bool>> APIUpdateWatchedTXs(string coinSymbol)
    {
      return null;
    }
    #endregion

    #region GetAvailableBalance

    [HttpGet]
    [Route(Constants.GetAvailableBalance)]
    public EncryptedActionResult<WalletResponseDataWrapper<decimal>> GetAvailableBalance(string coinSymbol, int vaultIndex, int index)
    {
      if (coinSymbol == "eur")
      {
        return new EncryptedActionResult<WalletResponseDataWrapper<decimal>>(
          new WalletResponseDataWrapper<decimal>(
            _coinw_
            .GetAvailableBalance(vaultIndex, index)
          )
        );
      }
      else
      {
        return null;
      }
    }

    #endregion


    #region GetFeeForTX

    [HttpGet]
    [Route(Constants.GetFeeForTX)]
    public EncryptedActionResult<WalletResponseDataWrapper<decimal>> GetFeeForTX(string coinSymbol, int vaultIndex, string tx, DateTime when)
    {
      if (coinSymbol == "eur")
      {
        return new EncryptedActionResult<WalletResponseDataWrapper<decimal>>(
          new WalletResponseDataWrapper<decimal>(
            _coinw_
            .GetFeeForTX(tx, vaultIndex, when)
          )
        );
      }
      else
      {
        return null;
      }
    }

    #endregion

    #region ExecuteTransfersPackage

    [HttpPost]
    [Route(Constants.ExecuteTransfersPackage)]
    public EncryptedActionResult<WalletResponseDataWrapper<IEnumerable<OutgoingTransferOrder>>> ExecuteTransfersPackage(string coinSymbol, int moveAll, int vaultIndex)
    {
      if (coinSymbol == "eur")
      {
        Request.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
        string rawData = new System.IO.StreamReader(Request.InputStream).ReadToEnd();

        EncryptedPayloadWrapper<IEnumerable<OutgoingTransferOrder>> retWrap = new EncryptedPayloadWrapper<IEnumerable<OutgoingTransferOrder>>(rawData);
        IEnumerable<OutgoingTransferOrder> orders = retWrap.WrappedObject;

        return new EncryptedActionResult<WalletResponseDataWrapper<IEnumerable<OutgoingTransferOrder>>>(
          new WalletResponseDataWrapper<IEnumerable<OutgoingTransferOrder>>(
            _coinw_
            .ExecuteTransfersPackage(orders, moveAll, vaultIndex)
          )
        );
      }
      else
      {
        return null;
      }
    }

    #endregion
  }
}

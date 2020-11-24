﻿using binance.dex.sdk.httpapi;
using binance.dex.sdk.message;
using binance.dex.sdk.model;
using NBitcoin;
using NBitcoin.DataEncoders;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;

namespace binance.dex.sdk.crypto
{
    public class Wallet
    {
        private const string hdPath = "44'/714'/0'/0/0";
        private readonly Dictionary<BinanceDexEnvironmentData, string> chainIds = new Dictionary<BinanceDexEnvironmentData, string>();
        private readonly HttpApiClient client;

        public string PrivateKey { get; set; }
        public string Mnemonic { get; set; }
        public string Address { get; set; }
        public Key EcKey { get; set; }
        public byte[] AddressBytes { get; set; }
        public byte[] PubKeyForSign { get; set; }
        public long? AccountNumber { get; set; }
        public long? Sequence { get; set; }
        public BinanceDexEnvironmentData Env { get; set; }
        public string ChainId { get; set; }

        const string pbase32 = "abcdefghijklmnopqrstuvwxyz234567";

        public Wallet(Key key, BinanceDexEnvironmentData env)
        {
            EcKey = key;
            Env = env;
            Bech32Encoder bech32Encoder = Encoders.Bech32(Env.Hrp);
            AddressBytes = EcKey.PubKey.Hash.ToBytes();
            string b32 = Encoders.Base32.EncodeData(AddressBytes);
            byte[] address32 = new byte[b32.Length];
            for (int i = 0; i < b32.Length; i++)
            {
                address32[i] = (byte)pbase32.IndexOf(b32[i]);
            }
            Address = bech32Encoder.EncodeData(address32);
            byte[] pubKey = EcKey.PubKey.ToBytes();
            byte[] pubKeyPrefix = MessageType.GetTransactionType(EMessageType.PubKey);
            PubKeyForSign = new byte[pubKey.Length + pubKeyPrefix.Length + 1];
            pubKeyPrefix.CopyTo(PubKeyForSign, 0);
            PubKeyForSign[pubKeyPrefix.Length] = 33;
            pubKey.CopyTo(PubKeyForSign, pubKeyPrefix.Length + 1);

            client = new HttpApiClient(Env);
        }

        public static Wallet FromPrivateKey(string privateKey, BinanceDexEnvironmentData env)
        {
            BigInteger privateKeyBigInteger = BigInteger.Parse("0" + privateKey, NumberStyles.HexNumber); // append "0" to get unsigned BigInteger
            //Key key = new Key(privateKeyBigInteger.ToByteArray(true, true));
            Key key = new Key(privateKeyBigInteger.ToByteArray()); // for .net standard framework
            Wallet wallet = new Wallet(key, env);
            wallet.PrivateKey = privateKey;
            return wallet;
        }

        public static Wallet FromMnemonicCode(string mnemonicCode, BinanceDexEnvironmentData env)
        {
            Mnemonic mnemonic = new Mnemonic(mnemonicCode);
            byte[] seed = mnemonic.DeriveSeed();
            ExtKey extKey = new ExtKey(seed);
            KeyPath keyPath = KeyPath.Parse(hdPath);
            ExtKey derived = extKey.Derive(keyPath);
            Wallet wallet = new Wallet(derived.PrivateKey, env);
            wallet.Mnemonic = mnemonicCode;
            return wallet;
        }

        public static Wallet FromRandomMnemonicCode(BinanceDexEnvironmentData env)
        {
            Mnemonic mnemonic = new Mnemonic(Wordlist.English, WordCount.TwentyFour);
            return FromMnemonicCode(mnemonic.ToString(), env);
        }

        public byte[] DecodeAddress(string address)
        {
            Bech32Decoder bech32Decoder = new Bech32Decoder(Env.Hrp);
            return bech32Decoder.DecodeData(address);
        }

        public void InitAccount()
        {
            Account account = client.Account(Address);
            if (account != null)
            {
                AccountNumber = account.AccountNumber;
                Sequence = account.Sequence;
            }
            else
            {
                throw new InvalidOperationException(
                    "Cannot get account information for address " + Address +
                        " (does this account exist on the blockchain yet?)");
            }
        }

        public void ReloadAccountSequence()
        {
            AccountSequence accountSequence = client.AccountSequence(Address);
            Sequence = accountSequence.Sequence;
        }

        public void EnsureWalletIsReady()
        {
            if (AccountNumber == null)
            {
                InitAccount();
            }
            else if (Sequence == null)
            {
                ReloadAccountSequence();
            }

            if (ChainId == null)
            {
                //ChainId = chainIds.GetValueOrDefault(Env);

                /// for .netstandard framework
                string resultId;
                var result = chainIds.TryGetValue(Env, out resultId);
                if (result)
                    ChainId = resultId;
                else
                    ChainId = null;
                ////
                if (ChainId == null)
                {
                    InitChainId(client);
                }
            }
        }

        public void InitChainId(HttpApiClient client)
        {
            Infos info = client.NodeInfo();
            ChainId = info.NodeInfo.Network;
            chainIds.Add(Env, ChainId);
        }
    }
}

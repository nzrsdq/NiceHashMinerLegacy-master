﻿using NiceHashMiner.Devices;
using NiceHashMiner.Enums;
using NiceHashMiner.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NiceHashMiner.Miners
{
    public static class MinersManager
    {
        private static MiningSession _curMiningSession;

        public static void StopAllMiners()
        {
            _curMiningSession?.StopAllMiners();
            _curMiningSession = null;
        }

        public static void StopAllMinersNonProfitable()
        {
            _curMiningSession?.StopAllMinersNonProfitable();
        }

        public static string GetActiveMinersGroup()
        {
            // if no session it is idle
            return _curMiningSession != null ? _curMiningSession.GetActiveMinersGroup() : "IDLE";
        }

        public static List<int> GetActiveMinersIndexes()
        {
            return _curMiningSession != null ? _curMiningSession.ActiveDeviceIndexes : new List<int>();
        }

        public static double GetTotalRate()
        {
            return _curMiningSession?.GetTotalRate() ?? 0;
        }

        public static bool StartInitialize(IMainFormRatesComunication mainFormRatesComunication,
            string miningLocation, string worker, string btcAdress)
        {
            _curMiningSession = new MiningSession(ComputeDeviceManager.Avaliable.AllAvaliableDevices,
                mainFormRatesComunication, miningLocation, worker, btcAdress);

            return _curMiningSession.IsMiningEnabled;
        }

        public static bool IsMiningEnabled()
        {
            return _curMiningSession != null && _curMiningSession.IsMiningEnabled;
        }


        /// <summary>
        /// SwichMostProfitable should check the best combination for most profit.
        /// Calculate profit for each supported algorithm per device and group.
        /// </summary>
        /// <param name="niceHashData"></param>
        public static async Task SwichMostProfitableGroupUpMethod(Dictionary<AlgorithmType, NiceHashSma> niceHashData)
        {
            if (_curMiningSession != null) await _curMiningSession.SwichMostProfitableGroupUpMethod(niceHashData);
        }

        public static async Task MinerStatsCheck(Dictionary<AlgorithmType, NiceHashSma> niceHashData)
        {
            if (_curMiningSession != null) await _curMiningSession.MinerStatsCheck(niceHashData);
        }
    }
}

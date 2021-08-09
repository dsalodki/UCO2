using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UCO2.Models
{
    public class ViewModel
    {
        public decimal StartAmount { get; set; }
        public int PlayersNumber { get; set; }
    }

    public class InputParams
    {

        public decimal InitAmount { get; set; }
        public decimal BalanceAmount { get; set; }
        public int CountPeople { get; set; }

    }

    public class PaymentService
    {
        private object _lockObj = new object();
        private InputParams inputParams;

        public PaymentService(InputParams inputParams)
        {
            this.inputParams = inputParams;
        }

        public async Task<InputParams> Start()
        {
            this.inputParams.BalanceAmount = this.inputParams.InitAmount;
            List<Task> players = new List<Task>();
            for (var i = 0; i < this.inputParams.CountPeople; i++)
            {
                var task = new Task(TakeAmount);
                players.Add(task);
            }
            players.ForEach(x => x.Start());

            Task.WaitAll(players.ToArray());
            return this.inputParams;
        }

        private void TakeAmount()
        {

            while (true)
            {
                lock (_lockObj)
                {
                    if (CanTakeMore())
                    {
                        //Delay for data base transaction
                        Task.Delay(100).Wait();
                        this.inputParams.BalanceAmount = this.inputParams.BalanceAmount - 0.1M;
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        private bool CanTakeMore()
        {
            if (this.inputParams.BalanceAmount > 1.1M)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookScraper
{
    class SafeToPull
    {
        private string _ip = "70.91.210.154";

        private bool _isSafe;

        public bool IsSafe
        {
            get
            {
                return _isSafe;
            }
            private set
            {
                _isSafe = value;
            }
        }

        public bool isSafePull()
        {
            bool val = isSameIp();
            if (!val /*&& timerWatch()*/)
            {
                return true;
            }
            else
                return false;
        }

        public bool isSameIp ()
        {
            try
            {
                string pubIp = new System.Net.WebClient().DownloadString("https://api.ipify.org");
                if (this._ip.Equals(pubIp))
                {
                    this._isSafe = true;
                    return true;
                }
                this._isSafe = false;
                return false;
            }catch(Exception e)
            {
                return false;
            }
        }

        public bool timerWatch()
        {
            return false;
        }
    }
}
